using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming
namespace BDC_V1.Enumerations
{
    public enum Enum_A10_SubsystemTypes
    {
        [Description("Standard Foundations")]
        A1010 
    }

    public enum Enum_A20_SubsystemTypes
    {
        None
    }

    public enum Enum_B10_SubsystemTypes
    {
        None
    }

    public enum Enum_B20_SubsystemTypes
    {
        None
    }

    public enum Enum_B30_SubsystemTypes
    {
        None
    }

    public enum Enum_C10_SubsystemTypes
    {
        None
    }

    public enum Enum_C20_SubsystemTypes
    {
        None
    }

    public enum Enum_C30_SubsystemTypes
    {
        [Description("WALL FINISHES")]
        C3010,

        [Description("FLOOR FINISHES")]
        C3020,

        [Description("CEILING FINISHES")]
        C3030,

        [Description("INT COATINGS / SPECIAL FINISHES")]
        C3040
    }

    public enum Enum_D10_SubsystemTypes
    {
        None
    }

    public enum Enum_D20_SubsystemTypes
    {
        None
    }

    public enum Enum_D30_SubsystemTypes
    {
        [Description("ENERGY SUPPLY")]
        D3010,

        [Description("HEAT GENERATING SYSTEMS")]
        D3020
    }

    public enum Enum_D40_SubsystemTypes
    {
        None
    }

    public enum Enum_D50_SubsystemTypes
    {
        [Description("POWER DISTRIBUTION SYSTEMS")]
        D5010,
    }

}
