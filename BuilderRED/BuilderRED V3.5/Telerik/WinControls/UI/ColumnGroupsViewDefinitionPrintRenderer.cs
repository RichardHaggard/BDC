// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupsViewDefinitionPrintRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ColumnGroupsViewDefinitionPrintRenderer : BaseGridPrintRenderer
  {
    private int currentPage = 1;
    private bool firstPage;
    private Size rowSize;

    public ColumnGroupsViewDefinitionPrintRenderer(RadGridView grid)
      : base(grid)
    {
    }

    protected virtual Size GetRowSize(GridViewRowInfo row, ColumnGroupRowLayout rowLayout)
    {
      if (this.rowSize == Size.Empty)
      {
        int width1 = 0;
        int height = rowLayout.GetRowHeight(row) + this.GridView.TableElement.RowSpacing;
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
        {
          if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
          {
            ColumnGroupsCellArrangeInfo columnData = rowLayout.GetColumnData(renderColumn);
            if (columnData != null)
            {
              int width2 = (int) columnData.Bounds.Width;
              if ((double) width1 < (double) columnData.Bounds.X + (double) width2)
                width1 = (int) columnData.Bounds.X + width2;
            }
          }
        }
        this.rowSize = new Size(width1, height);
      }
      return this.rowSize;
    }

    protected virtual void PrintRowWideCell(
      GridViewRowInfo row,
      ColumnGroupRowLayout rowLayout,
      GridPrintSettings settings,
      int currentX,
      int currentY,
      Graphics graphics)
    {
      Size rowSize = this.GetRowSize(row, rowLayout);
      Rectangle rectangle = new Rectangle(new Point(currentX, currentY), rowSize);
      CellPrintElement printCell = new CellPrintElement();
      if (row is GridViewGroupRowInfo)
      {
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
      ColumnGroupRowLayout rowLayout,
      GridPrintSettings settings,
      int currentX,
      int currentY,
      Graphics graphics)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
      {
        if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
        {
          float rowHeight = (float) rowLayout.GetRowHeight(row);
          RectangleF correctedColumnBounds = rowLayout.GetCorrectedColumnBounds(row, renderColumn, this.GridView.RightToLeft == RightToLeft.Yes, new RectangleF(0.0f, 0.0f, rowLayout.DesiredSize.Width, rowHeight));
          if (!(correctedColumnBounds == RectangleF.Empty))
          {
            if (renderColumn.PinPosition == PinnedColumnPosition.Left)
            {
              if ((double) num1 < (double) correctedColumnBounds.Right + (double) rowLayout.Owner.CellSpacing)
              {
                num1 = correctedColumnBounds.Right + (float) rowLayout.Owner.CellSpacing;
                num2 = num1;
              }
            }
            else if (renderColumn.PinPosition == PinnedColumnPosition.None)
            {
              if ((double) num2 < (double) num1 + (double) correctedColumnBounds.Right + (double) rowLayout.Owner.CellSpacing)
                num2 = num1 + correctedColumnBounds.Right + (float) rowLayout.Owner.CellSpacing;
              correctedColumnBounds.X += num1;
            }
            else
              correctedColumnBounds.X += num2;
            correctedColumnBounds.Offset((float) currentX, (float) currentY);
            CellPrintElement cellPrintElement;
            if (row is GridViewTableHeaderRowInfo)
            {
              GridViewCellInfo cell = this.GridView.MasterView.TableHeaderRow.Cells[renderColumn.Name];
              cellPrintElement = this.CreateHeaderCellPrintElement(renderColumn);
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
              GridViewCellInfo cell = (row as GridViewSummaryRowInfo).Cells[renderColumn.Name];
              if (cell != null)
              {
                cellPrintElement = this.CreateSummaryCellPrintElement(cell);
                if (cellPrintElement.Font != settings.SummaryCellFont)
                {
                  if (settings.SummaryCellFont != null)
                    cellPrintElement.Font = settings.SummaryCellFont;
                  else
                    settings.SummaryCellFont = cellPrintElement.Font;
                }
              }
              else
                continue;
            }
            else
            {
              GridViewCellInfo cell = row.Cells[renderColumn.Name];
              if (cell != null)
              {
                if (renderColumn is GridViewImageColumn)
                {
                  cellPrintElement = this.CreateImageCellPrintElement(cell);
                }
                else
                {
                  cellPrintElement = this.CreateDataCellPrintElement(cell);
                  if (cellPrintElement.Font != settings.CellFont)
                  {
                    if (settings.CellFont != null)
                      cellPrintElement.Font = settings.CellFont;
                    else
                      settings.CellFont = cellPrintElement.Font;
                  }
                }
              }
              else
                continue;
            }
            cellPrintElement.TextPadding = this.GridView.PrintStyle.CellPadding;
            cellPrintElement.RightToLeft = this.GridView.RightToLeft == RightToLeft.Yes;
            Rectangle rectangle = new Rectangle((int) correctedColumnBounds.X, (int) correctedColumnBounds.Y, (int) correctedColumnBounds.Width, (int) correctedColumnBounds.Height);
            PrintCellFormattingEventArgs e = new PrintCellFormattingEventArgs(row, renderColumn, cellPrintElement);
            this.OnPrintCellFormatting(e);
            e.PrintCell.Paint(graphics, rectangle);
            this.OnPrintCellPaint(new PrintCellPaintEventArgs(graphics, row, renderColumn, rectangle));
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
      int x = drawArea.X;
      int y = drawArea.Y;
      int height = drawArea.Height;
      ColumnGroupRowLayout rowLayout = new ColumnGroupRowLayout(this.GridView.ViewDefinition as ColumnGroupsViewDefinition);
      rowLayout.IgnoreColumnVisibility = settings.PrintHiddenColumns;
      rowLayout.Context = GridLayoutContext.Printer;
      rowLayout.Initialize(this.GridView.TableElement);
      int num1 = 0;
      foreach (GridViewColumn systemColumn in rowLayout.SystemColumns)
        num1 += rowLayout.GetColumnWidth(systemColumn);
      if (settings.FitWidthMode == PrintFitWidthMode.FitPageWidth)
      {
        this.GridView.BeginUpdate();
        GridViewAutoSizeColumnsMode autoSizeColumnsMode = rowLayout.ViewTemplate.AutoSizeColumnsMode;
        rowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        rowLayout.MeasureRow(new SizeF((float) (drawArea.Width + num1), (float) drawArea.Height));
        rowLayout.ViewTemplate.AutoSizeColumnsMode = autoSizeColumnsMode;
        this.GridView.EndUpdate(false);
      }
      else
      {
        this.GridView.BeginUpdate();
        GridViewAutoSizeColumnsMode autoSizeColumnsMode = rowLayout.ViewTemplate.AutoSizeColumnsMode;
        rowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
        rowLayout.MeasureRow(new SizeF((float) this.GridView.Width, (float) this.GridView.Height));
        rowLayout.ViewTemplate.AutoSizeColumnsMode = autoSizeColumnsMode;
        this.GridView.EndUpdate(false);
      }
      int num2 = (int) rowLayout.DesiredSize.Width - num1;
      if (settings.FitWidthMode == PrintFitWidthMode.NoFitCentered)
        x += (drawArea.Width - num2) / 2;
      if (this.firstPage && pageNumber == 1 || settings.PrintHeaderOnEachPage)
      {
        this.firstPage = false;
        this.PrintRow((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow, rowLayout, settings, x, y, graphics);
        int rowHeight = rowLayout.GetRowHeight((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow);
        y += rowHeight;
        height -= rowHeight;
      }
      int num3 = y;
      bool flag2 = true;
      while (traverser.MoveNext())
      {
        int num4 = rowLayout.GetRowHeight(traverser.Current) + this.GridView.TableElement.RowSpacing;
        if (traverser.Current is GridViewGroupRowInfo || traverser.Current is GridViewDataRowInfo || traverser.Current is GridViewSummaryRowInfo)
        {
          if ((y + num4 >= drawArea.Bottom || num3 + num4 >= drawArea.Bottom) && !flag2)
          {
            traverser.MovePrevious();
            num3 = y;
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
                  y += num4;
                }
                else
                  num3 += num4;
              }
            }
            else if (!(traverser.Current is GridViewSummaryRowInfo) || settings.PrintSummaries)
            {
              if (this.currentPage == pageNumber)
              {
                this.PrintRow(traverser.Current, rowLayout, settings, x, y, graphics);
                y += num4;
              }
              else
                num3 += num4;
            }
            else
              continue;
            if (height < num4 && flag2)
            {
              ++this.currentPage;
              break;
            }
            flag2 = false;
          }
        }
      }
    }

    public override void Reset()
    {
      this.currentPage = 1;
      this.firstPage = true;
      this.rowSize = Size.Empty;
    }

    public override System.Type ViewDefinitionType
    {
      get
      {
        return typeof (ColumnGroupsViewDefinition);
      }
    }
  }
}
