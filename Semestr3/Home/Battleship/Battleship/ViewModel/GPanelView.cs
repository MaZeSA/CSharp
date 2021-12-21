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

        public List<Ship> DeadShips { set; get; } = new List<Ship>();

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

        private void Win()
        {
            EndGameBackgroundBrush = new SolidColorBrush(Color.FromArgb(150, 7, 112, 49));
            EndGameString = "You Win!!";
            EndGameElementVisibility = Visibility.Visible;
        }
        private void Lost()
        {
            EndGameBackgroundBrush = new SolidColorBrush(Color.FromArgb(150, 255, 0, 0));
            EndGameString = "You lost!!";
            EndGameElementVisibility = Visibility.Visible;
        }

        int countEnemyDeadShip = 0;
        private void AddEnemyDeadShip(List<DeadShip> deadShips)
        {
            foreach(var s in deadShips )
            {
                Ship ship = null;
                switch(s.Type)
                {
                    case ShipType.Corvette:
                        {
                            ship = new ShipCorvette(this);
                            break;
                        }
                    case ShipType.Cruiser:
                        {
                            ship = new ShipCruiser(this);
                            break;
                        }
                    case ShipType.Destroyer:
                        {
                            ship = new ShipDestroyer(this);
                            break;
                        }
                    case ShipType.Frigate:
                        {
                            ship = new ShipFrigate(this);
                            break;
                        }

                }
                ship.Column = s.Column;
                ship.Row = s.Row;
                ship.RowSpan = s.RowSpan;
                ship.ColumnSpan = s.ColumnSpan;
              
                ship.Life = false;

                EnemyVisualElementsModel.AddVisibleObj(ship);
                countEnemyDeadShip++;

                if (countEnemyDeadShip == ShipController.Ships.Count) Win();
            }
        }

        public List<DeadShip> GetNewDeadShip()
        {
            var res = new List<DeadShip>();
          
            foreach(var ship in ShipController.Ships)
            {
                if(ship.Life== false)
                {
                    if(DeadShips.IndexOf(ship) <0)
                    {
                        DeadShips.Add(ship);

                        DeadShip deadShip = new DeadShip
                        {
                            Column = ship.Column,
                            Row = ship.Row,
                            ColumnSpan = ship.ColumnSpan,
                            RowSpan = ship.RowSpan,
                            Type = (ShipType)ship.Length
                        };

                        res.Add(deadShip);
                        if (DeadShips.Count == ShipController.Ships.Count) Lost();
                    }
                }
            }
            return res;
        }

        public async void ReturnShot(Dictionary<int[], bool> keys)
        {
            await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Fire, Data = new Fire { FireType = Fire.Type.Answer, Pointers = keys, DeadShips = GetNewDeadShip() } });
        }

        public async void Shot(List<int[]> points)
        {
            if (!StepPermission) return;
            StepPermission = false;

            Dictionary<int[], bool> keys = new Dictionary<int[], bool>();
            foreach (int[] p in points)
            {
                keys.Add(p, false);
            }
            
            await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Fire, Data = new Fire { FireType = Fire.Type.Fire, Pointers = keys } }) ;
        }

        public void AllReady()
        {
            EnemyVisualElementsModel.BlackEnemyPanelVisibility = Visibility.Collapsed;
            StepPanelVisibility = Visibility.Visible;
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
                            var fire = packet.Data as Fire;

                            if (fire.FireType == Fire.Type.Fire)
                            {
                                bool stepP = true;

                                Dictionary<int[], bool> keys = new Dictionary<int[], bool>();
                                foreach (var t in fire.Pointers.Keys)
                                {
                                    bool result = VisualElementsModel.Shot(t[0], t[1], false);
                                    keys.Add(new int[2] { t[0], t[1] }, result);

                                    if (result) stepP = false;
                                }

                                ReturnShot(keys);
                                StepPermission = stepP;
                            }
                            else if (fire.FireType == Fire.Type.Answer)
                            {
                                foreach (var t in fire.Pointers)
                                {
                                    EnemyVisualElementsModel.Marker(t.Key[0], t.Key[1], t.Value);
                                }
                                if(fire.DeadShips.Count >0)
                                {
                                    AddEnemyDeadShip(fire.DeadShips);
                                }

                                StepPermission = fire.StepPermission;
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

        bool stepPermission = false;
        public bool StepPermission
        {
            set
            {
                if (value)
                {
                    StepString = "You Step";
                    BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 69, 232, 91));
                }
                else
                {
                    StepString = "Enemy Step";
                    BackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 117, 143));
                }
                stepPermission = value;
            }
            get => stepPermission;
        }
        string stepString = "Enemy Step";
        public string StepString
        {
            set
            {
                stepString = value; OnNotify();

            }
            get => stepString;
        }
       
        SolidColorBrush endGameBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 117, 143));
        public virtual SolidColorBrush EndGameBackgroundBrush
        {
            get => endGameBackgroundBrush;
            set { endGameBackgroundBrush = value; OnNotify(); }
        }
        Visibility endGameElementVisibility = Visibility.Collapsed;
        public Visibility EndGameElementVisibility
        {
            set { endGameElementVisibility = value; OnNotify(); }
            get => endGameElementVisibility;
        }
        string endGameString = "You win!!";
        public string EndGameString
        {
            set
            {
                endGameString = value; OnNotify();

            }
            get => endGameString;
        }

        SolidColorBrush backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 117, 143));
        public virtual SolidColorBrush BackgroundBrush
        {
            get => backgroundBrush;
            set { backgroundBrush = value; OnNotify(); }
        }
        Visibility visualElementVisibility = Visibility.Collapsed;
        public Visibility VisualElementVisibility
        {
            set { visualElementVisibility = value; OnNotify(); }
            get => visualElementVisibility;
        }

        Visibility stepPanelVisibility = Visibility.Collapsed;
        public Visibility StepPanelVisibility
        {
            set { stepPanelVisibility = value; OnNotify(); }
            get => stepPanelVisibility;
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
