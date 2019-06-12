// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ShapeLinesCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;

namespace Telerik.WinControls
{
  public class ShapeLinesCollection
  {
    private List<IShapeCurve> lines;
    private ShapePoint snappedCtrl;
    private PointF snappedPoint;
    private IShapeCurve snappedCurve;

    public List<IShapeCurve> Lines
    {
      get
      {
        return this.lines;
      }
    }

    public ShapePoint SnappedCtrlPoint
    {
      get
      {
        return this.snappedCtrl;
      }
    }

    public PointF SnappedPoint
    {
      get
      {
        return this.snappedPoint;
      }
    }

    public IShapeCurve SnappedCurve
    {
      get
      {
        return this.snappedCurve;
      }
    }

    public ShapeLinesCollection()
    {
      this.Reset();
    }

    public void CopyFrom(ShapeLinesCollection shape)
    {
      this.Reset();
      List<ShapePoint> shapePointList1 = new List<ShapePoint>();
      List<ShapePoint> shapePointList2 = new List<ShapePoint>();
      for (int index1 = 0; index1 < shape.lines.Count; ++index1)
      {
        IShapeCurve el = shape.lines[index1].Create();
        for (int index2 = 0; index2 < shape.lines[index1].ControlPoints.Length; ++index2)
        {
          int index3 = shapePointList1.IndexOf(shape.lines[index1].ControlPoints[index2]);
          if (index3 == -1)
          {
            shapePointList1.Add(shape.lines[index1].ControlPoints[index2]);
            index3 = shapePointList1.IndexOf(shape.lines[index1].ControlPoints[index2]);
            shapePointList2.Insert(index3, new ShapePoint(shape.lines[index1].ControlPoints[index2].Location));
          }
          el.ControlPoints[index2] = shapePointList2[index3];
        }
        this.Add(el);
        el.Update();
      }
    }

    public void Reset()
    {
      if (this.lines == null)
        this.lines = new List<IShapeCurve>();
      else
        this.lines.Clear();
      this.snappedCtrl = (ShapePoint) null;
      this.snappedPoint = new PointF();
    }

    public void Add(IShapeCurve el)
    {
      this.lines.Add(el);
    }

    public bool Remove(IShapeCurve el)
    {
      return this.lines.Remove(el);
    }

    public void UpdateShape()
    {
      for (int index = 0; index < this.lines.Count; ++index)
        this.lines[index].Update();
    }

    public RectangleF GetBoundingRect()
    {
      if (this.lines.Count < 1)
        return new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
      float val1_1;
      float num1 = val1_1 = this.lines[0].FirstPoint.X;
      float val1_2;
      float num2 = val1_2 = this.lines[0].FirstPoint.Y;
      for (int index1 = 0; index1 < this.lines.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.lines[index1].Points.Length; ++index2)
        {
          num1 = Math.Min(num1, this.lines[index1].Points[index2].X);
          num2 = Math.Min(num2, this.lines[index1].Points[index2].Y);
          val1_1 = Math.Max(val1_1, this.lines[index1].Points[index2].X);
          val1_2 = Math.Max(val1_2, this.lines[index1].Points[index2].Y);
        }
      }
      return new RectangleF(num1, num2, val1_1 - num1, val1_2 - num2);
    }

    public void DeletePoint(ShapePoint pt)
    {
      ShapePoint changeTo;
      IShapeCurve line;
      if (pt.IsLocked || !this.FindLineContainingPoint(pt, out changeTo, out line))
        return;
      this.Remove(line);
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].FirstPoint == pt)
        {
          this.lines[index].FirstPoint = changeTo;
          this.lines[index].Update();
        }
        if (this.lines[index].LastPoint == pt)
        {
          this.lines[index].LastPoint = changeTo;
          this.lines[index].Update();
        }
      }
    }

    public void ConvertCurve(IShapeCurve curve)
    {
      if (curve is ShapeLine)
      {
        ShapeBezier shapeBezier = new ShapeBezier(this.snappedCurve.FirstPoint, new ShapePoint((float) ((2.0 * (double) this.snappedCurve.Points[0].X + (double) this.snappedCurve.Points[1].X) / 3.0), (float) ((2.0 * (double) this.snappedCurve.Points[0].Y + (double) this.snappedCurve.Points[1].Y) / 3.0)), new ShapePoint((float) (((double) this.snappedCurve.Points[0].X + 2.0 * (double) this.snappedCurve.Points[1].X) / 3.0), (float) (((double) this.snappedCurve.Points[0].Y + 2.0 * (double) this.snappedCurve.Points[1].Y) / 3.0)), this.snappedCurve.LastPoint);
        this.lines[this.lines.IndexOf(curve)] = (IShapeCurve) shapeBezier;
      }
      else
      {
        ShapeLine shapeLine = new ShapeLine(this.snappedCurve.FirstPoint, this.snappedCurve.LastPoint);
        this.lines[this.lines.IndexOf(curve)] = (IShapeCurve) shapeLine;
      }
      this.snappedCurve = (IShapeCurve) null;
    }

    public void InsertPoint(IShapeCurve curve, PointF atPoint)
    {
      if (curve == null)
        return;
      if (curve is ShapeLine)
      {
        ShapePoint lastPoint = curve.LastPoint;
        ShapePoint from = new ShapePoint(atPoint);
        ShapeLine shapeLine = new ShapeLine(from, lastPoint);
        curve.LastPoint = from;
        this.lines.Insert(this.lines.IndexOf(curve) + 1, (IShapeCurve) shapeLine);
      }
      else
      {
        if (!(curve is ShapeBezier))
          return;
        PointF from = new PointF();
        PointF to = new PointF();
        ShapePoint lastPoint = this.snappedCurve.LastPoint;
        ShapePoint shapePoint = new ShapePoint(this.snappedPoint);
        if (!(curve as ShapeBezier).TangentAt(this.snappedPoint, ref from, ref to))
          return;
        ShapeBezier shapeBezier = new ShapeBezier();
        shapeBezier.ControlPoints[0] = shapePoint;
        shapeBezier.ControlPoints[1] = new ShapePoint(to);
        shapeBezier.ControlPoints[2] = curve.ControlPoints[2];
        shapeBezier.ControlPoints[3] = curve.LastPoint;
        curve.ControlPoints[2] = new ShapePoint(from);
        curve.ControlPoints[3] = shapePoint;
        curve.Update();
        shapeBezier.Update();
        this.lines.Insert(this.lines.IndexOf(curve) + 1, (IShapeCurve) shapeBezier);
      }
    }

    private bool FindLineContainingPoint(
      ShapePoint pt,
      out ShapePoint changeTo,
      out IShapeCurve line)
    {
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].FirstPoint == pt)
        {
          changeTo = this.lines[index].LastPoint;
          line = this.lines[index];
          return true;
        }
        if (this.lines[index].LastPoint == pt)
        {
          changeTo = this.lines[index].FirstPoint;
          line = this.lines[index];
          return true;
        }
      }
      changeTo = (ShapePoint) null;
      line = (IShapeCurve) null;
      return false;
    }

    public bool SnapToCtrlPoints(PointF pt, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance * snapDistance;
      this.snappedCtrl = (ShapePoint) null;
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].SnapToCtrlPoints(pt, snapDistance))
        {
          float num2 = ShapePoint.DistSquared(pt, this.lines[index].SnappedCtrlPoint.Location);
          if ((double) num1 >= (double) num2)
          {
            num1 = num2;
            this.snappedCtrl = this.lines[index].SnappedCtrlPoint;
            flag = true;
          }
        }
      }
      return flag;
    }

    public bool SnapToSegments(PointF pt, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance * snapDistance;
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].SnapToCurve(pt, snapDistance))
        {
          float num2 = ShapePoint.DistSquared(this.lines[index].SnappedPoint, pt);
          if ((double) num1 >= (double) num2)
          {
            num1 = num2;
            this.snappedPoint = this.lines[index].SnappedPoint;
            this.snappedCurve = this.lines[index];
            flag = true;
          }
        }
      }
      return flag;
    }

    public bool SnapToExtensions(PointF pt, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance * snapDistance;
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].SnapToCurveExtension(pt, snapDistance))
        {
          float num2 = ShapePoint.DistSquared(this.lines[index].SnappedPoint, pt);
          if ((double) num1 >= (double) num2)
          {
            num1 = num2;
            this.snappedPoint = this.lines[index].SnappedPoint;
            flag = true;
          }
        }
      }
      return flag;
    }

    public bool SnapToHorizontal(PointF pt, float yVal, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance * snapDistance;
      PointF pointF = new PointF();
      IShapeCurve shapeCurve = (IShapeCurve) null;
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].SnapToHorizontal(pt, yVal, snapDistance))
        {
          float num2 = ShapePoint.DistSquared(this.lines[index].SnappedPoint, pt);
          if ((double) num1 >= (double) num2)
          {
            num1 = num2;
            pointF = this.lines[index].SnappedPoint;
            shapeCurve = this.lines[index];
            flag = true;
          }
        }
      }
      if (flag)
      {
        this.snappedPoint = pointF;
        this.snappedCurve = shapeCurve;
      }
      return flag;
    }

    public bool SnapToVertical(PointF pt, float xVal, float snapDistance)
    {
      bool flag = false;
      float num1 = snapDistance * snapDistance;
      PointF pointF = new PointF();
      IShapeCurve shapeCurve = (IShapeCurve) null;
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index].SnapToVertical(pt, xVal, snapDistance))
        {
          float num2 = ShapePoint.DistSquared(this.lines[index].SnappedPoint, pt);
          if ((double) num1 >= (double) num2)
          {
            num1 = num2;
            pointF = this.lines[index].SnappedPoint;
            shapeCurve = this.lines[index];
            flag = true;
          }
        }
      }
      if (flag)
      {
        this.snappedPoint = pointF;
        this.snappedCurve = shapeCurve;
      }
      return flag;
    }

    public bool SnapToGrid(PointF pt, PointF gridPt, int type, float snapDistance)
    {
      int num = 0;
      PointF b = new PointF();
      IShapeCurve shapeCurve = (IShapeCurve) null;
      if ((type & 1) != 0)
      {
        num += this.SnapToHorizontal(pt, gridPt.Y, snapDistance) ? 1 : 0;
        b = this.snappedPoint;
        shapeCurve = this.snappedCurve;
      }
      if ((type & 2) != 0)
        num += this.SnapToVertical(pt, gridPt.X, snapDistance) ? 2 : 0;
      bool flag;
      switch (num)
      {
        case 1:
          flag = true;
          break;
        case 2:
          flag = true;
          break;
        case 3:
          if ((double) ShapePoint.DistSquared(pt, b) < (double) ShapePoint.DistSquared(pt, this.snappedPoint))
          {
            this.snappedPoint = b;
            this.snappedCurve = shapeCurve;
          }
          flag = true;
          break;
        default:
          flag = false;
          break;
      }
      return flag;
    }

    public GraphicsPath CreatePath(Rectangle dimension, Rectangle bound)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index] is ShapeBezier)
          graphicsPath.AddBezier(this.lines[index].ControlPoints[0].GetPoint(dimension, bound), this.lines[index].ControlPoints[1].GetPoint(dimension, bound), this.lines[index].ControlPoints[2].GetPoint(dimension, bound), this.lines[index].ControlPoints[3].GetPoint(dimension, bound));
        else if (this.lines[index] is ShapeLine)
          graphicsPath.AddLine(this.lines[index].FirstPoint.GetPoint(dimension, bound), this.lines[index].LastPoint.GetPoint(dimension, bound));
      }
      graphicsPath.CloseAllFigures();
      return graphicsPath;
    }

    public bool isSerializable()
    {
      for (int index = 0; index < this.lines.Count - 1; ++index)
      {
        if (this.lines[index].LastPoint != this.lines[index + 1].FirstPoint)
          return false;
      }
      return this.lines[0].FirstPoint == this.lines[this.lines.Count - 1].LastPoint;
    }

    public string SerializeProperties()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.lines.Count; ++index)
      {
        if (this.lines[index] is ShapeBezier)
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0},{1},{2},{3},{4},{5},{6},{7}:", (object) this.lines[index].ControlPoints[0].X, (object) this.lines[index].ControlPoints[0].Y, (object) true, (object) this.lines[index].ControlPoints[1].X, (object) this.lines[index].ControlPoints[1].Y, (object) this.lines[index].ControlPoints[2].X, (object) this.lines[index].ControlPoints[2].Y, (object) 0);
        else if (this.lines[index] is ShapeLine)
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0},{1},{2},{3},{4},{5},{6},{7}:", (object) this.lines[index].ControlPoints[0].X, (object) this.lines[index].ControlPoints[0].Y, (object) false, (object) 0, (object) 0, (object) 0, (object) 0, (object) 0);
      }
      return stringBuilder.ToString();
    }

    public void DeserializeProperties(string propertiesString)
    {
      string[] strArray1 = propertiesString.Split(':');
      IShapeCurve el = (IShapeCurve) null;
      ShapePoint shapePoint1 = new ShapePoint();
      ShapePoint shapePoint2 = shapePoint1;
      for (int index = 1; index < strArray1.Length - 1; ++index)
      {
        string[] strArray2 = strArray1[index].Split(',');
        shapePoint2.Set(float.Parse(strArray2[0], (IFormatProvider) CultureInfo.InvariantCulture), float.Parse(strArray2[1], (IFormatProvider) CultureInfo.InvariantCulture));
        if (bool.Parse(strArray2[2]))
        {
          el = (IShapeCurve) new ShapeBezier();
          el.ControlPoints[0] = shapePoint2;
          NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
          el.ControlPoints[1] = new ShapePoint(float.Parse(strArray2[3], (IFormatProvider) CultureInfo.InvariantCulture), float.Parse(strArray2[4], (IFormatProvider) CultureInfo.InvariantCulture));
          el.ControlPoints[2] = new ShapePoint(float.Parse(strArray2[5], (IFormatProvider) CultureInfo.InvariantCulture), float.Parse(strArray2[6], (IFormatProvider) CultureInfo.InvariantCulture));
          shapePoint2 = new ShapePoint();
          el.ControlPoints[3] = shapePoint2;
        }
        else
        {
          el = (IShapeCurve) new ShapeLine();
          el.ControlPoints[0] = shapePoint2;
          shapePoint2 = new ShapePoint();
          el.ControlPoints[1] = shapePoint2;
        }
        this.Add(el);
      }
      if (el == null)
        return;
      el.LastPoint = shapePoint1;
    }

    public ShapePoint GetFirstPoint()
    {
      if (this.lines == null || this.lines.Count < 1)
        return (ShapePoint) null;
      return this.lines[0].FirstPoint;
    }

    public ShapePoint GetLastPoint()
    {
      if (this.lines == null || this.lines.Count < 1)
        return (ShapePoint) null;
      return this.lines[this.lines.Count - 1].LastPoint;
    }

    public IShapeCurve GetFirstCurve()
    {
      if (this.lines == null || this.lines.Count < 1)
        return (IShapeCurve) null;
      return this.lines[0];
    }

    public IShapeCurve GetLastCurve()
    {
      if (this.lines == null || this.lines.Count < 1)
        return (IShapeCurve) null;
      return this.lines[this.lines.Count - 1];
    }
  }
}
