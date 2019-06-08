// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardMessageItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Telerik.WinControls.UI
{
  public class CardMessageItemElement : BaseChatItemElement
  {
    protected override LightVisualElement CreateMainMessageElement()
    {
      LightVisualElement mainMessageElement = base.CreateMainMessageElement();
      mainMessageElement.SmoothingMode = SmoothingMode.HighQuality;
      mainMessageElement.Shape = (ElementShape) new RoundRectShape(20);
      return mainMessageElement;
    }

    public override bool IsCompatible(BaseChatDataItem data, object context)
    {
      return (object) data.GetType() == (object) typeof (CardMessageDataItem);
    }

    public override void Attach(BaseChatDataItem data, object context)
    {
      base.Attach(data, context);
      CardMessageDataItem data1 = this.Data as CardMessageDataItem;
      BaseChatCardElement cardElement = data1.ChatMessagesViewElement.ChatElement.ChatFactory.CreateCardElement(data1.CardMessage.CardDataItem);
      cardElement.CardActionClicked += new CardActionEventHandler(this.CardActionClicked);
      this.MainMessageElement.Children.Add((RadElement) cardElement);
    }

    public override void Detach()
    {
      foreach (BaseChatCardElement child in this.MainMessageElement.Children)
        child.CardActionClicked -= new CardActionEventHandler(this.CardActionClicked);
      this.MainMessageElement.Children.Clear();
      base.Detach();
    }

    protected virtual void CardActionClicked(object sender, CardActionEventArgs e)
    {
      this.Data.ChatMessagesViewElement.OnCardActionClicked(sender, e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF1 = base.MeasureOverride(availableSize);
      if (this.Data == null)
        return sizeF1;
      this.MainMessageElement.Measure(new SizeF(availableSize.Width - (this.Data.ChatMessagesViewElement.ShowAvatars ? this.Data.ChatMessagesViewElement.AvatarSize.Width : 0.0f), float.PositiveInfinity));
      SizeF sizeF2 = new SizeF(this.AvatarPictureElement.DesiredSize.Width + this.MainMessageElement.DesiredSize.Width + (float) this.Margin.Horizontal, Math.Max(this.AvatarPictureElement.DesiredSize.Height, this.MainMessageElement.DesiredSize.Height) + this.NameLabelElement.DesiredSize.Height + (float) this.Margin.Vertical);
      this.Data.ActualSize = sizeF2;
      return sizeF2;
    }
  }
}
