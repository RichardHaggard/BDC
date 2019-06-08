// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Elements.RadItemReadOnlyCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Elements
{
  [ListBindable(false)]
  public class RadItemReadOnlyCollection : ReadOnlyCollectionBase, IEnumerable<RadItem>, IEnumerable
  {
    public event ItemChangedDelegate ItemsChanged;

    public RadItemReadOnlyCollection(RadItemOwnerCollection value)
    {
      this.AddRange(value);
    }

    public RadItemReadOnlyCollection(RadItem[] value)
    {
      this.AddRange(value);
    }

    protected virtual void OnItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      ItemChangedDelegate itemsChanged = this.ItemsChanged;
      if (itemsChanged == null)
        return;
      itemsChanged(changed, target, operation);
    }

    public RadItem this[int index]
    {
      get
      {
        return (RadItem) this.InnerList[index];
      }
    }

    public bool Contains(RadItem value)
    {
      return this.InnerList.Contains((object) value);
    }

    public void CopyTo(RadItem[] array, int index)
    {
      this.InnerList.CopyTo((Array) array, index);
    }

    public int IndexOf(RadItem value)
    {
      return this.InnerList.IndexOf((object) value);
    }

    public RadItemReadOnlyCollection.RadItemReadOnlyEnumerator GetEnumerator()
    {
      return new RadItemReadOnlyCollection.RadItemReadOnlyEnumerator(this);
    }

    internal int Add(RadItem value)
    {
      return this.InnerList.Add((object) value);
    }

    internal void AddRange(RadItem[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    internal void AddRange(RadItemOwnerCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    IEnumerator<RadItem> IEnumerable<RadItem>.GetEnumerator()
    {
      return (IEnumerator<RadItem>) new RadItemReadOnlyCollection.RadItemReadOnlyEnumerator(this);
    }

    public RadItem[] ToArray()
    {
      RadItem[] array = new RadItem[this.Count];
      this.CopyTo(array, 0);
      return array;
    }

    public void Sort()
    {
      this.InnerList.Sort();
    }

    public void Sort(IComparer comparer)
    {
      this.InnerList.Sort(comparer);
    }

    public void Sort(int index, int count, IComparer comparer)
    {
      this.InnerList.Sort(index, count, comparer);
    }

    public class RadItemReadOnlyEnumerator : IEnumerator<RadItem>, IDisposable, IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public RadItemReadOnlyEnumerator(RadItemReadOnlyCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public RadItem Current
      {
        get
        {
          return (RadItem) this.baseEnumerator.Current;
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
