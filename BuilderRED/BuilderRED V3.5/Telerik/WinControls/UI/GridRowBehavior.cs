// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridRowBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridRowBehavior : GridBehaviorImpl, IDisposable
  {
    private Point mouseDownLocation = Point.Empty;
    private Point mouseMoveLocation = Point.Empty;
    private GridRowElement rowToResize;
    private int rowToResizeInitialHeight;
    private bool enterEditMode;
    private bool wasInEditMode;
    private System.Windows.Forms.Timer scrollTimer;
    private int editorHashCode;
    private int anchorRowIndex;
    private int currentRowIndex;
    private int anchorColumnIndex;
    private int currentColumnIndex;
    private int mouseDownRowHierarchyLevel;
    private bool mouseDownOnLeftPinnedColumn;
    private bool mouseDownOnRightPinnedColumn;
    private bool selectionStartedOnAPinnedColumn;
    private bool mouseDownOnTopPinnedRow;
    private bool mouseDownOnBottomPinnedRow;
    private bool selectionStartedOnAPinnedRow;
    private GridViewColumn previousCurrentColumn;
    private List<GridViewColumn> orderedColumns;
    private List<GridViewRowInfo> orderedRows;

    public GridRowBehavior()
    {
      this.scrollTimer = new System.Windows.Forms.Timer();
      this.scrollTimer.Interval = 20;
      this.scrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
    }

    public void Dispose()
    {
      if (this.scrollTimer == null)
        return;
      this.scrollTimer.Enabled = false;
      this.scrollTimer.Dispose();
      this.scrollTimer = (System.Windows.Forms.Timer) null;
    }

    protected IGridNavigator Navigator
    {
      get
      {
        return this.GridViewElement.Navigator;
      }
    }

    protected GridViewEditManager EditorManager
    {
      get
      {
        return this.GridViewElement.EditorManager;
      }
    }

    protected MasterGridViewTemplate MasterTemplate
    {
      get
      {
        return this.GridViewElement.Template;
      }
    }

    protected bool IsPressedShift
    {
      get
      {
        return (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
      }
    }

    protected bool IsPressedControl
    {
      get
      {
        return (Control.ModifierKeys & Keys.Control) == Keys.Control;
      }
    }

    protected RadGridViewBeginEditMode BeginEditMode
    {
      get
      {
        return this.GridViewElement.BeginEditMode;
      }
    }

    protected bool IsInEditMode
    {
      get
      {
        return this.GridViewElement.IsInEditMode;
      }
    }

    protected BaseGridBehavior RootGridBehavior
    {
      get
      {
        return this.GridViewElement.GridBehavior as BaseGridBehavior;
      }
    }

    protected Point MouseDownLocation
    {
      get
      {
        return this.mouseDownLocation;
      }
    }

    protected GridRowElement RowToResize
    {
      get
      {
        return this.rowToResize;
      }
    }

    protected int RowToResizeInitialHeight
    {
      get
      {
        return this.rowToResizeInitialHeight;
      }
    }

    public override bool OnClick(EventArgs e)
    {
      return false;
    }

    public override bool OnDoubleClick(EventArgs e)
    {
      return false;
    }

    public override bool ProcessKey(KeyEventArgs keys)
    {
      IGridFilterPopupInteraction gridFilterPopup = this.GridViewElement.TableElement.GridFilterPopup;
      if (gridFilterPopup != null && gridFilterPopup.IsPopupOpen)
      {
        gridFilterPopup.ProcessKey(keys);
        return true;
      }
      switch (keys.KeyCode)
      {
        case Keys.Tab:
          return this.ProcessTabKey(keys);
        case Keys.Return:
          return this.ProcessEnterKey(keys);
        case Keys.Escape:
          return this.ProcessEscapeKey(keys);
        case Keys.Space:
          return this.ProcessSpaceKey(keys);
        case Keys.Prior:
          return this.ProcessPageUpKey(keys);
        case Keys.Next:
          return this.ProcessPageDownKey(keys);
        case Keys.End:
          return this.ProcessEndKey(keys);
        case Keys.Home:
          return this.ProcessHomeKey(keys);
        case Keys.Left:
          return this.ProcessLeftKey(keys);
        case Keys.Up:
          return this.ProcessUpKey(keys);
        case Keys.Right:
          return this.ProcessRightKey(keys);
        case Keys.Down:
          return this.ProcessDownKey(keys);
        case Keys.Insert:
          return this.ProcessInsertKey(keys);
        case Keys.Delete:
          return this.ProcessDeleteKey(keys);
        case Keys.Add:
          return this.ProcessAddKey(keys);
        case Keys.Subtract:
          return this.ProcessSubtractKey(keys);
        case Keys.F2:
          return this.ProcessF2Key(keys);
        default:
          return this.ProcessUnhandledKeys(keys);
      }
    }

    protected bool ProcessUnhandledKeys(KeyEventArgs keys)
    {
      if (keys.KeyCode != Keys.A || !keys.Control)
        return false;
      this.Navigator.SelectAll();
      return true;
    }

    public override bool ProcessKeyDown(KeyEventArgs keys)
    {
      return this.ProcessKey(keys);
    }

    public override bool ProcessKeyUp(KeyEventArgs keys)
    {
      return false;
    }

    public override bool ProcessKeyPress(KeyPressEventArgs keys)
    {
      if (keys.KeyChar > '\x001B')
        return this.ProcessAlphaNumericKey(keys);
      return false;
    }

    public override bool OnContextMenu(MouseEventArgs e)
    {
      return false;
    }

    protected virtual bool ProcessEscapeKey(KeyEventArgs keys)
    {
      if (!this.GridViewElement.IsInEditMode)
        return false;
      this.GridViewElement.CancelEdit();
      return true;
    }

    protected virtual bool ProcessEnterKey(KeyEventArgs keys)
    {
      bool isInEditMode = this.IsInEditMode;
      if (!isInEditMode && this.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnEnter)
        return this.GridViewElement.BeginEdit();
      if (isInEditMode && (!this.CanEndEdit() || !this.GridViewElement.EndEdit()))
        return false;
      switch (this.GridViewElement.EnterKeyMode)
      {
        case RadGridViewEnterKeyMode.EnterMovesToNextCell:
          this.Navigator.SelectNextColumn();
          break;
        case RadGridViewEnterKeyMode.EnterMovesToNextRow:
          this.Navigator.SelectNextRow(1);
          break;
        default:
          return false;
      }
      if (!isInEditMode || this.BeginEditMode == RadGridViewBeginEditMode.BeginEditProgrammatically)
        return false;
      this.GridViewElement.CurrentRow?.EnsureVisible();
      return this.GridViewElement.BeginEdit();
    }

    protected virtual bool ProcessSpaceKey(KeyEventArgs keys)
    {
      switch (this.GridControl.BeginEditMode)
      {
        case RadGridViewBeginEditMode.BeginEditOnKeystroke:
        case RadGridViewBeginEditMode.BeginEditOnKeystrokeOrF2:
          this.GridControl.BeginEdit();
          return false;
        default:
          GridViewRowInfo currentRow = this.GridViewElement.CurrentRow;
          if (currentRow != null)
          {
            MasterGridViewTemplate template = this.GridViewElement.Template;
            if (!template.MultiSelect)
              return false;
            if (template.SelectionMode == GridViewSelectionMode.FullRowSelect)
              currentRow.IsSelected = !currentRow.IsSelected;
            else if (currentRow.ViewTemplate.CurrentColumn != null)
            {
              GridViewCellInfo cell = currentRow.Cells[currentRow.ViewTemplate.CurrentColumn.Name];
              cell.IsSelected = !cell.IsSelected;
            }
          }
          return false;
      }
    }

    protected virtual bool ProcessF2Key(KeyEventArgs keys)
    {
      if (!this.IsInEditMode && (this.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnF2 || this.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnKeystrokeOrF2))
        return this.GridViewElement.BeginEdit();
      return false;
    }

    protected virtual bool ProcessUpKey(KeyEventArgs keys)
    {
      this.GridViewElement.Navigator.BeginSelection(this.GetKeyboardNavigationContext(keys));
      this.GridViewElement.Navigator.SelectPreviousRow(1);
      this.GridViewElement.Navigator.EndSelection();
      return true;
    }

    protected virtual bool ProcessDownKey(KeyEventArgs keys)
    {
      if (this.GridControl.CurrentRow is GridViewNewRowInfo && this.MasterTemplate.SelectLastAddedRow && this.GridControl.IsInEditMode)
      {
        this.GridControl.EndEdit();
        return true;
      }
      int count = this.GridControl.ChildRows.Count;
      this.GridViewElement.Navigator.BeginSelection(this.GetKeyboardNavigationContext(keys));
      this.GridViewElement.Navigator.SelectNextRow(1);
      this.GridViewElement.Navigator.EndSelection();
      if (count != this.GridControl.ChildRows.Count)
        this.GridViewElement.TableElement.RowScroller.UpdateScrollRange();
      return true;
    }

    protected virtual bool ProcessLeftKey(KeyEventArgs keys)
    {
      this.GridViewElement.Navigator.BeginSelection(this.GetKeyboardNavigationContext(keys));
      if (this.GridViewElement.RightToLeft)
      {
        if (!this.GridViewElement.Navigator.IsLastRow(this.GridViewElement.CurrentRow) || !this.GridViewElement.Navigator.IsLastColumn(this.GridViewElement.CurrentColumn))
          this.GridViewElement.Navigator.SelectNextColumn();
      }
      else if (!this.GridViewElement.Navigator.IsFirstRow(this.GridViewElement.CurrentRow) || !this.GridViewElement.Navigator.IsFirstColumn(this.GridViewElement.CurrentColumn))
        this.GridViewElement.Navigator.SelectPreviousColumn();
      this.GridViewElement.Navigator.EndSelection();
      return true;
    }

    protected virtual bool ProcessRightKey(KeyEventArgs keys)
    {
      this.GridViewElement.Navigator.BeginSelection(this.GetKeyboardNavigationContext(keys));
      if (this.GridViewElement.RightToLeft)
      {
        if (!this.GridViewElement.Navigator.IsFirstRow(this.GridViewElement.CurrentRow) || !this.GridViewElement.Navigator.IsFirstColumn(this.GridViewElement.CurrentColumn))
          this.GridViewElement.Navigator.SelectPreviousColumn();
      }
      else if (!this.GridViewElement.Navigator.IsLastRow(this.GridViewElement.CurrentRow) || !this.GridViewElement.Navigator.IsLastColumn(this.GridViewElement.CurrentColumn))
        this.GridViewElement.Navigator.SelectNextColumn();
      this.GridViewElement.Navigator.EndSelection();
      return true;
    }

    protected virtual bool ProcessTabKey(KeyEventArgs keys)
    {
      if (this.GridViewElement.StandardTab || this.MasterTemplate.CurrentView.ViewTemplate.Columns.Count == 0)
        return false;
      bool flag1 = this.IsInEditMode && this.GridViewElement.CurrentRow is GridViewNewRowInfo;
      bool shift = keys.Shift;
      if (keys.Control)
        return this.SelectNextControl(!shift);
      if (shift && this.IsOnFirstCell())
        return this.SelectNextControl(false);
      if (!shift && this.IsOnLastCell() && !flag1)
        return this.SelectNextControl(true);
      bool flag2 = flag1 && (shift && this.GridViewElement.Navigator.IsFirstColumn(this.GridViewElement.CurrentColumn) || !shift && this.GridViewElement.Navigator.IsLastColumn(this.GridViewElement.CurrentColumn));
      EventDispatcher eventDispatcher = this.GridViewElement.Template.EventDispatcher;
      if (flag2)
      {
        if (!this.GridViewElement.EndEdit())
          return true;
        eventDispatcher.SuspendEvent(EventDispatcher.CellValidating);
        eventDispatcher.SuspendEvent(EventDispatcher.CellValidated);
      }
      this.GridViewElement.Navigator.BeginSelection(this.GetKeyboardNavigationContext(keys));
      if (shift)
      {
        this.GridViewElement.Navigator.SelectPreviousColumn();
      }
      else
      {
        SystemRowPosition systemRowPosition = this.GridViewElement.CurrentRow != null ? this.GridViewElement.CurrentRow.ViewTemplate.AddNewRowPosition : this.GridViewElement.TableElement.MasterTemplate.AddNewRowPosition;
        if (flag1 && systemRowPosition == SystemRowPosition.Bottom && this.GridViewElement.Navigator.IsLastEditableColumn(this.GridViewElement.CurrentColumn))
        {
          this.GridViewElement.Navigator.SelectFirstColumn();
          while (this.GridViewElement.CurrentColumn.ReadOnly && this.GridViewElement.Navigator.SelectNextColumn())
            ;
        }
        else
          this.GridViewElement.Navigator.SelectNextColumn();
      }
      this.GridViewElement.Navigator.EndSelection();
      if (flag2)
      {
        this.GridViewElement.BeginEdit();
        eventDispatcher.ResumeEvent(EventDispatcher.CellValidating);
        eventDispatcher.ResumeEvent(EventDispatcher.CellValidated);
      }
      return true;
    }

    protected virtual bool ProcessAddKey(KeyEventArgs keys)
    {
      if (this.MasterTemplate.CurrentRow != null && this.MasterTemplate.CurrentRow.CanBeExpanded)
        this.MasterTemplate.CurrentRow.IsExpanded = true;
      return true;
    }

    protected virtual bool ProcessSubtractKey(KeyEventArgs keys)
    {
      if (this.MasterTemplate.CurrentRow != null && this.MasterTemplate.CurrentRow.CanBeExpanded)
        this.MasterTemplate.CurrentRow.IsExpanded = false;
      return true;
    }

    protected virtual bool ProcessDeleteKey(KeyEventArgs keys)
    {
      if (!this.MasterTemplate.GridReadOnly && !this.GridViewElement.CurrentView.ViewInfo.ViewTemplate.ReadOnly && (this.GridViewElement.CurrentView.ViewInfo.ViewTemplate.AllowDeleteRow && this.GridViewElement.CurrentRow is GridViewDataRowInfo))
        return this.Navigator.DeleteSelectedRows();
      return false;
    }

    protected virtual bool ProcessHomeKey(KeyEventArgs keys)
    {
      if (keys.Control)
      {
        GridViewRowInfo currentRow = this.GridControl.CurrentRow;
        this.GridViewElement.Navigator.SelectFirstRow();
        if (this.GridControl.MultiSelect && this.GridControl.SelectionMode == GridViewSelectionMode.FullRowSelect && keys.Shift)
        {
          this.MasterTemplate.SelectedRows.BeginUpdate();
          GridTraverser gridTraverser = new GridTraverser(this.GridControl.MasterView);
          while (gridTraverser.MoveNext())
          {
            if (gridTraverser.Current.IsVisible && gridTraverser.Current.CanBeSelected)
              gridTraverser.Current.IsSelected = true;
            if (gridTraverser.Current == currentRow)
              break;
          }
          this.MasterTemplate.SelectedRows.EndUpdate(true);
        }
      }
      else
        this.GridViewElement.Navigator.SelectFirstColumn();
      return true;
    }

    protected virtual bool ProcessEndKey(KeyEventArgs keys)
    {
      if (keys.Control)
      {
        GridViewRowInfo currentRow = this.GridControl.CurrentRow;
        bool flag;
        do
        {
          int count = this.GridControl.ChildRows.Count;
          this.GridViewElement.Navigator.SelectLastRow();
          flag = count != this.GridControl.ChildRows.Count;
          if (flag)
          {
            this.GridViewElement.TableElement.RowScroller.UpdateScrollRange();
            currentRow.IsSelected = currentRow.IsCurrent = false;
          }
        }
        while (flag);
        if (this.GridControl.MultiSelect && this.GridControl.SelectionMode == GridViewSelectionMode.FullRowSelect && keys.Shift)
        {
          this.MasterTemplate.SelectedRows.BeginUpdate();
          GridTraverser gridTraverser = new GridTraverser(this.GridControl.MasterView);
          gridTraverser.MoveToEnd();
          while (gridTraverser.MovePrevious())
          {
            if (gridTraverser.Current.IsVisible && gridTraverser.Current.CanBeSelected)
              gridTraverser.Current.IsSelected = true;
            if (gridTraverser.Current == currentRow)
              break;
          }
          this.MasterTemplate.SelectedRows.EndUpdate(true);
        }
      }
      else
        this.GridViewElement.Navigator.SelectLastColumn();
      return true;
    }

    protected virtual bool ProcessInsertKey(KeyEventArgs keys)
    {
      return false;
    }

    protected virtual bool ProcessAlphaNumericKey(KeyPressEventArgs keys)
    {
      if (!this.IsInEditMode && (this.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnKeystroke || this.BeginEditMode == RadGridViewBeginEditMode.BeginEditOnKeystrokeOrF2))
      {
        this.GridViewElement.ContextMenuManager.HideContextMenu();
        this.GridViewElement.BeginEdit();
        if (this.GridViewElement.ActiveEditor is RadTextBoxEditor)
        {
          this.GridViewElement.ActiveEditor.Value = (object) keys.KeyChar;
          if (this.GridViewElement.IsInEditMode)
          {
            RadTextBoxItem textBoxItem = ((RadTextBoxElement) ((BaseInputEditor) this.GridViewElement.ActiveEditor).EditorElement).TextBoxItem;
            textBoxItem.SelectionStart = 1;
            textBoxItem.SelectionLength = 0;
          }
          return true;
        }
        if (this.GridViewElement.ActiveEditor is GridSpinEditor)
        {
          int result = 0;
          if (int.TryParse(keys.KeyChar.ToString(), out result))
          {
            GridSpinEditor activeEditor1 = this.GridViewElement.ActiveEditor as GridSpinEditor;
            if (activeEditor1 != null)
              (activeEditor1.EditorElement as RadSpinEditorElement).TextBoxControl.Text = result.ToString();
            else
              this.GridViewElement.ActiveEditor.Value = (object) result;
            if (this.GridViewElement.IsInEditMode)
            {
              GridSpinEditor activeEditor2 = this.GridViewElement.ActiveEditor as GridSpinEditor;
              if (activeEditor2 != null)
              {
                RadSpinEditorElement editorElement = (RadSpinEditorElement) activeEditor2.EditorElement;
                editorElement.TextBoxControl.SelectionStart = 1;
                int num = editorElement.TextBoxControl.TextLength - 1;
                if (num < 0)
                  num = 0;
                editorElement.TextBoxControl.SelectionLength = num;
              }
            }
            return true;
          }
          if (keys.KeyChar.ToString() == Thread.CurrentThread.CurrentCulture.NumberFormat.NegativeSign)
          {
            GridSpinEditor activeEditor = this.GridViewElement.ActiveEditor as GridSpinEditor;
            if (activeEditor != null)
            {
              RadSpinEditorElement editorElement = (RadSpinEditorElement) activeEditor.EditorElement;
              editorElement.TextBoxControl.Text = Thread.CurrentThread.CurrentCulture.NumberFormat.NegativeSign;
              editorElement.TextBoxControl.SelectionStart = editorElement.TextBoxControl.Text.Length;
            }
            return true;
          }
          if (keys.KeyChar.ToString() == Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator)
          {
            GridSpinEditor activeEditor = this.GridViewElement.ActiveEditor as GridSpinEditor;
            if (activeEditor != null)
            {
              RadSpinEditorElement editorElement = (RadSpinEditorElement) activeEditor.EditorElement;
              editorElement.TextBoxControl.Text = "0" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
              editorElement.TextBoxControl.SelectionStart = editorElement.TextBoxControl.Text.Length;
            }
            return true;
          }
        }
        else if (this.GridViewElement.ActiveEditor is RadDropDownListEditor)
        {
          string findString = keys.KeyChar.ToString();
          RadDropDownListEditorElement editorElement = (this.GridViewElement.ActiveEditor as RadDropDownListEditor).EditorElement as RadDropDownListEditorElement;
          if ((editorElement.AutoCompleteMode & AutoCompleteMode.Append) == AutoCompleteMode.Append && editorElement.AutoCompleteAppend.FindShortestString(findString) == -1)
          {
            editorElement.EditableElementText = findString;
            editorElement.EditableElement.SelectionStart = 1;
            editorElement.EditableElement.SelectionLength = 0;
            return true;
          }
        }
        else
        {
          if (this.GridViewElement.ActiveEditor is GridBrowseEditor)
          {
            RadBrowseEditorElement editorElement = ((BaseInputEditor) this.GridViewElement.ActiveEditor).EditorElement as RadBrowseEditorElement;
            if (editorElement.ReadOnly)
              return false;
            this.GridViewElement.ActiveEditor.Value = (object) keys.KeyChar;
            if (this.GridViewElement.IsInEditMode)
            {
              editorElement.TextBoxItem.SelectionStart = 1;
              editorElement.TextBoxItem.SelectionLength = 0;
            }
            return true;
          }
          if (this.GridViewElement.ActiveEditor is RadCalculatorEditor)
          {
            RadCalculatorEditor activeEditor = this.GridViewElement.ActiveEditor as RadCalculatorEditor;
            int result = 0;
            if (int.TryParse(keys.KeyChar.ToString(), out result))
            {
              if (activeEditor != null)
                (activeEditor.EditorElement as RadCalculatorEditorElement).EditorContentElement.TextBoxItem.Text = result.ToString();
              else
                this.GridViewElement.ActiveEditor.Value = (object) result;
              if (this.GridViewElement.IsInEditMode && activeEditor != null)
              {
                RadCalculatorEditorElement editorElement = activeEditor.EditorElement as RadCalculatorEditorElement;
                editorElement.EditorContentElement.TextBoxItem.SelectionStart = 1;
                int num = editorElement.EditorContentElement.TextBoxItem.TextLength - 1;
                if (num < 0)
                  num = 0;
                editorElement.EditorContentElement.TextBoxItem.SelectionLength = num;
              }
              return true;
            }
            if (keys.KeyChar == '-')
            {
              if (activeEditor != null)
              {
                RadCalculatorEditorElement editorElement = activeEditor.EditorElement as RadCalculatorEditorElement;
                editorElement.EditorContentElement.TextBoxItem.Text = "-";
                editorElement.EditorContentElement.TextBoxItem.SelectionStart = 1;
              }
              return true;
            }
          }
          else if (this.GridViewElement.ActiveEditor is GridColorPickerEditor)
          {
            RadColorPickerEditorElement editorElement = (this.GridViewElement.ActiveEditor as GridColorPickerEditor).EditorElement as RadColorPickerEditorElement;
            editorElement.TextBoxItem.Text = keys.KeyChar.ToString();
            if (this.GridViewElement.IsInEditMode)
            {
              editorElement.TextBoxItem.SelectionStart = 1;
              editorElement.TextBoxItem.SelectionLength = 0;
            }
            return true;
          }
        }
      }
      return false;
    }

    protected virtual bool ProcessPageUpKey(KeyEventArgs keys)
    {
      return false;
    }

    protected virtual bool ProcessPageDownKey(KeyEventArgs keys)
    {
      return false;
    }

    public override bool OnMouseEnter(EventArgs e)
    {
      return false;
    }

    public override bool OnMouseLeave(EventArgs e)
    {
      return false;
    }

    public override bool OnMouseMove(MouseEventArgs e)
    {
      Point location = e.Location;
      if (this.RootGridBehavior.RowAtPoint == null)
        this.GridViewElement.TableElement.HoveredRow = (GridRowElement) null;
      if (e.Button == MouseButtons.None)
        return this.ShowSizeNSCursort(location);
      if (e.Button != MouseButtons.Left)
      {
        this.ResetControlCursor();
        return false;
      }
      if (this.rowToResize != null)
      {
        this.ResizeRow(location);
        return true;
      }
      GridCellElement cellAtPoint = this.RootGridBehavior.CellAtPoint;
      GridRowElement rowAtPoint = this.GetRowAtPoint(this.mouseDownLocation);
      bool flag = false;
      if (cellAtPoint != null && cellAtPoint.ViewTemplate != null && (!cellAtPoint.ViewTemplate.AllowRowReorder && rowAtPoint != null))
        flag = this.ProcessMouseSelection(location, cellAtPoint);
      if ((cellAtPoint == null || cellAtPoint.ColumnInfo == null || (cellAtPoint.ColumnInfo.IsPinned || cellAtPoint.RowInfo.IsPinned)) && (this.MasterTemplate.MultiSelect && this.mouseDownLocation != location && !this.GridViewElement.GridControl.EnableKineticScrolling))
      {
        this.mouseMoveLocation = location;
        if (!this.scrollTimer.Enabled)
          this.scrollTimer.Enabled = true;
        flag = false;
      }
      return flag;
    }

    public override bool OnMouseDown(MouseEventArgs e)
    {
      if (!this.ValidateOnUserInput(e))
        return true;
      if (e.Button == MouseButtons.Middle)
        return false;
      this.mouseDownLocation = e.Location;
      if (e.Button == MouseButtons.Right)
        this.OnMouseDownRight(e);
      if (e.Button == MouseButtons.Left)
        this.OnMouseDownLeft(e);
      if (this.GridViewElement.ElementTree != null)
        this.editorHashCode = this.GridControl.IsInEditMode ? this.GridControl.ActiveEditor.GetHashCode() : 0;
      return false;
    }

    public override bool OnMouseUp(MouseEventArgs e)
    {
      bool flag = false;
      if (e.Button == MouseButtons.Right)
        flag = this.OnMouseUpRight(e);
      else if (e.Button == MouseButtons.Left)
        flag = this.OnMouseUpLeft(e);
      this.RootGridBehavior.UnlockBehavior((IGridBehavior) this);
      this.ResetFieldValues();
      if (this.scrollTimer.Enabled)
        this.scrollTimer.Enabled = false;
      this.mouseMoveLocation = Point.Empty;
      this.orderedColumns = (List<GridViewColumn>) null;
      this.orderedRows = (List<GridViewRowInfo>) null;
      this.anchorRowIndex = -1;
      this.currentRowIndex = -1;
      this.anchorColumnIndex = -1;
      this.currentColumnIndex = -1;
      this.mouseDownOnLeftPinnedColumn = false;
      this.mouseDownOnRightPinnedColumn = false;
      this.selectionStartedOnAPinnedColumn = false;
      this.mouseDownOnTopPinnedRow = false;
      this.mouseDownOnBottomPinnedRow = false;
      this.selectionStartedOnAPinnedRow = false;
      return flag;
    }

    public override bool OnMouseDoubleClick(MouseEventArgs e)
    {
      return false;
    }

    public override bool OnMouseWheel(MouseEventArgs e)
    {
      return false;
    }

    protected virtual bool OnMouseDownRight(MouseEventArgs e)
    {
      if (this.IsInEditMode && !this.GridViewElement.EndEdit())
        return true;
      GridCellElement cellAtPoint = this.GetCellAtPoint(e.Location);
      if (cellAtPoint != null)
      {
        GridRowElement rowElement = cellAtPoint.RowElement;
        this.Navigator.BeginSelection(this.GetMouseNavigationContext(e));
        this.Navigator.Select(rowElement.RowInfo, cellAtPoint.ColumnInfo);
        this.Navigator.EndSelection();
        if (!cellAtPoint.IsInValidState(true))
          cellAtPoint = this.GetCellAtPoint(e.Location);
        cellAtPoint?.ShowContextMenu();
      }
      return false;
    }

    protected virtual bool OnMouseDownLeft(MouseEventArgs e)
    {
      this.previousCurrentColumn = this.GridViewElement.CurrentColumn;
      GridCellElement cellAtPoint = this.GetCellAtPoint(e.Location);
      GridRowElement rowAtPoint = this.GetRowAtPoint(e.Location);
      if (this.IsInEditMode && this.GridViewElement.SplitMode != RadGridViewSplitMode.None && cellAtPoint.TableElement != this.GridViewElement.CurrentView)
      {
        this.GridViewElement.EndEdit();
        this.GridViewElement.CurrentView = (IRowView) cellAtPoint.TableElement;
      }
      if (cellAtPoint == null || rowAtPoint == null)
      {
        if (rowAtPoint != null && rowAtPoint.RowInfo != this.GridViewElement.CurrentRow)
        {
          this.Navigator.BeginSelection(this.GetMouseNavigationContext(e));
          this.Navigator.Select(rowAtPoint.RowInfo, rowAtPoint.ViewTemplate.CurrentColumn);
          this.Navigator.EndSelection();
          if (rowAtPoint != null && rowAtPoint.RowInfo.IsCurrent)
            this.GridViewElement.CurrentView = (IRowView) rowAtPoint.TableElement;
        }
        if (rowAtPoint != null && this.CanResizeRow(e.Location, rowAtPoint))
        {
          this.RootGridBehavior.LockBehavior((IGridBehavior) this);
          this.rowToResize = rowAtPoint;
          this.rowToResizeInitialHeight = this.rowToResize.Size.Height;
          this.previousCurrentColumn = (GridViewColumn) null;
          return true;
        }
        this.previousCurrentColumn = (GridViewColumn) null;
        return false;
      }
      if (this.CanResizeRow(e.Location, rowAtPoint))
      {
        this.RootGridBehavior.LockBehavior((IGridBehavior) this);
        this.rowToResize = rowAtPoint;
        this.rowToResizeInitialHeight = this.rowToResize.Size.Height;
        this.previousCurrentColumn = (GridViewColumn) null;
        return true;
      }
      if (this.GridViewElement.SplitMode != RadGridViewSplitMode.None && rowAtPoint != null)
        this.GridViewElement.CurrentView = (IRowView) rowAtPoint.TableElement;
      if (cellAtPoint != null && cellAtPoint.ColumnInfo != null && cellAtPoint.RowInfo != null)
      {
        this.mouseDownOnLeftPinnedColumn = cellAtPoint.ColumnInfo.PinPosition == PinnedColumnPosition.Left;
        this.mouseDownOnRightPinnedColumn = cellAtPoint.ColumnInfo.PinPosition == PinnedColumnPosition.Right;
        this.selectionStartedOnAPinnedColumn = this.mouseDownOnLeftPinnedColumn || this.mouseDownOnRightPinnedColumn;
        this.mouseDownOnTopPinnedRow = cellAtPoint.RowInfo.PinPosition == PinnedRowPosition.Top;
        this.mouseDownOnBottomPinnedRow = cellAtPoint.RowInfo.PinPosition == PinnedRowPosition.Bottom;
        this.selectionStartedOnAPinnedRow = this.mouseDownOnTopPinnedRow || this.mouseDownOnBottomPinnedRow;
      }
      this.orderedRows = this.GetOrderedRows();
      this.anchorRowIndex = this.GetRowIndex(rowAtPoint.RowInfo);
      this.currentRowIndex = this.anchorRowIndex;
      this.orderedColumns = this.GetOrderedColumns();
      this.anchorColumnIndex = this.GetColumnIndex(cellAtPoint.ColumnInfo);
      this.currentColumnIndex = this.anchorColumnIndex;
      this.mouseDownRowHierarchyLevel = rowAtPoint.RowInfo.HierarchyLevel;
      bool flag = this.SelectPositionOnMouseDownLeft(e, rowAtPoint, cellAtPoint);
      this.previousCurrentColumn = (GridViewColumn) null;
      return flag;
    }

    private int GetColumnIndex(GridViewColumn columnInfo)
    {
      return this.orderedColumns.IndexOf(columnInfo);
    }

    private int GetRowIndex(GridViewRowInfo rowInfo)
    {
      return this.orderedRows.IndexOf(rowInfo);
    }

    protected virtual bool OnMouseUpRight(MouseEventArgs e)
    {
      return false;
    }

    protected virtual bool OnMouseUpLeft(MouseEventArgs e)
    {
      if (this.rowToResize != null)
        return true;
      GridCellElement cellAtPoint = this.GetCellAtPoint(e.Location);
      if (this.enterEditMode && this.BeginEditMode != RadGridViewBeginEditMode.BeginEditProgrammatically)
        this.GridViewElement.BeginEdit();
      else if (this.IsInEditMode && !this.enterEditMode && (cellAtPoint != null && cellAtPoint.ColumnInfo != null) && (!cellAtPoint.ColumnInfo.IsCurrent && this.editorHashCode == this.GridControl.ActiveEditor.GetHashCode()) && (this.GridViewElement.IsInEditMode && this.EditorManager.WasInEditMode || cellAtPoint is GridRowHeaderCellElement))
        this.GridViewElement.CloseEditor();
      return false;
    }

    protected virtual bool SelectPositionOnMouseDownLeft(
      MouseEventArgs e,
      GridRowElement rowElement,
      GridCellElement cellElement)
    {
      GridViewRowInfo currentRow = this.GridViewElement.CurrentRow;
      GridTableElement tableElement = rowElement.TableElement;
      GridViewRowInfo rowInfo = rowElement.RowInfo;
      GridViewColumn columnInfo = cellElement.ColumnInfo;
      this.wasInEditMode = this.IsInEditMode;
      bool flag1 = cellElement == this.GridViewElement.CurrentCell;
      GridDataCellElement currentCell = this.GridViewElement.CurrentCell;
      if (this.wasInEditMode && flag1 && this.EditorIsActive((GridCellElement) currentCell, e.Location))
        return false;
      if (rowInfo != null && rowInfo.IsCurrent && (columnInfo != null && columnInfo.IsCurrent) && (!this.IsPressedShift && !this.IsPressedControl && (this.GridControl.MultiSelect && this.GridControl.SelectionMode == GridViewSelectionMode.CellSelect)) && this.GridControl.SelectedCells.Count > 1)
        this.GridControl.ClearSelection();
      RadScrollBarElement vscrollBar = tableElement.VScrollBar;
      bool flag2 = vscrollBar.Value == vscrollBar.Maximum - vscrollBar.LargeChange + 1;
      bool flag3 = columnInfo is GridViewCheckBoxColumn;
      bool flag4 = this.previousCurrentColumn is GridViewCheckBoxColumn;
      this.enterEditMode = this.CanEnterEditMode(rowInfo) && (!this.wasInEditMode || flag1) && (flag1 && !flag3 || !flag1 && flag4) || rowInfo is GridViewNewRowInfo || rowInfo is GridViewFilteringRowInfo;
      GridNavigationContext navigationContext = this.GetMouseNavigationContext(e);
      this.Navigator.BeginSelection(navigationContext);
      if (this.GridControl.MultiSelect && this.GridControl.SelectionMode == GridViewSelectionMode.FullRowSelect && (flag1 && navigationContext.ModifierKeys == Keys.Control))
      {
        GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs(rowInfo, columnInfo);
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
        if (args.Cancel)
          return false;
        rowInfo.IsSelected = !rowInfo.IsSelected;
      }
      else
      {
        IGridNavigator navigator = this.Navigator;
        int num = this.GridViewElement.TableElement.HScrollBar.Value;
        this.Navigator.Select(rowInfo, columnInfo);
        if (cellElement is GridGroupExpanderCellElement)
          this.GridViewElement.TableElement.HScrollBar.Value = num;
      }
      if (flag2 && vscrollBar.Maximum - vscrollBar.LargeChange + 1 < vscrollBar.Maximum)
      {
        vscrollBar.Value = vscrollBar.Maximum - vscrollBar.LargeChange + 1;
        tableElement.UpdateLayout();
      }
      rowElement = this.GetRowAtPoint(e.Location);
      if (rowElement != null && rowElement.RowInfo != rowInfo)
      {
        rowInfo = rowElement.RowInfo;
        this.Navigator.Select(rowInfo, columnInfo);
      }
      this.Navigator.EndSelection();
      GridRowBehavior gridRowBehavior = this;
      gridRowBehavior.enterEditMode = ((gridRowBehavior.enterEditMode ? 1 : 0) & (!(columnInfo is GridViewDataColumn) ? 0 : (columnInfo.IsCurrent ? 1 : 0))) != 0;
      if (rowInfo.IsCurrent)
        this.GridViewElement.CurrentView = (IRowView) tableElement;
      if (this.wasInEditMode && this.IsInEditMode && this.EditorIsActive(cellElement, e.Location))
        return !this.EditorManager.IsPermanentEditor(this.GridViewElement.ActiveEditor.GetType());
      return false;
    }

    protected virtual bool ProcessMouseSelection(Point mousePosition, GridCellElement currentCell)
    {
      if (this.RootGridBehavior.LockedBehavior != this)
      {
        this.GridControl.Capture = true;
        this.RootGridBehavior.LockBehavior((IGridBehavior) this);
      }
      return this.DoMouseSelection(currentCell, mousePosition);
    }

    private void ProcessFullRowSelection(int rowUnderMouseIndex)
    {
      if (rowUnderMouseIndex == this.currentRowIndex || rowUnderMouseIndex < 0 || this.anchorRowIndex < 0)
        return;
      if (rowUnderMouseIndex < this.currentRowIndex && this.currentRowIndex > this.anchorRowIndex && rowUnderMouseIndex < this.anchorRowIndex || rowUnderMouseIndex > this.currentRowIndex && this.currentRowIndex < this.anchorRowIndex && rowUnderMouseIndex > this.anchorRowIndex)
      {
        int num1 = Math.Min(this.anchorRowIndex, this.currentRowIndex);
        int num2 = Math.Max(this.anchorRowIndex, this.currentRowIndex);
        for (int index = num1; index < num2; ++index)
          this.orderedRows[index].IsSelected = false;
      }
      bool flag1 = rowUnderMouseIndex < this.currentRowIndex && rowUnderMouseIndex < this.anchorRowIndex;
      if (rowUnderMouseIndex > this.currentRowIndex && rowUnderMouseIndex > this.anchorRowIndex || flag1)
      {
        int num1 = Math.Min(this.anchorRowIndex, rowUnderMouseIndex);
        int num2 = Math.Max(this.anchorRowIndex, rowUnderMouseIndex);
        for (int index = num1; index <= num2; ++index)
          this.orderedRows[index].IsSelected = true;
        this.currentRowIndex = rowUnderMouseIndex;
      }
      else
      {
        bool flag2 = rowUnderMouseIndex < this.currentRowIndex && rowUnderMouseIndex > this.anchorRowIndex;
        bool flag3 = rowUnderMouseIndex > this.currentRowIndex && rowUnderMouseIndex < this.anchorRowIndex;
        int num1 = Math.Min(this.currentRowIndex, rowUnderMouseIndex);
        int num2 = Math.Max(this.currentRowIndex, rowUnderMouseIndex);
        if (flag2)
          ++num1;
        if (flag3)
          --num2;
        for (int index = num1; index <= num2; ++index)
        {
          if (index != this.anchorRowIndex)
            this.orderedRows[index].IsSelected = false;
        }
        this.currentRowIndex = rowUnderMouseIndex;
      }
    }

    private bool ProcessCellSelection(int rowUnderMouseIndex, int columnUnderMouseIndex)
    {
      if (rowUnderMouseIndex == this.currentRowIndex && columnUnderMouseIndex == this.currentColumnIndex || (rowUnderMouseIndex < 0 || this.anchorRowIndex < 0) || (columnUnderMouseIndex < 0 || this.anchorColumnIndex < 0))
        return false;
      bool flag1 = rowUnderMouseIndex < this.currentRowIndex && this.currentRowIndex > this.anchorRowIndex && rowUnderMouseIndex < this.anchorRowIndex || rowUnderMouseIndex > this.currentRowIndex && this.currentRowIndex < this.anchorRowIndex && rowUnderMouseIndex > this.anchorRowIndex;
      bool flag2 = columnUnderMouseIndex < this.currentColumnIndex && this.currentColumnIndex > this.anchorColumnIndex && columnUnderMouseIndex < this.anchorColumnIndex || columnUnderMouseIndex > this.currentColumnIndex && this.currentColumnIndex < this.anchorColumnIndex && columnUnderMouseIndex > this.anchorColumnIndex;
      if (flag1 || flag2)
      {
        int num1 = Math.Min(this.anchorRowIndex, this.currentRowIndex);
        int num2 = Math.Max(this.anchorRowIndex, this.currentRowIndex);
        int num3 = Math.Min(this.anchorColumnIndex, this.currentColumnIndex);
        int num4 = Math.Max(this.anchorColumnIndex, this.currentColumnIndex);
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          for (int index2 = num3; index2 <= num4; ++index2)
          {
            GridViewCellInfo cell = this.orderedRows[index1].Cells[this.orderedColumns[index2].Index];
            if (cell != null && cell.IsSelected)
              cell.IsSelected = false;
          }
        }
      }
      bool flag3 = rowUnderMouseIndex < this.currentRowIndex && rowUnderMouseIndex < this.anchorRowIndex;
      bool flag4 = rowUnderMouseIndex > this.currentRowIndex && rowUnderMouseIndex > this.anchorRowIndex;
      bool flag5 = columnUnderMouseIndex < this.currentColumnIndex && columnUnderMouseIndex < this.anchorColumnIndex;
      bool flag6 = columnUnderMouseIndex > this.currentColumnIndex && columnUnderMouseIndex > this.anchorColumnIndex;
      if (flag4 || flag3 || (flag5 || flag6))
      {
        int num1 = Math.Min(this.anchorRowIndex, rowUnderMouseIndex);
        int num2 = Math.Max(this.anchorRowIndex, rowUnderMouseIndex);
        int num3 = Math.Min(this.anchorColumnIndex, columnUnderMouseIndex);
        int num4 = Math.Max(this.anchorColumnIndex, columnUnderMouseIndex);
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          for (int index2 = num3; index2 <= num4; ++index2)
          {
            GridViewCellInfo cell = this.orderedRows[index1].Cells[this.orderedColumns[index2].Index];
            if (cell != null && !cell.IsSelected)
              cell.IsSelected = true;
          }
        }
      }
      bool flag7 = rowUnderMouseIndex < this.currentRowIndex && rowUnderMouseIndex >= this.anchorRowIndex;
      bool flag8 = rowUnderMouseIndex > this.currentRowIndex && rowUnderMouseIndex <= this.anchorRowIndex;
      bool flag9 = columnUnderMouseIndex < this.currentColumnIndex && columnUnderMouseIndex >= this.anchorColumnIndex;
      bool flag10 = columnUnderMouseIndex > this.currentColumnIndex && columnUnderMouseIndex <= this.anchorColumnIndex;
      if (flag7 || flag8)
      {
        int num1 = Math.Min(this.currentRowIndex, rowUnderMouseIndex);
        int num2 = Math.Max(this.currentRowIndex, rowUnderMouseIndex);
        int num3 = Math.Min(this.anchorColumnIndex, this.currentColumnIndex);
        int num4 = Math.Max(this.anchorColumnIndex, this.currentColumnIndex);
        if (flag7)
          ++num1;
        if (flag8)
          --num2;
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          if (index1 != this.anchorRowIndex)
          {
            for (int index2 = num3; index2 <= num4; ++index2)
            {
              GridViewCellInfo cell = this.orderedRows[index1].Cells[this.orderedColumns[index2].Index];
              if (cell != null && cell.IsSelected)
                cell.IsSelected = false;
            }
          }
        }
      }
      if (flag9 || flag10)
      {
        int num1 = Math.Min(this.anchorRowIndex, this.currentRowIndex);
        int num2 = Math.Max(this.anchorRowIndex, this.currentRowIndex);
        int num3 = Math.Min(this.currentColumnIndex, columnUnderMouseIndex);
        int num4 = Math.Max(this.currentColumnIndex, columnUnderMouseIndex);
        if (flag9)
          ++num3;
        if (flag10)
          --num4;
        for (int index1 = num1; index1 <= num2; ++index1)
        {
          for (int index2 = num3; index2 <= num4; ++index2)
          {
            if (index2 != this.anchorColumnIndex)
            {
              GridViewCellInfo cell = this.orderedRows[index1].Cells[this.orderedColumns[index2].Index];
              if (cell != null && cell.IsSelected)
                cell.IsSelected = false;
            }
          }
        }
      }
      this.currentRowIndex = rowUnderMouseIndex;
      this.currentColumnIndex = columnUnderMouseIndex;
      return true;
    }

    private List<GridViewRowInfo> GetOrderedRows()
    {
      GridTraverser gridTraverser = new GridTraverser(this.GridControl.MasterView);
      gridTraverser.ProcessHierarchy = true;
      gridTraverser.TraversalMode = GridTraverser.TraversalModes.AllRows;
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      while (gridTraverser.MoveNext())
      {
        if (gridTraverser.Current is GridViewDataRowInfo && (this.GridControl.MasterTemplate.IsSelfReference || this.mouseDownRowHierarchyLevel == gridTraverser.Current.HierarchyLevel))
          gridViewRowInfoList.Add(gridTraverser.Current);
      }
      return gridViewRowInfoList;
    }

    private List<GridViewColumn> GetOrderedColumns()
    {
      GridTableElement tableElementAtPoint = this.GetTableElementAtPoint(this.mouseDownLocation);
      List<GridViewColumn> gridViewColumnList1 = new List<GridViewColumn>();
      List<GridViewColumn> gridViewColumnList2 = new List<GridViewColumn>();
      List<GridViewColumn> gridViewColumnList3 = new List<GridViewColumn>();
      IGridRowLayout rowLayout = tableElementAtPoint.ViewElement.RowLayout;
      for (int index = 0; index < rowLayout.RenderColumns.Count; ++index)
      {
        GridViewColumn renderColumn = rowLayout.RenderColumns[index];
        if (renderColumn.IsVisible)
        {
          switch (renderColumn.PinPosition)
          {
            case PinnedColumnPosition.Left:
              gridViewColumnList1.Add(renderColumn);
              continue;
            case PinnedColumnPosition.Right:
              gridViewColumnList3.Add(renderColumn);
              continue;
            case PinnedColumnPosition.None:
              gridViewColumnList2.Add(renderColumn);
              continue;
            default:
              continue;
          }
        }
      }
      List<GridViewColumn> gridViewColumnList4 = new List<GridViewColumn>();
      gridViewColumnList4.AddRange((IEnumerable<GridViewColumn>) gridViewColumnList1);
      gridViewColumnList4.AddRange((IEnumerable<GridViewColumn>) gridViewColumnList2);
      gridViewColumnList4.AddRange((IEnumerable<GridViewColumn>) gridViewColumnList3);
      return gridViewColumnList4;
    }

    private bool DoMouseSelection(GridCellElement currentCell, Point currentLocation)
    {
      if (!this.MasterTemplate.MultiSelect || this.GridViewElement.Template.AllowRowReorder || (this.GridViewElement.GridControl.EnableKineticScrolling || this.orderedColumns == null) || this.orderedRows == null || new Rectangle(this.mouseDownLocation.X - SystemInformation.DragSize.Width / 2, this.mouseDownLocation.Y - SystemInformation.DragSize.Height / 2, SystemInformation.DragSize.Width, SystemInformation.DragSize.Height).Contains(currentLocation))
        return false;
      GridTableElement currentView = this.GridViewElement.CurrentView as GridTableElement;
      if (currentView == null)
        return false;
      if (this.selectionStartedOnAPinnedColumn && this.GetViewportBounds(currentView).Contains(currentLocation))
      {
        if (this.mouseDownOnLeftPinnedColumn)
        {
          currentView.HScrollBar.Value = currentView.HScrollBar.Minimum;
          this.mouseDownOnLeftPinnedColumn = false;
        }
        if (this.mouseDownOnRightPinnedColumn)
        {
          currentView.HScrollBar.Value = currentView.HScrollBar.Maximum - currentView.HScrollBar.LargeChange + 1;
          this.mouseDownOnRightPinnedColumn = false;
        }
        this.selectionStartedOnAPinnedColumn = false;
      }
      if (this.selectionStartedOnAPinnedRow && this.GetViewportBounds(currentView).Contains(currentLocation))
      {
        if (this.mouseDownOnTopPinnedRow)
        {
          currentView.VScrollBar.Value = currentView.VScrollBar.Minimum;
          this.mouseDownOnTopPinnedRow = false;
        }
        if (this.mouseDownOnBottomPinnedRow)
        {
          currentView.VScrollBar.Value = currentView.VScrollBar.Maximum - currentView.VScrollBar.LargeChange + 1;
          this.mouseDownOnBottomPinnedRow = false;
        }
        this.selectionStartedOnAPinnedRow = false;
      }
      if (currentCell.RowInfo is GridViewDataRowInfo)
      {
        if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
          this.DoMultiFullRowSelect(currentCell, currentLocation);
        else
          this.DoMultiCellSelect(currentCell, currentLocation);
      }
      return true;
    }

    private void DoMultiCellSelect(GridCellElement currentCell, Point currentLocation)
    {
      int rowIndex = this.GetRowIndex(this.RootGridBehavior.RowAtPoint.RowInfo);
      int num1 = 0;
      GridTableElement currentView = this.GridViewElement.CurrentView as GridTableElement;
      bool flag1 = this.orderedColumns[0] is GridViewRowHeaderColumn;
      GridViewColumn gridViewColumn = this.anchorColumnIndex != 0 || !flag1 ? this.RootGridBehavior.CellAtPoint.ColumnInfo : this.orderedColumns[this.orderedColumns.Count - 1];
      if (this.RootGridBehavior.CellAtPoint.ColumnInfo is GridViewRowHeaderColumn && this.anchorColumnIndex != 0)
        gridViewColumn = this.GetFirstScrollableColumn(currentView);
      if (gridViewColumn != null)
        num1 = this.orderedColumns.IndexOf(gridViewColumn);
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      int num2 = Math.Min(this.anchorRowIndex, rowIndex);
      int num3 = Math.Max(this.anchorRowIndex, rowIndex);
      for (int index = num2; index < num3; ++index)
      {
        if (index >= 0)
          gridViewRowInfoList.Add(this.orderedRows[index]);
      }
      int columnStarIndex = Math.Min(this.anchorColumnIndex, num1);
      int columnEndIndex = Math.Max(this.anchorColumnIndex, num1);
      GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs((IEnumerable<GridViewRowInfo>) gridViewRowInfoList, columnStarIndex, columnEndIndex);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
      if (args.Cancel)
        return;
      bool flag2 = this.IsPressedShift || this.IsPressedControl;
      this.MasterTemplate.SelectedCells.BeginUpdate();
      this.GridViewElement.CurrentView.BeginUpdate();
      int count = this.MasterTemplate.SelectedCells.Count;
      bool notifyUpdates = this.ProcessCellSelection(rowIndex, num1);
      if (flag2)
        notifyUpdates = count != this.MasterTemplate.SelectedCells.Count;
      this.GridViewElement.CurrentView.EndUpdate(false);
      this.MasterTemplate.SelectedCells.EndUpdate(notifyUpdates);
    }

    private void DoMultiFullRowSelect(GridCellElement currentCell, Point currentLocation)
    {
      bool notifyUpdates = false;
      if (!this.IsPressedShift)
      {
        int num = this.IsPressedControl ? 1 : 0;
      }
      GridTableElement currentView = this.GridViewElement.CurrentView as GridTableElement;
      int rowUnderMouseIndex = this.orderedRows.IndexOf(this.RootGridBehavior.RowAtPoint.RowInfo);
      if (rowUnderMouseIndex != this.currentRowIndex)
      {
        GridViewSelectionCancelEventArgs args = new GridViewSelectionCancelEventArgs((GridCellElement) null);
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewSelectionCancelEventArgs>(EventDispatcher.SelectionChanging, (object) this, args);
        if (args.Cancel)
          return;
      }
      currentView.BeginUpdate();
      this.MasterTemplate.SelectedRows.BeginUpdate();
      int count = this.MasterTemplate.SelectedRows.Count;
      this.ProcessFullRowSelection(rowUnderMouseIndex);
      if (count != this.MasterTemplate.SelectedRows.Count)
        notifyUpdates = true;
      currentView.EndUpdate(false);
      this.MasterTemplate.SelectedRows.EndUpdate(notifyUpdates);
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      GridTableElement tableElementAtPoint = this.GetTableElementAtPoint(Control.MousePosition);
      if (this.GridControl == null || tableElementAtPoint == null)
        return;
      Point client = this.GridControl.PointToClient(Control.MousePosition);
      Rectangle viewportBounds = this.GetViewportBounds(tableElementAtPoint);
      if (viewportBounds.Contains(client) || (viewportBounds.Size.IsEmpty || !viewportBounds.Contains(this.mouseDownLocation)))
        this.scrollTimer.Enabled = false;
      else
        this.ExtendSelectionOnMouseMove(tableElementAtPoint, viewportBounds, this.mouseMoveLocation);
    }

    private Rectangle GetViewportBounds(GridTableElement tableElement)
    {
      ScrollableRowsContainerElement scrollableRows = tableElement.ViewElement.ScrollableRows;
      Rectangle boundingRectangle = tableElement.ViewElement.ScrollableRows.ControlBoundingRectangle;
      for (int index = 0; index < scrollableRows.Children.Count; ++index)
      {
        GridVirtualizedRowElement child = scrollableRows.Children[index] as GridVirtualizedRowElement;
        if (child != null)
        {
          VirtualizedColumnContainer scrollableColumns = child.ScrollableColumns;
          boundingRectangle.X = this.GridViewElement.RightToLeft ? child.RightPinnedColumns.ControlBoundingRectangle.Right : child.LeftPinnedColumns.ControlBoundingRectangle.Right;
          boundingRectangle.Width = scrollableColumns.ControlBoundingRectangle.Width;
          break;
        }
      }
      return boundingRectangle;
    }

    private bool ExtendSelectionOnMouseMove(
      GridTableElement tableElement,
      Rectangle viewportBounds,
      Point location)
    {
      if (!this.GridControl.MultiSelect)
        return false;
      bool flag1 = true;
      bool flag2 = true;
      if (!this.ExtendSelectionDown(tableElement, viewportBounds, location) && !this.ExtendSelectionUp(tableElement, viewportBounds, location))
        flag2 = false;
      if (!this.ExtendSelectionRight(tableElement, viewportBounds, location) && !this.ExtendSelectionLeft(tableElement, viewportBounds, location))
        flag1 = false;
      if (!flag1)
        return flag2;
      return true;
    }

    private bool ExtendSelectionRight(
      GridTableElement tableElement,
      Rectangle viewportBounds,
      Point location)
    {
      if (location.X <= viewportBounds.Right || this.mouseDownOnRightPinnedColumn)
        return false;
      RadScrollBarElement hscrollBar = tableElement.HScrollBar;
      if (hscrollBar.Value >= hscrollBar.Maximum - hscrollBar.LargeChange + 1)
        return false;
      int num1 = Math.Min(viewportBounds.Width, location.X - viewportBounds.Right);
      hscrollBar.Value = this.ClampValue(hscrollBar.Value + num1 * (this.GridViewElement.RightToLeft ? -1 : 1), hscrollBar.Minimum, hscrollBar.Maximum - hscrollBar.LargeChange + 1);
      tableElement.UpdateLayout();
      GridViewColumn scrollableColumn = this.GetLastScrollableColumn(tableElement);
      if (!this.IsPressedShift)
      {
        int num2 = this.IsPressedControl ? 1 : 0;
      }
      if (scrollableColumn != null && this.GridViewElement.Template.SelectionMode == GridViewSelectionMode.CellSelect)
      {
        this.GridViewElement.CurrentView.BeginUpdate();
        this.MasterTemplate.SelectedCells.BeginUpdate();
        this.ProcessCellSelection(this.currentRowIndex, this.orderedColumns.IndexOf(scrollableColumn));
        this.GridViewElement.CurrentView.EndUpdate(false);
        this.MasterTemplate.SelectedCells.EndUpdate(true);
      }
      return true;
    }

    private bool ExtendSelectionLeft(
      GridTableElement tableElement,
      Rectangle viewportBounds,
      Point location)
    {
      if (location.X >= viewportBounds.X || this.mouseDownOnLeftPinnedColumn)
        return false;
      RadScrollBarElement hscrollBar = tableElement.HScrollBar;
      if (hscrollBar.Value == hscrollBar.Minimum)
        return false;
      int num1 = Math.Min(viewportBounds.Width, viewportBounds.X - location.X);
      hscrollBar.Value = this.ClampValue(hscrollBar.Value - num1 * (this.GridViewElement.RightToLeft ? -1 : 1), hscrollBar.Minimum, hscrollBar.Maximum - hscrollBar.LargeChange + 1);
      tableElement.UpdateLayout();
      GridViewColumn scrollableColumn = this.GetFirstScrollableColumn(tableElement);
      if (!this.IsPressedShift)
      {
        int num2 = this.IsPressedControl ? 1 : 0;
      }
      if (scrollableColumn != null && this.GridViewElement.Template.SelectionMode == GridViewSelectionMode.CellSelect)
      {
        this.GridViewElement.CurrentView.BeginUpdate();
        this.MasterTemplate.SelectedCells.BeginUpdate();
        this.ProcessCellSelection(this.currentRowIndex, this.orderedColumns.IndexOf(scrollableColumn));
        this.GridViewElement.CurrentView.EndUpdate(false);
        this.MasterTemplate.SelectedCells.EndUpdate(true);
      }
      return true;
    }

    private bool ExtendSelectionDown(
      GridTableElement tableElement,
      Rectangle viewportBounds,
      Point location)
    {
      if (location.Y <= viewportBounds.Bottom || this.mouseDownOnBottomPinnedRow)
        return false;
      RadScrollBarElement scrollbar = this.GetScrollbar(tableElement);
      if (scrollbar.Value >= scrollbar.Maximum - scrollbar.LargeChange + 1)
        return false;
      int num1 = Math.Min(viewportBounds.Height, location.Y - viewportBounds.Bottom);
      scrollbar.Value = this.ClampValue(scrollbar.Value + num1, scrollbar.Minimum, scrollbar.Maximum - scrollbar.LargeChange + 1);
      tableElement.UpdateLayout();
      GridRowElement scrollableRowElement = this.GetLastScrollableRowElement(tableElement);
      if (!this.IsPressedShift)
      {
        int num2 = this.IsPressedControl ? 1 : 0;
      }
      if (scrollableRowElement != null)
      {
        if (this.GridViewElement.Template.SelectionMode == GridViewSelectionMode.CellSelect)
        {
          this.GridViewElement.CurrentView.BeginUpdate();
          this.MasterTemplate.SelectedCells.BeginUpdate();
          this.ProcessCellSelection(this.GetRowIndex(scrollableRowElement.RowInfo), this.currentColumnIndex);
          this.GridViewElement.CurrentView.EndUpdate(false);
          this.MasterTemplate.SelectedCells.EndUpdate(true);
        }
        else
          this.ProcessFullRowSelection(this.GetRowIndex(scrollableRowElement.RowInfo));
      }
      return true;
    }

    private bool ExtendSelectionUp(
      GridTableElement tableElement,
      Rectangle viewportBounds,
      Point location)
    {
      if (location.Y >= viewportBounds.Top || this.mouseDownOnTopPinnedRow)
        return false;
      RadScrollBarElement scrollbar = this.GetScrollbar(tableElement);
      if (scrollbar.Value == scrollbar.Minimum)
        return false;
      int num1 = Math.Min(viewportBounds.Height, viewportBounds.Top - location.Y);
      scrollbar.Value = this.ClampValue(scrollbar.Value - num1, scrollbar.Minimum, scrollbar.Maximum - scrollbar.LargeChange + 1);
      tableElement.UpdateLayout();
      GridRowElement scrollableRowElement = this.GetFirstScrollableRowElement(tableElement);
      if (!this.IsPressedShift)
      {
        int num2 = this.IsPressedControl ? 1 : 0;
      }
      if (scrollableRowElement != null)
      {
        if (this.GridViewElement.Template.SelectionMode == GridViewSelectionMode.CellSelect)
        {
          this.GridViewElement.CurrentView.BeginUpdate();
          this.MasterTemplate.SelectedCells.BeginUpdate();
          this.ProcessCellSelection(this.GetRowIndex(scrollableRowElement.RowInfo), this.currentColumnIndex);
          this.GridViewElement.CurrentView.EndUpdate(false);
          this.MasterTemplate.SelectedCells.EndUpdate(true);
        }
        else
          this.ProcessFullRowSelection(this.GetRowIndex(scrollableRowElement.RowInfo));
      }
      return true;
    }

    protected virtual void ResizeRow(Point currentLocation)
    {
      int num = this.rowToResizeInitialHeight + (currentLocation.Y - this.mouseDownLocation.Y);
      GridViewRowInfo rowInfo = this.rowToResize.RowInfo;
      if (rowInfo == null || num < rowInfo.MinHeight)
        return;
      rowInfo.Height = num;
    }

    protected virtual bool ShowSizeNSCursort(Point currentLocation)
    {
      GridRowElement rowAtPoint = this.GetRowAtPoint(currentLocation);
      if (this.CanResizeRow(currentLocation, rowAtPoint))
      {
        if (this.RootGridBehavior.OriginalCursor == (Cursor) null)
          this.RootGridBehavior.OriginalCursor = this.GridViewElement.ElementTree.Control.Cursor;
        this.GridViewElement.ElementTree.Control.Cursor = Cursors.SizeNS;
        return true;
      }
      GridHyperlinkCellElement cellAtPoint = this.GetCellAtPoint(currentLocation) as GridHyperlinkCellElement;
      if (cellAtPoint != null)
      {
        GridViewHyperlinkColumn columnInfo = cellAtPoint.ColumnInfo as GridViewHyperlinkColumn;
        RadElement elementAtPoint = GridVisualElement.GetElementAtPoint<RadElement>((RadElementTree) this.GridViewElement.ElementTree, currentLocation);
        if (columnInfo != null && columnInfo.HyperlinkOpenArea == HyperlinkOpenArea.Cell || elementAtPoint is GridHyperlinkCellContentElement)
        {
          if (this.RootGridBehavior.OriginalCursor == (Cursor) null)
            this.RootGridBehavior.OriginalCursor = this.GridViewElement.ElementTree.Control.Cursor;
          this.GridViewElement.ElementTree.Control.Cursor = Cursors.Hand;
          return true;
        }
      }
      this.ResetControlCursor();
      return false;
    }

    public virtual bool CanResizeRow(Point currentLocation, GridRowElement rowElement)
    {
      if (rowElement == null)
        return false;
      Rectangle boundingRectangle = rowElement.ControlBoundingRectangle;
      if (currentLocation.Y < boundingRectangle.Bottom - 2 || currentLocation.Y > boundingRectangle.Bottom + 2)
        return false;
      GridViewRowInfo rowInfo = rowElement.RowInfo;
      if (rowInfo.AllowResize && rowInfo.ViewTemplate != null && rowInfo.ViewTemplate.AllowRowResize)
        return !this.GridViewElement.AutoSizeRows;
      return false;
    }

    protected void ResetControlCursor()
    {
      if (!(this.RootGridBehavior.OriginalCursor != (Cursor) null))
        return;
      this.GridViewElement.ElementTree.Control.Cursor = this.RootGridBehavior.OriginalCursor;
      this.RootGridBehavior.OriginalCursor = (Cursor) null;
    }

    protected GridExpanderItem GetExpanderPrimitive(Point point)
    {
      return GridVisualElement.GetElementAtPoint<GridExpanderItem>((RadElementTree) this.GridViewElement.ElementTree, point);
    }

    protected GridCellElement GetCellAtPoint(Point point)
    {
      return GridVisualElement.GetElementAtPoint<GridCellElement>((RadElementTree) this.GridViewElement.ElementTree, point);
    }

    protected GridRowElement GetRowAtPoint(Point point)
    {
      return GridVisualElement.GetElementAtPoint<GridRowElement>((RadElementTree) this.GridViewElement.ElementTree, point);
    }

    protected GridTableElement GetTableElementAtPoint(Point point)
    {
      if (this.GridControl == null)
        return (GridTableElement) null;
      RadElement elementAtPoint = this.GridControl.ElementTree.GetElementAtPoint(point);
      GridTableElement gridTableElement = elementAtPoint as GridTableElement;
      if (gridTableElement == null)
      {
        if (elementAtPoint != null)
          gridTableElement = elementAtPoint.FindAncestor<GridTableElement>();
        if (gridTableElement == null)
          gridTableElement = this.GridControl.TableElement;
      }
      return gridTableElement;
    }

    protected virtual bool ValidateOnUserInput(MouseEventArgs e)
    {
      if (e != null && (this.GetCellAtPoint(e.Location) is GridDataCellElement || this.GetRowAtPoint(e.Location) is GridDataRowElement) || !this.GridViewElement.IsInEditMode)
        return true;
      return this.GridViewElement.EndEdit();
    }

    protected virtual bool ResetFieldValues()
    {
      this.GridControl.Capture = false;
      this.rowToResizeInitialHeight = 0;
      this.ResetControlCursor();
      this.mouseDownLocation = Point.Empty;
      this.anchorRowIndex = -1;
      this.currentRowIndex = -1;
      this.anchorColumnIndex = -1;
      this.currentColumnIndex = -1;
      this.enterEditMode = false;
      this.wasInEditMode = false;
      if (this.rowToResize != null)
      {
        this.rowToResize = (GridRowElement) null;
        return true;
      }
      this.GridViewElement.Invalidate();
      return false;
    }

    protected virtual bool SelectNextControl(bool forward)
    {
      if (this.GridViewElement.IsInEditMode && !this.GridViewElement.EndEdit())
        return !this.GridControl.StandardTab;
      this.GridControl.FindForm()?.SelectNextControl(this.GridViewElement.ElementTree.Control, forward, true, true, true);
      return true;
    }

    protected bool IsOnFirstCell()
    {
      GridViewRowInfo currentRow = this.GridViewElement.CurrentRow;
      bool flag1 = new ViewInfoTraverser(currentRow.ViewInfo).IsFirstRow(currentRow);
      if (flag1 && currentRow != null && currentRow.Parent is GridViewRowInfo)
        flag1 = false;
      bool flag2 = this.GridViewElement.Navigator.IsFirstColumn(this.GridViewElement.CurrentColumn);
      if (flag1)
        return flag2;
      return false;
    }

    protected bool IsOnLastCell()
    {
      GridViewRowInfo currentRow = this.GridViewElement.CurrentRow;
      bool flag1 = new ViewInfoTraverser(currentRow.ViewInfo).IsLastRow(currentRow);
      if (flag1 && currentRow != null)
      {
        for (GridViewRowInfo parent = currentRow.Parent as GridViewRowInfo; parent != null && flag1; parent = parent.Parent as GridViewRowInfo)
          flag1 = new ViewInfoTraverser(parent.ViewInfo).IsLastRow(parent);
      }
      bool flag2 = this.GridViewElement.Navigator.IsLastColumn(this.GridViewElement.CurrentColumn);
      if (flag1)
        return flag2;
      return false;
    }

    protected virtual GridNavigationContext GetKeyboardNavigationContext(
      KeyEventArgs keys)
    {
      return new GridNavigationContext(GridNavigationInputType.Keyboard, MouseButtons.None, keys.KeyData);
    }

    protected virtual GridNavigationContext GetMouseNavigationContext(
      MouseEventArgs e)
    {
      return new GridNavigationContext(GridNavigationInputType.Mouse, e.Button, Control.ModifierKeys);
    }

    protected virtual bool CanEnterEditMode(GridViewRowInfo rowInfo)
    {
      return rowInfo.IsCurrent;
    }

    private bool ScrollToNextRow(ItemScroller<GridViewRowInfo> scroller, GridViewRowInfo item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      ITraverser<GridViewRowInfo> enumerator = scroller.Traverser.GetEnumerator() as ITraverser<GridViewRowInfo>;
      int num = 0;
      while (enumerator.MoveNext() && !object.Equals((object) enumerator.Current, (object) item))
        num += (int) scroller.ElementProvider.GetElementSize(enumerator.Current).Height;
      scroller.Scrollbar.Value = num;
      return true;
    }

    private bool CanEndEdit()
    {
      RadTextBoxEditor activeEditor = this.GridViewElement.ActiveEditor as RadTextBoxEditor;
      return activeEditor == null || !activeEditor.Multiline || !activeEditor.AcceptsReturn;
    }

    private bool EditorIsActive(GridCellElement cell, Point pt)
    {
      IEditableCell editableCell = cell as IEditableCell;
      if (editableCell == null)
        return false;
      GridDataCellElement gridDataCellElement = cell as GridDataCellElement;
      if (gridDataCellElement == null)
        return false;
      RadElement editorElement = (RadElement) gridDataCellElement.GetEditorElement(editableCell.Editor);
      return editorElement != null && editorElement.ControlBoundingRectangle.Contains(pt);
    }

    private int ClampValue(int value, int minimum, int maximum)
    {
      if (value < minimum)
        return minimum;
      if (maximum > 0 && value > maximum)
        return maximum;
      return value;
    }

    private GridRowElement GetFirstScrollableRowElement(GridTableElement tableElement)
    {
      if (tableElement.ViewElement.ScrollableRows.Children.Count < 1)
        return (GridRowElement) null;
      return (GridRowElement) tableElement.ViewElement.ScrollableRows.Children[0];
    }

    private GridRowElement GetLastScrollableRowElement(GridTableElement tableElement)
    {
      int count = tableElement.ViewElement.ScrollableRows.Children.Count;
      if (count > 0)
        return (GridRowElement) tableElement.ViewElement.ScrollableRows.Children[count - 1];
      return (GridRowElement) null;
    }

    private GridViewColumn GetFirstScrollableColumn(GridTableElement tableElement)
    {
      GridRowElement scrollableRowElement = this.GetFirstScrollableRowElement(tableElement);
      if (scrollableRowElement == null)
        return (GridViewColumn) null;
      for (int index = 0; index < scrollableRowElement.VisualCells.Count; ++index)
      {
        if (!scrollableRowElement.VisualCells[index].IsPinned && scrollableRowElement.VisualCells[index].ColumnInfo is GridViewDataColumn)
          return scrollableRowElement.VisualCells[index].ColumnInfo;
      }
      return (GridViewColumn) null;
    }

    private GridViewColumn GetLastScrollableColumn(GridTableElement tableElement)
    {
      GridRowElement scrollableRowElement = this.GetFirstScrollableRowElement(tableElement);
      if (scrollableRowElement == null)
        return (GridViewColumn) null;
      int count = scrollableRowElement.VisualCells.Count;
      if (count > 0)
        return scrollableRowElement.VisualCells[count - 1].ColumnInfo;
      return (GridViewColumn) null;
    }

    private RadScrollBarElement GetScrollbar(GridTableElement tableElement)
    {
      if (this.GridViewElement.UseScrollbarsInHierarchy)
        return tableElement.VScrollBar;
      return this.GridViewElement.TableElement.VScrollBar;
    }
  }
}
