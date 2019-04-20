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
using System.Windows.Media;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    /// <summary>
    /// This is a wrapper around a <see cref="T:ObservableCollection"/> adding a Selected Index and Item
    /// </summary>
    /// <typeparam name="T">Class of the collection</typeparam>
    public class IndexedCollection<T> : PropertyBase, INotifyPropertyChanged
    {
        [NotNull]
        public ObservableCollection<T> Items { get; }

        public int SelectedIndex
        {
            get => _selectedIndex = Math.Max(-1, Math.Min(Items.Count - 1, _selectedIndex));
            set => SetPropertyFlagged(
                ref _selectedIndex, 
                Math.Max(-1, Math.Min(Items.Count - 1, value)), 
                nameof(SelectedItem));
        }
        private int _selectedIndex = -1;

        public T SelectedItem
        {
            get
            {
                var index = SelectedIndex;
                return (index >= 0)
                    ? Items[index]
                    : DefaultValue;
            }
            set => SelectedIndex = Items.IndexOf(value);
        }

        [CanBeNull] public T DefaultValue
        {
            get => _defaultValue;
            set => SetProperty(ref _defaultValue, value);
        }
        [CanBeNull] private T _defaultValue = default(T);

        public IndexedCollection([NotNull] ObservableCollection<T> items)
        {
            Items = items;

            Items.CollectionChanged += (o, i) =>
            {
                RaisePropertyChanged(new[]
                {
                    nameof(SelectedIndex),
                    nameof(SelectedItem)
                });
            };
        }

        [NotNull] 
        public static implicit operator ObservableCollection<T>([NotNull] IndexedCollection<T> source)
        {
            return source.Items;
        }
    }
}
