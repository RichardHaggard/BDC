// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadLayeredWindow
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class RadLayeredWindow : Control
  {
    private const int AC_SRC_OVER = 0;
    private const int AC_SRC_ALPHA = 1;
    private const int ULW_COLORKEY = 1;
    private const int ULW_ALPHA = 2;
    private const int ULW_OPAQUE = 4;
    private Bitmap content;
    private int suspendUpdateCount;
    private Size desiredSize;
    private bool updated;
    private bool topMost;
    private bool hitTestable;
    private float alpha;
    private bool recreateHandleOnSizeChanged;

    public RadLayeredWindow()
    {
      if (!OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
        throw new InvalidOperationException("Current OS does not support Layered Windows");
      this.updated = false;
      this.topMost = false;
      this.hitTestable = false;
      this.desiredSize = Size.Empty;
      this.alpha = 1f;
      this.recreateHandleOnSizeChanged = true;
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = new CreateParams();
        createParams.Parent = IntPtr.Zero;
        createParams.Style = int.MinValue;
        createParams.ExStyle = 524416;
        if (this.topMost)
          createParams.ExStyle |= 8;
        if (!this.hitTestable)
          createParams.ExStyle |= 32;
        return createParams;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 15:
          return;
        case 20:
          return;
        case 33:
          m.Result = (IntPtr) 3;
          return;
        case 132:
          if (!this.hitTestable)
          {
            m.Result = (IntPtr) -1;
            return;
          }
          break;
      }
      base.WndProc(ref m);
    }

    public override Image BackgroundImage
    {
      get
      {
        return base.BackgroundImage;
      }
      set
      {
        base.BackgroundImage = value;
        this.updated = false;
      }
    }

    public void BringToFront(bool activate)
    {
      this.UpdateZOrder(NativeMethods.HWND_TOP, activate);
    }

    public void SendToBack(bool activate)
    {
      this.UpdateZOrder(NativeMethods.HWND_BOTTOM, activate);
    }

    private void UpdateZOrder(HandleRef pos, bool activate)
    {
      int flags = 1539;
      if (!activate)
        flags |= 16;
      NativeMethods.SetWindowPos(new HandleRef((object) this, this.Handle), pos, 0, 0, 0, 0, flags);
    }

    public void SuspendUpdates()
    {
      ++this.suspendUpdateCount;
    }

    public void ResumeUpdates()
    {
      this.ResumeUpdates(true);
    }

    public void ResumeUpdates(bool update)
    {
      --this.suspendUpdateCount;
      this.suspendUpdateCount = Math.Max(0, this.suspendUpdateCount);
      if (this.suspendUpdateCount != 0 || !update)
        return;
      this.ShowWindow(this.Location);
    }

    public virtual void ShowWindow(Point screenLocation)
    {
      if (this.suspendUpdateCount > 0)
        return;
      bool isHandleCreated = this.IsHandleCreated;
      if (!isHandleCreated)
        NativeMethods.ShowWindow(this.Handle, 4);
      this.Location = screenLocation;
      if (!this.updated)
      {
        Size displaySize = this.DisplaySize;
        bool flag = this.Size != displaySize;
        this.Size = displaySize;
        if (isHandleCreated && flag && this.recreateHandleOnSizeChanged)
        {
          this.RecreateHandle();
          NativeMethods.ShowWindow(this.Handle, 4);
          this.Size = displaySize;
          this.Location = screenLocation;
        }
        this.Size = displaySize;
        if (displaySize != Size.Empty)
          this.UpdateWindow();
      }
      if (this.Visible)
        return;
      NativeMethods.ShowWindow(this.Handle, 4);
    }

    protected virtual void PaintWindow(Graphics g, Bitmap graphicsBitmap)
    {
      Image backgroundImage = this.BackgroundImage;
      if (backgroundImage == null)
        return;
      g.DrawImage(backgroundImage, 0, 0, this.Width, this.Height);
    }

    protected Bitmap Content
    {
      get
      {
        return this.content;
      }
    }

    protected void UpdateWindow()
    {
      if (this.suspendUpdateCount > 0)
        return;
      if (this.content != null)
        this.content.Dispose();
      this.content = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
      Graphics g = Graphics.FromImage((Image) this.content);
      this.PaintWindow(g, this.content);
      this.NativeUpdateWindow(this.content);
      g.Dispose();
      this.updated = true;
    }

    private void NativeUpdateWindow(Bitmap bmp)
    {
      Point location = this.Location;
      lock (MeasurementGraphics.SyncObject)
      {
        IntPtr dc = NativeMethods.GetDC(new HandleRef((object) null, IntPtr.Zero));
        IntPtr compatibleDc = NativeMethods.CreateCompatibleDC(new HandleRef((object) null, dc));
        IntPtr hbitmap = bmp.GetHbitmap(Color.FromArgb(0));
        IntPtr handle = NativeMethods.SelectObject(new HandleRef((object) null, compatibleDc), new HandleRef((object) null, hbitmap));
        NativeMethods.SIZESTRUCT psize;
        psize.cx = this.Width;
        psize.cy = this.Height;
        NativeMethods.POINTSTRUCT pptDst;
        pptDst.x = this.Left;
        pptDst.y = this.Top;
        NativeMethods.POINTSTRUCT pprSrc;
        pprSrc.x = 0;
        pprSrc.y = 0;
        NativeMethods.BLENDFUNCTION pblend;
        pblend.BlendOp = (byte) 0;
        pblend.BlendFlags = (byte) 0;
        pblend.SourceConstantAlpha = (byte) ((double) this.alpha * (double) byte.MaxValue);
        pblend.AlphaFormat = (byte) 1;
        NativeMethods.UpdateLayeredWindow(this.Handle, dc, ref pptDst, ref psize, compatibleDc, ref pprSrc, 0, ref pblend, 2);
        NativeMethods.SelectObject(new HandleRef((object) null, compatibleDc), new HandleRef((object) null, handle));
        NativeMethods.ReleaseDC(new HandleRef((object) null, IntPtr.Zero), new HandleRef((object) null, dc));
        NativeMethods.DeleteObject(new HandleRef((object) null, hbitmap));
        NativeMethods.DeleteDC(new HandleRef((object) null, compatibleDc));
      }
    }

    public bool RecreateHandleOnSizeChanged
    {
      get
      {
        return this.recreateHandleOnSizeChanged;
      }
      set
      {
        if (this.recreateHandleOnSizeChanged == value)
          return;
        this.recreateHandleOnSizeChanged = value;
      }
    }

    public bool Updated
    {
      get
      {
        return this.updated;
      }
      protected set
      {
        this.updated = value;
      }
    }

    public float Alpha
    {
      get
      {
        return this.alpha;
      }
      set
      {
        value = Math.Min(1f, value);
        value = Math.Max(0.0f, value);
        if ((double) this.alpha == (double) value)
          return;
        this.alpha = value;
        if (!this.updated)
          return;
        this.updated = false;
        this.UpdateWindow();
      }
    }

    public virtual Size DisplaySize
    {
      get
      {
        if (this.desiredSize != Size.Empty)
          return this.desiredSize;
        if (this.BackgroundImage != null)
          return this.BackgroundImage.Size;
        return Size.Empty;
      }
    }

    public Size DesiredSize
    {
      get
      {
        return this.desiredSize;
      }
      set
      {
        value.Width = Math.Max(0, value.Width);
        value.Height = Math.Max(0, value.Height);
        if (value == this.desiredSize)
          return;
        this.desiredSize = value;
        if (!this.updated)
          return;
        this.updated = false;
        this.ShowWindow(this.Location);
      }
    }

    public bool TopMost
    {
      get
      {
        return this.topMost;
      }
      set
      {
        if (this.topMost == value)
          return;
        this.topMost = value;
        if (!this.IsHandleCreated)
          return;
        this.UpdateStyles();
      }
    }

    public bool HitTestable
    {
      get
      {
        return this.hitTestable;
      }
      set
      {
        if (this.hitTestable == value)
          return;
        this.hitTestable = value;
        if (!this.IsHandleCreated)
          return;
        this.UpdateStyles();
      }
    }
  }
}
