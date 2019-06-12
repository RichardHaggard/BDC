// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IGridNavigator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public interface IGridNavigator
  {
    RadGridViewElement GridViewElement { get; }

    void Initialize(RadGridViewElement element);

    void BeginSelection(GridNavigationContext context);

    void EndSelection();

    bool Select(GridViewRowInfo row, GridViewColumn column);

    bool SelectFirstRow();

    bool SelectLastRow();

    bool SelectRow(GridViewRowInfo row);

    bool SelectNextRow(int step);

    bool SelectPreviousRow(int step);

    bool IsFirstRow(GridViewRowInfo row);

    bool IsLastRow(GridViewRowInfo row);

    bool SelectFirstColumn();

    bool SelectLastColumn();

    bool SelectNextColumn();

    bool SelectPreviousColumn();

    bool IsFirstColumn(GridViewColumn column);

    bool IsLastColumn(GridViewColumn column);

    bool IsFirstEditableColumn(GridViewColumn column);

    bool IsLastEditableColumn(GridViewColumn column);

    bool DeleteSelectedRows();

    void ClearSelection();

    void SelectAll();
  }
}
