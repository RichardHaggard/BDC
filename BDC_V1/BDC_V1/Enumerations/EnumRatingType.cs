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

        [Description("R+")]
        RPlus,

        [Description("R")]
        R,

        [Description("R-")]
        RMinus
    }
}
