using Battleship.Class;
using Battleship.Commands;
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
    public class CreateGameModel : INotifyPropertyChanged
    {
        public CommandCreateGame CommandCreateGame { set; get; }
        private TCPClient TCPClient { set; get; }


        Visibility createGameVisibility = Visibility.Collapsed;
        public Visibility CreateGameVisibility
        {
            set { createGameVisibility = value; OnNotify(); }
            get => createGameVisibility;
        }

        Visibility visibilityWait = Visibility.Collapsed; 
        public Visibility VisibilityWait
        {
            set { visibilityWait = value; OnNotify(); }
            get => visibilityWait;
        }

        public GameModel GameModel { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public CreateGameModel(GameModel gameModel)
        {
            CommandCreateGame = new CommandCreateGame(this);
            TCPClient = new TCPClient(); 
            GameModel = gameModel;
        }

        public void CreateGame()
        {
            VisibilityWait = Visibility.Visible;
            TCPClient.CreateGame(this);
            MessageBox.Show("llll");

        }
    }
}
