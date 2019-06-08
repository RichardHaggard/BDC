// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadButtonAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadButtonAccessibleObject : Control.ControlAccessibleObject
  {
    private RadButton owner;

    public RadButtonAccessibleObject(Control owner)
      : base(owner)
    {
      this.owner = (RadButton) owner;
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override void DoDefaultAction()
    {
      ((RadButtonBase) this.Owner).PerformClick();
    }

    public override AccessibleStates State
    {
      get
      {
        AccessibleStates state = base.State;
        if (((RadButtonBase) this.Owner).ButtonElement.IsMouseDown)
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
        return "RadButton ;" + this.Owner.Text + ";" + (object) this.Owner.BackColor.A + ", " + (object) this.Owner.BackColor.B + ", " + (object) this.Owner.BackColor.G + ";" + (object) this.Owner.ForeColor.A + ", " + (object) this.Owner.ForeColor.B + ", " + (object) this.Owner.ForeColor.G + ";" + (object) this.owner.ButtonElement.BorderElement.BackColor.A + ", " + (object) this.owner.ButtonElement.BorderElement.BackColor.B + ", " + (object) this.owner.ButtonElement.BorderElement.BackColor.G + ";" + (object) this.owner.ButtonElement.IsPressed;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return RadLabelAccessibleObject.StripHtmlLikeFormatting(this.owner.Text);
      }
      set
      {
        this.owner.Name = value;
      }
    }
  }
}
