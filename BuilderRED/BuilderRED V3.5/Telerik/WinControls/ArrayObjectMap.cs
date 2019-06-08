// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ArrayObjectMap
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  internal class ArrayObjectMap : FrugalMapBase
  {
    private const int GROWTH = 3;
    private const int MAXSIZE = 15;
    private const int MINSIZE = 9;
    private ushort _count;
    private FrugalMapBase.Entry[] _entries;
    private bool _sorted;

    private int Compare(int left, int right)
    {
      return this._entries[left].Key - this._entries[right].Key;
    }

    public override void GetKeyValuePair(int index, out int key, out object value)
    {
      if (index < (int) this._count)
      {
        value = this._entries[index].Value;
        key = this._entries[index].Key;
      }
      else
      {
        value = RadProperty.UnsetValue;
        key = int.MaxValue;
        throw new ArgumentOutOfRangeException(nameof (index));
      }
    }

    public override FrugalMapStoreState InsertEntry(int key, object value)
    {
      for (int index = 0; index < (int) this._count; ++index)
      {
        if (this._entries[index].Key == key)
        {
          this._entries[index].Value = value;
          return FrugalMapStoreState.Success;
        }
      }
      if ((ushort) 15 <= this._count)
        return FrugalMapStoreState.SortedArray;
      if (this._entries != null)
      {
        this._sorted = false;
        if (this._entries.Length <= (int) this._count)
        {
          FrugalMapBase.Entry[] entryArray = new FrugalMapBase.Entry[this._entries.Length + 3];
          Array.Copy((Array) this._entries, 0, (Array) entryArray, 0, this._entries.Length);
          this._entries = entryArray;
        }
      }
      else
      {
        this._entries = new FrugalMapBase.Entry[9];
        this._sorted = true;
      }
      this._entries[(int) this._count].Key = key;
      this._entries[(int) this._count].Value = value;
      ++this._count;
      return FrugalMapStoreState.Success;
    }

    public override void Iterate(ArrayList list, FrugalMapIterationCallback callback)
    {
      if (this._count <= (ushort) 0)
        return;
      for (int index = 0; index < (int) this._count; ++index)
        callback(list, this._entries[index].Key, this._entries[index].Value);
    }

    private int Partition(int left, int right)
    {
      int num = right;
      int index1 = left - 1;
      int index2 = right;
      while (true)
      {
        do
          ;
        while (this.Compare(++index1, num) < 0);
        do
          ;
        while (this.Compare(num, --index2) < 0 && index2 != left);
        if (index1 < index2)
        {
          FrugalMapBase.Entry entry = this._entries[index2];
          this._entries[index2] = this._entries[index1];
          this._entries[index1] = entry;
        }
        else
          break;
      }
      FrugalMapBase.Entry entry1 = this._entries[right];
      this._entries[right] = this._entries[index1];
      this._entries[index1] = entry1;
      return index1;
    }

    public override void Promote(FrugalMapBase newMap)
    {
      for (int index = 0; index < this._entries.Length; ++index)
      {
        if (newMap.InsertEntry(this._entries[index].Key, this._entries[index].Value) != FrugalMapStoreState.Success)
          throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData", new object[3]
          {
            (object) this.ToString(),
            (object) newMap.ToString(),
            (object) nameof (newMap)
          }));
      }
    }

    private void QSort(int left, int right)
    {
      if (left >= right)
        return;
      int num = this.Partition(left, right);
      this.QSort(left, num - 1);
      this.QSort(num + 1, right);
    }

    public override void RemoveEntry(int key)
    {
      for (int destinationIndex = 0; destinationIndex < (int) this._count; ++destinationIndex)
      {
        if (this._entries[destinationIndex].Key == key)
        {
          int length = (int) this._count - destinationIndex - 1;
          if (length > 0)
            Array.Copy((Array) this._entries, destinationIndex + 1, (Array) this._entries, destinationIndex, length);
          this._entries[(int) this._count - 1].Key = int.MaxValue;
          this._entries[(int) this._count - 1].Value = RadProperty.UnsetValue;
          --this._count;
          break;
        }
      }
    }

    public override object Search(int key)
    {
      for (int index = 0; index < (int) this._count; ++index)
      {
        if (key == this._entries[index].Key)
          return this._entries[index].Value;
      }
      return RadProperty.UnsetValue;
    }

    public override void Sort()
    {
      if (this._sorted || this._count <= (ushort) 1)
        return;
      this.QSort(0, (int) this._count - 1);
      this._sorted = true;
    }

    public override int Count
    {
      get
      {
        return (int) this._count;
      }
    }
  }
}
