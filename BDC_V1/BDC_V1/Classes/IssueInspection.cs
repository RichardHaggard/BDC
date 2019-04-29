using System.Collections.ObjectModel;
using System.Linq;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    /// <inheritdoc cref="IIssueInspection"/>
    public class IssueInspection : QcIssueBase, IIssueInspection
    {
        /// <inheritdoc />
        public override bool HasRating => true;

        /// <inheritdoc />
        public override EnumRatingType Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }
        private EnumRatingType _rating;

        /// <inheritdoc />
        public override CommentBase Comment
        {
            get => InspectionComment;
            set => InspectionComment = value;
        }

        /// <inheritdoc />
        public CommentBase InspectionComment
        {
            get => _comment;
            set => SetPropertyFlagged(ref _comment, value, nameof(Comment));
        }
        private CommentBase _comment;
    }
}
