using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleship.ViewModel.Pixels
{
    public class Pixel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Fire()
        { 
        }

        int row = 0;
        public int Row
        {
            set
            {
                row = value;
            }
            get => row;
        }

        int column = 0;
        public int Column
        {
            set
            {
                column = value;
            }
            get => column;
        }

        object pixelContent = "test";
        public object PixelContent
        {
            set
            {
                pixelContent = value;
                OnNotify();
            }
            get => pixelContent;
        }

        
    }
}
