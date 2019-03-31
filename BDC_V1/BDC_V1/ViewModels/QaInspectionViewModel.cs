using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDC_V1.ViewModels
{
    public class QaInspectionViewModel : ViewModelBase
    {
        // **************** Class enumerations ********************************************** //

        // **************** Class properties ************************************************ //

        // **************** Class data members ********************************************** //

        //[CanBeNull] 
        //protected IFacility LocalFacilityInfo
        //{
        //    get => _localFacilityInfo;
        //    set
        //    {
        //        if (SetProperty(ref _localFacilityInfo, value))
        //        {

        //        }
        //    }
        //}
        //[CanBeNull] private IFacility _localFacilityInfo;

        //protected override IConfigInfo LocalConfigInfo
        //{
        //    get => base.LocalConfigInfo;
        //    set
        //    {
        //        base.LocalConfigInfo = value;
        //    }
        //}

        //protected override IBredInfo LocalBredInfo
        //{
        //    get => base.LocalBredInfo;
        //    set
        //    {
        //        base.LocalBredInfo = value;
        //        LocalFacilityInfo = base.LocalBredInfo?.FacilityInfo;
        //    }
        //}


        // **************** Class constructors ********************************************** //

        public QaInspectionViewModel()
        {
            RegionManagerName = "QaInspectionItemControl";
        }

        // **************** Class members *************************************************** //

        protected override bool GetRegionManager()
        {
            return false;
        }

    }
}
