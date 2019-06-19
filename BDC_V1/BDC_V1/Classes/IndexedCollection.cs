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
    public class IndexedCollection<T> : ListCollectionView, INotifyPropertyChanged, INotifyCollectionChanged, IPropertyBaseHelper
    {
        /// <summary>Occurs when a property value changes.</summary>
        /// <remarks>Can't use the ListCollectionView.PropertyChanged as it is protected and is only used for internal changes</remarks>
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Condenses all the property notification into one common class for those classes that can't inherit PropertyBase directly
        /// </summary>
        protected readonly PropertyBaseHelper PBase;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set =>
                PBase.SetProperty(
                    ref _selectedIndex,
                    Math.Max(-1, Math.Min(base.Count - 1, value)),
                    new[]
                    {
                        nameof(SelectedIndex),
                        nameof(SelectedItem)
                    });
        }
        private int _selectedIndex = -1;

        [CanBeNull]
        public T SelectedItem
        {
            get => (_selectedIndex >= 0)
                ? (T) GetItemAt(_selectedIndex)
                : default(T);

            set
            {
                var index = ((value != null) && Contains(value))
                    ? IndexOf(value)
                    : -1;

                PBase.SetProperty(
                    ref _selectedIndex,
                    Math.Max(-1, Math.Min(base.Count - 1, index)),
                    new[]
                    {
                        nameof(SelectedIndex),
                        nameof(SelectedItem)
                    });
            }
        }

        public IndexedCollection([CanBeNull] IList items = null)
            : base(items ?? new ObservableCollection<T>())
        {
            PBase = new PropertyBaseHelper(this);
        }

        public IndexedCollection([NotNull] ICollectionView srcItems)
            : this(srcItems.SourceCollection as IList)
        {
        }

        // Get access to the base collection
        [NotNull] public IList<T> Collection => (IList<T>) SourceCollection;

        [CanBeNull] public T FirstOrDefault()
        {
            return (Count > 0)
                ? (T) GetItemAt(0)
                : default(T);
        }

        //public void Sort()
        //{
        //    var selected = SelectedItem;

        //    var sorting = new SortedList<T,T>();
        //    foreach (var item in Collection)
        //        sorting.Add(item, item);

        //    Collection.Clear();
        //    Collection.AddRange(sorting.Keys);

        //    SelectedItem = selected;
        //}

        public new void OnPropertyChanged(PropertyChangedEventArgs args) =>
            this.PropertyChanged?.Invoke(this, args);
    }
}
