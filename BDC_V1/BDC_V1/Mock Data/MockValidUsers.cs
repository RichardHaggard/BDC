using System;
using BDC_V1.Classes;

namespace BDC_V1.Mock_Data
{
    public class MockValidUsers : ValidUsers
    {
#if DEBUG
#warning Using MOCK data for ValidUsers
        public MockValidUsers()
        {
            ValidUserDictionary.Add(new Inspector {UserId = new Guid(), FirstName = "Rick"  , LastName = "Wakeman"}, "Yes");
            ValidUserDictionary.Add(new Inspector {UserId = new Guid(), FirstName = "Keith" , LastName = "Emerson"}, "ELP");
            ValidUserDictionary.Add(new Inspector {UserId = new Guid(), FirstName = "Carlos", LastName = "Santana"}, "EvilWoman");
            ValidUserDictionary.Add(new Inspector {UserId = new Guid(), FirstName = "George", LastName = "Jetson" }, "Leroy");
        }
#endif
    }
}
