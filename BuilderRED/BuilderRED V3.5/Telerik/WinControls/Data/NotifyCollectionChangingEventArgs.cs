// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.NotifyCollectionChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.Data
{
  public class NotifyCollectionChangingEventArgs : CancelEventArgs
  {
    private NotifyCollectionChangedAction action;
    private IList newItems;
    private int newStartingIndex;
    private IList oldItems;
    private int oldStartingIndex;
    private PropertyChangingEventArgsEx propertyArgs;

    public NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction action)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      this.InitializeAdd(action, (IList) null, -1);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      IList changedItems)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      if (action == NotifyCollectionChangedAction.Reset)
      {
        this.InitializeAdd(action, (IList) null, -1);
      }
      else
      {
        if (changedItems == null)
          throw new ArgumentNullException(nameof (changedItems));
        this.InitializeAddOrRemove(action, changedItems, -1);
      }
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      object changedItem)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      if (action == NotifyCollectionChangedAction.Reset)
        this.InitializeAdd(action, (IList) null, -1);
      else
        this.InitializeAddOrRemove(action, (IList) new object[1]
        {
          changedItem
        }, -1);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      if (newItems == null)
        throw new ArgumentNullException(nameof (newItems));
      if (oldItems == null)
        throw new ArgumentNullException(nameof (oldItems));
      this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      IList changedItems,
      int startingIndex)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      if (action == NotifyCollectionChangedAction.Reset)
      {
        this.InitializeAdd(action, (IList) null, -1);
      }
      else
      {
        if (changedItems == null)
          throw new ArgumentNullException(nameof (changedItems));
        this.InitializeAddOrRemove(action, changedItems, startingIndex);
      }
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      object changedItem,
      int index)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      if (action == NotifyCollectionChangedAction.Reset)
        this.InitializeAdd(action, (IList) null, -1);
      else
        this.InitializeAddOrRemove(action, (IList) new object[1]
        {
          changedItem
        }, index);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      this.InitializeMoveOrReplace(action, (IList) new object[1]
      {
        newItem
      }, (IList) new object[1]{ oldItem }, -1, -1);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems,
      int startingIndex)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      if (newItems == null)
        throw new ArgumentNullException(nameof (newItems));
      if (oldItems == null)
        throw new ArgumentNullException(nameof (oldItems));
      this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      IList changedItems,
      int index,
      int oldIndex)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      object changedItem,
      int index,
      int oldIndex)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      object[] objArray = new object[1]{ changedItem };
      this.InitializeMoveOrReplace(action, (IList) objArray, (IList) objArray, index, oldIndex);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem,
      int index)
    {
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
      this.InitializeMoveOrReplace(action, (IList) new object[1]
      {
        newItem
      }, (IList) new object[1]{ oldItem }, index, index);
    }

    public NotifyCollectionChangingEventArgs(
      NotifyCollectionChangedAction action,
      object newItem,
      object oldItem,
      int index,
      PropertyChangingEventArgsEx propertyArgs)
      : this(action, newItem, oldItem, index)
    {
      this.propertyArgs = propertyArgs;
    }

    private void InitializeAdd(
      NotifyCollectionChangedAction action,
      IList newItems,
      int newStartingIndex)
    {
      this.action = action;
      object[] objArray = new object[0];
      if (newItems != null)
      {
        objArray = new object[newItems.Count];
        newItems.CopyTo((Array) objArray, 0);
      }
      this.newItems = (IList) objArray;
      this.newStartingIndex = newStartingIndex;
    }

    private void InitializeAddOrRemove(
      NotifyCollectionChangedAction action,
      IList changedItems,
      int startingIndex)
    {
      this.action = action;
      switch (action)
      {
        case NotifyCollectionChangedAction.Add:
        case NotifyCollectionChangedAction.Batch:
          this.InitializeAdd(action, changedItems, startingIndex);
          break;
        case NotifyCollectionChangedAction.Remove:
          this.InitializeRemove(action, changedItems, startingIndex);
          break;
      }
    }

    private void InitializeMoveOrReplace(
      NotifyCollectionChangedAction action,
      IList newItems,
      IList oldItems,
      int startingIndex,
      int oldStartingIndex)
    {
      this.InitializeAdd(action, newItems, startingIndex);
      this.InitializeRemove(action, oldItems, oldStartingIndex);
    }

    private void InitializeRemove(
      NotifyCollectionChangedAction action,
      IList oldItems,
      int oldStartingIndex)
    {
      this.action = action;
      object[] objArray = new object[0];
      if (oldItems != null)
      {
        objArray = new object[oldItems.Count];
        oldItems.CopyTo((Array) objArray, 0);
      }
      this.oldItems = (IList) objArray;
      this.oldStartingIndex = oldStartingIndex;
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

    public PropertyChangingEventArgsEx PropertyArgs
    {
      get
      {
        return this.propertyArgs;
      }
      protected set
      {
        this.propertyArgs = value;
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
