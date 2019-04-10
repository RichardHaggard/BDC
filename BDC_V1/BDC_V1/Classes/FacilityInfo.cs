using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class FacilityInfo : PropertyBase, IFacilityInfo
    {
        /// <inheritdoc />
        public EnumConstType ConstType
        {
            get => _constType;
            set => SetProperty(ref _constType, value);
        }
        private EnumConstType _constType;

        /// <inheritdoc />
        public string BuildingId
        {
            get => _buildingId;
            set => SetProperty(ref _buildingId, value);
        }
        private string _buildingId;

        /// <inheritdoc />
        public uint BuildingIdNumber
        {
            get => _buildingIdNumber;
            set => SetProperty(ref _buildingIdNumber, value);
        }
        private uint _buildingIdNumber;

        /// <inheritdoc />
        public string BuildingName
        {
            get => _buildingName;
            set => SetProperty(ref _buildingName, value);
        }
        private string _buildingName;

        /// <inheritdoc />
        public string BuildingUse
        {
            get => _buildingUse;
            set => SetProperty(ref _buildingUse, value);
        }
        private string _buildingUse;

        /// <inheritdoc />
        public int YearBuilt
        {
            get => _yearBuilt;
            set => SetProperty(ref _yearBuilt, value);
        }
        private int _yearBuilt;

        /// <inheritdoc />
        public string AlternateId
        {
            get => _alternateId;
            set => SetProperty(ref _alternateId, value);
        }
        private string _alternateId;

        /// <inheritdoc />
        public string AlternateIdSource
        {
            get => _alternateIdSource;
            set => SetProperty(ref _alternateIdSource, value);
        }
        private string _alternateIdSource;

        /// <inheritdoc />
        public decimal TotalArea
        {
            get => _totalArea ?? (Width * Depth);
            set => SetProperty(ref _totalArea, value);
        }
        private decimal? _totalArea;

        /// <inheritdoc />
        public decimal Width
        {
            get => _width;
            set => SetPropertyFlagged(ref _width, value, nameof(TotalArea));
        }
        private decimal _width;

        /// <inheritdoc />
        public decimal Depth
        {
            get => _depth;
            set => SetPropertyFlagged(ref _depth, value, nameof(TotalArea));
        }
        private decimal _depth;

        /// <inheritdoc />
        public decimal Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }
        private decimal _height;

        /// <inheritdoc />
        public int NumFloors
        {
            get => _numFloors;
            set => SetProperty(ref _numFloors, value);
        }
        private int _numFloors;

        /// <inheritdoc />
        public IAddress Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        private IAddress _address;

        /// <inheritdoc />
        public IContact Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }
        private IContact _contact;

        /// <inheritdoc />
        public IIndexedCollection<IInspectionInfo> Inspections { get; } =
            new IndexedCollection<IInspectionInfo>();

        /// <inheritdoc />
        public IIndexedCollection<ImageSource> Images { get; } =
            new IndexedCollection<ImageSource>();

        /// <inheritdoc />
        public IIndexedCollection<IComment> Comments { get; } =
            new IndexedCollection<IComment>();
    }
}
