using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRun.Command
{
    public class CommandRemove : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        RegEdit RegEdit { set; get; }
        public CommandRemove(RegEdit regEdit)
        {
            RegEdit = regEdit;
        }

        public bool CanExecute(object parameter)
        {
            return !(parameter is null);
        }

        public void Execute(object parameter)
        {
            RegEdit.Remove();
        }
    }
}
