// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.LinearGaugeBar
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
  [Designer("Telerik.WinControls.UI.Design.LinearBarDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Represent a continuous band in Linear Gauge.")]
  public class LinearGaugeBar : GaugeVisualElement, IDrawing
  {
    private float width = 100f;
    private float width2 = 100f;
    private float rangeEnd = 100f;
    private bool isTopToBottom = true;
    private GaugeBrushType brushType = GaugeBrushType.Gradient;
    private float offset;
    private float rangeStart;
    private RadLinearGaugeElement owner;
    private float bindEndRangeOffset;
    private float bindStartRangeOffset;
    private bool bindStartRange;
    private bool bindEndRange;
    private IBrushFactory brushFactory;

    public LinearGaugeBar()
    {
      this.DrawBorder = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawBorder = false;
      this.DrawText = false;
      this.BackColor = this.BackColor2 = Color.LightBlue;
      this.brushFactory = (IBrushFactory) new LinearGaugeBrushFactory();
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

    public RadLinearGaugeElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    [DefaultValue(false)]
    [Description("Indicates whether the RangeStart property is bound to the gauge's Value.")]
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

    [DefaultValue(false)]
    [Description("Indicates whether the RangeEnd property is bound to the gauge's Value.")]
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

    [Description("Specifies the start range offset of the scale according to the gauge's value.")]
    [DefaultValue(0.0f)]
    public float BindStartRangeOffset
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

    [DefaultValue(0.0f)]
    [Description("Specifies the end range offset of the scale according to the gauge's value.")]
    public float BindEndRangeOffset
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

    [DefaultValue(0.0f)]
    [Description("Specifies the offset from the top/left scale.")]
    public float Offset
    {
      get
      {
        return this.offset;
      }
      set
      {
        this.offset = value;
        this.OnNotifyPropertyChanged(nameof (Offset));
      }
    }

    [DefaultValue(100f)]
    public float Width2
    {
      get
      {
        return this.width2;
      }
      set
      {
        this.width2 = value;
        this.OnNotifyPropertyChanged(nameof (Width2));
      }
    }

    [DefaultValue(100f)]
    public float Width
    {
      get
      {
        return this.width;
      }
      set
      {
        this.width = value;
        this.OnNotifyPropertyChanged(nameof (Width));
      }
    }

    [DefaultValue(100f)]
    public float RangeEnd
    {
      get
      {
        return this.rangeEnd;
      }
      set
      {
        this.rangeEnd = value;
        this.OnNotifyPropertyChanged(nameof (RangeEnd));
      }
    }

    [DefaultValue(0.0f)]
    public float RangeStart
    {
      get
      {
        return this.rangeStart;
      }
      set
      {
        this.rangeStart = value;
        this.OnNotifyPropertyChanged(nameof (RangeStart));
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      this.Invalidate();
    }

    [Description("Specifies the orientation of the bar.")]
    [DefaultValue(true)]
    public bool IsTopToBottom
    {
      get
      {
        return this.isTopToBottom;
      }
      set
      {
        this.isTopToBottom = value;
        this.OnNotifyPropertyChanged(nameof (IsTopToBottom));
      }
    }

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

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null || !(this.Parent is RadLinearGaugeElement))
        return;
      this.owner = (RadLinearGaugeElement) this.Parent;
      this.owner.ValueChanged -= new EventHandler(this.owner_ValueChanged);
      this.owner.ValueChanged += new EventHandler(this.owner_ValueChanged);
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
      this.OnOwnerValueChanged();
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
      this.RangeEnd = this.owner.Value + this.BindEndRangeOffset;
    }

    private void OnBindStartChanged()
    {
      if (this.owner == null)
        return;
      this.RangeStart = this.owner.Value + this.BindStartRangeOffset;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      if (this.owner == null)
        return;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      using (GraphicsPath path = this.Path)
      {
        using (Brush brush = this.BrushFactory.CreateBrush((GaugeVisualElement) this, this.BrushType))
        {
          using (Pen pen = new Pen(brush))
          {
            graphics.FillPath(brush, path);
            graphics.DrawPath(pen, path);
          }
        }
      }
    }

    public override GraphicsPath Path
    {
      get
      {
        if (this.owner == null)
          return new GraphicsPath();
        GraphicsPath graphicsPath = new GraphicsPath();
        bool vertical = this.owner.Vertical;
        float valueToGaugeScale1 = this.owner.CalculateValueToGaugeScale(this.rangeStart);
        float valueToGaugeScale2 = this.owner.CalculateValueToGaugeScale(this.rangeEnd);
        float width = this.width;
        float width2 = this.width2;
        if (this.AutoSize)
        {
          width *= (float) (((double) this.owner.GaugeOtherSizeLength - (double) this.offset) / 100.0);
          width2 *= (float) (((double) this.owner.GaugeOtherSizeLength - (double) this.offset) / 100.0);
        }
        PointF pointF1;
        PointF pointF2;
        PointF pointF3;
        PointF pointF4;
        if (!vertical)
        {
          if (this.isTopToBottom)
          {
            pointF1 = new PointF(valueToGaugeScale1, this.owner.GaugeSize.Y + this.offset);
            pointF2 = new PointF(valueToGaugeScale1, this.owner.GaugeSize.Y + width + this.offset);
            pointF3 = new PointF(valueToGaugeScale2, this.owner.GaugeSize.Y + width2 + this.offset);
            pointF4 = new PointF(valueToGaugeScale2, this.owner.GaugeSize.Y + this.offset);
          }
          else
          {
            pointF1 = new PointF(valueToGaugeScale1, this.owner.GaugeSize.Bottom - this.offset);
            pointF2 = new PointF(valueToGaugeScale1, this.owner.GaugeSize.Bottom - width + this.offset);
            pointF3 = new PointF(valueToGaugeScale2, this.owner.GaugeSize.Bottom - width2 + this.offset);
            pointF4 = new PointF(valueToGaugeScale2, this.owner.GaugeSize.Bottom - this.offset);
          }
        }
        else if (this.isTopToBottom)
        {
          pointF1 = new PointF(this.owner.GaugeSize.Y + this.offset, valueToGaugeScale1);
          pointF2 = new PointF(this.owner.GaugeSize.Y + width + this.offset, valueToGaugeScale1);
          pointF3 = new PointF(this.owner.GaugeSize.Y + width2 + this.offset, valueToGaugeScale2);
          pointF4 = new PointF(this.owner.GaugeSize.Y + this.offset, valueToGaugeScale2);
        }
        else
        {
          pointF1 = new PointF(this.owner.GaugeOtherSizeLength - this.offset, valueToGaugeScale1);
          pointF2 = new PointF(this.owner.GaugeOtherSizeLength - width - this.offset, valueToGaugeScale1);
          pointF3 = new PointF(this.owner.GaugeOtherSizeLength - width2 - this.offset, valueToGaugeScale2);
          pointF4 = new PointF(this.owner.GaugeOtherSizeLength - this.offset, valueToGaugeScale2);
        }
        graphicsPath.AddPolygon(new PointF[4]
        {
          pointF1,
          pointF2,
          pointF3,
          pointF4
        });
        graphicsPath.CloseFigure();
        return graphicsPath;
      }
    }
  }
}
