// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.Path
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.Drawing
{
  public abstract class Path : DisposableObject
  {
    public abstract object RawPath { get; }

    public abstract void AddLine(float x1, float y1, float x2, float y2);

    public abstract void AddArc(
      float x,
      float y,
      float width,
      float height,
      float startAngle,
      float sweepAngle);

    public abstract void AddBezier(
      float x1,
      float y1,
      float x2,
      float y2,
      float x3,
      float y3,
      float x4,
      float y4);

    public abstract void AddRectangle(float x1, float y1, float width, float height);

    public abstract void AddEllipse(RectangleF rectangle);

    public abstract RectangleF GetBounds();

    public void AddArc(RectangleF arc, float startAngle, float sweepAngle)
    {
      this.AddArc(arc.X, arc.Y, arc.Width, arc.Height, startAngle, sweepAngle);
    }

    public void AddRectangle(RectangleF rectangle)
    {
      this.AddRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    }

    public static Path CreatePath(DrawingMode mode)
    {
      if (mode == DrawingMode.GdiPlus)
        return (Path) new GdiPath();
      if (mode == DrawingMode.Direct2D)
        return (Path) null;
      return (Path) null;
    }

    public void EndFigure()
    {
      this.OnEndFigure();
    }

    public void BeginFigure()
    {
      this.OnBeginFigure();
    }

    public bool Attach(object rawPath)
    {
      return this.OnAttached(rawPath);
    }

    protected virtual bool OnAttached(object rawPath)
    {
      return false;
    }

    protected virtual void OnBeginFigure()
    {
    }

    protected virtual void OnEndFigure()
    {
    }
  }
}
