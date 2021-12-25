using Battleship.ViewModel;
using System;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandReady : ICommand
    {
        public CommandReady(GPanelView gPanelView)
        {
            GPanelView = gPanelView;
        }

        public GPanelView GPanelView { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (GPanelView.IsReady) return false;

            return GPanelView.ShipController.CheckCorectPlace();
        }
        public void Execute(object parameter)
        {
            GPanelView.Ready();
        }
    }
}
