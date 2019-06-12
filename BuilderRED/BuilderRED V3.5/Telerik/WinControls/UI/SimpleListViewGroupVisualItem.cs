// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SimpleListViewGroupVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class SimpleListViewGroupVisualItem : BaseListViewGroupVisualItem
  {
    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Data == null)
        return SizeF.Empty;
      SizeF finalSize = base.MeasureOverride(LayoutUtils.InfinitySize);
      finalSize.Width += this.ToggleElement.DesiredSize.Width;
      if (this.Data.Size.Height > 0)
        finalSize.Height = (float) this.Data.Size.Height;
      if (this.Data.Size.Width > 0)
        finalSize.Width = (float) this.Data.Size.Width;
      RadListViewElement owner = this.Data.Owner;
      if (owner != null && !owner.AllowArbitraryItemHeight)
        finalSize.Height = (float) owner.GroupItemSize.Height;
      if (owner != null && !owner.AllowArbitraryItemWidth && !this.Data.Owner.FullRowSelect)
        finalSize.Width = (float) owner.GroupItemSize.Width;
      if (owner != null && owner.FullRowSelect)
        finalSize.Width = Math.Max(this.GetClientRectangle(availableSize).Width, finalSize.Width);
      this.Data.ActualSize = finalSize.ToSize();
      SizeF size = this.GetClientRectangle(finalSize).Size;
      this.ToggleElement.Measure(availableSize);
      this.Layout.Measure(new SizeF(size.Width - this.ToggleElement.DesiredSize.Width, size.Height));
      return finalSize;
    }
  }
}
