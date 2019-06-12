// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.GroupBuilder`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Telerik.Collections.Generic;

namespace Telerik.WinControls.Data
{
  public class GroupBuilder<T> where T : IDataItem
  {
    private static List<Group<T>> Empty = new List<Group<T>>();
    private SortDescriptor[] groupNames = new SortDescriptor[0];
    private int version;
    private int countState;
    private Index<T> index;
    private GroupCollection<T> groups;
    private Dictionary<T, Group<T>> itemGroupsCache;
    private Telerik.WinControls.Data.GroupPredicate<T> groupPredicate;
    private IComparer<Group<T>> comparer;

    public GroupBuilder(Index<T> index)
    {
      this.index = index;
      this.groupPredicate = new Telerik.WinControls.Data.GroupPredicate<T>(this.GetItemKey);
      this.comparer = (IComparer<Group<T>>) new GroupComparer<T>((ListSortDirection[]) null);
      this.groups = GroupCollection<T>.Empty;
    }

    public GroupCollection<T> Groups
    {
      get
      {
        if (this.NeedsRefresh)
        {
          this.itemGroupsCache = new Dictionary<T, Group<T>>();
          this.groups = this.Perform((IReadOnlyCollection<T>) this.index, 0, (Group<T>) null);
          this.countState = this.index.Count;
          if (this.index.CollectionView.GroupDescriptors.Count > 0)
          {
            this.groupNames = new SortDescriptor[this.index.CollectionView.GroupDescriptors[0].GroupNames.Count];
            for (int index = 0; index < this.groupNames.Length; ++index)
            {
              SortDescriptor groupName = this.index.CollectionView.GroupDescriptors[0].GroupNames[index];
              this.groupNames[index] = new SortDescriptor(groupName.PropertyName, groupName.Direction);
            }
          }
          else
            this.groupNames = new SortDescriptor[0];
        }
        this.version = this.index.CollectionView.Version;
        return this.groups;
      }
    }

    public virtual Telerik.WinControls.Data.GroupPredicate<T> GroupPredicate
    {
      get
      {
        return this.groupPredicate;
      }
      set
      {
        if (!(this.groupPredicate != value))
          return;
        this.groupPredicate = value;
      }
    }

    public virtual Telerik.WinControls.Data.GroupPredicate<T> DefaultGroupPredicate
    {
      get
      {
        return new Telerik.WinControls.Data.GroupPredicate<T>(this.GetItemKey);
      }
    }

    public virtual IComparer<Group<T>> Comparer
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
        ++this.version;
      }
    }

    public bool NeedsRefresh
    {
      get
      {
        if (this.version != this.index.CollectionView.Version || this.countState != this.index.Count || (this.index.CollectionView.GroupDescriptors.Count == 0 || this.groupNames.Length != this.index.CollectionView.GroupDescriptors[0].GroupNames.Count))
          return true;
        for (int index = 0; index < this.groupNames.Length; ++index)
        {
          if (this.groupNames[index].Direction != this.index.CollectionView.GroupDescriptors[0].GroupNames[index].Direction || this.groupNames[index].PropertyName.CompareTo(this.index.CollectionView.GroupDescriptors[0].GroupNames[index].PropertyName) != 0)
            return true;
        }
        return false;
      }
    }

    public RadCollectionView<T> CollectionView
    {
      get
      {
        return this.index.CollectionView;
      }
    }

    public Group<T> GetItemGroup(T item)
    {
      if (this.Groups.Count == 0)
        return (Group<T>) null;
      if (this.itemGroupsCache != null && this.itemGroupsCache.ContainsKey(item))
        return this.itemGroupsCache[item];
      return (Group<T>) null;
    }

    public virtual GroupCollection<T> Perform(
      IReadOnlyCollection<T> items,
      int level,
      Group<T> parent)
    {
      GroupDescriptorCollection groupDescriptors = this.index.CollectionView.GroupDescriptors;
      if (level >= groupDescriptors.Count)
        return GroupCollection<T>.Empty;
      IGroupFactory<T> groupFactory = this.index.CollectionView.GroupFactory;
      GroupCollection<T> cachedGroups = this.GetCachedGroups(items, level);
      if (!GroupBuilder<T>.IsValid(level, groupDescriptors))
      {
        cachedGroups?.Dispose();
        return groupFactory.CreateCollection((IList<Group<T>>) GroupBuilder<T>.Empty);
      }
      IComparer<Group<T>> comparer = Activator.CreateInstance(this.Comparer.GetType()) as IComparer<Group<T>> ?? this.Comparer;
      if (comparer is GroupComparer<T>)
        ((GroupComparer<T>) comparer).Directions = this.GetComparerDirections(level);
      AvlTree<Group<T>> avlTree1 = new AvlTree<Group<T>>(comparer);
      AvlTree<Group<T>> avlTree2 = new AvlTree<Group<T>>((IComparer<Group<T>>) new GroupComparer<T>(this.GetComparerDirections(level)));
      foreach (T key1 in !this.CollectionView.CanPage || level != 0 || !this.CollectionView.PagingBeforeGrouping ? (IEnumerable<T>) items : (IEnumerable<T>) this.index.GetItemsOnPage(this.CollectionView.PageIndex))
      {
        object key2 = this.GroupPredicate(key1, level);
        Group<T> group1 = (Group<T>) new DataItemGroup<T>(key2);
        Group<T> group2 = avlTree2.Find(group1);
        if (group2 == null)
        {
          group2 = this.GetGroup(cachedGroups, group1, parent, key2, level);
          avlTree2.Add(group2);
        }
        group2.Items.Add(key1);
        if (this.itemGroupsCache.ContainsKey(key1))
          this.itemGroupsCache[key1] = group2;
        else
          this.itemGroupsCache.Add(key1, group2);
      }
      for (int index = 0; index < avlTree2.Count; ++index)
        avlTree1.Add(avlTree2[index]);
      cachedGroups?.Dispose();
      return groupFactory.CreateCollection((IList<Group<T>>) avlTree1);
    }

    private static bool IsValid(int level, GroupDescriptorCollection groupDescriptors)
    {
      bool flag = level < groupDescriptors.Count && groupDescriptors[level].GroupNames.Count != 0;
      if (flag)
      {
        for (int index = 0; index < groupDescriptors[level].GroupNames.Count; ++index)
        {
          if (groupDescriptors[level].GroupNames[index].PropertyIndex < 0)
          {
            flag = false;
            break;
          }
        }
      }
      return flag;
    }

    protected virtual Group<T> GetGroup(
      GroupCollection<T> cache,
      Group<T> newGroup,
      Group<T> parent,
      object key,
      int level)
    {
      Group<T> group = GroupBuilder<T>.GetCachedGroup(cache, newGroup);
      if (group == null)
      {
        group = this.index.CollectionView.GroupFactory.CreateGroup(key, parent);
        (group as DataItemGroup<T>).GroupBuilder = this;
      }
      return group;
    }

    private static Group<T> GetCachedGroup(GroupCollection<T> cache, Group<T> newGroup)
    {
      if (cache == null)
        return (Group<T>) null;
      AvlTree<Group<T>> groupList = cache.GroupList as AvlTree<Group<T>>;
      if (groupList == null)
        return (Group<T>) null;
      Group<T> group = groupList.Find(newGroup);
      group?.Items.Clear();
      return group;
    }

    private GroupCollection<T> GetCachedGroups(
      IReadOnlyCollection<T> items,
      int level)
    {
      GroupCollection<T> groupCollection = (GroupCollection<T>) null;
      if (level == 0)
        groupCollection = this.groups;
      if (groupCollection == null && items is DataItemGroup<T>)
        groupCollection = ((DataItemGroup<T>) items).Cache;
      return groupCollection;
    }

    private object GetItemKey(T item, int level)
    {
      GroupDescriptor groupDescriptor = this.index.CollectionView.GroupDescriptors[level];
      object[] objArray = new object[groupDescriptor.GroupNames.Count];
      for (int index = 0; index < groupDescriptor.GroupNames.Count; ++index)
      {
        SortDescriptor groupName = groupDescriptor.GroupNames[index];
        if (groupName.PropertyIndex >= 0)
          objArray[index] = this.GetItemKey(item, groupName);
      }
      return (object) objArray;
    }

    protected virtual object GetItemKey(T item, SortDescriptor descriptor)
    {
      ColorConverter colorConverter = (ColorConverter) null;
      int propertyIndex = descriptor.PropertyIndex;
      object obj = item[propertyIndex];
      if (obj == DBNull.Value)
        obj = (object) null;
      else if (obj is Color)
      {
        if (colorConverter == null)
          colorConverter = new ColorConverter();
        Color color = (Color) obj;
        obj = color.IsNamedColor ? (object) color.Name : (object) colorConverter.ConvertToString((object) color);
      }
      return obj;
    }

    private ListSortDirection[] GetComparerDirections(int level)
    {
      ListSortDirection[] listSortDirectionArray = new ListSortDirection[this.index.CollectionView.GroupDescriptors[level].GroupNames.Count];
      for (int index = 0; index < listSortDirectionArray.Length; ++index)
        listSortDirectionArray[index] = this.index.CollectionView.GroupDescriptors[level].GroupNames[index].Direction;
      return listSortDirectionArray;
    }

    protected internal int Version
    {
      get
      {
        return this.version;
      }
    }
  }
}
