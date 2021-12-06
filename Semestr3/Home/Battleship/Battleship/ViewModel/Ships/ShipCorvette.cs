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
    public class ShipCorvette: Ship
    {
        public ShipCorvette(VisualElementsModel visualElementsModel, int row, int column) : base(visualElementsModel)
        {
            Length = 1;
            Row = row;
            Column = column;
            ColumnSpan = Length;
            BackgroundBrush = Brushes.CadetBlue;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new Pixel(visualElementsModel, this, i) { BackgroundBrush = this.BackgroundBrush });
            }
        }
    }
}
