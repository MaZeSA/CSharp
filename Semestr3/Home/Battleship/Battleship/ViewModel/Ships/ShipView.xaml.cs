using Battleship.ViewModel.Interfaces;
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
    public partial class ShipView : UserControl, INotifyPropertyChanged/*, IBoody*/
    {       
        public IVisible ParentObj { get; }

        public ShipView(Ship ship)
        {
            InitializeComponent();
            this.DataContext = this;
            ParentObj = ship;
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
                       DataObject data = new DataObject();
            data.SetData("Object", ParentObj);
            data.SetData("sender", this);

            DragDrop.DoDragDrop(this, data, DragDropEffects.All);
        }
    }
}
