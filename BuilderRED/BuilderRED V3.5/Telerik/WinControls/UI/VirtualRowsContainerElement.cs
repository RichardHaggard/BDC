// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualRowsContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class VirtualRowsContainerElement : LayoutPanel
  {
    private int elementSpacing = -1;
    private ScrollableVirtualRowsContainer scrollableRows;
    private StackLayoutElement topPinnedRows;
    private StackLayoutElement bottomPinnedRows;
    private VirtualGridTableElement tableElement;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.scrollableRows = new ScrollableVirtualRowsContainer();
      this.Children.Add((RadElement) this.scrollableRows);
      this.topPinnedRows = new StackLayoutElement();
      this.topPinnedRows.Orientation = Orientation.Vertical;
      this.topPinnedRows.ElementSpacing = -1;
      this.Children.Add((RadElement) this.topPinnedRows);
      this.bottomPinnedRows = new StackLayoutElement();
      this.bottomPinnedRows.Orientation = Orientation.Vertical;
      this.bottomPinnedRows.ElementSpacing = -1;
      this.Children.Add((RadElement) this.bottomPinnedRows);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      SizeF availableSize1 = availableSize;
      float parentRowOffset = this.tableElement.ParentRowOffset;
      this.topPinnedRows.Measure(availableSize1);
      float height1 = this.topPinnedRows.DesiredSize.Height;
      if ((double) this.topPinnedRows.DesiredSize.Height > 0.0)
        height1 += (float) this.elementSpacing;
      availableSize1.Height -= height1;
      float num = Math.Max(0.0f, parentRowOffset - height1);
      empty.Width = this.topPinnedRows.DesiredSize.Width;
      empty.Height = height1;
      this.bottomPinnedRows.Measure(availableSize1);
      float height2 = this.bottomPinnedRows.DesiredSize.Height;
      if ((double) this.bottomPinnedRows.DesiredSize.Height > 0.0)
        height2 += (float) this.elementSpacing;
      availableSize1.Height -= height2;
      empty.Width = Math.Max(empty.Width, this.bottomPinnedRows.DesiredSize.Width);
      empty.Height += height2;
      this.scrollableRows.TopOffset = num;
      this.scrollableRows.Measure(availableSize1);
      empty.Width = Math.Max(empty.Width, this.scrollableRows.DesiredSize.Width);
      empty.Height += this.scrollableRows.DesiredSize.Height;
      empty.Width = Math.Min(empty.Width, availableSize.Width);
      empty.Height = Math.Min(empty.Height, availableSize.Height);
      SizeF sizeF = new SizeF(this.scrollableRows.DesiredSize.Width, this.scrollableRows.DesiredSize.Height + (float) this.TableElement.RowSpacing);
      if ((double) this.bottomPinnedRows.DesiredSize.Height > 0.0)
        availableSize1.Height += (float) this.elementSpacing;
      this.TableElement.RowScroller.ClientSize = availableSize1;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      float y1 = 0.0f;
      this.topPinnedRows.Arrange(new RectangleF(0.0f, y1, finalSize.Width, this.topPinnedRows.DesiredSize.Height));
      float y2 = y1 + (this.topPinnedRows.DesiredSize.Height + (float) this.elementSpacing);
      this.scrollableRows.Arrange(new RectangleF(0.0f, y2, finalSize.Width, this.scrollableRows.DesiredSize.Height));
      this.bottomPinnedRows.Arrange(new RectangleF(0.0f, y2 + (this.scrollableRows.DesiredSize.Height + (float) this.elementSpacing), finalSize.Width, this.bottomPinnedRows.DesiredSize.Height));
      return finalSize;
    }

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
      set
      {
        this.tableElement = value;
        this.scrollableRows.TableElement = this.tableElement;
        this.scrollableRows.DataProvider = (IEnumerable) this.tableElement.RowScroller;
        this.scrollableRows.ElementProvider = this.tableElement.RowScroller.ElementProvider;
        this.UpdateElementSpacing();
      }
    }

    public void UpdateElementSpacing()
    {
      if (this.TableElement == null)
        return;
      this.elementSpacing = this.TableElement.RowSpacing;
      this.scrollableRows.ItemSpacing = this.elementSpacing;
      this.topPinnedRows.ElementSpacing = this.elementSpacing;
      this.bottomPinnedRows.ElementSpacing = this.elementSpacing;
    }

    public ScrollableVirtualRowsContainer ScrollableRows
    {
      get
      {
        return this.scrollableRows;
      }
    }

    public StackLayoutElement TopPinnedRows
    {
      get
      {
        return this.topPinnedRows;
      }
    }

    public StackLayoutElement BottomPinnedRows
    {
      get
      {
        return this.bottomPinnedRows;
      }
    }

    public IEnumerable<VirtualGridRowElement> GetRowElements()
    {
      foreach (VirtualGridRowElement child in this.TopPinnedRows.Children)
        yield return child;
      foreach (VirtualGridRowElement child in this.ScrollableRows.Children)
        yield return child;
      foreach (VirtualGridRowElement child in this.BottomPinnedRows.Children)
        yield return child;
    }

    public VirtualGridRowElement GetRowElement(int rowIndex)
    {
      foreach (VirtualGridRowElement rowElement in this.GetRowElements())
      {
        if (rowElement.RowIndex == rowIndex)
          return rowElement;
      }
      return (VirtualGridRowElement) null;
    }
  }
}
