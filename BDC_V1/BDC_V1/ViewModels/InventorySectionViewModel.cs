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

        public ICommand CmdCancelEdit     { get; }
        public ICommand CmdDeleteSection  { get; }
        public ICommand CmdAddSection     { get; }
        public ICommand CmdSectionComment { get; }

        public bool Estimated
        {
            get { return _Estimated; }
            set 
            { 
                if ( SetProperty(ref _Estimated, value) )
                {
                    YearInstalledBg = (value) ? "Yellow" : "Transparent";
                }
            }
        }
        private bool _Estimated;


        public string YearInstalledBg
        {
            get { return _YearInstalledBg; }
            set { SetProperty(ref _YearInstalledBg, value); }
        }
        private string _YearInstalledBg;


        [NotNull]
        public IInventorySection InventorySection { get; }

        // **************** Class data members ********************************************** //

        //public override IComponentFacility LocalFacilityInfo
        //{
        //    get => base.LocalFacilityInfo;
        //    set
        //    {
        //        base.LocalFacilityInfo = value;

        //        InventorySection.Images.Clear();
        //        InventorySection.Images.AddRange(base.LocalFacilityInfo?.Images);

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

        public InventorySectionViewModel()
        {
            RegionManagerName = "InventorySectionItemControl";

            CmdCancelEdit     = new DelegateCommand(OnCancelEdit    );
            CmdAddSection     = new DelegateCommand(OnAddSection    );
            CmdDeleteSection  = new DelegateCommand(OnDeleteSection );
            CmdSectionComment = new DelegateCommand(OnSectionComment);

#if DEBUG
#warning Using MOCK data for InventorySection
            InventorySection = new MockInventorySection();
#endif
        }

        protected override void CreateImages() =>  
            CreateImages(InventorySection.HasImages? InventorySection.Images : null);

        // **************** Class members *************************************************** //

        private void OnCancelEdit    () { Debug.WriteLine("OnCancelEdit     is not implemented"); }
        private void OnAddSection    () { Debug.WriteLine("OnAddSection     is not implemented"); }
        private void OnDeleteSection () { Debug.WriteLine("OnDeleteSection  is not implemented"); }

        private void OnSectionComment() 
        {             
            var view = new CommentView();
            view.ShowDialog();
        }
    }
}
