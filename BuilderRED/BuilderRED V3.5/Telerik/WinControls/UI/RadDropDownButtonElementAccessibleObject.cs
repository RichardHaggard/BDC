// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownButtonElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  public class RadDropDownButtonElementAccessibleObject : RadControlAccessibleObject
  {
    private RadDropDownButtonElement buttonElement;

    public RadDropDownButtonElementAccessibleObject(
      RadDropDownButtonElement buttonElement,
      string name)
      : base(buttonElement.ElementTree.Control, name)
    {
      this.buttonElement = buttonElement;
      this.buttonElement.DropDownOpened += new EventHandler(this.buttonElement_DropDownOpened);
      this.buttonElement.DropDownClosed += new EventHandler(this.buttonElement_DropDownClosed);
      this.buttonElement.DropDownMenu.ItemSelected += new ItemSelectedEventHandler(this.DropDownMenu_ItemSelected);
    }

    public override object OwnerElement
    {
      get
      {
        return (object) this.buttonElement;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.ButtonDropDown;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.buttonElement.ElementTree.Control.PointToScreen(this.buttonElement.ControlBoundingRectangle.Location), this.buttonElement.ControlBoundingRectangle.Size);
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return this.buttonElement.ElementTree.Control.AccessibilityObject;
      }
    }

    private void buttonElement_DropDownOpened(object sender, EventArgs e)
    {
      (this.buttonElement.DropDownMenu.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.SystemMenuPopupStart);
    }

    private void DropDownMenu_ItemSelected(object sender, ItemSelectedEventArgs args)
    {
      (this.buttonElement.DropDownMenu.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.Selection);
    }

    private void buttonElement_DropDownClosed(object sender, EventArgs e)
    {
      (this.buttonElement.DropDownMenu.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.SystemMenuPopupEnd);
    }

    public override int GetChildCount()
    {
      return this.buttonElement.Items.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      RadDropDownMenu dropDownMenu = this.buttonElement.DropDownMenu;
      if (dropDownMenu != null && !dropDownMenu.IsLoaded)
      {
        dropDownMenu.LoadElementTree();
        dropDownMenu.SetTheme();
        dropDownMenu.RootElement.InvalidateMeasure(true);
        dropDownMenu.RootElement.UpdateLayout();
      }
      return (AccessibleObject) (this.buttonElement.Items[index] as RadMenuItemBase).AccessibleObject;
    }
  }
}
