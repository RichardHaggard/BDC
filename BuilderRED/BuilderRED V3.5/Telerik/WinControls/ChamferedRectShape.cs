// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ChamferedRectShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  [DebuggerDisplay("{Width}, {Angle}, {TopLeftChamfered}, {TopRightChamfered}, {BottomRightChamfered}, {BottomLeftChamfered}")]
  public class ChamferedRectShape : ElementShape
  {
    private int width;
    private int angle;
    private bool topLeftChamfered;
    private bool topRightChamfered;
    private bool bottomRightChamfered;
    private bool bottomLeftChamfered;

    public ChamferedRectShape()
      : this(5, 45, true, true, true, true)
    {
    }

    public ChamferedRectShape(int width)
      : this(width, 45, true, true, true, true)
    {
    }

    public ChamferedRectShape(int width, int angle)
      : this(width, angle, true, true, true, true)
    {
    }

    public ChamferedRectShape(
      int width,
      int angle,
      bool topLeftChamfered,
      bool topRightChamfered,
      bool bottomRightChamfered,
      bool bottomLeftChamfered)
    {
      this.width = width;
      this.angle = angle;
      this.topLeftChamfered = topLeftChamfered;
      this.bottomLeftChamfered = bottomLeftChamfered;
      this.bottomRightChamfered = bottomRightChamfered;
      this.topRightChamfered = topRightChamfered;
    }

    [DefaultValue(5)]
    public int Width
    {
      get
      {
        return this.width;
      }
      set
      {
        this.width = value;
      }
    }

    [DefaultValue(45)]
    public int Angle
    {
      get
      {
        return this.angle;
      }
      set
      {
        if (this.angle == value || value < 0 || value >= 90)
          return;
        this.angle = value;
      }
    }

    [DefaultValue(true)]
    public bool TopLeftChamfered
    {
      get
      {
        return this.topLeftChamfered;
      }
      set
      {
        this.topLeftChamfered = value;
      }
    }

    [DefaultValue(true)]
    public bool TopRightChamfered
    {
      get
      {
        return this.topRightChamfered;
      }
      set
      {
        this.topRightChamfered = value;
      }
    }

    [DefaultValue(true)]
    public bool BottomRightChamfered
    {
      get
      {
        return this.bottomRightChamfered;
      }
      set
      {
        this.bottomRightChamfered = value;
      }
    }

    [DefaultValue(true)]
    public bool BottomLeftChamfered
    {
      get
      {
        return this.bottomLeftChamfered;
      }
      set
      {
        this.bottomLeftChamfered = value;
      }
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath1 = new GraphicsPath();
      if (bounds.Height <= 0 || bounds.Width <= 0)
        return graphicsPath1;
      if (this.width <= 0 || !this.topLeftChamfered && !this.bottomLeftChamfered && (!this.bottomRightChamfered && !this.topRightChamfered))
      {
        graphicsPath1.AddRectangle(bounds);
        graphicsPath1.CloseFigure();
        return graphicsPath1;
      }
      int num1 = Math.Min(bounds.Width / 2, this.width);
      int val2 = (int) (Math.Tan(Math.PI / 180.0 * (double) this.Angle) * (double) this.width);
      int num2 = Math.Min(bounds.Height / 2, val2);
      List<Point> pointList = new List<Point>();
      if (this.TopLeftChamfered)
      {
        pointList.Add(new Point(bounds.X + num1, bounds.Y));
        pointList.Add(new Point(bounds.X, bounds.Y + num2));
      }
      else
        pointList.Add(new Point(bounds.X, bounds.Y));
      if (this.BottomLeftChamfered)
      {
        pointList.Add(new Point(bounds.X, bounds.Bottom - num2));
        pointList.Add(new Point(bounds.X + num1, bounds.Bottom));
      }
      else
        pointList.Add(new Point(bounds.X, bounds.Bottom));
      if (this.bottomRightChamfered)
      {
        pointList.Add(new Point(bounds.Right - num1, bounds.Bottom));
        pointList.Add(new Point(bounds.Right, bounds.Bottom - num2));
      }
      else
        pointList.Add(new Point(bounds.Right, bounds.Bottom));
      if (this.TopRightChamfered)
      {
        pointList.Add(new Point(bounds.Right, bounds.Y + num2));
        pointList.Add(new Point(bounds.Right - num1, bounds.Y));
      }
      else
        pointList.Add(new Point(bounds.Right, bounds.Y));
      if (this.TopLeftChamfered)
        pointList.Add(new Point(bounds.X + num1, bounds.Y));
      else
        pointList.Add(new Point(bounds.X, bounds.Y));
      byte[] types = new byte[pointList.Count];
      types[0] = (byte) 0;
      for (int index = 1; index < types.Length; ++index)
        types[index] = (byte) 1;
      GraphicsPath graphicsPath2 = new GraphicsPath(pointList.ToArray(), types);
      graphicsPath2.CloseFigure();
      this.MirrorPath(graphicsPath2, (RectangleF) bounds);
      return graphicsPath2;
    }

    public override string SerializeProperties()
    {
      StringBuilder stringBuilder = new StringBuilder(this.Width.ToString() + ", " + this.Angle.ToString());
      if (!this.TopLeftChamfered || !this.BottomLeftChamfered || (!this.BottomRightChamfered || !this.TopRightChamfered))
      {
        stringBuilder.Append(", " + this.TopLeftChamfered.ToString());
        stringBuilder.Append(", " + this.TopRightChamfered.ToString());
        stringBuilder.Append(", " + this.BottomRightChamfered.ToString());
        stringBuilder.Append(", " + this.BottomLeftChamfered.ToString());
      }
      return stringBuilder.ToString();
    }

    public override void DeserializeProperties(string propertiesString)
    {
      if (string.IsNullOrEmpty(propertiesString))
        return;
      string[] strArray = propertiesString.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length > 1)
      {
        this.Width = int.Parse(strArray[0]);
        this.Angle = int.Parse(strArray[1]);
      }
      if (strArray.Length <= 5)
        return;
      this.TopLeftChamfered = bool.Parse(strArray[2]);
      this.TopRightChamfered = bool.Parse(strArray[3]);
      this.BottomRightChamfered = bool.Parse(strArray[4]);
      this.BottomLeftChamfered = bool.Parse(strArray[5]);
    }
  }
}
