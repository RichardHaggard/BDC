using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface INotifyingCollection<T> : ICollection<T>, INotifyCollectionChanged
    {
        bool HasItems { get; }
        T this[int index] { get; set; }

        [NotNull]
        IEnumerable<T> RemoveRange([CanBeNull] IEnumerable<T> items);

        void AddRange([CanBeNull] IEnumerable<T> items);
    }
}