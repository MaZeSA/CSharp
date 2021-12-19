
using Battleship.Commands;
using Battleship.ViewModel.Interfaces;
using LibraryBattleship;
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
    public class CreateGameModel : IMenu
    {
        private TCPClient TCPClient { set; get; }
        public MenuControl MenuControl { get; } 
        public CommandCreateGame CommandCreateGame { set; get; }
        public NewGame NewGame { set; get; }

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
      
        public CreateGameModel(MenuControl menuControl)
        {
            TCPClient = new TCPClient();
            MenuControl = menuControl;
            CommandCreateGame = new CommandCreateGame(this);
            NewGame = new NewGame() { StatusGame = NewGame.Status.New };
        }

        public async void CreateGame()
        {
            await TCPClient.ConnectAsync();
            VisibilityWait = Visibility.Visible;
            var t = await CreateGameAsync(NewGame);
            if (t)
            {
                VisibilityWait = Visibility.Collapsed;
                MenuControl.GameModel.GPanelView.GameStarted(TCPClient);
            }
            else
            {
                MessageBox.Show("Error Game Create");
            }
        }


        public async Task<bool> CreateGameAsync(NewGame newGame)
        {
            var re = await Task.Run(() => TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.CreateNewGame, Data = newGame }));
            Packet p = await TCPClient.ReadStreamAsync();

            if (p.Type == Packet.TypePacket.CreateNewGame)
            {
                var nG = p.Data as NewGame;
                if(nG.StatusGame == NewGame.Status.Created)
                    return true;
            }
            return false;
        }

        public void Show()
        {
            CreateGameVisibility = Visibility.Visible;
        }

        public void Back()
        {
            CreateGameVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

       
    }
}
