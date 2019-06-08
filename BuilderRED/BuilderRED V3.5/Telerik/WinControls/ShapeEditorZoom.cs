// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShapeEditorZoom
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class ShapeEditorZoom
  {
    private float zoomFactor;
    private float zoomMin;
    private float zoomMax;
    private RectangleF workingArea;
    private RectangleF visibleArea;

    public ShapeEditorZoom()
    {
      this.zoomFactor = 1f;
      this.zoomMin = 0.5f;
      this.zoomMax = 2f;
      this.workingArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
      this.visibleArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public ShapeEditorZoom(float minZoom, float maxZoom)
    {
      this.ZoomOutMax = minZoom;
      this.ZoomInMax = maxZoom;
      this.ZoomFactor = 1f;
      this.workingArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
      this.visibleArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public ShapeEditorZoom(float minZoom, float maxZoom, float zoom)
    {
      this.ZoomOutMax = minZoom;
      this.ZoomInMax = maxZoom;
      this.ZoomFactor = zoom;
      this.workingArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
      this.visibleArea = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public float ZoomInMax
    {
      get
      {
        return this.zoomMax;
      }
      set
      {
        if ((double) value < 1.0)
          this.zoomMax = 1f;
        else
          this.zoomMax = value;
      }
    }

    public float ZoomOutMax
    {
      get
      {
        return 1f / this.zoomMin;
      }
      set
      {
        if ((double) value < 1.0)
          this.zoomMin = 1f;
        else
          this.zoomMin = 1f / value;
      }
    }

    public float ZoomFactor
    {
      get
      {
        return this.zoomFactor;
      }
      set
      {
        this.zoomFactor = Math.Max(Math.Min(value, this.zoomMax), this.zoomMin);
      }
    }

    public RectangleF WorkingArea
    {
      get
      {
        return this.workingArea;
      }
      set
      {
        this.workingArea = value;
      }
    }

    public RectangleF VisibleArea
    {
      get
      {
        return this.visibleArea;
      }
      set
      {
        this.visibleArea = value;
      }
    }

    public Size VisibleAreaSize
    {
      get
      {
        return new Size((int) Math.Floor((double) this.visibleArea.Width), (int) Math.Floor((double) this.visibleArea.Height));
      }
      set
      {
        this.visibleArea.Width = (float) value.Width;
        this.visibleArea.Height = (float) value.Height;
      }
    }

    public Point Location
    {
      get
      {
        return new Point((int) Math.Round((double) this.visibleArea.Location.X), (int) Math.Round((double) this.visibleArea.Location.Y));
      }
      set
      {
        this.visibleArea.X = (float) value.X;
        this.visibleArea.Y = (float) value.Y;
      }
    }

    public Size FullArea
    {
      get
      {
        return new Size((int) ((double) this.workingArea.Width * (double) this.zoomFactor), (int) ((double) this.workingArea.Height * (double) this.zoomFactor));
      }
    }

    public float ZoomIn(float step)
    {
      this.ZoomFactor += step;
      return this.zoomFactor;
    }

    public float ZoomOut(float step)
    {
      this.ZoomFactor -= step;
      return this.zoomFactor;
    }

    public bool Zoom(float value)
    {
      float zoomFactor = this.zoomFactor;
      this.ZoomFactor = value;
      return (double) zoomFactor != (double) this.zoomFactor;
    }

    public PointF PtToVirtual(PointF pt)
    {
      pt.X *= this.zoomFactor;
      pt.Y *= this.zoomFactor;
      return pt;
    }

    public PointF VirtualToPt(PointF pt)
    {
      pt.X /= this.zoomFactor;
      pt.Y /= this.zoomFactor;
      return pt;
    }

    public PointF PtToVisible(PointF pt)
    {
      pt.X = pt.X * this.zoomFactor - this.visibleArea.X;
      pt.Y = pt.Y * this.zoomFactor - this.visibleArea.Y;
      return pt;
    }

    public PointF VisibleToPt(PointF pt)
    {
      pt.X = (pt.X + this.visibleArea.X) / this.zoomFactor;
      pt.Y = (pt.Y + this.visibleArea.Y) / this.zoomFactor;
      return pt;
    }

    public RectangleF RectToVirtual(RectangleF rect)
    {
      rect.X *= this.zoomFactor;
      rect.Y *= this.zoomFactor;
      rect.Width *= this.zoomFactor;
      rect.Height *= this.zoomFactor;
      return rect;
    }

    public RectangleF VirtualToRect(RectangleF rect)
    {
      rect.X /= this.zoomFactor;
      rect.Y /= this.zoomFactor;
      rect.Width /= this.zoomFactor;
      rect.Height /= this.zoomFactor;
      return rect;
    }

    public RectangleF RectToVisible(RectangleF rect)
    {
      rect.X = rect.X * this.zoomFactor - this.visibleArea.X;
      rect.Y = rect.Y * this.zoomFactor - this.visibleArea.Y;
      rect.Width *= this.zoomFactor;
      rect.Height *= this.zoomFactor;
      return rect;
    }

    public RectangleF VisibleToRect(RectangleF rect)
    {
      rect.X = (rect.X + this.visibleArea.X) / this.zoomFactor;
      rect.Y = (rect.Y + this.visibleArea.Y) / this.zoomFactor;
      rect.Width /= this.zoomFactor;
      rect.Height /= this.zoomFactor;
      return rect;
    }

    public void ZoomAtPoint(PointF pt, PointF ptInView)
    {
      PointF pointF = this.PtToVirtual(pt);
      this.visibleArea.X = this.workingArea.X + pointF.X - ptInView.X;
      this.visibleArea.Y = this.workingArea.X + pointF.Y - ptInView.Y;
    }

    public void Scroll(SizeF scrollTo)
    {
      this.visibleArea.X = scrollTo.Width;
      this.visibleArea.Y = scrollTo.Height;
    }

    public void Scroll(PointF scrollTo)
    {
      this.visibleArea.X = scrollTo.X;
      this.visibleArea.Y = scrollTo.Y;
    }

    public void ScrollHorizontal(float to)
    {
      this.visibleArea.X = to;
    }

    public void ScrollVertical(float to)
    {
      this.visibleArea.Y = to;
    }

    public void FitToScreen(RectangleF? rect)
    {
      RectangleF? nullable = !rect.HasValue || rect.Value.IsEmpty ? new RectangleF?(this.workingArea) : new RectangleF?(rect.Value);
      this.ZoomFactor = Math.Min((this.visibleArea.Width - 20f) / nullable.Value.Width, (this.visibleArea.Height - 20f) / nullable.Value.Height);
      this.CenterVisible(nullable.Value);
    }

    public void FitToScreen()
    {
    }

    private void CenterVisible(RectangleF rect)
    {
      PointF pointF1 = new PointF(this.visibleArea.Width / 2f, this.visibleArea.Height / 2f);
      PointF pointF2 = new PointF(rect.Width / 2f, rect.Height / 2f);
      this.visibleArea.X = (rect.X + pointF2.X) * this.zoomFactor - pointF1.X;
      this.visibleArea.Y = (rect.Y + pointF2.Y) * this.zoomFactor - pointF1.Y;
    }
  }
}
