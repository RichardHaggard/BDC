// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TableViewDefinitionPrintRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TableViewDefinitionPrintRenderer : BaseGridPrintRenderer
  {
    private int currentPage = 1;
    private PrintPagesCollection currentPrintPage;
    private int currentColumnRange;
    private bool firstPage;
    private Size rowSize;
    private Dictionary<int, TableViewRowLayout> rowLayouts;

    public TableViewDefinitionPrintRenderer(RadGridView grid)
      : base(grid)
    {
      this.rowSize = Size.Empty;
      this.currentPrintPage = new PrintPagesCollection();
      this.rowLayouts = new Dictionary<int, TableViewRowLayout>();
    }

    public PrintPagesCollection PrintPages
    {
      get
      {
        return this.currentPrintPage;
      }
      set
      {
        this.currentPrintPage = value;
      }
    }

    public int CurrentPrintPage
    {
      get
      {
        return this.currentColumnRange;
      }
      set
      {
        this.currentColumnRange = value;
      }
    }

    protected virtual Size GetRowSize(GridViewRowInfo row, TableViewRowLayout rowLayout)
    {
      if (this.rowSize == Size.Empty)
      {
        int width = 0;
        int height = rowLayout.GetRowHeight(row) + this.GridView.TableElement.RowSpacing;
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
        {
          if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn) && rowLayout.LayoutImpl.GetArrangeInfo(renderColumn) != null)
          {
            int columnWidth = rowLayout.GetColumnWidth(renderColumn);
            if (width != 0)
              columnWidth += this.GridView.TableElement.CellSpacing;
            width += columnWidth;
          }
        }
        this.rowSize = new Size(width, height);
      }
      return this.rowSize;
    }

    protected virtual void PrintRowWideCell(
      GridViewRowInfo row,
      TableViewRowLayout rowLayout,
      GridPrintSettings settings,
      int currentX,
      int currentY,
      Graphics graphics)
    {
      int num1 = row.Group != null ? row.Group.Level + 1 : 0;
      int num2 = row.HierarchyLevel + 1 - num1;
      Size rowSize = this.GetRowSize(row, rowLayout);
      Rectangle rectangle = new Rectangle(currentX + num2 * settings.HierarchyIndent, currentY, rowSize.Width - num2 * settings.HierarchyIndent, rowSize.Height);
      CellPrintElement printCell = new CellPrintElement();
      if (row is GridViewGroupRowInfo)
      {
        if (this.PrintPages.Count > 0 && !settings.PrintHierarchy)
          rectangle.Width -= this.PrintPages[this.CurrentPrintPage].Count - 1;
        printCell = this.CreateGroupCellPrintElement(row as GridViewGroupRowInfo);
        if (printCell.Font != settings.GroupRowFont)
        {
          if (settings.GroupRowFont != null)
            printCell.Font = settings.GroupRowFont;
          else
            settings.GroupRowFont = printCell.Font;
        }
      }
      printCell.TextPadding = this.GridView.PrintStyle.CellPadding;
      printCell.RightToLeft = this.GridView.RightToLeft == RightToLeft.Yes;
      PrintCellFormattingEventArgs e = new PrintCellFormattingEventArgs(row, (GridViewColumn) null, printCell);
      this.OnPrintCellFormatting(e);
      e.PrintCell.Paint(graphics, rectangle);
      this.OnPrintCellPaint(new PrintCellPaintEventArgs(graphics, row, (GridViewColumn) null, rectangle));
    }

    protected virtual void PrintRow(
      GridViewRowInfo row,
      TableViewRowLayout rowLayout,
      GridPrintSettings settings,
      int currentX,
      int currentY,
      Graphics graphics)
    {
      this.PrintRow(row, rowLayout, settings, currentX, currentY, graphics, Rectangle.Empty);
    }

    protected virtual void PrintRow(
      GridViewRowInfo row,
      TableViewRowLayout rowLayout,
      GridPrintSettings settings,
      int currentX,
      int currentY,
      Graphics graphics,
      Rectangle drawArea)
    {
      List<GridViewColumn> gridViewColumnList = new List<GridViewColumn>();
      float num1 = 1f;
      if (this.PrintPages.Count > 0 && !settings.PrintHierarchy)
      {
        float num2 = 0.0f;
        foreach (GridViewColumn column in (List<GridViewColumn>) this.PrintPages[this.CurrentPrintPage])
        {
          if (!column.IsGrouped)
          {
            num2 += (float) rowLayout.GetColumnWidth(column);
            gridViewColumnList.Add(column);
          }
        }
        num1 = (float) drawArea.Width / num2;
      }
      else
      {
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
          gridViewColumnList.Add(renderColumn);
      }
      int num3 = row.Group != null ? row.Group.Level + 1 : 0;
      if (this.GridView.RightToLeft != RightToLeft.Yes)
        currentX += (row.HierarchyLevel - num3) * settings.HierarchyIndent;
      else
        currentX -= (row.HierarchyLevel - num3) * settings.HierarchyIndent;
      float num4 = 0.0f;
      float num5 = 0.0f;
      float num6 = 0.0f;
      bool flag = true;
      foreach (GridViewColumn gridViewColumn in gridViewColumnList)
      {
        if (!(gridViewColumn is GridViewRowHeaderColumn) && !(gridViewColumn is GridViewIndentColumn) && gridViewColumn.PinPosition == PinnedColumnPosition.Left)
        {
          num5 = (float) rowLayout.Owner.CellSpacing;
          num6 = (float) rowLayout.Owner.CellSpacing;
          break;
        }
      }
      foreach (GridViewColumn column in gridViewColumnList)
      {
        if (!(column is GridViewRowHeaderColumn) && !(column is GridViewIndentColumn))
        {
          TableViewCellArrangeInfo arrangeInfo = rowLayout.LayoutImpl.GetArrangeInfo(column);
          if (arrangeInfo != null)
          {
            float x = arrangeInfo.OffsetX < 0 ? 0.0f : (float) arrangeInfo.OffsetX;
            float y = (float) (int) arrangeInfo.Bounds.Y;
            float width = (float) arrangeInfo.CachedWidth;
            float height = (float) (this.GetDataRowHeight(row, (TableViewRowLayoutBase) rowLayout) + this.GridView.TableElement.RowSpacing);
            int num2 = flag ? 0 : rowLayout.Owner.CellSpacing;
            flag = false;
            if ((double) y + (double) height > (double) drawArea.Bottom)
              height = (float) drawArea.Height - y;
            if (column.PinPosition == PinnedColumnPosition.Left)
            {
              num5 += width + (float) num2;
              num6 += width + (float) num2;
            }
            else if (column.PinPosition == PinnedColumnPosition.None)
            {
              num6 += width + (float) num2;
              x += num5;
            }
            else
              x += num6;
            if (this.PrintPages.Count > 0 && !settings.PrintHierarchy)
            {
              x = num4;
              width = (float) Math.Ceiling((double) (width * num1));
            }
            if (this.GridView.RightToLeft == RightToLeft.Yes)
              x = (float) drawArea.Width - width - x;
            RectangleF rectangleF = new RectangleF(x, y, width, height);
            rectangleF.Offset((float) currentX, (float) currentY);
            CellPrintElement cellPrintElement;
            if (row is GridViewTableHeaderRowInfo)
            {
              GridViewCellInfo cell = this.GridView.MasterView.TableHeaderRow.Cells[column.Name];
              cellPrintElement = this.CreateHeaderCellPrintElement(column);
              if (cellPrintElement.Font != settings.HeaderCellFont)
              {
                if (settings.HeaderCellFont != null)
                  cellPrintElement.Font = settings.HeaderCellFont;
                else
                  settings.HeaderCellFont = cellPrintElement.Font;
              }
            }
            else if (row is GridViewSummaryRowInfo)
            {
              cellPrintElement = this.CreateSummaryCellPrintElement((row as GridViewSummaryRowInfo).Cells[column.Name]);
              if (cellPrintElement.Font != settings.SummaryCellFont)
              {
                if (settings.SummaryCellFont != null)
                  cellPrintElement.Font = settings.SummaryCellFont;
                else
                  settings.SummaryCellFont = cellPrintElement.Font;
              }
            }
            else
            {
              cellPrintElement = this.CreateDataCellPrintElement(row.Cells[column.Name]);
              if (cellPrintElement.Font != settings.CellFont)
              {
                if (settings.CellFont != null)
                  cellPrintElement.Font = settings.CellFont;
                else
                  settings.CellFont = cellPrintElement.Font;
              }
            }
            if (this.PrintPages.Count > 0 && !settings.PrintHierarchy)
              num4 += width;
            cellPrintElement.TextPadding = this.GridView.PrintStyle.CellPadding;
            cellPrintElement.RightToLeft = this.GridView.RightToLeft == RightToLeft.Yes;
            Rectangle rectangle = new Rectangle((int) rectangleF.X, (int) rectangleF.Y, (int) rectangleF.Width, (int) rectangleF.Height);
            PrintCellFormattingEventArgs e = new PrintCellFormattingEventArgs(row, column, cellPrintElement);
            this.OnPrintCellFormatting(e);
            e.PrintCell.Paint(graphics, rectangle);
            this.OnPrintCellPaint(new PrintCellPaintEventArgs(graphics, row, column, rectangle));
          }
        }
      }
    }

    public override void DrawPage(
      PrintGridTraverser traverser,
      Rectangle drawArea,
      Graphics graphics,
      GridPrintSettings settings,
      int pageNumber)
    {
      bool flag1 = this.currentPage != pageNumber;
      int height = drawArea.Height;
      int x = drawArea.X;
      int y = drawArea.Y;
      TableViewRowLayout rowLayout = this.GetRowLayout((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow, settings.FitWidthMode, settings.HierarchyIndent, drawArea);
      rowLayout.IgnoreColumnVisibility = settings.PrintHiddenColumns;
      int num1 = 0;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
      {
        if (renderColumn is GridViewRowHeaderColumn || renderColumn is GridViewIndentColumn)
          num1 += rowLayout.GetColumnWidth(renderColumn);
      }
      int num2 = (int) rowLayout.DesiredSize.Width - num1;
      if (settings.FitWidthMode == PrintFitWidthMode.NoFitCentered)
        x += (drawArea.Width - num2) / 2;
      if ((this.GridView.ShowColumnHeaders && this.firstPage && pageNumber == 1 || settings.PrintHeaderOnEachPage) && !settings.PrintHierarchy)
      {
        this.PrintRow((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow, rowLayout, settings, x, y, graphics, drawArea);
        int num3 = this.GetDataRowHeight((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow, (TableViewRowLayoutBase) rowLayout) + this.GridView.TableElement.RowSpacing;
        y += num3;
        height -= num3;
      }
      this.firstPage = false;
      int num4 = y;
      GridViewRowInfo row = (GridViewRowInfo) null;
      if (this.PrintPages.Count > 0 && !settings.PrintHierarchy)
        row = traverser.Current;
      bool flag2 = true;
      while (traverser.MoveNext())
      {
        if (traverser.Current is GridViewDataRowInfo || traverser.Current is GridViewGroupRowInfo || traverser.Current is GridViewSummaryRowInfo || traverser.Current is GridViewTableHeaderRowInfo && settings.PrintHierarchy)
        {
          GridViewHierarchyRowInfo current = traverser.Current as GridViewHierarchyRowInfo;
          if (current != null && current.Views.Count > 0)
          {
            switch (settings.ChildViewPrintMode)
            {
              case ChildViewPrintMode.PrintFirstView:
                current.ActiveView = current.Views[0];
                break;
              case ChildViewPrintMode.SelectViewToPrint:
                ChildViewPrintingEventArgs e = new ChildViewPrintingEventArgs(current.Views.IndexOf(current.ActiveView), current);
                this.OnChildViewPrinting(e);
                current.ActiveView = current.Views[e.ActiveViewIndex];
                break;
            }
          }
          rowLayout = this.GetRowLayout(traverser.Current, settings.FitWidthMode, settings.HierarchyIndent, drawArea);
          int num3 = !(traverser.Current is GridViewGroupRowInfo) ? this.GetDataRowHeight(traverser.Current, (TableViewRowLayoutBase) rowLayout) : this.GetRowSize(traverser.Current, rowLayout).Height;
          if ((y + num3 >= drawArea.Bottom || num4 + num3 >= drawArea.Bottom) && !flag2)
          {
            traverser.MovePrevious();
            num4 = y;
            bool flag3 = this.currentPage != pageNumber;
            ++this.currentPage;
            if (!flag3)
              break;
          }
          else
          {
            if (traverser.Current is GridViewGroupRowInfo)
            {
              if (settings.PrintGrouping)
              {
                if (this.currentPage == pageNumber)
                {
                  this.PrintRowWideCell(traverser.Current, rowLayout, settings, x, y, graphics);
                  y += num3 + this.GridView.TableElement.RowSpacing;
                }
                else
                  num4 += num3 + this.GridView.TableElement.RowSpacing;
              }
            }
            else if (!(traverser.Current is GridViewSummaryRowInfo) || settings.PrintSummaries)
            {
              if (this.currentPage == pageNumber)
              {
                this.PrintRow(traverser.Current, rowLayout, settings, x, y, graphics, drawArea);
                y += num3 + this.GridView.TableElement.RowSpacing;
              }
              else
                num4 += num3 + this.GridView.TableElement.RowSpacing;
            }
            else
              continue;
            if (height < num3 && flag2)
            {
              ++this.currentPage;
              break;
            }
            flag2 = false;
          }
        }
      }
      if (this.PrintPages.Count <= 0 || settings.PrintHierarchy)
        return;
      if (y + this.GetDataRowHeight(traverser.Current, (TableViewRowLayoutBase) rowLayout) < drawArea.Bottom || num4 + this.GetDataRowHeight(traverser.Current, (TableViewRowLayoutBase) rowLayout) < drawArea.Bottom)
        ++this.currentPage;
      ++this.CurrentPrintPage;
      this.CurrentPrintPage %= this.PrintPages.Count;
      if (this.CurrentPrintPage <= 0)
        return;
      if (row == null)
        traverser.Reset();
      else
        traverser.GoToRow(row);
    }

    public override void Reset()
    {
      this.currentPage = 1;
      this.firstPage = true;
      this.rowSize = Size.Empty;
      this.rowLayouts = new Dictionary<int, TableViewRowLayout>();
    }

    public override System.Type ViewDefinitionType
    {
      get
      {
        return typeof (TableViewDefinition);
      }
    }

    private TableViewRowLayout GetRowLayout(
      GridViewRowInfo row,
      PrintFitWidthMode fitWidthMode,
      int hierarchyIndent,
      Rectangle drawArea)
    {
      int key = row.ViewTemplate.GetHashCode() + row.HierarchyLevel;
      if (this.rowLayouts.ContainsKey(key))
        return this.rowLayouts[key];
      GridTableElement viewUiElement = row.ViewTemplate.ViewDefinition.CreateViewUIElement(row.ViewInfo) as GridTableElement;
      viewUiElement.Initialize(this.GridView.GridViewElement, row.ViewInfo);
      viewUiElement.RowHeight = this.GridView.TableElement.RowHeight;
      viewUiElement.TableHeaderHeight = this.GridView.TableElement.TableHeaderHeight;
      viewUiElement.GroupHeaderHeight = this.GridView.TableElement.GroupHeaderHeight;
      this.GridView.ElementTree.ApplyThemeToElement((RadObject) viewUiElement);
      TableViewRowLayout tableViewRowLayout = new TableViewRowLayout();
      tableViewRowLayout.Context = GridLayoutContext.Printer;
      tableViewRowLayout.Initialize(viewUiElement);
      int num1 = 0;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) tableViewRowLayout.RenderColumns)
      {
        if (renderColumn is GridViewRowHeaderColumn || renderColumn is GridViewIndentColumn)
          num1 += tableViewRowLayout.GetColumnWidth(renderColumn);
      }
      this.GridView.BeginUpdate();
      GridViewAutoSizeColumnsMode autoSizeColumnsMode = tableViewRowLayout.ViewTemplate.AutoSizeColumnsMode;
      if (fitWidthMode == PrintFitWidthMode.FitPageWidth)
      {
        ColumnsState state = this.SaveColumnsState(tableViewRowLayout.ViewTemplate);
        tableViewRowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        int num2 = row.Group != null ? row.Group.Level + 1 : 0;
        tableViewRowLayout.MeasureRow(new SizeF((float) (drawArea.Width + num1 - (row.HierarchyLevel - num2) * hierarchyIndent), (float) drawArea.Height));
        this.RestoreColumnsState(state);
      }
      else
      {
        tableViewRowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
        tableViewRowLayout.MeasureRow(new SizeF((float) this.GridView.Width, (float) this.GridView.Height));
      }
      tableViewRowLayout.ViewTemplate.AutoSizeColumnsMode = autoSizeColumnsMode;
      this.GridView.EndUpdate(false);
      this.rowLayouts.Add(key, tableViewRowLayout);
      return tableViewRowLayout;
    }

    protected virtual ColumnsState SaveColumnsState(GridViewTemplate template)
    {
      ColumnsState columnsState = new ColumnsState();
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) template.Columns)
      {
        columnsState.AllowResizeState[column] = column.AllowResize;
        column.AllowResize = true;
      }
      return columnsState;
    }

    protected virtual void RestoreColumnsState(ColumnsState state)
    {
      foreach (GridViewColumn key in state.AllowResizeState.Keys)
        key.AllowResize = state.AllowResizeState[key];
    }
  }
}
