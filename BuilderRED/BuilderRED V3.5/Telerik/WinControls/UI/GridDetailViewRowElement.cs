// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDetailViewRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridDetailViewRowElement : GridRowElement
  {
    private GridRowHeaderCellElement rowHeaderCell;
    private GridDetailViewCellElement contentCell;

    public override void Initialize(GridViewRowInfo rowInfo)
    {
      base.Initialize(rowInfo);
      if (this.Children.Count == 0)
      {
        this.contentCell = this.CreateCell((GridViewColumn) null) as GridDetailViewCellElement;
        this.Children.Add((RadElement) this.contentCell);
      }
      else
        this.contentCell.Initialize((GridViewColumn) null, (GridRowElement) this);
      if (this.ViewTemplate.ShowRowHeaderColumn)
      {
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.TableElement.ViewElement.RowLayout.RenderColumns)
        {
          if (renderColumn is GridViewRowHeaderColumn)
          {
            if (this.rowHeaderCell == null)
            {
              this.rowHeaderCell = (GridRowHeaderCellElement) this.TableElement.CellElementProvider.GetElement(renderColumn, (object) this);
              this.rowHeaderCell.ThemeRole = "DetailRowHeaderCell";
              this.Children.Insert(0, (RadElement) this.rowHeaderCell);
            }
            if (this.rowHeaderCell != null)
            {
              this.rowHeaderCell.Attach(renderColumn, (object) this);
              this.rowHeaderCell.Visibility = ElementVisibility.Visible;
              break;
            }
            break;
          }
        }
      }
      else if (this.rowHeaderCell != null)
        this.rowHeaderCell.Visibility = ElementVisibility.Collapsed;
      this.contentCell.SetContent();
      this.contentCell.UpdateInfo();
      GridViewRowInfo currentRow = this.ViewTemplate.MasterTemplate.CurrentRow;
      if (currentRow == null)
        return;
      foreach (GridViewInfo childViewInfo in (IEnumerable<GridViewInfo>) (this.RowInfo as GridViewDetailsRowInfo).ChildViewInfos)
      {
        if (currentRow.ViewInfo == childViewInfo)
        {
          this.GridViewElement.CurrentView = (IRowView) this.contentCell.ChildTableElement;
          break;
        }
      }
    }

    public override Type GetCellType(GridViewColumn column)
    {
      if (column is GridViewRowHeaderColumn)
        return typeof (GridRowHeaderCellElement);
      return typeof (GridDetailViewCellElement);
    }

    public override void Detach()
    {
      if (this.rowHeaderCell != null)
      {
        if (this.GridViewElement != null && this.contentCell.ChildTableElement == this.GridViewElement.CurrentView)
          this.GridViewElement.CurrentView = (IRowView) this.TableElement;
        this.rowHeaderCell.Detach();
      }
      this.contentCell.Detach();
      base.Detach();
    }

    public override bool IsCompatible(GridViewRowInfo data, object context)
    {
      GridViewDetailsRowInfo viewDetailsRowInfo = data as GridViewDetailsRowInfo;
      if (this.ViewInfo == null || viewDetailsRowInfo == null)
        return data is GridViewDetailsRowInfo;
      GridViewDetailsRowInfo rowInfo = this.RowInfo as GridViewDetailsRowInfo;
      return viewDetailsRowInfo.ChildViewInfo == rowInfo.ChildViewInfo;
    }

    public GridDetailViewCellElement ContentCell
    {
      get
      {
        return this.contentCell;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      SizeF availableSize1 = this.RestrictChildRowHeight(availableSize);
      foreach (GridCellElement child in this.Children)
      {
        child.Measure(availableSize1);
        GridDetailViewCellElement detailViewCellElement = child as GridDetailViewCellElement;
        if (detailViewCellElement != null && detailViewCellElement.ChildTableElement.ViewTemplate.BestFitQueue.Count > 0)
        {
          detailViewCellElement.ChildTableElement.BestFitHelper.ProcessRequests();
          child.Measure(availableSize1);
        }
        if ((double) availableSize1.Width != double.PositiveInfinity)
          availableSize1.Width -= child.DesiredSize.Width + (float) this.TableElement.CellSpacing;
        empty.Width += child.DesiredSize.Width + (float) this.TableElement.CellSpacing;
        if (child == detailViewCellElement)
          empty.Height = Math.Max(child.DesiredSize.Height, empty.Height);
      }
      empty.Height = Math.Min(availableSize.Height, empty.Height);
      this.UpdateChildRowHeight((int) empty.Height, availableSize);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float num = 0.0f;
      float x = clientRectangle.Left;
      if (this.rowHeaderCell != null && this.rowHeaderCell.Visibility == ElementVisibility.Visible)
      {
        SizeF elementSize = this.TableElement.ColumnScroller.ElementProvider.GetElementSize(this.rowHeaderCell.ColumnInfo);
        if (this.RightToLeft)
          x = clientRectangle.Right - elementSize.Width;
        this.rowHeaderCell.Arrange(new RectangleF(x, clientRectangle.Top, elementSize.Width, clientRectangle.Height));
        num = elementSize.Width;
      }
      this.contentCell.Arrange(new RectangleF(!this.RightToLeft ? clientRectangle.Left + num + (float) this.TableElement.CellSpacing : clientRectangle.Left, clientRectangle.Top, clientRectangle.Width - num, clientRectangle.Height));
      return finalSize;
    }

    private SizeF RestrictChildRowHeight(SizeF availableSize)
    {
      SizeF sizeF = availableSize;
      if (this.GridViewElement.UseScrollbarsInHierarchy)
      {
        int num = this.Data.Height;
        if (num == -1)
          num = this.TableElement.ChildRowHeight;
        sizeF.Height = Math.Min((float) num, sizeF.Height);
      }
      return sizeF;
    }

    private void UpdateChildRowHeight(int newHeight, SizeF availableSize)
    {
      GridViewDetailsRowInfo data = (GridViewDetailsRowInfo) this.Data;
      if (data.ActualHeight == newHeight)
        return;
      int num1 = data.ActualHeight == -1 ? 0 : data.ActualHeight;
      int num2 = newHeight - num1;
      if (num1 != 0 && !data.resetActualHeight)
      {
        GridTraverser gridTraverser = new GridTraverser((GridTraverser) this.TableElement.RowScroller.Traverser);
        gridTraverser.ProcessHierarchy = false;
        do
          ;
        while (gridTraverser.Current != data && gridTraverser.MoveNext());
        if (!gridTraverser.MoveNext())
          return;
      }
      data.resetActualHeight = false;
      data.ActualHeight = newHeight;
      if (!this.GridViewElement.UseScrollbarsInHierarchy && this.TableElement.ViewInfo.ParentRow != null)
        return;
      RowScroller rowScroller = this.TableElement.RowScroller;
      if (num1 == 0)
        rowScroller.UpdateScrollRange();
      else
        rowScroller.UpdateScrollRange(rowScroller.Scrollbar.Maximum + num2, false);
    }

    public override void UpdateInfo()
    {
      base.UpdateInfo();
      this.GridViewElement.CallViewRowFormatting((object) this, new RowFormattingEventArgs((GridRowElement) this));
    }
  }
}
