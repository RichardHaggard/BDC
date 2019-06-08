// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRadioButtonAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadRadioButtonAccessibleObject : Control.ControlAccessibleObject
  {
    public RadRadioButtonAccessibleObject(RadRadioButton owner)
      : base((Control) owner)
    {
      owner.ToggleStateChanged += new StateChangedEventHandler(this.owner_ToggleStateChanged);
    }

    private void owner_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.NotifyClients(AccessibleEvents.Focus);
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.RadioButton;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Owner.AccessibleName;
      }
      set
      {
        this.Owner.AccessibleName = value;
      }
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Owner.AccessibleDescription;
      }
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates state = base.State;
        switch (((RadToggleButton) this.Owner).ToggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.On:
            state |= AccessibleStates.Checked;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            state |= AccessibleStates.Mixed;
            break;
        }
        if (((RadControl) this.Owner).RootElement.IsMouseDown)
          state |= AccessibleStates.Pressed;
        return state;
      }
    }
  }
}
