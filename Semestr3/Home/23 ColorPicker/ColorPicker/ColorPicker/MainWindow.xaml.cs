using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ColorPicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Color_C();
        }

        public class Color_C : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            byte a = 255;
            public double A
            {
                set
                {
                    a = (byte)value;
                    LabelSolidColorBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    NotifyPropertyChanged();
                }
                get => a;
            }
            byte r = 0;
            public double R
            {
                set
                {
                    r = (byte)value;
                    LabelSolidColorBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    NotifyPropertyChanged();
                }
                get => r;
            }
            byte g = 0;
            public double G
            {
                set
                {
                    g = (byte)value;
                    LabelSolidColorBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    NotifyPropertyChanged();
                }
                get => g;
            }
            byte b = 0;
            public double B
            {
                set
                {
                    b = (byte)value;
                    LabelSolidColorBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
                    NotifyPropertyChanged();
                }
                get => b;
            }

            bool enabled_A = true;
            public bool Enable_A
            {
                set
                {
                    enabled_A = value;

                    if (!value)
                    { A = 0; }
                    NotifyPropertyChanged();
                }
                get => enabled_A;
            }
            bool enabled_R = true;
            public bool Enable_R
            {
                set
                {
                    enabled_R = value;

                    if (!value)
                    { R = 0; }
                    NotifyPropertyChanged();
                }
                get => enabled_R;
            }
            bool enabled_G = true;
            public bool Enable_G
            {
                set
                {
                    enabled_G = value;
                    if (!value)
                    { G = 0; }
                    NotifyPropertyChanged();
                }
                get => enabled_G;
            }
            bool enabled_B = true;
            public bool Enable_B
            {
                set
                {
                    enabled_B = value;
                    if (!value)
                    { B = 0; }
                    NotifyPropertyChanged();
                }
                get => enabled_B;
            }

            public SolidColorBrush LabelSolidColorBrush
            {
                set
                {
                    a = value.Color.A; 
                    r = value.Color.R;
                    g = value.Color.G;
                    b = value.Color.B;
                    NotifyPropertyChanged();
                }
                get => new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (SolidColorBrush item in listBox1.Items)
            {
                if (item.Color == (this.DataContext as Color_C).LabelSolidColorBrush.Color)
                    return;
            }
          
            listBox1.Items.Add((this.DataContext as Color_C).LabelSolidColorBrush);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Remove((sender as Button).DataContext);
        }
    }
}
