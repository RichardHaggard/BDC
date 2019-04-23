using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    /// <inheritdoc cref="PropertyBase" />
    public class PropertyBaseHelper : PropertyBase
    {
        private readonly IPropertyBaseHelper _parent;

        public PropertyBaseHelper([NotNull] IPropertyBaseHelper parent)
        {
            _parent = parent;
        }

        // send all notifications to the parent
        protected override void OnPropertyChanged(PropertyChangedEventArgs args) => 
            _parent.OnPropertyChanged(args);

        #region BindableBase

        public new bool SetProperty<T>(
            ref T storage, 
            [CanBeNull] T value, 
            [NotNull]   string propertyName)
        {
            return base.SetProperty(ref storage, value, propertyName);
        }

        public bool SetProperty<T>(
            ref T storage, 
            [CanBeNull] T value, 
            [NotNull]   IEnumerable<string> propertyNames)
        {
            // ReSharper disable PossibleMultipleEnumeration
            if (! base.SetProperty(ref storage, value, propertyNames.FirstOrDefault()))
                return false;

            base.RaisePropertyChanged(propertyNames.Skip(1));
            return true;
            // ReSharper restore PossibleMultipleEnumeration
        }

        public bool SetProperty<T>(
            ref T storage, 
            [CanBeNull] T value, 
            [NotNull]   string propertyName, 
            [NotNull]   Action onChanged)
        {
            return base.SetProperty(ref storage, value, onChanged, propertyName);
        }

        public bool SetProperty<T>(
            ref T storage, 
            [CanBeNull] T value, 
            [NotNull]   IEnumerable<string> propertyNames, 
            [NotNull]   Action onChanged)
        {
            // ReSharper disable PossibleMultipleEnumeration
            if (! base.SetProperty(ref storage, value, onChanged, propertyNames.FirstOrDefault()))
                return false;

            base.RaisePropertyChanged(propertyNames.Skip(1));
            return true;
            // ReSharper restore PossibleMultipleEnumeration
        }

        /// <inheritdoc cref="PropertyBase" />
        public new void RaisePropertyChanged([NotNull] string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
        }

#endregion

#region PropertBase       

        /// <inheritdoc cref="PropertyBase" />
        public new void RaisePropertyChanged([NotNull] IEnumerable<string> propertyNames)
        {
            base.RaisePropertyChanged(propertyNames);
        }

#endregion
    }
}
