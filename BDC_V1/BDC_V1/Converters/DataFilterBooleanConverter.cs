using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BDC_V1.Utils;
using BDC_V1.ViewModels;

namespace BDC_V1.Converters
{
    public class DataFilterBooleanConverter : DependencyObject, IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Length < 2) || 
                ! (values[0] is QaInventoryViewModel.EnumSortingFilter filter) ||
                ! (values[1] is string content))            
            {
                return Binding.DoNothing;
            }

            //var columnName = button.Content.ToString().Trim();
            var columnName  = content.Trim();
            var description = filter.Description();
            return (description == columnName);
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var retval = Enumerable.Repeat(Binding.DoNothing, targetTypes.Length).ToArray();
            retval[0] = value;

            return retval;
        }
    }
}
