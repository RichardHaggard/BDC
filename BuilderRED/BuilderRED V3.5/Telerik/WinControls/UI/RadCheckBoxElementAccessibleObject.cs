// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBoxElementAccessibleObject
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
  public class RadCheckBoxElementAccessibleObject : RadControlAccessibleObject
  {
    private RadCheckBoxElement ownerElement;

    public RadCheckBoxElementAccessibleObject(RadCheckBoxElement ownerElement, string name)
      : base(ownerElement.ElementTree.Control, name)
    {
      this.ownerElement = ownerElement;
      this.ownerElement.ToggleStateChanged += new StateChangedEventHandler(this.owner_ToggleStateChanged);
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
        return base.Description + this.ownerElement.Text;
      }
      set
      {
        this.ownerElement.Name = value;
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
        switch (this.ownerElement.ToggleState)
        {
          case Telerik.WinControls.Enumerations.ToggleState.On:
            state |= AccessibleStates.Checked;
            break;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            state |= AccessibleStates.Mixed;
            break;
        }
        if (this.ownerElement.IsMouseDown)
          state |= AccessibleStates.Pressed;
        return state;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.ownerElement.ElementTree.Control.PointToScreen(this.ownerElement.ControlBoundingRectangle.Location), this.ownerElement.Size);
      }
    }

    public override object OwnerElement
    {
      get
      {
        return (object) this.ownerElement;
      }
    }
  }
}
