// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewCheckedItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class ListViewCheckedItemCollection : ReadOnlyCollection<ListViewDataItem>
  {
    private RadListViewElement listView;

    public ListViewCheckedItemCollection(RadListViewElement listView)
      : base((IList<ListViewDataItem>) new List<ListViewDataItem>())
    {
      this.listView = listView;
    }

    internal void ProcessCheckedItem(ListViewDataItem listViewItem)
    {
      if (listViewItem.Owner != this.listView)
        this.Items.Remove(listViewItem);
      else if (listViewItem.CheckState == ToggleState.On)
      {
        if (this.Items.Contains(listViewItem))
          return;
        this.Items.Add(listViewItem);
      }
      else
      {
        if (!this.Items.Contains(listViewItem))
          return;
        this.Items.Remove(listViewItem);
      }
    }

    public void Clear()
    {
      if (this.Items.Count == 0)
        return;
      while (this.Items.Count > 0)
        this.Items[this.Items.Count - 1].CheckState = ToggleState.Off;
    }

    internal void Reset()
    {
      this.Items.Clear();
    }
  }
}
