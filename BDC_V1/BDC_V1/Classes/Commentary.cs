using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;

namespace BDC_V1.Classes
{
    public class Commentary : PropertyBase, ICommentary
    {
        // **************** Class enumerations ********************************************** //


        // **************** Class properties ************************************************ //

        public string FacilityId
        {
            get => _facilityId;
            set => SetProperty(ref _facilityId, value);
        }
        private string _facilityId;

        public string CodeIdText
        {
            get => _codeIdText;
            set => SetProperty(ref _codeIdText, value);
        }
        private string _codeIdText;

        public string CommentText
        {
            get => _commentText;
            set => SetProperty(ref _commentText, value);
        }
        private string _commentText;

        public string DCRText => Rating.Description();
        public EnumRatingType Rating
        {
            get => _rating;
            set => SetPropertyFlagged(ref _rating, value, nameof(DCRText));
        }
        private EnumRatingType _rating;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

    }
}
