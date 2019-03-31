using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.ViewModels
{
    public class InventorySectionViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public IInventorySectionType InventorySection { get; }

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
                    InventorySection.Images.Clear();
                    InventorySection.Images.AddRange(_localFacilityInfo?.Images);

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

        public InventorySectionViewModel()
        {
            RegionManagerName = "InventorySectionItemControl";
            InventorySection = new MockInventorySection();
        }


        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;

            const string xaml = "<ItemsPanelTemplate\r\n" + 
                                "  xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'\r\n" + 
                                "  xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>\r\n" +
                                "  <StackPanel Orientation=\"Horizontal\"\r\n" +
                                "              VerticalAlignment=\"Center\"\r\n" +
                                "              HorizontalAlignment=\"Left\"/>\r\n" +
                                "</ItemsPanelTemplate>";
            var foo = XamlReader.Parse(xaml) as ItemsPanelTemplate;

            ItemsControl = new ItemsControl() {ItemsPanel = foo};
            RegionManager.Regions[RegionManagerName].Add(ItemsControl);

            CreateImages();

            return true;
        }

        private void CreateImages()
        {
            if (ItemsControl == null)
                return;

            //var itemsWidth = 642;    // ItemsControl.ActualWidth;
            var itemsHeight = 120;   // ItemsControl.ActualHeight,

            var borderImages = new QuickObservableCollection<Border>();

            foreach (var item in InventorySection.Images)
            {
                var image = new Image
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment   = System.Windows.VerticalAlignment.Center,
                    Height   = itemsHeight,
                    MinWidth = 20,
                    Source   = item
                };

                var border = new Border()
                {
                    Background      = Brushes.White,
                    BorderThickness = new System.Windows.Thickness(1),
                    Margin          = new System.Windows.Thickness() { Right = 5 },
                    Child           = image
                };

                //if ((itemsWidth -= border.ActualWidth) <= 0) break;
                borderImages.Add(border);
            }

            ItemsControl.ItemsSource = borderImages;
        }

    }
}
