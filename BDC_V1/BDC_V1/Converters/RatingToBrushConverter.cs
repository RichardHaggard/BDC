using System;
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
    public class RatingToBrushConverter : DependencyObject, IValueConverter 
    {
        private static readonly RatingColorToBrushConverter  BrushConverter       = new RatingColorToBrushConverter ();
        private static readonly RatingToRatingColorConverter RatingColorConverter = new RatingToRatingColorConverter();

        /// <summary>
        ///     Converts <see cref="EnumRatingType"/> to a <see cref="Color"/>
        /// </summary>
        /// <param name="value"><see cref="EnumRatingType"/></param>
        /// <param name="targetType"><see cref="Color"/> or <see cref="Brush"/></param>
        /// <param name="parameter">not used</param>
        /// <param name="culture">not used</param>
        /// <returns>
        ///     <see cref="Color"/> or <see cref="Brush"/>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [NotNull]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new ArgumentException(@"Invalid target type=" + targetType,  nameof(targetType));

            if (value == null) throw new ArgumentNullException(nameof(value));
            try
            {
                if (!(value is EnumRatingType ratingType)) 
                    ratingType = (EnumRatingType) Enum.Parse(typeof(EnumRatingType), value.ToString());

                var ratingColor = RatingColorConverter.Convert(ratingType , typeof(EnumRatingColors), parameter, culture);
                var brush       = BrushConverter      .Convert(ratingColor, typeof(Brush), parameter, culture);

                return brush;
            }

            // catch exceptions locally so it's easier to debug
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }
        }
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            throw new NotImplementedException();
        }
    }
}
