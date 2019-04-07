using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BDC_V1.Enumerations;

namespace BDC_V1.Converters
{
    public class SystemElementFontWeightConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is EnumComponentTypes enumComponent) &&
                (enumComponent == EnumComponentTypes.FacilityType))
            {
                return FontWeights.Bold;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
