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
    public class Facility : FacilitySystems, IFacility
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

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

        public decimal TotalArea
        {
            get => _totalArea ?? (Width * Depth);
            set => SetProperty(ref _totalArea, value);
        }
        private decimal? _totalArea;

        public decimal Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }
        private decimal _width;

        public decimal Depth
        {
            get => _depth;
            set => SetProperty(ref _depth, value);
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
        private IAddress _address;

        public IContact Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }
        private IContact _contact;

        public override bool? HasComments => _facilityComments?.Any();
        public ObservableCollection<IComment> FacilityComments
        {
            get
            {
                if (_facilityComments == null)
                {
                    _facilityComments = new QuickObservableCollection<IComment>();
                    _facilityComments.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasComments));

                    RaisePropertyChanged(nameof(HasComments));
                }

                return _facilityComments;
            }
        }
        private ObservableCollection<IComment> _facilityComments;

        public override bool? HasImages => _images?.Any();
        public ObservableCollection<ImageSource> Images
        {
            get
            {
                if (_images == null)
                {
                    _images = new QuickObservableCollection<ImageSource>();
                    _images.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasImages));

                    RaisePropertyChanged(nameof(HasImages));
                }

                return _images;
            }
        }
        private QuickObservableCollection<ImageSource> _images;

        public override bool? HasInspections => _inspections?.Any();
        public ObservableCollection<IInspection>  Inspections
        {
            get
            {
                if (_inspections == null)
                {
                    _inspections = new QuickObservableCollection<IInspection>();
                    _inspections.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasInspections));

                    RaisePropertyChanged(nameof(HasInspections));
                }

                return _inspections;
            }
        }
        private QuickObservableCollection<IInspection> _inspections;

        // **************** Class data members ********************************************** //


        // **************** Class constructors ********************************************** //

        public Facility()
        {
            // add all of the basic system types
            foreach (EnumFacilitySystemTypes sysType in Enum.GetValues(typeof(EnumFacilitySystemTypes)))
            {
                if (sysType.ToString() != "None")
                {
                    SubSystems.Add(new FacilitySystems()
                    {
                        ComponentType = EnumComponentTypes.SystemType,
                        ComponentName = sysType.GetSystemName()
                    });
                }
            }

            // add subsystem types that will always be there
            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.A10.GetSystemName(), out var a10))
            {
                foreach (Enum_A10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_A10_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        a10.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.A20.GetSystemName(), out var a20))
            {
                foreach (Enum_A20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_A20_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        a20.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.B10.GetSystemName(), out var b10))
            {
                foreach (Enum_B10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B10_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        b10.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.B20.GetSystemName(), out var b20))
            {
                foreach (Enum_B20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B20_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        b20.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.B30.GetSystemName(), out var b30))
            {
                foreach (Enum_B30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_B30_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        b30.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.C10.GetSystemName(), out var c10))
            {
                foreach (Enum_C10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C10_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        c10.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.C20.GetSystemName(), out var c20))
            {
                foreach (Enum_C20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C20_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        c20.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.C30.GetSystemName(), out var c30))
            {
                foreach (Enum_C30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_C30_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        c30.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.D10.GetSystemName(), out var d10))
            {
                foreach (Enum_D10_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D10_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        d10.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.D20.GetSystemName(), out var d20))
            {
                foreach (Enum_D20_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D20_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        d20.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.D30.GetSystemName(), out var d30))
            {
                foreach (Enum_D30_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D30_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        d30.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }

            if (TryGet(EnumComponentTypes.SystemType, EnumFacilitySystemTypes.D40.GetSystemName(), out var d40))
            {
                foreach (Enum_D40_SubsystemTypes subType in Enum.GetValues(typeof(Enum_D40_SubsystemTypes)))
                {
                    if (subType.ToString() != "None")
                    {
                        d40.SubSystems.Add(new FacilitySystems()
                        {
                            ComponentType = EnumComponentTypes.SubsystemType,
                            ComponentName = subType.GetSystemName()
                        });
                    }
                }
            }
        }

        // **************** Class members *************************************************** //

    }
}
