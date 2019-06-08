// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public sealed class RowAccessibleObject : System.Windows.Forms.Control.ControlAccessibleObject
  {
    private GridViewRowInfo row;
    private RadGridView owner;
    private RadGridViewAccessibleObject parent;
    private bool isXP;

    public RowAccessibleObject(
      RadGridView owner,
      GridViewRowInfo row,
      RadGridViewAccessibleObject parent)
      : base((System.Windows.Forms.Control) owner)
    {
      this.owner = owner;
      this.parent = parent;
      this.row = row;
      this.isXP = Environment.OSVersion.Version.Major <= 5;
      if (owner.CurrentColumn != null)
        this.GetChild(owner.CurrentColumn.Index);
      owner.CurrentColumnChanged += new CurrentColumnChangedEventHandler(this.owner_CurrentColumnChanged);
    }

    private void owner_CurrentColumnChanged(object sender, CurrentColumnChangedEventArgs e)
    {
      int num = this.row.IsSelected ? 1 : 0;
    }

    public RadGridView Control
    {
      get
      {
        return this.owner;
      }
    }

    public override string Name
    {
      get
      {
        int num = this.owner.ChildRows.IndexOf(this.row);
        if (this.isXP || this.owner.EnableCodedUITests)
        {
          if (!string.IsNullOrEmpty(this.parent.CellDescription))
            return this.parent.CellDescription;
          return "Row " + (object) num;
        }
        string str1 = this.owner.CurrentColumn == null ? (string) null : this.owner.CurrentColumn.HeaderText;
        object obj = this.owner.CurrentCell == null ? (object) null : this.owner.CurrentCell.Value;
        string str2 = obj != null ? obj.ToString() : " DBNull";
        if (obj == null && this.owner.CurrentRow is GridViewGroupRowInfo)
          return "Row " + ((GridViewGroupRowInfo) this.owner.CurrentRow).HeaderText.ToString();
        return "Row " + (object) (num + 1) + " Column " + str1 + " Value " + str2;
      }
      set
      {
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.Row;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return this.owner.RectangleToScreen(this.parent.GetRowElement(this.row));
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return (AccessibleObject) this.parent;
      }
    }

    public override void DoDefaultAction()
    {
      int num = this.owner.EnableCodedUITests ? 1 : 0;
    }

    public override AccessibleObject GetFocused()
    {
      if (this.owner.CurrentRow == this.row)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    public override AccessibleObject GetSelected()
    {
      if (this.owner.CurrentRow == this.row)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    public override void Select(AccessibleSelection flags)
    {
      switch (flags)
      {
        case AccessibleSelection.TakeFocus:
        case AccessibleSelection.TakeSelection:
        case AccessibleSelection.ExtendSelection:
        case AccessibleSelection.AddSelection:
          int num = this.owner.EnableCodedUITests ? 1 : 0;
          break;
      }
      base.Select(flags);
    }

    public override int GetChildCount()
    {
      if (this.row is GridViewGroupRowInfo)
        return 0;
      return this.row.Cells.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      if (this.owner.TableElement == null)
        return (AccessibleObject) null;
      if (index >= 0 && index < this.GetChildCount())
        return this.parent.GetCellAccessibleObject(this.row.Cells[index], this);
      return (AccessibleObject) null;
    }

    public override bool Equals(object obj)
    {
      if (obj == null || (object) this.GetType() != (object) obj.GetType())
        return false;
      return this.Name == ((AccessibleObject) obj).Name;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    internal int ID
    {
      get
      {
        for (int index = 0; index < this.owner.RowCount - 1; ++index)
        {
          if (this.parent.GetChild(index).Equals((object) this))
            return index;
        }
        return -1;
      }
    }

    public override AccessibleObject Navigate(AccessibleNavigation navdir)
    {
      return this.parent.NavigateFromChild(this, navdir);
    }

    internal AccessibleObject NavigateFromChild(
      CellAccessibleObject child,
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

    public Rectangle GetCellElement(GridViewRowInfo row, GridViewColumn col)
    {
      IRowView rowView = this.owner.GridViewElement.GetRowView(row.ViewInfo);
      if (rowView != null)
      {
        GridCellElement cellElement = rowView.GetCellElement(row, col);
        if (cellElement != null)
          return cellElement.ControlBoundingRectangle;
      }
      return new Rectangle(0, 0, 0, 0);
    }
  }
}
