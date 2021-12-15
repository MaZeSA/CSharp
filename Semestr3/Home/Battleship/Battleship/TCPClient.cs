using Battleship.Class;
using Battleship.Pages.StartPage;
using Battleship.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class TCPClient
    {
        private const int port = 8888;
        private const string server = "127.0.0.1";
 
        TcpClient client { set; get; }
     
        public TCPClient()
        {
            client = new TcpClient();
        }


        public void Connect()
        {
          //  client.Connect(server, port);
        }

        public async void CreateGame(CreateGameModel newGame)
        {
           var re = await Task.Run(()=> MakeNewGame(newGame.NewGame));
           newGame.VisibilityWait = System.Windows.Visibility.Collapsed;
        }

        private async Task<string> MakeNewGame(NewGame newGame)
        {
            client.Connect(server, port);
            NetworkStream stream = client.GetStream();
            byte[] data = newGame.Serializer();
            await stream.WriteAsync(data, 0, data.Length);


            data = new byte[256];
            StringBuilder response = new StringBuilder();
            do
            {
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable); // пока данные есть в потоке

            client.Close();
            stream.Dispose();

            return response.ToString();
        }


            //static void rrr(string[] args)
            //{
            //    try
            //    {



            //        byte[] data = new byte[256];
            //        StringBuilder response = new StringBuilder();
            //        NetworkStream stream = client.GetStream();

            //        do
            //        {
            //            int bytes = stream.Read(data, 0, data.Length);
            //            response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            //        }
            //        while (stream.DataAvailable); // пока данные есть в потоке

            //        Console.WriteLine(response.ToString());

            //        // Закрываем потоки
            //        stream.Close();
            //        client.Close();
            //    }
            //    catch (SocketException e)
            //    {
            //        Console.WriteLine("SocketException: {0}", e);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("Exception: {0}", e.Message);
            //    }

            //    Console.WriteLine("Запрос завершен...");
            //    Console.Read();
            //}
        }
}
