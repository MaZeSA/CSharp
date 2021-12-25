using Battleship.Class;
using Battleship.Commands;
using Battleship.ViewModel.GamePanels;
using Battleship.ViewModel.Interfaces;
using Battleship.ViewModel.Ships;
using LibraryBattleship;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public AbstractVisualStyleClass EndGameStyle { set; get; } = new AbstractVisualStyleClass();
        public AbstractVisualStyleClass StepPanelStyle { set; get; } = new AbstractVisualStyleClass(); 
        public AbstractVisualStyleClass ReadyPanelStyle { set; get; } = new AbstractVisualStyleClass();
        public AbstractVisualStyleClass GPanelStyle { set; get; } = new AbstractVisualStyleClass(); 

        public List<Ship> DeadShips { set; get; } = new List<Ship>();

        public ChatMessage ChatMessage { set; get; } = new ChatMessage ();

        public bool IsReady { set; get; } = false;
        bool enemyReady = false;

        public GPanelView(GameModel gameModel)
        {
            GameModel = gameModel;
            VisualElementsModel = new VisualElementsModel(this);
            EnemyVisualElementsModel = new VisualElementsModel(this);
            VisualElementsModel.DeClick();

            ShipController = new ShipController(this);
            CommandSendChatMessage = new CommandSendChatMessage(this);
            CommandReady = new CommandReady(this);
 
            StepPermission = false;
            IsReady = false;

            EnemyVisualElementsModel.WaitingClientConnect();
            ReadyPanelStyle.AbstractlementVisibility = Visibility.Visible;
            ReadyPanelStyle.AbstractString = "Preparing for game..";
        }

        public void CloseGame()
        {
            TCPClient.Close();
            run = false;
        }

        private void EndGame()
        {
            EndGameStyle.AbstractBackgroundBrush = new SolidColorBrush(Color.FromArgb(200, 72, 170, 230));
            EndGameStyle.AbstractString = "Opponent left the game";
            EndGameStyle.AbstractlementVisibility = Visibility.Visible;
        }

        private void Win()
        {
           EndGameStyle.AbstractBackgroundBrush = new SolidColorBrush(Color.FromArgb(200, 7, 112, 49));
           EndGameStyle.AbstractString  = "You Win!!";
           EndGameStyle.AbstractlementVisibility = Visibility.Visible;
        }
        private void Lost()
        {
            EndGameStyle.AbstractBackgroundBrush = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
            EndGameStyle.AbstractString = "You lost!!";
            EndGameStyle.AbstractlementVisibility = Visibility.Visible;
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
            try
            {
                await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Fire, Data = new Fire { FireType = Fire.Type.Answer, Pointers = keys, DeadShips = GetNewDeadShip() } });
            }
            catch (Exception ex)
            { ChatMessages.Insert(0, new ChatMessage { Message = ex.Message, Who = 0 }); }
        }

        public async void Shot(List<int[]> points)
        {
            try
            {
                if (!StepPermission) return;
                StepPermission = false;

                Dictionary<int[], bool> keys = new Dictionary<int[], bool>();
                foreach (int[] p in points)
                {
                    keys.Add(p, false);
                }

                await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Fire, Data = new Fire { FireType = Fire.Type.Fire, Pointers = keys } });
            }
            catch (Exception ex)
            { ChatMessages.Insert(0, new ChatMessage { Message = ex.Message, Who = 0 }); }
        }

        public void AllReady()
        {
            EnemyVisualElementsModel.BlackEnemyPanelVisibility = Visibility.Collapsed;
            StepPanelStyle.AbstractlementVisibility = Visibility.Visible;
            ReadyPanelStyle.AbstractlementVisibility = Visibility.Collapsed;
        }

        public async void Ready()
        {
            try
            {
                await TCPClient.WriteStramAsync(new Packet { Type = Packet.TypePacket.Ready });

                IsReady = true;
                ReadyPanelStyle.AbstractString = "Ready! ";

                if (IsReady && enemyReady) AllReady();
            }
            catch (Exception ex)
            { ChatMessages.Insert(0, new ChatMessage { Message = ex.Message, Who = 0 }); }
        }
     
        public void GameStarted(TCPClient client)
        {
            TCPClient = client;
            (new CommandOpenMenu()).Execute(this);
           
            WaitStart();
        }

        bool run = true;
        private async void WaitStart()
        {
            run = true;
            try
            {
                while (run)
                {
                    Packet packet = await TCPClient.ReadStreamAsync();
                    
                    switch (packet?.Type)
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
                                if (IsReady && enemyReady) AllReady();

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
                                    if (fire.DeadShips.Count > 0)
                                    {
                                        AddEnemyDeadShip(fire.DeadShips);
                                    }

                                    StepPermission = fire.StepPermission;
                                }
                                break;
                            }
                        case Packet.TypePacket.Stop:
                            {
                                run = false;
                                continue;
                            }
                    }
                    if (!TCPClient.Client.Connected)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                TCPClient.Close();
                EndGame();
            }
        }

        public async void SendMessage()
        {
            try
            {
                ChatMessages.Insert(0, new ChatMessage { Message = ChatMessage.Message, Who = 0 });
                await TCPClient.WriteStramAsync(new Packet { Data = ChatMessage.Message, Type = Packet.TypePacket.Message });
                ChatMessage.Message = "";
            }
            catch (Exception ex)
            { ChatMessages.Insert(0, new ChatMessage { Message = ex.Message, Who = 0 }); }
        }

        bool stepPermission = false;
        public bool StepPermission
        {
            set
            {
                if (value)
                {
                    StepPanelStyle.AbstractString = "You Step";
                    StepPanelStyle.AbstractBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 69, 232, 91));
                }
                else
                {
                    StepPanelStyle.AbstractString = "Enemy Step";
                    StepPanelStyle.AbstractBackgroundBrush = new SolidColorBrush(Color.FromArgb(255, 0, 117, 143));
                }
                stepPermission = value;
            }
            get => stepPermission;
        }
           
        public void Show()
        {
            GPanelStyle.AbstractlementVisibility = Visibility.Visible;
        }
        public void Back()
        {
            GPanelStyle.AbstractlementVisibility = Visibility.Collapsed;
        }
    }
}
