// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSortField
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridSortField : SortDescriptor
  {
    private RadSortOrder sortOrder;

    public GridSortField()
      : base(string.Empty, ListSortDirection.Ascending)
    {
    }

    public GridSortField(string fieldName)
      : base(string.Empty, ListSortDirection.Ascending)
    {
      if (string.IsNullOrEmpty(fieldName))
        return;
      List<SortDescriptor> sortString = DataUtils.ParseSortString(fieldName);
      if (sortString.Count <= 0)
        return;
      this.PropertyName = sortString[0].PropertyName;
      this.Direction = sortString[0].Direction;
      this.sortOrder = GridViewHelper.GetSortDirection(this.Direction);
    }

    public GridSortField(string fieldName, RadSortOrder sortOrder)
      : base(string.Empty, ListSortDirection.Ascending)
    {
      if (!string.IsNullOrEmpty(fieldName))
      {
        List<SortDescriptor> sortString = DataUtils.ParseSortString(fieldName);
        if (sortString.Count > 0)
          this.PropertyName = sortString[0].PropertyName;
      }
      this.Direction = GridViewHelper.GetSortDirection(sortOrder);
      this.sortOrder = sortOrder;
    }

    public string FieldName
    {
      get
      {
        return this.PropertyName;
      }
      set
      {
        this.PropertyName = value;
      }
    }

    public RadSortOrder SortOrder
    {
      get
      {
        return this.sortOrder;
      }
      set
      {
        if (this.sortOrder == value)
          return;
        this.sortOrder = value;
        if (this.sortOrder == RadSortOrder.None && this.Owner != null)
          this.Owner.Remove((SortDescriptor) this);
        this.Direction = GridViewHelper.GetSortDirection(this.sortOrder);
        this.OnPropertyChanged(nameof (SortOrder));
      }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.PropertyName == "Direction") || this.Owner == null || !this.Owner.Contains((SortDescriptor) this))
        return;
      this.sortOrder = GridViewHelper.GetSortDirection(this.Direction);
    }

    [Browsable(false)]
    public bool IsEmpty
    {
      get
      {
        if (string.IsNullOrEmpty(this.PropertyName))
          return this.sortOrder == RadSortOrder.None;
        return false;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj is GridSortField)
        return this.PropertyName == (obj as GridSortField).PropertyName;
      return false;
    }

    public override int GetHashCode()
    {
      if (string.IsNullOrEmpty(this.PropertyName))
        return base.GetHashCode();
      return this.PropertyName.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("[{0}] {1}", (object) this.PropertyName, (object) this.SortOrderAsString());
    }

    public GridSortField Clone()
    {
      return new GridSortField(this.PropertyName, this.sortOrder);
    }

    public string SortOrderAsString()
    {
      return GridSortField.SortOrderAsString(this.sortOrder);
    }

    public static RadSortOrder SortOrderFromString(string sortOrder)
    {
      if (string.IsNullOrEmpty(sortOrder))
        return RadSortOrder.None;
      switch (sortOrder.ToUpper())
      {
        case "ASC":
          return RadSortOrder.Ascending;
        case "DESC":
          return RadSortOrder.Descending;
        default:
          return RadSortOrder.None;
      }
    }

    public static string SortOrderAsString(RadSortOrder sortOrder)
    {
      switch (sortOrder)
      {
        case RadSortOrder.Ascending:
          return "ASC";
        case RadSortOrder.Descending:
          return "DESC";
        default:
          return "";
      }
    }
  }
}
