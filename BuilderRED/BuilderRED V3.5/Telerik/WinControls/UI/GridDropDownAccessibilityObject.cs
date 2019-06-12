// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDropDownAccessibilityObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridDropDownAccessibilityObject : AccessibleObject
  {
    private RadDropDownListElement owner;
    private CellAccessibleObject cellAccessibleObject;
    private string name;

    public GridDropDownAccessibilityObject(
      RadDropDownListElement owner,
      CellAccessibleObject cellAccessibleObject,
      string name)
    {
      this.owner = owner;
      this.WireEvents();
      this.cellAccessibleObject = cellAccessibleObject;
      this.name = name;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.ComboBox;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        return AccessibleStates.Focused | AccessibleStates.Focusable;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return this.owner.ControlBoundingRectangle;
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return (AccessibleObject) this.cellAccessibleObject;
      }
    }

    public RadDropDownListElement DropDownListElement
    {
      get
      {
        return this.owner;
      }
    }

    public override string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public override string Value
    {
      get
      {
        if (this.DropDownListElement.DropDownStyle != RadDropDownStyle.DropDownList)
          return "";
        return this.DropDownListElement.Text;
      }
      set
      {
        this.DropDownListElement.Text = value;
      }
    }

    public override AccessibleObject GetChild(int index)
    {
      if (index == 0)
        return this.DropDownListElement.TextBox.TextBoxItem.HostedControl.AccessibilityObject;
      return this.DropDownListElement.Popup.AccessibilityObject;
    }

    public void UnwireEvents()
    {
      if (this.owner == null)
        return;
      this.owner.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.owner_SelectedIndexChanged);
    }

    public void WireEvents()
    {
      this.UnwireEvents();
      if (this.owner == null)
        return;
      this.owner.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.owner_SelectedIndexChanged);
    }

    public override int GetChildCount()
    {
      return 2;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern void NotifyWinEvent(int winEvent, HandleRef hwnd, int objType, int objID);

    public void NotifyClients(AccessibleEvents accEvent)
    {
      GridDropDownAccessibilityObject.NotifyWinEvent((int) accEvent, new HandleRef((object) this.DropDownListElement.Popup, this.DropDownListElement.Popup.Handle), -4, 0);
    }

    public void NotifyClients(AccessibleEvents accEvent, int childID)
    {
      GridDropDownAccessibilityObject.NotifyWinEvent((int) accEvent, new HandleRef((object) this.DropDownListElement.Popup, this.DropDownListElement.Popup.Handle), -4, childID + 1);
    }

    private void owner_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.NotifyClients(AccessibleEvents.Selection, this.DropDownListElement.SelectedIndex);
      this.NotifyClients(AccessibleEvents.Focus, this.DropDownListElement.SelectedIndex);
    }
  }
}
