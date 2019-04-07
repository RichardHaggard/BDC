using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class ComponentFacility : ComponentBase, IComponentFacility
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public override EnumComponentTypes ComponentType => EnumComponentTypes.FacilityType;

        public EnumConstType ConstType
        {
            get => _constType;
            set => SetProperty(ref _constType, value);
        }
        private EnumConstType _constType;

        public string BuildingId
        {
            get => ComponentName;
            set => ComponentName = value;
        }

        public uint BuildingIdNumber
        {
            get => _buildingIdNumber;
            set => SetProperty(ref _buildingIdNumber, value);
        }
        private uint _buildingIdNumber;

        public string BuildingName
        {
            get => _buildingName;
            set => SetProperty(ref _buildingName, value);
        }
        private string _buildingName;

        public string BuildingUse
        {
            get => _buildingUse;
            set => SetProperty(ref _buildingUse, value);
        }
        private string _buildingUse;

        public int YearBuilt
        {
            get => _yearBuilt;
            set => SetProperty(ref _yearBuilt, value);
        }
        private int _yearBuilt;

        public string AlternateId
        {
            get => _alternateId;
            set => SetProperty(ref _alternateId, value);
        }
        private string _alternateId;

        public string AlternateIdSource
        {
            get => _alternateIdSource;
            set => SetProperty(ref _alternateIdSource, value);
        }
        private string _alternateIdSource;

        // writing to this overrides the default calculation
        public decimal TotalArea
        {
            get => _totalArea ?? (Width * Depth);
            set => SetProperty(ref _totalArea, value);
        }
        private decimal? _totalArea;

        public decimal Width
        {
            get => _width;
            set => SetPropertyFlagged(ref _width, value, nameof(TotalArea));
        }
        private decimal _width;

        public decimal Depth
        {
            get => _depth;
            set => SetPropertyFlagged(ref _depth, value, nameof(TotalArea));
        }
        private decimal _depth;

        public decimal Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }
        private decimal _height;

        public int NumFloors
        {
            get => _numFloors;
            set => SetProperty(ref _numFloors, value);
        }
        private int _numFloors;

        public IAddress Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        private IAddress _address = new Address();

        public IContact Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }
        private IContact _contact = new Contact();

        // on-demand collection storage is allocated on first use
        // use the Has... properties to check for not empty
        public override bool HasImages => Images.HasItems;
        public INotifyingCollection<ImageSource> Images =>
            PropertyCollection<ImageSource>(ref _images, nameof(HasImages));
        [CanBeNull] private INotifyingCollection<ImageSource> _images;

        // Facility Comments
        public override bool HasFacilityComments => FacilityComments.HasItems;
        public INotifyingCollection<ICommentFacility> FacilityComments =>
            PropertyCollection<ICommentFacility>(ref _facilityComments, nameof(HasFacilityComments));
        [CanBeNull] private INotifyingCollection<ICommentFacility> _facilityComments;

        public override bool HasInspections => Inspections.HasItems;
        public INotifyingCollection<IInspectionInfo> Inspections =>
            PropertyCollection<IInspectionInfo>(ref _inspections, new []
            {
                nameof(HasInspections),
                nameof(HasAnyInspections)
            });
        [CanBeNull] private INotifyingCollection<IInspectionInfo> _inspections;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //


        // **************** Class members *************************************************** //

    }
}
