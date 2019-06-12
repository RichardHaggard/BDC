// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.SnapshotCollectionView`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Collections.Generic;

namespace Telerik.WinControls.Data
{
  public class SnapshotCollectionView<TDataItem> : ISnapshotCollectionView<TDataItem>, ICollectionView<TDataItem>, IReadOnlyCollection<TDataItem>, IEnumerable<TDataItem>, IEnumerable
    where TDataItem : IDataItem
  {
    private RadCollectionView<TDataItem> sourceView;
    private IEnumerable<TDataItem> sourceCollection;
    private Index<TDataItem> index;
    private Telerik.WinControls.Data.GroupBuilder<TDataItem> groupBuilder;

    public SnapshotCollectionView(
      IEnumerable<TDataItem> sourceCollection,
      RadCollectionView<TDataItem> sourceView)
    {
      this.sourceView = sourceView;
      this.sourceCollection = sourceCollection;
      RadCollectionView<TDataItem>.RebuildDescriptorIndex(sourceCollection, sourceView.SortDescriptors, sourceView.GroupDescriptors);
    }

    public void Load(IEnumerable<TDataItem> source)
    {
      this.sourceCollection = source;
      RadCollectionView<TDataItem>.RebuildDescriptorIndex(source, this.sourceView.SortDescriptors, this.sourceView.GroupDescriptors);
      this.Indexer.Load(this.sourceCollection);
    }

    public object Evaluate(string expression, TDataItem item)
    {
      int startIndex = this.IndexOf(item);
      if (startIndex >= 0)
        return this.Evaluate(expression, startIndex, 1);
      return (object) null;
    }

    public object Evaluate(string expression, int startIndex, int count)
    {
      if (startIndex >= this.Count)
        throw new ArgumentOutOfRangeException(nameof (startIndex));
      return this.sourceView.Evaluate(expression, this.GetItems(startIndex, count));
    }

    public void SetDirty()
    {
      this.Indexer.SetDirty();
    }

    private IEnumerable<TDataItem> GetItems(int startIndex, int count)
    {
      for (int i = 0; i < count; ++i)
        yield return this[startIndex + i];
    }

    protected virtual Index<TDataItem> Indexer
    {
      get
      {
        if (this.index == null)
          this.index = (Index<TDataItem>) new HybridIndex<TDataItem>(this.SourceView, this.sourceCollection);
        return this.index;
      }
    }

    protected virtual Telerik.WinControls.Data.GroupBuilder<TDataItem> GroupBuilder
    {
      get
      {
        if (this.groupBuilder == null)
        {
          this.groupBuilder = new Telerik.WinControls.Data.GroupBuilder<TDataItem>(this.Indexer);
          if (this.sourceView.GroupComparer != null)
            this.groupBuilder.Comparer = this.sourceView.GroupComparer;
        }
        return this.groupBuilder;
      }
    }

    public virtual IComparer<Group<TDataItem>> GroupComparer
    {
      get
      {
        return this.GroupBuilder.Comparer;
      }
      set
      {
        this.GroupBuilder.Comparer = value;
      }
    }

    protected virtual RadCollectionView<TDataItem> SourceView
    {
      get
      {
        return this.sourceView;
      }
    }

    bool ICollectionView<TDataItem>.CanFilter
    {
      get
      {
        return this.sourceView.CanFilter;
      }
    }

    bool ICollectionView<TDataItem>.CanGroup
    {
      get
      {
        return this.sourceView.CanGroup;
      }
    }

    bool ICollectionView<TDataItem>.CanSort
    {
      get
      {
        return this.sourceView.CanSort;
      }
    }

    Predicate<TDataItem> ICollectionView<TDataItem>.Filter
    {
      get
      {
        return this.sourceView.Filter;
      }
      set
      {
      }
    }

    SortDescriptorCollection ICollectionView<TDataItem>.SortDescriptors
    {
      get
      {
        return this.sourceView.SortDescriptors;
      }
    }

    GroupDescriptorCollection ICollectionView<TDataItem>.GroupDescriptors
    {
      get
      {
        return this.sourceView.GroupDescriptors;
      }
    }

    public GroupCollection<TDataItem> Groups
    {
      get
      {
        return this.GroupBuilder.Groups;
      }
    }

    public virtual Telerik.WinControls.Data.GroupPredicate<TDataItem> GroupPredicate
    {
      get
      {
        return this.GroupBuilder.GroupPredicate;
      }
      set
      {
        this.GroupBuilder.GroupPredicate = value;
      }
    }

    public IEnumerable<TDataItem> SourceCollection
    {
      get
      {
        return this.sourceCollection;
      }
    }

    void ICollectionView<TDataItem>.Refresh()
    {
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, args);
    }

    protected virtual void OnCurrentChanged(EventArgs args)
    {
      if (this.CurrentChanged == null)
        return;
      this.CurrentChanged((object) this, args);
    }

    protected virtual void OnCurrentChanging(CancelEventArgs args)
    {
      if (this.CurrentChanging == null)
        return;
      this.CurrentChanging((object) this, args);
    }

    TDataItem ICollectionView<TDataItem>.CurrentItem
    {
      get
      {
        return default (TDataItem);
      }
    }

    int ICollectionView<TDataItem>.CurrentPosition
    {
      get
      {
        return -1;
      }
    }

    public event EventHandler CurrentChanged;

    public event CancelEventHandler CurrentChanging;

    bool ICollectionView<TDataItem>.MoveCurrentTo(TDataItem item)
    {
      return false;
    }

    bool ICollectionView<TDataItem>.MoveCurrentToFirst()
    {
      return false;
    }

    bool ICollectionView<TDataItem>.MoveCurrentToLast()
    {
      return false;
    }

    bool ICollectionView<TDataItem>.MoveCurrentToNext()
    {
      return false;
    }

    bool ICollectionView<TDataItem>.MoveCurrentToPosition(int position)
    {
      return false;
    }

    bool ICollectionView<TDataItem>.MoveCurrentToPrevious()
    {
      return false;
    }

    public int Count
    {
      get
      {
        return this.Indexer.Count;
      }
    }

    public TDataItem this[int index]
    {
      get
      {
        return this.Indexer[index];
      }
    }

    public bool Contains(TDataItem value)
    {
      return this.Indexer.Contains(value);
    }

    public void CopyTo(TDataItem[] array, int index)
    {
      this.Indexer.CopyTo(array, index);
    }

    public int IndexOf(TDataItem value)
    {
      int index = this.Indexer.IndexOf(value);
      while (index >= 0 && !this[index].Equals((object) value))
      {
        ++index;
        if (index >= this.Count)
          return -1;
      }
      return index;
    }

    public IEnumerator<TDataItem> GetEnumerator()
    {
      return this.Indexer.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
