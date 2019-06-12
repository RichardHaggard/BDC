// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadChat
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Docking(DockingBehavior.Ask)]
  [ToolboxItem(true)]
  [TelerikToolboxCategory("Data Controls")]
  [Description("")]
  public class RadChat : RadControl
  {
    private RadChatElement chatElement;

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.chatElement = this.CreateChatElement();
      parent.Children.Add((RadElement) this.chatElement);
    }

    private RadChatElement CreateChatElement()
    {
      return new RadChatElement();
    }

    public RadChatElement ChatElement
    {
      get
      {
        return this.chatElement;
      }
    }

    [DefaultValue(null)]
    public Author Author
    {
      get
      {
        return this.ChatElement.Author;
      }
      set
      {
        this.ChatElement.Author = value;
      }
    }

    [DefaultValue(true)]
    public bool AutoAddUserMessages
    {
      get
      {
        return this.ChatElement.AutoAddUserMessages;
      }
      set
      {
        this.ChatElement.AutoAddUserMessages = value;
      }
    }

    [DefaultValue(typeof (SizeF), "28, 28")]
    public SizeF AvatarSize
    {
      get
      {
        return this.ChatElement.AvatarSize;
      }
      set
      {
        this.ChatElement.AvatarSize = value;
      }
    }

    [DefaultValue(true)]
    public bool ShowAvatars
    {
      get
      {
        return this.ChatElement.ShowAvatars;
      }
      set
      {
        this.ChatElement.ShowAvatars = value;
      }
    }

    [DefaultValue(false)]
    public bool ShowMessagesOnOneSide
    {
      get
      {
        return this.ChatElement.ShowMessagesOnOneSide;
      }
      set
      {
        this.ChatElement.ShowMessagesOnOneSide = value;
      }
    }

    [DefaultValue(typeof (TimeSpan), "24, 0, 0")]
    public TimeSpan TimeSeparatorInterval
    {
      get
      {
        return this.ChatElement.TimeSeparatorInterval;
      }
      set
      {
        this.ChatElement.TimeSeparatorInterval = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(360, 500);
      }
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.ChatElement.InputTextBox.Focus();
    }

    public virtual void ShowOverlay(ChatOverlayMessage message)
    {
      this.ChatElement.ShowOverlay(message);
    }

    public virtual void HideOverlay()
    {
      this.ChatElement.HideOverlay();
    }

    public void AddMessage(ChatMessage message)
    {
      this.ChatElement.AddMessage(message);
    }

    public event SendMessageEventHandler SendMessage
    {
      add
      {
        this.ChatElement.SendMessage += value;
      }
      remove
      {
        this.ChatElement.SendMessage -= value;
      }
    }

    public event SuggestedActionEventHandler SuggestedActionClicked
    {
      add
      {
        this.ChatElement.SuggestedActionClicked += value;
      }
      remove
      {
        this.ChatElement.SuggestedActionClicked -= value;
      }
    }

    public event CardActionEventHandler CardActionClicked
    {
      add
      {
        this.ChatElement.CardActionClicked += value;
      }
      remove
      {
        this.ChatElement.CardActionClicked -= value;
      }
    }

    public event ToolbarActionEventHandler ToolbarActionClicked
    {
      add
      {
        this.ChatElement.ToolbarActionClicked += value;
      }
      remove
      {
        this.ChatElement.ToolbarActionClicked -= value;
      }
    }

    public event TimeSeparatorEventHandler TimeSeparatorAdding
    {
      add
      {
        this.ChatElement.TimeSeparatorAdding += value;
      }
      remove
      {
        this.ChatElement.TimeSeparatorAdding -= value;
      }
    }

    public event ChatItemElementEventHandler ItemFormatting
    {
      add
      {
        this.ChatElement.ItemFormatting += value;
      }
      remove
      {
        this.ChatElement.ItemFormatting -= value;
      }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      int num1 = e.Delta / SystemInformation.MouseWheelScrollDelta;
      RadScrollBarElement scrollbar = this.ChatElement.MessagesViewElement.Scroller.Scrollbar;
      int num2 = scrollbar.Value - num1 * scrollbar.SmallChange;
      if (num2 > scrollbar.Maximum - scrollbar.LargeChange + 1)
        num2 = scrollbar.Maximum - scrollbar.LargeChange + 1;
      if (num2 < scrollbar.Minimum)
        num2 = scrollbar.Minimum;
      scrollbar.Value = num2;
    }
  }
}
