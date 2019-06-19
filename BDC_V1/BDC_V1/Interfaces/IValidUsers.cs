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
        [NotNull] ReadOnlyObservableCollection<IInspector> Users { get; }
        bool ValidateUser([NotNull] IInspector person, [CanBeNull] string password);

        bool Add   ([NotNull] IInspector person, [CanBeNull] string password);
        bool Remove([NotNull] IInspector person, [CanBeNull] string password);
        bool Clear ();
    }
}
