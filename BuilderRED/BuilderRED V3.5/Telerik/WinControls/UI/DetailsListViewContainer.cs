// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailsListViewContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DetailsListViewContainer : BaseListViewContainer
  {
    private float columnWidthSum;

    public DetailsListViewContainer(BaseListViewElement owner)
      : base(owner)
    {
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.columnWidthSum = 0.0f;
      foreach (ListViewDetailColumn column in (Collection<ListViewDetailColumn>) this.owner.Owner.Columns)
        this.columnWidthSum += column.Width;
      return base.BeginMeasure(availableSize);
    }

    protected override SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      if (element is BaseListViewGroupVisualItem)
      {
        element.Measure(new SizeF(this.columnWidthSum, availableSize.Height));
        this.cachedDesiredSize.Height += element.DesiredSize.Height;
        this.cachedDesiredSize.Width = Math.Max(this.cachedDesiredSize.Width, element.DesiredSize.Width);
        return element.DesiredSize;
      }
      SizeF sizeF = base.MeasureElementCore(element, availableSize);
      this.cachedDesiredSize.Height += sizeF.Height;
      this.cachedDesiredSize.Width = Math.Max(this.cachedDesiredSize.Width, sizeF.Width);
      return sizeF;
    }

    protected override RectangleF ArrangeElementCore(
      RadElement element,
      SizeF finalSize,
      RectangleF arrangeRect)
    {
      if (element is BaseListViewGroupVisualItem)
        element.PositionOffset = new SizeF((float) -(this.owner as DetailListViewElement).ColumnScrollBar.Value, 0.0f);
      return base.ArrangeElementCore(element, finalSize, arrangeRect);
    }
  }
}
