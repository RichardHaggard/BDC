using System;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class InspectionInfo : PropertyBase, IInspectionInfo
    {
        public EnumInspectionType InspectionType
        {
            get => _inspectionType;
            set => SetProperty(ref _inspectionType, value);
        }
        private EnumInspectionType _inspectionType;

        public string Component
        {
            get => _component;
            set => SetProperty(ref _component, value);
        }
        private string _component;

        public string Section
        {
            get => _section;
            set => SetProperty(ref _section, value);
        }
        private string _section;

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }
        private string _category;

        public string ComponentType
        {
            get => _componentType;
            set => SetProperty(ref _componentType, value);
        }
        private string _componentType;

        public decimal Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private decimal _quantity;

        public DateTime InspectionDate
        {
            get => _inspectionDate;
            set => SetProperty(ref _inspectionDate, value);
        }
        private DateTime _inspectionDate;

        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
        private string _note;

        public bool HasInspectionComments => InspectionComments.HasItems;
        public INotifyingCollection<CommentInspection> InspectionComments =>
            PropertyCollection<CommentInspection>(ref _inspectionComments, nameof(HasImages));
        [CanBeNull] private INotifyingCollection<CommentInspection> _inspectionComments;

        // on-demand collection storage is allocated on first use
        // use the Has... properties to check for not empty
        public bool HasImages => Images.HasItems;
        public INotifyingCollection<ImageSource> Images =>
            PropertyCollection<ImageSource>(ref _images, nameof(HasImages));
        [CanBeNull] private INotifyingCollection<ImageSource> _images;
    }
}
