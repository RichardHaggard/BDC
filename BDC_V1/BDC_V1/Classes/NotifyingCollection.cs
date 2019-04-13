using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    /// <inheritdoc cref="T:INotifyingCollection"/>
    public class NotifyingCollection<T> : BindableBase, INotifyingCollection<T>
    {
        public bool SuppressNotifications { get; set; }

        [NotNull]
        protected ObservableCollection<T> Collection
        {
            get => _collection;
            set
            {
                var oldCollection = _collection;
                SetProperty(ref _collection, value, () =>
                {
                    if (oldCollection != null)
                    {
                        oldCollection.CollectionChanged -= OnCollectionChanged;
                        oldCollection.Clear();
                    }

                    _collection.CollectionChanged += OnCollectionChanged;

                    CollectionChanged += (o, i) => { RaisePropertyChanged(nameof(HasItems)); };
                    RaisePropertyChanged(nameof(HasItems));
                });
            }
        }
        private ObservableCollection<T> _collection;

        public bool HasItems => _collection?.Any() ?? false;

        /// <inheritdoc />
        public int Count => _collection?.Count ?? 0;

        /// <inheritdoc />
        public virtual bool IsReadOnly => false;

        /// <inheritdoc />
        public int IndexOf(T val) => Collection.IndexOf(val);

        /// <inheritdoc />
        public void Insert(int index, T item) => Collection.Insert(index, item);

        /// <inheritdoc />
        public void RemoveAt(int index) => Collection.RemoveAt(index);

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public T this[int index]
        {
            get => Collection[index];
            set => Collection[index] = value;
        }

        /// <inheritdoc />
        public void Add(T item) => Collection.Add(item);

        /// <inheritdoc />
        public bool Contains(T item) => _collection?.Contains(item) ?? false;

        /// <inheritdoc />
        public bool Remove(T item) => _collection?.Remove  (item) ?? false;

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) =>
            Collection.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        /// <remarks>
        /// Notifications are blocked until the Clear is completed.
        /// </remarks>
        public void Clear()
        {
            if (_collection == null) return;

            var oldSuppressNotifications = SuppressNotifications;
            try
            {
                SuppressNotifications = true;
                Collection.Clear();
            }
            finally
            {
                SuppressNotifications = oldSuppressNotifications;
            }

            if (! oldSuppressNotifications)
            {
                OnCollectionChanged(_collection, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
            }
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> list)
        {
            if (list == null) return;

            var oldSuppressNotifications = SuppressNotifications;
            try
            {
                SuppressNotifications = true;
                Collection.AddRange(list);
            }
            finally
            {
                SuppressNotifications = oldSuppressNotifications;
            }

            if (! oldSuppressNotifications)
            {
                OnCollectionChanged(_collection, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Add,
                    list));
            }
        }

        /// <inheritdoc />
        public IEnumerable<T> RemoveRange(IEnumerable<T> list)
        {
            if ((_collection == null) || (list == null)) return new List<T>();

            // ReSharper disable once PossibleMultipleEnumeration
            var removedList = list.Where(item => Collection.Contains(item)).ToList();
            if (removedList.Any())
            {
                var oldSuppressNotifications = SuppressNotifications;
                try
                {
                    SuppressNotifications = true;
                    removedList.ForEach(item => Collection.Remove(item));
                }
                finally
                {
                    SuppressNotifications = oldSuppressNotifications;
                }

                if (! oldSuppressNotifications)
                {
                    OnCollectionChanged(_collection, new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove,
                        removedList));
                }
            }

            return removedList;
        }

        /// <summary>Void ctor</summary>
        public NotifyingCollection()
        {
            Collection = new ObservableCollection<T>();
        }
 
        /// <summary>List initializing ctor</summary>
        public NotifyingCollection([NotNull] IList<T> list)
            : this()
        {
            AddRange(list);
        }

        /// <summary>Enumerating initializing ctor</summary>
        public NotifyingCollection([NotNull] IEnumerable<T> collection)
            : this()
        {
            AddRange(collection);
        }

        ~NotifyingCollection()
        {
            if (_collection == null) return;

            _collection.CollectionChanged -= OnCollectionChanged;
            _collection.Clear();
            _collection = null;
        }

        /// <inheritdoc />
        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!SuppressNotifications) CollectionChanged?.Invoke(sender, e);
        }
    }
}
