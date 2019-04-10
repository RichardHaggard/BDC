using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Interfaces
{
    public interface IIndexedCollection<T> :  
        ICollection<T>, 
        INotifyPropertyChanged,
        INotifyCollectionChanged
    {
        /// <summary>
        /// non-volatile test for collection is null or empty
        /// </summary>
        bool HasContent { get; }

        /// <summary>
        /// Public r/w accessor for the current selected index
        /// </summary>
        /// <remarks>
        /// A return value of -1 indicates no current selection or
        /// collection is empty.
        /// </remarks>
        int SelectedIndex { get; set; }

        /// <summary>
        /// Default value to return 
        /// </summary>
        /// <remarks>
        /// Returns this value for <see cref="SelectedItem"/> if <see cref="SelectedIndex"/> == -1
        /// </remarks>
        [CanBeNull] T DefaultValue { get; set; }

        /// <summary>
        /// Current selection
        /// </summary>
        /// <remarks>
        /// Derived from <see cref="ItemCollection"/>[<see cref="SelectedIndex"/>]
        /// </remarks>
        [CanBeNull] T SelectedItem { get; set; }

        /// <summary>
        /// The collection
        /// </summary>
        [NotNull] INotifyingCollection<T> ItemCollection { get; }
    }
}
