using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BDC_V1.Interfaces;
using BDC_V1.Utils;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class NotifyingCollection<T> : BindableBase, INotifyPropertyChanged, INotifyingCollection<T>
    {
        protected bool SuppressNotification { get; set; }
 
        [NotNull]
        protected ObservableCollection<T> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = new ObservableCollection<T>();
                    _collection.CollectionChanged += OnCollectionChanged;

                    CollectionChanged += (o, i) =>
                    {
                        RaisePropertyChanged(nameof(HasItems));
                    };

                    RaisePropertyChanged(nameof(HasItems));
                }

                // ReSharper disable once AssignNullToNotNullAttribute
                return _collection;
            }
        }
        [CanBeNull] private ObservableCollection<T> _collection;

        public bool SuppressNotifications { get; set; } = false;

        public bool HasItems => _collection?.Any  () ?? false;
        public int Count     => _collection?.Count() ?? 0;
        public virtual bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()   => Collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T this[int index]
        {
            get => Collection[index];
            set => Collection[index] = value;
        }

        public void Add     (T item) => Collection.Add(item);
        public bool Contains(T item) => _collection?.Contains(item) ?? false;
        public bool Remove  (T item) => _collection?.Remove  (item) ?? false;

        public void CopyTo(T[] array, int arrayIndex) =>
            Collection.CopyTo(array, arrayIndex);

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged;

        public NotifyingCollection()
        {
        }
 
        public NotifyingCollection([NotNull] IList<T> list)
            : this()
        {
            AddRange(list);
        }

        public NotifyingCollection([NotNull] IEnumerable<T> collection)
            : this()
        {
            AddRange(collection);
        }

        ~NotifyingCollection()
        {
            Collection.CollectionChanged -= OnCollectionChanged;
        }

        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!SuppressNotifications) CollectionChanged?.Invoke(sender, e);
        }

        public void Clear()
        {
            if (_collection == null) return;

            var oldSuppressNotification = SuppressNotification;
            SuppressNotification = true;

            try
            {
                Collection.Clear();
            }
            finally
            {
                SuppressNotification = oldSuppressNotification;
            }

            OnCollectionChanged(_collection, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset));
        }

        public void AddRange(IEnumerable<T> list)
        {
            if (list == null) return;

            var oldSuppressNotification = SuppressNotification;
            SuppressNotification = true;

            try
            {
                Collection.AddRange(list);
            }
            finally
            {
                SuppressNotification = oldSuppressNotification;
            }

            OnCollectionChanged(_collection, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, 
                list));
        }

        public IEnumerable<T> RemoveRange([CanBeNull] IEnumerable<T> list)
        {
            var removedList = new List<T>();

            if ((_collection != null) && (list != null))
            {
                var oldSuppressNotification = SuppressNotification;
                SuppressNotification = true;

                try
                {
                    foreach (var item in list)
                    {
                        if (Collection.Contains(item))
                        {
                            removedList.Add(item);
                            Collection.Remove(item);
                        }
                    }
                }
                finally
                {
                    SuppressNotification = oldSuppressNotification;
                }

                OnCollectionChanged(_collection, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Remove, 
                    removedList));
            }

            return removedList;
        }
    }
}
