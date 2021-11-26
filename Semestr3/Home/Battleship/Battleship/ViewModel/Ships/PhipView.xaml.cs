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

namespace Battleship.ViewModel.Ships
{
    /// <summary>
    /// Логика взаимодействия для PhipView.xaml
    /// </summary>
    public partial class ShipView : UserControl, INotifyPropertyChanged
    {
        Ship Ship { set; get; }

        public ShipView(Ship ship)
        {
            InitializeComponent();
            this.DataContext = this;
            Ship = ship;
        }

        int row = 0;
        public int Row
        {
            set
            {
                row = value;
                OnNotify();
            }
            get => row;
        }

        int column = 0;
        public int Column
        {
            set
            {
                column = value;
                OnNotify();
            }
            get => column;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
