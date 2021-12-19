using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Battleship.Class
{
    public class ChatMessage
    {
        public HorizontalAlignment Who { set; get; } = HorizontalAlignment.Left;
        public string Message { set; get; }
        public virtual SolidColorBrush BackgroundBrush { set; get; } = Brushes.LightSeaGreen;

    }
}
