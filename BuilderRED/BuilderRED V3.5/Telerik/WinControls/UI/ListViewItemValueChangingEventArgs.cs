// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewItemValueChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class ListViewItemValueChangingEventArgs : ValueChangingEventArgs
  {
    private ListViewDataItem item;

    public ListViewItemValueChangingEventArgs(
      ListViewDataItem item,
      object newValue,
      object oldValue)
      : base(newValue, oldValue)
    {
      this.item = item;
    }

    public ListViewDataItem Item
    {
      get
      {
        return this.item;
      }
    }

    public RadListViewElement ListViewElement
    {
      get
      {
        return this.Item.Owner;
      }
    }
  }
}
