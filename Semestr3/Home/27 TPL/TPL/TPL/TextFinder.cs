using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL
{
    public class TextFinder
    {
        public string Txt { set; get; }

        public int Sentence { set; get; }


        public async void GetCountSent()
        {
           // var c = await GetCountSentence();
        }


        public void GetCountSentence()
        {
            int count = 0;
            string txt = Txt;

            string temp = "";
            while (txt.Length > 0)
            {
                temp += txt[0];
                if (txt[0] == '.' || txt[0] == '?' || txt[0] == '!' || txt.Length == 1)
                {
                    count++;
                    temp = "";
                }
                txt = txt.Remove(0, 1);
            }
        }
        public void GetCountSymbol()
        {
            // return Txt.Length; 

            int result = 0;
            foreach (char t in Txt)
                result++;
        }
        public void GetCountWord()
        {
            // return Txt.Length; 

            int result = 0;
            foreach (char t in Txt)
                result++;
        }

    }
    //public void GetCountSymbol()
    //{
    //    // return Txt.Length; 

    //    int result = 0;
    //    foreach (char t in Txt)
    //        result++;
    //}
    //public void GetCountWords()
    //{
    //    var res = Txt.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    //}
    //public void GetCountWords()
    //{
    //    var res = Txt.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    //}
}
}
