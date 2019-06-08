// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ProfessionalColors2DBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class ProfessionalColors2DBox : UserControl
  {
    private Point markerPoint = Point.Empty;
    private Color colorRGB = Color.Empty;
    private bool mouseMoving;
    private ColorModes colorMode;
    private HslColor colorHSL;
    private IContainer components;

    public ProfessionalColors2DBox()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    public ColorModes ColorMode
    {
      get
      {
        return this.colorMode;
      }
      set
      {
        this.colorMode = value;
        this.ResetMarker();
        this.Refresh();
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
        this.colorRGB = this.colorHSL.RgbValue;
        this.ResetMarker();
        this.Refresh();
      }
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
        this.colorHSL = HslColor.FromColor(this.colorRGB);
        this.ResetMarker();
        this.Refresh();
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      HslColor hslColor1 = HslColor.FromAhsl((int) byte.MaxValue);
      HslColor hslColor2 = HslColor.FromAhsl((int) byte.MaxValue);
      switch (this.ColorMode)
      {
        case ColorModes.Hue:
          hslColor1.H = this.ColorHSL.H;
          hslColor2.H = this.ColorHSL.H;
          hslColor1.S = 0.0;
          hslColor2.S = 1.0;
          break;
        case ColorModes.Saturation:
          hslColor1.S = this.ColorHSL.S;
          hslColor2.S = this.ColorHSL.S;
          hslColor1.L = 1.0;
          hslColor2.L = 0.0;
          break;
        case ColorModes.Luminance:
          hslColor1.L = this.ColorHSL.L;
          hslColor2.L = this.ColorHSL.L;
          hslColor1.S = 1.0;
          hslColor2.S = 0.0;
          break;
      }
      for (int index = 0; index < this.Height - 4; ++index)
      {
        int num = ColorProvider.Round((double) byte.MaxValue - (double) byte.MaxValue * (double) index / (double) (this.Height - 4));
        Color color1 = Color.Empty;
        Color color2 = Color.Empty;
        switch (this.ColorMode)
        {
          case ColorModes.Red:
            color1 = Color.FromArgb((int) this.ColorRGB.R, num, 0);
            color2 = Color.FromArgb((int) this.ColorRGB.R, num, (int) byte.MaxValue);
            break;
          case ColorModes.Green:
            color1 = Color.FromArgb(num, (int) this.ColorRGB.G, 0);
            color2 = Color.FromArgb(num, (int) this.ColorRGB.G, (int) byte.MaxValue);
            break;
          case ColorModes.Blue:
            color1 = Color.FromArgb(0, num, (int) this.ColorRGB.B);
            color2 = Color.FromArgb((int) byte.MaxValue, num, (int) this.ColorRGB.B);
            break;
          case ColorModes.Hue:
            hslColor2.L = hslColor1.L = 1.0 - (double) index / (double) (this.Height - 4);
            color1 = hslColor1.RgbValue;
            color2 = hslColor2.RgbValue;
            break;
          case ColorModes.Saturation:
          case ColorModes.Luminance:
            hslColor2.H = hslColor1.H = (double) index / (double) (this.Width - 4);
            color1 = hslColor1.RgbValue;
            color2 = hslColor2.RgbValue;
            break;
        }
        Rectangle rect1 = new Rectangle(2, 2, this.Width - 4, 1);
        Rectangle rect2 = new Rectangle(2, index + 2, this.Width - 4, 1);
        if (this.ColorMode == ColorModes.Saturation || this.ColorMode == ColorModes.Luminance)
        {
          rect1 = new Rectangle(2, 2, 1, this.Height - 4);
          rect2 = new Rectangle(index + 2, 2, 1, this.Height - 4);
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, color1, color2, 90f, false))
            e.Graphics.FillRectangle((Brush) linearGradientBrush, rect2);
        }
        else
        {
          using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect1, color1, color2, 0.0f, false))
            e.Graphics.FillRectangle((Brush) linearGradientBrush, rect2);
        }
      }
      Pen pen = Pens.White;
      if (this.colorHSL.L >= 40.0 / 51.0)
      {
        if (this.colorHSL.H < 13.0 / 180.0 || this.colorHSL.H > 5.0 / 9.0)
        {
          if (this.colorHSL.S <= 14.0 / 51.0)
            pen = Pens.Black;
        }
        else
          pen = Pens.Black;
      }
      e.Graphics.DrawEllipse(pen, this.markerPoint.X - 5, this.markerPoint.Y - 5, 10, 10);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.mouseMoving = true;
      this.SetMarker(e.X, e.Y);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.mouseMoving)
        return;
      this.SetMarker(e.X, e.Y);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.mouseMoving = false;
    }

    private void SetMarker(int x, int y)
    {
      if (x < 0)
        x = 0;
      if (x > this.Width - 4)
        x = this.Width - 4;
      if (y < 0)
        y = 0;
      if (y > this.Height - 4)
        y = this.Height - 4;
      if (this.markerPoint.X == x && this.markerPoint.Y == y)
        return;
      this.markerPoint = new Point(x, y);
      this.colorHSL = this.GetColor(x, y);
      this.colorRGB = this.colorHSL.RgbValue;
      this.Refresh();
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorRGB));
    }

    private void ResetMarker()
    {
      switch (this.colorMode)
      {
        case ColorModes.Red:
          this.markerPoint.X = ColorProvider.Round((double) (this.Width - 4) * (double) this.colorRGB.B / (double) byte.MaxValue);
          this.markerPoint.Y = ColorProvider.Round((double) (this.Height - 4) * (1.0 - (double) this.colorRGB.G / (double) byte.MaxValue));
          break;
        case ColorModes.Green:
          this.markerPoint.X = ColorProvider.Round((double) (this.Width - 4) * (double) this.colorRGB.B / (double) byte.MaxValue);
          this.markerPoint.Y = ColorProvider.Round((double) (this.Height - 4) * (1.0 - (double) this.colorRGB.R / (double) byte.MaxValue));
          break;
        case ColorModes.Blue:
          this.markerPoint.X = ColorProvider.Round((double) (this.Width - 4) * (double) this.colorRGB.R / (double) byte.MaxValue);
          this.markerPoint.Y = ColorProvider.Round((double) (this.Height - 4) * (1.0 - (double) this.colorRGB.G / (double) byte.MaxValue));
          break;
        case ColorModes.Hue:
          this.markerPoint.X = ColorProvider.Round((double) (this.Width - 4) * this.colorHSL.S);
          this.markerPoint.Y = ColorProvider.Round((double) (this.Height - 4) * (1.0 - this.colorHSL.L));
          break;
        case ColorModes.Saturation:
          this.markerPoint.X = ColorProvider.Round((double) (this.Width - 4) * this.colorHSL.H);
          this.markerPoint.Y = ColorProvider.Round((double) (this.Height - 4) * (1.0 - this.colorHSL.L));
          break;
        case ColorModes.Luminance:
          this.markerPoint.X = ColorProvider.Round((double) (this.Width - 4) * this.colorHSL.H);
          this.markerPoint.Y = ColorProvider.Round((double) (this.Height - 4) * (1.0 - this.colorHSL.S));
          break;
      }
    }

    private HslColor GetColor(int x, int y)
    {
      HslColor hslColor = HslColor.FromAhsl((int) byte.MaxValue);
      switch (this.ColorMode)
      {
        case ColorModes.Red:
          hslColor = HslColor.FromColor(Color.FromArgb((int) this.colorRGB.R, ColorProvider.Round((double) byte.MaxValue * (1.0 - (double) y / (double) (this.Height - 4))), ColorProvider.Round((double) byte.MaxValue * (double) x / (double) (this.Width - 4))));
          break;
        case ColorModes.Green:
          hslColor = HslColor.FromColor(Color.FromArgb(ColorProvider.Round((double) byte.MaxValue * (1.0 - (double) y / (double) (this.Height - 4))), (int) this.colorRGB.G, ColorProvider.Round((double) byte.MaxValue * (double) x / (double) (this.Width - 4))));
          break;
        case ColorModes.Blue:
          hslColor = HslColor.FromColor(Color.FromArgb(ColorProvider.Round((double) byte.MaxValue * (double) x / (double) (this.Width - 4)), ColorProvider.Round((double) byte.MaxValue * (1.0 - (double) y / (double) (this.Height - 4))), (int) this.colorRGB.B));
          break;
        case ColorModes.Hue:
          hslColor.H = this.colorHSL.H;
          hslColor.S = (double) x / (double) (this.Width - 4);
          hslColor.L = 1.0 - (double) y / (double) (this.Height - 4);
          break;
        case ColorModes.Saturation:
          hslColor.S = this.colorHSL.S;
          hslColor.H = (double) x / (double) (this.Width - 4);
          hslColor.L = 1.0 - (double) y / (double) (this.Height - 4);
          break;
        case ColorModes.Luminance:
          hslColor.L = this.colorHSL.L;
          hslColor.H = (double) x / (double) (this.Width - 4);
          hslColor.S = 1.0 - (double) y / (double) (this.Height - 4);
          break;
      }
      return hslColor;
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
    }
  }
}
