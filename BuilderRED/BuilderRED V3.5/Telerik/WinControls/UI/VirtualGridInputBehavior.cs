// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridInputBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class VirtualGridInputBehavior
  {
    private int resizedRow = -1;
    private int resizedColumn = -1;
    private bool isSelecting;
    private RadVirtualGridElement gridElement;
    private VirtualGridViewInfo selectedViewInfo;
    private VirtualGridTableElement selectedTableElement;
    protected internal bool wasInEditMode;
    protected bool beginEdit;
    private int scrollDelta;
    private VirtualGridTableElement tableElement;
    private VirtualGridInputBehavior.ScrollReason scrollReason;
    private readonly System.Windows.Forms.Timer scrollTimer;
    private readonly System.Windows.Forms.Timer selectionTimer;
    private Cursor originalMouseCursor;
    private VirtualGridViewInfo resizedViewInfo;
    private Point lastMousePosition;
    private Point mouseDownPosition;
    private VirtualGridCellElement mouseDownCell;

    public VirtualGridInputBehavior(RadVirtualGridElement gridElement)
    {
      this.gridElement = gridElement;
      this.selectionTimer = new System.Windows.Forms.Timer();
      this.selectionTimer.Interval = 30;
      this.selectionTimer.Tick += new EventHandler(this.selectionTimer_Tick);
      this.scrollTimer = new System.Windows.Forms.Timer();
      this.scrollTimer.Interval = 30;
      this.scrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
    }

    public bool IsSelecting
    {
      get
      {
        return this.isSelecting;
      }
    }

    public bool IsResizing
    {
      get
      {
        return this.resizedViewInfo != null;
      }
    }

    public bool IsResizingColumn
    {
      get
      {
        return this.resizedColumn >= 0;
      }
    }

    public bool IsResizingRow
    {
      get
      {
        return this.resizedRow >= 0;
      }
    }

    public RadVirtualGridElement GridElement
    {
      get
      {
        return this.gridElement;
      }
    }

    public void SelectCell(int row, int column)
    {
      this.SelectCell(row, column, false, false, this.GridElement.MasterViewInfo);
    }

    public void SelectCell(int row, int column, VirtualGridViewInfo viewInfo)
    {
      this.SelectCell(row, column, false, false, viewInfo);
    }

    public void SelectCell(
      int row,
      int column,
      bool shift,
      bool ctrl,
      VirtualGridViewInfo viewInfo)
    {
      if (this.GridElement.IsInEditMode && this.GridElement.CurrentCell != null && (this.GridElement.CurrentCell.RowIndex == -2 && row != -2))
      {
        this.GridElement.EndEdit();
        this.GridElement.CommitNewRow();
      }
      if (this.GridElement.CurrentCell != null && this.GridElement.CurrentCell.RowIndex == -2 && row != -2)
        (this.GridElement.GetRowElement(-2, this.GridElement.GetTableElement(this.GridElement.CurrentCell.ViewInfo)) as VirtualGridNewRowElement)?.UpdateContentVisibility(false);
      this.GridElement.CurrentCell = new VirtualGridCellInfo(row, column, viewInfo);
      if (this.GridElement.CurrentCell != null && (this.GridElement.CurrentCell.RowIndex != row || this.GridElement.CurrentCell.ColumnIndex != column || this.GridElement.CurrentCell.ViewInfo != viewInfo))
        return;
      if (!shift || viewInfo != this.gridElement.Selection.CurrentViewInfo)
      {
        List<VirtualGridRowElement> virtualGridRowElementList = new List<VirtualGridRowElement>();
        if (this.GridElement.Selection.HasSelection && !ctrl)
        {
          foreach (SelectionRegion selectedRegion in this.GridElement.Selection.SelectedRegions)
          {
            VirtualGridTableElement tableElement = this.GridElement.GetTableElement(selectedRegion.ViewInfo);
            if (tableElement != null)
            {
              for (int top = selectedRegion.Top; top <= selectedRegion.Bottom; ++top)
              {
                VirtualGridRowElement rowElement = tableElement.ViewElement.GetRowElement(top);
                if (rowElement != null)
                  virtualGridRowElementList.Add(rowElement);
              }
            }
          }
        }
        this.gridElement.Selection.BeginSelection(row, column, viewInfo, ctrl);
        foreach (VirtualGridRowElement virtualGridRowElement in virtualGridRowElementList)
          virtualGridRowElement.Synchronize(false);
      }
      else
        this.gridElement.Selection.ExtendCurrentRegion(row, column);
    }

    protected virtual bool SelectNextControl(bool forward)
    {
      if (this.GridElement.IsInEditMode && !this.GridElement.EndEdit())
        return !this.GridElement.StandardTab;
      this.GridElement.ElementTree.Control.FindForm()?.SelectNextControl(this.GridElement.ElementTree.Control, forward, true, true, true);
      return true;
    }

    protected int GetLastScrollableRow(VirtualGridTableElement tableElement)
    {
      ScrollableVirtualRowsContainer scrollableRows = this.GridElement.TableElement.ViewElement.ScrollableRows;
      VirtualGridTraverser enumerator = (VirtualGridTraverser) ((IEnumerable) tableElement.RowScroller).GetEnumerator();
      int index = 0;
      while (index < scrollableRows.Children.Count && (scrollableRows.Children[index].BoundingRectangle.Bottom <= scrollableRows.Size.Height && enumerator.MoveNext()))
        ++index;
      return enumerator.Current;
    }

    protected int GetFirstScrollableRow(VirtualGridTableElement tableElement)
    {
      ScrollableVirtualRowsContainer scrollableRows = this.GridElement.TableElement.ViewElement.ScrollableRows;
      VirtualGridTraverser enumerator = (VirtualGridTraverser) ((IEnumerable) tableElement.RowScroller).GetEnumerator();
      if (enumerator.Current < 0)
        enumerator.MoveNext();
      return enumerator.Current;
    }

    private int GetItemToPoint(VirtualGridItemScroller itemScroller, int offset)
    {
      int num1 = -itemScroller.ScrollOffset;
      int num2 = 0;
      foreach (int num3 in (IEnumerable) itemScroller)
      {
        int scrollHeight = itemScroller.GetScrollHeight(num3);
        if (num1 + scrollHeight > offset)
          return num3;
        num2 = num3;
        num1 += scrollHeight;
      }
      return num2;
    }

    private int GetFirstVisibleItem(VirtualGridItemScroller itemScroller)
    {
      IEnumerator enumerator = ((IEnumerable) itemScroller).GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
          return (int) enumerator.Current;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return 0;
    }

    public virtual bool HandleMouseDown(MouseEventArgs args)
    {
      RadElement elementAtPoint = this.gridElement.ElementTree.GetElementAtPoint(args.Location);
      VirtualGridCellElement virtualGridCellElement = elementAtPoint as VirtualGridCellElement;
      VirtualGridNewRowElement gridNewRowElement = elementAtPoint as VirtualGridNewRowElement;
      VirtualGridHeaderCellElement headerCellElement = virtualGridCellElement as VirtualGridHeaderCellElement;
      VirtualGridExpanderItem gridExpanderItem = elementAtPoint as VirtualGridExpanderItem;
      this.mouseDownCell = virtualGridCellElement;
      this.mouseDownPosition = args.Location;
      if (gridExpanderItem != null && args.Button == MouseButtons.Right)
        virtualGridCellElement = gridExpanderItem.Parent as VirtualGridCellElement;
      if (args.Button == MouseButtons.Left)
      {
        if (virtualGridCellElement is VirtualGridFilterCellElement)
        {
          this.SelectCell(virtualGridCellElement.RowIndex, virtualGridCellElement.ColumnIndex, false, false, virtualGridCellElement.ViewInfo);
          this.GridElement.BeginEdit(virtualGridCellElement);
        }
        else if (virtualGridCellElement != null && !(virtualGridCellElement is VirtualGridHeaderCellElement))
        {
          if (virtualGridCellElement.IsInResizeLocation(args.Location))
          {
            this.GridElement.Capture = true;
            this.resizedRow = virtualGridCellElement.RowIndex;
            this.resizedViewInfo = virtualGridCellElement.ViewInfo;
            this.lastMousePosition = args.Location;
            if (this.resizedRow > 0 && virtualGridCellElement.ControlBoundingRectangle.Y + 2 >= args.Y)
              --this.resizedRow;
          }
          else
          {
            this.isSelecting = true;
            this.GridElement.Capture = true;
            this.selectedViewInfo = virtualGridCellElement.ViewInfo;
            this.selectedTableElement = virtualGridCellElement.TableElement;
            bool shift = (Control.ModifierKeys & Keys.Shift) != Keys.None && this.GridElement.Selection.Multiselect;
            bool ctrl = (Control.ModifierKeys & Keys.Control) != Keys.None && this.GridElement.Selection.Multiselect;
            VirtualGridCellInfo currentCell = this.GridElement.CurrentCell;
            bool flag1 = currentCell != null && currentCell.RowIndex == virtualGridCellElement.RowIndex && currentCell.ColumnIndex == virtualGridCellElement.ColumnIndex && currentCell.ViewInfo == virtualGridCellElement.ViewInfo;
            bool isInEditMode = this.GridElement.IsInEditMode;
            int column = virtualGridCellElement.ColumnIndex;
            if (column < 0)
              column = 0;
            this.SelectCell(virtualGridCellElement.RowIndex, column, shift, ctrl, virtualGridCellElement.ViewInfo);
            bool flag2 = this.GridElement.CurrentCell != null && virtualGridCellElement.RowIndex == this.GridElement.CurrentCell.RowIndex && virtualGridCellElement.ColumnIndex == this.GridElement.CurrentCell.ColumnIndex && virtualGridCellElement.ViewInfo == this.GridElement.CurrentCell.ViewInfo;
            if (flag1 || flag2 && isInEditMode || virtualGridCellElement is VirtualGridNewCellElement)
              this.beginEdit = currentCell.ColumnIndex >= 0;
          }
        }
        else if (gridNewRowElement != null)
        {
          foreach (VirtualGridCellElement cellElement in gridNewRowElement.GetCellElements())
          {
            if (cellElement.ControlBoundingRectangle.Contains(args.Location))
            {
              this.SelectCell(cellElement.RowIndex, cellElement.ColumnIndex, false, false, cellElement.ViewInfo);
              this.beginEdit = cellElement.ColumnIndex >= 0;
              break;
            }
          }
        }
        else if (headerCellElement != null)
        {
          this.GridElement.EndEdit();
          if (headerCellElement.IsInResizeLocation(args.Location))
          {
            this.GridElement.Capture = true;
            this.resizedColumn = headerCellElement.ColumnIndex;
            this.resizedViewInfo = headerCellElement.ViewInfo;
            this.lastMousePosition = args.Location;
            if (!headerCellElement.TableElement.RightToLeft && this.resizedColumn > 0 && headerCellElement.ControlBoundingRectangle.X + 4 >= args.X || headerCellElement.TableElement.RightToLeft && this.resizedColumn < headerCellElement.TableElement.ColumnCount - 1 && headerCellElement.ControlBoundingRectangle.Right - 4 <= args.X)
              --this.resizedColumn;
            headerCellElement.TableElement.ColumnLayout.StartColumnResize(this.resizedColumn);
          }
        }
        else
        {
          if (elementAtPoint != null)
            virtualGridCellElement = elementAtPoint.FindAncestor<VirtualGridCellElement>();
          if (virtualGridCellElement == null || virtualGridCellElement.Editor == null)
            this.GridElement.EndEdit();
        }
      }
      else if (args.Button == MouseButtons.Right)
      {
        if (gridNewRowElement != null)
        {
          foreach (VirtualGridCellElement cellElement in gridNewRowElement.GetCellElements())
          {
            if (cellElement is VirtualGridNewCellElement && cellElement.ControlBoundingRectangle.Contains(args.Location))
            {
              virtualGridCellElement = cellElement;
              break;
            }
          }
        }
        if (virtualGridCellElement == null)
          return false;
        if (virtualGridCellElement is VirtualGridIndentCellElement)
        {
          if (virtualGridCellElement.RowIndex >= 0)
            this.SelectCell(virtualGridCellElement.RowIndex, 0, false, false, virtualGridCellElement.ViewInfo);
        }
        else if (!(virtualGridCellElement is VirtualGridHeaderCellElement))
        {
          bool flag = true;
          if (this.GridElement.MultiSelect)
          {
            foreach (SelectionRegion selectedRegion in this.GridElement.Selection.SelectedRegions)
            {
              if (selectedRegion.Contains(virtualGridCellElement.RowIndex, virtualGridCellElement.ColumnIndex))
              {
                flag = false;
                break;
              }
            }
          }
          if (flag)
            this.SelectCell(virtualGridCellElement.RowIndex, virtualGridCellElement.ColumnIndex, false, false, virtualGridCellElement.ViewInfo);
        }
        if (gridNewRowElement != null)
        {
          gridNewRowElement.InvalidateMeasure(true);
          gridNewRowElement.UpdateLayout();
        }
        if (this.GridElement.ContextMenu != null && (virtualGridCellElement is VirtualGridHeaderCellElement && virtualGridCellElement.ViewInfo.AllowColumnHeaderContextMenu || !(virtualGridCellElement is VirtualGridHeaderCellElement) && virtualGridCellElement.ViewInfo.AllowCellContextMenu))
        {
          VirtualGridContextMenu contextMenu = this.GridElement.ContextMenu as VirtualGridContextMenu;
          contextMenu?.InitializeMenuItems(virtualGridCellElement);
          VirtualGridContextMenuOpeningEventArgs args1 = new VirtualGridContextMenuOpeningEventArgs(virtualGridCellElement.RowIndex, virtualGridCellElement.ColumnIndex, virtualGridCellElement.ViewInfo, (RadDropDownMenu) contextMenu);
          if (this.GridElement.OnContextMenuOpening(args1) && args1.ContextMenu != null && args1.ContextMenu.Items.Count > 0)
          {
            args1.ContextMenu.RightToLeft = this.GridElement.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
            args1.ContextMenu.ThemeName = this.GridElement.ElementTree.ThemeName;
            args1.ContextMenu.Show(this.GridElement.ElementTree.Control, args.Location);
          }
        }
      }
      return false;
    }

    public virtual bool HandleMouseDoubleClick(MouseEventArgs args)
    {
      RadElement elementAtPoint = this.gridElement.ElementTree.GetElementAtPoint(args.Location);
      VirtualGridHeaderCellElement headerCellElement = elementAtPoint as VirtualGridHeaderCellElement;
      if (headerCellElement != null && headerCellElement.IsInResizeLocation(args.Location))
      {
        int columnIndex = headerCellElement.ColumnIndex;
        if (columnIndex > 0 && headerCellElement.ControlBoundingRectangle.X + 4 > args.X)
          --columnIndex;
        this.GridElement.BestFitColumn(columnIndex, headerCellElement.ViewInfo);
        return true;
      }
      VirtualGridCellElement virtualGridCellElement = elementAtPoint as VirtualGridCellElement;
      if (virtualGridCellElement != null && virtualGridCellElement.ColumnIndex >= 0 && (virtualGridCellElement.IsCurrent && this.GridElement.BeginEditMode != RadVirtualGridBeginEditMode.BeginEditProgrammatically))
        this.GridElement.BeginEdit();
      return false;
    }

    public virtual bool HandleMouseMove(MouseEventArgs args)
    {
      VirtualGridCellElement elementAtPoint = this.gridElement.ElementTree.GetElementAtPoint(args.Location) as VirtualGridCellElement;
      VirtualGridHeaderCellElement headerCellElement = elementAtPoint as VirtualGridHeaderCellElement;
      if (this.IsResizing)
      {
        if (this.GridElement.ElementTree.Control.Cursor == Cursors.SizeNS)
        {
          int num = args.Location.Y - this.lastMousePosition.Y;
          this.lastMousePosition = args.Location;
          this.resizedViewInfo.SetRowHeight(this.resizedRow, this.resizedViewInfo.GetRowHeight(this.resizedRow) + num);
          return true;
        }
        VirtualGridTableElement tableElement = this.GridElement.GetTableElement(this.resizedViewInfo);
        if (tableElement != null)
        {
          int delta = args.Location.X - this.lastMousePosition.X;
          if (tableElement.RightToLeft)
            delta *= -1;
          if (tableElement.ColumnLayout.ResizeColumn(delta))
            this.lastMousePosition = args.Location;
        }
        return true;
      }
      if (this.isSelecting && !(elementAtPoint is VirtualGridHeaderCellElement) && (elementAtPoint == null || elementAtPoint.ViewInfo == this.selectedViewInfo))
      {
        if (elementAtPoint != null)
        {
          this.selectionTimer.Stop();
          if (this.GridElement.Selection.Multiselect)
            this.SelectCell(elementAtPoint.RowIndex, elementAtPoint.ColumnIndex, true, false, elementAtPoint.ViewInfo);
        }
        else
        {
          int num = 0;
          Rectangle boundingRectangle = this.gridElement.ControlBoundingRectangle;
          if (args.X < boundingRectangle.Left)
            num |= 1;
          if (args.X > boundingRectangle.Right)
            num |= 4;
          if (args.Y < boundingRectangle.Top)
            num |= 2;
          if (args.Y > boundingRectangle.Bottom)
            num |= 8;
          this.selectionTimer.Tag = (object) num;
          if (!this.gridElement.ControlBoundingRectangle.Contains(args.Location) && this.GridElement.Selection.Multiselect)
            this.selectionTimer.Start();
          else
            this.selectionTimer.Stop();
        }
        return false;
      }
      if (headerCellElement != null && headerCellElement.IsInResizeLocation(args.Location))
      {
        if (this.IsResizing)
          return false;
        if (this.originalMouseCursor == (Cursor) null)
          this.originalMouseCursor = this.GridElement.ElementTree.Control.Cursor;
        this.GridElement.ElementTree.Control.Cursor = Cursors.SizeWE;
      }
      else if (elementAtPoint != null && elementAtPoint.IsInResizeLocation(args.Location))
      {
        if (this.IsResizing)
          return false;
        if (this.originalMouseCursor == (Cursor) null)
          this.originalMouseCursor = this.GridElement.ElementTree.Control.Cursor;
        this.GridElement.ElementTree.Control.Cursor = Cursors.SizeNS;
      }
      else if (this.originalMouseCursor != (Cursor) null && !this.IsResizing)
      {
        this.GridElement.ElementTree.Control.Cursor = this.originalMouseCursor;
        this.originalMouseCursor = (Cursor) null;
      }
      return false;
    }

    public virtual bool HandleMouseUp(MouseEventArgs args)
    {
      if (this.isSelecting)
      {
        this.selectionTimer.Stop();
        this.isSelecting = false;
        this.GridElement.Capture = false;
        this.selectedViewInfo = (VirtualGridViewInfo) null;
        this.mouseDownCell = (VirtualGridCellElement) null;
        if (!this.beginEdit)
          return false;
      }
      if (this.IsResizing)
      {
        VirtualGridTableElement tableElement = this.GridElement.GetTableElement(this.resizedViewInfo);
        if (tableElement != null && this.IsResizingColumn)
          tableElement.ColumnLayout.EndResizeColumn();
        this.resizedViewInfo = (VirtualGridViewInfo) null;
        this.resizedRow = -1;
        this.resizedColumn = -1;
        this.mouseDownCell = (VirtualGridCellElement) null;
        if (this.originalMouseCursor != (Cursor) null)
        {
          this.GridElement.ElementTree.Control.Cursor = this.originalMouseCursor;
          this.originalMouseCursor = (Cursor) null;
        }
        return false;
      }
      VirtualGridHeaderCellElement elementAtPoint = this.gridElement.ElementTree.GetElementAtPoint(args.Location) as VirtualGridHeaderCellElement;
      if (elementAtPoint != null && elementAtPoint == this.mouseDownCell && (args.Button == MouseButtons.Left && elementAtPoint.ViewInfo.AllowColumnSort))
      {
        ListSortDirection? nullable = new ListSortDirection?();
        if (elementAtPoint.SortOrder == RadSortOrder.None)
          nullable = new ListSortDirection?(ListSortDirection.Ascending);
        else if (elementAtPoint.SortOrder == RadSortOrder.Ascending)
          nullable = new ListSortDirection?(ListSortDirection.Descending);
        bool multiColumnSorting = elementAtPoint.ViewInfo.AllowMultiColumnSorting;
        SortDescriptorCollection sortDescriptors = elementAtPoint.ViewInfo.SortDescriptors;
        if (!multiColumnSorting)
        {
          int count = sortDescriptors.Count;
        }
        if (!nullable.HasValue)
        {
          if (multiColumnSorting)
            sortDescriptors.Remove(elementAtPoint.FieldName);
          else
            sortDescriptors.Clear();
        }
        else
        {
          SortDescriptor sortDescriptor = new SortDescriptor(elementAtPoint.FieldName, nullable.Value);
          int index = sortDescriptors.IndexOf(elementAtPoint.FieldName);
          if (!multiColumnSorting && sortDescriptors.Count == 1)
            sortDescriptors[0] = sortDescriptor;
          else if (index >= 0 && multiColumnSorting)
            sortDescriptors[index] = sortDescriptor;
          else if (!multiColumnSorting)
          {
            sortDescriptors.BeginUpdate();
            sortDescriptors.Clear();
            sortDescriptors.Add(sortDescriptor);
            sortDescriptors.EndUpdate();
          }
          else
            sortDescriptors.Add(sortDescriptor);
        }
      }
      if (this.beginEdit && this.GridElement.BeginEditMode != RadVirtualGridBeginEditMode.BeginEditProgrammatically)
      {
        this.GridElement.BeginEdit();
        this.beginEdit = false;
      }
      return false;
    }

    public virtual bool HandleMouseWheel(MouseEventArgs args)
    {
      if (this.GridElement.IsInEditMode && !this.GridElement.EndEdit())
        return false;
      int num1 = Math.Max(1, args.Delta / SystemInformation.MouseWheelScrollDelta);
      int delta = Math.Sign(args.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      RadScrollBarElement scrollBar = (Control.ModifierKeys & Keys.Shift) == Keys.Shift ? this.GridElement.TableElement.ColumnScroller.Scrollbar : this.GridElement.TableElement.RowScroller.Scrollbar;
      int num2 = scrollBar.Value;
      this.GridElement.TableElement.ScrollTo(delta, scrollBar);
      return scrollBar.Value != num2;
    }

    public virtual bool HandleKeyDown(KeyEventArgs args)
    {
      this.wasInEditMode = this.gridElement.IsInEditMode;
      switch (args.KeyCode)
      {
        case Keys.Tab:
          return this.HandleTabKey(args);
        case Keys.Return:
          return this.HandleEnterKey(args);
        case Keys.Escape:
          return this.HandleEscapeKey(args);
        case Keys.Space:
          return this.HandleSpaceKey(args);
        case Keys.Prior:
          return this.HandlePageUpKey(args);
        case Keys.Next:
          return this.HandlePageDownKey(args);
        case Keys.End:
          return this.HandleEndKey(args);
        case Keys.Home:
          return this.HandleHomeKey(args);
        case Keys.Left:
          return this.HandleLeftKey(args);
        case Keys.Up:
          return this.HandleUpKey(args);
        case Keys.Right:
          return this.HandleRightKey(args);
        case Keys.Down:
          return this.HandleDownKey(args);
        case Keys.Insert:
          return this.HandleInsertKey(args);
        case Keys.Delete:
          return this.HandleDeleteKey(args);
        case Keys.Add:
          return this.HandleAddKey(args);
        case Keys.Subtract:
          return this.HandleSubtractKey(args);
        case Keys.F2:
          return this.HandleF2Key(args);
        default:
          return this.HandleUnhandledKeys(args);
      }
    }

    public virtual bool HandleKeyUp(KeyEventArgs args)
    {
      return false;
    }

    public virtual bool HandleKeyPress(KeyPressEventArgs args)
    {
      if (args.KeyChar > '\x001B')
        return this.HandleAlphaNumericKey(args);
      return false;
    }

    protected virtual bool HandleUnhandledKeys(KeyEventArgs keys)
    {
      if (keys.KeyCode == Keys.A && keys.Control)
      {
        this.GridElement.Selection.SelectAll();
        return true;
      }
      if (keys.KeyCode == Keys.C && keys.Control)
        return this.GridElement.CopySelection();
      if (keys.KeyCode == Keys.X && keys.Control)
        return this.GridElement.CutSelection();
      if (keys.KeyCode == Keys.V && keys.Control)
        return this.GridElement.Paste();
      return false;
    }

    protected virtual bool HandleEscapeKey(KeyEventArgs keys)
    {
      if (!this.GridElement.IsInEditMode)
        return false;
      this.GridElement.CancelEdit();
      return true;
    }

    protected virtual bool HandleEnterKey(KeyEventArgs keys)
    {
      bool isInEditMode = this.GridElement.IsInEditMode;
      if (!isInEditMode && this.GridElement.BeginEditMode == RadVirtualGridBeginEditMode.BeginEditOnEnter)
        return this.GridElement.BeginEdit();
      if (isInEditMode && (!this.GridElement.CanEndEdit() || !this.GridElement.EndEdit()))
        return false;
      switch (this.GridElement.EnterKeyMode)
      {
        case RadVirtualGridEnterKeyMode.EnterMovesToNextCell:
          this.GridElement.MoveCurrentRight(false);
          break;
        case RadVirtualGridEnterKeyMode.EnterMovesToNextRow:
          this.GridElement.MoveCurrentDown(false);
          break;
        default:
          if (this.GridElement.CurrentCell != null && this.GridElement.CurrentCell.RowIndex == -2)
          {
            this.GridElement.EndEdit();
            this.GridElement.CommitNewRow();
          }
          return false;
      }
      if (!isInEditMode || this.GridElement.BeginEditMode == RadVirtualGridBeginEditMode.BeginEditProgrammatically)
        return false;
      if (this.GridElement.CurrentCell != null)
        this.GridElement.EnsureRowVisible(this.GridElement.CurrentCell.RowIndex);
      return this.GridElement.BeginEdit();
    }

    protected virtual bool HandleSpaceKey(KeyEventArgs keys)
    {
      switch (this.GridElement.BeginEditMode)
      {
        case RadVirtualGridBeginEditMode.BeginEditOnKeystroke:
        case RadVirtualGridBeginEditMode.BeginEditOnKeystrokeOrF2:
          this.GridElement.BeginEdit();
          return false;
        default:
          if (this.GridElement.CurrentCell != null && this.GridElement.Selection.Multiselect)
            this.GridElement.Selection.ExtendCurrentRegion(this.GridElement.CurrentCell.RowIndex, this.GridElement.CurrentCell.ColumnIndex);
          return false;
      }
    }

    protected virtual bool HandleF2Key(KeyEventArgs keys)
    {
      if (!this.GridElement.IsInEditMode && (this.GridElement.BeginEditMode == RadVirtualGridBeginEditMode.BeginEditOnF2 || this.GridElement.BeginEditMode == RadVirtualGridBeginEditMode.BeginEditOnKeystrokeOrF2))
        return this.GridElement.BeginEdit();
      return false;
    }

    protected virtual bool HandleUpKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell == null)
      {
        this.GridElement.MoveCurrent(0, 0, true);
        return true;
      }
      int firstScrollableRow = this.GetFirstScrollableRow(this.GridElement.TableElement);
      if (this.GridElement.CurrentCell == null || this.GridElement.CurrentCell.RowIndex >= firstScrollableRow + 1)
      {
        bool keepSelection = this.GridElement.Selection.Multiselect && keys.Shift;
        if (this.gridElement.IsInEditMode && !this.gridElement.EndEdit() || !this.GridElement.MoveCurrentUp(keepSelection))
          return false;
        if (this.wasInEditMode)
          this.GridElement.BeginEdit();
        return true;
      }
      this.tableElement = this.GridElement.TableElement;
      if (this.GridElement.TableElement.ViewElement.ScrollableRows.Children.Count == 0)
        return false;
      VirtualGridRowElement child = this.GridElement.TableElement.ViewElement.ScrollableRows.Children[0] as VirtualGridRowElement;
      if (child == null)
        return false;
      if (this.scrollDelta == 0)
      {
        this.scrollReason = VirtualGridInputBehavior.ScrollReason.ArrowKeyUp;
        this.scrollTimer.Start();
      }
      this.scrollDelta -= child.Size.Height - (int) this.tableElement.ViewElement.ScrollableRows.ScrollOffset.Height + this.tableElement.RowSpacing;
      return true;
    }

    protected virtual bool HandleDownKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell == null)
      {
        this.GridElement.MoveCurrent(0, 0, true);
        return true;
      }
      int lastScrollableRow = this.GetLastScrollableRow(this.GridElement.TableElement);
      if (this.GridElement.CurrentCell == null || this.GridElement.CurrentCell.RowIndex < lastScrollableRow || this.GridElement.EnablePaging)
      {
        bool keepSelection = this.GridElement.Selection.Multiselect && keys.Shift;
        if (this.gridElement.IsInEditMode && !this.gridElement.EndEdit() || !this.GridElement.MoveCurrentDown(keepSelection))
          return false;
        if (this.wasInEditMode)
          this.GridElement.BeginEdit();
        return true;
      }
      this.tableElement = this.GridElement.TableElement;
      if (this.scrollDelta == 0)
      {
        this.scrollReason = VirtualGridInputBehavior.ScrollReason.ArrowKeyDown;
        this.scrollTimer.Start();
      }
      this.scrollDelta += this.GridElement.GetRowHeight(this.GridElement.CurrentCell.RowIndex + 1) + (int) this.tableElement.ViewElement.ScrollableRows.ScrollOffset.Height;
      return true;
    }

    protected virtual bool HandleLeftKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell == null)
        this.GridElement.MoveCurrent(0, 0, true);
      bool keepSelection = this.GridElement.Selection.Multiselect && keys.Shift;
      if (this.gridElement.IsInEditMode && !this.gridElement.EndEdit() || !this.GridElement.MoveCurrentLeft(keepSelection))
        return false;
      if (this.wasInEditMode)
        this.GridElement.BeginEdit();
      return true;
    }

    protected virtual bool HandleRightKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell == null)
        this.GridElement.MoveCurrent(0, 0, true);
      bool keepSelection = this.GridElement.Selection.Multiselect && keys.Shift;
      if (this.gridElement.IsInEditMode && !this.gridElement.EndEdit() || !this.GridElement.MoveCurrentRight(keepSelection))
        return false;
      if (this.wasInEditMode)
        this.GridElement.BeginEdit();
      return true;
    }

    protected virtual bool HandleTabKey(KeyEventArgs keys)
    {
      if (this.GridElement.StandardTab || this.GridElement.ColumnCount == 0)
        return false;
      bool flag = this.GridElement.IsInEditMode && this.GridElement.CurrentCell.RowIndex == -2;
      bool shift = keys.Shift;
      if (keys.Control)
        return this.SelectNextControl(!shift);
      if (shift && this.GridElement.CurrentCell.RowIndex == 0 && this.GridElement.CurrentCell.ColumnIndex == 0)
        return this.SelectNextControl(false);
      if (!shift && this.GridElement.CurrentCell != null && (this.GridElement.CurrentCell.RowIndex == this.GridElement.RowCount && this.GridElement.CurrentCell.ColumnIndex == this.GridElement.ColumnCount) && !flag)
        return this.SelectNextControl(true);
      if (this.GridElement.CurrentCell != null && this.GridElement.CurrentCell.RowIndex == -2 && (shift && this.GridElement.CurrentCell.ColumnIndex == 0 || !shift && this.GridElement.CurrentCell.ColumnIndex == this.GridElement.ColumnCount - 1))
      {
        if (this.GridElement.EndEdit())
          this.GridElement.CommitNewRow();
      }
      else
      {
        bool isInEditMode = this.GridElement.IsInEditMode;
        if (isInEditMode && !this.GridElement.EndEdit())
          return true;
        if (shift)
          this.GridElement.MoveCurrentLeft(false);
        else
          this.GridElement.MoveCurrentRight(false);
        if (isInEditMode)
          this.GridElement.BeginEdit();
      }
      return true;
    }

    protected virtual bool HandleAddKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell != null && this.GridElement.OnQueryHasChildRows(this.GridElement.CurrentCell.RowIndex, this.GridElement.CurrentCell.ViewInfo))
        this.GridElement.ExpandRow(this.GridElement.CurrentCell.RowIndex);
      return true;
    }

    protected virtual bool HandleSubtractKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell != null && this.GridElement.OnQueryHasChildRows(this.GridElement.CurrentCell.RowIndex, this.GridElement.CurrentCell.ViewInfo))
        this.GridElement.CollapseRow(this.GridElement.CurrentCell.RowIndex);
      return true;
    }

    protected virtual bool HandleDeleteKey(KeyEventArgs keys)
    {
      return this.GridElement.DeleteSelectedRow();
    }

    protected virtual bool HandleHomeKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell == null)
        this.GridElement.MoveCurrent(0, 0, true);
      bool keepSelection = this.GridElement.Selection.Multiselect && keys.Shift;
      int rowIndex = this.GridElement.CurrentCell.RowIndex;
      int columnIndex = this.GridElement.CurrentCell.ColumnIndex;
      int rowOffset = 0;
      int columnOffset = -this.GridElement.CurrentCell.ColumnIndex;
      if (keys.Control)
      {
        if (this.GridElement.EnablePaging)
          this.GridElement.PageIndex = 0;
        rowOffset = -this.GridElement.CurrentCell.RowIndex;
      }
      this.GridElement.MoveCurrent(rowOffset, columnOffset, keepSelection);
      return true;
    }

    protected virtual bool HandleEndKey(KeyEventArgs keys)
    {
      if (this.GridElement.CurrentCell == null)
        this.GridElement.MoveCurrent(0, 0, true);
      bool keepSelection = this.GridElement.Selection.Multiselect && keys.Shift;
      int rowIndex = this.GridElement.CurrentCell.RowIndex;
      int columnIndex = this.GridElement.CurrentCell.ColumnIndex;
      int rowOffset = 0;
      int columnOffset = this.GridElement.CurrentCell.ViewInfo.ColumnCount - 1 - this.GridElement.CurrentCell.ColumnIndex;
      if (keys.Control)
      {
        if (this.GridElement.EnablePaging)
          this.GridElement.PageIndex = this.GridElement.TotalPages - 1;
        rowOffset = this.GridElement.CurrentCell.ViewInfo.RowCount - 1 - this.GridElement.CurrentCell.RowIndex;
      }
      this.GridElement.MoveCurrent(rowOffset, columnOffset, keepSelection);
      return true;
    }

    protected virtual bool HandleInsertKey(KeyEventArgs keys)
    {
      return false;
    }

    protected virtual bool HandleAlphaNumericKey(KeyPressEventArgs keys)
    {
      if (!this.GridElement.IsInEditMode && (this.GridElement.BeginEditMode == RadVirtualGridBeginEditMode.BeginEditOnKeystroke || this.GridElement.BeginEditMode == RadVirtualGridBeginEditMode.BeginEditOnKeystrokeOrF2))
      {
        this.GridElement.BeginEdit();
        if (this.GridElement.ActiveEditor is VirtualGridTextBoxEditor)
        {
          this.GridElement.ActiveEditor.Value = (object) keys.KeyChar;
          if (this.GridElement.IsInEditMode)
          {
            RadTextBoxItem textBoxItem = ((RadTextBoxElement) ((BaseInputEditor) this.GridElement.ActiveEditor).EditorElement).TextBoxItem;
            textBoxItem.SelectionStart = 1;
            textBoxItem.SelectionLength = 0;
          }
          return true;
        }
        if (this.GridElement.ActiveEditor is VirtualGridSpinEditor)
        {
          int result = 0;
          if (int.TryParse(keys.KeyChar.ToString(), out result))
          {
            VirtualGridSpinEditor activeEditor1 = this.GridElement.ActiveEditor as VirtualGridSpinEditor;
            if (activeEditor1 != null)
              (activeEditor1.EditorElement as RadSpinEditorElement).TextBoxControl.Text = result.ToString();
            else
              this.GridElement.ActiveEditor.Value = (object) result;
            if (this.GridElement.IsInEditMode)
            {
              VirtualGridSpinEditor activeEditor2 = this.GridElement.ActiveEditor as VirtualGridSpinEditor;
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
            VirtualGridSpinEditor activeEditor = this.GridElement.ActiveEditor as VirtualGridSpinEditor;
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
            VirtualGridSpinEditor activeEditor = this.GridElement.ActiveEditor as VirtualGridSpinEditor;
            if (activeEditor != null)
            {
              RadSpinEditorElement editorElement = (RadSpinEditorElement) activeEditor.EditorElement;
              editorElement.TextBoxControl.Text = "0" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
              editorElement.TextBoxControl.SelectionStart = editorElement.TextBoxControl.Text.Length;
            }
            return true;
          }
        }
        else if (this.GridElement.ActiveEditor is VirtualGridDropDownListEditor)
        {
          string findString = keys.KeyChar.ToString();
          RadDropDownListEditorElement editorElement = (this.GridElement.ActiveEditor as VirtualGridDropDownListEditor).EditorElement as RadDropDownListEditorElement;
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
          if (this.GridElement.ActiveEditor is VirtualGridBrowseEditor)
          {
            RadBrowseEditorElement editorElement = (this.GridElement.ActiveEditor as VirtualGridBrowseEditor).EditorElement as RadBrowseEditorElement;
            if (editorElement.ReadOnly)
              return false;
            this.GridElement.ActiveEditor.Value = (object) keys.KeyChar;
            if (this.GridElement.IsInEditMode)
            {
              editorElement.TextBoxItem.SelectionStart = 1;
              editorElement.TextBoxItem.SelectionLength = 0;
            }
            return true;
          }
          if (this.GridElement.ActiveEditor is VirtualGridCalculatorEditor)
          {
            VirtualGridCalculatorEditor activeEditor = this.GridElement.ActiveEditor as VirtualGridCalculatorEditor;
            int result = 0;
            if (int.TryParse(keys.KeyChar.ToString(), out result))
            {
              if (activeEditor != null)
                (activeEditor.EditorElement as RadCalculatorEditorElement).EditorContentElement.TextBoxItem.Text = result.ToString();
              else
                this.GridElement.ActiveEditor.Value = (object) result;
              if (this.GridElement.IsInEditMode && activeEditor != null)
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
          else if (this.GridElement.ActiveEditor is VirtualGridColorPickerEditor)
          {
            RadColorPickerEditorElement editorElement = (this.GridElement.ActiveEditor as GridColorPickerEditor).EditorElement as RadColorPickerEditorElement;
            editorElement.TextBoxItem.Text = keys.KeyChar.ToString();
            if (this.GridElement.IsInEditMode)
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

    protected virtual bool HandlePageUpKey(KeyEventArgs keys)
    {
      this.tableElement = this.GridElement.TableElement;
      if (this.GridElement.TableElement.ViewElement.ScrollableRows.Children.Count == 0)
        return false;
      VirtualGridRowElement child = this.GridElement.TableElement.ViewElement.ScrollableRows.Children[0] as VirtualGridRowElement;
      if (child == null)
        return false;
      if (this.scrollDelta == 0)
      {
        this.scrollReason = VirtualGridInputBehavior.ScrollReason.PageUp;
        this.scrollTimer.Start();
      }
      this.scrollDelta -= this.tableElement.ViewElement.ScrollableRows.Size.Height + (child.Size.Height - (int) this.tableElement.ViewElement.ScrollableRows.ScrollOffset.Height + this.tableElement.RowSpacing);
      return true;
    }

    protected virtual bool HandlePageDownKey(KeyEventArgs keys)
    {
      this.tableElement = this.GridElement.TableElement;
      if (this.scrollDelta == 0)
      {
        this.scrollReason = VirtualGridInputBehavior.ScrollReason.PageDown;
        this.scrollTimer.Start();
      }
      this.scrollDelta += this.tableElement.ViewElement.ScrollableRows.Size.Height + (int) this.tableElement.ViewElement.ScrollableRows.ScrollOffset.Height;
      return true;
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      if (this.scrollDelta == 0 || this.tableElement == null)
        return;
      RadScrollBarElement scrollBarElement = Control.ModifierKeys != Keys.Shift || this.scrollReason == VirtualGridInputBehavior.ScrollReason.ArrowKeyDown || this.scrollReason == VirtualGridInputBehavior.ScrollReason.ArrowKeyUp ? this.tableElement.VScrollBar : this.tableElement.HScrollBar;
      int num = scrollBarElement.Value + this.scrollDelta;
      if (num < scrollBarElement.Minimum)
        num = scrollBarElement.Minimum;
      else if (num > scrollBarElement.Maximum - scrollBarElement.LargeChange + 1)
        num = scrollBarElement.Maximum - scrollBarElement.LargeChange + 1;
      scrollBarElement.Value = num;
      if (this.GridElement.CurrentCell != null)
      {
        switch (this.scrollReason)
        {
          case VirtualGridInputBehavior.ScrollReason.PageUp:
            this.GridElement.CurrentCell = new VirtualGridCellInfo(this.GetFirstScrollableRow(this.GridElement.TableElement), this.GridElement.CurrentCell.ColumnIndex, this.GridElement.CurrentCell.ViewInfo);
            break;
          case VirtualGridInputBehavior.ScrollReason.PageDown:
            this.GridElement.CurrentCell = new VirtualGridCellInfo(this.GetLastScrollableRow(this.GridElement.TableElement), this.GridElement.CurrentCell.ColumnIndex, this.GridElement.CurrentCell.ViewInfo);
            break;
          case VirtualGridInputBehavior.ScrollReason.ArrowKeyUp:
            this.GridElement.MoveCurrentUp(this.GridElement.Selection.Multiselect && Control.ModifierKeys == Keys.Shift);
            break;
          case VirtualGridInputBehavior.ScrollReason.ArrowKeyDown:
            this.GridElement.MoveCurrentDown(this.GridElement.Selection.Multiselect && Control.ModifierKeys == Keys.Shift);
            break;
        }
      }
      this.scrollReason = VirtualGridInputBehavior.ScrollReason.MouseWheel;
      this.scrollDelta = 0;
      this.scrollTimer.Stop();
    }

    private void selectionTimer_Tick(object sender, EventArgs e)
    {
      Point screen = this.gridElement.ElementTree.Control.PointToScreen(this.gridElement.ControlBoundingRectangle.Location);
      Point point = screen + this.gridElement.ControlBoundingRectangle.Size;
      Point client = this.gridElement.ElementTree.Control.PointToClient(Control.MousePosition);
      int tag = (int) this.selectionTimer.Tag;
      int rowIndex = -1;
      int columnIndex = -1;
      if ((tag & 2) != 0)
      {
        this.gridElement.TableElement.VScrollBar.Value = Math.Max(0, this.gridElement.TableElement.VScrollBar.Value - (screen.Y - Control.MousePosition.Y) / 2);
        rowIndex = this.GetFirstVisibleItem(this.selectedTableElement.RowScroller);
        columnIndex = this.GetItemToPoint(this.selectedTableElement.ColumnScroller, client.X);
      }
      if ((tag & 8) != 0)
      {
        this.gridElement.TableElement.VScrollBar.Value = Math.Min(this.gridElement.TableElement.VScrollBar.Value + (Control.MousePosition.Y - point.Y) / 2, this.gridElement.TableElement.VScrollBar.Maximum - this.gridElement.TableElement.VScrollBar.LargeChange + 1);
        rowIndex = this.GetItemToPoint(this.selectedTableElement.RowScroller, this.selectedTableElement.ControlBoundingRectangle.Height);
        columnIndex = this.GetItemToPoint(this.selectedTableElement.ColumnScroller, client.X);
      }
      if ((tag & 1) != 0)
      {
        this.gridElement.TableElement.HScrollBar.Value = Math.Max(0, this.gridElement.TableElement.HScrollBar.Value - (screen.X - Control.MousePosition.X) / 2);
        columnIndex = this.GetFirstVisibleItem(this.selectedTableElement.ColumnScroller);
        rowIndex = this.GetItemToPoint(this.selectedTableElement.RowScroller, client.Y);
      }
      if ((tag & 4) != 0)
      {
        this.gridElement.TableElement.HScrollBar.Value = Math.Min(this.gridElement.TableElement.HScrollBar.Value + (Control.MousePosition.X - point.X) / 2, this.gridElement.TableElement.HScrollBar.Maximum - this.gridElement.TableElement.HScrollBar.LargeChange + 1);
        columnIndex = this.GetItemToPoint(this.selectedTableElement.ColumnScroller, this.selectedTableElement.ControlBoundingRectangle.Width);
        rowIndex = this.GetItemToPoint(this.selectedTableElement.RowScroller, client.Y);
      }
      if (rowIndex == -1 || columnIndex == -1)
        return;
      this.gridElement.Selection.ExtendCurrentRegion(rowIndex, columnIndex);
    }

    protected enum ScrollReason
    {
      MouseWheel,
      PageUp,
      PageDown,
      ArrowKeyUp,
      ArrowKeyDown,
    }
  }
}
