using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class IssueInspection : PropertyBase, IIssueInspection
    {
        public string FacilityId
        {
            get => _facilityId;
            set => SetProperty(ref _facilityId, value);
        }
        private string _facilityId;

        public string SystemId
        {
            get => _systemId;
            set => SetProperty(ref _systemId, value);
        }
        private string _systemId;

        public string ComponentId
        {
            get => _componentId;
            set => SetProperty(ref _componentId, value);
        }
        private string _componentId;

        public string TypeName
        {
            get => _typeName;
            set => SetProperty(ref _typeName, value);
        }
        private string _typeName;

        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }
        private string _sectionName;

        public string RatingText => Rating.Description();

        public EnumRatingType Rating
        {
            get => _rating;
            set => SetPropertyFlagged(ref _rating, value, nameof(RatingText));
        }
        private EnumRatingType _rating;

        public  bool HasInspectionComments => InspectionComments.HasItems;
        public INotifyingCollection<ICommentInspection> InspectionComments => 
            PropertyCollection<ICommentInspection>(ref _inspectionComments, nameof(HasInspectionComments));
        [CanBeNull] private INotifyingCollection<ICommentInspection> _inspectionComments;
    }
}
