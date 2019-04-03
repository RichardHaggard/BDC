using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class CommentaryType : BindableBase, ICommentaryType
    {
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
            set
            {
                if (SetProperty(ref _rating, value))
                    RaisePropertyChanged(nameof(DCRText));
            }
        }
        private EnumRatingType _rating;
    }
}
