using Battleship.Class;
using Battleship.Commands;
using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
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
using System.Windows.Media;

namespace Battleship.ViewModel
{
    public class GPanelView: IMenu
    {   
        public VisualElementsModel VisualElementsModel { set; get; }
        public VisualElementsModel EnemyVisualElementsModel { set; get; }
        public ShipController ShipController { set; get; }
        public GameModel GameModel { get; }
        TCPClient TCPClient { set; get; }
        public ObservableCollection<ChatMessage> ChatMessages { set; get; } = new ObservableCollection<ChatMessage>();
        public CommandSendChatMessage CommandSendChatMessage { set; get; }
        public CommandReady CommandReady { set; get; }

        bool meReady = false;
        bool enemyReady = false;

        public GPanelView(GameModel gameModel)
        {
            GameModel = gameModel;
            VisualElementsModel = new VisualElementsModel(this);
            EnemyVisualElementsModel = new VisualElementsModel(this);
            ShipController = new ShipController(this);
            CommandSendChatMessage = new CommandSendChatMessage(this);
            CommandReady = new CommandReady(this);

            EnemyVisualElementsModel.WaitingClientConnect();

            //ChatMessages.Add(new ChatMessage { Message = "test mesage", Who = HorizontalAlignment.Left, BackgroundBrush = Brushes.LightSeaGreen }); 
            //ChatMessages.Add(new ChatMessage { Message = "Big test mesage", Who = HorizontalAlignment.Right, BackgroundBrush = Brushes.LightGreen });
        }

        public async void Shot(List<int[]> points)
        { 
            Dictionary<int[], bool> keys = new Dictionary<int[], bool>();
            foreach (int[] p in points)
            {
                keys.Add(p, false);

                EnemyVisualElementsModel.Shot(p[0], p[1]);
            }

            await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Fire, Data = new Fire { FireType = Fire.Type.Fire, Pointers = keys } }) ;
        }

        public void AllReady()
        {
            EnemyVisualElementsModel.BlackEnemyPanelVisibility = Visibility.Collapsed;
        }

        public async void Ready()
        {
            await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Ready });
           
            meReady = true;
            if (meReady && enemyReady) AllReady();
        }
     
        public void GameStarted(TCPClient client)
        {
            TCPClient = client;
            (new CommandOpenMenu()).Execute(this);
           
            WaitStart();
        }


        private async void WaitStart()
        {
            while (true)
            {
                Packet packet = await TCPClient.ReadStreamAsync();

                switch (packet.Type)
                {
                    case Packet.TypePacket.Connected:
                        {
                            EnemyVisualElementsModel.ClientConnect();
                            break;
                        }
                    case Packet.TypePacket.Message:
                        {
                            ChatMessages.Insert(0, new ChatMessage { Message = packet.Data.ToString(), Who = HorizontalAlignment.Right, BackgroundBrush = Brushes.LightGreen });
                            break;
                        }
                    case Packet.TypePacket.Ready:
                        {
                            EnemyVisualElementsModel.ClientReady();
                           
                            enemyReady = true;
                            if (meReady && enemyReady) AllReady();
                          
                            break;
                        }
                    case Packet.TypePacket.Fire:
                        {
                            foreach (var t in (packet.Data as Fire).Pointers.Keys)
                            {
                                VisualElementsModel.Shot(t[0], t[1]);
                            }
                            break;
                        }

                }
            }
        }

        public async void SendMessage(string message)
        {
            ChatMessages.Insert(0, new ChatMessage { Message = message, Who = 0 });
            await TCPClient.WriteStramAsync(new Packet { Data = message, Type = Packet.TypePacket.Message });
        }

        Visibility visualElementVisibility = Visibility.Collapsed;
        public Visibility VisualElementVisibility
        {
            set { visualElementVisibility = value; OnNotify(); }
            get => visualElementVisibility;
        }

        public void Show()
        {
            VisualElementVisibility = Visibility.Visible;
        }

        public void Back()
        {
            VisualElementVisibility = Visibility.Collapsed;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotify([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
