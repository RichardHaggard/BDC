// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownPopupFormAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadDropDownPopupFormAccessibleObject : Control.ControlAccessibleObject
  {
    public RadDropDownPopupFormAccessibleObject(DropDownPopupForm form)
      : base((Control) form)
    {
      form.OwnerDropDownListElement.ListElement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ListElement_SelectedIndexChanged);
    }

    private void ListElement_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.List;
      }
    }

    protected DropDownPopupForm DropDown
    {
      get
      {
        return this.Owner as DropDownPopupForm;
      }
    }

    protected RadDropDownListElement List
    {
      get
      {
        return this.DropDown.OwnerElement as RadDropDownListElement;
      }
    }

    public override int GetChildCount()
    {
      return (this.DropDown.OwnerElement as RadDropDownListElement).ListElement.Items.Count;
    }

    public override AccessibleObject HitTest(int x, int y)
    {
      RadListVisualItem elementAtPoint = this.DropDown.ElementTree.GetElementAtPoint(this.DropDown.PointToClient(new Point(x, y))) as RadListVisualItem;
      if (elementAtPoint != null)
        return this.GetItemAccessibleObject(elementAtPoint.Data);
      return base.HitTest(x, y);
    }

    public override AccessibleObject GetChild(int index)
    {
      return this.GetItemAccessibleObject((this.DropDown.OwnerElement as RadDropDownListElement).ListElement.Items[index]);
    }

    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public override AccessibleObject Navigate(AccessibleNavigation direction)
    {
      return (AccessibleObject) null;
    }

    private AccessibleObject GetItemAccessibleObject(RadListDataItem item)
    {
      if (this.DropDown.OwnerDropDownListElement != null)
      {
        RadDropDownList control = this.DropDown.OwnerDropDownListElement.ElementTree.Control as RadDropDownList;
        if (control != null)
        {
          RadDropDownListAccessibleObject accessibilityObject = control.AccessibilityObject as RadDropDownListAccessibleObject;
          if (accessibilityObject != null)
            return (AccessibleObject) accessibilityObject.GetItemAccessibleObject(item);
        }
      }
      return (AccessibleObject) new RadListDataItemAccessibleObject(item, (AccessibleObject) this);
    }
  }
}
