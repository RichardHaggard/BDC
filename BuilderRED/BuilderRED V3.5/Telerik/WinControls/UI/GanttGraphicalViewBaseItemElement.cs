// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttGraphicalViewBaseItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public abstract class GanttGraphicalViewBaseItemElement : GanttViewBaseItemElement
  {
    private GanttViewGraphicalViewElement graphicalViewElement;
    private GanttGraphicalViewBaseTaskElement taskElement;
    private GanttViewTaskLinkHandleElement leftLinkHandleElement;
    private GanttViewTaskLinkHandleElement rightLinkHandleElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.AllowDrop = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.taskElement = this.CreateTaskElement();
      this.leftLinkHandleElement = this.CreateLeftHandleElement();
      this.rightLinkHandleElement = this.CreateRightHandleElement();
      this.Children.Add((RadElement) this.taskElement);
      this.Children.Add((RadElement) this.leftLinkHandleElement);
      this.Children.Add((RadElement) this.rightLinkHandleElement);
    }

    public GanttGraphicalViewBaseItemElement(GanttViewGraphicalViewElement graphicalViewElement)
    {
      this.graphicalViewElement = graphicalViewElement;
    }

    protected abstract GanttGraphicalViewBaseTaskElement CreateTaskElement();

    protected virtual GanttViewTaskLinkHandleElement CreateLeftHandleElement()
    {
      return new GanttViewTaskLinkHandleElement();
    }

    protected virtual GanttViewTaskLinkHandleElement CreateRightHandleElement()
    {
      return new GanttViewTaskLinkHandleElement();
    }

    public GanttViewGraphicalViewElement GraphicalViewElement
    {
      get
      {
        return this.graphicalViewElement;
      }
    }

    public GanttGraphicalViewBaseTaskElement TaskElement
    {
      get
      {
        return this.taskElement;
      }
    }

    public GanttViewTaskLinkHandleElement LeftLinkHandleElement
    {
      get
      {
        return this.leftLinkHandleElement;
      }
    }

    public GanttViewTaskLinkHandleElement RightLinkHandleElement
    {
      get
      {
        return this.rightLinkHandleElement;
      }
    }

    protected override void PaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (this.ElementTree.Control.Enabled)
        return;
      base.PaintChildren(graphics, clipRectange, angle, scale, useRelativeTransformation);
    }

    public override void Attach(GanttViewDataItem data, object context)
    {
      int num = (int) this.TaskElement.BindProperty(GanttGraphicalViewBaseTaskElement.SelectedProperty, (RadObject) data, GanttViewDataItem.SelectedProperty, PropertyBindingOptions.TwoWay);
      base.Attach(data, context);
      this.InvalidateMeasure();
      this.Invalidate();
    }

    public override void Detach()
    {
      int num = (int) this.TaskElement.UnbindProperty(GanttGraphicalViewBaseTaskElement.SelectedProperty);
      base.Detach();
    }

    public override void Synchronize()
    {
      base.Synchronize();
      this.GraphicalViewElement.GanttViewElement.OnGraphicalViewItemFormatting(new GanttViewGraphicalViewItemFormattingEventArgs(this.Data, this));
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.GraphicalViewElement.GanttViewElement.ReadOnly || !e.Property.Name.Contains("IsMouseOverElement") || this.GraphicalViewElement.GanttViewElement.SelectedLink != null && (this.GraphicalViewElement.GanttViewElement.SelectedLink.StartItem == this.Data || this.GraphicalViewElement.GanttViewElement.SelectedLink.EndItem == this.Data))
        return;
      this.LeftLinkHandleElement.Visibility = this.IsMouseOverElement ? ElementVisibility.Visible : ElementVisibility.Hidden;
      this.RightLinkHandleElement.Visibility = this.IsMouseOverElement ? ElementVisibility.Visible : ElementVisibility.Hidden;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      float width = (float) ((this.Data.End - this.Data.Start).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
      if (this.TaskElement is GanttViewMilestoneElement)
        this.TaskElement.Measure(new SizeF(clientRectangle.Height, clientRectangle.Height));
      else
        this.TaskElement.Measure(new SizeF(width, clientRectangle.Height));
      SizeF availableSize1 = new SizeF(Math.Min((float) this.GraphicalViewElement.LinksHandlesSize.Width, clientRectangle.Width), Math.Min((float) this.GraphicalViewElement.LinksHandlesSize.Height, clientRectangle.Height));
      this.LeftLinkHandleElement.Measure(availableSize1);
      this.RightLinkHandleElement.Measure(availableSize1);
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float x = (float) ((this.Data.Start - this.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds) - (float) this.GraphicalViewElement.HorizontalScrollBarElement.Value;
      float width = (float) ((this.Data.End - this.Data.Start).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
      if (this.TaskElement is GanttViewMilestoneElement)
        width = clientRectangle.Height;
      SizeF sizeF = new SizeF(Math.Min((float) this.GraphicalViewElement.LinksHandlesSize.Width, clientRectangle.Width), Math.Min((float) this.GraphicalViewElement.LinksHandlesSize.Height, clientRectangle.Height));
      this.LeftLinkHandleElement.Arrange(new RectangleF(x - sizeF.Width, clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) sizeF.Height) / 2.0), sizeF.Width, sizeF.Height));
      this.taskElement.Arrange(new RectangleF(x, clientRectangle.Y, width, clientRectangle.Height));
      this.RightLinkHandleElement.Arrange(new RectangleF(x + width, clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) sizeF.Height) / 2.0), sizeF.Width, sizeF.Height));
      return finalSize;
    }
  }
}
