using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandEndGame : ICommand
    {
        public CommandEndGame(GameModel gameModel)
        {
            GameModel = gameModel;
        }

        public GameModel GameModel { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GameModel.Restart();
        }
    }
}
