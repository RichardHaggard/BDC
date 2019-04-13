using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IIssueInventory : INotifyPropertyChanged
    {
        string FacilityId  { get; set; }
        string SystemId    { get; set; }
        string ComponentId { get; set; }
        string TypeName    { get; set; }
        string SectionName { get; set; }

        CommentBase InventoryComment { get; set; }
    }
}
