// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewListSource
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ListViewListSource : RadListSource<ListViewDataItem>
  {
    private RadListViewElement owner;

    public ListViewListSource(RadListViewElement owner)
      : base((IDataItemSource) owner)
    {
      this.owner = owner;
    }

    protected override void InitializeBoundRow(ListViewDataItem item, object dataBoundItem)
    {
      item.SetDataBoundItem(true, dataBoundItem);
    }
  }
}
