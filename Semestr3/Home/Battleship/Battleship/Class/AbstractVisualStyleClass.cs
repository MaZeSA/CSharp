using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Battleship.Class
{
    public class AbstractVisualStyleClass : INotifyPropertyChanged
    {
        SolidColorBrush abstractBackgroundBrush = Brushes.Red;
        public virtual SolidColorBrush AbstractBackgroundBrush
        {
            get => abstractBackgroundBrush;
            set { abstractBackgroundBrush = value; OnNotify(); }
        }
        Visibility abstractlementVisibility = Visibility.Collapsed;
        public Visibility AbstractlementVisibility
        {
            set { abstractlementVisibility = value; OnNotify(); }
            get => abstractlementVisibility;
        }
        string abstractString = "";
        public string AbstractString
        {
            set
            {
                abstractString = value; OnNotify();

            }
            get => abstractString;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
