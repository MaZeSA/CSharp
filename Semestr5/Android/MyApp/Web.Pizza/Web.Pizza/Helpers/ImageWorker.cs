using System.Drawing;
using System.Drawing.Imaging;

namespace Web.Pizza.Helpers
{

    public static class ImageWorker
    {
        public static Bitmap FromBase64StringToImage(this string base64String)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            try
            {
                using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
                {
                    memoryStream.Position = 0;
                    Image imgReturn;
                    imgReturn = Image.FromStream(memoryStream);
                    memoryStream.Close();
                    byteBuffer = null;
                    return new Bitmap(imgReturn);
                }
            }
            catch { return null; }
        }

        public static string SaveImage(string imageBase64)
        {
            string base64 = imageBase64;
            if (base64.Contains(","))
                base64 = base64.Split(',')[1];

            var img = base64.FromBase64StringToImage();
            string fileName = Path.GetRandomFileName() + ".jpg";
            string dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
           
            try
            {
                img.Save(dirSave, ImageFormat.Jpeg);
            }
            catch
            {
                fileName = "";
            }
            return fileName;
        }
        public static void RemoveImage(string fileName)
        {
            try
            {
                string file = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
