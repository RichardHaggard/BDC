// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CustomShapeConvertor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class CustomShapeConvertor
  {
    private List<OldShapePoint> points = new List<OldShapePoint>();
    private Rectangle dimension;

    public CustomShapeConvertor()
    {
      this.points = new List<OldShapePoint>();
    }

    public List<OldShapePoint> Points
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

    public string SerializeProperties()
    {
      string str = string.Format("{0},{1},{2},{3}:", (object) this.dimension.X, (object) this.dimension.Y, (object) this.dimension.Width, (object) this.dimension.Height);
      foreach (OldShapePoint point in this.points)
        str += string.Format("{0},{1},{2},{3},{4},{5},{6},{7}:", (object) (int) point.X, (object) (int) point.Y, (object) point.Bezier, (object) (int) point.ControlPoint1.X, (object) (int) point.ControlPoint1.Y, (object) (int) point.ControlPoint2.X, (object) (int) point.ControlPoint2.Y, (object) (int) point.Anchor);
      return str;
    }

    public CustomShape GetShape()
    {
      CustomShape customShape = new CustomShape();
      customShape.DeserializeProperties(this.SerializeProperties());
      return customShape;
    }
  }
}
