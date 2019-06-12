// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadKeyboardFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public sealed class RadKeyboardFilter : IMessageListener
  {
    [ThreadStatic]
    private static RadKeyboardFilter instance;
    private WeakReferenceList<IKeyboardListener> listeners;
    private bool hookInstalled;
    private bool enabled;
    private bool handleInputForUnmanagedControls;

    private RadKeyboardFilter()
    {
      this.listeners = new WeakReferenceList<IKeyboardListener>(true, false);
      this.enabled = true;
    }

    public static RadKeyboardFilter Instance
    {
      get
      {
        if (RadKeyboardFilter.instance == null)
          RadKeyboardFilter.instance = new RadKeyboardFilter();
        return RadKeyboardFilter.instance;
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
        this.enabled = value;
      }
    }

    public int ListenersCount
    {
      get
      {
        return this.listeners.Count;
      }
    }

    public bool HandleInputForUnmanagedControls
    {
      get
      {
        return this.handleInputForUnmanagedControls;
      }
      set
      {
        this.handleInputForUnmanagedControls = value;
      }
    }

    public void AddListener(IKeyboardListener listener)
    {
      if (this.ContainsListener(listener))
        return;
      this.listeners.Add(listener);
      this.UpdateHook();
    }

    public void RemoveListener(IKeyboardListener listener)
    {
      int index = this.listeners.IndexOf(listener);
      if (index < 0)
        return;
      this.listeners.RemoveAt(index);
      this.UpdateHook();
    }

    public bool ContainsListener(IKeyboardListener listener)
    {
      return this.listeners.IndexOf(listener) >= 0;
    }

    private void UpdateHook()
    {
      if (this.listeners.Count > 0)
      {
        if (this.hookInstalled)
          return;
        RadMessageFilter.Instance.AddListener((IMessageListener) this);
        this.hookInstalled = true;
      }
      else
      {
        if (!this.hookInstalled)
          return;
        RadMessageFilter.Instance.RemoveListener((IMessageListener) this);
        this.hookInstalled = false;
      }
    }

    InstalledHook IMessageListener.DesiredHook
    {
      get
      {
        return InstalledHook.GetMessage;
      }
    }

    MessagePreviewResult IMessageListener.PreviewMessage(
      ref Message msg)
    {
      if (!this.enabled || msg.Msg < 256 || msg.Msg > 264)
        return MessagePreviewResult.NotProcessed;
      Control target = Control.FromChildHandle(msg.HWnd);
      if (target == null && !this.handleInputForUnmanagedControls)
        return MessagePreviewResult.NotProcessed;
      return this.NotifyKeyboardEvent(target, msg);
    }

    private MessagePreviewResult NotifyKeyboardEvent(
      Control target,
      Message msg)
    {
      MessagePreviewResult messagePreviewResult1 = MessagePreviewResult.NotProcessed;
      foreach (IKeyboardListener listener in this.listeners)
      {
        MessagePreviewResult messagePreviewResult2 = this.NotifyListener(listener, target, msg);
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
      return messagePreviewResult1;
    }

    private MessagePreviewResult NotifyListener(
      IKeyboardListener listener,
      Control target,
      Message msg)
    {
      MessagePreviewResult messagePreviewResult = MessagePreviewResult.NotProcessed;
      switch (msg.Msg)
      {
        case 256:
        case 260:
          Keys keyData1 = (Keys) (long) msg.WParam | Control.ModifierKeys;
          messagePreviewResult = listener.OnPreviewKeyDown(target, new KeyEventArgs(keyData1));
          break;
        case 257:
        case 261:
          Keys keyData2 = (Keys) (long) msg.WParam | Control.ModifierKeys;
          messagePreviewResult = listener.OnPreviewKeyUp(target, new KeyEventArgs(keyData2));
          break;
        case 258:
        case 262:
          KeyPressEventArgs e = new KeyPressEventArgs((char) (long) msg.WParam);
          messagePreviewResult = listener.OnPreviewKeyPress(target, e);
          break;
      }
      return messagePreviewResult;
    }

    void IMessageListener.PreviewWndProc(Message msg)
    {
      throw new NotImplementedException();
    }

    void IMessageListener.PreviewSystemMessage(SystemMessage message, Message msg)
    {
      throw new NotImplementedException();
    }
  }
}
