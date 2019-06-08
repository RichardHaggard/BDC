// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListGroupFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class ListGroupFactory : IGroupFactory<RadListDataItem>
  {
    private long groupCounter = 1;
    private ListGroupCollection groups;
    protected internal RadListElement owner;
    public ListGroup DefaultGroup;

    public ListGroupFactory(RadListElement owner)
    {
      this.owner = owner;
      this.groups = new ListGroupCollection(this);
      this.DefaultGroup = new ListGroup((object) 0L, owner);
      this.DefaultGroup.Header = "Ungrouped";
      this.groups.GroupList.Add((Group<RadListDataItem>) this.DefaultGroup);
    }

    public ListGroupCollection Groups
    {
      get
      {
        return this.groups;
      }
    }

    public ListGroup CreateGroup(string header)
    {
      ListGroup listGroup = new ListGroup((object) this.groupCounter++, this.owner);
      listGroup.Header = header;
      return listGroup;
    }

    public Group<RadListDataItem> CreateGroup(
      object key,
      Group<RadListDataItem> parent,
      params object[] metaData)
    {
      foreach (ListGroup group in (ReadOnlyCollection<Group<RadListDataItem>>) this.groups)
      {
        long? key1 = group.Key as long?;
        long? nullable = key as long?;
        if ((key1.GetValueOrDefault() != nullable.GetValueOrDefault() ? 0 : (key1.HasValue == nullable.HasValue ? 1 : 0)) != 0)
        {
          group.GetItems().Clear();
          return (Group<RadListDataItem>) group;
        }
      }
      ListGroup listGroup = new ListGroup(key, this.owner);
      listGroup.Header = key.ToString();
      listGroup.GetItems().Clear();
      this.groups.GroupList.Add((Group<RadListDataItem>) listGroup);
      return (Group<RadListDataItem>) listGroup;
    }

    public GroupCollection<RadListDataItem> CreateCollection(
      IList<Group<RadListDataItem>> list)
    {
      return new GroupCollection<RadListDataItem>(list);
    }
  }
}
