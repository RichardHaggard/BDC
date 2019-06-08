// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StackViewLayoutInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class StackViewLayoutInfo : PageViewLayoutInfo
  {
    public StackViewItemSelectionMode selectionMode;
    public StackViewPosition position;
    public PageViewItemSizeInfo selectedItem;

    public StackViewLayoutInfo(RadPageViewStackElement layout, SizeF availableSize)
      : base((LightVisualElement) layout, availableSize)
    {
    }

    public override void Update()
    {
      base.Update();
      this.selectionMode = (StackViewItemSelectionMode) this.itemLayout.GetValue(RadPageViewStackElement.ItemSelectionModeProperty);
      this.position = (StackViewPosition) this.itemLayout.GetValue(RadPageViewStackElement.StackPositionProperty);
    }

    public override PageViewItemSizeInfo CreateItemSizeInfo(RadPageViewItem item)
    {
      RadPageViewItem selectedItem = (this.itemLayout as RadPageViewElement).SelectedItem;
      PageViewItemSizeInfo itemSizeInfo = base.CreateItemSizeInfo(item);
      if (object.ReferenceEquals((object) selectedItem, (object) item))
        this.selectedItem = itemSizeInfo;
      return itemSizeInfo;
    }

    public override SizeF GetMeasureSizeConstraint(RadItem item)
    {
      float num = this.vertical ? (float) item.Margin.Horizontal : (float) item.Margin.Vertical;
      return new SizeF((this.vertical ? this.availableSize.Width : this.availableSize.Height) - num, float.PositiveInfinity);
    }

    public override bool GetIsVertical()
    {
      StackViewPosition stackPosition = (this.itemLayout as RadPageViewStackElement).StackPosition;
      if (stackPosition != StackViewPosition.Top)
        return stackPosition == StackViewPosition.Bottom;
      return true;
    }
  }
}
