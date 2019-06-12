// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataGroup
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
  public class DataGroup : DataItemGroup<GridViewRowInfo>
  {
    private GridViewGroupRowInfo groupRow;
    private bool isValid;
    private bool isExpanded;
    private GroupDescriptor groupDescriptor;

    public DataGroup(object key, Group<GridViewRowInfo> parent, GridViewTemplate viewTemplate)
      : base(key, parent)
    {
      this.isValid = true;
      this.groupRow = new GridViewGroupRowInfo(viewTemplate.MasterViewInfo, this);
      this.groupRow.SuspendPropertyNotifications();
      this.groupRow.IsExpanded = viewTemplate.AutoExpandGroups;
      this.groupRow.ResumePropertyNotifications();
    }

    public override bool Contains(GridViewRowInfo item)
    {
      GridViewSummaryRowInfo viewSummaryRowInfo = item as GridViewSummaryRowInfo;
      if (this.groupRow != null && viewSummaryRowInfo != null && (this.groupRow.TopSummaryRows.Contains(viewSummaryRowInfo) || this.groupRow.BottomSummaryRows.Contains(viewSummaryRowInfo)))
        return true;
      return base.Contains(item);
    }

    public override void CopyTo(GridViewRowInfo[] array, int index)
    {
      for (int index1 = index; index1 < this.ItemCount; ++index1)
        array[index1] = this[index1];
    }

    public override IEnumerator<GridViewRowInfo> GetEnumerator()
    {
      foreach (GridViewRowInfo gridViewRowInfo in (IEnumerable<GridViewRowInfo>) this.Items)
        yield return gridViewRowInfo;
    }

    public void Expand()
    {
      if (this.groupRow == null)
        this.isExpanded = true;
      else
        this.groupRow.IsExpanded = true;
    }

    public void Expand(bool recursive)
    {
      if (this.groupRow == null)
      {
        this.isExpanded = true;
        if (!recursive)
          return;
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
          group.Expand(recursive);
      }
      else
      {
        this.groupRow.IsExpanded = true;
        if (!recursive)
          return;
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
          group.Expand(recursive);
      }
    }

    public void Collapse()
    {
      if (this.groupRow == null)
        this.isExpanded = false;
      else
        this.groupRow.IsExpanded = false;
    }

    public void Collapse(bool recursive)
    {
      if (this.groupRow == null)
      {
        this.isExpanded = false;
        if (!recursive)
          return;
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
          group.Collapse(recursive);
      }
      else
      {
        this.groupRow.IsExpanded = false;
        if (!recursive)
          return;
        foreach (DataGroup group in (ReadOnlyCollection<Group<GridViewRowInfo>>) this.Groups)
          group.Collapse(recursive);
      }
    }

    public override GridViewRowInfo this[int index]
    {
      get
      {
        if (index < 0 && index >= this.ItemCount)
          throw new IndexOutOfRangeException("Invalid index.");
        if (this.groupRow == null)
          return base[index];
        return this.Items[index];
      }
    }

    public override string Header
    {
      get
      {
        return base.Header;
      }
      set
      {
        base.Header = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsValid
    {
      get
      {
        return this.isValid;
      }
      internal set
      {
        this.isValid = value;
      }
    }

    public virtual DataGroupCollection Groups
    {
      get
      {
        if (base.Groups is DataGroupCollection)
          return (DataGroupCollection) base.Groups;
        return new DataGroupCollection((IList<Group<GridViewRowInfo>>) new ObservableCollection<Group<GridViewRowInfo>>());
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewGroupRowInfo GroupRow
    {
      get
      {
        return this.groupRow;
      }
      internal set
      {
        this.groupRow = value;
        if (this.groupRow == null)
          return;
        this.groupRow.IsExpanded = this.isExpanded;
      }
    }

    public bool IsExpanded
    {
      get
      {
        if (this.groupRow == null)
          return this.isExpanded;
        return this.groupRow.IsExpanded;
      }
    }

    public GroupDescriptor GroupDescriptor
    {
      get
      {
        return this.groupDescriptor;
      }
      internal set
      {
        this.groupDescriptor = value;
      }
    }
  }
}
