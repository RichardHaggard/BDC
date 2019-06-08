// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.GroupComparer`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  internal class GroupComparer<T> : IComparer<Group<T>> where T : IDataItem
  {
    private ListSortDirection[] directions;

    public GroupComparer()
    {
    }

    public GroupComparer(ListSortDirection[] directions)
    {
      this.directions = directions;
    }

    public ListSortDirection[] Directions
    {
      get
      {
        return this.directions;
      }
      set
      {
        this.directions = value;
      }
    }

    public int Compare(Group<T> x, Group<T> y)
    {
      object[] key1 = x.Key as object[];
      object[] key2 = y.Key as object[];
      if (key1 != null && key2 != null && key1.Length == key2.Length)
      {
        int num = 0;
        for (int index = 0; index < key1.Length; ++index)
        {
          IComparable comparable1 = key1[index] as IComparable;
          IComparable comparable2 = key2[index] as IComparable;
          num = comparable1 == null || comparable2 == null ? (comparable1 != comparable2 ? DataStorageHelper.CompareNulls((object) comparable1, (object) comparable2) : 0) : ((object) comparable1.GetType() != (object) comparable2.GetType() ? -1 : comparable1.CompareTo((object) comparable2));
          if (num != 0)
          {
            if (this.directions[index] == ListSortDirection.Descending)
              return -num;
            return num;
          }
        }
        return num;
      }
      if (!(x.Key is IComparable) || (object) x.Key.GetType() != (object) y.Key.GetType())
        return x.GetHashCode().CompareTo(y.GetHashCode());
      int num1 = ((IComparable) x.Key).CompareTo(y.Key);
      if (this.directions[this.directions.Length - 1] == ListSortDirection.Descending)
        return -num1;
      return num1;
    }
  }
}
