// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewFilterDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ListViewFilterDescriptorCollection : FilterDescriptorCollection
  {
    protected override void InsertItem(int index, FilterDescriptor item)
    {
      base.InsertItem(index, item);
      item.PropertyChanged += new PropertyChangedEventHandler(this.Item_PropertyChanged);
    }

    protected override void SetItem(int index, FilterDescriptor item)
    {
      this[index].PropertyChanged -= new PropertyChangedEventHandler(this.Item_PropertyChanged);
      base.SetItem(index, item);
      item.PropertyChanged += new PropertyChangedEventHandler(this.Item_PropertyChanged);
    }

    protected override void ClearItems()
    {
      for (int index = 0; index < this.Count; ++index)
        this[index].PropertyChanged -= new PropertyChangedEventHandler(this.Item_PropertyChanged);
      base.ClearItems();
    }

    protected override void RemoveItem(int index)
    {
      this[index].PropertyChanged -= new PropertyChangedEventHandler(this.Item_PropertyChanged);
      base.RemoveItem(index);
    }

    private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      FilterDescriptor filterDescriptor = sender as FilterDescriptor;
      if (filterDescriptor == null)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) filterDescriptor));
    }
  }
}
