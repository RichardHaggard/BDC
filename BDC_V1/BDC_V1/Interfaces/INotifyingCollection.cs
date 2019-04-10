using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    /// <summary>
    /// A minimal, interface-based observable collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifyingCollection<T> : ICollection<T>, INotifyCollectionChanged
    {
        /// <summary>
        /// Used to suppress notifications during lengthy operations
        /// </summary>
        bool SuppressNotifications { get; set; }
        
        /// <summary>
        /// Non-volatile test for is Null or Empty
        /// </summary>
        bool HasItems { get; }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than zero.-or-
        ///     <paramref name="index" /> is equal to or greater than <see cref="T:INotifyingCollection.Count" />.
        /// </exception>
        [CanBeNull] T this[int index] { get; set; }

        /// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.ObjectModel.Collection`1" />.</summary>
        /// <typeparam name="T">Type of objects within the collection.</typeparam>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.List`1" />. The value can be <see langword="null" /> for reference types.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="item" /> within the entire
        /// <see cref="T:INotifyingCollection" />, if found; otherwise, -1.
        /// </returns>
        int IndexOf([CanBeNull] T item);

        /// <summary>Add a range of items to a collection.</summary>
        /// <typeparam name="T">Type of objects within the collection.</typeparam>
        /// <param name="items">The items to add to the collection.</param>
        /// <returns>The collection.</returns>
        /// <remarks>
        /// Notifications are blocked until the range is completed.
        /// </remarks>
        [NotNull] IEnumerable<T> RemoveRange([CanBeNull] IEnumerable<T> items);

        /// <summary>Add a range of items to a collection.</summary>
        /// <typeparam name="T">Type of objects within the collection.</typeparam>
        /// <param name="items">The items to add to the collection.</param>
        /// <returns>The collection.</returns>
        /// <remarks>
        /// Notifications are blocked until the range is completed.
        /// </remarks>
        void AddRange([CanBeNull] IEnumerable<T> items);
    }
}