// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupBuilder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridGroupBuilder : GroupBuilder<GridViewRowInfo>
  {
    public GridGroupBuilder(Index<GridViewRowInfo> index)
      : base(index)
    {
    }

    protected override object GetItemKey(GridViewRowInfo item, SortDescriptor descriptor)
    {
      object cellValue = base.GetItemKey(item, descriptor);
      GridViewComboBoxColumn column = item.ViewTemplate.Columns[descriptor.PropertyIndex] as GridViewComboBoxColumn;
      if (column != null && column.DisplayMemberSort)
        cellValue = column.GetLookupValue(cellValue);
      return cellValue;
    }

    protected override Group<GridViewRowInfo> GetGroup(
      GroupCollection<GridViewRowInfo> cache,
      Group<GridViewRowInfo> newGroup,
      Group<GridViewRowInfo> parent,
      object key,
      int level)
    {
      GroupDescriptor groupDescriptor = this.CollectionView.GroupDescriptors[level];
      DataGroup group = (DataGroup) base.GetGroup(cache, newGroup, parent, key, level);
      if (group.GroupDescriptor != null && group.GroupDescriptor != groupDescriptor)
      {
        group.GroupDescriptor = (GroupDescriptor) null;
        group = (DataGroup) this.CollectionView.GroupFactory.CreateGroup(key, parent);
        group.GroupBuilder = (GroupBuilder<GridViewRowInfo>) this;
      }
      group.GroupDescriptor = groupDescriptor;
      return (Group<GridViewRowInfo>) group;
    }
  }
}
