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
    /// <summary>
    ///     Converts a <see cref="EnumRatingType"/> to a <see cref="EnumRatingColors"/>
    /// </summary>
    /// <returns>
    ///     The <see cref="EnumRatingColors"/> corresponding to the <see cref="EnumRatingType"/>
    /// </returns>
    /// <example>
    ///     <code>
    ///        private static readonly RatingToRatingColorConverter Converter = new RatingToRatingColorConverter();
    ///        
    ///        public static EnumRatingColors ToRatingColor(this EnumRatingType value) => 
    ///            (EnumRatingColors) (Converter.Convert(value, typeof(EnumRatingColors), null, null) ?? EnumRatingColors.Green);
    ///     </code>
    /// </example>
    public class RatingToRatingColorConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        ///     Converts <see cref="EnumRatingType"/> to a <see cref="EnumRatingColors"/>
        /// </summary>
        /// <param name="value"><see cref="EnumRatingType"/></param>
        /// <param name="targetType"><see cref="EnumRatingColors"/></param>
        /// <param name="parameter">not used</param>
        /// <param name="culture">not used</param>
        /// <returns>
        ///     <see cref="EnumRatingColors"/>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [NotNull]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(EnumRatingColors))
                throw new ArgumentException(@"Invalid target type=" + targetType,  nameof(targetType));

            if (value == null) throw new ArgumentNullException(nameof(value));
            try
            {
                if (!(value is EnumRatingType ratingType))
                    ratingType = (EnumRatingType) Enum.Parse(typeof(EnumRatingType), value.ToString());

                switch (ratingType)
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

                    case EnumRatingType.G:
                    case EnumRatingType.GPlus:
                    case EnumRatingType.GMinus:
                        return EnumRatingColors.Green;

                    case EnumRatingType.None:
                        return EnumRatingColors.None;

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

            throw new ArgumentOutOfRangeException(nameof(value), value, @"Value is not a known Rating Type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
