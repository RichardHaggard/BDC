// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShapeBezier
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls
{
  public class ShapeBezier : IShapeCurve
  {
    private ShapePoint[] ctrl;
    private ShapePoint[] points;
    private int detailLevel;
    private PointF snappedPoint;
    private ShapePoint snappedCtrl;
    private int snapSegmentNum;

    public ShapeBezier()
    {
      this.detailLevel = 32;
      this.ctrl = new ShapePoint[4];
      this.points = new ShapePoint[this.detailLevel];
      this.snappedCtrl = (ShapePoint) null;
      this.snappedPoint = new PointF();
      this.snapSegmentNum = -1;
    }

    public ShapeBezier(ShapePoint from, ShapePoint control1, ShapePoint control2, ShapePoint to)
    {
      this.detailLevel = 32;
      this.ctrl = new ShapePoint[4];
      this.ctrl[0] = from;
      this.ctrl[1] = control1;
      this.ctrl[2] = control2;
      this.ctrl[3] = to;
      this.snappedCtrl = (ShapePoint) null;
      this.snappedPoint = new PointF();
      this.points = new ShapePoint[this.detailLevel];
      this.snapSegmentNum = -1;
      this.GenerateSegments();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<PointF> TestPoints
    {
      get
      {
        return (List<PointF>) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ShapePoint[] Points
    {
      get
      {
        if (this.IsModified)
          this.GenerateSegments();
        return this.points;
      }
    }

    [Description("The starting point of the bezier curve.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Layout")]
    public ShapePoint FirstPoint
    {
      get
      {
        return this.ctrl[0];
      }
      set
      {
        if (value == null)
          return;
        this.ctrl[0] = value;
      }
    }

    [Description("The first control point of the bezier curve. The line between this FirstPoint and this point is tangent to the bezier curve.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Layout")]
    public ShapePoint Ctrl1
    {
      get
      {
        return this.ctrl[1];
      }
      set
      {
        if (value == null)
          return;
        this.ctrl[1] = value;
      }
    }

    [Browsable(true)]
    [Description("The second control point of the bezier curve. The line between this point and LastPoint is tangent to the bezier curve.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Layout")]
    public ShapePoint Ctrl2
    {
      get
      {
        return this.ctrl[2];
      }
      set
      {
        if (value == null)
          return;
        this.ctrl[2] = value;
      }
    }

    [Description("The ending point of the bezier curve.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Category("Layout")]
    public ShapePoint LastPoint
    {
      get
      {
        return this.ctrl[3];
      }
      set
      {
        this.ctrl[3] = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapePoint[] Extensions
    {
      get
      {
        return this.ctrl;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PointF SnappedPoint
    {
      get
      {
        return this.snappedPoint;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapePoint SnappedCtrlPoint
    {
      get
      {
        return this.snappedCtrl;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ShapePoint[] ControlPoints
    {
      get
      {
        return this.ctrl;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsModified
    {
      get
      {
        for (int index = 0; index < 4; ++index)
        {
          if (this.ctrl[index].IsModified)
            return true;
        }
        return false;
      }
    }

    private void GenerateSegments()
    {
      float num1 = (float) (3.0 * ((double) this.ctrl[1].X - (double) this.ctrl[0].X));
      float num2 = (float) (3.0 * ((double) this.ctrl[2].X - (double) this.ctrl[1].X)) - num1;
      float num3 = this.ctrl[3].X - this.ctrl[0].X - num1 - num2;
      float num4 = (float) (3.0 * ((double) this.ctrl[1].Y - (double) this.ctrl[0].Y));
      float num5 = (float) (3.0 * ((double) this.ctrl[2].Y - (double) this.ctrl[1].Y)) - num4;
      float num6 = this.ctrl[3].Y - this.ctrl[0].Y - num4 - num5;
      float num7 = 0.0f;
      int index = 0;
      while (index < this.detailLevel)
      {
        float num8 = num7 * num7;
        float num9 = num7 * num8;
        this.points[index] = new ShapePoint((float) ((double) num3 * (double) num9 + (double) num2 * (double) num8 + (double) num1 * (double) num7) + this.ctrl[0].X, (float) ((double) num6 * (double) num9 + (double) num5 * (double) num8 + (double) num4 * (double) num7) + this.ctrl[0].Y);
        ++index;
        num7 += 1f / (float) (this.detailLevel - 1);
      }
    }

    public bool Update()
    {
      this.GenerateSegments();
      return true;
    }

    public bool TangentAt(PointF atPoint, ref PointF from, ref PointF to)
    {
      if (this.snapSegmentNum < 0 || this.snapSegmentNum > this.points.Length - 2)
        return false;
      from = this.points[this.snapSegmentNum].Location;
      to = this.points[this.snapSegmentNum + 1].Location;
      return true;
    }

    public bool SnapToCurve(PointF pt, float snapDistance)
    {
      bool flag = false;
      PointF pointF = new PointF();
      PointF snapPoint = new PointF();
      float num = snapDistance * snapDistance;
      float dist = 0.0f;
      if (this.IsModified)
        return false;
      for (int index = 0; index < this.detailLevel - 1; ++index)
      {
        if (ShapeBezier.SnapToCurveSegment(ref snapPoint, pt, this.points[index], this.points[index + 1], snapDistance, ref dist))
        {
          dist = ShapePoint.DistSquared(pt, snapPoint);
          if ((double) num >= (double) dist)
          {
            num = dist;
            pointF = snapPoint;
            this.snapSegmentNum = index;
            flag = true;
          }
        }
      }
      if (!flag)
        return false;
      this.snappedPoint = pointF;
      return true;
    }

    public bool SnapToCurveExtension(PointF pt, float snapDistance)
    {
      PointF snapPoint1 = new PointF();
      PointF snapPoint2 = new PointF();
      bool flag = true;
      int num = 0;
      float dist1 = 0.0f;
      float dist2 = 0.0f;
      if (!this.ctrl[0].IsModified && !this.ctrl[1].IsModified)
        num += ShapeBezier.SnapToExtension(ref snapPoint1, pt, this.ctrl[0], this.ctrl[1], snapDistance, ref dist1) ? 1 : 0;
      if (!this.ctrl[3].IsModified && !this.ctrl[2].IsModified)
        num += ShapeBezier.SnapToExtension(ref snapPoint2, pt, this.ctrl[3], this.ctrl[2], snapDistance, ref dist2) ? 2 : 0;
      switch (num)
      {
        case 1:
          this.snappedPoint = snapPoint1;
          break;
        case 2:
          this.snappedPoint = snapPoint2;
          break;
        case 3:
          this.snappedPoint = (double) ShapePoint.DistSquared(pt, snapPoint1) > (double) ShapePoint.DistSquared(pt, snapPoint2) ? snapPoint2 : snapPoint1;
          break;
        default:
          flag = false;
          break;
      }
      return flag;
    }

    public bool SnapToCtrlPoints(PointF pt, float snapDistance)
    {
      float num1 = snapDistance * snapDistance;
      this.snappedCtrl = (ShapePoint) null;
      for (int index = 0; index < 4; ++index)
      {
        if (!this.ctrl[index].IsModified)
        {
          float num2 = ShapePoint.DistSquared(this.ctrl[index].Location, pt);
          if ((double) num1 >= (double) num2)
          {
            num1 = num2;
            this.snappedCtrl = this.ctrl[index];
          }
        }
      }
      return this.snappedCtrl != null;
    }

    public bool SnapToVertical(PointF pt, float xVal, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance;
      if (this.IsModified)
        return false;
      for (int index = 0; index < this.points.Length - 1; ++index)
      {
        float num2 = this.points[index + 1].X - this.points[index].X;
        if ((double) num2 != 0.0)
        {
          float num3 = this.points[index + 1].Y - this.points[index].Y;
          float y = this.points[index].Y + (xVal - this.points[index].X) * num3 / num2;
          if (ShapeBezier.IsPointOnSegment(xVal, y, this.points[index], this.points[index + 1]) && (double) Math.Abs(y - pt.Y) <= (double) num1)
          {
            this.snappedPoint.X = xVal;
            this.snappedPoint.Y = y;
            flag = true;
          }
        }
      }
      return flag;
    }

    public bool SnapToHorizontal(PointF pt, float yVal, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance;
      if (this.IsModified)
        return false;
      for (int index = 0; index < this.points.Length - 1; ++index)
      {
        float num2 = this.points[index + 1].Y - this.points[index].Y;
        if ((double) num2 != 0.0)
        {
          float num3 = this.points[index + 1].X - this.points[index].X;
          float x = this.points[index].X + (yVal - this.points[index].Y) * num3 / num2;
          if (ShapeBezier.IsPointOnSegment(x, yVal, this.points[index], this.points[index + 1]) && (double) Math.Abs(x - pt.X) <= (double) num1)
          {
            this.snappedPoint.Y = yVal;
            this.snappedPoint.X = x;
            flag = true;
          }
        }
      }
      return flag;
    }

    private static bool SnapToExtension(
      ref PointF snapPoint,
      PointF pt,
      ShapePoint from,
      ShapePoint to,
      float snapDistance,
      ref float dist)
    {
      float num1 = to.X - from.X;
      float num2 = to.Y - from.Y;
      if ((double) num1 == 0.0 || (double) num2 == 0.0)
      {
        if ((double) num1 == 0.0 && (double) num2 == 0.0)
          return false;
        snapPoint = pt;
        if ((double) num1 == 0.0)
        {
          dist = Math.Abs(pt.X - to.X);
          if ((double) dist >= (double) snapDistance)
            return false;
          snapPoint.X = to.X;
        }
        else
        {
          dist = Math.Abs(pt.Y - to.Y);
          if ((double) dist >= (double) snapDistance)
            return false;
          snapPoint.Y = to.Y;
        }
        return true;
      }
      float num3 = (float) (((double) num1 * ((double) from.Y - (double) pt.Y) - (double) num2 * ((double) from.X - (double) pt.X)) / Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2));
      dist = Math.Abs(num3);
      if ((double) dist > (double) snapDistance)
        return false;
      snapPoint.Y = pt.Y + (float) ((double) Math.Sign(num1) * (double) num3 / Math.Sqrt(1.0 + (double) num2 * (double) num2 / ((double) num1 * (double) num1)));
      snapPoint.X = from.X + (snapPoint.Y - from.Y) * num1 / num2;
      return true;
    }

    private static bool IsPointOnSegment(float x, float y, ShapePoint from, ShapePoint to)
    {
      return ((double) from.X == (double) to.X || (double) x >= (double) Math.Min(from.X, to.X) && (double) x <= (double) Math.Max(from.X, to.X)) && ((double) from.Y == (double) to.Y || (double) y >= (double) Math.Min(from.Y, to.Y) && (double) y <= (double) Math.Max(from.Y, to.Y));
    }

    private static bool SnapToCurveSegment(
      ref PointF snapPoint,
      PointF pt,
      ShapePoint from,
      ShapePoint to,
      float snapDistance,
      ref float dist)
    {
      if (!ShapeBezier.SnapToExtension(ref snapPoint, pt, from, to, snapDistance, ref dist))
        return false;
      return ShapeBezier.IsPointOnSegment(snapPoint.X, snapPoint.Y, from, to);
    }

    public IShapeCurve Create()
    {
      return (IShapeCurve) new ShapeBezier();
    }
  }
}
