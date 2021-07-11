using System;
using System.Collections.Generic;

namespace Anketa
{
   public class User
    {
        public string Name { set; get; }
        public string Surname { set; get; }
        public DateTime BirthDate { set; get; }
        public int Country { set; get; }
        public string Sex { set; get; }

        public List<string> HobyListCheck = new List<string>();
        public string Other{ set; get; }
    }
}