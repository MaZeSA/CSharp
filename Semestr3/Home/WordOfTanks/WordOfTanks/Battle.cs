using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksLib;

namespace WordOfTanks
{
    public class Battle
    {
        public Mower MowerLeft { set; get; }
        public Mower MowerRight { set; get; }
    }
    public class Mower
    {
        Tank Tank { set; get; }
        public Side SideMow { set; get; }
        public int Position { set; get; }

        public enum Side
        {
            Left,
            Right
        }
    }
}
