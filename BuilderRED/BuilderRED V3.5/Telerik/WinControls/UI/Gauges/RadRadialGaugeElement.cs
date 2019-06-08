// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadRadialGaugeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI.Gauges
{
  [Description("Represent main needle element. This element is container for all other elements in the Gauge")]
  public class RadRadialGaugeElement : LightVisualElement
  {
    public static RadProperty ValueProperty = RadProperty.Register(nameof (Value), typeof (float), typeof (RadRadialGaugeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.AffectsDisplay));
    private double startAngle = 120.0;
    private double sweepAngle = 300.0;
    private double rangeEnd = 100.0;
    private double axisThickness = 8.0;
    private double labelOffset = 7.0;
    private bool labelsPositionInside = true;
    private const int DefaulGap = 8;
    private const int DefaultBorderWidth = 2;
    private double rangeStart;
    private double centerOffsetX;
    private double centerOffsetY;
    private readonly RadItemOwnerCollection items;

    [Description("The ValueChanged event fires when the value is modified.")]
    public event EventHandler ValueChanged;

    public RadRadialGaugeElement()
    {
      this.DrawFill = false;
      this.Padding = new Padding(10);
      this.items = new RadItemOwnerCollection((RadElement) this);
      this.items.ItemTypes = new System.Type[6]
      {
        typeof (RadialGaugeArc),
        typeof (RadialGaugeNeedle),
        typeof (RadialGaugeLabels),
        typeof (RadialGaugeTicks),
        typeof (RadialGaugeSingleLabel),
        typeof (RadialGaugeBackground)
      };
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [RadEditItemsAction]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected internal double LabelOffset
    {
      get
      {
        return this.labelOffset;
      }
      set
      {
        this.labelOffset = value;
        this.OnNotifyPropertyChanged(nameof (LabelOffset));
      }
    }

    protected internal bool LabelsPositionInside
    {
      get
      {
        return this.labelsPositionInside;
      }
      set
      {
        this.labelsPositionInside = value;
        this.OnNotifyPropertyChanged("LabelsPosition");
      }
    }

    protected internal double AxisThickness
    {
      get
      {
        return this.axisThickness;
      }
      set
      {
        this.axisThickness = value;
        this.OnNotifyPropertyChanged(nameof (AxisThickness));
      }
    }

    [Bindable(true)]
    [Description("Specifies the gauge's value")]
    public float Value
    {
      get
      {
        return (float) this.GetValue(RadRadialGaugeElement.ValueProperty);
      }
      set
      {
        if ((double) value == (double) this.Value)
          return;
        int num = (int) this.SetValue(RadRadialGaugeElement.ValueProperty, (object) value);
        this.OnValueChanged();
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [Description("Specifies the gauge's end.")]
    public double RangeEnd
    {
      get
      {
        return this.rangeEnd;
      }
      set
      {
        if (value == this.rangeEnd)
          return;
        if (value <= this.rangeStart)
        {
          if (this.IsDesignMode)
            throw new ArgumentOutOfRangeException(nameof (RangeEnd), "Value of '" + (object) value + "' is not valid for 'RangeEnd'. 'RangeEnd' should be greater then 'RangeStart'.");
        }
        else
        {
          this.rangeEnd = value;
          this.OnNotifyPropertyChanged(nameof (RangeEnd));
        }
      }
    }

    [Description("Specifies the gauge's start.")]
    public double RangeStart
    {
      get
      {
        return this.rangeStart;
      }
      set
      {
        if (value == this.rangeStart)
          return;
        if (value >= this.rangeEnd)
        {
          if (this.IsDesignMode)
            throw new ArgumentOutOfRangeException(nameof (RangeStart), "Value of '" + (object) value + "' is not valid for 'RangeStart'. 'RangeStart' should be smaller then 'RangeEnd'.");
        }
        else
        {
          this.rangeStart = value;
          this.OnNotifyPropertyChanged(nameof (RangeStart));
        }
      }
    }

    [Description("Determines the angle value starting from the StartAngle to draw an arc in clockwise direction.")]
    public double SweepAngle
    {
      get
      {
        return this.sweepAngle;
      }
      set
      {
        if (value == this.sweepAngle)
          return;
        this.sweepAngle = value;
        this.OnNotifyPropertyChanged(nameof (SweepAngle));
      }
    }

    [Description("Determines the angle value starting from the StartAngle to draw an arc in clockwise direction.")]
    public double StartAngle
    {
      get
      {
        return this.startAngle;
      }
      set
      {
        if (value == this.startAngle)
          return;
        this.startAngle = value;
        this.OnNotifyPropertyChanged(nameof (StartAngle));
      }
    }

    public double EndAngle
    {
      get
      {
        return this.StartAngle + this.SweepAngle;
      }
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      base.OnPropertyChanging(args);
      if (args.Property != RadRadialGaugeElement.ValueProperty || (double) (float) args.NewValue >= this.rangeStart && (double) (float) args.NewValue <= this.rangeEnd)
        return;
      args.Cancel = true;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadRadialGaugeElement.ValueProperty)
        return;
      if ((double) (float) e.NewValue < this.rangeStart || (double) (float) e.NewValue > this.rangeEnd)
      {
        this.Value = (float) Math.Min(Math.Max((double) (float) e.NewValue, this.rangeStart), this.rangeEnd);
      }
      else
      {
        this.OnNotifyPropertyChanged("Value");
        this.OnValueChanged();
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
      this.Invalidate();
    }

    protected void OnValueChanged()
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ValueChanged", (object) this.Value);
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    internal float TextHeight
    {
      get
      {
        return RadGdiGraphics.MeasurementGraphics.MeasureString("1234567890", this.Font).Height;
      }
    }

    public RectangleF GaugeSize
    {
      get
      {
        RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.Size);
        float num1 = Math.Min(clientRectangle.Width, clientRectangle.Height);
        float x = (float) ((double) clientRectangle.X - (double) this.Padding.Left + ((double) clientRectangle.Width - (double) num1 + this.LabelOffset + this.AxisThickness) / 2.0 + 2.0);
        float y = (float) ((double) clientRectangle.Y - (double) this.Padding.Top + ((double) clientRectangle.Height - (double) num1 + this.LabelOffset + this.AxisThickness) / 2.0 + 2.0);
        float num2 = this.labelsPositionInside ? (float) ((double) num1 - this.AxisThickness - 8.0) : (float) ((double) num1 - 2.0 * this.AxisThickness - 8.0);
        return new RectangleF(x, y, num2, num2);
      }
    }

    public PointF GaugeCenter
    {
      get
      {
        RectangleF gaugeSize = this.GaugeSize;
        PointF pointF = new PointF(gaugeSize.X + gaugeSize.Width / 2f, gaugeSize.Y + gaugeSize.Height / 2f);
        pointF.X += gaugeSize.Width * (float) this.CenterOffsetX;
        pointF.Y += gaugeSize.Height * (float) this.CenterOffsetY;
        return pointF;
      }
    }

    internal double LabelGap
    {
      get
      {
        double num = 0.0;
        if (!this.LabelsPositionInside)
          num = this.LabelOffset + (double) this.TextHeight / 2.0;
        return num;
      }
    }

    internal double LabelRadius
    {
      get
      {
        RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.Size);
        return ((double) Math.Min(clientRectangle.Width, clientRectangle.Height) - (double) this.TextHeight) / 2.0;
      }
    }

    [Description("Controls the RadRadialGauge's offset in vertical direction.")]
    public double CenterOffsetY
    {
      get
      {
        return this.centerOffsetY;
      }
      set
      {
        this.centerOffsetY = value;
        this.OnNotifyPropertyChanged(nameof (CenterOffsetY));
      }
    }

    [Description("Controls the RadRadialGauge's offset in horizontal  direction.")]
    public double CenterOffsetX
    {
      get
      {
        return this.centerOffsetX;
      }
      set
      {
        this.centerOffsetX = value;
        this.OnNotifyPropertyChanged(nameof (CenterOffsetX));
      }
    }

    internal PointF PointFromCenter(PointF center, double radius, double angle)
    {
      double num = angle * Math.PI / 180.0;
      return new PointF(center.X + (float) radius * Convert.ToSingle(Math.Cos(num)), center.Y + (float) radius * Convert.ToSingle(Math.Sin(num)));
    }

    public double CalculateNeedleAngle(
      double value,
      double rangeStart,
      double rangeEnd,
      double startAngle,
      double sweepAngle)
    {
      value = Math.Min(Math.Max(value, rangeStart), rangeEnd);
      double num = (value - rangeStart) / (rangeEnd - rangeStart);
      return startAngle + sweepAngle * num;
    }

    internal RectangleF GetRectangleFromRadius(float radius)
    {
      return new RectangleF(this.GaugeCenter.X - radius, this.GaugeCenter.Y - radius, radius + radius, radius + radius);
    }

    internal RectangleF GetRectangleFromRadius(PointF center, float radius)
    {
      return new RectangleF(center.X - radius, center.Y - radius, radius + radius, radius + radius);
    }
  }
}
