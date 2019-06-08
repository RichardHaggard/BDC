// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShapesIntersection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls
{
  public class ShapesIntersection
  {
    private IShapeCurve curve1;
    private IShapeCurve curve2;
    private List<PointF> curveIntersections;
    private List<PointF> extIntersections;

    public IShapeCurve Curve1
    {
      get
      {
        return this.curve1;
      }
      set
      {
        this.curve1 = value;
      }
    }

    public IShapeCurve Curve2
    {
      get
      {
        return this.curve2;
      }
      set
      {
        this.curve2 = value;
      }
    }

    public List<PointF> CurveIntersections
    {
      get
      {
        return this.curveIntersections;
      }
    }

    public ShapesIntersection()
    {
      this.curve1 = (IShapeCurve) null;
      this.curve2 = (IShapeCurve) null;
      this.curveIntersections = new List<PointF>();
      this.extIntersections = new List<PointF>();
    }

    public ShapesIntersection(IShapeCurve c1, IShapeCurve c2)
    {
      this.curve1 = c1;
      this.curve2 = c2;
      this.curveIntersections = new List<PointF>();
      this.extIntersections = new List<PointF>();
    }

    public bool IntersectCurves()
    {
      bool flag = false;
      if (this.curve1 == null || this.curve2 == null || this.curve1 == this.curve2)
        return false;
      RectangleF rect1 = (RectangleF) new Rectangle();
      RectangleF rect2 = (RectangleF) new Rectangle();
      this.curveIntersections.Clear();
      for (int index1 = 0; index1 < this.curve1.Points.Length - 1; ++index1)
      {
        rect1 = this.SegmentBoundingRect(rect1, this.curve1.Points[index1], this.curve1.Points[index1 + 1]);
        for (int index2 = 0; index2 < this.curve2.Points.Length - 1; ++index2)
        {
          rect2 = this.SegmentBoundingRect(rect2, this.curve2.Points[index2], this.curve2.Points[index2 + 1]);
          rect2.Intersect(rect1);
          PointF res;
          if (!rect2.IsEmpty && ShapesIntersection.IntersectSegment(out res, this.curve1.Points[index1].Location, this.curve1.Points[index1 + 1].Location, this.curve2.Points[index2].Location, this.curve2.Points[index2 + 1].Location))
            this.curveIntersections.Add(res);
        }
      }
      return flag;
    }

    private RectangleF SegmentBoundingRect(
      RectangleF rect,
      ShapePoint pt1,
      ShapePoint pt2)
    {
      rect.X = Math.Min(pt1.X, pt2.X);
      rect.Y = Math.Min(pt1.Y, pt2.Y);
      rect.Width = Math.Abs(pt1.X - pt2.X);
      rect.Height = Math.Abs(pt1.Y - pt2.Y);
      return rect;
    }

    public static bool Intersect(out PointF res, PointF a, PointF b, PointF c, PointF d)
    {
      res = new PointF(0.0f, 0.0f);
      float num1 = (float) (((double) a.X - (double) b.X) * ((double) c.Y - (double) d.Y) - ((double) a.Y - (double) b.Y) * ((double) c.X - (double) d.X));
      if ((double) num1 == 0.0)
        return false;
      float num2 = (float) ((double) a.X * (double) b.Y - (double) b.X * (double) a.Y);
      float num3 = (float) ((double) c.X * (double) d.Y - (double) d.X * (double) c.Y);
      res.X = (float) ((double) num2 * ((double) c.X - (double) d.X) - (double) num3 * ((double) a.X - (double) b.X)) / num1;
      res.Y = (float) ((double) num2 * ((double) c.Y - (double) d.Y) - (double) num3 * ((double) a.Y - (double) b.Y)) / num1;
      return true;
    }

    public static bool IntersectSegment(out PointF res, PointF a, PointF b, PointF c, PointF d)
    {
      ShapesIntersection.Intersect(out res, a, b, c, d);
      return (double) res.X >= (double) Math.Min(a.X, b.X) && (double) res.X <= (double) Math.Max(a.X, b.X) && ((double) res.Y >= (double) Math.Min(a.Y, b.Y) && (double) res.Y <= (double) Math.Max(a.Y, b.Y)) && ((double) res.X >= (double) Math.Min(c.X, d.X) && (double) res.X <= (double) Math.Max(c.X, d.X) && ((double) res.Y >= (double) Math.Min(c.Y, d.Y) && (double) res.Y <= (double) Math.Max(c.Y, d.Y)));
    }
  }
}
