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

#if false
        public ICommand CmdAmberPlus { get; }
        public Brush AmberPlusBg
        {
            get => _amberPlusBg;
            set => SetProperty(ref _amberPlusBg, value);
        }
        private Brush _amberPlusBg = Brushes.Transparent;


        public Brush AmberPlusFg
        {
            get => _amberPlusFg;
            set => SetProperty(ref _amberPlusFg, value);
        }
        private Brush _amberPlusFg = Brushes.Orange;


        public ICommand CmdAmber { get; }
        public Brush AmberBg
        {
            get => _amberBg;
            set => SetProperty(ref _amberBg, value);
        }
        private Brush _amberBg = Brushes.Transparent;


        public Brush AmberFg
        {
            get => _amberFg;
            set => SetProperty(ref _amberFg, value);
        }
        private Brush _amberFg = Brushes.Orange;


        public ICommand CmdAmberMinus { get; }
        public Brush AmberMinusBg
        {
            get => _amberMinusBg;
            set => SetProperty(ref _amberMinusBg, value);
        }
        private Brush _amberMinusBg = Brushes.Transparent;


        public Brush AmberMinusFg
        {
            get => _amberMinusFg;
            set => SetProperty(ref _amberMinusFg, value);
        }
        private Brush _amberMinusFg = Brushes.Orange;


        public ICommand CmdGreenPlus { get; }
        public Brush GreenPlusBg
        {
            get => _greenPlusBg;
            set => SetProperty(ref _greenPlusBg, value);
        }
        private Brush _greenPlusBg = Brushes.Transparent;


        public Brush GreenPlusFg
        {
            get => _greenPlusFg;
            set => SetProperty(ref _greenPlusFg, value);
        }
        private Brush _greenPlusFg = Brushes.Green;


        public ICommand CmdGreen { get; }
        public Brush GreenBg
        {
            get => _greenBg;
            set => SetProperty(ref _greenBg, value);
        }
        private Brush _greenBg = Brushes.Transparent;


        public Brush GreenFg
        {
            get => _greenFg;
            set => SetProperty(ref _greenFg, value);
        }
        private Brush _greenFg = Brushes.Green;


        public ICommand CmdGreenMinus { get; }
        public Brush GreenMinusBg
        {
            get => _greenMinusBg;
            set => SetProperty(ref _greenMinusBg, value);
        }
        private Brush _greenMinusBg = Brushes.Transparent;


        public Brush GreenMinusFg
        {
            get => _greenMinusFg;
            set => SetProperty(ref _greenMinusFg, value);
        }
        private Brush _greenMinusFg = Brushes.Green;



        public ICommand CmdRedPlus { get; }
        public Brush RedPlusBg
        {
            get => _redPlusBg;
            set => SetProperty(ref _redPlusBg, value);
        }
        private Brush _redPlusBg = Brushes.Transparent;


        public Brush RedPlusFg
        {
            get => _redPlusFg;
            set => SetProperty(ref _redPlusFg, value);
        }
        private Brush _redPlusFg = Brushes.Red;


        public ICommand CmdRed { get; }
        public Brush RedBg
        {
            get => _redBg;
            set => SetProperty(ref _redBg, value);
        }
        private Brush _redBg = Brushes.Transparent;


        public Brush RedFg
        {
            get => _redFg;
            set => SetProperty(ref _redFg, value);
        }
        private Brush _redFg = Brushes.Red;


        public ICommand CmdRedMinus { get; }
        public Brush RedMinusBg
        {
            get => _redMinusBg;
            set => SetProperty(ref _redMinusBg, value);
        }
        private Brush _redMinusBg = Brushes.Transparent;


        public Brush RedMinusFg
        {
            get => _redMinusFg;
            set => SetProperty(ref _redMinusFg, value);
        }
        private Brush _redMinusFg = Brushes.Red;
#endif

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

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //

        public InspectionViewModel()
        {
            RegionManagerName = "InspectionItemControl";

            //CmdCondRating        = new DelegateCommand<object>(OnConditionRating);
            CmdCancelEdit        = new DelegateCommand(OnCancelEdit             );
            CmdDeleteInspection  = new DelegateCommand(OnDeleteInspection       );
            CmdInspectionComment = new DelegateCommand(OnCmdInspectionComment   );

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

        protected override void OnSelectedComment(CommentBase comment, bool isInspection=false)
        {
            base.OnSelectedComment(comment, true);
#if false
            var view = new GeneralCommentView();
            if (!(view.DataContext is GeneralCommentViewModel model))       
                throw new InvalidCastException("Invalid View Model");

            model.FacilityBaseInfo = null;              //TODO: Put real data in here
            model.CommentText = comment?.CommentText;
            if (view.ShowDialog() != true) return;

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
