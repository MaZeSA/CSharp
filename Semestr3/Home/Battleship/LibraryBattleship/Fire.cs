using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBattleship
{
    [Serializable]
    public class Fire
    {
        public enum Type
        {
            Fire,
            Answer
        }

        public Type FireType { set; get; } = Type.Fire;
        public Dictionary<int[], bool> Pointers { set; get; } = new Dictionary<int[], bool>();
    }
}
