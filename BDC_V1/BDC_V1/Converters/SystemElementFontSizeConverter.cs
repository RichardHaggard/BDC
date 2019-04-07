using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BDC_V1.Enumerations;

namespace BDC_V1.Converters
{
    public class SystemElementFontSizeConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is EnumComponentTypes enumComponent) &&
                (enumComponent == EnumComponentTypes.FacilityType) &&
                (parameter is double dblSize))
            {
                return dblSize * 1.2;
            }
 
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("SystemElementFontSizeConverter.ConvertBack");
        }
    }
}
