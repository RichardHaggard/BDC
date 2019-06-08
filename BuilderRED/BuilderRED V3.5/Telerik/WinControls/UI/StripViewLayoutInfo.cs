// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewLayoutInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class StripViewLayoutInfo : PageViewLayoutInfo
  {
    public StripViewAlignment align;
    public StripViewItemAlignment itemAlign;
    public StripViewItemFitMode fitMode;
    public int nonSystemItemCount;
    public SizeF pinnedItemsOffset;
    public SizeF previewItemSize;

    public StripViewLayoutInfo(LightVisualElement layout, SizeF available)
      : base(layout, available)
    {
    }

    public override void Update()
    {
      this.fitMode = (StripViewItemFitMode) this.itemLayout.GetValue(RadPageViewStripElement.ItemFitModeProperty);
      this.align = (StripViewAlignment) this.itemLayout.GetValue(RadPageViewStripElement.StripAlignmentProperty);
      this.itemAlign = (StripViewItemAlignment) this.itemLayout.GetValue(RadPageViewStripElement.ItemAlignmentProperty);
      base.Update();
    }

    public override PageViewItemSizeInfo CreateItemSizeInfo(RadPageViewItem item)
    {
      if (!item.IsSystemItem)
        ++this.nonSystemItemCount;
      return base.CreateItemSizeInfo(item);
    }

    public override bool GetIsVertical()
    {
      if (this.align != StripViewAlignment.Left)
        return this.align == StripViewAlignment.Right;
      return true;
    }
  }
}
