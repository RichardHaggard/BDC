// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewGroupVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DetailListViewGroupVisualItem : BaseListViewGroupVisualItem
  {
    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Data == null)
        return SizeF.Empty;
      SizeF finalSize = base.MeasureOverride(availableSize);
      finalSize.Width = this.GetClientRectangle(availableSize).Width;
      if (this.Data.Size.Height > 0)
        finalSize.Height = (float) this.Data.Size.Height;
      RadListViewElement owner = this.Data.Owner;
      if (owner != null && !owner.AllowArbitraryItemHeight)
        finalSize.Height = (float) owner.GroupItemSize.Height;
      this.Data.ActualSize = finalSize.ToSize();
      SizeF size = this.GetClientRectangle(finalSize).Size;
      this.Layout.Measure(new SizeF(size.Width - this.ToggleElement.DesiredSize.Width, size.Height));
      return finalSize;
    }
  }
}
