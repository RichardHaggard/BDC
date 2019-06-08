// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IGridRowLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface IGridRowLayout : IDisposable
  {
    GridTableElement Owner { get; }

    SizeF DesiredSize { get; }

    SizeF GroupRowDesiredSize { get; }

    IList<GridViewColumn> RenderColumns { get; }

    IList<GridViewColumn> ScrollableColumns { get; }

    GridViewDataColumn FirstDataColumn { get; }

    GridViewDataColumn LastDataColumn { get; }

    void Initialize(GridTableElement tableElement);

    SizeF MeasureRow(SizeF availableSize);

    RectangleF ArrangeCell(RectangleF clientRect, GridCellElement cell);

    void StartColumnResize(GridViewColumn column);

    void EndColumnResize();

    void ResizeColumn(int delta);

    void InvalidateRenderColumns();

    void InvalidateLayout();

    int GetRowHeight(GridViewRowInfo rowInfo);

    SizeF MeasurePinnedColumns(PinnedColumnTraverser dataProvider);

    void EnsureColumnsLayout();
  }
}
