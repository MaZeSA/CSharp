using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship.ViewModel
{
    public class MenuControl : INotifyPropertyChanged, IMenu
    {
        public MenuControl(GameModel gameModel)
        {
            GameModel = gameModel;
            CreateGameModel = new CreateGameModel(this);
            FindGameModel = new FindGameModel(this);
            VisualElementsModel = new VisualElementsModel(gameModel);
        }

        public GameModel GameModel { get; }
        public CreateGameModel CreateGameModel { set; get; }
        public FindGameModel FindGameModel { set; get; }
        public VisualElementsModel VisualElementsModel { set; get; }

        public void ShowCteateGameMenu()
        {
            CreateGameModel.CreateGameVisibility = Visibility.Visible;
        }

        Visibility menuVisibility = Visibility.Visible;
        public Visibility MenuVisibility
        {
            set { menuVisibility = value; OnNotify(); }
            get => menuVisibility;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Show()
        {
            MenuVisibility = Visibility.Visible;
        }

        public void Back()
        {
            //MenuVisibility = Visibility.Collapsed;
        }

        public void Close()
        {
            Environment.Exit(0);
        }
    }
}
