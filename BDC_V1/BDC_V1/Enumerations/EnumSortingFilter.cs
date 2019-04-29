using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Enumerations
{
    [DefaultValue(None)]
    public enum EnumSortingFilter
    {
        None,

        [Description("Fac. ID")]
        FacilityId,

        [Description("Sys.")]
        SystemId,

        [Description("Comp.")]
        InventoryId,

        [Description("Type")]
        TypeId,

        [Description("Section")]
        SectionName,

        [Description("DCR")]
        Rating,

        [Description("Inventory Issue")]
        InventoryIssue,

        [Description("Inspection Issue")]
        InspectionIssue,
    }
}
