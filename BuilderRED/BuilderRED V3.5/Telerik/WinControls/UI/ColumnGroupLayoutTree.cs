// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupLayoutTree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class ColumnGroupLayoutTree
  {
    private Dictionary<GridViewColumn, ColumnGroupColumnData> systemColumns = new Dictionary<GridViewColumn, ColumnGroupColumnData>();
    private ColumnGroupLayoutNode root;
    private GridViewTemplate template;
    private ColumnGroupRowLayout owner;
    private float groupRowsTotalHeight;
    private float columnRowsTotalHeight;
    private List<float> groupRowsHeights;
    private List<float> columnRowsHeights;
    private int groupRowLevels;
    internal bool widthChangesSuspended;
    private ColumnGroupLayoutNode resizedNode;
    private GridViewColumn resizedColumn;
    private SizeF previousAvailableSize;

    public float GroupRowsTotalHeight
    {
      get
      {
        return this.groupRowsTotalHeight;
      }
    }

    public float ColumnRowsTotalHeight
    {
      get
      {
        return this.columnRowsTotalHeight;
      }
    }

    public ColumnGroupLayoutTree(ColumnGroupRowLayout owner, GridViewTemplate template)
    {
      this.template = template;
      this.owner = owner;
    }

    public void Rebuild(ColumnGroupCollection rootGroups)
    {
      this.root = new ColumnGroupLayoutNode();
      this.root.Level = 0;
      List<GridViewColumnGroup> gridViewColumnGroupList = new List<GridViewColumnGroup>();
      int index = 0;
      int num = 0;
      foreach (GridViewColumnGroup rootGroup in (Collection<GridViewColumnGroup>) rootGroups)
      {
        if (rootGroup.IsVisible)
        {
          if (rootGroup.PinPosition == PinnedColumnPosition.Left)
          {
            gridViewColumnGroupList.Insert(index, rootGroup);
            ++index;
          }
          else if (rootGroup.PinPosition == PinnedColumnPosition.None)
          {
            gridViewColumnGroupList.Insert(index + num, rootGroup);
            ++num;
          }
          else
            gridViewColumnGroupList.Add(rootGroup);
        }
      }
      foreach (GridViewColumnGroup group in gridViewColumnGroupList)
      {
        if (group.IsVisible)
        {
          ColumnGroupLayoutNode node = new ColumnGroupLayoutNode(group);
          this.AddGroupsRecursively(node, group);
          this.root.Children.Add(node);
          node.Parent = this.root;
        }
      }
      this.systemColumns.Clear();
      foreach (GridViewColumn systemColumn in this.owner.SystemColumns)
        this.systemColumns.Add(systemColumn, new ColumnGroupColumnData());
      this.RebuildLayout(this.root);
      this.ResetOriginalBounds(this.root);
    }

    private void RebuildLayout(ColumnGroupLayoutNode node)
    {
      node.MinWidth = node.MaxWidth = node.Bounds.Width = node.OriginalWidth = 0.0f;
      foreach (ColumnGroupLayoutNode child in node.Children)
      {
        child.Level = node.Level + 1;
        this.RebuildLayout(child);
        node.MinWidth += child.MinWidth;
        if ((double) node.MaxWidth >= 0.0)
          node.MaxWidth += child.MaxWidth;
        if ((double) child.MaxWidth == 0.0)
          node.MaxWidth = -1f;
        node.Bounds.Width += child.Bounds.Width;
      }
      if ((double) node.MaxWidth == -1.0)
        node.MaxWidth = 0.0f;
      if (node.Rows != null && node.Children.Count == 0)
      {
        int num1 = 0;
        node.MaxWidth = (float) int.MaxValue;
        bool flag = false;
        foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) node.Rows)
        {
          int num2 = 0;
          int num3 = 0;
          int num4 = 0;
          foreach (string columnName in (Collection<string>) row.ColumnNames)
          {
            GridViewColumn column = (GridViewColumn) this.template.Columns[columnName];
            if (this.owner.ColumnIsVisible(column) || this.owner.IgnoreColumnVisibility)
            {
              flag = true;
              num2 += column.Width;
              num3 += column.MinWidth;
              if (num4 >= 0)
                num4 += column.MaxWidth;
              if (column.MaxWidth == 0)
                num4 = -1;
              ColumnGroupColumnData columnGroupColumnData = new ColumnGroupColumnData();
              columnGroupColumnData.Row = num1;
              columnGroupColumnData.Bounds.Width = (float) column.Width;
              columnGroupColumnData.OriginalWidth = (float) column.Width;
              columnGroupColumnData.MinWidth = (float) column.MinWidth;
              columnGroupColumnData.MaxWidth = (float) column.MaxWidth;
              columnGroupColumnData.ConstrainWidth();
              node.ColumnData.Add(column, columnGroupColumnData);
            }
          }
          node.Bounds.Width = Math.Max(node.Bounds.Width, (float) num2);
          node.MinWidth = Math.Max(node.MinWidth, (float) num3);
          node.MaxWidth = num4 != -1 ? Math.Min(node.MaxWidth, (float) num4) : node.MaxWidth;
          node.ConstrainWidth();
          ++num1;
        }
        node.MaxWidth = (double) node.MaxWidth == 2147483648.0 ? 0.0f : node.MaxWidth;
        if (!flag && this.owner.ShowEmptyGroups)
          node.Bounds.Width = 50f;
      }
      else
      {
        if (node.Children.Count == 0 && (node.Rows == null || node.Rows.Count == 0) && this.owner.ShowEmptyGroups)
          node.Bounds.Width = 50f;
        this.groupRowLevels = Math.Max(this.groupRowLevels, node.Level);
      }
      node.OriginalWidth = node.Bounds.Width;
    }

    private void AddGroupsRecursively(ColumnGroupLayoutNode node, GridViewColumnGroup group)
    {
      foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
      {
        if (group1.IsVisible)
        {
          ColumnGroupLayoutNode node1 = new ColumnGroupLayoutNode(group1);
          this.AddGroupsRecursively(node1, group1);
          node.Children.Add(node1);
          node1.Parent = node;
        }
      }
      if (group.Groups.Count > 0 || group.Rows.Count <= 0)
        return;
      node.Children.Add(new ColumnGroupLayoutNode(group.Rows)
      {
        Parent = node
      });
    }

    public SizeF MeasureRow(SizeF availableSize)
    {
      this.previousAvailableSize = availableSize;
      float x1 = 0.0f;
      float x2 = 0.0f;
      float x3 = 0.0f;
      List<ColumnGroupLayoutNode> columnGroupLayoutNodeList = new List<ColumnGroupLayoutNode>();
      float num1 = 0.0f;
      this.groupRowsHeights = new List<float>();
      this.columnRowsHeights = new List<float>();
      bool changesSuspended = this.widthChangesSuspended;
      this.widthChangesSuspended = true;
      foreach (KeyValuePair<GridViewColumn, ColumnGroupColumnData> systemColumn in this.systemColumns)
      {
        float num2 = (float) Math.Max(systemColumn.Key.MinWidth, (int) this.owner.Owner.ColumnScroller.ElementProvider.GetElementSize(systemColumn.Key).Width);
        if (systemColumn.Key.MaxWidth > 0)
          num2 = Math.Min((float) systemColumn.Key.MaxWidth, num2);
        systemColumn.Value.Bounds = new RectangleF(x1, 0.0f, num2, 0.0f);
        x1 += systemColumn.Value.Bounds.Width;
      }
      this.widthChangesSuspended = changesSuspended;
      foreach (ColumnGroupLayoutNode child in this.root.Children)
      {
        if (child.Group.PinPosition == PinnedColumnPosition.Left)
          x1 += this.ArrangeGroupNode(child, new PointF(x1, 0.0f), child.Bounds.Width);
        else if (child.Group.PinPosition == PinnedColumnPosition.Right)
        {
          x2 += this.ArrangeGroupNode(child, new PointF(x2, 0.0f), child.Bounds.Width);
        }
        else
        {
          columnGroupLayoutNodeList.Add(child);
          num1 += child.OriginalWidth;
        }
      }
      availableSize.Width -= x1 + x2;
      foreach (ColumnGroupLayoutNode node in columnGroupLayoutNodeList)
      {
        if (this.template.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill && node != this.resizedNode)
          node.Bounds.Width = node.GetConstrainedWidth(node.OriginalWidth * (availableSize.Width - x3) / num1);
        x3 += this.ArrangeGroupNode(node, new PointF(x3, 0.0f), node.Bounds.Width);
        num1 -= node.OriginalWidth;
      }
      float num3 = availableSize.Width - x3;
      if (this.template.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill)
      {
        x3 = 0.0f;
        foreach (ColumnGroupLayoutNode node in columnGroupLayoutNodeList)
        {
          if (node != this.resizedNode)
          {
            float width = node.Bounds.Width;
            node.Bounds.Width = node.GetConstrainedWidth(node.Bounds.Width + num3);
            num3 -= node.Bounds.Width - width;
          }
          x3 += this.ArrangeGroupNode(node, new PointF(x3, 0.0f), node.Bounds.Width);
        }
      }
      this.groupRowsTotalHeight = 0.0f;
      this.columnRowsTotalHeight = 0.0f;
      foreach (float groupRowsHeight in this.groupRowsHeights)
        this.groupRowsTotalHeight += groupRowsHeight;
      foreach (float columnRowsHeight in this.columnRowsHeights)
        this.columnRowsTotalHeight += columnRowsHeight;
      foreach (KeyValuePair<GridViewColumn, ColumnGroupColumnData> systemColumn in this.systemColumns)
        systemColumn.Value.Bounds.Height = this.groupRowsTotalHeight + this.columnRowsTotalHeight;
      foreach (ColumnGroupLayoutNode child in this.root.Children)
        this.SpanColumnsVertically(child, 0.0f);
      return new SizeF(x1 + x3 + x2, this.groupRowsTotalHeight + this.columnRowsTotalHeight);
    }

    private float ArrangeGroupNode(ColumnGroupLayoutNode node, PointF location, float width)
    {
      node.Bounds.Width = width;
      node.Bounds.Location = location;
      if (node.Children.Count != 0)
      {
        node.Bounds = new RectangleF(location, new SizeF(width, (float) node.Group.RowSpan));
        float num1 = 0.0f;
        float num2 = 0.0f;
        float num3 = 0.0f;
        foreach (ColumnGroupLayoutNode child in node.Children)
          num1 += child.OriginalWidth;
        foreach (ColumnGroupLayoutNode child in node.Children)
        {
          if (child != this.resizedNode)
            child.Bounds.Width = child.GetConstrainedWidth(child.OriginalWidth * (node.Bounds.Width - num3) / num1);
          num3 += child.Bounds.Width;
          num1 -= child.OriginalWidth;
        }
        float num4 = width - num3;
        foreach (ColumnGroupLayoutNode child in node.Children)
        {
          if (child != this.resizedNode)
          {
            float width1 = child.Bounds.Width;
            child.Bounds.Width = child.GetConstrainedWidth(child.Bounds.Width + num4);
            num4 -= child.Bounds.Width - width1;
          }
        }
        foreach (ColumnGroupLayoutNode child in node.Children)
          num2 += this.ArrangeGroupNode(child, new PointF(location.X + num2, location.Y + (float) node.Group.RowSpan), child.Bounds.Width);
      }
      else if (node.Rows != null)
      {
        for (int index = 0; index < node.Rows.Count; ++index)
        {
          SizeF sizeF = this.ArrangeGroupRow(node.Rows[index], node, location, node.Bounds.Width);
          location.Y += sizeF.Height;
          node.Bounds.Width = node.GetConstrainedWidth(Math.Max(node.Bounds.Width, sizeF.Width));
          while (this.columnRowsHeights.Count <= index)
            this.columnRowsHeights.Add(0.0f);
          this.columnRowsHeights[index] = Math.Max(this.columnRowsHeights[index], sizeF.Height);
        }
      }
      if (node.Group != null)
      {
        while (this.groupRowsHeights.Count <= node.Level)
          this.groupRowsHeights.Add(0.0f);
        this.groupRowsHeights[node.Level] = Math.Max(this.groupRowsHeights[node.Level], (float) node.Group.RowSpan);
      }
      return node.Bounds.Width;
    }

    private SizeF ArrangeGroupRow(
      GridViewColumnGroupRow row,
      ColumnGroupLayoutNode node,
      PointF location,
      float availableWidth)
    {
      float num1 = 0.0f;
      foreach (string columnName in (Collection<string>) row.ColumnNames)
      {
        GridViewColumn column = (GridViewColumn) this.template.Columns[columnName];
        if (this.owner.ColumnIsVisible(column) || this.owner.IgnoreColumnVisibility)
        {
          ColumnGroupColumnData columnGroupColumnData = node.ColumnData[column];
          num1 += columnGroupColumnData.OriginalWidth;
        }
      }
      float num2 = 0.0f;
      float num3 = (float) row.MinHeight;
      foreach (string columnName in (Collection<string>) row.ColumnNames)
      {
        GridViewColumn column = (GridViewColumn) this.template.Columns[columnName];
        if (this.owner.ColumnIsVisible(column) || this.owner.IgnoreColumnVisibility)
        {
          ColumnGroupColumnData columnGroupColumnData = node.ColumnData[column];
          if (this.resizedColumn != column)
            columnGroupColumnData.Bounds.Width = columnGroupColumnData.GetConstrainedWidth(columnGroupColumnData.OriginalWidth * availableWidth / num1);
          columnGroupColumnData.Bounds.Location = new PointF(location.X + num2, location.Y);
          columnGroupColumnData.Bounds.Height = (float) column.RowSpan;
          num2 += columnGroupColumnData.Bounds.Width;
          num3 = Math.Max(num3, columnGroupColumnData.Bounds.Height);
          num1 -= columnGroupColumnData.OriginalWidth;
          availableWidth -= columnGroupColumnData.Bounds.Width;
          if (column is GridViewDataColumn)
            this.SetColumnWidthInternal(column, (int) columnGroupColumnData.Bounds.Width);
        }
      }
      float width1 = 0.0f;
      foreach (string columnName in (Collection<string>) row.ColumnNames)
      {
        GridViewColumn column = (GridViewColumn) this.template.Columns[columnName];
        if (this.owner.ColumnIsVisible(column) || this.owner.IgnoreColumnVisibility)
        {
          ColumnGroupColumnData columnGroupColumnData = node.ColumnData[column];
          if (this.resizedColumn != column)
          {
            float width2 = columnGroupColumnData.Bounds.Width;
            columnGroupColumnData.Bounds.Width = columnGroupColumnData.GetConstrainedWidth(columnGroupColumnData.Bounds.Width + availableWidth);
            availableWidth -= columnGroupColumnData.Bounds.Width - width2;
          }
          columnGroupColumnData.Bounds.Location = new PointF(location.X + width1, location.Y);
          width1 += columnGroupColumnData.Bounds.Width;
          if (column is GridViewDataColumn)
          {
            column.SuspendPropertyNotifications();
            this.SetColumnWidthInternal(column, (int) columnGroupColumnData.Bounds.Width);
            column.ResumePropertyNotifications();
          }
        }
      }
      return new SizeF(width1, num3);
    }

    private void SpanColumnsVertically(ColumnGroupLayoutNode current, float y)
    {
      if (current.Group != null)
      {
        current.Bounds.Y = y;
        current.Bounds.Height = this.groupRowsHeights[current.Level];
        if (current.Children.Count == 1 && current.Children[0].Group == null)
          current.Bounds.Height = this.groupRowsTotalHeight - y;
      }
      foreach (ColumnGroupLayoutNode child in current.Children)
        this.SpanColumnsVertically(child, y + current.Bounds.Height);
      if (current.Children.Count != 0 || current.Rows == null || current.Rows.Count <= 0)
        return;
      float num = 0.0f;
      for (int index = 0; index < current.Rows.Count; ++index)
      {
        foreach (string columnName in (Collection<string>) current.Rows[index].ColumnNames)
        {
          GridViewColumn column = (GridViewColumn) this.template.Columns[columnName];
          if (this.owner.ColumnIsVisible(column) || this.owner.IgnoreColumnVisibility)
          {
            ColumnGroupColumnData columnGroupColumnData = current.ColumnData[column];
            columnGroupColumnData.Bounds.Y = y + num;
            columnGroupColumnData.Bounds.Height = index == current.Rows.Count - 1 ? this.columnRowsTotalHeight + this.groupRowsTotalHeight - columnGroupColumnData.Bounds.Y : this.columnRowsHeights[index];
          }
        }
        num += this.columnRowsHeights[index];
      }
    }

    public void StartColumnResize(GridViewColumn column)
    {
      ColumnGroupLayoutNode node;
      ColumnGroupColumnData data;
      this.FindColumnNode(column, out node, out data);
      if (node != null && data == null)
      {
        ColumnGroupLayoutNode parent = node.Parent;
        ColumnGroupLayoutNode columnGroupLayoutNode;
        for (columnGroupLayoutNode = node; parent != null && this.IsLastGroupInRow(parent.Children, columnGroupLayoutNode); parent = parent.Parent)
          columnGroupLayoutNode = parent;
        if (columnGroupLayoutNode != this.root)
        {
          if (this.IsLastUnpinned(columnGroupLayoutNode))
          {
            this.resizedNode = (ColumnGroupLayoutNode) null;
          }
          else
          {
            this.resizedNode = columnGroupLayoutNode;
            this.resizedNode.OriginalWidth = this.resizedNode.Bounds.Width;
          }
        }
        else
        {
          if (this.template.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill || this.root.Children.Count <= 0)
            return;
          this.resizedNode = this.root.Children[this.root.Children.Count - 1];
          this.resizedNode.OriginalWidth = this.resizedNode.Bounds.Width;
        }
      }
      else
      {
        if (node == null || data == null)
          return;
        if (this.IsLastColumnInRow(node.Rows[data.Row], column))
        {
          ColumnGroupLayoutNode parent = node.Parent;
          ColumnGroupLayoutNode columnGroupLayoutNode;
          for (columnGroupLayoutNode = node; parent != null && this.IsLastGroupInRow(parent.Children, columnGroupLayoutNode); parent = parent.Parent)
            columnGroupLayoutNode = parent;
          if (columnGroupLayoutNode == this.root)
            return;
          if (this.IsLastUnpinned(columnGroupLayoutNode))
          {
            this.resizedNode = (ColumnGroupLayoutNode) null;
          }
          else
          {
            this.resizedNode = columnGroupLayoutNode;
            this.resizedNode.OriginalWidth = this.resizedNode.Bounds.Width;
          }
        }
        else
        {
          this.resizedNode = node;
          this.resizedColumn = column;
          this.resizedNode.ColumnData[this.resizedColumn].OriginalWidth = this.resizedNode.ColumnData[this.resizedColumn].Bounds.Width;
        }
      }
    }

    public void EndColumnResize()
    {
      this.resizedNode = (ColumnGroupLayoutNode) null;
      this.resizedColumn = (GridViewColumn) null;
      this.ResetOriginalBounds(this.root);
    }

    public void ResizeColumn(int delta)
    {
      if (this.resizedColumn == null && this.resizedNode != null)
      {
        float constrainedWidth = this.resizedNode.GetConstrainedWidth(this.resizedNode.OriginalWidth + (float) delta);
        float num1 = this.resizedNode.Bounds.Width - constrainedWidth;
        float num2 = 0.0f;
        float num3 = 0.0f;
        float num4 = 0.0f;
        for (int index = this.resizedNode.Parent.Children.IndexOf(this.resizedNode) + 1; index < this.resizedNode.Parent.Children.Count; ++index)
        {
          num2 += this.resizedNode.Parent.Children[index].MinWidth;
          if ((double) num3 >= 0.0)
            num3 += this.resizedNode.Parent.Children[index].MaxWidth;
          if ((double) this.resizedNode.Parent.Children[index].MaxWidth == 0.0)
            num3 = -1f;
          num4 += this.resizedNode.Parent.Children[index].Bounds.Width;
        }
        if ((double) num4 + (double) num1 < (double) num2)
          num1 = num2 - num4;
        if ((double) num3 > 0.0 && (double) num4 + (double) num1 > (double) num3)
          num1 = num3 - num4;
        if (this.resizedNode.Parent == this.root && this.template.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.None)
          this.resizedNode.Bounds.Width = constrainedWidth;
        else
          this.resizedNode.Bounds.Width = this.resizedNode.GetConstrainedWidth(this.resizedNode.Bounds.Width - num1);
      }
      else
      {
        if (this.resizedColumn == null || this.resizedNode == null)
          return;
        ColumnGroupColumnData columnGroupColumnData = this.resizedNode.ColumnData[this.resizedColumn];
        float constrainedWidth1 = columnGroupColumnData.GetConstrainedWidth(columnGroupColumnData.OriginalWidth + (float) delta);
        float num1 = columnGroupColumnData.Bounds.Width - constrainedWidth1;
        float num2 = 0.0f;
        float num3 = 0.0f;
        float num4 = 0.0f;
        for (int index = this.resizedNode.Rows[columnGroupColumnData.Row].ColumnNames.IndexOf(this.resizedColumn.Name) + 1; index < this.resizedNode.Rows[columnGroupColumnData.Row].ColumnNames.Count; ++index)
        {
          GridViewColumn column = (GridViewColumn) this.template.Columns[this.resizedNode.Rows[columnGroupColumnData.Row].ColumnNames[index]];
          if (this.owner.ColumnIsVisible(column) || this.owner.IgnoreColumnVisibility)
          {
            num2 += this.resizedNode.ColumnData[column].MinWidth;
            if ((double) num3 >= 0.0)
              num3 += this.resizedNode.ColumnData[column].MaxWidth;
            if ((double) this.resizedNode.ColumnData[column].MaxWidth == 0.0)
              num3 = -1f;
            num4 += this.resizedNode.ColumnData[column].Bounds.Width;
          }
        }
        if ((double) num4 + (double) num1 < (double) num2)
          num1 = num2 - num4;
        if ((double) num3 > 0.0 && (double) num4 + (double) num1 > (double) num3)
          num1 = num3 - num4;
        float constrainedWidth2 = columnGroupColumnData.GetConstrainedWidth(columnGroupColumnData.Bounds.Width - num1);
        columnGroupColumnData.Bounds.Width = constrainedWidth2;
      }
    }

    public void SetBestFitWidth(GridViewColumn column, float width)
    {
      ColumnGroupLayoutNode node;
      ColumnGroupColumnData data;
      this.FindColumnNode(column, out node, out data);
      if (node == null || data == null)
        return;
      this.SetColumnWidthInternal(column, (int) width);
      data.Bounds.Width = data.GetConstrainedWidth(width);
      data.OriginalWidth = data.Bounds.Width;
      float num1 = 0.0f;
      foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) node.Rows)
      {
        float val2 = 0.0f;
        foreach (string columnName in (Collection<string>) row.ColumnNames)
        {
          if (this.owner.ColumnIsVisible((GridViewColumn) this.template.Columns[columnName]) || this.owner.IgnoreColumnVisibility)
            val2 += node.ColumnData[(GridViewColumn) this.template.Columns[columnName]].Bounds.Width;
        }
        num1 = Math.Max(num1, val2);
      }
      float num2 = node.GetConstrainedWidth(num1) - node.Bounds.Width;
      for (; node != this.root; node = node.Parent)
      {
        node.Bounds.Width += num2;
        node.OriginalWidth = node.Bounds.Width;
        if (node.Parent == this.root)
          break;
      }
      if (this.template.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill)
      {
        int num3 = this.root.Children.IndexOf(node);
        List<ColumnGroupLayoutNode> range = this.root.Children.GetRange(num3 + 1, this.root.Children.Count - num3 - 1);
        float num4 = -num2;
        for (int index = 0; index < num3; ++index)
          range.Add(this.root.Children[index]);
        if (range.Count > 0)
        {
          foreach (ColumnGroupLayoutNode columnGroupLayoutNode in range)
          {
            float num5 = columnGroupLayoutNode.GetConstrainedWidth(columnGroupLayoutNode.Bounds.Width + num4 / (float) range.Count) - columnGroupLayoutNode.Bounds.Width;
            columnGroupLayoutNode.Bounds.Width += num5;
            columnGroupLayoutNode.OriginalWidth = columnGroupLayoutNode.Bounds.Width;
            num4 -= num5;
          }
          foreach (ColumnGroupLayoutNode columnGroupLayoutNode in range)
          {
            float num5 = columnGroupLayoutNode.GetConstrainedWidth(columnGroupLayoutNode.Bounds.Width + num4) - columnGroupLayoutNode.Bounds.Width;
            columnGroupLayoutNode.Bounds.Width += num5;
            columnGroupLayoutNode.OriginalWidth = columnGroupLayoutNode.Bounds.Width;
            num4 -= num5;
          }
        }
      }
      this.resizedNode = node;
      this.MeasureRow(this.previousAvailableSize);
      this.resizedNode = (ColumnGroupLayoutNode) null;
      this.ResetOriginalBounds(this.root);
    }

    private void ResetOriginalBounds(ColumnGroupLayoutNode node)
    {
      node.OriginalWidth = node.Bounds.Width;
      foreach (ColumnGroupLayoutNode child in node.Children)
        this.ResetOriginalBounds(child);
      foreach (KeyValuePair<GridViewColumn, ColumnGroupColumnData> keyValuePair in node.ColumnData)
      {
        keyValuePair.Value.OriginalWidth = keyValuePair.Value.Bounds.Width;
        this.SetColumnWidthInternal(keyValuePair.Key, (int) keyValuePair.Value.Bounds.Width);
      }
    }

    private void SetColumnWidthInternal(GridViewColumn column, int width)
    {
      if (!this.owner.IsResizeInProgress)
        column.SuspendPropertyNotifications();
      this.widthChangesSuspended = true;
      column.Width = (int) Math.Round((double) width / (double) column.DpiScale.Width);
      this.widthChangesSuspended = false;
      if (this.owner.IsResizeInProgress)
        return;
      column.ResumePropertyNotifications();
    }

    public float GetGroupRowHeight(int rowIndex)
    {
      if (this.groupRowsHeights == null || rowIndex < 0 || rowIndex > this.groupRowsHeights.Count)
        return 0.0f;
      return this.groupRowsHeights[rowIndex];
    }

    public float GetColumnRowHeight(int rowIndex)
    {
      if (this.columnRowsHeights == null || rowIndex < 0 || rowIndex > this.columnRowsHeights.Count)
        return 0.0f;
      return this.columnRowsHeights[rowIndex];
    }

    public ColumnGroupsCellArrangeInfo GetColumnData(
      GridViewColumn column)
    {
      if (this.systemColumns.ContainsKey(column))
      {
        ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = new ColumnGroupsCellArrangeInfo(column);
        groupsCellArrangeInfo.Bounds = this.systemColumns[column].Bounds;
        return groupsCellArrangeInfo;
      }
      ColumnGroupLayoutNode node;
      ColumnGroupColumnData data;
      this.FindColumnNode(column, out node, out data);
      if (data == null && node != null)
      {
        ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = new ColumnGroupsCellArrangeInfo(column);
        groupsCellArrangeInfo.Group = node.Parent.Group;
        groupsCellArrangeInfo.Bounds = node.Bounds;
        groupsCellArrangeInfo.Depth = node.Level;
        return groupsCellArrangeInfo;
      }
      if (data == null || node == null)
        return (ColumnGroupsCellArrangeInfo) null;
      ColumnGroupsCellArrangeInfo groupsCellArrangeInfo1 = new ColumnGroupsCellArrangeInfo(column);
      groupsCellArrangeInfo1.Group = node.Parent.Group;
      groupsCellArrangeInfo1.Bounds = node.ColumnData[column].Bounds;
      groupsCellArrangeInfo1.RowIndex = node.ColumnData[column].Row;
      groupsCellArrangeInfo1.Depth = groupsCellArrangeInfo1.RowIndex + this.groupRowLevels + 1;
      groupsCellArrangeInfo1.Row = groupsCellArrangeInfo1.Group.Rows == null || groupsCellArrangeInfo1.Group.Rows.Count <= groupsCellArrangeInfo1.RowIndex ? (GridViewColumnGroupRow) null : groupsCellArrangeInfo1.Group.Rows[groupsCellArrangeInfo1.RowIndex];
      return groupsCellArrangeInfo1;
    }

    private bool IsLastColumnInRow(GridViewColumnGroupRow row, GridViewColumn column)
    {
      int num1 = 0;
      int num2 = -1;
      foreach (string columnName in (Collection<string>) row.ColumnNames)
      {
        GridViewColumn column1 = (GridViewColumn) this.template.Columns[columnName];
        if (this.owner.ColumnIsVisible(column1) || this.owner.IgnoreColumnVisibility)
        {
          if (column1 == column)
            num2 = num1;
          ++num1;
        }
      }
      return num2 == num1 - 1;
    }

    private bool IsLastGroupInRow(List<ColumnGroupLayoutNode> list, ColumnGroupLayoutNode groupNode)
    {
      int num1 = 0;
      int num2 = -1;
      foreach (ColumnGroupLayoutNode columnGroupLayoutNode in list)
      {
        if (columnGroupLayoutNode.Group == null || columnGroupLayoutNode.Group.IsVisible)
        {
          if (columnGroupLayoutNode == groupNode)
            num2 = num1;
          ++num1;
        }
      }
      return num2 == num1 - 1;
    }

    private bool IsLastUnpinned(ColumnGroupLayoutNode currentChild)
    {
      int num = currentChild.Parent.Children.IndexOf(currentChild);
      ColumnGroupLayoutNode columnGroupLayoutNode = currentChild.Parent.Children.Count > num + 1 ? currentChild.Parent.Children[num + 1] : (ColumnGroupLayoutNode) null;
      return currentChild.Group != null && currentChild.Group.PinPosition == PinnedColumnPosition.None && (columnGroupLayoutNode != null && columnGroupLayoutNode.Group != null) && (columnGroupLayoutNode.Group.PinPosition == PinnedColumnPosition.Right && this.template.AutoSizeColumnsMode == GridViewAutoSizeColumnsMode.Fill);
    }

    private void FindColumnNode(
      GridViewColumn column,
      out ColumnGroupLayoutNode node,
      out ColumnGroupColumnData data)
    {
      Queue<ColumnGroupLayoutNode> columnGroupLayoutNodeQueue = new Queue<ColumnGroupLayoutNode>();
      GridViewGroupColumn gridViewGroupColumn = column as GridViewGroupColumn;
      columnGroupLayoutNodeQueue.Enqueue(this.root);
      while (columnGroupLayoutNodeQueue.Count > 0)
      {
        foreach (ColumnGroupLayoutNode child in columnGroupLayoutNodeQueue.Dequeue().Children)
        {
          if (gridViewGroupColumn != null && gridViewGroupColumn.Group == child.Group && child.Group != null)
          {
            node = child;
            data = (ColumnGroupColumnData) null;
            return;
          }
          if (child.ColumnData.ContainsKey(column))
          {
            node = child;
            data = child.ColumnData[column];
            return;
          }
          columnGroupLayoutNodeQueue.Enqueue(child);
        }
      }
      node = (ColumnGroupLayoutNode) null;
      data = (ColumnGroupColumnData) null;
    }

    public Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> GetColumnsData()
    {
      Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> dictionary1 = new Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo>();
      Dictionary<GridViewColumnGroup, GridViewColumn> dictionary2 = new Dictionary<GridViewColumnGroup, GridViewColumn>();
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.owner.RenderColumns)
      {
        if (renderColumn is GridViewGroupColumn)
          dictionary2.Add(((GridViewGroupColumn) renderColumn).Group, renderColumn);
      }
      Queue<ColumnGroupLayoutNode> columnGroupLayoutNodeQueue = new Queue<ColumnGroupLayoutNode>();
      columnGroupLayoutNodeQueue.Enqueue(this.root);
      while (columnGroupLayoutNodeQueue.Count > 0)
      {
        ColumnGroupLayoutNode columnGroupLayoutNode = columnGroupLayoutNodeQueue.Dequeue();
        if (columnGroupLayoutNode != this.root)
        {
          ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = new ColumnGroupsCellArrangeInfo(dictionary2[columnGroupLayoutNode.Group]);
          groupsCellArrangeInfo.Group = columnGroupLayoutNode.Parent.Group;
          groupsCellArrangeInfo.Bounds = columnGroupLayoutNode.Bounds;
          groupsCellArrangeInfo.Depth = columnGroupLayoutNode.Level;
          dictionary1.Add(dictionary2[columnGroupLayoutNode.Group], groupsCellArrangeInfo);
        }
        foreach (ColumnGroupLayoutNode child in columnGroupLayoutNode.Children)
        {
          if (child.Rows != null)
          {
            foreach (KeyValuePair<GridViewColumn, ColumnGroupColumnData> keyValuePair in child.ColumnData)
            {
              ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = new ColumnGroupsCellArrangeInfo(keyValuePair.Key);
              groupsCellArrangeInfo.Group = columnGroupLayoutNode.Group;
              groupsCellArrangeInfo.Bounds = keyValuePair.Value.Bounds;
              groupsCellArrangeInfo.RowIndex = keyValuePair.Value.Row;
              groupsCellArrangeInfo.Depth = groupsCellArrangeInfo.RowIndex + this.groupRowLevels + 1;
              groupsCellArrangeInfo.Row = groupsCellArrangeInfo.Group.Rows == null || groupsCellArrangeInfo.Group.Rows.Count <= groupsCellArrangeInfo.RowIndex ? (GridViewColumnGroupRow) null : groupsCellArrangeInfo.Group.Rows[groupsCellArrangeInfo.RowIndex];
              dictionary1.Add(keyValuePair.Key, groupsCellArrangeInfo);
            }
          }
          else
            columnGroupLayoutNodeQueue.Enqueue(child);
        }
      }
      return dictionary1;
    }

    public Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> GetSystemColumnsData()
    {
      Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo> dictionary = new Dictionary<GridViewColumn, ColumnGroupsCellArrangeInfo>();
      foreach (KeyValuePair<GridViewColumn, ColumnGroupColumnData> systemColumn in this.systemColumns)
      {
        ColumnGroupsCellArrangeInfo groupsCellArrangeInfo = new ColumnGroupsCellArrangeInfo(systemColumn.Key);
        dictionary.Add(systemColumn.Key, groupsCellArrangeInfo);
      }
      return dictionary;
    }
  }
}
