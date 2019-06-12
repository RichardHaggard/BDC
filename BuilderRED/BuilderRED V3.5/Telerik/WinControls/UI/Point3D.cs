// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Point3D
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.UI.Carousel;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (Point3DConverter))]
  public class Point3D
  {
    public static Point3D Empty = new Point3D(0.0, 0.0, 0.0);
    private double x;
    private double y;
    private double z;

    public Point3D()
    {
    }

    public Point3D(double x, double y, double z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public Point3D(double x, double y)
    {
      this.x = x;
      this.y = y;
    }

    public Point3D(PointF pt)
      : this((double) pt.X, (double) pt.Y)
    {
    }

    public Point3D(Point pt)
      : this((double) pt.X, (double) pt.Y)
    {
    }

    public Point3D(Point3D pt)
      : this(pt.X, pt.Y, pt.Z)
    {
    }

    [NotifyParentProperty(true)]
    [DefaultValue(0.0)]
    public double X
    {
      get
      {
        return this.x;
      }
      set
      {
        this.x = value;
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(0.0)]
    public double Y
    {
      get
      {
        return this.y;
      }
      set
      {
        this.y = value;
      }
    }

    [DefaultValue(0.0)]
    [NotifyParentProperty(true)]
    public double Z
    {
      get
      {
        return this.z;
      }
      set
      {
        this.z = value;
      }
    }

    public void Negate()
    {
      this.x = -this.x;
      this.y = -this.y;
      this.z = -this.z;
    }

    public double Length()
    {
      return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
    }

    public void Normalize()
    {
      double num = this.Length();
      this.x /= num;
      this.y /= num;
      this.z /= num;
    }

    public void Add(double x, double y, double z)
    {
      this.x += x;
      this.y += y;
      this.z += z;
    }

    public void Add(Point3D pt)
    {
      this.Add(pt.x, pt.y, pt.z);
    }

    public void Subtract(double x, double y, double z)
    {
      this.Add(-x, -y, -z);
    }

    public void Subtract(Point3D pt)
    {
      this.Add(-pt.x, -pt.y, -pt.z);
    }

    public void Multiply(double coef)
    {
      this.x *= coef;
      this.y *= coef;
      this.z *= coef;
    }

    public void Divide(double coef)
    {
      this.x /= coef;
      this.y /= coef;
      this.z /= coef;
    }

    public static Point3D CrossProduct(Point3D vect1, Point3D vect2)
    {
      return new Point3D(vect1.y * vect2.z - vect1.z * vect2.y, vect1.z * vect2.x - vect1.x * vect2.z, vect1.x * vect2.y - vect1.y * vect2.x);
    }

    public static double DotProduct(Point3D vect1, Point3D vect2)
    {
      return vect1.x * vect2.x + vect1.y * vect2.y + vect1.z * vect2.z;
    }

    public override bool Equals(object obj)
    {
      if (base.Equals(obj))
        return true;
      Point3D point3D = obj as Point3D;
      if (point3D != null && object.Equals((object) this.x, (object) point3D.x) && object.Equals((object) this.y, (object) point3D.y))
        return object.Equals((object) this.z, (object) point3D.z);
      return false;
    }

    public override int GetHashCode()
    {
      return (int) this.x ^ (int) this.y ^ (int) this.z;
    }

    public static Point3D operator +(Point3D pt1, Point3D pt2)
    {
      return new Point3D(pt1.x + pt2.x, pt1.y + pt2.y, pt1.z + pt2.z);
    }

    public static Point3D operator -(Point3D pt1, Point3D pt2)
    {
      return new Point3D(pt1.x - pt2.x, pt1.y - pt2.y, pt1.z - pt2.z);
    }

    public static Point3D operator *(Point3D pt, float coef)
    {
      return new Point3D(pt.x * (double) coef, pt.y * (double) coef, pt.z * (double) coef);
    }

    public static Point3D operator /(Point3D pt, float coef)
    {
      return new Point3D(pt.x / (double) coef, pt.y / (double) coef, pt.z / (double) coef);
    }

    public static Point3D operator -(Point3D pt)
    {
      return new Point3D(-pt.x, -pt.y, -pt.z);
    }

    public static explicit operator PointF(Point3D pt)
    {
      return pt.ToPointF();
    }

    public static explicit operator Point(Point3D pt)
    {
      return pt.ToPoint();
    }

    public PointF ToPointF()
    {
      return new PointF((float) this.x, (float) this.y);
    }

    public Point ToPoint()
    {
      return new Point((int) Math.Round(this.x), (int) Math.Round(this.y));
    }
  }
}
