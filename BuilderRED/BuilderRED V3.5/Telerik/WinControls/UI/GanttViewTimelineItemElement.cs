// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTimelineItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTimelineItemElement : LightVisualElement, IVirtualizedElement<GanttViewTimelineDataItem>
  {
    private GanttViewTimelineDataItem data;
    private GanttViewGraphicalViewElement graphicalViewElement;
    private GanttViewTimelineItemTopElement topElement;
    private GanttViewTimelineItemBottomStackElement bottomElement;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.topElement = this.CreateTimelineItemTopElement();
      this.bottomElement = this.CreateTimelineItemBottomStackElement();
      this.bottomElement.StretchHorizontally = true;
      this.bottomElement.StretchVertically = true;
      this.Children.AddRange((RadElement) this.topElement, (RadElement) this.bottomElement);
    }

    public virtual GanttViewTimelineItemTopElement CreateTimelineItemTopElement()
    {
      return new GanttViewTimelineItemTopElement();
    }

    public virtual GanttViewTimelineItemBottomStackElement CreateTimelineItemBottomStackElement()
    {
      return new GanttViewTimelineItemBottomStackElement();
    }

    public GanttViewTimelineItemElement(
      GanttViewTimelineDataItem data,
      GanttViewGraphicalViewElement graphicalViewElement)
    {
      this.data = data;
      this.graphicalViewElement = graphicalViewElement;
    }

    public GanttViewGraphicalViewElement GraphicalViewElement
    {
      get
      {
        return this.graphicalViewElement;
      }
    }

    public GanttViewTimelineItemTopElement TopElement
    {
      get
      {
        return this.topElement;
      }
    }

    public GanttViewTimelineItemBottomStackElement BottomElement
    {
      get
      {
        return this.bottomElement;
      }
    }

    protected internal virtual void CalculateItems()
    {
      this.SuspendLayout();
      GanttTimelineCellsInfo timelineCellInfoForItem = this.GraphicalViewElement.TimelineBehavior.GetTimelineCellInfoForItem(this.Data, this.GraphicalViewElement.TimelineRange);
      while (this.bottomElement.Children.Count > timelineCellInfoForItem.NumberOfcells)
        this.bottomElement.Children.RemoveAt(0);
      while (this.bottomElement.Children.Count < timelineCellInfoForItem.NumberOfcells)
        this.bottomElement.Children.Add((RadElement) this.GraphicalViewElement.TimelineBehavior.CreateElement());
      this.topElement.Text = this.GraphicalViewElement.TimelineBehavior.GetTimelineTopElementText(this.Data);
      for (int index = 0; index < this.bottomElement.Children.Count; ++index)
        ((RadItem) this.bottomElement.Children[index]).Text = this.GraphicalViewElement.TimelineBehavior.GetTimelineBottomElementText(this.Data, index + timelineCellInfoForItem.StartIndex);
      this.ResumeLayout(true);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      float width = this.Data.Width - (float) this.GraphicalViewElement.TimelineContainer.ItemSpacing + availableSize.Width - clientRectangle.Width;
      this.topElement.Measure(new SizeF(width, clientRectangle.Height / 2f));
      this.bottomElement.Measure(new SizeF(width, clientRectangle.Height / 2f));
      return new SizeF(width, this.topElement.DesiredSize.Height + this.bottomElement.DesiredSize.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect = new RectangleF(clientRectangle.X, clientRectangle.Y, this.DesiredSize.Width, clientRectangle.Height / 2f);
      this.topElement.Arrange(finalRect);
      this.bottomElement.Arrange(new RectangleF(clientRectangle.X, finalRect.Bottom, this.DesiredSize.Width, clientRectangle.Height - finalRect.Height));
      return clientRectangle.Size;
    }

    public GanttViewTimelineDataItem Data
    {
      get
      {
        return this.data;
      }
    }

    public void Attach(GanttViewTimelineDataItem data, object context)
    {
      this.data = data;
      this.Synchronize();
    }

    public void Detach()
    {
      this.data = (GanttViewTimelineDataItem) null;
    }

    public void Synchronize()
    {
      this.CalculateItems();
      this.GraphicalViewElement.GanttViewElement.OnTimelineItemFormatting(new GanttViewTimelineItemFormattingEventArgs(this.Data, this));
    }

    public bool IsCompatible(GanttViewTimelineDataItem data, object context)
    {
      return true;
    }
  }
}
