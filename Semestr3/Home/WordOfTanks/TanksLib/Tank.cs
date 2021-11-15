using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksLib
{
    public class Tank
    {
        string Name { set; get; }
        int Ammunition { set; get; }
        int Armor { set; get; }
        int Mobilyty { set; get; }

        public Tank(string name)
        {
            Random random = new Random();

            Name = name;
            Ammunition = random.Next(0, 100);
            Armor = random.Next(0, 100);
            Mobilyty = random.Next(0, 100);
        }

        public static Tank operator ^(Tank tank1, Tank tank2)
        {
            int res = 0;

            res += tank1.Ammunition > tank2.Ammunition ? 1 : 0; 
            res += tank1.Armor > tank2.Armor ? 1 : 0;
            res += tank1.Mobilyty > tank2.Mobilyty ? 1 : 0;

            return res >= 2 ? tank1 : tank2;
        }
    }
}
