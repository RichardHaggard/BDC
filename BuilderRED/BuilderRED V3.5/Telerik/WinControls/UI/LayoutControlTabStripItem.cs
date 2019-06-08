// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlTabStripItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class LayoutControlTabStripItem : RadPageViewStripItem
  {
    private LayoutControlGroupItem layoutGroupItem;

    public LayoutControlTabStripItem(LayoutControlGroupItem layoutGroupItem)
    {
      this.Text = layoutGroupItem.Text;
      this.layoutGroupItem = layoutGroupItem;
      this.layoutGroupItem.TextChanged += new EventHandler(this.contentGroup_TextChanged);
    }

    private void contentGroup_TextChanged(object sender, EventArgs e)
    {
      this.Text = this.layoutGroupItem.Text;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.layoutGroupItem.TextChanged -= new EventHandler(this.contentGroup_TextChanged);
    }

    protected override RadPageViewItemButtonsPanel CreateButtonsPanel()
    {
      return (RadPageViewItemButtonsPanel) new LayoutControlTabStripItemButtonsPanel(this);
    }

    public LayoutControlGroupItem LayoutGroupItem
    {
      get
      {
        return this.layoutGroupItem;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadPageViewStripItem);
      }
    }
  }
}
