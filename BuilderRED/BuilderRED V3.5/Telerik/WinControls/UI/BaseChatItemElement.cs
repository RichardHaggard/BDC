// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseChatItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class BaseChatItemElement : LightVisualElement, IVirtualizedElement<BaseChatDataItem>
  {
    public static RadProperty IsOwnMessageProperty = RadProperty.Register("IsOwnProperty", typeof (bool), typeof (BaseChatItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private BaseChatDataItem baseDataItem;
    private ChatMessageAvatarElement avatarPicture;
    private LightVisualElement mainMessageElement;
    private ChatMessageNameElement nameLabel;
    private ChatMessageStatusElement statusLabel;

    static BaseChatItemElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new BaseChatItemElementStateManagerFactory(), typeof (BaseChatItemElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(11, 0, 11, 0);
      this.Margin = new Padding(0, 0, 0, 5);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.avatarPicture = this.CreateAvatarElement();
      this.nameLabel = this.CreateNameLabel();
      this.statusLabel = this.CreateStatusLabel();
      this.mainMessageElement = this.CreateMainMessageElement();
      this.Children.Add((RadElement) this.avatarPicture);
      this.Children.Add((RadElement) this.nameLabel);
      this.Children.Add((RadElement) this.statusLabel);
      this.Children.Add((RadElement) this.mainMessageElement);
      int num1 = (int) this.avatarPicture.BindProperty(ChatMessageAvatarElement.IsOwnMessageProperty, (RadObject) this, BaseChatItemElement.IsOwnMessageProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.nameLabel.BindProperty(ChatMessageNameElement.IsOwnMessageProperty, (RadObject) this, BaseChatItemElement.IsOwnMessageProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.statusLabel.BindProperty(ChatMessageStatusElement.IsOwnMessageProperty, (RadObject) this, BaseChatItemElement.IsOwnMessageProperty, PropertyBindingOptions.OneWay);
    }

    protected virtual ChatMessageAvatarElement CreateAvatarElement()
    {
      ChatMessageAvatarElement messageAvatarElement = new ChatMessageAvatarElement();
      messageAvatarElement.Margin = new Padding(5, 0, 5, 0);
      messageAvatarElement.SmoothingMode = SmoothingMode.HighQuality;
      messageAvatarElement.ImageLayout = ImageLayout.Stretch;
      messageAvatarElement.Shape = (ElementShape) new CircleShape();
      return messageAvatarElement;
    }

    protected virtual LightVisualElement CreateMainMessageElement()
    {
      return (LightVisualElement) new ChatMessageBubbleElement();
    }

    protected virtual ChatMessageNameElement CreateNameLabel()
    {
      ChatMessageNameElement messageNameElement = new ChatMessageNameElement();
      messageNameElement.Font = new Font(this.Font.FontFamily, 7.5f);
      messageNameElement.ForeColor = Color.FromArgb(180, Color.Black);
      return messageNameElement;
    }

    protected virtual ChatMessageStatusElement CreateStatusLabel()
    {
      ChatMessageStatusElement messageStatusElement = new ChatMessageStatusElement();
      messageStatusElement.Font = new Font(this.Font.FontFamily, 7.5f);
      messageStatusElement.ForeColor = Color.FromArgb(180, Color.Black);
      return messageStatusElement;
    }

    public ChatMessageAvatarElement AvatarPictureElement
    {
      get
      {
        return this.avatarPicture;
      }
    }

    public LightVisualElement MainMessageElement
    {
      get
      {
        return this.mainMessageElement;
      }
    }

    public ChatMessageNameElement NameLabelElement
    {
      get
      {
        return this.nameLabel;
      }
    }

    public ChatMessageStatusElement StatusLabelElement
    {
      get
      {
        return this.statusLabel;
      }
    }

    public bool IsOwnMessage
    {
      get
      {
        return (bool) this.GetValue(BaseChatItemElement.IsOwnMessageProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseChatItemElement.IsOwnMessageProperty, (object) value);
      }
    }

    public virtual BaseChatDataItem Data
    {
      get
      {
        return this.baseDataItem;
      }
      protected set
      {
        this.baseDataItem = value;
      }
    }

    public virtual void Attach(BaseChatDataItem data, object context)
    {
      this.Data = data;
      this.Synchronize();
      this.Data.PropertyChanged += new PropertyChangedEventHandler(this.DataPropertyChanged);
    }

    protected virtual void DataPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Synchronize();
    }

    public virtual void Detach()
    {
      this.Data.PropertyChanged -= new PropertyChangedEventHandler(this.DataPropertyChanged);
      this.Data = (BaseChatDataItem) null;
    }

    public virtual bool IsCompatible(BaseChatDataItem data, object context)
    {
      return false;
    }

    public virtual void Synchronize()
    {
      if (this.Data.ChatMessagesViewElement.ShowAvatars)
      {
        if (this.Data.MessageType == ChatMessageType.Single || this.Data.MessageType == ChatMessageType.First)
          this.AvatarPictureElement.Visibility = ElementVisibility.Visible;
        else
          this.AvatarPictureElement.Visibility = ElementVisibility.Hidden;
      }
      else
        this.AvatarPictureElement.Visibility = ElementVisibility.Collapsed;
      if (this.Data.MessageType == ChatMessageType.Single || this.Data.MessageType == ChatMessageType.First)
        this.NameLabelElement.Visibility = ElementVisibility.Visible;
      else
        this.NameLabelElement.Visibility = ElementVisibility.Collapsed;
      this.StatusLabelElement.Visibility = this.Data.MessageType == ChatMessageType.Single || this.Data.MessageType == ChatMessageType.Last ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.IsOwnMessage = this.Data.IsOwnMessage;
      this.AvatarPictureElement.Image = this.Data.Message.Author.Avatar;
      this.AvatarPictureElement.MaxSize = TelerikDpiHelper.ScaleSize(Size.Round(this.Data.ChatMessagesViewElement.AvatarSize), new SizeF(1f / this.DpiScaleFactor.Width, 1f / this.DpiScaleFactor.Height));
      this.NameLabelElement.Text = this.GetNameLabelText();
      this.StatusLabelElement.Text = this.Data.Status;
      this.Data.ChatMessagesViewElement.ChatElement.OnItemFormatting(new ChatItemElementEventArgs(this));
    }

    protected virtual string GetNameLabelText()
    {
      if (this.Data.IsOwnMessage)
        return this.Data.Message.TimeStamp.ToShortTimeString();
      return string.Format("{0}, {1}", (object) this.Data.Message.TimeStamp.ToShortTimeString(), (object) this.Data.Message.Author.Name);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      this.Data.ActualSize = sizeF;
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.Data == null)
        return sizeF;
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect1;
      RectangleF finalRect2;
      RectangleF finalRect3;
      if (this.Data.ChatMessagesViewElement.ShowAvatars)
      {
        RectangleF finalRect4;
        if (this.Data.ChatMessagesViewElement.ShowMessagesOnOneSide || !this.Data.IsOwnMessage)
        {
          finalRect4 = new RectangleF(clientRectangle.X, clientRectangle.Y + this.NameLabelElement.DesiredSize.Height, this.AvatarPictureElement.DesiredSize.Width, this.AvatarPictureElement.DesiredSize.Height);
          finalRect1 = new RectangleF(finalRect4.Right, clientRectangle.Y, this.NameLabelElement.DesiredSize.Width, this.NameLabelElement.DesiredSize.Height);
          finalRect2 = new RectangleF(clientRectangle.X + this.AvatarPictureElement.DesiredSize.Width, clientRectangle.Y + this.NameLabelElement.DesiredSize.Height, this.MainMessageElement.DesiredSize.Width, this.MainMessageElement.DesiredSize.Height);
          finalRect3 = new RectangleF(finalRect2.X, finalRect2.Bottom, this.StatusLabelElement.DesiredSize.Width, this.StatusLabelElement.DesiredSize.Height);
        }
        else
        {
          finalRect4 = new RectangleF(clientRectangle.Right - this.AvatarPictureElement.DesiredSize.Width, clientRectangle.Y + this.NameLabelElement.DesiredSize.Height, this.AvatarPictureElement.DesiredSize.Width, this.AvatarPictureElement.DesiredSize.Height);
          finalRect1 = new RectangleF(finalRect4.X - this.NameLabelElement.DesiredSize.Width, clientRectangle.Y, this.NameLabelElement.DesiredSize.Width, this.NameLabelElement.DesiredSize.Height);
          finalRect2 = new RectangleF(clientRectangle.Right - this.AvatarPictureElement.DesiredSize.Width - this.MainMessageElement.DesiredSize.Width, clientRectangle.Y + this.NameLabelElement.DesiredSize.Height, this.MainMessageElement.DesiredSize.Width, this.MainMessageElement.DesiredSize.Height);
          finalRect3 = new RectangleF(finalRect2.Right - this.StatusLabelElement.DesiredSize.Width, finalRect2.Bottom, this.StatusLabelElement.DesiredSize.Width, this.StatusLabelElement.DesiredSize.Height);
        }
        this.AvatarPictureElement.Arrange(finalRect4);
      }
      else if (this.Data.ChatMessagesViewElement.ShowMessagesOnOneSide || !this.Data.IsOwnMessage)
      {
        finalRect1 = new RectangleF(clientRectangle.X, clientRectangle.Y, this.NameLabelElement.DesiredSize.Width, this.NameLabelElement.DesiredSize.Height);
        finalRect2 = new RectangleF(clientRectangle.X, finalRect1.Bottom, this.MainMessageElement.DesiredSize.Width, this.MainMessageElement.DesiredSize.Height);
        finalRect3 = new RectangleF(finalRect2.X, finalRect2.Bottom, this.StatusLabelElement.DesiredSize.Width, this.StatusLabelElement.DesiredSize.Height);
      }
      else
      {
        finalRect1 = new RectangleF(clientRectangle.Right - this.NameLabelElement.DesiredSize.Width, clientRectangle.Y, this.NameLabelElement.DesiredSize.Width, this.NameLabelElement.DesiredSize.Height);
        finalRect2 = new RectangleF(clientRectangle.Right - this.MainMessageElement.DesiredSize.Width, finalRect1.Bottom, this.MainMessageElement.DesiredSize.Width, this.MainMessageElement.DesiredSize.Height);
        finalRect3 = new RectangleF(finalRect2.Right - this.StatusLabelElement.DesiredSize.Width, finalRect2.Bottom, this.StatusLabelElement.DesiredSize.Width, this.StatusLabelElement.DesiredSize.Height);
      }
      this.NameLabelElement.Arrange(finalRect1);
      this.MainMessageElement.Arrange(finalRect2);
      this.StatusLabelElement.Arrange(finalRect3);
      return sizeF;
    }
  }
}
