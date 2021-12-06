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
    public class ShipFrigate : Ship
    {
        public ShipFrigate(VisualElementsModel visualElementsModel, int r, int c) : base(visualElementsModel)
        {
            Length = 2;
            Row = 2;
            ColumnSpan = Length;
            BackgroundBrush = Brushes.Brown;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new Pixel(visualElementsModel, this, i) { BackgroundBrush = this.BackgroundBrush});
            }
        }
     }
}
