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
    public enum EnumRepairType
    {
        None = -1,
        Repair,
        Replace
    }
}
