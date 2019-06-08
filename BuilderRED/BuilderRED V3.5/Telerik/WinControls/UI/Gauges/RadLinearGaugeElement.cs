// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadLinearGaugeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI.Gauges
{
  [Description("Represent main needle element. This element is container for all other elements in the Gauge")]
  public class RadLinearGaugeElement : LightVisualElement
  {
    public static RadProperty ValueProperty = RadProperty.Register(nameof (Value), typeof (float), typeof (RadLinearGaugeElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 50f, ElementPropertyOptions.AffectsDisplay));
    private float rangeEnd = 100f;
    private const int DefaultBorderWidth = 2;
    private readonly RadItemOwnerCollection items;
    private bool vertical;
    private float rangeStart;

    [Description("The ValueChanged event fires when the value is modified.")]
    public event EventHandler ValueChanged;

    [Description(" The OrientationChanged event fires when the orientation of the gauges is changed.")]
    public event EventHandler OrientationChanged;

    public RadLinearGaugeElement()
    {
      this.DrawFill = false;
      this.items = new RadItemOwnerCollection((RadElement) this);
      this.items.ItemTypes = new Type[6]
      {
        typeof (LinearGaugeBar),
        typeof (LinearGaugeTicks),
        typeof (LinearGaugeLabels),
        typeof (LinearGaugeSingleLabel),
        typeof (LinearGaugeLine),
        typeof (LinearGaugeNeedleIndicator)
      };
    }

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

    protected internal float GaugeLength
    {
      get
      {
        if (!this.vertical)
          return this.GaugeSize.Width;
        return this.GaugeSize.Height;
      }
    }

    protected internal float GaugeOtherSizeLength
    {
      get
      {
        if (this.vertical)
          return this.GaugeSize.Width;
        return this.GaugeSize.Height;
      }
    }

    protected internal float GaugeFullLength
    {
      get
      {
        return this.vertical ? (float) this.Size.Height : (float) this.Size.Width;
      }
    }

    protected internal float GaugeOtherSizeFullLength
    {
      get
      {
        return !this.vertical ? (float) this.Size.Height : (float) this.Size.Width;
      }
    }

    protected internal float GaugeStartPoint
    {
      get
      {
        return !this.vertical ? (float) this.BoundingRectangle.Location.X : (float) this.BoundingRectangle.Location.Y;
      }
    }

    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    public virtual RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Description("Specifies the gauge's value")]
    [Bindable(true)]
    public float Value
    {
      get
      {
        return (float) this.GetValue(RadLinearGaugeElement.ValueProperty);
      }
      set
      {
        if ((double) value == (double) this.Value || (double) value < (double) this.RangeStart || (double) value > (double) this.rangeEnd)
          return;
        int num = (int) this.SetValue(RadLinearGaugeElement.ValueProperty, (object) value);
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    [Description("Set or Get Gauge Orientation")]
    [DefaultValue(false)]
    public bool Vertical
    {
      get
      {
        return this.vertical;
      }
      set
      {
        this.vertical = value;
        this.OnOrientationChanged();
        this.OnNotifyPropertyChanged(nameof (Vertical));
      }
    }

    internal RectangleF GaugeSize
    {
      get
      {
        return this.GetClientRectangle((SizeF) this.Size);
      }
    }

    internal PointF GaugeCenter
    {
      get
      {
        RectangleF gaugeSize = this.GaugeSize;
        PointF pointF = new PointF(gaugeSize.X + gaugeSize.Width / 2f, gaugeSize.Y + gaugeSize.Height / 2f);
        if (this.vertical)
          pointF = new PointF(gaugeSize.Y + gaugeSize.Height / 2f, gaugeSize.X + gaugeSize.Width / 2f);
        return pointF;
      }
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      base.OnPropertyChanging(args);
      if (args.Property != RadLinearGaugeElement.ValueProperty || (double) (float) args.NewValue >= (double) this.rangeStart && (double) (float) args.NewValue <= (double) this.rangeEnd)
        return;
      args.Cancel = true;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadLinearGaugeElement.ValueProperty)
        return;
      float newValue = (float) e.NewValue;
      if ((double) newValue < (double) this.rangeStart || (double) newValue > (double) this.rangeEnd)
      {
        this.Value = Math.Min(Math.Max((float) e.NewValue, this.rangeStart), this.rangeEnd);
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

    protected void OnOrientationChanged()
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, EventArgs.Empty);
    }

    internal float CalculateValueToGaugeScale(float value)
    {
      value = Math.Min(Math.Max(this.RangeStart, value), this.RangeEnd);
      float num = !this.Vertical ? this.GaugeSize.Width : this.GaugeSize.Height;
      if (!this.vertical)
        return (float) ((double) num * ((double) value - (double) this.RangeStart) / ((double) this.RangeEnd - (double) this.RangeStart));
      return this.GaugeSize.Height - (float) ((double) num * ((double) value - (double) this.RangeStart) / ((double) this.RangeEnd - (double) this.RangeStart));
    }

    internal PointF PointFromCenter(PointF center, double radius, double angle)
    {
      double num = angle * Math.PI / 180.0;
      return new PointF(center.X + (float) radius * Convert.ToSingle(Math.Cos(num)), center.Y + (float) radius * Convert.ToSingle(Math.Sin(num)));
    }
  }
}
