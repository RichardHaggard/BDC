// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.LinearGaugeLine
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
  [Description("Represent a continuous band in Linear Gauge.")]
  [Designer("Telerik.WinControls.UI.Design.LinearGaugeLineDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class LinearGaugeLine : GaugeVisualElement, IDrawing
  {
    private float width = 1f;
    private float rangeEnd = 100f;
    private GaugeBrushType brushType = GaugeBrushType.Gradient;
    private float offset;
    private float rangeStart;
    private RadLinearGaugeElement owner;
    private float bindEndRangeOffset;
    private float bindStartRangeOffset;
    private bool bindStartRange;
    private bool bindEndRange;
    private IBrushFactory brushFactory;

    public LinearGaugeLine()
    {
      this.DrawBorder = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawBorder = false;
      this.DrawText = false;
      this.BackColor = this.BackColor2 = Color.Black;
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

    [Description("")]
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

    [Description("")]
    [DefaultValue(0.0f)]
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

    [DefaultValue(1f)]
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

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      this.Invalidate();
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
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      using (GraphicsPath path = this.Path)
      {
        using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        {
          float width = this.Width;
          if (this.AutoSize)
            width *= this.owner.GaugeOtherSizeLength / 100f;
          using (Pen pen = new Pen(brush, width))
            graphics.DrawPath(pen, path);
        }
      }
    }

    public override bool HitTest(Point point)
    {
      float width = this.Width;
      if (this.AutoSize)
        width = (float) ((double) this.width * (double) this.owner.GaugeOtherSizeLength / 100.0);
      using (GraphicsPath path = this.Path)
      {
        using (Matrix gdiMatrix = this.TotalTransform.ToGdiMatrix())
        {
          path.Transform(gdiMatrix);
          using (Pen pen = new Pen(Color.Red, width))
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
        PointF pt1;
        PointF pt2;
        if (!vertical)
        {
          pt1 = new PointF(valueToGaugeScale1, this.owner.GaugeSize.Y + this.offset);
          pt2 = new PointF(valueToGaugeScale2, this.owner.GaugeSize.Y + this.offset);
        }
        else
        {
          pt1 = new PointF(this.owner.GaugeSize.X + this.offset, valueToGaugeScale1);
          pt2 = new PointF(this.owner.GaugeSize.X + this.offset, valueToGaugeScale2);
        }
        graphicsPath.AddLine(pt1, pt2);
        return graphicsPath;
      }
    }
  }
}
