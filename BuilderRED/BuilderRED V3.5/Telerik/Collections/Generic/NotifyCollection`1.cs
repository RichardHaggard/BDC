// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.NotifyCollection`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.Collections.Generic
{
  [Serializable]
  public class NotifyCollection<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
  {
    public static NotifyCollection<T> Empty = new NotifyCollection<T>((IList<T>) new List<T>());
    private int suspended;
    private int version;

    public NotifyCollection()
    {
    }

    public NotifyCollection(IList<T> list)
      : base(list)
    {
    }

    public void Move(int oldIndex, int newIndex)
    {
      this.MoveItem(oldIndex, newIndex);
    }

    public void AddRange(params T[] items)
    {
      this.BeginUpdate();
      for (int index = 0; index < items.Length; ++index)
        this.Add(items[index]);
      this.EndUpdate();
    }

    public void AddRange(IEnumerable<T> items)
    {
      this.BeginUpdate();
      foreach (T obj in items)
        this.Add(obj);
      this.EndUpdate();
    }

    public void BeginUpdate()
    {
      ++this.suspended;
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public virtual void EndUpdate(bool notify)
    {
      --this.suspended;
      if (this.suspended > 0)
        return;
      this.suspended = 0;
      if (!notify)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public virtual IDisposable DeferRefresh()
    {
      this.BeginUpdate();
      return (IDisposable) new NotifyCollection<T>.DeferHelper<T>(this);
    }

    private void EndDefer()
    {
      this.EndUpdate();
    }

    protected internal int Version
    {
      get
      {
        return this.version;
      }
    }

    protected bool Suspended
    {
      get
      {
        return this.suspended > 0;
      }
    }

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Add, (object) item, index);
    }

    protected override void SetItem(int index, T item)
    {
      T obj = this[index];
      base.SetItem(index, item);
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, (object) obj, (object) item, index);
    }

    protected override void RemoveItem(int index)
    {
      T obj = this[index];
      base.RemoveItem(index);
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, (object) obj, index);
    }

    protected override void ClearItems()
    {
      base.ClearItems();
      this.OnPropertyChanged("Count");
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Reset);
    }

    protected virtual void MoveItem(int oldIndex, int newIndex)
    {
      T obj = this[oldIndex];
      base.RemoveItem(oldIndex);
      base.InsertItem(newIndex, obj);
      this.OnPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Move, (object) newIndex, (object) obj, newIndex);
    }

    protected virtual NotifyCollectionChangedEventArgs CreateEventArguments(
      NotifyCollectionChangedAction action)
    {
      return new NotifyCollectionChangedEventArgs(action);
    }

    protected virtual NotifyCollectionChangedEventArgs CreateEventArguments(
      NotifyCollectionChangedAction action,
      object item,
      int index)
    {
      return new NotifyCollectionChangedEventArgs(action, item, index);
    }

    protected virtual NotifyCollectionChangedEventArgs CreateEventArguments(
      NotifyCollectionChangedAction action,
      object oldItem,
      object item,
      int index)
    {
      return new NotifyCollectionChangedEventArgs(action, item, oldItem, index);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      if (this.CollectionChanged == null || this.suspended != 0)
        return;
      this.CollectionChanged((object) this, args);
      ++this.version;
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action)
    {
      this.OnCollectionChanged(this.CreateEventArguments(action));
    }

    private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
    {
      this.OnCollectionChanged(this.CreateEventArguments(action, item, index));
    }

    private void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      object oldItem,
      object item,
      int index)
    {
      this.OnCollectionChanged(this.CreateEventArguments(action, oldItem, item, index));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    private class DeferHelper<TItem> : IDisposable
    {
      private NotifyCollection<TItem> collection;

      public DeferHelper(NotifyCollection<TItem> collection)
      {
        this.collection = collection;
      }

      public void Dispose()
      {
        if (this.collection == null)
          return;
        this.collection.EndDefer();
        this.collection = (NotifyCollection<TItem>) null;
      }
    }
  }
}
