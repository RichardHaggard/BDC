using System.Collections.ObjectModel;
using System.Linq;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    /// <inheritdoc cref="IIssueInventory"/>
    public class IssueInventory : QcIssueBase, IIssueInventory
    {
        /// <inheritdoc />
        public override bool HasRating => false;

        public override EnumRatingType Rating
        {
            get => EnumRatingType.None;
            set { }
        }

        /// <inheritdoc />
        public override CommentBase Comment
        {
            get => InventoryComment;
            set => InventoryComment = value;
        }

        /// <inheritdoc />
        public CommentBase InventoryComment
        {
            get => _comment;
            set => SetPropertyFlagged(ref _comment, value, nameof(Comment));
        }
        private CommentBase _comment;
    }
}
