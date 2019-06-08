﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadCommandBarBaseItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  [DebuggerDisplay("Count = {Count}")]
  [Editor("Telerik.WinControls.UI.Design.RadCommandBarBaseItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [Serializable]
  public class RadCommandBarBaseItemCollection : CollectionBase, IEnumerable<RadCommandBarBaseItem>, IEnumerable
  {
    private Type[] itemTypes = new Type[1]
    {
      typeof (RadCommandBarBaseItemCollection)
    };
    private int suspendNotifyCount;
    private RadElement owner;
    private Type[] excludedTypes;
    private Type[] sealedTypes;
    private Type defaultType;

    public event RadCommandBarBaseItemCollectionItemChangedDelegate ItemsChanged;

    protected virtual void OnItemsChanged(
      RadCommandBarBaseItem target,
      ItemsChangeOperation operation)
    {
      if (this.suspendNotifyCount > 0)
        return;
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          target.SetOwnerCommandBarCollection((CollectionBase) this);
          break;
        case ItemsChangeOperation.Removed:
          target.SetOwnerCommandBarCollection((CollectionBase) null);
          break;
        case ItemsChangeOperation.Cleared:
          using (RadCommandBarBaseItemCollection.RadCommandBarBaseItemEnumerator enumerator = this.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.SetOwnerCommandBarCollection((CollectionBase) null);
            break;
          }
      }
      if (this.ItemsChanged == null)
        return;
      this.ItemsChanged(this, target, operation);
    }

    public RadCommandBarBaseItemCollection(RadElement owner)
    {
      this.owner = owner;
    }

    public RadElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        if (this.owner == value)
          return;
        if (this.owner != null)
          this.RemoveAllFromOwner();
        this.owner = value;
        if (this.owner == null)
          return;
        this.SynchronizeAllWithOwner();
      }
    }

    private void SynchronizeAllWithOwner()
    {
      foreach (RadItem radItem in this)
      {
        if (radItem.Parent != this.owner)
          this.owner.Children.Add((RadElement) radItem);
      }
    }

    private void RemoveAllFromOwner()
    {
      for (int index1 = this.Count - 1; index1 >= 0; --index1)
      {
        int index2 = this.owner.Children.IndexOf((RadElement) this[index1]);
        if (index2 >= 0)
          this.owner.Children.RemoveAt(index2);
      }
    }

    protected override void OnSetComplete(int index, object oldValue, object newValue)
    {
      if (this.owner != null && this.owner.IsInValidState(false))
      {
        RadItem radItem = newValue as RadItem;
        int index1 = this.owner.Children.IndexOf((RadElement) radItem);
        if (index1 >= 0)
          this.owner.Children[index1] = (RadElement) radItem;
        else
          this.owner.Children.Add((RadElement) radItem);
      }
      base.OnSetComplete(index, oldValue, newValue);
    }

    protected override void OnClear()
    {
      this.OnItemsChanged((RadCommandBarBaseItem) null, ItemsChangeOperation.Clearing);
      if (this.owner == null || !this.owner.IsInValidState(false))
        return;
      this.RemoveAllFromOwner();
    }

    protected override void OnInsertComplete(int index, object value)
    {
      if (this.owner != null)
      {
        RadItem radItem = value as RadItem;
        if (radItem.Parent != this.owner)
        {
          int index1 = index;
          if (index1 > this.owner.Children.Count)
            index1 = this.owner.Children.Count;
          this.owner.Children.Insert(index1, (RadElement) radItem);
        }
      }
      this.OnItemsChanged(value as RadCommandBarBaseItem, ItemsChangeOperation.Inserted);
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      if (this.owner != null && this.owner.IsInValidState(false))
      {
        RadItem radItem = value as RadItem;
        if (radItem.Parent == this.owner)
          this.owner.Children.Remove((RadElement) radItem);
      }
      base.OnRemoveComplete(index, value);
    }

    public RadCommandBarBaseItemCollection()
    {
    }

    public RadCommandBarBaseItemCollection(RadCommandBarBaseItemCollection value)
    {
      this.AddRange(value);
    }

    public RadCommandBarBaseItemCollection(RadCommandBarBaseItem[] value)
    {
      this.AddRange(value);
    }

    protected override void OnValidate(object value)
    {
      if (!(value is RadCommandBarBaseItem))
        throw new InvalidOperationException("Collection contains only instances of Type RadCommandBarBaseItem");
    }

    public void SuspendNotifications()
    {
      ++this.suspendNotifyCount;
    }

    public void ResumeNotifications()
    {
      if (this.suspendNotifyCount <= 0)
        return;
      --this.suspendNotifyCount;
    }

    public virtual Type[] ItemTypes
    {
      get
      {
        return this.itemTypes;
      }
      set
      {
        this.itemTypes = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual Type[] ExcludedTypes
    {
      get
      {
        return this.excludedTypes;
      }
      set
      {
        this.excludedTypes = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual Type[] SealedTypes
    {
      get
      {
        return this.sealedTypes;
      }
      set
      {
        this.sealedTypes = value;
      }
    }

    public virtual Type DefaultType
    {
      get
      {
        return this.defaultType;
      }
      set
      {
        this.defaultType = value;
      }
    }

    IEnumerator<RadCommandBarBaseItem> IEnumerable<RadCommandBarBaseItem>.GetEnumerator()
    {
      return (IEnumerator<RadCommandBarBaseItem>) new RadCommandBarBaseItemCollection.RadCommandBarBaseItemEnumerator(this);
    }

    public RadCommandBarBaseItemCollection.RadCommandBarBaseItemEnumerator GetEnumerator()
    {
      return new RadCommandBarBaseItemCollection.RadCommandBarBaseItemEnumerator(this);
    }

    protected override void OnRemove(int index, object value)
    {
      this.OnItemsChanged(value as RadCommandBarBaseItem, ItemsChangeOperation.Removing);
    }

    protected override void OnSet(int index, object oldValue, object newValue)
    {
      base.OnSet(index, oldValue, newValue);
    }

    protected override void OnClearComplete()
    {
      this.OnItemsChanged((RadCommandBarBaseItem) null, ItemsChangeOperation.Cleared);
    }

    protected virtual void OnSort()
    {
      this.OnItemsChanged((RadCommandBarBaseItem) null, ItemsChangeOperation.Sorting);
    }

    public int Add(RadCommandBarBaseItem value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(params RadCommandBarBaseItem[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(RadCommandBarBaseItemCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public void Insert(int index, RadCommandBarBaseItem value)
    {
      this.List.Insert(index, (object) value);
    }

    public void Remove(RadItem value)
    {
      int index = this.List.IndexOf((object) value);
      if (index < 0)
        return;
      this.List.RemoveAt(index);
    }

    public void Sort()
    {
      this.OnSort();
      this.InnerList.Sort();
      this.OnSortComplete();
    }

    public void Sort(IComparer comparer)
    {
      this.OnSort();
      this.InnerList.Sort(comparer);
      this.OnSortComplete();
    }

    public void Sort(int index, int count, IComparer comparer)
    {
      this.OnSort();
      this.InnerList.Sort(index, count, comparer);
      this.OnSortComplete();
    }

    public bool Contains(RadCommandBarBaseItem value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(RadCommandBarBaseItem value)
    {
      return this.List.IndexOf((object) value);
    }

    public void CopyTo(RadCommandBarBaseItem[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public RadCommandBarBaseItem[] ToArray()
    {
      RadCommandBarBaseItem[] array = new RadCommandBarBaseItem[this.Count];
      this.CopyTo(array, 0);
      return array;
    }

    public RadCommandBarBaseItem this[int index]
    {
      get
      {
        return (RadCommandBarBaseItem) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    [Browsable(false)]
    public RadItem First
    {
      get
      {
        if (this.Count != 0)
          return (RadItem) this[0];
        return (RadItem) null;
      }
    }

    public void AdjustNewItem()
    {
      for (int index = 0; index < this.List.Count; ++index)
      {
        RadItem radItem = (RadItem) this.List[index];
        if ((bool) radItem.GetValue(RadItem.IsAddNewItemProperty))
        {
          this.SuspendNotifications();
          this.List.Remove((object) radItem);
          this.List.Add((object) radItem);
          this.ResumeNotifications();
        }
      }
    }

    [Browsable(false)]
    public RadItem Last
    {
      get
      {
        if (this.Count != 0)
          return (RadItem) this[this.Count - 1];
        return (RadItem) null;
      }
    }

    public RadCommandBarBaseItem this[string itemName]
    {
      get
      {
        for (int index = 0; index < this.List.Count; ++index)
        {
          RadCommandBarBaseItem commandBarBaseItem = (RadCommandBarBaseItem) this.List[index];
          if (commandBarBaseItem.Name == itemName)
            return commandBarBaseItem;
        }
        return (RadCommandBarBaseItem) null;
      }
    }

    protected void OnSortComplete()
    {
      if (this.owner == null)
        return;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorted);
    }

    public class RadCommandBarBaseItemEnumerator : IEnumerator<RadCommandBarBaseItem>, IDisposable, IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public RadCommandBarBaseItemEnumerator(RadCommandBarBaseItemCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public RadCommandBarBaseItem Current
      {
        get
        {
          return (RadCommandBarBaseItem) this.baseEnumerator.Current;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return this.baseEnumerator.Current;
        }
      }

      public bool MoveNext()
      {
        return this.baseEnumerator.MoveNext();
      }

      bool IEnumerator.MoveNext()
      {
        return this.baseEnumerator.MoveNext();
      }

      public void Reset()
      {
        this.baseEnumerator.Reset();
      }

      void IEnumerator.Reset()
      {
        this.baseEnumerator.Reset();
      }

      void IDisposable.Dispose()
      {
      }
    }
  }
}
