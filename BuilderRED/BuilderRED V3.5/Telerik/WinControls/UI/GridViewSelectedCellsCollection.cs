// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSelectedCellsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSelectedCellsCollection : ReadOnlyObservableCollection<GridViewCellInfo>
  {
    private Hashtable selectedRows;
    private Hashtable hashtable;
    private MasterGridViewTemplate masterTemplate;
    private int update;

    public GridViewSelectedCellsCollection(MasterGridViewTemplate masterTemplate)
      : base(new ObservableCollection<GridViewCellInfo>())
    {
      this.masterTemplate = masterTemplate;
      this.hashtable = new Hashtable();
      this.ObservableItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ObservableItems_CollectionChanged);
      this.selectedRows = new Hashtable();
    }

    public GridViewSelectedCellsCollection()
      : this((MasterGridViewTemplate) null)
    {
    }

    private ObservableCollection<GridViewCellInfo> ObservableItems
    {
      get
      {
        return this.Items as ObservableCollection<GridViewCellInfo>;
      }
    }

    internal IEnumerable<GridViewRowInfo> DistinctRows
    {
      get
      {
        IEnumerator enumerator = this.selectedRows.Keys.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            GridViewRowInfo row = (GridViewRowInfo) enumerator.Current;
            yield return row;
          }
        }
        finally
        {
          IDisposable disposable = enumerator as IDisposable;
          disposable?.Dispose();
        }
      }
    }

    public void BeginUpdate()
    {
      this.ObservableItems.BeginUpdate();
      ++this.update;
    }

    public void EndUpdate(bool notifyUpdates)
    {
      --this.update;
      this.ObservableItems.EndUpdate(notifyUpdates);
    }

    private void ObservableItems_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.RaiseSelectionChanged(true);
    }

    private void RaiseSelectionChanged(bool nofityUpdates)
    {
      if (this.masterTemplate == null || !nofityUpdates || this.update != 0)
        return;
      this.masterTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.SelectionChanged, (object) this, EventArgs.Empty);
    }

    internal void Clear()
    {
      this.hashtable.Clear();
      this.Items.Clear();
      this.selectedRows.Clear();
    }

    internal void Remove(GridViewCellInfo item)
    {
      this.RemoveFromHashtable(item);
      this.Items.Remove(item);
    }

    private void RemoveFromHashtable(GridViewCellInfo item)
    {
      this.hashtable.Remove((object) this.GetHashCodeString(item));
      this.selectedRows.Remove((object) item.RowInfo);
    }

    internal void Add(GridViewCellInfo item)
    {
      if (this.masterTemplate == null)
        throw new InvalidOperationException("The instance must have MasterTemplate.", (Exception) new NullReferenceException("MasterTemplate instance cannot be null."));
      if (item.IsSelected)
        return;
      this.AddInHashtable(item);
      this.Items.Add(item);
    }

    internal bool HasSelectedCells(GridViewCellInfoCollection items)
    {
      bool flag = true;
      foreach (GridViewCellInfo gridViewCellInfo in items)
        flag &= this.Contains(gridViewCellInfo);
      return flag;
    }

    internal void AddRange(GridViewCellInfoCollection items)
    {
      foreach (GridViewCellInfo gridViewCellInfo in items)
        this.Add(gridViewCellInfo);
    }

    internal void RemoveRange(GridViewCellInfoCollection items)
    {
      foreach (GridViewCellInfo gridViewCellInfo in items)
        this.Remove(gridViewCellInfo);
    }

    private bool AddInHashtable(GridViewCellInfo item)
    {
      this.hashtable.Add((object) this.GetHashCodeString(item), (object) item);
      GridViewRowInfo rowInfo = item.RowInfo;
      if (!this.selectedRows.ContainsKey((object) rowInfo))
        this.selectedRows.Add((object) rowInfo, (object) null);
      return false;
    }

    internal bool IsSelected(GridViewRowInfo row, GridViewColumn column)
    {
      if (row != null && column is GridViewDataColumn)
        return this.hashtable.Contains((object) this.GetHashCodeString(row, column));
      return false;
    }

    internal ReadOnlyCollection<GridViewCellInfo> GetSelectedCells(
      GridViewRowInfo row)
    {
      List<GridViewCellInfo> gridViewCellInfoList = new List<GridViewCellInfo>();
      foreach (GridViewCellInfo gridViewCellInfo in (IEnumerable<GridViewCellInfo>) this.Items)
      {
        if (gridViewCellInfo.RowInfo == row)
          gridViewCellInfoList.Add(gridViewCellInfo);
      }
      return gridViewCellInfoList.AsReadOnly();
    }

    private string GetHashCodeString(GridViewCellInfo item)
    {
      return this.GetHashCodeString(item.RowInfo, item.ColumnInfo);
    }

    private string GetHashCodeString(GridViewRowInfo row, GridViewColumn column)
    {
      return row.GetHashCode().ToString() + column.GetHashCode().ToString();
    }
  }
}
