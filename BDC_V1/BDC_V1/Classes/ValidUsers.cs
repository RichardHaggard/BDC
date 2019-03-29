using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class ValidUsers : IValidUsers
    {
        protected readonly Dictionary<IPerson, string> ValidUserDictionary = new Dictionary<IPerson, string>();

        [NotNull]
        public IReadOnlyCollection<IPerson> GetValidUsers => ValidUserDictionary.Keys;

        public bool ValidateUser([CanBeNull] IPerson userName, [CanBeNull] string password)
        {
            if ((userName == null) || string.IsNullOrEmpty(password)) return false;
            return ValidUserDictionary.TryGetValue(userName, out var validPass) && (validPass == password);
        }
    }
}
