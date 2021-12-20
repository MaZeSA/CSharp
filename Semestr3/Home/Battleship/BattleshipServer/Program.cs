using LibraryBattleship;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipServer
{
    class Program
    {
        static Dictionary<string, ActualGames> DictionaryActualGames = new Dictionary<string, ActualGames>();

        const int port = 8888; // порт для прослушивания подключений
        static void Main(string[] args)
        {

           var proc= Process.Start(@"D:\Git\Semestr3\Home\Battleship\Battleship\bin\Debug\Battleship.exe");

            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                // запуск слушателя
                server.Start();

                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    WorkClient(new TCPClient(server.AcceptTcpClient()));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
                proc.Kill();
            }
        }


        static async void WorkClient(TCPClient Client)
        {
            string cldata = Client.Client.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Подключен клиент. Выполнение запроса... " + cldata);

            bool run = true;

            while (run)
            {
                Packet packet = await ReadData(Client);

                if (packet is null) { }
                else
                {
                    switch(packet.Type)
                    {
                        case Packet.TypePacket.CreateNewGame:
                            {
                                Console.WriteLine("Запрос на створення гри... " + cldata);

                                packet = AddNewGame(packet, Client);
                                run = false;
                                break;
                            };
                        case Packet.TypePacket.GetListGame:
                            {
                                Console.WriteLine("Запрос списку сворених ігор... " + cldata);

                                packet = GetGames(packet);
                                break;
                            }
                        case Packet.TypePacket.ConectedTo:
                            {
                                Console.WriteLine("Клієнт підключений " + cldata);
                                packet = ConectedToGames(packet, Client);
                                run = false;
                                break;
                            }
                    }
                }

                await SendData(Client, packet);

               // Client.Close();
               // return;
            }
        }

        static async Task SendData(TCPClient client, Packet packet)
        {
            await client.WriteStramAsync(packet);
        }
        static async Task<Packet> ReadData(TCPClient client)
        {
          return await client.ReadStreamAsync();
        }

        static Packet GetGames(Packet packet)
        {
            List<LiteGame> newGames = new List<LiteGame>();
            foreach (var item in DictionaryActualGames.Values)
            {
                if (item.Game.StatusGame == NewGame.Status.Created)
                    newGames.Add(
                        new LiteGame { GameName = item.Game.GameName, SuperWeapon = item.Game.SuperWeapon }
                        );
            }

            packet.Data = newGames;
            return packet;
        }
        static Packet AddNewGame(Packet packet, TCPClient client)
        {
            var game = packet.Data as NewGame;

            if (DictionaryActualGames.ContainsKey(game.GameName))
            {
                packet.ErrorMessage = "Name Game is already!";
                packet.Type = Packet.TypePacket.Error;
            }
            else
            {
                DictionaryActualGames.Add(game.GameName, new ActualGames { Server = client, Game = game });
                game.StatusGame = NewGame.Status.Created;
            }
            
            Console.WriteLine( client.Client.Client.RemoteEndPoint.ToString());
           // packet.Type = Packet.TypePacket.CreateNewGame;
            return packet ;
        }

        static void RemoveGame(string name)
        {
            DictionaryActualGames.Remove(name);
        }

        static Packet ConectedToGames(Packet packet, TCPClient client)
        {
            var game = packet.Data as LiteGame;
            ActualGames actual = null;

            if (DictionaryActualGames.TryGetValue(game.GameName, out actual))
            {
                if (game.Password == actual.Game.Password)
                {
                    actual.Client = client;
                    packet.Type = Packet.TypePacket.Connected;
                    actual.Game.StatusGame = NewGame.Status.Play;
                 
                    actual.GetMessegeConnectToSetver();
                    
                    actual.Lissen(client);
                    actual.Lissen(actual.Server);

                    return packet;
                }
                else
                    packet.ErrorMessage = "Bet password!";
            }
            packet.Type = Packet.TypePacket.Error;
            return packet;
        }

    }
}
