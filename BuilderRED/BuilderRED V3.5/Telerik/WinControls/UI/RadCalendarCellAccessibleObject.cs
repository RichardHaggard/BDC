// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendarCellAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadCalendarCellAccessibleObject : AccessibleObject
  {
    private RadCalendarAccessibleObject ownerAccessibleObject;
    private int column;
    private int row;

    public RadCalendarCellAccessibleObject(
      RadCalendarAccessibleObject ownerAccessibleObject,
      int column,
      int row)
    {
      this.ownerAccessibleObject = ownerAccessibleObject;
      this.column = column;
      this.row = row;
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Cell;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.ownerAccessibleObject.Owner.SelectedDate.ToString();
      }
      set
      {
      }
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Name;
      }
    }

    public override AccessibleObject Parent
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return (AccessibleObject) this.ownerAccessibleObject;
      }
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.ownerAccessibleObject.Owner.CurrentViewColumn == this.column && this.ownerAccessibleObject.Owner.CurrentViewRow == this.row)
          accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
        return accessibleStates;
      }
    }
  }
}
