// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadHdcWrapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  public class RadHdcWrapper : IDeviceContext, IDisposable
  {
    private const string NativeLibrary = "gdi32";
    private const int GM_COMPATIBLE = 1;
    private const int GM_ADVANCED = 2;
    private const int MWT_IDENTITY = 1;
    private const int MWT_LEFTMULTIPLY = 2;
    private const int MWT_RIGHTMULTIPLY = 3;
    private IntPtr hdc;
    private IntPtr clipRegion;
    private IntPtr origRegion;
    private int dcState;
    private int graphicsMode;
    private bool useTransform;
    private RadHdcWrapper.XFORM transform;
    private RadHdcWrapper.XFORM oldTransform;
    private Graphics graphics;

    public RadHdcWrapper(Graphics g, bool useTransfrom)
    {
      this.graphics = g;
      Region clip = g.Clip;
      this.clipRegion = clip.GetHrgn(g);
      clip.Dispose();
      this.useTransform = useTransfrom;
      if (!this.useTransform)
        return;
      this.transform = RadHdcWrapper.XFORM.FromMatrix(g.Transform);
    }

    ~RadHdcWrapper()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      this.ReleaseHdc();
      this.graphics = (Graphics) null;
    }

    public IntPtr GetHdc()
    {
      this.hdc = this.graphics.GetHdc();
      this.dcState = RadHdcWrapper.SaveDC(this.hdc);
      if (this.useTransform)
      {
        this.graphicsMode = RadHdcWrapper.SetGraphicsMode(this.hdc, 2);
        RadHdcWrapper.GetWorldTransform(this.hdc, ref this.oldTransform);
        RadHdcWrapper.ModifyWorldTransform(this.hdc, ref this.transform, 2);
      }
      if (this.clipRegion != IntPtr.Zero)
      {
        this.origRegion = RadHdcWrapper.CreateRectRgn(0, 0, 0, 0);
        if (RadHdcWrapper.GetClipRgn(this.hdc, this.origRegion) == 1)
          RadHdcWrapper.CombineRgn(this.clipRegion, this.origRegion, this.clipRegion, 1);
        RadHdcWrapper.SelectClipRgn(this.hdc, this.clipRegion);
      }
      return this.hdc;
    }

    public void ReleaseHdc()
    {
      if (this.hdc == IntPtr.Zero)
        return;
      RadHdcWrapper.RestoreDC(this.hdc, this.dcState);
      if (this.useTransform)
      {
        RadHdcWrapper.SetGraphicsMode(this.hdc, this.graphicsMode);
        RadHdcWrapper.SetWorldTransform(this.hdc, ref this.oldTransform);
      }
      if (this.clipRegion != IntPtr.Zero)
      {
        NativeMethods.DeleteObject(new HandleRef((object) null, this.clipRegion));
        NativeMethods.DeleteObject(new HandleRef((object) null, this.origRegion));
        this.clipRegion = IntPtr.Zero;
        this.origRegion = IntPtr.Zero;
      }
      this.graphics.ReleaseHdc(this.hdc);
      this.hdc = IntPtr.Zero;
    }

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int SaveDC(IntPtr hDC);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int SetGraphicsMode(IntPtr hdc, int iMode);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int GetWorldTransform(IntPtr hdc, ref RadHdcWrapper.XFORM xForm);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int SetWorldTransform(IntPtr hdc, ref RadHdcWrapper.XFORM xForm);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int ModifyWorldTransform(
      IntPtr hdc,
      ref RadHdcWrapper.XFORM xForm,
      int mode);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern IntPtr CreateRectRgn(int X1, int Y1, int X2, int Y2);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int GetClipRgn(IntPtr hDC, IntPtr hrgn);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int CombineRgn(
      IntPtr hDestRgn,
      IntPtr hSrcRgn1,
      IntPtr hSrcRgn2,
      int nCombineMode);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern IntPtr RestoreDC(IntPtr hDC, int savedDC);

    private struct XFORM
    {
      public static RadHdcWrapper.XFORM Identity = new RadHdcWrapper.XFORM(1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f);
      public float eM11;
      public float eM12;
      public float eM21;
      public float eM22;
      public float eDx;
      public float eDy;

      public XFORM(float m11, float m12, float m21, float m22, float dx, float dy)
      {
        this.eM11 = m11;
        this.eM12 = m12;
        this.eM21 = m21;
        this.eM22 = m22;
        this.eDx = dx;
        this.eDy = dy;
      }

      public static RadHdcWrapper.XFORM FromMatrix(Matrix m)
      {
        float[] elements = m.Elements;
        return new RadHdcWrapper.XFORM(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
      }
    }
  }
}
