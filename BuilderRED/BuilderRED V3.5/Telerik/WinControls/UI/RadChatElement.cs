// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadChatElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Paint;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class RadChatElement : LightVisualElement
  {
    private ChatFactory chatFactory = new ChatFactory();
    private bool autoAddUserMessages = true;
    private Author author;
    private StackLayoutElement stack;
    private ChatMessagesViewElement messagesViewElement;
    private ChatSuggestedActionsElement suggestedActionsElement;
    private RadTextBoxElement inputTextBox;
    private ChatToolbarElement toolbarElement;
    private ChatSendButtonElement sendButton;
    private ChatShowToolbarElement showToolbarButton;
    private bool isOverlayShown;
    private bool isPopupOverlayShown;
    private LightVisualElement overlayElement;
    private Telerik.WinControls.UI.OverlayPopupElement overlayPopupElement;
    private LightVisualElement typingIndicator;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stack = this.CreateStackLayoutElement();
      this.messagesViewElement = this.CreateMessagesListView();
      this.overlayPopupElement = this.CreateOverlayPopupElement();
      this.suggestedActionsElement = this.CreateSuggestedActionsElement();
      this.inputTextBox = this.CreateInputTextBox();
      this.toolbarElement = this.CreateToolbarElement();
      this.typingIndicator = this.CreateTypingIndicatorElement();
      this.showToolbarButton = this.CreateShowToolbarButton();
      this.sendButton = this.CreateSendButton();
      this.inputTextBox.ButtonsStack.Children.Add((RadElement) this.showToolbarButton);
      this.inputTextBox.ButtonsStack.Children.Add((RadElement) this.sendButton);
      this.stack.Children.Add((RadElement) this.messagesViewElement);
      this.stack.Children.Add((RadElement) this.overlayPopupElement);
      this.stack.Children.Add((RadElement) this.suggestedActionsElement);
      this.stack.Children.Add((RadElement) this.typingIndicator);
      this.stack.Children.Add((RadElement) this.inputTextBox);
      this.stack.Children.Add((RadElement) this.toolbarElement);
      this.Children.Add((RadElement) this.stack);
      this.overlayElement = new LightVisualElement();
      this.overlayElement.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.overlayElement);
    }

    public RadChatElement()
    {
      this.WireEvents();
    }

    protected virtual StackLayoutElement CreateStackLayoutElement()
    {
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.FitInAvailableSize = true;
      stackLayoutElement.Orientation = Orientation.Vertical;
      stackLayoutElement.StretchHorizontally = true;
      stackLayoutElement.StretchVertically = true;
      return stackLayoutElement;
    }

    protected virtual ChatSendButtonElement CreateSendButton()
    {
      ChatSendButtonElement sendButtonElement = new ChatSendButtonElement();
      sendButtonElement.Padding = new Padding(5);
      return sendButtonElement;
    }

    protected virtual ChatShowToolbarElement CreateShowToolbarButton()
    {
      ChatShowToolbarElement showToolbarElement = new ChatShowToolbarElement();
      showToolbarElement.Font = new Font(this.Font.FontFamily, 20f);
      showToolbarElement.Text = "•••";
      return showToolbarElement;
    }

    protected virtual ChatToolbarElement CreateToolbarElement()
    {
      ChatToolbarElement chatToolbarElement = new ChatToolbarElement(this);
      chatToolbarElement.StretchVertically = false;
      return chatToolbarElement;
    }

    protected virtual RadTextBoxElement CreateInputTextBox()
    {
      RadTextBoxElement radTextBoxElement = (RadTextBoxElement) new ChatInputTextBoxElement();
      radTextBoxElement.StretchVertically = false;
      radTextBoxElement.TextBoxItem.NullText = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("TypeAMessage");
      return radTextBoxElement;
    }

    protected virtual ChatSuggestedActionsElement CreateSuggestedActionsElement()
    {
      ChatSuggestedActionsElement suggestedActionsElement = new ChatSuggestedActionsElement(this);
      suggestedActionsElement.StretchVertically = false;
      suggestedActionsElement.Visibility = ElementVisibility.Collapsed;
      return suggestedActionsElement;
    }

    protected virtual Telerik.WinControls.UI.OverlayPopupElement CreateOverlayPopupElement()
    {
      Telerik.WinControls.UI.OverlayPopupElement overlayPopupElement = new Telerik.WinControls.UI.OverlayPopupElement();
      overlayPopupElement.Visibility = ElementVisibility.Collapsed;
      return overlayPopupElement;
    }

    protected virtual ChatMessagesViewElement CreateMessagesListView()
    {
      ChatMessagesViewElement messagesViewElement = new ChatMessagesViewElement(this);
      messagesViewElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      return messagesViewElement;
    }

    protected virtual LightVisualElement CreateTypingIndicatorElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.typing_indicator_light;
      lightVisualElement.StretchHorizontally = true;
      lightVisualElement.StretchVertically = false;
      lightVisualElement.ImageAlignment = ContentAlignment.MiddleCenter;
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Visibility = ElementVisibility.Collapsed;
      return lightVisualElement;
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    public virtual Author Author
    {
      get
      {
        return this.author;
      }
      set
      {
        this.author = value;
      }
    }

    public bool AutoAddUserMessages
    {
      get
      {
        return this.autoAddUserMessages;
      }
      set
      {
        this.autoAddUserMessages = value;
      }
    }

    public ChatMessagesViewElement MessagesViewElement
    {
      get
      {
        return this.messagesViewElement;
      }
    }

    public ChatSuggestedActionsElement SuggestedActionsElement
    {
      get
      {
        return this.suggestedActionsElement;
      }
    }

    public ChatToolbarElement ToolbarElement
    {
      get
      {
        return this.toolbarElement;
      }
    }

    public RadTextBoxElement InputTextBox
    {
      get
      {
        return this.inputTextBox;
      }
    }

    public ChatShowToolbarElement ShowToolbarButtonElement
    {
      get
      {
        return this.showToolbarButton;
      }
    }

    public ChatSendButtonElement SendButtonElement
    {
      get
      {
        return this.sendButton;
      }
    }

    public LightVisualElement OverlayElement
    {
      get
      {
        return this.overlayElement;
      }
    }

    public LightVisualElement OverlayPopupElement
    {
      get
      {
        return (LightVisualElement) this.overlayPopupElement;
      }
    }

    public SizeF AvatarSize
    {
      get
      {
        return this.MessagesViewElement.AvatarSize;
      }
      set
      {
        this.MessagesViewElement.AvatarSize = value;
      }
    }

    public bool ShowAvatars
    {
      get
      {
        return this.MessagesViewElement.ShowAvatars;
      }
      set
      {
        this.MessagesViewElement.ShowAvatars = value;
      }
    }

    public bool ShowMessagesOnOneSide
    {
      get
      {
        return this.MessagesViewElement.ShowMessagesOnOneSide;
      }
      set
      {
        this.MessagesViewElement.ShowMessagesOnOneSide = value;
      }
    }

    public TimeSpan TimeSeparatorInterval
    {
      get
      {
        return this.MessagesViewElement.TimeSeparatorInterval;
      }
      set
      {
        this.MessagesViewElement.TimeSeparatorInterval = value;
      }
    }

    public bool IsOverlayShown
    {
      get
      {
        return this.isOverlayShown;
      }
      protected set
      {
        this.isOverlayShown = value;
      }
    }

    public bool IsPopupOverlayShown
    {
      get
      {
        return this.isPopupOverlayShown;
      }
      protected set
      {
        this.isPopupOverlayShown = value;
      }
    }

    public ChatFactory ChatFactory
    {
      get
      {
        return this.chatFactory;
      }
      set
      {
        this.chatFactory = value;
      }
    }

    protected virtual void WireEvents()
    {
      this.InputTextBox.KeyDown += new KeyEventHandler(this.OnInputTextBoxKeyDown);
      this.ShowToolbarButtonElement.Click += new EventHandler(this.OnShowToolbarButtonElementClick);
      this.SendButtonElement.Click += new EventHandler(this.OnSendButtonElementClick);
      LocalizationProvider<ChatLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.ChatLocalizationProvider_CurrentProviderChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.InputTextBox.KeyDown -= new KeyEventHandler(this.OnInputTextBoxKeyDown);
      this.ShowToolbarButtonElement.Click -= new EventHandler(this.OnShowToolbarButtonElementClick);
      this.SendButtonElement.Click -= new EventHandler(this.OnSendButtonElementClick);
      LocalizationProvider<ChatLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.ChatLocalizationProvider_CurrentProviderChanged);
    }

    public virtual void ShowTypingIndicator(params Author[] authors)
    {
      this.typingIndicator.Visibility = ElementVisibility.Visible;
      if (authors == null || authors.Length == 0)
      {
        this.typingIndicator.Image = !TelerikHelper.IsDarkTheme(this.ElementTree.ThemeName) ? (Image) Telerik\u002EWinControls\u002EUI\u002EResources.typing_indicator_light : (Image) Telerik\u002EWinControls\u002EUI\u002EResources.typing_indicator_dark;
        Application.DoEvents();
      }
      else
      {
        this.typingIndicator.Image = (Image) null;
        StringBuilder stringBuilder = new StringBuilder();
        if (authors.Length == 1)
        {
          stringBuilder.Append(authors[0].Name).Append(" is typing...");
        }
        else
        {
          stringBuilder.Append(authors[0].Name);
          for (int index = 1; index < authors.Length - 1; ++index)
            stringBuilder.Append(", ").Append(authors[index].Name);
          stringBuilder.Append(" and ").Append(authors[authors.Length - 1].Name).Append(" are typing...");
        }
        this.typingIndicator.Text = stringBuilder.ToString();
      }
    }

    public virtual void HideTypingIndicator()
    {
      this.typingIndicator.Visibility = ElementVisibility.Collapsed;
      this.typingIndicator.Text = string.Empty;
    }

    public virtual void ShowOverlay(ChatOverlayMessage message)
    {
      if (this.IsOverlayShown || this.IsPopupOverlayShown)
      {
        if (this.IsOverlayShown)
        {
          BaseChatOverlay child = this.OverlayElement.Children[0] as BaseChatOverlay;
          if (child != null)
            child.ChatElement = (RadChatElement) null;
          this.OverlayElement.Children.Clear();
          this.OverlayElement.Visibility = ElementVisibility.Collapsed;
          this.InputTextBox.Visibility = ElementVisibility.Visible;
          this.IsOverlayShown = false;
        }
        else if (this.IsPopupOverlayShown)
        {
          BaseChatOverlay child = this.OverlayPopupElement.Children[0] as BaseChatOverlay;
          if (child != null)
            child.ChatElement = (RadChatElement) null;
          this.OverlayPopupElement.Children.Clear();
          this.OverlayPopupElement.Visibility = ElementVisibility.Collapsed;
          this.IsPopupOverlayShown = false;
        }
      }
      message.OverlayElement.ChatElement = this;
      if (message.ShowAsPopup)
      {
        message.OverlayElement.PrepareForPopupDisplay();
        this.OverlayPopupElement.Children.Add((RadElement) message.OverlayElement);
        this.OverlayPopupElement.Visibility = ElementVisibility.Visible;
        this.IsPopupOverlayShown = true;
      }
      else
      {
        message.OverlayElement.PrepareForOverlayDisplay();
        this.OverlayElement.Children.Add((RadElement) message.OverlayElement);
        this.OverlayElement.Visibility = ElementVisibility.Visible;
        this.InputTextBox.Visibility = ElementVisibility.Collapsed;
        this.IsOverlayShown = true;
      }
      message.OverlayElement.CurrentValue = message.OverlayElement.CurrentValue;
      foreach (RadHostItem descendant in message.OverlayElement.GetDescendants((Predicate<RadElement>) (x => x is RadHostItem), TreeTraversalMode.BreadthFirst))
      {
        if (!string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName))
        {
          ThemeResolutionService.ApplyThemeToControlTree(descendant.HostedControl, ThemeResolutionService.ApplicationThemeName);
        }
        else
        {
          string themeName = this.ElementTree.ThemeName;
          if (string.IsNullOrEmpty(themeName))
            themeName = "ControlDefault";
          ThemeResolutionService.ApplyThemeToControlTree(descendant.HostedControl, themeName);
        }
      }
    }

    public virtual void HideOverlay()
    {
      if (this.IsOverlayShown)
      {
        BaseChatOverlay child = this.OverlayElement.Children[0] as BaseChatOverlay;
        if (child != null)
          child.ChatElement = (RadChatElement) null;
        this.OverlayElement.Children.Clear();
        this.OverlayElement.Visibility = ElementVisibility.Collapsed;
        this.InputTextBox.Visibility = ElementVisibility.Visible;
        this.IsOverlayShown = false;
      }
      else if (this.IsPopupOverlayShown)
      {
        BaseChatOverlay child = this.OverlayPopupElement.Children[0] as BaseChatOverlay;
        if (child != null)
          child.ChatElement = (RadChatElement) null;
        this.OverlayPopupElement.Children.Clear();
        this.OverlayPopupElement.Visibility = ElementVisibility.Collapsed;
        this.IsPopupOverlayShown = false;
      }
      this.InputTextBox.Focus();
    }

    public virtual void AddMessage(ChatMessage message)
    {
      if (message.Author == null)
        throw new ArgumentNullException("Author", "An author must be specified for each message added.");
      bool flag = this.MessagesViewElement.VScrollBar.Value == this.MessagesViewElement.VScrollBar.Maximum - this.MessagesViewElement.VScrollBar.LargeChange + 1;
      BaseChatDataItem dataItem = this.ChatFactory.CreateDataItem(message);
      if (dataItem != null)
        this.MessagesViewElement.Items.Add(dataItem);
      ChatSuggestedActionsMessage suggestedActionsMessage = message as ChatSuggestedActionsMessage;
      if (suggestedActionsMessage != null)
      {
        this.SuggestedActionsElement.ClearActions();
        this.SuggestedActionsElement.AddActions(suggestedActionsMessage.SuggestedActions);
        this.SuggestedActionsElement.Visibility = ElementVisibility.Visible;
      }
      ChatOverlayMessage message1 = message as ChatOverlayMessage;
      if (message1 != null)
        this.ShowOverlay(message1);
      if (!flag)
        return;
      for (int index = 0; index < 3; ++index)
      {
        this.MessagesViewElement.VScrollBar.Value = this.MessagesViewElement.VScrollBar.Maximum - this.MessagesViewElement.VScrollBar.LargeChange + 1;
        Application.DoEvents();
      }
    }

    protected override void PostPaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale)
    {
      base.PostPaintChildren(graphics, clipRectange, angle, scale);
      this.PaintBorder(graphics, angle, scale);
    }

    protected virtual void OnInputTextBoxKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Return)
      {
        this.SubmitUserMessage();
      }
      else
      {
        if (!e.Control || e.KeyCode != Keys.V || !Clipboard.ContainsImage())
          return;
        Image image = Clipboard.GetImage();
        Size size = image.Size;
        if (image.Width > 250)
        {
          float num = (float) image.Width / 250f;
          size.Width = (int) Math.Round((double) image.Width / (double) num, MidpointRounding.AwayFromZero);
          size.Height = (int) Math.Round((double) image.Height / (double) num, MidpointRounding.AwayFromZero);
        }
        ChatMediaMessage chatMediaMessage = new ChatMediaMessage(image, size, this.Author, DateTime.Now, (object) null);
        if (this.AutoAddUserMessages)
          this.AddMessage((ChatMessage) chatMediaMessage);
        else
          this.OnSendMessage(new SendMessageEventArgs((ChatMessage) chatMediaMessage));
      }
    }

    protected virtual void OnSendButtonElementClick(object sender, EventArgs e)
    {
      if (TelerikHelper.StringIsNullOrWhiteSpace(this.InputTextBox.Text))
        return;
      this.SubmitUserMessage();
    }

    protected virtual void OnShowToolbarButtonElementClick(object sender, EventArgs e)
    {
      if (this.ToolbarElement.Visibility == ElementVisibility.Collapsed)
        this.ToolbarElement.Visibility = ElementVisibility.Visible;
      else
        this.ToolbarElement.Visibility = ElementVisibility.Collapsed;
    }

    private void ChatLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.InputTextBox.TextBoxItem.NullText = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("TypeAMessage");
    }

    protected virtual void SubmitUserMessage()
    {
      string text = this.inputTextBox.Text;
      if (TelerikHelper.StringIsNullOrWhiteSpace(text))
        return;
      this.inputTextBox.Text = string.Empty;
      ChatTextMessage chatTextMessage = new ChatTextMessage(text, this.Author, DateTime.Now);
      if (this.AutoAddUserMessages)
      {
        if (this.Author == null)
          throw new ArgumentNullException("Author", "The Author property has to be set to a valid instance when AutoAddUserMessages is set to true.");
        this.AddMessage((ChatMessage) chatTextMessage);
      }
      if (this.IsPopupOverlayShown)
        this.HideOverlay();
      this.OnSendMessage(new SendMessageEventArgs((ChatMessage) chatTextMessage));
    }

    public event SendMessageEventHandler SendMessage;

    protected internal virtual void OnSendMessage(SendMessageEventArgs e)
    {
      if (this.SendMessage == null)
        return;
      this.SendMessage((object) this, e);
    }

    public event SuggestedActionEventHandler SuggestedActionClicked;

    protected internal virtual void OnSuggestedActionClicked(SuggestedActionEventArgs e)
    {
      if (this.SuggestedActionClicked == null)
        return;
      this.SuggestedActionClicked((object) this, e);
    }

    public event CardActionEventHandler CardActionClicked;

    protected internal virtual void OnCardActionClicked(object sender, CardActionEventArgs e)
    {
      if (this.CardActionClicked == null)
        return;
      this.CardActionClicked(sender, e);
    }

    public event ToolbarActionEventHandler ToolbarActionClicked;

    protected internal virtual void OnToolbarActionClick(ToolbarActionEventArgs e)
    {
      if (this.ToolbarActionClicked == null)
        return;
      this.ToolbarActionClicked((object) this, e);
    }

    public event TimeSeparatorEventHandler TimeSeparatorAdding;

    protected internal virtual void OnTimeSeparatorAdding(TimeSeparatorEventArgs e)
    {
      if (this.TimeSeparatorAdding == null)
        return;
      this.TimeSeparatorAdding((object) this, e);
    }

    public event ChatItemElementEventHandler ItemFormatting;

    protected internal virtual void OnItemFormatting(ChatItemElementEventArgs e)
    {
      if (this.ItemFormatting == null)
        return;
      this.ItemFormatting((object) this, e);
    }
  }
}
