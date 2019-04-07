using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BDC_V1.Enumerations;

namespace BDC_V1.Converters
{
    public class RtgToBackgroundConverter : DependencyObject, IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            if (!(value is EnumRatingType rtgValue)) 
                throw new ArgumentException("Rating must be an enumerable");

            switch (rtgValue)
            {
                case EnumRatingType.G:
                case EnumRatingType.GPlus:
                case EnumRatingType.GMinus:
                    return Brushes.Green;

                case EnumRatingType.Y:
                case EnumRatingType.YPlus:
                case EnumRatingType.YMinus:
                    return Brushes.Yellow;

                case EnumRatingType.R:
                case EnumRatingType.RPlus:
                case EnumRatingType.RMinus:
                    return Brushes.Red;

                default:
                    return Brushes.White;
            }
        }
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            throw new NotImplementedException();
        }
    }
}
