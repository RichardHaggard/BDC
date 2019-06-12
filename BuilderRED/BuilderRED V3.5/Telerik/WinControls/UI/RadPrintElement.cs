// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPrintElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadPrintElement
  {
    private Color foreColor = Color.Black;
    private Color backColor = Color.LightGray;
    private Color borderColor = Color.Black;
    private bool drawBorder = true;
    private bool drawText = true;
    private readonly SizeF defaultScaleTransform = new SizeF(1f, 1f);
    private SizeF scaleTransform = new SizeF(1f, 1f);
    private Padding textPadding = Padding.Empty;
    private ContentAlignment textAlignment;
    private string text;
    private Font font;
    private StringTrimming stringTrimming;
    private StringFormatFlags stringFormatFlags;
    private bool drawFill;
    private bool rightToLeft;
    private float angleTransform;
    private Image image;
    private ImageLayout imageLayout;
    private ContentAlignment imageAlignment;
    private PointF[] polygon;
    private bool enableHtmlTextRendering;

    public RadPrintElement()
    {
      this.foreColor = Color.Black;
      this.backColor = Color.White;
      this.borderColor = Color.Black;
      this.textAlignment = ContentAlignment.MiddleCenter;
      this.text = string.Empty;
      this.font = SystemFonts.DefaultFont;
      this.stringTrimming = StringTrimming.EllipsisCharacter;
    }

    public RadPrintElement(string text)
      : this()
    {
      this.Text = text;
    }

    public Padding TextPadding
    {
      get
      {
        return this.textPadding;
      }
      set
      {
        this.textPadding = value;
      }
    }

    public bool DrawText
    {
      get
      {
        return this.drawText;
      }
      set
      {
        this.drawText = value;
      }
    }

    public float AngleTransform
    {
      get
      {
        return this.angleTransform;
      }
      set
      {
        this.angleTransform = value;
      }
    }

    public SizeF ScaleTransform
    {
      get
      {
        return this.scaleTransform;
      }
      set
      {
        this.scaleTransform = value;
      }
    }

    public bool DrawFill
    {
      get
      {
        return this.drawFill;
      }
      set
      {
        this.drawFill = value;
      }
    }

    public bool DrawBorder
    {
      get
      {
        return this.drawBorder;
      }
      set
      {
        this.drawBorder = value;
      }
    }

    public bool RightToLeft
    {
      get
      {
        return this.rightToLeft;
      }
      set
      {
        this.rightToLeft = value;
      }
    }

    public Color ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        this.foreColor = value;
      }
    }

    public Color BackColor
    {
      get
      {
        return this.backColor;
      }
      set
      {
        this.backColor = value;
      }
    }

    public Color BorderColor
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

    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
      }
    }

    public virtual string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }

    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
      }
    }

    public StringTrimming StringTrimming
    {
      get
      {
        return this.stringTrimming;
      }
      set
      {
        this.stringTrimming = value;
      }
    }

    public StringFormatFlags StringFormatFlags
    {
      get
      {
        return this.stringFormatFlags;
      }
      set
      {
        this.stringFormatFlags = value;
      }
    }

    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
      }
    }

    public ImageLayout ImageLayout
    {
      get
      {
        return this.imageLayout;
      }
      set
      {
        this.imageLayout = value;
      }
    }

    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlignment;
      }
      set
      {
        this.imageAlignment = value;
      }
    }

    public PointF[] Polygon
    {
      get
      {
        return this.polygon;
      }
      set
      {
        this.polygon = value;
      }
    }

    public bool EnableHtmlTextRendering
    {
      get
      {
        return this.enableHtmlTextRendering;
      }
      set
      {
        this.enableHtmlTextRendering = value;
      }
    }

    public void Paint(Graphics g, Rectangle rect)
    {
      Matrix matrix = this.PerformTransofmations((RectangleF) rect);
      g.Transform = matrix;
      this.PaintElement(g, rect);
      if (matrix == null)
        return;
      g.ResetTransform();
    }

    public void Paint(Graphics g, RectangleF rect)
    {
      Matrix matrix = this.PerformTransofmations(rect);
      g.Transform = matrix;
      this.PaintElement(g, rect);
      if (matrix == null)
        return;
      g.ResetTransform();
    }

    protected virtual void PaintElement(Graphics g, Rectangle rect)
    {
      this.PaintFill(g, rect);
      this.PaintImage(g, rect);
      this.PaintBorder(g, rect);
      this.PaintText(g, rect);
    }

    protected virtual void PaintElement(Graphics g, RectangleF rect)
    {
      this.PaintFill(g, rect);
      this.PaintImage(g, rect);
      this.PaintBorder(g, rect);
      this.PaintText(g, rect);
    }

    protected virtual void PaintFill(Graphics g, Rectangle rect)
    {
      if (!this.DrawFill)
        return;
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
      {
        if (this.Polygon != null && this.Polygon.Length >= 3)
          g.FillPolygon((Brush) solidBrush, this.Polygon);
        else
          g.FillRectangle((Brush) solidBrush, rect);
      }
    }

    protected virtual void PaintFill(Graphics g, RectangleF rect)
    {
      if (!this.DrawFill)
        return;
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
      {
        if (this.Polygon != null && this.Polygon.Length >= 3)
          g.FillPolygon((Brush) solidBrush, this.Polygon);
        else
          g.FillRectangle((Brush) solidBrush, rect);
      }
    }

    protected virtual void PaintImage(Graphics g, Rectangle rect)
    {
      if (this.image == null || rect.Size == Size.Empty)
        return;
      ContentAlignment imageAlignment = this.ImageAlignment;
      switch (this.ImageLayout)
      {
        case ImageLayout.None:
          Graphics graphics = g;
          RectangleF rectangleF = LayoutUtils.Align(new SizeF((float) Math.Min(rect.Width, this.image.Size.Width), (float) Math.Min(rect.Height, this.image.Size.Height)), (RectangleF) rect, imageAlignment);
          graphics.DrawImageUnscaledAndClipped(this.image, new Rectangle(new Point((int) rectangleF.Location.X, (int) rectangleF.Location.Y), rectangleF.Size.ToSize()));
          break;
        case ImageLayout.Tile:
          for (float x = (float) rect.X; (double) x <= (double) (rect.Height - this.image.Height); x += (float) this.image.Height)
          {
            for (float y = (float) rect.Y; (double) y < (double) (rect.Width - this.image.Width); y += (float) this.image.Width)
              g.DrawImage(this.image, (int) y, (int) x);
          }
          break;
        case ImageLayout.Center:
          PointF pointF = new PointF(Math.Max(0.0f, (float) (rect.X + Math.Max(0, (rect.Width - this.image.Width) / 2))), Math.Max(0.0f, (float) (rect.Y + Math.Max(0, (rect.Height - this.image.Height) / 2))));
          SizeF sizeF = new SizeF((float) Math.Min(rect.Width, this.image.Size.Width), (float) Math.Min(rect.Height, this.image.Size.Height));
          g.DrawImageUnscaledAndClipped(this.image, new Rectangle(new Point((int) pointF.X, (int) pointF.Y), sizeF.ToSize()));
          break;
        case ImageLayout.Stretch:
          g.DrawImage(this.image, rect.X, rect.Y, rect.Width, rect.Height);
          break;
        case ImageLayout.Zoom:
          if (this.image.Width == 0 || this.image.Height == 0)
            break;
          float num = Math.Min((float) rect.Width / (float) this.image.Width, (float) rect.Height / (float) this.image.Height);
          if ((double) num <= 0.0)
            break;
          int width = (int) Math.Round((double) this.image.Width * (double) num);
          int height = (int) Math.Round((double) this.image.Height * (double) num);
          int x1 = rect.X + (rect.Width - width) / 2;
          int y1 = rect.Y + (rect.Height - height) / 2;
          g.DrawImage(this.image, x1, y1, width, height);
          break;
      }
    }

    protected virtual void PaintImage(Graphics g, RectangleF rect)
    {
      if (this.image == null || rect.Size == (SizeF) Size.Empty)
        return;
      ContentAlignment imageAlignment = this.ImageAlignment;
      switch (this.ImageLayout)
      {
        case ImageLayout.None:
          Graphics graphics = g;
          RectangleF rectangleF = LayoutUtils.Align(new SizeF(Math.Min(rect.Width, (float) this.image.Size.Width), Math.Min(rect.Height, (float) this.image.Size.Height)), rect, imageAlignment);
          graphics.DrawImageUnscaledAndClipped(this.image, new Rectangle(new Point((int) rectangleF.Location.X, (int) rectangleF.Location.Y), rectangleF.Size.ToSize()));
          break;
        case ImageLayout.Tile:
          for (float x = rect.X; (double) x <= (double) rect.Height - (double) this.image.Height; x += (float) this.image.Height)
          {
            for (float y = rect.Y; (double) y < (double) rect.Width - (double) this.image.Width; y += (float) this.image.Width)
              g.DrawImage(this.image, (int) y, (int) x);
          }
          break;
        case ImageLayout.Center:
          PointF pointF = new PointF(Math.Max(0.0f, rect.X + Math.Max(0.0f, (float) (((double) rect.Width - (double) this.image.Width) / 2.0))), Math.Max(0.0f, rect.Y + Math.Max(0.0f, (float) (((double) rect.Height - (double) this.image.Height) / 2.0))));
          SizeF sizeF = new SizeF(Math.Min(rect.Width, (float) this.image.Size.Width), Math.Min(rect.Height, (float) this.image.Size.Height));
          g.DrawImageUnscaledAndClipped(this.image, new Rectangle(new Point((int) pointF.X, (int) pointF.Y), sizeF.ToSize()));
          break;
        case ImageLayout.Stretch:
          g.DrawImage(this.image, rect.X, rect.Y, rect.Width, rect.Height);
          break;
        case ImageLayout.Zoom:
          if (this.image.Width == 0 || this.image.Height == 0)
            break;
          float num = Math.Min(rect.Width / (float) this.image.Width, rect.Height / (float) this.image.Height);
          if ((double) num <= 0.0)
            break;
          int width = (int) Math.Round((double) this.image.Width * (double) num);
          int height = (int) Math.Round((double) this.image.Height * (double) num);
          int x1 = (int) rect.X + ((int) rect.Width - width) / 2;
          int y1 = (int) rect.Y + ((int) rect.Height - height) / 2;
          g.DrawImage(this.image, x1, y1, width, height);
          break;
      }
    }

    protected virtual void PaintBorder(Graphics g, Rectangle rect)
    {
      if (!this.DrawBorder)
        return;
      using (Pen pen = new Pen(this.BorderColor))
      {
        if (this.Polygon != null && this.Polygon.Length >= 3)
          g.DrawPolygon(pen, this.Polygon);
        else
          g.DrawRectangle(pen, rect);
      }
    }

    protected virtual void PaintBorder(Graphics g, RectangleF rect)
    {
      if (!this.DrawBorder)
        return;
      using (Pen pen = new Pen(this.BorderColor))
      {
        if (this.Polygon != null && this.Polygon.Length >= 3)
          g.DrawPolygon(pen, this.Polygon);
        else
          g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
      }
    }

    protected virtual void PaintText(Graphics g, Rectangle rect)
    {
      this.PaintText(g, new RectangleF((float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height));
    }

    protected virtual void PaintText(Graphics g, RectangleF rect)
    {
      Padding textPadding = this.TextPadding;
      if (string.IsNullOrEmpty(this.Text) || !this.DrawText)
        return;
      if (this.EnableHtmlTextRendering && this.Text.Contains("<html>"))
      {
        TextPrimitiveHtmlImpl primitiveHtmlImpl = new TextPrimitiveHtmlImpl();
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(g);
        TextParams textParams = new TextParams();
        textParams.alignment = this.TextAlignment;
        textParams.ClipText = true;
        textParams.font = this.Font;
        textParams.foreColor = this.ForeColor;
        textParams.paintingRectangle = rect;
        textParams.rightToLeft = this.RightToLeft;
        textParams.text = this.Text;
        textParams.textWrap = true;
        if (textPadding != Padding.Empty)
        {
          textParams.paintingRectangle.X += (float) textPadding.Left;
          textParams.paintingRectangle.Y += (float) textPadding.Top;
          textParams.paintingRectangle.Width -= (float) textPadding.Horizontal;
          textParams.paintingRectangle.Height -= (float) textPadding.Vertical;
        }
        primitiveHtmlImpl.PaintPrimitive((IGraphics) radGdiGraphics, textParams);
      }
      else
      {
        using (StringFormat format = new StringFormat())
        {
          using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
          {
            format.Alignment = this.GetHorizontalAlignment(this.TextAlignment);
            format.LineAlignment = this.GetVerticalAlignment(this.TextAlignment);
            format.Trimming = this.StringTrimming;
            format.FormatFlags = this.StringFormatFlags;
            if (textPadding != Padding.Empty)
            {
              rect.X += (float) textPadding.Left;
              rect.Y += (float) textPadding.Top;
              rect.Width -= (float) textPadding.Horizontal;
              rect.Height -= (float) textPadding.Vertical;
            }
            string s = this.Text;
            if (s.Length > RadGdiGraphics.GdiStringLengthLimit)
              s = s.Substring(0, RadGdiGraphics.GdiStringLengthLimit);
            try
            {
              g.DrawString(s, this.Font, (Brush) solidBrush, rect, format);
            }
            catch
            {
              try
              {
                using (Font font = new Font(this.Font, FontStyle.Regular))
                  g.DrawString(s, font, (Brush) solidBrush, rect, format);
              }
              catch
              {
                g.DrawString(s, Control.DefaultFont, (Brush) solidBrush, rect, format);
              }
            }
          }
        }
      }
    }

    public virtual StringAlignment GetVerticalAlignment(ContentAlignment alignment)
    {
      switch (alignment)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.TopCenter:
        case ContentAlignment.TopRight:
          return StringAlignment.Near;
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.MiddleRight:
          return StringAlignment.Center;
        case ContentAlignment.BottomLeft:
        case ContentAlignment.BottomCenter:
        case ContentAlignment.BottomRight:
          return StringAlignment.Far;
        default:
          return StringAlignment.Center;
      }
    }

    public virtual StringAlignment GetHorizontalAlignment(ContentAlignment alignment)
    {
      switch (alignment)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          return StringAlignment.Near;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          return StringAlignment.Center;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          return StringAlignment.Far;
        default:
          return StringAlignment.Center;
      }
    }

    private Matrix PerformTransofmations(RectangleF bounds)
    {
      RadMatrix identity = RadMatrix.Identity;
      if (this.ScaleTransform != this.defaultScaleTransform)
      {
        identity.Translate(-bounds.X, -bounds.Y, MatrixOrder.Append);
        identity.Scale(this.ScaleTransform.Width, this.ScaleTransform.Height, MatrixOrder.Append);
        identity.Translate(bounds.X, bounds.Y, MatrixOrder.Append);
      }
      if ((double) this.AngleTransform != 0.0)
      {
        SizeF sz = new SizeF(bounds.Width / 2f, bounds.Height / 2f);
        identity.RotateAt(this.AngleTransform, PointF.Add(bounds.Location, sz), MatrixOrder.Append);
        RectangleF boundingRect = TelerikHelper.GetBoundingRect(bounds, identity);
        identity.Translate(bounds.X - boundingRect.X, bounds.Y - boundingRect.Y, MatrixOrder.Append);
      }
      return identity.ToGdiMatrix();
    }

    public RectangleF GetTransformedBounds(RectangleF bounds)
    {
      RadMatrix identity = RadMatrix.Identity;
      if (this.ScaleTransform != this.defaultScaleTransform)
      {
        identity.Translate(-bounds.X, -bounds.Y, MatrixOrder.Append);
        identity.Scale(this.ScaleTransform.Width, this.ScaleTransform.Height, MatrixOrder.Append);
        identity.Translate(bounds.X, bounds.Y, MatrixOrder.Append);
      }
      if ((double) this.AngleTransform != 0.0)
      {
        SizeF sz = new SizeF(bounds.Width / 2f, bounds.Height / 2f);
        identity.RotateAt(this.AngleTransform, PointF.Add(bounds.Location, sz), MatrixOrder.Append);
        RectangleF boundingRect = TelerikHelper.GetBoundingRect(bounds, identity);
        identity.Translate(bounds.X - boundingRect.X, bounds.Y - boundingRect.Y, MatrixOrder.Append);
      }
      return identity.TransformRectangle(bounds);
    }
  }
}
