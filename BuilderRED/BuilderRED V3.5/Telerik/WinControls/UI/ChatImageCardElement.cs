// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatImageCardElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ChatImageCardElement : BaseChatCardElement
  {
    private StackLayoutPanel stackElement;
    private LightVisualElement imageElement;
    private LightVisualElement titleElement;
    private LightVisualElement subtitleElement;
    private LightVisualElement textElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MaxSize = new Size(250, 0);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.stackElement = this.CreateStackElement();
      this.imageElement = this.CreateImageElement();
      this.titleElement = this.CreateTitleElement();
      this.subtitleElement = this.CreateSubtitleElement();
      this.textElement = this.CreateTextElement();
      this.stackElement.Children.Add((RadElement) this.imageElement);
      this.stackElement.Children.Add((RadElement) this.titleElement);
      this.stackElement.Children.Add((RadElement) this.subtitleElement);
      this.stackElement.Children.Add((RadElement) this.textElement);
      this.Children.Add((RadElement) this.stackElement);
    }

    public ChatImageCardElement(ChatImageCardDataItem item)
      : base((BaseChatCardDataItem) item)
    {
    }

    private void Action_Click(object sender, EventArgs e)
    {
      ChatImageCardDataItem dataItem = this.DataItem as ChatImageCardDataItem;
      LightVisualElement lightVisualElement = sender as LightVisualElement;
      foreach (ChatCardAction action in dataItem.Actions)
      {
        if (action.Text == lightVisualElement.Text)
          this.OnCardActionClicked(new CardActionEventArgs(action, this.DataItem.UserData));
      }
    }

    protected virtual StackLayoutPanel CreateStackElement()
    {
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Vertical;
      stackLayoutPanel.ShouldHandleMouseInput = false;
      return stackLayoutPanel;
    }

    protected virtual LightVisualElement CreateImageElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.ImageLayout = ImageLayout.Zoom;
      lightVisualElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      lightVisualElement.Shape = (ElementShape) new RoundRectShape(20, true, false, true, false);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateTitleElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10, 10, 10, 0);
      lightVisualElement.TextWrap = true;
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 16f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateSubtitleElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10, 0, 10, 0);
      lightVisualElement.TextWrap = true;
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    protected virtual LightVisualElement CreateTextElement()
    {
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Padding = new Padding(10);
      lightVisualElement.TextWrap = true;
      lightVisualElement.TextAlignment = ContentAlignment.MiddleLeft;
      lightVisualElement.Font = new Font(this.Font.FontFamily, 10f);
      lightVisualElement.ShouldHandleMouseInput = false;
      return lightVisualElement;
    }

    public StackLayoutPanel StackElement
    {
      get
      {
        return this.stackElement;
      }
    }

    public LightVisualElement ImageElement
    {
      get
      {
        return this.imageElement;
      }
    }

    public LightVisualElement TitleElement
    {
      get
      {
        return this.titleElement;
      }
    }

    public LightVisualElement SubtitleElement
    {
      get
      {
        return this.subtitleElement;
      }
    }

    public LightVisualElement TextElement
    {
      get
      {
        return this.textElement;
      }
    }

    protected override void Synchronise()
    {
      base.Synchronise();
      ChatImageCardDataItem dataItem = this.DataItem as ChatImageCardDataItem;
      this.ImageElement.Image = dataItem.Image;
      this.TitleElement.Text = dataItem.Title;
      this.SubtitleElement.Text = dataItem.Subtitle;
      this.TextElement.Text = dataItem.Text;
      while (this.StackElement.Children.Count > 4)
        this.StackElement.Children.RemoveAt(4);
      if (dataItem.Actions == null)
        return;
      foreach (ChatCardAction action in dataItem.Actions)
      {
        LightVisualElement lightVisualElement = new LightVisualElement();
        lightVisualElement.Text = action.Text;
        lightVisualElement.Padding = new Padding(0, 8, 0, 8);
        lightVisualElement.TextAlignment = ContentAlignment.MiddleCenter;
        lightVisualElement.BorderBoxStyle = BorderBoxStyle.FourBorders;
        lightVisualElement.BorderTopColor = Color.Gray;
        lightVisualElement.BorderTopShadowColor = Color.Gray;
        lightVisualElement.BorderTopWidth = 2f;
        lightVisualElement.BorderLeftWidth = 0.0f;
        lightVisualElement.BorderRightWidth = 0.0f;
        lightVisualElement.BorderBottomWidth = 0.0f;
        lightVisualElement.DrawBorder = true;
        lightVisualElement.SmoothingMode = SmoothingMode.AntiAlias;
        lightVisualElement.Click += new EventHandler(this.Action_Click);
        this.StackElement.Children.Add((RadElement) lightVisualElement);
      }
    }
  }
}
