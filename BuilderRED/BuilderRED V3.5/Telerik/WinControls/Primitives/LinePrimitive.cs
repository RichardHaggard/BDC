// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.LinePrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class LinePrimitive : FillPrimitive
  {
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (LinePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineWidthProperty = RadProperty.Register(nameof (LineWidth), typeof (int), typeof (LinePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (SeparatorOrientation), typeof (SepOrientation), typeof (LinePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SepOrientation.Horizontal, ElementPropertyOptions.AffectsDisplay));

    public virtual int LineWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(LinePrimitive.LineWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(LinePrimitive.LineWidthProperty, (object) value);
      }
    }

    public virtual SepOrientation SeparatorOrientation
    {
      get
      {
        return (SepOrientation) this.GetValue(LinePrimitive.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(LinePrimitive.OrientationProperty, (object) value);
      }
    }

    public virtual int SweepAngle
    {
      get
      {
        return (int) this.GetValue(LinePrimitive.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LinePrimitive.SweepAngleProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.FitToSizeMode = RadFitToSizeMode.FitToParentPadding;
    }

    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      switch (this.SeparatorOrientation)
      {
        case SepOrientation.Vertical:
          this.SweepAngle = 90;
          break;
        case SepOrientation.Horizontal:
          this.SweepAngle = 0;
          break;
      }
      int val2 = 4;
      Color[] colorStops = new Color[Math.Min(Math.Max(this.NumberOfColors, 1), val2)];
      float[] colorOffsets = new float[Math.Min(Math.Max(this.NumberOfColors, 1), val2)];
      if (this.NumberOfColors > 0)
      {
        colorStops[0] = this.BackColor;
        colorOffsets[0] = 0.0f;
      }
      if (this.NumberOfColors > 1)
      {
        colorStops[colorStops.Length - 1] = this.BackColor2;
        colorOffsets[colorStops.Length - 1] = 1f;
      }
      if (this.NumberOfColors > 2)
      {
        colorStops[1] = this.BackColor2;
        colorOffsets[1] = this.GradientPercentage;
        colorStops[colorStops.Length - 1] = this.BackColor3;
        colorOffsets[colorStops.Length - 1] = 1f;
      }
      if (this.NumberOfColors > 3)
      {
        colorStops[2] = this.BackColor3;
        colorOffsets[2] = this.GradientPercentage2;
        colorStops[colorStops.Length - 1] = this.BackColor4;
        colorOffsets[colorStops.Length - 1] = 1f;
      }
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      if (this.SeparatorOrientation == SepOrientation.Vertical)
        rectangle.Width = this.LineWidth;
      else
        rectangle.Height = this.LineWidth;
      switch (this.GradientStyle)
      {
        case GradientStyles.Solid:
          g.FillRectangle(rectangle, this.BackColor);
          break;
        case GradientStyles.Linear:
        case GradientStyles.Radial:
          if (this.NumberOfColors < 2)
          {
            g.FillRectangle(rectangle, this.BackColor);
            break;
          }
          g.FillGradientRectangle(rectangle, colorStops, colorOffsets, this.GradientStyle, this.GradientAngle, this.GradientPercentage, this.GradientPercentage2);
          break;
        case GradientStyles.Glass:
          g.FillGlassRectangle(rectangle, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2);
          break;
        case GradientStyles.OfficeGlass:
          g.FillOfficeGlassRectangle(rectangle, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2, true);
          break;
        case GradientStyles.OfficeGlassRect:
          g.FillOfficeGlassRectangle(rectangle, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2, false);
          break;
        case GradientStyles.Gel:
          g.FillGellRectangle(rectangle, colorStops, this.GradientPercentage, this.GradientPercentage2);
          break;
        case GradientStyles.Vista:
          g.FillVistaRectangle(rectangle, this.BackColor, this.BackColor2, this.BackColor3, this.BackColor4, this.GradientPercentage, this.GradientPercentage2);
          break;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      if (this.SeparatorOrientation == SepOrientation.Horizontal)
        return new SizeF(0.0f, 1f);
      return new SizeF(1f, 0.0f);
    }
  }
}
