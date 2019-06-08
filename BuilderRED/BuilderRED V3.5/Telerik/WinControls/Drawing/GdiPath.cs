// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiPath
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.Drawing
{
  public class GdiPath : Path
  {
    private GraphicsPath graphicsPath = new GraphicsPath();

    public override void AddLine(float x1, float y1, float x2, float y2)
    {
      this.graphicsPath.AddLine(x1, y1, x2, y2);
    }

    public override void AddArc(
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle)
    {
      this.graphicsPath.AddArc(x, y, width, height, startAngle, sweepAngle);
    }

    public override void AddBezier(
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3,
      float x4,
      float y4)
    {
      this.graphicsPath.AddBezier(x1, y1, x2, y2, x3, y3, x4, y4);
    }

    public override void AddRectangle(float x1, float y1, float width, float height)
    {
      this.graphicsPath.AddRectangle(new RectangleF(x1, y1, width, height));
    }

    public override void AddEllipse(RectangleF rectangle)
    {
      this.graphicsPath.AddEllipse(rectangle);
    }

    public override RectangleF GetBounds()
    {
      return this.graphicsPath.GetBounds();
    }

    protected override void OnBeginFigure()
    {
      base.OnBeginFigure();
      this.graphicsPath.StartFigure();
    }

    protected override void OnEndFigure()
    {
      base.OnEndFigure();
      this.graphicsPath.CloseFigure();
    }

    protected override void DisposeUnmanagedResources()
    {
      base.DisposeUnmanagedResources();
      this.graphicsPath.Dispose();
    }

    public override object RawPath
    {
      get
      {
        return (object) this.graphicsPath;
      }
    }

    protected override bool OnAttached(object rawPath)
    {
      if (!(rawPath is GraphicsPath))
        return base.OnAttached(rawPath);
      this.graphicsPath = (GraphicsPath) rawPath;
      return true;
    }
  }
}
