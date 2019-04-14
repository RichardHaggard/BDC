using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Converters;
using BDC_V1.Enumerations;

namespace BDC_V1.Utils
{
    public static class EnumRatingExtension
    {
        private static readonly RatingToRatingColorConverter Converter = new RatingToRatingColorConverter();

        public static EnumRatingColors ToRatingColor(this EnumRatingType value) => 
            (EnumRatingColors) (Converter.Convert(value, typeof(EnumRatingColors), null, null) ?? EnumRatingColors.Green);
    }
}
