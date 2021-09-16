using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TackManager
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
            if (string.IsNullOrWhiteSpace(textBoxStart.Text))
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Process.Start(openFileDialog.FileName);
                }
            }
            else
            {
                Process.Start(textBoxStart.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            run = !run;
            statusLabel.Content = run ? "Run" : "Stoped";
            Run();
        }

        bool run = false;
        async void Run()
        {
            while (run)
            {
                listView1.ItemsSource = await Task.Run(() => Update());

                foreach (Process process in listView1.Items)
                {
                    if (process.Id == id)
                        listView1.SelectedItem = process;
                }
            }
        }

        Process[] Update()
        {
            Thread.Sleep(4000);
            return Process.GetProcesses();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            run = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var proc = listView1.SelectedItem as Process;
            foreach (Process item in listView1.Items)
            {
                if (item.ProcessName == proc.ProcessName)
                {
                    item.Kill();
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            (listView1.SelectedItem as Process).Kill();
        }

        static int id;
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                id = (listView1.SelectedItem as Process).Id;
                stackPanelBottom.IsEnabled = true;
            }
            else
            {
                stackPanelBottom.IsEnabled = false;
            }
        }

    }
}
