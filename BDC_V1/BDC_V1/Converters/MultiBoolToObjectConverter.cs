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
    public class MultiBoolToObjectConverter : DependencyObject, IMultiValueConverter
    {
        private static readonly BoolToObjectConverter BoolConverter = new BoolToObjectConverter();

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values == null) || (values.Length < 3))
                throw new ArgumentOutOfRangeException(nameof(values), values?.Length, @"Too few items in array");

            return BoolConverter.Convert(new[] {values[1], values[2]}, targetType, values[0], culture);
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
           return BoolConverter.ConvertBack(value, targetTypes, parameter, culture);
        }
    }
}
