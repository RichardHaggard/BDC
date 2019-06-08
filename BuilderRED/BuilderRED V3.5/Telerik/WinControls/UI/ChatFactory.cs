// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class ChatFactory
  {
    public virtual BaseChatItemElement CreateItemElement(BaseChatDataItem item)
    {
      if ((object) item.GetType() == (object) typeof (TextMessageDataItem))
        return (BaseChatItemElement) new TextMessageItemElement();
      if ((object) item.GetType() == (object) typeof (CardMessageDataItem))
        return (BaseChatItemElement) new CardMessageItemElement();
      if ((object) item.GetType() == (object) typeof (CarouselMessageDataItem))
        return (BaseChatItemElement) new CarouselMessageItemElement();
      if ((object) item.GetType() == (object) typeof (MediaMessageDataItem))
        return (BaseChatItemElement) new MediaMessageItemElement();
      if ((object) item.GetType() == (object) typeof (ChatTimeSeparatorDataItem))
        return (BaseChatItemElement) new ChatTimeSeparatorItemElement();
      return (BaseChatItemElement) null;
    }

    public virtual ToolbarActionElement CreateToolbarActionElement(
      ToolbarActionDataItem item)
    {
      return new ToolbarActionElement(item);
    }

    public virtual SuggestedActionElement CreateSuggestedActionElement(
      SuggestedActionDataItem item)
    {
      return new SuggestedActionElement(item);
    }

    public virtual BaseChatCardElement CreateCardElement(
      BaseChatCardDataItem cardDataItem)
    {
      if ((object) cardDataItem.GetType() == (object) typeof (ChatFlightCardDataItem))
        return (BaseChatCardElement) new ChatFlightCardElement(cardDataItem as ChatFlightCardDataItem);
      if ((object) cardDataItem.GetType() == (object) typeof (ChatImageCardDataItem))
        return (BaseChatCardElement) new ChatImageCardElement(cardDataItem as ChatImageCardDataItem);
      if ((object) cardDataItem.GetType() == (object) typeof (ChatProductCardDataItem))
        return (BaseChatCardElement) new ChatProductCardElement(cardDataItem as ChatProductCardDataItem);
      if ((object) cardDataItem.GetType() == (object) typeof (ChatWeatherCardDataItem))
        return (BaseChatCardElement) new ChatWeatherCardElement(cardDataItem as ChatWeatherCardDataItem);
      return (BaseChatCardElement) null;
    }

    public virtual BaseChatDataItem CreateDataItem(ChatMessage message)
    {
      ChatTextMessage message1 = message as ChatTextMessage;
      if (message1 != null)
        return (BaseChatDataItem) new TextMessageDataItem(message1);
      ChatMediaMessage message2 = message as ChatMediaMessage;
      if (message2 != null)
        return (BaseChatDataItem) new MediaMessageDataItem(message2);
      ChatCardMessage message3 = message as ChatCardMessage;
      if (message3 != null)
        return (BaseChatDataItem) new CardMessageDataItem(message3);
      ChatCarouselMessage message4 = message as ChatCarouselMessage;
      if (message4 != null)
        return (BaseChatDataItem) new CarouselMessageDataItem(message4);
      return (BaseChatDataItem) null;
    }
  }
}
