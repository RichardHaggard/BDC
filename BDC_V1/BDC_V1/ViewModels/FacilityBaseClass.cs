using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.ViewModels
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
                if ((LocalBredInfo != null) && LocalBredInfo.FacilityInfo.HasItems)
                {
                    value = Math.Max(0, value);
                    value = Math.Min(value, LocalBredInfo.FacilityInfo.Count - 1);

                    SetProperty(ref _facilityIndex, value);

                    // ReSharper disable once PossibleNullReferenceException
                    LocalFacilityInfo = LocalBredInfo.FacilityInfo[_facilityIndex];
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

        [CanBeNull] 
        protected ItemsControl ItemsControl { get; set; }

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;

            ItemsControl = GetItemsControl(RegionManager);
            CreateImages();

            return true;
        }

        protected void CreateImages()
        {
            if ((LocalFacilityInfo?.Images == null) || (ItemsControl == null)) 
                return;

            if (ItemsControl.ItemsSource is NotifyingCollection<Border> oldItems)
                oldItems.Clear();

            var imageSize = new System.Windows.Size()
            {
                Height = 120, // ItemsControl.ActualHeight, 
                Width  = 20   // minimum width
            };

            var itemList = base.CreateImages(imageSize, LocalFacilityInfo.Images);

            // ReSharper disable once PossibleNullReferenceException
            ItemsControl.ItemsSource = new NotifyingCollection<Border>(itemList);
        }
    }
}
