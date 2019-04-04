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
    public class SystemElementFontSizeConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is EnumComponentTypes enumComponent) &&
                (parameter is double dblSize))
            {
                switch (enumComponent)
                {
                    case EnumComponentTypes.FacilityType:
                        return dblSize * 1.2;

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("SystemElementFontSizeConverter.ConvertBack");
        }
    }
}
