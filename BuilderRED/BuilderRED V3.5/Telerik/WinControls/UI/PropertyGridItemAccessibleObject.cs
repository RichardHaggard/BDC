// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemAccessibleObject
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
  public class PropertyGridItemAccessibleObject : PropertyGridItemBaseAccessibleObject
  {
    private PropertyGridItem item;

    public PropertyGridItemAccessibleObject(
      PropertyGridItem item,
      RadPropertyGridAccessibilityInstance parent)
      : base(parent)
    {
      this.item = item;
    }

    public PropertyGridItem Item
    {
      get
      {
        return this.item;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        PropertyGridItemElement element = this.GetElement<PropertyGridItemElement>((PropertyGridItemBase) this.item);
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
      this.Control.SelectedGridItem = (PropertyGridItemBase) this.item;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return this.Control.IsDisposed ? 0 : 2;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      if (index == 0)
        return (AccessibleObject) new PropertyGridCellAccessibleObject(this.item.Label, (PropertyGridItemBaseAccessibleObject) this);
      return (AccessibleObject) new PropertyGridCellAccessibleObject(this.item.FormattedValue, (PropertyGridItemBaseAccessibleObject) this);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetFocused()
    {
      if (this.Control.SelectedGridItem == this.item)
        return (AccessibleObject) this;
      return (AccessibleObject) null;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetSelected()
    {
      if (this.Control.SelectedGridItem == this.item)
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
          this.Control.SelectedGridItem = (PropertyGridItemBase) this.item;
          break;
      }
      base.Select(flags);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject HitTest(int x, int y)
    {
      RadElement elementAtPoint = this.Control.RootElement.ElementTree.GetElementAtPoint(this.Control.PointToClient(new Point(x, y)));
      if (elementAtPoint == null)
        return base.HitTest(x, y);
      PropertyGridTextElement ancestor1 = elementAtPoint.FindAncestor<PropertyGridTextElement>();
      if (ancestor1 != null)
        return (AccessibleObject) new PropertyGridCellAccessibleObject(ancestor1.Text, (PropertyGridItemBaseAccessibleObject) this);
      PropertyGridValueElement ancestor2 = elementAtPoint.FindAncestor<PropertyGridValueElement>();
      if (ancestor2 != null)
        return (AccessibleObject) new PropertyGridCellAccessibleObject(((PropertyGridItem) ancestor2.VisualItem.Data).FormattedValue, (PropertyGridItemBaseAccessibleObject) this);
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
      return "This is a row with Index " + (object) this.Control.Items.IndexOf(this.item);
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
        if (this.Control.SelectedGridItem == this.item)
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
        if (this.item.Value == null)
          return "";
        return this.item.Value.ToString();
      }
      set
      {
        this.item.Value = (object) value;
      }
    }
  }
}
