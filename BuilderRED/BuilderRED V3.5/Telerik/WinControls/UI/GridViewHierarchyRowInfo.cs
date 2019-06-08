// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHierarchyRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewHierarchyRowInfo : GridViewDataRowInfo
  {
    private int masterViewInfoVersion = -2147483647;
    private GridViewInfo activeView;
    private GridViewInfoCollection views;
    private GridViewDetailsRowInfo childRow;
    internal GridViewChildRowCollection childRows;
    private bool suspendCurrentRowChange;

    public GridViewHierarchyRowInfo(GridViewInfo owner)
      : base(owner)
    {
      if (this.ViewTemplate.Templates.Count <= 0)
        return;
      this.childRow = this.CreateGridViewDetailsRowInfo(this);
    }

    public GridViewHierarchyRowInfo(GridViewDataRowInfo row)
      : this(row.ViewInfo)
    {
      this.Cache = row.Cache;
    }

    protected virtual GridViewDetailsRowInfo CreateGridViewDetailsRowInfo(
      GridViewHierarchyRowInfo hierarchyRow)
    {
      GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewDetailsRowInfo(this), hierarchyRow.ViewInfo);
      this.ViewTemplate.OnCreateRowInfo(e);
      return e.RowInfo as GridViewDetailsRowInfo;
    }

    public int Level
    {
      get
      {
        int num = 1;
        for (IHierarchicalRow parent = this.Parent; parent != null && parent != this.ViewTemplate; parent = parent.Parent)
          ++num;
        return num;
      }
    }

    public GridViewInfo ActiveView
    {
      get
      {
        this.EnsureViews();
        return this.activeView;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("ActiveView can not be null.");
        if (this.activeView == value)
          return;
        this.EnsureViews();
        if (!this.views.Contains(value.ViewTemplate))
          throw new InvalidOperationException("Invalid View for this Template");
        this.activeView = value;
        if (this.ViewTemplate != null && this.ViewTemplate.MasterTemplate != null && (this.ViewTemplate.MasterTemplate.Owner != null && this.ViewTemplate.MasterTemplate.Owner.SplitMode != RadGridViewSplitMode.None) || (this.ViewTemplate.MasterTemplate == null || this.activeView.ChildRows.Count <= 0 || (this.ViewTemplate.MasterTemplate.CurrentRow == null || this.ViewTemplate.MasterTemplate.CurrentRow.ViewInfo == this.activeView)) || this.suspendCurrentRowChange)
          return;
        this.ViewTemplate.MasterTemplate.SuspendEnsureVisible = true;
        this.activeView.ChildRows[0].IsCurrent = true;
        this.ViewTemplate.MasterTemplate.SuspendEnsureVisible = false;
      }
    }

    public GridViewDetailsRowInfo ChildRow
    {
      get
      {
        return this.childRow;
      }
    }

    public override GridViewChildRowCollection ChildRows
    {
      get
      {
        if (this.ViewTemplate.IsSelfReference)
        {
          if (this.childRows == null || this.ViewTemplate.MasterViewInfo.Version != this.masterViewInfoVersion)
          {
            this.masterViewInfoVersion = this.ViewTemplate.MasterViewInfo.Version;
            this.childRows = new GridViewChildRowCollection();
            this.childRows.Load((IReadOnlyCollection<GridViewRowInfo>) this.ViewInfo.LoadHierarchicalData(this, (ICollectionView<GridViewRowInfo>) null));
          }
          if (this.childRows.Count != 0 || this.ViewTemplate.Templates.Count <= 0)
            return this.childRows;
          if (this.ActiveView != null && this.ActiveView.ViewTemplate == this.ViewTemplate)
          {
            this.suspendCurrentRowChange = true;
            this.ActiveView = this.Views[1];
            this.suspendCurrentRowChange = false;
          }
          return this.ActiveView.ChildRows;
        }
        if (this.ActiveView != null)
          return this.ActiveView.ChildRows;
        return GridViewChildRowCollection.Empty;
      }
    }

    public override IHierarchicalRow Parent
    {
      get
      {
        if (base.Parent == null)
          return (IHierarchicalRow) this.ViewTemplate;
        return base.Parent;
      }
    }

    public override bool HasChildViews
    {
      get
      {
        if (this.ViewTemplate != null)
          return this.ViewTemplate.IsSelfReference;
        return false;
      }
    }

    public IReadOnlyCollection<GridViewInfo> Views
    {
      get
      {
        this.EnsureViews();
        return (IReadOnlyCollection<GridViewInfo>) this.views;
      }
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridDataRowElement);
      }
    }

    public void EnsureViews()
    {
      if (this.views == null)
        this.views = new GridViewInfoCollection();
      this.RebuildViews();
      if (this.activeView != null && this.views.Contains(this.activeView))
        return;
      for (int index = 0; index < this.views.Count; ++index)
      {
        if (this.views[index].HasChildRows() || this.views[index].ViewTemplate.AllowAddNewRow)
        {
          this.activeView = this.views[index];
          break;
        }
      }
      if (this.activeView != null || this.views.Count <= 0)
        return;
      this.activeView = this.views[0];
    }

    private void RebuildViews()
    {
      if (this.ViewTemplate == null)
        return;
      int index1 = 0;
      while (index1 < this.views.Count)
      {
        GridViewTemplate viewTemplate = this.views[index1].ViewTemplate;
        if (viewTemplate == null || !this.ViewTemplate.Templates.Contains(viewTemplate))
        {
          if (viewTemplate == this.ViewTemplate && this.ViewTemplate.IsSelfReference)
            ++index1;
          else
            this.views.RemoveAt(index1);
        }
        else
          ++index1;
      }
      int count = this.ViewTemplate.Templates.Count;
      if (this.ViewTemplate.IsSelfReference)
        ++count;
      if (this.views.Count != count)
      {
        GridViewInfo[] gridViewInfoArray = new GridViewInfo[this.views.Count];
        this.views.CopyTo(gridViewInfoArray, 0);
        this.views.Clear();
        for (int index2 = 0; index2 < this.ViewTemplate.Templates.Count; ++index2)
          this.views.Add(this.Find(gridViewInfoArray, this.ViewTemplate.Templates[index2]) ?? new GridViewInfo(this.ViewTemplate.Templates[index2], this));
      }
      if (!this.ViewTemplate.IsSelfReference || this.views.Contains(this.ViewTemplate.MasterViewInfo))
        return;
      this.views.Insert(0, this.ViewTemplate.MasterViewInfo);
    }

    private GridViewInfo Find(
      GridViewInfo[] viewInfos,
      GridViewTemplate gridViewTemplate)
    {
      for (int index = 0; index < viewInfos.Length; ++index)
      {
        if (viewInfos[index].ViewTemplate == gridViewTemplate)
          return viewInfos[index];
      }
      return (GridViewInfo) null;
    }

    public override bool HasChildRows()
    {
      if (this.ViewTemplate != null && this.ViewTemplate.IsSelfReference)
      {
        foreach (GridViewInfo view in (IEnumerable<GridViewInfo>) this.Views)
        {
          if (!view.ViewTemplate.IsSelfReference && (view.HasChildRows() || view.ViewTemplate.AllowAddNewRow || view.ViewTemplate.FilterDescriptors.Count > 0))
            return true;
        }
        return base.HasChildRows();
      }
      foreach (GridViewInfo view in (IEnumerable<GridViewInfo>) this.Views)
      {
        if (view.HasChildRows())
          return true;
      }
      return false;
    }

    protected override void OnPropertyChanging(PropertyChangingEventArgsEx args)
    {
      base.OnPropertyChanging(args);
      if (!(args.PropertyName == "IsExpanded"))
        return;
      if ((bool) args.NewValue && !this.HasChildRows())
      {
        args.Cancel = true;
      }
      else
      {
        ChildViewExpandingEventArgs args1 = new ChildViewExpandingEventArgs(this);
        this.ViewTemplate.EventDispatcher.RaiseEvent<ChildViewExpandingEventArgs>(EventDispatcher.ChildViewExpanding, (object) this.ViewTemplate, args1);
        args.Cancel = args1.Cancel;
      }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.PropertyName == "IsExpanded"))
        return;
      if (this.ViewTemplate.EventDispatcher.IsSuspended)
        this.ViewTemplate.NotifyRowExpanded();
      ChildViewExpandedEventArgs args = new ChildViewExpandedEventArgs(this);
      this.ViewTemplate.EventDispatcher.RaiseEvent<ChildViewExpandedEventArgs>(EventDispatcher.ChildViewExpanded, (object) this.ViewTemplate, args);
    }
  }
}
