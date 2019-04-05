using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.ViewModels
{
    public class InventoryViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class data members ********************************************** //

        // **************** Class constructors ********************************************** //

        public InventoryViewModel()
        {
            RegionManagerName = "InventoryItemControl";
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }
    }
}
