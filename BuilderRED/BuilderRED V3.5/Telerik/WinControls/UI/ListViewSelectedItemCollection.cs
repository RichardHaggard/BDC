// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewSelectedItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class ListViewSelectedItemCollection : ReadOnlyCollection<ListViewDataItem>
  {
    private RadListViewElement listViewElement;

    public ListViewSelectedItemCollection(RadListViewElement listView)
      : base((IList<ListViewDataItem>) new List<ListViewDataItem>())
    {
      this.listViewElement = listView;
    }

    internal void ProcessSelectedItem(ListViewDataItem listViewItem)
    {
      if (listViewItem.Selected && listViewItem.Owner == this.listViewElement)
      {
        if (this.Items.Contains(listViewItem))
          return;
        this.Items.Add(listViewItem);
        this.listViewElement.OnSelectedItemsChanged();
      }
      else
      {
        if (this.Items.Remove(listViewItem))
          this.listViewElement.OnSelectedItemsChanged();
        if (this.listViewElement.SelectedItem != listViewItem)
          return;
        ListViewDataItem listViewDataItem = (ListViewDataItem) null;
        if (this.Count > 0)
          listViewDataItem = this[this.Count - 1];
        this.listViewElement.SetSelectedItem(listViewDataItem);
      }
    }

    public void Clear()
    {
      if (this.Items.Count == 0)
        return;
      while (this.Items.Count > 0)
      {
        ListViewDataItem listViewItem = this.Items[0];
        if (!listViewItem.Selected)
          this.ProcessSelectedItem(listViewItem);
        else
          listViewItem.Selected = false;
      }
    }

    internal void Reset()
    {
      this.Items.Clear();
    }
  }
}
