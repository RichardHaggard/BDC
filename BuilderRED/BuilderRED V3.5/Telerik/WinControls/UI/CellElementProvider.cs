// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CellElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class CellElementProvider : BaseVirtualizedElementProvider<GridViewColumn>
  {
    private GridTableElement rowView;
    private List<CellElementProvider.CellElementInfo> cachedElements;

    public CellElementProvider(GridTableElement rowView)
    {
      this.rowView = rowView;
      this.cachedElements = new List<CellElementProvider.CellElementInfo>();
    }

    public override int CachedElementsCount
    {
      get
      {
        return this.cachedElements.Count;
      }
    }

    public override IVirtualizedElement<GridViewColumn> GetElement(
      GridViewColumn data,
      object context)
    {
      foreach (CellElementProvider.CellElementInfo cachedElement in this.cachedElements)
      {
        if (this.IsCompatible(cachedElement, data, context))
        {
          this.cachedElements.Remove(cachedElement);
          this.PreInitializeCachedElement(cachedElement.Element, context);
          return cachedElement.Element;
        }
      }
      return this.CreateElement(data, context);
    }

    public override IVirtualizedElement<GridViewColumn> CreateElement(
      GridViewColumn data,
      object context)
    {
      return ((GridRowElement) context).CreateCell(data) as IVirtualizedElement<GridViewColumn>;
    }

    public override bool CacheElement(IVirtualizedElement<GridViewColumn> element)
    {
      GridCellElement gridCellElement = element as GridCellElement;
      if (gridCellElement.RowInfo != null)
        this.cachedElements.Add(new CellElementProvider.CellElementInfo(element, gridCellElement.RowInfo.GetType()));
      else
        (element as IDisposable)?.Dispose();
      return true;
    }

    public override void ClearCache()
    {
      for (int index = this.cachedElements.Count - 1; index >= 0; --index)
      {
        (this.cachedElements[index].Element as IDisposable)?.Dispose();
        this.cachedElements.RemoveAt(index);
      }
      base.ClearCache();
    }

    private bool IsCompatible(
      CellElementProvider.CellElementInfo info,
      GridViewColumn data,
      object context)
    {
      bool flag = true;
      GridRowElement gridRowElement = context as GridRowElement;
      if (gridRowElement != null)
        flag = (object) gridRowElement.RowInfo.GetType() == (object) info.RowInfoType;
      if (flag)
        return this.IsCompatible(info.Element, data, context);
      return false;
    }

    public override bool IsCompatible(
      IVirtualizedElement<GridViewColumn> element,
      GridViewColumn data,
      object context)
    {
      Type cellType = ((GridRowElement) context).GetCellType(data);
      Type type = element.GetType();
      if ((object) cellType != null && cellType.IsAssignableFrom(type))
        return element.IsCompatible(data, context);
      return false;
    }

    public override SizeF GetElementSize(GridViewColumn item)
    {
      float width = (float) item.Width;
      if (item is GridViewRowHeaderColumn)
        width = (float) this.rowView.RowHeaderColumnWidth;
      else if (item is GridViewIndentColumn)
        width = (float) this.rowView.GroupIndent;
      if ((double) width < 0.0)
        width = this.DefaultElementSize.Width;
      if (item is GridViewGroupColumn)
      {
        ColumnGroupRowLayout rowLayout = this.rowView.ViewElement.RowLayout as ColumnGroupRowLayout;
        if (rowLayout != null)
        {
          ColumnGroupsCellArrangeInfo columnData = rowLayout.GetColumnData(item);
          if (columnData != null)
            width = columnData.Bounds.Width;
        }
      }
      return new SizeF(width, 0.0f);
    }

    protected class CellElementInfo
    {
      private IVirtualizedElement<GridViewColumn> element;
      private Type rowType;

      public CellElementInfo(IVirtualizedElement<GridViewColumn> element, Type rowType)
      {
        this.element = element;
        this.rowType = rowType;
      }

      public IVirtualizedElement<GridViewColumn> Element
      {
        get
        {
          return this.element;
        }
      }

      public Type RowInfoType
      {
        get
        {
          return this.rowType;
        }
      }
    }
  }
}
