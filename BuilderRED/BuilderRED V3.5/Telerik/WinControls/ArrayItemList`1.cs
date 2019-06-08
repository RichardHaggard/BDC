// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ArrayItemList`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  internal class ArrayItemList<T> : FrugalListBase<T>
  {
    private const int GROWTH = 3;
    private const int LARGEGROWTH = 18;
    private const int MINSIZE = 9;
    private T[] _entries;

    public ArrayItemList()
    {
    }

    public ArrayItemList(int size)
    {
      size += 2;
      size -= size % 3;
      this._entries = new T[size];
    }

    public override FrugalListStoreState Add(T value)
    {
      if (this._entries != null && this._count < this._entries.Length)
      {
        this._entries[this._count] = value;
        ++this._count;
      }
      else
      {
        if (this._entries != null)
        {
          int length = this._entries.Length;
          T[] objArray = new T[length >= 18 ? length + (length >> 2) : length + 3];
          Array.Copy((Array) this._entries, 0, (Array) objArray, 0, this._entries.Length);
          this._entries = objArray;
        }
        else
          this._entries = new T[9];
        this._entries[this._count] = value;
        ++this._count;
      }
      return FrugalListStoreState.Success;
    }

    public override void Clear()
    {
      for (int index = 0; index < this._count; ++index)
      {
        T obj = default (T);
        this._entries[index] = obj;
      }
      this._count = 0;
    }

    public override object Clone()
    {
      ArrayItemList<T> arrayItemList = new ArrayItemList<T>(this.Capacity);
      arrayItemList.Promote(this);
      return (object) arrayItemList;
    }

    public override bool Contains(T value)
    {
      return -1 != this.IndexOf(value);
    }

    public override void CopyTo(T[] array, int index)
    {
      for (int index1 = 0; index1 < this._count; ++index1)
        array[index + index1] = this._entries[index1];
    }

    public override T EntryAt(int index)
    {
      return this._entries[index];
    }

    public override int IndexOf(T value)
    {
      for (int index = 0; index < this._count; ++index)
      {
        if (this._entries[index].Equals((object) value))
          return index;
      }
      return -1;
    }

    public override void Insert(int index, T value)
    {
      if (this._entries == null || this._count >= this._entries.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      Array.Copy((Array) this._entries, index, (Array) this._entries, index + 1, this._count - index);
      this._entries[index] = value;
      ++this._count;
    }

    public void Promote(ArrayItemList<T> oldList)
    {
      int count = oldList.Count;
      if (this._entries.Length >= count)
      {
        this.SetCount(oldList.Count);
        for (int index = 0; index < count; ++index)
          this.SetAt(index, oldList.EntryAt(index));
      }
      else
        throw new ArgumentException(string.Format("FrugalList_TargetMapCannotHoldAllData", new object[3]
        {
          (object) oldList.ToString(),
          (object) this.ToString(),
          (object) nameof (oldList)
        }));
    }

    public override void Promote(FrugalListBase<T> oldList)
    {
      for (int index = 0; index < oldList.Count; ++index)
      {
        if (this.Add(oldList.EntryAt(index)) != FrugalListStoreState.Success)
          throw new ArgumentException(string.Format("FrugalList_TargetMapCannotHoldAllData", new object[3]
          {
            (object) oldList.ToString(),
            (object) this.ToString(),
            (object) nameof (oldList)
          }));
      }
    }

    public void Promote(SixItemList<T> oldList)
    {
      int count = oldList.Count;
      this.SetCount(oldList.Count);
      switch (count)
      {
        case 0:
          break;
        case 1:
          this.SetAt(0, oldList.EntryAt(0));
          break;
        case 2:
          this.SetAt(0, oldList.EntryAt(0));
          this.SetAt(1, oldList.EntryAt(1));
          break;
        case 3:
          this.SetAt(0, oldList.EntryAt(0));
          this.SetAt(1, oldList.EntryAt(1));
          this.SetAt(2, oldList.EntryAt(2));
          break;
        case 4:
          this.SetAt(0, oldList.EntryAt(0));
          this.SetAt(1, oldList.EntryAt(1));
          this.SetAt(2, oldList.EntryAt(2));
          this.SetAt(3, oldList.EntryAt(3));
          break;
        case 5:
          this.SetAt(0, oldList.EntryAt(0));
          this.SetAt(1, oldList.EntryAt(1));
          this.SetAt(2, oldList.EntryAt(2));
          this.SetAt(3, oldList.EntryAt(3));
          this.SetAt(4, oldList.EntryAt(4));
          break;
        case 6:
          this.SetAt(0, oldList.EntryAt(0));
          this.SetAt(1, oldList.EntryAt(1));
          this.SetAt(2, oldList.EntryAt(2));
          this.SetAt(3, oldList.EntryAt(3));
          this.SetAt(4, oldList.EntryAt(4));
          this.SetAt(5, oldList.EntryAt(5));
          break;
        default:
          throw new ArgumentOutOfRangeException("index");
      }
    }

    public override bool Remove(T value)
    {
      for (int index = 0; index < this._count; ++index)
      {
        if (this._entries[index].Equals((object) value))
        {
          this.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public override void RemoveAt(int index)
    {
      int length = this._count - index - 1;
      if (length > 0)
        Array.Copy((Array) this._entries, index + 1, (Array) this._entries, index, length);
      this._entries[this._count - 1] = default (T);
      --this._count;
    }

    public override void SetAt(int index, T value)
    {
      this._entries[index] = value;
    }

    private void SetCount(int value)
    {
      if (value < 0 || value > this._entries.Length)
        throw new ArgumentOutOfRangeException("Count");
      this._count = value;
    }

    public override void Sort()
    {
      Array.Sort<T>(this._entries, 0, this._count);
    }

    public override T[] ToArray()
    {
      T[] objArray = new T[this._count];
      for (int index = 0; index < this._count; ++index)
        objArray[index] = this._entries[index];
      return objArray;
    }

    public override int Capacity
    {
      get
      {
        if (this._entries != null)
          return this._entries.Length;
        return 0;
      }
    }
  }
}
