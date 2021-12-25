using System;
using System.Windows;
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
            if (!(parameter is null))
                if (bool.Parse(parameter.ToString()))
                {
                    var res = MessageBox.Show("Exit the game?", "Exit", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.No)
                        return;
                }

            GameModel.Restart();
        }
    }
}
