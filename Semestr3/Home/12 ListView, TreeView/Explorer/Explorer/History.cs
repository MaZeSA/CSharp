using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer
{
   public static class History
    {

        static int count = 0;
        static Noda actualNoda { set; get; }
        static Noda backNoda { set; get; }

        public static void SetHistory(string path)
        {
            backNoda = actualNoda;
            actualNoda = new Noda { BackSpace = backNoda, Path = path };
            count++;
        }

        public static string GetHistory()
        {
            if (actualNoda is null)
                return new Noda().Path;

            count--;

            string p = actualNoda.BackSpace.Path;
            actualNoda = actualNoda.BackSpace;
            backNoda = actualNoda.BackSpace;

            return p;
        }

        public static List<string> GetAllHistory()
        {
            var res = new List<string>();
            bool t = true;
            Noda noda = actualNoda.BackSpace;
            while(noda.BackSpace != null)
            {
                res.Add(noda.Path);
                noda = noda.BackSpace;
            }
            return res;
        }

        class Noda
        {
            public Noda BackSpace { set; get; }
            public string Path { set; get; } = "root";
        }
    }
}
