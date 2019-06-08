// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupByFieldCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Text;
using Telerik.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GridGroupByFieldCollection : NotifyCollection<GridGroupByField>
  {
    public GridGroupByField FindByName(string fieldName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.Compare(fieldName, this[index].FieldName, true) == 0)
          return this[index];
      }
      return (GridGroupByField) null;
    }

    public GridGroupByField FindByName(string fieldName, bool aggregate)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsAggregate == aggregate && string.Compare(fieldName, this[index].FieldName, true) == 0)
          return this[index];
      }
      return (GridGroupByField) null;
    }

    public GridGroupByField FindByAlias(string fieldAlias)
    {
      if (string.IsNullOrEmpty(fieldAlias))
        return (GridGroupByField) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsFieldAliasSet && string.Compare(fieldAlias, this[index].FieldAlias, true) == 0)
          return this[index];
      }
      return (GridGroupByField) null;
    }

    public GridGroupByField Find(string field, bool aggregate)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsAggregate == aggregate && this[index].IsReferredAs(field))
          return this[index];
      }
      return (GridGroupByField) null;
    }

    public GridGroupByField Find(string field)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].IsReferredAs(field))
          return this[index];
      }
      return (GridGroupByField) null;
    }

    public GridGroupByField Find(GridGroupByField field)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Aggregate == field.Aggregate && this[index].IsReferredAs(field.FieldName))
          return this[index];
      }
      return (GridGroupByField) null;
    }

    public int IndexOf(string fieldName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].FieldName.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
          return index;
      }
      return -1;
    }

    public bool Contains(string fieldName)
    {
      return this.IndexOf(fieldName) >= 0;
    }

    public override string ToString()
    {
      if (this.Count < 1)
        return base.ToString();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.Count; ++index)
      {
        string str1 = "";
        if (index < this.Count - 1)
          str1 = ",";
        string str2 = "ASC";
        if (this[index].SortOrder == RadSortOrder.Descending)
          str2 = "DESC";
        stringBuilder.AppendFormat("{0} {1}{2}", (object) this[index].FieldName, (object) str2, (object) str1);
      }
      return stringBuilder.ToString();
    }
  }
}
