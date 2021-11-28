using Battleship.ViewModel.GamePanels;
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
        public ShipFrigate(BaseViewModel viewContext) : base(viewContext)
        {
            Length = 3;

            for (int i = 0; i < Length; i++)
            {
                Boody.Add(new ShipView(this) { Row = 0, Column = i, Background= Brushes.Brown });
            }

            Boody[0].Background = Brushes.DeepPink;
        }
        public ShipFrigate(BaseViewModel viewContext, int Row, int Column) : base(viewContext)
        {
            Length = 3;

            for (int i = 0; i < Length; i++)
            {
                Boody.Add(new ShipView(this) { Row = 0 + Row, Column = i + Column, Background = Brushes.Brown });
            }

            Boody[0].Background = Brushes.DeepPink;
        }
    }
}
