// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewVirtualizedElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CardViewVirtualizedElementProvider : ListViewVirtualizedElementProvider
  {
    public CardViewVirtualizedElementProvider(BaseListViewElement owner)
      : base(owner)
    {
    }

    public override IVirtualizedElement<ListViewDataItem> CreateElement(
      ListViewDataItem data,
      object context)
    {
      if (data is ListViewDataItemGroup)
        return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new IconListViewGroupVisualItem(), ListViewType.IconsView, data);
      return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new CardListViewVisualItem(), ListViewType.IconsView, data);
    }
  }
}
