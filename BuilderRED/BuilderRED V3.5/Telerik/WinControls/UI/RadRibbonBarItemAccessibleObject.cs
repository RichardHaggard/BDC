// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarItemAccessibleObject
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
  public class RadRibbonBarItemAccessibleObject : RadControlAccessibleObject
  {
    private RadItem owner;
    private RadRibbonBarAccessibleObject parent;

    public RadRibbonBarItemAccessibleObject(
      RadItem owner,
      RadRibbonBarAccessibleObject parent,
      string name)
      : base(owner.ElementTree.Control, name)
    {
      this.owner = owner;
      this.parent = parent;
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        if (this.parent.RibbonBar.EnableCodedUITests)
          return base.Name;
        return this.parent.GetRibbonSelectedItem()?.Text;
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
        if (!(this.owner is RadButtonElement))
          return this.owner.Text;
        RadButtonElement owner = this.owner as RadButtonElement;
        return "RadButton ;" + owner.Text + ";" + (object) owner.BackColor.A + ", " + (object) owner.BackColor.B + ", " + (object) owner.BackColor.G + ";" + (object) owner.ForeColor.A + ", " + (object) owner.ForeColor.B + ", " + (object) owner.ForeColor.G + ";" + (object) owner.BorderElement.BackColor.A + ", " + (object) owner.BorderElement.BackColor.B + ", " + (object) owner.BorderElement.BackColor.G + ";" + (object) owner.IsPressed;
      }
    }

    public override AccessibleStates State
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        AccessibleStates accessibleStates = AccessibleStates.Focusable;
        if (this.owner.ContainsMouse || this.owner.IsMouseOver)
          accessibleStates |= AccessibleStates.Focused;
        if (this.owner is RadToggleButtonElement)
        {
          switch (((RadToggleButtonElement) this.owner).ToggleState)
          {
            case Telerik.WinControls.Enumerations.ToggleState.On:
              accessibleStates |= AccessibleStates.Checked;
              break;
            case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
              accessibleStates |= AccessibleStates.Mixed;
              break;
          }
          if (this.owner.IsMouseDown)
            accessibleStates |= AccessibleStates.Pressed;
        }
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
        return new Rectangle(this.owner.PointToScreen(this.owner.Location), this.owner.Size);
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        if (this.owner is RibbonTab)
          return AccessibleRole.PageTab;
        if (this.owner is RadDropDownButtonElement)
          return AccessibleRole.ButtonMenu;
        if (this.owner is RadCheckBoxElement)
          return AccessibleRole.CheckButton;
        if (this.owner is RadRadioButtonElement)
          return AccessibleRole.RadioButton;
        return this.owner is RadDropDownListElement ? AccessibleRole.ComboBox : AccessibleRole.PushButton;
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
