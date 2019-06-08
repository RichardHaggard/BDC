// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.NativeWindowAdapter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class NativeWindowAdapter : NativeWindow, IDisposable
  {
    private WndHookDelegate callback;
    private WndHookDelegate postCallback;

    public NativeWindowAdapter(IntPtr intPtr)
    {
      this.AssignHandle(intPtr);
    }

    ~NativeWindowAdapter()
    {
      this.Dispose();
    }

    public void SetControl(Control control)
    {
      this.AssignHandle(control.Handle);
    }

    public void AddHook(WndHookDelegate callback)
    {
      this.callback = callback;
    }

    public void RemoveHook(WndHookDelegate callback)
    {
      this.callback = (WndHookDelegate) null;
    }

    public void AddPostWndProcHook(WndHookDelegate callback)
    {
      this.postCallback = callback;
    }

    public void RemovePostWndProcHook(WndHookDelegate callback)
    {
      this.postCallback = (WndHookDelegate) null;
    }

    protected override void WndProc(ref Message m)
    {
      bool handled = false;
      bool defWndProc = false;
      if (this.callback != null)
      {
        IntPtr num1 = this.callback(m.HWnd, m.Msg, m.WParam, m.LParam, ref handled, ref defWndProc);
      }
      if (defWndProc)
      {
        this.DefWndProc(ref m);
      }
      else
      {
        if (handled)
          return;
        base.WndProc(ref m);
        if (this.postCallback == null)
          return;
        IntPtr num2 = this.postCallback(m.HWnd, m.Msg, m.WParam, m.LParam, ref handled, ref defWndProc);
      }
    }

    public void Dispose()
    {
      this.ReleaseHandle();
    }
  }
}
