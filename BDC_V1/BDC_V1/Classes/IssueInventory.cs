using System.Collections.ObjectModel;
using System.Linq;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    //<!-- TODO: Collapse QaInventoryView and QaInspectionView into a single source -->

    /// <inheritdoc cref="IIssueInventory"/>
    public class IssueInventory : QcIssueBase, IIssueInventory
    {
        /// <inheritdoc />
        public CommentBase InventoryComment
        {
            get => _inventoryComment;
            set => SetProperty(ref _inventoryComment, value);
        }
        private CommentBase _inventoryComment;
    }
}
