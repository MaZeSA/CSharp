using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TanksLib;
using WordOfTanks.Commands;

namespace WordOfTanks
{
    public class Game
    {
        public ObservableCollection<Battle> Battles { set; get; } = new ObservableCollection<Battle>();
        public GameСontrol GameСontrol { set; get; }

        CancellationTokenSource cancelTokenSource { set; get; }
        List<Task> tasks = new List<Task>();

        public int Width { set; get; } //для підтримки динамічної ширини вікна

        const int COUNT = 5;

        public Game()
        {
            GameСontrol = new GameСontrol(this);
            Preparation();
        }

        private void Preparation()
        {
            cancelTokenSource = new CancellationTokenSource();
            Battles.Clear();
            for (int i = 0; i < COUNT; i++)
            {
                Battles.Add(new Battle());
            }
        }

        public void StartGames(int width)
        {
            Width = width;

            tasks.Clear();
            foreach (var battle in Battles)
            {
                tasks.Add(Task.Factory.StartNew(() => Play(battle)));
            }

            GameСontrol.SetStop();
        }

        private void Play(Battle battle)
        {
            CancellationToken token = cancelTokenSource.Token;

            while (battle.Move(Width))
            {
                if (token.IsCancellationRequested)
                    return;

                Thread.Sleep(50);
            }

            var win = battle.GetWinner();
            battle.KillLoser(win);
            battle.Win(win);
        }

        public void StopGame()
        {
            cancelTokenSource.Cancel();
            Preparation();
            GameСontrol.SetStart();
        }
    }
}
