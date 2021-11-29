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
        public ShipCruiser() : base()
        {
            Length = 4;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new ShipView(this) { Row = 0, Column = i });
            }

         //   VisulBoodies[0].Content = Brushes.DeepPink;
        }
        public ShipCruiser( int Row, int Column) : base()
        {
            Length = 4;

            for (int i = 0; i < Length; i++)
            {
                VisulBoodies.Add(new ShipView(this) { Row = 0 + Row, Column = i + Column });
            }

           // VisulBoodies[0].Background = Brushes.DeepPink;
        }
    }
}
