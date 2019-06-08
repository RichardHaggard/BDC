// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollableVirtualCellsContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ScrollableVirtualCellsContainer : VirtualizedStackContainer<int>
  {
    private VirtualGridRowElement rowElement;
    private SizeF availableSize;
    private SizeF desiredSize;

    public VirtualGridRowElement RowElement
    {
      get
      {
        return this.rowElement;
      }
      internal set
      {
        this.rowElement = value;
      }
    }

    protected override object GetElementContext()
    {
      return (object) this.rowElement;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Orientation = Orientation.Horizontal;
    }

    protected override void InitializeOffset()
    {
      if (this.Orientation == Orientation.Vertical)
      {
        this.offset = this.ScrollOffset.Height + this.ArtificialOffset;
        this.availableSize.Height -= this.offset;
      }
      else
      {
        this.offset = this.ScrollOffset.Width;
        this.availableSize.Width -= this.offset;
      }
    }

    protected override bool IsItemVisible(int data)
    {
      int num1 = 0;
      if (this.RowElement.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
      {
        for (int itemIndex = 0; itemIndex < this.RowElement.TableElement.ColumnScroller.Scrollbar.Value; ++itemIndex)
          num1 += this.RowElement.TableElement.ColumnScroller.GetScrollHeight(this.RowElement.TableElement.ViewInfo.ColumnsViewState.GetItemSize(itemIndex));
      }
      else
        num1 = this.RowElement.TableElement.HScrollBar.Value;
      RectangleF rectangleF = new RectangleF((float) num1, 0.0f, this.availableSize.Width, this.availableSize.Height);
      int num2 = 0;
      foreach (int topPinnedItem in this.RowElement.ViewInfo.ColumnsViewState.TopPinnedItems)
      {
        if (data > topPinnedItem)
          num2 += this.RowElement.ViewInfo.GetColumnWidth(topPinnedItem) + this.RowElement.ViewInfo.CellSpacing;
      }
      int num3 = this.RowElement.ViewInfo.ColumnsViewState.GetItemOffset(data) - num2;
      int itemSize = this.RowElement.ViewInfo.ColumnsViewState.GetItemSize(data);
      if ((double) (num3 + itemSize) > (double) rectangleF.X)
        return (double) num3 < (double) rectangleF.Right;
      return false;
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.availableSize = availableSize;
      this.desiredSize = SizeF.Empty;
      return base.BeginMeasure(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.InitializeOffset();
      RectangleF rectangleF = new RectangleF(PointF.Empty, finalSize);
      float num1 = rectangleF.X;
      float y = rectangleF.Y;
      if (this.RowElement.RightToLeft)
        num1 = rectangleF.Right;
      foreach (VirtualGridCellElement child in this.Children)
      {
        float columnWidth = (float) this.RowElement.TableElement.GetColumnWidth(child.ColumnIndex);
        RectangleF arrangeRect;
        if (this.RowElement.RightToLeft)
        {
          float num2 = num1 - columnWidth;
          arrangeRect = new RectangleF(num2 - this.ScrollOffset.Width, y, columnWidth, rectangleF.Height);
          num1 = num2 - (float) this.RowElement.TableElement.CellSpacing;
        }
        else
        {
          arrangeRect = new RectangleF(num1 + this.ScrollOffset.Width, y, columnWidth, rectangleF.Height);
          num1 += columnWidth + (float) this.RowElement.TableElement.CellSpacing;
        }
        this.ArrangeElementCore((RadElement) child, finalSize, arrangeRect);
      }
      return finalSize;
    }

    protected override SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      int num = 0;
      if (this.RowElement.TableElement.ColumnScroller.ScrollMode == ItemScrollerScrollModes.Discrete)
      {
        for (int itemIndex = 0; itemIndex < this.RowElement.TableElement.ColumnScroller.Scrollbar.Value; ++itemIndex)
          num += this.RowElement.TableElement.ColumnScroller.GetScrollHeight(this.RowElement.TableElement.ViewInfo.ColumnsViewState.GetItemSize(itemIndex));
      }
      else
        num = this.RowElement.TableElement.HScrollBar.Value;
      VirtualGridCellElement virtualGridCellElement = element as VirtualGridCellElement;
      int itemOffset = this.RowElement.ViewInfo.ColumnsViewState.GetItemOffset(virtualGridCellElement.Data);
      int itemSize = this.RowElement.ViewInfo.ColumnsViewState.GetItemSize(virtualGridCellElement.Data);
      element.Measure(availableSize);
      if (itemOffset + itemSize > num && itemOffset < num)
        return new SizeF((float) (itemOffset + itemSize - num), element.DesiredSize.Height);
      return element.DesiredSize;
    }
  }
}
