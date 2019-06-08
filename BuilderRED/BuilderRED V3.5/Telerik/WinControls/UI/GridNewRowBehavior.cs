// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridNewRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridNewRowBehavior : GridRowBehavior
  {
    protected override bool ProcessEscapeKey(KeyEventArgs keys)
    {
      if (this.GridViewElement.IsInEditMode)
        return base.ProcessEscapeKey(keys);
      GridViewNewRowInfo currentRow = (GridViewNewRowInfo) this.GridViewElement.CurrentRow;
      currentRow.CancelAddNewRow();
      if (currentRow.ViewTemplate.DataView.CurrentItem != null)
        currentRow.ViewTemplate.DataView.CurrentItem.IsCurrent = true;
      if (currentRow.ViewInfo != null && currentRow.ViewInfo.ChildRows.Count == 0)
        currentRow.IsCurrent = false;
      return true;
    }

    protected override bool ProcessEnterKey(KeyEventArgs keys)
    {
      bool isInEditMode = this.GridViewElement.IsInEditMode;
      if (!isInEditMode && this.GridViewElement.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnEnter)
        return this.GridViewElement.BeginEdit();
      if (isInEditMode)
      {
        RadTextBoxEditor activeEditor = this.EditorManager.ActiveEditor as RadTextBoxEditor;
        if (activeEditor != null && activeEditor.AcceptsReturn)
          return false;
      }
      if (this.GridViewElement.NewRowEnterKeyMode == RadGridViewNewRowEnterKeyMode.None)
      {
        if (isInEditMode)
          this.GridViewElement.CloseEditor();
        return true;
      }
      GridViewNewRowInfo currentRow1 = (GridViewNewRowInfo) this.GridViewElement.CurrentRow;
      if (this.GridViewElement.NewRowEnterKeyMode == RadGridViewNewRowEnterKeyMode.EnterMovesToNextCell)
      {
        bool flag = !this.IsInEditMode;
        if (isInEditMode)
        {
          if (this.IsOnLastCell())
          {
            currentRow1?.DeferUserAddedRow();
            flag = this.GridViewElement.EndEdit();
            if (currentRow1.RowPosition == SystemRowPosition.Bottom)
            {
              this.GridViewElement.UpdateLayout();
              currentRow1.EnsureVisible();
              this.Navigator.SelectFirstColumn();
              flag = false;
            }
          }
          else
            flag = this.GridViewElement.CloseEditor();
        }
        if (flag)
          this.Navigator.SelectNextColumn();
        currentRow1?.RaiseUserAddedRow();
        if (isInEditMode && this.GridViewElement.CurrentRow is GridViewNewRowInfo && this.GridViewElement.BeginEditMode != RadGridViewBeginEditMode.BeginEditProgrammatically)
          return this.GridViewElement.BeginEdit();
        return false;
      }
      if (this.GridViewElement.NewRowEnterKeyMode == RadGridViewNewRowEnterKeyMode.EnterMovesToLastAddedRow)
        currentRow1.MoveToLastRow = true;
      if (!isInEditMode)
      {
        RowValidatingEventArgs args = new RowValidatingEventArgs((GridViewRowInfo) currentRow1);
        this.MasterTemplate.EventDispatcher.RaiseEvent<RowValidatingEventArgs>(EventDispatcher.RowValidating, (object) this, args);
        if (args.Cancel)
          return false;
        currentRow1?.DeferUserAddedRow();
        currentRow1.EndAddNewRow();
      }
      else
      {
        currentRow1?.DeferUserAddedRow();
        if (!this.GridViewElement.EndEdit())
          return false;
      }
      if (this.GridViewElement.NewRowEnterKeyMode == RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow)
      {
        if (currentRow1.PinPosition == PinnedRowPosition.Bottom)
          this.EnsureLastRowVisible(currentRow1);
        else if (currentRow1.RowPosition == SystemRowPosition.Bottom)
        {
          this.GridViewElement.UpdateLayout();
          currentRow1.EnsureVisible();
        }
        else
        {
          EventDispatcher eventDispatcher = currentRow1.ViewTemplate.MasterTemplate.EventDispatcher;
          eventDispatcher.SuspendEvent(EventDispatcher.CellValidating);
          eventDispatcher.SuspendEvent(EventDispatcher.CellValidated);
          eventDispatcher.SuspendEvent(EventDispatcher.RowValidating);
          eventDispatcher.SuspendEvent(EventDispatcher.RowValidated);
          GridViewRowInfo currentRow2;
          GridViewRowInfo currentRow3;
          do
          {
            currentRow2 = ((BaseGridNavigator) this.Navigator).MasterTemplate.CurrentRow;
            this.Navigator.SelectNextRow(1);
            currentRow3 = ((BaseGridNavigator) this.Navigator).MasterTemplate.CurrentRow;
          }
          while (!(currentRow3 is GridViewDataRowInfo) && !(currentRow3 is GridViewGroupRowInfo) && currentRow3 != currentRow2);
          eventDispatcher.ResumeEvent(EventDispatcher.CellValidating);
          eventDispatcher.ResumeEvent(EventDispatcher.CellValidated);
          eventDispatcher.ResumeEvent(EventDispatcher.RowValidating);
          eventDispatcher.ResumeEvent(EventDispatcher.RowValidated);
        }
      }
      currentRow1?.RaiseUserAddedRow();
      return false;
    }

    protected override bool ProcessDownKey(KeyEventArgs keys)
    {
      GridViewNewRowInfo currentRow = (GridViewNewRowInfo) this.GridViewElement.CurrentRow;
      if (this.GridViewElement.Template.AddNewRowPosition == SystemRowPosition.Bottom)
        return false;
      return base.ProcessDownKey(keys);
    }

    public override bool OnMouseDown(MouseEventArgs e)
    {
      RadElement radElement = this.GridViewElement.ElementTree.GetElementAtPoint(e.Location);
      GridTableElement currentView = this.GridViewElement.CurrentView as GridTableElement;
      if (currentView != null)
      {
        for (; radElement != null; radElement = radElement.Parent)
        {
          if (radElement == currentView.HScrollBar)
          {
            this.GridViewElement.EditorManager.CloseEditor();
            return false;
          }
        }
      }
      return base.OnMouseDown(e);
    }

    protected override bool OnMouseDownLeft(MouseEventArgs e)
    {
      GridNewRowElement rowAtPoint = this.GetRowAtPoint(e.Location) as GridNewRowElement;
      if (rowAtPoint == null)
        return base.OnMouseDownLeft(e);
      rowAtPoint.UpdateContentVisibility(true);
      bool flag = base.OnMouseDownLeft(e);
      if (rowAtPoint.RowInfo == null || rowAtPoint.RowInfo.IsCurrent)
        return flag;
      rowAtPoint.UpdateContentVisibility(false);
      return this.GridViewElement.EndEdit();
    }

    protected override bool OnMouseDownRight(MouseEventArgs e)
    {
      (this.GetRowAtPoint(e.Location) as GridNewRowElement)?.UpdateContentVisibility(true);
      return base.OnMouseDownRight(e);
    }

    protected override bool OnMouseUpLeft(MouseEventArgs e)
    {
      bool flag = base.OnMouseUpLeft(e);
      if (!flag && !this.GridViewElement.EditorManager.IsInEditMode)
      {
        GridCellElement cellAtPoint = this.RootGridBehavior.CellAtPoint;
        if (cellAtPoint != null && cellAtPoint.ColumnInfo != null && (cellAtPoint.ColumnInfo.ReadOnly && cellAtPoint.RowInfo is GridViewNewRowInfo))
          ((GridViewNewRowInfo) cellAtPoint.RowInfo).InitializeNewRow();
      }
      return flag;
    }

    protected override bool CanEnterEditMode(GridViewRowInfo rowInfo)
    {
      return true;
    }

    private void EnsureLastRowVisible(GridViewNewRowInfo newRowInfo)
    {
      this.GetLastScrollableRow((GridViewRowInfo) newRowInfo)?.EnsureVisible();
    }

    private GridViewRowInfo GetLastScrollableRow(GridViewRowInfo newRowInfo)
    {
      GridTraverser gridTraverser = new GridTraverser(newRowInfo.ViewInfo, GridTraverser.TraversalModes.ScrollableRows);
      gridTraverser.MoveToEnd();
      if (gridTraverser.Current != null)
        return gridTraverser.Current;
      return (GridViewRowInfo) null;
    }
  }
}
