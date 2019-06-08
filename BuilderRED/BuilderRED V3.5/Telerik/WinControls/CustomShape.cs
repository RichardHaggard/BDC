// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CustomShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Telerik.WinControls
{
  [Designer("Telerik.WinControls.OldShapeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [DesignTimeVisible(false)]
  [ToolboxItem(false)]
  public class CustomShape : ElementShape
  {
    private ShapeLinesCollection shape;
    private Rectangle dimension;

    public CustomShape()
    {
      this.shape = new ShapeLinesCollection();
    }

    public CustomShape(Rectangle rect)
    {
      this.shape = new ShapeLinesCollection();
      this.dimension = rect;
      this.AddLine((PointF) rect.Location, new PointF((float) rect.Right, (float) rect.Top));
      this.AppendLine(new PointF((float) rect.Right, (float) rect.Bottom));
      this.AppendLine(new PointF((float) rect.Left, (float) rect.Bottom));
      this.CloseFigureUsingLine();
    }

    public CustomShape(IContainer container)
    {
      this.shape = new ShapeLinesCollection();
      container.Add((IComponent) this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle Dimension
    {
      get
      {
        return this.dimension;
      }
      set
      {
        this.dimension = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ShapeLinesCollection Shape
    {
      get
      {
        return this.shape;
      }
      set
      {
        if (value == null)
          return;
        this.shape = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string AsString
    {
      get
      {
        return this.SerializeProperties();
      }
      set
      {
        this.DeserializeProperties(value);
      }
    }

    public CustomShape Clone()
    {
      CustomShape customShape = new CustomShape();
      customShape.shape.CopyFrom(this.shape);
      customShape.dimension = new Rectangle(this.dimension.Location, this.dimension.Size);
      return customShape;
    }

    public void CopyFrom(CustomShape cs)
    {
      if (cs == null)
        return;
      Rectangle dimension = cs.Dimension;
      this.shape.CopyFrom(cs.shape);
      this.dimension = new Rectangle(cs.Dimension.Location, cs.Dimension.Size);
    }

    public override string SerializeProperties()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0},{1},{2},{3}:", (object) this.dimension.X, (object) this.dimension.Y, (object) this.dimension.Width, (object) this.dimension.Height) + this.shape.SerializeProperties();
    }

    public override void DeserializeProperties(string propertiesString)
    {
      this.shape.Reset();
      string[] strArray = propertiesString.Split(':')[0].Split(',');
      if (strArray.Length < 4)
        return;
      this.dimension = new Rectangle((int) Math.Round((double) float.Parse(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture)), (int) Math.Round((double) float.Parse(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture)), (int) Math.Round((double) float.Parse(strArray[2], (IFormatProvider) CultureInfo.InvariantCulture)), (int) Math.Round((double) float.Parse(strArray[3], (IFormatProvider) CultureInfo.InvariantCulture)));
      this.shape.DeserializeProperties(propertiesString);
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath path = this.shape.CreatePath(this.dimension, bounds);
      this.MirrorPath(path, (RectangleF) bounds);
      return path;
    }

    public RectangleF GetBoundingRectangle()
    {
      return this.shape.GetBoundingRect();
    }

    public bool DoFixDimension(bool forceFix)
    {
      if (!this.dimension.IsEmpty && !forceFix)
        return true;
      RectangleF boundingRect = this.shape.GetBoundingRect();
      if (boundingRect.IsEmpty)
        return false;
      this.dimension = new Rectangle((int) Math.Round((double) boundingRect.X), (int) Math.Round((double) boundingRect.Y), (int) Math.Round((double) boundingRect.Width), (int) Math.Round((double) boundingRect.Height));
      return true;
    }

    public bool DoFixDimension()
    {
      return this.DoFixDimension(false);
    }

    public void AddLine(PointF from, PointF to)
    {
      this.shape.Add((IShapeCurve) new ShapeLine(new ShapePoint(from), new ShapePoint(to)));
    }

    public void AddBezier(PointF from, PointF ctrl1, PointF ctrl2, PointF to)
    {
      this.shape.Add((IShapeCurve) new ShapeBezier(new ShapePoint(from), new ShapePoint(ctrl1), new ShapePoint(ctrl2), new ShapePoint(to)));
    }

    public bool AppendLine(PointF to)
    {
      ShapePoint lastPoint = this.shape.GetLastPoint();
      if (lastPoint == null)
        return false;
      this.shape.Add((IShapeCurve) new ShapeLine(lastPoint, new ShapePoint(to)));
      return true;
    }

    public bool AppendBezier(PointF ctrl1, PointF ctrl2, PointF to)
    {
      ShapePoint lastPoint = this.shape.GetLastPoint();
      if (lastPoint == null)
        return false;
      this.shape.Add((IShapeCurve) new ShapeBezier(lastPoint, new ShapePoint(ctrl1), new ShapePoint(ctrl2), new ShapePoint(to)));
      return true;
    }

    public bool CloseFigureUsingLine()
    {
      ShapePoint firstPoint = this.shape.GetFirstPoint();
      ShapePoint lastPoint = this.shape.GetLastPoint();
      if (firstPoint == null || lastPoint == null)
        return false;
      this.shape.Add((IShapeCurve) new ShapeLine(lastPoint, firstPoint));
      return true;
    }

    public bool CloseFigureUsingBezier(PointF ctrl1, PointF ctrl2)
    {
      ShapePoint firstPoint = this.shape.GetFirstPoint();
      ShapePoint lastPoint = this.shape.GetLastPoint();
      if (firstPoint == null || lastPoint == null)
        return false;
      this.shape.Add((IShapeCurve) new ShapeBezier(lastPoint, new ShapePoint(ctrl1), new ShapePoint(ctrl2), firstPoint));
      return true;
    }

    protected bool CreateClosedShape(ShapePoint[] pts)
    {
      int length = pts.Length;
      if (length < 2)
        return false;
      this.shape.Reset();
      for (int index = 0; index < length; ++index)
        this.shape.Add((IShapeCurve) new ShapeLine(pts[index], pts[(index + 1) % length]));
      return this.DoFixDimension(true);
    }

    public bool CreateClosedShape(PointF[] points)
    {
      if (points == null || points.Length < 2)
        return false;
      ShapePoint[] pts = new ShapePoint[points.Length];
      for (int index = 0; index < pts.Length; ++index)
        pts[index] = new ShapePoint(points[index]);
      return this.CreateClosedShape(pts);
    }

    public bool CreateClosedShape(List<PointF> points)
    {
      if (points == null || points.Count < 2)
        return false;
      ShapePoint[] pts = new ShapePoint[points.Count];
      for (int index = 0; index < pts.Length; ++index)
        pts[index] = new ShapePoint(points[index]);
      return this.CreateClosedShape(pts);
    }

    public void CreateRectangleShape(PointF from, PointF to)
    {
      this.CreateClosedShape(new ShapePoint[4]
      {
        new ShapePoint(from),
        new ShapePoint(to.X, from.Y),
        new ShapePoint(to),
        new ShapePoint(from.X, to.Y)
      });
    }

    public void CreateRectangleShape(float x, float y, float width, float height)
    {
      this.CreateRectangleShape(new PointF(x, y), new PointF(x + width, y + height));
    }

    public void CreateRectangleShape(PointF pos, SizeF size)
    {
      this.CreateRectangleShape(new PointF(pos.X, pos.Y), new PointF(pos.X + size.Width, pos.Y + size.Height));
    }

    public void CreateRectangleShape(Rectangle rect)
    {
      this.CreateRectangleShape((PointF) rect.Location, new PointF((float) rect.Right, (float) rect.Bottom));
    }
  }
}
