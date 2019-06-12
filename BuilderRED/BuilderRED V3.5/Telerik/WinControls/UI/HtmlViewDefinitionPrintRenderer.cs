// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HtmlViewDefinitionPrintRenderer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class HtmlViewDefinitionPrintRenderer : BaseGridPrintRenderer
  {
    private int currentPage = 1;
    private bool firstPage;
    private Size rowSize;

    public HtmlViewDefinitionPrintRenderer(RadGridView grid)
      : base(grid)
    {
    }

    protected virtual Size GetRowSize(GridViewRowInfo row, HtmlViewRowLayout rowLayout)
    {
      if (this.rowSize == Size.Empty)
      {
        int width1 = 0;
        int height = rowLayout.GetRowHeight(row) + this.GridView.TableElement.RowSpacing;
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
        {
          if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
          {
            HtmlViewCellArrangeInfo arrangeInfo = rowLayout.GetArrangeInfo(renderColumn);
            if (arrangeInfo != null)
            {
              int width2 = (int) arrangeInfo.Bounds.Width;
              if ((double) width1 < (double) arrangeInfo.Bounds.X + (double) width2)
                width1 = (int) arrangeInfo.Bounds.X + width2;
            }
          }
        }
        this.rowSize = new Size(width1, height);
      }
      return this.rowSize;
    }

    protected virtual void PrintRowWideCell(
      GridViewRowInfo row,
      HtmlViewRowLayout rowLayout,
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
      HtmlViewRowLayout rowLayout,
      GridPrintSettings settings,
      int currentX,
      int currentY,
      Graphics graphics)
    {
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
      {
        if (!(renderColumn is GridViewRowHeaderColumn) && !(renderColumn is GridViewIndentColumn))
        {
          HtmlViewCellArrangeInfo arrangeInfo = rowLayout.GetArrangeInfo(renderColumn);
          if (arrangeInfo != null)
          {
            RectangleF bounds = arrangeInfo.Bounds;
            bounds.Offset((float) currentX, (float) currentY);
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
              cellPrintElement = this.CreateSummaryCellPrintElement((row as GridViewSummaryRowInfo).Cells[renderColumn.Name]);
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
              GridViewCellInfo cell = row.Cells[renderColumn.Name];
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
            cellPrintElement.TextPadding = this.GridView.PrintStyle.CellPadding;
            cellPrintElement.RightToLeft = this.GridView.RightToLeft == RightToLeft.Yes;
            Rectangle rectangle = new Rectangle((int) bounds.X, (int) bounds.Y, (int) bounds.Width, (int) bounds.Height);
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
      HtmlViewRowLayout rowLayout = new HtmlViewRowLayout(this.GridView.ViewDefinition as HtmlViewDefinition);
      rowLayout.IgnoreColumnVisibility = settings.PrintHiddenColumns;
      rowLayout.Context = GridLayoutContext.Printer;
      rowLayout.Initialize(this.GridView.TableElement);
      int num1 = 0;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) rowLayout.RenderColumns)
      {
        if (renderColumn is GridViewRowHeaderColumn || renderColumn is GridViewIndentColumn)
          num1 += rowLayout.GetColumnWidth(renderColumn);
      }
      if (settings.FitWidthMode == PrintFitWidthMode.FitPageWidth)
      {
        this.GridView.BeginUpdate();
        GridViewAutoSizeColumnsMode autoSizeColumnsMode = rowLayout.ViewTemplate.AutoSizeColumnsMode;
        rowLayout.ViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        rowLayout.MeasureRow(new SizeF((float) drawArea.Width, (float) drawArea.Height));
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
        int num3 = rowLayout.GetRowHeight((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow) + this.GridView.TableElement.RowSpacing;
        y += num3;
        height -= num3;
      }
      int num4 = y;
      bool flag2 = true;
      while (traverser.MoveNext())
      {
        int rowHeight = rowLayout.GetRowHeight(traverser.Current);
        if (traverser.Current is GridViewGroupRowInfo || traverser.Current is GridViewDataRowInfo || traverser.Current is GridViewSummaryRowInfo)
        {
          if ((y + rowHeight >= drawArea.Bottom || num4 + rowHeight >= drawArea.Bottom) && !flag2)
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
                  y += rowHeight + this.GridView.TableElement.RowSpacing;
                }
                else
                  num4 += rowHeight + this.GridView.TableElement.RowSpacing;
              }
            }
            else if (!(traverser.Current is GridViewSummaryRowInfo) || settings.PrintSummaries)
            {
              if (this.currentPage == pageNumber)
              {
                this.PrintRow(traverser.Current, rowLayout, settings, x, y, graphics);
                y += rowHeight + this.GridView.TableElement.RowSpacing;
              }
              else
                num4 += rowHeight + this.GridView.TableElement.RowSpacing;
            }
            else
              continue;
            if (height < rowHeight && flag2)
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
        return typeof (HtmlViewDefinition);
      }
    }
  }
}
