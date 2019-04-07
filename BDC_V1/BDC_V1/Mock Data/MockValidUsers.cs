using BDC_V1.Classes;

namespace BDC_V1.Mock_Data
{
    public class MockValidUsers : ValidUsers
    {
#if DEBUG
#warning Using MOCK data for ValidUsers
        public MockValidUsers()
        {
            ValidUserDictionary.Add(new Person() {FirstName = "Rick"  , LastName = "Wakeman"}, "Yes");
            ValidUserDictionary.Add(new Person() {FirstName = "Keith" , LastName = "Emerson"}, "ELP");
            ValidUserDictionary.Add(new Person() {FirstName = "Carlos", LastName = "Santana"}, "EvilWoman");
            ValidUserDictionary.Add(new Person() {FirstName = "George", LastName = "Jetson" }, "Leroy");
        }
#endif
    }
}
