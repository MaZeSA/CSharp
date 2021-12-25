using Battleship.ViewModel;
using System;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandConnecToGames : ICommand
    {
        public CommandConnecToGames(FindGameModel findGameModel)
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
           return !(FindGameModel.SelectedGame is null);
        }

        public void Execute(object parameter)
        {
            FindGameModel.ConnectToGames();
        }
    }
}
