using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using LibraryScreen;
using static LibraryScreen.ScreenProtocol;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            list1.ItemsSource = dataByte;
        }
        
        UdpClient client = null;
        HeaderPacket header; 
        IPEndPoint remoteEp = null;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new UdpClient();
            dataByte.Clear();

            const int port = 2020;

            var info = Encoding.UTF8.GetBytes("GetScreen");
            client.Connect("127.0.0.1", port);

            var count = client.Send(info, info.Length);
            test.Content = ($"Get Send!");

            ReadFileStream(client);
        }
        
        private void ReadFileStream(UdpClient client)
        {
            byte[] receive = client.Receive(ref remoteEp);
            header = receive.Deserializer<HeaderPacket>(); 

            Thread t = new Thread(Load) { IsBackground = true };
            t.Start();
        }
        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        public ObservableCollection<Packet> dataByte = new ObservableCollection<Packet>();
        private void Add(Packet packet)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    dataByte.Add(packet);
                    using (MemoryStream memStream = new MemoryStream(header.Size))
                    {
                        foreach (var item in dataByte)
                        {
                            memStream.Position = item.StartRead;
                            memStream.Write(item.Data, 0, (int)item.EndRead);
                        }

                        memStream.Position = 0;
                        var byt = new byte[header.Size];
                        memStream.Read(byt, 0, header.Size);

                        image1.Source = ConvertByteArrayToBitmapImage(byt);
                    }
                }));
        }

        void Load()
        {
            do
            {
                try
                {
                    Add(SerializerDeserializerExtensions.Deserializer<Packet>(client.Receive(ref remoteEp)));
                }
                catch { }
            } 
            while (dataByte.Count < header.PacketCount);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            client.Close();
        }
    }
}
