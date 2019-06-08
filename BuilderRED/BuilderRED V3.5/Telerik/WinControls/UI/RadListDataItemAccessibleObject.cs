// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListDataItemAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadListDataItemAccessibleObject : RadItemAccessibleObject
  {
    private RadListDataItem item;
    private AccessibleObject parent;

    public RadListDataItemAccessibleObject(RadListDataItem item, AccessibleObject parent)
    {
      this.item = item;
      this.parent = parent;
    }

    public override object Owner
    {
      get
      {
        return (object) this.item;
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
        if (this.SelectedDataItem == this.item)
          accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
        if (this.item.VisualItem != null && this.item.VisualItem.ContainsMouse)
          accessibleStates |= AccessibleStates.HotTracked;
        if (this.Bounds == Rectangle.Empty)
          accessibleStates |= AccessibleStates.Offscreen;
        return accessibleStates;
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
        return this.parent;
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
        if (this.item.VisualItem != null)
          return new Rectangle(this.item.OwnerControl.PointToScreen(this.item.VisualItem.ControlBoundingRectangle.Location), this.item.VisualItem.ControlBoundingRectangle.Size);
        return Rectangle.Empty;
      }
    }

    protected RadListDataItem SelectedDataItem
    {
      get
      {
        if (this.item.OwnerControl.IsDisposed)
          return (RadListDataItem) null;
        if (this.item.OwnerControl is RadListControl)
          return (this.item.OwnerControl as RadListControl).SelectedItem;
        if (this.item.OwnerControl is RadDropDownList)
          return (this.item.OwnerControl as RadDropDownList).SelectedItem;
        if (this.item.Owner != null)
          return this.item.Owner.SelectedItem;
        return (RadListDataItem) null;
      }
      set
      {
        if (this.item.OwnerControl is RadListControl)
          (this.item.OwnerControl as RadListControl).SelectedItem = value;
        else if (this.item.OwnerControl is RadDropDownList)
        {
          (this.item.OwnerControl as RadDropDownList).SelectedItem = value;
        }
        else
        {
          if (this.item.Owner == null)
            return;
          this.item.Owner.SelectedItem = value;
        }
      }
    }

    public override string Description
    {
      get
      {
        return "RadListItem ;" + this.Name + ";" + (object) this.GetChildCount();
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void DoDefaultAction()
    {
      this.SelectedDataItem = this.item;
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
          this.SelectedDataItem = this.item;
          break;
      }
      base.Select(flags);
    }

    public override AccessibleObject Navigate(AccessibleNavigation navdir)
    {
      int num = this.item.Owner.Items.IndexOf(this.item);
      switch (navdir)
      {
        case AccessibleNavigation.Up:
        case AccessibleNavigation.Previous:
          int index1 = num - 1;
          if (index1 >= 0 && index1 < this.item.Owner.Items.Count)
            return this.GetItemAccessibleObject(this.item.Owner.Items[index1]);
          break;
        case AccessibleNavigation.Down:
        case AccessibleNavigation.Next:
          int index2 = num + 1;
          if (index2 >= 0 && index2 < this.item.Owner.Items.Count)
            return this.GetItemAccessibleObject(this.item.Owner.Items[index2]);
          break;
      }
      return (AccessibleObject) null;
    }

    private AccessibleObject GetItemAccessibleObject(RadListDataItem item)
    {
      RadListControlAccessibleObject parent1 = this.parent as RadListControlAccessibleObject;
      RadDropDownListAccessibleObject parent2 = this.parent as RadDropDownListAccessibleObject;
      if (parent1 != null)
        return (AccessibleObject) parent1.GetItemAccessibleObject(item);
      if (parent2 != null)
        return (AccessibleObject) parent2.GetItemAccessibleObject(item);
      return (AccessibleObject) new RadListDataItemAccessibleObject(item, (AccessibleObject) null);
    }
  }
}
