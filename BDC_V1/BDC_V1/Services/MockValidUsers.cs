using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;

namespace BDC_V1.Services
{
    public class MockValidUsers : IValidUsers
    {
        private readonly Dictionary<string, string> _validUsers = new Dictionary<string, string>();

        public IReadOnlyCollection<string> GetValidUsers()
        {
            return _validUsers.Keys;
        }

        public bool ValidateUser(string userName, string password)
        {
            return _validUsers.TryGetValue(userName, out var validPass) && (validPass == password);
        }

        public MockValidUsers()
        {
            _validUsers.Add("Rick Wakeman", "Yes");
            _validUsers.Add("Keith Emerson", "ELP");
            _validUsers.Add("Carlos Santana", "EvilWoman");
        }
    }
}
