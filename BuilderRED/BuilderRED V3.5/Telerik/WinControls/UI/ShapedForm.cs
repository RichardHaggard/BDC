// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ShapedForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls.UI
{
  public class ShapedForm : Form
  {
    private Color borderColor = Color.Black;
    private int borderWidth = 1;
    private Color trasnparencyCompositionKey = Color.Magenta;
    private bool allowResize = true;
    private PenAlignment penAlignment = PenAlignment.Left;
    private const int designTimeDpi = 96;
    private static bool isCompositionAvailable;
    private static DllWrapper dllWrapper;
    private int oldDpi;
    private int currentDpi;
    private bool isMoving;
    private bool shouldScale;
    private Size minSizeState;
    private Size maxSizeState;
    private Stack<Dictionary<Control, AnchorStyles>> anchorsStack;
    private FormWindowState oldWindowState;
    private ElementShape shape;
    private GraphicsPath borderPath;
    private GraphicsPath outerPath;
    private string themeName;
    private bool active;
    private bool enableCompositionOnVista;
    private static ShapedForm.DwmIsCompositionEnabled dwmIsCompositionEnabled;
    private static ShapedForm.DwmEnableBlurBehindWindow dwmEnableBlurBehindWindow;
    private bool shouldSuspendSizeChange;
    private IContainer components;

    public ShapedForm()
    {
      if (ShapedForm.dllWrapper == null)
      {
        ShapedForm.dllWrapper = new DllWrapper("dwmapi.dll");
        if (ShapedForm.dllWrapper.IsDllLoaded)
        {
          ShapedForm.dwmIsCompositionEnabled = (ShapedForm.DwmIsCompositionEnabled) ShapedForm.dllWrapper.GetFunctionAsDelegate("DwmIsCompositionEnabled", typeof (ShapedForm.DwmIsCompositionEnabled));
          if (ShapedForm.dwmIsCompositionEnabled != null)
          {
            bool isEnabled = false;
            int num = ShapedForm.dwmIsCompositionEnabled(ref isEnabled);
            if (isEnabled)
            {
              ShapedForm.dwmEnableBlurBehindWindow = (ShapedForm.DwmEnableBlurBehindWindow) ShapedForm.dllWrapper.GetFunctionAsDelegate("DwmEnableBlurBehindWindow", typeof (ShapedForm.DwmEnableBlurBehindWindow));
              ShapedForm.isCompositionAvailable = true;
            }
          }
        }
      }
      this.InitializeComponent();
    }

    public PenAlignment BorderAlignment
    {
      get
      {
        return this.penAlignment;
      }
      set
      {
        this.penAlignment = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Allow form's resize")]
    [Browsable(true)]
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

    [Description("Gets or sets the form's border color")]
    [Category("Appearance")]
    [Browsable(true)]
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

    [Category("Appearance")]
    [Description("Gets or sets the form's border width")]
    [DefaultValue(1)]
    [Browsable(true)]
    public int BorderWidth
    {
      get
      {
        return this.borderWidth;
      }
      set
      {
        this.borderWidth = value;
      }
    }

    [Description("Represents the shape of the form. Setting shape affects also border form painting. Shape is considered when painting, clipping and hit-testing an element.")]
    [DefaultValue(null)]
    [Editor(typeof (ElementShapeEditor), typeof (UITypeEditor))]
    [Browsable(true)]
    [Category("Appearance")]
    public ElementShape Shape
    {
      get
      {
        return this.shape;
      }
      set
      {
        if (this.shape != null)
          this.shape.Dispose();
        this.shape = value;
        this.ApplyShape();
      }
    }

    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("StyleSheet")]
    [Description("Gets or sets theme name.")]
    [DefaultValue("")]
    public string ThemeName
    {
      get
      {
        return this.themeName;
      }
      set
      {
        this.themeName = value;
        this.ApplyTheme(this.themeName);
      }
    }

    [DefaultValue(false)]
    [Description("Enables or disables transparent background on Vista.")]
    [Browsable(true)]
    [Category("Appearance")]
    public bool EnableCompositionOnVista
    {
      get
      {
        return this.enableCompositionOnVista;
      }
      set
      {
        this.enableCompositionOnVista = value;
      }
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style |= 131072;
        return createParams;
      }
    }

    public new FormBorderStyle FormBorderStyle
    {
      get
      {
        return base.FormBorderStyle;
      }
      set
      {
        base.FormBorderStyle = value;
        this.ClientSize = this.ClientSize;
        this.SetClientSizeCore(this.ClientSize.Width, this.ClientSize.Height);
      }
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.shouldSuspendSizeChange && this.DesignMode && this.FormBorderStyle != FormBorderStyle.None)
      {
        width = Math.Min(this.ClientSize.Width, width);
        height = Math.Min(this.ClientSize.Height, height);
      }
      base.SetBoundsCore(x, y, width, height, specified);
    }

    protected override void SetClientSizeCore(int x, int y)
    {
      this.shouldSuspendSizeChange = true;
      base.SetClientSizeCore(x, y);
      this.shouldSuspendSizeChange = false;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!this.DesignMode && this.enableCompositionOnVista && ShapedForm.isCompositionAvailable)
      {
        Telerik.WinControls.NativeMethods.SetLayeredWindowAttributes(new HandleRef((object) this, this.Handle), 100, (byte) 0, 1);
        ShapedForm.dwmEnableBlurBehindWindow(this.Handle, ref new Telerik.WinControls.NativeMethods.DWM_BLURBEHIND()
        {
          dwFlags = 5U,
          fEnable = true,
          fTransitionOnMaximized = false
        });
        ThemeResolutionService.ApplicationThemeChanged += new ThemeChangedHandler(this.ApplicationThemeChanged);
      }
      if (!TelerikHelper.IsWindows8OrLower && !TelerikHelper.IsWindows10CreatorsUpdateOrHigher)
        return;
      SizeF monitorDpi = Telerik.WinControls.NativeMethods.GetMonitorDpi(Screen.FromControl((Control) this), Telerik.WinControls.NativeMethods.DpiType.Effective);
      this.oldDpi = this.currentDpi;
      this.currentDpi = (int) Math.Round((double) monitorDpi.Width * 96.0, MidpointRounding.AwayFromZero);
      this.HandleDpiChanged();
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      this.PaintFrame(e.Graphics);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.WindowState != FormWindowState.Minimized)
        this.oldWindowState = this.WindowState;
      this.ApplyShape();
      if (!this.Visible)
        return;
      using (Graphics graphics = this.CreateGraphics())
        this.PaintFrame(graphics);
    }

    protected override void WndProc(ref Message m)
    {
      if (this.shape != null || this.IsMdiContainer)
      {
        switch (m.Msg)
        {
          case 131:
            m.Result = IntPtr.Zero;
            return;
          case 133:
            m.Result = IntPtr.Zero;
            return;
          case 134:
            this.active = Convert.ToBoolean(m.WParam.ToInt32());
            this.Invalidate();
            m.Result = (IntPtr) 1;
            return;
          case 174:
          case 175:
            m.Result = IntPtr.Zero;
            return;
        }
      }
      if (m.Msg == 736)
      {
        if (!RadControl.EnableDpiScaling)
          return;
        this.oldDpi = this.currentDpi;
        this.currentDpi = (int) (short) (int) m.WParam;
        if (this.oldDpi != this.currentDpi)
        {
          if (this.isMoving)
            this.shouldScale = true;
          else
            this.HandleDpiChanged();
        }
      }
      switch (m.Msg)
      {
        case 36:
          Telerik.WinControls.NativeMethods.MINMAXINFO structure = (Telerik.WinControls.NativeMethods.MINMAXINFO) Marshal.PtrToStructure(m.LParam, typeof (Telerik.WinControls.NativeMethods.MINMAXINFO));
          if (this.Parent == null)
          {
            Screen screen = Screen.FromControl((Control) this);
            Rectangle workingArea = screen.WorkingArea;
            Rectangle bounds = screen.Bounds;
            structure.ptMaxSize.x = workingArea.Width;
            structure.ptMaxSize.y = workingArea.Height;
            structure.ptMaxPosition.x = workingArea.X - bounds.X;
            structure.ptMaxPosition.y = workingArea.Y - bounds.Y;
            Marshal.StructureToPtr((object) structure, m.LParam, false);
          }
          base.WndProc(ref m);
          return;
        case 71:
          if (this.IsMdiContainer)
          {
            this.OnLayout(new LayoutEventArgs((Control) this, "Bounds"));
            break;
          }
          break;
        case 132:
          if (this.FormBorderStyle == FormBorderStyle.None && this.AllowResize)
          {
            Point client = this.PointToClient(new Point(m.LParam.ToInt32()));
            m.Result = (IntPtr) this.GetHitTest(client);
            return;
          }
          break;
        case 787:
          this.WmTaskBarMenu();
          break;
      }
      base.WndProc(ref m);
    }

    protected virtual void ApplicationThemeChanged(object sender, ThemeChangedEventArgs args)
    {
      if (args.ThemeName == null)
        return;
      this.ThemeName = args.ThemeName;
    }

    private void WmTaskBarMenu()
    {
      if (this.FormBorderStyle != FormBorderStyle.None)
        return;
      ContextMenu contextMenu = new ContextMenu();
      MenuItem menuItem1 = new MenuItem("Restore");
      menuItem1.Click += new EventHandler(this.item_RestoreClick);
      contextMenu.MenuItems.Add(menuItem1);
      contextMenu.MenuItems.Add(new MenuItem("Move")
      {
        Enabled = false
      });
      contextMenu.MenuItems.Add(new MenuItem("Size")
      {
        Enabled = false
      });
      MenuItem menuItem2 = new MenuItem("Minimize");
      menuItem2.Click += new EventHandler(this.item_MinimizeClick);
      menuItem2.Enabled = this.WindowState != FormWindowState.Minimized && this.MinimizeBox;
      contextMenu.MenuItems.Add(menuItem2);
      MenuItem menuItem3 = new MenuItem("Maximize");
      menuItem3.Click += new EventHandler(this.item_MaximizeClick);
      menuItem3.Enabled = this.WindowState != FormWindowState.Maximized && this.MaximizeBox;
      contextMenu.MenuItems.Add(menuItem3);
      contextMenu.MenuItems.Add(new MenuItem("-"));
      MenuItem menuItem4 = new MenuItem("Close    Alt+F4");
      menuItem4.Click += new EventHandler(this.item_CloseClick);
      contextMenu.MenuItems.Add(menuItem4);
      contextMenu.Show((Control) this, this.PointToClient(Control.MousePosition));
    }

    protected override void OnResizeBegin(EventArgs e)
    {
      base.OnResizeBegin(e);
      this.isMoving = true;
    }

    protected override void OnResizeEnd(EventArgs e)
    {
      base.OnResizeEnd(e);
      this.isMoving = false;
      if (!this.shouldScale || !RadControl.EnableDpiScaling)
        return;
      this.shouldScale = false;
      this.HandleDpiChanged();
    }

    protected override void OnMove(EventArgs e)
    {
      base.OnMove(e);
      if (!this.shouldScale || !this.CanPerformScaling() || !RadControl.EnableDpiScaling)
        return;
      this.shouldScale = false;
      this.HandleDpiChanged();
    }

    private void item_RestoreClick(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Maximized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = this.oldWindowState;
    }

    private void item_MinimizeClick(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void item_MaximizeClick(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Maximized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = FormWindowState.Maximized;
    }

    private void item_CloseClick(object sender, EventArgs e)
    {
      this.Close();
    }

    protected virtual void PaintFrame(Graphics g)
    {
      if (!this.DesignMode && this.enableCompositionOnVista && ShapedForm.isCompositionAvailable)
      {
        g.Clear(Color.Black);
        if (this.FormBorderStyle != FormBorderStyle.None)
          return;
      }
      else if (this.BackgroundImage != null)
      {
        base.OnPaintBackground(new PaintEventArgs(g, this.ClientRectangle));
      }
      else
      {
        try
        {
          g.Clear(this.BackColor);
        }
        catch (Exception ex)
        {
        }
      }
      using (Pen pen = new Pen(this.borderColor, (float) this.BorderWidth))
      {
        pen.Alignment = this.penAlignment;
        SmoothingMode smoothingMode = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        if (this.borderPath != null)
          g.DrawPath(pen, this.borderPath);
        else
          g.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        g.SmoothingMode = smoothingMode;
      }
    }

    protected virtual int GetHitTest(Point point)
    {
      int num1 = 3;
      int num2 = 24;
      if (this.WindowState == FormWindowState.Maximized)
        num1 = -1;
      if (point.X <= num1 * 2 && point.Y <= num1 * 2)
        return 13;
      if (point.X >= this.Bounds.Width - num1 * 2 && point.Y <= num1 * 2)
        return 14;
      if (point.X <= num1 * 2 && point.Y >= this.Bounds.Height - num1 * 2)
        return 16;
      if (point.X >= this.Bounds.Width - num1 * 2 && point.Y >= this.Bounds.Height - num1 * 2)
        return 17;
      if (point.X <= num1)
        return 10;
      if (point.X >= this.Bounds.Width - num1)
        return 11;
      if (point.Y <= num1)
        return 12;
      if (point.Y >= this.Bounds.Height - num1)
        return 15;
      return point.Y <= num2 ? 2 : 1;
    }

    private bool CanPerformScaling()
    {
      return Screen.FromControl((Control) this).Bounds.Contains(this.Bounds);
    }

    private void ApplyTheme(string themeName)
    {
      switch (themeName)
      {
        case "ControlDefault":
          this.BorderWidth = 1;
          this.BackColor = Color.FromArgb(191, 219, 254);
          this.BorderColor = Color.FromArgb(59, 90, 130);
          break;
        case "Office2007Black":
        case "Office2010Black":
          this.BorderWidth = 1;
          this.BackColor = Color.FromArgb(83, 83, 83);
          this.BorderColor = Color.FromArgb(47, 47, 47);
          break;
        case "Office2007Silver":
        case "Office2010Silver":
          this.BorderWidth = 1;
          this.BackColor = Color.FromArgb(208, 212, 221);
          this.BorderColor = Color.FromArgb(152, 152, 152);
          break;
        case "Telerik":
        case "TelerikMetro":
          this.BorderWidth = 1;
          this.BackColor = Color.FromArgb(254, 254, 254);
          this.BorderColor = Color.FromArgb(152, 152, 152);
          break;
      }
      Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, this.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, 567);
      Telerik.WinControls.NativeMethods.RedrawWindow(new HandleRef((object) null, this.Handle), IntPtr.Zero, new HandleRef((object) null, IntPtr.Zero), 1281);
    }

    private void ApplyShape()
    {
      if (this.shape == null)
        return;
      if (this.Region != null)
        this.Region.Dispose();
      if (this.borderPath != null)
        this.borderPath.Dispose();
      if (this.outerPath != null)
        this.outerPath.Dispose();
      if ((object) this.shape.GetType() == (object) typeof (RoundRectShape))
      {
        this.borderPath = this.shape.CreatePath(new RectangleF(0.0f, 0.0f, (float) this.Bounds.Width - 1f, (float) this.Bounds.Height - 1f));
        this.outerPath = this.shape.CreatePath(new RectangleF(0.0f, 0.0f, (float) this.Bounds.Width, (float) this.Bounds.Height));
        int radius = 2 * ((RoundRectShape) this.shape).Radius;
        this.Region = Telerik.WinControls.NativeMethods.CreateRoundRectRgn(new Rectangle(Point.Empty, this.Bounds.Size), radius);
      }
      else
      {
        this.borderPath = this.shape.CreatePath(new RectangleF(0.0f, 0.0f, (float) this.Bounds.Width - 1f, (float) this.Bounds.Height - 1f));
        this.outerPath = this.shape.CreatePath(new RectangleF(0.0f, 0.0f, (float) this.Bounds.Width, (float) (this.Bounds.Height + this.borderWidth)));
        this.Region = new Region(this.outerPath);
      }
    }

    public static short LOWORD(int number)
    {
      return (short) number;
    }

    private void HandleDpiChanged()
    {
      float num = 1f;
      if (this.oldDpi != 0)
        num = (float) this.currentDpi / (float) this.oldDpi;
      else if (this.oldDpi == 0)
        num = (float) this.currentDpi / 96f;
      if ((double) num == 1.0)
        return;
      this.maxSizeState = this.MaximumSize;
      this.minSizeState = this.MinimumSize;
      this.MinimumSize = Size.Empty;
      this.MaximumSize = Size.Empty;
      this.SaveAnchorStates();
      this.Scale(new SizeF(num, num));
      this.RestoreAnchorStates();
      this.MinimumSize = TelerikDpiHelper.ScaleSize(this.minSizeState, new SizeF(num, num));
      this.MaximumSize = TelerikDpiHelper.ScaleSize(this.maxSizeState, new SizeF(num, num));
    }

    private void SaveAnchorStates()
    {
      if (this.anchorsStack == null)
        this.anchorsStack = new Stack<Dictionary<Control, AnchorStyles>>();
      Dictionary<Control, AnchorStyles> dictionary = new Dictionary<Control, AnchorStyles>();
      Queue<Control> controlQueue = new Queue<Control>();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        controlQueue.Enqueue(control);
      while (controlQueue.Count > 0)
      {
        Control key = controlQueue.Dequeue();
        if (key.Dock == DockStyle.None && key.Anchor != AnchorStyles.None)
        {
          dictionary.Add(key, key.Anchor);
          key.Anchor = AnchorStyles.None;
        }
        foreach (Control control in (ArrangedElementCollection) key.Controls)
          controlQueue.Enqueue(control);
      }
      this.anchorsStack.Push(dictionary);
    }

    private void RestoreAnchorStates()
    {
      Dictionary<Control, AnchorStyles> dictionary = this.anchorsStack.Pop();
      Queue<Control> controlQueue = new Queue<Control>();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        controlQueue.Enqueue(control);
      while (controlQueue.Count > 0)
      {
        Control key = controlQueue.Dequeue();
        if (dictionary.ContainsKey(key))
        {
          key.Anchor = dictionary[key];
          dictionary.Remove(key);
        }
        foreach (Control control in (ArrangedElementCollection) key.Controls)
          controlQueue.Enqueue(control);
      }
      dictionary.Clear();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
      ThemeResolutionService.ApplicationThemeChanged -= new ThemeChangedHandler(this.ApplicationThemeChanged);
      if (this.Region != null)
        this.Region.Dispose();
      if (this.borderPath != null)
        this.borderPath.Dispose();
      if (this.outerPath == null)
        return;
      this.outerPath.Dispose();
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (ShapedForm);
      this.Text = nameof (ShapedForm);
      this.ResumeLayout(false);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int DwmIsCompositionEnabled(ref bool isEnabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate void DwmEnableBlurBehindWindow(
      IntPtr hWnd,
      ref Telerik.WinControls.NativeMethods.DWM_BLURBEHIND pBlurBehind);
  }
}
