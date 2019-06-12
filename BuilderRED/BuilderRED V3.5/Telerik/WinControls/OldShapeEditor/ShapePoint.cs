// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapeEditor.ShapePoint
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.OldShapeEditor
{
  public class ShapePoint : ShapePointBase
  {
    private ShapePointBase controlPoint1 = new ShapePointBase();
    private ShapePointBase controlPoint2 = new ShapePointBase();
    private bool bezier;

    [Category("Appearance")]
    [Description("the bezier curve control point 1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ShapePointBase ControlPoint1
    {
      get
      {
        return this.controlPoint1;
      }
      set
      {
        this.controlPoint1 = value;
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("The bezier curve control point 2")]
    public ShapePointBase ControlPoint2
    {
      get
      {
        return this.controlPoint2;
      }
      set
      {
        this.controlPoint2 = value;
      }
    }

    [DefaultValue(false)]
    [Description("Determines if this point marks the begin of a bezier curve")]
    [Category("Appearance")]
    public bool Bezier
    {
      get
      {
        return this.bezier;
      }
      set
      {
        this.bezier = value;
      }
    }

    public ShapePoint()
    {
    }

    public ShapePoint(int x, int y)
      : base((float) x, (float) y)
    {
    }

    public ShapePoint(Point point)
      : base(point)
    {
    }

    public ShapePoint(ShapePoint point)
      : base((ShapePointBase) point)
    {
      this.ControlPoint1 = new ShapePointBase(point.ControlPoint1);
      this.ControlPoint2 = new ShapePointBase(point.ControlPoint2);
      this.Bezier = point.bezier;
    }

    private ShapePoint.LineDirections GetLineDirection(ShapePointBase nextPoint)
    {
      if ((double) this.X == (double) nextPoint.X)
        return (double) this.Y < (double) nextPoint.Y ? ShapePoint.LineDirections.South : ShapePoint.LineDirections.Nord;
      if ((double) this.Y == (double) nextPoint.Y)
        return (double) this.X < (double) nextPoint.X ? ShapePoint.LineDirections.West : ShapePoint.LineDirections.East;
      if ((double) this.X < (double) nextPoint.X)
        return (double) this.Y < (double) nextPoint.Y ? ShapePoint.LineDirections.SouthWest : ShapePoint.LineDirections.NordWest;
      return (double) this.Y < (double) nextPoint.Y ? ShapePoint.LineDirections.NordEast : ShapePoint.LineDirections.SouthEast;
    }

    public void CreateBezier(ShapePointBase nextPoint)
    {
      int lineDirection = (int) this.GetLineDirection(nextPoint);
      this.ControlPoint1.Set(this.X + 10f, this.Y);
      this.ControlPoint2.Set(nextPoint.X - 10f, nextPoint.Y);
    }

    public Point[] GetCurve(ShapePoint nextPoint)
    {
      double x1 = (double) this.X;
      double x2 = (double) nextPoint.X;
      double x3 = (double) this.controlPoint1.X;
      double x4 = (double) this.controlPoint2.X;
      double y1 = (double) this.Y;
      double y2 = (double) nextPoint.Y;
      double y3 = (double) this.controlPoint1.Y;
      double y4 = (double) this.controlPoint2.Y;
      double num1 = 3.0 * (x3 - x1);
      double num2 = 3.0 * (x4 - x3) - num1;
      double num3 = x2 - x1 - num1 - num2;
      double num4 = 3.0 * (y3 - y1);
      double num5 = 3.0 * (y4 - y3) - num4;
      double num6 = y2 - y1 - num4 - num5;
      Point[] pointArray = new Point[10];
      for (int index = 0; index < 10; ++index)
      {
        double num7 = (double) index / 9.0;
        double num8 = num7 * num7;
        double num9 = num8 * num7;
        pointArray[index] = new Point((int) (num3 * num9 + num2 * num8 + num1 * num7 + x1), (int) (num6 * num9 + num5 * num8 + num4 * num7 + y1));
      }
      return pointArray;
    }

    public bool IsVisible(ShapePoint nextPoint, Point pt, int width)
    {
      if (this.bezier)
        return this.IsCurveVisible(this.GetCurve(nextPoint), pt, (double) width);
      return this.IsLineVisible(this.GetPoint(), nextPoint.GetPoint(), pt, (double) width);
    }

    private bool IsLineVisible(Point pt1, Point pt2, Point pt, double radius)
    {
      double num1 = (double) (pt1.Y - pt2.Y);
      double num2 = (double) (pt2.X - pt1.X);
      double num3 = (double) (pt1.X * pt2.Y - pt2.X * pt1.Y);
      if (Math.Abs((num1 * (double) pt.X + num2 * (double) pt.Y + num3) / Math.Sqrt(num1 * num1 + num2 * num2)) < radius)
      {
        double num4 = (double) Math.Min(pt1.X, pt2.X) - radius;
        double num5 = (double) Math.Max(pt1.X, pt2.X) + radius;
        double num6 = (double) Math.Min(pt1.Y, pt2.Y) - radius;
        double num7 = (double) Math.Max(pt1.Y, pt2.Y) + radius;
        if (num4 <= (double) pt.X && (double) pt.X <= num5 && (num6 <= (double) pt.Y && (double) pt.Y <= num7))
          return true;
      }
      return false;
    }

    private bool IsCurveVisible(Point[] points, Point pt, double radius)
    {
      for (int index = 0; index < points.Length - 1; ++index)
      {
        if (this.IsLineVisible(points[index], points[index + 1], pt, radius))
          return true;
      }
      return false;
    }

    internal enum LineDirections
    {
      South,
      Nord,
      East,
      West,
      SouthEast,
      SouthWest,
      NordEast,
      NordWest,
    }

    internal enum LinePositions
    {
      Horizontal,
      Vertical,
    }
  }
}
