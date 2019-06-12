// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRelationDataProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.Collections.Generic;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewRelationDataProvider : GridViewHierarchyDataProvider
  {
    private bool valid = true;
    private GridViewRelation relation;
    private AvlTree<GridViewRowInfo> indexer;
    private GridViewRelationRowComparer comparer;
    private bool childRowsLoaded;

    public GridViewRelationDataProvider(GridViewTemplate template)
      : base(template)
    {
      this.relation = template.MasterTemplate.Relations.Find(template.Parent, template);
      if (this.relation == null)
        return;
      this.comparer = new GridViewRelationRowComparer(this.relation);
      this.indexer = new AvlTree<GridViewRowInfo>((IComparer<GridViewRowInfo>) this.comparer);
      this.Refresh();
      this.Template.ListSource.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ListSource_CollectionChanged);
    }

    private void ListSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.Refresh();
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
      if (!this.childRowsLoaded)
        this.Refresh();
      if (this.Relation == null || this.indexer.Count == 0)
        return (IList<GridViewRowInfo>) new List<GridViewRowInfo>();
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      if (this.IsValid)
      {
        int lo = this.indexer.Index(parentRow);
        int num = this.indexer.LastIndex(parentRow);
        if (lo >= 0)
        {
          IEnumerator<GridViewRowInfo> forwardEnumerator = this.indexer.GetForwardEnumerator(lo, num + 1);
          while (forwardEnumerator.MoveNext())
          {
            forwardEnumerator.Current.SetParent(parentRow);
            gridViewRowInfoList.Add(forwardEnumerator.Current);
          }
        }
      }
      return (IList<GridViewRowInfo>) gridViewRowInfoList;
    }

    public override void Refresh()
    {
      if (this.relation == null || this.IsSuspendedNotifications)
        return;
      this.comparer.Reset();
      if (!this.comparer.IsValid)
      {
        this.valid = false;
      }
      else
      {
        this.childRowsLoaded = true;
        this.valid = true;
        this.indexer.Clear();
        for (int index = 0; index < this.Template.ListSource.Count; ++index)
          this.indexer.Add(this.Template.ListSource[index]);
      }
    }

    public override bool IsValid
    {
      get
      {
        return this.valid;
      }
    }

    public override GridViewHierarchyRowInfo GetParent(
      GridViewRowInfo gridViewRowInfo)
    {
      foreach (GridViewRowInfo row in this.Template.Parent.Rows)
      {
        for (int index = 0; index < this.Relation.ChildColumnNames.Count; ++index)
        {
          object xValue = gridViewRowInfo.Cells[gridViewRowInfo.ViewTemplate == this.relation.ParentTemplate ? this.Relation.ParentColumnNames[index] : this.Relation.ChildColumnNames[index]].Value;
          object yValue = row.Cells[row.ViewTemplate == this.relation.ParentTemplate ? this.Relation.ParentColumnNames[index] : this.Relation.ChildColumnNames[index]].Value;
          IComparable comparable = xValue as IComparable;
          if ((comparable == null || yValue == null || (object) yValue.GetType() != (object) xValue.GetType() ? DataUtils.CompareNulls(xValue, yValue) : comparable.CompareTo(yValue)) == 0)
            return row as GridViewHierarchyRowInfo;
        }
      }
      return (GridViewHierarchyRowInfo) null;
    }

    public override void Dispose()
    {
      if (this.Template != null && this.Template.ListSource != null)
        this.Template.ListSource.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ListSource_CollectionChanged);
      base.Dispose();
    }
  }
}
