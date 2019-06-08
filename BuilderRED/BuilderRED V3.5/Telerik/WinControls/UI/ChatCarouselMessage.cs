// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatCarouselMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ChatCarouselMessage : ChatMessage
  {
    private IEnumerable<BaseChatCardDataItem> cards;

    public ChatCarouselMessage(
      IEnumerable<BaseChatCardDataItem> cards,
      Author author,
      DateTime timeStamp)
      : this(cards, author, timeStamp, (object) null)
    {
    }

    public ChatCarouselMessage(
      IEnumerable<BaseChatCardDataItem> cards,
      Author author,
      DateTime timeStamp,
      object userData)
      : base(author, timeStamp, userData)
    {
      this.cards = cards;
      foreach (BaseChatCardDataItem card in this.Cards)
        card.PropertyChanged += new PropertyChangedEventHandler(this.Card_PropertyChanged);
    }

    public IEnumerable<BaseChatCardDataItem> Cards
    {
      get
      {
        return this.cards;
      }
    }

    private void Card_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged("CardProperty");
    }
  }
}
