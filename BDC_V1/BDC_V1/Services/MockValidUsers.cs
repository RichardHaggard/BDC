using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Services
{
    public class MockValidUsers : IValidUsers
    {
        private readonly Dictionary<IPerson, string> _validUsers = new Dictionary<IPerson, string>();

        [NotNull]
        public IReadOnlyCollection<IPerson> GetValidUsers()
        {
            return _validUsers.Keys;
        }

        public bool ValidateUser([CanBeNull] IPerson userName, [CanBeNull] string password)
        {
            if ((userName == null) || string.IsNullOrEmpty(password)) return false;
            return _validUsers.TryGetValue(userName, out var validPass) && (validPass == password);
        }

        public MockValidUsers()
        {
            _validUsers.Add(new Person() {FirstName = "Rick"  , LastName = "Wakeman"}, "Yes");
            _validUsers.Add(new Person() {FirstName = "Keith" , LastName = "Emerson"}, "ELP");
            _validUsers.Add(new Person() {FirstName = "Carlos", LastName = "Santana"}, "EvilWoman");
        }
    }
}
