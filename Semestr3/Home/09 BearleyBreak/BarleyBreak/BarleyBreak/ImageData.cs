using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarleyBreak
{
   public class ImageData
    {
        public string Name { set; get; }
        public Image[,] Images { set; get; }
        public Image OriginalImage { set; get; }
        public int X { set; get; }
        public int Y { set; get; }

        public int Width { set; get; }
        public int Height { set; get; }
    }
}
