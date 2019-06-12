// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ListElementProvider : VirtualizedPanelElementProvider<RadListDataItem, RadListVisualItem>
  {
    protected RadListElement listElement;

    public ListElementProvider(RadListElement listElement)
    {
      if (listElement == null)
        throw new ArgumentException("Owner element can not be null.");
      this.listElement = listElement;
    }

    public override IVirtualizedElement<RadListDataItem> CreateElement(
      RadListDataItem data,
      object context)
    {
      return (IVirtualizedElement<RadListDataItem>) (this.listElement.OnCreatingVisualListItem(!string.IsNullOrEmpty(this.listElement.DescriptionTextMember) || data is DescriptionTextListDataItem ? (RadListVisualItem) new DescriptionTextListVisualItem() : new RadListVisualItem()) ?? (!string.IsNullOrEmpty(this.listElement.DescriptionTextMember) ? (RadListVisualItem) new DescriptionTextListVisualItem() : new RadListVisualItem()));
    }

    public override SizeF GetElementSize(RadListDataItem item)
    {
      if (item.Owner == null)
        return SizeF.Empty;
      if (this.listElement.AutoSizeItems)
      {
        if (item.MeasuredSize == (SizeF) Size.Empty)
        {
          SizeF availableSize = new SizeF(float.PositiveInfinity, float.PositiveInfinity);
          if (this.listElement.FitItemsToSize && this.listElement.ViewElement.Size != Size.Empty)
            availableSize = new SizeF((float) (this.listElement.ViewElement.Size.Width - 15), (float) this.listElement.ViewElement.Size.Height);
          item.MeasuredSize = this.listElement.MeasureItem(item, availableSize);
        }
        return item.MeasuredSize;
      }
      if (item.Height != -1)
        return new SizeF(0.0f, (float) item.Height);
      if (this.listElement.IsDescriptionText && this.listElement.GetValueSource(RadListElement.ItemHeightProperty) < ValueSource.Style)
        return new SizeF(0.0f, (float) this.listElement.ItemHeight * 2f);
      return new SizeF(0.0f, (float) this.listElement.ItemHeight);
    }

    public override bool ShouldUpdate(
      IVirtualizedElement<RadListDataItem> element,
      RadListDataItem data,
      object context)
    {
      return base.ShouldUpdate(element, data, context);
    }
  }
}
