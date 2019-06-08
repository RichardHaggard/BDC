// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatDataItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ChatDataItemCollection : IList<BaseChatDataItem>, ICollection<BaseChatDataItem>, IEnumerable<BaseChatDataItem>, IEnumerable, INotifyCollectionChanged
  {
    private List<BaseChatDataItem> list;
    private ChatMessagesViewElement owner;
    private bool suspendUpdate;

    public ChatDataItemCollection(ChatMessagesViewElement owner)
    {
      this.list = new List<BaseChatDataItem>();
      this.owner = owner;
    }

    public ChatMessagesViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public BaseChatDataItem this[int index]
    {
      get
      {
        return this.list[index];
      }
      set
      {
        this.list[index] = value;
      }
    }

    public int Count
    {
      get
      {
        return this.list.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public void Add(BaseChatDataItem item)
    {
      bool flag = this.Owner.ShouldAddTimeSeparator(item, this.list.Count > 0 ? this.list[this.list.Count - 1] : (BaseChatDataItem) null);
      if (this.list.Count > 0)
      {
        if (!flag)
        {
          BaseChatDataItem baseChatDataItem = this.list[this.list.Count - 1];
          if (baseChatDataItem.Message.Author.Equals((object) item.Message.Author))
          {
            if (baseChatDataItem.MessageType == ChatMessageType.Single)
              baseChatDataItem.MessageType = ChatMessageType.First;
            else if (baseChatDataItem.MessageType == ChatMessageType.Last)
              baseChatDataItem.MessageType = ChatMessageType.Middle;
            item.MessageType = ChatMessageType.Last;
          }
        }
        else
        {
          BaseChatDataItem baseChatDataItem = this.list[this.list.Count - 1];
          if (baseChatDataItem.MessageType == ChatMessageType.First)
            baseChatDataItem.MessageType = ChatMessageType.Single;
          else if (baseChatDataItem.MessageType == ChatMessageType.Middle)
            baseChatDataItem.MessageType = ChatMessageType.Last;
        }
      }
      item.ChatMessagesViewElement = this.Owner;
      if (flag)
      {
        ChatTimeSeparatorDataItem separatorDataItem = new ChatTimeSeparatorDataItem(new ChatTimeSeparatorMessage(item.Message.TimeStamp));
        separatorDataItem.ChatMessagesViewElement = this.Owner;
        this.list.Add((BaseChatDataItem) separatorDataItem);
      }
      this.list.Add(item);
      if (this.suspendUpdate)
        return;
      this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item));
    }

    public void AddRange(IEnumerable<BaseChatDataItem> items)
    {
      this.suspendUpdate = true;
      foreach (BaseChatDataItem baseChatDataItem in items)
        this.Add(baseChatDataItem);
      this.suspendUpdate = false;
      this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) items));
    }

    public void Clear()
    {
      foreach (BaseChatDataItem baseChatDataItem in this.list)
        baseChatDataItem.ChatMessagesViewElement = (ChatMessagesViewElement) null;
      this.list.Clear();
      this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public bool Contains(BaseChatDataItem item)
    {
      return this.list.Contains(item);
    }

    public void CopyTo(BaseChatDataItem[] array, int arrayIndex)
    {
      this.list.CopyTo(array, arrayIndex);
    }

    public IEnumerator<BaseChatDataItem> GetEnumerator()
    {
      return (IEnumerator<BaseChatDataItem>) this.list.GetEnumerator();
    }

    public int IndexOf(BaseChatDataItem item)
    {
      return this.list.IndexOf(item);
    }

    public void Insert(int index, BaseChatDataItem item)
    {
      this.list.Insert(index, item);
      item.ChatMessagesViewElement = this.owner;
      this.AdjustMessageTypes();
      this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) item, index));
    }

    public void InsertRange(int index, IEnumerable<BaseChatDataItem> items)
    {
      this.list.InsertRange(index, items);
      this.AdjustMessageTypes();
      this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) items, index));
    }

    public bool Remove(BaseChatDataItem item)
    {
      bool flag = this.list.Remove(item);
      if (flag)
      {
        this.AdjustMessageTypes();
        item.ChatMessagesViewElement = (ChatMessagesViewElement) null;
        this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) item));
      }
      return flag;
    }

    public void RemoveAt(int index)
    {
      BaseChatDataItem baseChatDataItem = this.list[index];
      this.list.RemoveAt(index);
      this.AdjustMessageTypes();
      baseChatDataItem.ChatMessagesViewElement = (ChatMessagesViewElement) null;
      this.owner.Update(ChatMessagesViewElement.UpdateModes.InvalidateMeasure | ChatMessagesViewElement.UpdateModes.UpdateLayout | ChatMessagesViewElement.UpdateModes.UpdateScroll);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) baseChatDataItem, index));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable) this.list).GetEnumerator();
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, e);
    }

    protected virtual void AdjustMessageTypes()
    {
      if (this.list.Count == 0)
        return;
      if (this.list.Count == 1)
        this.list[0].MessageType = ChatMessageType.Single;
      else if (this.list.Count == 2)
      {
        if (this.list[0].Message.Author.Equals((object) this.list[1].Message.Author))
        {
          this.list[0].MessageType = ChatMessageType.First;
          this.list[1].MessageType = ChatMessageType.Last;
        }
        else
        {
          this.list[0].MessageType = ChatMessageType.Single;
          this.list[1].MessageType = ChatMessageType.Single;
        }
      }
      else
      {
        this.list[0].MessageType = !this.list[0].Message.Author.Equals((object) this.list[1].Message.Author) ? ChatMessageType.Single : ChatMessageType.First;
        for (int index = 1; index < this.list.Count - 2; ++index)
        {
          BaseChatDataItem baseChatDataItem1 = this.list[index - 1];
          BaseChatDataItem baseChatDataItem2 = this.list[index];
          BaseChatDataItem baseChatDataItem3 = this.list[index + 1];
          baseChatDataItem2.MessageType = !baseChatDataItem1.Message.Author.Equals((object) baseChatDataItem2.Message.Author) ? (!baseChatDataItem2.Message.Author.Equals((object) baseChatDataItem3.Message.Author) ? ChatMessageType.Single : ChatMessageType.First) : (!baseChatDataItem2.Message.Author.Equals((object) baseChatDataItem3.Message.Author) ? ChatMessageType.Last : ChatMessageType.Middle);
        }
        if (this.list[this.list.Count - 2].Message.Author.Equals((object) this.list[this.list.Count - 1].Message.Author))
          this.list[this.list.Count - 1].MessageType = ChatMessageType.Last;
        else
          this.list[this.list.Count - 1].MessageType = ChatMessageType.Single;
      }
    }
  }
}
