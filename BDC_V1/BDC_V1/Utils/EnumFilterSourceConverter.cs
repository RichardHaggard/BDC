using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.ViewModels;

namespace BDC_V1.Utils
{
    public class EnumFilterSourceConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is CpyCmViewModel.EnumFilterSourceType srcVal))
                return value;

            if (!(parameter is string parStr))
                return value;

            if (!Enum.TryParse(parStr, out CpyCmViewModel.EnumFilterSourceType parVal))
                return value;

            return (parVal == srcVal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
