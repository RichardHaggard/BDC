﻿using System;
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
        public ICommand CmdNextSection    { get; }

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

        // **************** Class constructors ********************************************** //

        public InventorySectionViewModel()
        {
            RegionManagerName = "InventorySectionItemControl";

            CmdCancelEdit     = new DelegateCommand(OnCancelEdit    );
            CmdAddSection     = new DelegateCommand(OnAddSection    );
            CmdDeleteSection  = new DelegateCommand(OnDeleteSection );
            CmdSectionComment = new DelegateCommand(OnSectionComment);
            CmdNextSection    = new DelegateCommand(OnNextSection   );

#if DEBUG
#warning Using MOCK data for InventorySection
            InventorySection = new MockInventorySection();
#endif
        }

        // **************** Class members *************************************************** //

        private void OnCancelEdit   () { Debug.WriteLine("OnCancelEdit    is not implemented"); }
        private void OnAddSection   () { Debug.WriteLine("OnAddSection    is not implemented"); }
        private void OnDeleteSection() { Debug.WriteLine("OnDeleteSection is not implemented"); }
        private void OnNextSection  () { Debug.WriteLine("OnNextSection   is not implemented"); }

        private void OnSectionComment() 
        {             
            OnSelectedComment(null);
        }
    }
}
