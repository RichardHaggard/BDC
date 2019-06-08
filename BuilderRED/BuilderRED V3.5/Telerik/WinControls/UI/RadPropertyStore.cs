// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPropertyStore
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [TypeDescriptionProvider(typeof (PropertyStoreTypeDescriptionProvider))]
  public class RadPropertyStore : IList<PropertyStoreItem>, ICollection<PropertyStoreItem>, IEnumerable<PropertyStoreItem>, IEnumerable, INotifyCollectionChanged, INotifyPropertyChanged
  {
    private ObservableCollection<PropertyStoreItem> propertyItems;

    public RadPropertyStore()
    {
      this.propertyItems = new ObservableCollection<PropertyStoreItem>();
      this.propertyItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.propertyItems_CollectionChanged);
      this.propertyItems.PropertyChanged += new PropertyChangedEventHandler(this.propertyItems_PropertyChanged);
    }

    public virtual int Count
    {
      get
      {
        return this.propertyItems.Count;
      }
    }

    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public virtual void Add(PropertyStoreItem item)
    {
      if (this.ItemExistsInCollection(item))
        throw new ArgumentException("An item with the same name already exists in the collection. Item name: " + item.PropertyName);
      this.propertyItems.Add(item);
      item.Owner = this;
    }

    public virtual void Add(Type propertyType, string propertyName, object value)
    {
      this.Add(new PropertyStoreItem(propertyType, propertyName, value));
    }

    public virtual void AddRange(IEnumerable<PropertyStoreItem> items)
    {
      foreach (PropertyStoreItem propertyStoreItem in items)
        this.Add(propertyStoreItem);
    }

    public virtual void Insert(int index, PropertyStoreItem item)
    {
      if (this.ItemExistsInCollection(item))
        throw new ArgumentException("An item with the same name already exists in the collection. Item name: " + item.PropertyName);
      this.propertyItems.Insert(index, item);
      item.Owner = this;
    }

    public virtual PropertyStoreItem this[int index]
    {
      get
      {
        return this.propertyItems[index];
      }
      set
      {
        if (this.ItemExistsInCollection(value) && this.propertyItems[index].PropertyName != value.PropertyName)
          throw new ArgumentException("An item with the same name already exists in the collection. Item name: " + value.PropertyName);
        this.propertyItems[index] = value;
      }
    }

    public virtual PropertyStoreItem this[string propertyName]
    {
      get
      {
        foreach (PropertyStoreItem propertyItem in (Collection<PropertyStoreItem>) this.propertyItems)
        {
          if (propertyItem.PropertyName == propertyName)
            return propertyItem;
        }
        return (PropertyStoreItem) null;
      }
      set
      {
        PropertyStoreItem propertyStoreItem = (PropertyStoreItem) null;
        foreach (PropertyStoreItem propertyItem in (Collection<PropertyStoreItem>) this.propertyItems)
        {
          if (propertyItem.PropertyName == propertyName)
          {
            propertyStoreItem = propertyItem;
            break;
          }
        }
        if (propertyStoreItem == null)
          return;
        if (this.ItemExistsInCollection(value) && propertyStoreItem.PropertyName != value.PropertyName)
          throw new ArgumentException("An item with the same name already exists in the collection. Item name: " + value.PropertyName);
      }
    }

    public virtual int IndexOf(PropertyStoreItem item)
    {
      return this.propertyItems.IndexOf(item);
    }

    public virtual bool Contains(PropertyStoreItem item)
    {
      return this.propertyItems.Contains(item);
    }

    public virtual bool Remove(PropertyStoreItem item)
    {
      if (item.Owner == this)
        item.Owner = (RadPropertyStore) null;
      return this.propertyItems.Remove(item);
    }

    public virtual bool Remove(string propertyName)
    {
      foreach (PropertyStoreItem propertyItem in (Collection<PropertyStoreItem>) this.propertyItems)
      {
        if (propertyItem.PropertyName == propertyName)
          return this.Remove(propertyItem);
      }
      return false;
    }

    public virtual void RemoveAt(int index)
    {
      this.propertyItems.RemoveAt(index);
      if (index >= this.propertyItems.Count || index < 0)
        return;
      this.propertyItems[index].Owner = (RadPropertyStore) null;
    }

    public virtual void Clear()
    {
      this.propertyItems.Clear();
      foreach (PropertyStoreItem propertyItem in (Collection<PropertyStoreItem>) this.propertyItems)
        propertyItem.Owner = (RadPropertyStore) null;
    }

    public virtual void CopyTo(PropertyStoreItem[] array, int arrayIndex)
    {
      this.propertyItems.CopyTo(array, arrayIndex);
    }

    public IEnumerator<PropertyStoreItem> GetEnumerator()
    {
      return this.propertyItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable) this.propertyItems).GetEnumerator();
    }

    protected virtual bool ItemExistsInCollection(PropertyStoreItem item)
    {
      foreach (PropertyStoreItem propertyItem in (Collection<PropertyStoreItem>) this.propertyItems)
      {
        if (item.PropertyName == propertyItem.PropertyName)
          return true;
      }
      return false;
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, e);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    public virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public event PropertyStoreItemValueChangedEventHandler ItemValueChanged;

    public virtual void OnItemValueChanged(PropertyStoreItem item)
    {
      this.OnItemValueChanged(new PropertyStoreItemValueChangedEventArgs(item));
    }

    public virtual void OnItemValueChanged(PropertyStoreItemValueChangedEventArgs e)
    {
      if (this.ItemValueChanged == null)
        return;
      this.ItemValueChanged((object) this, e);
    }

    private void propertyItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnCollectionChanged(e);
    }

    private void propertyItems_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnNotifyPropertyChanged(e);
    }
  }
}
