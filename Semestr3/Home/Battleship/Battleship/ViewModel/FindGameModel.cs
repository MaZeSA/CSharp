
using Battleship.ViewModel.Interfaces;
using LibraryBattleship;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship.ViewModel
{
    public class FindGameModel : IMenu
    {
        private TCPClient TCPClient { set; get; }
        public MenuControl MenuControl { get; }
        public NewGame NewGame { set; get; }
        public ObservableCollection<NewGame> NewGames { set; get; } = new ObservableCollection<NewGame>();

        Visibility findGameVisibility = Visibility.Collapsed;
        public Visibility FindGameVisibility
        {
            set { findGameVisibility = value; OnNotify(); }
            get => findGameVisibility;
        }

        public FindGameModel(MenuControl menuControl)
        {
            MenuControl = menuControl;
        }

        public void Show()
        {
            FindGameVisibility = Visibility.Visible;
        }

        public void Back()
        {
            FindGameVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
