using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace _09_MVVM.ViewModel.Commands
{
    public class LoadFiveDayCommand : ICommand
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
        WeatherVM VM { get; set; }

        public LoadFiveDayCommand(WeatherVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            var vm = parameter as WeatherVM;
            return !(vm.SelectedCity is null);
        }

        public void Execute(object parameter)
        {
            if(parameter is null)
            {
                VM.GetFiveDayConditionsAsync();
            }
            else
            {
                var vm = parameter as WeatherVM;
                if(!(vm is null))
                {
                    vm.ShowPopup = !vm.ShowPopup;
                }
            }
        }
    }
}
