using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel.Ships
{
    public abstract class Ship
    {
        public List<ShipView> ShipBoody { set; get; } = new List<ShipView>();
        public int Length { set; get; }
        public string Name { set; get; }
        public ViewContext ViewContext { set; get; }

        public Ship(ViewContext viewContext)
        {
            ViewContext = viewContext;
        }
        
        public virtual void AddShip()
        {
            ViewContext.SetShip(this);
        }
        public virtual void RemoveShip()
        {
            ViewContext.RemoveShip(this);
        }
    }
}
