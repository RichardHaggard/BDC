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
    public class IndexedCollection<T> : ObservableCollection<T>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        public int SelectedIndex
        {
            get => _selectedIndex = Math.Max(-1, Math.Min(Count - 1, _selectedIndex));
            set => SetPropertyFlagged(
                ref _selectedIndex, 
                Math.Max(-1, Math.Min(Count - 1, value)), 
                nameof(SelectedItem));
        }
        private int _selectedIndex = -1;

        [CanBeNull] public T SelectedItem
        {
            get
            {
                var index = SelectedIndex;
                return (index >= 0)
                    ? this[index]
                    : DefaultValue;
            }
            set => SelectedIndex = IndexOf(value);
        }

        [CanBeNull] public T DefaultValue
        {
            get => _defaultValue;
            set => SetProperty(ref _defaultValue, value);
        }
        [CanBeNull] private T _defaultValue = default(T);

        public IEnumerable<T> RemoveRange([CanBeNull] IEnumerable<T> removeList)
        {
            var returnList = new List<T>(removeList?.Where(Contains) ?? new List<T>());
            foreach (var item in returnList) Remove(item);
            return returnList;
        }

        /// <inheritdoc />
        public IndexedCollection()
        {
        }
 
        /// <inheritdoc />
        public IndexedCollection([NotNull] IList<T> list)
            : base(list)
        {
        }

        /// <inheritdoc />
        public IndexedCollection([NotNull] IEnumerable<T> collection)
            : base(collection)
        {
        }


        /// <summary>
        /// Set the property value and raise optional flags
        /// </summary>
        /// <typeparam name="T1">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="flag">a property names to raise</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected virtual bool SetPropertyFlagged<T1>(
            ref T1 storage, 
            [CanBeNull] T1 value, 
            [CanBeNull] string flag,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            return SetProperty(ref storage, value,
                () => { RaisePropertyChanged(flag); },
                propertyName);
        }

        /// <summary>
        /// Set the property value and raise optional flags
        /// </summary>
        /// <typeparam name="T1">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="flags">a list of property names to raise</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected virtual bool SetPropertyFlagged<T1>(
            ref T1 storage, 
            [CanBeNull] T1 value,
            [NotNull] IEnumerable<string> flags,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            return SetProperty(ref storage, value,
                () => { RaisePropertyChanged(flags); },
                propertyName);
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T1">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected virtual bool SetProperty<T1>(
            ref T1 storage, 
            [CanBeNull] T1 value, 
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
          if (EqualityComparer<T1>.Default.Equals(storage, value))
            return false;

          storage = value;
          this.RaisePropertyChanged(propertyName);

          return true;
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T1">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="onChanged">Action to perform if the value is changed</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners. This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected virtual bool SetProperty<T1>(
            ref T1 storage, 
            [CanBeNull] T1 value, 
            [CanBeNull] Action onChanged, 
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
          if (EqualityComparer<T1>.Default.Equals(storage, value))
            return false;

          storage = value;
          onChanged?.Invoke();
          this.RaisePropertyChanged(propertyName);

          return true;
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        protected new virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChanged?.Invoke((object) this, e);
        }

        /// <summary>Raises this object's PropertyChanged event.</summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.
        ///     This value is optional and can be provided automatically when invoked
        ///     from compilers that support
        ///     <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" />.
        /// </param>
        protected void RaisePropertyChanged([CanBeNull, CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Raises this object's PropertyChanged event.</summary>
        /// <param name="propertyNames">List of properties to be notified</param>
        protected void RaisePropertyChanged([NotNull] IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames.Where(item => !string.IsNullOrEmpty(item)))
                RaisePropertyChanged(propertyName);
        }
    }
}
