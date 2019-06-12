// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollBarAccessibleObject
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
  public class RadScrollBarAccessibleObject : RadControlAccessibleObject
  {
    private RadScrollBar ownerControl;

    public RadScrollBarAccessibleObject(RadScrollBar owner, string name)
      : base((Control) owner, name)
    {
      this.ownerControl = owner;
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
        return (this.Owner as RadScrollBar).Value.ToString();
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
        return (this.Owner as RadScrollBar).Value.ToString();
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
          return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.ownerControl.ScrollBarElement.FirstButton, "ScrollBarElement.FirstButton");
        case 1:
          return (AccessibleObject) new RadItemElementAccessibleObject((RadItem) this.ownerControl.ScrollBarElement.ThumbElement, AccessibleRole.Indicator, "ScrollBarElement.ThumbElement");
        case 2:
          return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.ownerControl.ScrollBarElement.SecondButton, ".ScrollBarElement.SecondButton");
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public override object OwnerElement
    {
      get
      {
        return (object) this.ownerControl;
      }
    }
  }
}
