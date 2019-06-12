// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.HybridIndex`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Telerik.WinControls.Data;

namespace Telerik.Collections.Generic
{
  public class HybridIndex<T> : Index<T> where T : IDataItem
  {
    private IList<T> items = (IList<T>) new List<T>();
    private int version = -2147483647;
    private int threshold = 10000;
    private const int DATA_STRUCTURE_THRESHOLD = 10000;
    private const int ITEM_CAPACITY = 64;
    private IEnumerable<T> source;
    private bool multithreadedFiltering;

    public bool MultithreadedFiltering
    {
      get
      {
        return this.multithreadedFiltering;
      }
      set
      {
        this.multithreadedFiltering = value;
      }
    }

    public int Threshold
    {
      get
      {
        return this.threshold;
      }
      set
      {
        if (this.threshold == value)
          return;
        this.threshold = value;
        this.Perform();
      }
    }

    public HybridIndex(RadCollectionView<T> collectionView)
      : base(collectionView)
    {
      this.source = collectionView.SourceCollection;
      this.InitializeItems();
    }

    public HybridIndex(RadCollectionView<T> collectionView, IEnumerable<T> source)
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
      if (this.CollectionView.BypassFilter && this.CollectionView.BypassSort)
      {
        this.items.Clear();
        foreach (T obj in this.source)
          this.items.Add(obj);
        this.version = this.CollectionView.Version;
      }
      else
      {
        IList source = this.source as IList;
        lock (this)
        {
          if (source != null && source.Count <= this.Threshold)
            this.PerformWithQuickSort();
          else
            this.PerformWithBinaryTree();
        }
      }
    }

    protected virtual void PerformWithBinaryTree()
    {
      if (this.version == this.CollectionView.Version)
        return;
      RBOrderedMultiTree<T> orderedMultiTree = (RBOrderedMultiTree<T>) null;
      IEnumerable<T> e = this.CollectionView.IsIncrementalFiltering ? (IEnumerable<T>) this.items : this.source;
      if (this.CollectionView.CanSort && this.CollectionView.SortDescriptors.Count != 0)
        orderedMultiTree = new RBOrderedMultiTree<T>(this.CollectionView.Comparer);
      else
        this.items = (IList<T>) new List<T>(64);
      if (this.CollectionView.CanFilter && this.CollectionView.Filter != null)
      {
        if (orderedMultiTree != null)
        {
          foreach (T filteredItem in this.GetFilteredItems(e))
            orderedMultiTree.Add(filteredItem);
          this.items = orderedMultiTree.Collection;
        }
        else
        {
          this.items.Clear();
          foreach (T filteredItem in this.GetFilteredItems(e))
            this.items.Add(filteredItem);
        }
        this.version = this.CollectionView.Version;
      }
      else if (this.CollectionView.CanSort && this.CollectionView.SortDescriptors.Count != 0)
      {
        if (orderedMultiTree != null)
        {
          foreach (T aKey in e)
            orderedMultiTree.Add(aKey);
          this.items = orderedMultiTree.Collection;
        }
        else
        {
          this.items.Clear();
          foreach (T obj in e)
            this.items.Add(obj);
        }
        this.version = this.CollectionView.Version;
      }
      else
      {
        this.InitializeItems();
        this.version = this.CollectionView.Version;
      }
    }

    protected virtual void PerformWithQuickSort()
    {
      if (this.version == this.CollectionView.Version)
        return;
      IEnumerable<T> e = this.CollectionView.IsIncrementalFiltering ? (IEnumerable<T>) this.items : this.source;
      bool flag = this.CollectionView.CanSort && this.CollectionView.SortDescriptors.Count != 0;
      this.items = (IList<T>) new List<T>(64);
      if (this.CollectionView.CanFilter && this.CollectionView.Filter != null)
      {
        this.items.Clear();
        foreach (T filteredItem in this.GetFilteredItems(e))
        {
          if (flag && !this.CollectionView.BypassSort)
          {
            List<T> items = (List<T>) this.items;
            int index = items.BinarySearch(filteredItem, this.CollectionView.Comparer);
            if (index < 0)
            {
              items.Insert(index * -1 - 1, filteredItem);
            }
            else
            {
              do
                ;
              while (++index < items.Count && this.CollectionView.Comparer.Compare(filteredItem, items[index]) == 0);
              items.Insert(index, filteredItem);
            }
          }
          else
            this.items.Add(filteredItem);
        }
        this.version = this.CollectionView.Version;
      }
      else if (this.CollectionView.CanSort && this.CollectionView.SortDescriptors.Count != 0)
      {
        this.items.Clear();
        foreach (T x in e)
        {
          if (flag && !this.CollectionView.BypassSort)
          {
            List<T> items = (List<T>) this.items;
            int index = items.BinarySearch(x, this.CollectionView.Comparer);
            if (index < 0)
            {
              items.Insert(index * -1 - 1, x);
            }
            else
            {
              do
                ;
              while (++index < items.Count && this.CollectionView.Comparer.Compare(x, items[index]) == 0);
              items.Insert(index, x);
            }
          }
          else
            this.items.Add(x);
        }
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

    protected virtual List<T> GetFilteredItems(IEnumerable<T> e)
    {
      List<T> objList = new List<T>();
      this.CollectionView.mainThreadId = Thread.CurrentThread.ManagedThreadId;
      if (this.MultithreadedFiltering)
      {
        this.CollectionView.ClearThreadedFilterNodes();
        Thread[] threadArray = new Thread[Environment.ProcessorCount];
        HybridIndex<T>.ThreadedFilteringArgument[] filteringArgumentArray = new HybridIndex<T>.ThreadedFilteringArgument[Environment.ProcessorCount];
        int num = 0;
        foreach (T obj in e)
          ++num;
        for (int index = 0; index < threadArray.Length; ++index)
        {
          threadArray[index] = new Thread(new ParameterizedThreadStart(this.FilterTask));
          filteringArgumentArray[index] = new HybridIndex<T>.ThreadedFilteringArgument();
          filteringArgumentArray[index].Enumeration = e;
          filteringArgumentArray[index].FirstIndex = index * num / threadArray.Length;
          filteringArgumentArray[index].LastIndex = index == threadArray.Length - 1 ? num - 1 : (index + 1) * num / threadArray.Length - 1;
        }
        for (int index = 0; index < threadArray.Length; ++index)
          threadArray[index].Start((object) filteringArgumentArray[index]);
        for (int index = 0; index < threadArray.Length; ++index)
          threadArray[index].Join();
        for (int index = 0; index < threadArray.Length; ++index)
        {
          if (filteringArgumentArray[index].Result != null)
            objList.AddRange((IEnumerable<T>) filteringArgumentArray[index].Result);
        }
      }
      else
      {
        foreach (T obj in e)
        {
          if (this.CollectionView.PassesFilter(obj))
            objList.Add(obj);
        }
      }
      return objList;
    }

    private void FilterTask(object parameter)
    {
      HybridIndex<T>.ThreadedFilteringArgument filteringArgument = parameter as HybridIndex<T>.ThreadedFilteringArgument;
      if (filteringArgument == null)
        return;
      int num = 0;
      filteringArgument.Result = new List<T>();
      foreach (T obj in filteringArgument.Enumeration)
      {
        if (num >= filteringArgument.FirstIndex && num <= filteringArgument.LastIndex && this.CollectionView.PassesFilter(obj))
          filteringArgument.Result.Add(obj);
        ++num;
      }
    }

    private class ThreadedFilteringArgument
    {
      public List<T> Result;
      public IEnumerable<T> Enumeration;
      public int FirstIndex;
      public int LastIndex;
    }
  }
}
