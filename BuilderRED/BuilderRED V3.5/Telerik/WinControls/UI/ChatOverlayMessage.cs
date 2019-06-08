// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatOverlayMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ChatOverlayMessage : ChatMessage
  {
    private BaseChatOverlay overlayElement;
    private bool showAsPopup;

    public ChatOverlayMessage(BaseChatOverlay overlayElement, Author author, DateTime timeStamp)
      : this(overlayElement, false, author, timeStamp, (object) null)
    {
    }

    public ChatOverlayMessage(
      BaseChatOverlay overlayElement,
      bool showAsPopup,
      Author author,
      DateTime timeStamp)
      : this(overlayElement, showAsPopup, author, timeStamp, (object) null)
    {
    }

    public ChatOverlayMessage(
      BaseChatOverlay overlayElement,
      bool showAsPopup,
      Author author,
      DateTime timeStamp,
      object userData)
      : base(author, timeStamp, userData)
    {
      this.overlayElement = overlayElement;
      this.showAsPopup = showAsPopup;
    }

    public BaseChatOverlay OverlayElement
    {
      get
      {
        return this.overlayElement;
      }
    }

    public bool ShowAsPopup
    {
      get
      {
        return this.showAsPopup;
      }
      set
      {
        this.showAsPopup = value;
      }
    }
  }
}
