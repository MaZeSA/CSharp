using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsn
{
    public class Settlement
    {
        public string SettlementName { set; get; }
        public List<string> StreetList { set; get; } = new List<string>();
    }
}
