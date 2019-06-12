// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTitleBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadTitleBarElement : RadItem
  {
    private bool allowResize = true;
    private bool canManageOwnerForm = true;
    private BorderPrimitive border;
    private FillPrimitive fill;
    private TextPrimitive captionElement;
    private RadImageButtonElement closeButton;
    private RadImageButtonElement minimizeButton;
    private RadImageButtonElement maximizeButton;
    private RadImageButtonElement helpButton;
    private StackLayoutElement systemButtons;
    private Image leftImage;
    private Image rightImage;
    private Image middleImage;
    private Point downPoint;
    private Icon imageIcon;
    protected ImagePrimitive titleBarIcon;
    protected DockLayoutPanel layout;

    static RadTitleBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadTitleBarElementStateManager(), typeof (RadTitleBarElement));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.fill = new FillPrimitive();
      this.TitleBarFill.Class = "TitleFill";
      this.Children.Add((RadElement) this.TitleBarFill);
      this.border = new BorderPrimitive();
      this.border.Class = "TitleBorder";
      this.Children.Add((RadElement) this.border);
      this.layout = new DockLayoutPanel();
      this.layout.StretchVertically = true;
      this.layout.StretchHorizontally = true;
      this.Children.Add((RadElement) this.layout);
      this.titleBarIcon = new ImagePrimitive();
      this.titleBarIcon.Class = "TitleIcon";
      this.titleBarIcon.ImageScaling = ImageScaling.SizeToFit;
      this.layout.Children.Add((RadElement) this.titleBarIcon);
      this.systemButtons = new StackLayoutElement();
      this.systemButtons.Orientation = Orientation.Horizontal;
      this.systemButtons.Alignment = ContentAlignment.MiddleRight;
      this.helpButton = new RadImageButtonElement();
      this.helpButton.StateManager = new TitleBarButtonStateManager().StateManagerInstance;
      this.helpButton.StretchHorizontally = false;
      this.helpButton.StretchVertically = false;
      this.helpButton.Class = "HelpButton";
      this.helpButton.ButtonFillElement.Name = "HelpButtonFill";
      this.helpButton.Click += new EventHandler(this.OnHelpButtonClick);
      this.helpButton.ThemeRole = "TitleBarHelpButton";
      this.helpButton.Visibility = ElementVisibility.Collapsed;
      this.systemButtons.Children.Add((RadElement) this.helpButton);
      this.minimizeButton = new RadImageButtonElement();
      this.minimizeButton.StateManager = new TitleBarButtonStateManager().StateManagerInstance;
      this.minimizeButton.StretchHorizontally = false;
      this.minimizeButton.StretchVertically = false;
      this.minimizeButton.Class = "MinimizeButton";
      this.minimizeButton.ButtonFillElement.Name = "MinimizeButtonFill";
      this.minimizeButton.Click += new EventHandler(this.OnMinimize);
      this.minimizeButton.ThemeRole = "TitleBarMinimizeButton";
      this.systemButtons.Children.Add((RadElement) this.minimizeButton);
      this.maximizeButton = new RadImageButtonElement();
      this.maximizeButton.StateManager = new TitleBarButtonStateManager().StateManagerInstance;
      this.maximizeButton.StretchHorizontally = false;
      this.maximizeButton.StretchVertically = false;
      this.maximizeButton.Class = "MaximizeButton";
      this.maximizeButton.ButtonFillElement.Name = "MaximizeButtonFill";
      this.maximizeButton.Click += new EventHandler(this.OnMaximizeRestore);
      this.maximizeButton.ThemeRole = "TitleBarMaximizeButton";
      this.systemButtons.Children.Add((RadElement) this.maximizeButton);
      this.closeButton = new RadImageButtonElement();
      this.closeButton.StateManager = new TitleBarButtonStateManager().StateManagerInstance;
      this.closeButton.StretchHorizontally = false;
      this.closeButton.StretchVertically = false;
      this.closeButton.Class = "CloseButton";
      this.closeButton.ButtonFillElement.Name = "CloseButtonFill";
      this.closeButton.Click += new EventHandler(this.OnClose);
      this.closeButton.ThemeRole = "TitleBarCloseButton";
      this.systemButtons.Children.Add((RadElement) this.closeButton);
      this.layout.Children.Add((RadElement) this.systemButtons);
      this.captionElement = (TextPrimitive) new TitleBarTextPrimitive();
      this.captionElement.UseMnemonic = false;
      this.captionElement.ShowKeyboardCues = false;
      this.captionElement.Class = "TitleCaption";
      this.captionElement.StretchVertically = true;
      this.layout.Children.Add((RadElement) this.captionElement);
      this.SetDockingAlignmentUponRtlChange(this.RightToLeft);
      int num = (int) this.captionElement.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
    }

    [Browsable(false)]
    public StackLayoutElement SystemButtons
    {
      get
      {
        return this.systemButtons;
      }
    }

    public RadButtonElement MinimizeButton
    {
      get
      {
        return (RadButtonElement) this.minimizeButton;
      }
    }

    public RadButtonElement MaximizeButton
    {
      get
      {
        return (RadButtonElement) this.maximizeButton;
      }
    }

    public RadButtonElement CloseButton
    {
      get
      {
        return (RadButtonElement) this.closeButton;
      }
    }

    public RadButtonElement HelpButton
    {
      get
      {
        return (RadButtonElement) this.helpButton;
      }
    }

    public virtual Image LeftImage
    {
      get
      {
        return this.leftImage;
      }
      set
      {
        if (this.leftImage == value)
          return;
        this.leftImage = value;
        this.OnNotifyPropertyChanged(nameof (LeftImage));
      }
    }

    public virtual Image RightImage
    {
      get
      {
        return this.rightImage;
      }
      set
      {
        if (this.rightImage == value)
          return;
        this.rightImage = value;
        this.OnNotifyPropertyChanged(nameof (RightImage));
      }
    }

    public virtual Image MiddleImage
    {
      get
      {
        return this.middleImage;
      }
      set
      {
        if (this.middleImage == value)
          return;
        this.middleImage = value;
        this.OnNotifyPropertyChanged(nameof (MiddleImage));
      }
    }

    [Browsable(false)]
    public FillPrimitive FillPrimitive
    {
      get
      {
        return this.fill;
      }
    }

    [Browsable(false)]
    public BorderPrimitive BorderPrimitive
    {
      get
      {
        return this.border;
      }
    }

    public TextPrimitive TitlePrimitive
    {
      get
      {
        return this.captionElement;
      }
      set
      {
        this.captionElement = value;
      }
    }

    public ImagePrimitive IconPrimitive
    {
      get
      {
        return this.titleBarIcon;
      }
      set
      {
        this.titleBarIcon = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Determines whether the parent form can be resized by dragging the title bar's edges.")]
    public bool AllowResize
    {
      get
      {
        return this.allowResize;
      }
      set
      {
        this.allowResize = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Determines whether the parent form can be managed by the title bar.")]
    public bool CanManageOwnerForm
    {
      get
      {
        return this.canManageOwnerForm;
      }
      set
      {
        this.allowResize = value;
        this.canManageOwnerForm = value;
      }
    }

    public RadElement CaptionElement
    {
      get
      {
        return (RadElement) this.captionElement;
      }
    }

    public event TitleBarSystemEventHandler Close;

    public event TitleBarSystemEventHandler Minimize;

    public event TitleBarSystemEventHandler MaximizeRestore;

    protected void OnClose(object sender, EventArgs args)
    {
      if (!this.canManageOwnerForm)
        return;
      if (this.Close != null)
        this.Close(sender, args);
      (this.ElementTree != null ? this.ElementTree.Control as RadTitleBar : (RadTitleBar) null)?.CallOnClose(sender, args);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Closed");
    }

    protected void OnMinimize(object sender, EventArgs args)
    {
      if (!this.canManageOwnerForm)
        return;
      if (this.Minimize != null)
        this.Minimize(sender, args);
      (this.ElementTree.Control as RadTitleBar)?.CallOnMinimize(sender, args);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Minimize");
    }

    protected void OnMaximizeRestore(object sender, EventArgs args)
    {
      if (!this.canManageOwnerForm)
        return;
      if (this.MaximizeRestore != null)
        this.MaximizeRestore(sender, args);
      (this.ElementTree.Control as RadTitleBar)?.CallOnMaximizeRestore(sender, args);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Restore");
    }

    protected void OnHelpButtonClick(object sender, EventArgs args)
    {
      if (!this.canManageOwnerForm)
        return;
      (this.ElementTree.Control as RadTitleBar)?.CallOnHelpButtonClicked(sender, args);
    }

    [DefaultValue(null)]
    [Category("Window Style")]
    public Icon ImageIcon
    {
      get
      {
        return this.imageIcon;
      }
      set
      {
        this.imageIcon = value;
        if (this.ElementTree == null)
          return;
        Form parentForm = RadTitleBar.FindParentForm(this.ElementTree.Control);
        if (parentForm != null)
          parentForm.Icon = value;
        this.titleBarIcon.Image = value != null ? (Image) Bitmap.FromHicon(value.Handle) : (Image) null;
        this.imageIcon = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FillPrimitive TitleBarFill
    {
      get
      {
        return this.fill;
      }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      this.HandleDoubleClickClick();
    }

    public virtual void HandleDoubleClickClick()
    {
      if (this.ElementTree == null)
        return;
      Form parentForm = RadTitleBar.FindParentForm(this.ElementTree.Control);
      if (parentForm == null || !parentForm.MaximizeBox)
        return;
      this.OnMaximizeRestore((object) this, EventArgs.Empty);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.HandleMouseDown(e);
    }

    public virtual void HandleMouseDown(MouseEventArgs e)
    {
      this.downPoint = e.Location;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.HandleMouseUp();
    }

    public virtual void HandleMouseUp()
    {
      Cursor.Current = Cursors.Default;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.IsDesignMode || this.DesignMode)
        return;
      Form form = (Form) null;
      if (this.ElementTree != null)
        form = RadTitleBar.FindParentForm(this.ElementTree.Control);
      if (form == null)
        return;
      this.HandleMouseMove(e, form);
    }

    public virtual void HandleMouseMove(MouseEventArgs e, Form form)
    {
      ShapedForm shapedForm = form as ShapedForm;
      bool flag = true;
      if (shapedForm != null)
        flag = shapedForm.AllowResize;
      if (form.WindowState != FormWindowState.Normal && form.WindowState != FormWindowState.Maximized)
        return;
      if (e.Button == MouseButtons.Left && this.downPoint != e.Location)
      {
        Telerik.WinControls.NativeMethods.ReleaseCapture();
        if (flag && this.allowResize)
        {
          if (e.Y >= 0 && e.Y < 7)
          {
            if (e.X < 7)
            {
              Cursor.Current = Cursors.SizeNWSE;
              Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 13, IntPtr.Zero);
            }
            else if (e.X > this.Size.Width - 7)
            {
              Cursor.Current = Cursors.SizeNESW;
              Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 14, IntPtr.Zero);
            }
            else
            {
              Cursor.Current = Cursors.SizeNS;
              Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 12, IntPtr.Zero);
            }
          }
          else if (e.X < 7)
          {
            Cursor.Current = Cursors.SizeWE;
            Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 10, IntPtr.Zero);
          }
          else if (e.X > this.Size.Width - 7)
          {
            Cursor.Current = Cursors.SizeWE;
            Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 11, IntPtr.Zero);
          }
        }
        if (!this.canManageOwnerForm || e.X >= this.Size.Width - this.systemButtons.Size.Width)
          return;
        Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, 2, IntPtr.Zero);
      }
      else
      {
        if (!this.allowResize)
          return;
        if (e.Y >= 0 && e.Y < 4)
        {
          Cursor.Current = Cursors.SizeNS;
          if (e.X < 7)
            Cursor.Current = Cursors.SizeNWSE;
          else if (e.X > this.Size.Width - 7)
            Cursor.Current = Cursors.SizeNESW;
          else
            Cursor.Current = Cursors.SizeNS;
        }
        else if (e.X < 7)
          Cursor.Current = Cursors.SizeWE;
        else if (e.X > this.Size.Width - 7)
          Cursor.Current = Cursors.SizeWE;
        else
          Cursor.Current = Cursors.Default;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadElement.RightToLeftProperty)
        this.SetDockingAlignmentUponRtlChange((bool) e.NewValue);
      base.OnPropertyChanged(e);
    }

    private void SetDockingAlignmentUponRtlChange(bool rtlState)
    {
      if (rtlState)
      {
        DockLayoutPanel.SetDock((RadElement) this.titleBarIcon, Dock.Right);
        DockLayoutPanel.SetDock((RadElement) this.systemButtons, Dock.Left);
      }
      else
      {
        DockLayoutPanel.SetDock((RadElement) this.titleBarIcon, Dock.Left);
        DockLayoutPanel.SetDock((RadElement) this.systemButtons, Dock.Right);
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      switch (e.PropertyName)
      {
        case "LeftImage":
        case "RightImage":
        case "MiddleImage":
          this.Invalidate();
          break;
      }
    }

    protected override void PaintOverride(
      IGraphics screenRadGraphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (this.LeftImage != null && this.RightImage != null && this.MiddleImage != null)
      {
        Graphics underlayGraphics = screenRadGraphics.UnderlayGraphics as Graphics;
        using (Brush brush = (Brush) new SolidBrush(Color.FromArgb((int) byte.MaxValue, this.BackColor)))
          underlayGraphics.FillRectangle(brush, this.BoundingRectangle);
        int height = this.MiddleImage.Height;
        int x = this.Size.Width - this.rightImage.Width;
        Rectangle rectangle = new Rectangle(this.leftImage.Width, 0, this.Size.Width - this.leftImage.Width - this.rightImage.Width, height);
        Rectangle clipRect = Rectangle.Intersect(this.BoundingRectangle, rectangle);
        ImageLayout backgroundImageLayout = ImageLayout.Tile;
        if (this.ElementTree != null)
          backgroundImageLayout = this.ElementTree.Control.BackgroundImageLayout;
        RadTitleBarElement.DrawBackgroundImage(underlayGraphics, this.MiddleImage, this.BackColor, backgroundImageLayout, rectangle, clipRect, Point.Empty, this.RightToLeft);
        underlayGraphics.DrawImage(this.rightImage, new Point(x, 0));
        underlayGraphics.DrawImage(this.leftImage, Point.Empty);
      }
      base.PaintOverride(screenRadGraphics, clipRectangle, angle, scale, useRelativeTransformation);
    }

    internal static void DrawBackgroundImage(
      Graphics g,
      Image backgroundImage,
      Color backColor,
      ImageLayout backgroundImageLayout,
      Rectangle bounds,
      Rectangle clipRect,
      Point scrollOffset,
      bool rightToLeft)
    {
      if (backgroundImageLayout == ImageLayout.Tile)
      {
        using (TextureBrush textureBrush = new TextureBrush(backgroundImage, WrapMode.Tile))
        {
          if (scrollOffset != Point.Empty)
          {
            Matrix transform = textureBrush.Transform;
            transform.Translate((float) scrollOffset.X, (float) scrollOffset.Y);
            textureBrush.Transform = transform;
          }
          g.FillRectangle((Brush) textureBrush, clipRect);
        }
      }
      else
      {
        Rectangle backgroundImageRectangle = RadTitleBarElement.CalculateBackgroundImageRectangle(bounds, backgroundImage, backgroundImageLayout);
        if (rightToLeft && backgroundImageLayout == ImageLayout.None)
          backgroundImageRectangle.X += clipRect.Width - backgroundImageRectangle.Width;
        using (SolidBrush solidBrush = new SolidBrush(backColor))
          g.FillRectangle((Brush) solidBrush, clipRect);
        if (!clipRect.Contains(backgroundImageRectangle))
        {
          switch (backgroundImageLayout)
          {
            case ImageLayout.None:
              backgroundImageRectangle.Offset(clipRect.Location);
              Rectangle destRect1 = backgroundImageRectangle;
              destRect1.Intersect(clipRect);
              Rectangle rectangle1 = new Rectangle(Point.Empty, destRect1.Size);
              g.DrawImage(backgroundImage, destRect1, rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height, GraphicsUnit.Pixel);
              break;
            case ImageLayout.Stretch:
            case ImageLayout.Zoom:
              backgroundImageRectangle.Intersect(clipRect);
              g.DrawImage(backgroundImage, backgroundImageRectangle);
              break;
            default:
              Rectangle destRect2 = backgroundImageRectangle;
              destRect2.Intersect(clipRect);
              Rectangle rectangle2 = new Rectangle(new Point(destRect2.X - backgroundImageRectangle.X, destRect2.Y - backgroundImageRectangle.Y), destRect2.Size);
              g.DrawImage(backgroundImage, destRect2, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, GraphicsUnit.Pixel);
              break;
          }
        }
        else
        {
          ImageAttributes imageAttr = new ImageAttributes();
          imageAttr.SetWrapMode(WrapMode.TileFlipXY);
          g.DrawImage(backgroundImage, backgroundImageRectangle, 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, imageAttr);
          imageAttr.Dispose();
        }
      }
    }

    internal static Rectangle CalculateBackgroundImageRectangle(
      Rectangle bounds,
      Image backgroundImage,
      ImageLayout imageLayout)
    {
      Rectangle rectangle = bounds;
      if (backgroundImage != null)
      {
        switch (imageLayout)
        {
          case ImageLayout.None:
            rectangle.Size = backgroundImage.Size;
            return rectangle;
          case ImageLayout.Tile:
            return rectangle;
          case ImageLayout.Center:
            rectangle.Size = backgroundImage.Size;
            Size size1 = bounds.Size;
            if (size1.Width > rectangle.Width)
              rectangle.X = (size1.Width - rectangle.Width) / 2;
            if (size1.Height > rectangle.Height)
              rectangle.Y = (size1.Height - rectangle.Height) / 2;
            return rectangle;
          case ImageLayout.Stretch:
            rectangle.Size = bounds.Size;
            return rectangle;
          case ImageLayout.Zoom:
            Size size2 = backgroundImage.Size;
            float num1 = (float) bounds.Width / (float) size2.Width;
            float num2 = (float) bounds.Height / (float) size2.Height;
            if ((double) num1 < (double) num2)
            {
              rectangle.Width = bounds.Width;
              rectangle.Height = (int) ((double) size2.Height * (double) num1 + 0.5);
              if (bounds.Y >= 0)
                rectangle.Y = (bounds.Height - rectangle.Height) / 2;
              return rectangle;
            }
            rectangle.Height = bounds.Height;
            rectangle.Width = (int) ((double) size2.Width * (double) num2 + 0.5);
            if (bounds.X >= 0)
              rectangle.X = (bounds.Width - rectangle.Width) / 2;
            return rectangle;
        }
      }
      return rectangle;
    }
  }
}
