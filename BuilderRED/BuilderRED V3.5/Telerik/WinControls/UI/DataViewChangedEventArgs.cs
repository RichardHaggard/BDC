// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataViewChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class DataViewChangedEventArgs : EventArgs
  {
    private ViewChangedAction action;
    private IList newItems;
    private int newStartingIndex;
    private IList oldItems;
    private int oldStartingIndex;
    private string propertyName;

    public DataViewChangedEventArgs(ViewChangedAction action)
    {
      this.action = action;
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
    }

    public DataViewChangedEventArgs(ViewChangedAction action, IList changedItems)
      : this(action)
    {
      this.newItems = changedItems == null ? (IList) null : ArrayList.ReadOnly(changedItems);
    }

    public DataViewChangedEventArgs(ViewChangedAction action, object changedItem)
      : this(action, (IList) new object[1]
      {
        changedItem
      })
    {
    }

    public DataViewChangedEventArgs(ViewChangedAction action, IList newItems, IList oldItems)
      : this(action, newItems)
    {
      this.oldItems = oldItems == null ? (IList) null : ArrayList.ReadOnly(oldItems);
    }

    public DataViewChangedEventArgs(
      ViewChangedAction action,
      IList changedItems,
      int startingIndex)
      : this(action, changedItems)
    {
      this.newStartingIndex = startingIndex;
    }

    public DataViewChangedEventArgs(ViewChangedAction action, object changedItem, int index)
      : this(action, changedItem)
    {
      this.newStartingIndex = index;
    }

    public DataViewChangedEventArgs(ViewChangedAction action, object newItem, object oldItem)
      : this(action, (IList) new object[1]{ newItem }, (IList) new object[1]
      {
        oldItem
      })
    {
    }

    public DataViewChangedEventArgs(
      ViewChangedAction action,
      IList newItems,
      IList oldItems,
      int startingIndex)
      : this(action, newItems, oldItems)
    {
      this.newStartingIndex = startingIndex;
    }

    public DataViewChangedEventArgs(
      ViewChangedAction action,
      IList changedItems,
      int index,
      int oldIndex)
      : this(action, changedItems)
    {
      this.newStartingIndex = index;
      this.oldStartingIndex = oldIndex;
    }

    public DataViewChangedEventArgs(
      ViewChangedAction action,
      object changedItem,
      int index,
      int oldIndex)
      : this(action, (IList) new object[1]
      {
        changedItem
      }, (IList) new object[1]{ changedItem })
    {
      this.newStartingIndex = index;
      this.oldStartingIndex = oldIndex;
    }

    public DataViewChangedEventArgs(
      ViewChangedAction action,
      object newItem,
      object oldItem,
      int index)
      : this(action, (IList) new object[1]{ newItem }, (IList) new object[1]
      {
        oldItem
      })
    {
      this.newStartingIndex = index;
    }

    public DataViewChangedEventArgs(
      ViewChangedAction action,
      IList newItems,
      IList oldItems,
      int startingIndex,
      string propertyName)
      : this(action, newItems, oldItems, startingIndex)
    {
      this.propertyName = propertyName;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ViewChangedAction Action
    {
      get
      {
        return this.action;
      }
      internal set
      {
        this.action = value;
      }
    }

    public IList NewItems
    {
      get
      {
        return this.newItems;
      }
    }

    public int NewStartingIndex
    {
      get
      {
        return this.newStartingIndex;
      }
    }

    public IList OldItems
    {
      get
      {
        return this.oldItems;
      }
    }

    public int OldStartingIndex
    {
      get
      {
        return this.oldStartingIndex;
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
