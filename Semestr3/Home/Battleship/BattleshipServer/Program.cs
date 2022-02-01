﻿using LibraryBattleship;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BattleshipServer
{
    class Program
    {
      
        static Dictionary<string, ActualGames> DictionaryActualGames = new Dictionary<string, ActualGames>();

        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                var ip = ConfigurationManager.AppSettings["IP"];
                var port = ConfigurationManager.AppSettings["Port"];

                Console.WriteLine("Сервер запущено на: " + ip + ":" + port);

                IPAddress localAddr = IPAddress.Parse(ip);
                server = new TcpListener(localAddr, Int32.Parse(port));

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
            }
        }

        static async void WorkClient(TCPClient Client)
        {
            string cldata = Client.Client.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Подключен клиент. Выполнение запроса... " + cldata);

            bool run = true;
            try
            {
                while (run)
                {
                    Packet packet = await ReadData(Client);

                    switch (packet?.Type)
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
                        case Packet.TypePacket.Stop:
                            {
                                Console.WriteLine("Клієнт відключився " + cldata);
                                run = false;
                                continue;
                            }
                        case Packet.TypePacket.ServerClose:
                            {
                                Environment.Exit(0);
                                break;
                            }
                        case Packet.TypePacket.ServerRestart:
                            {
                                var path = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                                Process.Start(System.IO.Path.Combine(path, "BattleshipServer.exe"));
                                Environment.Exit(0);
                                break;
                            }
                    }

                    if (!Client.Client.Connected)
                    {
                        Console.WriteLine("Підключення розірвано " + cldata);
                        break;
                    }
                    await SendData(Client, packet);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Підключення розірвано " + ex.Message);
            }
            finally
            {
                //Client.Close();
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

                    DictionaryActualGames.Remove(game.GameName);

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