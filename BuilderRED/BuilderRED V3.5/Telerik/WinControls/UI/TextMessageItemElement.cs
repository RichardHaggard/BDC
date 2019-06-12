// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextMessageItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TextMessageItemElement : BaseChatItemElement
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      int num = (int) this.MainMessageElement.BindProperty(ChatMessageBubbleElement.IsOwnMessageProperty, (RadObject) this, BaseChatItemElement.IsOwnMessageProperty, PropertyBindingOptions.OneWay);
    }

    protected override LightVisualElement CreateMainMessageElement()
    {
      LightVisualElement mainMessageElement = base.CreateMainMessageElement();
      mainMessageElement.EnableElementShadow = true;
      mainMessageElement.Font = new Font(this.Font.FontFamily, 9.75f);
      mainMessageElement.SmoothingMode = SmoothingMode.HighQuality;
      mainMessageElement.Padding = new Padding(10);
      mainMessageElement.TextWrap = true;
      return mainMessageElement;
    }

    public override bool IsCompatible(BaseChatDataItem data, object context)
    {
      return (object) data.GetType() == (object) typeof (TextMessageDataItem);
    }

    public override void Synchronize()
    {
      this.MainMessageElement.Text = ((TextMessageDataItem) this.Data).TextMessage.Message;
      this.MainMessageElement.TextAlignment = this.Data.IsOwnMessage ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
      bool flag1 = !this.Data.ChatMessagesViewElement.ShowMessagesOnOneSide && this.Data.IsOwnMessage;
      bool flag2 = this.Data.ChatMessagesViewElement.ShowMessagesOnOneSide || !this.Data.IsOwnMessage;
      switch (this.Data.MessageType)
      {
        case ChatMessageType.First:
          this.MainMessageElement.Shape = (ElementShape) new RoundRectShape(10, true, flag1, true, flag2);
          break;
        case ChatMessageType.Middle:
          this.MainMessageElement.Shape = (ElementShape) new RoundRectShape(10, flag1, flag1, flag2, flag2);
          break;
        case ChatMessageType.Last:
          this.MainMessageElement.Shape = (ElementShape) new RoundRectShape(10, flag1, true, flag2, true);
          break;
        case ChatMessageType.Single:
          this.MainMessageElement.Shape = (ElementShape) new RoundRectShape(10);
          break;
      }
      base.Synchronize();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF1 = base.MeasureOverride(availableSize);
      if (this.Data == null)
        return sizeF1;
      this.MainMessageElement.Measure(new SizeF((float) ((double) availableSize.Width * 0.800000011920929 - (this.Data.ChatMessagesViewElement.ShowAvatars ? (double) this.Data.ChatMessagesViewElement.AvatarSize.Width : 0.0)), float.PositiveInfinity));
      SizeF sizeF2 = new SizeF(this.AvatarPictureElement.DesiredSize.Width + this.MainMessageElement.DesiredSize.Width * 1.2f + (float) this.Margin.Horizontal, Math.Max(this.AvatarPictureElement.DesiredSize.Height, this.MainMessageElement.DesiredSize.Height) + this.NameLabelElement.DesiredSize.Height + (float) this.Margin.Vertical);
      this.Data.ActualSize = sizeF2;
      return sizeF2;
    }
  }
}
