// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSelfReferenceComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewSelfReferenceComparer : IComparer<GridViewRowInfo>
  {
    private GridViewRelation relation;
    private GridViewColumn[] parentColumns;
    private GridViewColumn[] childColumns;

    public GridViewSelfReferenceComparer(GridViewRelation relation)
    {
      this.relation = relation;
      this.parentColumns = new GridViewColumn[relation.ParentColumnNames.Count];
      for (int index = 0; index < this.relation.ParentColumnNames.Count; ++index)
        this.parentColumns[index] = (GridViewColumn) this.relation.ParentTemplate.Columns[this.relation.ParentColumnNames[index]];
      this.childColumns = new GridViewColumn[relation.ChildColumnNames.Count];
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

    public void Reset()
    {
      this.parentColumns = new GridViewColumn[this.relation.ParentColumnNames.Count];
      for (int index = 0; index < this.relation.ParentColumnNames.Count; ++index)
        this.parentColumns[index] = (GridViewColumn) this.relation.ParentTemplate.Columns[this.relation.ParentColumnNames[index]];
      this.childColumns = new GridViewColumn[this.relation.ChildColumnNames.Count];
      for (int index = 0; index < this.relation.ChildColumnNames.Count; ++index)
        this.childColumns[index] = (GridViewColumn) this.relation.ChildTemplate.Columns[this.relation.ChildColumnNames[index]];
    }

    public int Compare(GridViewRowInfo x, GridViewRowInfo y)
    {
      for (int index = 0; index < this.relation.ChildColumnNames.Count; ++index)
      {
        object xValue = x[this.childColumns[index]];
        object yValue = y[this.childColumns[index]];
        int num;
        if (xValue == yValue)
          num = 0;
        else if (xValue == null || xValue == DBNull.Value)
          num = -1;
        else if (yValue == null || yValue == DBNull.Value)
          num = 1;
        else if (this.TrySyncValues(xValue, ref yValue))
        {
          if (!(xValue is IComparable))
            throw new ArgumentException("Argument of type " + (object) xValue.GetType() + " does not implement IComparable", "xValue");
          num = ((IComparable) xValue).CompareTo(yValue);
        }
        else
          throw new ArgumentException("Cannot convert from " + (object) xValue.GetType() + " to " + (object) yValue.GetType());
        if (num != 0)
          return num;
      }
      return 0;
    }

    private bool TrySyncValues(object xValue, ref object yValue)
    {
      Type type1 = xValue.GetType();
      Type type2 = yValue.GetType();
      if ((object) type1 == (object) type2)
        return true;
      TypeConverter converter1 = TypeDescriptor.GetConverter(type1);
      if (converter1 != null && converter1.CanConvertFrom(type2))
      {
        yValue = converter1.ConvertFrom(yValue);
      }
      else
      {
        TypeConverter converter2 = TypeDescriptor.GetConverter(type2);
        if (converter2 != null && converter2.CanConvertTo(type1))
          yValue = converter2.ConvertTo(yValue, type1);
      }
      Type type3 = yValue.GetType();
      return (object) type1 == (object) type3;
    }
  }
}
