using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Threads_AsyncMethods
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var count = Convert.ToInt32(txtCount.Text);

            for(int i =0; i< count; i++)
            {
                var progressBar = new ProgressBar 
                { 
                    Orientation = Orientation.Vertical, 
                    Margin= new Thickness(2), 
                    Width = 30,
                };
                dockProgress.Children.Add(progressBar);

                var th = new Thread(StartDance) { IsBackground = true };
                th.Start(progressBar);
            }
        }

        Random random = new Random();
        private void StartDance(object progress)
        {
            while (true)
            {
                var value = random.Next(0, 100);
                var color = Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));

                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                    {
                        (progress as ProgressBar).Foreground = new SolidColorBrush(color);
                        (progress as ProgressBar).Value = value;
                    });
                Thread.Sleep(200);
            }
        }

    }
}
