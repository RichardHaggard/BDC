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
    public class RatingColorToBrushConverter : DependencyObject, IValueConverter
    {
        private static readonly RatingColorToColorConverter ColorConverter = new RatingColorToColorConverter();

        /// <summary>
        ///     Converts <see cref="EnumRatingColors"/> to a <see cref="Color"/>
        /// </summary>
        /// <param name="value"><see cref="EnumRatingColors"/></param>
        /// <param name="targetType"><see cref="Brush"/></param>
        /// <param name="parameter">not used</param>
        /// <param name="culture">not used</param>
        /// <returns>
        ///     <see cref="Brush"/>
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
                if (!(value is EnumRatingColors ratingColor)) 
                    ratingColor = (EnumRatingColors) Enum.Parse(typeof(EnumRatingColors), value.ToString());

                // this should never come back null
                var color = (Color) ColorConverter.Convert(ratingColor, typeof(Color), parameter, culture);

                return new SolidColorBrush(color);
            }

            // catch exceptions locally so it's easier to debug
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                throw;
            }
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
