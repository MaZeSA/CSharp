using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FileSearh
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
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    txbPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            progBar.Visibility = Visibility.Hidden;
            Start(txbPath.Text, txbText.Text);
        }

        private async void Start(string path, string txt)
        {
            await Task.Run(() => Search(path,txt));
        }

        void Search(string path, string txt)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(path))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            txtCurrent.Text = f;
                        });

                        var res = File.ReadAllText(f).Split(new[] { txt}, StringSplitOptions.None);
                        if (res.Length > 0)
                        {
                            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                            {
                                txtResult.Text += $"File Name {Path.GetFileName(f)}\n Path to file: {f}\nCount matches: {res.Length - 1}\n\n";
                            });
                        }
                    }
                    Search(d, txt);
                }
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    txtResult.Text += ex.Message;
                });
            }
            finally
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    progBar.Visibility = Visibility.Hidden;
                    txtCurrent.Text = "Successful";
                });
            }
        }
    }
}
