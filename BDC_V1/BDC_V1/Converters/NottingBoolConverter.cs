using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BDC_V1.Converters
{
    public class NottingBoolConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (! (value is bool bValue)) throw new ArgumentException(@"value is not a bool", nameof(value));
            if (targetType != typeof(bool)) throw new ArgumentException(@"target is not a bool", nameof(targetType));
            return !bValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, null, culture);
        }
    }
}
