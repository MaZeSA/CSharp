using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordOfTanks.Commands
{
    public class CommandStartGame : ICommand
    {
        public event EventHandler CanExecuteChanged;
     
        public CommandStartGame(Game game)
        {
            Game = game;
        }

        public Game Game { get; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var widow = parameter as MainWindow;
            Game.StartGames(Convert.ToInt32(widow.Width));
        }
    }
}
