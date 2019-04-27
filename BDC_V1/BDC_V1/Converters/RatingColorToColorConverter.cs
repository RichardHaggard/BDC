using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BDC_V1.Enumerations;
using JetBrains.Annotations;

namespace BDC_V1.Converters
{
    public class RatingColorToColorConverter : DependencyObject, IValueConverter 
    {
        /// <summary>
        ///     Converts <see cref="EnumRatingColors"/> to a <see cref="Color"/>
        /// </summary>
        /// <param name="value"><see cref="EnumRatingColors"/></param>
        /// <param name="targetType"><see cref="Color"/></param>
        /// <param name="parameter">not used</param>
        /// <param name="culture">not used</param>
        /// <returns>
        ///     <see cref="Color"/>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [NotNull]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            if (targetType != typeof(Color))
                throw new ArgumentException(@"Invalid target type=" + targetType,  nameof(targetType));

            if (value == null) throw new ArgumentNullException(nameof(value));
            try
            {
                if (!(value is EnumRatingColors ratingColor)) 
                    ratingColor = (EnumRatingColors) Enum.Parse(typeof(EnumRatingColors), value.ToString());

                switch (ratingColor)
                {
                    case EnumRatingColors.None:   return Colors.White;
                    case EnumRatingColors.Green:  return Colors.LightGreen;
                    case EnumRatingColors.Yellow: return Colors.Yellow;
                    case EnumRatingColors.Amber:  return Color.FromRgb(0xff, 0xbf, 0x00);
                    case EnumRatingColors.Red:    return Colors.Red;
                    default: break;
                }
            }

            // catch exceptions locally so it's easier to debug
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }

            throw new ArgumentOutOfRangeException(nameof(value), value, @"Value is not a known RatingColor Type");
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
