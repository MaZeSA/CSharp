using Battleship.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandCreateGame : ICommand
    {
        public CommandCreateGame(CreateGameModel createGameModel)
        {
            CreateGameModel = createGameModel;
        }

        public CreateGameModel CreateGameModel { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CreateGameModel.CreateGame();
        }
    }
}
