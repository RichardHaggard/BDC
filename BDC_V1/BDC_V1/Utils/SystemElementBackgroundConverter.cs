using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BDC_V1.Enumerations;

namespace BDC_V1.Utils
{
    public class SystemElementBackgroundConverter : DependencyObject, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Length == 2) &&
                (values[0] is EnumComponentTypes enumComponent) &&
                (values[1] is bool hasQaIssues))
            {
                switch (enumComponent)
                {
                    case EnumComponentTypes.FacilityType:
                        return hasQaIssues
                            ? System.Windows.Media.Brushes.Red // 1+ QA issues
                            : System.Windows.Media.Brushes.LightGreen; // All QA passes

                    case EnumComponentTypes.None:
                    case EnumComponentTypes.SystemType:
                    case EnumComponentTypes.SubsystemType:
                    case EnumComponentTypes.ComponentType:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("SystemElementBackgroundConverter.ConvertBack");
        }
    }
}
