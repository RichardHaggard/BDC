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
        private const double DoubleTolerance = 0.001;

        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdCondRating        { get; }
        public ICommand CmdCancelEdit        { get; }
        public ICommand CmdDeleteInspection  { get; }
        public ICommand CmdInspectionComment { get; }

        public ICommand CmdAmberPlus { get; }
        public Brush AmberPlusBg
        {
            get { return _AmberPlusBg; }
            set { SetProperty(ref _AmberPlusBg, value); }
        }
        private Brush _AmberPlusBg = Brushes.Transparent;


        public Brush AmberPlusFg
        {
            get { return _AmberPlusFg; }
            set { SetProperty(ref _AmberPlusFg, value); }
        }
        private Brush _AmberPlusFg = Brushes.Orange;


        public ICommand CmdAmber { get; }
        public Brush AmberBg
        {
            get { return _AmberBg; }
            set { SetProperty(ref _AmberBg, value); }
        }
        private Brush _AmberBg = Brushes.Transparent;


        public Brush AmberFg
        {
            get { return _AmberFg; }
            set { SetProperty(ref _AmberFg, value); }
        }
        private Brush _AmberFg = Brushes.Orange;


        public ICommand CmdAmberMinus { get; }
        public Brush AmberMinusBg
        {
            get { return _AmberMinusBg; }
            set { SetProperty(ref _AmberMinusBg, value); }
        }
        private Brush _AmberMinusBg = Brushes.Transparent;


        public Brush AmberMinusFg
        {
            get { return _AmberMinusFg; }
            set { SetProperty(ref _AmberMinusFg, value); }
        }
        private Brush _AmberMinusFg = Brushes.Orange;


        public ICommand CmdGreenPlus { get; }
        public Brush GreenPlusBg
        {
            get { return _GreenPlusBg; }
            set { SetProperty(ref _GreenPlusBg, value); }
        }
        private Brush _GreenPlusBg = Brushes.Transparent;


        public Brush GreenPlusFg
        {
            get { return _GreenPlusFg; }
            set { SetProperty(ref _GreenPlusFg, value); }
        }
        private Brush _GreenPlusFg = Brushes.Green;


        public ICommand CmdGreen { get; }
        public Brush GreenBg
        {
            get { return _GreenBg; }
            set { SetProperty(ref _GreenBg, value); }
        }
        private Brush _GreenBg = Brushes.Transparent;


        public Brush GreenFg
        {
            get { return _GreenFg; }
            set { SetProperty(ref _GreenFg, value); }
        }
        private Brush _GreenFg = Brushes.Green;


        public ICommand CmdGreenMinus { get; }
        public Brush GreenMinusBg
        {
            get { return _GreenMinusBg; }
            set { SetProperty(ref _GreenMinusBg, value); }
        }
        private Brush _GreenMinusBg = Brushes.Transparent;


        public Brush GreenMinusFg
        {
            get { return _GreenMinusFg; }
            set { SetProperty(ref _GreenMinusFg, value); }
        }
        private Brush _GreenMinusFg = Brushes.Green;



        public ICommand CmdRedPlus { get; }
        public Brush RedPlusBg
        {
            get { return _RedPlusBg; }
            set { SetProperty(ref _RedPlusBg, value); }
        }
        private Brush _RedPlusBg = Brushes.Transparent;


        public Brush RedPlusFg
        {
            get { return _RedPlusFg; }
            set { SetProperty(ref _RedPlusFg, value); }
        }
        private Brush _RedPlusFg = Brushes.Red;


        public ICommand CmdRed { get; }
        public Brush RedBg
        {
            get { return _RedBg; }
            set { SetProperty(ref _RedBg, value); }
        }
        private Brush _RedBg = Brushes.Transparent;


        public Brush RedFg
        {
            get { return _RedFg; }
            set { SetProperty(ref _RedFg, value); }
        }
        private Brush _RedFg = Brushes.Red;


        public ICommand CmdRedMinus { get; }
        public Brush RedMinusBg
        {
            get { return _RedMinusBg; }
            set { SetProperty(ref _RedMinusBg, value); }
        }
        private Brush _RedMinusBg = Brushes.Transparent;


        public Brush RedMinusFg
        {
            get { return _RedMinusFg; }
            set { SetProperty(ref _RedMinusFg, value); }
        }
        private Brush _RedMinusFg = Brushes.Red;





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

            CmdCondRating        = new DelegateCommand<object>(OnConditionRating);
            CmdCancelEdit        = new DelegateCommand(OnCancelEdit             );
            CmdDeleteInspection  = new DelegateCommand(OnDeleteInspection       );
            CmdInspectionComment = new DelegateCommand(OnCmdInspectionComment   );

            CmdAmberPlus = new DelegateCommand(OnCmdAmberPlus);
            CmdAmber = new DelegateCommand(OnCmdAmber);
            CmdAmberMinus = new DelegateCommand(OnCmdAmberMinus);
            CmdGreenPlus = new DelegateCommand(OnCmdGreenPlus);
            CmdGreen = new DelegateCommand(OnCmdGreen);
            CmdGreenMinus = new DelegateCommand(OnCmdGreenMinus);
            CmdRedPlus = new DelegateCommand(OnCmdRedPlus);
            CmdRed = new DelegateCommand(OnCmdRed);
            CmdRedMinus = new DelegateCommand(OnCmdRedMinus);


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
