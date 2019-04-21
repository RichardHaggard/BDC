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
using BDC_V1.Mock_Data;
using BDC_V1.Services;
using BDC_V1.Utils;
using BDC_V1.Views;
using JetBrains.Annotations;
using Prism.Commands;

namespace BDC_V1.ViewModels
{
    public class InspectionViewModel : FacilityBaseClass
    {
        //private const double DoubleTolerance = 0.001;

        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        //public ICommand CmdCondRating        { get; }
        public ICommand CmdCancelEdit        { get; }
        public ICommand CmdDeleteInspection  { get; }
        public ICommand CmdInspectionComment { get; }
        public ICommand CmdPaintedCoated     { get; }

        public bool IsRemembered
        {
            get => _isRemembered;
            set => SetProperty(ref _isRemembered, value);
        }
        private bool _isRemembered;

        [NotNull]
        public IInspectionInfo InspectionInfo
        {
            get => _inspectionInfo;
            set
            {
                if (SetProperty(ref _inspectionInfo, value))
                {
//#if DEBUG
//#warning Overriding Inspection images 
//                    _inspectionInfo.Images.Clear();
//                    _inspectionInfo.Images.AddRange(LocalFacilityInfo?.Images);
//#endif
                }
            }
        }
        private IInspectionInfo _inspectionInfo = new InspectionInfo();

//        public override IComponentFacility LocalFacilityInfo
//        {
//            get => base.LocalFacilityInfo;
//            set
//            {
//                base.LocalFacilityInfo = value;
//#if DEBUG
//#warning Overriding Inspection images
//                InspectionInfo.Images.Clear();
//                InspectionInfo.Images.AddRange(LocalFacilityInfo?.Images);
//#endif
//                // ObservableCollection should raise it's own notify
//                //RaisePropertyChanged(nameof(InspectionInfo));
//            }
//        }

        public override ObservableCollection<CommentBase> CommentContainer =>
            InspectionInfo.InspectionComments;

        public override ObservableCollection<ImageSource> ImageContainer => 
            InspectionInfo.Images;

        public override string TabName       => "INSPECTION";
        public override string PhotoTypeText => "Inspection photos";

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //

        public InspectionViewModel()
        {
            RegionManagerName = "InspectionItemControl";

            //CmdCondRating        = new DelegateCommand<object>(OnConditionRating);
            CmdCancelEdit        = new DelegateCommand(OnCancelEdit             );
            CmdDeleteInspection  = new DelegateCommand(OnDeleteInspection       );
            CmdInspectionComment = new DelegateCommand(OnCmdInspectionComment   );
            CmdPaintedCoated     = new DelegateCommand(OnCmdPaintedCoated       );

#if false
            CmdAmberPlus = new DelegateCommand(OnCmdAmberPlus);
            CmdAmber = new DelegateCommand(OnCmdAmber);
            CmdAmberMinus = new DelegateCommand(OnCmdAmberMinus);
            CmdGreenPlus = new DelegateCommand(OnCmdGreenPlus);
            CmdGreen = new DelegateCommand(OnCmdGreen);
            CmdGreenMinus = new DelegateCommand(OnCmdGreenMinus);
            CmdRedPlus = new DelegateCommand(OnCmdRedPlus);
            CmdRed = new DelegateCommand(OnCmdRed);
            CmdRedMinus = new DelegateCommand(OnCmdRedMinus);
#endif

            IsRemembered = false;
#if DEBUG
#warning Using MOCK data for InspectionInfo
            InspectionInfo = new MockInspectionInfo();
#endif
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            if (!base.GetRegionManager() || (RegionManager == null)) return false;
            return true;
        }

        private void OnCancelEdit()
        {
            Debug.WriteLine("OnCancelEdit not implemented");
        }

#if false
        private void OnCmdAmberPlus()
        {
            SetAllButtonColors();
            AmberPlusBg = Brushes.Orange;
            AmberPlusFg = Brushes.Black;
        }

        private void OnCmdAmber()
        {
            SetAllButtonColors();
            AmberBg = Brushes.Orange;
            AmberFg = Brushes.Black;
        }

        private void OnCmdAmberMinus()
        {
            SetAllButtonColors();
            AmberMinusBg = Brushes.Orange;
            AmberMinusFg = Brushes.Black;
        }



        private void OnCmdGreenPlus()
        {
            SetAllButtonColors();
            GreenPlusBg = Brushes.Green;
            GreenPlusFg = Brushes.Black;
        }

        private void OnCmdGreen()
        {
            SetAllButtonColors();
            GreenBg = Brushes.Green;
            GreenFg = Brushes.Black;
        }

        private void OnCmdGreenMinus()
        {
            SetAllButtonColors();
            GreenMinusBg = Brushes.Green;
            GreenMinusFg = Brushes.Black;
        }


        private void OnCmdRedPlus()
        {
            SetAllButtonColors();
            RedPlusBg = Brushes.Red;
            RedPlusFg = Brushes.Black;
        }

        private void OnCmdRed()
        {
            SetAllButtonColors();
            RedBg = Brushes.Red;
            RedFg = Brushes.Black;
        }

        private void OnCmdRedMinus()
        {
            SetAllButtonColors();
            RedMinusBg = Brushes.Red;
            RedMinusFg = Brushes.Black;
        }


        private void SetAllButtonColors()
        {
            AmberPlusBg = Brushes.Blue;
            AmberPlusFg = Brushes.White;
            AmberBg = Brushes.Blue;
            AmberFg = Brushes.White;
            AmberMinusBg = Brushes.Blue;
            AmberMinusFg = Brushes.White;
            GreenPlusBg = Brushes.Blue;
            GreenPlusFg = Brushes.White;
            GreenBg = Brushes.Blue;
            GreenFg = Brushes.White;
            GreenMinusBg = Brushes.Blue;
            GreenMinusFg = Brushes.White;
            RedPlusBg = Brushes.Blue;
            RedPlusFg = Brushes.White;
            RedBg = Brushes.Blue;
            RedFg = Brushes.White;
            RedMinusBg = Brushes.Blue;
            RedMinusFg = Brushes.White;
        }
#endif

        private void OnCmdInspectionComment()
        {
            OnSelectedComment(null, true);
        }


        private void OnCmdPaintedCoated()
        {
            // This is a click on the 'label' that is to the right of the 
            // Painted/Coated checkbox. It is actually a button but it looks like a label.
            // In any case, pretend that the check box itself was toggled.

            InspectionInfo.IsPainted = !InspectionInfo.IsPainted;
        }


        protected override void OnSelectedComment(CommentBase comment, bool isInspection=false)
        {
            base.OnSelectedComment(comment, true);
#if false
            var view = new GeneralCommentView();
            if (!(view.DataContext is GeneralCommentViewModel model))       
                throw new InvalidCastException("Invalid View Model");

            model.FacilityBaseInfo = null;              //TODO: Put real data in here
            model.CommentText = comment?.CommentText;
            //if (view.ShowDialog() != true) return;
            if (view.ShowDialogInParent(true) != true) return;

            // TODO: Fix the CommentViewModel to return a CommentBase class on success
            DoSelectedComment(model.Result, comment, model.CommentText);
#endif
        }

        private void OnDeleteInspection()
        {
            Debug.WriteLine("OnDeleteInspection not implemented");
        }

#if false
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
#endif
    }
}
