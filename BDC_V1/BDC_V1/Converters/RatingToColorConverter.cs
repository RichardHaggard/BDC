using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BDC_V1.Enumerations;

namespace BDC_V1.Converters
{
    public class RatingToRatingColorConverter : DependencyObject, IValueConverter
    {
        // Rating to color
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EnumRatingType rtgValue)) 
                throw new ArgumentException(@"Rating must be an enumerable", nameof(rtgValue));

            if (targetType != typeof(EnumRatingColors))
                throw new ArgumentException(@"Target type must be a rating color enumeration", nameof(targetType));

            switch (rtgValue)
            {
                case EnumRatingType.Y:
                case EnumRatingType.YPlus:
                case EnumRatingType.YMinus:
                    return EnumRatingColors.Yellow;

                case EnumRatingType.A:
                case EnumRatingType.APlus:
                case EnumRatingType.AMinus:
                    return EnumRatingColors.Amber;

                case EnumRatingType.R:
                case EnumRatingType.RPlus:
                case EnumRatingType.RMinus:
                    return EnumRatingColors.Red;

                case EnumRatingType.None:
                case EnumRatingType.G:
                case EnumRatingType.GPlus:
                case EnumRatingType.GMinus:
                    return EnumRatingColors.Green;

                default:
                    throw new ArgumentOutOfRangeException(nameof(rtgValue), rtgValue, 
                        @"Value is not a known Rating Type");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
