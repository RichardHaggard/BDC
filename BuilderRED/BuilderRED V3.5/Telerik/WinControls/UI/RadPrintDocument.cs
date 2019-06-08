// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPrintDocument
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadPrintDocumentDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  public class RadPrintDocument : PrintDocument
  {
    private int pageCount = 3;
    private DateTime printStartDate = DateTime.MinValue;
    private string leftHeader = "";
    private string rightHeader = "";
    private string middleHeader = "";
    private string leftFooter = "";
    private string rightFooter = "";
    private string middleFooter = "";
    private Font headerFont = Control.DefaultFont;
    private Font footerFont = Control.DefaultFont;
    private int headerHeight = 30;
    private int footerHeight = 30;
    private int selectionLength = 1;
    public const string LogoString = "[Logo]";
    public const string PageNumberString = "[Page #]";
    public const string TotalPagesString = "[Total Pages]";
    public const string DatePrintedString = "[Date Printed]";
    public const string TimePrintedString = "[Time Printed]";
    public const string UserNamePrintedString = "[User Name]";
    private IPrintable associatedObject;
    private RadPrintWatermark watermark;
    private int printedPage;
    private bool isPrinting;
    private bool reverseHeaderOnEvenPages;
    private bool reverseFooterOnEvenPages;
    private Image logo;
    private bool landscape;
    private bool autoPortraitLandscape;
    private PaperSize paperSize;
    private PaperSource paperSource;
    private Margins margins;
    private int currentPage;
    private bool unassignObjectAfterPrint;

    public RadPrintDocument()
    {
      this.watermark = new RadPrintWatermark();
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentPage
    {
      get
      {
        return this.currentPage;
      }
      set
      {
        this.currentPage = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionLength
    {
      get
      {
        return this.selectionLength;
      }
      set
      {
        this.selectionLength = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(null)]
    public Image Logo
    {
      get
      {
        return this.logo;
      }
      set
      {
        this.logo = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool Landscape
    {
      get
      {
        if (this.Site != null)
          return this.landscape;
        return this.DefaultPageSettings.Landscape;
      }
      set
      {
        if (this.Site != null)
          this.landscape = value;
        else
          this.DefaultPageSettings.Landscape = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public bool AutoPortraitLandscape
    {
      get
      {
        return this.autoPortraitLandscape;
      }
      set
      {
        this.autoPortraitLandscape = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public PaperSize PaperSize
    {
      get
      {
        if (this.Site != null)
          return this.paperSize;
        return this.DefaultPageSettings.PaperSize;
      }
      set
      {
        if (this.Site != null)
        {
          if (value.ToString() != this.DefaultPageSettings.PaperSize.ToString())
            this.paperSize = value;
          else
            this.paperSize = (PaperSize) null;
        }
        else
        {
          if (value == null)
            return;
          this.DefaultPageSettings.PaperSize = value;
        }
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public PaperSource PaperSource
    {
      get
      {
        if (this.Site != null)
          return this.paperSource;
        return this.DefaultPageSettings.PaperSource;
      }
      set
      {
        if (this.Site != null)
        {
          if (value.ToString() != this.DefaultPageSettings.PaperSource.ToString())
            this.paperSource = value;
          else
            this.paperSource = (PaperSource) null;
        }
        else
        {
          if (value == null)
            return;
          this.DefaultPageSettings.PaperSource = value;
        }
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public Margins Margins
    {
      get
      {
        if (this.Site != null)
          return this.margins;
        return this.DefaultPageSettings.Margins;
      }
      set
      {
        if (this.Site != null)
        {
          if (value != this.DefaultPageSettings.Margins)
            this.margins = value;
          else
            this.margins = (Margins) null;
        }
        else
        {
          if (!(value != (Margins) null))
            return;
          this.DefaultPageSettings.Margins = value;
        }
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the font of the page header.")]
    [DefaultValue(typeof (Font), "Microsoft Sans Serif, 8.25pt")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font HeaderFont
    {
      get
      {
        return this.headerFont;
      }
      set
      {
        this.headerFont = value;
      }
    }

    [DefaultValue(typeof (Font), "Microsoft Sans Serif, 8.25pt")]
    [Description("Gets or sets the font of the page footer.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font FooterFont
    {
      get
      {
        return this.footerFont;
      }
      set
      {
        this.footerFont = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("[true] if the LeftHeader and RightHeader should be reversed on even pages, [false] otherwise.")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool ReverseHeaderOnEvenPages
    {
      get
      {
        return this.reverseHeaderOnEvenPages;
      }
      set
      {
        this.reverseHeaderOnEvenPages = value;
      }
    }

    [Description("[true] if the LeftFooter and RightFooter should be reversed on even pages, [false] otherwise.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool ReverseFooterOnEvenPages
    {
      get
      {
        return this.reverseFooterOnEvenPages;
      }
      set
      {
        this.reverseFooterOnEvenPages = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Description("Gets or sets the text that will be printed near the upper left corner of the page.")]
    [DefaultValue("")]
    public string LeftHeader
    {
      get
      {
        return this.leftHeader;
      }
      set
      {
        this.leftHeader = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the text that will be printed near the upper right corner of the page.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    public string RightHeader
    {
      get
      {
        return this.rightHeader;
      }
      set
      {
        this.rightHeader = value;
      }
    }

    [DefaultValue("")]
    [Description("Gets or sets the text that will be printed at the upper center of the page.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public string MiddleHeader
    {
      get
      {
        return this.middleHeader;
      }
      set
      {
        this.middleHeader = value;
      }
    }

    [DefaultValue("")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the text that will be printed near the bottom left corner of the page.")]
    public string LeftFooter
    {
      get
      {
        return this.leftFooter;
      }
      set
      {
        this.leftFooter = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue("")]
    [Description("Gets or sets the text that will be printed near the bottom right corner of the page.")]
    public string RightFooter
    {
      get
      {
        return this.rightFooter;
      }
      set
      {
        this.rightFooter = value;
      }
    }

    [DefaultValue("")]
    [Browsable(true)]
    [Description("Gets or sets the text that will be printed at the bottom center of the page.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string MiddleFooter
    {
      get
      {
        return this.middleFooter;
      }
      set
      {
        this.middleFooter = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(30)]
    [Description("Gets or sets the height of the header area.")]
    public int HeaderHeight
    {
      get
      {
        return this.headerHeight;
      }
      set
      {
        this.headerHeight = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(30)]
    [Description("Gets or sets the height of the header area.")]
    [Browsable(true)]
    public int FooterHeight
    {
      get
      {
        return this.footerHeight;
      }
      set
      {
        this.footerHeight = value;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public IPrintable AssociatedObject
    {
      get
      {
        return this.associatedObject;
      }
      set
      {
        if (this.associatedObject == value)
          return;
        if (this.associatedObject is IComponent)
          ((IComponent) this.associatedObject).Disposed -= new EventHandler(this.associatedObject_Disposed);
        this.associatedObject = value;
        if (this.associatedObject is IComponent)
          ((IComponent) this.associatedObject).Disposed += new EventHandler(this.associatedObject_Disposed);
        this.OnAssociatedObjectChanged();
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public RadPrintWatermark Watermark
    {
      get
      {
        return this.watermark;
      }
      set
      {
        this.watermark = value;
      }
    }

    [Browsable(false)]
    public int PrintedPage
    {
      get
      {
        return this.printedPage;
      }
    }

    [Browsable(false)]
    public int PageCount
    {
      get
      {
        return this.pageCount;
      }
    }

    [Browsable(false)]
    public bool IsPrinting
    {
      get
      {
        return this.isPrinting;
      }
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
      if (this.watermark != null && !this.watermark.DrawInFront && this.watermark.ShouldPrintOnPage(this.printedPage + 1))
        this.PrintWatermark(e);
      ++this.printedPage;
      e.HasMorePages = this.AssociatedObject.PrintPage(this.printedPage, this, e);
      this.PrintHeader(e);
      this.PrintFooter(e);
      base.OnPrintPage(e);
      if (this.watermark != null && this.watermark.DrawInFront && this.watermark.ShouldPrintOnPage(this.printedPage))
        this.PrintWatermark(e);
      if (this.PrinterSettings.PrintRange == PrintRange.SomePages && this.printedPage >= this.PrinterSettings.ToPage)
        e.HasMorePages = false;
      if (this.PrinterSettings.PrintRange == PrintRange.CurrentPage)
        e.HasMorePages = false;
      if (this.PrinterSettings.PrintRange != PrintRange.Selection || this.CurrentPage + this.SelectionLength - 1 != this.printedPage)
        return;
      e.HasMorePages = false;
    }

    protected override void OnBeginPrint(PrintEventArgs e)
    {
      base.OnBeginPrint(e);
      this.printedPage = 0;
      if (this.PrinterSettings.PrintRange == PrintRange.SomePages)
      {
        int num1 = Math.Min(this.PrinterSettings.FromPage, this.PrinterSettings.ToPage);
        int num2 = Math.Max(this.PrinterSettings.FromPage, this.PrinterSettings.ToPage);
        this.PrinterSettings.FromPage = num1;
        this.PrinterSettings.ToPage = num2;
        this.printedPage = Math.Max(this.PrinterSettings.FromPage - 1, 0);
      }
      else if (this.PrinterSettings.PrintRange == PrintRange.Selection || this.PrinterSettings.PrintRange == PrintRange.CurrentPage)
        this.printedPage = this.CurrentPage - 1;
      this.isPrinting = true;
      this.printStartDate = DateTime.Now;
      this.pageCount = this.AssociatedObject.BeginPrint(this, e);
      if (this.pageCount > 0)
        return;
      this.pageCount = 0;
      e.Cancel = true;
    }

    protected override void OnEndPrint(PrintEventArgs e)
    {
      base.OnEndPrint(e);
      e.Cancel = !this.AssociatedObject.EndPrint(this, e);
      if (!e.Cancel)
      {
        this.printedPage = 0;
        this.isPrinting = false;
        this.printStartDate = DateTime.MinValue;
      }
      if (!this.unassignObjectAfterPrint)
        return;
      this.unassignObjectAfterPrint = true;
      this.AssociatedObject = (IPrintable) null;
    }

    protected virtual void PrintHeader(PrintPageEventArgs args)
    {
      Rectangle rectangle = new Rectangle(args.MarginBounds.Location, new Size(args.MarginBounds.Width, this.headerHeight));
      StringFormat format = new StringFormat();
      format.LineAlignment = StringAlignment.Center;
      string s1 = !this.ReverseHeaderOnEvenPages || this.PrintedPage % 2 != 0 ? this.LeftHeader : this.RightHeader;
      string s2 = !this.ReverseHeaderOnEvenPages || this.PrintedPage % 2 != 0 ? this.RightHeader : this.LeftHeader;
      if (this.HasLogoInHeaderFooterString(s1) && this.Logo != null)
        this.PrintLogo(args.Graphics, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height));
      format.Alignment = StringAlignment.Near;
      args.Graphics.DrawString(this.ParseHeaderFooterString(s1), this.HeaderFont, Brushes.Black, (RectangleF) rectangle, format);
      if (this.HasLogoInHeaderFooterString(this.MiddleHeader) && this.Logo != null)
        this.PrintLogo(args.Graphics, new Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height));
      format.Alignment = StringAlignment.Center;
      args.Graphics.DrawString(this.ParseHeaderFooterString(this.MiddleHeader), this.HeaderFont, Brushes.Black, (RectangleF) rectangle, format);
      if (this.HasLogoInHeaderFooterString(s2) && this.Logo != null)
        this.PrintLogo(args.Graphics, new Rectangle(rectangle.Right - rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height));
      format.Alignment = StringAlignment.Far;
      args.Graphics.DrawString(this.ParseHeaderFooterString(s2), this.HeaderFont, Brushes.Black, (RectangleF) rectangle, format);
    }

    protected virtual void PrintFooter(PrintPageEventArgs args)
    {
      Rectangle rectangle = new Rectangle(new Point(args.MarginBounds.Location.X, args.MarginBounds.Bottom - this.footerHeight), new Size(args.MarginBounds.Width, this.footerHeight));
      StringFormat format = new StringFormat();
      format.LineAlignment = StringAlignment.Center;
      string s1 = !this.ReverseFooterOnEvenPages || this.PrintedPage % 2 != 0 ? this.LeftFooter : this.RightFooter;
      string s2 = !this.ReverseFooterOnEvenPages || this.PrintedPage % 2 != 0 ? this.RightFooter : this.LeftFooter;
      if (this.HasLogoInHeaderFooterString(s1) && this.Logo != null)
        this.PrintLogo(args.Graphics, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width / 3, rectangle.Height));
      format.Alignment = StringAlignment.Near;
      args.Graphics.DrawString(this.ParseHeaderFooterString(s1), this.FooterFont, Brushes.Black, (RectangleF) rectangle, format);
      if (this.HasLogoInHeaderFooterString(this.MiddleFooter) && this.Logo != null)
        this.PrintLogo(args.Graphics, new Rectangle(rectangle.X + rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height));
      format.Alignment = StringAlignment.Center;
      args.Graphics.DrawString(this.ParseHeaderFooterString(this.MiddleFooter), this.FooterFont, Brushes.Black, (RectangleF) rectangle, format);
      if (this.HasLogoInHeaderFooterString(s2) && this.Logo != null)
        this.PrintLogo(args.Graphics, new Rectangle(rectangle.Right - rectangle.Width / 3, rectangle.Y, rectangle.Width / 3, rectangle.Height));
      format.Alignment = StringAlignment.Far;
      args.Graphics.DrawString(this.ParseHeaderFooterString(s2), this.FooterFont, Brushes.Black, (RectangleF) rectangle, format);
    }

    protected virtual void PrintLogo(Graphics g, Rectangle rect)
    {
      g.DrawImage(this.Logo, rect);
    }

    protected virtual void PrintWatermark(PrintPageEventArgs args)
    {
      if (this.watermark.DrawText)
      {
        Brush brush = (Brush) new SolidBrush(Color.FromArgb((int) (float) ((double) ((float) this.watermark.TextOpacity / (float) byte.MaxValue) * (double) ((float) this.watermark.ForeColor.A / (float) byte.MaxValue) * (double) byte.MaxValue), (int) this.watermark.ForeColor.R, (int) this.watermark.ForeColor.G, (int) this.watermark.ForeColor.B));
        Point location = new Point((int) ((Decimal) this.watermark.TextHOffset / (Decimal) this.DefaultPageSettings.PaperSize.Width * (Decimal) this.DefaultPageSettings.Bounds.Width), (int) ((Decimal) this.watermark.TextVOffset / (Decimal) this.DefaultPageSettings.PaperSize.Height * (Decimal) this.DefaultPageSettings.Bounds.Height));
        this.DrawTextWithAngle(args.Graphics, location, this.watermark.TextAngle, this.watermark.Text, this.watermark.Font, brush);
      }
      if (!this.watermark.DrawImage)
        return;
      this.DrawWatermarkImage(args.Graphics);
    }

    protected virtual void OnAssociatedObjectChanged()
    {
    }

    protected string ParseHeaderFooterString(string s)
    {
      s = s.Replace("[Page #]", this.printedPage.ToString());
      s = s.Replace("[Total Pages]", this.pageCount.ToString());
      s = s.Replace("[Date Printed]", this.printStartDate.ToShortDateString());
      s = s.Replace("[Time Printed]", this.printStartDate.ToShortTimeString());
      s = s.Replace("[User Name]", SystemInformation.UserName);
      s = s.Replace("[Logo]", string.Empty);
      return s;
    }

    protected bool HasLogoInHeaderFooterString(string s)
    {
      return s.Contains("[Logo]");
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

    private void DrawWatermarkImage(Graphics graphics)
    {
      Image image = Image.FromFile(this.watermark.ImagePath);
      Rectangle destRect = new Rectangle(new Point((int) ((Decimal) this.watermark.ImageHOffset / (Decimal) this.DefaultPageSettings.PaperSize.Width * (Decimal) this.DefaultPageSettings.Bounds.Width), (int) ((Decimal) this.watermark.ImageVOffset / (Decimal) this.DefaultPageSettings.PaperSize.Height * (Decimal) this.DefaultPageSettings.Bounds.Height)), this.DefaultPageSettings.Bounds.Size);
      ImageAttributes imageAttr = new ImageAttributes();
      imageAttr.SetColorMatrix(new ColorMatrix()
      {
        Matrix33 = (float) this.watermark.ImageOpacity / (float) byte.MaxValue
      }, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
      if (this.watermark.ImageTiling)
      {
        TextureBrush textureBrush = new TextureBrush(image, new Rectangle(Point.Empty, image.Size), imageAttr);
        textureBrush.WrapMode = WrapMode.Tile;
        graphics.FillRectangle((Brush) textureBrush, this.DefaultPageSettings.Bounds);
        textureBrush.Dispose();
      }
      else
      {
        destRect.Size = image.Size;
        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
      }
    }

    private void associatedObject_Disposed(object sender, EventArgs e)
    {
      if (!this.IsPrinting)
        this.AssociatedObject = (IPrintable) null;
      else
        this.unassignObjectAfterPrint = true;
    }
  }
}
