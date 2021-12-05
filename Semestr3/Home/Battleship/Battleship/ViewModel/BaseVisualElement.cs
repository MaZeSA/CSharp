using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Battleship.ViewModel
{
    public abstract class BaseVisualElement : INotifyPropertyChanged
    {
        int column = 0;
        public virtual int Column
        {
            get => column;
            set { column = value; OnNotify(); }
        }
        int row = 0;
        public virtual int Row
        {
            get => row;
            set { row = value; OnNotify(); }
        }
        int columnSpan = 1;
        public int ColumnSpan
        {
            get => columnSpan;
            set { columnSpan = value; OnNotify(); }
        }
        int rowSpan = 1;
        public int RowSpan
        {
            get => rowSpan;
            set { rowSpan = value; OnNotify(); }
        }
        SolidColorBrush backgroundBrush;
        public SolidColorBrush BackgroundBrush
        {
            get => backgroundBrush;
            set { backgroundBrush = value; OnNotify(); }
        }
        SolidColorBrush borderBrush;
        public SolidColorBrush BorderBrush
        {
            get => borderBrush;
            set { borderBrush = value; OnNotify(); }
        }
        Thickness borderThickness;
        public Thickness BorderThickness
        {
            get => borderThickness;
            set { borderThickness = value; OnNotify(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
