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
    public class ChatMessage : INotifyPropertyChanged
    {
        public HorizontalAlignment Who { set; get; } = HorizontalAlignment.Left;
        string message;
        public string Message 
        { 
            set { message = value; OnNotify(); }
            get => message;
        }
        public virtual SolidColorBrush BackgroundBrush { set; get; } = Brushes.LightSeaGreen;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
