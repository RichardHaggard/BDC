// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewMilestoneItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewMilestoneItemElement : GanttGraphicalViewBaseItemElement
  {
    public GanttViewMilestoneItemElement(GanttViewGraphicalViewElement owner)
      : base(owner)
    {
    }

    protected override GanttGraphicalViewBaseTaskElement CreateTaskElement()
    {
      return (GanttGraphicalViewBaseTaskElement) new GanttViewMilestoneElement();
    }

    public override bool IsCompatible(GanttViewDataItem data, object context)
    {
      if (data != null)
        return data.Start == data.End;
      return false;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float num = (float) ((this.Data.Start - this.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds) - (float) this.GraphicalViewElement.HorizontalScrollBarElement.Value;
      float height = clientRectangle.Height;
      float x = num - height / 2f;
      SizeF sizeF = new SizeF(Math.Min((float) this.GraphicalViewElement.LinksHandlesSize.Width, clientRectangle.Width), Math.Min((float) this.GraphicalViewElement.LinksHandlesSize.Height, clientRectangle.Height));
      this.LeftLinkHandleElement.Arrange(new RectangleF(x - sizeF.Width, clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) sizeF.Height) / 2.0), sizeF.Width, sizeF.Height));
      this.TaskElement.Arrange(new RectangleF(x, clientRectangle.Y, height, clientRectangle.Height));
      this.RightLinkHandleElement.Arrange(new RectangleF(x + height, clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) sizeF.Height) / 2.0), sizeF.Width, sizeF.Height));
      return finalSize;
    }
  }
}
