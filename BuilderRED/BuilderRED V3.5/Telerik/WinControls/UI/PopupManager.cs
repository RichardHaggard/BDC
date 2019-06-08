// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PopupManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public sealed class PopupManager : IMessageListener
  {
    private List<IPopupControl> popups = new List<IPopupControl>();
    private IPopupControl lastActivatedPopup;
    private bool hooked;
    [ThreadStatic]
    private static PopupManager defaultManager;

    private PopupManager()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool Hooked
    {
      get
      {
        return this.hooked;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public int PopupCount
    {
      get
      {
        return this.popups.Count;
      }
    }

    public IPopupControl LastActivatedPopup
    {
      get
      {
        return this.lastActivatedPopup;
      }
      set
      {
        this.lastActivatedPopup = value;
      }
    }

    public void AddPopup(IPopupControl form)
    {
      if (!this.popups.Contains(form))
      {
        form.OwnerPopup?.Children.Add(form);
        this.popups.Add(form);
        this.lastActivatedPopup = form;
      }
      if (this.popups.Count <= 0 || this.hooked)
        return;
      RadMessageFilter.Instance.AddListener((IMessageListener) this);
      this.hooked = true;
    }

    public void RemovePopup(IPopupControl form)
    {
      if (this.popups.Contains(form))
      {
        while (form.Children.Count > 0)
          form.Children[form.Children.Count - 1].ClosePopup(RadPopupCloseReason.ParentClosed);
        form.OwnerPopup?.Children.Remove(form);
        this.popups.Remove(form);
      }
      if (this.popups.Count == 0)
      {
        this.lastActivatedPopup = (IPopupControl) null;
        RadMessageFilter.Instance.RemoveListener((IMessageListener) this);
        this.hooked = false;
      }
      else
        this.lastActivatedPopup = this.popups[this.popups.Count - 1];
    }

    public bool ClosePopup(IPopupControl popup)
    {
      if (!this.ContainsPopup(popup) || !popup.CanClosePopup(RadPopupCloseReason.CloseCalled))
        return false;
      PopupCloseInfo closeInfo = new PopupCloseInfo(RadPopupCloseReason.CloseCalled, (object) null);
      popup.ClosePopup(closeInfo);
      return closeInfo.Closed;
    }

    public void CloseAll(RadPopupCloseReason reason)
    {
      while (this.popups.Count > 0)
      {
        this.lastActivatedPopup = this.popups[this.popups.Count - 1];
        if (!this.lastActivatedPopup.CanClosePopup(reason))
          return;
        PopupCloseInfo closeInfo = new PopupCloseInfo(reason, (object) null);
        this.lastActivatedPopup.ClosePopup(closeInfo);
        if (!closeInfo.Closed)
          return;
      }
      this.lastActivatedPopup = (IPopupControl) null;
    }

    public void CloseAllToRoot(RadPopupCloseReason reason, IPopupControl leaf)
    {
      if (leaf.Children.Count > 0)
        return;
      bool flag = false;
      if (!leaf.CanClosePopup(reason))
        return;
      PopupCloseInfo closeInfo1 = new PopupCloseInfo(reason, (object) null);
      leaf.ClosePopup(closeInfo1);
      if (!closeInfo1.Closed)
        return;
      IPopupControl ownerPopup = leaf.OwnerPopup;
      while (!flag)
      {
        if (ownerPopup != null && ownerPopup.CanClosePopup(reason))
        {
          PopupCloseInfo closeInfo2 = new PopupCloseInfo(reason, (object) null);
          ownerPopup.ClosePopup(closeInfo2);
          if (!closeInfo2.Closed)
            flag = true;
          ownerPopup = ownerPopup.OwnerPopup;
        }
        else
          flag = true;
      }
    }

    public bool ContainsPopup(IPopupControl form)
    {
      return this.popups.Contains(form);
    }

    private void OnActivate(IntPtr param)
    {
      if (!(param == IntPtr.Zero) || this.popups.Count == 0)
        return;
      this.CloseAll(RadPopupCloseReason.AppFocusChange);
    }

    private bool OnKeyDown(ref Message msg)
    {
      if (this.lastActivatedPopup == null)
        return false;
      return this.lastActivatedPopup.OnKeyDown((Keys) (int) msg.WParam);
    }

    private bool OnMouseWheel(Message m)
    {
      if (this.lastActivatedPopup == null)
        return false;
      Control target = Control.FromChildHandle(m.HWnd);
      if (target == null)
        return false;
      int delta = Telerik.WinControls.NativeMethods.Util.SignedHIWORD(m.WParam);
      return this.lastActivatedPopup.OnMouseWheel(target, delta);
    }

    internal void OnMouseDown(Point cursorPos)
    {
      int num = this.popups.Count - 1;
      bool flag1 = false;
      while (num > -1 && this.popups.Count > 0)
      {
        IPopupControl popup = this.popups[num--];
        bool flag2 = popup.Bounds.Contains(cursorPos);
        if (!flag1 && !flag2)
        {
          if (popup.CanClosePopup(RadPopupCloseReason.Mouse))
          {
            PopupCloseInfo closeInfo = new PopupCloseInfo(RadPopupCloseReason.Mouse, (object) null);
            popup.ClosePopup(closeInfo);
            flag1 = !closeInfo.Closed;
          }
          else
            flag1 = true;
        }
        else if (popup.OwnerPopup == null)
          flag1 = false;
        else if (flag2)
          flag1 = true;
      }
    }

    public static PopupManager Default
    {
      get
      {
        if (PopupManager.defaultManager == null)
          PopupManager.defaultManager = new PopupManager();
        return PopupManager.defaultManager;
      }
    }

    InstalledHook IMessageListener.DesiredHook
    {
      get
      {
        return InstalledHook.GetMessage | InstalledHook.CallWndProc;
      }
    }

    MessagePreviewResult IMessageListener.PreviewMessage(
      ref Message msg)
    {
      switch (msg.Msg)
      {
        case 161:
        case 164:
        case 167:
        case 513:
        case 516:
        case 519:
          this.OnMouseDown(Cursor.Position);
          break;
        case 256:
        case 260:
          if (this.OnKeyDown(ref msg))
            return MessagePreviewResult.ProcessedNoDispatch;
          break;
        case 522:
          if (this.OnMouseWheel(msg))
            return MessagePreviewResult.Processed;
          this.CloseAll(RadPopupCloseReason.Mouse);
          break;
      }
      return MessagePreviewResult.Processed;
    }

    void IMessageListener.PreviewWndProc(Message msg)
    {
      if (msg.Msg != 28)
        return;
      this.OnActivate(msg.WParam);
    }

    void IMessageListener.PreviewSystemMessage(SystemMessage message, Message msg)
    {
      throw new NotImplementedException();
    }
  }
}
