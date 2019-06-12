// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewSummaryElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewSummaryElement : GanttGraphicalViewBaseTaskElement
  {
    private GanttViewSummaryLeftElement leftElement;
    private GanttViewSummaryRightElement rightElement;
    private GanttViewSummaryMiddleElement middleElement;
    private SummaryProgressIndicatorElement progressIndicatorElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CaptureOnMouseDown = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.leftElement = this.CreateLeftElement();
      this.rightElement = this.CreateRightElement();
      this.middleElement = this.CreateMiddleElement();
      this.progressIndicatorElement = this.CreateProgressIndicatorElement();
      this.Children.Add((RadElement) this.leftElement);
      this.Children.Add((RadElement) this.rightElement);
      this.Children.Add((RadElement) this.middleElement);
      this.Children.Add((RadElement) this.progressIndicatorElement);
      int num1 = (int) this.middleElement.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.leftElement, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.middleElement.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.rightElement, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
    }

    protected override void DisposeManagedResources()
    {
      int num = (int) this.middleElement.UnbindProperty(RadElement.IsMouseOverProperty);
      base.DisposeManagedResources();
    }

    protected virtual GanttViewSummaryMiddleElement CreateMiddleElement()
    {
      return new GanttViewSummaryMiddleElement();
    }

    protected virtual GanttViewSummaryLeftElement CreateLeftElement()
    {
      return new GanttViewSummaryLeftElement();
    }

    protected virtual GanttViewSummaryRightElement CreateRightElement()
    {
      return new GanttViewSummaryRightElement();
    }

    protected virtual SummaryProgressIndicatorElement CreateProgressIndicatorElement()
    {
      return new SummaryProgressIndicatorElement();
    }

    public SummaryProgressIndicatorElement ProgressIndicatorElement
    {
      get
      {
        return this.progressIndicatorElement;
      }
    }

    public GanttViewSummaryLeftElement LeftElement
    {
      get
      {
        return this.leftElement;
      }
    }

    public GanttViewSummaryRightElement RightElement
    {
      get
      {
        return this.rightElement;
      }
    }

    public GanttViewSummaryMiddleElement MiddleElement
    {
      get
      {
        return this.middleElement;
      }
    }

    public override bool CanBeResized()
    {
      GanttViewSummaryItemElement parent = this.Parent as GanttViewSummaryItemElement;
      if (parent != null && !parent.GraphicalViewElement.GanttViewElement.ReadOnly)
        return parent.GraphicalViewElement.GanttViewElement.AllowSummaryEditing;
      return base.CanBeResized();
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      GanttViewSummaryItemElement parent = this.Parent as GanttViewSummaryItemElement;
      if (parent != null)
        return parent.GraphicalViewElement.GanttViewElement.AllowSummaryEditing;
      return base.CanDragCore(dragStartPoint);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      this.leftElement.Measure(availableSize);
      this.rightElement.Measure(availableSize);
      this.middleElement.Measure(availableSize);
      this.ProgressIndicatorElement.Measure(new SizeF((float) (int) ((Decimal) clientRectangle.Width / new Decimal(100) * ((GanttViewBaseItemElement) this.Parent).Data.Progress), clientRectangle.Height));
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect1 = new RectangleF((PointF) Point.Empty, this.leftElement.DesiredSize);
      RectangleF finalRect2 = new RectangleF(clientRectangle.Right - this.rightElement.DesiredSize.Width, clientRectangle.Y, this.rightElement.DesiredSize.Width, this.rightElement.DesiredSize.Height);
      this.leftElement.Arrange(finalRect1);
      this.rightElement.Arrange(finalRect2);
      this.middleElement.Arrange(new RectangleF(PointF.Empty, finalSize));
      int num = (int) ((Decimal) clientRectangle.Width / new Decimal(100) * ((GanttViewBaseItemElement) this.Parent).Data.Progress);
      this.ProgressIndicatorElement.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Bottom - this.ProgressIndicatorElement.DesiredSize.Height, (float) num, this.ProgressIndicatorElement.DesiredSize.Height));
      return finalSize;
    }
  }
}
