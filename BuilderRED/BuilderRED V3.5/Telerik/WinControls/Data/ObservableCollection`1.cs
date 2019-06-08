// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.ObservableCollection`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.Data
{
  [Serializable]
  public class ObservableCollection<T> : Collection<T>, IList, ICollection, IEnumerable, INotifyCollectionChanged, INotifyCollectionChanging, INotifyPropertyChanged, INotifyPropertyChangingEx
  {
    private const string CountString = "Count";
    private const string IndexerName = "Item[]";
    private int update;
    private int itemUpdate;

    public ObservableCollection()
    {
    }

    public ObservableCollection(IList<T> list)
      : base(list != null ? (IList<T>) new List<T>(list.Count) : (IList<T>) new List<T>())
    {
      IList<T> items = this.Items;
      if (list == null || items == null)
        return;
      foreach (T obj in (IEnumerable<T>) list)
        items.Add(obj);
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event NotifyCollectionChangingEventHandler CollectionChanging;

    protected override void ClearItems()
    {
      if (this.OnCollectionReseting())
        return;
      T[] array = new T[this.Count];
      this.Items.CopyTo(array, 0);
      base.ClearItems();
      this.OnNotifyPropertyChanged("Count");
      this.OnNotifyPropertyChanged("Item[]");
      this.OnCollectionReset((IList) array);
    }

    protected override void InsertItem(int index, T item)
    {
      this.InsertItem(index, item, (Action<T>) null);
    }

    protected virtual void InsertItem(int index, T item, Action<T> approvedAction)
    {
      if (this.OnCollectionChanging(NotifyCollectionChangedAction.Add, (object) item, index))
        return;
      if (approvedAction != null)
        approvedAction(item);
      base.InsertItem(index, item);
      this.OnNotifyPropertyChanged("Count");
      this.OnNotifyPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Add, (object) item, index);
    }

    public void Move(int oldIndex, int newIndex)
    {
      this.MoveItem(oldIndex, newIndex);
    }

    protected virtual void MoveItem(int oldIndex, int newIndex)
    {
      T obj = this[oldIndex];
      if (this.OnCollectionChanging(NotifyCollectionChangedAction.Move, (object) obj, newIndex, oldIndex))
        return;
      base.RemoveItem(oldIndex);
      base.InsertItem(newIndex, obj);
      this.OnNotifyPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Move, (object) obj, newIndex, oldIndex);
    }

    public virtual void BeginUpdate()
    {
      ++this.update;
    }

    public virtual void BeginItemUpdate()
    {
      ++this.itemUpdate;
    }

    public void EndItemUpdate()
    {
      this.EndItemUpdate(true);
    }

    public virtual void EndItemUpdate(bool notifyUpdates)
    {
      --this.itemUpdate;
      if (this.itemUpdate < 0)
      {
        this.itemUpdate = 0;
      }
      else
      {
        if (this.itemUpdate != 0 || !notifyUpdates)
          return;
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged));
      }
    }

    public virtual void EndUpdate(bool notifyUpdates)
    {
      --this.update;
      if (this.update < 0)
      {
        this.update = 0;
      }
      else
      {
        if (this.update != 0 || !notifyUpdates)
          return;
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Batch));
      }
    }

    public void EndUpdate()
    {
      this.EndUpdate(true);
    }

    public bool IsUpdated
    {
      get
      {
        return this.update == 0;
      }
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.update != 0 || this.itemUpdate != 0)
        return;
      this.NotifyListenersCollectionChanged(e);
    }

    protected virtual void NotifyListenersCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, e);
    }

    protected internal void CallCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged(sender, e);
    }

    protected virtual bool OnCollectionChanging(NotifyCollectionChangingEventArgs e)
    {
      if (this.update != 0 || this.itemUpdate != 0)
        return false;
      this.NotifyListenersCollectionChanging(e);
      return e.Cancel;
    }

    protected virtual void NotifyListenersCollectionChanging(NotifyCollectionChangingEventArgs e)
    {
      if (this.CollectionChanging == null)
        return;
      this.CollectionChanging((object) this, e);
    }

    protected internal void CallCollectionChanging(
      object sender,
      NotifyCollectionChangingEventArgs e)
    {
      if (this.CollectionChanging == null)
        return;
      this.CollectionChanging(sender, e);
    }

    protected override void RemoveItem(int index)
    {
      T obj = this[index];
      if (this.OnCollectionChanging(NotifyCollectionChangedAction.Remove, (object) obj, index))
        return;
      base.RemoveItem(index);
      this.OnNotifyPropertyChanged("Count");
      this.OnNotifyPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Remove, (object) obj, index);
    }

    protected override void SetItem(int index, T item)
    {
      T obj = this[index];
      if (this.OnCollectionChanging(NotifyCollectionChangedAction.Replace, (object) obj, (object) item, index))
        return;
      base.SetItem(index, item);
      this.OnNotifyPropertyChanged("Item[]");
      this.OnCollectionChanged(NotifyCollectionChangedAction.Replace, (object) obj, (object) item, index);
    }

    protected void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      object item,
      int index)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
    }

    protected void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      object item,
      int index,
      int oldIndex)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
    }

    protected void OnCollectionChanged(
      NotifyCollectionChangedAction action,
      object oldItem,
      object newItem,
      int index)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
    }

    protected void OnCollectionReset(IList oldItems)
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, (IList) null, oldItems));
    }

    protected bool OnCollectionChanging(
      NotifyCollectionChangedAction action,
      object item,
      int index)
    {
      return this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(action, item, index));
    }

    protected bool OnCollectionChanging(
      NotifyCollectionChangedAction action,
      object item,
      int index,
      int oldIndex)
    {
      return this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(action, item, index, oldIndex));
    }

    protected bool OnCollectionChanging(
      NotifyCollectionChangedAction action,
      object oldItem,
      object newItem,
      int index)
    {
      return this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(action, newItem, oldItem, index));
    }

    protected bool OnCollectionReseting()
    {
      return this.OnCollectionChanging(new NotifyCollectionChangingEventArgs(NotifyCollectionChangedAction.Reset));
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Occurs when when a property of an object changes change.")]
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected virtual void OnNotifyPropertyChanging(string propertyName)
    {
      this.OnNotifyPropertyChanging(new PropertyChangingEventArgsEx(propertyName));
    }

    protected virtual void OnNotifyPropertyChanging(PropertyChangingEventArgsEx e)
    {
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, e);
    }

    int IList.Add(object value)
    {
      if (!(value is T))
        return -1;
      this.Add((T) value);
      return this.IndexOf((T) value);
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      if (value is T)
        return this.Contains((T) value);
      return false;
    }

    int IList.IndexOf(object value)
    {
      if (value is T)
        return this.IndexOf((T) value);
      return -1;
    }

    void IList.Insert(int index, object value)
    {
      if (!(value is T))
        return;
      this.Insert(index, (T) value);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      if (!(value is T))
        return;
      this.Remove((T) value);
    }

    void IList.RemoveAt(int index)
    {
      this.RemoveAt(index);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this.Items[index];
      }
      set
      {
        if (!(value is T))
          return;
        this.Items[index] = (T) value;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (array.Rank != 1)
        throw new ArgumentException(nameof (array), "Multidimentional arrays not supported!");
      if (array.GetLowerBound(0) != 0)
        throw new ArgumentException(nameof (array), "Non-zero lower bound");
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      if (array.Length - index < this.Count)
        throw new ArgumentOutOfRangeException(nameof (array), "Array too small");
      T[] array1 = array as T[];
      if (array1 != null)
      {
        this.Items.CopyTo(array1, index);
      }
      else
      {
        Type elementType = array.GetType().GetElementType();
        Type c = typeof (T);
        if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
          throw new ArgumentException(nameof (array), "Invalid array type");
        object[] objArray = array as object[];
        if (objArray == null)
          throw new ArgumentException(nameof (array), "Invalid array type");
        try
        {
          for (int index1 = 0; index1 < this.Items.Count; ++index1)
            objArray[index++] = (object) this.Items[index1];
        }
        catch (ArrayTypeMismatchException ex)
        {
          throw new ArgumentException(nameof (array), "Invalid array type");
        }
      }
    }

    int ICollection.Count
    {
      get
      {
        return this.Items.Count;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) null;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.Items.GetEnumerator();
    }
  }
}
