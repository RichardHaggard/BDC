// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatTimeSeparatorItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatTimeSeparatorItemElement : BaseChatItemElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Margin = new Padding(0);
    }

    public ChatTimeSeparatorItemElement()
    {
      this.AvatarPictureElement.Visibility = ElementVisibility.Collapsed;
      this.NameLabelElement.Visibility = ElementVisibility.Collapsed;
      this.StatusLabelElement.Visibility = ElementVisibility.Collapsed;
      this.MainMessageElement.Visibility = ElementVisibility.Collapsed;
    }

    public override bool IsCompatible(BaseChatDataItem data, object context)
    {
      return (object) data.GetType() == (object) typeof (ChatTimeSeparatorDataItem);
    }

    public override void Synchronize()
    {
      BaseChatDataItem data = this.Data;
      if (this.Data.Message.TimeStamp.Date == DateTime.Now.Date)
        this.Text = "TODAY";
      else if (this.Data.Message.TimeStamp.Date == DateTime.Now.Date.AddDays(-1.0))
        this.Text = "YESTERDAY";
      else
        this.Text = string.Format("{0:ddd dd,yyyy}", (object) this.Data.Message.TimeStamp).ToUpper();
      this.Data.ChatMessagesViewElement.ChatElement.OnItemFormatting(new ChatItemElementEventArgs((BaseChatItemElement) this));
    }
  }
}
