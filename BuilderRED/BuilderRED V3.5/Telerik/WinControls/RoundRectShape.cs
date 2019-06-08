// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RoundRectShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RoundRectShape : ElementShape
  {
    private int radius = 5;
    private bool bottomLeftRounded = true;
    private bool topLeftRounded = true;
    private bool bottomRightRounded = true;
    private bool topRightRounded = true;

    public RoundRectShape()
    {
    }

    public RoundRectShape(IContainer components)
      : base(components)
    {
    }

    public RoundRectShape(int radius)
    {
      this.radius = radius;
    }

    public RoundRectShape(
      int radius,
      bool topLeftRounded,
      bool bottomLeftRounded,
      bool topRightRounded,
      bool bottomRightRounded)
      : this(radius)
    {
      this.bottomLeftRounded = bottomLeftRounded;
      this.topLeftRounded = topLeftRounded;
      this.bottomRightRounded = bottomRightRounded;
      this.topRightRounded = topRightRounded;
    }

    [Category("Appearance")]
    [DefaultValue(5)]
    [Description("Gets or sets the radius of the shape.")]
    public int Radius
    {
      get
      {
        return this.radius;
      }
      set
      {
        this.radius = value;
      }
    }

    [DefaultValue(true)]
    public bool BottomLeftRounded
    {
      get
      {
        return this.bottomLeftRounded;
      }
      set
      {
        this.bottomLeftRounded = value;
      }
    }

    [DefaultValue(true)]
    public bool TopLeftRounded
    {
      get
      {
        return this.topLeftRounded;
      }
      set
      {
        this.topLeftRounded = value;
      }
    }

    [DefaultValue(true)]
    public bool BottomRightRounded
    {
      get
      {
        return this.bottomRightRounded;
      }
      set
      {
        this.bottomRightRounded = value;
      }
    }

    [DefaultValue(true)]
    public bool TopRightRounded
    {
      get
      {
        return this.topRightRounded;
      }
      set
      {
        this.topRightRounded = value;
      }
    }

    public override Region CreateRegion(Rectangle bounds)
    {
      return NativeMethods.CreateRoundRectRgn(bounds, this.radius);
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      return this.CreatePath(new RectangleF((float) bounds.X, (float) bounds.Y, (float) bounds.Width, (float) bounds.Height));
    }

    public override GraphicsPath CreatePath(RectangleF bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      if ((double) bounds.Height <= 0.0 || (double) bounds.Width <= 0.0)
        return graphicsPath;
      if ((double) this.Radius <= 0.0)
      {
        graphicsPath.AddRectangle(bounds);
        graphicsPath.CloseFigure();
        return graphicsPath;
      }
      int num1 = this.Radius;
      bool flag = false;
      if ((double) num1 >= (double) Math.Min(bounds.Width, bounds.Height) / 2.0)
      {
        num1 = Math.Max(1, (int) ((double) Math.Min(bounds.Width, bounds.Height) / 2.0) - 1);
        flag = true;
      }
      if (flag && this.TopLeftRounded && (this.TopRightRounded && this.BottomLeftRounded) && this.BottomRightRounded)
      {
        if ((double) bounds.Width > (double) bounds.Height)
        {
          float height = bounds.Height;
          SizeF size = new SizeF(height, height);
          RectangleF rect = new RectangleF(bounds.Location, size);
          if (rect.Size != SizeF.Empty)
          {
            graphicsPath.AddArc(rect, 90f, 180f);
            rect.X = bounds.Right - height;
            graphicsPath.AddArc(rect, 270f, 180f);
          }
          else
            graphicsPath.AddEllipse(bounds);
        }
        else if ((double) bounds.Width < (double) bounds.Height)
        {
          float width = bounds.Width;
          SizeF size = new SizeF(width, width);
          RectangleF rect = new RectangleF(bounds.Location, size);
          if (rect.Size != SizeF.Empty)
          {
            graphicsPath.AddArc(rect, 180f, 180f);
            rect.Y = bounds.Bottom - width;
            graphicsPath.AddArc(rect, 0.0f, 180f);
          }
          else
            graphicsPath.AddEllipse(bounds);
        }
        else
          graphicsPath.AddEllipse(bounds);
        graphicsPath.CloseFigure();
        return graphicsPath;
      }
      float num2 = (float) num1 * 2f;
      SizeF size1 = new SizeF(num2, num2);
      RectangleF rect1 = new RectangleF(bounds.Location, size1);
      float num3 = 1f;
      SizeF size2 = new SizeF(num3, num3);
      RectangleF rect2 = new RectangleF(bounds.Location, size2);
      if (this.TopLeftRounded)
        graphicsPath.AddArc(rect1, 180f, 90f);
      else
        graphicsPath.AddArc(rect2, 180f, 90f);
      rect1.X = bounds.Right - num2;
      rect2.X = bounds.Right - num3;
      if (this.TopRightRounded)
        graphicsPath.AddArc(rect1, 270f, 90f);
      else
        graphicsPath.AddArc(rect2, 270f, 90f);
      rect1.Y = bounds.Bottom - num2;
      rect2.Y = bounds.Bottom - num3;
      if (this.BottomRightRounded)
        graphicsPath.AddArc(rect1, 0.0f, 90f);
      else
        graphicsPath.AddArc(rect2, 0.0f, 90f);
      rect1.X = bounds.Left;
      rect2.X = bounds.Left;
      if (this.BottomLeftRounded)
        graphicsPath.AddArc(rect1, 90f, 90f);
      else
        graphicsPath.AddArc(rect2, 90f, 90f);
      graphicsPath.CloseFigure();
      this.MirrorPath(graphicsPath, bounds);
      return (GraphicsPath) graphicsPath.Clone();
    }

    protected override bool ShouldMirrorPath()
    {
      if (!base.ShouldMirrorPath())
        return false;
      bool flag = true;
      if (this.TopLeftRounded && this.TopRightRounded && (this.BottomLeftRounded && this.BottomLeftRounded))
        flag = false;
      else if (this.TopLeftRounded && this.TopRightRounded)
        flag = false;
      else if (this.BottomLeftRounded && this.BottomRightRounded)
        flag = false;
      return flag;
    }

    public override string SerializeProperties()
    {
      string str = this.radius.ToString();
      if (!this.BottomLeftRounded || !this.TopLeftRounded || (!this.BottomRightRounded || !this.TopRightRounded))
        str = str + ", " + this.BottomLeftRounded.ToString() + ", " + this.TopLeftRounded.ToString() + ", " + this.BottomRightRounded.ToString() + ", " + this.TopRightRounded.ToString();
      return str;
    }

    public override void DeserializeProperties(string propertiesString)
    {
      if (string.IsNullOrEmpty(propertiesString))
        return;
      string[] strArray = propertiesString.Split(',');
      if (strArray.Length > 0)
        this.radius = int.Parse(strArray[0]);
      if (strArray.Length > 1)
        this.BottomLeftRounded = bool.Parse(strArray[1]);
      if (strArray.Length > 2)
        this.TopLeftRounded = bool.Parse(strArray[2]);
      if (strArray.Length > 3)
        this.BottomRightRounded = bool.Parse(strArray[3]);
      if (strArray.Length <= 4)
        return;
      this.TopRightRounded = bool.Parse(strArray[4]);
    }
  }
}
