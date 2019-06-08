// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMaskedEditBoxAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadMaskedEditBoxAccessibleObject : RadControlAccessibleObject
  {
    private RadMaskedEditBox owner;

    public RadMaskedEditBoxAccessibleObject(RadMaskedEditBox owner, string name)
      : base((Control) owner, name)
    {
      this.owner = owner;
      this.owner.MaskedEditBoxElement.TextChanged += new EventHandler(this.owner_TextChanged);
    }

    private void owner_TextChanged(object sender, EventArgs e)
    {
      this.NotifyClients(AccessibleEvents.Focus);
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

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override int GetChildCount()
    {
      return 0;
    }

    public override string Description
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.Name;
      }
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

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.IpAddress;
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
