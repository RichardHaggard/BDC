// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  internal class PropertyGridGroupAccessibleObject : PropertyGridItemBaseAccessibleObject
  {
    private PropertyGridGroupItem group;

    public PropertyGridGroupAccessibleObject(
      PropertyGridGroupItem group,
      RadPropertyGridAccessibilityInstance parent)
      : base(parent)
    {
      this.group = group;
    }

    public PropertyGridGroupItem Group
    {
      get
      {
        return this.group;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        PropertyGridGroupElement element = this.GetElement<PropertyGridGroupElement>((PropertyGridItemBase) this.group);
        if (element != null)
          return new Rectangle(this.Control.PointToScreen(element.ControlBoundingRectangle.Location), element.ControlBoundingRectangle.Size);
        return Rectangle.Empty;
      }
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Row;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void DoDefaultAction()
    {
      this.Control.SelectedGridItem = (PropertyGridItemBase) this.group;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return this.Control.IsDisposed ? 0 : 1;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      return (AccessibleObject) new PropertyGridCellAccessibleObject(this.group.Label, (PropertyGridItemBaseAccessibleObject) this);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetFocused()
    {
      if (this.Control.SelectedGridItem == this.group)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetSelected()
    {
      if (this.Control.SelectedGridItem == this.group)
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
          this.Control.SelectedGridItem = (PropertyGridItemBase) this.group;
          break;
      }
      base.Select(flags);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject HitTest(int x, int y)
    {
      PropertyGridGroupElement elementAtPoint = this.Control.RootElement.ElementTree.GetElementAtPoint(this.Control.PointToClient(new Point(x, y))) as PropertyGridGroupElement;
      if (elementAtPoint != null)
        return (AccessibleObject) new PropertyGridCellAccessibleObject(elementAtPoint.Data.Label, (PropertyGridItemBaseAccessibleObject) this);
      return base.HitTest(x, y);
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.RowDescrption();
      }
    }

    private string RowDescrption()
    {
      return "This is a group row with Index " + (object) this.Control.Groups.IndexOf(this.group);
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.Control.SelectedGridItem == this.group)
          accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
        return accessibleStates;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.RowDescrption();
      }
      set
      {
      }
    }

    public override string Value
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return "";
      }
      set
      {
        base.Value = value;
      }
    }
  }
}
