using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
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

namespace Battleship.ViewModel.GamePanels.Pixels
{
    /// <summary>
    /// Логика взаимодействия для EntyView.xaml
    /// </summary>
    public partial class EntyView : UserControl, IBoody
    { 
        public int Column { get; set; } = 0;
        public int Row { get; set; } = 0;

        public IVisible ParentObj { get; }

        public EntyView(int row, int column)
        {
            Row = row;
            Column = column;
            InitializeComponent();
            DataContext = this;
        }
    }
}
