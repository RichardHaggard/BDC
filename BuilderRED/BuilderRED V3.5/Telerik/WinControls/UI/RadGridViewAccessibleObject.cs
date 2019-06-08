// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGridViewAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public sealed class RadGridViewAccessibleObject : System.Windows.Forms.Control.ControlAccessibleObject
  {
    private static readonly object syncObj = new object();
    private string cellDescription = "";
    private Dictionary<GridViewRowInfo, RowAccessibleObject> rowCache = new Dictionary<GridViewRowInfo, RowAccessibleObject>();
    private Dictionary<GridViewCellInfo, CellAccessibleObject> cellCache = new Dictionary<GridViewCellInfo, CellAccessibleObject>();
    private RadGridView owner;

    public RadGridViewAccessibleObject(RadGridView owner)
      : base((System.Windows.Forms.Control) owner)
    {
      this.owner = owner;
    }

    public RadGridView Control
    {
      get
      {
        return this.owner;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.Table;
      }
    }

    public override int GetChildCount()
    {
      return this.owner.ChildRows.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      if (index >= 0 && index < this.owner.ChildRows.Count)
        return this.GetRowAccessibleObject(this.owner.ChildRows[index]);
      return (AccessibleObject) null;
    }

    public override string Name
    {
      get
      {
        return "Telerik.WinControls.UI.RadGridView ; " + (this.owner.RowCount + 1).ToString() + ";" + (this.owner.ColumnCount + 1).ToString() + this.Description;
      }
      set
      {
        this.owner.Name = value;
      }
    }

    internal AccessibleObject NavigateFromChild(
      RowAccessibleObject child,
      AccessibleNavigation navdir)
    {
      switch (navdir)
      {
        case AccessibleNavigation.Up:
        case AccessibleNavigation.Previous:
          return this.GetChild(child.ID - 1);
        case AccessibleNavigation.Down:
        case AccessibleNavigation.Next:
          return this.GetChild(child.ID + 1);
        default:
          return (AccessibleObject) null;
      }
    }

    private string CreateDescription(int rowIndex)
    {
      string str1 = this.Control.CurrentColumn == null ? (string) null : this.Control.CurrentColumn.HeaderText;
      object obj = this.Control.CurrentCell == null ? (object) null : this.Control.CurrentCell.Value;
      string str2 = obj != null ? obj.ToString() : " DBNull";
      string str3 = " Column " + str1 + " Value " + str2;
      return string.Empty;
    }

    public string CellDescription
    {
      get
      {
        return this.cellDescription;
      }
      set
      {
        this.cellDescription = value;
      }
    }

    public AccessibleObject GetRowAccessibleObject(GridViewRowInfo rowInfo)
    {
      lock (RadGridViewAccessibleObject.syncObj)
      {
        if (!this.rowCache.ContainsKey(rowInfo))
          this.rowCache.Add(rowInfo, new RowAccessibleObject(this.owner, rowInfo, this));
      }
      return (AccessibleObject) this.rowCache[rowInfo];
    }

    public AccessibleObject GetCellAccessibleObject(
      GridViewCellInfo cellInfo,
      RowAccessibleObject parent)
    {
      lock (RadGridViewAccessibleObject.syncObj)
      {
        if (!this.cellCache.ContainsKey(cellInfo))
          this.cellCache.Add(cellInfo, new CellAccessibleObject(this.owner, cellInfo, parent));
      }
      return (AccessibleObject) this.cellCache[cellInfo];
    }

    public Rectangle GetRowElement(GridViewRowInfo row)
    {
      IRowView rowView = this.owner.GridViewElement.GetRowView(row.ViewInfo);
      if (rowView != null)
      {
        GridRowElement rowElement = rowView.GetRowElement(row);
        if (rowElement != null)
          return rowElement.ControlBoundingRectangle;
      }
      return new Rectangle(0, 0, 0, 0);
    }
  }
}
