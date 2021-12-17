using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandOpenMenu : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IMenu Menu { set; get; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Menu?.Back();
            Menu = parameter as IMenu;
            Menu?.Show();
        }
    }
}
