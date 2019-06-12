// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.Index`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.Collections.Generic
{
  public abstract class Index<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
    where T : IDataItem
  {
    private RadCollectionView<T> collectionView;

    public Index(RadCollectionView<T> collectionView)
    {
      this.collectionView = collectionView;
    }

    public RadCollectionView<T> CollectionView
    {
      get
      {
        return this.collectionView;
      }
    }

    public abstract IList<T> Items { get; }

    public virtual IList<T> GetItemsOnPage(int pageIndex)
    {
      IList<T> objList = (IList<T>) new List<T>(this.CollectionView.PageSize);
      int num = pageIndex * this.CollectionView.PageSize;
      for (int index = num; index < num + this.CollectionView.PageSize && index < this.Items.Count; ++index)
        objList.Add(this.Items[index]);
      return objList;
    }

    public virtual int GetItemPageIndex(T item)
    {
      return this.IndexOf(item) / this.CollectionView.PageSize;
    }

    protected internal virtual void SetDirty()
    {
    }

    public virtual void Load(IEnumerable<T> source)
    {
    }

    protected abstract void Perform();

    public int Count
    {
      get
      {
        return this.Items.Count;
      }
    }

    public T this[int index]
    {
      get
      {
        return this.Items[index];
      }
    }

    public bool Contains(T value)
    {
      return this.Items.Contains(value);
    }

    public void CopyTo(T[] array, int index)
    {
      this.Items.CopyTo(array, index);
    }

    public int IndexOf(T value)
    {
      return this.Items.IndexOf(value);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
