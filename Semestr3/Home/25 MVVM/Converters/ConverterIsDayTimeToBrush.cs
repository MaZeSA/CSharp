using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace _09_MVVM.Converters
{
    public class ConverterIsDayTimeToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var th = (bool)value;

            return th ? new SolidColorBrush(Color.FromArgb(120, 200, 200, 200)) : new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
