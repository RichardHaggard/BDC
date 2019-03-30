using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.Interfaces
{
    public interface IInventoryType
    {
        string FacilityId  { get; set; }
        string SystemId    { get; set; }
        string ComponentId { get; set; }
        string TypeName    { get; set; }
        string SectionName { get; set; }
        string InventIssue { get; set; }
    }
}
