// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSummaryRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Telerik.WinControls.UI
{
  public class GridViewSummaryRowInfo : GridViewSystemRowInfo
  {
    private int summaryValueVersion = -1;
    private GridViewSummaryRowItem summaryRowItem;
    private GridViewGroupRowInfo groupRow;
    private GridViewHierarchyRowInfo hierarchyRow;
    private bool suspendGroupSummaryEvaluateEvent;

    public GridViewSummaryRowInfo(GridViewInfo gridViewInfo, GridViewGroupRowInfo group)
      : base(gridViewInfo)
    {
      this.groupRow = group;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual GridViewSummaryRowItem SummaryRowItem
    {
      get
      {
        return this.summaryRowItem;
      }
      internal set
      {
        this.summaryRowItem = value;
      }
    }

    internal GridViewHierarchyRowInfo HierarchyRow
    {
      get
      {
        return this.hierarchyRow;
      }
      set
      {
        this.hierarchyRow = value;
      }
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridSummaryRowElement);
      }
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.None;
      }
    }

    internal override object this[GridViewColumn column]
    {
      get
      {
        GridViewDataColumn column1 = column as GridViewDataColumn;
        if (column1 != null)
          return (object) this.GetSummary(column1);
        return base[column];
      }
      set
      {
        base[column] = value;
      }
    }

    public override int HierarchyLevel
    {
      get
      {
        int num = 0;
        if (this.HierarchyRow != null)
          num = this.HierarchyRow.HierarchyLevel + 1;
        else if (this.groupRow != null)
          num = this.groupRow.HierarchyLevel + 1;
        return num;
      }
    }

    public virtual string GetSummary(GridViewDataColumn column)
    {
      if (column.Index < 0)
        return string.Empty;
      if (this.summaryValueVersion != this.ViewInfo.summaryValueVersion)
      {
        this.ClearCache();
        this.summaryValueVersion = this.ViewInfo.summaryValueVersion;
      }
      object obj1 = this.Cache[(GridViewColumn) column];
      if (obj1 != null)
        return obj1.ToString();
      if (this.summaryRowItem == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder(16);
      foreach (GridViewSummaryItem summaryItem in (Collection<GridViewSummaryItem>) this.summaryRowItem)
      {
        if (summaryItem.Name == column.Name)
        {
          DataGroup group = (DataGroup) null;
          object obj2 = (object) null;
          IHierarchicalRow parent;
          if (this.groupRow != null)
          {
            obj2 = summaryItem.Evaluate((IHierarchicalRow) this.groupRow);
            parent = (IHierarchicalRow) this.groupRow;
            group = this.groupRow.Group;
          }
          else if (this.hierarchyRow != null)
          {
            if (this.hierarchyRow.ActiveView.ChildRows.Count > 0)
              obj2 = summaryItem.Evaluate((IHierarchicalRow) this.hierarchyRow);
            parent = (IHierarchicalRow) this.hierarchyRow;
          }
          else
          {
            if (this.ViewTemplate.DataView.Count > 0)
              obj2 = summaryItem.Evaluate((IHierarchicalRow) this.ViewTemplate);
            parent = (IHierarchicalRow) this.ViewTemplate;
          }
          string formatString = summaryItem.FormatString;
          object obj3 = obj2;
          if (!this.suspendGroupSummaryEvaluateEvent)
          {
            this.suspendGroupSummaryEvaluateEvent = true;
            GroupSummaryEvaluationEventArgs args = new GroupSummaryEvaluationEventArgs(obj2, group, summaryItem.FormatString, summaryItem, parent, (object) this);
            this.ViewTemplate.EventDispatcher.RaiseEvent<GroupSummaryEvaluationEventArgs>(EventDispatcher.GroupSummaryEvaluate, (object) this.ViewTemplate, args);
            formatString = args.FormatString;
            obj3 = args.Value;
            this.suspendGroupSummaryEvaluateEvent = false;
          }
          if (obj2 != null)
            stringBuilder.Append(string.Format(formatString, obj3));
        }
      }
      string str = stringBuilder.ToString();
      this.Cache[(GridViewColumn) column] = (object) str;
      return str;
    }

    public virtual object[] GetSummaryValues(GridViewDataColumn column)
    {
      List<object> objectList = new List<object>();
      foreach (GridViewSummaryItem gridViewSummaryItem in (Collection<GridViewSummaryItem>) this.summaryRowItem)
      {
        if (gridViewSummaryItem.Name == column.Name)
        {
          if (string.IsNullOrEmpty(gridViewSummaryItem.GetSummaryExpression()))
            throw new ArgumentException("The aggregate expression is not set. You need to define an aggregate expression for your summary e.g. Sum, Min, Max.");
          objectList.Add(this.ViewTemplate.DataView.Evaluate(gridViewSummaryItem.GetSummaryExpression(), this.groupRow.Group[0].Index, this.groupRow.Group.ItemCount));
        }
      }
      return objectList.ToArray();
    }

    public override DataGroup Group
    {
      get
      {
        if (this.groupRow != null)
          return this.groupRow.Group;
        return base.Group;
      }
      internal set
      {
        base.Group = value;
      }
    }

    public override void InvalidateRow()
    {
      this.ClearCache();
      base.InvalidateRow();
    }
  }
}
