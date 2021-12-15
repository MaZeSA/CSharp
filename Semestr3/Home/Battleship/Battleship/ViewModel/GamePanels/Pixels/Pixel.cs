using Battleship.ViewModel.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels.Pixels
{
    public class Pixel : BaseVisualElement, IBoody
    {
        public object Content => throw new NotImplementedException();

        public IVisible ParentObj { get; }
        public int NumPixel { get; }

        public override int Row
        {
            get => ParentObj.RowSpan > 1 ? ParentObj.Row + NumPixel : ParentObj.Row;
        }
        public override int Column
        {
            get => ParentObj.ColumnSpan > 1 ? ParentObj.Column + NumPixel : ParentObj.Column;
        }
        bool shot = false;
        public bool ShotShow
        {
            get => shot;
            set { shot = value; OnNotify(); }
        }
        public Pixel(GameModel gameModel, IVisible obj, int num):base(gameModel) 
        {
            NumPixel = num;
            ParentObj = obj; 
        }

        public override void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            if (moved is null) return;
            moved.Move(Row, Column);
        }
    }
}
