// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselMessageItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CarouselMessageItemElement : BaseChatItemElement
  {
    protected override LightVisualElement CreateMainMessageElement()
    {
      HorizontalScrollableStackElement scrollableStackElement = new HorizontalScrollableStackElement();
      scrollableStackElement.Margin = new Padding(-60, 0, 60, 0);
      scrollableStackElement.Padding = new Padding(60, 0, 0, 0);
      scrollableStackElement.ScrollBar.ValueChanged += new EventHandler(this.ScrollBar_ValueChanged);
      return (LightVisualElement) scrollableStackElement;
    }

    private void ScrollBar_ValueChanged(object sender, EventArgs e)
    {
      (this.Data as CarouselMessageDataItem).ScrollOffset = (this.MainMessageElement as HorizontalScrollableStackElement).ScrollBar.Value;
    }

    public override bool IsCompatible(BaseChatDataItem data, object context)
    {
      return (object) data.GetType() == (object) typeof (CarouselMessageDataItem);
    }

    public override void Synchronize()
    {
      CarouselMessageDataItem data = this.Data as CarouselMessageDataItem;
      HorizontalScrollableStackElement mainMessageElement = this.MainMessageElement as HorizontalScrollableStackElement;
      foreach (BaseChatCardElement child in mainMessageElement.ItemsLayout.Children)
        child.CardActionClicked -= new CardActionEventHandler(this.CardActionClicked);
      mainMessageElement.ItemsLayout.Children.Clear();
      foreach (BaseChatCardDataItem card in data.CarouselMessage.Cards)
      {
        BaseChatCardElement cardElement = data.ChatMessagesViewElement.ChatElement.ChatFactory.CreateCardElement(card);
        cardElement.Margin = new Padding(5, 0, 5, 0);
        cardElement.CardActionClicked += new CardActionEventHandler(this.CardActionClicked);
        mainMessageElement.ItemsLayout.Children.Add((RadElement) cardElement);
      }
      mainMessageElement.ScrollBar.Value = data.ScrollOffset;
      base.Synchronize();
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
      this.MainMessageElement.Measure(new SizeF((float) ((double) availableSize.Width - (double) this.Padding.Horizontal - (this.Data.ChatMessagesViewElement.ShowAvatars ? (double) this.AvatarPictureElement.DesiredSize.Width : 0.0)), float.PositiveInfinity));
      SizeF sizeF2 = new SizeF(this.AvatarPictureElement.DesiredSize.Width + this.MainMessageElement.DesiredSize.Width + (float) this.Margin.Horizontal, Math.Max(this.AvatarPictureElement.DesiredSize.Height, this.MainMessageElement.DesiredSize.Height) + this.NameLabelElement.DesiredSize.Height + (float) this.Margin.Vertical);
      this.Data.ActualSize = sizeF2;
      return sizeF2;
    }
  }
}
