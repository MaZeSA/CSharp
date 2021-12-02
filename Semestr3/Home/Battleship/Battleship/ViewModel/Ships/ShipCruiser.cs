using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.GamePanels.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Battleship.ViewModel.Ships
{
    public class ShipCruiser : Ship
    {
        public ShipCruiser(int row, int column) : base()
        {
            Length = 4;
            Row = row;
            Column = column;
            ColumnSpan = Length;
            BackgroundBrush = Brushes.Coral;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new Pixel(this) { BackgroundBrush = this.BackgroundBrush});
            }
        }
    }
}
