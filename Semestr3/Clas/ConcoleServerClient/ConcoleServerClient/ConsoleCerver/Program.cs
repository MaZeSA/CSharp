using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleCerver
{
    class Program
    {
        private const int Port = 2020;
        private const int Backlog = 100;
        private const int Size = 100;
        static AutoResetEvent done = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.Title = "Server";
            StartServer();
        }

        private static void StartServer()
        {
            IPHostEntry entry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = entry.AddressList[0];

            Socket server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Bind(new IPEndPoint(ip, Port));
                server.Listen(Backlog);

                while (true)
                {
                    Console.WriteLine("Wait for connection...");
                    server.BeginAccept(AcceptCallback, server);
                    done.WaitOne(); // чекаю на сигнал
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            done.Set(); // встановлюэмо подію в сигнальний стан
            var server = (Socket)ar.AsyncState;
            var client = server.EndAccept(ar);

            var buffer = new byte[Size];
            var data = new // TransferObject -> struct, class implement above
            {
                Socket = client,
                Buffer = buffer,
                Size = Size
            };

            client.BeginReceive(buffer, 0, Size, SocketFlags.None, ReceiveCallback, data);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            var data = (dynamic)ar.AsyncState;
            //  var client = data.GetType().GetProperty("Socket").GetValue(data);
            var client = (Socket)data.Socket;
            var countBytes = client.EndReceive(ar);
            var buffer = (byte[])data.Buffer;

            string result = Encoding.UTF8.GetString(buffer, 0, countBytes);
            Console.WriteLine("Got: {0} from {1}", result, client.RemoteEndPoint);

            if (result == "_GetTime")
            {
                SendTo(client, DateTime.Now.ToLongTimeString());
            }
            else if (result == "_GetDate")
            {
                SendTo(client, DateTime.Now.ToShortDateString());
            }
            else 
            { SendTo(client, "BedComand"); }
        }

        private static void SendTo(Socket client, string data)
        {
            var responce = Encoding.UTF8.GetBytes(data);
            client.BeginSend(responce, 0, responce.Length, SocketFlags.None, SendCallback, client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            var client = (Socket)ar.AsyncState;
            int countBytes = client.EndSend(ar);

            Console.WriteLine("OK. Sent {0} bytes to client", countBytes);

            client.Close();
        }
    }
}
