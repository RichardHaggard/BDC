using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
#if false
    public class IndexedCollection<T> : PropertyBase, IIndexedCollection<T>
    {
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
        public ObservableCollection<T> ItemCollection { get; } =
            new ObservableCollection<T>();

        /// <inheritdoc />>
        public virtual bool HasContent => ItemCollection.Any();



            //PropertyCollection<T>(ref _itemCollection, string.Empty, () =>
            //    {
            //        ItemCollection.CollectionChanged += (o, i) =>
            //        {
            //            RaisePropertyChanged(nameof(ItemCollection));
            //            RaisePropertyChanged(nameof(SelectedItem)  );
            //            RaisePropertyChanged(nameof(SelectedIndex) );
            //            RaisePropertyChanged(nameof(HasContent)    );

            //            CollectionChanged?.Invoke(o, i);
            //        };
            //    });

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

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> list) => 
            ItemCollection.AddRange(list);

        /// <inheritdoc />
        public IEnumerable<T> RemoveRange([CanBeNull] IEnumerable<T> removeList)
        {
            var returnList = new List<T>();

            if (removeList != null)
            {
                // insure we are only removing items contained within the collection
                returnList.AddRange(removeList.Where(item => ItemCollection.Contains(item)));
                foreach (var item in returnList) ItemCollection.Remove(item);
            }

            return returnList;
        }

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
            : this()
        {
            AddRange(list);
        }

        /// <summary>
        /// initializing ctor
        /// </summary>
        public IndexedCollection([NotNull] IEnumerable<T> collection)
            : this()
        {
            AddRange(collection);
        }
    }
#endif
}
