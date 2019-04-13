using System;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class FacilityBaseClass : ImagesModelBase
    {
        [CanBeNull] 
        public virtual IComponentFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            set => SetProperty(ref _localFacilityInfo, value, () =>
            {
                if (_localFacilityInfo != null)
                {
                    CreateImages();
                    // NotifyingCollection should raise it's own notify
                    //RaisePropertyChanged(Images);
                }
            });
        }
        private IComponentFacility _localFacilityInfo;

        // ??? KLUDGE ???
        public virtual int FacilityIndex
        {
            get => _facilityIndex;
            set
            {
                if ((LocalBredInfo != null) && LocalBredInfo.HasFacilities)
                {
                    value = Math.Max(0, value);
                    value = Math.Min(value, LocalBredInfo.FacilityInfo.Count - 1);

                    SetProperty(ref _facilityIndex, value);

                    // ReSharper disable once PossibleNullReferenceException
                    LocalFacilityInfo = LocalBredInfo.FacilityInfo[_facilityIndex] as IComponentFacility;
                    return;
                }

                SetProperty(ref _facilityIndex, 0);
            }
        }
        private int _facilityIndex;

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;
                FacilityIndex = 0;
            }
        }

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;

            ImageItemsControl = GetIImageItemControl(RegionManager);
            CreateImages();

            return true;
        }

        protected virtual void CreateImages()
        {
            if (LocalFacilityInfo != null)
            {
                CreateImages(LocalFacilityInfo.HasImages? LocalFacilityInfo.Images : null);
            }
        }
    }
}
