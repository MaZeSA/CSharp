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
        public PreviewDragEnterCommand PreviewDragEnterCommand { set; get; } = new PreviewDragEnterCommand();
      
        public EntityPixel(int row, int colum) 
        {
            VisulBoodies = new List<IBoody>();

            Row = row;
            Column = colum;
            BorderBrush = Brushes.Black;
            BackgroundBrush = Brushes.Gray;
            BorderThickness = new Thickness(1);
            VisulBoodies.Add(new Pixel(this, 0) { BackgroundBrush = this.BackgroundBrush }); 
        }

        public override void UIElement_OnDragEnter(object sender, DragEventArgs e)
        {
            var moved = (IVisible)e.Data.GetData("Object");
            if (moved is null) return;
            moved.Move(Row, Column);
        }
    }
}
