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

        public Pixel(IVisible obj) 
        {
            //Column = obj.Column;
            //Row = obj.Row;
            ParentObj = obj; 
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
