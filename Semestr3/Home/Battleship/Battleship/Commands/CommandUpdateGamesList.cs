using Battleship.ViewModel;
using System;
using System.Net;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandUpdateGamesList : ICommand
    {
        public CommandUpdateGamesList(FindGameModel findGameModel)
        {
            FindGameModel = findGameModel;
        }

        public FindGameModel FindGameModel { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (IPAddress.TryParse(FindGameModel.Ip, out _) && FindGameModel.Port > 0)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            FindGameModel.GetGames();
        }
    }
}
