using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksLib;

namespace WordOfTanks
{
    public class Game
    {
        List<Tank> Tanks_T34 { set; get; } = new List<Tank>(5); 
        List<Tank> Tanks_Pantera { set; get; } = new List<Tank>(5);

        const int COUNT = 5;

        public Game()
        {
            for(int i =0; i< COUNT; i++)
            {
                Tanks_T34.Add(new Tank($"T-34 #{i}"));
                Tanks_Pantera.Add(new Tank($"Pantera #{i}"));
            }
        }

        public void StartGames()
        {
            Task[] tasks2 = new Task[COUNT];
            for (int i = 0; i < COUNT; i++)
                tasks2[i] = Task.Factory.StartNew(() => Play(Tanks_T34[i], Tanks_Pantera[i]));
        }

        private void Play(Tank tank1, Tank tank2)
        {
            var viner = tank1 ^ tank2;
        }
    }
}
