using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public abstract class FacilityBaseClass : ImagesModelBase
    {
        [CanBeNull] 
        public virtual IComponentFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            set => SetProperty(ref _localFacilityInfo, value, () =>
            {
                if (_localFacilityInfo != null)
                {
                    // ObservableCollection should raise it's own notify
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

                    // necessary for comments and images to update. TODO: Might be needed elsewhere as well
                    RaisePropertyChanged(new[] {nameof(ImageContainer), nameof(CommentContainer)});
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
            return true;
        }
    }
}
