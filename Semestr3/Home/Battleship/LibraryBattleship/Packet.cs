using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Fire
        } 
        public TypePacket Type { set; get; }
        public object Data { set; get; }
        public string ErrorMessage{ set; get; }

}
}
