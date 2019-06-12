// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiRadialGradientBrush
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.Drawing
{
  public class GdiRadialGradientBrush : RadRadialGradientBrush
  {
    private GraphicsPath graphicsPath = new GraphicsPath();
    private Brush brush;
    private GradientStop[] gradientStops;

    public GdiRadialGradientBrush(
      PointF center,
      float xRadius,
      float yRadius,
      GradientStop[] gradientStops)
    {
      this.gradientStops = gradientStops;
      this.graphicsPath = new GraphicsPath();
      RectangleF rect = new RectangleF(new PointF(center.X - xRadius, center.Y - yRadius), new SizeF(xRadius * 2f, yRadius * 2f));
      if (!rect.IsEmpty)
      {
        this.graphicsPath.AddEllipse(rect);
        this.brush = (Brush) new PathGradientBrush(this.graphicsPath)
        {
          CenterPoint = center,
          CenterColor = gradientStops[0].Color,
          InterpolationColors = GdiUtility.GetColorBlend(gradientStops)
        };
      }
      else
        this.brush = (Brush) new SolidBrush(Color.Transparent);
    }

    protected override void DisposeUnmanagedResources()
    {
      base.DisposeUnmanagedResources();
      if (this.brush != null)
        this.brush.Dispose();
      if (this.graphicsPath == null)
        return;
      this.graphicsPath.Dispose();
    }

    public override object RawBrush
    {
      get
      {
        return (object) this.brush;
      }
    }

    public override PointF GetCenter()
    {
      PathGradientBrush brush = this.brush as PathGradientBrush;
      if (brush == null)
        return PointF.Empty;
      return brush.CenterPoint;
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
      PathGradientBrush brush = this.brush as PathGradientBrush;
      if (brush == null)
        return;
      brush.CenterColor = gradientStops[0].Color;
      brush.InterpolationColors = GdiUtility.GetColorBlend(gradientStops);
    }

    public override float GetRadiusX()
    {
      return this.graphicsPath.GetBounds().Width / 2f;
    }

    public override float GetRadiusY()
    {
      return this.graphicsPath.GetBounds().Height / 2f;
    }
  }
}
