// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class DataGroupCollection : GroupCollection<GridViewRowInfo>, IReadOnlyCollection<GridViewRowInfo>, IEnumerable<GridViewRowInfo>, IEnumerable
  {
    public static DataGroupCollection Empty = new DataGroupCollection((IList<Group<GridViewRowInfo>>) new List<Group<GridViewRowInfo>>());

    public DataGroupCollection(IList<Group<GridViewRowInfo>> list)
      : base(list)
    {
    }

    public DataGroup this[int index]
    {
      get
      {
        return (DataGroup) base[index];
      }
    }

    GridViewRowInfo IReadOnlyCollection<GridViewRowInfo>.this[
      int index]
    {
      get
      {
        return (GridViewRowInfo) this[index].GroupRow;
      }
    }

    bool IReadOnlyCollection<GridViewRowInfo>.Contains(
      GridViewRowInfo value)
    {
      GridViewGroupRowInfo viewGroupRowInfo = value as GridViewGroupRowInfo;
      if (viewGroupRowInfo == null)
        return false;
      return this.IndexOf((Group<GridViewRowInfo>) viewGroupRowInfo.Group) >= 0;
    }

    void IReadOnlyCollection<GridViewRowInfo>.CopyTo(
      GridViewRowInfo[] array,
      int index)
    {
      for (int index1 = index; index1 < this.Count; ++index1)
        array[index1] = (GridViewRowInfo) this[index1].GroupRow;
    }

    int IReadOnlyCollection<GridViewRowInfo>.IndexOf(
      GridViewRowInfo value)
    {
      GridViewGroupRowInfo viewGroupRowInfo = value as GridViewGroupRowInfo;
      if (viewGroupRowInfo == null)
        return -1;
      return this.IndexOf((Group<GridViewRowInfo>) viewGroupRowInfo.Group);
    }

    IEnumerator<GridViewRowInfo> IEnumerable<GridViewRowInfo>.GetEnumerator()
    {
      for (int i = 0; i < this.Count; ++i)
        yield return (GridViewRowInfo) this[i].GroupRow;
    }
  }
}
