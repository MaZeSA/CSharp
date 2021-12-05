using LibraryScreen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static LibraryScreen.ScreenProtocol;

namespace UDPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                const int port = 2020;
                UdpClient server = new UdpClient(port);
                Console.Title = "Server";
                Console.WriteLine("Wait..");
                IPEndPoint remoteEp = null;
                byte[] info = server.Receive(ref remoteEp);
                var received = Encoding.UTF8.GetString(info);
                Console.WriteLine($"Received {received}, {remoteEp}");

                if (received.Equals("GetScreen", StringComparison.OrdinalIgnoreCase))
                {
                    var screen = GetScreen();

                    var list = ScreenProtocol.Cut(screen, 2048);

                    var header = (new HeaderPacket(screen.Length, list.Count)).Serializer();
                    server.Send(header, header.Length, remoteEp);

                    foreach (var item in list)
                    {
                        var Packet = item.Serializer();
                        server.Send(Packet, Packet.Length, remoteEp);
                        Console.WriteLine("Send Packet " + item.Counter);
                        //Thread.Sleep(50);
                    }

                    server.Close();
                }
            }
        }

        static byte[] GetScreen()
        {
            using (var stream = new MemoryStream())
            {
                Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(printscreen as Image);
                graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

                printscreen.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
