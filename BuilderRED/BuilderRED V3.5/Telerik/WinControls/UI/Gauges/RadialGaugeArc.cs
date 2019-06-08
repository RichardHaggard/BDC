// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadialGaugeArc
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI.Gauges
{
  [Designer("Telerik.WinControls.UI.Design.ArcDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Represent a continuous band spanning the entire sweep angle.")]
  public class RadialGaugeArc : GaugeVisualElement, IDrawing
  {
    private double width = 20.0;
    private double rangeEnd = 100.0;
    internal double innerRadius = 10.0;
    private GaugeBrushType brushType = GaugeBrushType.Gradient;
    private double rangeStart;
    private RadRadialGaugeElement owner;
    internal double startAngle;
    internal double endAngle;
    internal double radius;
    private double bindEndRangeOffset;
    private double bindStartRangeOffset;
    private bool bindStartRange;
    private bool bindEndRange;
    private double outherRadiusPercentage;
    private IBrushFactory brushFactory;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawFill = false;
      this.DrawText = false;
      this.brushFactory = (IBrushFactory) new Telerik.WinControls.UI.Gauges.BrushFactory();
      this.BackColor = Color.FromArgb(111, 185, 71);
      this.BackColor2 = Color.FromArgb(142, 205, 101);
      this.GradientPercentage = 0.07f;
      this.GradientPercentage2 = 0.15f;
      this.NumberOfColors = 2;
    }

    public RadialGaugeArc()
    {
    }

    public RadialGaugeArc(
      double width,
      double rangeStart,
      double rangeEnd,
      RadRadialGaugeElement owner)
      : this()
    {
      this.width = width;
      this.rangeStart = rangeStart;
      this.rangeEnd = rangeEnd;
      this.owner = owner;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    public RadRadialGaugeElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
        return;
      this.owner = (RadRadialGaugeElement) this.Parent;
      this.owner.ValueChanged -= new EventHandler(this.owner_ValueChanged);
      this.owner.ValueChanged += new EventHandler(this.owner_ValueChanged);
      this.OnOwnerValueChanged();
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    private void owner_ValueChanged(object sender, EventArgs e)
    {
      this.OnOwnerValueChanged();
    }

    private void OnOwnerValueChanged()
    {
      if (this.BindStartRange)
        this.OnBindStartChanged();
      if (!this.BindEndRange)
        return;
      this.OnBindEndChanged();
    }

    private void OnBindEndChanged()
    {
      if (this.owner == null)
        return;
      this.RangeEnd = (double) this.owner.Value + this.BindEndRangeOffset;
    }

    private void OnBindStartChanged()
    {
      if (this.owner == null)
        return;
      this.RangeStart = (double) this.owner.Value + this.BindStartRangeOffset;
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      if (this.Width <= 0.0)
        return;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      this.DrawFilledArcSegment(graphics);
    }

    private void DrawFilledArcSegment(Graphics graphic)
    {
      using (GraphicsPath path = this.Path)
      {
        using (Brush brush = this.BrushFactory.CreateBrush((GaugeVisualElement) this, this.BrushType))
          graphic.FillPath(brush, path);
      }
    }

    [DefaultValue(GaugeBrushType.Gradient)]
    public GaugeBrushType BrushType
    {
      get
      {
        return this.brushType;
      }
      set
      {
        this.brushType = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (BrushType)));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IBrushFactory BrushFactory
    {
      get
      {
        return this.brushFactory;
      }
      set
      {
        this.brushFactory = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (BrushFactory)));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double OutherRadiusPercentage
    {
      get
      {
        return this.outherRadiusPercentage;
      }
    }

    [DefaultValue(0.0)]
    [Description("Controls the radius of the arc.")]
    public double Radius
    {
      get
      {
        return this.radius;
      }
      set
      {
        this.radius = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Radius)));
      }
    }

    [Description("The width of the arc.")]
    [DefaultValue(0.0)]
    public double Width
    {
      get
      {
        return this.width;
      }
      set
      {
        if (this.width == value)
          return;
        this.width = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Width)));
      }
    }

    [DefaultValue(0.0)]
    [Description("The start value of the arc.")]
    public double RangeStart
    {
      get
      {
        return this.rangeStart;
      }
      set
      {
        if (this.rangeStart == value)
          return;
        this.rangeStart = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (RangeStart)));
      }
    }

    [Description("The end value of the arc.")]
    [DefaultValue(20.0)]
    public double RangeEnd
    {
      get
      {
        return this.rangeEnd;
      }
      set
      {
        if (this.rangeEnd == value)
          return;
        this.rangeEnd = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (RangeEnd)));
      }
    }

    [Description("Indicates whether the RangeStart property is bound to the gauge's Value.")]
    [DefaultValue(false)]
    public bool BindStartRange
    {
      get
      {
        return this.bindStartRange;
      }
      set
      {
        this.bindStartRange = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (BindStartRange)));
        this.OnBindStartChanged();
      }
    }

    [Description("Indicates whether the RangeEnd property is bound to the gauge's Value.")]
    [DefaultValue(false)]
    public bool BindEndRange
    {
      get
      {
        return this.bindEndRange;
      }
      set
      {
        this.bindEndRange = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (BindEndRange)));
        this.OnBindEndChanged();
      }
    }

    [DefaultValue(0.0)]
    [Description("")]
    public double BindStartRangeOffset
    {
      get
      {
        return this.bindStartRangeOffset;
      }
      set
      {
        this.bindStartRangeOffset = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (BindStartRangeOffset)));
      }
    }

    [Description("")]
    [DefaultValue(0.0)]
    public double BindEndRangeOffset
    {
      get
      {
        return this.bindEndRangeOffset;
      }
      set
      {
        this.bindEndRangeOffset = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (BindEndRangeOffset)));
      }
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (e.PropertyName != "Shape")
        this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
      this.Invalidate();
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }

    public override GraphicsPath Path
    {
      get
      {
        double num = this.Width;
        if (this.AutoSize)
          num = (double) this.owner.GaugeSize.Width / 2.0 * (this.Width / 100.0);
        bool flag = false;
        this.outherRadiusPercentage = this.radius;
        if (this.outherRadiusPercentage == 0.0)
        {
          this.outherRadiusPercentage = this.owner.LabelRadius - this.owner.LabelGap;
          flag = true;
        }
        if (this.AutoSize && !flag)
          this.outherRadiusPercentage = (double) this.owner.GaugeSize.Width / 2.0 * (this.Radius / 100.0);
        this.startAngle = this.owner.StartAngle;
        this.endAngle = this.owner.StartAngle + this.owner.SweepAngle;
        this.innerRadius = this.outherRadiusPercentage - num;
        if (this.innerRadius <= 0.0 || this.outherRadiusPercentage <= 0.0)
          return new GraphicsPath();
        if (this.RangeStart > this.owner.RangeStart)
          this.startAngle = this.owner.CalculateNeedleAngle(this.RangeStart, this.owner.RangeStart, this.owner.RangeEnd, this.owner.StartAngle, this.owner.SweepAngle);
        if (this.RangeEnd < this.owner.RangeEnd)
          this.endAngle = this.owner.CalculateNeedleAngle(this.RangeEnd, this.owner.RangeStart, this.owner.RangeEnd, this.owner.StartAngle, this.owner.SweepAngle);
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddLine(this.owner.PointFromCenter(this.owner.GaugeCenter, this.innerRadius, this.startAngle), this.owner.PointFromCenter(this.owner.GaugeCenter, this.outherRadiusPercentage, this.startAngle));
        graphicsPath.AddArc(new RectangleF(this.owner.GaugeCenter.X - (float) this.outherRadiusPercentage, this.owner.GaugeCenter.Y - (float) this.outherRadiusPercentage, (float) (this.outherRadiusPercentage * 2.0), (float) (this.outherRadiusPercentage * 2.0)), (float) this.startAngle, (float) (this.endAngle - this.startAngle));
        graphicsPath.AddLine(this.owner.PointFromCenter(this.owner.GaugeCenter, this.outherRadiusPercentage, this.endAngle), this.owner.PointFromCenter(this.owner.GaugeCenter, this.innerRadius, this.endAngle));
        graphicsPath.StartFigure();
        graphicsPath.AddArc(new RectangleF(this.owner.GaugeCenter.X - (float) this.innerRadius, this.owner.GaugeCenter.Y - (float) this.innerRadius, (float) (this.innerRadius * 2.0), (float) (this.innerRadius * 2.0)), (float) this.startAngle, (float) (this.endAngle - this.startAngle));
        return graphicsPath;
      }
    }
  }
}
