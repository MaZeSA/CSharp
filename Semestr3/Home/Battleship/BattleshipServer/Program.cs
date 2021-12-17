using LibraryBattleship;
using System;
using System.Collections.Generic;
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
        const int port = 8888; // порт для прослушивания подключений
        static void Main(string[] args)
        {
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

                  //  Console.WriteLine("Отправлено сообщение: {0}", Client.Client.Client.RemoteEndPoint.ToString());
                    // закрываем поток
                    //stream.Close();
                    //// закрываем подключение
                    //client.Close();
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
            Console.WriteLine("Подключен клиент. Выполнение запроса...");


            while (true)
            {
               Packet packet = await ReadData(Client);

                if (packet is null) { }
                else
                {
                    if (packet.Type == Packet.TypePacket.NewGame)
                    {
                        Console.WriteLine("Запрос на створення гри...");
                        (packet.Data as NewGame).StatusGame = NewGame.Status.Find;
                        SendData(Client, packet);
                    }
                }
            }
        }

        static async void SendData(TCPClient client, Packet packet)
        {
            await client.WriteStramAsync(packet);
        }
        static async Task<Packet> ReadData(TCPClient client)
        {
          return await client.ReadStreamAsync();
        }
    }
}
