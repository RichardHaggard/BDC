// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadNCEnabledControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class RadNCEnabledControl : RadControl
  {
    protected virtual bool EnableNCPainting
    {
      get
      {
        return false;
      }
    }

    protected virtual bool EnableNCModification
    {
      get
      {
        return false;
      }
    }

    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 131:
          this.OnWMNCCalcSize(ref m);
          break;
        case 133:
          this.OnWMNCPaint(ref m);
          break;
        default:
          base.WndProc(ref m);
          break;
      }
    }

    private void OnWMNCPaint(ref Message m)
    {
      if (!this.EnableNCPainting)
      {
        base.WndProc(ref m);
      }
      else
      {
        if (!this.IsHandleCreated)
          return;
        HandleRef hWnd = new HandleRef((object) this, this.Handle);
        NativeMethods.RECT rect = new NativeMethods.RECT();
        if (!NativeMethods.GetWindowRect(hWnd, ref rect))
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
        IntPtr dcEx = NativeMethods.GetDCEx(hWnd, hrgnClip, flags);
        try
        {
          if (!(dcEx != IntPtr.Zero))
            return;
          using (Graphics g = Graphics.FromHdc(dcEx))
            this.OnNCPaint(g);
        }
        finally
        {
          NativeMethods.ReleaseDC(new HandleRef((object) this, m.HWnd), new HandleRef((object) null, dcEx));
        }
      }
    }

    private void OnWMNCCalcSize(ref Message m)
    {
      if (!this.EnableNCModification)
        base.WndProc(ref m);
      else if (m.WParam == new IntPtr(1))
      {
        NativeMethods.NCCALCSIZE_PARAMS nccalcsizeParams = new NativeMethods.NCCALCSIZE_PARAMS();
        NativeMethods.NCCALCSIZE_PARAMS structure = (NativeMethods.NCCALCSIZE_PARAMS) Marshal.PtrToStructure(m.LParam, typeof (NativeMethods.NCCALCSIZE_PARAMS));
        Padding ncMetrics = this.GetNCMetrics();
        structure.rgrc[0].top += ncMetrics.Top;
        structure.rgrc[0].left += ncMetrics.Left;
        structure.rgrc[0].right -= ncMetrics.Right;
        structure.rgrc[0].bottom -= ncMetrics.Bottom;
        Marshal.StructureToPtr((object) structure, m.LParam, true);
        m.Result = IntPtr.Zero;
      }
      else
      {
        base.WndProc(ref m);
        NativeMethods.RECT rect = new NativeMethods.RECT();
        NativeMethods.RECT structure = (NativeMethods.RECT) Marshal.PtrToStructure(m.LParam, typeof (NativeMethods.RECT));
        Padding ncMetrics = this.GetNCMetrics();
        structure.top += ncMetrics.Top;
        structure.left += ncMetrics.Left;
        structure.right -= ncMetrics.Right;
        structure.bottom -= ncMetrics.Bottom;
        Marshal.StructureToPtr((object) structure, m.LParam, true);
        m.Result = IntPtr.Zero;
      }
    }

    protected virtual void OnNCPaint(Graphics g)
    {
    }

    protected virtual Padding GetNCMetrics()
    {
      return Padding.Empty;
    }
  }
}
