using LibraryBattleship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipServer
{
    public class ActualGames
    {
        public TCPClient Server { set; get; }
        public TCPClient Client { set; get; } 
        
        public int Step { set; get; } = 0;

        public NewGame Game { set; get; }

        public async void Lissen(TCPClient client)
        {
            TCPClient frend = client == Server ? Client : Server;

            string ip = client.Client.Client.RemoteEndPoint.ToString();

            Console.WriteLine("Клієнт почав слухати сервер... " + ip);
            try
            {
                while (true)
                {
                    Packet packet = await ReadData(client);

                    switch (packet?.Type)
                    {
                        case Packet.TypePacket.Message:
                            {
                                Console.WriteLine("Відправлення повідомлення до... " + ip);
                                await SendData(frend, packet);
                                break;
                            }
                        case Packet.TypePacket.Ready:
                            {
                                Console.WriteLine("Гравець готовий... " + ip);
                                await SendData(frend, packet);
                                break;
                            }
                        case Packet.TypePacket.Fire:
                            {
                                if ((packet.Data as Fire).FireType == Fire.Type.Answer)
                                {
                                    Console.WriteLine("Гравець повернув пезультат пострілу для" + ip);
                                }
                                else
                                {
                                    Console.WriteLine("Гравець здійснив постріл по  " + ip);
                                }
                                await SendData(frend, packet);
                                break;
                            }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Client.Close();
                Server.Close();
            }
        }

        async Task SendData(TCPClient client, Packet packet)
        {
            await client.WriteStramAsync(packet);
        }
        async Task<Packet> ReadData(TCPClient client)
        {
            return await client.ReadStreamAsync();
        }


        public async void GetMessegeConnectToSetver()
        {
           await Server.WriteStramAsync(new Packet { Type = Packet.TypePacket.Connected });
        }

       
    }
}
