// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Shapes.PathElementShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;

namespace Telerik.WinControls.Shapes
{
  public class PathElementShape : CustomShape
  {
    private GraphicsPath path;
    private VisualElement owner;
    private RectangleF bounds;
    private GraphicsPath scaledPath;

    public PathElementShape()
    {
    }

    public PathElementShape(GraphicsPath path, VisualElement owner)
      : this()
    {
      this.path = path;
      this.owner = owner;
      this.bounds = (RectangleF) owner.Bounds;
    }

    public RectangleF Bounds
    {
      get
      {
        return this.bounds;
      }
      set
      {
        this.bounds = value;
      }
    }

    public VisualElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    public GraphicsPath Path
    {
      get
      {
        return this.path;
      }
      set
      {
        this.path = value;
      }
    }

    public GraphicsPath ScaledPath
    {
      get
      {
        return this.scaledPath;
      }
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      if (this.path == null)
        return new GraphicsPath();
      Matrix matrix = new Matrix();
      matrix.Scale((float) bounds.Width / this.bounds.Width, (float) bounds.Height / this.bounds.Height);
      this.scaledPath = (GraphicsPath) this.path.Clone();
      this.scaledPath.Transform(matrix);
      matrix.Dispose();
      this.MirrorPath(this.scaledPath, (RectangleF) bounds);
      return this.scaledPath;
    }

    public override string SerializeProperties()
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (this.path.PathData.Points.Length == 0)
        return string.Empty;
      foreach (byte pathType in this.path.PathTypes)
      {
        stringBuilder1.Append(pathType.ToString());
        stringBuilder1.Append(",");
      }
      string str = stringBuilder1.ToString().TrimEnd(',');
      StringBuilder stringBuilder2 = new StringBuilder("@");
      foreach (PointF pathPoint in this.path.PathPoints)
      {
        stringBuilder2.Append(pathPoint.X.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + pathPoint.Y.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        stringBuilder2.Append(",");
      }
      return str + stringBuilder2.ToString().TrimEnd(',');
    }

    public override void DeserializeProperties(string propertiesString)
    {
      string[] strArray1 = propertiesString.Split('@');
      string[] strArray2 = strArray1[0].Split(',');
      byte[] types = new byte[strArray2.Length];
      for (int index = 0; index < strArray2.Length; ++index)
      {
        byte num = byte.Parse(strArray2[index]);
        types[index] = num;
      }
      string[] strArray3 = strArray1[1].Split(',');
      PointF[] pts = new PointF[strArray3.Length / 2];
      int index1 = 0;
      int num1 = 0;
      for (; index1 < strArray3.Length; index1 += 2)
        pts[num1++] = new PointF()
        {
          X = float.Parse(strArray3[index1], (IFormatProvider) CultureInfo.InvariantCulture),
          Y = float.Parse(strArray3[index1 + 1], (IFormatProvider) CultureInfo.InvariantCulture)
        };
      this.path = new GraphicsPath(pts, types);
    }
  }
}
