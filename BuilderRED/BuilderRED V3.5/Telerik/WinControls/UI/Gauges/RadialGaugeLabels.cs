// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadialGaugeLabels
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI.Gauges
{
  [Description("Represents the scale labels.")]
  [Designer("Telerik.WinControls.UI.Design.LabelsDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadialGaugeLabels : GaugeVisualElement, IDrawing
  {
    private int labelsCount = 10;
    private string labelFormat = "#,##0.#";
    private float labelRadiusPercentage = 70f;
    private float? labelStartVisibleRange = new float?();
    private float? labelEndVisibleRange = new float?();
    private float labelFontSize = 8f;
    private RadRadialGaugeElement owner;

    public RadialGaugeLabels()
    {
      this.DrawText = false;
    }

    public RadialGaugeLabels(RadRadialGaugeElement owner)
      : this()
    {
      this.owner = owner;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    [Description("Specifies the font size. Default value is 8")]
    [DefaultValue(8f)]
    public float LabelFontSize
    {
      get
      {
        return this.labelFontSize;
      }
      set
      {
        this.labelFontSize = value;
        this.OnNotifyPropertyChanged("LableFontSize");
      }
    }

    [Description("Specifies the start value from which the labels are displayed.")]
    [DefaultValue(null)]
    public float? LabelStartVisibleRange
    {
      get
      {
        return this.labelStartVisibleRange;
      }
      set
      {
        this.labelStartVisibleRange = value;
        this.OnNotifyPropertyChanged(nameof (LabelStartVisibleRange));
      }
    }

    [DefaultValue(null)]
    [Description("Specifies the end value to which the labels are displayed.")]
    public float? LabelEndVisibleRange
    {
      get
      {
        return this.labelEndVisibleRange;
      }
      set
      {
        this.labelEndVisibleRange = value;
        this.OnNotifyPropertyChanged(nameof (LabelEndVisibleRange));
      }
    }

    [DefaultValue(70f)]
    [Description("Controls how far according to the gauge's arc the labels are rendered")]
    public float LabelRadiusPercentage
    {
      get
      {
        return this.labelRadiusPercentage;
      }
      set
      {
        this.labelRadiusPercentage = value;
        this.OnNotifyPropertyChanged(nameof (LabelRadiusPercentage));
      }
    }

    [DefaultValue("#,##0.#")]
    [Description("Specifies the format of the label's value.")]
    public string LabelFormat
    {
      get
      {
        return this.labelFormat;
      }
      set
      {
        this.labelFormat = value;
        this.OnNotifyPropertyChanged(nameof (LabelFormat));
      }
    }

    [DefaultValue(10)]
    [Description("Controls how many labels will be displayed next ticks for the specified range.")]
    public int LabelsCount
    {
      get
      {
        return this.labelsCount;
      }
      set
      {
        this.labelsCount = value;
        this.OnNotifyPropertyChanged(nameof (LabelsCount));
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName != "Shape")
        this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
      this.Invalidate();
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      float emSize = this.Font.Size;
      if (this.AutoSize)
        emSize = this.owner.GaugeSize.Width * (this.labelFontSize / 100f);
      if ((double) emSize <= 0.0)
        return;
      using (Font font = new Font(this.Font.FontFamily, emSize, this.Font.Style))
      {
        using (StringFormat format = new StringFormat())
        {
          format.Alignment = StringAlignment.Center;
          format.LineAlignment = StringAlignment.Center;
          double num1 = (this.owner.RangeEnd - this.owner.RangeStart) / (double) this.labelsCount;
          double num2 = this.owner.SweepAngle / (double) this.labelsCount;
          double radius = (double) this.owner.GaugeSize.Width / 2.0 * ((double) this.LabelRadiusPercentage / 100.0);
          for (int index = 0; index <= this.labelsCount; ++index)
          {
            double angle = this.owner.StartAngle + (double) index * num2;
            float num3 = (float) (this.owner.RangeStart + (double) index * num1);
            if (!this.labelStartVisibleRange.HasValue || (double) this.labelStartVisibleRange.Value <= (double) num3)
            {
              if (this.labelEndVisibleRange.HasValue)
              {
                if ((double) this.labelEndVisibleRange.Value < (double) num3)
                  continue;
              }
              string s;
              try
              {
                s = num3.ToString(this.labelFormat);
              }
              catch
              {
                s = num3.ToString();
              }
              PointF point = this.owner.PointFromCenter(this.owner.GaugeCenter, radius, angle);
              using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
                graphics.DrawString(s, font, (Brush) solidBrush, point, format);
            }
          }
        }
      }
    }

    public override GraphicsPath Path
    {
      get
      {
        GraphicsPath graphicsPath = new GraphicsPath();
        float emSize = this.Font.Size;
        if (this.AutoSize)
          emSize = this.owner.GaugeSize.Width * (this.labelFontSize / 100f);
        if ((double) emSize <= 0.0)
          return new GraphicsPath();
        using (Font font = new Font(this.Font.FontFamily, emSize, this.Font.Style))
        {
          using (StringFormat stringFormat = new StringFormat())
          {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            double num1 = (this.owner.RangeEnd - this.owner.RangeStart) / (double) this.labelsCount;
            double num2 = this.owner.SweepAngle / (double) this.labelsCount;
            double radius = (double) this.owner.GaugeSize.Width / 2.0 * ((double) this.LabelRadiusPercentage / 100.0);
            for (int index = 0; index <= this.labelsCount; ++index)
            {
              double angle = this.owner.StartAngle + (double) index * num2;
              float num3 = (float) (this.owner.RangeStart + (double) index * num1);
              if (!this.labelStartVisibleRange.HasValue || (double) this.labelStartVisibleRange.Value <= (double) num3)
              {
                if (this.labelEndVisibleRange.HasValue)
                {
                  if ((double) this.labelEndVisibleRange.Value < (double) num3)
                    continue;
                }
                string text;
                try
                {
                  text = num3.ToString(this.labelFormat);
                }
                catch
                {
                  text = num3.ToString();
                }
                PointF pt = this.owner.PointFromCenter(this.owner.GaugeCenter, radius, angle);
                SizeF size = (SizeF) TextRenderer.MeasureText(text, font);
                RectangleF rect = new RectangleF(PointF.Subtract(pt, new SizeF(size.Width / 2f, size.Height / 2f)), size);
                graphicsPath.StartFigure();
                graphicsPath.AddRectangle(rect);
              }
            }
          }
        }
        return graphicsPath;
      }
    }

    public override bool HitTest(Point point)
    {
      using (GraphicsPath path = this.Path)
      {
        using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
        {
          path.Transform(gdiMatrix);
          return path.IsVisible(point);
        }
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
        return;
      this.owner = (RadRadialGaugeElement) this.Parent;
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }
  }
}
