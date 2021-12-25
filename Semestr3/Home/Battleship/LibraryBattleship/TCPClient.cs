using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace LibraryBattleship
{
    public class TCPClient
    {
        public TcpClient Client { set; get; }
        public NetworkStream Stream { set; get; }

        public TCPClient(TcpClient client)
        {
            Client = client;
            Stream = Client.GetStream();
        }
        public TCPClient() {  Client = new TcpClient();   }


        public async Task ConnectAsync(string server, int port)
        {
            Client.Connect(server, port);
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

        public void Close()
        {
            Stream?.Close();
            Client?.Close();
        }
    }
}
