// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplashForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class SplashForm : Form
  {
    private byte alpha = 30;
    private byte step = 5;
    private byte sleepTime = 6;
    private IntPtr handle;
    private IContainer components;

    public SplashForm()
    {
      this.InitializeComponent();
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 524288;
        createParams.ExStyle &= -262145;
        return createParams;
      }
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
        if (value == null)
          return;
        this.Size = this.BackgroundImage.Size;
      }
    }

    public void AnimatedHide()
    {
      this.UpdateLayeredWindow(this.alpha);
      new Thread(new ThreadStart(this.AnimateFuncHide)).Start();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.SetupLayeredWindow();
    }

    protected virtual void SetupLayeredWindow()
    {
      if (this.BackgroundImage == null)
        throw new NullReferenceException("SplashForm.BackgroundImage can not be null");
      if (!(this.BackgroundImage is Bitmap) || this.BackgroundImage.PixelFormat != PixelFormat.Format32bppArgb && this.BackgroundImage.PixelFormat != PixelFormat.Format24bppRgb)
        throw new Exception("SplashForm does not support this image format");
      this.handle = this.Handle;
      this.UpdateLayeredWindow(this.alpha);
      new Thread(new ThreadStart(this.AnimateFuncShow)).Start();
    }

    protected virtual void UpdateLayeredWindow(byte alpha)
    {
      Bitmap backgroundImage = (Bitmap) this.BackgroundImage;
      IntPtr dc = Telerik.WinControls.NativeMethods.GetDC(new HandleRef((object) this, IntPtr.Zero));
      IntPtr compatibleDc = Telerik.WinControls.NativeMethods.CreateCompatibleDC(new HandleRef((object) this, dc));
      IntPtr hbitmap = backgroundImage.GetHbitmap(Color.FromArgb(0));
      IntPtr handle = Telerik.WinControls.NativeMethods.SelectObject(new HandleRef((object) this, compatibleDc), new HandleRef((object) this, hbitmap));
      Telerik.WinControls.NativeMethods.SIZESTRUCT psize = new Telerik.WinControls.NativeMethods.SIZESTRUCT(backgroundImage.Width, backgroundImage.Height);
      Telerik.WinControls.NativeMethods.POINTSTRUCT pprSrc = new Telerik.WinControls.NativeMethods.POINTSTRUCT(0, 0);
      Telerik.WinControls.NativeMethods.POINTSTRUCT pptDst = new Telerik.WinControls.NativeMethods.POINTSTRUCT(this.Left, this.Top);
      Telerik.WinControls.NativeMethods.UpdateLayeredWindow(this.handle, dc, ref pptDst, ref psize, compatibleDc, ref pprSrc, 0, ref new Telerik.WinControls.NativeMethods.BLENDFUNCTION()
      {
        BlendOp = (byte) 0,
        BlendFlags = (byte) 0,
        SourceConstantAlpha = alpha,
        AlphaFormat = (byte) 1
      }, 2);
      Telerik.WinControls.NativeMethods.ReleaseDC(new HandleRef((object) this, IntPtr.Zero), new HandleRef((object) this, dc));
      if (hbitmap != IntPtr.Zero)
      {
        Telerik.WinControls.NativeMethods.SelectObject(new HandleRef((object) this, compatibleDc), new HandleRef((object) this, handle));
        Telerik.WinControls.NativeMethods.DeleteObject(new HandleRef((object) this, hbitmap));
      }
      Telerik.WinControls.NativeMethods.DeleteDC(new HandleRef((object) this, compatibleDc));
    }

    protected virtual void AnimateFuncShow()
    {
      while ((int) this.alpha + (int) this.step <= (int) byte.MaxValue)
      {
        Thread.Sleep((int) this.sleepTime);
        this.alpha += this.step;
        this.UpdateLayeredWindow(this.alpha);
      }
      if (this.alpha == byte.MaxValue)
        return;
      this.UpdateLayeredWindow(byte.MaxValue);
    }

    protected virtual void AnimateFuncHide()
    {
      while ((int) this.alpha > (int) this.step)
      {
        Thread.Sleep((int) this.sleepTime);
        this.alpha -= this.step;
        this.UpdateLayeredWindow(this.alpha);
      }
      this.Hide();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(292, 271);
      this.FormBorderStyle = FormBorderStyle.None;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Name = nameof (SplashForm);
      this.Text = nameof (SplashForm);
      this.TopMost = true;
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
    }
  }
}
