// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownButtonAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadDropDownButtonAccessibleObject : Control.ControlAccessibleObject
  {
    private RadDropDownButton owner;

    public RadDropDownButtonAccessibleObject(RadDropDownButton owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.ButtonDropDown;
      }
    }

    public override string Name
    {
      get
      {
        return this.Description + RadLabelAccessibleObject.StripHtmlLikeFormatting(this.owner.Text);
      }
      set
      {
        this.owner.Name = value;
      }
    }

    public override int GetChildCount()
    {
      return 1;
    }

    public override AccessibleObject GetChild(int index)
    {
      RadDropDownButton owner = this.Owner as RadDropDownButton;
      string name = owner.Name;
      if (string.IsNullOrEmpty(name))
        name = "RadDropDownButton";
      if (owner is RadApplicationMenu)
        return (AccessibleObject) new RadApplicationMenuButtonElementAccessibleObject(owner.DropDownButtonElement as RadApplicationMenuButtonElement, name);
      return (AccessibleObject) new RadDropDownButtonElementAccessibleObject(owner.DropDownButtonElement, name);
    }
  }
}
