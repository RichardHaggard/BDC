// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.LinearGaugeTicks
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
  [Description("Represents the scale ticks.")]
  [Designer("Telerik.WinControls.UI.Design.LinearTicksDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class LinearGaugeTicks : GaugeVisualElement
  {
    private int ticksCount = 10;
    private float ticksLenghtPercentage = 10f;
    private Color tickColor = Color.Black;
    private float tickThickness = 1f;
    private float ticksLocationPercentage = 10f;
    private float? tickStartIndexVisibleRange = new float?();
    private float? tickEndIndexVisibleRange = new float?();
    private int ticksOffset;
    private RadLinearGaugeElement owner;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = false;
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
        if (this.ticksCount > 0)
        {
          double num1 = (double) this.owner.GaugeLength / 2.0;
          if (this.ticksCount != 0)
            num1 = ((double) this.owner.GaugeLength - 1.0) / (double) this.ticksCount;
          float num2 = this.ticksLenghtPercentage;
          if (this.AutoSize)
            num2 = (float) ((double) this.owner.GaugeOtherSizeLength * (double) this.ticksLenghtPercentage / 100.0);
          double num3 = (double) this.ticksLocationPercentage * (double) this.owner.GaugeOtherSizeLength / 100.0;
          for (int index = 0; index < this.ticksCount + 1; ++index)
          {
            double d = (double) this.owner.GaugeStartPoint + (double) index * num1;
            if ((!this.tickStartIndexVisibleRange.HasValue || (double) this.tickStartIndexVisibleRange.Value <= (double) index) && (!this.tickEndIndexVisibleRange.HasValue || (double) this.tickEndIndexVisibleRange.Value >= (double) index))
            {
              float num4 = (float) Math.Floor(d);
              PointF pt1;
              PointF pt2;
              if (!this.owner.Vertical)
              {
                pt1 = new PointF(num4, this.owner.GaugeSize.Y + this.ticksLocationPercentage);
                pt2 = new PointF(num4, this.owner.GaugeSize.Y + this.ticksLocationPercentage - num2);
              }
              else
              {
                pt1 = new PointF(this.owner.GaugeSize.X + this.ticksLocationPercentage, num4);
                pt2 = new PointF(this.owner.GaugeSize.X + this.ticksLocationPercentage - num2, num4);
              }
              graphicsPath.StartFigure();
              graphicsPath.AddLine(pt1, pt2);
              graphicsPath.CloseFigure();
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
        width = (float) ((double) this.owner.GaugeLength * (double) this.tickThickness / 100.0);
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
      this.owner = (RadLinearGaugeElement) this.Parent;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }

    [Description("Specifies at which index the visible ticks range will start.")]
    [DefaultValue(null)]
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

    [DefaultValue(10f)]
    [Description("Controls how far according to the gauge's arc the ticks will be rendered.")]
    public float TicksLocationPercentage
    {
      get
      {
        return this.ticksLocationPercentage;
      }
      set
      {
        this.ticksLocationPercentage = value;
        this.OnNotifyPropertyChanged(nameof (TicksLocationPercentage));
      }
    }

    [Description("Specifies the width of ticks.")]
    [DefaultValue(1f)]
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

    [DefaultValue(typeof (Color), "Black")]
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
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(0)]
    [Browsable(false)]
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

    [DefaultValue(10)]
    [Description("Specifies how many ticks will be displayed.")]
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
  }
}
