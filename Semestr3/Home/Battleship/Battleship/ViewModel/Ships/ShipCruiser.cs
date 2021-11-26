using Battleship.ViewModel.Pixels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel.Ships
{
    public class ShipCruiser : Ship
    {
        public ShipCruiser(ViewContext viewContext) : base(viewContext)
        {
            Length = 4;

            for (int i = 0; i < Length; i++)
            {
                ShipBoody.Add(new ShipView(this) { Column = i, Row = 0 });
            }
        }


    }
}
