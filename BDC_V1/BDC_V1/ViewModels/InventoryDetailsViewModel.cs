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
    public class InventoryDetailsViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        public ICommand CmdAddDetail          { get; }
        public ICommand CmdCancelEdit         { get; }
        public ICommand CmdCopyDetail         { get; }
        public ICommand CmdDeleteDetail       { get; }
        public ICommand CmdDetailsComment     { get; }
        public ICommand CmdNextDetail         { get; }
        public ICommand CmdShowBarcodeScanner { get; }


        [NotNull]
        public IInventoryDetail InventoryDetails { get; }

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

        // **************** Class constructors ********************************************** //

        public InventoryDetailsViewModel()
        {
            RegionManagerName = "InventoryDetailsItemControl";

            CmdAddDetail          = new DelegateCommand( OnAddDetail             );
            CmdCancelEdit         = new DelegateCommand( OnCancelEdit            );
            CmdCopyDetail         = new DelegateCommand( OnCopyDetail            );
            CmdDeleteDetail       = new DelegateCommand( OnDeleteDetail          );
            CmdDetailsComment     = new DelegateCommand( OnDetailsComment        );
            CmdNextDetail         = new DelegateCommand( OnCmdNextDetails        );
            CmdShowBarcodeScanner = new DelegateCommand( OnCmdShowBarcodeScanner );

#if DEBUG
#warning Using MOCK data for InventoryDetails
            InventoryDetails = new MockInventoryDetails();
#endif
        }

        // **************** Class members *************************************************** //

        protected override void CreateImages() =>  
            CreateImages(InventoryDetails.HasImages? InventoryDetails.Images : null);

        private void OnCancelEdit    () { Debug.WriteLine("OnCancelEdit     is not implemented"); }
        private void OnDeleteDetail  () { Debug.WriteLine("OnDeleteDetail   is not implemented"); }
        private void OnAddDetail     () { Debug.WriteLine("OnAddDetail      is not implemented"); }
        private void OnCopyDetail    () { Debug.WriteLine("OnCopyDetail     is not implemented"); }

        private void OnCmdNextDetails()
        {
            // Go to the next Details item in the list.
            // If already at the last then wrap back to the first.
            if (++InventoryDetails.DetailSelectedIndex >= InventoryDetails.DetailSelectors.Count)
                InventoryDetails.DetailSelectedIndex = 0;
        }


        private void OnDetailsComment() 
        {             
            var view = new CommentView();
            view.ShowDialog();
        }

        private void OnCmdShowBarcodeScanner()
        {
            MessageBox.Show("Barcode Reader Launches", "Barcode Reader");
        }
    }
}
