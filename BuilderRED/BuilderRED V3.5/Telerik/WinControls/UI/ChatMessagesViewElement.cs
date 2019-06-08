// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatMessagesViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatMessagesViewElement : VirtualizedScrollPanel<BaseChatDataItem, BaseChatItemElement>
  {
    public static RadProperty AvatarSizeProperty = RadProperty.Register(nameof (AvatarSize), typeof (SizeF), typeof (ChatMessagesViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new SizeF(28f, 28f), ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowAvatarsProperty = RadProperty.Register(nameof (ShowAvatars), typeof (bool), typeof (ChatMessagesViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowMessagesOnOneSideProperty = RadProperty.Register(nameof (ShowMessagesOnOneSide), typeof (bool), typeof (ChatMessagesViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    private ChatMessagesViewElement.UpdateModes suspendedUpdates = ChatMessagesViewElement.UpdateModes.InvalidateItems;
    private TimeSpan timeSeparatorInterval = new TimeSpan(24, 0, 0);
    private bool isUpdateSuspended;
    private bool scrollToBottomAfterEndUpdate;
    private RadChatElement chatElement;

    public ChatMessagesViewElement(RadChatElement chatElement)
    {
      this.chatElement = chatElement;
      this.ViewElement.FitElementsToSize = true;
      this.Scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.Scroller.AllowHiddenScrolling = true;
      this.ItemSpacing = 5;
      this.Items = (IList<BaseChatDataItem>) new ChatDataItemCollection(this);
    }

    protected override ItemScroller<BaseChatDataItem> CreateItemScroller()
    {
      return (ItemScroller<BaseChatDataItem>) new ChatMessagesViewItemScroller();
    }

    public RadChatElement ChatElement
    {
      get
      {
        return this.chatElement;
      }
    }

    public SizeF AvatarSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSizeF((SizeF) this.GetValue(ChatMessagesViewElement.AvatarSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ChatMessagesViewElement.AvatarSizeProperty, (object) value);
      }
    }

    public bool ShowAvatars
    {
      get
      {
        return (bool) this.GetValue(ChatMessagesViewElement.ShowAvatarsProperty);
      }
      set
      {
        int num = (int) this.SetValue(ChatMessagesViewElement.ShowAvatarsProperty, (object) value);
      }
    }

    public bool ShowMessagesOnOneSide
    {
      get
      {
        return (bool) this.GetValue(ChatMessagesViewElement.ShowMessagesOnOneSideProperty);
      }
      set
      {
        int num = (int) this.SetValue(ChatMessagesViewElement.ShowMessagesOnOneSideProperty, (object) value);
      }
    }

    public TimeSpan TimeSeparatorInterval
    {
      get
      {
        return this.timeSeparatorInterval;
      }
      set
      {
        this.timeSeparatorInterval = value;
      }
    }

    protected override IVirtualizedElementProvider<BaseChatDataItem> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<BaseChatDataItem>) new ChatVirtualizedElementProvider();
    }

    public virtual void Update(ChatMessagesViewElement.UpdateModes updateMode)
    {
      if (this.IsUpdateSuspended)
      {
        this.suspendedUpdates |= updateMode;
      }
      else
      {
        if ((updateMode & ChatMessagesViewElement.UpdateModes.InvalidateMeasure) == ChatMessagesViewElement.UpdateModes.InvalidateMeasure)
          this.InvalidateMeasure(true);
        if ((updateMode & ChatMessagesViewElement.UpdateModes.UpdateLayout) == ChatMessagesViewElement.UpdateModes.UpdateLayout)
          this.UpdateLayout();
        if ((updateMode & ChatMessagesViewElement.UpdateModes.UpdateScroll) == ChatMessagesViewElement.UpdateModes.UpdateScroll)
          this.Scroller.UpdateScrollRange();
        if ((updateMode & ChatMessagesViewElement.UpdateModes.Invalidate) != ChatMessagesViewElement.UpdateModes.Invalidate)
          return;
        this.ViewElement.Invalidate();
      }
    }

    public virtual void BeginUpdate()
    {
      this.isUpdateSuspended = true;
      this.scrollToBottomAfterEndUpdate = this.VScrollBar.Value == this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
    }

    public bool IsUpdateSuspended
    {
      get
      {
        return this.isUpdateSuspended;
      }
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public virtual void EndUpdate(bool update)
    {
      this.isUpdateSuspended = false;
      if (update)
      {
        this.Update(this.suspendedUpdates);
        this.suspendedUpdates = ChatMessagesViewElement.UpdateModes.InvalidateItems;
      }
      if (!this.scrollToBottomAfterEndUpdate)
        return;
      this.VScrollBar.Value = this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
    }

    protected internal virtual void OnCardActionClicked(object sender, CardActionEventArgs e)
    {
      this.ChatElement.OnCardActionClicked(sender, e);
    }

    public virtual bool ShouldAddTimeSeparator(BaseChatDataItem item, BaseChatDataItem previousItem)
    {
      if (this.TimeSeparatorInterval == TimeSpan.Zero)
        return false;
      bool shouldAddSeparator = false;
      if (previousItem == null || item.Message.TimeStamp - previousItem.Message.TimeStamp >= this.timeSeparatorInterval)
        shouldAddSeparator = true;
      TimeSeparatorEventArgs e = new TimeSeparatorEventArgs(shouldAddSeparator, previousItem, item);
      this.ChatElement.OnTimeSeparatorAdding(e);
      return e.ShouldAddSeparator;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (!float.IsInfinity(availableSize.Width) && !float.IsInfinity(availableSize.Height))
        return availableSize;
      return sizeF;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == ChatMessagesViewElement.ShowAvatarsProperty || e.Property == ChatMessagesViewElement.ShowMessagesOnOneSideProperty)
      {
        foreach (BaseChatItemElement child in this.ViewElement.Children)
          child.Synchronize();
      }
      if (e.Property == ChatMessagesViewElement.ShowMessagesOnOneSideProperty)
        this.InvalidateArrange(true);
      if (e.Property != ChatMessagesViewElement.AvatarSizeProperty || this.ChatElement == null)
        return;
      this.ChatElement.InvalidateMeasure(true);
      this.ChatElement.InvalidateArrange(true);
      this.ChatElement.UpdateLayout();
    }

    public enum UpdateModes
    {
      InvalidateItems = 1,
      InvalidateMeasure = 2,
      UpdateLayout = 4,
      Invalidate = 8,
      RefreshLayout = 14, // 0x0000000E
      UpdateScroll = 16, // 0x00000010
      RefreshAll = 31, // 0x0000001F
    }
  }
}
