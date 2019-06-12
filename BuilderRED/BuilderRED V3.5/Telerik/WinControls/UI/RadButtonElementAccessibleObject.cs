// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadButtonElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadButtonElementAccessibleObject : RadControlAccessibleObject
  {
    private RadButtonItem owner;

    public RadButtonElementAccessibleObject(RadButtonItem owner, string name)
      : base(owner.ElementTree.Control, name)
    {
      this.owner = owner;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void DoDefaultAction()
    {
      this.owner.CallDoClick(EventArgs.Empty);
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates state = base.State;
        if (this.owner.IsMouseDown)
          state |= AccessibleStates.Pressed;
        return state;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.PushButton;
      }
    }

    public override string Description
    {
      get
      {
        return this.owner.Text;
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
