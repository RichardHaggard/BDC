// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadMenuAccessibleObject : Control.ControlAccessibleObject
  {
    private RadMenu owner;

    public RadMenuAccessibleObject(RadMenu owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.MenuBar;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return this.owner.Items.Count;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject GetChild(int index)
    {
      return (AccessibleObject) (this.owner.Items[index] as RadMenuItemBase).AccessibleObject;
    }

    public override AccessibleObject HitTest(int x, int y)
    {
      RadElement elementAtPoint = this.owner.ElementTree.GetElementAtPoint(this.owner.PointToClient(new Point(x, y)));
      RadMenuItemBase radMenuItemBase = (RadMenuItemBase) null;
      if (elementAtPoint != null)
        radMenuItemBase = elementAtPoint.FindAncestor<RadMenuItemBase>();
      if (radMenuItemBase != null)
        return (AccessibleObject) radMenuItemBase.AccessibleObject;
      return base.HitTest(x, y);
    }
  }
}
