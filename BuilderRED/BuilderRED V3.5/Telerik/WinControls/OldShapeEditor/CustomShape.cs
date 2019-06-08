// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapeEditor.CustomShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.OldShapeEditor
{
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  public class CustomShape : ElementShape
  {
    private List<ShapePoint> points = new List<ShapePoint>();
    private Rectangle dimension;

    public CustomShape()
    {
      this.points = new List<ShapePoint>();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<ShapePoint> Points
    {
      get
      {
        return this.points;
      }
    }

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

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      for (int index = 0; index < this.points.Count; ++index)
      {
        ShapePoint point1 = this.points[index];
        ShapePoint shapePoint = index < this.points.Count - 1 ? this.points[index + 1] : this.points[0];
        Point point2 = point1.GetPoint(this.dimension, bounds);
        Point point3 = shapePoint.GetPoint(this.dimension, bounds);
        if (point1.Bezier)
          graphicsPath.AddBezier(point2, point1.ControlPoint1.GetPoint(this.dimension, bounds), point1.ControlPoint2.GetPoint(this.dimension, bounds), point3);
        else
          graphicsPath.AddLine(point2, point3);
      }
      graphicsPath.CloseAllFigures();
      return graphicsPath;
    }

    public override string SerializeProperties()
    {
      string str = string.Format("{0},{1},{2},{3}:", (object) this.dimension.X, (object) this.dimension.Y, (object) this.dimension.Width, (object) this.dimension.Height);
      foreach (ShapePoint point in this.points)
        str += string.Format("{0},{1},{2},{3},{4},{5},{6},{7}:", (object) (int) point.X, (object) (int) point.Y, (object) point.Bezier, (object) (int) point.ControlPoint1.X, (object) (int) point.ControlPoint1.Y, (object) (int) point.ControlPoint2.X, (object) (int) point.ControlPoint2.Y, (object) (int) point.Anchor);
      return str;
    }

    public override void DeserializeProperties(string propertiesString)
    {
      string[] strArray1 = propertiesString.Split(':');
      string[] strArray2 = strArray1[0].Split(',');
      this.dimension = new Rectangle(int.Parse(strArray2[0]), int.Parse(strArray2[1]), int.Parse(strArray2[2]), int.Parse(strArray2[3]));
      for (int index = 1; index < strArray1.Length; ++index)
      {
        string[] strArray3 = strArray1[index].Split(',');
        if (strArray3.Length > 2)
        {
          ShapePoint shapePoint = new ShapePoint(int.Parse(strArray3[0]), int.Parse(strArray3[1]));
          shapePoint.Bezier = bool.Parse(strArray3[2]);
          shapePoint.ControlPoint1.X = (float) int.Parse(strArray3[3]);
          shapePoint.ControlPoint1.Y = (float) int.Parse(strArray3[4]);
          shapePoint.ControlPoint2.X = (float) int.Parse(strArray3[5]);
          shapePoint.ControlPoint2.Y = (float) int.Parse(strArray3[6]);
          shapePoint.Anchor = (AnchorStyles) int.Parse(strArray3[7]);
          this.points.Add(shapePoint);
        }
      }
    }
  }
}
