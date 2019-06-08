// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseGridBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BaseGridBehavior : SplitGridBehavior, IDisposable
  {
    private IDictionary<System.Type, IGridBehavior> rowBehaviors;
    private IGridBehavior lockedBehavior;
    private IGridBehavior hoveredBehavior;
    private Cursor originalCursor;
    private GridRowBehavior defaultRowBehavior;
    private GridCellElement cellAtPoint;
    private RadScrollBarElement scrollBarAtPoint;
    private GridRowElement rowAtPoint;
    private Timer scrollTimer;
    private bool disposed;
    private int scrollDelta;
    private GridTableElement tableElement;
    private BaseGridBehavior.ScrollReason scrollReason;

    public BaseGridBehavior()
    {
      this.rowBehaviors = (IDictionary<System.Type, IGridBehavior>) new Dictionary<System.Type, IGridBehavior>();
      this.scrollReason = BaseGridBehavior.ScrollReason.MouseWheel;
      this.lockedBehavior = (IGridBehavior) null;
      this.originalCursor = (Cursor) null;
      this.scrollBarAtPoint = (RadScrollBarElement) null;
      this.defaultRowBehavior = new GridRowBehavior();
      this.RegisterBehavior(typeof (GridViewDataRowInfo), (IGridBehavior) new GridDataRowBehavior());
      this.RegisterBehavior(typeof (GridViewNewRowInfo), (IGridBehavior) new GridNewRowBehavior());
      this.RegisterBehavior(typeof (GridViewGroupRowInfo), (IGridBehavior) new GridGroupRowBehavior());
      this.RegisterBehavior(typeof (GridViewFilteringRowInfo), (IGridBehavior) new GridFilterRowBehavior());
      this.RegisterBehavior(typeof (GridViewTableHeaderRowInfo), (IGridBehavior) new GridHeaderRowBehavior());
      this.RegisterBehavior(typeof (GridViewSearchRowInfo), (IGridBehavior) new GridSearchRowBehavior());
      this.RegisterBehavior(typeof (GridViewHierarchyRowInfo), (IGridBehavior) new GridHierarchyRowBehavior());
      this.RegisterBehavior(typeof (GridViewDetailsRowInfo), (IGridBehavior) new GridDetailViewRowBehavior());
      this.scrollTimer = new Timer();
      this.scrollTimer.Interval = 10;
      this.scrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
    }

    public Cursor OriginalCursor
    {
      get
      {
        return this.originalCursor;
      }
      set
      {
        this.originalCursor = value;
      }
    }

    public IGridBehavior LockedBehavior
    {
      get
      {
        return this.lockedBehavior;
      }
    }

    public virtual IGridBehavior DefaultRowBehavior
    {
      get
      {
        return (IGridBehavior) this.defaultRowBehavior;
      }
    }

    public GridCellElement CellAtPoint
    {
      get
      {
        return this.cellAtPoint;
      }
    }

    public GridRowElement RowAtPoint
    {
      get
      {
        return this.rowAtPoint;
      }
    }

    public virtual void RegisterBehavior(System.Type rowType, IGridBehavior rowBehavior)
    {
      IGridBehavior gridBehavior = (IGridBehavior) null;
      this.rowBehaviors.TryGetValue(rowType, out gridBehavior);
      if (gridBehavior == null)
      {
        this.rowBehaviors.Add(rowType, rowBehavior);
        if (this.GridViewElement == null)
          return;
        rowBehavior.Initialize(this.GridViewElement);
      }
      else
      {
        this.rowBehaviors[rowType] = rowBehavior;
        if (this.GridViewElement == null)
          return;
        rowBehavior.Initialize(this.GridViewElement);
      }
    }

    public virtual bool UnregisterBehavior(System.Type rowType)
    {
      IGridBehavior gridBehavior = (IGridBehavior) null;
      this.rowBehaviors.TryGetValue(rowType, out gridBehavior);
      if (gridBehavior == null)
        return false;
      this.rowBehaviors.Remove(rowType);
      return true;
    }

    public virtual void LockBehavior(IGridBehavior behavior)
    {
      if (behavior == null)
        throw new ArgumentNullException(nameof (behavior));
      if (this.lockedBehavior != null && this.lockedBehavior != behavior)
        throw new InvalidOperationException("There is already locked behavior of type " + this.lockedBehavior.GetType().FullName);
      this.lockedBehavior = behavior;
    }

    public virtual void UnlockBehavior(IGridBehavior behavior)
    {
      if (this.lockedBehavior != behavior)
        return;
      this.lockedBehavior = (IGridBehavior) null;
    }

    public virtual IGridBehavior GetBehavior(System.Type rowType)
    {
      IGridBehavior lockedBehavior = this.lockedBehavior;
      if (lockedBehavior == null)
        this.rowBehaviors.TryGetValue(rowType, out lockedBehavior);
      return lockedBehavior ?? this.DefaultRowBehavior;
    }

    protected virtual IGridBehavior GetCurrentRowBehavior()
    {
      if (this.GridViewElement.CurrentRow != null)
        return this.GetBehavior(this.GridViewElement.CurrentRow.GetType());
      return (IGridBehavior) null;
    }

    protected virtual IGridBehavior GetRowBehaviorAtPoint(Point point)
    {
      this.rowAtPoint = GridVisualElement.GetElementAtPoint<GridRowElement>((RadElementTree) this.GridViewElement.ElementTree, point);
      this.cellAtPoint = GridVisualElement.GetElementAtPoint<GridCellElement>((RadElementTree) this.GridViewElement.ElementTree, point);
      if (this.lockedBehavior != null)
        return this.lockedBehavior;
      if (this.rowAtPoint != null)
        return this.GetBehavior(this.rowAtPoint.RowInfo.GetType());
      return this.GetCurrentRowBehavior();
    }

    protected virtual bool OnMouseDownLeft(MouseEventArgs e)
    {
      return false;
    }

    protected virtual bool OnMouseDownRight(MouseEventArgs e)
    {
      return false;
    }

    private GridTableElement ScrollableTableElement(bool wheelDown)
    {
      GridTableElement gridTableElement = this.GridViewElement.CurrentView as GridTableElement;
      RadScrollBarElement scrollBarElement = Control.ModifierKeys != Keys.Shift ? gridTableElement.VScrollBar : gridTableElement.HScrollBar;
      for (; gridTableElement != null; gridTableElement = gridTableElement.FindAncestor<GridTableElement>())
      {
        if (wheelDown)
        {
          int num = scrollBarElement.Maximum - scrollBarElement.LargeChange + 1;
          if (scrollBarElement.Value < num)
            return gridTableElement;
        }
        else if (scrollBarElement.Value > scrollBarElement.Minimum)
          return gridTableElement;
      }
      return gridTableElement;
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      if (this.scrollDelta == 0 || this.tableElement == null)
        return;
      RadScrollBarElement scrollBarElement = Control.ModifierKeys != Keys.Shift ? this.tableElement.VScrollBar : this.tableElement.HScrollBar;
      int num = scrollBarElement.Value + this.scrollDelta;
      if (num < scrollBarElement.Minimum)
        num = scrollBarElement.Minimum;
      else if (num > scrollBarElement.Maximum - scrollBarElement.LargeChange + 1)
        num = scrollBarElement.Maximum - scrollBarElement.LargeChange + 1;
      if (scrollBarElement.LargeChange > 0)
        scrollBarElement.Value = num;
      if (this.scrollReason == BaseGridBehavior.ScrollReason.PageUp)
        this.NavigateToPage(this.GetFirstScrollableRow(this.tableElement, true), Keys.None);
      else if (this.scrollReason == BaseGridBehavior.ScrollReason.PageDown)
      {
        this.NavigateToPage(this.GetLastScrollableRow(this.tableElement), Keys.None);
        this.GridViewElement.TableElement.RowScroller.UpdateScrollRange();
      }
      this.scrollReason = BaseGridBehavior.ScrollReason.MouseWheel;
      this.scrollDelta = 0;
      this.scrollTimer.Stop();
    }

    public override void Initialize(RadGridViewElement gridRootElement)
    {
      base.Initialize(gridRootElement);
      foreach (KeyValuePair<System.Type, IGridBehavior> rowBehavior in (IEnumerable<KeyValuePair<System.Type, IGridBehavior>>) this.rowBehaviors)
        rowBehavior.Value.Initialize(this.GridViewElement);
      if (this.defaultRowBehavior == null)
        return;
      this.defaultRowBehavior.Initialize(this.GridViewElement);
    }

    public override bool OnClick(EventArgs e)
    {
      IGridBehavior currentRowBehavior = this.GetCurrentRowBehavior();
      return currentRowBehavior != null && currentRowBehavior.OnClick(e);
    }

    public override bool OnDoubleClick(EventArgs e)
    {
      IGridBehavior currentRowBehavior = this.GetCurrentRowBehavior();
      return currentRowBehavior != null && currentRowBehavior.OnDoubleClick(e);
    }

    public override bool ProcessKey(KeyEventArgs keys)
    {
      IGridBehavior currentRowBehavior = this.GetCurrentRowBehavior();
      if (currentRowBehavior != null && currentRowBehavior.ProcessKey(keys))
        return true;
      switch (keys.KeyCode)
      {
        case Keys.Prior:
          return this.ProcessPageUpKey(keys);
        case Keys.Next:
          return this.ProcessPageDownKey(keys);
        case Keys.Apps:
          GridCellElement currentCell = (GridCellElement) this.GridViewElement.CurrentCell;
          if (currentCell != null)
          {
            Point control = currentCell.PointToControl(new Point(currentCell.Size.Width, currentCell.Size.Height));
            this.GridViewElement.ContextMenuManager.ShowContextMenu((IContextMenuProvider) currentCell, control);
          }
          return true;
        default:
          if (!keys.Control)
            return false;
          switch (keys.KeyCode)
          {
            case Keys.C:
              if (this.GridViewElement.Template.ClipboardCopyMode != GridViewClipboardCopyMode.Disable)
              {
                this.GridViewElement.Template.Copy();
                break;
              }
              break;
            case Keys.F:
              if (this.GridControl.AllowSearchRow)
              {
                if (!this.GridControl.MasterTemplate.MasterViewInfo.TableSearchRow.IsVisible)
                {
                  this.GridControl.MasterTemplate.MasterViewInfo.TableSearchRow.IsVisible = true;
                  this.GridViewElement.UpdateLayout();
                }
                this.GridViewElement.TableElement.FindDescendant<GridSearchCellElement>().SearchTextBox.Focus();
                break;
              }
              break;
            case Keys.V:
              if (!this.GridViewElement.Template.GridReadOnly && !this.GridViewElement.Template.ReadOnly && (this.GridControl.CurrentColumn != null && !this.GridControl.CurrentColumn.ReadOnly))
              {
                this.GridViewElement.Template.Paste();
                break;
              }
              break;
            case Keys.X:
              if (!this.GridViewElement.Template.GridReadOnly && !this.GridViewElement.Template.ReadOnly && (this.GridControl.CurrentColumn != null && !this.GridControl.CurrentColumn.ReadOnly) && this.GridViewElement.Template.ClipboardCutMode != GridViewClipboardCutMode.Disable)
              {
                this.GridViewElement.Template.Cut();
                break;
              }
              break;
          }
          return false;
      }
    }

    public override bool ProcessKeyDown(KeyEventArgs keys)
    {
      return this.ProcessKey(keys);
    }

    public override bool ProcessKeyUp(KeyEventArgs keys)
    {
      IGridBehavior currentRowBehavior = this.GetCurrentRowBehavior();
      return currentRowBehavior != null && currentRowBehavior.ProcessKeyUp(keys);
    }

    public override bool ProcessKeyPress(KeyPressEventArgs keys)
    {
      IGridBehavior currentRowBehavior = this.GetCurrentRowBehavior();
      return currentRowBehavior != null && currentRowBehavior.ProcessKeyPress(keys);
    }

    public override bool OnMouseDown(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && this.OnMouseDownLeft(e) || e.Button == MouseButtons.Right && this.OnMouseDownRight(e) || base.OnMouseDown(e))
        return true;
      this.scrollBarAtPoint = GridVisualElement.GetElementAtPoint<RadScrollBarElement>((RadElementTree) this.GridViewElement.ElementTree, e.Location);
      if (this.scrollBarAtPoint != null && this.scrollBarAtPoint.Parent is GridTableElement)
      {
        GridViewEditManager editorManager = this.GridControl.EditorManager;
        bool whenValidationFails = editorManager.CloseEditorWhenValidationFails;
        editorManager.CloseEditorWhenValidationFails = true;
        editorManager.CloseEditor();
        editorManager.CloseEditorWhenValidationFails = whenValidationFails;
        return false;
      }
      IGridBehavior rowBehaviorAtPoint = this.GetRowBehaviorAtPoint(e.Location);
      return rowBehaviorAtPoint != null && rowBehaviorAtPoint.OnMouseDown(e);
    }

    public override bool OnMouseUp(MouseEventArgs e)
    {
      if (base.OnMouseUp(e))
        return true;
      if (this.scrollBarAtPoint != null)
      {
        this.scrollBarAtPoint = (RadScrollBarElement) null;
        return false;
      }
      IGridBehavior rowBehaviorAtPoint = this.GetRowBehaviorAtPoint(e.Location);
      return rowBehaviorAtPoint != null && rowBehaviorAtPoint.OnMouseUp(e);
    }

    public override bool OnMouseDoubleClick(MouseEventArgs e)
    {
      IGridBehavior rowBehaviorAtPoint = this.GetRowBehaviorAtPoint(e.Location);
      return rowBehaviorAtPoint != null && rowBehaviorAtPoint.OnMouseDoubleClick(e);
    }

    public override bool OnMouseMove(MouseEventArgs e)
    {
      if (this.RowAtPoint == null && e.Button == MouseButtons.None && (Cursor.Current != Cursors.Default && this.OriginalCursor != (Cursor) null))
      {
        this.GridControl.Cursor = this.OriginalCursor;
        this.OriginalCursor = (Cursor) null;
      }
      if (this.GridViewElement.ElementTree.GetElementAtPoint(e.Location) is GroupPanelSizeGripElement)
      {
        this.GridControl.Cursor = Cursors.SizeNS;
        return true;
      }
      if (base.OnMouseMove(e))
        return true;
      if (this.scrollBarAtPoint != null)
        return false;
      IGridBehavior rowBehaviorAtPoint = this.GetRowBehaviorAtPoint(e.Location);
      if (this.hoveredBehavior != null && this.hoveredBehavior != rowBehaviorAtPoint)
      {
        this.hoveredBehavior.OnMouseLeave((EventArgs) e);
        this.hoveredBehavior = (IGridBehavior) null;
      }
      if (rowBehaviorAtPoint != null)
      {
        if (rowBehaviorAtPoint != this.hoveredBehavior)
        {
          this.hoveredBehavior = rowBehaviorAtPoint;
          this.hoveredBehavior.OnMouseEnter((EventArgs) e);
        }
        if (rowBehaviorAtPoint.OnMouseMove(e))
          return true;
      }
      return false;
    }

    public override bool OnMouseLeave(EventArgs e)
    {
      if (this.hoveredBehavior != null)
        return this.hoveredBehavior.OnMouseLeave(e);
      return false;
    }

    public override bool OnMouseWheel(MouseEventArgs e)
    {
      HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
      if (handledMouseEventArgs != null && handledMouseEventArgs.Handled)
        return false;
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int num2 = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      bool flag = Control.ModifierKeys == Keys.Shift;
      RadElement elementAtPoint = this.GridControl.ElementTree.GetElementAtPoint(e.Location);
      if (elementAtPoint != null)
      {
        GroupPanelElement ancestor = elementAtPoint.FindAncestor<GroupPanelElement>();
        if (ancestor != null)
        {
          RadScrollBarElement scrollBarElement = !flag ? ancestor.ScrollView.VScrollBar : ancestor.ScrollView.HScrollBar;
          int num3 = scrollBarElement.Value - num2 * scrollBarElement.SmallChange;
          if (num3 > scrollBarElement.Maximum - scrollBarElement.LargeChange + 1)
            num3 = scrollBarElement.Maximum - scrollBarElement.LargeChange + 1;
          if (num3 < scrollBarElement.Minimum)
            num3 = scrollBarElement.Minimum;
          scrollBarElement.Value = num3;
          return true;
        }
      }
      IGridBehavior rowBehaviorAtPoint = this.GetRowBehaviorAtPoint(e.Location);
      if (rowBehaviorAtPoint != null && rowBehaviorAtPoint.OnMouseWheel(e))
        return true;
      this.tableElement = (GridTableElement) null;
      if (this.GridViewElement.UseScrollbarsInHierarchy)
        this.tableElement = this.ScrollableTableElement(e.Delta < 0);
      else if (this.GridViewElement.SplitMode != RadGridViewSplitMode.None)
        this.tableElement = this.GetGridTableElementAtPoint(e.Location);
      if (this.tableElement == null)
        this.tableElement = this.GridViewElement.TableElement;
      if (this.tableElement != null && (!this.GridViewElement.IsInEditMode || this.GridViewElement.EndEdit()))
      {
        RadScrollBarElement scrollBarElement = !flag ? this.tableElement.VScrollBar : this.tableElement.HScrollBar;
        int num3 = scrollBarElement.Value - num2 * scrollBarElement.SmallChange;
        if (num3 > scrollBarElement.Maximum - scrollBarElement.LargeChange + 1)
          num3 = scrollBarElement.Maximum - scrollBarElement.LargeChange + 1;
        if (num3 < scrollBarElement.Minimum)
          num3 = 0;
        else if (num3 > scrollBarElement.Maximum)
          num3 = scrollBarElement.Maximum;
        if (num3 != scrollBarElement.Value)
        {
          if (this.scrollDelta == 0)
            this.scrollTimer.Start();
          this.scrollDelta += num3 - scrollBarElement.Value;
          if (handledMouseEventArgs != null)
            handledMouseEventArgs.Handled = true;
        }
      }
      return false;
    }

    public override bool OnContextMenu(MouseEventArgs e)
    {
      IGridBehavior currentRowBehavior = this.GetCurrentRowBehavior();
      return currentRowBehavior != null && currentRowBehavior.OnContextMenu(e);
    }

    protected virtual bool ProcessPageUpKey(KeyEventArgs keys)
    {
      this.tableElement = (GridTableElement) this.GridViewElement.CurrentView;
      if (this.tableElement.ViewElement.ScrollableRows.Children.Count == 0)
        return false;
      GridRowElement child = this.tableElement.ViewElement.ScrollableRows.Children[0] as GridRowElement;
      if (child == null)
        return false;
      if (this.scrollDelta == 0)
      {
        this.scrollReason = BaseGridBehavior.ScrollReason.PageUp;
        this.scrollTimer.Start();
      }
      this.scrollDelta -= this.tableElement.ViewElement.ScrollableRows.Size.Height + (child.Size.Height - (int) this.tableElement.ViewElement.ScrollableRows.ScrollOffset.Height + this.tableElement.RowSpacing);
      return true;
    }

    protected virtual bool ProcessPageDownKey(KeyEventArgs keys)
    {
      this.tableElement = (GridTableElement) this.GridViewElement.CurrentView;
      if (this.scrollDelta == 0)
      {
        this.scrollReason = BaseGridBehavior.ScrollReason.PageDown;
        this.scrollTimer.Start();
      }
      this.scrollDelta += this.tableElement.ViewElement.ScrollableRows.Size.Height + (int) this.tableElement.ViewElement.ScrollableRows.ScrollOffset.Height;
      return true;
    }

    protected virtual void NavigateToPage(GridViewRowInfo row, Keys keys)
    {
      IGridNavigator navigator = this.GridViewElement.Navigator;
      navigator.BeginSelection(new GridNavigationContext(GridNavigationInputType.Keyboard, MouseButtons.None, keys));
      navigator.SelectRow(row);
      navigator.EndSelection();
    }

    protected GridViewRowInfo GetLastScrollableRow(GridTableElement tableElement)
    {
      ScrollableRowsContainerElement scrollableRows = tableElement.ViewElement.ScrollableRows;
      GridTraverser enumerator = (GridTraverser) ((IEnumerable) tableElement.RowScroller).GetEnumerator();
      int index = 0;
      while (index < scrollableRows.Children.Count && (scrollableRows.Children[index].BoundingRectangle.Bottom <= scrollableRows.Size.Height && enumerator.MoveNext()))
        ++index;
      return enumerator.Current;
    }

    protected GridViewRowInfo GetFirstScrollableRow(
      GridTableElement tableElement,
      bool checkBounds)
    {
      ScrollableRowsContainerElement scrollableRows = tableElement.ViewElement.ScrollableRows;
      GridTraverser enumerator = (GridTraverser) ((IEnumerable) tableElement.RowScroller).GetEnumerator();
      if (enumerator.Current == null)
        enumerator.MoveNext();
      return enumerator.Current;
    }

    ~BaseGridBehavior()
    {
      this.Dispose(false);
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (this.scrollTimer != null)
      {
        this.scrollTimer.Dispose();
        this.scrollTimer = (Timer) null;
      }
      this.scrollBarAtPoint = (RadScrollBarElement) null;
      this.rowAtPoint = (GridRowElement) null;
      this.cellAtPoint = (GridCellElement) null;
      this.disposed = true;
    }

    protected enum ScrollReason
    {
      MouseWheel,
      PageUp,
      PageDown,
    }
  }
}
