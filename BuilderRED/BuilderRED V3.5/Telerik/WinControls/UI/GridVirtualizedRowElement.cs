// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridVirtualizedRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridVirtualizedRowElement : GridRowElement
  {
    private PinnedColumnsContainerElement leftPinnedColumns;
    private VirtualizedColumnContainer scrollableColumns;
    private PinnedColumnsContainerElement rightPinnedColumns;
    private int elementSpacing;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.leftPinnedColumns = this.CreateLeftPinnedColumnsContainer();
      this.leftPinnedColumns.Context = (GridRowElement) this;
      this.Children.Add((RadElement) this.leftPinnedColumns);
      this.scrollableColumns = this.CreateScrollableColumnsContainer();
      this.scrollableColumns.Context = (GridRowElement) this;
      this.Children.Add((RadElement) this.scrollableColumns);
      this.rightPinnedColumns = this.CreateRightPinnedColumnsContainer();
      this.rightPinnedColumns.Context = (GridRowElement) this;
      this.Children.Add((RadElement) this.rightPinnedColumns);
    }

    public override void InitializeRowView(GridTableElement tableElement)
    {
      base.InitializeRowView(tableElement);
      this.LeftPinnedColumns.ElementProvider = this.TableElement.ColumnScroller.ElementProvider;
      this.RightPinnedColumns.ElementProvider = this.TableElement.ColumnScroller.ElementProvider;
      this.ScrollableColumns.ElementProvider = this.TableElement.ColumnScroller.ElementProvider;
      this.ScrollableColumns.DataProvider = (IEnumerable) this.TableElement.ColumnScroller;
      this.LeftPinnedColumns.DataProvider = (IEnumerable) new PinnedColumnTraverser(this.TableElement.ViewElement.RowLayout.RenderColumns, PinnedColumnPosition.Left);
      this.RightPinnedColumns.DataProvider = (IEnumerable) new PinnedColumnTraverser(this.TableElement.ViewElement.RowLayout.RenderColumns, PinnedColumnPosition.Right);
      this.ElementSpacing = this.TableElement.CellSpacing;
    }

    public override void Detach()
    {
      base.Detach();
      this.DetachCells();
    }

    protected virtual void DetachCells()
    {
      while (this.VisualCells.Count > 0)
      {
        GridCellElement visualCell = this.VisualCells[0];
        visualCell.Parent.Children.Remove((RadElement) visualCell);
        GridVirtualizedCellElement virtualizedCellElement = visualCell as GridVirtualizedCellElement;
        if (virtualizedCellElement != null)
        {
          this.TableElement.CellElementProvider.CacheElement((IVirtualizedElement<GridViewColumn>) virtualizedCellElement);
          virtualizedCellElement.Detach();
        }
        else
          visualCell.Dispose();
      }
    }

    public int ElementSpacing
    {
      get
      {
        return this.elementSpacing;
      }
      set
      {
        if (this.elementSpacing != value)
        {
          this.elementSpacing = value;
          this.InvalidateMeasure();
        }
        this.leftPinnedColumns.ItemSpacing = value;
        this.scrollableColumns.ItemSpacing = value;
        this.rightPinnedColumns.ItemSpacing = value;
      }
    }

    public PinnedColumnsContainerElement LeftPinnedColumns
    {
      get
      {
        return this.leftPinnedColumns;
      }
    }

    public PinnedColumnsContainerElement RightPinnedColumns
    {
      get
      {
        return this.rightPinnedColumns;
      }
    }

    public VirtualizedColumnContainer ScrollableColumns
    {
      get
      {
        return this.scrollableColumns;
      }
    }

    public override void UpdateCells()
    {
      this.LeftPinnedColumns.UpdateItems();
      this.RightPinnedColumns.UpdateItems();
      this.ScrollableColumns.UpdateItems();
      foreach (GridVirtualizedCellElement visualCell in this.VisualCells)
        visualCell.Initialize(visualCell.ColumnInfo, (GridRowElement) this);
    }

    public GridCellElement GetCellElement(int columnIndex)
    {
      return this.GetCellElement(this.ViewTemplate.Columns[0].Name);
    }

    public GridCellElement GetCellElement(GridViewColumn column)
    {
      return this.GetCellElement(column.Name);
    }

    public GridCellElement GetCellElement(string name)
    {
      foreach (GridCellElement visualCell in this.VisualCells)
      {
        if (visualCell.Name == name)
          return visualCell;
      }
      return (GridCellElement) null;
    }

    protected virtual PinnedColumnsContainerElement CreateLeftPinnedColumnsContainer()
    {
      return new PinnedColumnsContainerElement();
    }

    protected virtual VirtualizedColumnContainer CreateScrollableColumnsContainer()
    {
      return new VirtualizedColumnContainer();
    }

    protected virtual PinnedColumnsContainerElement CreateRightPinnedColumnsContainer()
    {
      return new PinnedColumnsContainerElement();
    }

    protected override void WireEvents()
    {
      base.WireEvents();
      this.TableElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.TableElement_RadPropertyChanged);
    }

    protected override void UnwireEvents()
    {
      base.UnwireEvents();
      this.TableElement.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.TableElement_RadPropertyChanged);
    }

    private void TableElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != GridTableElement.CellSpacingProperty)
        return;
      this.ElementSpacing = (int) e.NewValue;
    }

    protected override SizeF MeasureElements(
      SizeF availableSize,
      SizeF clientSize,
      Padding borderThickness)
    {
      SizeF empty = SizeF.Empty;
      PinnedColumnsContainerElement containerElement1 = this.leftPinnedColumns;
      PinnedColumnsContainerElement containerElement2 = this.rightPinnedColumns;
      if (this.RightToLeft)
      {
        containerElement1 = this.rightPinnedColumns;
        containerElement2 = this.leftPinnedColumns;
      }
      containerElement1.Measure(availableSize);
      float num1 = 0.0f;
      if ((double) containerElement1.DesiredSize.Width > 0.0)
        num1 = containerElement1.DesiredSize.Width + (float) this.elementSpacing;
      availableSize.Width -= num1;
      empty.Height = containerElement1.DesiredSize.Height;
      empty.Width = num1;
      containerElement2.Measure(availableSize);
      float num2 = (double) containerElement2.DesiredSize.Width <= 0.0 ? 0.0f : containerElement2.DesiredSize.Width + (float) this.elementSpacing;
      availableSize.Width -= num2;
      empty.Height = Math.Max(empty.Height, containerElement2.DesiredSize.Height);
      empty.Width += num2;
      this.scrollableColumns.Measure(availableSize);
      empty.Height = Math.Max(empty.Height, this.scrollableColumns.DesiredSize.Height);
      empty.Width += this.scrollableColumns.DesiredSize.Width;
      if (float.IsInfinity(availableSize.Height))
        empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.Layout.Arrange(this.GetClientRectangle(finalSize));
      PinnedColumnsContainerElement containerElement1 = this.leftPinnedColumns;
      PinnedColumnsContainerElement containerElement2 = this.rightPinnedColumns;
      if (this.RightToLeft)
      {
        containerElement1 = this.rightPinnedColumns;
        containerElement2 = this.leftPinnedColumns;
      }
      float x1 = 0.0f;
      containerElement1.Arrange(new RectangleF(x1, 0.0f, containerElement1.DesiredSize.Width, finalSize.Height));
      if ((double) containerElement1.DesiredSize.Width > 0.0)
        x1 += containerElement1.DesiredSize.Width + (float) this.elementSpacing;
      this.scrollableColumns.Arrange(new RectangleF(x1, 0.0f, this.scrollableColumns.DesiredSize.Width, finalSize.Height));
      float x2 = x1 + this.scrollableColumns.DesiredSize.Width;
      if ((double) containerElement2.DesiredSize.Width > 0.0)
        x2 += (float) this.elementSpacing;
      containerElement2.Arrange(new RectangleF(x2, 0.0f, containerElement2.DesiredSize.Width, finalSize.Height));
      return finalSize;
    }
  }
}
