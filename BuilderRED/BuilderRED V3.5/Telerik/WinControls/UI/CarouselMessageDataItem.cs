// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselMessageDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CarouselMessageDataItem : BaseChatDataItem
  {
    private ChatCarouselMessage message;
    private int scrollOffset;

    public CarouselMessageDataItem(ChatCarouselMessage message)
      : base((ChatMessage) message)
    {
      this.message = message;
    }

    public ChatCarouselMessage CarouselMessage
    {
      get
      {
        return this.message;
      }
    }

    public int ScrollOffset
    {
      get
      {
        return this.scrollOffset;
      }
      set
      {
        this.scrollOffset = value;
      }
    }
  }
}
