// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBoxAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadCheckBoxAccessibleObject : RadControlAccessibleObject
  {
    private RadCheckBox owner;

    public RadCheckBoxAccessibleObject(RadCheckBox owner, string name)
      : base((Control) owner, name)
    {
      this.owner = owner;
      this.owner.ToggleStateChanged += new StateChangedEventHandler(this.owner_ToggleStateChanged);
    }

    private void owner_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.NotifyClients(AccessibleEvents.Focus);
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.CheckButton;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return base.Description + this.owner.Text;
      }
      set
      {
        this.owner.Name = value;
      }
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Name;
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

    public override object OwnerElement
    {
      get
      {
        return (object) this.owner;
      }
    }

    public override void DoDefaultAction()
    {
      (this.OwnerElement as RadCheckBox).ToggleState = this.GetNextToggleState((this.OwnerElement as RadCheckBox).ToggleState);
    }

    private Telerik.WinControls.Enumerations.ToggleState GetNextToggleState(
      Telerik.WinControls.Enumerations.ToggleState toggleState)
    {
      switch (toggleState)
      {
        case Telerik.WinControls.Enumerations.ToggleState.On:
          return (this.OwnerElement as RadCheckBox).IsThreeState ? Telerik.WinControls.Enumerations.ToggleState.Indeterminate : Telerik.WinControls.Enumerations.ToggleState.Off;
        case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
          return Telerik.WinControls.Enumerations.ToggleState.Off;
        default:
          return Telerik.WinControls.Enumerations.ToggleState.On;
      }
    }
  }
}
