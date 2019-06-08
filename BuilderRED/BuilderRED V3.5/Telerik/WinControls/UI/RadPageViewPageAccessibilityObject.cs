// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewPageAccessibilityObject
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
  public class RadPageViewPageAccessibilityObject : RadControlAccessibleObject
  {
    private RadPageViewPage owner;
    private RadPageViewAccessibilityObject parent;

    public RadPageViewPageAccessibilityObject(
      RadPageViewPage owner,
      RadPageViewAccessibilityObject parent,
      string name)
      : base((Control) owner, name)
    {
      this.owner = owner;
      this.parent = parent;
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
        AccessibleStates accessibleStates = AccessibleStates.Focusable;
        if (this.parent.OwnerPageView.SelectedPage == this.owner)
          accessibleStates |= AccessibleStates.Focused;
        return accessibleStates;
      }
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.PageTab;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.parent.OwnerPageView.PointToScreen(this.owner.Item.ControlBoundingRectangle.Location), this.owner.Item.ControlBoundingRectangle.Size);
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Description + this.owner.Item.Text;
      }
      set
      {
        base.Name = value;
      }
    }

    public override object OwnerElement
    {
      get
      {
        return (object) this.owner;
      }
    }
  }
}
