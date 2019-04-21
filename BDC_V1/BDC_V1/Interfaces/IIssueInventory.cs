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
    //<!-- TODO: Collapse QaInventoryView and QaInspectionView into a single source -->

    /// <inheritdoc />
    public interface IIssueInventory : IQcIssueBase
    {
        CommentBase InventoryComment { get; set; }
    }
}
