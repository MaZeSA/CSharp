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
        public ShipCruiser(BaseViewModel viewContext) : base(viewContext)
        {
            Length = 4;

            for (int i = 0; i < Length; i++)
            {
                Boody.Add(new ShipView(this) { Row = 0, Column = i });
            }

            Boody[0].Background = Brushes.DeepPink;
        }
        public ShipCruiser(BaseViewModel viewContext, int Row, int Column) : base(viewContext)
        {
            Length = 4;

            for (int i = 0; i < Length; i++)
            {
                Boody.Add(new ShipView(this) { Row = 0 + Row, Column = i + Column });
            }

            Boody[0].Background = Brushes.DeepPink;
        }
    }
}
