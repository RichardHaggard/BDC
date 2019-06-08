// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItemAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class ListViewDataItemAccessibleObject : AccessibleObject
  {
    private ListViewDataItem item;

    public ListViewDataItemAccessibleObject(ListViewDataItem item)
    {
      this.item = item;
    }

    public ListViewDataItem Item
    {
      get
      {
        return this.item;
      }
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
        if (this.item.Selected)
          accessibleStates |= AccessibleStates.Selected;
        if (this.item.Current)
          accessibleStates |= AccessibleStates.Focused;
        BaseListViewVisualItem element = this.item.Owner.ViewElement.GetElement(this.item);
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
        return "This is a list view item named: " + this.item.Text;
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
        return this.item.Owner.ElementTree.Control.AccessibilityObject;
      }
    }

    public override string Value
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.item.Text;
      }
    }

    public override string Name
    {
      get
      {
        return this.item.Text;
      }
      set
      {
        this.item.Text = value;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        BaseListViewVisualItem element = this.item.Owner.ViewElement.GetElement(this.item);
        if (element != null)
          return new Rectangle(this.item.Owner.ElementTree.Control.PointToScreen(element.ControlBoundingRectangle.Location), element.ControlBoundingRectangle.Size);
        return Rectangle.Empty;
      }
    }

    public override AccessibleObject GetSelected()
    {
      if (!this.item.Selected)
        return (AccessibleObject) null;
      return (AccessibleObject) this;
    }

    public override AccessibleObject GetFocused()
    {
      if (!this.item.Current)
        return (AccessibleObject) null;
      return (AccessibleObject) this;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void Select(AccessibleSelection flags)
    {
      switch (flags)
      {
        case AccessibleSelection.TakeFocus:
          this.item.Owner.CurrentItem = this.item;
          break;
        case AccessibleSelection.TakeSelection:
        case AccessibleSelection.ExtendSelection:
        case AccessibleSelection.AddSelection:
          this.item.Owner.Select(new ListViewDataItem[1]
          {
            this.item
          });
          break;
      }
      base.Select(flags);
    }
  }
}
