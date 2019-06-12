// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiRoundedRectangle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.Drawing
{
  public class GdiRoundedRectangle : RoundedRectangle
  {
    private GraphicsPath path;

    public GdiRoundedRectangle(RectangleF rectangle, float radius)
      : base(rectangle, radius)
    {
    }

    public override object RawRoundedRectangle
    {
      get
      {
        if (this.path == null)
          this.path = this.GetRoundedRect(this.Rectangle, this.Radius);
        return (object) this.path;
      }
    }

    protected override void OnPropertyChanged(string propertyName)
    {
      base.OnPropertyChanged(propertyName);
      this.DisposePath();
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.DisposePath();
    }

    private void DisposePath()
    {
      if (this.path == null)
        return;
      this.path.Dispose();
      this.path = (GraphicsPath) null;
    }

    private GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
    {
      if ((double) radius <= 0.0)
      {
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddRectangle(baseRect);
        graphicsPath.CloseFigure();
        return graphicsPath;
      }
      if ((double) radius >= (double) Math.Min(baseRect.Width, baseRect.Height) / 2.0)
        return this.GetCapsule(baseRect);
      float num = radius * 2f;
      SizeF size = new SizeF(num, num);
      RectangleF rect = new RectangleF(baseRect.Location, size);
      GraphicsPath graphicsPath1 = new GraphicsPath();
      graphicsPath1.AddArc(rect, 180f, 90f);
      rect.X = baseRect.Right - num;
      graphicsPath1.AddArc(rect, 270f, 90f);
      rect.Y = baseRect.Bottom - num;
      graphicsPath1.AddArc(rect, 0.0f, 90f);
      rect.X = baseRect.Left;
      graphicsPath1.AddArc(rect, 90f, 90f);
      graphicsPath1.CloseFigure();
      return graphicsPath1;
    }

    private GraphicsPath GetCapsule(RectangleF baseRect)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if ((double) baseRect.Height > 0.0)
      {
        if ((double) baseRect.Width > 0.0)
        {
          try
          {
            if ((double) baseRect.Width > (double) baseRect.Height)
            {
              float height = baseRect.Height;
              SizeF size = new SizeF(height, height);
              RectangleF rect = new RectangleF(baseRect.Location, size);
              graphicsPath.AddArc(rect, 90f, 180f);
              rect.X = baseRect.Right - height;
              graphicsPath.AddArc(rect, 270f, 180f);
            }
            else if ((double) baseRect.Width < (double) baseRect.Height)
            {
              float width = baseRect.Width;
              SizeF size = new SizeF(width, width);
              RectangleF rect = new RectangleF(baseRect.Location, size);
              graphicsPath.AddArc(rect, 180f, 180f);
              rect.Y = baseRect.Bottom - width;
              graphicsPath.AddArc(rect, 0.0f, 180f);
            }
            else
              graphicsPath.AddEllipse(baseRect);
          }
          catch (Exception ex)
          {
            graphicsPath.AddEllipse(baseRect);
          }
          finally
          {
            graphicsPath.CloseFigure();
          }
          return graphicsPath;
        }
      }
      graphicsPath.AddEllipse(baseRect);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }
  }
}
