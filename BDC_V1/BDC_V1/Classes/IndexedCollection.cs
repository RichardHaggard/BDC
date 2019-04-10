using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Classes
{
    public class IndexedCollection<T> : PropertyBase, IIndexedCollection<T>
    {
        /// <inheritdoc />>
        public bool HasContent => ItemCollection.HasItems;

        /// <inheritdoc />>
        public int SelectedIndex
        {
            get
            {
                if (! HasContent) _selectedIndex = -1;
                else _selectedIndex = 
                    Math.Max(-1, 
                    Math.Min(ItemCollection.Count - 1, _selectedIndex));

                return _selectedIndex;
            }
            
            set
            {
                if (! HasContent) value = -1;
                else value = 
                    Math.Max(-1, 
                    Math.Min(ItemCollection.Count - 1, value));

                SetPropertyFlagged(ref _selectedIndex, value, nameof(SelectedItem));
            }
        }
        private int _selectedIndex = -1;

        /// <inheritdoc />>
        public T DefaultValue
        {
            get => _defaultValue;
            set => SetProperty(ref _defaultValue, value);
        }
        [CanBeNull] private T _defaultValue;

        /// <inheritdoc />>
        public T SelectedItem
        {
            get => (SelectedIndex >= 0)
                ? ItemCollection[SelectedIndex]
                : DefaultValue;

            set => SelectedIndex = (HasContent)
                ? ItemCollection.IndexOf(value)
                : -1;
        }

        /// <inheritdoc />>
        public INotifyingCollection<T> ItemCollection =>
            PropertyCollection<T>(ref _itemCollection, string.Empty, () =>
                {
                    ItemCollection.CollectionChanged += (o, i) =>
                    {
                        RaisePropertyChanged(nameof(ItemCollection));
                        RaisePropertyChanged(nameof(SelectedItem)  );
                        RaisePropertyChanged(nameof(SelectedIndex) );
                        RaisePropertyChanged(nameof(HasContent)    );

                        CollectionChanged?.Invoke(o, i);
                    };
                });

        [CanBeNull] private INotifyingCollection<T> _itemCollection;

        /// <inheritdoc />>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => 
            ItemCollection.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => 
            ((IEnumerable) ItemCollection).GetEnumerator();

        /// <inheritdoc />
        public void Add(T item) => 
            ItemCollection.Add(item);

        /// <inheritdoc />
        public void Clear() => 
            ItemCollection.Clear();

        /// <inheritdoc />
        public bool Contains(T item) => 
            ItemCollection.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) => 
            ItemCollection.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(T item) => 
            ItemCollection.Remove(item);

        /// <inheritdoc />
        public int Count => 
            ItemCollection.Count;

        /// <inheritdoc />
        public bool IsReadOnly => 
            ItemCollection.IsReadOnly;

        /// <summary>
        /// Add multiple items
        /// </summary>
        /// <remarks>
        /// Suppresses notifications until complete
        /// </remarks>
        /// <param name="list"></param>
        public void AddRange(IEnumerable<T> list) => 
            ItemCollection.AddRange(list);

        /// <summary>
        /// Remove multiple items
        /// </summary>
        /// <remarks>
        /// Suppresses notifications until complete
        /// </remarks>
        /// <param name="list"></param>
        public IEnumerable<T> RemoveRange([CanBeNull] IEnumerable<T> list) => 
            ItemCollection.RemoveRange(list);

        /// <summary>
        /// void ctor
        /// </summary>
        public IndexedCollection()
        {
        }
 
        /// <summary>
        /// initializing ctor
        /// </summary>
        public IndexedCollection([NotNull] IList<T> list)
        {
            AddRange(list);
        }

        /// <summary>
        /// initializing ctor
        /// </summary>
        public IndexedCollection([NotNull] IEnumerable<T> collection)
        {
            AddRange(collection);
        }
    }
}
