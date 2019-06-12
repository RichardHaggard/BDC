// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlTabStripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class LayoutControlTabStripElement : RadPageViewStripElement
  {
    public LayoutControlTabStripItem SelectedTab
    {
      get
      {
        return (LayoutControlTabStripItem) this.SelectedItem;
      }
    }

    protected override void SetSelectedContent(RadPageViewItem item)
    {
      foreach (LayoutControlTabStripItem controlTabStripItem in (IEnumerable<RadPageViewItem>) this.Items)
      {
        ElementVisibility elementVisibility;
        bool flag;
        if (item == controlTabStripItem)
        {
          elementVisibility = ElementVisibility.Visible;
          flag = true;
        }
        else
        {
          elementVisibility = ElementVisibility.Collapsed;
          flag = false;
        }
        controlTabStripItem.LayoutGroupItem.Visibility = elementVisibility;
        foreach (LayoutControlItemBase layoutControlItemBase in controlTabStripItem.LayoutGroupItem.Items)
          layoutControlItemBase.IsHidden = !flag;
        controlTabStripItem.LayoutGroupItem.Bounds = new Rectangle(Point.Empty, this.ContentArea.Size);
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadPageViewStripElement);
      }
    }

    public void SetSelectedGroup(LayoutControlGroupItem value)
    {
      foreach (LayoutControlTabStripItem controlTabStripItem in (IEnumerable<RadPageViewItem>) this.Items)
      {
        if (controlTabStripItem.LayoutGroupItem == value)
        {
          this.SelectedItem = (RadPageViewItem) controlTabStripItem;
          break;
        }
      }
    }

    protected internal override void CloseItem(RadPageViewItem item)
    {
      base.CloseItem(item);
      if (this.ElementTree == null)
        return;
      LayoutControlTabStripItem controlTabStripItem = item as LayoutControlTabStripItem;
      RadLayoutControl control = this.ElementTree.Control as RadLayoutControl;
      if (controlTabStripItem == null || control == null)
        return;
      control.RemoveItem((LayoutControlItemBase) controlTabStripItem.LayoutGroupItem);
    }
  }
}
