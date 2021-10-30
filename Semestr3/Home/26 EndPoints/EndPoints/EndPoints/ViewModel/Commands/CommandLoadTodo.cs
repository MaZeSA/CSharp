using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EndPoints.ViewModel.Commands
{
    public class CommandLoadTodos : ICommand
    {
        public event EventHandler CanExecuteChanged;
        JHolder JH { get; set; }

        public CommandLoadTodos(JHolder jh)
        {
            JH = jh;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            JH.GetTodosAsync();
        }
    }
}