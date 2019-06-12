// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls.UI
{
  public class GridViewCellInfo : IEquatable<GridViewCellInfo>
  {
    private GridViewRowInfo row;
    private GridViewColumn column;
    private GridViewCellInfoCollection owner;
    private string errorText;
    private GridViewCellStyle style;
    private bool readOnly;
    private object tag;

    public GridViewCellInfo(
      GridViewRowInfo row,
      GridViewColumn column,
      GridViewCellInfoCollection owner)
    {
      this.row = row;
      this.column = column;
      this.owner = owner;
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.RowInfo.ViewTemplate;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.RowInfo.ViewInfo;
      }
    }

    public GridViewRowInfo RowInfo
    {
      get
      {
        return this.row;
      }
    }

    public GridViewColumn ColumnInfo
    {
      get
      {
        return this.column;
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.RowInfo.ViewTemplate.MasterTemplate.SelectedCells.IsSelected(this.row, this.column);
      }
      set
      {
        if (this.IsSelected == value)
          return;
        if (value)
        {
          if (!this.RowInfo.ViewTemplate.MasterTemplate.MultiSelect && this.RowInfo.ViewTemplate.MasterTemplate.SelectedCells.Count > 0)
            this.RowInfo.ViewTemplate.MasterTemplate.SelectedCells.Clear();
          this.RowInfo.ViewTemplate.MasterTemplate.SelectedCells.Add(this);
        }
        else
          this.RowInfo.ViewTemplate.MasterTemplate.SelectedCells.Remove(this);
      }
    }

    public bool IsCurrent
    {
      get
      {
        if (this.ColumnInfo != null && this.RowInfo != null && this.ColumnInfo.IsCurrent)
          return this.RowInfo.IsCurrent;
        return false;
      }
    }

    public object Value
    {
      get
      {
        return this.RowInfo[this.ColumnInfo];
      }
      set
      {
        object obj = this.Value;
        if (object.Equals(obj, value))
          return;
        ValueChangingEventArgs args = new ValueChangingEventArgs(value, obj);
        this.ViewTemplate.EventDispatcher.RaiseEvent<ValueChangingEventArgs>(EventDispatcher.ValueChanging, (object) this, args);
        if (args.Cancel)
          return;
        GridViewGroupRowInfo parent1 = this.RowInfo.Parent as GridViewGroupRowInfo;
        this.RowInfo[this.ColumnInfo] = RadDataConverter.Instance.Parse((IDataConversionInfoProvider) (this.ColumnInfo as GridViewDataColumn), value);
        if (this.RowInfo is GridViewNewRowInfo)
          this.RowInfo.IsModified = true;
        GridViewGroupRowInfo parent2 = this.RowInfo.Parent as GridViewGroupRowInfo;
        if (parent1 != null && parent2 != null && parent1 != parent2)
        {
          this.ViewTemplate.InvalidateGroupSummaryRows(parent1.Group, true);
          this.ViewTemplate.InvalidateGroupSummaryRows(parent2.Group, true);
        }
        this.ViewTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.ValueChanged, (object) this, EventArgs.Empty);
        this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCellEventArgs>(EventDispatcher.CellValueChanged, (object) this, new GridViewCellEventArgs(this.row, this.column, (IInputEditor) null));
      }
    }

    public string ErrorText
    {
      get
      {
        if (!string.IsNullOrEmpty(this.errorText))
          return this.errorText;
        if (!string.IsNullOrEmpty(this.ColumnInfo.FieldName))
          return this.RowInfo.GetErrorText(this.ColumnInfo.FieldName);
        return string.Empty;
      }
      set
      {
        if (!(this.errorText != value))
          return;
        this.errorText = value;
        this.PersistCellInfo();
        this.RowInfo.InvalidateRow();
      }
    }

    public GridViewCellStyle Style
    {
      get
      {
        if (this.style == null)
          this.style = new GridViewCellStyle(this);
        return this.style;
      }
      internal set
      {
        this.style = value;
      }
    }

    public bool HasStyle
    {
      get
      {
        return this.style != null;
      }
    }

    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        if (this.readOnly == value)
          return;
        this.readOnly = value;
        this.PersistCellInfo();
      }
    }

    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.PersistCellInfo();
      }
    }

    public bool Equals(GridViewCellInfo cellInfo)
    {
      if (cellInfo != null && this.ColumnInfo == cellInfo.ColumnInfo)
        return this.RowInfo == cellInfo.RowInfo;
      return false;
    }

    public void EnsureVisible()
    {
      GridViewTemplate viewTemplate = this.row.ViewTemplate;
      viewTemplate.OnViewChanged((object) viewTemplate, new DataViewChangedEventArgs(ViewChangedAction.EnsureCellVisible, (IList) new object[2]
      {
        (object) this.row,
        (object) this.column
      }));
    }

    public void BeginEdit()
    {
      GridViewTemplate viewTemplate = this.row.ViewTemplate;
      if (!this.row.IsCurrent || !this.column.IsCurrent)
        GridViewSynchronizationService.RaiseCurrentChanged(viewTemplate, this.row, this.column, true);
      viewTemplate.OnViewChanged((object) viewTemplate, new DataViewChangedEventArgs(ViewChangedAction.BeginEdit, (IList) new object[2]
      {
        (object) this.row,
        (object) this.column
      }));
    }

    public void EndEdit()
    {
      GridViewTemplate viewTemplate = this.row.ViewTemplate;
      if (!this.row.IsCurrent || !this.column.IsCurrent)
        return;
      viewTemplate.OnViewChanged((object) viewTemplate, new DataViewChangedEventArgs(ViewChangedAction.EndEdit, (IList) new object[2]
      {
        (object) this.row,
        (object) this.column
      }));
    }

    internal void PersistCellInfo()
    {
      this.row.PersistCellInfoCollection(ref this.owner);
      if (this.owner.SavedCells.ContainsKey(this.ColumnInfo.GetHashCode()))
        return;
      this.owner.SavedCells.Add(this.ColumnInfo.GetHashCode(), this);
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
