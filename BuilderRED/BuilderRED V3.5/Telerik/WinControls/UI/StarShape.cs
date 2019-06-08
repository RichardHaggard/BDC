// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StarShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class StarShape : ElementShape
  {
    private int arms;
    private float innerRadiusRatio;

    public StarShape()
    {
      this.arms = 5;
      this.innerRadiusRatio = 0.375f;
    }

    public StarShape(int arms, float innerRadiusRatio)
    {
      this.arms = arms;
      this.innerRadiusRatio = innerRadiusRatio;
    }

    public int Arms
    {
      get
      {
        return this.arms;
      }
      set
      {
        this.arms = value;
      }
    }

    public float InnerRadiusRatio
    {
      get
      {
        return this.innerRadiusRatio;
      }
      set
      {
        this.innerRadiusRatio = value;
      }
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      double num1 = Math.PI / (double) this.Arms;
      double num2 = Math.PI / 2.0;
      float num3 = (float) Math.Min(bounds.Width, bounds.Height);
      RectangleF rectangleF = new RectangleF((float) bounds.X + (float) (((double) bounds.Width - (double) num3) / 2.0), (float) bounds.Y + (float) (((double) bounds.Height - (double) num3) / 2.0), num3, num3);
      PointF pointF = new PointF((float) bounds.X + (float) bounds.Width / 2f, (float) bounds.Y + (float) bounds.Height / 2f);
      PointF[] points = new PointF[this.Arms * 2];
      float num4 = (float) (bounds.Right - bounds.X) / (rectangleF.Right - rectangleF.X);
      float num5 = (float) (bounds.Bottom - bounds.Y) / (rectangleF.Bottom - rectangleF.Y);
      for (int index = 0; index < 2 * this.Arms; ++index)
      {
        float num6 = (index & 1) == 0 ? num3 / 2f : num3 / 2f * this.InnerRadiusRatio;
        float num7 = pointF.X + (float) Math.Cos((double) index * num1 - num2) * num6;
        float num8 = pointF.Y + (float) Math.Sin((double) index * num1 - num2) * num6;
        float x = (float) bounds.X + (num7 - rectangleF.X) * num4;
        float y = (float) bounds.Y + (num8 - rectangleF.Y) * num5;
        points[index] = new PointF(x, y);
      }
      graphicsPath.AddPolygon(points);
      return graphicsPath;
    }
  }
}
