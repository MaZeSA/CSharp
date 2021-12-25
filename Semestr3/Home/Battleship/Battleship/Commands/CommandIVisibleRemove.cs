using Battleship.ViewModel.Interfaces;
using System;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandIVisibleRemove : ICommand
    {
        public CommandIVisibleRemove(IVisible baseVisualElement)
        {
            IVisible = baseVisualElement;
        }

        public IVisible IVisible { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            IVisible.GPanelView.VisualElementsModel.RemoveVisibleObj(parameter as IVisible);
        }
    }
}
