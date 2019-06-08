// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class ListGroupCollection : GroupCollection<RadListDataItem>
  {
    private ListGroupFactory factory;

    public ListGroupCollection(ListGroupFactory factory)
      : base((IList<Group<RadListDataItem>>) new List<Group<RadListDataItem>>())
    {
      this.factory = factory;
    }

    public ListGroup AddGroup(string name)
    {
      ListGroup group = this.factory.CreateGroup(name);
      this.Items.Add((Group<RadListDataItem>) group);
      return group;
    }

    public void RemoveGroup(ListGroup group)
    {
      if (group.GetItems().Count > 0)
        group.ClearItems();
      this.Items.Remove((Group<RadListDataItem>) group);
    }

    public ListGroup this[int index]
    {
      get
      {
        return base[index] as ListGroup;
      }
    }
  }
}
