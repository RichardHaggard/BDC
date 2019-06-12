// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ThemedFormBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public abstract class ThemedFormBehavior : FormControlBehavior
  {
    private const int HIT_TEST_RADIUS = 8;
    private MdiClient mdiClient;
    private System.Windows.Forms.Form activeMdiChild;
    private CreateParams currentFormParams;
    private int? currentFormState;

    public ThemedFormBehavior()
    {
    }

    public ThemedFormBehavior(IComponentTreeHandler treeHandler)
      : base(treeHandler)
    {
    }

    public ThemedFormBehavior(IComponentTreeHandler treeHandler, bool shouldCreateChildren)
      : base(treeHandler, shouldCreateChildren)
    {
    }

    protected virtual bool IsMaximized
    {
      get
      {
        if (this.Form.IsHandleCreated)
          return Telerik.WinControls.NativeMethods.IsZoomed(new HandleRef((object) this, this.Form.Handle));
        return this.Form.WindowState == FormWindowState.Maximized;
      }
    }

    protected virtual bool IsMinimized
    {
      get
      {
        if (this.Form.IsHandleCreated)
          return Telerik.WinControls.NativeMethods.IsIconic(new HandleRef((object) this, this.Form.Handle));
        return this.Form.WindowState == FormWindowState.Minimized;
      }
    }

    protected virtual bool IsNormal
    {
      get
      {
        return this.Form.WindowState == FormWindowState.Normal;
      }
    }

    protected int CurrentFormState
    {
      get
      {
        return this.Form.WindowState == FormWindowState.Maximized ? 2 : (this.Form.WindowState == FormWindowState.Normal ? 0 : 1);
      }
    }

    protected bool IsMdiChildMaximized
    {
      get
      {
        if (this.Form.IsMdiContainer && this.mdiClient != null)
        {
          if (this.Form.ActiveMdiChild != null && this.Form.ActiveMdiChild.IsHandleCreated)
            return Telerik.WinControls.NativeMethods.IsZoomed(new HandleRef((object) this.Form.ActiveMdiChild, this.Form.ActiveMdiChild.Handle));
          foreach (System.Windows.Forms.Form control in (ArrangedElementCollection) this.mdiClient.Controls)
          {
            if (control.Disposing || control.IsDisposed)
              return false;
            if (control.IsHandleCreated && Telerik.WinControls.NativeMethods.IsZoomed(new HandleRef((object) control, control.Handle)))
              return true;
          }
        }
        return false;
      }
    }

    protected MdiClient FormMdiClient
    {
      get
      {
        if (this.mdiClient == null && this.Form.IsMdiContainer)
        {
          for (int index = 0; index < this.Form.Controls.Count; ++index)
          {
            if (this.Form.Controls[index] is MdiClient)
              return this.mdiClient = this.Form.Controls[index] as MdiClient;
          }
        }
        return this.mdiClient;
      }
    }

    protected bool IsMenuInForm
    {
      get
      {
        return this.Form.MainMenuStrip != null && !(this.Form.MainMenuStrip is RadRibbonFormMainMenuStrip) || this.MainMenuInForm != null;
      }
    }

    protected RadMenu MainMenuInForm
    {
      get
      {
        foreach (Control control in (ArrangedElementCollection) this.Form.Controls)
        {
          if (control is RadMenu)
          {
            RadMenu radMenu = control as RadMenu;
            if (radMenu.IsMainMenu)
              return radMenu;
          }
        }
        return (RadMenu) null;
      }
    }

    protected System.Windows.Forms.Form MaximizedMDIChild
    {
      get
      {
        System.Windows.Forms.Form activeMdiChild = this.Form.ActiveMdiChild;
        if (activeMdiChild != null && activeMdiChild.WindowState == FormWindowState.Maximized)
          return activeMdiChild;
        return (System.Windows.Forms.Form) null;
      }
    }

    public virtual Rectangle TopResizeFrame
    {
      get
      {
        return new Rectangle(8, 0, this.Form.Width - 16, 8);
      }
    }

    public virtual Rectangle TopLeftResizeFrame
    {
      get
      {
        return new Rectangle(0, 0, 8, 8);
      }
    }

    public virtual Rectangle LeftResizeFrame
    {
      get
      {
        return new Rectangle(0, 8, 8, this.Form.Height - 16);
      }
    }

    public virtual Rectangle BottomLeftResizeFrame
    {
      get
      {
        return new Rectangle(0, this.Form.Height - 8, 8, 8);
      }
    }

    public virtual Rectangle BottomResizeFrame
    {
      get
      {
        return new Rectangle(8, this.Form.Height - 8, this.Form.Width - 16, 8);
      }
    }

    public virtual Rectangle BottomRightResizeFrame
    {
      get
      {
        return new Rectangle(this.Form.Width - 8, this.Form.Height - 8, 8, 8);
      }
    }

    public virtual Rectangle RightResizeFrame
    {
      get
      {
        return new Rectangle(this.Form.Width - 8, 8, 8, this.Form.Height - 16);
      }
    }

    public virtual Rectangle TopRightResizeFrame
    {
      get
      {
        return new Rectangle(this.Form.Width - 8, 0, 8, 8);
      }
    }

    public virtual Rectangle CaptionFrame
    {
      get
      {
        return new Rectangle(0, 0, this.Form.Width, this.ClientMargin.Top);
      }
    }

    public virtual Rectangle LeftBorderFrame
    {
      get
      {
        return new Rectangle(0, this.ClientMargin.Top, this.ClientMargin.Left, this.Form.Height - (this.ClientMargin.Top + this.ClientMargin.Bottom));
      }
    }

    public virtual Rectangle BottomBorderFrame
    {
      get
      {
        return new Rectangle(0, this.Form.Height - this.ClientMargin.Bottom, this.Form.Width, this.ClientMargin.Bottom);
      }
    }

    public virtual Rectangle RightBorderFrame
    {
      get
      {
        return new Rectangle(this.Form.Width - this.ClientMargin.Right, this.ClientMargin.Top, this.ClientMargin.Right, this.Form.Height - (this.ClientMargin.Top + this.ClientMargin.Bottom));
      }
    }

    public virtual Rectangle ClientFrame
    {
      get
      {
        return new Rectangle(this.ClientMargin.Left, this.ClientMargin.Top, this.Form.ClientRectangle.Width, this.Form.ClientRectangle.Height);
      }
    }

    public abstract Rectangle MenuBounds { get; }

    public abstract Rectangle SystemButtonsBounds { get; }

    public abstract Rectangle IconBounds { get; }

    public abstract Rectangle CaptionTextBounds { get; }

    public abstract bool AllowTheming { get; set; }

    public virtual CreateParams CurrentFormParams
    {
      get
      {
        return this.currentFormParams;
      }
    }

    public RadFormControlBase Form
    {
      get
      {
        return this.targetHandler as RadFormControlBase;
      }
      set
      {
        if (value == null)
          return;
        if (this.targetHandler != null)
          throw new InvalidOperationException("A Form is already associated.");
        this.targetHandler = (IComponentTreeHandler) value;
        this.OnFormAssociated();
      }
    }

    protected virtual Point GetMappedWindowPoint(Point screenPoint)
    {
      Telerik.WinControls.NativeMethods.POINT pt = new Telerik.WinControls.NativeMethods.POINT();
      pt.x = screenPoint.X;
      pt.y = screenPoint.Y;
      if (this.Form.IsHandleCreated)
        Telerik.WinControls.NativeMethods.MapWindowPoints(new HandleRef((object) this, IntPtr.Zero), new HandleRef((object) this, this.Form.Handle), pt, 1);
      return new Point(pt.x + this.ClientMargin.Left, pt.y + this.ClientMargin.Top);
    }

    protected int GetMaximumFormHeightAccordingToCurrentScreen()
    {
      int height = Screen.PrimaryScreen.WorkingArea.Height;
      if (this.Form.FormBorderStyle == FormBorderStyle.None)
        height = Screen.PrimaryScreen.Bounds.Height;
      Screen screen = Screen.FromControl((Control) this.Form);
      if (screen != null)
      {
        height = screen.WorkingArea.Height;
        if (this.Form.FormBorderStyle == FormBorderStyle.None)
          height = screen.Bounds.Height;
      }
      return height + SystemInformation.FrameBorderSize.Height * 2;
    }

    public override CreateParams CreateParams(CreateParams parameters)
    {
      CreateParams createParams = base.CreateParams(parameters);
      createParams.Style |= 13369344;
      if (this.Form.MaximizeBox)
        createParams.Style |= 65536;
      else
        createParams.Style &= -65537;
      if (this.Form.MinimizeBox)
        createParams.Style |= 131072;
      else
        createParams.Style &= -131073;
      if (!this.Form.MinimizeBox && !this.Form.MaximizeBox && this.Form.HelpButton)
        createParams.ExStyle |= 1024;
      else
        createParams.ExStyle &= -1025;
      if (this.Form.ControlBox)
        createParams.Style |= 524288;
      else
        createParams.Style &= -524289;
      if (!DWMAPI.IsCompositionEnabled && this.AllowTheming || this.Form.IsDesignMode)
        createParams.Style &= -12582913;
      return this.currentFormParams = createParams;
    }

    protected override void OnFormAssociated()
    {
      base.OnFormAssociated();
      this.Form.ControlAdded += new ControlEventHandler(this.Form_ControlAdded);
      this.Form.ControlRemoved += new ControlEventHandler(this.Form_ControlRemoved);
      this.Form.MdiChildActivate += new EventHandler(this.Form_MdiChildActivate);
    }

    protected override void Dispose(bool disposing)
    {
      this.Form.ControlAdded -= new ControlEventHandler(this.Form_ControlAdded);
      this.Form.ControlRemoved -= new ControlEventHandler(this.Form_ControlRemoved);
      this.Form.MdiChildActivate -= new EventHandler(this.Form_MdiChildActivate);
      if (this.activeMdiChild != null)
        this.activeMdiChild.TextChanged -= new EventHandler(this.ActiveMDIChild_TextChanged);
      base.Dispose(disposing);
    }

    protected virtual void OnActiveMDIChildTextChanged()
    {
    }

    protected virtual void OnWindowStateChanged(int currentFormState, int newFormState)
    {
    }

    protected virtual void RefreshNC()
    {
      if (!this.Form.IsHandleCreated || this.Form.ClientSize.Height <= 0 || this.Form.ClientSize.Width <= 0)
        return;
      Telerik.WinControls.NativeMethods.RedrawWindow(new HandleRef((object) null, this.Form.Handle), IntPtr.Zero, new HandleRef((object) null, IntPtr.Zero), 1281);
    }

    protected virtual void InvalidateNC(Rectangle bounds)
    {
      if (!this.Form.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.RECT rcUpdate = new Telerik.WinControls.NativeMethods.RECT(bounds.Left - this.ClientMargin.Left, bounds.Top - this.ClientMargin.Top, bounds.Right, bounds.Bottom);
      Telerik.WinControls.NativeMethods.RedrawWindow(new HandleRef((object) this, this.Form.Handle), ref rcUpdate, new HandleRef((object) null, IntPtr.Zero), 1025);
    }

    protected virtual void OnGetMinMaxInfo(MinMaxInfo minMaxInfo)
    {
    }

    protected virtual void OnNCPaint(Graphics graphics)
    {
      this.PaintTitleBar(graphics);
      this.PaintBorders(graphics);
    }

    private void PaintTitleBar(Graphics hDCGraphics)
    {
      if (this.ClientMargin.Top <= 0 || this.Form.Width <= 0)
        return;
      Bitmap bitmap = new Bitmap(this.Form.Width, this.ClientMargin.Top);
      Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
        if (this.FormElement != null && ((double) this.FormElement.DpiScaleFactor.Width != 1.0 || (double) this.FormElement.DpiScaleFactor.Height != 1.0))
          graphics.Clear(((VisualElement) this.FormElement).BackColor);
        this.PaintElement((IGraphics) radGdiGraphics, rectangle, (VisualElement) this.Form.RootElement);
      }
      hDCGraphics.DrawImage((Image) bitmap, rectangle);
      bitmap.Dispose();
    }

    private void PaintBorders(Graphics hDCGraphics)
    {
      Rectangle rectangle1 = new Rectangle(0, this.ClientMargin.Top, this.ClientMargin.Left, this.Form.Height - this.ClientMargin.Top);
      Rectangle rectangle2 = new Rectangle(this.Form.Width - this.ClientMargin.Right, this.ClientMargin.Top, this.ClientMargin.Right, this.Form.Height - this.ClientMargin.Top);
      Rectangle rectangle3 = new Rectangle(this.ClientMargin.Left, this.Form.Height - this.ClientMargin.Bottom, this.Form.Width - (this.ClientMargin.Left + this.ClientMargin.Right), this.ClientMargin.Bottom);
      if (rectangle1.Width > 0 && rectangle1.Height > 0)
      {
        Bitmap bitmap = new Bitmap(rectangle1.Width, rectangle1.Height);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
          radGdiGraphics.TranslateTransform(-rectangle1.X, -rectangle1.Y);
          radGdiGraphics.FillRectangle(rectangle1, this.Form.RootElement.BackColor);
          this.PaintElement((IGraphics) radGdiGraphics, rectangle1, (VisualElement) this.Form.RootElement);
        }
        hDCGraphics.DrawImage((Image) bitmap, rectangle1);
        bitmap.Dispose();
      }
      if (rectangle2.Width > 0 && rectangle2.Height > 0)
      {
        Bitmap bitmap = new Bitmap(rectangle2.Width, rectangle2.Height);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
          radGdiGraphics.TranslateTransform(-rectangle2.X, -rectangle2.Y);
          radGdiGraphics.FillRectangle(rectangle2, this.Form.RootElement.BackColor);
          this.PaintElement((IGraphics) radGdiGraphics, rectangle2, (VisualElement) this.Form.RootElement);
        }
        hDCGraphics.DrawImage((Image) bitmap, rectangle2);
        bitmap.Dispose();
      }
      if (rectangle3.Width <= 0 || rectangle3.Height <= 0)
        return;
      Bitmap bitmap1 = new Bitmap(rectangle3.Width, rectangle3.Height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
      {
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
        radGdiGraphics.TranslateTransform(-rectangle3.X, -rectangle3.Y);
        radGdiGraphics.FillRectangle(rectangle3, this.Form.RootElement.BackColor);
        this.PaintElement((IGraphics) radGdiGraphics, rectangle3, (VisualElement) this.Form.RootElement);
      }
      hDCGraphics.DrawImage((Image) bitmap1, rectangle3);
      bitmap1.Dispose();
    }

    protected virtual void PaintElement(
      IGraphics graphics,
      Rectangle clipRectangle,
      VisualElement element)
    {
      if (element is RootRadElement)
        ((RootRadElement) element).Paint(graphics, clipRectangle, true);
      else
        element.Paint(graphics, clipRectangle, 0.0f, new SizeF(1f, 1f), false);
    }

    private void OnWMGetMinMaxInfo(ref Message m)
    {
      Telerik.WinControls.NativeMethods.MINMAXINFO structure = (Telerik.WinControls.NativeMethods.MINMAXINFO) Marshal.PtrToStructure(m.LParam, typeof (Telerik.WinControls.NativeMethods.MINMAXINFO));
      if (this.Form.IsMdiChild)
        structure.ptMaxTrackSize.x = Math.Max(structure.ptMaxTrackSize.x, structure.ptMaxSize.x);
      else if (!this.Form.IsMdiChild && this.IsMaximized && !DWMAPI.IsCompositionEnabled)
      {
        structure.ptMaxTrackSize.y = this.GetMaximumFormHeightAccordingToCurrentScreen();
        if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 1)
        {
          Screen screen = Screen.FromControl((Control) this.Form);
          if (screen != null)
            structure.ptMaxPosition.y = screen.WorkingArea.Y - SystemInformation.FrameBorderSize.Height;
        }
      }
      structure.ptMinTrackSize.x = SystemInformation.MinimumWindowSize.Width;
      structure.ptMinTrackSize.y = this.ClientMargin.Vertical;
      MinMaxInfo minMaxInfo = new MinMaxInfo();
      minMaxInfo.MaxPosition = new Point(structure.ptMaxPosition.x, structure.ptMaxPosition.y);
      minMaxInfo.MaxTrackSize = new Size(structure.ptMaxTrackSize.x, structure.ptMaxTrackSize.y);
      minMaxInfo.MinTrackSize = new Size(structure.ptMinTrackSize.x, structure.ptMinTrackSize.y);
      minMaxInfo.MaxSize = new Size(structure.ptMaxSize.x, structure.ptMaxSize.y);
      minMaxInfo.SizeReserved = new Size(structure.ptReserved.x, structure.ptReserved.y);
      this.OnGetMinMaxInfo(minMaxInfo);
      structure.ptMaxPosition.x = minMaxInfo.MaxPosition.X;
      structure.ptMaxPosition.y = minMaxInfo.MaxPosition.Y;
      structure.ptMaxTrackSize.x = minMaxInfo.MaxTrackSize.Width;
      structure.ptMaxTrackSize.y = minMaxInfo.MaxTrackSize.Height;
      structure.ptMinTrackSize.x = minMaxInfo.MinTrackSize.Width;
      structure.ptMinTrackSize.y = minMaxInfo.MinTrackSize.Height;
      structure.ptMaxSize.x = minMaxInfo.MaxSize.Width;
      structure.ptMaxSize.y = minMaxInfo.MaxSize.Height;
      structure.ptReserved.x = minMaxInfo.SizeReserved.Width;
      structure.ptReserved.y = minMaxInfo.SizeReserved.Height;
      Marshal.StructureToPtr((object) structure, m.LParam, true);
      this.CallBaseWndProc(ref m);
    }

    private void OnWmActivate(ref Message m)
    {
      m.Result = new IntPtr(0);
      this.CallBaseWndProc(ref m);
      this.RefreshNC();
    }

    private void OnWMNCHitTest(ref Message m)
    {
      Point screenPoint = new Point(m.LParam.ToInt32());
      IntPtr num = new IntPtr(0);
      Point mappedWindowPoint = this.GetMappedWindowPoint(screenPoint);
      if (this.CaptionFrame.Contains(mappedWindowPoint))
        num = new IntPtr(2);
      if (this.MenuBounds.Contains(mappedWindowPoint))
        num = new IntPtr(5);
      if (this.Form.FormBorderStyle != FormBorderStyle.FixedToolWindow && this.Form.FormBorderStyle != FormBorderStyle.FixedSingle && (this.Form.FormBorderStyle != FormBorderStyle.Fixed3D && this.Form.FormBorderStyle != FormBorderStyle.FixedDialog))
      {
        if (this.TopResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(12);
        if (this.TopLeftResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(13);
        if (this.LeftResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(10);
        if (this.BottomLeftResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(16);
        if (this.BottomResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(15);
        if (this.BottomRightResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(17);
        if (this.RightResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(11);
        if (this.TopRightResizeFrame.Contains(mappedWindowPoint))
          num = new IntPtr(14);
      }
      if (this.ClientFrame.Contains(mappedWindowPoint))
        num = new IntPtr(1);
      m.Result = num;
    }

    private void OnWMNCCalcSize(ref Message m)
    {
      if (m.WParam == new IntPtr(1))
      {
        Telerik.WinControls.NativeMethods.NCCALCSIZE_PARAMS nccalcsizeParams = new Telerik.WinControls.NativeMethods.NCCALCSIZE_PARAMS();
        Telerik.WinControls.NativeMethods.NCCALCSIZE_PARAMS structure = (Telerik.WinControls.NativeMethods.NCCALCSIZE_PARAMS) Marshal.PtrToStructure(m.LParam, typeof (Telerik.WinControls.NativeMethods.NCCALCSIZE_PARAMS));
        Padding clientMargin = this.ClientMargin;
        if (this.Form.IsMdiChild && this.IsMaximized && !DWMAPI.IsCompositionEnabled)
          structure.rgrc[0].top = -clientMargin.Top;
        structure.rgrc[0].top += clientMargin.Top;
        structure.rgrc[0].left += clientMargin.Left;
        structure.rgrc[0].right -= clientMargin.Right;
        structure.rgrc[0].bottom -= clientMargin.Bottom;
        Marshal.StructureToPtr((object) structure, m.LParam, true);
        m.Result = IntPtr.Zero;
      }
      else
      {
        this.CallBaseWndProc(ref m);
        Telerik.WinControls.NativeMethods.RECT rect = new Telerik.WinControls.NativeMethods.RECT();
        Telerik.WinControls.NativeMethods.RECT structure = (Telerik.WinControls.NativeMethods.RECT) Marshal.PtrToStructure(m.LParam, typeof (Telerik.WinControls.NativeMethods.RECT));
        Padding clientMargin = this.ClientMargin;
        if (this.Form.IsMdiChild && this.IsMaximized && !DWMAPI.IsCompositionEnabled)
          structure.top = -clientMargin.Top;
        structure.top += clientMargin.Top;
        structure.left += clientMargin.Left;
        structure.right -= clientMargin.Right;
        structure.bottom -= clientMargin.Bottom;
        Marshal.StructureToPtr((object) structure, m.LParam, true);
        m.Result = IntPtr.Zero;
      }
    }

    private void OnWMNCPaint(ref Message m)
    {
      if (!this.Form.IsHandleCreated)
        return;
      HandleRef hWnd = new HandleRef((object) this, this.Form.Handle);
      Telerik.WinControls.NativeMethods.RECT rect = new Telerik.WinControls.NativeMethods.RECT();
      if (!Telerik.WinControls.NativeMethods.GetWindowRect(hWnd, ref rect))
        return;
      Rectangle rectangle = new Rectangle(0, 0, rect.right - rect.left, rect.bottom - rect.top);
      if (rectangle.Width <= 0 || rectangle.Height <= 0)
        return;
      IntPtr zero = IntPtr.Zero;
      int flags = 19;
      IntPtr handle = IntPtr.Zero;
      if (m.WParam != (IntPtr) 1)
      {
        flags |= 128;
        handle = m.WParam;
      }
      HandleRef hrgnClip = new HandleRef((object) this, handle);
      IntPtr dcEx = Telerik.WinControls.NativeMethods.GetDCEx(hWnd, hrgnClip, flags);
      try
      {
        if (!(dcEx != IntPtr.Zero))
          return;
        using (Graphics graphics = Graphics.FromHdc(dcEx))
          this.OnNCPaint(graphics);
      }
      finally
      {
        Telerik.WinControls.NativeMethods.ReleaseDC(new HandleRef((object) this, m.HWnd), new HandleRef((object) null, dcEx));
      }
    }

    private void OnWMNCActivate(ref Message m)
    {
      m.LParam = new IntPtr(-1);
      if (m.WParam == IntPtr.Zero)
        m.Result = IntPtr.Zero;
      this.CallDefWndProc(ref m);
      this.RefreshNC();
    }

    public override void InvalidateElement(RadElement element, Rectangle bounds)
    {
      this.InvalidateNC(bounds);
    }

    public override bool HandleWndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 5:
          this.OnWMSize(ref m);
          return true;
        case 6:
          this.OnWmActivate(ref m);
          return true;
        case 36:
          this.OnWMGetMinMaxInfo(ref m);
          return true;
        case 131:
          this.OnWMNCCalcSize(ref m);
          return true;
        case 132:
          this.OnWMNCHitTest(ref m);
          return true;
        case 133:
          this.OnWMNCPaint(ref m);
          return true;
        case 134:
          this.OnWMNCActivate(ref m);
          return true;
        case 174:
        case 175:
          m.Result = new IntPtr(0);
          return true;
        default:
          return false;
      }
    }

    private void OnWMSize(ref Message m)
    {
      int wparam = (int) m.WParam;
      if (this.currentFormState.HasValue)
      {
        int? currentFormState = this.currentFormState;
        int num = wparam;
        if ((currentFormState.GetValueOrDefault() != num ? 1 : (!currentFormState.HasValue ? 1 : 0)) != 0)
        {
          this.OnWindowStateChanged(this.currentFormState.Value, wparam);
          this.currentFormState = new int?(wparam);
        }
      }
      else
        this.currentFormState = new int?(wparam);
      this.CallBaseWndProc(ref m);
    }

    private void Form_MdiChildActivate(object sender, EventArgs e)
    {
      if (this.activeMdiChild != null)
      {
        this.activeMdiChild.TextChanged -= new EventHandler(this.ActiveMDIChild_TextChanged);
        this.activeMdiChild = (System.Windows.Forms.Form) null;
      }
      this.activeMdiChild = this.Form.ActiveMdiChild;
      if (this.activeMdiChild == null)
        return;
      this.activeMdiChild.TextChanged += new EventHandler(this.ActiveMDIChild_TextChanged);
    }

    private void ActiveMDIChild_TextChanged(object sender, EventArgs e)
    {
      this.OnActiveMDIChildTextChanged();
    }

    private void Form_ControlRemoved(object sender, ControlEventArgs e)
    {
      if (!(e.Control is MdiClient))
        return;
      this.mdiClient = (MdiClient) null;
    }

    private void Form_ControlAdded(object sender, ControlEventArgs e)
    {
      if (!(e.Control is MdiClient))
        return;
      this.Form.Update();
      this.mdiClient = e.Control as MdiClient;
    }
  }
}
