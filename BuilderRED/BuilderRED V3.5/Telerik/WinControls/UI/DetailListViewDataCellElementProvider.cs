// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewDataCellElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DetailListViewDataCellElementProvider : BaseVirtualizedElementProvider<ListViewDetailColumn>
  {
    private DetailListViewVisualItem owner;

    public DetailListViewDataCellElementProvider(DetailListViewVisualItem owner)
    {
      this.owner = owner;
    }

    public override IVirtualizedElement<ListViewDetailColumn> CreateElement(
      ListViewDetailColumn data,
      object context)
    {
      ListViewCellElementCreatingEventArgs args = new ListViewCellElementCreatingEventArgs((DetailListViewCellElement) new DetailListViewDataCellElement(this.owner, data));
      if (data != null && data.Owner != null)
        data.Owner.OnCellCreating(args);
      return (IVirtualizedElement<ListViewDetailColumn>) args.CellElement;
    }

    public override SizeF GetElementSize(ListViewDetailColumn item)
    {
      return new SizeF(item.Width, 50f);
    }
  }
}
