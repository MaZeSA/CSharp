﻿using Battleship.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship.Commands
{
    public class TestCommand : ICommand
    {
        public TestCommand(ViewContext viewContext)
        {
            ViewContext = viewContext;
        }

        public ViewContext ViewContext { get; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewContext.RemoveShip(null);
        }
    }
}
