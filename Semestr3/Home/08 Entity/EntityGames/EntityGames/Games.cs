using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGames
{
  public  class Games
    {
        public int Id { set; get; }
        public string NameGame { set; get; }
        public string Studio { set; get; }

        public string Style { set; get; }

        public string GameMode { set; get; }
        public int Count { set; get; }
        public DateTime Date { set; get; }
    }
}
