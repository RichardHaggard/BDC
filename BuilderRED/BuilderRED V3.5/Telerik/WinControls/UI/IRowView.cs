// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IRowView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface IRowView : IGridView
  {
    ReadOnlyCollection<IRowView> ChildViews { get; }

    GridCellElement CurrentCell { get; }

    GridRowElement CurrentRow { get; }

    Point CurrentCellAddress { get; }

    IList<GridRowElement> VisualRows { get; }

    int RowsPerPage { get; }

    int DisplayedRowCount(bool includePartialRow);

    int DisplayedColumnCount(bool includePartialColumn);

    GridRowElement GetRowElement(GridViewRowInfo rowInfo);

    GridCellElement GetCellElement(GridViewRowInfo rowInfo, GridViewColumn column);

    void InvalidateRow(GridViewRowInfo rowInfo);

    void InvalidateCell(GridViewRowInfo rowInfo, GridViewColumn column);

    bool EnsureRowVisible(GridViewRowInfo gridViewRowInfo);

    bool EnsureCellVisible(GridViewRowInfo rowInfo, GridViewColumn column);

    bool IsRowVisible(GridViewRowInfo value);

    bool IsCurrentView { get; }

    bool BeginUpdate();

    bool EndUpdate(bool performUpdate);

    bool EndUpdate();
  }
}
