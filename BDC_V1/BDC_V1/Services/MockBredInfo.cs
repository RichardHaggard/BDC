using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;

namespace BDC_V1.Services
{
    public class MockBredInfo : BredInfo
    {
#if DEBUG
#warning Using MOCK data for BredInfo
        public MockBredInfo()
        {
            FacilityInfo.AddRange(MockFacility.Facilities);
        }
#endif
    }
}
