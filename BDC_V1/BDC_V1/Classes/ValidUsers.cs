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
        protected readonly Dictionary<Person, string> ValidUserDictionary =
            new Dictionary<Person, string>();

        [NotNull] 
        protected IReadOnlyDictionary<Person, string> ReadOnlyUserDictionary => 
            ValidUserDictionary;

        public ReadOnlyObservableCollection<Person> Users
        {
            get
            {
                if (_users != null) return _users;

                var baseObject = new ObservableCollection<Person>();
                baseObject.AddRange(ReadOnlyUserDictionary.Keys);

                return _users = new ReadOnlyObservableCollection<Person>(baseObject);
            }
        }

        [CanBeNull] private ReadOnlyObservableCollection<Person> _users;
            
        public bool ValidateUser(Person person, string password)
        {
            return ValidUserDictionary.TryGetValue(person, out var validPass) && 
                   (string.IsNullOrEmpty(validPass) || (validPass == password));
        }

        public bool Add(Person person, string password)
        {
            // Don't attempt to overwrite an existing user
            if (ValidUserDictionary.ContainsKey(person)) 
                return false;

            ValidUserDictionary.Add(person, password ?? string.Empty);

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }

        public bool Remove(Person person, string password)
        {
            if (! ValidateUser(person, password))
                return false;

            ValidUserDictionary.Remove(person);

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }

        public bool Clear()
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
