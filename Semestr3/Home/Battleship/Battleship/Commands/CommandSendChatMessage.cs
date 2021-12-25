using Battleship.ViewModel;
using System;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class CommandSendChatMessage : ICommand
    {
        public CommandSendChatMessage(GPanelView gPanelView)
        {
            GPanelView = gPanelView;
        }

        public GPanelView GPanelView { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GPanelView.SendMessage();
        }
    }
}
