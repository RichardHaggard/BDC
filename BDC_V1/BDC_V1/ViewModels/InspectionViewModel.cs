using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BDC_V1.Enumerations;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class InspectionViewModel : ViewModelBase
    {
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
                    RaisePropertyChanged(nameof(InspectionInfo));
                }
            }
        }
        [CanBeNull] private IFacility _localFacilityInfo;

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
        }

        // **************** Class members *************************************************** //

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
