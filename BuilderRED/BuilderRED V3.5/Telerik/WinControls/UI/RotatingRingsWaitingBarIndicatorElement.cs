// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RotatingRingsWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class RotatingRingsWaitingBarIndicatorElement : BaseRingWaitingBarIndicatorElement
  {
    public static RadProperty OuterRingSweepAngleProperty = RadProperty.Register(nameof (OuterRingSweepAngle), typeof (int), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 120, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OuterRingWidthProperty = RadProperty.Register(nameof (OuterRingWidth), typeof (int), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OuterRingBackgroundColorProperty = RadProperty.Register(nameof (OuterRingBackgroundColor), typeof (Color), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(121, 148, 185), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerRingSweepAngleProperty = RadProperty.Register(nameof (InnerRingSweepAngle), typeof (int), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 120, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerRingStartAngleProperty = RadProperty.Register(nameof (InnerRingStartAngle), typeof (int), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerRingWidthProperty = RadProperty.Register(nameof (InnerRingWidth), typeof (int), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty InnerRingBackgroundColorProperty = RadProperty.Register(nameof (InnerRingBackgroundColor), typeof (Color), typeof (RotatingRingsWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(121, 148, 185), ElementPropertyOptions.AffectsDisplay));
    private int currentOuterRingAngle;
    private int currentInnerRingAngle;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.currentOuterRingAngle = this.InitialStartElementAngle;
      this.currentInnerRingAngle = this.InnerRingStartAngle;
    }

    [RadPropertyDefaultValue("OuterRingSweepAngle", typeof (RotatingRingsWaitingBarIndicatorElement))]
    [RadRange(0, 360)]
    [Category("Appearance")]
    public int OuterRingSweepAngle
    {
      get
      {
        return (int) this.GetValue(RotatingRingsWaitingBarIndicatorElement.OuterRingSweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.OuterRingSweepAngleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("OuterRingWidth", typeof (RotatingRingsWaitingBarIndicatorElement))]
    [Category("Appearance")]
    [RadRange(0, 2147483647)]
    public int OuterRingWidth
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(RotatingRingsWaitingBarIndicatorElement.OuterRingWidthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.OuterRingWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("OuterRingBackgroundColor", typeof (RotatingRingsWaitingBarIndicatorElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color OuterRingBackgroundColor
    {
      get
      {
        return (Color) this.GetValue(RotatingRingsWaitingBarIndicatorElement.OuterRingBackgroundColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.OuterRingBackgroundColorProperty, (object) value);
      }
    }

    [RadRange(0, 360)]
    [RadPropertyDefaultValue("InnerRingSweepAngle", typeof (RotatingRingsWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public int InnerRingSweepAngle
    {
      get
      {
        return (int) this.GetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingSweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingSweepAngleProperty, (object) value);
      }
    }

    [RadRange(0, 360)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("InnerRingStartAngle", typeof (RotatingRingsWaitingBarIndicatorElement))]
    public int InnerRingStartAngle
    {
      get
      {
        return (int) this.GetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingStartAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingStartAngleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("InnerRingWidth", typeof (RotatingRingsWaitingBarIndicatorElement))]
    [RadRange(0, 360)]
    public int InnerRingWidth
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingWidthProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingWidthProperty, (object) value);
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("InnerRingBackgroundColor", typeof (RotatingRingsWaitingBarIndicatorElement))]
    public Color InnerRingBackgroundColor
    {
      get
      {
        return (Color) this.GetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingBackgroundColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RotatingRingsWaitingBarIndicatorElement.InnerRingBackgroundColorProperty, (object) value);
      }
    }

    public override void Animate(int step)
    {
      base.Animate(step);
      if (this.RotationDirection == RotationDirection.CounterClockwise)
        step *= -1;
      this.currentOuterRingAngle += step;
      this.currentOuterRingAngle %= 360;
      this.currentInnerRingAngle -= step;
      this.currentInnerRingAngle %= 360;
      this.Invalidate();
    }

    public override void ResetAnimation()
    {
      this.currentOuterRingAngle = this.InitialStartElementAngle;
      this.currentInnerRingAngle = this.InnerRingStartAngle;
    }

    public override void Paint(Graphics graphics, Rectangle rectangle)
    {
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      Point rectangleCenter = this.GetRectangleCenter(rectangle);
      float num1 = (float) this.Radius * 2f - (float) this.OuterRingWidth;
      RectangleF rect = new RectangleF((float) rectangleCenter.X - num1 / 2f, (float) rectangleCenter.Y - num1 / 2f, num1, num1);
      using (Pen pen = new Pen(this.OuterRingBackgroundColor))
      {
        pen.Width = (float) this.OuterRingWidth;
        graphics.DrawArc(pen, rect, (float) this.currentOuterRingAngle, 359f);
        pen.Color = this.ElementColor;
        graphics.DrawArc(pen, rect, (float) this.currentOuterRingAngle, (float) this.OuterRingSweepAngle);
      }
      float num2 = (float) this.InnerRadius * 2f - (float) this.InnerRingWidth;
      rect = new RectangleF((float) rectangleCenter.X - num2 / 2f, (float) rectangleCenter.Y - num2 / 2f, num2, num2);
      using (Pen pen = new Pen(this.InnerRingBackgroundColor))
      {
        pen.Width = (float) this.InnerRingWidth;
        graphics.DrawArc(pen, rect, (float) this.currentInnerRingAngle, 359f);
        pen.Color = this.ElementColor2;
        graphics.DrawArc(pen, rect, (float) this.currentInnerRingAngle, (float) this.InnerRingSweepAngle);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RotatingRingsWaitingBarIndicatorElement.OuterRingSweepAngleProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("OuterRingSweepAngle must be between 0 and 360.");
      }
      else if (e.Property == RotatingRingsWaitingBarIndicatorElement.OuterRingWidthProperty)
      {
        if ((int) e.NewValue < 0)
          throw new ArgumentException("OuterRingWidth must be greater than 0.");
      }
      else if (e.Property == RotatingRingsWaitingBarIndicatorElement.InnerRingSweepAngleProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("InnerRingSweepAngle must be between 0 and 360.");
      }
      else if (e.Property == RotatingRingsWaitingBarIndicatorElement.InnerRingStartAngleProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0 || 360 < newValue)
          throw new ArgumentException("InnerRingStartAngle must be between 0 and 360.");
      }
      else if (e.Property == RotatingRingsWaitingBarIndicatorElement.InnerRingWidthProperty && (int) e.NewValue < 0)
        throw new ArgumentException("InnerRingWidth must be greater than 0.");
    }
  }
}
