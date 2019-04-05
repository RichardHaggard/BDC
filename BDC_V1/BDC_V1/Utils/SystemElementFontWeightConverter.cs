using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.ViewModels;

namespace BDC_V1.Utils
{
    public class SystemElementFontWeightConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EnumComponentTypes enumComponent)
            {
                switch (enumComponent)
                {
                    case EnumComponentTypes.FacilityType:
                        return FontWeights.Bold;

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
            return Binding.DoNothing;
        }
    }
}
