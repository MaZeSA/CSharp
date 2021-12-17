using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBattleship
{
    [Serializable]
    public class NewGame
    {
        public string GameName { get; set; }

        public string Password { get; set; }

        public bool SuperWeapon { get; set; }

        public Status StatusGame { get; set; }
        public enum Status
        {
            New,
            Find
        }
    }
}
