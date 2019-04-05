using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.Utils;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class InspectionViewModel : FacilityBaseClass
    {
        private const double DoubleTolerance = 0.001;

        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCondRating        { get; }
        public ICommand CmdCancelEdit        { get; }
        public ICommand CmdDeleteInspection  { get; }
        public ICommand CmdInspectionComment { get; }

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

        public override IFacility LocalFacilityInfo
        {
            get => base.LocalFacilityInfo;
            set
            {
                base.LocalFacilityInfo = value;

                InspectionInfo.Images.Clear();
                InspectionInfo.Images.AddRange(LocalFacilityInfo?.Images);

                // QuickObservableCollection should raise it's own notify
                //RaisePropertyChanged(nameof(InspectionInfo));
            }
        }

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //

        public InspectionViewModel()
        {
            RegionManagerName = "InspectionItemControl";

            CmdCondRating        = new DelegateCommand<object>(OnConditionRating);
            CmdCancelEdit        = new DelegateCommand(OnCancelEdit             );
            CmdDeleteInspection  = new DelegateCommand(OnDeleteInspection       );
            CmdInspectionComment = new DelegateCommand(OnCmdInspectionComment   );

            IsPainted    = false;
            IsRemembered = false;
            InspectionInfo = new MockInspectionInfo();
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;

            ItemsControl = GetItemsControl(RegionManager);
            CreateImages();

            return true;
        }

        private void OnCancelEdit()
        {
            Debug.WriteLine("OnCancelEdit not implemented");
        }

        private void OnCmdInspectionComment()
        {
            CmInspView view = new CmInspView();
            view.ShowDialog();
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
