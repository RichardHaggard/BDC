using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Utils;

namespace BDC_V1.Enumerations
{
    public enum EnumFacilitySystemTypes
    {
        [Description("FOUNDATIONS")]
        A10,

        [Description("BASEMENT CONSTRUCTION")]
        A20,

        [Description("SUPERSTRUCTURE")]
        B10,

        [Description("EXTERIOR ENCLOSURE")]
        B20,

        [Description("ROOFING")]
        B30,

        [Description("INTERIOR CONSTRUCTION")]
        C10,

        [Description("STAIRS")]
        C20,

        [Description("INTERIOR FINISHES")]
        C30,

        [Description("CONVEYING")]
        D10,

        [Description("PLUMBING")]
        D20,

        [Description("HVAC")]
        D30,

        [Description("FIRE PROTECTION")]
        D40,

        [Description("POWER DISTRIBUTION")]
        D50
    }
}
