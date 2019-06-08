// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonTabStripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RibbonTabStripElement : RadPageViewStripElement
  {
    private bool paintTabShadows;
    private RadRibbonBarCommandTabCollection tabItems;

    public RibbonTabStripElement()
    {
      this.tabItems = new RadRibbonBarCommandTabCollection((RadElement) this);
      this.tabItems.ItemTypes = new Type[1]
      {
        typeof (RibbonTab)
      };
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BypassLayoutPolicies = false;
      this.UpdateSelectedItemContent = false;
      this.StripButtons = StripViewButtons.None;
    }

    public bool PaintTabShadows
    {
      get
      {
        return this.paintTabShadows;
      }
      set
      {
        this.paintTabShadows = value;
      }
    }

    public RadRibbonBarCommandTabCollection TabItems
    {
      get
      {
        return this.tabItems;
      }
      set
      {
        this.tabItems = value;
      }
    }

    protected internal override void SetSelectedItem(RadPageViewItem item)
    {
      if (item != null && item.Visibility != ElementVisibility.Visible)
        return;
      base.SetSelectedItem(item);
    }

    protected override PointF CalcLayoutOffset(PointF startPoint)
    {
      startPoint.X = startPoint.X + (float) this.Margin.Left + (float) this.Location.X;
      startPoint.Y = startPoint.Y + (float) this.Margin.Top + (float) this.Location.Y;
      return startPoint;
    }

    protected override RectangleF PerformArrange(RectangleF clientRect)
    {
      if (this.ContentArea.Parent == this)
        return base.PerformArrange(clientRect);
      this.ArrangeItems(clientRect);
      return this.ArrangeContent(clientRect);
    }
  }
}
