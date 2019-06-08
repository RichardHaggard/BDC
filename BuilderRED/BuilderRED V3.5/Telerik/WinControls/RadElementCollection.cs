// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadElementCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [DebuggerDisplay("Count = {Count}")]
  [Serializable]
  public class RadElementCollection : CollectionBase, IEnumerable<RadElement>, IEnumerable
  {
    private bool checkValid = true;
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool checkForAlreadyAddedItems = true;
    private RadElement owner;
    private bool suspended;

    public RadElementCollection(RadElement owner)
      : base(1)
    {
      this.owner = owner;
    }

    public RadElementCollection(RadElement owner, RadElementCollection value)
      : base(1)
    {
      this.owner = owner;
      this.AddRange(value);
    }

    public RadElementCollection(RadElement owner, RadElement[] value)
      : base(1)
    {
      this.owner = owner;
      this.AddRange(value);
    }

    public RadElement this[int index]
    {
      get
      {
        return (RadElement) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public RadElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public int Add(RadElement value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(params RadElement[] value)
    {
      this.Capacity = this.Count + value.Length;
      this.suspended = true;
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
      this.suspended = false;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.BatchInsert);
    }

    public void AddRange(IList<RadElement> value)
    {
      this.Capacity = this.Count + value.Count;
      this.suspended = true;
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
      this.suspended = false;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.BatchInsert);
    }

    public void AddRange(RadElementCollection value)
    {
      this.Capacity = this.Count + value.Count;
      this.suspended = true;
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
      this.suspended = false;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.BatchInsert);
    }

    public bool Contains(RadElement value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(RadElement[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(RadElement value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, RadElement value)
    {
      this.List.Insert(index, (object) value);
    }

    public RadElementCollection.RadElementEnumerator GetEnumerator()
    {
      return new RadElementCollection.RadElementEnumerator(this);
    }

    public void Remove(RadElement value)
    {
      this.List.Remove((object) value);
    }

    protected override void OnValidate(object value)
    {
      if (value is RadElement || value == null)
        return;
      int num = (int) MessageBox.Show("Error", "Error when adding " + value.GetType().FullName + " From " + value.GetType().Assembly.Location + " into " + typeof (RadElement).Assembly.FullName + " " + typeof (RadElement).Assembly.Location);
    }

    public void Sort()
    {
      if (!this.suspended)
        this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorting);
      this.InnerList.Sort();
      if (this.suspended)
        return;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorted);
    }

    public void Sort(IComparer comparer)
    {
      if (!this.suspended)
        this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorting);
      this.InnerList.Sort(comparer);
      if (this.suspended)
        return;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorted);
    }

    public void Sort(int index, int count, IComparer comparer)
    {
      if (!this.suspended)
        this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorting);
      this.InnerList.Sort(index, count, comparer);
      if (this.suspended)
        return;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorted);
    }

    public void Move(int indexFrom, int indexTo)
    {
      if (indexFrom < 0 || indexFrom >= this.List.Count || (indexTo < 0 || indexTo > this.List.Count))
        return;
      this.checkForAlreadyAddedItems = false;
      this.suspended = true;
      object obj = this.List[indexFrom];
      this.List.RemoveAt(indexFrom);
      this.List.Insert(indexTo, obj);
      this.suspended = false;
      this.owner.ResetZOrderCache();
      this.checkForAlreadyAddedItems = true;
    }

    public void SwitchItems(int indexFrom, int indexTo)
    {
      if (indexFrom < 0 || indexFrom >= this.List.Count || (indexTo < 0 || indexTo > this.List.Count))
        return;
      this.checkValid = false;
      RadElement radElement = (RadElement) this.List[indexTo];
      this.List[indexTo] = this.List[indexFrom];
      this.List[indexFrom] = (object) radElement;
      this.checkValid = true;
    }

    private void CheckElementAlreadyAdded(RadElement element)
    {
      if (element.Parent == this.owner)
        throw new InvalidOperationException("Element already added");
      RadElement parent = element.Parent;
      if (parent == null || parent == this.owner)
        return;
      parent.Children.Remove(element);
    }

    protected override void OnInsert(int index, object value)
    {
      RadElement radElement = value as RadElement;
      if (this.checkForAlreadyAddedItems)
        this.CheckElementAlreadyAdded(radElement);
      if (!this.suspended)
        this.owner.ChangeCollection(radElement, ItemsChangeOperation.Inserting);
      base.OnInsert(index, value);
    }

    protected override void OnInsertComplete(int index, object value)
    {
      if (!this.suspended)
        this.owner.ChangeCollection(value as RadElement, ItemsChangeOperation.Inserted);
      base.OnInsertComplete(index, value);
    }

    protected override void OnSet(int index, object oldValue, object newValue)
    {
      RadElement radElement = newValue as RadElement;
      if (this.checkValid)
        this.CheckElementAlreadyAdded(radElement);
      if (!this.suspended)
        this.owner.ChangeCollection(radElement, ItemsChangeOperation.Setting);
      base.OnSet(index, oldValue, newValue);
    }

    protected override void OnSetComplete(int index, object oldValue, object newValue)
    {
      base.OnSetComplete(index, oldValue, newValue);
      if (this.suspended)
        return;
      this.owner.ChangeCollection(newValue as RadElement, ItemsChangeOperation.Set);
    }

    protected override void OnRemove(int index, object value)
    {
      if (!this.suspended)
        this.owner.ChangeCollection(value as RadElement, ItemsChangeOperation.Removing);
      base.OnRemove(index, value);
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      if (this.suspended)
        return;
      this.owner.ChangeCollection(value as RadElement, ItemsChangeOperation.Removed);
    }

    protected override void OnClear()
    {
      if (!this.suspended)
        this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Clearing);
      base.OnClear();
    }

    protected override void OnClearComplete()
    {
      base.OnClearComplete();
      if (this.suspended)
        return;
      this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Cleared);
    }

    public int BinarySearch(int index, int count, RadElement value, IComparer comparer)
    {
      return this.InnerList.BinarySearch(index, count, (object) value, comparer);
    }

    IEnumerator<RadElement> IEnumerable<RadElement>.GetEnumerator()
    {
      return (IEnumerator<RadElement>) new RadElementCollection.RadElementEnumerator(this);
    }

    public class RadElementEnumerator : IEnumerator<RadElement>, IDisposable, IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public RadElementEnumerator(RadElementCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public RadElement Current
      {
        get
        {
          return (RadElement) this.baseEnumerator.Current;
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
