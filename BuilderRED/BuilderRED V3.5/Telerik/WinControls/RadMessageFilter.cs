// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadMessageFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public sealed class RadMessageFilter
  {
    private static bool callNextHookWhenNoDispatch = true;
    [ThreadStatic]
    private static RadMessageFilter instance;
    private InstalledHook installedHook;
    private WeakReferenceList<IMessageListener> listeners;
    private bool enabled;
    private RadMessageFilter.HookProc getMessageDelegate;
    private RadMessageFilter.HookProc callWndProcDelegate;
    private RadMessageFilter.HookProc systemMessageDelegate;
    private IntPtr getMessageHookAddress;
    private IntPtr callWndProcHookAddress;
    private IntPtr systemMessageHookAddress;

    private RadMessageFilter()
    {
      this.getMessageDelegate = new RadMessageFilter.HookProc(this.GetMessageHookProc);
      this.callWndProcDelegate = new RadMessageFilter.HookProc(this.CallWndProcHookProc);
      this.systemMessageDelegate = new RadMessageFilter.HookProc(this.SystemMessageHookProc);
      this.installedHook = InstalledHook.None;
      this.listeners = new WeakReferenceList<IMessageListener>(true);
      this.enabled = true;
    }

    public static bool CallNextHookWhenNoDispatch
    {
      get
      {
        return RadMessageFilter.callNextHookWhenNoDispatch;
      }
      set
      {
        RadMessageFilter.callNextHookWhenNoDispatch = value;
      }
    }

    public static RadMessageFilter Instance
    {
      get
      {
        if (RadMessageFilter.instance == null)
          RadMessageFilter.instance = new RadMessageFilter();
        return RadMessageFilter.instance;
      }
    }

    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        if (this.enabled == value)
          return;
        this.enabled = value;
        this.OnEnabledChanged();
      }
    }

    public void AddListener(IMessageListener listener)
    {
      if (this.ContainsListener(listener))
        return;
      this.listeners.Add(listener);
      this.EnsureInstalledHooks();
    }

    public void RemoveListener(IMessageListener listener)
    {
      this.listeners.Remove(listener);
      this.EnsureInstalledHooks();
    }

    public void RemoveAll()
    {
      this.listeners.Clear();
      this.Uninstall(InstalledHook.All);
    }

    public bool ContainsListener(IMessageListener listener)
    {
      return this.listeners.IndexOf(listener) >= 0;
    }

    public InstalledHook InstalledHook
    {
      get
      {
        return this.installedHook;
      }
    }

    private void OnEnabledChanged()
    {
      if (this.enabled)
        this.EnsureInstalledHooks();
      else
        this.Uninstall(InstalledHook.All);
    }

    private void Uninstall(InstalledHook toUninstall)
    {
      if ((toUninstall & InstalledHook.CallWndProc) != InstalledHook.None && (this.installedHook & InstalledHook.CallWndProc) != InstalledHook.None)
      {
        RadMessageFilter.UnhookWindowsHookEx(this.callWndProcHookAddress);
        this.callWndProcHookAddress = IntPtr.Zero;
        this.installedHook &= ~InstalledHook.CallWndProc;
      }
      if ((toUninstall & InstalledHook.GetMessage) != InstalledHook.None && (this.installedHook & InstalledHook.GetMessage) != InstalledHook.None)
      {
        RadMessageFilter.UnhookWindowsHookEx(this.getMessageHookAddress);
        this.getMessageHookAddress = IntPtr.Zero;
        this.installedHook &= ~InstalledHook.GetMessage;
      }
      if ((toUninstall & InstalledHook.SystemMessage) == InstalledHook.None || (this.installedHook & InstalledHook.SystemMessage) == InstalledHook.None)
        return;
      RadMessageFilter.UnhookWindowsHookEx(this.systemMessageHookAddress);
      this.systemMessageHookAddress = IntPtr.Zero;
      this.installedHook &= ~InstalledHook.SystemMessage;
    }

    private void Install(InstalledHook toInstall)
    {
      int currentThreadId = AppDomain.GetCurrentThreadId();
      if ((toInstall & InstalledHook.CallWndProc) != InstalledHook.None && (this.installedHook & InstalledHook.CallWndProc) == InstalledHook.None)
      {
        this.callWndProcHookAddress = RadMessageFilter.SetWindowsHookEx(RadMessageFilter.HookType.WH_CALLWNDPROC, this.callWndProcDelegate, IntPtr.Zero, currentThreadId);
        this.installedHook |= InstalledHook.CallWndProc;
      }
      if ((toInstall & InstalledHook.GetMessage) != InstalledHook.None && (this.installedHook & InstalledHook.GetMessage) == InstalledHook.None)
      {
        this.getMessageHookAddress = RadMessageFilter.SetWindowsHookEx(RadMessageFilter.HookType.WH_GETMESSAGE, this.getMessageDelegate, IntPtr.Zero, currentThreadId);
        this.installedHook |= InstalledHook.GetMessage;
      }
      if ((toInstall & InstalledHook.SystemMessage) == InstalledHook.None || (this.installedHook & InstalledHook.SystemMessage) != InstalledHook.None)
        return;
      this.systemMessageHookAddress = RadMessageFilter.SetWindowsHookEx(RadMessageFilter.HookType.WH_MSGFILTER, this.systemMessageDelegate, IntPtr.Zero, currentThreadId);
      this.installedHook |= InstalledHook.SystemMessage;
    }

    private void EnsureInstalledHooks()
    {
      if (!this.enabled)
        return;
      InstalledHook desiredHook = this.GetDesiredHook();
      if (this.installedHook == desiredHook)
        return;
      InstalledHook toInstall = InstalledHook.None;
      InstalledHook toUninstall = InstalledHook.None;
      if ((desiredHook & InstalledHook.CallWndProc) == InstalledHook.CallWndProc)
        toInstall |= InstalledHook.CallWndProc;
      else
        toUninstall |= InstalledHook.CallWndProc;
      if ((desiredHook & InstalledHook.GetMessage) == InstalledHook.GetMessage)
        toInstall |= InstalledHook.GetMessage;
      else
        toUninstall |= InstalledHook.GetMessage;
      if ((desiredHook & InstalledHook.SystemMessage) == InstalledHook.SystemMessage)
        toInstall |= InstalledHook.SystemMessage;
      else
        toUninstall |= InstalledHook.SystemMessage;
      this.Install(toInstall);
      this.Uninstall(toUninstall);
    }

    private InstalledHook GetDesiredHook()
    {
      InstalledHook installedHook = InstalledHook.None;
      foreach (IMessageListener listener in this.listeners)
      {
        InstalledHook desiredHook = listener.DesiredHook;
        if ((desiredHook & InstalledHook.CallWndProc) == InstalledHook.CallWndProc)
          installedHook |= InstalledHook.CallWndProc;
        if ((desiredHook & InstalledHook.GetMessage) == InstalledHook.GetMessage)
          installedHook |= InstalledHook.GetMessage;
        if ((desiredHook & InstalledHook.SystemMessage) == InstalledHook.SystemMessage)
          installedHook |= InstalledHook.SystemMessage;
      }
      return installedHook;
    }

    private int GetMessageHookProc(int code, IntPtr wParam, IntPtr lParam)
    {
      if (code >= 0)
      {
        if ((long) wParam > 0L)
        {
          Message structure = (Message) Marshal.PtrToStructure(lParam, typeof (Message));
          if ((this.NotifyGetMessageEvent(ref structure) & MessagePreviewResult.NoDispatch) == MessagePreviewResult.NoDispatch)
          {
            if (RadMessageFilter.callNextHookWhenNoDispatch)
            {
              structure.HWnd = new IntPtr(-1);
              structure.Msg = -1;
              Marshal.StructureToPtr((object) structure, lParam, true);
            }
          }
        }
      }
      try
      {
        return RadMessageFilter.CallNextHookEx(this.getMessageHookAddress, code, wParam, lParam);
      }
      catch (AccessViolationException ex)
      {
      }
      return 0;
    }

    private int CallWndProcHookProc(int code, IntPtr wParam, IntPtr lParam)
    {
      if (code >= 0)
        this.NotifyCallWndProcEvent(wParam, lParam);
      return RadMessageFilter.CallNextHookEx(this.callWndProcHookAddress, code, wParam, lParam);
    }

    private int SystemMessageHookProc(int code, IntPtr wParam, IntPtr lParam)
    {
      if (code >= 0)
        this.NotifyMessageFilterEvent((SystemMessage) code, lParam);
      return RadMessageFilter.CallNextHookEx(this.systemMessageHookAddress, code, wParam, lParam);
    }

    private MessagePreviewResult NotifyGetMessageEvent(ref Message msg)
    {
      MessagePreviewResult messagePreviewResult1 = MessagePreviewResult.NotProcessed;
      foreach (IMessageListener messageListener in new List<IMessageListener>((IEnumerable<IMessageListener>) this.listeners))
      {
        if ((messageListener.DesiredHook & InstalledHook.GetMessage) != InstalledHook.None)
        {
          MessagePreviewResult messagePreviewResult2 = messageListener.PreviewMessage(ref msg);
          if ((messagePreviewResult2 & MessagePreviewResult.Processed) == MessagePreviewResult.Processed)
            messagePreviewResult1 |= MessagePreviewResult.Processed;
          if ((messagePreviewResult2 & MessagePreviewResult.NoDispatch) == MessagePreviewResult.NoDispatch)
            messagePreviewResult1 |= MessagePreviewResult.NoDispatch;
          if ((messagePreviewResult2 & MessagePreviewResult.NoContinue) == MessagePreviewResult.NoContinue)
          {
            messagePreviewResult1 |= MessagePreviewResult.NoContinue;
            break;
          }
        }
      }
      return messagePreviewResult1;
    }

    private void NotifyCallWndProcEvent(IntPtr wParam, IntPtr lParam)
    {
      RadMessageFilter.CWPSTRUCT structure = (RadMessageFilter.CWPSTRUCT) Marshal.PtrToStructure(lParam, typeof (RadMessageFilter.CWPSTRUCT));
      Message msg = Message.Create(structure.hWnd, structure.msg, structure.wParam, structure.lParam);
      foreach (IMessageListener listener in this.listeners)
      {
        if ((listener.DesiredHook & InstalledHook.CallWndProc) == InstalledHook.CallWndProc)
          listener.PreviewWndProc(msg);
      }
    }

    private void NotifyMessageFilterEvent(SystemMessage message, IntPtr lParam)
    {
      Message structure = (Message) Marshal.PtrToStructure(lParam, typeof (Message));
      foreach (IMessageListener listener in this.listeners)
      {
        if ((listener.DesiredHook & InstalledHook.SystemMessage) == InstalledHook.SystemMessage)
          listener.PreviewSystemMessage(message, structure);
      }
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(
      RadMessageFilter.HookType code,
      RadMessageFilter.HookProc func,
      IntPtr hInstance,
      int threadID);

    [DllImport("user32.dll")]
    private static extern int UnhookWindowsHookEx(IntPtr hhook);

    [DllImport("user32.dll")]
    private static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);

    private enum HookType
    {
      WH_MSGFILTER = -1,
      WH_JOURNALRECORD = 0,
      WH_JOURNALPLAYBACK = 1,
      WH_KEYBOARD = 2,
      WH_GETMESSAGE = 3,
      WH_CALLWNDPROC = 4,
      WH_CBT = 5,
      WH_SYSMSGFILTER = 6,
      WH_MOUSE = 7,
      WH_HARDWARE = 8,
      WH_DEBUG = 9,
      WH_SHELL = 10, // 0x0000000A
      WH_FOREGROUNDIDLE = 11, // 0x0000000B
      WH_CALLWNDPROCRET = 12, // 0x0000000C
      WH_KEYBOARD_LL = 13, // 0x0000000D
      WH_MOUSE_LL = 14, // 0x0000000E
    }

    private delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

    private struct MouseHookStruct
    {
      public NativeMethods.POINT pt;
      public int hwnd;
      public int wHitTestCode;
      public int dwExtraInfo;
    }

    private struct CWPSTRUCT
    {
      public IntPtr lParam;
      public IntPtr wParam;
      public int msg;
      public IntPtr hWnd;
    }
  }
}
