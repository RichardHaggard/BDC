// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDataView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridDataView : RadDataView<GridViewRowInfo>
  {
    private GridViewListSource listSource;
    private GridViewColumnCollection columns;

    public GridDataView(GridViewListSource listSource)
      : base((IEnumerable<GridViewRowInfo>) listSource)
    {
      this.listSource = listSource;
    }

    protected override GroupBuilder<GridViewRowInfo> CreateGroupBuilder()
    {
      return (GroupBuilder<GridViewRowInfo>) new GridGroupBuilder(this.Indexer);
    }

    protected override object GetFieldValue(GridViewRowInfo item, string fieldName)
    {
      if (this.columns == null)
        this.columns = this.listSource.Template.Columns;
      GridViewDataColumn[] columnByFieldName = this.columns.GetColumnByFieldName(fieldName);
      if (columnByFieldName.Length > 0)
        return columnByFieldName[0].GetValue(item, GridViewDataOperation.Filtering);
      return this.columns[fieldName]?.GetValue(item, GridViewDataOperation.Filtering);
    }

    protected override bool VersionUpdateNeeded(NotifyCollectionChangedEventArgs args)
    {
      bool flag = base.VersionUpdateNeeded(args);
      if (!flag && !string.IsNullOrEmpty(args.PropertyName))
      {
        if (this.columns == null)
          this.columns = this.listSource.Template.Columns;
        GridViewDataColumn[] columnByFieldName = this.columns.GetColumnByFieldName(args.PropertyName);
        for (int index = 0; index < columnByFieldName.Length; ++index)
        {
          if (this.GroupDescriptors.Contains(columnByFieldName[index].Name) || this.SortDescriptors.Contains(columnByFieldName[index].Name))
            return true;
        }
      }
      return flag;
    }
  }
}
