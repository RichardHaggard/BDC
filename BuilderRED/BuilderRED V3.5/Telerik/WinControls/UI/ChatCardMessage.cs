// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatCardMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ChatCardMessage : ChatMessage
  {
    private BaseChatCardDataItem cardDataItem;

    public ChatCardMessage(BaseChatCardDataItem cardDataItem, Author author, DateTime timeStamp)
      : this(cardDataItem, author, timeStamp, (object) null)
    {
    }

    public ChatCardMessage(
      BaseChatCardDataItem cardDataItem,
      Author author,
      DateTime timeStamp,
      object userData)
      : base(author, timeStamp, userData)
    {
      this.cardDataItem = cardDataItem;
    }

    public BaseChatCardDataItem CardDataItem
    {
      get
      {
        return this.cardDataItem;
      }
    }
  }
}
