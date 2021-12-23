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

        TCPClient firstStep { set; get; }

        public async void Lissen(TCPClient client)
        {
            TCPClient frend = client == Server ? Client : Server;

            string ip = client.Client.Client.RemoteEndPoint.ToString();

            Console.WriteLine("Клієнт почав слухати сервер... " + ip);
            try
            {
                bool run = true;

                while (run)
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

                                if (firstStep is null) firstStep = client;
                                else
                                {
                                    packet.Type = Packet.TypePacket.Fire;
                                    packet.Data = new Fire { FireType = Fire.Type.Answer, StepPermission = true };
                                    await SendData(firstStep, packet);
                                }
                                break;
                            }
                        case Packet.TypePacket.Fire:
                            {
                                var fire = (packet.Data as Fire);

                                if (fire.FireType == Fire.Type.Answer)
                                {
                                    Console.WriteLine("Гравець повернув пезультат пострілу для" + ip);
                                    fire.StepPermission = false;
                                    foreach (var r in fire.Pointers.Values)
                                    {
                                        if (r)
                                        {
                                            fire.StepPermission = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Гравець здійснив постріл по  " + ip);
                                }
                                await SendData(frend, packet);
                                break;
                            }
                        case Packet.TypePacket.Stop:
                            {
                                run = false;
                                continue;
                            }
                    }

                    if (!Client.Client.Connected)
                    {
                        Console.WriteLine("Клієнт відключився " + ip);
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
                Client.Close();

                try
                {
                    if (!frend.Client.Connected)
                    {
                        Console.WriteLine("Клієнт відключився, передача напарнику " + ip);
                        await SendData(frend, new Packet { Type = Packet.TypePacket.Stop });
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }

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
