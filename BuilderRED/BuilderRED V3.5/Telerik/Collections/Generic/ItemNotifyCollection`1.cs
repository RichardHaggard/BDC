// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.ItemNotifyCollection`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.Collections.Generic
{
  [Serializable]
  public class ItemNotifyCollection<T> : NotifyCollection<T> where T : INotifyPropertyChanged
  {
    public ItemNotifyCollection()
    {
    }

    public ItemNotifyCollection(IList<T> list)
      : base(list)
    {
    }

    protected override void InsertItem(int index, T item)
    {
      this.SubscribeItem(item);
      base.InsertItem(index, item);
    }

    protected override void SetItem(int index, T item)
    {
      this.UnsubscribeItem(this[index]);
      this.SubscribeItem(item);
      base.SetItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
      this.UnsubscribeItem(this[index]);
      base.RemoveItem(index);
    }

    protected override void ClearItems()
    {
      foreach (T obj in (IEnumerable<T>) this.Items)
        this.UnsubscribeItem(obj);
      base.ClearItems();
    }

    protected virtual void SubscribeItem(T item)
    {
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    protected virtual void UnsubscribeItem(T item)
    {
      item.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, sender));
    }
  }
}
