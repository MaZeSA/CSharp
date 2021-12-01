using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoRun.Command
{
    public class CommandAdd : ICommand
    {
        public event EventHandler CanExecuteChanged;

        RegEdit RegEdit { set; get; }
        public CommandAdd(RegEdit regEdit)
        {
            RegEdit = regEdit;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            RegEdit.Add();
        }
    }
}
