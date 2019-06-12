// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.DllWrapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  public class DllWrapper : IDisposable
  {
    private IntPtr nativeDll = IntPtr.Zero;

    public DllWrapper()
    {
    }

    public DllWrapper(string dllName)
    {
      this.nativeDll = NativeMethods.LoadLibrary(dllName);
    }

    public bool IsDllLoaded
    {
      get
      {
        return this.nativeDll != IntPtr.Zero;
      }
    }

    public bool LoadDll(string dllName)
    {
      this.FreeDll();
      lock (this)
        this.nativeDll = NativeMethods.LoadLibrary(dllName);
      return this.IsDllLoaded;
    }

    public bool FreeDll()
    {
      bool flag = false;
      lock (this)
      {
        if (this.nativeDll != IntPtr.Zero)
        {
          flag = NativeMethods.FreeLibrary(this.nativeDll);
          this.nativeDll = IntPtr.Zero;
        }
      }
      return flag;
    }

    public object GetFunctionAsDelegate(string functionName, Type delegateType)
    {
      object obj = (object) null;
      lock (this)
      {
        if (this.nativeDll != IntPtr.Zero)
        {
          if ((object) delegateType != null)
          {
            IntPtr procAddress = NativeMethods.GetProcAddress(this.nativeDll, functionName);
            if (procAddress != IntPtr.Zero)
              obj = (object) Marshal.GetDelegateForFunctionPointer(procAddress, delegateType);
          }
        }
      }
      return obj;
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      this.FreeDll();
    }

    ~DllWrapper()
    {
      this.Dispose(false);
    }
  }
}
