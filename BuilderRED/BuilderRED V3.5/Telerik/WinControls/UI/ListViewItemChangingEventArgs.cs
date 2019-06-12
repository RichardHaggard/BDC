// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewItemChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ListViewItemChangingEventArgs : CancelEventArgs
  {
    private ListViewDataItem oldItem;
    private ListViewDataItem newItem;

    public ListViewItemChangingEventArgs(ListViewDataItem oldItem, ListViewDataItem newItem)
    {
      this.oldItem = oldItem;
      this.newItem = newItem;
      this.Cancel = false;
    }

    public ListViewDataItem OldItem
    {
      get
      {
        return this.oldItem;
      }
    }

    public ListViewDataItem NewItem
    {
      get
      {
        return this.newItem;
      }
    }
  }
}
