// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShapeLine
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls
{
  public class ShapeLine : IShapeCurve
  {
    private ShapePoint[] points;
    private PointF snapPoint;
    private ShapePoint snapCtrl;

    public ShapeLine()
    {
      this.points = new ShapePoint[2];
      this.snapPoint = new PointF();
      this.snapCtrl = (ShapePoint) null;
    }

    public ShapeLine(ShapePoint from, ShapePoint to)
    {
      this.points = new ShapePoint[2];
      this.points[0] = from;
      this.points[1] = to;
      this.snapPoint = new PointF();
      this.snapCtrl = (ShapePoint) null;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<PointF> TestPoints
    {
      get
      {
        return (List<PointF>) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapePoint[] Points
    {
      get
      {
        return this.points;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("The starting point of the line")]
    [Category("Layout")]
    public ShapePoint FirstPoint
    {
      get
      {
        return this.points[0];
      }
      set
      {
        this.points[0] = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Layout")]
    [Description("The ending point of the line")]
    [Browsable(true)]
    public ShapePoint LastPoint
    {
      get
      {
        return this.points[1];
      }
      set
      {
        this.points[1] = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ShapePoint[] Extensions
    {
      get
      {
        return this.points;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PointF SnappedPoint
    {
      get
      {
        return this.snapPoint;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapePoint SnappedCtrlPoint
    {
      get
      {
        return this.snapCtrl;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapePoint[] ControlPoints
    {
      get
      {
        return this.points;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsModified
    {
      get
      {
        if (!this.points[0].IsModified)
          return this.points[1].IsModified;
        return true;
      }
    }

    public bool Update()
    {
      return true;
    }

    public IShapeCurve Create()
    {
      return (IShapeCurve) new ShapeLine();
    }

    public bool SnapToCurve(PointF pt, float snapDistance)
    {
      return this.SnapToCurveExtension(pt, snapDistance) && ((double) this.points[0].X == (double) this.points[1].X || (double) this.snapPoint.X >= (double) Math.Min(this.points[0].X, this.points[1].X) && (double) this.snapPoint.X <= (double) Math.Max(this.points[0].X, this.points[1].X)) && ((double) this.points[0].Y == (double) this.points[1].Y || (double) this.snapPoint.Y >= (double) Math.Min(this.points[0].Y, this.points[1].Y) && (double) this.snapPoint.Y <= (double) Math.Max(this.points[0].Y, this.points[1].Y));
    }

    public bool SnapToCurveExtension(PointF pt, float snapDistance)
    {
      float num1 = this.points[1].X - this.points[0].X;
      float num2 = this.points[1].Y - this.points[0].Y;
      if (this.IsModified)
        return false;
      if ((double) num1 == 0.0 || (double) num2 == 0.0)
      {
        if ((double) num1 == 0.0 && (double) num2 == 0.0)
          return false;
        this.snapPoint = pt;
        if ((double) num1 == 0.0)
        {
          if ((double) Math.Abs(pt.X - this.points[1].X) >= (double) snapDistance)
            return false;
          this.snapPoint.X = this.points[1].X;
        }
        else
        {
          if ((double) Math.Abs(pt.Y - this.points[1].Y) >= (double) snapDistance)
            return false;
          this.snapPoint.Y = this.points[1].Y;
        }
        return true;
      }
      float num3 = (float) (((double) num1 * ((double) this.points[0].Y - (double) pt.Y) - (double) num2 * ((double) this.points[0].X - (double) pt.X)) / Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2));
      if ((double) Math.Abs(num3) > (double) snapDistance)
        return false;
      this.snapPoint.Y = pt.Y + (float) ((double) Math.Sign(num1) * (double) num3 / Math.Sqrt(1.0 + (double) num2 * (double) num2 / ((double) num1 * (double) num1)));
      this.snapPoint.X = this.points[0].X + (this.snapPoint.Y - this.points[0].Y) * num1 / num2;
      return true;
    }

    public bool SnapToCtrlPoints(PointF pt, float snapDistance)
    {
      float num1 = snapDistance * snapDistance;
      float num2 = num1 + 2f;
      float num3 = num1 + 2f;
      if (!this.points[0].IsModified)
        num2 = ShapePoint.DistSquared(this.points[0].Location, pt);
      if (!this.points[1].IsModified)
        num3 = ShapePoint.DistSquared(this.points[1].Location, pt);
      ShapePoint point;
      if ((double) num2 < (double) num3)
      {
        point = this.points[0];
      }
      else
      {
        num2 = num3;
        point = this.points[1];
      }
      if ((double) num2 >= (double) num1)
        return false;
      this.snapCtrl = point;
      return true;
    }

    public bool SnapToVertical(PointF pt, float xVal, float snapDistance)
    {
      if (this.IsModified)
        return false;
      float num1 = this.points[1].X - this.points[0].X;
      if ((double) num1 == 0.0)
        return false;
      float num2 = this.points[1].Y - this.points[0].Y;
      this.snapPoint.X = xVal;
      this.snapPoint.Y = this.points[0].Y + (this.snapPoint.X - this.points[0].X) * num2 / num1;
      return (double) this.snapPoint.Y - (double) pt.Y <= (double) snapDistance;
    }

    public bool SnapToHorizontal(PointF pt, float yVal, float snapDistance)
    {
      if (this.IsModified)
        return false;
      float num1 = this.points[1].Y - this.points[0].Y;
      if ((double) num1 == 0.0)
        return false;
      float num2 = this.points[1].X - this.points[0].X;
      this.snapPoint.Y = yVal;
      this.snapPoint.X = this.points[0].X + (this.snapPoint.Y - this.points[0].Y) * num2 / num1;
      return (double) this.snapPoint.X - (double) pt.X <= (double) snapDistance;
    }
  }
}
