// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPrintPreviewControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadPrintPreviewControl : PrintPreviewControl
  {
    private Color borderColor = Color.White;
    private Color innerBorderColor = Color.White;
    private Color pageShadowColor = Color.Black;
    private int shadowThickness = 6;

    public int ShadowThickness
    {
      get
      {
        return this.shadowThickness;
      }
      set
      {
        this.shadowThickness = value;
      }
    }

    public Color PageShadowColor
    {
      get
      {
        return this.pageShadowColor;
      }
      set
      {
        this.pageShadowColor = value;
      }
    }

    public static int PixelsToPhysical(int pixels, int dpi)
    {
      return (int) ((double) pixels * 100.0 / (double) dpi);
    }

    public static int PhysicalToPixels(int physicalSize, int dpi)
    {
      return (int) ((double) (physicalSize * dpi) / 100.0);
    }

    public static Point PixelsToPhysical(Point pixels, Point dpi)
    {
      return new Point(RadPrintPreviewControl.PixelsToPhysical(pixels.X, dpi.X), RadPrintPreviewControl.PixelsToPhysical(pixels.Y, dpi.Y));
    }

    public static Point PhysicalToPixels(Point physical, Point dpi)
    {
      return new Point(RadPrintPreviewControl.PhysicalToPixels(physical.X, dpi.X), RadPrintPreviewControl.PhysicalToPixels(physical.Y, dpi.Y));
    }

    public Color PageBorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
      }
    }

    public Color PageInnerBorderColor
    {
      get
      {
        return this.innerBorderColor;
      }
      set
      {
        this.innerBorderColor = value;
      }
    }

    public Point ScrollOffset
    {
      get
      {
        return (Point) typeof (PrintPreviewControl).GetProperty("Position", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this, (object[]) null);
      }
      set
      {
        typeof (PrintPreviewControl).GetProperty("Position", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) this, (object) value, (object[]) null);
      }
    }

    private Size VirtualSize
    {
      get
      {
        return (Size) typeof (PrintPreviewControl).GetField("virtualSize", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this);
      }
    }

    private Point screendpi
    {
      get
      {
        return (Point) typeof (PrintPreviewControl).GetField(nameof (screendpi), BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this);
      }
    }

    public RadPrintPreviewControl()
    {
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 156, 179, 207);
      this.PageShadowColor = Color.FromArgb((int) byte.MaxValue, 109, 125, 144);
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
      base.OnPaint(pevent);
      PreviewPageInfo[] previewPageInfoArray = (PreviewPageInfo[]) typeof (PrintPreviewControl).GetField("pageInfo", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) this);
      if (previewPageInfoArray == null || previewPageInfoArray.Length == 0)
        return;
      Point empty = Point.Empty;
      int val1 = 0;
      Size size1 = this.Size;
      Size size2 = this.Size;
      Point point1 = new Point(this.VirtualSize);
      Point point2 = new Point(Math.Max(0, (size1.Width - point1.X) / 2), Math.Max(0, (size2.Height - point1.Y) / 2));
      point2.X -= this.ScrollOffset.X;
      point2.Y -= this.ScrollOffset.Y;
      int pixels1 = RadPrintPreviewControl.PhysicalToPixels(10, this.screendpi.Y);
      int pixels2 = RadPrintPreviewControl.PhysicalToPixels(10, this.screendpi.X);
      Rectangle[] rectangleArray = new Rectangle[this.Rows * this.Columns];
      for (int index1 = 0; index1 < this.Rows; ++index1)
      {
        empty.X = 0;
        empty.Y = val1 * index1;
        for (int index2 = 0; index2 < this.Columns; ++index2)
        {
          int index3 = this.StartPage + index2 + index1 * this.Columns;
          if (index3 < previewPageInfoArray.Length)
          {
            Size physicalSize = previewPageInfoArray[index3].PhysicalSize;
            Point pixels3 = RadPrintPreviewControl.PhysicalToPixels(new Point(new Size((int) (this.Zoom * (double) physicalSize.Width), (int) (this.Zoom * (double) physicalSize.Height))), this.screendpi);
            int x = point2.X + pixels2 * (index2 + 1) + empty.X;
            int y = point2.Y + pixels1 * (index1 + 1) + empty.Y;
            empty.X += pixels3.X;
            val1 = Math.Max(val1, pixels3.Y);
            rectangleArray[index3 - this.StartPage] = new Rectangle(x, y, pixels3.X, pixels3.Y);
          }
        }
      }
      for (int index = 0; index < rectangleArray.Length; ++index)
      {
        if (index + this.StartPage < previewPageInfoArray.Length)
        {
          Rectangle rect = rectangleArray[index];
          Pen pen1 = new Pen(this.PageBorderColor);
          pevent.Graphics.DrawRectangle(pen1, rect);
          pen1.Dispose();
          Brush brush = (Brush) new SolidBrush(this.PageShadowColor);
          int num = Math.Min(1 + (int) Math.Ceiling((double) this.ShadowThickness * this.Zoom), 10);
          pevent.Graphics.FillRectangle(brush, new Rectangle(rect.Left + num, rect.Bottom + 1, rect.Width + 1, num));
          pevent.Graphics.FillRectangle(brush, new Rectangle(rect.Right + 1, rect.Top + num, num, rect.Height));
          rect.Inflate(-1, -1);
          --rect.Width;
          --rect.Height;
          Pen pen2 = new Pen(this.PageInnerBorderColor);
          pevent.Graphics.DrawRectangle(pen2, rect);
        }
      }
    }
  }
}
