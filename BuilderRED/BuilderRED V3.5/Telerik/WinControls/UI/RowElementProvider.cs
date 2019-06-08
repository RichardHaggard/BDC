// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RowElementProvider : BaseVirtualizedElementProvider<GridViewRowInfo>
  {
    private GridTableElement rowView;

    public RowElementProvider(GridTableElement rowView)
    {
      this.rowView = rowView;
      this.DefaultElementSize = new SizeF(0.0f, 24f);
    }

    public override IVirtualizedElement<GridViewRowInfo> CreateElement(
      GridViewRowInfo data,
      object context)
    {
      GridViewCreateRowEventArgs e = new GridViewCreateRowEventArgs(data, data.RowElementType);
      this.rowView.GridViewElement.CallCreateRow(e);
      if (e.RowElement != null)
        return (IVirtualizedElement<GridViewRowInfo>) e.RowElement;
      if ((object) e.RowType != null)
        return (IVirtualizedElement<GridViewRowInfo>) Activator.CreateInstance(e.RowType);
      return (IVirtualizedElement<GridViewRowInfo>) null;
    }

    public override IVirtualizedElement<GridViewRowInfo> GetElement(
      GridViewRowInfo data,
      object context)
    {
      if (data.ViewTemplate.ColumnCount == 0)
        return (IVirtualizedElement<GridViewRowInfo>) null;
      IVirtualizedElement<GridViewRowInfo> element = base.GetElement(data, context);
      GridVirtualizedRowElement virtualizedRowElement = element as GridVirtualizedRowElement;
      if (virtualizedRowElement != null)
        virtualizedRowElement.ScrollableColumns.ScrollOffset = new SizeF((float) -this.rowView.ColumnScroller.ScrollOffset, 0.0f);
      return element;
    }

    public override SizeF GetElementSize(GridViewRowInfo item)
    {
      float num = (float) this.rowView.ViewElement.RowLayout.GetRowHeight(item);
      float width = this.rowView.ViewElement.RowLayout.DesiredSize.Width;
      if (item.MinHeight >= 0 && (double) num > 0.0 && (!(item is GridViewTableHeaderRowInfo) || !(item.ViewTemplate.ViewDefinition is ColumnGroupsViewDefinition)) && !(item is GridViewDetailsRowInfo))
        num = Math.Max(num, (float) item.MinHeight);
      if (item.MaxHeight > 0 && (double) num > 0.0 && (!(item is GridViewTableHeaderRowInfo) || !(item.ViewTemplate.ViewDefinition is ColumnGroupsViewDefinition)) && !(item is GridViewDetailsRowInfo))
        num = Math.Min(num, (float) item.MaxHeight);
      if (item is GridViewDetailsRowInfo)
        width = 0.0f;
      if (item is GridViewGroupRowInfo)
        width = this.rowView.ViewElement.RowLayout.GroupRowDesiredSize.Width;
      return new SizeF(width, num);
    }

    public int GetElementHeightByRowType(GridViewRowInfo item)
    {
      if (item is GridViewTableHeaderRowInfo)
        return this.rowView.TableHeaderHeight;
      if (item is GridViewFilteringRowInfo)
        return this.rowView.FilterRowHeight;
      if (item is GridViewDetailsRowInfo)
        return this.rowView.ChildRowHeight;
      if (item is GridViewGroupRowInfo)
        return this.rowView.GroupHeaderHeight;
      return this.rowView.RowHeight;
    }

    public override bool IsCompatible(
      IVirtualizedElement<GridViewRowInfo> element,
      GridViewRowInfo data,
      object context)
    {
      if (element != null && element.Data != null && (element.Data.IsCurrent && this.rowView.GridViewElement.IsInEditMode))
        return element.Data == data;
      return base.IsCompatible(element, data, context);
    }
  }
}
