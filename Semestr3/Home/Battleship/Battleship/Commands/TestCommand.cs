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
    public class TestCommand : ICommand
    {
        public TestCommand(BaseViewModel viewContext)
        {
            ViewContext = viewContext;
        }

        public BaseViewModel ViewContext { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
         //   ViewContext.RemoveShip(null);
        }
    }
}
