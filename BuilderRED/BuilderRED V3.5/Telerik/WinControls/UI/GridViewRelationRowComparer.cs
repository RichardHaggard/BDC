// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRelationRowComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.UI
{
  public class GridViewRelationRowComparer : IComparer<GridViewRowInfo>
  {
    private GridViewRelation relation;
    private GridViewColumn[] parentColumns;
    private GridViewColumn[] childColumns;

    public GridViewRelationRowComparer(GridViewRelation relation)
    {
      this.relation = relation;
      this.Reset();
    }

    public void Reset()
    {
      this.parentColumns = new GridViewColumn[this.relation.ParentColumnNames.Count];
      for (int index = 0; index < this.relation.ParentColumnNames.Count; ++index)
        this.parentColumns[index] = (GridViewColumn) this.relation.ParentTemplate.Columns[this.relation.ParentColumnNames[index]];
      this.childColumns = new GridViewColumn[this.relation.ChildColumnNames.Count];
      for (int index = 0; index < this.relation.ChildColumnNames.Count; ++index)
        this.childColumns[index] = (GridViewColumn) this.relation.ChildTemplate.Columns[this.relation.ChildColumnNames[index]];
    }

    public bool IsValid
    {
      get
      {
        for (int index = 0; index < this.parentColumns.Length; ++index)
        {
          if (this.parentColumns[index] == null)
            return false;
        }
        for (int index = 0; index < this.childColumns.Length; ++index)
        {
          if (this.childColumns[index] == null)
            return false;
        }
        return true;
      }
    }

    public int Compare(GridViewRowInfo x, GridViewRowInfo y)
    {
      for (int index = 0; index < this.childColumns.Length; ++index)
      {
        object xValue = x[x.ViewTemplate == this.relation.ParentTemplate ? this.parentColumns[index] : this.childColumns[index]];
        object yValue = y[y.ViewTemplate == this.relation.ParentTemplate ? this.parentColumns[index] : this.childColumns[index]];
        IComparable comparable = xValue as IComparable;
        int num = comparable == null || yValue == null || (object) yValue.GetType() != (object) xValue.GetType() ? DataUtils.CompareNulls(xValue, yValue) : comparable.CompareTo(yValue);
        if (num != 0)
          return num;
      }
      return 0;
    }
  }
}
