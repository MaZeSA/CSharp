using Battleship.ViewModel;
using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandIVisibleRemove : ICommand
    {
        public CommandIVisibleRemove(IVisible baseVisualElement)
        {
            IVisible = baseVisualElement;
        }

        public CommandIVisibleRemove(VisualElementsModel visualElementsModel)
        {
            VisualElementsModel = visualElementsModel;
        }

        public IVisible IVisible { get; }
        public VisualElementsModel VisualElementsModel { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VisualElementsModel.RemoveVisibleObj(parameter as IVisible);
        }
    }
}
