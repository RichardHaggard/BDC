// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WindowsHook
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  public class WindowsHook
  {
    protected IntPtr hook = IntPtr.Zero;
    protected WindowsHook.HookProc hookFunc;
    protected WindowsHook.HookType hookType;

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(
      WindowsHook.HookType code,
      WindowsHook.HookProc func,
      IntPtr hInstance,
      int threadID);

    [DllImport("user32.dll")]
    private static extern int UnhookWindowsHookEx(IntPtr hhook);

    [DllImport("user32.dll")]
    private static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public event WindowsHook.HookEventHandler HookInvoked;

    public bool IsInstalled
    {
      get
      {
        return this.hook != IntPtr.Zero;
      }
    }

    public WindowsHook(WindowsHook.HookType hook)
    {
      this.hookType = hook;
      this.hookFunc = new WindowsHook.HookProc(this.CoreHookProc);
    }

    public void Install()
    {
      using (Process currentProcess = Process.GetCurrentProcess())
      {
        using (ProcessModule mainModule = currentProcess.MainModule)
          this.hook = WindowsHook.SetWindowsHookEx(this.hookType, this.hookFunc, WindowsHook.GetModuleHandle(mainModule.ModuleName), 0);
      }
    }

    public void Uninstall()
    {
      WindowsHook.UnhookWindowsHookEx(this.hook);
      this.hook = IntPtr.Zero;
    }

    protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
    {
      if (code < 0 || this.OnHookInvoked(new HookEventArgs(code, wParam, lParam)))
        return WindowsHook.CallNextHookEx(this.hook, code, wParam, lParam);
      return code;
    }

    protected virtual bool OnHookInvoked(HookEventArgs e)
    {
      if (this.HookInvoked != null)
        return this.HookInvoked((object) this, e);
      return true;
    }

    public enum HookType
    {
      WH_JOURNALRECORD,
      WH_JOURNALPLAYBACK,
      WH_KEYBOARD,
      WH_GETMESSAGE,
      WH_CALLWNDPROC,
      WH_CBT,
      WH_SYSMSGFILTER,
      WH_MOUSE,
      WH_HARDWARE,
      WH_DEBUG,
      WH_SHELL,
      WH_FOREGROUNDIDLE,
      WH_CALLWNDPROCRET,
      WH_KEYBOARD_LL,
      WH_MOUSE_LL,
    }

    public struct POINT
    {
      public int x;
      public int y;
    }

    public struct MouseHookStruct
    {
      public WindowsHook.POINT pt;
      public int hwnd;
      public int wHitTestCode;
      public int dwExtraInfo;
    }

    public struct MSLLHOOKSTRUCT
    {
      public WindowsHook.POINT pt;
      public uint mouseData;
      public uint flags;
      public uint time;
      public IntPtr dwExtraInfo;
    }

    public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

    public delegate bool HookEventHandler(object sender, HookEventArgs e);
  }
}
