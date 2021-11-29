using Battleship.ViewModel;
using Battleship.ViewModel.GamePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandForSelectShip /*: ICommand*/
    {
        //public CommandForSelectShip(ActionViewModel actionViewModel)
        //{
        //    ActionViewModel = actionViewModel;
        //}

        //public ActionViewModel ActionViewModel { get; }

        //public event EventHandler CanExecuteChanged
        //{
        //    add
        //    {
        //        CommandManager.RequerySuggested += value;
        //    }
        //    remove
        //    {
        //        CommandManager.RequerySuggested -= value;
        //    }
        //}

        //public bool CanExecute(object parameter)
        //{
        //    return !(ActionViewModel?.Selected is null);
        //}

        //public void Execute(object parameter)
        //{
        //    ActionViewModel?.CommandForVisualElement(parameter.ToString());
        //}
    }
}
