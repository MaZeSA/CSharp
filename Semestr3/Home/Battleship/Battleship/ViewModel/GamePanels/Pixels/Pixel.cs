using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels.Pixels
{
    public class Pixel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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

        SolidColorBrush background;
        public SolidColorBrush Background
        {
            set
            {
                background = value;
                OnNotify();
            }
            get => background;
        }

        SolidColorBrush borderBrush = new SolidColorBrush(Colors.Black);
        public SolidColorBrush BorderBrush
        {
            set
            {
                borderBrush = value;
                OnNotify();
            }
            get => borderBrush;
        }

        Thickness borderThickness = new Thickness(1);
        public Thickness BorderThickness
        {
            set
            {
                borderThickness = value;
                OnNotify();
            }
            get => borderThickness;
        }
        
        IVisible pixelContent;
        public IVisible PixelContent
        {
            set
            {
                pixelContent = value;
                BorderThickness = value is null ? new Thickness(1) : new Thickness(0);
                OnNotify("Content");
            }
            get => pixelContent;
        }

        public IBoody Content
        {
            get=> PixelContent?.GetBoody(this);
        }


        public void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object"); 
        }

        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            var send = (IBoody)e.Data.GetData("sender");

            if (moved is null)return;

            var r = Row - send.Row ;
            var c = Column - send.Column;
            moved.Move(r, c);

            Background = new SolidColorBrush(Colors.Red);
        }
    }
}
