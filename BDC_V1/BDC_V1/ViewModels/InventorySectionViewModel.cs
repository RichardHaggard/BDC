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
    public class InventorySectionViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdAddSection     { get; }
        [NotNull] public ICommand CmdCancelEdit     { get; }
        [NotNull] public ICommand CmdDecValue       { get; }
        [NotNull] public ICommand CmdDeleteSection  { get; }
        [NotNull] public ICommand CmdIncValue       { get; }
        [NotNull] public ICommand CmdNextSection    { get; }
        [NotNull] public ICommand CmdSectionComment { get; }
        [NotNull] public ICommand CmdToday          { get; }

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

        [NotNull] public IInventorySection InventorySection { get; }

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

        protected override ObservableCollection<ICommentBase> CommentContainerSource =>
            InventorySection.SectionComments;

        protected override ObservableCollection<ImageSource> ImageContainerSource =>
            InventorySection.Images;

        public IndexedCollection<string> FunctionalArea { get; } =
            new IndexedCollection<string>(new ObservableCollection<string>());

        public override string TabName       => "INVENTORY SECTION";
        public override string PhotoTypeText => "Section photos";
        public override string DetailHeaderText => 
            $@"Section Comment for {InventorySection.SectionName}";

        // **************** Class constructors ********************************************** //

        public InventorySectionViewModel()
        {
            RegionManagerName = "InventorySectionItemControl";

            CmdAddSection     = new DelegateCommand(OnAddSection    );
            CmdCancelEdit     = new DelegateCommand(OnCancelEdit    );
            CmdDecValue       = new DelegateCommand(OnCmdDecValue   );
            CmdDeleteSection  = new DelegateCommand(OnDeleteSection );
            CmdIncValue       = new DelegateCommand(OnCmdIncValue   );
            CmdNextSection    = new DelegateCommand(OnNextSection   );
            CmdSectionComment = new DelegateCommand(OnSectionComment);
            CmdToday          = new DelegateCommand(OnCmdToday      );

            FunctionalArea.Add("Please select functional area...");
            FunctionalArea.SelectedIndex = 0;
#if DEBUG
            InventorySection = new MockInventorySection();
#endif
            if (InventorySection != null)
            {
                InventorySection.ComponentType = InventorySection.ComponentTypes[0];
                InventorySection.SectionName   = InventorySection.SectionNames  [0];
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
                case MessageBoxResult.OK:
                case MessageBoxResult.Cancel:
                    break;
#if DEBUG
                default:
                    throw new ArgumentOutOfRangeException();
#endif
            }
        }

        private void OnNextSection()
        {
            if (!InventorySection.SectionNames.Any()) return;

            var index = InventorySection.SectionNames.IndexOf(InventorySection.SectionName);

            if (++index >= InventorySection.SectionNames.Count)
                index = 0;

            InventorySection.SectionName = InventorySection.SectionNames[index];
        }

        private void OnSectionComment() { OnSelectedComment(null); }
        private void OnCmdToday() { InventorySection.Date = DateTime.Now.ToShortDateString(); }
    }
}
