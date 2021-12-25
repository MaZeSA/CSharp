using System;

namespace LibraryBattleship
{
    [Serializable]
    public class Packet
    {
        public enum TypePacket
        {
            CreateNewGame,
            GetListGame,
            ConectedTo,
            Connected,
            Data,
            Error,
            Message,
            Ready,
            Fire,
            Stop,
            ServerClose,
            ServerRestart
        } 
        public TypePacket Type { set; get; }
        public object Data { set; get; }
        public string ErrorMessage{ set; get; }

}
}
