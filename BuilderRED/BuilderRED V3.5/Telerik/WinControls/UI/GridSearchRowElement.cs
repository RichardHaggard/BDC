// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSearchRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridSearchRowElement : GridRowElement
  {
    private GridSearchCellElement searchCellElement;

    public override void Initialize(GridViewRowInfo rowInfo)
    {
      if (this.Children.Count == 0)
      {
        this.RowInfo = rowInfo;
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.TableElement.ViewElement.RowLayout.RenderColumns)
        {
          if (renderColumn is GridViewRowHeaderColumn)
          {
            GridCellElement cell = this.CreateCell(renderColumn);
            if (cell != null)
            {
              if (cell is GridRowHeaderCellElement)
                cell.ThemeRole = "GridRowHeaderCellElement";
              this.Children.Add((RadElement) cell);
            }
            else
              break;
          }
          if (renderColumn is GridViewDataColumn)
            break;
        }
        this.searchCellElement = (GridSearchCellElement) this.CreateCell((GridViewColumn) null);
        this.Children.Add((RadElement) this.searchCellElement);
        this.RowInfo = (GridViewRowInfo) null;
      }
      base.Initialize(rowInfo);
      int index = 0;
      for (IList<GridViewColumn> renderColumns = this.TableElement.ViewElement.RowLayout.RenderColumns; index < this.Children.Count && index < renderColumns.Count; ++index)
      {
        GridCellElement child = this.Children[index] as GridCellElement;
        GridViewColumn column = renderColumns[index];
        if (child != null)
        {
          if (child is GridSearchCellElement)
            column = (GridViewColumn) null;
          child.Initialize(column, (GridRowElement) this);
        }
      }
    }

    public override bool CanApplyFormatting
    {
      get
      {
        return false;
      }
    }

    public GridSearchCellElement SearchCellElement
    {
      get
      {
        return this.searchCellElement;
      }
    }

    public override bool IsCompatible(GridViewRowInfo data, object context)
    {
      return data is GridViewSearchRowInfo;
    }

    public override void Detach()
    {
      foreach (GridCellElement child in this.Children)
        (child as GridVirtualizedCellElement)?.Detach();
      base.Detach();
    }

    public override Type GetCellType(GridViewColumn column)
    {
      GridViewSearchRowInfo rowInfo = (GridViewSearchRowInfo) this.RowInfo;
      if (column == null)
        return typeof (GridSearchCellElement);
      return base.GetCellType(column);
    }

    public override void UpdateContent()
    {
      foreach (GridCellElement child in this.Children)
        child.SetContent();
    }

    protected override bool ShouldUsePaintBuffer()
    {
      return false;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.TableElement.ViewElement.RowLayout.MeasureRow(availableSize);
      SizeF elementSize = this.TableElement.RowScroller.ElementProvider.GetElementSize(this.RowInfo);
      if ((double) elementSize.Width != 0.0)
        availableSize.Width = Math.Min(availableSize.Width, elementSize.Width);
      if (!float.IsInfinity(availableSize.Height))
        availableSize.Height = elementSize.Height;
      this.Layout.Measure(this.GetClientRectangle(availableSize).Size);
      foreach (GridCellElement child in this.Children)
      {
        if (child != this.SearchCellElement)
        {
          child.Measure(availableSize);
          availableSize.Width -= child.DesiredSize.Width + (float) this.TableElement.CellSpacing;
        }
      }
      if (this.SearchCellElement != null)
        this.SearchCellElement.Measure(availableSize);
      if (float.IsInfinity(availableSize.Height))
      {
        int num = this.RowInfo.Height == -1 ? (int) elementSize.Height : this.RowInfo.Height;
        elementSize.Height = Math.Max(this.SearchCellElement.DesiredSize.Height, (float) this.RowInfo.MinHeight);
        this.RowInfo.SuspendPropertyNotifications();
        this.RowInfo.Height = (int) elementSize.Height;
        this.RowInfo.ResumePropertyNotifications();
        this.TableElement.RowScroller.UpdateScrollRange(this.TableElement.RowScroller.Scrollbar.Maximum + (this.RowInfo.Height - num), false);
      }
      return elementSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float num1 = 0.0f;
      foreach (GridCellElement child in this.Children)
      {
        if (child != this.SearchCellElement)
        {
          float y = 0.0f;
          float width1 = this.TableElement.ColumnScroller.ElementProvider.GetElementSize(child.ColumnInfo).Width;
          float height = finalSize.Height;
          float width2 = finalSize.Width;
          child.Arrange(new RectangleF(this.RightToLeft ? width2 - num1 - width1 : num1, y, width1, height));
          num1 += width1 + (float) this.TableElement.CellSpacing;
        }
      }
      if (this.SearchCellElement != null)
      {
        float width = clientRectangle.Width - num1;
        float num2 = clientRectangle.Left;
        float y = clientRectangle.Top;
        float height = clientRectangle.Height;
        if (this.SearchCellElement.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
        {
          width = finalSize.Width - num1;
          num2 = 0.0f;
          y = 0.0f;
          height = finalSize.Height;
        }
        this.SearchCellElement.Arrange(new RectangleF(this.RightToLeft ? num2 : num1, y, width, height));
      }
      return finalSize;
    }
  }
}
