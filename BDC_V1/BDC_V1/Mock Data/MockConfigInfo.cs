using BDC_V1.Classes;
using BDC_V1.Services;

namespace BDC_V1.Mock_Data
{
    public class MockConfigInfo : ConfigInfo
    {
#if DEBUG
#warning Using MOCK data for ConfigInfo
        public MockConfigInfo()
        {
            ValidUsers = new MockValidUsers();
        }
#endif
    }
}
