// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseChatItemOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public abstract class BaseChatItemOverlay : BaseChatOverlay
  {
    private LightVisualElement titleElement;
    private LightVisualElement currentValueElement;
    private LightVisualElement actionsBar;
    private RadButtonElement ok;
    private RadButtonElement cancel;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      StackLayoutElement stackLayoutElement1 = new StackLayoutElement();
      stackLayoutElement1.Orientation = Orientation.Vertical;
      stackLayoutElement1.StretchHorizontally = true;
      stackLayoutElement1.StretchVertically = true;
      this.titleElement = this.CreateTitleElement();
      this.actionsBar = this.CreateActionsBarElement();
      this.ok = this.CreateOkButton();
      this.cancel = this.CreateCancelButton();
      StackLayoutElement stackLayoutElement2 = new StackLayoutElement();
      stackLayoutElement2.Orientation = Orientation.Horizontal;
      stackLayoutElement2.Children.Add((RadElement) this.ok);
      stackLayoutElement2.Children.Add((RadElement) this.cancel);
      this.actionsBar.Children.Add((RadElement) stackLayoutElement2);
      this.currentValueElement = this.CreateCurrentValueElement();
      stackLayoutElement1.Children.Add((RadElement) this.titleElement);
      stackLayoutElement1.Children.Add(this.CreateMainElement());
      stackLayoutElement1.Children.Add((RadElement) this.currentValueElement);
      stackLayoutElement1.Children.Add((RadElement) this.actionsBar);
      this.Children.Add((RadElement) stackLayoutElement1);
    }

    protected virtual LightVisualElement CreateTitleElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.TextWrap = true;
      lightVisualElement.StretchVertically = false;
      lightVisualElement.DrawFill = true;
      lightVisualElement.GradientStyle = GradientStyles.Solid;
      lightVisualElement.Padding = new Padding(10);
      return lightVisualElement;
    }

    protected virtual RadButtonElement CreateOkButton()
    {
      RadButtonElement radButtonElement = new RadButtonElement();
      radButtonElement.Padding = new Padding(3);
      radButtonElement.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("OverlayOK");
      return radButtonElement;
    }

    protected virtual RadButtonElement CreateCancelButton()
    {
      RadButtonElement radButtonElement = new RadButtonElement();
      radButtonElement.Padding = new Padding(3);
      radButtonElement.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("OverlayCancel");
      return radButtonElement;
    }

    protected virtual LightVisualElement CreateActionsBarElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.StretchVertically = false;
      lightVisualElement.DrawFill = true;
      lightVisualElement.GradientStyle = GradientStyles.Solid;
      lightVisualElement.Padding = new Padding(10);
      lightVisualElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
      lightVisualElement.BorderTopWidth = 1f;
      lightVisualElement.BorderLeftWidth = 0.0f;
      lightVisualElement.BorderRightWidth = 0.0f;
      lightVisualElement.BorderBottomWidth = 0.0f;
      lightVisualElement.DrawBorder = true;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateCurrentValueElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.StretchVertically = false;
      lightVisualElement.DrawFill = true;
      lightVisualElement.GradientStyle = GradientStyles.Solid;
      lightVisualElement.Padding = new Padding(10);
      return lightVisualElement;
    }

    protected abstract RadElement CreateMainElement();

    public BaseChatItemOverlay(string title)
    {
      this.ok.Click += new EventHandler(this.Ok_Click);
      this.cancel.Click += new EventHandler(this.Cancel_Click);
      this.titleElement.Text = title;
    }

    public RadButtonElement OkButton
    {
      get
      {
        return this.ok;
      }
    }

    [Browsable(false)]
    [Obsolete("Use the CancelButton property instead.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadButtonElement OkCancel
    {
      get
      {
        return this.cancel;
      }
    }

    public RadButtonElement CancelButton
    {
      get
      {
        return this.cancel;
      }
    }

    public override void PrepareForPopupDisplay()
    {
      base.PrepareForPopupDisplay();
      this.titleElement.Visibility = ElementVisibility.Collapsed;
      this.currentValueElement.Visibility = ElementVisibility.Collapsed;
      this.actionsBar.Visibility = ElementVisibility.Collapsed;
    }

    public override void PrepareForOverlayDisplay()
    {
      base.PrepareForOverlayDisplay();
      this.OkButton.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("OverlayOK");
      this.CancelButton.Text = LocalizationProvider<ChatLocalizationProvider>.CurrentProvider.GetLocalizedString("OverlayCancel");
      this.titleElement.Visibility = ElementVisibility.Visible;
      this.currentValueElement.Visibility = ElementVisibility.Visible;
      this.actionsBar.Visibility = ElementVisibility.Visible;
    }

    protected override void SetCurrentValue(object value)
    {
      base.SetCurrentValue(value);
      if (this.IsInline)
      {
        this.ChatElement.InputTextBox.Text = this.GetDisplayString(value);
        this.ChatElement.InputTextBox.Focus();
      }
      else
        this.currentValueElement.Text = this.GetDisplayString(value);
    }

    protected virtual string GetDisplayString(object value)
    {
      return string.Concat(value);
    }

    private void Ok_Click(object sender, EventArgs e)
    {
      this.OnOkClicked();
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
      this.OnCancelClicked();
    }

    protected virtual void OnOkClicked()
    {
      RadChatElement chatElement = this.ChatElement;
      ChatTextMessage chatTextMessage = new ChatTextMessage(Convert.ToString(this.CurrentValue), chatElement.Author, DateTime.Now);
      if (chatElement.AutoAddUserMessages)
        chatElement.AddMessage((ChatMessage) chatTextMessage);
      chatElement.HideOverlay();
      chatElement.OnSendMessage(new SendMessageEventArgs((ChatMessage) chatTextMessage));
    }

    protected virtual void OnCancelClicked()
    {
      this.ChatElement.HideOverlay();
    }
  }
}
