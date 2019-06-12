// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SimpleListViewContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class SimpleListViewContainer : BaseListViewContainer
  {
    private int maxDesiredWidth;

    public SimpleListViewContainer(BaseListViewElement owner)
      : base(owner)
    {
    }

    protected override RectangleF ArrangeElementCore(
      RadElement element,
      SizeF finalSize,
      RectangleF arrangeRect)
    {
      if (element is BaseListViewGroupVisualItem)
        return base.ArrangeElementCore(element, finalSize, arrangeRect);
      if (this.owner.FullRowSelect)
        arrangeRect.Width = (float) ((IVirtualizedElement<ListViewDataItem>) element).Data.ActualSize.Width;
      if (this.owner.Owner.ShowGroups && (this.owner.Owner.EnableCustomGrouping || this.owner.Owner.EnableGrouping) && (this.owner.Owner.Groups.Count > 0 && !this.owner.Owner.FullRowSelect))
      {
        arrangeRect.X += (float) this.owner.Owner.GroupIndent;
        arrangeRect.Width -= (float) this.owner.Owner.GroupIndent;
      }
      element.Arrange(arrangeRect);
      return arrangeRect;
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.maxDesiredWidth = 0;
      return base.BeginMeasure(availableSize);
    }

    protected override bool MeasureElement(IVirtualizedElement<ListViewDataItem> element)
    {
      bool flag = base.MeasureElement(element);
      this.maxDesiredWidth = Math.Max(this.maxDesiredWidth, element.Data.ActualSize.Width);
      this.cachedDesiredSize.Height += (float) element.Data.ActualSize.Height;
      this.cachedDesiredSize.Width = Math.Max(this.cachedDesiredSize.Width, (float) element.Data.ActualSize.Width);
      return flag;
    }

    protected override SizeF EndMeasure()
    {
      SizeF sizeF = base.EndMeasure();
      if (this.owner.FullRowSelect)
      {
        foreach (ListViewDataItem listViewDataItem in this.DataProvider)
          listViewDataItem.ActualSize = new Size(this.maxDesiredWidth, listViewDataItem.ActualSize.Height);
      }
      return sizeF;
    }
  }
}
