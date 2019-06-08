// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSummaryRowCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSummaryRowCollection : IReadOnlyCollection<GridViewSummaryRowInfo>, IEnumerable<GridViewSummaryRowInfo>, IEnumerable, ITraversable
  {
    private IList<GridViewSummaryRowInfo> list;

    public GridViewSummaryRowCollection(IList<GridViewSummaryRowInfo> list)
    {
      this.list = list;
    }

    protected IList<GridViewSummaryRowInfo> Items
    {
      get
      {
        return this.list;
      }
    }

    public int IndexOf(GridViewSummaryRowItem summaryItem)
    {
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index].SummaryRowItem == summaryItem)
          return index;
      }
      return -1;
    }

    public bool Contains(GridViewSummaryRowItem summaryItem)
    {
      return this.IndexOf(summaryItem) != -1;
    }

    internal GridViewSummaryRowInfo Add(
      GridViewInfo viewInfo,
      GridViewSummaryRowItem summaryItem,
      bool top)
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewSummaryRowInfo(viewInfo, (GridViewGroupRowInfo) null), viewInfo);
      viewInfo.ViewTemplate.OnCreateRowInfo(e);
      GridViewSummaryRowInfo rowInfo = e.RowInfo as GridViewSummaryRowInfo;
      if (rowInfo != null)
      {
        rowInfo.SummaryRowItem = summaryItem;
        rowInfo.RowPosition = top ? SystemRowPosition.Top : SystemRowPosition.Bottom;
        this.Items.Add(rowInfo);
      }
      return rowInfo;
    }

    internal GridViewSummaryRowInfo Remove(GridViewSummaryRowItem summaryItem)
    {
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index].SummaryRowItem == summaryItem)
        {
          GridViewSummaryRowInfo viewSummaryRowInfo = this.Items[index];
          this.Items.Remove(viewSummaryRowInfo);
          return viewSummaryRowInfo;
        }
      }
      return (GridViewSummaryRowInfo) null;
    }

    internal void Clear()
    {
      this.Items.Clear();
    }

    internal void AddRows(GridViewInfo viewInfo, GridViewSummaryRowItemCollection items, bool top)
    {
      foreach (GridViewSummaryRowItem summaryItem in (Collection<GridViewSummaryRowItem>) items)
        this.Add(viewInfo, summaryItem, top);
    }

    public int Count
    {
      get
      {
        return this.list.Count;
      }
    }

    public GridViewSummaryRowInfo this[int index]
    {
      get
      {
        return this.list[index];
      }
    }

    public bool Contains(GridViewSummaryRowInfo value)
    {
      return this.list.Contains(value);
    }

    public void CopyTo(GridViewSummaryRowInfo[] array, int index)
    {
      this.list.CopyTo(array, index);
    }

    public int IndexOf(GridViewSummaryRowInfo value)
    {
      return this.list.IndexOf(value);
    }

    public IEnumerator<GridViewSummaryRowInfo> GetEnumerator()
    {
      return this.list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.list.GetEnumerator();
    }

    object ITraversable.this[int index]
    {
      get
      {
        return (object) this[index];
      }
    }
  }
}
