// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiLinearGradientBrush
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.Drawing
{
  public class GdiLinearGradientBrush : RadLinearGradientBrush
  {
    private LinearGradientBrush rawBrush;
    private GradientStop[] gradientStops;
    private float angle;
    private RadLinearGradientMode mode;

    public GdiLinearGradientBrush(
      RectangleF rectangle,
      GradientStop[] gradientStops,
      RadLinearGradientMode mode)
    {
      if (mode == RadLinearGradientMode.None)
        throw new NotSupportedException("The brush cannot be initialized with " + (object) mode);
      this.mode = mode;
      this.angle = 0.0f;
      this.gradientStops = gradientStops;
      rectangle.Width = Math.Max(1f, rectangle.Width);
      rectangle.Height = Math.Max(1f, rectangle.Height);
      this.rawBrush = new LinearGradientBrush(rectangle, Color.Black, Color.Black, (LinearGradientMode) this.mode);
      LinearGradientBrush rawBrush = this.rawBrush;
      GradientStop[] gradientStops1;
      if (gradientStops.Length == 1)
        gradientStops1 = new GradientStop[2]
        {
          new GradientStop(gradientStops[0].Color, 0.0f),
          new GradientStop(gradientStops[0].Color, 1f)
        };
      else
        gradientStops1 = gradientStops;
      ColorBlend colorBlend = GdiUtility.GetColorBlend(gradientStops1);
      rawBrush.InterpolationColors = colorBlend;
      this.rawBrush.WrapMode = WrapMode.TileFlipXY;
    }

    public GdiLinearGradientBrush(RectangleF rectangle, GradientStop[] gradientStops, float angle)
    {
      this.angle = angle;
      this.mode = RadLinearGradientMode.None;
      this.gradientStops = gradientStops;
      rectangle.Width = Math.Max(1f, rectangle.Width);
      rectangle.Height = Math.Max(1f, rectangle.Height);
      this.rawBrush = new LinearGradientBrush(rectangle, Color.Black, Color.Black, angle);
      this.rawBrush.WrapMode = WrapMode.TileFlipXY;
      this.rawBrush.InterpolationColors = GdiUtility.GetColorBlend(gradientStops);
    }

    protected override void DisposeUnmanagedResources()
    {
      this.DisposeManagedResources();
      if (this.rawBrush == null)
        return;
      this.rawBrush.Dispose();
      this.rawBrush = (LinearGradientBrush) null;
    }

    public override object RawBrush
    {
      get
      {
        return (object) this.rawBrush;
      }
    }

    public override RectangleF GetRectangle()
    {
      return this.rawBrush.Rectangle;
    }

    public override float GetAngle()
    {
      return this.angle;
    }

    public override RadLinearGradientMode GetLinearGradientMode()
    {
      return this.mode;
    }

    public override GradientStop[] GetGradientStops()
    {
      return this.gradientStops;
    }

    public void SetGradientStops(GradientStop[] gradientStops)
    {
      if (gradientStops == null || gradientStops.Length == 0)
        throw new ArgumentException(nameof (gradientStops));
      this.gradientStops = gradientStops;
      this.rawBrush.InterpolationColors = GdiUtility.GetColorBlend(gradientStops);
    }
  }
}
