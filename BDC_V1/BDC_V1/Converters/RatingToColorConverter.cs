﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BDC_V1.Enumerations;
using JetBrains.Annotations;

namespace BDC_V1.Converters
{
    /// <summary>
    ///     Converts a <see cref="EnumRatingType"/> to a <see cref="SolidColorBrush"/>
    /// </summary>
    /// <returns>
    ///     The <see cref="SolidColorBrush"/> corresponding to the <see cref="EnumRatingType"/>
    /// </returns>
    /// <example>
    ///     <code>
    ///        private static readonly RatingToRatingColorConverter Converter = new RatingToRatingColorConverter();
    ///        
    ///        public static EnumRatingColors ToRatingColor(this EnumRatingType value) => 
    ///            (EnumRatingColors) (Converter.Convert(value, typeof(EnumRatingColors), null, null) ?? EnumRatingColors.Green);
    ///     </code>
    /// </example>
    public class RatingToColorConverter : DependencyObject, IValueConverter 
    {
        private static readonly RatingColorToColorConverter  ColorConverter       = new RatingColorToColorConverter ();
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
            if (targetType != typeof(Color))
                throw new ArgumentException(@"Invalid target type=" + targetType,  nameof(targetType));

            if (value == null) throw new ArgumentNullException(nameof(value));
            try
            {
                if (!(value is EnumRatingType ratingType)) 
                    ratingType = (EnumRatingType) Enum.Parse(typeof(EnumRatingType), value.ToString());

                var ratingColor = RatingColorConverter.Convert(ratingType , typeof(EnumRatingColors), parameter, culture);
                var color       = ColorConverter      .Convert(ratingColor, typeof(Color), parameter, culture);

                return color;
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
