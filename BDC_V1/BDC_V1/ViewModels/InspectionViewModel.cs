﻿using System;
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
using System.Windows.Markup;
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

        // **************** Class data members ********************************************** //

        [CanBeNull] private ItemsControl ItemsControl { get; set; }

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
            RegionManagerName = "InspectionItemControl";

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
            if ((InspectionInfo == null) || (ItemsControl == null))
                return;

            //var itemsWidth = 634;    // ItemsControl.ActualWidth;
            var itemsHeight = 120;   // ItemsControl.ActualHeight,

            var borderImages = new QuickObservableCollection<Border>();

            foreach (var item in InspectionInfo.Images)
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
