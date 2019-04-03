using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.ViewModels
{
    public class FacilityViewModel : ImagesModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class data members ********************************************** //

        // **************** Class properties ************************************************ //

      //public ImageSource ImgEditTextComments  { get; }
      //public ImageSource ImgRemember          { get; }
      //public ImageSource ImgPhotosCropped     { get; }
      //public ImageSource ImgCancelEdit        { get; }

        [CanBeNull] private ItemsControl ItemsControl { get; set; }

        public IFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            set
            {
                if (SetProperty(ref _localFacilityInfo, value))
                {
                    CreateImages();
                    // QuickObservableCollection should raise it's own notify
                    //RaisePropertyChanged(Images);
                }
            }
        }

        private IFacility _localFacilityInfo;

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

        public FacilityViewModel()
        {
            RegionManagerName = "FacilityItemControl";

          //ImgEditTextComments = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/EditText_Comments.png");
          //ImgRemember         = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/Remember.png", new Size(0,0));
          //ImgPhotosCropped    = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/Photos_cropped.jpg");
          //ImgCancelEdit       = MakeBitmapTransparent.MakeTransparent(@"pack://application:,,,/Resources/Cancel_Undo.png");
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
            if ((LocalFacilityInfo?.Images == null) || (ItemsControl == null)) 
                return;

            if (ItemsControl.ItemsSource is ObservableCollection<Border> oldItems)
                oldItems.Clear();

            var imageSize = new System.Windows.Size()
            {
                Height = 120, // ItemsControl.ActualHeight, 
                Width  = 20   // minimum width
            };

            var itemList = base.CreateImages(imageSize, LocalFacilityInfo.Images);

            // ReSharper disable once PossibleNullReferenceException
            ItemsControl.ItemsSource = new QuickObservableCollection<Border>(itemList);
        }
    }
}
