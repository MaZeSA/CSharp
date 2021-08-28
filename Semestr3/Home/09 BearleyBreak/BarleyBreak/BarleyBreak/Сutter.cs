using System.Drawing;

namespace BarleyBreak
{
   public class Сutter
    {
       public static Bitmap[,] Cut(Image img,int x, int y)
        {
            int widthThird = (int)((double)img.Width / x + 0.5);
            int heightThird = (int)((double)img.Height / y + 0.5);
            Bitmap[,] bmps = new Bitmap[y, x];
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();
                }
            return bmps;
        }
    }
}
 