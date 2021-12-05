using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LibraryScreen
{
   
    public static class ScreenProtocol
    {
        [Serializable]
        public class HeaderPacket
        {
            public HeaderPacket(int size, int packetCount)
            {
                Size = size;
                PacketCount = packetCount;
            }
            public int Size { get; }
            public int PacketCount { get; }

        }
        [Serializable]
        public class Packet
        {
            public Packet(int i) => Counter = i;

            public int Counter { get; }
            public byte[] Data { get; set; }
            public long StartRead { get; set; }
            public long EndRead { get; set; }
        }

        public static List<Packet> Cut(byte[] data, int sizePack)
        {
            List<Packet> packets = new List<Packet>();

            if (data.Length <= 2048)
            {
                var packet = new Packet(0) { StartRead = 0, EndRead = data.Length, Data = data };
                packets.Add(packet);
            }
            else
            {
                using (MemoryStream memStream = new MemoryStream(data))
                {
                    Packet Packet = null;
                    int Counter = 0;
                    do
                    {
                        Packet = new Packet(Counter++);
                        var size = data.Length - memStream.Position > sizePack ? sizePack : data.Length - memStream.Position;
                        var bytes = new byte[size];

                        Packet.StartRead = memStream.Position;
                        Packet.EndRead = memStream.Read(bytes, 0, (int)size);
                        Packet.Data = bytes;

                        packets.Add(Packet);
                    }
                    while (Packet.EndRead >= 2);
                }
            }
            return packets;
        }
    }

    public static class SerializerDeserializerExtensions
    {
        public static byte[] Serializer(this object _object)
        {
            byte[] bytes;
            using (var _MemoryStream = new MemoryStream())
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                _BinaryFormatter.Serialize(_MemoryStream, _object);
                bytes = _MemoryStream.ToArray();
            }
            return bytes;
        }

        public static T Deserializer<T>(this byte[] _byteArray)
        {
            T ReturnValue;
            using (var _MemoryStream = new MemoryStream(_byteArray))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                ReturnValue = (T)_BinaryFormatter.Deserialize(_MemoryStream);
            }
            return ReturnValue;
        }
    }
}
