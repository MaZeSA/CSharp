using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBattleship
{
    public enum ShipType
    {
        Corvette = 1,
        Frigate = 2,
        Destroyer = 3,
        Cruiser = 4
    }

    [Serializable]
    public class DeadShip
    {
        public int Row { set; get; }
        public int Column { set; get; }
        public int RowSpan { set; get; }
        public int ColumnSpan { set; get; }
        public ShipType Type { set; get; }
    }

    [Serializable]
    public class Fire
    {
        public enum Type
        {
            Fire,
            Answer
        }

        public Type FireType { set; get; } = Type.Fire;
        public Dictionary<int[], bool> Pointers { set; get; } = new Dictionary<int[], bool>();

        public List<DeadShip> DeadShips { set; get; } = new List<DeadShip>();

        public bool StepPermission { set; get; } = false;
    }
}
