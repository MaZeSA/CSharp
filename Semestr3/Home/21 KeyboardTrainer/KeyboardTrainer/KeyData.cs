using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KeyboardTrainer
{
    public class KeyData 
    {
        public string L_Key { set; get; }
        public string U_Key { set; get; }

        public int Width { set; get; }
        public SolidColorBrush Background { set; get; }

        SolidColorBrush Background1 = new SolidColorBrush(Color.FromRgb(240, 119, 149));
        //var r = new List<KeyData>()
        //    {
        //          new KeyData{ L_Key = "`", U_Key ="~", Width =50, Background =Background1 },
        //        new KeyData{ L_Key = "1", U_Key ="!", Width =50, Background =Background1 },
        //        new KeyData{ L_Key = "2", U_Key ="@", Width =50, Background =Background1 },
        //        new KeyData{ L_Key = "3", U_Key ="#", Width =50, Background =Background1 },
        //        new KeyData{ L_Key = "4", U_Key ="$", Width =50, Background =Background1 },
        //    };
    }
}
