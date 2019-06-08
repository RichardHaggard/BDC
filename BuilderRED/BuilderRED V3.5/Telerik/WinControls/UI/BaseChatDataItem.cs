// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseChatDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public abstract class BaseChatDataItem : IDataItem, INotifyPropertyChanged
  {
    private object tag;
    private string status;
    private ChatMessage message;
    private ChatMessageType messageType;
    private SizeF actualSize;
    private ChatMessagesViewElement owner;

    public BaseChatDataItem(ChatMessage message)
    {
      this.message = message;
      this.messageType = ChatMessageType.Single;
      this.message.PropertyChanged += new PropertyChangedEventHandler(this.Message_PropertyChanged);
    }

    public ChatMessage Message
    {
      get
      {
        return this.message;
      }
      set
      {
        if (this.message == value)
          return;
        if (this.message != null)
          this.message.PropertyChanged -= new PropertyChangedEventHandler(this.Message_PropertyChanged);
        this.message = value;
        if (this.message != null)
          this.message.PropertyChanged += new PropertyChangedEventHandler(this.Message_PropertyChanged);
        this.OnPropertyChanged(nameof (Message));
      }
    }

    public ChatMessagesViewElement ChatMessagesViewElement
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    [Browsable(false)]
    public virtual SizeF ActualSize
    {
      get
      {
        return this.actualSize;
      }
      set
      {
        if (!(this.actualSize != value))
          return;
        if (this.owner != null)
          this.owner.Scroller.UpdateScrollRange(this.owner.Scroller.Scrollbar.Maximum + (int) ((double) value.Height - (double) this.actualSize.Height), false);
        this.actualSize = value;
        this.OnPropertyChanged(nameof (ActualSize));
      }
    }

    public ChatMessageType MessageType
    {
      get
      {
        return this.messageType;
      }
      set
      {
        if (this.messageType == value)
          return;
        this.messageType = value;
        this.OnPropertyChanged(nameof (MessageType));
      }
    }

    public string Status
    {
      get
      {
        return this.status;
      }
      set
      {
        if (!(this.status != value))
          return;
        this.status = value;
        this.OnPropertyChanged(nameof (Status));
      }
    }

    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.OnPropertyChanged(nameof (Tag));
      }
    }

    public bool IsOwnMessage
    {
      get
      {
        if (this.Message == null || this.Message.Author == null || (this.ChatMessagesViewElement == null || this.ChatMessagesViewElement.ChatElement == null))
          return false;
        return this.Message.Author.Equals((object) this.ChatMessagesViewElement.ChatElement.Author);
      }
    }

    private void Message_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged("Message" + e.PropertyName);
    }

    object IDataItem.this[string name]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    object IDataItem.this[int index]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    object IDataItem.DataBoundItem
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    int IDataItem.FieldCount
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    int IDataItem.IndexOf(string name)
    {
      throw new NotImplementedException();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
