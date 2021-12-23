
using Battleship.Class;
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

        public AbstractVisualStyleClass CreateGamaStyle { set; get; } = new AbstractVisualStyleClass();
        public AbstractVisualStyleClass CreateGameWaitStyle { set; get; } = new AbstractVisualStyleClass();
              
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
            CreateGameWaitStyle.AbstractlementVisibility = Visibility.Visible;
            var t = await CreateGameAsync(NewGame);
            if (t)
            {
                CreateGameWaitStyle.AbstractlementVisibility = Visibility.Collapsed;
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
           CreateGamaStyle.AbstractlementVisibility  = Visibility.Visible;
        }

        public void Back()
        {
            CreateGamaStyle.AbstractlementVisibility = Visibility.Collapsed;
        }
    }
}
