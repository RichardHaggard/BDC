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
    public class InventorySectionViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdAddSection     { get; }
        public ICommand CmdCancelEdit     { get; }
        public ICommand CmdDecValue       { get; }
        public ICommand CmdDeleteSection  { get; }
        public ICommand CmdIncValue       { get; }
        public ICommand CmdNextSection    { get; }
        public ICommand CmdSectionComment { get; }
        public ICommand CmdToday          { get; }

        // TODO: Should this be part of the IInventorySection instead of here ???
        public bool Estimated
        {
            get => _estimated;
            set => SetProperty(ref _estimated, value);
        }
        private bool _estimated;


        public bool IsRemembered
        {
            get => _isRemembered;
            set =>  SetProperty(ref _isRemembered, value);
        }
        private bool _isRemembered;

        [NotNull]
        public IInventorySection InventorySection { get; }


        public int SelectedIndex 
            {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        private int _selectedIndex;


        // **************** Class data members ********************************************** //

        //public override IComponentFacility LocalFacilityInfo
        //{
        //    get => base.LocalFacilityInfo;
        //    set
        //    {
        //        base.LocalFacilityInfo = value;

        //        InventorySection.Images.Clear();
        //        InventorySection.Images.AddRange(base.LocalFacilityInfo?.Images);

        //        // ObservableCollection should raise it's own notify
        //        //RaisePropertyChanged(nameof(InventorySection));
        //    }
        //}

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        public override ObservableCollection<CommentBase> CommentContainer =>
            InventorySection.SectionComments;   // TODO: Fix this

        public override ObservableCollection<ImageSource> ImageContainer => 
            InventorySection.Images;

        public override string TabName       => "INVENTORY SECTION";
        public override string PhotoTypeText => "Section photos";

        // **************** Class constructors ********************************************** //

        public InventorySectionViewModel()
        {
            RegionManagerName = "InventorySectionItemControl";

            CmdAddSection     = new DelegateCommand(OnAddSection);
            CmdCancelEdit     = new DelegateCommand(OnCancelEdit    );
            CmdDecValue       = new DelegateCommand(OnCmdDecValue);
            CmdDeleteSection  = new DelegateCommand(OnDeleteSection );
            CmdIncValue       = new DelegateCommand(OnCmdIncValue);
            CmdNextSection    = new DelegateCommand(OnNextSection);
            CmdSectionComment = new DelegateCommand(OnSectionComment);
            CmdToday          = new DelegateCommand(OnCmdToday);

#if DEBUG
#warning Using MOCK data for InventorySection
            InventorySection = new MockInventorySection();
#endif
            if (InventorySection != null)
            {
                InventorySection.ComponentType = InventorySection.ComponentTypes.FirstOrDefault();
                InventorySection.SectionName   = InventorySection.SectionNames  .FirstOrDefault();
            }
        }

        // **************** Class members *************************************************** //

        private void OnCancelEdit() { Debug.WriteLine("OnCancelEdit is not implemented"); }

        private void OnAddSection() { Debug.WriteLine("OnAddSection is not implemented"); }

        private void OnCmdDecValue()
        {
            if (!double.TryParse(InventorySection.Quantity, out var d)) return;

            if (--d < 0) d = 0;
            InventorySection.Quantity = $"{d:0.00}";
        }

        private void OnCmdIncValue()
        {
            if (!double.TryParse(InventorySection.Quantity, out var d)) return;

            InventorySection.Quantity = $"{++d:0.00}";
        }

        private void OnDeleteSection()
        {
            var result = FatFingerMessageBoxView.Show(
                "Do you want to delete this section and related information?",
                "DELETE SECTION?",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Question);

            switch(result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnNextSection  () 
        {
            if (++SelectedIndex >= InventorySection.SectionNames.Count)
                SelectedIndex = 0;

            InventorySection.SectionName = InventorySection.SectionNames[SelectedIndex];
        }

        private void OnSectionComment() { OnSelectedComment(null); }
        private void OnCmdToday() { InventorySection.Date = DateTime.Now.ToShortDateString(); }
    }
}
