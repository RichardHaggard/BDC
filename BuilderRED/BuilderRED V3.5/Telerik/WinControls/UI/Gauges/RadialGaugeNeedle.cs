// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadialGaugeNeedle
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
  [Description("Represent a scale indicator that points to a value.")]
  [Designer("Telerik.WinControls.UI.Design.NeedleDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadialGaugeNeedle : GaugeVisualElement, IDrawing
  {
    public static RadProperty ValueProperty = RadProperty.Register(nameof (Value), typeof (float), typeof (RadialGaugeNeedle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.AffectsDisplay));
    private double backLenghtPercentage = 10.0;
    private double pointRadiusPercentage = 5.0;
    private double innerPointRadiusPercentage = 5.0;
    private double lenghtPercentage = 100.0;
    private double thickness = 1.0;
    private RadRadialGaugeElement owner;
    private bool bindValue;
    private double bindOffset;

    public event EventHandler ValueChanged;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.BackColor = Color.Black;
      this.BackColor2 = Color.Black;
      this.DrawText = false;
    }

    public RadialGaugeNeedle()
    {
    }

    public RadialGaugeNeedle(RadRadialGaugeElement owner)
      : this()
    {
      this.owner = owner;
      this.owner.ValueChanged -= new EventHandler(this.owner_ValueChanged);
      this.owner.ValueChanged += new EventHandler(this.owner_ValueChanged);
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    private void owner_ValueChanged(object sender, EventArgs e)
    {
      if (!this.BindValue)
        return;
      this.Value = this.owner.Value + (float) this.BindOffset;
    }

    protected void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    [DefaultValue(0.0)]
    [Description("Specifies the value offset of the needle according to the gauge's value.")]
    public double BindOffset
    {
      get
      {
        return this.bindOffset;
      }
      set
      {
        this.bindOffset = value;
        this.OnNotifyPropertyChanged(nameof (BindOffset));
      }
    }

    [Description("Indicates whether the needle's value is bound to the gauge's Value.")]
    [DefaultValue(false)]
    public bool BindValue
    {
      get
      {
        return this.bindValue;
      }
      set
      {
        this.bindValue = value;
        if (this.bindValue && this.owner != null)
          this.Value = this.owner.Value;
        this.OnNotifyPropertyChanged(nameof (BindValue));
      }
    }

    [DefaultValue(5.0)]
    [Description("Specifies the inner radius of the needle's start point.")]
    public double InnerPointRadiusPercentage
    {
      get
      {
        return this.innerPointRadiusPercentage;
      }
      set
      {
        this.innerPointRadiusPercentage = value;
        this.OnNotifyPropertyChanged(nameof (InnerPointRadiusPercentage));
      }
    }

    [DefaultValue(10.0)]
    [Description("Specifies the value with which the needle juts out from the center point.")]
    public double BackLenghtPercentage
    {
      get
      {
        return this.backLenghtPercentage;
      }
      set
      {
        this.backLenghtPercentage = value;
        this.OnNotifyPropertyChanged(nameof (BackLenghtPercentage));
      }
    }

    [Description("Specifies the outer radius of the needle's start point.")]
    [DefaultValue(5.0)]
    public double PointRadiusPercentage
    {
      get
      {
        return this.pointRadiusPercentage;
      }
      set
      {
        this.pointRadiusPercentage = value;
        this.OnNotifyPropertyChanged(nameof (PointRadiusPercentage));
      }
    }

    [Description("Controls the needle width.")]
    [DefaultValue(1.0)]
    public double Thickness
    {
      get
      {
        return this.thickness;
      }
      set
      {
        if (this.thickness == value || value <= 0.0)
          return;
        this.thickness = value;
        this.OnNotifyPropertyChanged(nameof (Thickness));
      }
    }

    [Description("Specifies the needle's value.")]
    [Bindable(true)]
    public float Value
    {
      get
      {
        return (float) this.GetValue(RadialGaugeNeedle.ValueProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadialGaugeNeedle.ValueProperty, (object) value);
        this.OnNotifyPropertyChanged(nameof (Value));
        this.OnValueChanged();
      }
    }

    [Description("Controls how long the needle will be rendered.")]
    [DefaultValue(100.0)]
    public double LenghtPercentage
    {
      get
      {
        return this.lenghtPercentage;
      }
      set
      {
        this.lenghtPercentage = value;
        this.OnNotifyPropertyChanged("Thickness");
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName != "Shape")
        this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
      this.Invalidate();
    }

    public override GraphicsPath Path
    {
      get
      {
        GraphicsPath graphicsPath = new GraphicsPath();
        double num1 = (double) this.owner.GaugeSize.Width / 2.0 * (this.lenghtPercentage / 100.0);
        double radius = (double) this.owner.GaugeSize.Width / 2.0 * (this.backLenghtPercentage / 100.0);
        double needleAngle = this.owner.CalculateNeedleAngle((double) this.Value, this.owner.RangeStart, this.owner.RangeEnd, this.owner.StartAngle, this.owner.SweepAngle);
        float num2 = (float) this.thickness;
        if (this.AutoSize)
          num2 = this.owner.GaugeSize.Width * (float) (this.thickness / 100.0);
        PointF pointF1 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) num2 / 2.0, needleAngle + 90.0);
        PointF pointF2 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) num2 / 2.0, needleAngle - 90.0);
        PointF pointF3 = this.owner.PointFromCenter(this.owner.GaugeCenter, num1 + radius, needleAngle);
        this.owner.PointFromCenter(this.owner.GaugeCenter, radius, needleAngle + 180.0);
        graphicsPath.AddClosedCurve(new PointF[3]
        {
          pointF1,
          pointF2,
          pointF3
        }, 0.0f);
        graphicsPath.CloseFigure();
        double num3 = (double) this.owner.GaugeSize.Width / 2.0 * (this.pointRadiusPercentage / 100.0);
        Matrix matrix = new Matrix();
        matrix.Translate((float) (-radius * Math.Cos(needleAngle * Math.PI / 180.0)), (float) (-radius * Math.Sin(needleAngle * Math.PI / 180.0)));
        graphicsPath.Transform(matrix);
        if (num3 > 0.0)
        {
          graphicsPath.StartFigure();
          graphicsPath.AddEllipse(this.owner.GetRectangleFromRadius((float) num3));
          graphicsPath.CloseFigure();
        }
        return graphicsPath;
      }
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      double needleAngle = this.owner.CalculateNeedleAngle((double) this.Value, this.owner.RangeStart, this.owner.RangeEnd, this.owner.StartAngle, this.owner.SweepAngle);
      float num1 = (float) this.thickness;
      if (this.AutoSize)
        num1 = this.owner.GaugeSize.Width * (float) (this.thickness / 100.0);
      PointF point1 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) num1 / 2.0, needleAngle + 90.0);
      PointF point2 = this.owner.PointFromCenter(this.owner.GaugeCenter, (double) num1 / 2.0, needleAngle - 90.0);
      Brush brush = !(point1 == point2) ? (Brush) new LinearGradientBrush(point1, point2, this.BackColor, this.BackColor2) : (Brush) new SolidBrush(this.BackColor);
      using (GraphicsPath path = this.Path)
      {
        graphics.FillPath(brush, path);
        using (Pen pen = new Pen(brush))
          graphics.DrawPath(pen, path);
      }
      if (this.pointRadiusPercentage > 0.0)
      {
        double num2 = (double) this.owner.GaugeSize.Width / 2.0 * (this.pointRadiusPercentage / 100.0);
        graphics.FillEllipse(brush, this.owner.GetRectangleFromRadius((float) num2));
      }
      double num3 = (double) this.owner.GaugeSize.Width / 2.0 * (this.innerPointRadiusPercentage / 100.0);
      if (num3 <= 0.0)
        return;
      graphics.FillEllipse(Brushes.White, this.owner.GetRectangleFromRadius((float) num3));
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null || !(this.Parent is RadRadialGaugeElement))
        return;
      this.owner = (RadRadialGaugeElement) this.Parent;
      this.owner.ValueChanged -= new EventHandler(this.owner_ValueChanged);
      this.owner.ValueChanged += new EventHandler(this.owner_ValueChanged);
      if (this.bindValue)
        this.Value = this.owner.Value;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }
  }
}
