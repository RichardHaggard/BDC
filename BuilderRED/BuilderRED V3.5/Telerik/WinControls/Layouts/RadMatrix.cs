// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.RadMatrix
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.Layouts
{
  public struct RadMatrix
  {
    public static readonly RadMatrix Identity = new RadMatrix(1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
    public static readonly RadMatrix Empty = new RadMatrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
    public const float PI = 3.141593f;
    public const float TwoPI = 6.283186f;
    public const float RadianToDegree = 57.29577f;
    public const float DegreeToRadian = 0.01745329f;
    public float DX;
    public float DY;
    public float M11;
    public float M12;
    public float M21;
    public float M22;

    public RadMatrix(float m11, float m12, float m21, float m22, float dx, float dy)
    {
      this.M11 = m11;
      this.M12 = m12;
      this.M21 = m21;
      this.M22 = m22;
      this.DX = dx;
      this.DY = dy;
    }

    public RadMatrix(RadMatrix source)
    {
      this.M11 = source.M11;
      this.M12 = source.M12;
      this.M21 = source.M21;
      this.M22 = source.M22;
      this.DX = source.DX;
      this.DY = source.DY;
    }

    public RadMatrix(Matrix gdiMatrix)
    {
      float[] elements = gdiMatrix.Elements;
      this.M11 = elements[0];
      this.M12 = elements[1];
      this.M21 = elements[2];
      this.M22 = elements[3];
      this.DX = elements[4];
      this.DY = elements[5];
    }

    public RadMatrix(PointF offset)
    {
      this.M11 = 1f;
      this.M12 = 0.0f;
      this.M21 = 0.0f;
      this.M22 = 1f;
      this.DX = offset.X;
      this.DY = offset.Y;
    }

    public RadMatrix(float scaleX, float scaleY)
    {
      this = new RadMatrix(scaleX, scaleY, PointF.Empty);
    }

    public RadMatrix(float scaleX, float scaleY, PointF origin)
    {
      this.M11 = scaleX;
      this.M12 = 0.0f;
      this.M21 = 0.0f;
      this.M22 = scaleY;
      this.DX = origin.X - scaleX * origin.X;
      this.DY = origin.Y - scaleY * origin.Y;
    }

    public RadMatrix(float angle)
    {
      this = new RadMatrix(angle, PointF.Empty);
    }

    public RadMatrix(float angle, PointF origin)
    {
      if ((double) angle == 0.0 || (double) angle == 360.0)
      {
        this = RadMatrix.Identity;
      }
      else
      {
        float cos;
        float sin;
        RadMatrix.GetCosSin(angle, out cos, out sin);
        this.M11 = cos;
        this.M12 = sin;
        this.M21 = -sin;
        this.M22 = cos;
        if (origin != PointF.Empty)
        {
          float x = origin.X;
          float y = origin.Y;
          this.DX = (float) ((double) x - (double) cos * (double) x + (double) sin * (double) y);
          this.DY = (float) ((double) y - (double) cos * (double) y - (double) sin * (double) x);
        }
        else
        {
          this.DX = 0.0f;
          this.DY = 0.0f;
        }
      }
    }

    private static void GetCosSin(float angle, out float cos, out float sin)
    {
      if ((double) angle < 0.0)
        angle = 360f + angle;
      if ((double) angle == 90.0)
      {
        cos = 0.0f;
        sin = 1f;
      }
      else if ((double) angle == 180.0)
      {
        cos = -1f;
        sin = 0.0f;
      }
      else if ((double) angle == 270.0)
      {
        cos = 0.0f;
        sin = -1f;
      }
      else
      {
        float num = angle * ((float) Math.PI / 180f);
        cos = (float) Math.Cos((double) num);
        sin = (float) Math.Sin((double) num);
      }
    }

    public void Scale(float scaleX, float scaleY)
    {
      this.Scale(scaleX, scaleY, MatrixOrder.Prepend);
    }

    public void Scale(float scaleX, float scaleY, MatrixOrder order)
    {
      this.Multiply(new RadMatrix(scaleX, scaleY), order);
    }

    public void Rotate(float angle)
    {
      this.Rotate(angle, MatrixOrder.Prepend);
    }

    public void Rotate(float angle, MatrixOrder order)
    {
      this.Multiply(new RadMatrix(angle), order);
    }

    public void RotateAt(float angle, PointF origin)
    {
      this.RotateAt(angle, origin, MatrixOrder.Prepend);
    }

    public void RotateAt(float angle, PointF origin, MatrixOrder order)
    {
      if ((double) angle == 0.0)
        return;
      this.Multiply(new RadMatrix(angle, origin), order);
    }

    public void Translate(float dx, float dy)
    {
      this.Translate(dx, dy, MatrixOrder.Prepend);
    }

    public void Translate(float dx, float dy, MatrixOrder order)
    {
      this.Multiply(new RadMatrix(new PointF(dx, dy)), order);
    }

    public void Multiply(RadMatrix m)
    {
      this.Multiply(m, MatrixOrder.Prepend);
    }

    public void Multiply(RadMatrix m, MatrixOrder order)
    {
      if (order == MatrixOrder.Append)
        this *= m;
      else
        this = m * this;
    }

    public void Divide(RadMatrix m)
    {
      m.Invert();
      this.Multiply(m, MatrixOrder.Prepend);
    }

    public void Invert()
    {
      if (this.IsIdentity)
        return;
      float determinant = this.Determinant;
      if ((double) determinant == 0.0)
      {
        this = RadMatrix.Empty;
      }
      else
      {
        float m11 = this.M22 / determinant;
        float m12 = -this.M12 / determinant;
        float m21 = -this.M21 / determinant;
        float m22 = this.M11 / determinant;
        float dx = (float) ((double) this.DX * -(double) m11 - (double) this.DY * (double) m21);
        float dy = (float) ((double) this.DX * -(double) m12 - (double) this.DY * (double) m22);
        this = new RadMatrix(m11, m12, m21, m22, dx, dy);
      }
    }

    public void Reset()
    {
      this = RadMatrix.Identity;
    }

    public PointF TransformPoint(PointF point)
    {
      float x = point.X;
      float y = point.Y;
      return new PointF((float) ((double) x * (double) this.M11 + (double) y * (double) this.M21) + this.DX, (float) ((double) x * (double) this.M12 + (double) y * (double) this.M22) + this.DY);
    }

    public void TransformPoints(PointF[] points)
    {
      int length = points.Length;
      for (int index = 0; index < length; ++index)
        points[index] = this.TransformPoint(points[index]);
    }

    public RectangleF TransformRectangle(RectangleF bounds)
    {
      PointF location = bounds.Location;
      PointF point = new PointF(location.X + bounds.Width, location.Y + bounds.Height);
      PointF pointF1 = this.TransformPoint(location);
      PointF pointF2 = this.TransformPoint(point);
      return new RectangleF(pointF1.X, pointF1.Y, pointF2.X - pointF1.X, pointF2.Y - pointF1.Y);
    }

    public Matrix ToGdiMatrix()
    {
      return new Matrix(this.M11, this.M12, this.M21, this.M22, this.DX, this.DY);
    }

    public bool Equals(Matrix gdiMatrix)
    {
      return this.Equals(gdiMatrix.Elements);
    }

    public bool Equals(float[] elements)
    {
      if (elements.Length != 6)
        throw new ArgumentException("Invalid float array to compare to.");
      if ((double) this.M11 == (double) elements[0] && (double) this.M12 == (double) elements[1] && ((double) this.M21 == (double) elements[2] && (double) this.M22 == (double) elements[3]) && (double) this.DX == (double) elements[4])
        return (double) this.DY == (double) elements[5];
      return false;
    }

    public static float PointsDistance(PointF pt1, PointF pt2)
    {
      double num1 = (double) pt2.X - (double) pt1.X;
      double num2 = (double) pt2.Y - (double) pt1.Y;
      return (float) Math.Sqrt(num1 * num1 + num2 * num2);
    }

    public static RadMatrix operator *(RadMatrix a, RadMatrix b)
    {
      return new RadMatrix((float) ((double) a.M11 * (double) b.M11 + (double) a.M12 * (double) b.M21), (float) ((double) a.M11 * (double) b.M12 + (double) a.M12 * (double) b.M22), (float) ((double) a.M21 * (double) b.M11 + (double) a.M22 * (double) b.M21), (float) ((double) a.M21 * (double) b.M12 + (double) a.M22 * (double) b.M22), (float) ((double) a.DX * (double) b.M11 + (double) a.DY * (double) b.M21) + b.DX, (float) ((double) a.DX * (double) b.M12 + (double) a.DY * (double) b.M22) + b.DY);
    }

    public static bool operator ==(RadMatrix a, RadMatrix b)
    {
      if ((double) a.M11 == (double) b.M11 && (double) a.M12 == (double) b.M12 && ((double) a.M21 == (double) b.M21 && (double) a.M22 == (double) b.M22) && (double) a.DX == (double) b.DX)
        return (double) a.DY == (double) b.DY;
      return false;
    }

    public static bool operator !=(RadMatrix a, RadMatrix b)
    {
      return !(a == b);
    }

    public override int GetHashCode()
    {
      return this.M11.GetHashCode() ^ this.M12.GetHashCode() ^ this.M21.GetHashCode() ^ this.M22.GetHashCode() ^ this.DX.GetHashCode() ^ this.DY.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (!(obj is RadMatrix))
        return false;
      return (RadMatrix) obj == this;
    }

    public override string ToString()
    {
      return "RadMatrix: Offset [" + (object) this.DX + ", " + (object) this.DY + "]";
    }

    public bool IsEmpty
    {
      get
      {
        return this == RadMatrix.Empty;
      }
    }

    public bool IsIdentity
    {
      get
      {
        return this == RadMatrix.Identity;
      }
    }

    public float Determinant
    {
      get
      {
        return (float) ((double) this.M11 * (double) this.M22 - (double) this.M12 * (double) this.M21);
      }
    }

    public bool IsInvertible
    {
      get
      {
        return (double) this.Determinant != 0.0;
      }
    }

    public float ScaleX
    {
      get
      {
        return RadMatrix.PointsDistance(this.TransformPoint(PointF.Empty), this.TransformPoint(new PointF(1f, 0.0f)));
      }
    }

    public float ScaleY
    {
      get
      {
        return RadMatrix.PointsDistance(this.TransformPoint(PointF.Empty), this.TransformPoint(new PointF(0.0f, 1f)));
      }
    }

    public float Rotation
    {
      get
      {
        return (float) (Math.Atan2((double) this.M12, (double) this.M11) * 57.2957725524902);
      }
    }

    public float[] Elements
    {
      get
      {
        return new float[6]{ this.M11, this.M12, this.M21, this.M22, this.DX, this.DY };
      }
    }
  }
}
