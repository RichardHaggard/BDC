using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BDC_V1.Enumerations;

namespace BDC_V1.Converters
{
    public class SystemElementBackgroundConverter : DependencyObject, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Length == 2) &&
                (values[0] is EnumComponentTypes enumComponent) &&
                (enumComponent == EnumComponentTypes.FacilityType) &&
                (values[1] is bool hasAnyQaIssues))
            {
                return hasAnyQaIssues
                    ? System.Windows.Media.Brushes.Red // 1+ QA issues
                    : System.Windows.Media.Brushes.LightGreen; // All QA passes
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("SystemElementBackgroundConverter.ConvertBack");
        }
    }
}
