// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PageViewLayoutInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  internal class PageViewLayoutInfo
  {
    public bool vertical;
    public int itemSpacing;
    public int itemCount;
    public SizeF previousSize;
    public SizeF availableSize;
    public SizeF measuredSize;
    public float maxWidth;
    public float maxHeight;
    public float availableLength;
    public float paddingLength;
    public float borderLength;
    public float layoutLength;
    public PageViewItemSizeMode sizeMode;
    public PageViewContentOrientation contentOrientation;
    public LightVisualElement itemLayout;
    public List<PageViewItemSizeInfo> items;

    public PageViewLayoutInfo(LightVisualElement layout, SizeF available)
    {
      this.items = new List<PageViewItemSizeInfo>();
      this.itemLayout = layout;
      this.availableSize = available;
      this.previousSize = layout.PreviousConstraint;
      this.Update();
    }

    public virtual void Update()
    {
      this.sizeMode = (PageViewItemSizeMode) this.itemLayout.GetValue(RadPageViewElement.ItemSizeModeProperty);
      this.contentOrientation = (PageViewContentOrientation) this.itemLayout.GetValue(RadPageViewElement.ItemContentOrientationProperty);
      this.itemSpacing = (int) this.itemLayout.GetValue(RadPageViewElement.ItemSpacingProperty);
      this.vertical = this.GetIsVertical();
      this.availableLength = this.vertical ? this.availableSize.Height : this.availableSize.Width;
      this.paddingLength = this.vertical ? (float) this.itemLayout.Padding.Vertical : (float) this.itemLayout.Padding.Horizontal;
      Padding borderThickness = LightVisualElement.GetBorderThickness(this.itemLayout, true);
      this.borderLength = this.vertical ? (float) borderThickness.Vertical : (float) borderThickness.Horizontal;
      int num = 0;
      foreach (RadElement child in this.itemLayout.Children)
      {
        RadPageViewItem radPageViewItem = child as RadPageViewItem;
        if (radPageViewItem != null && radPageViewItem.Visibility != ElementVisibility.Collapsed)
        {
          radPageViewItem.Measure(this.GetMeasureSizeConstraint((RadItem) radPageViewItem));
          PageViewItemSizeInfo itemSizeInfo = this.CreateItemSizeInfo(radPageViewItem);
          itemSizeInfo.desiredSize = new SizeF(radPageViewItem.DesiredSize.Width, radPageViewItem.DesiredSize.Height);
          this.maxWidth = Math.Max(itemSizeInfo.desiredSize.Width, this.maxWidth);
          this.maxHeight = Math.Max(itemSizeInfo.desiredSize.Height, this.maxHeight);
          this.items.Add(itemSizeInfo);
          itemSizeInfo.itemIndex = num++;
        }
      }
      this.itemCount = this.items.Count;
    }

    public virtual PageViewItemSizeInfo CreateItemSizeInfo(RadPageViewItem item)
    {
      return new PageViewItemSizeInfo(item, this.vertical);
    }

    public virtual SizeF GetMeasureSizeConstraint(RadItem item)
    {
      return LayoutUtils.InfinitySize;
    }

    public virtual bool GetIsVertical()
    {
      return false;
    }
  }
}
