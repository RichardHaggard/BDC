// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuButtonElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadApplicationMenuButtonElementAccessibleObject : RadDropDownButtonElementAccessibleObject
  {
    public RadApplicationMenuButtonElementAccessibleObject(
      RadApplicationMenuButtonElement owner,
      string name)
      : base((RadDropDownButtonElement) owner, name)
    {
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.ButtonMenu;
      }
    }

    public override string Name
    {
      get
      {
        return this.Description;
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
        return "Application Menu";
      }
    }

    public override int GetChildCount()
    {
      RadApplicationMenuDropDown dropDownMenu = (this.OwnerElement as RadApplicationMenuButtonElement).DropDownMenu as RadApplicationMenuDropDown;
      return base.GetChildCount() + dropDownMenu.RightColumnItems.Count + dropDownMenu.ButtonItems.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      RadApplicationMenuDropDown dropDownMenu = (this.OwnerElement as RadApplicationMenuButtonElement).DropDownMenu as RadApplicationMenuDropDown;
      int childCount = base.GetChildCount();
      if (index < childCount)
        return base.GetChild(index);
      index -= childCount;
      int count = dropDownMenu.RightColumnItems.Count;
      if (index < count)
        return (AccessibleObject) (dropDownMenu.RightColumnItems[index] as RadMenuItemBase).AccessibleObject;
      index -= count;
      return (AccessibleObject) (dropDownMenu.ButtonItems[index] as RadMenuItemBase).AccessibleObject;
    }
  }
}
