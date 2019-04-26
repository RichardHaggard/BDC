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
    public class InventoryDetailsViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull] public ICommand CmdAddDetail          { get; }
        [NotNull] public ICommand CmdCancelEdit         { get; }
        [NotNull] public ICommand CmdCopyDetail         { get; }
        [NotNull] public ICommand CmdDeleteDetail       { get; }
        [NotNull] public ICommand CmdDetailsComment     { get; }
        [NotNull] public ICommand CmdNextDetail         { get; }
        [NotNull] public ICommand CmdShowBarcodeScanner { get; }

        [NotNull] public IInventoryDetail InventoryDetails { get; }

        public string EquipmentMakeUserEntered
        {
            get => _equipmentMakeUserEntered;
            set => SetProperty(ref _equipmentMakeUserEntered, value, () =>
            {
                InventoryDetails.EquipmentMakes.Add(_equipmentMakeUserEntered);
                InventoryDetails.EquipmentMake = _equipmentMakeUserEntered;
            });
        }
        private string _equipmentMakeUserEntered = string.Empty;

        public string ManufacturerUserEntered
        {
            get => _manufacturerUserEntered;
            set => SetProperty(ref _equipmentMakeUserEntered, value, () =>
            {
                InventoryDetails.Manufacturers.Add(_manufacturerUserEntered);
                InventoryDetails.Manufacturer = _manufacturerUserEntered;
            });
        }
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string _manufacturerUserEntered = string.Empty;


        // **************** Class data members ********************************************** //

        //public override IComponentFacility LocalFacilityInfo
        //{
        //    get => base.LocalFacilityInfo;
        //    set
        //    {
        //        base.LocalFacilityInfo = value;

        //        InventoryDetails.Images.Clear();
        //        InventoryDetails.Images.AddRange(base.LocalFacilityInfo?.Images);

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
            InventoryDetails.DetailComments;

        protected override ObservableCollection<ImageSource> ImageContainerSource =>
            InventoryDetails.Images;

        public override string TabName       => "INVENTORY DETAILS";
        public override string PhotoTypeText => "Inventory detail photos";
        public override string DetailHeaderText => 
            $@"Section Detail Comment for {InventoryDetails.CurrentSection}";

        // **************** Class constructors ********************************************** //

        public InventoryDetailsViewModel()
        {
            RegionManagerName = "InventoryDetailsItemControl";

            CmdAddDetail          = new DelegateCommand(OnAddDetail            );
            CmdCancelEdit         = new DelegateCommand(OnCancelEdit           );
            CmdCopyDetail         = new DelegateCommand(OnCopyDetail           );
            CmdDeleteDetail       = new DelegateCommand(OnDeleteDetail         );
            CmdDetailsComment     = new DelegateCommand(OnDetailsComment       );
            CmdNextDetail         = new DelegateCommand(OnCmdNextDetails       );
            CmdShowBarcodeScanner = new DelegateCommand(OnCmdShowBarcodeScanner);

#if DEBUG
//#warning Using MOCK data for InventoryDetails
            InventoryDetails = new MockInventoryDetails();
#endif
            if (InventoryDetails != null)
            {
                InventoryDetails.Manufacturer  = InventoryDetails.Manufacturers .FirstOrDefault();
                InventoryDetails.EquipmentMake = InventoryDetails.EquipmentMakes.FirstOrDefault();
            }
        }

        // **************** Class members *************************************************** //

        private void OnCancelEdit  () { Debug.WriteLine("OnCancelEdit   is not implemented"); }
        private void OnDeleteDetail() { Debug.WriteLine("OnDeleteDetail is not implemented"); }
        private void OnAddDetail   () { Debug.WriteLine("OnAddDetail    is not implemented"); }
        private void OnCopyDetail  () { Debug.WriteLine("OnCopyDetail   is not implemented"); }

        private void OnCmdNextDetails()
        {
            // Go to the next Details item in the list.
            // If already at the last then wrap back to the first.
            if (++InventoryDetails.DetailSelectedIndex >= InventoryDetails.DetailSelectors.Count)
                InventoryDetails.DetailSelectedIndex = 0;
        }

        private void OnDetailsComment() 
        {             
            OnSelectedComment(CommentContainer?.FirstOrDefault());
        }

        private void OnCmdShowBarcodeScanner()
        {
            BdcMessageBoxView.Show("Barcode Reader Launches", "Barcode Reader");
        }
    }
}
