// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatTextMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ChatTextMessage : ChatMessage
  {
    private string message;

    public ChatTextMessage(string message, Author author, DateTime timeStamp)
      : this(message, author, timeStamp, (object) null)
    {
    }

    public ChatTextMessage(string message, Author author, DateTime timeStamp, object userData)
      : base(author, timeStamp, userData)
    {
      this.message = message;
    }

    public string Message
    {
      get
      {
        return this.message;
      }
      set
      {
        if (!(this.message != value))
          return;
        this.message = value;
        this.OnPropertyChanged(nameof (Message));
      }
    }
  }
}
