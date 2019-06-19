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
        protected readonly Dictionary<IInspector, string> ValidUserDictionary =
            new Dictionary<IInspector, string>();

        [NotNull] 
        protected IReadOnlyDictionary<IInspector, string> ReadOnlyUserDictionary => 
            ValidUserDictionary;

        public ReadOnlyObservableCollection<IInspector> Users
        {
            get
            {
                if (_users != null) return _users;

                var baseObject = new ObservableCollection<IInspector>();
                baseObject.AddRange(ReadOnlyUserDictionary.Keys);

                return _users = new ReadOnlyObservableCollection<IInspector>(baseObject);
            }
        }

        [CanBeNull] private ReadOnlyObservableCollection<IInspector> _users;
            
        public bool ValidateUser(IInspector inspector, string password)
        {
            return ValidUserDictionary.TryGetValue(inspector, out var validPass) && 
                   (string.IsNullOrEmpty(validPass) || (validPass == password));
        }

        public bool Add(IInspector inspector, string password)
        {
            // Don't attempt to overwrite an existing user
            if (ValidUserDictionary.ContainsKey(inspector)) 
                return false;

            ValidUserDictionary.Add(inspector, password ?? string.Empty);

            SetProperty(ref _users, null, nameof(Users));
            return true;
        }

        public bool Remove(IInspector inspector, string password)
        {
            if (! ValidateUser(inspector, password))
                return false;

            ValidUserDictionary.Remove(inspector);

            SetProperty(ref _users, null, nameof(Users));
            return true;
        }

        public bool Clear()
        {
            if (! ValidUserDictionary.Any()) 
                return false;

            ValidUserDictionary.Clear();

            SetProperty(ref _users, null, nameof(Users));
            return true;
        }
    }
}
