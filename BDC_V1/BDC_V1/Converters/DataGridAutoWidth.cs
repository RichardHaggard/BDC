using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BDC_V1.Converters
{
    public class DataGridAutoWidth : DependencyObject, IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Length < 2) || 
                ! (values[0] is double windowWidth) ||
                ! (values[1] is double minWidth))
            {
                return Binding.DoNothing;
            }

            var width = Math.Max(windowWidth - 10, minWidth);
            return width;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
