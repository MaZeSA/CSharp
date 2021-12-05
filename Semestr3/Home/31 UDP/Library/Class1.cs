using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library
{
    public static class ScreenProtocol
    {
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

        public class Packet
        {
            public Packet(int i) => Counter = i;

            public int Counter { get; }
            public byte[] Data { get; set; }
            public long StartRead { get; set; }
            public long EndRead { get; set; }
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
