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
                   (validPass == password);
        }

        protected bool Add([NotNull] Person person, [CanBeNull] string password)
        {
            // Don't attempt to overwrite an existing user
            if (ValidUserDictionary.ContainsKey(person)) 
                return false;

            ValidUserDictionary.Add(person, password);

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }

        protected bool Remove(Person person, string password)
        {
            if (! ValidateUser(person, password))
                return false;

            ValidUserDictionary.Remove(person);

            _users = null;
            RaisePropertyChanged(nameof(Users));
            return true;
        }

        protected bool Clear(Person person, string password)
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
