using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    /// <inheritdoc cref="T:IList"/>/>
    /// <summary>
    /// A minimal, interface-based observable collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INotifyingCollection<T> : IList<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        /// <summary>
        /// Used to suppress notifications during lengthy operations
        /// </summary>
        bool SuppressNotifications { get; set; }
        
        /// <summary>
        /// Non-volatile test for is Null or Empty
        /// </summary>
        bool HasItems { get; }

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