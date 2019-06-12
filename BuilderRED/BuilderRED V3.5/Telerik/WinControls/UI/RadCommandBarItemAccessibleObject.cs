// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarItemAccessibleObject
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
  public class RadCommandBarItemAccessibleObject : RadControlAccessibleObject
  {
    private RadCommandBarBaseItem owner;
    private RadCommandBarAccessibleObject parent;

    public RadCommandBarItemAccessibleObject(
      RadCommandBarBaseItem owner,
      RadCommandBarAccessibleObject parent,
      string name)
      : base(owner.ElementTree.Control, name)
    {
      this.owner = owner;
      this.parent = parent;
      this.owner.MouseDown += new MouseEventHandler(this.owner_MouseDown);
    }

    private void owner_MouseDown(object sender, MouseEventArgs e)
    {
      this.parent.NotifyClients(AccessibleEvents.Focus);
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Diagram;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.owner.Name;
      }
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] set
      {
        this.owner.Name = value;
      }
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable;
        if (this.owner.ContainsMouse)
          accessibleStates |= AccessibleStates.Focused;
        return accessibleStates;
      }
    }

    public override AccessibleObject Parent
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return (AccessibleObject) this.parent;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.owner.ElementTree.Control.PointToScreen(this.owner.ControlBoundingRectangle.Location), this.owner.Size);
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
