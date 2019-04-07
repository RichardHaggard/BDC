using BDC_V1.Classes;
using BDC_V1.Services;

namespace BDC_V1.Mock_Data
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
