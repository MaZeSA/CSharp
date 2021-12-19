using Battleship.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return GPanelView.ShipController.CheckCorectPlace();
        }
        public void Execute(object parameter)
        {
            GPanelView.Ready();
        }
    }
}
