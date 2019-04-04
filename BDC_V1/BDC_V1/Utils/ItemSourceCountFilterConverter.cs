using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BDC_V1.Utils
{
    public  class ItemSourceCountFilterConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IEnumerable values))
                return value;

            var take = 10;
            if (parameter != null)
                int.TryParse(parameter as string, out take);

            if (take < 1) return value;
            var list = new List<object>();

            var count = 0;
            foreach (var val in values)
            {
                list.Add(val);
                if (++count >= take) break;
            }

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
