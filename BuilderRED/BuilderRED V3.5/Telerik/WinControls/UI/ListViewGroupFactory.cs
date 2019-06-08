// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewGroupFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ListViewGroupFactory : IGroupFactory<ListViewDataItem>
  {
    private RadListViewElement owner;

    public ListViewGroupFactory(RadListViewElement owner)
    {
      this.owner = owner;
    }

    public Group<ListViewDataItem> CreateGroup(
      object key,
      Group<ListViewDataItem> parent,
      params object[] metaData)
    {
      return (Group<ListViewDataItem>) new DataItemGroup<ListViewDataItem>(key, parent);
    }

    public GroupCollection<ListViewDataItem> CreateCollection(
      IList<Group<ListViewDataItem>> list)
    {
      this.InitializeGroups(list);
      return new GroupCollection<ListViewDataItem>(list);
    }

    private void InitializeGroups(IList<Group<ListViewDataItem>> list)
    {
      HybridDictionary hybridDictionary = new HybridDictionary();
      HybridDictionary keys = new HybridDictionary();
      foreach (ListViewDataItemGroup autoGroup in (Collection<ListViewDataItemGroup>) this.owner.Groups.AutoGroups)
        hybridDictionary.Add((object) autoGroup.DataGroup, (object) autoGroup);
      int num = 0;
      foreach (Group<ListViewDataItem> group in (IEnumerable<Group<ListViewDataItem>>) list)
        keys.Add((object) group, (object) num++);
      foreach (ListViewDataItemGroup viewDataItemGroup in (IEnumerable) hybridDictionary.Values)
      {
        if (!keys.Contains((object) viewDataItemGroup.DataGroup))
          this.owner.Groups.AutoGroups.Remove(viewDataItemGroup);
      }
      foreach (Group<ListViewDataItem> key in (IEnumerable) keys.Keys)
      {
        if (!hybridDictionary.Contains((object) key))
          this.owner.Groups.AutoGroups.Add(new ListViewDataItemGroup()
          {
            DataGroup = key
          });
      }
      int count = this.owner.Groups.AutoGroups.Count;
      ListViewDataItemGroup[] array = new ListViewDataItemGroup[count];
      this.owner.Groups.AutoGroups.CopyTo(array, 0);
      Array.Sort<ListViewDataItemGroup>(array, (IComparer<ListViewDataItemGroup>) new ListViewGroupFactory.GroupsComparer(keys));
      this.owner.Groups.AutoGroups.BeginUpdate();
      for (int index = 0; index < count; ++index)
        this.owner.Groups.AutoGroups[index] = array[index];
      this.owner.Groups.AutoGroups.EndUpdate();
      foreach (ListViewDataItemGroup group in this.owner.Groups)
      {
        foreach (ListViewDataItem listViewDataItem in group.Items)
          listViewDataItem.SetGroupCore(group, false);
      }
    }

    private class GroupsComparer : IComparer<ListViewDataItemGroup>
    {
      private HybridDictionary keys;

      public GroupsComparer(HybridDictionary keys)
      {
        this.keys = keys;
      }

      public int Compare(ListViewDataItemGroup x, ListViewDataItemGroup y)
      {
        return ((int) this.keys[(object) x.DataGroup]).CompareTo((int) this.keys[(object) y.DataGroup]);
      }
    }
  }
}
