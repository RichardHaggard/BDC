// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadVirtualGridElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Paint;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class RadVirtualGridElement : LightVisualElement
  {
    private bool showNoDataText = true;
    private Queue<VirtualGridCellInfo> bestFitQueue = new Queue<VirtualGridCellInfo>();
    private RadVirtualGridBeginEditMode beginEditMode = RadVirtualGridBeginEditMode.BeginEditOnKeystrokeOrF2;
    private VirtualGridTableElement tableElement;
    private VirtualGridViewInfo masterViewInfo;
    private VirtualGridInputBehavior inputBehavior;
    private VirtualGridCellInfo currentCell;
    private VirtualGridSelection selection;
    private RadDropDownMenu contextMenu;
    private bool useScrollbarsInHierarchy;
    private bool standardTab;
    private IInputEditor activeEditor;
    private RadVirtualGridEnterKeyMode enterKeyMode;
    private VirtualGridCellElement editedCell;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadVirtualGridLocalizationProvider_CurrentProviderChanged);
      this.BackColor = Color.White;
      this.GradientStyle = GradientStyles.Solid;
      this.DrawFill = true;
      this.masterViewInfo = new VirtualGridViewInfo(this);
      this.inputBehavior = new VirtualGridInputBehavior(this);
      this.contextMenu = (RadDropDownMenu) new VirtualGridContextMenu(this);
      this.selection = new VirtualGridSelection();
      this.selection.CurrentViewInfo = this.MasterViewInfo;
      this.selection.SelectionChanged += new EventHandler(this.OnSelectionChanged);
      this.selection.SelectionChanging += new VirtualGridSelectionChangingEventHandler(this.OnSelectionChanging);
      this.tableElement = this.CreateTableElement(this, this.masterViewInfo);
      this.Children.Add((RadElement) this.tableElement);
    }

    protected virtual VirtualGridTableElement CreateTableElement(
      RadVirtualGridElement virtualGridElement,
      VirtualGridViewInfo viewInfo)
    {
      return new VirtualGridTableElement(virtualGridElement, viewInfo);
    }

    protected override void DisposeManagedResources()
    {
      LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadVirtualGridLocalizationProvider_CurrentProviderChanged);
      base.DisposeManagedResources();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowColumnResize
    {
      get
      {
        return this.MasterViewInfo.AllowColumnResize;
      }
      set
      {
        this.MasterViewInfo.AllowColumnResize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool AllowRowResize
    {
      get
      {
        return this.MasterViewInfo.AllowRowResize;
      }
      set
      {
        this.MasterViewInfo.AllowRowResize = value;
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ShowHeaderRow
    {
      get
      {
        return this.MasterViewInfo.ShowHeaderRow;
      }
      set
      {
        this.MasterViewInfo.ShowHeaderRow = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowAddNewRow
    {
      get
      {
        return this.MasterViewInfo.ShowNewRow;
      }
      set
      {
        this.MasterViewInfo.ShowNewRow = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowFiltering
    {
      get
      {
        return this.MasterViewInfo.ShowFilterRow;
      }
      set
      {
        this.MasterViewInfo.ShowFilterRow = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowSorting
    {
      get
      {
        return this.MasterViewInfo.AllowColumnSort;
      }
      set
      {
        this.MasterViewInfo.AllowColumnSort = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowMultiColumnSorting
    {
      get
      {
        return this.MasterViewInfo.AllowMultiColumnSorting;
      }
      set
      {
        this.MasterViewInfo.AllowMultiColumnSorting = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowEdit
    {
      get
      {
        return this.MasterViewInfo.AllowEdit;
      }
      set
      {
        this.MasterViewInfo.AllowEdit = value;
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowDelete
    {
      get
      {
        return this.MasterViewInfo.AllowDelete;
      }
      set
      {
        this.MasterViewInfo.AllowDelete = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowCut
    {
      get
      {
        return this.MasterViewInfo.AllowCut;
      }
      set
      {
        this.MasterViewInfo.AllowCut = value;
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowCopy
    {
      get
      {
        return this.MasterViewInfo.AllowCopy;
      }
      set
      {
        this.MasterViewInfo.AllowCopy = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowPaste
    {
      get
      {
        return this.MasterViewInfo.AllowPaste;
      }
      set
      {
        this.MasterViewInfo.AllowPaste = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool EnableAlternatingRowColor
    {
      get
      {
        return this.MasterViewInfo.EnableAlternatingRowColor;
      }
      set
      {
        this.MasterViewInfo.EnableAlternatingRowColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowColumnHeaderContextMenu
    {
      get
      {
        return this.MasterViewInfo.AllowColumnHeaderContextMenu;
      }
      set
      {
        this.MasterViewInfo.AllowColumnHeaderContextMenu = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool AllowCellContextMenu
    {
      get
      {
        return this.MasterViewInfo.AllowCellContextMenu;
      }
      set
      {
        this.MasterViewInfo.AllowCellContextMenu = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadDropDownMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        this.contextMenu = value;
      }
    }

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public VirtualGridViewInfo MasterViewInfo
    {
      get
      {
        return this.masterViewInfo;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VirtualGridInputBehavior InputBehavior
    {
      get
      {
        return this.inputBehavior;
      }
      set
      {
        this.inputBehavior = value;
      }
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VirtualGridSelection Selection
    {
      get
      {
        return this.selection;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public VirtualGridCellInfo CurrentCell
    {
      get
      {
        return this.currentCell;
      }
      set
      {
        this.SetCurrentCell(value);
      }
    }

    [DefaultValue(VirtualGridSelectionMode.CellSelect)]
    public VirtualGridSelectionMode SelectionMode
    {
      get
      {
        return this.Selection.SelectionMode;
      }
      set
      {
        this.Selection.SelectionMode = value;
      }
    }

    [DefaultValue(false)]
    public bool MultiSelect
    {
      get
      {
        return this.Selection.Multiselect;
      }
      set
      {
        this.Selection.Multiselect = value;
      }
    }

    [DefaultValue(false)]
    public bool UseScrollbarsInHierarchy
    {
      get
      {
        return this.useScrollbarsInHierarchy;
      }
      set
      {
        this.useScrollbarsInHierarchy = value;
      }
    }

    public int TotalPages
    {
      get
      {
        return this.MasterViewInfo.TotalPages;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnablePaging
    {
      get
      {
        return this.MasterViewInfo.EnablePaging;
      }
      set
      {
        this.MasterViewInfo.EnablePaging = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(100)]
    public int PageSize
    {
      get
      {
        return this.MasterViewInfo.PageSize;
      }
      set
      {
        this.MasterViewInfo.PageSize = value;
      }
    }

    [DefaultValue(0)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int PageIndex
    {
      get
      {
        return this.MasterViewInfo.PageIndex;
      }
      set
      {
        this.MasterViewInfo.PageIndex = value;
      }
    }

    [DefaultValue(0)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int RowCount
    {
      get
      {
        return this.MasterViewInfo.RowCount;
      }
      set
      {
        this.MasterViewInfo.RowCount = value;
      }
    }

    [DefaultValue(0)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ColumnCount
    {
      get
      {
        return this.MasterViewInfo.ColumnCount;
      }
      set
      {
        this.MasterViewInfo.ColumnCount = value;
      }
    }

    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.MasterViewInfo.SortDescriptors;
      }
    }

    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.MasterViewInfo.FilterDescriptors;
      }
    }

    [DefaultValue(false)]
    public bool StandardTab
    {
      get
      {
        return this.standardTab;
      }
      set
      {
        this.standardTab = value;
      }
    }

    [DefaultValue(true)]
    public bool ShowNoDataText
    {
      get
      {
        return this.showNoDataText;
      }
      set
      {
        if (this.showNoDataText == value)
          return;
        this.showNoDataText = value;
        this.TableElement.UpdateNoDataText();
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (ShowNoDataText)));
      }
    }

    public void BeginUpdate()
    {
      this.TableElement.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.TableElement.EndUpdate();
    }

    public void CommitNewRow()
    {
      if (this.CurrentCell == null)
        return;
      this.OnUserAddedRow(this.CurrentCell.ViewInfo.NewRowValues);
      this.TableElement.SynchronizeRows();
      this.CurrentCell.ViewInfo.NewRowValues.Clear();
    }

    public bool DeleteSelectedRow()
    {
      if (this.CurrentCell == null || !this.MasterViewInfo.AllowDelete)
        return false;
      List<int> intList = new List<int>();
      if (this.Selection.Multiselect)
      {
        foreach (SelectionRegion selectedRegion in this.Selection.SelectedRegions)
        {
          for (int top = selectedRegion.Top; top <= selectedRegion.Bottom; ++top)
            intList.Add(top);
        }
      }
      else
        intList.Add(this.CurrentCell.RowIndex);
      this.DeleteRow((IEnumerable<int>) intList, this.CurrentCell.ViewInfo);
      return true;
    }

    public void DeleteRow(IEnumerable<int> rowIndices, VirtualGridViewInfo viewInfo)
    {
      this.OnUserDeletedRow(new VirtualGridRowsEventArgs(rowIndices, viewInfo));
      this.TableElement.SynchronizeRows();
    }

    public int GetRowHeight(int rowIndex)
    {
      return this.MasterViewInfo.GetRowHeight(rowIndex);
    }

    public void SetRowHeight(int rowIndex, int height)
    {
      this.MasterViewInfo.SetRowHeight(rowIndex, height);
    }

    public void SetRowHeight(int height, params int[] rowIndices)
    {
      this.TableElement.SetRowHeight(height, rowIndices);
    }

    public int GetColumnWidth(int columnIndex)
    {
      return this.MasterViewInfo.GetColumnWidth(columnIndex);
    }

    public void SetColumnWidth(int columnIndex, int width)
    {
      this.MasterViewInfo.SetColumnWidth(columnIndex, width);
    }

    public void SetColumnWidth(int width, params int[] columnIndices)
    {
      this.TableElement.SetColumnWidth(width, columnIndices);
    }

    public void SetRowPinPosition(int rowIndex, PinnedRowPosition pinPosition)
    {
      this.MasterViewInfo.SetRowPinPosition(rowIndex, pinPosition);
    }

    public void SetColumnPinPosition(int columnIndex, PinnedColumnPosition pinPosition)
    {
      this.MasterViewInfo.SetColumnPinPosition(columnIndex, pinPosition);
    }

    public bool IsRowPinned(int rowIndex)
    {
      return this.MasterViewInfo.IsRowPinned(rowIndex);
    }

    public bool IsColumnPinned(int columnIndex)
    {
      return this.MasterViewInfo.IsColumnPinned(columnIndex);
    }

    public bool ExpandRow(int rowIndex)
    {
      return this.MasterViewInfo.ExpandRow(rowIndex);
    }

    public bool CollapseRow(int rowIndex)
    {
      return this.MasterViewInfo.CollapseRow(rowIndex);
    }

    public bool IsRowExpanded(int rowIndex)
    {
      return this.MasterViewInfo.IsRowExpanded(rowIndex);
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.MasterViewInfo.ColumnsViewState.Reset();
      this.MasterViewInfo.RowsViewState.Reset();
      this.TableElement.RowScroller.UpdateScrollRange();
    }

    public event VirtualGridCellValueNeededEventHandler CellValueNeeded;

    protected internal virtual void OnCellValueNeeded(VirtualGridCellValueNeededEventArgs args)
    {
      if (this.CellValueNeeded == null)
        return;
      this.CellValueNeeded((object) this, args);
    }

    public event VirtualGridRowElementEventHandler RowFormatting;

    protected internal virtual void OnRowFormatting(VirtualGridRowElementEventArgs args)
    {
      if (this.RowFormatting == null)
        return;
      this.RowFormatting((object) this, args);
    }

    public event VirtualGridCellElementEventHandler CellFormatting;

    protected internal virtual void OnCellFormatting(VirtualGridCellElementEventArgs args)
    {
      if (this.CellFormatting == null)
        return;
      this.CellFormatting((object) this, args);
    }

    public event VirtualGridEventHandler SortDescriptorsChanged;

    protected internal virtual void OnSortDescriptorsChanged(VirtualGridViewInfo viewInfo)
    {
      if (this.SortDescriptorsChanged != null)
        this.SortDescriptorsChanged((object) this, new VirtualGridEventArgs(viewInfo));
      this.TableElement.SynchronizeRows();
    }

    public event VirtualGridEventHandler FilterDescriptorsChanged;

    protected internal virtual void OnFilterDescriptorsChanged(VirtualGridViewInfo viewInfo)
    {
      if (this.FilterDescriptorsChanged != null)
        this.FilterDescriptorsChanged((object) this, new VirtualGridEventArgs(viewInfo));
      this.TableElement.SynchronizeRows();
    }

    public event VirtualGridRowsEventHandler UserDeletedRow;

    protected virtual void OnUserDeletedRow(VirtualGridRowsEventArgs e)
    {
      if (this.UserDeletedRow == null)
        return;
      this.UserDeletedRow((object) this, e);
    }

    public event VirtualGridNewRowEventHandler UserAddedRow;

    private void OnUserAddedRow(Dictionary<int, object> values)
    {
      if (this.UserAddedRow == null)
        return;
      this.UserAddedRow((object) this, new VirtualGridNewRowEventArgs(new Dictionary<int, object>((IDictionary<int, object>) values)));
    }

    public event VirtualGridEventHandler PageIndexChanged;

    protected internal virtual void OnPageIndexChanged(VirtualGridEventArgs args)
    {
      if (this.PageIndexChanged == null)
        return;
      this.PageIndexChanged((object) this, args);
    }

    public event VirtualGridPageChangingEventHandler PageIndexChanging;

    protected internal virtual void OnPageIndexChanging(VirtualGridPageChangingEventArgs args)
    {
      if (this.PageIndexChanging == null)
        return;
      this.PageIndexChanging((object) this, args);
    }

    public event VirtualGridCreateRowEventHandler CreateRowElement;

    protected internal virtual void OnCreateRowElement(VirtualGridCreateRowEventArgs e)
    {
      if (this.CreateRowElement == null)
        return;
      this.CreateRowElement((object) this, e);
    }

    public event VirtualGridCreateCellEventHandler CreateCellElement;

    protected internal virtual void OnCreateCellElement(VirtualGridCreateCellEventArgs e)
    {
      if (this.CreateCellElement == null)
        return;
      this.CreateCellElement((object) this, e);
    }

    public event VirtualGridCellEditorInitializedEventHandler CellEditorInitialized;

    protected virtual void OnCellEditorInitialized(VirtualGridCellEditorInitializedEventArgs args)
    {
      if (this.CellEditorInitialized == null)
        return;
      this.CellEditorInitialized((object) this, args);
    }

    public event VirtualGridEditorRequiredEventHandler EditorRequired;

    protected virtual void OnEditorRequired(VirtualGridEditorRequiredEventArgs args)
    {
      if (this.EditorRequired == null)
        return;
      this.EditorRequired((object) this, args);
    }

    public event VirtualGridCellValuePushedEventHandler CellValuePushed;

    protected virtual void OnCellValuePushed(VirtualGridCellValuePushedEventArgs args)
    {
      if (this.CellValuePushed == null)
        return;
      this.CellValuePushed((object) this, args);
    }

    public event ValueChangingEventHandler ValueChanging;

    protected internal virtual void OnValueChanging(object sender, ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging(sender, e);
    }

    public event EventHandler ValueChanged;

    protected internal virtual void OnValueChanged(object sender, EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged(sender, e);
    }

    public event VirtualGridCellInfoCancelEventHandler CurrentCellChanging;

    protected virtual bool OnCurrentCellChanging(VirtualGridCellInfo cellInfo)
    {
      if (this.CurrentCellChanging == null)
        return true;
      VirtualGridCellInfoCancelEventArgs e = new VirtualGridCellInfoCancelEventArgs(cellInfo);
      this.CurrentCellChanging((object) this, e);
      return !e.Cancel;
    }

    public event EventHandler CurrentCellChanged;

    protected virtual void OnCurrentCellChanged(EventArgs e)
    {
      if (this.CurrentCellChanged == null)
        return;
      this.CurrentCellChanged((object) this, e);
    }

    public event VirtualGridContextMenuOpeningEventHandler ContextMenuOpening;

    protected internal virtual bool OnContextMenuOpening(VirtualGridContextMenuOpeningEventArgs args)
    {
      if (this.ContextMenuOpening == null)
        return true;
      this.ContextMenuOpening((object) this, args);
      return !args.Cancel;
    }

    public event VirtualGridSelectionChangingEventHandler SelectionChanging;

    protected virtual void OnSelectionChanging(
      object sender,
      VirtualGridSelectionChangingEventArgs e)
    {
      if (this.SelectionChanging == null)
        return;
      this.SelectionChanging((object) this, e);
    }

    public event EventHandler SelectionChanged;

    protected virtual void OnSelectionChanged(object sender, EventArgs e)
    {
      this.TableElement.SynchronizeRows(true, false);
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, e);
    }

    public event VirtualGridRowExpandingEventHandler RowExpanding;

    protected internal virtual void OnRowExpanding(VirtualGridRowExpandingEventArgs args)
    {
      if (this.RowExpanding == null)
        return;
      this.RowExpanding((object) this, args);
    }

    public event VirtualGridRowExpandedEventHandler RowExpanded;

    protected internal virtual void OnRowExpanded(VirtualGridRowExpandedEventArgs args)
    {
      if (this.RowExpanded == null)
        return;
      this.RowExpanded((object) this, args);
    }

    public event VirtualGridRowExpandingEventHandler RowCollapsing;

    protected internal virtual void OnRowCollapsing(VirtualGridRowExpandingEventArgs args)
    {
      if (this.RowCollapsing == null)
        return;
      this.RowCollapsing((object) this, args);
    }

    public event VirtualGridRowExpandedEventHandler RowCollapsed;

    protected internal virtual void OnRowCollapsed(VirtualGridRowExpandedEventArgs args)
    {
      if (this.RowCollapsed == null)
        return;
      this.RowCollapsed((object) this, args);
    }

    public event VirtualGridColumnWidthChangingEventHandler ColumnWidthChanging;

    protected internal virtual bool OnColumnWidthChanging(
      VirtualGridColumnWidthChangingEventArgs args)
    {
      if (this.ColumnWidthChanging == null)
        return true;
      this.ColumnWidthChanging((object) this, args);
      return !args.Cancel;
    }

    public event VirtualGridColumnEventHandler ColumnWidthChanged;

    protected internal virtual void OnColumnWidthChanged(VirtualGridColumnEventArgs args)
    {
      if (this.ColumnWidthChanged == null)
        return;
      this.ColumnWidthChanged((object) this, args);
    }

    public event VirtualGridRowHeightChangingEventHandler RowHeightChanging;

    protected internal virtual bool OnRowHeightChanging(VirtualGridRowHeightChangingEventArgs args)
    {
      if (this.RowHeightChanging == null)
        return true;
      this.RowHeightChanging((object) this, args);
      return !args.Cancel;
    }

    public event VirtualGridRowEventHandler RowHeightChanged;

    protected internal virtual void OnRowHeightChanged(VirtualGridRowEventArgs args)
    {
      if (this.RowHeightChanged == null)
        return;
      this.RowHeightChanged((object) this, args);
    }

    public event VirtualGridCellElementEventHandler CellClick;

    protected internal virtual void OnCellClick(VirtualGridCellElementEventArgs args)
    {
      if (this.CellClick == null)
        return;
      this.CellClick((object) this, args);
    }

    public event EventHandler CellDoubleClick;

    protected internal virtual void OnCellDoubleClick(VirtualGridCellElementEventArgs args)
    {
      if (this.CellDoubleClick == null)
        return;
      this.CellDoubleClick((object) this, (EventArgs) args);
    }

    public event VirtualGridCellElementMouseEventHandler CellMouseMove;

    protected internal virtual void OnCellMouseMove(VirtualGridCellElementMouseEventArgs args)
    {
      if (this.CellMouseMove == null)
        return;
      this.CellMouseMove((object) this, args);
    }

    public event VirtualGridCellPaintEventHandler CellPaint;

    protected internal virtual void OnCellPaint(VirtualGridCellElement cell, IGraphics g)
    {
      if (this.CellPaint == null)
        return;
      this.CellPaint((object) this, new VirtualGridCellPaintEventArgs(cell, cell.ViewInfo, (Graphics) g.UnderlayGraphics));
    }

    public event VirtualGridRowPaintEventHandler RowPaint;

    protected internal virtual void OnRowPaint(VirtualGridRowElement row, IGraphics g)
    {
      if (this.RowPaint == null)
        return;
      this.RowPaint((object) this, new VirtualGridRowPaintEventArgs(row, row.ViewInfo, (Graphics) g.UnderlayGraphics));
    }

    public event VirtualGridCellValidatingEventHandler CellValidating;

    protected internal virtual void OnCellValidating(VirtualGridCellValidatingEventArgs args)
    {
      if (this.CellValidating == null)
        return;
      this.CellValidating((object) this, args);
    }

    public event VirtualGridRowValidatingEventHandler RowValidating;

    protected internal virtual void OnRowValidating(VirtualGridRowValidatingEventArgs e)
    {
      if (this.RowValidating == null)
        return;
      this.RowValidating((object) this, e);
    }

    public event VirtualGridRowEventHandler RowValidated;

    protected internal virtual void OnRowValidated(VirtualGridRowEventArgs e)
    {
      if (this.RowValidated == null)
        return;
      this.RowValidated((object) this, e);
    }

    public event VirtualGridClipboardEventHandler Copying;

    protected internal virtual void OnCopying(VirtualGridClipboardEventArgs args)
    {
      if (this.Copying == null)
        return;
      this.Copying((object) this, args);
    }

    public event VirtualGridClipboardEventHandler Pasting;

    protected internal virtual void OnPasting(VirtualGridClipboardEventArgs args)
    {
      if (this.Pasting == null)
        return;
      this.Pasting((object) this, args);
    }

    public event VirtualGridViewInfoPropertyChangedEventHandler ViewInfoPropertyChanged;

    protected internal virtual void OnViewInfoPropertyChanged(
      VirtualGridViewInfo viewInfo,
      string propertyName)
    {
      if (this.ViewInfoPropertyChanged == null)
        return;
      this.ViewInfoPropertyChanged((object) this, new VirtualGridViewInfoPropertyChangedEventArgs(viewInfo, propertyName));
    }

    public event VirtualGridQueryHasChildRowsEventHandler QueryHasChildRows;

    protected internal virtual bool OnQueryHasChildRows(int rowIndex, VirtualGridViewInfo viewInfo)
    {
      if (this.QueryHasChildRows == null)
        return false;
      VirtualGridQueryHasChildRowsEventArgs e = new VirtualGridQueryHasChildRowsEventArgs(rowIndex, viewInfo);
      this.QueryHasChildRows((object) this, e);
      return e.HasChildRows;
    }

    [DefaultValue(RadVirtualGridBeginEditMode.BeginEditOnKeystrokeOrF2)]
    public RadVirtualGridBeginEditMode BeginEditMode
    {
      get
      {
        return this.beginEditMode;
      }
      set
      {
        this.beginEditMode = value;
      }
    }

    [DefaultValue(RadVirtualGridEnterKeyMode.None)]
    public RadVirtualGridEnterKeyMode EnterKeyMode
    {
      get
      {
        return this.enterKeyMode;
      }
      set
      {
        this.enterKeyMode = value;
      }
    }

    public bool IsInEditMode
    {
      get
      {
        return this.activeEditor != null;
      }
    }

    public bool BeginEdit()
    {
      if (this.CurrentCell == null)
        return false;
      this.EnsureCellVisible(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
      this.UpdateLayout();
      return this.BeginEdit(this.FindCellElement(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo));
    }

    public virtual bool BeginEdit(VirtualGridCellElement currentCell)
    {
      if (this.IsInEditMode || currentCell == null || !currentCell.ViewInfo.AllowEdit && !(currentCell is VirtualGridNewCellElement) && !(currentCell is VirtualGridFilterCellElement) || !currentCell.CanEdit)
        return false;
      VirtualGridEditorRequiredEventArgs args = new VirtualGridEditorRequiredEventArgs(this.GetEditor(currentCell.ViewInfo.GetColumnDataType(currentCell.ColumnIndex)) ?? this.GetDefaultEditor(currentCell.Value), currentCell.RowIndex, currentCell.ColumnIndex, currentCell.ViewInfo);
      this.OnEditorRequired(args);
      IInputEditor editor = args.Editor;
      if (editor == null || args.Cancel)
        return false;
      this.activeEditor = editor;
      currentCell.AddEditor(this.ActiveEditor);
      currentCell.RowElement.IsInEditMode = true;
      this.editedCell = currentCell;
      currentCell.UpdateLayout();
      this.InitializeEditor(editor, currentCell);
      this.ActiveEditor.BeginEdit();
      return true;
    }

    protected virtual void InitializeEditor(IInputEditor activeEditor, VirtualGridCellElement cell)
    {
      if (cell == null)
        return;
      ISupportInitialize supportInitialize = activeEditor as ISupportInitialize;
      supportInitialize?.BeginInit();
      activeEditor.Initialize((object) cell, cell.Value);
      VirtualGridCellEditorInitializedEventArgs args = new VirtualGridCellEditorInitializedEventArgs(this.ActiveEditor, cell.RowIndex, cell.ColumnIndex, cell.ViewInfo);
      this.OnCellEditorInitialized(args);
      RadControl radControl = this.ElementTree == null || this.ElementTree.Control == null ? (RadControl) null : this.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        BaseInputEditor activeEditor1 = args.ActiveEditor as BaseInputEditor;
        if (activeEditor1 != null)
          activeEditor1.EditorElement.StretchVertically = true;
      }
      supportInitialize?.EndInit();
    }

    protected virtual IInputEditor GetEditor(System.Type dataType)
    {
      if ((object) dataType == null)
        return (IInputEditor) null;
      if (dataType.IsGenericType && (object) dataType.GetGenericTypeDefinition() == (object) typeof (Nullable<>))
        dataType = dataType.GetGenericArguments()[0];
      if ((object) dataType == (object) typeof (Color))
        return (IInputEditor) new VirtualGridColorPickerEditor();
      if ((object) dataType == (object) typeof (Image))
        return (IInputEditor) new VirtualGridBrowseEditor();
      if ((object) dataType == (object) typeof (DateTime))
        return (IInputEditor) new VirtualGridDateTimeEditor();
      if (TelerikHelper.IsNumericType(dataType))
        return (IInputEditor) new VirtualGridSpinEditor();
      return (IInputEditor) new VirtualGridTextBoxEditor();
    }

    protected virtual IInputEditor GetDefaultEditor(object value)
    {
      if (value == null || value == DBNull.Value)
        return (IInputEditor) new VirtualGridTextBoxEditor();
      System.Type type = value.GetType();
      if (type.IsGenericType && (object) type.GetGenericTypeDefinition() == (object) typeof (Nullable<>))
        type = type.GetGenericArguments()[0];
      if ((object) type == (object) typeof (Color))
        return (IInputEditor) new VirtualGridColorPickerEditor();
      if ((object) type == (object) typeof (Image))
        return (IInputEditor) new VirtualGridBrowseEditor();
      if ((object) type == (object) typeof (DateTime))
        return (IInputEditor) new VirtualGridDateTimeEditor();
      if (TelerikHelper.IsNumericType(type))
        return (IInputEditor) new VirtualGridSpinEditor();
      return (IInputEditor) new VirtualGridTextBoxEditor();
    }

    public VirtualGridCellElement FindCellElement(
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo)
    {
      foreach (VirtualGridCellElement descendant in this.GetDescendants((Predicate<RadElement>) (x => x is VirtualGridCellElement), TreeTraversalMode.BreadthFirst))
      {
        if (descendant.RowIndex == rowIndex && descendant.ColumnIndex == columnIndex && descendant.ViewInfo == viewInfo)
          return descendant;
      }
      return (VirtualGridCellElement) null;
    }

    public virtual bool CanEndEdit()
    {
      RadTextBoxEditor activeEditor = this.ActiveEditor as RadTextBoxEditor;
      return activeEditor == null || !activeEditor.Multiline || !activeEditor.AcceptsReturn;
    }

    public virtual bool CancelEdit()
    {
      if (this.IsInEditMode)
        return this.EndEditCore(false);
      return false;
    }

    public virtual bool EndEdit()
    {
      if (this.IsInEditMode)
        return this.EndEditCore(true);
      return false;
    }

    private bool EndEditCore(bool commit)
    {
      VirtualGridCellElement virtualGridCellElement = this.editedCell ?? this.FindCellElement(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
      if (commit)
      {
        if (virtualGridCellElement is VirtualGridNewCellElement)
          virtualGridCellElement.ViewInfo.NewRowValues[virtualGridCellElement.ColumnIndex] = this.ActiveEditor.Value;
        else if (this.CurrentCell.RowIndex >= 0)
        {
          VirtualGridCellValidatingEventArgs args = new VirtualGridCellValidatingEventArgs(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo, this.ActiveEditor.Value);
          this.OnCellValidating(args);
          if (args.Cancel)
            return false;
          this.SetCellValue(this.ActiveEditor.Value, virtualGridCellElement.RowIndex, virtualGridCellElement.ColumnIndex, virtualGridCellElement.ViewInfo);
        }
      }
      virtualGridCellElement.RowElement.IsInEditMode = false;
      virtualGridCellElement.RemoveEditor(this.ActiveEditor);
      virtualGridCellElement.RowElement.Synchronize();
      this.activeEditor = (IInputEditor) null;
      this.editedCell = (VirtualGridCellElement) null;
      return true;
    }

    public void SetCellValue(
      object value,
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo)
    {
      this.OnCellValuePushed(new VirtualGridCellValuePushedEventArgs(value, rowIndex, columnIndex, viewInfo));
    }

    protected virtual bool SetCurrentCell(VirtualGridCellInfo value)
    {
      if ((this.currentCell != null || value == null) && (this.currentCell == null || value != null) && ((this.currentCell == null || this.currentCell.RowIndex == value.RowIndex) && (this.currentCell == null || this.currentCell.ColumnIndex == value.ColumnIndex)) && (this.currentCell == null || this.currentCell.ViewInfo == value.ViewInfo) || (!this.OnCurrentCellChanging(value) || this.IsInEditMode && !this.EndEdit()))
        return false;
      int rowIndex = int.MinValue;
      VirtualGridViewInfo viewInfo = (VirtualGridViewInfo) null;
      if (this.CurrentCell != null)
      {
        rowIndex = this.CurrentCell.RowIndex;
        viewInfo = this.CurrentCell.ViewInfo;
      }
      if (this.CurrentCell != null && (value == null || this.CurrentCell.ViewInfo != value.ViewInfo || this.CurrentCell.RowIndex != value.RowIndex))
      {
        VirtualGridRowValidatingEventArgs e = new VirtualGridRowValidatingEventArgs(rowIndex, viewInfo);
        this.OnRowValidating(e);
        if (e.Cancel)
          return false;
      }
      VirtualGridTableElement gridTableElement = (VirtualGridTableElement) null;
      if (this.CurrentCell != null)
        gridTableElement = this.GetTableElement(this.CurrentCell.ViewInfo);
      this.currentCell = value;
      if (this.currentCell != null)
        this.EnsureCellVisible(this.currentCell.RowIndex, this.currentCell.ColumnIndex, this.currentCell.ViewInfo);
      if (this.CurrentCell != null)
        gridTableElement = this.GetTableElement(this.CurrentCell.ViewInfo);
      gridTableElement?.ViewElement.GetRowElement(rowIndex)?.Synchronize(false);
      if (value != null)
        gridTableElement = this.GetTableElement(value.ViewInfo);
      if (gridTableElement != null && value != null)
        gridTableElement.ViewElement.GetRowElement(value.RowIndex)?.Synchronize(false);
      if (viewInfo != null)
        this.OnRowValidated(new VirtualGridRowEventArgs(rowIndex, viewInfo));
      this.OnCurrentCellChanged(EventArgs.Empty);
      return true;
    }

    public bool MoveCurrentLeft(bool keepSelection)
    {
      return this.MoveCurrent(0, -1, keepSelection);
    }

    public bool MoveCurrentRight(bool keepSelection)
    {
      return this.MoveCurrent(0, 1, keepSelection);
    }

    public bool MoveCurrentDown(bool keepSelection)
    {
      if (this.CurrentCell == null)
      {
        if (this.RowCount <= 0 || this.ColumnCount <= 0)
          return false;
        this.CurrentCell = new VirtualGridCellInfo(0, 0, this.MasterViewInfo);
        return true;
      }
      ReadOnlyCollection<int> topPinnedItems = this.MasterViewInfo.RowsViewState.TopPinnedItems;
      ReadOnlyCollection<int> bottomPinnedItems = this.MasterViewInfo.RowsViewState.BottomPinnedItems;
      if (topPinnedItems.Contains(this.CurrentCell.RowIndex))
      {
        int num = topPinnedItems.IndexOf(this.CurrentCell.RowIndex);
        if (num < topPinnedItems.Count - 1)
          return this.MoveCurrent(topPinnedItems[num + 1] - this.CurrentCell.RowIndex, 0, keepSelection);
        int itemIndex = 0;
        while (this.MasterViewInfo.RowsViewState.IsPinned(itemIndex) && itemIndex < this.MasterViewInfo.RowCount)
          ++itemIndex;
        if (itemIndex < this.MasterViewInfo.RowCount)
          return this.MoveCurrent(itemIndex - this.CurrentCell.RowIndex, 0, keepSelection);
        if (bottomPinnedItems.Count > 0)
          return this.MoveCurrent(bottomPinnedItems[0] - this.CurrentCell.RowIndex, 0, keepSelection);
        return false;
      }
      if (bottomPinnedItems.Contains(this.CurrentCell.RowIndex))
      {
        int num = bottomPinnedItems.IndexOf(this.CurrentCell.RowIndex);
        if (num < bottomPinnedItems.Count - 1)
          return this.MoveCurrent(bottomPinnedItems[num + 1] - this.CurrentCell.RowIndex, 0, keepSelection);
        return false;
      }
      if (this.CurrentCell.RowIndex == -2)
      {
        if (this.AllowFiltering)
        {
          if ((!this.IsInEditMode || this.EndEdit()) && this.GetRowElement(-3, this.GetTableElement(this.CurrentCell.ViewInfo)) is VirtualGridFilterRowElement)
          {
            this.InputBehavior.SelectCell(-3, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
            return true;
          }
        }
        else
        {
          if (this.MasterViewInfo.RowsViewState.TopPinnedItems.Count > 0)
            this.InputBehavior.SelectCell(this.CurrentCell.ViewInfo.RowsViewState.TopPinnedItems[0], this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          else
            this.InputBehavior.SelectCell(0, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          return true;
        }
      }
      else
      {
        if (this.CurrentCell.RowIndex == -3)
        {
          if (this.MasterViewInfo.RowsViewState.TopPinnedItems.Count > 0)
            this.InputBehavior.SelectCell(this.CurrentCell.ViewInfo.RowsViewState.TopPinnedItems[0], this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          else
            this.InputBehavior.SelectCell(0, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          return true;
        }
        int itemIndex = this.CurrentCell.RowIndex + 1;
        while (this.MasterViewInfo.RowsViewState.IsPinned(itemIndex) && itemIndex < this.MasterViewInfo.RowCount)
          ++itemIndex;
        if (itemIndex < this.MasterViewInfo.RowCount)
          return this.MoveCurrent(itemIndex - this.CurrentCell.RowIndex, 0, keepSelection);
        if (bottomPinnedItems.Count > 0)
          return this.MoveCurrent(bottomPinnedItems[0] - this.CurrentCell.RowIndex, 0, keepSelection);
      }
      return false;
    }

    public bool MoveCurrentUp(bool keepSelection)
    {
      if (this.CurrentCell == null)
      {
        if (this.RowCount <= 0 || this.ColumnCount <= 0)
          return false;
        this.CurrentCell = new VirtualGridCellInfo(0, 0, this.MasterViewInfo);
        return true;
      }
      ReadOnlyCollection<int> topPinnedItems = this.MasterViewInfo.RowsViewState.TopPinnedItems;
      ReadOnlyCollection<int> bottomPinnedItems = this.MasterViewInfo.RowsViewState.BottomPinnedItems;
      if (bottomPinnedItems.Contains(this.CurrentCell.RowIndex))
      {
        int num = bottomPinnedItems.IndexOf(this.CurrentCell.RowIndex);
        if (num > 0)
          return this.MoveCurrent(bottomPinnedItems[num - 1] - this.CurrentCell.RowIndex, 0, keepSelection);
        int itemIndex = this.MasterViewInfo.RowCount - 1;
        while (this.MasterViewInfo.RowsViewState.IsPinned(itemIndex) && itemIndex > 0)
          --itemIndex;
        if (itemIndex > 0)
          return this.MoveCurrent(itemIndex - this.CurrentCell.RowIndex, 0, keepSelection);
        if (topPinnedItems.Count > 0)
          return this.MoveCurrent(topPinnedItems[topPinnedItems.Count - 1] - this.CurrentCell.RowIndex, 0, keepSelection);
        return false;
      }
      if (topPinnedItems.Contains(this.CurrentCell.RowIndex))
      {
        int num = topPinnedItems.IndexOf(this.CurrentCell.RowIndex);
        if (num > 0)
          return this.MoveCurrent(topPinnedItems[num - 1] - this.CurrentCell.RowIndex, 0, keepSelection);
        return false;
      }
      int num1 = this.CurrentCell.ViewInfo.RowsViewState.TopPinnedItems.IndexOf(this.CurrentCell.RowIndex);
      int rowIndex = this.CurrentCell.RowIndex;
      if (num1 == 0 || rowIndex == 0)
      {
        if (this.AllowFiltering)
        {
          this.InputBehavior.SelectCell(-3, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          return true;
        }
        if (this.AllowAddNewRow)
        {
          this.InputBehavior.SelectCell(-2, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          return true;
        }
      }
      else
      {
        if (this.CurrentCell.RowIndex == -3 && this.AllowAddNewRow)
        {
          this.InputBehavior.SelectCell(-2, this.CurrentCell.ColumnIndex, this.CurrentCell.ViewInfo);
          return true;
        }
        int itemIndex = this.CurrentCell.RowIndex - 1;
        while (this.MasterViewInfo.RowsViewState.IsPinned(itemIndex) && itemIndex > 0)
          --itemIndex;
        if (itemIndex >= 0)
          return this.MoveCurrent(itemIndex - this.CurrentCell.RowIndex, 0, keepSelection);
        if (topPinnedItems.Count > 0)
          return this.MoveCurrent(topPinnedItems[topPinnedItems.Count - 1] - this.CurrentCell.RowIndex, 0, keepSelection);
      }
      return false;
    }

    public bool MoveCurrent(int rowOffset, int columnOffset, bool keepSelection)
    {
      if (this.CurrentCell == null)
      {
        this.CurrentCell = new VirtualGridCellInfo(0, 0, this.masterViewInfo);
        this.Selection.BeginSelection(0, 0, this.masterViewInfo, false);
        return true;
      }
      int row = this.CurrentCell.RowIndex + rowOffset;
      int column = this.CurrentCell.ColumnIndex + columnOffset;
      bool flag = rowOffset == 0 && -2 == row;
      if (column < 0 && columnOffset < 0)
      {
        --row;
        column = this.CurrentCell.ViewInfo.ColumnCount - 1;
      }
      if (row < 0 && !flag)
        return false;
      if (column >= this.CurrentCell.ViewInfo.ColumnCount)
      {
        ++row;
        column = 0;
      }
      if (row >= this.CurrentCell.ViewInfo.RowCount || column >= this.CurrentCell.ViewInfo.ColumnCount)
        return false;
      if (this.EnablePaging)
      {
        if (row >= (this.PageIndex + 1) * this.PageSize)
          ++this.PageIndex;
        if (row < this.PageIndex * this.PageSize && this.PageIndex > 0)
          --this.PageIndex;
      }
      this.InputBehavior.SelectCell(row, column, keepSelection, false, this.CurrentCell.ViewInfo);
      return true;
    }

    public void EnsureRowVisible(int rowIndex)
    {
      this.EnsureRowVisible(rowIndex, this.MasterViewInfo);
    }

    public void EnsureRowVisible(int rowIndex, VirtualGridViewInfo viewInfo)
    {
      this.EnsureRowVisible(rowIndex, this.GetTableElement(viewInfo));
    }

    public void EnsureRowVisible(int rowIndex, VirtualGridTableElement tableElement)
    {
      if (tableElement == null || rowIndex < 0 || tableElement.IsRowPinned(rowIndex))
        return;
      tableElement.UpdateLayout();
      ScrollableVirtualRowsContainer scrollableRows = tableElement.ViewElement.ScrollableRows;
      foreach (VirtualGridRowElement child in scrollableRows.Children)
      {
        if (child.RowIndex == rowIndex)
        {
          if (child.ControlBoundingRectangle.Top < scrollableRows.ControlBoundingRectangle.Top)
            tableElement.RowScroller.Scrollbar.Value += child.ControlBoundingRectangle.Top - scrollableRows.ControlBoundingRectangle.Top;
          if (child.ControlBoundingRectangle.Bottom <= scrollableRows.ControlBoundingRectangle.Bottom || tableElement.IsRowExpanded(rowIndex))
            return;
          tableElement.RowScroller.Scrollbar.Value += child.ControlBoundingRectangle.Bottom - scrollableRows.ControlBoundingRectangle.Bottom;
          return;
        }
      }
      int itemIndex = 0;
      if (tableElement.ViewInfo.EnablePaging)
        itemIndex = Math.Min(tableElement.ViewInfo.RowsViewState.ItemCount - 1, tableElement.ViewInfo.RowsViewState.PageIndex * tableElement.ViewInfo.RowsViewState.PageSize);
      int num = tableElement.ViewInfo.RowsViewState.GetItemOffset(rowIndex) - tableElement.ViewInfo.RowsViewState.GetItemOffset(itemIndex);
      if (rowIndex > tableElement.RowScroller.Traverser.Current)
        num -= scrollableRows.ControlBoundingRectangle.Height - tableElement.ViewInfo.GetRowHeight(rowIndex);
      tableElement.RowScroller.Scrollbar.Value = num;
      tableElement.UpdateLayout();
    }

    public void EnsureCellVisible(int rowIndex, int columnIndex)
    {
      this.EnsureCellVisible(rowIndex, columnIndex, this.MasterViewInfo);
    }

    public void EnsureCellVisible(int rowIndex, int columnIndex, VirtualGridViewInfo viewInfo)
    {
      this.EnsureCellVisible(rowIndex, columnIndex, this.GetTableElement(viewInfo));
    }

    public void EnsureCellVisible(
      int rowIndex,
      int columnIndex,
      VirtualGridTableElement tableElement)
    {
      if (tableElement == null || rowIndex < 0 || columnIndex < 0)
        return;
      this.EnsureRowVisible(rowIndex, tableElement);
      if (tableElement.IsColumnPinned(columnIndex))
        return;
      VirtualGridRowElement rowElement = this.GetRowElement(rowIndex, tableElement);
      if (rowElement == null)
        return;
      foreach (VirtualGridCellElement child in rowElement.CellContainer.Children)
      {
        if (child.ColumnIndex == columnIndex)
        {
          RadScrollBarElement scrollbar = tableElement.ColumnScroller.Scrollbar;
          if (child.ControlBoundingRectangle.Left < rowElement.CellContainer.ControlBoundingRectangle.Left)
          {
            int num = child.ControlBoundingRectangle.Left - rowElement.CellContainer.ControlBoundingRectangle.Left;
            if (scrollbar.Value + num > 0 && scrollbar.Value + num < scrollbar.Maximum - scrollbar.LargeChange + 1)
              scrollbar.Value += num;
          }
          if (child.ControlBoundingRectangle.Right <= rowElement.CellContainer.ControlBoundingRectangle.Right)
            return;
          int num1 = child.ControlBoundingRectangle.Right - rowElement.CellContainer.ControlBoundingRectangle.Right;
          if (scrollbar.Value + num1 <= 0 || scrollbar.Value + num1 >= scrollbar.Maximum - scrollbar.LargeChange + 1)
            return;
          scrollbar.Value += num1;
          return;
        }
      }
      int columnScrollOffset = this.GetColumnScrollOffset(columnIndex, tableElement);
      if (columnIndex > tableElement.ColumnScroller.Traverser.Current)
        columnScrollOffset -= rowElement.CellContainer.ControlBoundingRectangle.Width - tableElement.ViewInfo.GetColumnWidth(columnIndex);
      tableElement.ColumnScroller.Scrollbar.Value = columnScrollOffset;
    }

    public int GetRowScrollOffset(int rowIndex, VirtualGridViewInfo viewInfo)
    {
      if (viewInfo == null)
        return 0;
      return viewInfo.RowsViewState.GetItemScrollOffset(rowIndex);
    }

    public int GetRowScrollOffset(int rowIndex, VirtualGridTableElement tableElement)
    {
      return this.GetRowScrollOffset(rowIndex, tableElement.ViewInfo);
    }

    public int GetColumnScrollOffset(int columnIndex, VirtualGridViewInfo viewInfo)
    {
      if (viewInfo == null)
        return 0;
      return viewInfo.ColumnsViewState.GetItemOffset(columnIndex);
    }

    public int GetColumnScrollOffset(int columnIndex, VirtualGridTableElement tableElement)
    {
      return this.GetColumnScrollOffset(columnIndex, tableElement.ViewInfo);
    }

    public VirtualGridTableElement GetTableElement(
      VirtualGridViewInfo viewInfo)
    {
      foreach (VirtualGridTableElement descendant in this.GetDescendants((Predicate<RadElement>) (e => e is VirtualGridTableElement), TreeTraversalMode.BreadthFirst))
      {
        if (descendant.ViewInfo == viewInfo)
          return descendant;
      }
      return (VirtualGridTableElement) null;
    }

    public VirtualGridRowElement GetRowElement(
      int rowIndex,
      VirtualGridViewInfo viewInfo)
    {
      return this.GetRowElement(rowIndex, this.GetTableElement(viewInfo));
    }

    public VirtualGridRowElement GetRowElement(
      int rowIndex,
      VirtualGridTableElement tableElement)
    {
      if (tableElement == null)
        return (VirtualGridRowElement) null;
      foreach (VirtualGridRowElement rowElement in tableElement.ViewElement.GetRowElements())
      {
        if (rowElement.RowIndex == rowIndex)
          return rowElement;
      }
      return (VirtualGridRowElement) null;
    }

    public VirtualGridCellElement GetCellElement(
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo)
    {
      return this.GetCellElement(rowIndex, columnIndex, this.GetTableElement(viewInfo));
    }

    public VirtualGridCellElement GetCellElement(
      int rowIndex,
      int columnIndex,
      VirtualGridTableElement tableElement)
    {
      VirtualGridRowElement rowElement = this.GetRowElement(rowIndex, tableElement);
      if (rowElement == null)
        return (VirtualGridCellElement) null;
      foreach (VirtualGridCellElement cellElement in rowElement.GetCellElements())
      {
        if (cellElement.ColumnIndex == columnIndex)
          return cellElement;
      }
      return (VirtualGridCellElement) null;
    }

    public void BestFitColumn(int columnIndex)
    {
      this.BestFitColumn(columnIndex, this.masterViewInfo);
    }

    public void BestFitColumn(int columnIndex, VirtualGridViewInfo viewInfo)
    {
      this.bestFitQueue.Enqueue(new VirtualGridCellInfo(0, columnIndex, viewInfo));
      this.ProcessBestFitRequests();
    }

    public void BestFitColumns()
    {
      this.BestFitColumns(this.masterViewInfo);
    }

    public void BestFitColumns(VirtualGridViewInfo viewInfo)
    {
      VirtualGridTableElement tableElement = this.GetTableElement(viewInfo);
      if (tableElement == null)
        return;
      int num = -tableElement.ColumnScroller.ScrollOffset;
      foreach (int columnIndex in (IEnumerable) tableElement.ColumnScroller.Traverser)
      {
        this.BestFitColumn(columnIndex, viewInfo);
        num += viewInfo.GetColumnWidth(columnIndex);
        if (num > tableElement.ControlBoundingRectangle.Width)
          break;
      }
    }

    private void ProcessBestFitRequests()
    {
      if (!this.CanExecuteLayoutOperation())
        return;
      while (this.bestFitQueue.Count > 0)
      {
        VirtualGridCellInfo virtualGridCellInfo = this.bestFitQueue.Dequeue();
        VirtualGridTableElement tableElement = this.GetTableElement(virtualGridCellInfo.ViewInfo);
        if (tableElement != null)
        {
          float val1 = 5f;
          bool flag = false;
          foreach (VirtualGridRowElement rowElement in tableElement.ViewElement.GetRowElements())
          {
            foreach (VirtualGridCellElement cellElement in rowElement.GetCellElements())
            {
              if (cellElement.ColumnIndex == virtualGridCellInfo.ColumnIndex)
              {
                flag = true;
                cellElement.BestFitMeasure = true;
                cellElement.Measure(LayoutUtils.InfinitySize);
                cellElement.BestFitMeasure = false;
                val1 = Math.Max(val1, cellElement.DesiredSize.Width);
                break;
              }
            }
          }
          if (flag)
            virtualGridCellInfo.ViewInfo.SetColumnWidth(virtualGridCellInfo.ColumnIndex, (int) Math.Round((double) val1));
        }
      }
      this.TableElement.ColumnLayout.ResetCache();
      this.InvalidateMeasure(true);
      this.UpdateLayout();
    }

    public bool CutSelection()
    {
      if (!this.Selection.HasSelection || !this.AllowCut || !this.AllowCopy)
        return false;
      int minRowIndex = this.Selection.MinRowIndex;
      int maxRowIndex = this.Selection.MaxRowIndex;
      int minColumnIndex = this.Selection.MinColumnIndex;
      int maxColumnIndex = this.Selection.MaxColumnIndex;
      this.CopyToClipboard(minRowIndex, minColumnIndex, maxRowIndex, maxColumnIndex, this.Selection.CurrentViewInfo, true, true);
      return true;
    }

    public bool CopySelection()
    {
      if (!this.Selection.HasSelection || !this.AllowCopy)
        return false;
      int minRowIndex = this.Selection.MinRowIndex;
      int maxRowIndex = this.Selection.MaxRowIndex;
      int minColumnIndex = this.Selection.MinColumnIndex;
      int maxColumnIndex = this.Selection.MaxColumnIndex;
      this.CopyToClipboard(minRowIndex, minColumnIndex, maxRowIndex, maxColumnIndex, this.Selection.CurrentViewInfo, true, false);
      return true;
    }

    public void CopyToClipboard(
      int startRow,
      int startColumn,
      int endRow,
      int endColumn,
      VirtualGridViewInfo viewInfo)
    {
      this.CopyToClipboard(startRow, startColumn, endRow, endColumn, viewInfo, false, false);
    }

    public void CopyToClipboard(
      int startRow,
      int startColumn,
      int endRow,
      int endColumn,
      VirtualGridViewInfo viewInfo,
      bool selectedOnly,
      bool cut)
    {
      DataObject dataObject = new DataObject();
      string str1 = this.ProcessContent(DataFormats.Text, startRow, startColumn, endRow, endColumn, viewInfo, selectedOnly, cut);
      dataObject.SetData(DataFormats.UnicodeText, false, (object) str1);
      VirtualGridClipboardEventArgs args1 = new VirtualGridClipboardEventArgs(false, DataFormats.UnicodeText, dataObject, this.CurrentCell.ViewInfo);
      this.OnCopying(args1);
      if (args1.Cancel)
        return;
      dataObject.SetData(DataFormats.Text, false, (object) str1);
      VirtualGridClipboardEventArgs args2 = new VirtualGridClipboardEventArgs(false, DataFormats.Text, dataObject, this.CurrentCell.ViewInfo);
      this.OnCopying(args2);
      if (args2.Cancel)
        return;
      string str2 = this.ProcessContent(DataFormats.CommaSeparatedValue, startRow, startColumn, endRow, endColumn, viewInfo, selectedOnly, cut);
      dataObject.SetData(DataFormats.CommaSeparatedValue, false, (object) str2);
      VirtualGridClipboardEventArgs args3 = new VirtualGridClipboardEventArgs(false, DataFormats.CommaSeparatedValue, dataObject, this.CurrentCell.ViewInfo);
      this.OnCopying(args3);
      if (args3.Cancel)
        return;
      Clipboard.SetDataObject((object) dataObject);
    }

    protected virtual string ProcessContent(
      string format,
      int startRow,
      int startColumn,
      int endRow,
      int endColumn,
      VirtualGridViewInfo viewInfo,
      bool selectedOnly,
      bool cut)
    {
      StringBuilder stringBuilder = new StringBuilder();
      VirtualGridRowElement element1 = (VirtualGridRowElement) this.TableElement.RowScroller.ElementProvider.GetElement(0, (object) null);
      element1.Initialize(this.TableElement);
      VirtualGridCellElement element2 = (VirtualGridCellElement) this.TableElement.ColumnScroller.ElementProvider.GetElement(0, (object) element1);
      bool flag1 = this.SelectionMode == VirtualGridSelectionMode.FullRowSelect && this.Selection.MinRowIndex < this.Selection.MaxRowIndex;
      if (flag1)
      {
        startColumn = 0;
        endColumn = viewInfo.ColumnCount - 1;
      }
      bool flag2 = false;
      int num1 = startRow;
      while (num1 <= endRow)
      {
        if (!selectedOnly || this.Selection.RowContainsSelection(num1, viewInfo))
        {
          int num2 = startColumn;
          bool flag3 = false;
          element1.Detach();
          element1.Attach(num1, (object) null);
          while (num2 <= endColumn)
          {
            if (!selectedOnly || flag1 || this.Selection.IsSelected(num1, num2, viewInfo))
            {
              if (flag3)
                stringBuilder.Append(format == DataFormats.CommaSeparatedValue ? ',' : '\t');
              else if (flag2)
              {
                stringBuilder.Append('\r');
                stringBuilder.Append('\n');
              }
              element2.Detach();
              element2.Attach(num2, (object) element1);
              stringBuilder.Append(element2.Text);
              flag3 = true;
              if (cut && !flag1)
                this.OnCellValuePushed(new VirtualGridCellValuePushedEventArgs((object) null, num1, num2, viewInfo));
            }
            if (num2++ > endColumn)
              break;
          }
          if (flag3)
          {
            flag2 = true;
            if (cut && flag1)
              this.OnUserDeletedRow(new VirtualGridRowsEventArgs((IEnumerable<int>) new int[1]
              {
                num1
              }, viewInfo));
          }
        }
        if (num1++ > endRow)
          break;
      }
      return stringBuilder.ToString();
    }

    public virtual bool Paste()
    {
      if (!this.AllowPaste || !this.AllowEdit || this.CurrentCell == null)
        return false;
      object dataObject = (object) Clipboard.GetDataObject();
      List<List<string>> stringListList = new List<List<string>>();
      if (Clipboard.ContainsData(DataFormats.Html))
      {
        VirtualGridClipboardEventArgs args = new VirtualGridClipboardEventArgs(false, DataFormats.Html, dataObject as DataObject, this.CurrentCell.ViewInfo);
        this.OnPasting(args);
        if (!args.Cancel)
          stringListList = this.GetHtmlData();
      }
      if (stringListList.Count == 0 && (Clipboard.ContainsData(DataFormats.UnicodeText) || Clipboard.ContainsData(DataFormats.Text)))
      {
        VirtualGridClipboardEventArgs args = new VirtualGridClipboardEventArgs(false, DataFormats.Text, dataObject as DataObject, this.CurrentCell.ViewInfo);
        this.OnPasting(args);
        if (!args.Cancel)
          stringListList = this.GetTextData();
      }
      if (stringListList.Count == 0 && Clipboard.ContainsData(DataFormats.CommaSeparatedValue))
      {
        VirtualGridClipboardEventArgs args = new VirtualGridClipboardEventArgs(false, DataFormats.CommaSeparatedValue, dataObject as DataObject, this.CurrentCell.ViewInfo);
        this.OnPasting(args);
        if (!args.Cancel)
          stringListList = this.GetCsvData();
      }
      int columnIndex = this.CurrentCell.ColumnIndex;
      int rowIndex = this.CurrentCell.RowIndex;
      for (int index1 = 0; index1 < stringListList.Count && rowIndex + index1 < this.CurrentCell.ViewInfo.RowCount; ++index1)
      {
        for (int index2 = 0; index2 < stringListList[index1].Count && columnIndex + index2 < this.CurrentCell.ViewInfo.ColumnCount; ++index2)
          this.OnCellValuePushed(new VirtualGridCellValuePushedEventArgs((object) stringListList[index1][index2], rowIndex + index1, columnIndex + index2, this.CurrentCell.ViewInfo));
      }
      this.GetTableElement(this.CurrentCell.ViewInfo).SynchronizeRows();
      return true;
    }

    private List<List<string>> GetHtmlData()
    {
      List<List<string>> stringListList = new List<List<string>>();
      List<string> stringList = new List<string>();
      StringTokenizer stringTokenizer = new StringTokenizer(Clipboard.GetData(DataFormats.Html).ToString(), "<");
      for (int index = stringTokenizer.Count(); index > 0; --index)
      {
        string str1 = stringTokenizer.NextToken();
        if (str1.Equals("tr>", StringComparison.InvariantCultureIgnoreCase))
        {
          stringList = new List<string>();
          stringListList.Add(stringList);
        }
        if (str1.StartsWith("td>", StringComparison.InvariantCultureIgnoreCase))
        {
          int num = str1.IndexOf('>');
          if (num > 0)
          {
            string str2 = str1.Remove(0, num + 1).Replace("&nbsp;", " ").Replace("&quot;", "\"").Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
            stringList.Add(str2);
          }
        }
      }
      return stringListList;
    }

    private List<List<string>> GetCsvData()
    {
      List<List<string>> stringListList = new List<List<string>>();
      StringTokenizer stringTokenizer1 = new StringTokenizer(Clipboard.GetData(DataFormats.CommaSeparatedValue).ToString(), "\n");
      for (int index1 = stringTokenizer1.Count(); index1 > 0; --index1)
      {
        List<string> stringList = new List<string>();
        stringListList.Add(stringList);
        StringTokenizer stringTokenizer2 = new StringTokenizer(stringTokenizer1.NextToken(), ",");
        for (int index2 = stringTokenizer2.Count(); index2 > 0; --index2)
        {
          string str = stringTokenizer2.NextToken().Trim();
          stringList.Add(str);
        }
      }
      return stringListList;
    }

    private List<List<string>> GetTextData()
    {
      List<List<string>> stringListList = new List<List<string>>();
      StringTokenizer stringTokenizer = new StringTokenizer(Clipboard.GetData(DataFormats.UnicodeText).ToString(), "\n");
      for (int index = stringTokenizer.Count(); index > 0; --index)
      {
        List<string> stringList = new List<string>();
        stringListList.Add(stringList);
        string str1 = stringTokenizer.NextToken();
        char[] chArray = new char[1]{ '\t' };
        foreach (string str2 in str1.Split(chArray))
          stringList.Add(str2.Trim());
      }
      return stringListList;
    }

    internal static T GetElementAtPoint<T>(RadElementTree componentTree, Point point) where T : RadElement
    {
      if (componentTree != null)
      {
        for (RadElement radElement = componentTree.GetElementAtPoint(point); radElement != null; radElement = radElement.Parent)
        {
          T obj = radElement as T;
          if ((object) obj != null)
            return obj;
        }
      }
      return default (T);
    }

    private void RadVirtualGridLocalizationProvider_CurrentProviderChanged(
      object sender,
      EventArgs e)
    {
      (this.contextMenu as VirtualGridContextMenu)?.InitializeMenuItemsText();
      this.tableElement.UpdateNoDataText();
      if (this.TableElement.PagingPanelElement == null)
        return;
      this.TableElement.PagingPanelElement.UpdateView();
    }
  }
}
