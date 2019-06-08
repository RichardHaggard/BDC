// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ItemStructList`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  internal struct ItemStructList<T>
  {
    public int Count;
    public T[] List;

    public ItemStructList(int capacity)
    {
      this.List = new T[capacity];
      this.Count = 0;
    }

    public int Add()
    {
      return this.Add(1, true);
    }

    public int Add(int delta)
    {
      return this.Add(delta, true);
    }

    public void Add(ref T item)
    {
      this.List[this.Add(1, false)] = item;
      ++this.Count;
    }

    public void Add(T item)
    {
      this.List[this.Add(1, false)] = item;
      ++this.Count;
    }

    private int Add(int delta, bool incrCount)
    {
      if (this.List != null)
      {
        if (this.Count + delta > this.List.Length)
        {
          T[] objArray = new T[Math.Max(this.List.Length * 2, this.Count + delta)];
          this.List.CopyTo((Array) objArray, 0);
          this.List = objArray;
        }
      }
      else
        this.List = new T[Math.Max(delta, 2)];
      int count = this.Count;
      if (incrCount)
        this.Count += delta;
      return count;
    }

    public void AppendTo(ref ItemStructList<T> destinationList)
    {
      for (int index = 0; index < this.Count; ++index)
        destinationList.Add(ref this.List[index]);
    }

    public void Clear()
    {
      Array.Clear((Array) this.List, 0, this.Count);
      this.Count = 0;
    }

    public bool Contains(T value)
    {
      return this.IndexOf(value) != -1;
    }

    public void EnsureIndex(int index)
    {
      int delta = index + 1 - this.Count;
      if (delta <= 0)
        return;
      this.Add(delta);
    }

    public int IndexOf(T value)
    {
      int num = -1;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this.List[index].Equals((object) value))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public bool IsValidIndex(int index)
    {
      if (index >= 0)
        return index < this.Count;
      return false;
    }

    public void Remove(T value)
    {
      int destinationIndex = this.IndexOf(value);
      if (destinationIndex == -1)
        return;
      Array.Copy((Array) this.List, destinationIndex + 1, (Array) this.List, destinationIndex, this.Count - destinationIndex - 1);
      Array.Clear((Array) this.List, this.Count - 1, 1);
      --this.Count;
    }

    public void Sort()
    {
      if (this.List == null)
        return;
      Array.Sort<T>(this.List, 0, this.Count);
    }

    public T[] ToArray()
    {
      T[] objArray = new T[this.Count];
      Array.Copy((Array) this.List, 0, (Array) objArray, 0, this.Count);
      return objArray;
    }
  }
}
