// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewBestFitHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class ListViewBestFitHelper
  {
    private DetailListViewElement detailListView;
    private ListViewBestFitQueue bestFitQueue;
    private bool updateItemsHeight;
    private int processing;

    public ListViewBestFitHelper(DetailListViewElement detailListView)
    {
      this.detailListView = detailListView;
      this.bestFitQueue = new ListViewBestFitQueue(detailListView);
    }

    protected DetailListViewElement DetailListView
    {
      get
      {
        return this.detailListView;
      }
    }

    protected internal ListViewBestFitQueue BestFitQueue
    {
      get
      {
        return this.bestFitQueue;
      }
    }

    public void BestFitColumn(ListViewDetailColumn column)
    {
      this.BestFitQueue.EnqueueBestFitColumn(column);
      this.ProcessRequests();
    }

    public void BestFitColumns()
    {
      this.BestFitQueue.EnqueueBestFitColumns();
      this.ProcessRequests();
    }

    public void BestFitColumns(ListViewBestFitColumnMode mode)
    {
      this.BestFitQueue.EnqueueBestFitColumns(mode);
      this.ProcessRequests();
    }

    public void ProcessRequests()
    {
      if (this.processing > 0 || this.DetailListView == null || this.DetailListView.ViewElement == null)
        return;
      ++this.processing;
      if (this.BestFitQueue.Count == 0 || !this.DetailListView.CanBestFit())
      {
        --this.processing;
      }
      else
      {
        this.DetailListView.UpdateLayout();
        if (this.DetailListView.Children.Count == 0 || this.DetailListView.Owner.Columns.Count == 0)
          --this.processing;
        else if (this.DetailListView.DesiredSize == SizeF.Empty)
        {
          this.DetailListView.UpdateLayout();
          --this.processing;
        }
        else
        {
          this.updateItemsHeight = false;
          while (this.BestFitQueue.Count > 0)
          {
            ListViewBestFitQueue.ListViewBestFitRequest viewBestFitRequest = this.BestFitQueue.Dequeue();
            ListViewBestFitColumnMode? autoSizeMode = viewBestFitRequest.AutoSizeMode;
            if (viewBestFitRequest.Operation == ListViewBestFitQueue.BestFitOperation.BestFitAllColumns)
            {
              this.BestFitColumnsCore(autoSizeMode);
            }
            else
            {
              ListViewBestFitColumnMode mode = autoSizeMode.HasValue ? autoSizeMode.Value : viewBestFitRequest.Column.AutoSizeMode;
              this.BestFitColumnCore(viewBestFitRequest.Column, mode);
            }
          }
          this.UpdateLayout();
          --this.processing;
        }
      }
    }

    protected virtual void BestFitColumnsCore(ListViewBestFitColumnMode? mode)
    {
      ListViewBestFitColumnMode? nullable = mode;
      if ((nullable.GetValueOrDefault() != ListViewBestFitColumnMode.None ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
        return;
      float[] numArray = new float[this.DetailListView.Owner.Columns.Count];
      int index1 = -1;
      foreach (ListViewDetailColumn column in (Collection<ListViewDetailColumn>) this.DetailListView.Owner.Columns)
      {
        ++index1;
        ListViewBestFitColumnMode mode1 = mode.HasValue ? mode.Value : column.AutoSizeMode;
        if (column.Visible && mode1 != ListViewBestFitColumnMode.None)
        {
          float columnWidth = this.CalculateColumnWidth(column, mode1);
          float val1 = numArray[index1];
          numArray[index1] = Math.Max(val1, columnWidth);
        }
      }
      for (int index2 = 0; index2 < numArray.Length; ++index2)
      {
        ListViewDetailColumn column = this.DetailListView.Owner.Columns[index2];
        if (column != null && column.Visible && column.AutoSizeMode != ListViewBestFitColumnMode.None)
          this.SetColumnWidth(column, numArray[index2]);
      }
    }

    protected virtual void BestFitColumnCore(
      ListViewDetailColumn column,
      ListViewBestFitColumnMode mode)
    {
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (mode == ListViewBestFitColumnMode.None || !column.Visible)
        return;
      float columnWidth = this.CalculateColumnWidth(column, mode);
      this.SetColumnWidth(column, columnWidth);
    }

    protected virtual float CalculateColumnWidth(
      ListViewDetailColumn column,
      ListViewBestFitColumnMode mode)
    {
      IVirtualizedElementProvider<ListViewDataItem> elementProvider = this.DetailListView.Scroller.ElementProvider;
      float val1 = 0.0f;
      if (mode == ListViewBestFitColumnMode.HeaderCells || mode == ListViewBestFitColumnMode.AllCells)
      {
        DetailListViewHeaderCellElement element = this.DetailListView.ColumnScroller.ElementProvider.GetElement(column, (object) null) as DetailListViewHeaderCellElement;
        this.DetailListView.ColumnContainer.Children.Add((RadElement) element);
        element.Attach(column, (object) null);
        element.ResetLayout(true);
        val1 = Math.Max(val1, this.GetCellDesiredWidth((DetailListViewCellElement) element));
        this.DetailListView.ColumnContainer.Children.Remove((RadElement) element);
        this.Detach(this.DetailListView.ColumnScroller.ElementProvider, (DetailListViewCellElement) element);
        if (mode == ListViewBestFitColumnMode.HeaderCells)
          return val1;
      }
      IEnumerator<ListViewDataItem> enumerator = (IEnumerator<ListViewDataItem>) null;
      if (mode == ListViewBestFitColumnMode.DataCells || mode == ListViewBestFitColumnMode.AllCells)
        enumerator = (IEnumerator<ListViewDataItem>) new ListViewTraverser(this.DetailListView.Owner);
      enumerator.Reset();
      while (enumerator.MoveNext())
      {
        ListViewDataItem current = enumerator.Current;
        if (this.CanBestFitItem(current))
        {
          DetailListViewVisualItem element1 = elementProvider.GetElement(current, (object) null) as DetailListViewVisualItem;
          element1.Attach(current, (object) null);
          this.DetailListView.ColumnContainer.Children.Add((RadElement) element1);
          IVirtualizedElementProvider<ListViewDetailColumn> cellElementProvider = (IVirtualizedElementProvider<ListViewDetailColumn>) new DetailListViewDataCellElementProvider(element1);
          DetailListViewCellElement element2 = cellElementProvider.GetElement(column, (object) element1) as DetailListViewCellElement;
          element1.Children.Add((RadElement) element2);
          element2.Attach(column, (object) element1);
          element2.ResetLayout(true);
          val1 = Math.Max(val1, this.GetCellDesiredWidth(element2));
          element1.Children.Remove((RadElement) element2);
          this.DetailListView.ColumnContainer.Children.Remove((RadElement) element1);
          this.Detach(cellElementProvider, element2);
          this.Detach(elementProvider, element1);
        }
      }
      return val1;
    }

    private void Detach(
      IVirtualizedElementProvider<ListViewDetailColumn> cellElementProvider,
      DetailListViewCellElement cell)
    {
      if (cell != null)
      {
        cellElementProvider.CacheElement((IVirtualizedElement<ListViewDetailColumn>) cell);
        cell.Detach();
        if (cell.Parent == null || !cell.Parent.Children.Contains((RadElement) cell))
          return;
        cell.Parent.Children.Remove((RadElement) cell);
      }
      else
        cell.Dispose();
    }

    private void Detach(
      IVirtualizedElementProvider<ListViewDataItem> itemElementProvider,
      DetailListViewVisualItem item)
    {
      if (item != null)
      {
        itemElementProvider.CacheElement((IVirtualizedElement<ListViewDataItem>) item);
        item.Detach();
        if (item.Parent == null || !item.Parent.Children.Contains((RadElement) item))
          return;
        item.Parent.Children.Remove((RadElement) item);
      }
      else
        item.Dispose();
    }

    protected virtual bool CanBestFitItem(ListViewDataItem item)
    {
      return item != null && !(item is ListViewDataItemGroup);
    }

    protected virtual float GetCellDesiredWidth(DetailListViewCellElement cell)
    {
      cell.IgnoreColumnWidth = true;
      cell.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
      cell.IgnoreColumnWidth = false;
      return cell.DesiredSize.Width;
    }

    protected virtual void SetColumnWidth(ListViewDetailColumn column, float desiredWidth)
    {
      if (column == null)
        return;
      if ((double) column.MaxWidth > 0.0 && (double) desiredWidth > (double) column.MaxWidth)
        desiredWidth = column.MaxWidth;
      if ((double) desiredWidth < (double) column.MinWidth)
        desiredWidth = column.MinWidth;
      if (!this.updateItemsHeight)
        this.updateItemsHeight = (double) column.Width != (double) desiredWidth && this.DetailListView.AutoSizeItems;
      column.Width = (float) (int) desiredWidth;
    }

    private void UpdateLayout()
    {
      this.DetailListView.InvalidateMeasure();
      if (!this.DetailListView.AutoSizeItems)
        return;
      this.DetailListView.InvalidateArrange();
    }
  }
}
