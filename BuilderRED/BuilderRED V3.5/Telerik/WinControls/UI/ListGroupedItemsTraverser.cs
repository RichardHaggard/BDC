// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListGroupedItemsTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class ListGroupedItemsTraverser : ItemsTraverser<RadListDataItem>
  {
    protected GroupCollection<RadListDataItem> groupsCollection;
    protected Group<RadListDataItem> currentGroup;

    private static IList<RadListDataItem> GetItemList(
      GroupCollection<RadListDataItem> groupsCollection,
      Dictionary<Group<RadListDataItem>, RadListDataGroupItem> groupHeaderItems)
    {
      List<RadListDataItem> radListDataItemList = new List<RadListDataItem>();
      foreach (Group<RadListDataItem> groups in (ReadOnlyCollection<Group<RadListDataItem>>) groupsCollection)
      {
        if (groups.ItemCount != 0)
        {
          radListDataItemList.Add((RadListDataItem) groupHeaderItems[groups]);
          radListDataItemList.AddRange((IEnumerable<RadListDataItem>) groups);
        }
      }
      return (IList<RadListDataItem>) radListDataItemList;
    }

    public ListGroupedItemsTraverser(
      GroupCollection<RadListDataItem> groupsCollection,
      Dictionary<Group<RadListDataItem>, RadListDataGroupItem> groupHeaderItems)
      : base(ListGroupedItemsTraverser.GetItemList(groupsCollection, groupHeaderItems))
    {
      this.groupsCollection = groupsCollection;
    }

    protected override bool OnItemsNavigating(RadListDataItem current)
    {
      base.OnItemsNavigating(current);
      RadListDataGroupItem listDataGroupItem = current as RadListDataGroupItem;
      if (listDataGroupItem != null)
        return listDataGroupItem.Group.Key.Equals((object) 0L);
      return current.Group.Collapsed;
    }
  }
}
