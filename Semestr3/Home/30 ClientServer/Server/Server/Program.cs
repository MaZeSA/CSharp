using Server.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static Base DataBase { set; get; } = new Base();
       
        static void Main(string[] args)
        {
            LoadData();
            
            Console.WriteLine("Wait...");
            CreateSocket();
        }

        static void LoadData()
        {
            DataBase.LoadData();
            Console.WriteLine("List Settlements:");
            Console.WriteLine(DataBase.GetListSettlements());
            
        }

        private static void CreateSocket()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            const int PORT = 2020;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            try
            {
                server.Bind(ep);

                const int BACKLOG = 10;
                server.Listen(BACKLOG);

                while (true)
                {
                    Socket client = server.Accept(); // wait for a client

                    const int SIZE = 10;
                    int count = 0;
                    var buffer = new byte[SIZE];
                    string data = "";
                    do
                    {
                        int tempCount = client.Receive(buffer);
                        data += Encoding.UTF8.GetString(buffer, 0, tempCount);
                        count += tempCount;
                    } while (client.Available > 0);

                    Console.WriteLine("Received: " + data);
                    Console.WriteLine("Received from: " + client.RemoteEndPoint);

                    if(data == "_GetListSettlements")
                    {
                        client.Send(Encoding.Unicode.GetBytes(DataBase.GetListSettlements()));
                    }
                    else
                    {
                        client.Send(Encoding.Unicode.GetBytes(DataBase.GetListStreet(data)));
                    }

                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
