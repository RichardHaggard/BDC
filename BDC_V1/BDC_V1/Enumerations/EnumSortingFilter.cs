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

        [Description("Facility ID")]
        FacilityId,

        [Description("System ID")]
        SystemId,

        [Description("Inventory")]
        InventoryId,

        [Description("Type")]
        TypeId,

        [Description("Section")]
        SectionName,

        [Description("Rtg.")]
        Rating,

        [Description("Inventory Issue")]
        InventoryIssue,

        [Description("Inspection Issue")]
        InspectionIssue,
    }
}
