using Battleship.Commands;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels.Pixels
{
    public class EntityPixel : BaseVisualElement
    {
        public EntityPixel(GameModel GameModel, int row, int colum) : base(GameModel)
        {
            VisulBoodies = new List<IBoody>();

            Row = row;
            Column = colum;
            BorderBrush = Brushes.Black;
            BackgroundBrush = new SolidColorBrush(Color.FromArgb(150, 127, 224, 230));
            BorderThickness = new Thickness(1, 1, 0, 0);
            VisulBoodies.Add(new Pixel(GameModel, this, 0) { BackgroundBrush = this.BackgroundBrush }); 
        }

        //public override void UIElement_OnDragEnter(object sender, DragEventArgs e)
        //{
        //    var moved = (IVisible)e.Data.GetData("Object");
        //    if (moved is null) return;
        //    moved.Move(Row, Column);
        //}
    }
}
