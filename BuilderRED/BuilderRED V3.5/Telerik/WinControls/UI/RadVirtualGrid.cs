// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadVirtualGrid
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.Licensing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadVirtualGridDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Displays data in tabular format, providing multi-level hierarchy, sorting, filtering and more")]
  [ToolboxItem(true)]
  [TelerikToolboxCategory("Data Controls")]
  public class RadVirtualGrid : RadControl
  {
    public const int HeaderRowIndex = -1;
    public const int NewRowIndex = -2;
    public const int FilterRowIndex = -3;
    public const int IndentCellIndex = -1;
    private RadVirtualGridElement virtualGridElement;
    private ComponentXmlSerializationInfo xmlSerializationInfo;
    private Point mouseDownLocation;

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

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
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

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
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

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VirtualGridSelection Selection
    {
      get
      {
        return this.VirtualGridElement.Selection;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public VirtualGridCellInfo CurrentCell
    {
      get
      {
        return this.VirtualGridElement.CurrentCell;
      }
      set
      {
        this.VirtualGridElement.CurrentCell = value;
      }
    }

    [DefaultValue(VirtualGridSelectionMode.CellSelect)]
    public VirtualGridSelectionMode SelectionMode
    {
      get
      {
        return this.VirtualGridElement.SelectionMode;
      }
      set
      {
        this.VirtualGridElement.SelectionMode = value;
      }
    }

    [DefaultValue(false)]
    public bool MultiSelect
    {
      get
      {
        return this.VirtualGridElement.MultiSelect;
      }
      set
      {
        this.VirtualGridElement.MultiSelect = value;
      }
    }

    [DefaultValue(false)]
    public bool UseScrollbarsInHierarchy
    {
      get
      {
        return this.VirtualGridElement.UseScrollbarsInHierarchy;
      }
      set
      {
        this.VirtualGridElement.UseScrollbarsInHierarchy = value;
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

    [DefaultValue(100)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
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

    [DefaultValue(false)]
    public bool StandardTab
    {
      get
      {
        return this.VirtualGridElement.StandardTab;
      }
      set
      {
        this.VirtualGridElement.StandardTab = value;
      }
    }

    [DefaultValue(true)]
    public bool ShowNoDataText
    {
      get
      {
        return this.VirtualGridElement.ShowNoDataText;
      }
      set
      {
        this.VirtualGridElement.ShowNoDataText = value;
      }
    }

    [Description("Gets or sets a value indicating how column widths are determined.")]
    [DefaultValue(VirtualGridAutoSizeColumnsMode.None)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VirtualGridAutoSizeColumnsMode AutoSizeColumnsMode
    {
      get
      {
        return this.MasterViewInfo.AutoSizeColumnsMode;
      }
      set
      {
        this.MasterViewInfo.AutoSizeColumnsMode = value;
      }
    }

    [Description(" Gets or sets a value indicating whether there is a visual indication for the row currently under the mouse.")]
    [DefaultValue(true)]
    public bool EnableHotTracking
    {
      get
      {
        return this.TableElement.EnableHotTracking;
      }
      set
      {
        this.TableElement.EnableHotTracking = value;
      }
    }

    [Browsable(false)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.VirtualGridElement.SortDescriptors;
      }
    }

    [Browsable(false)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.VirtualGridElement.FilterDescriptors;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(240, 150));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadVirtualGridElement VirtualGridElement
    {
      get
      {
        return this.virtualGridElement;
      }
    }

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.VirtualGridElement.TableElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public VirtualGridViewInfo MasterViewInfo
    {
      get
      {
        return this.VirtualGridElement.MasterViewInfo;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.virtualGridElement = this.CreateElement();
      parent.Children.Add((RadElement) this.virtualGridElement);
    }

    protected virtual RadVirtualGridElement CreateElement()
    {
      return new RadVirtualGridElement();
    }

    public void BeginUpdate()
    {
      this.VirtualGridElement.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.VirtualGridElement.EndUpdate();
    }

    public void SelectAll()
    {
      this.VirtualGridElement.Selection.SelectAll();
    }

    public void SelectCell(int row, int column)
    {
      this.VirtualGridElement.InputBehavior.SelectCell(row, column, this.MasterViewInfo);
    }

    public void SelectCell(int row, int column, VirtualGridViewInfo viewInfo)
    {
      this.VirtualGridElement.InputBehavior.SelectCell(row, column, viewInfo);
    }

    public void BestFitColumns()
    {
      this.VirtualGridElement.BestFitColumns();
    }

    public void BestFitColumns(VirtualGridViewInfo viewInfo)
    {
      this.VirtualGridElement.BestFitColumns(viewInfo);
    }

    [DefaultValue(RadVirtualGridBeginEditMode.BeginEditOnKeystrokeOrF2)]
    public RadVirtualGridBeginEditMode BeginEditMode
    {
      get
      {
        return this.VirtualGridElement.BeginEditMode;
      }
      set
      {
        this.VirtualGridElement.BeginEditMode = value;
      }
    }

    [DefaultValue(RadVirtualGridEnterKeyMode.None)]
    public RadVirtualGridEnterKeyMode EnterKeyMode
    {
      get
      {
        return this.VirtualGridElement.EnterKeyMode;
      }
      set
      {
        this.VirtualGridElement.EnterKeyMode = value;
      }
    }

    public void BeginEdit()
    {
      this.VirtualGridElement.BeginEdit();
    }

    public bool EndEdit()
    {
      return this.VirtualGridElement.EndEdit();
    }

    public bool CancelEdit()
    {
      return this.VirtualGridElement.CancelEdit();
    }

    public bool IsInEditMode
    {
      get
      {
        return this.VirtualGridElement.IsInEditMode;
      }
    }

    public IInputEditor ActiveEditor
    {
      get
      {
        return this.VirtualGridElement.ActiveEditor;
      }
    }

    public event VirtualGridCellValueNeededEventHandler CellValueNeeded
    {
      add
      {
        this.VirtualGridElement.CellValueNeeded += value;
      }
      remove
      {
        this.VirtualGridElement.CellValueNeeded -= value;
      }
    }

    public event VirtualGridRowElementEventHandler RowFormatting
    {
      add
      {
        this.VirtualGridElement.RowFormatting += value;
      }
      remove
      {
        this.VirtualGridElement.RowFormatting -= value;
      }
    }

    public event VirtualGridCellElementEventHandler CellFormatting
    {
      add
      {
        this.VirtualGridElement.CellFormatting += value;
      }
      remove
      {
        this.VirtualGridElement.CellFormatting -= value;
      }
    }

    public event VirtualGridRowExpandingEventHandler RowExpanding
    {
      add
      {
        this.VirtualGridElement.RowExpanding += value;
      }
      remove
      {
        this.VirtualGridElement.RowExpanding -= value;
      }
    }

    public event VirtualGridRowExpandingEventHandler RowCollapsing
    {
      add
      {
        this.VirtualGridElement.RowCollapsing += value;
      }
      remove
      {
        this.VirtualGridElement.RowCollapsing -= value;
      }
    }

    public event VirtualGridCellInfoCancelEventHandler CurrentCellChanging
    {
      add
      {
        this.VirtualGridElement.CurrentCellChanging += value;
      }
      remove
      {
        this.VirtualGridElement.CurrentCellChanging -= value;
      }
    }

    public event EventHandler CurrentCellChanged
    {
      add
      {
        this.VirtualGridElement.CurrentCellChanged += value;
      }
      remove
      {
        this.VirtualGridElement.CurrentCellChanged -= value;
      }
    }

    public event VirtualGridEventHandler SortChanged
    {
      add
      {
        this.VirtualGridElement.SortDescriptorsChanged += value;
      }
      remove
      {
        this.VirtualGridElement.SortDescriptorsChanged -= value;
      }
    }

    public event VirtualGridEventHandler FilterChanged
    {
      add
      {
        this.VirtualGridElement.FilterDescriptorsChanged += value;
      }
      remove
      {
        this.VirtualGridElement.FilterDescriptorsChanged -= value;
      }
    }

    public event VirtualGridRowsEventHandler UserDeletedRow
    {
      add
      {
        this.VirtualGridElement.UserDeletedRow += value;
      }
      remove
      {
        this.VirtualGridElement.UserDeletedRow -= value;
      }
    }

    public event VirtualGridNewRowEventHandler UserAddedRow
    {
      add
      {
        this.VirtualGridElement.UserAddedRow += value;
      }
      remove
      {
        this.VirtualGridElement.UserAddedRow -= value;
      }
    }

    public event VirtualGridEventHandler PageChanged
    {
      add
      {
        this.VirtualGridElement.PageIndexChanged += value;
      }
      remove
      {
        this.VirtualGridElement.PageIndexChanged -= value;
      }
    }

    public event VirtualGridPageChangingEventHandler PageChanging
    {
      add
      {
        this.VirtualGridElement.PageIndexChanging += value;
      }
      remove
      {
        this.VirtualGridElement.PageIndexChanging -= value;
      }
    }

    public event VirtualGridCreateRowEventHandler CreateRowElement
    {
      add
      {
        this.VirtualGridElement.CreateRowElement += value;
      }
      remove
      {
        this.VirtualGridElement.CreateRowElement -= value;
      }
    }

    public event VirtualGridCreateCellEventHandler CreateCellElement
    {
      add
      {
        this.VirtualGridElement.CreateCellElement += value;
      }
      remove
      {
        this.VirtualGridElement.CreateCellElement -= value;
      }
    }

    public event VirtualGridCellEditorInitializedEventHandler CellEditorInitialized
    {
      add
      {
        this.VirtualGridElement.CellEditorInitialized += value;
      }
      remove
      {
        this.VirtualGridElement.CellEditorInitialized -= value;
      }
    }

    public event VirtualGridEditorRequiredEventHandler EditorRequired
    {
      add
      {
        this.VirtualGridElement.EditorRequired += value;
      }
      remove
      {
        this.VirtualGridElement.EditorRequired -= value;
      }
    }

    public event VirtualGridCellValuePushedEventHandler CellValuePushed
    {
      add
      {
        this.VirtualGridElement.CellValuePushed += value;
      }
      remove
      {
        this.VirtualGridElement.CellValuePushed -= value;
      }
    }

    public event ValueChangingEventHandler ValueChanging
    {
      add
      {
        this.VirtualGridElement.ValueChanging += value;
      }
      remove
      {
        this.VirtualGridElement.ValueChanging -= value;
      }
    }

    public event EventHandler ValueChanged
    {
      add
      {
        this.VirtualGridElement.ValueChanged += value;
      }
      remove
      {
        this.VirtualGridElement.ValueChanged -= value;
      }
    }

    public event VirtualGridContextMenuOpeningEventHandler ContextMenuOpening
    {
      add
      {
        this.VirtualGridElement.ContextMenuOpening += value;
      }
      remove
      {
        this.VirtualGridElement.ContextMenuOpening -= value;
      }
    }

    public event VirtualGridSelectionChangingEventHandler SelectionChanging
    {
      add
      {
        this.VirtualGridElement.SelectionChanging += value;
      }
      remove
      {
        this.VirtualGridElement.SelectionChanging -= value;
      }
    }

    public event EventHandler SelectionChanged
    {
      add
      {
        this.VirtualGridElement.SelectionChanged += value;
      }
      remove
      {
        this.VirtualGridElement.SelectionChanged -= value;
      }
    }

    public event VirtualGridRowExpandedEventHandler RowExpanded
    {
      add
      {
        this.VirtualGridElement.RowExpanded += value;
      }
      remove
      {
        this.VirtualGridElement.RowExpanded -= value;
      }
    }

    public event VirtualGridRowExpandedEventHandler RowCollapsed
    {
      add
      {
        this.VirtualGridElement.RowCollapsed += value;
      }
      remove
      {
        this.VirtualGridElement.RowCollapsed -= value;
      }
    }

    public event VirtualGridColumnWidthChangingEventHandler ColumnWidthChanging
    {
      add
      {
        this.VirtualGridElement.ColumnWidthChanging += value;
      }
      remove
      {
        this.VirtualGridElement.ColumnWidthChanging -= value;
      }
    }

    public event VirtualGridColumnEventHandler ColumnWidthChanged
    {
      add
      {
        this.VirtualGridElement.ColumnWidthChanged += value;
      }
      remove
      {
        this.VirtualGridElement.ColumnWidthChanged -= value;
      }
    }

    public event VirtualGridRowHeightChangingEventHandler RowHeightChanging
    {
      add
      {
        this.VirtualGridElement.RowHeightChanging += value;
      }
      remove
      {
        this.VirtualGridElement.RowHeightChanging -= value;
      }
    }

    public event VirtualGridRowEventHandler RowHeightChanged
    {
      add
      {
        this.VirtualGridElement.RowHeightChanged += value;
      }
      remove
      {
        this.VirtualGridElement.RowHeightChanged -= value;
      }
    }

    public event VirtualGridCellElementEventHandler CellClick
    {
      add
      {
        this.VirtualGridElement.CellClick += value;
      }
      remove
      {
        this.VirtualGridElement.CellClick -= value;
      }
    }

    public event EventHandler CellDoubleClick
    {
      add
      {
        this.VirtualGridElement.CellDoubleClick += value;
      }
      remove
      {
        this.VirtualGridElement.CellDoubleClick -= value;
      }
    }

    public event VirtualGridCellElementMouseEventHandler CellMouseMove
    {
      add
      {
        this.VirtualGridElement.CellMouseMove += value;
      }
      remove
      {
        this.VirtualGridElement.CellMouseMove -= value;
      }
    }

    public event VirtualGridCellPaintEventHandler CellPaint
    {
      add
      {
        this.VirtualGridElement.CellPaint += value;
      }
      remove
      {
        this.VirtualGridElement.CellPaint -= value;
      }
    }

    public event VirtualGridRowPaintEventHandler RowPaint
    {
      add
      {
        this.VirtualGridElement.RowPaint += value;
      }
      remove
      {
        this.VirtualGridElement.RowPaint -= value;
      }
    }

    public event VirtualGridCellValidatingEventHandler CellValidating
    {
      add
      {
        this.VirtualGridElement.CellValidating += value;
      }
      remove
      {
        this.VirtualGridElement.CellValidating -= value;
      }
    }

    public event VirtualGridRowValidatingEventHandler RowValidating
    {
      add
      {
        this.VirtualGridElement.RowValidating += value;
      }
      remove
      {
        this.VirtualGridElement.RowValidating -= value;
      }
    }

    public event VirtualGridRowEventHandler RowValidated
    {
      add
      {
        this.VirtualGridElement.RowValidated += value;
      }
      remove
      {
        this.VirtualGridElement.RowValidated -= value;
      }
    }

    public event VirtualGridClipboardEventHandler Copying
    {
      add
      {
        this.VirtualGridElement.Copying += value;
      }
      remove
      {
        this.VirtualGridElement.Copying -= value;
      }
    }

    public event VirtualGridClipboardEventHandler Pasting
    {
      add
      {
        this.VirtualGridElement.Pasting += value;
      }
      remove
      {
        this.VirtualGridElement.Pasting -= value;
      }
    }

    public event VirtualGridViewInfoPropertyChangedEventHandler ViewInfoPropertyChanged
    {
      add
      {
        this.VirtualGridElement.ViewInfoPropertyChanged += value;
      }
      remove
      {
        this.VirtualGridElement.ViewInfoPropertyChanged -= value;
      }
    }

    public event VirtualGridQueryHasChildRowsEventHandler QueryHasChildRows
    {
      add
      {
        this.VirtualGridElement.QueryHasChildRows += value;
      }
      remove
      {
        this.VirtualGridElement.QueryHasChildRows -= value;
      }
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      this.VirtualGridElement.TableElement.SynchronizeRows(true);
    }

    protected override void OnValidating(CancelEventArgs e)
    {
      if (this.CurrentCell != null)
      {
        if (this.IsInEditMode)
        {
          BaseVirtualGridEditor activeEditor = this.ActiveEditor as BaseVirtualGridEditor;
          if (activeEditor != null && !activeEditor.EndEditOnLostFocus)
          {
            base.OnValidating(e);
            return;
          }
          e.Cancel = !this.EndEdit();
        }
        if (!e.Cancel)
        {
          VirtualGridRowValidatingEventArgs e1 = new VirtualGridRowValidatingEventArgs(this.CurrentCell.RowIndex, this.CurrentCell.ViewInfo);
          this.VirtualGridElement.OnRowValidating(e1);
          e.Cancel = e1.Cancel;
          if (!e1.Cancel)
            this.VirtualGridElement.OnRowValidated(new VirtualGridRowEventArgs(this.CurrentCell.RowIndex, this.CurrentCell.ViewInfo));
        }
      }
      base.OnValidating(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.mouseDownLocation = e.Location;
      if (this.VirtualGridElement.InputBehavior.HandleMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.VirtualGridElement.InputBehavior.HandleMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.VirtualGridElement.InputBehavior.HandleMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.VirtualGridElement.InputBehavior.HandleMouseWheel(e))
        return;
      base.OnMouseWheel(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.VirtualGridElement.InputBehavior.HandleMouseDoubleClick(e))
        return;
      base.OnMouseDoubleClick(e);
      VirtualGridCellElement elementAtPoint = RadVirtualGridElement.GetElementAtPoint<VirtualGridCellElement>((RadElementTree) this.ElementTree, e.Location);
      if (elementAtPoint == null)
        return;
      elementAtPoint.CallDoDoubleClick((EventArgs) e);
      this.VirtualGridElement.OnCellDoubleClick(new VirtualGridCellElementEventArgs(elementAtPoint, elementAtPoint.ViewInfo));
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      base.OnMouseClick(e);
      VirtualGridCellElement elementAtPoint1 = RadVirtualGridElement.GetElementAtPoint<VirtualGridCellElement>((RadElementTree) this.ElementTree, e.Location);
      if (elementAtPoint1 == null)
        return;
      VirtualGridCellElement elementAtPoint2 = RadVirtualGridElement.GetElementAtPoint<VirtualGridCellElement>((RadElementTree) this.ElementTree, this.mouseDownLocation);
      if (elementAtPoint2 != elementAtPoint1)
        return;
      this.VirtualGridElement.OnCellClick(new VirtualGridCellElementEventArgs(elementAtPoint2, elementAtPoint2.ViewInfo));
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!this.VirtualGridElement.IsInEditMode || e.KeyData == Keys.Tab || e.KeyData == Keys.Return)
      {
        if (this.VirtualGridElement.InputBehavior.HandleKeyDown(e))
          return;
        base.OnKeyDown(e);
      }
      else
      {
        if (!(this.ActiveEditor is VirtualGridTextBoxControlEditor))
          return;
        ((this.ActiveEditor as VirtualGridTextBoxControlEditor).EditorElement as RadTextBoxControlElement).InputHandler.ProcessKeyDown(e);
      }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (!this.VirtualGridElement.IsInEditMode)
      {
        if (this.VirtualGridElement.InputBehavior.HandleKeyUp(e))
          return;
        base.OnKeyUp(e);
      }
      else
      {
        if (!(this.ActiveEditor is VirtualGridTextBoxControlEditor))
          return;
        RadTextBoxControlElement editorElement = (this.ActiveEditor as VirtualGridTextBoxControlEditor).EditorElement as RadTextBoxControlElement;
        bool flag = editorElement.CaretIndex == 0;
        if (editorElement.CaretIndex == editorElement.Text.Length && e.KeyData == Keys.Right || flag && e.KeyData == Keys.Left && editorElement.SelectionLength == 0)
          this.VirtualGridElement.InputBehavior.HandleKeyDown(e);
        else
          editorElement.InputHandler.ProcessKeyUp(e);
      }
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (!this.VirtualGridElement.IsInEditMode)
      {
        if (this.VirtualGridElement.InputBehavior.HandleKeyPress(e))
          return;
        base.OnKeyPress(e);
      }
      else
      {
        if (!(this.ActiveEditor is VirtualGridTextBoxControlEditor))
          return;
        ((this.ActiveEditor as VirtualGridTextBoxControlEditor).EditorElement as RadTextBoxControlElement).InputHandler.ProcessKeyPress(e);
      }
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Prior:
        case Keys.Next:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
        case Keys.Left | Keys.Shift:
        case Keys.Up | Keys.Shift:
        case Keys.Right | Keys.Shift:
        case Keys.Down | Keys.Shift:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (!this.IsDisposed && (this.VirtualGridElement.IsInEditMode || !this.VirtualGridElement.StandardTab) && (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift)))
      {
        KeyEventArgs args = new KeyEventArgs(keyData);
        if (this.VirtualGridElement.InputBehavior != null && this.VirtualGridElement.InputBehavior.HandleKeyDown(args))
          return true;
      }
      if (!this.VirtualGridElement.IsInEditMode || keyData != Keys.Escape)
        return base.ProcessDialogKey(keyData);
      this.VirtualGridElement.InputBehavior.HandleKeyDown(new KeyEventArgs(keyData));
      return true;
    }

    public virtual ComponentXmlSerializationInfo GetDefaultXmlSerializationInfo()
    {
      return new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection()
      {
        {
          typeof (Control),
          "Controls",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "DataBindings",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "Tag",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "Size",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "Location",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "Dock",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "Anchor",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (Control),
          "CausesValidation",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (RadControl),
          "ThemeName",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (RadControl),
          "RootElement",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (RadElement),
          "Style",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (RadVirtualGrid),
          "Name",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (RadVirtualGrid),
          "Visible",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (RadComponentElement),
          "DataBindings",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)
          }
        },
        {
          typeof (VirtualGridViewInfo),
          "CustomColumns",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridViewInfo),
          "ColumnDataTypes",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridViewInfo),
          "FilterRowHeight",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridViewInfo),
          "HeaderRowHeight",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridViewInfo),
          "NewRowHeight",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "ItemSizes",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "ItemCount",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "ExpandedSizes",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "ExpandedHeight",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "DefaultItemSize",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "TopPinnedItemsList",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        },
        {
          typeof (VirtualGridTableViewState),
          "BottomPinnedItemsList",
          new Attribute[1]
          {
            (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible)
          }
        }
      });
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ComponentXmlSerializationInfo XmlSerializationInfo
    {
      get
      {
        if (this.xmlSerializationInfo == null)
          this.xmlSerializationInfo = this.GetDefaultXmlSerializationInfo();
        return this.xmlSerializationInfo;
      }
      set
      {
        this.xmlSerializationInfo = value;
      }
    }

    public virtual void SaveLayout(XmlWriter xmlWriter)
    {
      GridViewLayoutSerializer layoutSerializer = new GridViewLayoutSerializer(this.XmlSerializationInfo);
      xmlWriter.WriteStartElement(nameof (RadVirtualGrid));
      layoutSerializer.WriteObjectElement(xmlWriter, (object) this);
      xmlWriter.WriteEndElement();
    }

    public virtual void SaveLayout(Stream stream)
    {
      GridViewLayoutSerializer layoutSerializer = new GridViewLayoutSerializer(this.XmlSerializationInfo);
      StreamWriter streamWriter = new StreamWriter(stream);
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) streamWriter);
      xmlTextWriter.WriteStartElement(nameof (RadVirtualGrid));
      layoutSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      xmlTextWriter.WriteEndElement();
      streamWriter.Flush();
    }

    public virtual void SaveLayout(string fileName)
    {
      GridViewLayoutSerializer layoutSerializer = new GridViewLayoutSerializer(this.XmlSerializationInfo);
      using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, Encoding.UTF8))
      {
        xmlTextWriter.Formatting = Formatting.Indented;
        xmlTextWriter.WriteStartElement(nameof (RadVirtualGrid));
        layoutSerializer.WriteObjectElement((XmlWriter) xmlTextWriter, (object) this);
      }
    }

    public virtual void LoadLayout(string fileName)
    {
      if (!File.Exists(fileName))
      {
        int num = (int) MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(fileName))
        {
          GridViewLayoutSerializer layoutSerializer = new GridViewLayoutSerializer(this.XmlSerializationInfo);
          using (XmlTextReader xmlTextReader = new XmlTextReader((TextReader) streamReader))
            this.LoadLayout((XmlReader) xmlTextReader);
        }
      }
    }

    public virtual void LoadLayout(Stream stream)
    {
      if (stream == null || stream.Length <= 0L)
        return;
      if (stream.Position == stream.Length)
        stream.Position = 0L;
      StreamReader streamReader = new StreamReader(stream);
      GridViewLayoutSerializer layoutSerializer = new GridViewLayoutSerializer(this.XmlSerializationInfo);
      this.LoadLayout((XmlReader) new XmlTextReader((TextReader) streamReader));
    }

    public virtual void LoadLayout(XmlReader xmlReader)
    {
      this.EndEdit();
      bool flag = false;
      try
      {
        this.BeginUpdate();
        this.MasterViewInfo.ResetViewState();
        GridViewLayoutSerializer layoutSerializer = new GridViewLayoutSerializer(this.XmlSerializationInfo);
        xmlReader.Read();
        layoutSerializer.ReadObjectElement(xmlReader, (object) this);
        flag = true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.VirtualGridElement.TableElement.RowScroller.UpdateScrollRange();
        this.VirtualGridElement.TableElement.ColumnScroller.UpdateScrollRange();
        this.VirtualGridElement.TableElement.InvalidatePinnedRows();
        this.VirtualGridElement.MasterViewInfo.RowsViewState.UpdateOnItemSizeChanged();
        this.VirtualGridElement.MasterViewInfo.ColumnsViewState.UpdateOnItemSizeChanged();
        this.EndUpdate();
        this.VirtualGridElement.TableElement.ColumnLayout.ResetCache();
        this.VirtualGridElement.InvalidateMeasure(true);
        this.VirtualGridElement.UpdateLayout();
      }
      if (!flag)
        return;
      this.OnLayoutLoaded((object) this, new LayoutLoadedEventArgs());
    }

    [Browsable(true)]
    [Description("Fires when the layout is loaded.")]
    [Category("Action")]
    public event LayoutLoadedEventHandler LayoutLoaded;

    protected internal void OnLayoutLoaded(object sender, LayoutLoadedEventArgs e)
    {
      if (this.LayoutLoaded == null)
        return;
      this.LayoutLoaded((object) this, e);
    }
  }
}
