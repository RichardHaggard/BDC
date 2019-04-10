﻿using System;
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
            [NotNull] string[] flags,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (!base.SetProperty(ref storage, value, propertyName)) 
                return false;

            foreach (var flag in flags.Where(flag => ! string.IsNullOrEmpty(flag)))
                RaisePropertyChanged(flag);

            return true;
        }

        protected virtual bool SetPropertyFlagged<T>(ref T storage, 
            [CanBeNull] T value, 
            [CanBeNull] string flag,
            [CanBeNull] Action onChanged,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (!base.SetProperty(ref storage, value, onChanged, propertyName)) 
                return false;

            if (! string.IsNullOrEmpty(flag)) 
                RaisePropertyChanged(flag);

            return true;
        }

        protected virtual bool SetPropertyFlagged<T>(ref T storage,
            [CanBeNull] T value, 
            [NotNull] string[] flags, 
            [CanBeNull] Action onChanged,
            [CanBeNull, CallerMemberName] string propertyName = null)
        {
            if (!base.SetProperty(ref storage, value, onChanged, propertyName)) 
                return false;

            foreach (var flag in flags.Where(flag => ! string.IsNullOrEmpty(flag)))
                RaisePropertyChanged(flag);

            return true;
        }

        protected INotifyingCollection<T> PropertyCollection<T>(
            [NotNull] ref INotifyingCollection<T> collection,
            [CanBeNull] Action onChanged = null) 
        {
            if (collection == null)
            {
                collection = new NotifyingCollection<T>();
                onChanged?.Invoke();
            }

            return collection ?? (collection = new NotifyingCollection<T>());
        }

        protected INotifyingCollection<T> PropertyCollection<T>(
            [NotNull] ref INotifyingCollection<T> collection, 
            [CanBeNull] string propertyName,
            [CanBeNull] Action onChanged = null) 
        {
            if (collection == null)
            {
                collection = new NotifyingCollection<T>();
                if (!string.IsNullOrEmpty(propertyName))
                    collection.CollectionChanged += (o, i) => RaisePropertyChanged(propertyName);

                RaisePropertyChanged(propertyName);
                onChanged?.Invoke();
            }

            return collection;
        }

        protected INotifyingCollection<T> PropertyCollection<T>(
            [NotNull] ref INotifyingCollection<T> collection, 
            [NotNull] string[] propertyNames,
            [CanBeNull] Action onChanged = null) 
        {
            if (collection == null)
            {
                collection = new NotifyingCollection<T>();

                var items = propertyNames.Where(item => !string.IsNullOrEmpty(item)).ToArray();
                if (items.Any())
                {
                    collection.CollectionChanged += (o, i) =>
                    {
                        foreach (var propertyName in items)
                            RaisePropertyChanged(propertyName);
                    };

                    foreach (var propertyName in items)
                        RaisePropertyChanged(propertyName);
                }

                onChanged?.Invoke();
            }

            return collection;
        }
    }
}
