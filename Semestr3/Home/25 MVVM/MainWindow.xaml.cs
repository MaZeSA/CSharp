using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace _09_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(popup1.IsOpen)
            {
                popup1.IsOpen = false; 
                popup1.IsOpen = true;
            }
        }
    }
}
