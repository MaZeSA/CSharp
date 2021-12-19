
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
        public ObservableCollection<LiteGame> NewGames { set; get; } = new ObservableCollection<LiteGame>();
        public Commands.CommandConnecToGames CommandConnecToGames { set; get; } 
        Visibility findGameVisibility = Visibility.Collapsed;
        public Visibility FindGameVisibility
        {
            set 
            { 
                findGameVisibility = value; 
                OnNotify();

                if (value == Visibility.Visible) GetGames();
            }
            get => findGameVisibility;
        }

        string password;
        public string Password
        {
            set  {
                password = value;
                OnNotify();
            }
            get => password;
        }

        LiteGame selectedGame;
        public LiteGame SelectedGame 
        {
            set
            {
                selectedGame = value;
                OnNotify();
            }
            get => selectedGame;
        }

        public FindGameModel(MenuControl menuControl)
        {
            CommandConnecToGames = new Commands.CommandConnecToGames(this);
            MenuControl = menuControl;
            TCPClient = new TCPClient();
        }

        private async void GetGames()
        {
            await TCPClient.ConnectAsync();
            await FindGamesAsync();
        }

        public async Task<bool> FindGamesAsync()
        {
            var re = await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.GetListGame });
            return await LissenServer();
        }

        public async void ConnectToGames()
        {
            Packet packet = new Packet();
            packet.Type = Packet.TypePacket.ConectedTo;

            SelectedGame.Password = Password;
            packet.Data = SelectedGame;

            var re = await TCPClient.WriteStramAsync(packet);
            bool connect = await LissenServer();

            if(connect)
            {
                MenuControl.GameModel.GPanelView.EnemyVisualElementsModel.ClientConnect();
                MenuControl.GameModel.GPanelView.GameStarted(TCPClient);
            }
        }

        public async Task<bool> LissenServer()
        {
            Packet p = await TCPClient.ReadStreamAsync();

            if (p.Type == Packet.TypePacket.GetListGame)
            {
                foreach (var item in p.Data as List<LiteGame>)
                {
                    NewGames.Add(item);
                }
                return true;
            }
            else if(p.Type == Packet.TypePacket.Connected)
            {
                return true;
            }
            return false;
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
