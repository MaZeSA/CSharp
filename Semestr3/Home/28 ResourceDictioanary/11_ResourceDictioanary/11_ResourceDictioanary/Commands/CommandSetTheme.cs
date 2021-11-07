using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _11_ResourceDictioanary.Commands
{
    public class CommandSetTheme : ICommand
    {
        public CommandSetTheme(Menu menu)
        {
            Menu = menu;
        }

        public Menu Menu { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Menu.ChangeThemes(parameter.ToString());
        }
    }
}
