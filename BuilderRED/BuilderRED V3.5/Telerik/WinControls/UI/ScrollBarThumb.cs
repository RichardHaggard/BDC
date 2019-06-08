// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollBarThumb
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class ScrollBarThumb : RadItem
  {
    public static RadProperty IsPressedProperty = RadProperty.Register(nameof (IsPressed), typeof (bool), typeof (ScrollBarThumb), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private FillPrimitive fill;
    private BorderPrimitive borderPrimitive;
    private ImagePrimitive gripImage;
    private Point captureCursor;
    private Point captureLocation;

    static ScrollBarThumb()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ScrollBarThumbStateManager(), typeof (ScrollBarThumb));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.fill.Class = "ScrollBarThumbFill";
      this.Children.Add((RadElement) this.fill);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive.Class = "ScrollBarThumbBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.gripImage = new ImagePrimitive();
      this.gripImage.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.gripImage.Image = this.GripImage;
      this.gripImage.Class = "ScrollBarThumbImagePrimitive";
      this.Children.Add((RadElement) this.gripImage);
      int num = (int) this.fill.BindProperty(ScrollBarThumb.IsPressedProperty, (RadObject) this, ScrollBarThumb.IsPressedProperty, PropertyBindingOptions.OneWay);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsPressed
    {
      get
      {
        return (bool) this.GetValue(ScrollBarThumb.IsPressedProperty);
      }
    }

    public Image GripImage
    {
      get
      {
        return this.gripImage.Image;
      }
      set
      {
        this.gripImage.Image = value;
      }
    }

    public FillPrimitive ThumbFill
    {
      get
      {
        return this.fill;
      }
    }

    public BorderPrimitive ThumbBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      RadScrollBarElement parent = this.Parent as RadScrollBarElement;
      if (parent == null || !parent.Enabled)
        return;
      this.Capture = true;
      int num = (int) this.SetValue(ScrollBarThumb.IsPressedProperty, (object) true);
      this.captureCursor = e.Location;
      this.captureLocation = this.BoundingRectangle.Location;
      this.captureLocation.X -= this.Margin.Left;
      this.captureLocation.Y -= this.Margin.Top;
      parent.FireThumbTrackMessage();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if ((e.Delta <= 0 || e.Button != MouseButtons.Left || this.Capture) && !this.Capture)
        return;
      this.SetPosition(e, true);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.Capture)
        return;
      int num = (int) this.SetValue(ScrollBarThumb.IsPressedProperty, (object) false);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (!this.Capture && (e.Button != MouseButtons.Left || this.Capture))
        return;
      this.Capture = false;
      int num = (int) this.SetValue(ScrollBarThumb.IsPressedProperty, (object) false);
      this.SetPosition(e, false);
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      if ((this.Parent as RadScrollBarElement).ScrollType == ScrollType.Horizontal)
      {
        if (!this.Enabled)
          return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Disabled;
        if (!this.IsPressed && !this.IsMouseOver)
          return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Normal;
        if (this.IsPressed)
          return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Pressed;
        return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.Hot;
      }
      if (!this.Enabled)
        return VisualStyleElement.ScrollBar.ThumbButtonVertical.Disabled;
      if (!this.IsPressed && !this.IsMouseOver)
        return VisualStyleElement.ScrollBar.ThumbButtonVertical.Normal;
      if (this.IsPressed)
        return VisualStyleElement.ScrollBar.ThumbButtonVertical.Pressed;
      return VisualStyleElement.ScrollBar.ThumbButtonVertical.Hot;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.GetXPVisualStyle();
    }

    protected override void PaintElementSkin(IGraphics graphics)
    {
      base.PaintElementSkin(graphics);
      if ((this.Parent as RadScrollBarElement).ScrollType == ScrollType.Horizontal)
      {
        if (!SystemSkinManager.Instance.SetCurrentElement(VisualStyleElement.ScrollBar.GripperHorizontal.Normal))
          return;
        Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
        Size partPreferredSize = SystemSkinManager.Instance.GetPartPreferredSize(underlayGraphics, this.Bounds, ThemeSizeType.True);
        Rectangle bounds = new Rectangle(new Point((this.ControlBoundingRectangle.Width - partPreferredSize.Width) / 2, (this.ControlBoundingRectangle.Height - partPreferredSize.Height) / 2), partPreferredSize);
        SystemSkinManager.Instance.PaintCurrentElement(underlayGraphics, bounds);
      }
      else
      {
        if (!SystemSkinManager.Instance.SetCurrentElement(VisualStyleElement.ScrollBar.GripperVertical.Normal))
          return;
        Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
        Size partPreferredSize = SystemSkinManager.Instance.GetPartPreferredSize(underlayGraphics, this.Bounds, ThemeSizeType.True);
        Rectangle bounds = new Rectangle(new Point((this.ControlBoundingRectangle.Width - partPreferredSize.Width) / 2, (this.ControlBoundingRectangle.Height - partPreferredSize.Height) / 2), partPreferredSize);
        SystemSkinManager.Instance.PaintCurrentElement(underlayGraphics, bounds);
      }
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0 && (element is FillPrimitive || element is BorderPrimitive))
        return false;
      return base.ShouldPaintChild(element);
    }

    private Point GetNewLocation(MouseEventArgs e)
    {
      Point empty = Point.Empty;
      return Point.Add(Point.Subtract(e.Location, new Size(this.captureCursor)), new Size(this.captureLocation));
    }

    private void SetPosition(MouseEventArgs e, bool dragging)
    {
      (this.Parent as RadScrollBarElement)?.SetThumbPosition(this.GetNewLocation(e), dragging);
    }
  }
}
