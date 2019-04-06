using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.ViewModels
{
    public class InventorySectionViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public IInventorySection InventorySection { get; }

        // **************** Class data members ********************************************** //

        public override IComponentFacility LocalFacilityInfo
        {
            get => base.LocalFacilityInfo;
            set
            {
                base.LocalFacilityInfo = value;

                InventorySection.Images.Clear();
                InventorySection.Images.AddRange(base.LocalFacilityInfo?.Images);

                // NotifyingCollection should raise it's own notify
                //RaisePropertyChanged(nameof(InventorySection));
            }
        }

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
            InventorySection = new MockInventorySection();
        }


        // **************** Class members *************************************************** //

    }
}
