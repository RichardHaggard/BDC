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
    public class GatedEnumBoolConverter : DependencyObject, IMultiValueConverter
    {
        private readonly EnumBooleanConverter _enumConverter = new EnumBooleanConverter();

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values?.Length >= 2) &&
                (values[1] is bool bEnabled) &&
                (!bEnabled))
            {
                return DependencyProperty.UnsetValue;
            }

            return _enumConverter.Convert(values?[0], targetType, parameter, culture);
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new[]
            {
                _enumConverter.ConvertBack(value, targetTypes?[0], parameter, culture),
                DependencyProperty.UnsetValue
            };
        }
    }
}
