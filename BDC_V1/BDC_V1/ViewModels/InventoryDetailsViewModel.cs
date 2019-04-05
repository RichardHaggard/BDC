using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using BDC_V1.Services;
using BDC_V1.Utils;
using JetBrains.Annotations;

namespace BDC_V1.ViewModels
{
    public class InventoryDetailsViewModel : FacilityBaseClass
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        [NotNull]
        public IInventoryDetailsType InventoryDetails { get; }

        // **************** Class data members ********************************************** //

        public override IFacility LocalFacilityInfo
        {
            get => base.LocalFacilityInfo;
            set
            {
                base.LocalFacilityInfo = value;

                InventoryDetails.Images.Clear();
                InventoryDetails.Images.AddRange(base.LocalFacilityInfo?.Images);

                // QuickObservableCollection should raise it's own notify
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

        public InventoryDetailsViewModel()
        {
            RegionManagerName = "InventoryDetailsItemControl";
            InventoryDetails = new MockInventoryDetails();
        }

        // **************** Class members *************************************************** //

    }
}
