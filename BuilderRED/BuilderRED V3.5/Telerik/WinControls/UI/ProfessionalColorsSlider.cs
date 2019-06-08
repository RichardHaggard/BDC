// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ProfessionalColorsSlider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class ProfessionalColorsSlider : UserControl
  {
    private HslColor colorHSL = HslColor.FromAhsl((int) byte.MaxValue);
    private Color colorRGB = Color.Empty;
    private bool mouseMoving;
    private bool setHueSilently;
    private ColorModes colorMode;
    private int position;
    private IContainer components;

    public ProfessionalColorsSlider()
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
        this.ResetSlider();
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
        this.ResetSlider();
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
        if (!this.setHueSilently)
          this.colorHSL = HslColor.FromColor(this.colorRGB);
        this.ResetSlider();
        this.Refresh();
      }
    }

    public int Position
    {
      get
      {
        return this.position;
      }
      set
      {
        int num = value;
        if (num < 0)
          num = 0;
        if (num > this.Height - 9)
          num = this.Height - 9;
        if (num == this.position)
          return;
        this.position = num;
        this.ResetHSLRGB();
        this.Refresh();
        if (this.ColorChanged == null)
          return;
        this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorRGB));
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      HslColor hslColor = HslColor.FromAhsl((int) byte.MaxValue);
      switch (this.ColorMode)
      {
        case ColorModes.Hue:
          hslColor.L = hslColor.S = 1.0;
          break;
        case ColorModes.Saturation:
          hslColor.H = this.ColorHSL.H;
          hslColor.L = this.ColorHSL.L;
          break;
        case ColorModes.Luminance:
          hslColor.H = this.ColorHSL.H;
          hslColor.S = this.ColorHSL.S;
          break;
      }
      for (int index = 0; index < this.Height - 8; ++index)
      {
        double num = this.ColorMode >= ColorModes.Hue ? 1.0 - (double) index / (double) (this.Height - 8) : (double) byte.MaxValue - (double) ColorProvider.Round((double) byte.MaxValue * (double) index / ((double) this.Height - 8.0));
        Color color = Color.Empty;
        switch (this.ColorMode)
        {
          case ColorModes.Red:
            color = Color.FromArgb((int) num, (int) this.ColorRGB.G, (int) this.ColorRGB.B);
            break;
          case ColorModes.Green:
            color = Color.FromArgb((int) this.ColorRGB.R, (int) num, (int) this.ColorRGB.B);
            break;
          case ColorModes.Blue:
            color = Color.FromArgb((int) this.ColorRGB.R, (int) this.ColorRGB.G, (int) num);
            break;
          case ColorModes.Hue:
            hslColor.H = num;
            color = hslColor.RgbValue;
            break;
          case ColorModes.Saturation:
            hslColor.S = num;
            color = hslColor.RgbValue;
            break;
          case ColorModes.Luminance:
            hslColor.L = num;
            color = hslColor.RgbValue;
            break;
        }
        using (Pen pen = new Pen(color))
          e.Graphics.DrawLine(pen, 11, index + 4, this.Width - 11, index + 4);
      }
      this.DrawSlider(e.Graphics);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.mouseMoving = true;
      this.Position = e.Y - 4;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.mouseMoving)
        return;
      this.Position = e.Y - 4;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.mouseMoving = false;
    }

    private void DrawSlider(Graphics g)
    {
      using (Pen pen = new Pen(Color.FromArgb(116, 114, 106)))
      {
        Brush white = Brushes.White;
        Point[] points = new Point[7]{ new Point(1, this.position), new Point(3, this.position), new Point(7, this.position + 4), new Point(3, this.position + 8), new Point(1, this.position + 8), new Point(0, this.position + 7), new Point(0, this.position + 1) };
        g.FillPolygon(white, points);
        g.DrawPolygon(pen, points);
        points[0] = new Point(this.Width - 2, this.position);
        points[1] = new Point(this.Width - 4, this.position);
        points[2] = new Point(this.Width - 8, this.position + 4);
        points[3] = new Point(this.Width - 4, this.position + 8);
        points[4] = new Point(this.Width - 2, this.position + 8);
        points[5] = new Point(this.Width - 1, this.position + 7);
        points[6] = new Point(this.Width - 1, this.position + 1);
        g.FillPolygon(white, points);
        g.DrawPolygon(pen, points);
      }
    }

    private void ResetHSLRGB()
    {
      this.setHueSilently = true;
      switch (this.ColorMode)
      {
        case ColorModes.Red:
          this.ColorRGB = Color.FromArgb((int) byte.MaxValue - ColorProvider.Round((double) byte.MaxValue * (double) this.position / (double) (this.Height - 9)), (int) this.ColorRGB.G, (int) this.ColorRGB.B);
          this.ColorHSL = HslColor.FromColor(this.ColorRGB);
          break;
        case ColorModes.Green:
          this.ColorRGB = Color.FromArgb((int) this.ColorRGB.R, (int) byte.MaxValue - ColorProvider.Round((double) byte.MaxValue * (double) this.position / (double) (this.Height - 9)), (int) this.ColorRGB.B);
          this.ColorHSL = HslColor.FromColor(this.ColorRGB);
          break;
        case ColorModes.Blue:
          this.ColorRGB = Color.FromArgb((int) this.ColorRGB.R, (int) this.ColorRGB.G, (int) byte.MaxValue - ColorProvider.Round((double) byte.MaxValue * (double) this.position / (double) (this.Height - 9)));
          this.ColorHSL = HslColor.FromColor(this.ColorRGB);
          break;
        case ColorModes.Hue:
          this.colorHSL.H = 1.0 - (double) this.position / (double) (this.Height - 9);
          this.ColorRGB = this.ColorHSL.RgbValue;
          break;
        case ColorModes.Saturation:
          this.colorHSL.S = 1.0 - (double) this.position / (double) (this.Height - 9);
          this.ColorRGB = this.ColorHSL.RgbValue;
          break;
        case ColorModes.Luminance:
          this.colorHSL.L = 1.0 - (double) this.position / (double) (this.Height - 9);
          this.ColorRGB = this.ColorHSL.RgbValue;
          break;
      }
      this.setHueSilently = false;
    }

    private void ResetSlider()
    {
      double num = 0.0;
      switch (this.ColorMode)
      {
        case ColorModes.Red:
          num = (double) this.colorRGB.R / (double) byte.MaxValue;
          break;
        case ColorModes.Green:
          num = (double) this.colorRGB.G / (double) byte.MaxValue;
          break;
        case ColorModes.Blue:
          num = (double) this.colorRGB.B / (double) byte.MaxValue;
          break;
        case ColorModes.Hue:
          num = this.colorHSL.H;
          break;
        case ColorModes.Saturation:
          num = this.colorHSL.S;
          break;
        case ColorModes.Luminance:
          num = this.colorHSL.L;
          break;
      }
      this.position = this.Height - 8 - ColorProvider.Round((double) (this.Height - 8) * num);
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
