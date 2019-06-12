// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.DataItemComparer`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  internal class DataItemComparer<TDataItem> : IComparer<TDataItem> where TDataItem : IDataItem
  {
    private SortDescriptorCollection sortContext;

    public DataItemComparer(SortDescriptorCollection sortContext)
    {
      this.sortContext = sortContext;
    }

    int IComparer<TDataItem>.Compare(TDataItem x, TDataItem y)
    {
      int num1 = x.GetHashCode().CompareTo(y.GetHashCode());
      if (num1 == 0)
        return 0;
      if (this.sortContext == null || this.sortContext.Count == 0)
        return num1;
      for (int index = 0; index < this.sortContext.Count; ++index)
      {
        if (this.sortContext[index].PropertyIndex < 0)
          this.sortContext[index].PropertyIndex = x.IndexOf(this.sortContext[index].PropertyName);
        object val1 = x[this.sortContext[index].PropertyIndex];
        object val2 = y[this.sortContext[index].PropertyIndex];
        int num2 = !(val1 is IComparable) || val2 == null || (object) val2.GetType() != (object) val1.GetType() ? DataStorageHelper.CompareNulls(val1, val2) : ((IComparable) val1).CompareTo(val2);
        if (num2 != 0)
        {
          if (this.sortContext[index].Direction != ListSortDirection.Descending)
            return num2;
          return -num2;
        }
      }
      return 0;
    }
  }
}
