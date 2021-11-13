using Jsn;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataBase
{
    public class Base
    {
        List<Settlement> Settlements { set; get; } = new List<Settlement>();
      
        public void LoadData()
        {
            try
            {
                var json = File.ReadAllText("data.jsn");
                Settlements = JsonConvert.DeserializeObject<List<Settlement>>(json);
            }
            catch(Exception ex)
            {}
        }

        public string GetListSettlements()
        {
            var t = Settlements?.Select(x => x.SettlementName + "|");

            var res = "";
            foreach (var r in t)
                res += r;

            return res;
        }

        public string GetListStreet(string settlement)
        {
            var settl = Settlements.FirstOrDefault(x => x.SettlementName == settlement);

            var res = "";
            foreach (var r in settl.StreetList)
                res += r + "|";

            return res;
        }
    }
}
