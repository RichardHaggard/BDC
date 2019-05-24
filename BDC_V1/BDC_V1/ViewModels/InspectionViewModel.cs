using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        [NotNull] public ICommand CmdCancelEdit        { get; }
        [NotNull] public ICommand CmdDeleteInspection  { get; }
        [NotNull] public ICommand CmdInspectionComment { get; }
        [NotNull] public ICommand CmdPaintedCoated     { get; }

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
            set => SetProperty(ref _inspectionInfo, value);
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

        protected override ObservableCollection<ICommentBase> CommentContainerSource =>
            InspectionInfo.InspectionComments;

        protected override ObservableCollection<ImageSource> ImageContainerSource =>
            InspectionInfo.Images;

        protected override GeneralCommentViewModel.CommentTypes CommentType =>
            GeneralCommentViewModel.CommentTypes.Inspection;

        public override string TabName       => "INSPECTION";
        public override string PhotoTypeText => "Inspection photos";
        public override string DetailHeaderText => 
            $@"Inspection Comment on {InspectionInfo.Component} for inspection on {InspectionInfo.InspectionDate.ToShortDateString()}";

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //

        public InspectionViewModel()
        {
            RegionManagerName = "InspectionItemControl";

            CmdCancelEdit        = new DelegateCommand(OnCancelEdit          );
            CmdDeleteInspection  = new DelegateCommand(OnDeleteInspection    );
            CmdInspectionComment = new DelegateCommand(OnCmdInspectionComment);
            CmdPaintedCoated     = new DelegateCommand(OnCmdPaintedCoated    );

            IsRemembered = false;
#if DEBUG
//#warning Using MOCK data for InspectionInfo
            InspectionInfo = new MockInspectionInfo();
#endif
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return base.GetRegionManager() && (RegionManager != null);
        }

        private void OnCmdInspectionComment()
        {
            OnSelectedComment(CommentContainer.FirstOrDefault());
        }

        private void OnCmdPaintedCoated()
        {
            // This is a click on the 'label' that is to the right of the 
            // Painted/Coated checkbox. It is actually a button but it looks like a label.
            // In any case, pretend that the check box itself was toggled.

            InspectionInfo.IsPainted = !InspectionInfo.IsPainted;
        }

        private void OnDeleteInspection()
        {
            Debug.WriteLine("OnDeleteInspection not implemented");
        }

        private void OnCancelEdit()
        {
            Debug.WriteLine("OnCancelEdit not implemented");
        }
    }
}
