using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Converters;

namespace BDC_V1.Enumerations
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum EnumControlResult
    {
        ResultDeleteItem = -2,
        ResultCancelled = -1,
        ResultDeferred,
        ResultSaveNow
    }
}
