// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadialGaugeTicks
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI.Gauges
{
  [Designer("Telerik.WinControls.UI.Design.TicksDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Represents the scale ticks.")]
  public class RadialGaugeTicks : GaugeVisualElement, IDrawing
  {
    private int ticksCount = 10;
    private float ticksLenghtPercentage = 10f;
    private Color tickColor = Color.Black;
    private float tickThickness = 2f;
    private float ticksRadiusPercentage = 90f;
    private float? tickStartIndexVisibleRange = new float?();
    private float? tickEndIndexVisibleRange = new float?();
    private int ticksOffset;
    private bool circleTicks;
    private RadRadialGaugeElement owner;

    public RadialGaugeTicks()
    {
      this.DrawText = false;
    }

    public RadialGaugeTicks(RadRadialGaugeElement owner)
      : this()
    {
      this.owner = owner;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    [DefaultValue(null)]
    [Description("Specifies at which index the visible ticks range will start.")]
    public float? TickStartIndexVisibleRange
    {
      get
      {
        return this.tickStartIndexVisibleRange;
      }
      set
      {
        this.tickStartIndexVisibleRange = value;
        this.OnNotifyPropertyChanged(nameof (TickStartIndexVisibleRange));
      }
    }

    [DefaultValue(null)]
    [Description("Specifies at which index the visible ticks range will end.")]
    public float? TickEndIndexVisibleRange
    {
      get
      {
        return this.tickEndIndexVisibleRange;
      }
      set
      {
        this.tickEndIndexVisibleRange = value;
        this.OnNotifyPropertyChanged(nameof (TickEndIndexVisibleRange));
      }
    }

    [DefaultValue(90f)]
    [Description("Controls how far according to the gauge's arc the ticks will be rendered.")]
    public float TicksRadiusPercentage
    {
      get
      {
        return this.ticksRadiusPercentage;
      }
      set
      {
        this.ticksRadiusPercentage = value;
        this.OnNotifyPropertyChanged(nameof (TicksRadiusPercentage));
      }
    }

    [Description("Controls whether the specific ticks are circle or not.")]
    [DefaultValue(false)]
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

    [DefaultValue(2f)]
    [Description("Specifies the width of ticks.")]
    public float TickThickness
    {
      get
      {
        return this.tickThickness;
      }
      set
      {
        this.tickThickness = value;
        this.OnNotifyPropertyChanged(nameof (TickThickness));
      }
    }

    [Description("Specifies the color for the ticks")]
    public Color TickColor
    {
      get
      {
        return this.tickColor;
      }
      set
      {
        this.tickColor = value;
        this.OnNotifyPropertyChanged(nameof (TickColor));
      }
    }

    [Description("Specifies the ticks back length towards the center point.")]
    [DefaultValue(0)]
    public int TicksOffset
    {
      get
      {
        return this.ticksOffset;
      }
      set
      {
        this.ticksOffset = value;
        this.OnNotifyPropertyChanged(nameof (TicksOffset));
      }
    }

    [DefaultValue(10f)]
    [Description("Controls the ticks length.")]
    public float TicksLenghtPercentage
    {
      get
      {
        return this.ticksLenghtPercentage;
      }
      set
      {
        this.ticksLenghtPercentage = value;
        this.OnNotifyPropertyChanged(nameof (TicksLenghtPercentage));
      }
    }

    [Description("Specifies how many ticks will be displayed.")]
    [DefaultValue(10)]
    public int TicksCount
    {
      get
      {
        return this.ticksCount;
      }
      set
      {
        this.ticksCount = value;
        this.OnNotifyPropertyChanged(nameof (TicksCount));
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
        if (this.owner == null)
          return new GraphicsPath();
        GraphicsPath graphicsPath = new GraphicsPath();
        double num1 = this.owner.SweepAngle / (double) this.ticksCount;
        double num2 = (double) this.owner.GaugeSize.Width / 2.0 * (double) this.ticksLenghtPercentage / 100.0;
        double num3 = (double) this.owner.GaugeSize.Width / 2.0 * (double) this.ticksRadiusPercentage / 100.0;
        for (int index = 0; index < this.ticksCount + 1; ++index)
        {
          double angle = this.owner.StartAngle + (double) index * num1;
          if ((!this.tickStartIndexVisibleRange.HasValue || (double) this.tickStartIndexVisibleRange.Value <= (double) index) && (!this.tickEndIndexVisibleRange.HasValue || (double) this.tickEndIndexVisibleRange.Value >= (double) index))
          {
            if (!this.circleTicks)
            {
              PointF pt1 = this.owner.PointFromCenter(this.owner.GaugeCenter, num3 - (double) this.ticksOffset, angle);
              PointF pt2 = this.owner.PointFromCenter(this.owner.GaugeCenter, num3 + num2, angle);
              graphicsPath.StartFigure();
              graphicsPath.AddLine(pt1, pt2);
            }
            else
            {
              PointF center = this.owner.PointFromCenter(this.owner.GaugeCenter, num3 - (double) this.ticksOffset, angle);
              graphicsPath.StartFigure();
              graphicsPath.AddEllipse(this.owner.GetRectangleFromRadius(center, this.ticksLenghtPercentage));
            }
          }
        }
        return graphicsPath;
      }
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      float width = this.tickThickness;
      if (this.AutoSize)
        width = this.owner.GaugeSize.Width * (this.tickThickness / 100f);
      using (GraphicsPath path = this.Path)
      {
        using (Pen pen = new Pen(this.tickColor, width))
          graphics.DrawPath(pen, path);
      }
    }

    public override bool HitTest(Point point)
    {
      float width = this.tickThickness;
      if (this.AutoSize)
        width = this.owner.GaugeSize.Width * (this.tickThickness / 100f);
      using (GraphicsPath path = this.Path)
      {
        using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
        {
          path.Transform(gdiMatrix);
          using (Pen pen = new Pen(this.tickColor, width))
          {
            if (!path.IsOutlineVisible(point, pen))
            {
              if (!path.IsVisible(point))
                goto label_17;
            }
            return true;
          }
        }
      }
label_17:
      return false;
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
        return;
      this.owner = (RadRadialGaugeElement) this.Parent;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }
  }
}
