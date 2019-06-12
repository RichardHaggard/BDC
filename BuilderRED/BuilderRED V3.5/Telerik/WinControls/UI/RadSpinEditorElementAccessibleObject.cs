﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinEditorElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadSpinEditorElementAccessibleObject : AccessibleObject
  {
    private RadSpinElement owner;

    public RadSpinEditorElementAccessibleObject(RadSpinElement owner)
    {
      this.owner = owner;
    }

    public override AccessibleObject GetChild(int index)
    {
      if (index >= 0 && index < this.GetChildCount())
      {
        switch (index)
        {
          case 0:
            return (AccessibleObject) new RadTextBoxElementAccessibleObject(this.owner.TextBoxItem);
          case 1:
            return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.owner.ButtonUp, "SpinUpButton");
          case 2:
            return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.owner.ButtonDown, "SpinDownButton");
        }
      }
      return (AccessibleObject) null;
    }

    public override int GetChildCount()
    {
      return 3;
    }

    public override AccessibleRole Role
    {
      get
      {
        AccessibleRole accessibleRole = AccessibleRole.SpinButton;
        if (accessibleRole != AccessibleRole.Default)
          return accessibleRole;
        return AccessibleRole.SpinButton;
      }
    }
  }
}
