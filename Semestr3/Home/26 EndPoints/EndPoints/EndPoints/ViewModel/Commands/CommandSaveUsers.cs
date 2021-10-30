using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EndPoints.ViewModel.Commands
{
    public class CommandSaveUsers : ICommand
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
        JHolder JH { get; set; }

        public CommandSaveUsers(JHolder jh)
        {
            JH = jh;
        }
        public bool CanExecute(object parameter)
        {
            return JH.Users.Count > 0;
        }

        public void Execute(object parameter)
        {
            JH.SaveUsersAsync();
        }
    }
}
