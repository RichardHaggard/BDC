using System.Collections.ObjectModel;
using System.Linq;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    //<!-- TODO: Collapse QaInventoryView and QaInspectionView into a single source -->

    /// <inheritdoc cref="IIssueInspection"/>
    public class IssueInspection : QcIssueBase, IIssueInspection
    {
        /// <inheritdoc />
        public string RatingText => Rating.Description();

        /// <inheritdoc />
        public EnumRatingType Rating
        {
            get => _rating;
            set => SetPropertyFlagged(ref _rating, value, nameof(RatingText));
        }
        private EnumRatingType _rating;

        /// <inheritdoc />
        public CommentBase InspectionComment
        {
            get => _inspectionComment;
            set => SetProperty(ref _inspectionComment, value);
        }
        private CommentBase _inspectionComment;
    }
}
