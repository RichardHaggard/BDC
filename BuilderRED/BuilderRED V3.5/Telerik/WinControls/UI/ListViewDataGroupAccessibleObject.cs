// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataGroupAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewDataGroupAccessibleObject : AccessibleObject
  {
    private ListViewDataItemGroup group;

    public ListViewDataGroupAccessibleObject(ListViewDataItemGroup item)
    {
      this.group = item;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.ListItem;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.group.Selected)
          accessibleStates |= AccessibleStates.Selected;
        if (this.group.Current)
          accessibleStates |= AccessibleStates.Focused;
        BaseListViewVisualItem element = this.group.Owner.ViewElement.GetElement((ListViewDataItem) this.group);
        if (element != null && element.ContainsMouse)
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
        return "This is a list view group named: " + this.group.Text;
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
        return this.group.Owner.ElementTree.Control.AccessibilityObject;
      }
    }

    public override string Value
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.group.Text;
      }
    }

    public override string Name
    {
      get
      {
        return this.group.Text;
      }
      set
      {
        this.group.Text = value;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        BaseListViewVisualItem element = this.group.Owner.ViewElement.GetElement((ListViewDataItem) this.group);
        if (element != null)
          return new Rectangle(this.group.Owner.ElementTree.Control.PointToScreen(element.ControlBoundingRectangle.Location), element.ControlBoundingRectangle.Size);
        return Rectangle.Empty;
      }
    }

    public override int GetChildCount()
    {
      return 0;
    }

    public override AccessibleObject GetSelected()
    {
      if (!this.group.Selected)
        return (AccessibleObject) null;
      return (AccessibleObject) this;
    }

    public override AccessibleObject GetFocused()
    {
      if (!this.group.Current)
        return (AccessibleObject) null;
      return (AccessibleObject) this;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void Select(AccessibleSelection flags)
    {
      switch (flags)
      {
        case AccessibleSelection.TakeFocus:
          this.group.Owner.CurrentItem = (ListViewDataItem) this.group;
          break;
        case AccessibleSelection.TakeSelection:
        case AccessibleSelection.ExtendSelection:
        case AccessibleSelection.AddSelection:
          this.group.Owner.Select(new ListViewDataItem[1]
          {
            (ListViewDataItem) this.group
          });
          break;
      }
      base.Select(flags);
    }
  }
}
