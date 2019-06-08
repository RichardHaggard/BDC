// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.RadBulletGraph
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI.Gauges
{
  [ToolboxItem(true)]
  [Description("The RadLinearGauge control is designed to display a a single quantitative measure.")]
  public class RadBulletGraph : RadLinearGauge
  {
    private float comparativeMeasure = 50f;
    private float featuredMeasure = 75f;

    public RadBulletGraph()
    {
      this.Items.ItemTypes = new Type[8]
      {
        typeof (BulletGraphQualitativeBar),
        typeof (BulletGraphFeaturedMeasureBar),
        typeof (LinearGaugeBar),
        typeof (LinearGaugeTicks),
        typeof (LinearGaugeLabels),
        typeof (LinearGaugeSingleLabel),
        typeof (LinearGaugeLine),
        typeof (LinearGaugeNeedleIndicator)
      };
      this.ForeColor = Color.Black;
    }

    [DefaultValue(75f)]
    [Bindable(true)]
    [Description("This portion of the bullet graph displays the primary data.")]
    public float FeaturedMeasure
    {
      get
      {
        return this.featuredMeasure;
      }
      set
      {
        if ((double) this.featuredMeasure == (double) value)
          return;
        foreach (GaugeVisualElement gaugeVisualElement in (RadItemCollection) this.Items)
        {
          BulletGraphFeaturedMeasureBar featuredMeasureBar = gaugeVisualElement as BulletGraphFeaturedMeasureBar;
          if (featuredMeasureBar != null)
          {
            featuredMeasureBar.RangeEnd = value + featuredMeasureBar.BindEndRangeOffset;
            this.Invalidate();
            this.featuredMeasure = value;
            this.OnNotifyPropertyChanged(nameof (FeaturedMeasure));
            this.OnFeaturedMeasureChanged();
          }
        }
      }
    }

    [Bindable(true)]
    [Description("Presents a value which should be less visually dominant than the featured measure, but easy to see in relation to the featured measure.")]
    [DefaultValue(50f)]
    public virtual float ComparativeMeasure
    {
      get
      {
        return this.comparativeMeasure;
      }
      set
      {
        if ((double) this.comparativeMeasure == (double) value)
          return;
        foreach (GaugeVisualElement gaugeVisualElement in (RadItemCollection) this.Items)
        {
          LinearGaugeNeedleIndicator gaugeNeedleIndicator = gaugeVisualElement as LinearGaugeNeedleIndicator;
          if (gaugeNeedleIndicator != null)
          {
            gaugeNeedleIndicator.Value = value + gaugeNeedleIndicator.BindOffset;
            this.Invalidate();
            this.OnNotifyPropertyChanged(nameof (ComparativeMeasure));
            this.OnComparativeMeasureChanged();
          }
        }
        this.comparativeMeasure = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the ForeColor of the control. This is actually the ForeColor property of the root element.")]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [Description(" The FeaturedMeasureChanged event fires when the FeaturedMeasure property of the gauges is changed.")]
    public event EventHandler FeaturedMeasureChanged;

    private void OnFeaturedMeasureChanged()
    {
      if (this.FeaturedMeasureChanged == null)
        return;
      this.FeaturedMeasureChanged((object) this, EventArgs.Empty);
    }

    [Description(" The ComparativeMeasureChanged event fires when the ComparativeMeasure property of the gauges is changed.")]
    public event EventHandler ComparativeMeasureChanged;

    private void OnComparativeMeasureChanged()
    {
      if (this.ComparativeMeasureChanged == null)
        return;
      this.ComparativeMeasureChanged((object) this, EventArgs.Empty);
    }
  }
}
