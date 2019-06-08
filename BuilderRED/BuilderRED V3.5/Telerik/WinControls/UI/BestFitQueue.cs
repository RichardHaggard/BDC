// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BestFitQueue
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  internal class BestFitQueue
  {
    protected static readonly BestFitQueue.BestFitRequest BestFitAllColumns = new BestFitQueue.BestFitRequest(BestFitQueue.BestFitOperation.BestFitAllColumns, (GridViewColumn) null);
    private GridViewTemplate template;
    private LinkedList<BestFitQueue.BestFitRequest> bestFitRequests;

    public BestFitQueue(GridViewTemplate template)
    {
      this.bestFitRequests = new LinkedList<BestFitQueue.BestFitRequest>();
      this.template = template;
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
      this.ClearBestFitColumnRequests(new BestFitColumnMode?());
      this.bestFitRequests.AddLast(BestFitQueue.BestFitAllColumns);
    }

    public void EnqueueBestFitColumns(BestFitColumnMode mode)
    {
      this.ClearBestFitColumnRequests(new BestFitColumnMode?(mode));
      this.bestFitRequests.AddLast(new BestFitQueue.BestFitRequest(BestFitQueue.BestFitOperation.BestFitAllColumns, mode));
    }

    private void ClearBestFitColumnRequests(BestFitColumnMode? mode)
    {
      LinkedListNode<BestFitQueue.BestFitRequest> previous;
      for (LinkedListNode<BestFitQueue.BestFitRequest> node = this.bestFitRequests.Last; node != null; node = previous)
      {
        BestFitQueue.BestFitRequest bestFitRequest = node.Value;
        previous = node.Previous;
        if (bestFitRequest.Operation != BestFitQueue.BestFitOperation.BestFitColumn)
        {
          BestFitColumnMode? autoSizeMode = bestFitRequest.AutoSizeMode;
          BestFitColumnMode? nullable = mode;
          if ((autoSizeMode.GetValueOrDefault() != nullable.GetValueOrDefault() ? 0 : (autoSizeMode.HasValue == nullable.HasValue ? 1 : 0)) == 0)
            continue;
        }
        this.bestFitRequests.Remove(node);
      }
    }

    public void EnqueueBestFitColumn(GridViewColumn column)
    {
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (column.OwnerTemplate != this.template)
        throw new ArgumentException("Column " + (object) column + " is from different GridViewTemplate");
      if (!column.IsVisible)
        return;
      BestFitQueue.BestFitRequest bestFitRequest = new BestFitQueue.BestFitRequest(BestFitQueue.BestFitOperation.BestFitColumn, column);
      if (this.bestFitRequests.Contains(BestFitQueue.BestFitAllColumns) || this.bestFitRequests.Contains(bestFitRequest))
        return;
      this.bestFitRequests.AddLast(bestFitRequest);
    }

    public BestFitQueue.BestFitRequest Dequeue()
    {
      if (this.bestFitRequests.First == null)
        return (BestFitQueue.BestFitRequest) null;
      BestFitQueue.BestFitRequest bestFitRequest = this.bestFitRequests.First.Value;
      this.bestFitRequests.RemoveFirst();
      return bestFitRequest;
    }

    public BestFitQueue.BestFitRequest Dequeue(GridViewColumn column)
    {
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (column.OwnerTemplate != this.template)
        throw new ArgumentException("Column " + (object) column + " is not owned by the current GridViewTemplate.");
      BestFitQueue.BestFitRequest bestFitRequest = new BestFitQueue.BestFitRequest(BestFitQueue.BestFitOperation.BestFitColumn, column);
      if (this.bestFitRequests.Contains(BestFitQueue.BestFitAllColumns))
      {
        this.bestFitRequests.Clear();
        foreach (GridViewColumn column1 in (Collection<GridViewDataColumn>) this.template.Columns)
        {
          if (column1 != column)
            this.bestFitRequests.AddLast(new BestFitQueue.BestFitRequest(BestFitQueue.BestFitOperation.BestFitColumn, column1));
        }
      }
      else
        this.bestFitRequests.Remove(bestFitRequest);
      return bestFitRequest;
    }

    public enum BestFitOperation
    {
      BestFitColumn,
      BestFitAllColumns,
    }

    public class BestFitRequest : IEquatable<BestFitQueue.BestFitRequest>
    {
      private BestFitColumnMode? autoSizeMode = new BestFitColumnMode?();
      private BestFitQueue.BestFitOperation operation;
      private GridViewColumn column;

      public BestFitRequest(BestFitQueue.BestFitOperation operation, GridViewColumn column)
      {
        this.operation = operation;
        this.column = column;
        this.autoSizeMode = this.column != null ? new BestFitColumnMode?(column.AutoSizeMode) : new BestFitColumnMode?();
      }

      public BestFitRequest(BestFitQueue.BestFitOperation operation)
        : this(operation, (GridViewColumn) null)
      {
      }

      public BestFitRequest(BestFitQueue.BestFitOperation operation, BestFitColumnMode mode)
        : this(operation)
      {
        this.autoSizeMode = new BestFitColumnMode?(mode);
      }

      public BestFitQueue.BestFitOperation Operation
      {
        get
        {
          return this.operation;
        }
      }

      public BestFitColumnMode? AutoSizeMode
      {
        get
        {
          return this.autoSizeMode;
        }
      }

      public GridViewColumn Column
      {
        get
        {
          return this.column;
        }
      }

      public bool Equals(BestFitQueue.BestFitRequest request)
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
