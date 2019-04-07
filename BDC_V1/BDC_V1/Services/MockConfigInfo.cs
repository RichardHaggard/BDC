using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;

namespace BDC_V1.Services
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
