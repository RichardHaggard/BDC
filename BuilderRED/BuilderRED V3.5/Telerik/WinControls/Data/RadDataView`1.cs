// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.RadDataView`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Collections.Generic;

namespace Telerik.WinControls.Data
{
  public class RadDataView<TDataItem> : RadCollectionView<TDataItem> where TDataItem : IDataItem
  {
    private bool canFilter = true;
    private bool canSort = true;
    private bool canGroup = true;
    private bool hybridIndex = true;
    private bool canPage;
    private IComparer<TDataItem> comparer;
    private Index<TDataItem> indexer;
    private Telerik.WinControls.Data.GroupBuilder<TDataItem> groupBuilder;
    private int changingIndex;

    public RadDataView(IEnumerable<TDataItem> collection)
      : base(collection)
    {
      this.comparer = (IComparer<TDataItem>) new DataItemComparer<TDataItem>(this.SortDescriptors);
      this.indexer = this.CreateIndex();
      this.groupBuilder = this.CreateGroupBuilder();
    }

    public override IComparer<TDataItem> Comparer
    {
      get
      {
        return this.comparer;
      }
      set
      {
        if (this.comparer == value)
          return;
        this.comparer = value;
        this.indexer = this.CreateIndex();
        this.groupBuilder = this.CreateGroupBuilder();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Comparer)));
      }
    }

    [DefaultValue(true)]
    public bool UseHybridIndex
    {
      get
      {
        return this.hybridIndex;
      }
      set
      {
        if (this.hybridIndex == value)
          return;
        this.hybridIndex = value;
        this.indexer = this.CreateIndex();
        this.groupBuilder = this.CreateGroupBuilder();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (UseHybridIndex)));
      }
    }

    protected virtual Index<TDataItem> CreateIndex()
    {
      if (this.UseHybridIndex)
        return (Index<TDataItem>) new HybridIndex<TDataItem>((RadCollectionView<TDataItem>) this);
      return (Index<TDataItem>) new AvlIndex<TDataItem>((RadCollectionView<TDataItem>) this);
    }

    public override IComparer<Group<TDataItem>> GroupComparer
    {
      get
      {
        return this.groupBuilder.Comparer;
      }
      set
      {
        this.groupBuilder.Comparer = value;
      }
    }

    public override GroupCollection<TDataItem> Groups
    {
      get
      {
        if (!this.CanPage || this.PagingBeforeGrouping)
          return this.groupBuilder.Groups;
        List<Group<TDataItem>> groupList = new List<Group<TDataItem>>();
        int num = this.PageIndex * this.PageSize;
        for (int index = num; index < num + this.PageSize && index < this.groupBuilder.Groups.Count; ++index)
          groupList.Add(this.groupBuilder.Groups[index]);
        return this.GroupFactory.CreateCollection((IList<Group<TDataItem>>) groupList);
      }
    }

    public Telerik.WinControls.Data.GroupBuilder<TDataItem> GroupBuilder
    {
      get
      {
        return this.groupBuilder;
      }
    }

    public override Telerik.WinControls.Data.GroupPredicate<TDataItem> GroupPredicate
    {
      get
      {
        return this.groupBuilder.GroupPredicate;
      }
      set
      {
        if (!(this.groupBuilder.GroupPredicate != value))
          return;
        this.groupBuilder.GroupPredicate = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (GroupPredicate)));
      }
    }

    public override Telerik.WinControls.Data.GroupPredicate<TDataItem> DefaultGroupPredicate
    {
      get
      {
        return this.groupBuilder.DefaultGroupPredicate;
      }
    }

    public override bool CanPage
    {
      get
      {
        return this.canPage;
      }
      set
      {
        if (this.canPage == value)
          return;
        this.canPage = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("PageSize"));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanFilter
    {
      get
      {
        return this.canFilter;
      }
      set
      {
        if (this.canFilter == value)
          return;
        this.canFilter = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("Filter"));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanGroup
    {
      get
      {
        return this.canGroup;
      }
      set
      {
        if (this.CanGroup == value)
          return;
        this.canGroup = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("GroupDescriptors"));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanSort
    {
      get
      {
        return this.canSort;
      }
      set
      {
        if (this.canSort == value)
          return;
        this.canSort = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("SortDescriptors"));
      }
    }

    public Index<TDataItem> Indexer
    {
      get
      {
        return this.indexer;
      }
    }

    public override int TotalPages
    {
      get
      {
        if (this.CanGroup && this.GroupDescriptors.Count > 0 && !this.PagingBeforeGrouping)
          return this.groupBuilder.Groups.Count / this.PageSize + (this.groupBuilder.Groups.Count % this.PageSize > 0 ? 1 : 0);
        return base.TotalPages;
      }
    }

    protected virtual Telerik.WinControls.Data.GroupBuilder<TDataItem> CreateGroupBuilder()
    {
      return new Telerik.WinControls.Data.GroupBuilder<TDataItem>(this.indexer);
    }

    public override int IndexOf(TDataItem item)
    {
      int index = base.IndexOf(item);
      while (index >= 0 && !this[index].Equals((object) item))
      {
        ++index;
        if (index >= this.Count)
          return -1;
      }
      return index;
    }

    public override TDataItem Find(int itemIndex, object dataBoundItem)
    {
      if (!this.HasDataOperation)
        return base.Find(itemIndex, dataBoundItem);
      return default (TDataItem);
    }

    public override bool MoveToLastPage()
    {
      if (this.GroupDescriptors.Count > 0)
      {
        int num = !this.PagingBeforeGrouping ? this.groupBuilder.Groups.Count : this.Indexer.Count;
        if (num > 0)
          return this.MoveToPage(num / this.PageSize + (num % this.PageSize > 0 ? 0 : -1));
      }
      return base.MoveToLastPage();
    }

    protected override void RefreshOverride()
    {
      if (this.IsInUpdate)
        this.EnsureDescriptorIndex();
      else
        this.RebuildData(true);
    }

    protected override IList<TDataItem> Items
    {
      get
      {
        if (this.CanPage && this.indexer.Items.Count > this.PageSize && (!this.CanGroup || this.CanGroup && this.GroupDescriptors.Count == 0 || this.CanGroup && this.GroupDescriptors.Count > 0 && this.PagingBeforeGrouping))
          return this.indexer.GetItemsOnPage(this.PageIndex);
        return this.indexer.Items;
      }
    }

    public override int GetItemPage(TDataItem item)
    {
      if (this.CanPage)
        return this.indexer.GetItemPageIndex(item);
      return 0;
    }

    public override int ItemCount
    {
      get
      {
        return this.indexer.Count;
      }
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == "FilterExpression") && !(e.PropertyName == "Filter") && (!(e.PropertyName == "SortDescriptors") && !(e.PropertyName == "Comparer")) && (!(e.PropertyName == "GroupDescriptors") && !(e.PropertyName == "GroupPredicate") && (!(e.PropertyName == "PageSize") && !(e.PropertyName == "PagingBeforeGrouping"))))
        return;
      this.RefreshOverride();
      this.EnsurePageIndex();
    }

    protected override void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      int num = this.HasDataOperation ? 1 : 0;
      bool flag = false;
      switch (args.Action)
      {
        case NotifyCollectionChangedAction.Add:
          flag = this.AddItem((TDataItem) args.NewItems[0]);
          break;
        case NotifyCollectionChangedAction.Remove:
          flag = this.RemoveItem(args);
          break;
        case NotifyCollectionChangedAction.Replace:
          flag = this.ReplaceItem(args);
          break;
        case NotifyCollectionChangedAction.Move:
          flag = this.MoveItem(args);
          break;
        case NotifyCollectionChangedAction.Reset:
        case NotifyCollectionChangedAction.Batch:
          this.RefreshOverride();
          return;
        case NotifyCollectionChangedAction.ItemChanging:
          this.OnItemChanging((TDataItem) args.NewItems[0]);
          break;
        case NotifyCollectionChangedAction.ItemChanged:
          flag = this.ChangeItem((TDataItem) args.NewItems[0], args.PropertyName);
          this.changingIndex = -1;
          break;
      }
      if (!flag)
        return;
      base.ProcessCollectionChanged(args);
    }

    private void RebuildData(bool notify)
    {
      this.EnsureDescriptorIndex();
      if (notify)
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      this.SyncCurrent(this.CurrentItem);
    }

    private void SyncCurrent(TDataItem item)
    {
      if ((object) item == null)
      {
        RadListSource<TDataItem> sourceCollection = this.SourceCollection as RadListSource<TDataItem>;
        if (sourceCollection != null && sourceCollection.Count > 0 && (sourceCollection.Position >= 0 && sourceCollection.Position < sourceCollection.Count))
          item = sourceCollection[sourceCollection.Position];
      }
      if ((object) item == null && this.Count > 0)
        item = this.Items[0];
      if ((object) item == null)
        return;
      if (!this.PassesFilter(item))
      {
        if (this.Count < 0)
          return;
        this.SetCurrentPositionCore(this.Count == 0 ? -1 : 0, true, CurrentChangeReason.Sync);
      }
      else
      {
        if (!this.HasDataOperation)
          return;
        int newPosition = this.IndexOf(item);
        if (newPosition == this.CurrentPosition)
          return;
        this.MoveCurrentToPosition(newPosition);
      }
    }

    private void OnItemChanging(TDataItem item)
    {
      if (!this.HasDataOperation)
        return;
      this.changingIndex = this.Items.IndexOf(item);
    }

    private void EnsureDescriptorIndex()
    {
      if (this.SourceCollection == null)
        return;
      RadCollectionView<TDataItem>.RebuildDescriptorIndex(this.SourceCollection, this.SortDescriptors, this.GroupDescriptors);
    }

    private void UpdateItemSorted(TDataItem item, string propertyName)
    {
      if (!this.SortDescriptors.Contains(propertyName))
        return;
      this.indexer.Items.RemoveAt(this.changingIndex);
      this.indexer.Items.Add(item);
    }

    private bool HasFields
    {
      get
      {
        IEnumerator<TDataItem> enumerator = this.SourceCollection.GetEnumerator();
        enumerator.MoveNext();
        if ((object) enumerator.Current != null)
          return enumerator.Current.FieldCount != 0;
        return false;
      }
    }

    public override void EnsureDescriptors()
    {
      this.EnsureDescriptorIndex();
    }

    private bool AddItem(TDataItem item)
    {
      if (!this.PassesFilter(item))
        return false;
      if (this.ChangeCurrentOnAdd)
      {
        bool flag = this.SortDescriptors.Count != 0;
        int newPosition = this.IndexOf(item);
        if (newPosition > -1 || !flag)
          this.SetCurrentPositionCore(newPosition, true, CurrentChangeReason.Add);
      }
      return true;
    }

    private bool RemoveItem(NotifyCollectionChangedEventArgs args)
    {
      TDataItem newItem = (TDataItem) args.NewItems[0];
      int index = -1;
      if (this.HasDataOperation)
      {
        index = this.Items.IndexOf(newItem);
        if (index >= 0)
        {
          this.Items.RemoveAt(index);
          this.SyncCurrent(default (TDataItem));
          return true;
        }
        this.RebuildData(false);
      }
      int num = this.Count - 1;
      int newPosition = this.CurrentPosition < num ? this.CurrentPosition : num;
      if (index >= 0)
      {
        while (index >= this.Count)
          --index;
        newPosition = index;
      }
      this.MoveCurrentToPosition(newPosition);
      return true;
    }

    private bool ReplaceItem(NotifyCollectionChangedEventArgs args)
    {
      if (!this.HasDataOperation)
        return true;
      bool flag = false;
      TDataItem oldItem = (TDataItem) args.OldItems[0];
      if (this.PassesFilter(oldItem))
      {
        this.Items.Remove(oldItem);
        flag = true;
      }
      if (args.NewStartingIndex < args.NewItems.Count)
      {
        TDataItem newItem = (TDataItem) args.NewItems[args.NewStartingIndex];
        if (this.PassesFilter(newItem))
        {
          if (this.SortDescriptors.Count != 0)
            this.indexer.Items.Add(newItem);
          else
            this.indexer.Items.Insert(args.NewStartingIndex, newItem);
          flag = true;
        }
      }
      return flag;
    }

    private bool MoveItem(NotifyCollectionChangedEventArgs args)
    {
      return this.PassesFilter((TDataItem) args.NewItems[0]);
    }

    private bool ChangeItem(TDataItem item, string propertyName)
    {
      if (!this.HasDataOperation)
        return true;
      if (!this.PassesFilter(item))
      {
        this.changingIndex = this.Items.IndexOf(item);
        if (this.changingIndex >= 0)
        {
          if (this.Items.Count > 0)
            this.Items.RemoveAt(this.changingIndex);
          return true;
        }
        if (this.changingIndex >= 0)
          return false;
        this.RebuildData(false);
        return true;
      }
      if (this.changingIndex >= 0 && this.SortDescriptors.Count > 0)
        this.UpdateItemSorted(item, propertyName);
      else if (this.Items.IndexOf(item) < 0)
        this.RebuildData(false);
      return true;
    }
  }
}
