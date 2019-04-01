﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class InventoryDetailsViewModel : ImagesModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public IInventoryDetailsType InventoryDetails { get; }

        // **************** Class data members ********************************************** //

        [CanBeNull] private ItemsControl ItemsControl { get; set; }

        [CanBeNull] 
        protected IFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            private set
            {
                if (SetProperty(ref _localFacilityInfo, value))
                {
                    InventoryDetails.Images.Clear();
                    InventoryDetails.Images.AddRange(_localFacilityInfo?.Images);

                    // QuickObservableCollection should raise it's own notify
                    //RaisePropertyChanged(nameof(InventorySection));

                    CreateImages();
                    // QuickObservableCollection should raise it's own notify
                    //RaisePropertyChanged(Images);
                }
            }
        }
        [CanBeNull] private IFacility _localFacilityInfo;

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        protected override IBredInfo LocalBredInfo
        {
            get => base.LocalBredInfo;
            set
            {
                base.LocalBredInfo = value;
                LocalFacilityInfo = base.LocalBredInfo?.FacilityInfo;
            }
        }

        // **************** Class constructors ********************************************** //

        public InventoryDetailsViewModel()
        {
            RegionManagerName = "InventoryDetailsItemControl";
            InventoryDetails = new MockInventoryDetails();
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;

            ItemsControl = GetItemsControl(RegionManager);
            CreateImages();

            return true;
        }

        private void CreateImages()
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if ((InventoryDetails?.Images == null) || (ItemsControl == null)) 
                return;

            if (ItemsControl.ItemsSource is ObservableCollection<Border> oldItems)
                oldItems.Clear();

            var imageSize = new System.Windows.Size()
            {
                Height = 120, // ItemsControl.ActualHeight, 
                Width  = 20   // minimum width
            };

            var itemList = base.CreateImages(imageSize, InventoryDetails.Images);

            // ReSharper disable once PossibleNullReferenceException
            ItemsControl.ItemsSource = new QuickObservableCollection<Border>(itemList);
        }
    }
}
