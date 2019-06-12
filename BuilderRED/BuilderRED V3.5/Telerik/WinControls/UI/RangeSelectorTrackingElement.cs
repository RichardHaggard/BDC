// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorTrackingElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorTrackingElement : RangeSelectorVisualElementWithOrientation
  {
    private float startRange = 30f;
    private float endRange = 70f;
    private int minSelectionLenght = 3;
    private string toolTipThumbFormatString = "{0:0.00} : {1:0.00}";
    private Point toolTipOffset = new Point(10, 0);
    private int toolTipDuration = 1000;
    private RangeSelectorHoverElement leftHover;
    private RangeSelectorThumbLineElement leftThumbLine;
    private RangeSelectorThumb leftThumb;
    private RangeSelectorRangeElement range;
    private RangeSelectorThumbLineElement rightThumbLine;
    private RangeSelectorThumb rightThumb;
    private RangeSelectorHoverElement rightHover;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.ShouldHandleMouseInput = false;
      this.ZIndex = 2;
    }

    protected override void CreateChildElements()
    {
      this.leftHover = new RangeSelectorHoverElement(true);
      this.leftThumbLine = new RangeSelectorThumbLineElement();
      this.leftThumbLine.Class = "SelectorLeftThumbElement";
      this.leftThumb = new RangeSelectorThumb(true);
      this.leftThumb.Class = "SelectorLeftThumb";
      this.range = new RangeSelectorRangeElement();
      this.rightThumbLine = new RangeSelectorThumbLineElement();
      this.rightThumbLine.Class = "SelectorRightThumbElement";
      this.rightThumb = new RangeSelectorThumb(false);
      this.rightThumb.Class = "SelectorRightThumb";
      this.rightHover = new RangeSelectorHoverElement(false);
      int num1 = (int) this.leftThumb.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.range, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.leftThumb.BindProperty(RadElement.IsMouseDownProperty, (RadObject) this.range, RadElement.IsMouseDownProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.rightThumb.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.range, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.rightThumb.BindProperty(RadElement.IsMouseDownProperty, (RadObject) this.range, RadElement.IsMouseDownProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.leftThumbLine.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.range, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.leftThumbLine.BindProperty(RadElement.IsMouseDownProperty, (RadObject) this.range, RadElement.IsMouseDownProperty, PropertyBindingOptions.TwoWay);
      int num7 = (int) this.range.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.rightThumbLine, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num8 = (int) this.range.BindProperty(RadElement.IsMouseDownProperty, (RadObject) this.rightThumbLine, RadElement.IsMouseDownProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.leftHover);
      this.Children.Add((RadElement) this.leftThumbLine);
      this.Children.Add((RadElement) this.LeftThumb);
      this.Children.Add((RadElement) this.range);
      this.Children.Add((RadElement) this.RightThumb);
      this.Children.Add((RadElement) this.rightThumbLine);
      this.Children.Add((RadElement) this.rightHover);
    }

    public float StartRange
    {
      get
      {
        return this.startRange;
      }
      set
      {
        if ((double) value == (double) this.startRange || (double) value < 0.0 || (double) value > (double) (100 - this.MinSelectionLength))
          return;
        if (this.RangeSelectorElement.ShouldFireSelectionChangeEvent)
          this.SetupStartRangeWithAllEvents(value);
        else
          this.SetupStartRange(value);
      }
    }

    public float EndRange
    {
      get
      {
        return this.endRange;
      }
      set
      {
        if ((double) value == (double) this.endRange || (double) value < (double) this.MinSelectionLength || (double) value > 100.0)
          return;
        if (this.RangeSelectorElement.ShouldFireSelectionChangeEvent)
          this.SetupEndRangeWithAllEvents(value);
        else
          this.SetupEndRange(value);
      }
    }

    public int MinSelectionLength
    {
      get
      {
        return this.minSelectionLenght;
      }
      set
      {
        if (value <= 3 || value >= 100)
          throw new ArgumentException("MinSelectionLenght should be between 3 and 99");
        this.minSelectionLenght = value;
      }
    }

    public RangeSelectorHoverElement LeftHover
    {
      get
      {
        return this.leftHover;
      }
    }

    public RangeSelectorThumbLineElement LeftThumbLine
    {
      get
      {
        return this.leftThumbLine;
      }
    }

    public RangeSelectorRangeElement Range
    {
      get
      {
        return this.range;
      }
    }

    public RangeSelectorThumbLineElement RightThumbLine
    {
      get
      {
        return this.rightThumbLine;
      }
    }

    public RangeSelectorHoverElement RightHover
    {
      get
      {
        return this.rightHover;
      }
    }

    public RangeSelectorThumb LeftThumb
    {
      get
      {
        return this.leftThumb;
      }
    }

    public RangeSelectorThumb RightThumb
    {
      get
      {
        return this.rightThumb;
      }
    }

    public string ToolTipThumbFormatString
    {
      get
      {
        return this.toolTipThumbFormatString;
      }
      set
      {
        this.toolTipThumbFormatString = value;
      }
    }

    public Point ToolTipOffset
    {
      get
      {
        return this.toolTipOffset;
      }
      set
      {
        this.toolTipOffset = value;
      }
    }

    public int ToolTipDuration
    {
      get
      {
        return this.toolTipDuration;
      }
      set
      {
        this.toolTipDuration = value;
      }
    }

    protected virtual void OnThumbLeftValueChanging(ValueChangingEventArgs e)
    {
      this.RangeSelectorElement.OnThumbLeftValueChanging(e);
    }

    protected virtual void OnThumbLeftValueChanged(EventArgs e)
    {
      this.RangeSelectorElement.OnThumbLeftValueChanged(e);
    }

    protected virtual void OnThumbRightValueChanging(ValueChangingEventArgs e)
    {
      this.RangeSelectorElement.OnThumbRightValueChanging(e);
    }

    protected virtual void OnThumbRightValueChanged(EventArgs e)
    {
      this.RangeSelectorElement.OnThumbRightValueChanged(e);
    }

    private void OnSelectionChanging(
      RangeSelectorSelectionChangingEventArgs changingArgs)
    {
      this.RangeSelectorElement.OnSelectionChanging(changingArgs);
    }

    private void OnSelectionChanged(EventArgs eventArgs)
    {
      this.RangeSelectorElement.OnSelectionChanged(eventArgs);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF availableSize1 = new SizeF(availableSize.Width, availableSize.Height);
      if (this.RangeSelectorElement.AssociatedElement != null && this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        availableSize1 = new SizeF(availableSize.Width * this.RangeSelectorElement.TotalZoomFactor, availableSize.Height);
      base.MeasureOverride(availableSize1);
      return availableSize1;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        this.LeftHover.Arrange(new RectangleF(new PointF(0.0f, 0.0f), new SizeF(this.LeftHover.DesiredSize.Width, finalSize.Height)));
        this.LeftThumbLine.Arrange(new RectangleF(new PointF(this.LeftHover.DesiredSize.Width, 0.0f), new SizeF(this.LeftThumbLine.DesiredSize.Width, finalSize.Height)));
        this.LeftThumb.Arrange(new RectangleF(new PointF(this.LeftHover.DesiredSize.Width - (float) (((double) this.LeftThumb.DesiredSize.Width - (double) this.LeftThumbLine.DesiredSize.Width) / 2.0), (float) (((double) finalSize.Height - (double) this.LeftThumb.DesiredSize.Height) / 2.0)), this.LeftThumb.DesiredSize));
        PointF location1 = new PointF(this.LeftHover.DesiredSize.Width + this.LeftThumbLine.DesiredSize.Width, 0.0f);
        float width = finalSize.Width - this.LeftHover.DesiredSize.Width - this.LeftThumbLine.DesiredSize.Width - this.RightThumbLine.DesiredSize.Width - this.RightHover.DesiredSize.Width;
        SizeF size = new SizeF(width, finalSize.Height);
        this.Range.Arrange(new RectangleF(location1, size));
        PointF location2 = new PointF(this.LeftHover.DesiredSize.Width + this.LeftThumbLine.DesiredSize.Width + width, 0.0f);
        this.RightThumbLine.Arrange(new RectangleF(location2, new SizeF(this.RightThumbLine.DesiredSize.Width, finalSize.Height)));
        this.RightThumb.Arrange(new RectangleF(new PointF(location2.X - (float) (((double) this.RightThumb.DesiredSize.Width - (double) this.RightThumbLine.DesiredSize.Width) / 2.0), (float) (((double) finalSize.Height - (double) this.RightThumb.DesiredSize.Height) / 2.0)), this.RightThumb.DesiredSize));
        this.RightHover.Arrange(new RectangleF(new PointF(finalSize.Width - this.RightHover.DesiredSize.Width, 0.0f), new SizeF(this.RightHover.DesiredSize.Width, finalSize.Height)));
      }
      else
      {
        this.LeftHover.Arrange(new RectangleF(new PointF(0.0f, finalSize.Height - this.LeftHover.DesiredSize.Height), new SizeF(finalSize.Width, this.LeftHover.DesiredSize.Height)));
        this.LeftThumbLine.Arrange(new RectangleF(new PointF(0.0f, finalSize.Height - this.LeftHover.DesiredSize.Height), new SizeF(finalSize.Height, this.LeftThumbLine.DesiredSize.Height)));
        this.LeftThumb.Arrange(new RectangleF(new PointF((float) (((double) finalSize.Width - (double) this.LeftThumb.DesiredSize.Width) / 2.0), (float) ((double) finalSize.Height - (double) this.LeftHover.DesiredSize.Height - ((double) this.LeftThumb.DesiredSize.Height - (double) this.LeftThumbLine.DesiredSize.Height) / 2.0)), this.LeftThumb.DesiredSize));
        PointF location1 = new PointF(0.0f, this.RightHover.DesiredSize.Height + this.RightThumbLine.DesiredSize.Height);
        float height = finalSize.Height - this.LeftHover.DesiredSize.Height - this.LeftThumbLine.DesiredSize.Height - this.RightThumbLine.DesiredSize.Height - this.RightHover.DesiredSize.Height;
        SizeF size = new SizeF(finalSize.Width, height);
        this.Range.Arrange(new RectangleF(location1, size));
        PointF location2 = new PointF(0.0f, finalSize.Height - (this.LeftHover.DesiredSize.Height + this.LeftThumbLine.DesiredSize.Height + height));
        this.RightThumbLine.Arrange(new RectangleF(location2, new SizeF(finalSize.Width, this.RightThumbLine.DesiredSize.Height)));
        this.RightThumb.Arrange(new RectangleF(new PointF((float) (((double) finalSize.Width - (double) this.LeftThumb.DesiredSize.Width) / 2.0), location2.Y - (float) (((double) this.RightThumb.DesiredSize.Height - (double) this.RightThumbLine.DesiredSize.Height) / 2.0)), this.RightThumb.DesiredSize));
        this.RightHover.Arrange(new RectangleF(new PointF(0.0f, 0.0f), new SizeF(finalSize.Width, this.RightHover.DesiredSize.Height)));
      }
      return finalSize;
    }

    internal void SetupStartRange(float value)
    {
      ValueChangingEventArgs e = new ValueChangingEventArgs((object) value, (object) this.startRange);
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbLeftValueChanging(e);
      if (e.Cancel)
        return;
      if ((double) this.EndRange - (double) value < (double) this.MinSelectionLength)
        this.EndRange = value + (float) this.MinSelectionLength;
      this.startRange = value;
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbLeftValueChanged(new EventArgs());
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).AssociatedViewStart = value;
      this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
      this.InvalidateMeasure(true);
    }

    internal void SetupStartRangeWithAllEvents(float value)
    {
      ValueChangingEventArgs e = new ValueChangingEventArgs((object) value, (object) this.startRange);
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbLeftValueChanging(e);
      if (e.Cancel)
        return;
      this.startRange = value;
      RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(value, this.EndRange);
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnSelectionChanging(changingArgs);
      if (e.Cancel)
        return;
      if ((double) this.EndRange - (double) value < (double) this.MinSelectionLength)
        this.EndRange = value + (float) this.MinSelectionLength;
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbLeftValueChanged(new EventArgs());
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnSelectionChanged(new EventArgs());
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).AssociatedViewStart = value;
      this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
      this.InvalidateMeasure(true);
    }

    internal void SetupStartRangeWithOutEvents(float value)
    {
      if ((double) this.EndRange - (double) value < (double) this.MinSelectionLength)
        this.EndRange = value + (float) this.MinSelectionLength;
      this.startRange = value;
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).AssociatedViewStart = value;
      this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
      this.InvalidateMeasure(true);
    }

    internal void SetupEndRange(float value)
    {
      ValueChangingEventArgs e = new ValueChangingEventArgs((object) value, (object) this.endRange);
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbRightValueChanging(e);
      if (e.Cancel)
        return;
      if ((double) value - (double) this.StartRange < (double) this.MinSelectionLength)
        this.StartRange = value - (float) this.MinSelectionLength;
      this.endRange = value;
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbRightValueChanged(new EventArgs());
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).AssociatedViewEnd = value;
      this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
      this.InvalidateMeasure(true);
    }

    internal void SetupEndRangeWithAllEvents(float value)
    {
      ValueChangingEventArgs e = new ValueChangingEventArgs((object) value, (object) this.endRange);
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbRightValueChanging(e);
      if (e.Cancel)
        return;
      this.endRange = value;
      RangeSelectorSelectionChangingEventArgs changingArgs = new RangeSelectorSelectionChangingEventArgs(this.StartRange, value);
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnSelectionChanging(changingArgs);
      if (e.Cancel)
        return;
      if ((double) value - (double) this.StartRange < (double) this.MinSelectionLength)
        this.StartRange = value - (float) this.MinSelectionLength;
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnThumbRightValueChanged(new EventArgs());
      if (!this.RangeSelectorElement.EnableFastScrolling || this.RangeSelectorElement.EnableFastScrolling && this.RangeSelectorElement.IsMouseUp)
        this.OnSelectionChanged(new EventArgs());
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).AssociatedViewEnd = value;
      this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
      this.InvalidateMeasure(true);
    }

    internal void SetupEndRangeWithOutEvents(float value)
    {
      if ((double) value - (double) this.StartRange < (double) this.MinSelectionLength)
        this.StartRange = value - (float) this.MinSelectionLength;
      this.endRange = value;
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).AssociatedViewEnd = value;
      this.RangeSelectorElement.ScrollSelectorElement.InvalidateMeasure(true);
      this.InvalidateMeasure(true);
    }
  }
}
