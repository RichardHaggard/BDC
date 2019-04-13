﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BDC_V1.Interfaces;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public abstract class PropertyBase : BindableBase, INotifyPropertyChanged
    {
        // **************** Class members *************************************************** //

        protected virtual bool SetPropertyFlagged<T>(ref T storage, 
            [CanBeNull] T value, 
            [CanBeNull] string flag,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (!base.SetProperty(ref storage, value, propertyName)) 
                return false;

            if (! string.IsNullOrEmpty(flag)) 
                RaisePropertyChanged(flag);

            return true;
        }

        protected virtual bool SetPropertyFlagged<T>(ref T storage, 
            [CanBeNull] T value,
            [NotNull] IEnumerable<string> flags,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (!base.SetProperty(ref storage, value, propertyName)) 
                return false;

            RaisePropertyChanged(flags.Where(flag => ! string.IsNullOrEmpty(flag)));
            return true;
        }

        protected virtual bool SetPropertyFlagged<T>(ref T storage, 
            [CanBeNull] T value, 
            [CanBeNull] string flag,
            [CanBeNull] Action onChanged,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (base.SetProperty(ref storage, value, onChanged, propertyName))
                return false;

            if (! string.IsNullOrEmpty(flag)) 
                RaisePropertyChanged(flag);

            return true;
        }

        protected virtual bool SetPropertyFlagged<T>(ref T storage,
            [CanBeNull] T value, 
            [NotNull] IEnumerable<string> flags, 
            [CanBeNull] Action onChanged,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (!base.SetProperty(ref storage, value, onChanged, propertyName)) 
                return false;

            RaisePropertyChanged(flags
                .Where(flag => ! string.IsNullOrEmpty(flag)));

            return true;
        }

        protected void RaisePropertyChanged(IEnumerable<string> propertyNames)
        {
            foreach (var propertyName in propertyNames)
                RaisePropertyChanged(propertyName);
        }
    }
}
