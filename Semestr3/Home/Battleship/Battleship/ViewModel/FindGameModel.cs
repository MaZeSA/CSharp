
using Battleship.ViewModel.Interfaces;
using LibraryBattleship;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship.ViewModel
{
    public class FindGameModel : IMenu, INotifyPropertyChanged
    {
        private TCPClient TCPClient { set; get; }
        public MenuControl MenuControl { get; }
        public ObservableCollection<LiteGame> NewGames { set; get; } = new ObservableCollection<LiteGame>();
        public Commands.CommandConnecToGames CommandConnecToGames { set; get; } 
        public Commands.CommandUpdateGamesList CommandUpdateGamesList { set; get; }
     
        Visibility findGameVisibility = Visibility.Collapsed;
        public Visibility FindGameVisibility
        {
            set 
            { 
                findGameVisibility = value; 
                OnNotify();

                if (value == Visibility.Visible);
            }
            get => findGameVisibility;
        }

        string password;
        public string Password
        {
            set
            {
                password = value;
                OnNotify();
            }
            get => password;
        }

        string ip = "127.0.0.1";
        public string Ip
        {
            set
            {
                ip = value;
                OnNotify();
            }
            get => ip;
        }
        int port = 8888;
        public int Port
        {
            set
            {
                port = value;
                OnNotify();
            }
            get => port;
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

        string error = "";
        public string Error
        {
            get => error;
            set { error = value; OnNotify(); }
        }

        public FindGameModel(MenuControl menuControl)
        {
            CommandConnecToGames = new Commands.CommandConnecToGames(this);
            CommandUpdateGamesList = new Commands.CommandUpdateGamesList(this);
            MenuControl = menuControl;
        }

        public async void GetGames()
        {
            Error = "";
            try
            {
                await Connect();
                await FindGamesAsync();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        private async Task Connect()
        {
            if (TCPClient is null)
            {
                TCPClient = new TCPClient();
              await TCPClient.ConnectAsync(Ip, Port);
                return;
            }
            else if (!TCPClient.Client.Connected)
            {
                TCPClient.Close();
                TCPClient = new TCPClient();

                await TCPClient.ConnectAsync(Ip, Port);
                return;
            }
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

            NewGames.Clear();

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

            CommandUpdateGamesList.Execute(null);
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
