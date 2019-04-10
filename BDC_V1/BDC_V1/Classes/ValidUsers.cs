using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class ValidUsers : BindableBase, IValidUsers
    {
        [NotNull] 
        protected readonly Dictionary<IPerson, string> ValidUserDictionary =
            new Dictionary<IPerson, string>();

        [NotNull] 
        protected IReadOnlyDictionary<IPerson, string> ReadOnlyUserDictionary => 
            ValidUserDictionary;

        public ReadOnlyObservableCollection<IPerson> Users => 
            _users ?? (_users = new ReadOnlyObservableCollection<IPerson>(
                new ObservableCollection<IPerson>(ValidUserDictionary.Keys)));

        [CanBeNull] private ReadOnlyObservableCollection<IPerson> _users;
            
        public bool ValidateUser(IPerson person, string password)
        {
            return ValidUserDictionary.TryGetValue(person, out var validPass) && 
                   (validPass == password);
        }

        protected bool Add([NotNull] IPerson person, [CanBeNull] string password)
        {
            // Don't attempt to overwrite an existing user
            if (ValidUserDictionary.ContainsKey(person)) 
                return false;

            ValidUserDictionary.Add(person, password);

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }

        protected bool Remove(IPerson person, string password)
        {
            if (! ValidateUser(person, password))
                return false;

            ValidUserDictionary.Remove(person);

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }

        protected bool Clear(IPerson person, string password)
        {
            if (! ValidUserDictionary.Any()) 
                return false;

            ValidUserDictionary.Clear();

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }
    }
}
