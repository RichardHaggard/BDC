// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownMenuAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadDropDownMenuAccessibleObject : Control.ControlAccessibleObject
  {
    private RadDropDownMenu owner;

    public RadDropDownMenuAccessibleObject(RadDropDownMenu owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.MenuPopup;
      }
    }

    public override string Name
    {
      get
      {
        if (this.owner.OwnerElement == null)
          return "DropDown";
        return (this.owner.OwnerElement as RadItem).AccessibleName + "DropDown";
      }
      set
      {
        (this.owner.OwnerElement as RadItem).AccessibleName = value;
      }
    }

    public override int GetChildCount()
    {
      return this.owner.Items.Count;
    }

    public override AccessibleObject HitTest(int x, int y)
    {
      RadElement elementAtPoint = this.owner.ElementTree.GetElementAtPoint(this.owner.PointToClient(new Point(x, y)));
      RadMenuItemBase radMenuItemBase = (RadMenuItemBase) null;
      if (elementAtPoint != null)
        radMenuItemBase = elementAtPoint.FindAncestor<RadMenuItemBase>();
      if (radMenuItemBase != null)
        return (AccessibleObject) radMenuItemBase.AccessibleObject;
      return base.HitTest(x, y);
    }

    public override AccessibleObject GetChild(int index)
    {
      RadMenuItemBase radMenuItemBase = this.owner.Items[index] as RadMenuItemBase;
      if (radMenuItemBase != null)
        return (AccessibleObject) radMenuItemBase.AccessibleObject;
      return base.GetChild(index);
    }
  }
}
