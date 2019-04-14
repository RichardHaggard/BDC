using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Converters;

namespace BDC_V1.Enumerations
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter)), DefaultValue(None)]
    public enum EnumRatingType
    {
        [Description("?")]
        None,

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
}
