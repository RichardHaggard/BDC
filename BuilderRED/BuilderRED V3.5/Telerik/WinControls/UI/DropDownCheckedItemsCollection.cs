// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DropDownCheckedItemsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class DropDownCheckedItemsCollection : ReadOnlyCollection<RadCheckedListDataItem>
  {
    private RadCheckedDropDownListElement dropDownElement;

    public DropDownCheckedItemsCollection(RadCheckedDropDownListElement owner)
      : base((IList<RadCheckedListDataItem>) new List<RadCheckedListDataItem>())
    {
      this.dropDownElement = owner;
    }

    internal void ProcessCheckedItem(RadCheckedListDataItem dataItem)
    {
      if (dataItem is CheckAllDataItem)
        return;
      if (dataItem.Owner != this.dropDownElement.ListElement)
        this.Items.Remove(dataItem);
      else if (dataItem.Checked)
      {
        if (this.Items.Contains(dataItem))
          return;
        this.Items.Add(dataItem);
        if (this.Items.Count != this.dropDownElement.Items.Count)
          return;
        this.dropDownElement.CheckAllItem.SetCheckState(true, true);
      }
      else
      {
        this.dropDownElement.CheckAllItem.SetCheckState(false, true);
        if (!this.Items.Contains(dataItem))
          return;
        this.Items.Remove(dataItem);
      }
    }

    public virtual void Clear()
    {
      if (this.Items.Count == 0)
        return;
      for (int index = this.Items.Count - 1; index >= 0; --index)
        this.Items[index].Checked = false;
      this.Items.Clear();
    }

    internal void Reset()
    {
      this.Items.Clear();
    }
  }
}
