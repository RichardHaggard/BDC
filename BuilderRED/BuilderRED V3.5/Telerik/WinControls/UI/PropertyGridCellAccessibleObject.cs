// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridCellAccessibleObject
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
  internal class PropertyGridCellAccessibleObject : AccessibleObject
  {
    private string text;
    private PropertyGridItemBaseAccessibleObject parent;

    public PropertyGridCellAccessibleObject(
      string text,
      PropertyGridItemBaseAccessibleObject parent)
    {
      this.text = text;
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
      return this.parent.Control.IsEditing ? 1 : 0;
    }

    public override Rectangle Bounds
    {
      get
      {
        PropertyGridItemAccessibleObject parent1 = this.parent as PropertyGridItemAccessibleObject;
        if (parent1 != null)
        {
          PropertyGridItemElement element = parent1.GetElement<PropertyGridItemElement>((PropertyGridItemBase) parent1.Item);
          PropertyGridContentElement textElement = element.TextElement;
          if (textElement.Text == this.text)
            return new Rectangle(element.ElementTree.Control.PointToScreen(textElement.ControlBoundingRectangle.Location), textElement.Size);
          PropertyGridValueElement valueElement = element.ValueElement;
          if (valueElement.Text == this.text)
            return new Rectangle(element.ElementTree.Control.PointToScreen(valueElement.ControlBoundingRectangle.Location), valueElement.Size);
        }
        PropertyGridGroupAccessibleObject parent2 = this.parent as PropertyGridGroupAccessibleObject;
        if (parent2 != null)
        {
          PropertyGridGroupElement element = parent2.GetElement<PropertyGridGroupElement>((PropertyGridItemBase) parent2.Group);
          PropertyGridGroupTextElement textElement = element.TextElement;
          if (textElement.Text == this.text)
            return new Rectangle(element.ElementTree.Control.PointToScreen(textElement.ControlBoundingRectangle.Location), textElement.Size);
        }
        return base.Bounds;
      }
    }

    public override AccessibleObject GetChild(int index)
    {
      return base.GetChild(index);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetFocused()
    {
      PropertyGridGroupAccessibleObject parent = this.parent as PropertyGridGroupAccessibleObject;
      if (parent == null)
        return (AccessibleObject) null;
      if (((PropertyGridGroupAccessibleObject) this.parent).Group == parent.Control.SelectedGridItem)
        return (AccessibleObject) this;
      if (this.parent is PropertyGridItemAccessibleObject && ((PropertyGridItemAccessibleObject) this.parent).Item == parent.Control.SelectedGridItem)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetSelected()
    {
      PropertyGridGroupAccessibleObject parent = this.parent as PropertyGridGroupAccessibleObject;
      if (parent == null)
        return (AccessibleObject) null;
      if (((PropertyGridGroupAccessibleObject) this.parent).Group == parent.Control.SelectedGridItem)
        return (AccessibleObject) this;
      if (this.parent is PropertyGridItemAccessibleObject && ((PropertyGridItemAccessibleObject) this.parent).Item == parent.Control.SelectedGridItem)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void Select(AccessibleSelection flags)
    {
      PropertyGridItemBase propertyGridItemBase = !(this.parent is PropertyGridGroupAccessibleObject) ? (PropertyGridItemBase) ((PropertyGridItemAccessibleObject) this.parent).Item : (PropertyGridItemBase) ((PropertyGridGroupAccessibleObject) this.parent).Group;
      switch (flags)
      {
        case AccessibleSelection.TakeFocus:
        case AccessibleSelection.TakeSelection:
        case AccessibleSelection.ExtendSelection:
        case AccessibleSelection.AddSelection:
          this.parent.Control.SelectedGridItem = propertyGridItemBase;
          break;
      }
      base.Select(flags);
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.text;
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
        return (AccessibleObject) this.parent;
      }
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        PropertyGridItemBase propertyGridItemBase = !(this.parent is PropertyGridGroupAccessibleObject) ? (PropertyGridItemBase) ((PropertyGridItemAccessibleObject) this.parent).Item : (PropertyGridItemBase) ((PropertyGridGroupAccessibleObject) this.parent).Group;
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.parent.Control.SelectedGridItem == propertyGridItemBase)
          accessibleStates |= AccessibleStates.Selected | AccessibleStates.Focused;
        return accessibleStates;
      }
    }
  }
}
