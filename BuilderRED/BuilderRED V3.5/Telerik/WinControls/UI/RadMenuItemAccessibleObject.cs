// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuItemAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadMenuItemAccessibleObject : RadItemAccessibleObject, IDisposable
  {
    private RadMenuItemBase owner;
    private bool disposed;

    public RadMenuItemAccessibleObject(RadMenuItemBase owner)
    {
      this.owner = owner;
      this.owner.DropDownOpened += new EventHandler(this.Owner_DropDownOpened);
      this.owner.DropDownClosed += new RadPopupClosedEventHandler(this.Owner_DropDownClosed);
    }

    ~RadMenuItemAccessibleObject()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      this.owner.DropDownOpened -= new EventHandler(this.Owner_DropDownOpened);
      this.owner.DropDownClosed -= new RadPopupClosedEventHandler(this.Owner_DropDownClosed);
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.owner.ElementTree.Control.PointToScreen(this.owner.ControlBoundingRectangle.Location), this.owner.Size);
      }
    }

    public override string Description
    {
      get
      {
        return this.owner.AccessibleDescription;
      }
    }

    public override string Name
    {
      get
      {
        return this.owner.AccessibleName;
      }
      set
      {
        this.owner.AccessibleName = value;
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.MenuItem;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        if (!this.owner.Enabled)
          return base.State;
        AccessibleStates accessibleStates1 = AccessibleStates.Default;
        AccessibleStates accessibleStates2 = !this.owner.Selected ? accessibleStates1 | AccessibleStates.Focusable : accessibleStates1 | AccessibleStates.Focused;
        RadMenuItem owner = this.owner as RadMenuItem;
        if (owner != null && owner.IsChecked)
          accessibleStates2 |= AccessibleStates.Checked;
        return accessibleStates2;
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return this.owner.ElementTree.Control.AccessibilityObject;
      }
    }

    private void Owner_DropDownOpened(object sender, EventArgs e)
    {
      (this.owner.DropDown.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.SystemMenuPopupStart);
    }

    private void Owner_DropDownClosed(object sender, RadPopupClosedEventArgs args)
    {
      (this.owner.DropDown.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.SystemMenuPopupEnd);
    }

    public override void DoDefaultAction()
    {
      this.owner.PerformClick();
    }

    public override int GetChildCount()
    {
      if (this.owner == null)
        return 0;
      return this.owner.Items.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      RadDropDownMenu dropDown = this.owner.DropDown;
      if (dropDown != null && !dropDown.IsLoaded)
      {
        dropDown.LoadElementTree();
        dropDown.SetTheme();
        dropDown.RootElement.InvalidateMeasure(true);
        dropDown.RootElement.UpdateLayout();
      }
      return (AccessibleObject) (this.owner.Items[index] as RadMenuItemBase).AccessibleObject;
    }

    public override object Owner
    {
      get
      {
        return (object) this.owner;
      }
    }
  }
}
