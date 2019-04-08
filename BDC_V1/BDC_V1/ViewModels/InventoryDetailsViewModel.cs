using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand CmdCancelEdit     { get; }
        public ICommand CmdDeleteDetail   { get; }
        public ICommand CmdAddDetail      { get; }
        public ICommand CmdCopyDetail     { get; }
        public ICommand CmdDetailsComment { get; }


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

        //        // NotifyingCollection should raise it's own notify
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

            CmdCancelEdit     = new DelegateCommand(OnCancelEdit    );
            CmdDeleteDetail   = new DelegateCommand(OnDeleteDetail  );
            CmdAddDetail      = new DelegateCommand(OnAddDetail     );
            CmdCopyDetail     = new DelegateCommand(OnCopyDetail    );
            CmdDetailsComment = new DelegateCommand(OnDetailsComment);

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

        private void OnDetailsComment() 
        {             
            var view = new CommentView();
            view.ShowDialog();
        }

    }
}
