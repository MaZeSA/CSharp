using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client_
    {
        const int port = 2020;
      
        public Client_()
        {
            GetListSettlements();
        }

        public ObservableCollection<string> SettlementsName { set; get; } = new ObservableCollection<string>();
        public ObservableCollection<string> ListStreets { set; get; } = new ObservableCollection<string>();

        public string selectedItem;
        public string SelectedItem 
        { 
            set 
            {
                selectedItem = value;
                GetListStreet(value);
            }
            get => selectedItem;
        }


        public void GetListStreet(string s)
        {
            ListStreets.Clear();
            var data = GetServerData(s);

            string[] res = data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var r in res)
                ListStreets.Add(r);
        }
 
        public void GetListSettlements()
        {
            var data = GetServerData("_GetListSettlements");

            string[] res = data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var r in res)
                SettlementsName.Add(r);
        }
       
        private string GetServerData(string command)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), port);
                if (client.Connected)
                {
                    client.Send(Encoding.UTF8.GetBytes(command));

                    const int SIZE = 10;
                    int count = 0;
                    var buffer = new byte[SIZE];
                    string data = "";
                    do
                    {
                        int tempCount = client.Receive(buffer);
                        data += Encoding.Unicode.GetString(buffer, 0, tempCount);
                        count += tempCount;
                    } while (client.Available > 0);

                    return data;
                }
                return "";
            }
            catch (SocketException ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
            finally
            {
                client.Close();
            }
        }

    }
}
