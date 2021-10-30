using System;
using System.Windows.Input;

namespace _09_MVVM.ViewModel.Commands
{
    public class SearchCommand : ICommand
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

        public SearchCommand(WeatherVM vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter) // query
        {
            return !string.IsNullOrWhiteSpace(parameter as string);
        }

        public async void Execute(object parameter)
        {
            await VM.MakeRequestCitiesAsync();
        }
    }
}
