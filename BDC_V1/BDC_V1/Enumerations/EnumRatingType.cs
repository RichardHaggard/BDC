using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Enumerations
{
    [DefaultValue(Unknown)]
    public enum EnumRatingType
    {
        [Description("?")]
        Unknown,

        [Description("G+")]
        GPlus,

        [Description("G")]
        G,

        [Description("G-")]
        GMinus,

        [Description("Y+")]
        YPlus,

        [Description("Y")]
        Y,

        [Description("Y-")]
        YMinus,

        [Description("A+")]
        APlus,

        [Description("A")]
        A,

        [Description("A-")]
        AMinus,

        [Description("R+")]
        RPlus,

        [Description("R")]
        R,

        [Description("R-")]
        RMinus
    }

    public static class EnumRatingConverter
    {
        public static EnumRatingColors ToRatingColor(this EnumRatingType value)
        {
            switch (value)
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

                //case EnumRatingType.Unknown:
                //case EnumRatingType.G:
                //case EnumRatingType.GPlus:
                //case EnumRatingType.GMinus:
                default:
                    return EnumRatingColors.Green;
            }
        }
    }
}
