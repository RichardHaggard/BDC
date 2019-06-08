// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowsContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RowsContainerElement : LayoutPanel
  {
    private PinnedRowsContainerElement topPinnedRows;
    private PinnedRowsContainerElement bottomPinnedRows;
    private ScrollableRowsContainerElement scrollableRows;
    private IGridRowLayout rowLayout;
    private int elementSpacing;
    private int oldHScrollValue;
    private int oldVScrollValue;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.elementSpacing = -1;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.ClipDrawing = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      ChildrenChangedEventHandler changedEventHandler = new ChildrenChangedEventHandler(this.rows_ChildrenChanged);
      this.topPinnedRows = new PinnedRowsContainerElement();
      this.topPinnedRows.ZIndex = 10;
      this.topPinnedRows.ChildrenChanged += changedEventHandler;
      this.Children.Add((RadElement) this.topPinnedRows);
      this.scrollableRows = new ScrollableRowsContainerElement();
      this.scrollableRows.ChildrenChanged += changedEventHandler;
      this.Children.Add((RadElement) this.scrollableRows);
      this.bottomPinnedRows = new PinnedRowsContainerElement();
      this.bottomPinnedRows.ZIndex = 5;
      this.bottomPinnedRows.ChildrenChanged += changedEventHandler;
      this.Children.Add((RadElement) this.bottomPinnedRows);
    }

    protected override void DisposeManagedResources()
    {
      if (this.rowLayout != null)
      {
        this.rowLayout.Dispose();
        this.rowLayout = (IGridRowLayout) null;
      }
      base.DisposeManagedResources();
    }

    public PinnedRowsContainerElement TopPinnedRows
    {
      get
      {
        return this.topPinnedRows;
      }
    }

    public PinnedRowsContainerElement BottomPinnedRows
    {
      get
      {
        return this.bottomPinnedRows;
      }
    }

    public ScrollableRowsContainerElement ScrollableRows
    {
      get
      {
        return this.scrollableRows;
      }
    }

    public GridTableElement TableElement
    {
      get
      {
        return this.Parent as GridTableElement;
      }
    }

    public IGridRowLayout RowLayout
    {
      get
      {
        if (this.rowLayout == null)
          this.rowLayout = (IGridRowLayout) new TableViewRowLayout();
        return this.rowLayout;
      }
      set
      {
        if (this.rowLayout == value || value == null)
          return;
        if (this.rowLayout != null)
          this.rowLayout.Dispose();
        this.rowLayout = value;
        this.ClearRows();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (RowLayout)));
      }
    }

    public int ElementSpacing
    {
      get
      {
        return this.elementSpacing;
      }
      set
      {
        if (this.elementSpacing == value)
          return;
        this.elementSpacing = value;
        this.InvalidateMeasure();
      }
    }

    public void UpdateRows()
    {
      this.UpdateRows(false);
    }

    public void UpdateRows(bool updateCells)
    {
      if (updateCells)
      {
        this.RowLayout.InvalidateLayout();
        foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.TableElement.VisualRows)
        {
          GridVirtualizedRowElement virtualizedRowElement = visualRow as GridVirtualizedRowElement;
          if (virtualizedRowElement != null)
          {
            virtualizedRowElement.LeftPinnedColumns.UpdateItems();
            virtualizedRowElement.ScrollableColumns.UpdateItems();
            virtualizedRowElement.RightPinnedColumns.UpdateItems();
          }
        }
      }
      this.RowLayout.EnsureColumnsLayout();
      this.TopPinnedRows.UpdateItems();
      this.ScrollableRows.UpdateItems();
      this.BottomPinnedRows.UpdateItems();
      this.InvalidateMeasure();
    }

    public void UpdateRowsWhenColumnsChanged()
    {
      this.RowLayout.InvalidateRenderColumns();
      this.TableElement.ColumnScroller.Traverser = (ITraverser<GridViewColumn>) new ItemsTraverser<GridViewColumn>(this.RowLayout.ScrollableColumns);
      this.UpdateRows(true);
    }

    public void ClearRows()
    {
      this.TopPinnedRows.ClearItems();
      this.BottomPinnedRows.ClearItems();
      this.ScrollableRows.ClearItems();
      this.TableElement.RowScroller.ElementProvider.ClearCache();
      this.TableElement.ColumnScroller.ElementProvider.ClearCache();
      this.InvalidateMeasure();
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      this.TopPinnedRows.ElementProvider = this.TableElement.RowScroller.ElementProvider;
      this.TopPinnedRows.ItemSpacing = this.TableElement.RowSpacing;
      this.ScrollableRows.ElementProvider = this.TableElement.RowScroller.ElementProvider;
      this.ScrollableRows.ItemSpacing = this.TableElement.RowSpacing;
      this.ScrollableRows.DataProvider = (IEnumerable) this.TableElement.RowScroller;
      this.BottomPinnedRows.ElementProvider = this.TableElement.RowScroller.ElementProvider;
      this.BottomPinnedRows.ItemSpacing = this.TableElement.RowSpacing;
      this.TableElement.RowScroller.ScrollerUpdated += new EventHandler(this.RowScroller_ScrollerUpdated);
      this.TableElement.ColumnScroller.ScrollerUpdated += new EventHandler(this.ColumnScroller_ScrollerUpdated);
    }

    private void ColumnScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.RowLayout.EnsureColumnsLayout();
      foreach (GridRowElement visualRow in (IEnumerable<GridRowElement>) this.TableElement.VisualRows)
      {
        GridVirtualizedRowElement virtualizedRowElement = visualRow as GridVirtualizedRowElement;
        if (virtualizedRowElement != null)
        {
          virtualizedRowElement.ScrollableColumns.ScrollOffset = new SizeF((float) -this.TableElement.ColumnScroller.ScrollOffset, 0.0f);
          virtualizedRowElement.ScrollableColumns.UpdateItems();
        }
      }
      this.oldHScrollValue = this.TableElement.ColumnScroller.Scrollbar.Value;
    }

    private void RowScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.RowLayout.EnsureColumnsLayout();
      if (this.TableElement.RowScroller.Scrollbar.Value == this.TableElement.RowScroller.Scrollbar.Minimum)
        this.TableElement.RowScroller.ScrollOffset = 0;
      this.ScrollableRows.ScrollOffset = new SizeF(0.0f, (float) -this.TableElement.RowScroller.ScrollOffset);
      if (this.oldVScrollValue > this.TableElement.RowScroller.Scrollbar.Value)
        this.scrollableRows.UpdateOnScroll(ScrollableRowsUpdateAction.ScrollUp);
      else
        this.scrollableRows.UpdateOnScroll(ScrollableRowsUpdateAction.ScrollDown);
      this.oldVScrollValue = this.TableElement.RowScroller.Scrollbar.Value;
    }

    private void rows_ChildrenChanged(object sender, ChildrenChangedEventArgs e)
    {
      if (e.ChangeOperation != ItemsChangeOperation.Inserted)
        return;
      ((GridRowElement) e.Child).InitializeRowView(this.TableElement);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      SizeF availableSize1 = availableSize;
      if (this.TableElement.ViewTemplate != null)
        this.TableElement.BestFitHelper.ProcessRequests();
      this.TableElement.ViewElement.RowLayout.MeasureRow(availableSize1);
      if (this.TableElement.RowScroller.Scrollbar.Maximum == 0)
        this.TableElement.RowScroller.UpdateScrollRange();
      this.topPinnedRows.Measure(availableSize1);
      float num = this.topPinnedRows.DesiredSize.Height + (float) this.elementSpacing;
      availableSize1.Height -= num;
      empty.Width = this.topPinnedRows.DesiredSize.Width;
      empty.Height = num;
      this.bottomPinnedRows.Measure(availableSize1);
      float height = this.bottomPinnedRows.DesiredSize.Height;
      if ((double) this.bottomPinnedRows.DesiredSize.Height > 0.0)
        height += (float) this.elementSpacing;
      availableSize1.Height -= height;
      empty.Width = Math.Max(empty.Width, this.bottomPinnedRows.DesiredSize.Width);
      empty.Height += height;
      this.scrollableRows.Measure(availableSize1);
      empty.Width = Math.Max(empty.Width, this.scrollableRows.DesiredSize.Width);
      empty.Height += this.scrollableRows.DesiredSize.Height;
      empty.Width = Math.Min(empty.Width, availableSize.Width);
      empty.Height = Math.Min(empty.Height, availableSize.Height);
      SizeF sizeF = new SizeF(this.scrollableRows.DesiredSize.Width, this.scrollableRows.DesiredSize.Height + (float) this.TableElement.RowSpacing);
      if ((double) this.bottomPinnedRows.DesiredSize.Height > 0.0)
        availableSize1.Height += (float) this.elementSpacing;
      this.TableElement.RowScroller.ClientSize = availableSize1;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      float y1 = 0.0f;
      this.topPinnedRows.Arrange(new RectangleF(0.0f, y1, finalSize.Width, this.topPinnedRows.DesiredSize.Height));
      float y2 = y1 + (this.topPinnedRows.DesiredSize.Height + (float) this.elementSpacing);
      this.scrollableRows.Arrange(new RectangleF(0.0f, y2, finalSize.Width, this.scrollableRows.DesiredSize.Height));
      float y3 = y2 + (this.scrollableRows.DesiredSize.Height + (float) this.elementSpacing);
      if (this.TableElement.ViewTemplate.BottomPinnedRowsMode == GridViewBottomPinnedRowsMode.Fixed)
        y3 = finalSize.Height - this.bottomPinnedRows.DesiredSize.Height;
      this.bottomPinnedRows.Arrange(new RectangleF(0.0f, y3, finalSize.Width, this.bottomPinnedRows.DesiredSize.Height));
      return finalSize;
    }
  }
}
