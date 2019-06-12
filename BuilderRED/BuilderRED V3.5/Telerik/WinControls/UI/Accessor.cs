// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Accessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  internal class Accessor : IDisposable
  {
    private GridViewColumn column;
    protected bool subPropertyMode;

    public Accessor(GridViewColumn column)
    {
      if (column == null)
        throw new ArgumentException("Column argument can not be null.");
      this.column = column;
      this.subPropertyMode = !string.IsNullOrEmpty(this.column.FieldName) && this.column.FieldName.Contains(".");
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.column.OwnerTemplate;
      }
    }

    public virtual object this[GridViewRowInfo row]
    {
      get
      {
        return row.Cache[this.column];
      }
      set
      {
        this.SetUnboundValue(row, value);
      }
    }

    private void SetUnboundValue(GridViewRowInfo row, object value)
    {
      GridViewDataRowInfo gridViewDataRowInfo = row as GridViewDataRowInfo;
      if (gridViewDataRowInfo != null && this.Template != null)
        this.Template.ListSource.NotifyItemChanging(row, this.column.Name);
      row.Cache[this.column] = value;
      if (gridViewDataRowInfo != null && this.Template != null)
        this.Template.ListSource.NotifyItemChanged(row, this.column.Name);
      if (this.Template.IsUpdating)
        return;
      row.InvalidateRow();
    }

    public void Dispose()
    {
      this.column = (GridViewColumn) null;
    }

    public virtual bool SubPropertyMode
    {
      get
      {
        return this.subPropertyMode;
      }
    }
  }
}
