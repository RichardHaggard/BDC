// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.FrugalObjectList`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  internal class FrugalObjectList<T>
  {
    internal FrugalListBase<T> _listStore;

    public FrugalObjectList()
    {
    }

    public FrugalObjectList(int size)
    {
      this.Capacity = size;
    }

    public int Add(T value)
    {
      if (this._listStore == null)
        this._listStore = (FrugalListBase<T>) new SingleItemList<T>();
      FrugalListStoreState frugalListStoreState = this._listStore.Add(value);
      if (frugalListStoreState != FrugalListStoreState.Success)
      {
        if (FrugalListStoreState.ThreeItemList == frugalListStoreState)
        {
          ThreeItemList<T> threeItemList = new ThreeItemList<T>();
          threeItemList.Promote(this._listStore);
          int num = (int) threeItemList.Add(value);
          this._listStore = (FrugalListBase<T>) threeItemList;
        }
        else if (FrugalListStoreState.SixItemList == frugalListStoreState)
        {
          SixItemList<T> sixItemList = new SixItemList<T>();
          sixItemList.Promote(this._listStore);
          this._listStore = (FrugalListBase<T>) sixItemList;
          int num = (int) sixItemList.Add(value);
          this._listStore = (FrugalListBase<T>) sixItemList;
        }
        else
        {
          if (FrugalListStoreState.Array != frugalListStoreState)
            throw new InvalidOperationException("FrugalList_CannotPromoteBeyondArray");
          ArrayItemList<T> arrayItemList = new ArrayItemList<T>(this._listStore.Count + 1);
          arrayItemList.Promote(this._listStore);
          this._listStore = (FrugalListBase<T>) arrayItemList;
          int num = (int) arrayItemList.Add(value);
          this._listStore = (FrugalListBase<T>) arrayItemList;
        }
      }
      return this._listStore.Count - 1;
    }

    public void Clear()
    {
      if (this._listStore == null)
        return;
      this._listStore.Clear();
    }

    public FrugalObjectList<T> Clone()
    {
      FrugalObjectList<T> frugalObjectList = new FrugalObjectList<T>();
      if (this._listStore != null)
        frugalObjectList._listStore = (FrugalListBase<T>) this._listStore.Clone();
      return frugalObjectList;
    }

    public bool Contains(T value)
    {
      if (this._listStore != null && this._listStore.Count > 0)
        return this._listStore.Contains(value);
      return false;
    }

    public void CopyTo(T[] array, int index)
    {
      if (this._listStore == null || this._listStore.Count <= 0)
        return;
      this._listStore.CopyTo(array, index);
    }

    public void EnsureIndex(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      int num1 = index + 1 - this.Count;
      if (num1 <= 0)
        return;
      this.Capacity = index + 1;
      T obj = default (T);
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int num2 = (int) this._listStore.Add(obj);
      }
    }

    public int IndexOf(T value)
    {
      if (this._listStore != null && this._listStore.Count > 0)
        return this._listStore.IndexOf(value);
      return -1;
    }

    public void Insert(int index, T value)
    {
      if (index != 0 && (this._listStore == null || index > this._listStore.Count || index < 0))
        throw new ArgumentOutOfRangeException(nameof (index));
      int num = 1;
      if (this._listStore != null && this._listStore.Count == this._listStore.Capacity)
        num = this.Capacity + 1;
      this.Capacity = num;
      this._listStore.Insert(index, value);
    }

    public bool Remove(T value)
    {
      if (this._listStore != null && this._listStore.Count > 0)
        return this._listStore.Remove(value);
      return false;
    }

    public void RemoveAt(int index)
    {
      if (this._listStore == null || index >= this._listStore.Count || index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      this._listStore.RemoveAt(index);
    }

    public void Sort()
    {
      if (this._listStore == null || this._listStore.Count <= 0)
        return;
      this._listStore.Sort();
    }

    public T[] ToArray()
    {
      if (this._listStore != null && this._listStore.Count > 0)
        return this._listStore.ToArray();
      return (T[]) null;
    }

    public int Capacity
    {
      get
      {
        if (this._listStore != null)
          return this._listStore.Capacity;
        return 0;
      }
      set
      {
        int num = 0;
        if (this._listStore != null)
          num = this._listStore.Capacity;
        if (num >= value)
          return;
        FrugalListBase<T> frugalListBase = value != 1 ? (value > 3 ? (value > 6 ? (FrugalListBase<T>) new ArrayItemList<T>(value) : (FrugalListBase<T>) new SixItemList<T>()) : (FrugalListBase<T>) new ThreeItemList<T>()) : (FrugalListBase<T>) new SingleItemList<T>();
        if (this._listStore != null)
          frugalListBase.Promote(this._listStore);
        this._listStore = frugalListBase;
      }
    }

    public int Count
    {
      get
      {
        if (this._listStore != null)
          return this._listStore.Count;
        return 0;
      }
    }

    public T this[int index]
    {
      get
      {
        if (this._listStore == null || index >= this._listStore.Count || index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        return this._listStore.EntryAt(index);
      }
      set
      {
        if (this._listStore == null || index >= this._listStore.Count || index < 0)
          throw new ArgumentOutOfRangeException(nameof (index));
        this._listStore.SetAt(index, value);
      }
    }
  }
}
