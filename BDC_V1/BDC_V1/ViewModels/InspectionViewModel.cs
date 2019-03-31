using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using BDC_V1.Classes;
using BDC_V1.Enumerations;
using BDC_V1.Events;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using CommonServiceLocator;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Unity;
using Brushes = System.Windows.Media.Brushes;
using EventAggregator = BDC_V1.Events.EventAggregator;
using Image = System.Windows.Controls.Image;

namespace BDC_V1.ViewModels
{
    public class InspectionViewModel : ViewModelBase
    {
        private const double DoubleTolerance = 0.001;

        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCondRating       { get; }
        public ICommand CmdCancelEdit       { get; }
        public ICommand CmdDeleteInspection { get; }

        public bool IsRemembered
        {
            get => _isRemembered;
            set => SetProperty(ref _isRemembered, value);
        }
        private bool _isRemembered;

        public bool IsPainted
        {
            get => _isPainted;
            set => SetProperty(ref _isPainted, value);
        }
        private bool _isPainted;

        public IInspectionInfoType InspectionInfo
        {
            get => _inspectionInfo;
            private set
            {
                if (SetProperty(ref _inspectionInfo, value))
                {
                }
            }
        }
        private IInspectionInfoType _inspectionInfo;

        public QuickObservableCollection<Border> Images { get; } =
            new QuickObservableCollection<Border>();

        // OneWayToSource doesn't need notifications
        public double ImagesHeight
        {
            get => _imagesHeight;
            set
            {
                if (Math.Abs(_imagesHeight - value) > DoubleTolerance)
                {
                    _imagesHeight = value;
                    CreateImages();
                }
            }
        }
        private double _imagesHeight;

        public double ImagesWidth  { get; set; }

        // **************** Class data members ********************************************** //

        [CanBeNull] private ItemsControl ItemsControl { get; set; }

        //[NotNull]
        //public ICommand TabSelectionChange { get; set; }

        [NotNull]
        public ICommand ViewActivated { get; set; }

        [CanBeNull]
        public IRegionManager RegionManager
        {
            get => this._regionManager;
            set => SetProperty(ref _regionManager, value);
        } 
        private IRegionManager _regionManager;

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        [CanBeNull] 
        protected IFacility LocalFacilityInfo
        {
            get => _localFacilityInfo;
            private set
            {
                if (SetProperty(ref _localFacilityInfo, value))
                {
                    InspectionInfo.Images.Clear();
                    InspectionInfo.Images.AddRange(_localFacilityInfo?.Images);

                    // QuickObservableCollection should raise it's own notify
                    //RaisePropertyChanged(nameof(InspectionInfo));

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

        public InspectionViewModel()
        {
            CmdCondRating       = new DelegateCommand<object>(OnConditionRating);
            CmdCancelEdit       = new DelegateCommand(OnCancelEdit      );
            CmdDeleteInspection = new DelegateCommand(OnDeleteInspection);

            IsPainted    = false;
            IsRemembered = false;
            InspectionInfo = new MockInspectionInfo();

            //EventAggregator.GetEvent<PubSubEvent<TabChangeEvent>>()
            //    .Subscribe((item) =>
            //    {
            //        if ((item.TabControlName == "ViewTabControl") &&
            //             (item.TabItemName   ==  "Inspection"))
            //        {
            //            CreateImages();
            //        }
            //    });

            ViewActivated = new RelayCommand(ViewActivatedEventHandler);         
        }

        // **************** Class members *************************************************** //

     
        private void ViewActivatedEventHandler(object sender, object e)
        {
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if ((RegionManager != null) &&
                RegionManager.Regions.ContainsRegionWithName("ImagesItemControl"))
            {
                var viewsCollections = RegionManager.Regions["ImagesItemControl"].ActiveViews;
                if (viewsCollections != null)
                {
                    foreach (var view in viewsCollections)
                        RegionManager.Regions["ImagesItemControl"].Remove(view);
                }

                ItemsControl = new ItemsControl();
                RegionManager.Regions["ImagesItemControl"].Add(ItemsControl);
            }
            else
            {
                ItemsControl = null;
            }

            if (ItemsControl != null) CreateImages();
        }

        private void CreateImages()
        {
            Images.Clear();

            if ((LocalFacilityInfo == null) || (ItemsControl == null))
                return;

            var itemsWidth = ItemsControl.ActualWidth;

            foreach (var item in LocalFacilityInfo.Images)
            {
                var image = new Image
                {
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment   = System.Windows.VerticalAlignment.Center,
                    Height   = ItemsControl.ActualHeight,
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

                if ((itemsWidth -= border.ActualWidth) <= 0) break;
                Images.Add(border);
            }

            ItemsControl.ItemsSource = Images;
        }

        private void OnCancelEdit()
        {
            Debug.WriteLine("OnCancelEdit not implemented");
        }

        private void OnDeleteInspection()
        {
            Debug.WriteLine("OnDeleteInspection not implemented");
        }

        private void OnConditionRating(object param)
        {
            if (param is Array paramArray)
            {
                if (paramArray.Length == 3)
                {
                    var color  = paramArray.GetValue(0) as string;
                    var colIdx = paramArray.GetValue(1) as string;
                    var rowIdx = paramArray.GetValue(2) as string;

                    if (!string.IsNullOrEmpty(color) &&
                        !string.IsNullOrEmpty(colIdx) &&
                        !string.IsNullOrEmpty(rowIdx))
                    {
                        switch (color)
                        {
                            case "Green":
                                break;
                            case "Amber":
                                break;
                            case "Red":
                                break;
                            default:
                                throw new IndexOutOfRangeException($"invalid color:\"{color}\"");
                        }

                        switch (colIdx)
                        {
                            case "0":
                                break;
                            case "1":
                                break;
                            case "2":
                                break;
                            default:
                                throw new IndexOutOfRangeException($"invalid colIdx:\"{colIdx}\"");
                        }

                        switch (rowIdx)
                        {
                            case "0":
                                break;
                            case "1":
                                break;
                            case "2":
                                break;
                            default:
                                throw new IndexOutOfRangeException($"invalid rowIdx:\"{rowIdx}\"");
                        }

                        Debug.WriteLine($"OnConditionRating({color},{colIdx},{rowIdx}) not implemented");
                        return;
                    }
                }
            }

            throw new InvalidCastException("invalid param");
        }
    }
}
