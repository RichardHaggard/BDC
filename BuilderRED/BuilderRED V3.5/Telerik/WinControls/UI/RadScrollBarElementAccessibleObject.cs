// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollBarElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadScrollBarElementAccessibleObject : Control.ControlAccessibleObject
  {
    protected RadScrollBarElement ownerElement;

    public RadScrollBarElementAccessibleObject(RadScrollBarElement ownerElement)
      : base(ownerElement.ElementTree.Control)
    {
      this.ownerElement = ownerElement;
    }

    public override AccessibleRole Role
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return AccessibleRole.ScrollBar;
      }
    }

    public override string Value
    {
      [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        return this.ownerElement.Value.ToString();
      }
      set
      {
        base.Value = value;
      }
    }

    public override string Name
    {
      get
      {
        return this.Value;
      }
      set
      {
        base.Name = value;
      }
    }

    public override string Description
    {
      get
      {
        return this.ownerElement.Value.ToString();
      }
    }

    public override int GetChildCount()
    {
      return 3;
    }

    public override AccessibleObject GetChild(int index)
    {
      switch (index)
      {
        case 0:
          return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.ownerElement.FirstButton, "srollbarElement.FirstButton");
        case 1:
          return (AccessibleObject) new RadItemElementAccessibleObject((RadItem) this.ownerElement.ThumbElement, AccessibleRole.Indicator, "srollbarElement.ThumbElement");
        case 2:
          return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.ownerElement.SecondButton, "srollbarElement.SecondButton");
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
