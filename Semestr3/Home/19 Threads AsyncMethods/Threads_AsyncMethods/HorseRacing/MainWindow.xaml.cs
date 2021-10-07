using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace HorseRacing
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

            for (int i = 0; i < staskProgress.Children.Count; i++)
            {
                var th = new Thread(Go) { IsBackground = true };
                th.Start(staskProgress.Children[i]);
            }
        }

        Random random = new Random();
        private void Go(object progressBar)
        {
            var _stopWatch = new Stopwatch();
            _stopWatch.Start();

            for (int i = 0; i < 100; i++)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    (progressBar as ProgressBar).Value++;
                });

                Thread.Sleep(random.Next(0, 200));
            }

            _stopWatch.Stop();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                txtResult.Text += "Horse " + (staskProgress.Children.IndexOf(progressBar as ProgressBar) + 1) + " Result=" + _stopWatch.Elapsed.TotalSeconds + Environment.NewLine;
            });
        }
    }
}
