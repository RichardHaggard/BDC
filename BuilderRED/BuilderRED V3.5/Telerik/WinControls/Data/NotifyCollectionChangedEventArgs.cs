// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.NotifyCollectionChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls.Data
{
  public class NotifyCollectionChangedEventArgs : EventArgs
  {
    private string propertyName = string.Empty;
    private NotifyCollectionChangedAction action;
    private CollectionResetReason resetReason;
    private IList newItems;
    private IList oldItems;
    private int newStartingIndex;
    private int oldStartingIndex;

    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
      protected internal set
      {
        this.propertyName = value;
      }
    }

    public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
    {
      this.action = action;
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      IList changedItems)
      : this(action)
    {
      this.newItems = changedItems == null ? (IList) null : ArrayList.ReadOnly(changedItems);
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      object changedItem)
      : this(action, (IList) new object[1]{ changedItem })
    {
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems)
      : this(action, newItems)
    {
      this.oldItems = oldItems == null ? (IList) null : ArrayList.ReadOnly(oldItems);
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      IList changedItems,
      int startingIndex)
      : this(action, changedItems)
    {
      this.newStartingIndex = startingIndex;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      object changedItem,
      int index)
      : this(action, changedItem)
    {
      this.newStartingIndex = index;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem)
      : this(action, (IList) new object[1]{ newItem }, (IList) new object[1]{ oldItem })
    {
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems,
      int startingIndex)
      : this(action, newItems, oldItems)
    {
      this.newStartingIndex = startingIndex;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      IList changedItems,
      int index,
      int oldIndex)
      : this(action, changedItems)
    {
      this.newStartingIndex = index;
      this.oldStartingIndex = oldIndex;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      object changedItem,
      int index,
      int oldIndex)
      : this(action, (IList) new object[1]{ changedItem }, (IList) new object[1]{ changedItem })
    {
      this.newStartingIndex = index;
      this.oldStartingIndex = oldIndex;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem,
      int index)
      : this(action, (IList) new object[1]{ newItem }, (IList) new object[1]{ oldItem })
    {
      this.newStartingIndex = index;
    }

    public NotifyCollectionChangedEventArgs(
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem,
      int index,
      string propertyName)
      : this(action, newItem, oldItem, index)
    {
      this.propertyName = propertyName;
    }

    public NotifyCollectionChangedAction Action
    {
      get
      {
        return this.action;
      }
      protected set
      {
        this.action = value;
      }
    }

    public CollectionResetReason ResetReason
    {
      get
      {
        return this.resetReason;
      }
      internal set
      {
        this.resetReason = value;
      }
    }

    public IList NewItems
    {
      get
      {
        return this.newItems;
      }
      protected set
      {
        this.newItems = value;
      }
    }

    public int NewStartingIndex
    {
      get
      {
        return this.newStartingIndex;
      }
      protected set
      {
        this.newStartingIndex = value;
      }
    }

    public IList OldItems
    {
      get
      {
        return this.oldItems;
      }
      protected set
      {
        this.oldItems = value;
      }
    }

    public int OldStartingIndex
    {
      get
      {
        return this.oldStartingIndex;
      }
      protected set
      {
        this.oldStartingIndex = value;
      }
    }
  }
}
