using Battleship.ViewModel.Interfaces;
using System;
using System.Windows;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels.Pixels
{
    public class Pixel : BaseVisualElement, IBoody
    {
        public object Content => throw new NotImplementedException();

        public IVisible ParentObj {get;}
        public int Counter { get; set; } = 0;
        public override int Row { get => ParentObj.RowSpan > 1 ? ParentObj.Row + Counter : ParentObj.Row; }
        public override int Column { get => ParentObj.ColumnSpan > 1 ? ParentObj.Column + Counter : ParentObj.Column; }

        public Pixel(IVisible obj, int count) 
        {
            Counter = count;
            ParentObj = obj;
            BackgroundBrush = ParentObj.BackgroundBrush;
        }

        public void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            //var moved = (IVisible)e.Data.GetData("Object");
            ////    //var send = (IBoody)e.Data.GetData("sender");

            //if (moved is null) return;
            ////    //dynamic t = e.OriginalSource;
            ////    //var rw = t.DataContext;
            //var r = ParentObj.Row;
            //var c = ParentObj.Column;
            //moved.Move(r, c);
        }
    }
}
