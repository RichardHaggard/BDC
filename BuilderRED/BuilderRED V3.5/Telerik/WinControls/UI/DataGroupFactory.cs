// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataGroupFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class DataGroupFactory : IGroupFactory<GridViewRowInfo>
  {
    private GridViewTemplate owner;

    public DataGroupFactory(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    public Group<GridViewRowInfo> CreateGroup(
      object key,
      Group<GridViewRowInfo> parent,
      params object[] metaData)
    {
      DataGroup dataGroup = new DataGroup(key, parent, this.owner);
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) dataGroup.GroupRow, this.owner.MasterViewInfo);
      this.owner.OnCreateRowInfo(e);
      GridViewGroupRowInfo rowInfo = e.RowInfo as GridViewGroupRowInfo;
      if (rowInfo != null && rowInfo != dataGroup.GroupRow)
      {
        rowInfo.ViewInfo = this.owner.MasterViewInfo;
        dataGroup.GroupRow = rowInfo;
      }
      return (Group<GridViewRowInfo>) dataGroup;
    }

    public GroupCollection<GridViewRowInfo> CreateCollection(
      IList<Group<GridViewRowInfo>> list)
    {
      return (GroupCollection<GridViewRowInfo>) new DataGroupCollection(list);
    }
  }
}
