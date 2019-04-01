using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BDC_V1.Utils
{
    public class RtgToBackgroundConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            if (!(value is string rtgValue)) 
                throw new ArgumentException("Rtg must be a string");

            switch (rtgValue)
            {
                case "R+" : return Brushes.Red;
                case "Y+" : return Brushes.Yellow;
                case "?"  : return Brushes.White;
                default   : return Brushes.White;
            }
        }
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            throw new NotImplementedException();
        }
    }
}
