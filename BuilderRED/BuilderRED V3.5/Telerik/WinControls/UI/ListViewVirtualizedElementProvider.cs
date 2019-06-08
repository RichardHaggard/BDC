// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewVirtualizedElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ListViewVirtualizedElementProvider : VirtualizedPanelElementProvider<ListViewDataItem, BaseListViewVisualItem>
  {
    private BaseListViewElement owner;

    public ListViewVirtualizedElementProvider(BaseListViewElement owner)
    {
      this.owner = owner;
    }

    protected BaseListViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public override IVirtualizedElement<ListViewDataItem> CreateElement(
      ListViewDataItem data,
      object context)
    {
      Type type = this.owner.GetType();
      ListViewType listViewType = this.GetListViewType(type);
      if (data is ListViewDataItemGroup)
      {
        if ((object) type == (object) typeof (SimpleListViewElement) || type.IsSubclassOf(typeof (SimpleListViewElement)))
          return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new SimpleListViewGroupVisualItem(), listViewType, data);
        if ((object) type == (object) typeof (IconListViewElement) || type.IsSubclassOf(typeof (IconListViewElement)))
          return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new IconListViewGroupVisualItem(), listViewType, data);
        if ((object) type == (object) typeof (DetailListViewElement) || type.IsSubclassOf(typeof (DetailListViewElement)))
          return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new DetailListViewGroupVisualItem(), listViewType, data);
        return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new SimpleListViewGroupVisualItem(), listViewType, data);
      }
      if ((object) type == (object) typeof (SimpleListViewElement) || type.IsSubclassOf(typeof (SimpleListViewElement)))
        return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new SimpleListViewVisualItem(), listViewType, data);
      if ((object) type == (object) typeof (IconListViewElement) || type.IsSubclassOf(typeof (IconListViewElement)))
        return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new IconListViewVisualItem(), listViewType, data);
      if ((object) type == (object) typeof (DetailListViewElement) || type.IsSubclassOf(typeof (DetailListViewElement)))
        return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new DetailListViewVisualItem(), listViewType, data);
      return (IVirtualizedElement<ListViewDataItem>) this.OnElementCreating((BaseListViewVisualItem) new SimpleListViewVisualItem(), listViewType, data);
    }

    public override SizeF GetElementSize(ListViewDataItem item)
    {
      return (SizeF) item.ActualSize;
    }

    protected virtual BaseListViewVisualItem OnElementCreating(
      BaseListViewVisualItem item,
      ListViewType viewType,
      ListViewDataItem dataItem)
    {
      ListViewVisualItemCreatingEventArgs args = new ListViewVisualItemCreatingEventArgs(item, viewType, dataItem);
      this.owner.Owner.OnVisualItemCreating(args);
      return args.VisualItem;
    }

    private ListViewType GetListViewType(Type type)
    {
      if ((object) type == (object) typeof (SimpleListViewElement))
        return ListViewType.ListView;
      if ((object) type == (object) typeof (IconListViewElement))
        return ListViewType.IconsView;
      return (object) type == (object) typeof (DetailListViewElement) ? ListViewType.DetailsView : ListViewType.ListView;
    }
  }
}
