using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IValidUsers
    {
        [NotNull] ReadOnlyObservableCollection<Person> Users { get; }
        bool ValidateUser([NotNull] Person person, [CanBeNull] string password);

        bool Add   ([NotNull] Person person, [CanBeNull] string password);
        bool Remove([NotNull] Person person, [CanBeNull] string password);
        bool Clear ();
    }
}
