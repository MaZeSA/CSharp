using Battleship.Commands;
using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship.ViewModel.GamePanels
{
    public class ActionViewModel : BaseViewModel
    {
        public CommandForSelectShip CommandForSelectShip { set; get; }

        public ActionViewModel(GameView gameView) : base(gameView)
        {
            CommandForSelectShip = new CommandForSelectShip(this);
        }

        Visibility actionVisibility = Visibility.Collapsed;
        public Visibility ActionVisibility
        {
            set
            {
                actionVisibility = value;
                if (value == Visibility.Collapsed)
                    SetColorAllPixels(Brushes.Transparent);

                OnNotify();
            }
            get => actionVisibility;
        }


        IVisible selected;
        public IVisible Selected
        {
            set
            {
                if (selected != value && selected != null)
                    selected.ChangeParent(GameViewModel.BaseViewModel);

                selected = value;
                selected?.Add();
                ActionVisibility = value is null ? Visibility.Collapsed : Visibility.Visible;
                OnNotify();
            }
            get => selected;
        }

        public virtual void CommandForVisualElement(string command)
        {
            switch (command)
            {
                case "add":
                    {
                        Selected.Add();
                        break;
                    }
                case "ok":
                    {
                        Selected = null;
                        break;
                    }
                case "rotate":
                    {
                        Selected?.Rotate();
                        break;
                    }
                case "remove":
                    {
                        Selected?.Remove();
                        selected = null;
                        break;
                    }
            }
        }

    }
}
