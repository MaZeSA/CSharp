using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBattleship
{
    public class TCPClient
    {
        private const int port = 8888;
        private const string server = "127.0.0.1";

        public TcpClient Client { set; get; }
        public NetworkStream Stream { set; get; }

        public TCPClient(TcpClient client)
        {
            Client = client;
            Stream = Client.GetStream();
        }
        public TCPClient() { }


        public async Task ConnectAsync()
        {
            Close();

            Client = new TcpClient();
            await Client.ConnectAsync(server, port);
            Stream = Client.GetStream();
        }

        public async Task<bool> WriteStramAsync(Packet packet)
        {
            byte[] data = packet.Serializer();
            await Stream.WriteAsync(data, 0, data.Length);
            return true;
        }

        public async Task<Packet> ReadStreamAsync()
        {
            try
            {
                byte[] data = new byte[Client.ReceiveBufferSize];
                int bytes = await Stream.ReadAsync(data, 0, data.Length);
                Packet result = null;

                using (Stream streamread = new MemoryStream(data))
                {
                    byte[] t = new byte[bytes];
                    streamread.Read(t, 0, bytes);

                    result = t.Deserializer<Packet>();
                }

                return result;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            return null;
        }

        public void Close()
        {
            Stream?.Close();
            Client?.Close();
        }
    }
}
