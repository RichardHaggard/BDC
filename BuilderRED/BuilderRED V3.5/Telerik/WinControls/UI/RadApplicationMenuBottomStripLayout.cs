// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuBottomStripLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadApplicationMenuBottomStripLayout : StackLayoutPanel
  {
    private bool autoHideMenuOnClick = true;

    [DefaultValue(true)]
    public bool AutoHideMenuOnClick
    {
      get
      {
        return this.autoHideMenuOnClick;
      }
      set
      {
        this.autoHideMenuOnClick = value;
      }
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      if (changeOperation == ItemsChangeOperation.Inserted)
      {
        RadItem radItem = child as RadItem;
        if (radItem != null)
          radItem.Click += new EventHandler(this.item_Click);
      }
      if (changeOperation == ItemsChangeOperation.Removed)
      {
        RadItem radItem = child as RadItem;
        if (radItem != null)
          radItem.Click -= new EventHandler(this.item_Click);
      }
      if (changeOperation != ItemsChangeOperation.Clearing)
        return;
      foreach (RadElement child1 in this.Children)
      {
        RadItem radItem = child1 as RadItem;
        if (radItem != null)
          radItem.Click -= new EventHandler(this.item_Click);
      }
    }

    private void item_Click(object sender, EventArgs e)
    {
      RadApplicationMenu control = this.ElementTree.Control as RadApplicationMenu;
      if (control == null || !this.autoHideMenuOnClick)
        return;
      control.DropDownButtonElement.DropDownMenu.ClosePopup(RadPopupCloseReason.CloseCalled);
    }
  }
}
