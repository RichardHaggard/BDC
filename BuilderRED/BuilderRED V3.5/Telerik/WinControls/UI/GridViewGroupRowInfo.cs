// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewGroupRowInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class GridViewGroupRowInfo : GridViewRowInfo
  {
    private int version = -1;
    internal int summaryRowsVersion = -1;
    private DataGroup ownerGroup;
    private List<GridViewSummaryRowInfo> topSummaryRowSource;
    private List<GridViewSummaryRowInfo> bottomSummaryRowSource;
    private GridViewSummaryRowCollection topSummaryRows;
    private GridViewSummaryRowCollection bottomSummaryRows;
    private GridViewChildRowCollection childRows;
    private string headerText;
    private string formattedSummaryText;
    private bool suspendGroupSummaryEvaluateEvent;
    private int childGroupsCount;

    public GridViewGroupRowInfo(GridViewInfo gridViewInfo, DataGroup dataGroup)
      : base(gridViewInfo)
    {
      this.topSummaryRowSource = new List<GridViewSummaryRowInfo>();
      this.topSummaryRows = new GridViewSummaryRowCollection((IList<GridViewSummaryRowInfo>) this.topSummaryRowSource);
      this.bottomSummaryRowSource = new List<GridViewSummaryRowInfo>();
      this.bottomSummaryRows = new GridViewSummaryRowCollection((IList<GridViewSummaryRowInfo>) this.bottomSummaryRowSource);
      this.ownerGroup = dataGroup;
      this.childRows = new GridViewChildRowCollection();
    }

    public override AllowedGridViewRowInfoStates AllowedStates
    {
      get
      {
        return AllowedGridViewRowInfoStates.Current | AllowedGridViewRowInfoStates.Expanadable;
      }
    }

    internal override bool IsValid
    {
      get
      {
        return this.Group != null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DataGroup Group
    {
      get
      {
        if (this.ownerGroup.IsValid)
          return this.ownerGroup;
        return (DataGroup) null;
      }
      internal set
      {
        this.ownerGroup = value;
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

    public int GroupLevel
    {
      get
      {
        if (this.Group != null)
          return this.Group.Level;
        return 0;
      }
    }

    public override GridViewChildRowCollection ChildRows
    {
      get
      {
        if (this.summaryRowsVersion != this.ViewTemplate.SummaryRowsVersion || this.Group.Groups.Count != this.childGroupsCount)
          this.BuildSummaryRows();
        if (this.version == this.ViewTemplate.DataView.Version)
          return this.childRows;
        DataGroup group = this.Group;
        DataGroupCollection groups = group.Groups;
        if (group == null)
        {
          this.childRows.Load((IList<GridViewRowInfo>) new GridViewRowInfo[0]);
          return this.childRows;
        }
        if (groups.Count == 0)
        {
          foreach (GridViewRowInfo gridViewRowInfo in (Telerik.WinControls.Data.Group<GridViewRowInfo>) group)
            gridViewRowInfo.SetParent((GridViewRowInfo) this);
          this.childRows.Load((IReadOnlyCollection<GridViewRowInfo>) group);
        }
        else
        {
          foreach (DataGroup dataGroup in (ReadOnlyCollection<Telerik.WinControls.Data.Group<GridViewRowInfo>>) groups)
          {
            dataGroup.GroupRow.SetParent((GridViewRowInfo) this);
            dataGroup.GroupRow.ViewInfo = this.ViewInfo;
          }
          this.childRows.Load((IReadOnlyCollection<GridViewRowInfo>) groups);
        }
        this.version = this.ViewTemplate.DataView.Version;
        return this.childRows;
      }
    }

    public GridViewSummaryRowCollection TopSummaryRows
    {
      get
      {
        return this.topSummaryRows;
      }
    }

    public GridViewSummaryRowCollection BottomSummaryRows
    {
      get
      {
        return this.bottomSummaryRows;
      }
    }

    public override Type RowElementType
    {
      get
      {
        return typeof (GridGroupHeaderRowElement);
      }
    }

    public string HeaderText
    {
      get
      {
        if (!string.IsNullOrEmpty(this.headerText))
          return this.headerText;
        if (this.Group == null || this.Group.Level >= this.ViewTemplate.GroupDescriptors.Count)
          return string.Empty;
        string str1 = string.Empty;
        GroupDescriptor groupDescriptor = this.ViewTemplate.GroupDescriptors[this.Group.Level];
        string str2 = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator + " ";
        for (int index1 = 0; index1 < groupDescriptor.GroupNames.Count; ++index1)
        {
          int index2 = this.ViewTemplate.Columns.IndexOf(groupDescriptor.GroupNames[index1].PropertyName);
          str1 = index2 < 0 ? str1 + groupDescriptor.GroupNames[index1].PropertyName + str2 : str1 + this.ViewTemplate.Columns[index2].HeaderText + str2;
        }
        if (!string.IsNullOrEmpty(str1))
        {
          string str3 = str1.Trim();
          string str4 = str3.Substring(0, str3.Length - 1);
          object[] objArray1 = new object[groupDescriptor.Aggregates.Count];
          for (int index = 0; index < objArray1.Length; ++index)
            objArray1[index] = this.Group.Evaluate(groupDescriptor.Aggregates[index]);
          GridViewGroupSummaryItem groupSummaryItem = new GridViewGroupSummaryItem();
          groupSummaryItem.SuspendNotifications();
          groupSummaryItem.Name = groupDescriptor.GroupNames[0].PropertyName;
          groupSummaryItem.FormatString = groupDescriptor.Format;
          groupSummaryItem.ResumeNotifications(false);
          object summary = this.GetValue(groupDescriptor);
          if (!this.suspendGroupSummaryEvaluateEvent)
          {
            this.suspendGroupSummaryEvaluateEvent = true;
            summary = this.RaiseGroupSummaryEvaluateEvent((GridViewSummaryItem) groupSummaryItem, summary);
            this.suspendGroupSummaryEvaluateEvent = false;
          }
          object[] objArray2 = new object[groupDescriptor.Aggregates.Count + 2];
          objArray2[0] = (object) str4;
          objArray2[1] = summary;
          for (int index = 0; index < objArray1.Length; ++index)
            objArray2[index + 2] = objArray1[index];
          str1 = string.Format(groupSummaryItem.FormatString, objArray2);
        }
        return str1;
      }
      set
      {
        if (!(this.headerText != value))
          return;
        string headerText = this.headerText;
        this.headerText = value;
        this.OnPropertyChanged((PropertyChangedEventArgs) new GridPropertyChangedEventArgs(nameof (HeaderText), (object) headerText, (object) this.headerText));
      }
    }

    public string GetSummary()
    {
      if (this.formattedSummaryText != null)
        return this.formattedSummaryText;
      if (this.Group == null)
        return string.Empty;
      string str = Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator + " ";
      StringBuilder stringBuilder = new StringBuilder(16);
      foreach (Collection<GridViewSummaryItem> summaryRowGroupHeader in (Collection<GridViewSummaryRowItem>) this.ViewTemplate.SummaryRowGroupHeaders)
      {
        foreach (GridViewSummaryItem summaryItem in summaryRowGroupHeader)
        {
          summaryItem.SuspendNotifications();
          summaryItem.Template = this.ViewTemplate;
          summaryItem.ResumeNotifications(false);
          object obj = this.ProcessSummaryItem(summaryItem);
          GridViewColumn column = (GridViewColumn) this.ViewTemplate.Columns[summaryItem.Name];
          if (column != null)
            this[column] = obj;
          stringBuilder.Append(string.Format(summaryItem.FormatString, obj) + str);
        }
      }
      this.formattedSummaryText = stringBuilder.ToString();
      this.formattedSummaryText = this.formattedSummaryText.TrimEnd(str.ToCharArray());
      return this.formattedSummaryText;
    }

    protected internal override void ClearCache()
    {
      base.ClearCache();
      this.formattedSummaryText = (string) null;
    }

    protected virtual void BuildSummaryRows()
    {
      if (this.ViewTemplate.ShowParentGroupSummaries || this.Group.Groups.Count == 0)
      {
        this.BuildTopSummaryRows();
        this.BuildBottomSummaryRows();
      }
      else
      {
        this.topSummaryRows.Clear();
        this.bottomSummaryRows.Clear();
      }
      this.summaryRowsVersion = this.ViewTemplate.SummaryRowsVersion;
      this.childGroupsCount = this.Group.Groups.Count;
    }

    protected virtual void BuildTopSummaryRows()
    {
      if (this.ViewTemplate.SummaryRowsTop.Count <= 0)
        return;
      int index = 0;
      while (index < this.topSummaryRowSource.Count)
      {
        if (!this.ViewTemplate.SummaryRowsTop.Contains(this.topSummaryRowSource[index].SummaryRowItem))
          this.topSummaryRowSource.RemoveAt(index);
        else
          ++index;
      }
      foreach (GridViewSummaryRowItem summaryItem in (Collection<GridViewSummaryRowItem>) this.ViewTemplate.SummaryRowsTop)
      {
        if (!this.topSummaryRows.Contains(summaryItem))
        {
          GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewSummaryRowInfo(this.ViewInfo, this), this.ViewInfo);
          this.ViewTemplate.OnCreateRowInfo(e);
          GridViewSummaryRowInfo rowInfo = e.RowInfo as GridViewSummaryRowInfo;
          if (rowInfo != null)
          {
            rowInfo.SummaryRowItem = summaryItem;
            this.topSummaryRowSource.Add(rowInfo);
          }
        }
      }
    }

    protected virtual void BuildBottomSummaryRows()
    {
      if (this.ViewTemplate.SummaryRowsBottom.Count <= 0)
        return;
      int index = 0;
      while (index < this.bottomSummaryRowSource.Count)
      {
        if (!this.ViewTemplate.SummaryRowsBottom.Contains(this.bottomSummaryRowSource[index].SummaryRowItem))
          this.bottomSummaryRowSource.RemoveAt(index);
        else
          ++index;
      }
      foreach (GridViewSummaryRowItem summaryItem in (Collection<GridViewSummaryRowItem>) this.ViewTemplate.SummaryRowsBottom)
      {
        if (!this.bottomSummaryRows.Contains(summaryItem))
        {
          GridViewCreateRowInfoEventArgs e = new GridViewCreateRowInfoEventArgs((GridViewRowInfo) new GridViewSummaryRowInfo(this.ViewInfo, this), this.ViewInfo);
          this.ViewTemplate.OnCreateRowInfo(e);
          GridViewSummaryRowInfo rowInfo = e.RowInfo as GridViewSummaryRowInfo;
          if (rowInfo != null)
          {
            rowInfo.SummaryRowItem = summaryItem;
            this.bottomSummaryRowSource.Add(rowInfo);
          }
        }
      }
    }

    private object ProcessSummaryItem(GridViewSummaryItem summaryItem)
    {
      object summary = summaryItem.Evaluate((IHierarchicalRow) this.Group.GroupRow);
      this.RaiseGroupSummaryEvaluateEvent(summaryItem, summary);
      return summary;
    }

    private object RaiseGroupSummaryEvaluateEvent(GridViewSummaryItem summaryItem, object summary)
    {
      summaryItem.SuspendNotifications();
      summaryItem.Template = this.ViewTemplate;
      GroupSummaryEvaluationEventArgs args = new GroupSummaryEvaluationEventArgs(summary, this.Group, summaryItem.FormatString, summaryItem, (IHierarchicalRow) this.Group.GroupRow, (object) this);
      args.Value = summary;
      this.ViewTemplate.EventDispatcher.RaiseEvent<GroupSummaryEvaluationEventArgs>(EventDispatcher.GroupSummaryEvaluate, (object) this.ViewTemplate, args);
      summaryItem.FormatString = args.FormatString;
      summaryItem.ResumeNotifications(false);
      return args.Value;
    }

    private object GetValue(GroupDescriptor descriptor)
    {
      object[] key = this.ownerGroup.Key as object[];
      if (key != null)
      {
        if (key.Length == 1)
          return this.GetComboBoxColumnLookupValue(descriptor.GroupNames[0].PropertyName, key[0]);
        StringBuilder stringBuilder = new StringBuilder((int) byte.MaxValue);
        for (int index = 0; index < key.Length && index < descriptor.GroupNames.Count; ++index)
        {
          if (key[index] != null)
          {
            object columnLookupValue = this.GetComboBoxColumnLookupValue(descriptor.GroupNames[index].PropertyName, key[index]);
            stringBuilder.Append(columnLookupValue.ToString() + (object) ',');
          }
        }
        if (stringBuilder.Length > 1)
          return (object) stringBuilder.ToString(0, stringBuilder.Length - 1);
      }
      return (object) null;
    }

    private object GetComboBoxColumnLookupValue(string name, object key)
    {
      GridViewComboBoxColumn column = this.ViewTemplate.Columns[name] as GridViewComboBoxColumn;
      if (column != null)
      {
        object obj = column.DisplayMemberSort ? key : column.GetLookupValue(key);
        if (obj != null)
          return obj;
      }
      return key;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("PinPosition property is not supported for GridViewGroupRowInfo.")]
    [Browsable(false)]
    public override PinnedRowPosition PinPosition
    {
      get
      {
        return base.PinPosition;
      }
      set
      {
        throw new InvalidOperationException("GroupRow cannot be pinned!");
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("IsPinned property is not supported for GridViewGroupRowInfo.")]
    public override bool IsPinned
    {
      get
      {
        return base.IsPinned;
      }
      set
      {
        throw new InvalidOperationException("GroupRow cannot be pinned!");
      }
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.PropertyName == "IsExpanded"))
        return;
      GroupExpandedEventArgs args = new GroupExpandedEventArgs(this.Group, this.IsExpanded);
      this.ViewTemplate.EventDispatcher.RaiseEvent<GroupExpandedEventArgs>(EventDispatcher.GroupExpanded, (object) this.ViewTemplate, args);
      if (this.ViewTemplate == null)
        return;
      this.ViewTemplate.OnViewChanged((object) this, new DataViewChangedEventArgs(ViewChangedAction.ExpandedChanged));
    }

    protected override void OnPropertyChanging(PropertyChangingEventArgsEx args)
    {
      base.OnPropertyChanging(args);
      if (!(args.PropertyName == "IsExpanded"))
        return;
      this.BuildSummaryRows();
      GroupExpandingEventArgs args1 = new GroupExpandingEventArgs(this.Group, this.IsExpanded);
      this.ViewTemplate.EventDispatcher.RaiseEvent<GroupExpandingEventArgs>(EventDispatcher.GroupExpanding, (object) this.ViewTemplate, args1);
      if (!args1.Cancel)
        return;
      args.Cancel = true;
    }
  }
}
