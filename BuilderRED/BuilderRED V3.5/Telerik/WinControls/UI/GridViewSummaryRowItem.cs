// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSummaryRowItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (GridViewSummaryRowItemTypeConverter))]
  public class GridViewSummaryRowItem : ObservableCollection<GridViewSummaryItem>
  {
    private GridViewTemplate template;

    public GridViewSummaryRowItem()
    {
    }

    public GridViewSummaryRowItem(GridViewSummaryItem[] items)
    {
      if (items == null)
        return;
      this.SetItems(items);
    }

    private void SetItems(GridViewSummaryItem[] items)
    {
      this.BeginUpdate();
      foreach (GridViewSummaryItem gridViewSummaryItem in items)
        this.Add(gridViewSummaryItem);
      this.EndUpdate(true);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<GridViewSummaryItem> this[string fieldName]
    {
      get
      {
        return this.GetByFieldName(fieldName);
      }
    }

    [Description("Gets or sets the array of GridViewSummaryItem fields that describe this summary row.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public GridViewSummaryItem[] Fields
    {
      get
      {
        GridViewSummaryItem[] array = new GridViewSummaryItem[this.Count];
        this.CopyTo(array, 0);
        return array;
      }
      set
      {
        this.BeginUpdate();
        this.Clear();
        foreach (GridViewSummaryItem gridViewSummaryItem in value)
          this.Add(gridViewSummaryItem);
        this.EndUpdate(true);
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
      internal set
      {
        this.template = value;
        foreach (GridViewSummaryItem gridViewSummaryItem in (Collection<GridViewSummaryItem>) this)
          gridViewSummaryItem.Template = value;
      }
    }

    private List<GridViewSummaryItem> GetByFieldName(string fieldName)
    {
      List<GridViewSummaryItem> gridViewSummaryItemList = new List<GridViewSummaryItem>();
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.Equals(fieldName, this[index].Name, this.template.CaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
          gridViewSummaryItemList.Add(this[index]);
      }
      if (gridViewSummaryItemList.Count <= 0)
        return (List<GridViewSummaryItem>) null;
      return gridViewSummaryItemList;
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      base.OnCollectionChanged(e);
      if (e.Action == NotifyCollectionChangedAction.ItemChanging || this.template == null)
        return;
      this.template.OnViewChanged((object) this.template, new DataViewChangedEventArgs(ViewChangedAction.DataChanged));
      foreach (GridViewSummaryItem gridViewSummaryItem in (Collection<GridViewSummaryItem>) this)
        gridViewSummaryItem.Template = this.Template;
    }
  }
}
