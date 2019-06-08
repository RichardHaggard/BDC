// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadTextBoxAccessibleObject : Control.ControlAccessibleObject
  {
    private RadTextBox owner;

    public RadTextBoxAccessibleObject(RadTextBox owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.Text;
      }
    }

    public override string Name
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Description + this.owner.Text;
      }
      set
      {
        this.owner.Name = value;
      }
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return 0;
    }

    public override string Value
    {
      get
      {
        return this.owner.Text;
      }
      set
      {
        this.owner.Text = value;
      }
    }
  }
}
