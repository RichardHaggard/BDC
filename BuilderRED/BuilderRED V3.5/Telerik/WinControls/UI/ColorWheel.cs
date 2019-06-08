// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColorWheel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class ColorWheel : UserControl
  {
    private Point markerPoint = Point.Empty;
    private Color colorRGB = Color.Empty;
    private const int COLOR_COUNT = 1536;
    private const double DEGREES_PER_RADIAN = 57.2957795130823;
    private bool mouseMoving;
    private HslColor colorHSL;
    private IContainer components;

    public ColorWheel()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public Color ColorRGB
    {
      get
      {
        return this.colorRGB;
      }
      set
      {
        this.colorRGB = value;
        this.colorHSL = HslColor.FromColor(value);
        this.Refresh();
        this.UpdateMarker();
      }
    }

    public HslColor ColorHSL
    {
      get
      {
        return this.colorHSL;
      }
      set
      {
        this.colorHSL = value;
        this.colorRGB = value.RgbValue;
        this.Refresh();
        this.UpdateMarker();
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      int num = Math.Min(this.Width, this.Height) / 2;
      Point centerPoint = new Point(this.Width / 2, this.Height / 2);
      Point[] points = new Point[1536];
      Color[] colorArray = new Color[1536];
      for (int index = 0; index <= 1535; ++index)
        points[index] = this.GetPoint((double) index * 360.0 / 1536.0, (double) num, centerPoint);
      for (int index = 0; index <= 1535; ++index)
        colorArray[index] = this.HSVtoRGB((int) ((double) index * (double) byte.MaxValue / 1536.0), (int) byte.MaxValue, (int) byte.MaxValue);
      using (PathGradientBrush pathGradientBrush = new PathGradientBrush(points))
      {
        pathGradientBrush.CenterColor = Color.White;
        pathGradientBrush.CenterPoint = new PointF((float) num, (float) num);
        pathGradientBrush.SurroundColors = colorArray;
        e.Graphics.FillEllipse((Brush) pathGradientBrush, 0, 0, this.Width, this.Height);
      }
      e.Graphics.DrawEllipse(Pens.Black, this.markerPoint.X - 4, this.markerPoint.Y - 4, 8, 8);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.mouseMoving = true;
      Region region;
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(new Rectangle(0, 0, this.Width, this.Height));
        region = new Region(path);
      }
      if (!region.IsVisible((float) e.X, (float) e.Y))
        return;
      this.markerPoint = new Point(e.X, e.Y);
      this.UpdateColor();
      this.Refresh();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.mouseMoving)
        return;
      Region region;
      using (GraphicsPath path = new GraphicsPath())
      {
        path.AddEllipse(new Rectangle(0, 0, this.Width, this.Height));
        region = new Region(path);
      }
      if (!region.IsVisible((float) e.X, (float) e.Y))
        return;
      this.markerPoint = new Point(e.X, e.Y);
      this.UpdateColor();
      this.Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.mouseMoving = false;
    }

    private Point GetPoint(double degrees, double radius, Point centerPoint)
    {
      double num = degrees / (180.0 / Math.PI);
      return new Point((int) ((double) centerPoint.X + Math.Floor(radius * Math.Cos(num))), (int) ((double) centerPoint.Y - Math.Floor(radius * Math.Sin(num))));
    }

    private Color HSVtoRGB(int hue, int saturation, int value)
    {
      double num1 = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      double num4 = (double) hue / (double) byte.MaxValue * 360.0 % 360.0;
      double num5 = (double) saturation / (double) byte.MaxValue;
      double num6 = (double) value / (double) byte.MaxValue;
      if (num5 == 0.0)
      {
        num1 = num6;
        num2 = num6;
        num3 = num6;
      }
      else
      {
        double d = num4 / 60.0;
        int num7 = (int) Math.Floor(d);
        double num8 = d - (double) num7;
        double num9 = num6 * (1.0 - num5);
        double num10 = num6 * (1.0 - num5 * num8);
        double num11 = num6 * (1.0 - num5 * (1.0 - num8));
        switch (num7)
        {
          case 0:
            num1 = num6;
            num2 = num11;
            num3 = num9;
            break;
          case 1:
            num1 = num10;
            num2 = num6;
            num3 = num9;
            break;
          case 2:
            num1 = num9;
            num2 = num6;
            num3 = num11;
            break;
          case 3:
            num1 = num9;
            num2 = num10;
            num3 = num6;
            break;
          case 4:
            num1 = num11;
            num2 = num9;
            num3 = num6;
            break;
          case 5:
            num1 = num6;
            num2 = num9;
            num3 = num10;
            break;
        }
      }
      return Color.FromArgb((int) (num1 * (double) byte.MaxValue), (int) (num2 * (double) byte.MaxValue), (int) (num3 * (double) byte.MaxValue));
    }

    private void UpdateColor()
    {
      int num = Math.Min(this.Width, this.Height) / 2;
      Point point = new Point(this.Width / 2, this.Height / 2);
      Point pt = new Point(this.markerPoint.X - point.X, this.markerPoint.Y - point.Y);
      this.colorRGB = this.HSVtoRGB(this.CalcDegrees(pt) * (int) byte.MaxValue / 360, (int) (Math.Sqrt((double) (pt.X * pt.X + pt.Y * pt.Y)) / (double) num * (double) byte.MaxValue), (int) byte.MaxValue);
      this.colorHSL = HslColor.FromColor(this.colorRGB);
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorRGB));
    }

    private int CalcDegrees(Point pt)
    {
      int num1;
      if (pt.X == 0)
      {
        num1 = pt.Y <= 0 ? 90 : 270;
      }
      else
      {
        int num2 = (int) (-Math.Atan((double) pt.Y / (double) pt.X) * (180.0 / Math.PI));
        if (pt.X < 0)
          num2 += 180;
        num1 = (num2 + 360) % 360;
      }
      return num1;
    }

    private void UpdateMarker()
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.AutoScaleMode = AutoScaleMode.None;
    }
  }
}
