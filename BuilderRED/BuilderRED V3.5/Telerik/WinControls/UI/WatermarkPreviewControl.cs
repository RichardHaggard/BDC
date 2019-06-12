// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WatermarkPreviewControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class WatermarkPreviewControl : RadControl
  {
    private RadPrintWatermark watermark;
    private PaperSize paperSize;

    public RadPrintWatermark Watermark
    {
      get
      {
        return this.watermark;
      }
      set
      {
        this.watermark = value;
        this.Refresh();
      }
    }

    public PaperSize PaperSize
    {
      get
      {
        return this.paperSize;
      }
      set
      {
        this.paperSize = value;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      this.DrawPageBackground(e.Graphics);
      if (this.watermark != null && this.paperSize != null)
      {
        if (this.watermark.DrawImage)
          this.DrawWatermarkImage(e.Graphics);
        if (this.watermark.DrawText)
          this.DrawWatermarkText(e.Graphics);
      }
      this.DrawPageBorder(e.Graphics);
      this.DrawPageShadow(e.Graphics);
    }

    private void DrawPageBackground(Graphics graphics)
    {
      graphics.FillRectangle(Brushes.White, 0, 0, this.Width - 3, this.Height - 3);
    }

    private void DrawPageBorder(Graphics graphics)
    {
      graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 3, this.Height - 3);
    }

    private void DrawWatermarkImage(Graphics graphics)
    {
      Image image = Image.FromFile(this.watermark.ImagePath);
      Rectangle rectangle = new Rectangle(new Point((int) ((Decimal) this.watermark.ImageHOffset / (Decimal) this.paperSize.Width * (Decimal) this.Width) + 1, (int) ((Decimal) this.watermark.ImageVOffset / (Decimal) this.paperSize.Height * (Decimal) this.Height) + 1), new Size(this.Width - 5, this.Height - 5));
      ImageAttributes imageAttr = new ImageAttributes();
      imageAttr.SetColorMatrix(new ColorMatrix()
      {
        Matrix33 = (float) this.watermark.ImageOpacity / (float) byte.MaxValue
      }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      if (this.watermark.ImageTiling)
      {
        TextureBrush textureBrush = new TextureBrush(image, new Rectangle(Point.Empty, image.Size), imageAttr);
        textureBrush.WrapMode = WrapMode.Tile;
        graphics.FillRectangle((Brush) textureBrush, rectangle);
        textureBrush.Dispose();
      }
      else
      {
        rectangle.Size = new Size(image.Size.Width / 5, image.Size.Height / 5);
        graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
      }
    }

    private void DrawWatermarkText(Graphics graphics)
    {
      Brush brush = (Brush) new SolidBrush(Color.FromArgb((int) (float) ((double) ((float) this.watermark.TextOpacity / (float) byte.MaxValue) * (double) ((float) this.watermark.ForeColor.A / (float) byte.MaxValue) * (double) byte.MaxValue), (int) this.watermark.ForeColor.R, (int) this.watermark.ForeColor.G, (int) this.watermark.ForeColor.B));
      Point location = new Point((int) ((Decimal) this.watermark.TextHOffset / (Decimal) this.paperSize.Width * (Decimal) this.Width) + 1, (int) ((Decimal) this.watermark.TextVOffset / (Decimal) this.paperSize.Height * (Decimal) this.Height) + 1);
      Font font = new Font(this.watermark.Font.FontFamily, this.watermark.Font.Size / 5f, this.watermark.Font.Style);
      this.DrawTextWithAngle(graphics, location, this.watermark.TextAngle, this.watermark.Text, font, brush);
    }

    private void DrawPageShadow(Graphics graphics)
    {
      graphics.FillRectangle(Brushes.Gray, 3, this.Height - 2, this.Width - 3, 2);
      graphics.FillRectangle(Brushes.Gray, this.Width - 2, 3, 2, this.Height - 3);
    }

    private void DrawTextWithAngle(
      Graphics g,
      Point location,
      float angle,
      string text,
      Font font,
      Brush brush)
    {
      g.TextRenderingHint = TextRenderingHint.AntiAlias;
      g.TranslateTransform((float) -location.X, (float) -location.Y, MatrixOrder.Append);
      g.RotateTransform(angle, MatrixOrder.Append);
      g.TranslateTransform((float) location.X, (float) location.Y, MatrixOrder.Append);
      g.DrawString(text, font, brush, (PointF) location);
      g.ResetTransform();
      g.TextRenderingHint = TextRenderingHint.SystemDefault;
    }
  }
}
