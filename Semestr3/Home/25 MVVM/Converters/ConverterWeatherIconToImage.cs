using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace _09_MVVM.Converters
{
    class ConverterWeatherIconToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parametr1 = value.ToString();
            var parametr0 = parametr1.Length > 1 ? "" : "0";
            var url = "https://developer.accuweather.com/sites/default/files/{0}{1}-s.png";

            BitmapImage bitmap = new BitmapImage();

            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(string.Format(url, parametr0, parametr1), UriKind.Absolute);
                bitmap.EndInit();
            }
            catch
            {
                bitmap = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.non.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
