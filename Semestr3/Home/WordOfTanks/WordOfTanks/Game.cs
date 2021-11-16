using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TanksLib;

namespace WordOfTanks
{
    public class Game
    {
        public ObservableCollection<Battle> Battles { set; get; } = new ObservableCollection<Battle>();

        const int COUNT = 5;

        public Game()
        {
            for (int i = 0; i < COUNT; i++)
            {
                Battles.Add(new Battle());
            }
            StartGames();
        }

        public void StartGames()
        {
            List<Task> tasks = new List<Task>();

            foreach (var battle in Battles)
            {
                tasks.Add(Task.Factory.StartNew(() => Play(battle)));
            }
        } 

        private void Play(Battle battle)
        {
            while (battle.Move(792))
            {
                Thread.Sleep(50);
            }

           var vin = battle.GetWinner();
            battle.KillLoser(vin);
        }
    }
}
