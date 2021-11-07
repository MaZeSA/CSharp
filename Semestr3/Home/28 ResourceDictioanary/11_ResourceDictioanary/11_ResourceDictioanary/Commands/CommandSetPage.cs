using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _11_ResourceDictioanary.Commands
{
    public class CommandSetPage : ICommand
    {
        public event EventHandler CanExecuteChanged;
       
        PageSelector PS { get; set; }

        public CommandSetPage(PageSelector ps)
        {
            PS = ps;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PS.SetSource(parameter?.ToString());
        }
    }
}
