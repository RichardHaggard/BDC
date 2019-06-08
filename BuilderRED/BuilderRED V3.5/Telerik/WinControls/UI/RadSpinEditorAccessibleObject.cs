// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinEditorAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadSpinEditorAccessibleObject : RadControlAccessibleObject
  {
    private RadSpinEditor owner;

    public RadSpinEditorAccessibleObject(RadSpinEditor owner, string name)
      : base((Control) owner, name)
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
            return (AccessibleObject) new RadTextBoxElementAccessibleObject(this.owner.SpinElement.TextBoxItem);
          case 1:
            return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.owner.SpinElement.ButtonUp, "SpinElement.ButtonUp");
          case 2:
            return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) this.owner.SpinElement.ButtonDown, "SpinElement.ButtonDown");
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
        AccessibleRole accessibleRole = this.Owner.AccessibleRole;
        if (accessibleRole != AccessibleRole.Default)
          return accessibleRole;
        return AccessibleRole.WhiteSpace;
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
