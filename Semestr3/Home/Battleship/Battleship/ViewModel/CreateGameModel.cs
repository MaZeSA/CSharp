
using Battleship.Class;
using Battleship.Commands;
using Battleship.ViewModel.Interfaces;
using LibraryBattleship;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship.ViewModel
{
    public class CreateGameModel : IMenu, INotifyPropertyChanged
    {
        private TCPClient TCPClient { set; get; }
        public MenuControl MenuControl { get; } 
        public CommandCreateGame CommandCreateGame { set; get; }

        string gameName = "NewGame";
        public string GameName 
        {
            get => gameName;
            set { gameName = value; OnNotify(); }
        }

        public string Password { get; set; }

        public bool SuperWeapon { get; set; }

        string iPServer = "127.0.0.1";
        public string IPServer 
        {
            get => iPServer;
            set { iPServer = value; OnNotify(); }
        }
        int portServer = 8888;
        public int PortServer
        {
            get => portServer;
            set { portServer = value; OnNotify();}
        }
        string error = "";
        public string Error
        {
            get => error;
            set { error = value; OnNotify(); }
        }

        public AbstractVisualStyleClass CreateGamaStyle { set; get; } = new AbstractVisualStyleClass();
        public AbstractVisualStyleClass CreateGameWaitStyle { set; get; } = new AbstractVisualStyleClass();
              
        public CreateGameModel(MenuControl menuControl)
        {
            TCPClient = new TCPClient();
            MenuControl = menuControl;
            CommandCreateGame = new CommandCreateGame(this);
        }

        public async void CreateGame()
        {
            Error = "";
            try
            {
                await ConnectAsync();

                CreateGameWaitStyle.AbstractlementVisibility = Visibility.Visible;
                await CreateGameAsync();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                CreateGameWaitStyle.AbstractlementVisibility = Visibility.Collapsed;
            }
    
        }
        private async Task CreateGameAsync()
        {
            var t = await CreateGameAsync(
                   new NewGame { GameName = GameName, Password = Password, StatusGame = NewGame.Status.New, SuperWeapon = SuperWeapon }
                    );

            if (t)
            {
                CreateGameWaitStyle.AbstractlementVisibility = Visibility.Collapsed;
                MenuControl.GameModel.GPanelView.GameStarted(TCPClient);
            }
            else
            {
                CreateGameWaitStyle.AbstractlementVisibility = Visibility.Collapsed;
            }
        }
        private async Task ConnectAsync()
        {
            TCPClient?.Close();

            TCPClient = new TCPClient();
            await TCPClient.ConnectAsync(IPServer, PortServer);
        }

        public async Task<bool> CreateGameAsync(NewGame newGame)
        {
            var re = await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.CreateNewGame, Data = newGame });
            Packet p = await TCPClient.ReadStreamAsync();

            if (p.Type == Packet.TypePacket.CreateNewGame)
            {
                var nG = p.Data as NewGame;
                if(nG.StatusGame == NewGame.Status.Created)
                    return true;
            }
            else if(p.Type == Packet.TypePacket.Error)
            {
                Error = p.ErrorMessage;
            }
            return false;
        }

        public void Show()
        {
           CreateGamaStyle.AbstractlementVisibility  = Visibility.Visible;
        }

        public void Back()
        {
            CreateGamaStyle.AbstractlementVisibility = Visibility.Collapsed;
        }  
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
