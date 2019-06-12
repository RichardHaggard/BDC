// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSystemRowCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSystemRowCollection : IReadOnlyCollection<GridViewSystemRowInfo>, IEnumerable<GridViewSystemRowInfo>, IEnumerable, ITraversable
  {
    private GridViewInfo viewInfo;
    private List<GridViewSystemRowInfo> list;

    public GridViewSystemRowCollection(GridViewInfo viewInfo)
    {
      this.viewInfo = viewInfo;
      this.list = new List<GridViewSystemRowInfo>();
    }

    protected IList<GridViewSystemRowInfo> Items
    {
      get
      {
        return (IList<GridViewSystemRowInfo>) this.list;
      }
    }

    public void Move(int indexFrom, int indexTo)
    {
      if (indexFrom == indexTo || indexFrom < 0 || (indexFrom >= this.list.Count || indexTo < 0) || indexTo >= this.list.Count)
        return;
      GridViewSystemRowInfo viewSystemRowInfo = this.list[indexFrom];
      this.list.RemoveAt(indexFrom);
      this.list.Insert(indexTo, viewSystemRowInfo);
      if (this.viewInfo.ViewTemplate == null || this.viewInfo.ViewTemplate.MasterTemplate == null)
        return;
      this.viewInfo.ViewTemplate.MasterTemplate.Owner.GridViewElement.InvalidateMeasure(true);
    }

    internal void Add(GridViewSystemRowInfo row)
    {
      this.list.Add(row);
      this.list.Sort();
    }

    public int Count
    {
      get
      {
        return this.list.Count;
      }
    }

    public GridViewSystemRowInfo this[int index]
    {
      get
      {
        return this.list[index];
      }
    }

    public bool Contains(GridViewSystemRowInfo value)
    {
      return this.list.Contains(value);
    }

    public void CopyTo(GridViewSystemRowInfo[] array, int index)
    {
      this.list.CopyTo(array, index);
    }

    public int IndexOf(GridViewSystemRowInfo value)
    {
      return this.list.IndexOf(value);
    }

    public IEnumerator<GridViewSystemRowInfo> GetEnumerator()
    {
      return (IEnumerator<GridViewSystemRowInfo>) this.list.GetEnumerator();
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
