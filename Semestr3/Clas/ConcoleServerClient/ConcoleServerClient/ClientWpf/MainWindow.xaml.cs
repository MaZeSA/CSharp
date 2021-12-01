using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Port = 2020;
        private const int Size = 100;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartClient((sender as Button).Tag.ToString());
        }
      
        private void StartClient(string c)
        {
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = entry.AddressList[0];

            Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.BeginConnect(new IPEndPoint(ip, Port), ConnectCallback, new CommandClass { Client = client, Command = c });
            }
            catch (SocketException e)
            {
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            var command = (CommandClass)ar.AsyncState;
            command.Client.EndConnect(ar);

            var data = Encoding.UTF8.GetBytes(command.Command);

            command.Client.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, command.Client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            var client = (Socket)ar.AsyncState;
            var countBytes = client.EndSend(ar);
            Console.WriteLine("Send to server {0} bytes", countBytes);

            ReceiveFrom(client);
        }

        private void ReceiveFrom(Socket client)
        {
            var buffer = new byte[Size];

            var data = new
            {
                Socket = client,
                Buffer = buffer,
                Size = Size
            };

            client.BeginReceive(buffer, 0, Size, SocketFlags.None, ReceiveCallback, data);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var data = (dynamic)ar.AsyncState;
            var client = (Socket)data.Socket;
            var buffer = (byte[])data.Buffer;

            var countBytes = client.EndReceive(ar);

            var responce = Encoding.UTF8.GetString(buffer, 0, countBytes);

            textBlock1.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => textBlock1.Text = responce));
        }

        class CommandClass
        {
            public string Command { set; get; }
            public Socket Client { set; get; }
        }
    }
}
