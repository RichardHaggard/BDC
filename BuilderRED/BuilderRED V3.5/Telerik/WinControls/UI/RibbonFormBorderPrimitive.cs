// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonFormBorderPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RibbonFormBorderPrimitive : BasePrimitive
  {
    public static RadProperty BorderColorProperty = RadProperty.Register(nameof (BorderColor), typeof (Color), typeof (RibbonFormBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.DarkBlue, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderColorProperty1 = RadProperty.Register(nameof (BorderColor1), typeof (Color), typeof (RibbonFormBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.LightBlue, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShadowColorProperty = RadProperty.Register(nameof (ShadowColor), typeof (Color), typeof (RibbonFormBorderPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(30, Color.Black), ElementPropertyOptions.AffectsDisplay));
    private const int OUTER_BORDER_WIDTH = 1;
    private const int INNER_BORDER_WIDTH = 3;
    private RadRibbonBar ribbonBar;
    private Bitmap ribbonCaptionFillBitmap;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Determines the color of the form's first border")]
    public Color BorderColor
    {
      get
      {
        return (Color) this.GetValue(RibbonFormBorderPrimitive.BorderColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RibbonFormBorderPrimitive.BorderColorProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Determines the color of the form's second border")]
    public Color BorderColor1
    {
      get
      {
        return (Color) this.GetValue(RibbonFormBorderPrimitive.BorderColorProperty1);
      }
      set
      {
        int num = (int) this.SetValue(RibbonFormBorderPrimitive.BorderColorProperty1, (object) value);
      }
    }

    [Description("Determines the color of the form's client area shadow")]
    [Category("Appearance")]
    [Browsable(true)]
    public Color ShadowColor
    {
      get
      {
        return (Color) this.GetValue(RibbonFormBorderPrimitive.ShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RibbonFormBorderPrimitive.ShadowColorProperty, (object) value);
      }
    }

    private RadRibbonBar GetRibbonBar()
    {
      if (this.ribbonBar != null)
        return this.ribbonBar;
      foreach (Control control in (ArrangedElementCollection) this.ElementTree.Control.Controls)
      {
        if (control is RadRibbonBar)
        {
          RadRibbonBar radRibbonBar = control as RadRibbonBar;
          radRibbonBar.RibbonBarElement.CaptionFill.PropertyChanged += new PropertyChangedEventHandler(this.CaptionFill_PropertyChanged);
          return this.ribbonBar = radRibbonBar;
        }
      }
      return (RadRibbonBar) null;
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
        return;
      this.Parent.BorderThickness = new Padding(4, 1, 4, 4);
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintPrimitive(graphics, angle, scale);
      if (this.Size.Height == 0 || this.Size.Width == 0)
        return;
      Rectangle rect1 = new Rectangle(new Point(1, 1), Size.Subtract(this.Size, new Size(2, 2)));
      this.PaintInnerBorder(graphics, rect1, this.BorderColor1);
      this.PaintTitleBarExtensions(graphics);
      Rectangle rect2 = new Rectangle(Point.Empty, Size.Subtract(this.Size, new Size(1, 1)));
      this.PaintOuterBorder(graphics, rect2, this.BorderColor);
      this.PaintClientAreaShadow(graphics, this.ShadowColor);
    }

    private void PaintTitleBarExtensions(IGraphics graphics)
    {
      RadRibbonBar ribbonBar = this.GetRibbonBar();
      if (ribbonBar == null || ribbonBar.RibbonBarElement.Children.Count <= 5)
        return;
      FillPrimitive captionFill = ribbonBar.RibbonBarElement.CaptionFill;
      BorderPrimitive captionBorder = ribbonBar.RibbonBarElement.CaptionBorder;
      if (captionFill != null)
        this.PaintExtensionsFill(graphics, captionFill);
      if (captionBorder == null || captionBorder.Visibility != ElementVisibility.Visible)
        return;
      this.PaintExtensionsBorders(graphics, captionFill, captionBorder);
    }

    private void PaintExtensionsFill(IGraphics graphics, FillPrimitive titleBarFill)
    {
      this.ribbonCaptionFillBitmap = titleBarFill.GetAsBitmap(Brushes.Transparent, 0.0f, new SizeF(1f, 1f));
      if (this.ribbonCaptionFillBitmap == null)
        return;
      Point location = titleBarFill.ControlBoundingRectangle.Location;
      location.Offset(new Point(0, 1));
      graphics.DrawBitmap((Image) this.ribbonCaptionFillBitmap, location.X, location.Y, this.Size.Width, titleBarFill.ControlBoundingRectangle.Height);
      this.ribbonCaptionFillBitmap.Dispose();
    }

    private void PaintExtensionsBorders(
      IGraphics graphics,
      FillPrimitive titleBarFill,
      BorderPrimitive fillPrimitiveBorder)
    {
      Rectangle rectangle1 = new Rectangle(new Point(1, titleBarFill.ControlBoundingRectangle.Y + 1), new Size(3, titleBarFill.ControlBoundingRectangle.Height));
      int y1 = (double) fillPrimitiveBorder.BottomWidth > 1.0 ? (int) ((double) rectangle1.Bottom - (double) fillPrimitiveBorder.BottomWidth) : rectangle1.Bottom - 2;
      int y2 = (double) fillPrimitiveBorder.BottomWidth > 1.0 ? (int) ((double) rectangle1.Bottom - (double) fillPrimitiveBorder.BottomWidth / 2.0) : rectangle1.Bottom - 1;
      Point point1 = new Point(rectangle1.X, y1);
      Point point2 = new Point(rectangle1.Right, y1);
      Point point3 = new Point(rectangle1.X, y2);
      Point point4 = new Point(rectangle1.Right, y2);
      graphics.DrawLine(fillPrimitiveBorder.BottomShadowColor, point1.X, point1.Y, point2.X, point2.Y);
      graphics.DrawLine(fillPrimitiveBorder.BottomColor, point3.X, point3.Y, point4.X, point4.Y);
      Rectangle rectangle2 = new Rectangle(new Point(this.Size.Width - 4, titleBarFill.ControlBoundingRectangle.Y + 1), new Size(3, titleBarFill.ControlBoundingRectangle.Height));
      int y3 = (double) fillPrimitiveBorder.BottomWidth > 1.0 ? (int) ((double) rectangle2.Bottom - (double) fillPrimitiveBorder.BottomWidth) : rectangle2.Bottom - 2;
      int y4 = (double) fillPrimitiveBorder.BottomWidth > 1.0 ? (int) ((double) rectangle2.Bottom - (double) fillPrimitiveBorder.BottomWidth / 2.0) : rectangle2.Bottom - 1;
      Point point5 = new Point(rectangle2.X, y3);
      Point point6 = new Point(rectangle2.Right, y3);
      Point point7 = new Point(rectangle2.X, y4);
      Point point8 = new Point(rectangle2.Right, y4);
      graphics.DrawLine(fillPrimitiveBorder.BottomShadowColor, point5.X, point5.Y, point6.X, point6.Y);
      graphics.DrawLine(fillPrimitiveBorder.BottomColor, point7.X, point7.Y, point8.X, point8.Y);
    }

    private void PaintOuterBorder(IGraphics graphics, Rectangle rect, Color color)
    {
      GraphicsPath path = this.ElementTree.RootElement.Shape != null ? (!(this.ElementTree.RootElement.Shape is OfficeShape) ? this.ElementTree.RootElement.Shape.CreatePath(rect) : (this.ElementTree.RootElement.Shape as OfficeShape).GetContourPath(rect)) : new OfficeShape(true).GetContourPath(rect);
      graphics.DrawPath(path, color, PenAlignment.Inset, 1f);
      path.Dispose();
    }

    private void PaintInnerBorder(IGraphics graphics, Rectangle rect, Color color)
    {
      graphics.DrawRectangle(rect, color, PenAlignment.Inset, 3f);
    }

    private void PaintClientAreaShadow(IGraphics graphics, Color color)
    {
      RadRibbonBar ribbonBar = this.GetRibbonBar();
      if (ribbonBar == null)
        return;
      int height = ribbonBar.RibbonBarElement.RibbonCaption.ControlBoundingRectangle.Height;
      graphics.DrawLine(color, 3, height + 1, 3, this.ControlBoundingRectangle.Height - 4);
      graphics.DrawLine(color, 4, this.ControlBoundingRectangle.Height - 4, this.ControlBoundingRectangle.Width - 5, this.ControlBoundingRectangle.Height - 4);
      graphics.DrawLine(color, this.ControlBoundingRectangle.Width - 4, height + 1, this.ControlBoundingRectangle.Width - 4, this.ControlBoundingRectangle.Height - 4);
    }

    private void CaptionFill_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "BackColor") && !(e.PropertyName == "BackColor2") && (!(e.PropertyName == "BackColor3") && !(e.PropertyName == "BackColor4")))
        return;
      this.Invalidate();
    }

    protected override void DisposeManagedResources()
    {
      if (this.ribbonBar != null && !this.ribbonBar.Disposing && !this.ribbonBar.IsDisposed)
        this.ribbonBar.RibbonBarElement.CaptionFill.PropertyChanged -= new PropertyChangedEventHandler(this.CaptionFill_PropertyChanged);
      base.DisposeManagedResources();
    }
  }
}
