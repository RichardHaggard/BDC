// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSelfReferenceDataProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewSelfReferenceDataProvider : GridViewHierarchyDataProvider
  {
    private GridViewRelation relation;
    private AvlTree<GridViewRowInfo> indexer;
    private GridViewSelfReferenceComparer comparer;

    public GridViewSelfReferenceDataProvider(GridViewTemplate template)
      : base(template)
    {
      this.relation = template.MasterTemplate.Relations.Find(template.Parent, template);
      if (this.relation == null)
        return;
      this.comparer = new GridViewSelfReferenceComparer(this.relation);
      this.indexer = new AvlTree<GridViewRowInfo>((IComparer<GridViewRowInfo>) this.comparer);
      this.Refresh();
      this.Template.ListSource.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ListSource_CollectionChanged);
    }

    public override void Dispose()
    {
      if (this.Template != null && this.Template.ListSource != null)
        this.Template.ListSource.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ListSource_CollectionChanged);
      base.Dispose();
    }

    public override GridViewRelation Relation
    {
      get
      {
        return this.relation;
      }
    }

    public override IList<GridViewRowInfo> GetChildRows(
      GridViewRowInfo parentRow,
      GridViewInfo view)
    {
      if (this.Relation == null || this.indexer.Count == 0)
        return (IList<GridViewRowInfo>) new List<GridViewRowInfo>();
      bool flag = true;
      if (parentRow == null)
      {
        parentRow = this.indexer[0];
        flag = false;
      }
      else
        parentRow = (GridViewRowInfo) new GridViewSelfReferenceDataProvider.ParentDataRow(this, ((GridViewHierarchyRowInfo) parentRow).ActiveView, parentRow);
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      int lo = this.indexer.Index(parentRow);
      int num = this.indexer.LastIndex(parentRow);
      if (lo >= 0)
      {
        IEnumerator<GridViewRowInfo> forwardEnumerator = this.indexer.GetForwardEnumerator(lo, num + 1);
        while (forwardEnumerator.MoveNext())
        {
          if (flag)
            forwardEnumerator.Current.SetParent(parentRow);
          gridViewRowInfoList.Add(forwardEnumerator.Current);
        }
      }
      return (IList<GridViewRowInfo>) gridViewRowInfoList;
    }

    public override void Refresh()
    {
      if (this.relation == null)
        return;
      this.comparer.Reset();
      if (!this.comparer.IsValid)
        return;
      this.indexer.Clear();
      Dictionary<object, object> dictionary = new Dictionary<object, object>();
      for (int index = 0; index < this.Template.ListSource.Count; ++index)
      {
        GridViewRowInfo gridViewRowInfo = this.Template.ListSource[index];
        object obj = gridViewRowInfo.Cells[this.Relation.ChildColumnNames[0]].Value;
        object key = gridViewRowInfo.Cells[this.Relation.ParentColumnNames[0]].Value;
        if (obj != null && obj.Equals(key))
          throw new ArgumentException(string.Format("Row with id '{0}' has a parent id '{1}'. The row cannot be parented by itself.", key, obj));
        if (key != null)
        {
          if (dictionary.ContainsKey(key))
            throw new ArgumentException(string.Format("Row with id '{0}' already appears in the hierarchy with parent id '{1}'. Each row in a self-reference hierarchy can appear only once in the hierarchy.", key, dictionary[key]));
          dictionary.Add(key, obj);
        }
        this.indexer.Add(gridViewRowInfo);
      }
      if (this.IsSuspendedNotifications)
        return;
      this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.DataChanged));
    }

    private void DispatchDataViewChangedEvent(DataViewChangedEventArgs args)
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send);
      GridViewSynchronizationService.DispatchEvent(this.Template, new GridViewEvent((object) this.Template, (object) this.Template, new object[1]
      {
        (object) args
      }, eventInfo), false);
    }

    public override GridViewHierarchyRowInfo GetParent(
      GridViewRowInfo gridViewRowInfo)
    {
      return (GridViewHierarchyRowInfo) null;
    }

    private void ListSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.ItemChanging)
        return;
      this.Template.DataView.LazyRefresh();
      if (e.Action == NotifyCollectionChangedAction.ItemChanged && !this.relation.ParentColumnNames.Contains(e.PropertyName) && (!this.relation.ChildColumnNames.Contains(e.PropertyName) && !string.IsNullOrEmpty(e.PropertyName)))
        return;
      this.Refresh();
    }

    private class ParentDataRow : GridViewDataRowInfo
    {
      private GridViewSelfReferenceDataProvider provider;

      public ParentDataRow(
        GridViewSelfReferenceDataProvider provider,
        GridViewInfo viewInfo,
        GridViewRowInfo parent)
        : base(viewInfo)
      {
        this.IsAttached = false;
        this.provider = provider;
        GridViewColumn[] gridViewColumnArray1 = new GridViewColumn[this.provider.Relation.ParentColumnNames.Count];
        GridViewColumn[] gridViewColumnArray2 = new GridViewColumn[this.provider.Relation.ParentColumnNames.Count];
        for (int index = 0; index < this.provider.Relation.ParentColumnNames.Count; ++index)
          gridViewColumnArray1[index] = (GridViewColumn) this.provider.Relation.ParentTemplate.Columns[this.provider.Relation.ParentColumnNames[index]];
        for (int index = 0; index < this.provider.Relation.ChildColumnNames.Count; ++index)
          gridViewColumnArray2[index] = (GridViewColumn) this.provider.Relation.ChildTemplate.Columns[this.provider.Relation.ChildColumnNames[index]];
        for (int index = 0; index < this.provider.Relation.ChildColumnNames.Count; ++index)
          this[gridViewColumnArray2[index]] = parent[gridViewColumnArray1[index]];
      }
    }
  }
}
