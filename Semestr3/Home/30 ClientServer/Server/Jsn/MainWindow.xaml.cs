using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

namespace Jsn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listBox1.ItemsSource = Settlements;
        }

        ObservableCollection<Settlement> Settlements = new ObservableCollection<Settlement>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var i = new Settlement { SettlementName = textBox1.Text };
            var list = textBox2.Text.Split('\n').ToList();

            foreach (var t in list)
                i.StreetList.Add(t.Trim().Replace("\r",""));

            Settlements.Add(i);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //data = JsonConvert.DeserializeObject<List<T>>(json);
            var str = JsonConvert.SerializeObject(Settlements);
            File.WriteAllText("data.jsn", str);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (var t in Settlements)
                foreach (var r in t.StreetList)
                    textBlock.Text += r;
        }
    }
}
