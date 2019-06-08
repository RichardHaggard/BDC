// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewBestFitQueue
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  internal class ListViewBestFitQueue
  {
    protected static readonly ListViewBestFitQueue.ListViewBestFitRequest BestFitAllColumns = new ListViewBestFitQueue.ListViewBestFitRequest(ListViewBestFitQueue.BestFitOperation.BestFitAllColumns, (ListViewDetailColumn) null);
    private DetailListViewElement detailListView;
    private LinkedList<ListViewBestFitQueue.ListViewBestFitRequest> bestFitRequests;

    public ListViewBestFitQueue(DetailListViewElement detailListView)
    {
      this.bestFitRequests = new LinkedList<ListViewBestFitQueue.ListViewBestFitRequest>();
      this.detailListView = detailListView;
    }

    public int Count
    {
      get
      {
        return this.bestFitRequests.Count;
      }
    }

    public void EnqueueBestFitColumns()
    {
      this.ClearBestFitColumnRequests(new ListViewBestFitColumnMode?());
      this.bestFitRequests.AddLast(ListViewBestFitQueue.BestFitAllColumns);
    }

    public void EnqueueBestFitColumns(ListViewBestFitColumnMode mode)
    {
      this.ClearBestFitColumnRequests(new ListViewBestFitColumnMode?(mode));
      this.bestFitRequests.AddLast(new ListViewBestFitQueue.ListViewBestFitRequest(ListViewBestFitQueue.BestFitOperation.BestFitAllColumns, mode));
    }

    private void ClearBestFitColumnRequests(ListViewBestFitColumnMode? mode)
    {
      LinkedListNode<ListViewBestFitQueue.ListViewBestFitRequest> previous;
      for (LinkedListNode<ListViewBestFitQueue.ListViewBestFitRequest> node = this.bestFitRequests.Last; node != null; node = previous)
      {
        ListViewBestFitQueue.ListViewBestFitRequest viewBestFitRequest = node.Value;
        previous = node.Previous;
        if (viewBestFitRequest.Operation != ListViewBestFitQueue.BestFitOperation.BestFitColumn)
        {
          ListViewBestFitColumnMode? autoSizeMode = viewBestFitRequest.AutoSizeMode;
          ListViewBestFitColumnMode? nullable = mode;
          if ((autoSizeMode.GetValueOrDefault() != nullable.GetValueOrDefault() ? 0 : (autoSizeMode.HasValue == nullable.HasValue ? 1 : 0)) == 0)
            continue;
        }
        this.bestFitRequests.Remove(node);
      }
    }

    public void EnqueueBestFitColumn(ListViewDetailColumn column)
    {
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (column.Owner.ViewElement != this.detailListView)
        throw new ArgumentException("Column " + (object) column + " is from different ListViewElement");
      if (!column.Visible)
        return;
      ListViewBestFitQueue.ListViewBestFitRequest viewBestFitRequest = new ListViewBestFitQueue.ListViewBestFitRequest(ListViewBestFitQueue.BestFitOperation.BestFitColumn, column);
      if (this.bestFitRequests.Contains(ListViewBestFitQueue.BestFitAllColumns) || this.bestFitRequests.Contains(viewBestFitRequest))
        return;
      this.bestFitRequests.AddLast(viewBestFitRequest);
    }

    public ListViewBestFitQueue.ListViewBestFitRequest Dequeue()
    {
      if (this.bestFitRequests.First == null)
        return (ListViewBestFitQueue.ListViewBestFitRequest) null;
      ListViewBestFitQueue.ListViewBestFitRequest viewBestFitRequest = this.bestFitRequests.First.Value;
      this.bestFitRequests.RemoveFirst();
      return viewBestFitRequest;
    }

    public ListViewBestFitQueue.ListViewBestFitRequest Dequeue(
      ListViewDetailColumn column)
    {
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (column.Owner.ViewElement != this.detailListView)
        throw new ArgumentException("Column " + (object) column + " is not owned by the current GridViewTemplate.");
      ListViewBestFitQueue.ListViewBestFitRequest viewBestFitRequest = new ListViewBestFitQueue.ListViewBestFitRequest(ListViewBestFitQueue.BestFitOperation.BestFitColumn, column);
      if (this.bestFitRequests.Contains(ListViewBestFitQueue.BestFitAllColumns))
      {
        this.bestFitRequests.Clear();
        foreach (ListViewDetailColumn column1 in (Collection<ListViewDetailColumn>) this.detailListView.Owner.Columns)
        {
          if (column1 != column)
            this.bestFitRequests.AddLast(new ListViewBestFitQueue.ListViewBestFitRequest(ListViewBestFitQueue.BestFitOperation.BestFitColumn, column1));
        }
      }
      else
        this.bestFitRequests.Remove(viewBestFitRequest);
      return viewBestFitRequest;
    }

    public enum BestFitOperation
    {
      BestFitColumn,
      BestFitAllColumns,
    }

    public class ListViewBestFitRequest : IEquatable<ListViewBestFitQueue.ListViewBestFitRequest>
    {
      private ListViewBestFitColumnMode? autoSizeMode = new ListViewBestFitColumnMode?();
      private ListViewBestFitQueue.BestFitOperation operation;
      private ListViewDetailColumn column;

      public ListViewBestFitRequest(
        ListViewBestFitQueue.BestFitOperation operation,
        ListViewDetailColumn column)
      {
        this.operation = operation;
        this.column = column;
        this.autoSizeMode = this.column != null ? new ListViewBestFitColumnMode?(column.AutoSizeMode) : new ListViewBestFitColumnMode?();
      }

      public ListViewBestFitRequest(ListViewBestFitQueue.BestFitOperation operation)
        : this(operation, (ListViewDetailColumn) null)
      {
      }

      public ListViewBestFitRequest(
        ListViewBestFitQueue.BestFitOperation operation,
        ListViewBestFitColumnMode mode)
        : this(operation)
      {
        this.autoSizeMode = new ListViewBestFitColumnMode?(mode);
      }

      public ListViewBestFitQueue.BestFitOperation Operation
      {
        get
        {
          return this.operation;
        }
      }

      public ListViewBestFitColumnMode? AutoSizeMode
      {
        get
        {
          return this.autoSizeMode;
        }
      }

      public ListViewDetailColumn Column
      {
        get
        {
          return this.column;
        }
      }

      public bool Equals(
        ListViewBestFitQueue.ListViewBestFitRequest request)
      {
        if (request != null && this.Column == request.Column)
          return this.Operation == request.Operation;
        return false;
      }

      public override int GetHashCode()
      {
        int hashCode = this.Operation.GetHashCode();
        if (this.Column != null)
          hashCode ^= this.Column.GetHashCode();
        return hashCode;
      }
    }
  }
}
