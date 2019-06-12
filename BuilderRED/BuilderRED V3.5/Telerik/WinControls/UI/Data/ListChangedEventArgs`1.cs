// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.ListChangedEventArgs`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Telerik.WinControls.UI.Data
{
  public class ListChangedEventArgs<T> : EventArgs
  {
    private int newIndex = -1;
    private string propertyName = string.Empty;
    private ListChangedType listChangeType;
    private IList<T> newItems;
    private IList<T> oldItems;

    public ListChangedEventArgs(ListChangedType listChangedType)
    {
      this.listChangeType = listChangedType;
    }

    public ListChangedEventArgs(ListChangedType listChangedType, T newItem, int newIndex)
    {
      this.listChangeType = listChangedType;
      this.newItems = (IList<T>) new ReadOnlyCollection<T>((IList<T>) new T[1]
      {
        newItem
      });
      this.newIndex = newIndex;
    }

    public ListChangedEventArgs(
      ListChangedType listChangedType,
      T changedItem,
      string propertyName)
    {
      this.listChangeType = listChangedType;
      this.oldItems = (IList<T>) new ReadOnlyCollection<T>((IList<T>) new T[1]
      {
        changedItem
      });
      this.propertyName = propertyName;
    }

    public ListChangedEventArgs(ListChangedType listChangedType, T newItem, T oldItem)
      : this(listChangedType, newItem, -1)
    {
      this.oldItems = (IList<T>) new ReadOnlyCollection<T>((IList<T>) new T[1]
      {
        oldItem
      });
    }

    public ListChangedEventArgs(ListChangedType listChangedType, IList<T> newItems)
    {
      this.listChangeType = listChangedType;
      this.newItems = (IList<T>) new ReadOnlyCollection<T>(newItems);
    }

    public ListChangedEventArgs(
      ListChangedType listChangedType,
      IList<T> changedItems,
      string propertyName)
    {
      this.listChangeType = listChangedType;
      this.oldItems = (IList<T>) new ReadOnlyCollection<T>(changedItems);
      this.propertyName = propertyName;
    }

    public ListChangedEventArgs(
      ListChangedType listChangedType,
      IList<T> newItems,
      IList<T> oldItems)
      : this(listChangedType, newItems)
    {
      this.oldItems = (IList<T>) new ReadOnlyCollection<T>(oldItems);
    }

    public ListChangedType ListChangedType
    {
      get
      {
        return this.listChangeType;
      }
    }

    public int NewIndex
    {
      get
      {
        return this.newIndex;
      }
    }

    public IList<T> NewItems
    {
      get
      {
        return this.newItems;
      }
    }

    public IList<T> OldItems
    {
      get
      {
        return this.oldItems;
      }
    }

    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
    }
  }
}
