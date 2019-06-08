// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewCellAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewCellAccessibleObject : AccessibleObject
  {
    private ListViewDataItem item;
    private ListViewDetailColumn column;
    private AccessibleObject parent;

    public ListViewCellAccessibleObject(
      ListViewDataItem item,
      ListViewDetailColumn column,
      AccessibleObject parent)
    {
      this.item = item;
      this.column = column;
      this.parent = parent;
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Cell;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return 0;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetFocused()
    {
      if (this.item.Owner.CurrentColumn == this.column && this.item.Owner.CurrentItem == this.item)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetSelected()
    {
      if (this.item.Owner.CurrentColumn == this.column && this.item.Owner.CurrentItem == this.item)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void Select(AccessibleSelection flags)
    {
      switch (flags)
      {
        case AccessibleSelection.TakeFocus:
        case AccessibleSelection.TakeSelection:
        case AccessibleSelection.ExtendSelection:
        case AccessibleSelection.AddSelection:
          this.item.Owner.CurrentColumn = this.column;
          this.item.Owner.CurrentItem = this.item;
          break;
      }
      base.Select(flags);
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.item.Owner.Items.IndexOf(this.item).ToString() + " ," + this.column.Name;
      }
      set
      {
      }
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.item.Owner.Items.IndexOf(this.item).ToString() + " ," + this.column.Name;
      }
    }

    public override AccessibleObject Parent
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.parent;
      }
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.item.Owner.CurrentColumn == this.column && this.item.Owner.CurrentItem == this.item)
          accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
        return accessibleStates;
      }
    }
  }
}
