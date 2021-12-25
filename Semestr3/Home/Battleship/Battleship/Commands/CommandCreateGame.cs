using Battleship.ViewModel;
using System;
using System.Net;
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

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(CreateGameModel.GameName))
            {
                if (IPAddress.TryParse(CreateGameModel.IPServer, out _) && CreateGameModel.PortServer > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            CreateGameModel.CreateGame();
        }
    }
}
