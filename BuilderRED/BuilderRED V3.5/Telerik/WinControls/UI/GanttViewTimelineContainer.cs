// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTimelineContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewTimelineContainer : VirtualizedStackContainer<GanttViewTimelineDataItem>
  {
    private GanttViewGraphicalViewElement owner;
    private SizeF availableSize;
    private SizeF desiredSize;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.ItemSpacing = -1;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Orientation = Orientation.Horizontal;
    }

    public GanttViewTimelineContainer(GanttViewGraphicalViewElement owner)
    {
      this.owner = owner;
    }

    public GanttViewGraphicalViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.availableSize = availableSize;
      this.desiredSize = SizeF.Empty;
      return base.BeginMeasure(availableSize);
    }

    protected override SizeF EndMeasure()
    {
      if (this.Children.Count > 0)
      {
        if (this.Orientation == Orientation.Horizontal)
        {
          this.desiredSize.Width -= (float) this.ItemSpacing;
          this.desiredSize.Width = Math.Min(this.desiredSize.Width, this.availableSize.Width);
        }
        else
        {
          this.desiredSize.Height -= (float) this.ItemSpacing;
          this.desiredSize.Height = Math.Min(this.desiredSize.Height, this.availableSize.Height);
        }
      }
      return this.desiredSize;
    }

    protected override bool MeasureElement(
      IVirtualizedElement<GanttViewTimelineDataItem> element)
    {
      GanttViewTimelineItemElement timelineItemElement = element as GanttViewTimelineItemElement;
      if (timelineItemElement == null)
        return false;
      SizeF sizeF = this.MeasureElementCore((RadElement) timelineItemElement, this.availableSize);
      this.desiredSize.Height = Math.Max(this.desiredSize.Height, sizeF.Height);
      this.desiredSize.Width += sizeF.Width;
      return (double) this.desiredSize.Width - (double) this.Owner.TimelineScroller.Scrollbar.Value < (double) this.availableSize.Width;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF rectangleF = new RectangleF(PointF.Empty, finalSize);
      float x = rectangleF.X;
      foreach (RadElement child in this.Children)
      {
        RectangleF arrangeRect = new RectangleF(x, rectangleF.Y, child.DesiredSize.Width, finalSize.Height);
        arrangeRect.X -= (float) this.Owner.TimelineScroller.ScrollOffset;
        this.ArrangeElementCore(child, finalSize, arrangeRect);
        x += child.DesiredSize.Width + (float) this.ItemSpacing;
      }
      return finalSize;
    }
  }
}
