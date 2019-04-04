using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class FacilitySystems : SystemElement, IFacilitySystems
    {
        public ObservableCollection<ISystemElement> SubSystems
        {
            get
            {
                if (_subSystems == null)
                {
                    _subSystems = new QuickObservableCollection<ISystemElement>();
                    _subSystems.CollectionChanged += (o, i) =>
                    {
                        RaisePropertyChanged(nameof(HasSubsystems));
                        RaisePropertyChanged(nameof(HasAnyComponents));
                    };

                    RaisePropertyChanged(nameof(HasSubsystems));
                    RaisePropertyChanged(nameof(HasAnyComponents));
                }

                return _subSystems;
            }
        }
        private QuickObservableCollection<ISystemElement> _subSystems;

        public ObservableCollection<IQaIssue> QaIssueList
        {
            get
            {
                if (_qaIssueList == null)
                {
                    _qaIssueList = new QuickObservableCollection<IQaIssue>();
                    _qaIssueList.CollectionChanged += (o, i) => RaisePropertyChanged(nameof(HasQaIssues));

                    RaisePropertyChanged(nameof(HasQaIssues));
                }

                return _qaIssueList;
            }
        }
        private QuickObservableCollection<IQaIssue> _qaIssueList;


        public override bool? HasSubsystems => _subSystems?.Any();

        public override bool HasAnyComponents 
        {
            get
            {
                if (ComponentType == EnumComponentTypes.ComponentType) return true;
                if (!HasSubsystems.Equals(true)) return false;

                foreach (var item in SubSystems)
                {
                    if ((item is IFacilitySystems facItem) &&
                        facItem.HasAnyComponents)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override bool? HasQaIssues 
        {
            get
            {
                if ((_qaIssueList != null) && QaIssueList.Any()) return true;
                if (!HasSubsystems.Equals(true)) return false;

                foreach (var item in SubSystems)
                {
                    if ((item is IFacilitySystems facItem) &&
                        facItem.HasQaIssues.Equals(true))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public IFacilitySystems Get(EnumComponentTypes type, string name)
        {
            return TryGet(type, name, out var val) ? val : null;
        }

        public IFacilitySystems Get(ISystemElement type)
        {
            return TryGet(type.ComponentType, type.ComponentName, out var val) ? val : null;
        }

        // recursive search function
        public bool TryGet(EnumComponentTypes type, string name, out IFacilitySystems val)
        {
            switch (type)
            {
                case EnumComponentTypes.None:
                    if (ComponentName == name)
                    {
                        val = this;
                        return true;
                    }
                    break;

                case EnumComponentTypes.FacilityType:
                case EnumComponentTypes.SystemType:
                case EnumComponentTypes.SubsystemType:
                case EnumComponentTypes.ComponentType:
                    if ((ComponentType == type) &&
                        (ComponentName == name))
                    {
                        val = this;
                        return true;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            if (HasSubsystems.Equals(true))
            {
                foreach (var item in SubSystems)
                {
                    if ((item is IFacilitySystems facItem) &&
                        facItem.TryGet(type, name, out val))
                    {
                        return true;
                    }
                }
            }

            val = null;
            return false;
        }
    }
}
