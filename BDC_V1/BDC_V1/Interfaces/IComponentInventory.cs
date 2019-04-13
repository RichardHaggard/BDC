using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    [SuppressMessage("ReSharper", "InvalidXmlDocComment")]
    public interface IComponentInventory : IComponentBase, INotifyPropertyChanged
    {
        IInventorySection Section { get; }
        IInventoryDetail  Detail  { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the <see cref="HasInspectionIssues"/> property to check for not empty
        /// </remarks>
        [NotNull] 
        ObservableCollection<IssueInspection> InspectionIssues { get; }

        /// <remarks>
        /// on-demand collection storage is allocated on first us
        /// use the <see cref="HasInventoryIssues"/> property to check for not empty
        /// </remarks>
        [NotNull] 
        ObservableCollection<IssueInventory> InventoryIssues { get; }
    }
}
