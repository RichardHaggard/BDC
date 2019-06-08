// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorScrollElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorScrollElement : RangeSelectorVisualElementWithOrientation
  {
    private ViewPosition scrollPostion = ViewPosition.BottomRight;
    private float end = 100f;
    private float minScrollLength = 10f;
    private bool showButtons = true;
    private string toolTipThumbFormatString = "{0:0.00} : {1:0.00}";
    private string toolTipSelectionFormatString = "{0:0.00} : {1:0.00}";
    private Point toolTipOffset = new Point(10, 0);
    private int toolTipDuration = 1000;
    private RangeSelectorArrowButton leftTopButton;
    private RangeSelectorScrollThumb leftTopThumb;
    private RangeSelectorScrollRange range;
    private RangeSelectorScrollThumb bottomRightThumb;
    private RangeSelectorArrowButton bottomRightButton;
    private RangeSelectorScrollSelectionRange sellectionRange;
    private float start;
    private ToolTip toolTip;

    protected override void CreateChildElements()
    {
      this.leftTopButton = new RangeSelectorArrowButton();
      this.leftTopButton.Class = "ScrollLeftArrowButton";
      this.leftTopButton.Click += new EventHandler(this.leftTopButton_Click);
      this.leftTopThumb = new RangeSelectorScrollThumb(true);
      this.range = new RangeSelectorScrollRange();
      this.bottomRightThumb = new RangeSelectorScrollThumb(false);
      this.bottomRightButton = new RangeSelectorArrowButton();
      this.bottomRightButton.Class = "ScrollRightArrowButton";
      this.bottomRightButton.Click += new EventHandler(this.bottomRightButton_Click);
      this.sellectionRange = new RangeSelectorScrollSelectionRange();
      int num1 = (int) this.leftTopThumb.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.range, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.leftTopThumb.BindProperty(RadElement.IsMouseDownProperty, (RadObject) this.range, RadElement.IsMouseDownProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.bottomRightThumb.BindProperty(RadElement.IsMouseOverProperty, (RadObject) this.range, RadElement.IsMouseOverProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.bottomRightThumb.BindProperty(RadElement.IsMouseDownProperty, (RadObject) this.range, RadElement.IsMouseDownProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.leftTopButton);
      this.Children.Add((RadElement) this.leftTopThumb);
      this.Children.Add((RadElement) this.range);
      this.Children.Add((RadElement) this.bottomRightThumb);
      this.Children.Add((RadElement) this.sellectionRange);
      this.Children.Add((RadElement) this.bottomRightButton);
    }

    protected override void DisposeManagedResources()
    {
      if (this.leftTopButton != null)
        this.leftTopButton.Click -= new EventHandler(this.leftTopButton_Click);
      if (this.bottomRightButton != null)
        this.bottomRightButton.Click -= new EventHandler(this.bottomRightButton_Click);
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    public ViewPosition ScrollDockPostion
    {
      get
      {
        return this.scrollPostion;
      }
      set
      {
        this.scrollPostion = value;
      }
    }

    public RangeSelectorArrowButton BottomRightButton
    {
      get
      {
        return this.bottomRightButton;
      }
    }

    public RangeSelectorScrollThumb LeftTopThumb
    {
      get
      {
        return this.leftTopThumb;
      }
    }

    public RangeSelectorScrollRange Range
    {
      get
      {
        return this.range;
      }
    }

    public RangeSelectorScrollThumb BottomRightThumb
    {
      get
      {
        return this.bottomRightThumb;
      }
    }

    public RangeSelectorArrowButton LeftTopButton
    {
      get
      {
        return this.leftTopButton;
      }
    }

    public RangeSelectorScrollSelectionRange SellectionRange
    {
      get
      {
        return this.sellectionRange;
      }
    }

    public float Start
    {
      get
      {
        return this.start;
      }
      set
      {
        if ((double) this.start == (double) value || (double) value < 0.0 || ((double) value > 100.0 - (double) this.MinScrollLength || (double) this.end - (double) value < (double) this.MinScrollLength))
          return;
        this.start = value;
        if (!this.RangeSelectorElement.EnableFastScrolling)
          this.RangeSelectorElement.RangeSelectorViewZoomStart = value;
        this.InvalidateMeasure(true);
      }
    }

    public float End
    {
      get
      {
        return this.end;
      }
      set
      {
        if ((double) this.end == (double) value || (double) value < 0.0 + (double) this.MinScrollLength || ((double) value > 100.0 || (double) value - (double) this.start < (double) this.MinScrollLength))
          return;
        this.end = value;
        if (!this.RangeSelectorElement.EnableFastScrolling)
          this.RangeSelectorElement.RangeSelectorViewZoomEnd = value;
        this.InvalidateMeasure(true);
      }
    }

    public float MinScrollLength
    {
      get
      {
        return this.minScrollLength;
      }
      set
      {
        this.minScrollLength = value;
      }
    }

    public bool ShowButtons
    {
      get
      {
        return this.showButtons;
      }
      set
      {
        this.showButtons = value;
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

    public string ToolTipSelectionFormatString
    {
      get
      {
        return this.toolTipSelectionFormatString;
      }
      set
      {
        this.toolTipSelectionFormatString = value;
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

    public ToolTip ToolTip
    {
      get
      {
        return this.toolTip;
      }
      internal set
      {
        this.toolTip = value;
      }
    }

    private void bottomRightButton_Click(object sender, EventArgs e)
    {
      if ((double) this.End == 100.0)
        return;
      if ((double) this.End + 1.0 > 100.0)
      {
        float num = 100f - this.End;
        this.End = 100f;
        this.Start += num;
      }
      else
      {
        ++this.Start;
        ++this.End;
      }
    }

    private void leftTopButton_Click(object sender, EventArgs e)
    {
      if ((double) this.Start == 0.0)
        return;
      if ((double) this.Start - 1.0 < 0.0)
      {
        float num = 0.0f - this.Start;
        this.Start = 0.0f;
        this.End += num;
      }
      else
      {
        --this.Start;
        --this.End;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      this.LeftTopThumb.Measure(availableSize);
      this.BottomRightThumb.Measure(availableSize);
      this.Range.Measure(availableSize);
      this.SellectionRange.Measure(availableSize);
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
        {
          empty.Height = Math.Max(child.DesiredSize.Height, empty.Height);
          empty.Width = availableSize.Width;
        }
        else
        {
          empty.Height = availableSize.Height;
          empty.Width = Math.Max(child.DesiredSize.Width, empty.Width);
        }
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        float num = (float) (((double) finalSize.Width - (double) this.BottomRightButton.DesiredSize.Width - (double) this.LeftTopButton.DesiredSize.Width) / 100.0);
        this.LeftTopButton.Arrange(new RectangleF(0.0f, 0.0f, this.LeftTopButton.DesiredSize.Width, this.LeftTopButton.DesiredSize.Height));
        this.LeftTopThumb.Arrange(new RectangleF(new PointF(num * this.Start + this.LeftTopButton.DesiredSize.Width, 0.0f), new SizeF(this.LeftTopThumb.DesiredSize.Width, finalSize.Height)));
        this.Range.Arrange(new RectangleF(new PointF(num * this.Start + this.LeftTopThumb.DesiredSize.Width + this.LeftTopButton.DesiredSize.Width, 0.0f), new SizeF((this.End - this.Start) * num - this.LeftTopThumb.DesiredSize.Width - this.BottomRightThumb.DesiredSize.Width, finalSize.Height)));
        this.BottomRightThumb.Arrange(new RectangleF(new PointF(num * this.End + this.LeftTopButton.DesiredSize.Width - this.BottomRightThumb.DesiredSize.Width, 0.0f), new SizeF(this.BottomRightThumb.DesiredSize.Width, finalSize.Height)));
        this.BottomRightButton.Arrange(new RectangleF(finalSize.Width - this.BottomRightButton.DesiredSize.Width, 0.0f, this.BottomRightButton.DesiredSize.Width, this.BottomRightButton.DesiredSize.Height));
        this.SellectionRange.Arrange(new RectangleF(new PointF(this.RangeSelectorElement.StartRange * num + this.LeftTopButton.DesiredSize.Width, 0.0f), new SizeF((this.RangeSelectorElement.EndRange - this.RangeSelectorElement.StartRange) * num, finalSize.Height)));
      }
      return finalSize;
    }
  }
}
