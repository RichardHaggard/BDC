// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupRowLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ColumnGroupRowLayout : TableViewRowLayoutBase
  {
    private Dictionary<GridViewColumnGroup, GridViewGroupColumn> groupColumns = new Dictionary<GridViewColumnGroup, GridViewGroupColumn>();
    private List<GridViewColumn> systemColumns = new List<GridViewColumn>();
    private List<GridViewColumn> scrollableColumns = new List<GridViewColumn>();
    private SizeF desiredSize = SizeF.Empty;
    private ColumnGroupsViewDefinition viewDefinition;
    private int lastPinnedLeftPosition;
    private int lastScrollablePosition;
    private bool isResizeInProgress;
    private SizeF cachedAvailableSize;
    private Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> currentAutoSizeArrangeInfo;
    private ColumnGroupLayoutTree tree;
    private bool showEmptyGroups;

    public ColumnGroupRowLayout(ColumnGroupsViewDefinition viewDefinition)
    {
      this.viewDefinition = viewDefinition;
    }

    public override void Initialize(GridTableElement tableElement)
    {
      this.tree = new ColumnGroupLayoutTree(this, tableElement.ViewTemplate);
      base.Initialize(tableElement);
    }

    public Dictionary<GridViewColumnGroup, GridViewGroupColumn> GroupColumns
    {
      get
      {
        return this.groupColumns;
      }
    }

    public override void Dispose()
    {
      this.viewDefinition = (ColumnGroupsViewDefinition) null;
      base.Dispose();
    }

    internal bool IsResizeInProgress
    {
      get
      {
        return this.isResizeInProgress;
      }
    }

    public bool ShowEmptyGroups
    {
      get
      {
        if (this.viewDefinition != null && (this.viewDefinition.ViewTemplate.Site != null || this.viewDefinition.ViewTemplate.MasterTemplate != null && this.viewDefinition.ViewTemplate.MasterTemplate.OwnerSite != null))
          return true;
        return this.showEmptyGroups;
      }
      set
      {
        this.showEmptyGroups = value;
      }
    }

    public override SizeF DesiredSize
    {
      get
      {
        return this.desiredSize;
      }
    }

    public int GroupsHeight
    {
      get
      {
        if (this.tree == null)
          return 0;
        return (int) this.tree.GroupRowsTotalHeight;
      }
    }

    public int ColumnsHeight
    {
      get
      {
        if (this.tree == null)
          return 0;
        return (int) this.tree.ColumnRowsTotalHeight;
      }
    }

    public override IList<GridViewColumn> ScrollableColumns
    {
      get
      {
        return (IList<GridViewColumn>) this.scrollableColumns;
      }
    }

    public ColumnGroupsViewDefinition ViewDefinition
    {
      get
      {
        return this.viewDefinition;
      }
    }

    public List<GridViewColumn> SystemColumns
    {
      get
      {
        return this.systemColumns;
      }
    }

    internal bool ColumnWidthUpdateSuspended
    {
      get
      {
        return this.tree.widthChangesSuspended;
      }
    }

    public override SizeF MeasureRow(SizeF availableSize)
    {
      if ((double) this.cachedAvailableSize.Width == (double) availableSize.Width)
        return this.desiredSize;
      this.cachedAvailableSize = availableSize;
      if (this.ViewTemplate.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill)
        this.Owner.ViewElement.InvalidateMeasure(true);
      this.desiredSize = this.tree.MeasureRow(availableSize);
      if (this.Context != GridLayoutContext.Printer)
      {
        this.Owner.ColumnScroller.ClientSize = new SizeF(availableSize.Width, availableSize.Height);
        this.Owner.ColumnScroller.UpdateScrollRange((int) this.desiredSize.Width, true);
      }
      this.isResizeInProgress = false;
      return this.DesiredSize;
    }

    public override SizeF MeasurePinnedColumns(PinnedColumnTraverser dataProvider)
    {
      SizeF sizeF = new SizeF(0.0f, this.desiredSize.Height);
      float val1 = float.MaxValue;
      foreach (GridViewColumn column in (ItemsTraverser<GridViewColumn>) dataProvider)
      {
        ColumnGroupsCellArrangeInfo columnData = this.GetColumnData(column);
        if (columnData != null)
        {
          sizeF.Width = Math.Max(sizeF.Width, (float) (int) columnData.Bounds.Right);
          val1 = Math.Min(val1, columnData.Bounds.X);
        }
      }
      if ((double) val1 != 3.40282346638529E+38)
        sizeF.Width -= val1;
      if (dataProvider.PinPosition == PinnedColumnPosition.Right)
        sizeF.Width -= (float) this.Owner.CellSpacing;
      return sizeF;
    }

    public override RectangleF ArrangeCell(RectangleF clientRect, GridCellElement cell)
    {
      RectangleF correctedColumnBounds1 = this.GetCorrectedColumnBounds(cell.RowInfo, cell.ColumnInfo, cell.RightToLeft, clientRect);
      if (this.ViewTemplate.IsSelfReference && cell is GridDataCellElement && (this.FirstDataColumn != null && cell.ColumnInfo != this.FirstDataColumn))
      {
        RectangleF correctedColumnBounds2 = this.GetCorrectedColumnBounds(cell.RowInfo, (GridViewColumn) this.FirstDataColumn, cell.RightToLeft, clientRect);
        ((GridDataCellElement) cell).IsLeftMost = cell.RightToLeft ? (double) Math.Abs(correctedColumnBounds2.Right - correctedColumnBounds1.Right) < 0.01 : (double) Math.Abs(correctedColumnBounds2.X - correctedColumnBounds1.X) < 0.01;
      }
      return correctedColumnBounds1;
    }

    public RectangleF GetCorrectedColumnBounds(
      GridViewRowInfo row,
      GridViewColumn column,
      bool rightToLeft,
      RectangleF clientRect)
    {
      ColumnGroupsCellArrangeInfo info = this.currentAutoSizeArrangeInfo == null || !this.currentAutoSizeArrangeInfo.ContainsKey(column) ? this.GetColumnData(column) : this.currentAutoSizeArrangeInfo[column];
      if (info == null)
        return RectangleF.Empty;
      RectangleF bounds = info.Bounds;
      if (row == null || row is GridViewTableHeaderRowInfo || this.Owner.GridViewElement.AutoSizeRows)
      {
        float num1 = info.Bounds.X + info.Bounds.Width;
        float num2 = info.Bounds.Y + info.Bounds.Height;
        bounds = new RectangleF((float) (int) info.Bounds.X, (float) (int) info.Bounds.Y, (float) ((int) num1 - (int) info.Bounds.X), (float) ((int) num2 - (int) info.Bounds.Y));
      }
      else
      {
        float num1 = info.Bounds.Y - this.tree.GroupRowsTotalHeight;
        float num2 = info.Bounds.X + info.Bounds.Width;
        float num3 = num1 + info.Bounds.Height;
        if ((row is GridViewDataRowInfo || row is GridViewSummaryRowInfo) && row.Height != -1)
        {
          num1 *= (float) row.Height / this.tree.ColumnRowsTotalHeight;
          num3 *= (float) row.Height / this.tree.ColumnRowsTotalHeight;
        }
        bounds = new RectangleF((float) (int) info.Bounds.X, (float) (int) num1, (float) ((int) num2 - (int) info.Bounds.X), (float) ((int) num3 - (int) num1));
      }
      int x = (int) bounds.X;
      bounds.X = column.PinPosition == PinnedColumnPosition.Right ? bounds.X + (float) this.Owner.CellSpacing : Math.Max(0.0f, bounds.X + (float) this.Owner.CellSpacing);
      bounds.Width = (float) ((int) bounds.Width - ((int) bounds.X - x));
      if (row is GridViewTableHeaderRowInfo && this.ShouldStretchColumn(info))
      {
        bounds.Y -= this.tree.GroupRowsTotalHeight;
        bounds.Height += this.tree.GroupRowsTotalHeight;
      }
      else if (row is GridViewDataRowInfo && info.RowIndex > 0 || !(row is GridViewDataRowInfo) && info.Depth > 1)
      {
        bounds.Y += (float) this.Owner.CellSpacing;
        bounds.Height -= (float) this.Owner.CellSpacing;
      }
      if (column is GridViewRowHeaderColumn || column is GridViewIndentColumn)
      {
        bounds.Y = 0.0f;
        bounds.Height = clientRect.Height;
      }
      if (rightToLeft)
        bounds = LayoutUtils.RTLTranslateNonRelative(bounds, clientRect);
      return bounds;
    }

    private bool ShouldStretchColumn(ColumnGroupsCellArrangeInfo info)
    {
      GridViewColumnGroup group = info.Group;
      if (group == null)
        return false;
      GridViewColumnGroup rootColumnGroup = group?.RootColumnGroup;
      return (rootColumnGroup == null || !rootColumnGroup.ShowHeader) && (info.Row == null && group.Parent == rootColumnGroup || group == rootColumnGroup && group.Rows.IndexOf(info.Row) == 0);
    }

    public override int GetColumnOffset(GridViewColumn column)
    {
      ColumnGroupsCellArrangeInfo columnData = this.GetColumnData(column);
      if (columnData == null)
        return 0;
      return (int) columnData.Bounds.X;
    }

    public override void StartColumnResize(GridViewColumn column)
    {
      this.tree.StartColumnResize(column);
    }

    public override void EndColumnResize()
    {
      this.tree.EndColumnResize();
    }

    public override void ResizeColumn(int delta)
    {
      this.isResizeInProgress = true;
      this.cachedAvailableSize = SizeF.Empty;
      this.tree.ResizeColumn(delta);
      this.Owner.ViewElement.InvalidateMeasure(true);
    }

    public override void InvalidateRenderColumns()
    {
      base.InvalidateRenderColumns();
      foreach (DisposableObject disposableObject in this.groupColumns.Values)
        disposableObject.Dispose();
      this.systemColumns.Clear();
      this.groupColumns.Clear();
      this.scrollableColumns.Clear();
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.RenderColumns)
        this.systemColumns.Add(renderColumn);
      this.lastPinnedLeftPosition = this.RenderColumns.Count;
      this.lastScrollablePosition = this.RenderColumns.Count;
      foreach (GridViewColumnGroup columnGroup in (Collection<GridViewColumnGroup>) this.ViewDefinition.ColumnGroups)
        this.AddGroupColumn(columnGroup);
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.RenderColumns)
      {
        GridViewDataColumn column = renderColumn as GridViewDataColumn;
        if (column != null)
        {
          this.SetFirstDataColumn(column);
          break;
        }
      }
      this.tree.Rebuild(this.ViewDefinition.ColumnGroups);
    }

    public override void InvalidateLayout()
    {
      this.cachedAvailableSize = SizeF.Empty;
    }

    public override int GetRowHeight(GridViewRowInfo rowInfo)
    {
      GridViewDetailsRowInfo viewDetailsRowInfo = rowInfo as GridViewDetailsRowInfo;
      if (viewDetailsRowInfo != null && viewDetailsRowInfo.ActualHeight != -1)
        return viewDetailsRowInfo.ActualHeight;
      if (rowInfo is GridViewTableHeaderRowInfo)
      {
        if (!this.Owner.GridViewElement.AutoSizeRows || rowInfo.Height == -1)
          return (int) this.DesiredSize.Height;
        return rowInfo.Height;
      }
      if (rowInfo.Height != -1)
        return Math.Max(rowInfo.Height, rowInfo.MinHeight);
      if (rowInfo is GridViewGroupRowInfo)
        return this.Owner.GroupHeaderHeight;
      return (int) this.tree.ColumnRowsTotalHeight;
    }

    public virtual bool IsColumnVisible(GridViewColumn column, RectangleF viewRect)
    {
      ColumnGroupsCellArrangeInfo columnData = this.GetColumnData(column);
      if (columnData == null)
        return false;
      RectangleF bounds = columnData.Bounds;
      if ((double) bounds.Left >= (double) viewRect.Left && (double) bounds.Left <= (double) viewRect.Right || (double) bounds.Right >= (double) viewRect.Left && (double) bounds.Right <= (double) viewRect.Right)
        return true;
      if ((double) bounds.Left < (double) viewRect.Left)
        return (double) bounds.Right > (double) viewRect.Right;
      return false;
    }

    public override void EnsureColumnsLayout()
    {
    }

    public ColumnGroupsCellArrangeInfo GetColumnData(
      GridViewColumn column)
    {
      return this.tree.GetColumnData(column);
    }

    private void AddGroupColumn(GridViewColumnGroup group)
    {
      if (!group.IsVisible)
        return;
      GridViewGroupColumn gridViewGroupColumn = new GridViewGroupColumn(group);
      gridViewGroupColumn.OwnerTemplate = this.ViewTemplate;
      this.groupColumns.Add(group, gridViewGroupColumn);
      this.AddColumn(group, (GridViewColumn) gridViewGroupColumn);
      foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
        this.AddGroupColumn(group1);
      if (group.Groups.Count != 0)
        return;
      foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
      {
        foreach (string columnName in (Collection<string>) row.ColumnNames)
        {
          if (this.ColumnIsVisible((GridViewColumn) this.ViewTemplate.Columns[columnName]) || this.IgnoreColumnVisibility)
            this.AddColumn(group, (GridViewColumn) this.ViewTemplate.Columns[columnName]);
        }
      }
    }

    private void AddColumn(GridViewColumnGroup group, GridViewColumn column)
    {
      if (!column.IsVisible && !this.IgnoreColumnVisibility || column is GridViewGroupColumn && !group.ShowHeader)
        return;
      if (group.PinPosition == PinnedColumnPosition.Left)
      {
        this.RenderColumns.Insert(this.lastPinnedLeftPosition, column);
        ++this.lastPinnedLeftPosition;
        ++this.lastScrollablePosition;
      }
      else if (group.PinPosition == PinnedColumnPosition.Right)
      {
        this.RenderColumns.Add(column);
      }
      else
      {
        this.RenderColumns.Insert(this.lastScrollablePosition, column);
        this.scrollableColumns.Add(column);
        ++this.lastScrollablePosition;
      }
    }

    public void SetBestFitWidth(GridViewColumn column, float desiredWidth)
    {
      this.isResizeInProgress = true;
      this.cachedAvailableSize = SizeF.Empty;
      this.tree.SetBestFitWidth(column, desiredWidth);
      this.Owner.ViewElement.InvalidateMeasure(true);
    }

    public float MeasureAutoSizeRow(RadElementCollection cells)
    {
      List<float> floatList = new List<float>();
      Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> columnsData = this.tree.GetColumnsData();
      foreach (RadElement cell in cells)
      {
        GridCellElement gridCellElement = cell as GridCellElement;
        if (gridCellElement != null && columnsData.ContainsKey(gridCellElement.ColumnInfo))
        {
          ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = columnsData[gridCellElement.ColumnInfo];
          if (!(gridCellElement.ColumnInfo is GridViewGroupColumn) || this.HasVisibleColumn(((GridViewGroupColumn) gridCellElement.ColumnInfo).Group))
          {
            while (floatList.Count <= groupsCellArrangeInfo.Depth)
              floatList.Add(0.0f);
            floatList[groupsCellArrangeInfo.Depth] = Math.Max(floatList[groupsCellArrangeInfo.Depth], gridCellElement.DesiredSize.Height);
          }
        }
      }
      float num1 = 0.0f;
      foreach (float num2 in floatList)
        num1 += num2;
      return num1;
    }

    public void EndAutoSizeRowArrange()
    {
      this.currentAutoSizeArrangeInfo = (Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo>) null;
    }

    public void BeginAutoSizeRowArrange(RadElementCollection cells)
    {
      List<float> floatList1 = new List<float>();
      int val1_1 = 0;
      int val1_2 = 0;
      Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> columnsData = this.tree.GetColumnsData();
      foreach (RadElement cell in cells)
      {
        GridCellElement gridCellElement = cell as GridCellElement;
        if (gridCellElement != null && columnsData.ContainsKey(gridCellElement.ColumnInfo))
        {
          ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = columnsData[gridCellElement.ColumnInfo];
          if (gridCellElement.ColumnInfo is GridViewGroupColumn)
          {
            if (this.HasVisibleColumn(((GridViewGroupColumn) gridCellElement.ColumnInfo).Group))
              val1_1 = Math.Max(val1_1, groupsCellArrangeInfo.Depth);
            else
              continue;
          }
          else
            val1_2 = Math.Max(val1_2, groupsCellArrangeInfo.Depth);
          while (floatList1.Count <= groupsCellArrangeInfo.Depth)
            floatList1.Add(0.0f);
          floatList1[groupsCellArrangeInfo.Depth] = Math.Max(floatList1[groupsCellArrangeInfo.Depth], gridCellElement.DesiredSize.Height);
        }
      }
      if (floatList1.Count == 0)
        return;
      List<float> floatList2 = new List<float>();
      floatList2.Add(floatList1[0]);
      for (int index = 1; index < floatList1.Count; ++index)
        floatList2.Add(floatList2[index - 1] + floatList1[index]);
      foreach (RadElement cell in cells)
      {
        GridCellElement gridCellElement = cell as GridCellElement;
        if (gridCellElement != null && columnsData.ContainsKey(gridCellElement.ColumnInfo))
        {
          ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = columnsData[gridCellElement.ColumnInfo];
          groupsCellArrangeInfo.Bounds.Height = floatList1[groupsCellArrangeInfo.Depth];
          groupsCellArrangeInfo.Bounds.Y = floatList2[groupsCellArrangeInfo.Depth - 1];
          if (gridCellElement.ColumnInfo is GridViewGroupColumn)
          {
            if (((GridViewGroupColumn) gridCellElement.ColumnInfo).Group.Groups.Count == 0)
              groupsCellArrangeInfo.Bounds.Height = floatList2[val1_1] - floatList2[groupsCellArrangeInfo.Depth - 1];
          }
          else if (groupsCellArrangeInfo.RowIndex == groupsCellArrangeInfo.Group.Rows.Count - 1)
            groupsCellArrangeInfo.Bounds.Height = floatList2[val1_2] - floatList2[groupsCellArrangeInfo.Depth - 1];
        }
      }
      foreach (KeyValuePair<GridViewColumn, ColumnGroupsCellArrangeInfo> keyValuePair in this.tree.GetSystemColumnsData())
      {
        keyValuePair.Value.Bounds.Height = floatList2[floatList2.Count - 1];
        columnsData.Add(keyValuePair.Key, keyValuePair.Value);
      }
      this.currentAutoSizeArrangeInfo = columnsData;
    }

    private bool HasVisibleColumn(GridViewColumnGroup group)
    {
      if (this.ShowEmptyGroups)
        return true;
      if (group.Groups.Count > 0)
      {
        foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
        {
          if (this.HasVisibleColumn(group1))
            return true;
        }
      }
      else
      {
        foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
        {
          foreach (string columnName in (Collection<string>) row.ColumnNames)
          {
            if (this.ColumnIsVisible((GridViewColumn) this.ViewTemplate.Columns[columnName]))
              return true;
          }
        }
      }
      return false;
    }
  }
}
