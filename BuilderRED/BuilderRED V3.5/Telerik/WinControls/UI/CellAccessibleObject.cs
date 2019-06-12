// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CellAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.UI.Code.Accessiblity;

namespace Telerik.WinControls.UI
{
  public class CellAccessibleObject : RadItemAccessibleObject
  {
    private GridViewCellInfo cell;
    private RadGridView owner;
    private RowAccessibleObject parent;
    private GridViewColumn column;
    private GridViewRowInfo row;
    private bool isXP;
    private GridDropDownAccessibilityObject dropDownListAccessibleObject;

    public CellAccessibleObject(
      RadGridView owner,
      GridViewCellInfo cell,
      RowAccessibleObject parent)
    {
      this.owner = owner;
      this.parent = parent;
      this.cell = cell;
      this.column = cell.ColumnInfo;
      this.row = cell.RowInfo;
      this.isXP = Environment.OSVersion.Version.Major <= 5;
      this.owner.CellBeginEdit += new GridViewCellCancelEventHandler(this.owner_CellBeginEdit);
      this.owner.CellEndEdit += new GridViewCellEventHandler(this.owner_CellEndEdit);
    }

    public override string Name
    {
      get
      {
        if (this.isXP || this.owner.EnableCodedUITests)
          return this.cell.Value.ToString();
        int num = this.owner.ChildRows.IndexOf(this.cell.RowInfo);
        string str1 = this.owner.CurrentColumn == null ? (string) null : this.owner.CurrentColumn.HeaderText;
        object obj = this.owner.CurrentCell == null ? (object) null : this.owner.CurrentCell.Value;
        string str2 = obj != null ? obj.ToString() : " DBNull";
        if (obj == null && this.owner.CurrentRow is GridViewGroupRowInfo)
          str2 = ((GridViewGroupRowInfo) this.owner.CurrentRow).HeaderText.ToString();
        return "Row " + (object) (num + 1) + " Column " + str1 + " Value " + str2;
      }
      set
      {
      }
    }

    public override string Description
    {
      get
      {
        return "Telerik.WinControls.UI.GridViewCellInfo ;" + this.cell.Value + ";" + (object) this.cell.Style.BackColor.A + ", " + (object) this.cell.Style.BackColor.B + ", " + (object) this.cell.Style.BackColor.G + ";" + (object) this.cell.Style.ForeColor.A + ", " + (object) this.cell.Style.ForeColor.B + ", " + (object) this.cell.Style.ForeColor.G + ";" + (object) this.cell.Style.BorderColor.A + ", " + (object) this.cell.Style.BorderColor.B + ", " + (object) this.cell.Style.BorderColor.G + ";" + (object) this.cell.IsSelected;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.Cell;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return this.owner.RectangleToScreen(this.parent.GetCellElement(this.row, this.column));
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return (AccessibleObject) this.parent;
      }
    }

    public override int GetChildCount()
    {
      return !this.owner.IsInEditMode || this.row.Index < 0 || (!this.row.IsCurrent || !this.column.IsCurrent) ? 0 : 1;
    }

    public override AccessibleObject GetChild(int index)
    {
      if (this.owner.IsInEditMode && this.owner.CurrentRow == this.row && this.owner.CurrentColumn == this.column)
      {
        BaseGridEditor activeEditor = this.owner.ActiveEditor as BaseGridEditor;
        if (activeEditor != null)
        {
          RadElement editorElement = activeEditor.EditorElement;
          RadSpinEditorElement editor = editorElement as RadSpinEditorElement;
          if (editor != null)
            return (AccessibleObject) new RadGridSpinEditorElementAccessibleObject(this.owner, editor, this, editor.ControlBoundingRectangle.Size, new Point(editor.ControlBoundingRectangle.X, editor.ControlBoundingRectangle.Y), editor.Name);
          RadDropDownListEditorElement listEditorElement = editorElement as RadDropDownListEditorElement;
          if (listEditorElement != null)
          {
            if (this.dropDownListAccessibleObject != null)
              this.dropDownListAccessibleObject.UnwireEvents();
            this.dropDownListAccessibleObject = new GridDropDownAccessibilityObject((RadDropDownListElement) listEditorElement, this, listEditorElement.Name);
            return (AccessibleObject) this.dropDownListAccessibleObject;
          }
        }
      }
      return base.GetChild(index);
    }

    public override AccessibleObject GetFocused()
    {
      if (this.owner.CurrentColumn == this.column && this.owner.CurrentRow == this.row)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    public override AccessibleObject GetSelected()
    {
      if (this.owner.CurrentColumn == this.column && this.owner.CurrentRow == this.row)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.owner.GridViewElement.EditorManager.ActiveEditor is RadMaskedEditBoxEditor)
        {
          RadMaskedEditBoxEditor activeEditor = (RadMaskedEditBoxEditor) this.owner.GridViewElement.EditorManager.ActiveEditor;
        }
        if (this.owner.CurrentColumn != null && this.owner.CurrentColumn.Equals((object) this.column) && (this.owner.CurrentRow != null && this.owner.CurrentRow.Equals((object) this.row)))
          accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
        return accessibleStates;
      }
    }

    public override void Select(AccessibleSelection flags)
    {
      switch (flags)
      {
        case AccessibleSelection.TakeFocus:
        case AccessibleSelection.TakeSelection:
        case AccessibleSelection.ExtendSelection:
        case AccessibleSelection.AddSelection:
          if (!this.owner.EnableCodedUITests)
          {
            this.owner.CurrentColumn = this.column;
            this.owner.CurrentRow = this.row;
            break;
          }
          break;
      }
      base.Select(flags);
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
        for (int index = 0; index < this.parent.GetChildCount() - 1; ++index)
        {
          if (object.Equals((object) this.parent.GetChild(index), (object) this))
            return index;
        }
        return -1;
      }
    }

    public override AccessibleObject Navigate(AccessibleNavigation navdir)
    {
      return this.parent.NavigateFromChild(this, navdir);
    }

    private void owner_CellEndEdit(object sender, GridViewCellEventArgs e)
    {
      if (this.dropDownListAccessibleObject == null)
        return;
      this.dropDownListAccessibleObject.UnwireEvents();
    }

    private void owner_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
    {
      if (e.Column != this.cell.ColumnInfo || e.Row != this.cell.RowInfo)
        return;
      this.GetChild(0)?.Select(AccessibleSelection.TakeFocus);
    }

    public override object Owner
    {
      get
      {
        return (object) this.cell;
      }
    }
  }
}
