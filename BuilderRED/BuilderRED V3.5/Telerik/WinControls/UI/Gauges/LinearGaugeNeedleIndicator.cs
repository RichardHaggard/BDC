// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.LinearGaugeNeedleIndicator
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
  [Description("Present additional information for the RadLinearGauge.")]
  [Designer("Telerik.WinControls.UI.Design.LinearGaugeNeedleIndicatorDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class LinearGaugeNeedleIndicator : GaugeVisualElement, IDrawing
  {
    public static RadProperty ValueProperty = RadProperty.Register(nameof (Value), typeof (float), typeof (LinearGaugeNeedleIndicator), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.0f, ElementPropertyOptions.AffectsDisplay));
    private float locationPercentage = 0.5f;
    private float backLenghtPercentage = 10f;
    private float pointRadiusPercentage = 5f;
    private float innerPointRadiusPercentage = 5f;
    private float lenghtPercentage = 10f;
    private float thickness = 1f;
    private string labelFormat = "#,##0.#";
    private float labelFontSize = 8f;
    private bool isFilled = true;
    private SizeF textOffsetFromIndicator = SizeF.Empty;
    private RadLinearGaugeElement owner;
    private bool bindValue;
    private float bindOffset;
    private Direction direction;
    private bool circleTicks;
    private bool drawValue;
    private float lineLenght;

    public event EventHandler ValueChanged;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.BackColor = Color.Black;
      this.BackColor2 = Color.Black;
      this.DrawText = false;
      this.Value = 50f;
    }

    public LinearGaugeNeedleIndicator()
    {
    }

    public LinearGaugeNeedleIndicator(RadLinearGaugeElement owner)
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
      this.Value = this.owner.Value + this.BindOffset;
    }

    protected void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    [DefaultValue(8f)]
    public float LabelFontSize
    {
      get
      {
        return this.labelFontSize * this.DpiScaleFactor.Height;
      }
      set
      {
        this.labelFontSize = value;
        this.OnNotifyPropertyChanged("labelFontSize");
      }
    }

    [DefaultValue(0.0f)]
    public float LineLenght
    {
      get
      {
        return this.lineLenght * this.DpiScaleFactor.Height;
      }
      set
      {
        this.lineLenght = value;
        this.OnNotifyPropertyChanged(nameof (LineLenght));
      }
    }

    [DefaultValue(true)]
    public bool IsFilled
    {
      get
      {
        return this.isFilled;
      }
      set
      {
        this.isFilled = value;
        this.OnNotifyPropertyChanged(nameof (IsFilled));
      }
    }

    [DefaultValue(typeof (SizeF), "0, 0")]
    public SizeF TextOffsetFromIndicator
    {
      get
      {
        return new SizeF(this.textOffsetFromIndicator.Width * this.DpiScaleFactor.Width, this.textOffsetFromIndicator.Height * this.DpiScaleFactor.Height);
      }
      set
      {
        this.textOffsetFromIndicator = value;
        this.OnNotifyPropertyChanged(nameof (TextOffsetFromIndicator));
      }
    }

    [DefaultValue("#,##0.#")]
    [Description("Specifies the label format. By default, it is set to #,##0.#.")]
    public string LabelFormat
    {
      get
      {
        return this.labelFormat;
      }
      set
      {
        if (value == this.labelFormat)
          return;
        this.labelFormat = value;
        this.OnNotifyPropertyChanged(nameof (LabelFormat));
      }
    }

    [DefaultValue(false)]
    public bool DrawValue
    {
      get
      {
        return this.drawValue;
      }
      set
      {
        this.drawValue = value;
        this.OnNotifyPropertyChanged(nameof (DrawValue));
      }
    }

    [DefaultValue(false)]
    [Description("Controls whether the specific ticks are circle or not.")]
    public bool CircleTicks
    {
      get
      {
        return this.circleTicks;
      }
      set
      {
        this.circleTicks = value;
        this.OnNotifyPropertyChanged(nameof (CircleTicks));
      }
    }

    [DefaultValue(0.5f)]
    public float LocationPercentage
    {
      get
      {
        return this.locationPercentage;
      }
      set
      {
        this.locationPercentage = value;
        this.OnNotifyPropertyChanged("BindLocationPercentageOffset");
      }
    }

    [DefaultValue(Direction.Up)]
    public Direction Direction
    {
      get
      {
        return this.direction;
      }
      set
      {
        this.direction = value;
        this.OnNotifyPropertyChanged(nameof (Direction));
      }
    }

    [DefaultValue(0.0f)]
    [Description("Specifies the value offset of the needle according to the gauge's value.")]
    public float BindOffset
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

    [DefaultValue(false)]
    [Description("Indicates whether the needle's value is bound to the gauge's Value.")]
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

    [Browsable(false)]
    [Description("Specifies the inner radius of the needle's start point.")]
    [DefaultValue(5f)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public float InnerPointRadiusPercentage
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

    [Description("Specifies the value with which the needle juts out from the center point.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(10f)]
    public float BackLenghtPercentage
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

    [DefaultValue(5f)]
    [Description("Specifies the outer radius of the needle's start point.")]
    public float PointRadiusPercentage
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

    [Description("Controls the indicator width.")]
    [DefaultValue(1f)]
    public float Thickness
    {
      get
      {
        return this.thickness * this.DpiScaleFactor.Width;
      }
      set
      {
        if ((double) this.thickness == (double) value || (double) value <= 0.0)
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
        return (float) this.GetValue(LinearGaugeNeedleIndicator.ValueProperty);
      }
      set
      {
        int num = (int) this.SetValue(LinearGaugeNeedleIndicator.ValueProperty, (object) value);
        this.OnNotifyPropertyChanged(nameof (Value));
        this.OnValueChanged();
      }
    }

    [DefaultValue(10f)]
    [Description("Controls how long the needle will be rendered.")]
    public float LenghtPercentage
    {
      get
      {
        return this.lenghtPercentage;
      }
      set
      {
        this.lenghtPercentage = value;
        this.OnNotifyPropertyChanged(nameof (LenghtPercentage));
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
        if (this.owner == null)
          return graphicsPath;
        float valueToGaugeScale = this.owner.CalculateValueToGaugeScale(this.Value);
        float num1 = this.owner.GaugeSize.Y + this.locationPercentage;
        float num2 = this.LenghtPercentage;
        float num3 = this.LineLenght;
        if (this.AutoSize)
        {
          num3 = (float) ((double) this.owner.GaugeOtherSizeLength * (double) this.LineLenght / 100.0);
          num2 = (float) ((double) this.owner.GaugeLength * (double) this.LenghtPercentage / 100.0);
        }
        RectangleF rect = new RectangleF(valueToGaugeScale, num1, num2, num2);
        float num4 = rect.Width / 2f;
        float num5 = rect.Height / 2f;
        if (this.owner.Vertical)
          rect = new RectangleF(num1, valueToGaugeScale, num2, num2);
        PointF pointF1 = (PointF) Point.Empty;
        PointF pointF2 = (PointF) Point.Empty;
        PointF pointF3 = (PointF) Point.Empty;
        PointF pt1 = (PointF) Point.Empty;
        PointF pt2 = (PointF) Point.Empty;
        switch (this.direction)
        {
          case Direction.Up:
            pt2 = new PointF(rect.Left + num4, rect.Top - num3);
            pointF1 = new PointF(rect.Left + num4, rect.Top);
            pt1 = pointF1 = pointF1;
            pointF2 = new PointF(rect.Left, rect.Bottom);
            pointF3 = new PointF(rect.Right, rect.Bottom);
            break;
          case Direction.Down:
            pt2 = new PointF(rect.Left + num4, rect.Bottom + num3);
            pointF1 = new PointF(rect.Left + num4, rect.Bottom);
            pt1 = pointF1 = pointF1;
            pointF2 = new PointF(rect.Left, rect.Top);
            pointF3 = new PointF(rect.Right, rect.Top);
            break;
          case Direction.Left:
            pt2 = new PointF(rect.Left - num3, rect.Top + num5);
            pointF1 = new PointF(rect.Left, rect.Top + num5);
            pt1 = pointF1 = pointF1;
            pointF2 = new PointF(rect.Right, rect.Top);
            pointF3 = new PointF(rect.Right, rect.Bottom);
            break;
          case Direction.Right:
            pt2 = new PointF(rect.Right + num3, rect.Top + num5);
            pointF1 = new PointF(rect.Right, rect.Top + num5);
            pt1 = pointF1 = pointF1;
            pointF2 = new PointF(rect.Left, rect.Bottom);
            pointF3 = new PointF(rect.Left, rect.Top);
            break;
        }
        graphicsPath.StartFigure();
        if (this.circleTicks)
          graphicsPath.AddEllipse(rect);
        else
          graphicsPath.AddPolygon(new PointF[3]
          {
            pointF1,
            pointF2,
            pointF3
          });
        graphicsPath.CloseFigure();
        if (pt2 != pt1)
        {
          graphicsPath.AddLine(pt1, pt2);
          graphicsPath.CloseFigure();
        }
        if (pt2 != pt1)
        {
          graphicsPath.AddLine(pt1, pt2);
          graphicsPath.CloseFigure();
        }
        using (Matrix matrix = new Matrix())
        {
          if (this.owner.Vertical)
            matrix.Translate(0.0f, -num5);
          else
            matrix.Translate(-num4, 0.0f);
          graphicsPath.Transform(matrix);
        }
        return graphicsPath;
      }
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      if (this.owner == null)
        return;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      using (GraphicsPath path = this.Path)
      {
        if (this.drawValue)
        {
          string empty = string.Empty;
          string s = !this.BindValue ? this.Value.ToString(this.labelFormat) : this.owner.Value.ToString(this.labelFormat);
          using (Brush brush = (Brush) new SolidBrush(this.ForeColor))
          {
            PointF point = path.PathPoints[0] + this.textOffsetFromIndicator;
            graphics.DrawString(s, this.Font, brush, point);
          }
        }
        using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        {
          if (this.IsFilled)
            graphics.FillPath(brush, path);
          using (Pen pen = new Pen(brush, this.thickness))
            graphics.DrawPath(pen, path);
        }
      }
    }

    public override bool HitTest(Point point)
    {
      using (GraphicsPath path = this.Path)
      {
        using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
        {
          path.Transform(gdiMatrix);
          using (Brush brush = (Brush) new SolidBrush(this.BackColor))
          {
            using (Pen pen = new Pen(brush, this.thickness))
            {
              if (!path.IsOutlineVisible(point, pen))
              {
                if (!path.IsVisible(point))
                  goto label_19;
              }
              return true;
            }
          }
        }
      }
label_19:
      return false;
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null || !(this.Parent is RadLinearGaugeElement))
        return;
      this.owner = (RadLinearGaugeElement) this.Parent;
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
