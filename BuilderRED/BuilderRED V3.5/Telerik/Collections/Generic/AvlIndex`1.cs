// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.AvlIndex`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.Collections.Generic
{
  public class AvlIndex<T> : Index<T> where T : IDataItem
  {
    private IList<T> items = (IList<T>) new List<T>();
    private int version = -2147483647;
    private const int ITEM_CAPACITY = 64;
    private IEnumerable<T> source;

    public AvlIndex(RadCollectionView<T> collectionView)
      : base(collectionView)
    {
      this.source = collectionView.SourceCollection;
      this.InitializeItems();
    }

    public AvlIndex(RadCollectionView<T> collectionView, IEnumerable<T> source)
      : base(collectionView)
    {
      this.source = source;
      this.InitializeItems();
    }

    public override IList<T> Items
    {
      get
      {
        this.Perform();
        return this.items;
      }
    }

    protected internal override void SetDirty()
    {
      ++this.version;
    }

    public override void Load(IEnumerable<T> source)
    {
      this.source = source;
      this.InitializeItems();
    }

    protected override void Perform()
    {
      if (this.version == this.CollectionView.Version)
        return;
      IEnumerable<T> objs = this.CollectionView.IsIncrementalFiltering ? (IEnumerable<T>) this.items : this.source;
      this.items = !this.CollectionView.CanSort || this.CollectionView.SortDescriptors.Count == 0 ? (IList<T>) new List<T>(64) : (IList<T>) new AvlTree<T>(this.CollectionView.Comparer);
      if (this.CollectionView.CanFilter && this.CollectionView.Filter != null)
      {
        this.items.Clear();
        foreach (T obj in objs)
        {
          if (this.CollectionView.PassesFilter(obj))
            this.items.Add(obj);
        }
        this.version = this.CollectionView.Version;
      }
      else if (this.CollectionView.CanSort && this.CollectionView.SortDescriptors.Count != 0)
      {
        this.items.Clear();
        foreach (T obj in objs)
          this.items.Add(obj);
        this.version = this.CollectionView.Version;
      }
      else
      {
        this.InitializeItems();
        this.version = this.CollectionView.Version;
      }
    }

    private void InitializeItems()
    {
      if (this.source is IList<T>)
      {
        this.items = (IList<T>) this.source;
      }
      else
      {
        this.items = (IList<T>) new List<T>((int) byte.MaxValue);
        IEnumerator<T> enumerator = this.source.GetEnumerator();
        while (enumerator.MoveNext())
          this.items.Add(enumerator.Current);
        this.version = int.MinValue;
      }
    }
  }
}
