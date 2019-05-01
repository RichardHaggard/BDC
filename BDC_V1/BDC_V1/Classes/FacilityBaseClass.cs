using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public abstract class FacilityBaseClass : ImagesModelBase, IFacilityBase
    {
        private readonly FacilityBase _facilityBase = new FacilityBase();

        public virtual IComponentFacility LocalFacilityInfo => _facilityBase.LocalFacilityInfo;

        // ??? KLUDGE ???
        public virtual int FacilityIndex
        {
            get => _facilityBase.FacilityIndex;
            set
            {
                if ((LocalBredInfo == null) || !LocalBredInfo.HasFacilities || (Facilities == null))
                    _facilityBase.FacilityIndex = -1;
                else 
                    _facilityBase.FacilityIndex = Math.Min(value, Facilities.Count - 1);
            }
        }

        public IList<IComponentFacility> Facilities
        {
            get => _facilityBase.Facilities;
            set => _facilityBase.Facilities = value;
        }

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;

                Facilities = base.LocalBredInfo?
                    .FacilityInfo.Cast<IComponentFacility>().ToList();

                FacilityIndex = (base.LocalBredInfo != null) ? 0 : -1;
            }
        }

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;
            return true;
        }

        protected override IFacilityBase FacilityBaseInfo => this;

        protected FacilityBaseClass()
        {
            _facilityBase.PropertyChanged += (o, i) =>
            {
                RaisePropertyChanged(i.PropertyName);

                if (i.PropertyName == nameof(_facilityBase.LocalFacilityInfo))
                    RaisePropertyChanged(new[]
                    {
                        nameof(ImageContainer), 
                        nameof(CommentContainer)
                    });
            };
        }
    }
}
