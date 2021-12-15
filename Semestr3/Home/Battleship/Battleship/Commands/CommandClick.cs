using Battleship.ViewModel.GamePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandClick : ICommand
    { 
        public CommandClick(GameModel gameModel)
        {
            GameModel = gameModel;
        }

        public VisualElementsModel VisualElementsModel { get; }
        public GameModel GameModel { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GameModel.VisualElementsModel.Shot();
        }
    }
}
