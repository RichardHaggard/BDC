// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewHeaderRowAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewHeaderRowAccessibleObject : AccessibleObject
  {
    private DetailListViewColumnContainer row;
    private RadListViewElement owner;

    public ListViewHeaderRowAccessibleObject(
      DetailListViewColumnContainer row,
      RadListViewElement owner)
    {
      this.row = row;
      this.owner = owner;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.Row;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.row != null && this.row.ContainsMouse)
          accessibleStates |= AccessibleStates.HotTracked;
        if (this.Bounds == Rectangle.Empty)
          accessibleStates |= AccessibleStates.Offscreen;
        return accessibleStates;
      }
    }

    public override string Description
    {
      get
      {
        return "This is a list view item header row";
      }
    }

    public override string Help
    {
      get
      {
        return "Currently there is no help available";
      }
    }

    public override AccessibleObject Parent
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.row.ElementTree.Control.AccessibilityObject;
      }
    }

    public override string Name
    {
      get
      {
        return "List View Header Row";
      }
      set
      {
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return this.row.ControlBoundingRectangle;
      }
    }

    public override int GetChildCount()
    {
      return this.owner.Columns.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      return (AccessibleObject) new ListViewHeaderCellAccessibleObject(this.owner.Columns[index], (AccessibleObject) this);
    }
  }
}
