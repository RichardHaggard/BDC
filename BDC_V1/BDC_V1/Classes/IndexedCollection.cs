using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    /// <summary>
    /// This is a wrapper around a <see cref="T:ObservableCollection"/> adding a Selected Index and Item
    /// </summary>
    /// <typeparam name="T">Class of the collection</typeparam>
    public class IndexedCollection<T> : PropertyBase, INotifyPropertyChanged, IList<T>
    {
        [NotNull] public ListCollectionView Items { get; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, Math.Max(-1, Math.Min(Items.Count - 1, value)),
                () =>
                {
                    _selectedItem = (_selectedIndex >= 0)
                        ? (T) Items.GetItemAt(_selectedIndex)
                        : default(T);

                    RaisePropertyChanged(nameof(SelectedItem));
                });
        }

        private int _selectedIndex = -1;

        [CanBeNull]
        public T SelectedItem
        {
            get
            {
                try
                {
                    return (_selectedIndex == -1)
                        ? _selectedItem = default(T)
                        : _selectedItem;
                }
                catch (Exception)
                {
                    return default(T);
                }
            }

            set
            {
                try
                {
                    SetProperty(ref _selectedItem, value,
                        () =>
                        {
                            var oldIndex = _selectedIndex;

                            _selectedIndex = Items.Contains(_selectedItem) 
                                ? Items.IndexOf(_selectedItem) 
                                : -1;

                            if (oldIndex != _selectedIndex)
                                RaisePropertyChanged(nameof(SelectedIndex));
                        });
                }
                catch (Exception)
                {
                    _selectedItem = default(T);
                }
            }
        }
        private T _selectedItem;

        /// <inheritdoc />
        public IndexedCollection([NotNull] IList items)
            : this(new ListCollectionView(items)) 
        {
        }

        public IndexedCollection([NotNull] ListCollectionView itemView)
        {
            Items = itemView;
            ((INotifyCollectionChanged)Items).CollectionChanged += (o, i) =>
            {
                if (SelectedIndex == -1) return;

                var oldItem = SelectedItem;
                switch (i.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    case NotifyCollectionChangedAction.Move:
                    case NotifyCollectionChangedAction.Remove:
                    case NotifyCollectionChangedAction.Replace:
                        SelectedIndex = Items.IndexOf(oldItem);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        //SelectedIndex = -1;
                        RaisePropertyChanged(new[]
                        {
                            nameof(SelectedIndex),
                            nameof(SelectedItem)
                        });
                        break;
#if DEBUG
                    default:
                        throw new ArgumentOutOfRangeException();
#endif
                }
            };
        }

        public void Refresh() => Items.Refresh();

        public Predicate<T> Filter
        {
            get => Items.Filter as Predicate<T>;
            set => Items.Filter = value as Predicate<object>;
        }

        public IEnumerable<T> Collection => Items.SourceCollection.Cast<T>();

        public IList<T> CollectionList
        {
            get
            {
                if (!(Collection is IList<T> list))
                    throw new InvalidCastException();

                return list;
            }
        }

        [NotNull] public T First() => CollectionList.First();
        [NotNull] public T Last () => CollectionList.Last ();

        [CanBeNull] public T FirstOrDefault() => CollectionList.FirstOrDefault();
        [CanBeNull] public T LastOrDefault () => CollectionList.LastOrDefault ();

        public void AddRange([NotNull] IndexedCollection<T> items) => AddRange(items.Collection);
        public void AddRange([CanBeNull] IEnumerable<T> items)
        {
            if (items == null) return;

            var itemArray = items.Where(i => i != null).ToArray();
            if (!itemArray.Any()) return;

            foreach (var item in itemArray) Add(item);
        }

        /// <inheritdoc />
        public void Add(T item) => CollectionList.Add(item);

        /// <inheritdoc />
        public bool Contains(T item) => (item != null) && Items.Contains(item);

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) => CollectionList.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        bool ICollection<T>.Remove(T item) => CollectionList.Remove(item);

        /// <inheritdoc />
        public int IndexOf(T item) => (item != null) ? Items.IndexOf(item) : -1; 

        /// <inheritdoc />
        public void Insert(int index, T item) => CollectionList.Insert(index, item);

        /// <inheritdoc />
        public void RemoveAt(int index) => CollectionList.RemoveAt(index);

        public void Remove(T item) => CollectionList.Remove(item);

        /// <inheritdoc />
        public T this[int index]
        {
            get => CollectionList[index];
            set => CollectionList[index] = value;
        }

        /// <inheritdoc />
        public void Clear() => CollectionList.Clear();

        /// <inheritdoc />
        public int Count => Items.Count;

        /// <inheritdoc />
        public bool IsReadOnly => CollectionList.IsReadOnly;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => CollectionList.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
