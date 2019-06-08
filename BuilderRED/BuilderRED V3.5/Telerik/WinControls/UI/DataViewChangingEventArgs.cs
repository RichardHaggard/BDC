// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataViewChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class DataViewChangingEventArgs : CancelEventArgs
  {
    private ViewChangedAction action;
    private IList newItems;
    private int newStartingIndex;
    private IList oldItems;
    private int oldStartingIndex;

    public DataViewChangingEventArgs(ViewChangedAction action)
    {
      this.action = action;
      this.newStartingIndex = -1;
      this.oldStartingIndex = -1;
    }

    public DataViewChangingEventArgs(ViewChangedAction action, IList changedItems)
      : this(action)
    {
      this.newItems = changedItems == null ? (IList) null : ArrayList.ReadOnly(changedItems);
    }

    public DataViewChangingEventArgs(ViewChangedAction action, object changedItem)
      : this(action, (IList) new object[1]
      {
        changedItem
      })
    {
    }

    public DataViewChangingEventArgs(ViewChangedAction action, IList newItems, IList oldItems)
      : this(action, newItems)
    {
      this.oldItems = oldItems == null ? (IList) null : ArrayList.ReadOnly(oldItems);
    }

    public DataViewChangingEventArgs(
      ViewChangedAction action,
      IList changedItems,
      int startingIndex)
      : this(action, changedItems)
    {
      this.newStartingIndex = startingIndex;
    }

    public DataViewChangingEventArgs(ViewChangedAction action, object changedItem, int index)
      : this(action, changedItem)
    {
      this.newStartingIndex = index;
    }

    public DataViewChangingEventArgs(ViewChangedAction action, object newItem, object oldItem)
      : this(action, (IList) new object[1]{ newItem }, (IList) new object[1]
      {
        oldItem
      })
    {
    }

    public DataViewChangingEventArgs(
      ViewChangedAction action,
      IList newItems,
      IList oldItems,
      int startingIndex)
      : this(action, newItems, oldItems)
    {
      this.newStartingIndex = startingIndex;
    }

    public DataViewChangingEventArgs(
      ViewChangedAction action,
      IList changedItems,
      int index,
      int oldIndex)
      : this(action, changedItems)
    {
      this.newStartingIndex = index;
      this.oldStartingIndex = oldIndex;
    }

    public DataViewChangingEventArgs(
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

    public DataViewChangingEventArgs(
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

    public ViewChangedAction Action
    {
      get
      {
        return this.action;
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
  }
}
