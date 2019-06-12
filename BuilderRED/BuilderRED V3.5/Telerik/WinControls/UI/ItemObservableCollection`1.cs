// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemObservableCollection`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class ItemObservableCollection<T> : ObservableCollection<T> where T : class, INotifyPropertyChangingEx, INotifyPropertyChanged
  {
    public ItemObservableCollection()
    {
    }

    public ItemObservableCollection(IList<T> list)
      : base(list)
    {
    }

    protected virtual void ItemEventsSubscribe(T item)
    {
      if ((object) item == null)
        return;
      item.PropertyChanging += new PropertyChangingEventHandlerEx(this.OnItemPropertyChanging);
      if ((object) item is INotifyCollectionChanging)
        ((object) item as INotifyCollectionChanging).CollectionChanging += new NotifyCollectionChangingEventHandler(this.OnItemPropertyChanging);
      item.PropertyChanged += new PropertyChangedEventHandler(this.OnItemPropertyChanged);
      if (!((object) item is INotifyCollectionChanged))
        return;
      ((object) item as INotifyCollectionChanged).CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemPropertyChanged);
    }

    protected virtual void ItemEventsUnsubscribe(T item)
    {
      if ((object) item == null)
        return;
      item.PropertyChanging -= new PropertyChangingEventHandlerEx(this.OnItemPropertyChanging);
      if ((object) item is INotifyCollectionChanging)
        ((object) item as INotifyCollectionChanging).CollectionChanging -= new NotifyCollectionChangingEventHandler(this.OnItemPropertyChanging);
      item.PropertyChanged -= new PropertyChangedEventHandler(this.OnItemPropertyChanged);
      if (!((object) item is INotifyCollectionChanged))
        return;
      ((object) item as INotifyCollectionChanged).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnItemPropertyChanged);
    }

    protected virtual void OnItemPropertyChanging(object sender, CancelEventArgs e)
    {
      if (!(sender is T))
        throw new ArgumentException(string.Format("Event sender is not object of the class {0} nor a class deriving from it!", (object) typeof (T).FullName), nameof (sender));
      T obj = sender as T;
      int index = this.IndexOf(obj);
      if ((object) obj == null || index == -1)
      {
        this.ItemEventsUnsubscribe(obj);
      }
      else
      {
        PropertyChangingEventArgsEx propertyArgs = e as PropertyChangingEventArgsEx;
        NotifyCollectionChangingEventArgs e1 = propertyArgs != null ? new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) obj, (object) obj, index, propertyArgs) : new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) obj, (object) obj, index);
        this.OnCollectionChanging(e1);
        e.Cancel = e1.Cancel;
      }
    }

    protected virtual void OnItemPropertyChanged(object sender, EventArgs e)
    {
      if (!(sender is T))
        throw new ArgumentException(string.Format("Event sender is not object of the class {0} nor a class deriving from it!", (object) typeof (T).FullName), nameof (sender));
      T obj = sender as T;
      int index = this.IndexOf(obj);
      if ((object) obj == null || index == -1)
      {
        this.ItemEventsUnsubscribe(obj);
      }
      else
      {
        PropertyChangedEventArgs changedEventArgs = e as PropertyChangedEventArgs;
        this.OnCollectionChanged(changedEventArgs != null ? new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) obj, (object) obj, index, changedEventArgs.PropertyName) : new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) obj, (object) obj, index));
      }
    }

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      this.ItemEventsSubscribe(item);
    }

    protected override void RemoveItem(int index)
    {
      this.ItemEventsUnsubscribe(this[index]);
      base.RemoveItem(index);
    }

    protected override void SetItem(int index, T item)
    {
      this.ItemEventsUnsubscribe(this[index]);
      this.ItemEventsSubscribe(item);
      base.SetItem(index, item);
    }

    protected override void ClearItems()
    {
      for (int index = 0; index < this.Count; ++index)
        this.ItemEventsUnsubscribe(this[index]);
      base.ClearItems();
    }
  }
}
