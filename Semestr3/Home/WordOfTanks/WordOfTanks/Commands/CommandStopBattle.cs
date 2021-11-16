using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordOfTanks.Commands
{
    public class CommandStopBattle : ICommand
    {
        public CommandStopBattle(Game game)
        {
            Game = game;
        }

        public Game Game { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Game.StopGame();
        }
    }
}
