// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatVirtualizedElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatVirtualizedElementProvider : VirtualizedPanelElementProvider<BaseChatDataItem, BaseChatItemElement>
  {
    public override IVirtualizedElement<BaseChatDataItem> CreateElement(
      BaseChatDataItem data,
      object context)
    {
      return (IVirtualizedElement<BaseChatDataItem>) data.ChatMessagesViewElement.ChatElement.ChatFactory.CreateItemElement(data);
    }

    public override SizeF GetElementSize(BaseChatDataItem item)
    {
      return item.ActualSize;
    }
  }
}
