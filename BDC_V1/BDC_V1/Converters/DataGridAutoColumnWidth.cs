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
    public class DataGridAutoColumnWidth : DependencyObject, IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values.Length < 2) || 
                ! (values[0] is DataGrid grid) ||
                ! (values[1] is double minWidth))            
            {
                return Binding.DoNothing;
            }

            double colWidths = 0;
            foreach (var col in grid.Columns) colWidths += col.ActualWidth;

            var lastWidth = (grid.ActualWidth - colWidths) + grid.Columns.Last().ActualWidth;
            return lastWidth;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
