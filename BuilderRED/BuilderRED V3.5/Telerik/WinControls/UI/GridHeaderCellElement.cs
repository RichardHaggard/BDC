// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Data;
using Telerik.WinControls.Interfaces;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.Properties;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class GridHeaderCellElement : GridVirtualizedCellElement
  {
    private RadSortOrder? sortOrder = new RadSortOrder?();
    private SizeF lastShowDpiScaleFactor = new SizeF(1f, 1f);
    public static RadProperty IsSortedAscendingProperty = RadProperty.Register(nameof (IsSortedAscending), typeof (bool), typeof (GridHeaderCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSortedDescendingProperty = RadProperty.Register(nameof (IsSortedDescending), typeof (bool), typeof (GridHeaderCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private ArrowPrimitive arrow;
    private GridHeaderCellElement.ArrowPositionEnum arrowPosition;
    private ConditionalFormattingForm conditionalFormattingForm;
    private ReadOnlyCollection<SortDescriptor> groupSortDescriptors;
    private StackLayoutElement stackLayout;
    private GridFilterButtonElement filterFunctionButton;
    private double zoomedColumnWidth;
    private IGridFilterPopup lastFilterPopup;
    private RadGridView grid;

    static GridHeaderCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new HeaderCellStateManagerFactory(), typeof (GridHeaderCellElement));
    }

    public GridHeaderCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
      int num = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) column.HeaderTextAlignment);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.Class = "HeaderCell";
      this.ClipDrawing = true;
      this.DrawFill = true;
      this.BackColor2 = SystemColors.ControlDark;
      this.BorderColor = Color.Black;
    }

    protected override void CreateChildElements()
    {
      this.filterFunctionButton = new GridFilterButtonElement();
      this.filterFunctionButton.NotifyParentOnMouseInput = false;
      this.filterFunctionButton.StretchHorizontally = false;
      this.filterFunctionButton.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
      this.filterFunctionButton.Alignment = System.Drawing.ContentAlignment.MiddleRight;
      this.filterFunctionButton.Class = "HeaderFilterFunctionButton";
      this.filterFunctionButton.Image = (Image) Resources.FilteringIcon;
      this.filterFunctionButton.Click += new EventHandler(this.FilterFunctionButton_Click);
      this.filterFunctionButton.ToolTipText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterOperatorNoFilter");
      this.filterFunctionButton.GetValue(GridFilterCellElement.IsFilterAppliedProperty);
      this.stackLayout = new StackLayoutElement();
      this.stackLayout.Orientation = Orientation.Horizontal;
      this.stackLayout.Children.Add((RadElement) this.filterFunctionButton);
      this.Children.Add((RadElement) this.stackLayout);
      this.arrow = new ArrowPrimitive();
      this.arrow.Alignment = System.Drawing.ContentAlignment.MiddleRight;
      this.arrow.Visibility = ElementVisibility.Hidden;
      this.arrow.RadPropertyChanged += new RadPropertyChangedEventHandler(this.arrow_RadPropertyChanged);
      this.Children.Add((RadElement) this.arrow);
    }

    protected virtual IGridFilterPopup CreateFilterPopup()
    {
      BaseFilterPopup baseFilterPopup = !(this.ColumnInfo is GridViewDateTimeColumn) ? (BaseFilterPopup) new RadListFilterPopup((GridViewDataColumn) this.ColumnInfo) : (BaseFilterPopup) new RadDateFilterPopup((GridViewDataColumn) this.ColumnInfo);
      baseFilterPopup.SetTheme(this.GridControl.ThemeName);
      baseFilterPopup.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      baseFilterPopup.HorizontalPopupAlignment = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
      return (IGridFilterPopup) baseFilterPopup;
    }

    protected override void DisposeManagedResources()
    {
      this.arrow = (ArrowPrimitive) null;
      if (this.ContextMenu != null)
      {
        this.ContextMenu.Dispose();
        this.ContextMenu = (RadDropDownMenu) null;
      }
      if (this.conditionalFormattingForm != null)
      {
        this.conditionalFormattingForm.Dispose();
        this.conditionalFormattingForm = (ConditionalFormattingForm) null;
      }
      if (this.lastFilterPopup != null)
      {
        this.lastFilterPopup.Dispose();
        this.lastFilterPopup = (IGridFilterPopup) null;
      }
      base.DisposeManagedResources();
    }

    public override void Detach()
    {
      base.Detach();
      int num1 = (int) this.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty);
      this.sortOrder = new RadSortOrder?();
    }

    protected internal virtual BaseCompositeFilterDialog CreateCompositeFilterForm()
    {
      if (this.MasterTemplate != null)
      {
        GridViewCreateCompositeFilterDialogEventArgs args = new GridViewCreateCompositeFilterDialogEventArgs();
        args.Dialog = (BaseCompositeFilterDialog) new CompositeDataFilterForm();
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCreateCompositeFilterDialogEventArgs>(EventDispatcher.CreateCompositeFilterDialog, (object) this, args);
        if (args.Dialog != null)
          return args.Dialog;
      }
      return (BaseCompositeFilterDialog) new CompositeDataFilterForm();
    }

    public bool IsSortedAscending
    {
      get
      {
        return (bool) this.GetValue(GridHeaderCellElement.IsSortedAscendingProperty);
      }
    }

    public bool IsSortedDescending
    {
      get
      {
        return (bool) this.GetValue(GridHeaderCellElement.IsSortedDescendingProperty);
      }
    }

    public RadSortOrder SortOrder
    {
      get
      {
        return this.GetColumnSortOrder();
      }
    }

    public RadButtonElement FilterButton
    {
      get
      {
        return (RadButtonElement) this.filterFunctionButton;
      }
    }

    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    public override bool SupportsConditionalFormatting
    {
      get
      {
        return false;
      }
    }

    public override object Value
    {
      get
      {
        if (this.ColumnInfo != null)
          return (object) this.ColumnInfo.HeaderText;
        return (object) "";
      }
      set
      {
      }
    }

    internal override bool CanBestFit(BestFitColumnMode bestFitMode)
    {
      return (bestFitMode & BestFitColumnMode.HeaderCells) > (BestFitColumnMode) 0;
    }

    protected StackLayoutElement StackLayout
    {
      get
      {
        return this.stackLayout;
      }
    }

    protected GridHeaderCellElement.ArrowPositionEnum ArrowPosition
    {
      get
      {
        return this.arrowPosition;
      }
      set
      {
        this.arrowPosition = value;
      }
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      if (!this.ViewTemplate.AllowColumnReorder && !this.ViewTemplate.EnableGrouping)
        return false;
      ColumnGroupsViewDefinition viewDefinition = this.TableElement.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
      if (viewDefinition != null)
      {
        GridViewColumnGroup group = viewDefinition.FindGroup(this.ColumnInfo);
        if (group != null)
        {
          int num = 0;
          if (group.Rows.Count > 0)
          {
            foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
            {
              foreach (string columnName in (Collection<string>) row.ColumnNames)
              {
                if (this.ViewTemplate.Columns[columnName].IsVisible)
                {
                  ++num;
                  if (num > 1)
                    return true;
                }
              }
            }
          }
          return this.CanHideGroup(group);
        }
      }
      int num1 = 0;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
      {
        if (column.IsGrouped || !column.IsVisible)
          ++num1;
      }
      return num1 != this.ViewTemplate.ColumnCount - 1;
    }

    protected override object GetDragContextCore()
    {
      return (object) this.Data;
    }

    protected override Image GetDragHintCore()
    {
      return (Image) RadGridViewDragDropService.GetDragImageHint(this.TextAlignment, base.GetDragHintCore() as Bitmap, this.Layout.RightPart.Bounds, 100);
    }

    protected bool CanHideGroup(GridViewColumnGroup group)
    {
      if (this.TableElement == null)
        return true;
      ColumnGroupRowLayout rowLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      if (rowLayout != null && rowLayout.ShowEmptyGroups)
        return true;
      int num = 0;
      foreach (GridViewColumnGroup gridViewColumnGroup in group.Parent != null ? (Collection<GridViewColumnGroup>) group.Parent.Groups : (Collection<GridViewColumnGroup>) (this.TableElement.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition).ColumnGroups)
      {
        if (gridViewColumnGroup.IsVisible)
          ++num;
        if (num > 1)
          return true;
      }
      return false;
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GridViewColumn)
      {
        GridViewColumn column = dataContext as GridViewColumn;
        if ((column.OwnerTemplate == null || column.OwnerTemplate == this.Data.OwnerTemplate) && (column.AllowReorder && this.Data.OwnerTemplate.AllowColumnReorder))
        {
          ColumnGroupsViewDefinition viewDefinition = this.TableElement.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
          ColumnGroupRowLayout rowLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
          if (viewDefinition == null)
            return true;
          if (!this.CanReorder(viewDefinition, column) || !this.CanReorder(viewDefinition, this.ColumnInfo))
            return false;
          if (!rowLayout.ShowEmptyGroups && !(column is GridViewGroupColumn))
            return this.GetGroupChildrenCount(viewDefinition.FindGroup(column)) > 1;
          return true;
        }
      }
      else if (dataContext is GroupFieldDragDropContext)
      {
        GroupFieldDragDropContext fieldDragDropContext = dataContext as GroupFieldDragDropContext;
        if (fieldDragDropContext.ViewTemplate == this.ViewTemplate)
        {
          GridViewColumn column = (GridViewColumn) this.ViewTemplate.Columns[fieldDragDropContext.SortDescription.PropertyName];
          if (this.ViewTemplate.AllowColumnReorder && column.AllowReorder)
            return true;
          bool flag = RadGridViewDragDropService.IsDroppedAtLeft(currentMouseLocation, this.Size.Width);
          if (flag && this.ColumnIndex == column.Index + 1)
            return true;
          if (!flag)
            return column.Index == this.ColumnIndex + 1;
          return false;
        }
      }
      return false;
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      RadPosition dropPosition = RadGridViewDragDropService.GetDropPosition(dropLocation, this.Size);
      if (dataContext is GridViewDataColumn)
      {
        GridViewDataColumn draggedItem = dataContext as GridViewDataColumn;
        GridViewColumnCollection columns = this.Data.OwnerTemplate.Columns;
        if (!draggedItem.IsVisible)
          draggedItem.IsVisible = true;
        ColumnGroupsViewDefinition viewDefinition = this.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
        if (viewDefinition != null)
        {
          this.DropInColumnGroupsView(viewDefinition, (GridViewColumn) draggedItem, dropPosition);
        }
        else
        {
          draggedItem.PinPosition = this.ColumnInfo.PinPosition;
          if (!this.RightToLeft)
            RadGridViewDragDropService.MoveOnLeftOrRight<GridViewDataColumn>((dropPosition & RadPosition.Left) != RadPosition.None, (Collection<GridViewDataColumn>) columns, this.Data as GridViewDataColumn, draggedItem);
          else
            RadGridViewDragDropService.MoveOnLeftOrRight<GridViewDataColumn>((dropPosition & RadPosition.Right) != RadPosition.None, (Collection<GridViewDataColumn>) columns, this.Data as GridViewDataColumn, draggedItem);
        }
      }
      else
      {
        if (!(dataContext is GroupFieldDragDropContext))
          return;
        this.DragDropSortDescription(dataContext as GroupFieldDragDropContext, dropPosition);
      }
    }

    protected virtual void DropInColumnGroupsView(
      ColumnGroupsViewDefinition view,
      GridViewColumn column,
      RadPosition dropPosition)
    {
      column.IsPinned = false;
      GridViewColumnGroupRow row1 = this.FindRow(view.ColumnGroups, this.ColumnInfo);
      GridViewColumnGroupRow row2 = this.FindRow(view.ColumnGroups, column);
      GridViewDataColumn gridViewDataColumn = column as GridViewDataColumn;
      if (row1 != null && this.ViewTemplate.AllowColumnReorder)
      {
        int num = row1.ColumnNames.IndexOf(this.ColumnInfo.Name);
        if (row2 == row1 && row2.ColumnNames.IndexOf(gridViewDataColumn.Name) < num)
          --num;
        if (row2 != null)
        {
          row2.ColumnNames.Remove(gridViewDataColumn.Name);
          if (row2.ColumnNames.Count == 0)
            this.FindGroup(view.ColumnGroups, row2)?.Rows.Remove(row2);
        }
        if ((dropPosition & RadPosition.Bottom) != RadPosition.None)
        {
          GridViewColumnGroup group = this.FindGroup(view.ColumnGroups, row1);
          int index1 = group.Rows.IndexOf(row1) + 1;
          if (group.Rows.Count == index1)
            group.Rows.Add(new GridViewColumnGroupRow());
          int index2 = Math.Min(num, group.Rows[index1].ColumnNames.Count);
          group.Rows[index1].ColumnNames.Insert(index2, gridViewDataColumn.Name);
        }
        else if ((dropPosition & RadPosition.Left) != RadPosition.None)
          row1.ColumnNames.Insert(num, gridViewDataColumn.Name);
        else if (num < row1.ColumnNames.Count - 1)
          row1.ColumnNames.Insert(num + 1, gridViewDataColumn.Name);
        else
          row1.ColumnNames.Add(gridViewDataColumn.Name);
      }
      GridViewColumnGroup group1 = view.FindGroup(column);
      if (group1 != null && group1.IsPinned)
      {
        column.SuspendPropertyNotifications();
        column.PinPosition = group1.PinPosition;
        column.ResumePropertyNotifications();
      }
      ColumnGroupRowLayout rowLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      rowLayout.InvalidateLayout();
      rowLayout.InvalidateRenderColumns();
      this.TableElement.ViewElement.UpdateRows(true);
    }

    private void DragDropSortDescription(
      GroupFieldDragDropContext draggGroupFieldContext,
      RadPosition dropPosition)
    {
      SortDescriptor sortDescription = draggGroupFieldContext.SortDescription;
      GridViewTemplate viewTemplate = draggGroupFieldContext.ViewTemplate;
      GroupDescriptor groupDescription = draggGroupFieldContext.GroupDescription;
      if (this.RaiseGroupByChanging(viewTemplate, groupDescription))
        return;
      viewTemplate.GroupDescriptors.BeginUpdate();
      groupDescription.GroupNames.Remove(sortDescription);
      if (groupDescription.GroupNames.Count == 0)
        viewTemplate.GroupDescriptors.Remove(groupDescription);
      viewTemplate.GroupDescriptors.EndUpdate();
      GridViewDataColumn draggedItem = (GridViewDataColumn) null;
      foreach (GridViewDataColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
      {
        if (column.FieldName == sortDescription.PropertyName)
        {
          draggedItem = column;
          draggedItem.PinPosition = this.ColumnInfo.PinPosition;
          break;
        }
      }
      if (draggedItem == null)
        return;
      ColumnGroupsViewDefinition viewDefinition = this.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
      if (viewDefinition != null)
      {
        if (!this.ViewTemplate.AllowColumnReorder)
          return;
        this.DropInColumnGroupsView(viewDefinition, (GridViewColumn) draggedItem, dropPosition);
      }
      else
        RadGridViewDragDropService.MoveOnLeftOrRight<GridViewDataColumn>((dropPosition & RadPosition.Left) != RadPosition.None, (Collection<GridViewDataColumn>) this.ViewTemplate.Columns, this.Data as GridViewDataColumn, draggedItem);
    }

    private bool RaiseGroupByChanging(GridViewTemplate template, GroupDescriptor descriptor)
    {
      IList newItems = (IList) new List<GroupDescriptor>();
      newItems.Add((object) descriptor);
      GridViewCollectionChangingEventArgs args = new GridViewCollectionChangingEventArgs(template, NotifyCollectionChangedAction.Batch, newItems, (IList) template.GroupDescriptors, 0, 0, (PropertyChangingEventArgsEx) null);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.GroupByChanging, (object) template, args);
      return args.Cancel;
    }

    protected virtual GridViewColumnGroup FindGroup(
      ColumnGroupCollection groups,
      GridViewColumnGroupRow row)
    {
      foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) groups)
      {
        GridViewColumnGroup group2 = this.FindGroup(group1.Groups, row);
        if (group2 != null)
          return group2;
        foreach (GridViewColumnGroupRow row1 in (Collection<GridViewColumnGroupRow>) group1.Rows)
        {
          if (row1 == row)
            return group1;
        }
      }
      return (GridViewColumnGroup) null;
    }

    protected virtual GridViewColumnGroupRow FindRow(
      ColumnGroupCollection groups,
      GridViewColumn column)
    {
      foreach (GridViewColumnGroup group in (Collection<GridViewColumnGroup>) groups)
      {
        GridViewColumnGroupRow row1 = this.FindRow(group.Groups, column);
        if (row1 != null)
          return row1;
        foreach (GridViewColumnGroupRow row2 in (Collection<GridViewColumnGroupRow>) group.Rows)
        {
          foreach (string columnName in (Collection<string>) row2.ColumnNames)
          {
            if (column.Name == columnName)
              return row2;
          }
        }
      }
      return (GridViewColumnGroupRow) null;
    }

    private bool CanReorder(ColumnGroupsViewDefinition view, GridViewColumn column)
    {
      for (GridViewColumnGroup gridViewColumnGroup = view.FindGroup(column); gridViewColumnGroup != null; gridViewColumnGroup = gridViewColumnGroup.Parent)
      {
        if (!gridViewColumnGroup.AllowReorder)
          return false;
      }
      return true;
    }

    private int GetGroupChildrenCount(GridViewColumnGroup group)
    {
      int num = 0;
      foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
        num += row.ColumnNames.Count;
      return num;
    }

    public override RadDropDownMenu MergeMenus(
      IContextMenuManager contextMenuManager,
      params object[] parameters)
    {
      if (this.ViewTemplate.AllowColumnHeaderContextMenu)
        return base.MergeMenus(contextMenuManager, parameters);
      return (RadDropDownMenu) null;
    }

    protected internal override void ShowContextMenu()
    {
      if (this.ViewTemplate == null || !this.ViewTemplate.AllowColumnHeaderContextMenu)
        return;
      base.ShowContextMenu();
    }

    protected override void CreateContextMenuItems(RadDropDownMenu menu)
    {
      this.CreateSortMenuItems(menu);
      menu.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.CreateConditionalFormattingMenuItems(menu);
      menu.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.CreateGroupingMenuItems(menu);
      IGridRowLayout rowLayout = this.TableElement.ViewElement.RowLayout;
      if (!(rowLayout is HtmlViewRowLayout))
        this.CreateColumnChooserMenuItems(menu);
      this.CreateExpressionMenuItems(menu);
      if (rowLayout is TableViewRowLayout)
        this.CreateColumnPinningMenuItems(menu);
      this.CreateBestFitMenuItems(menu);
    }

    private void CreateSortMenuItems(RadDropDownMenu contextMenu)
    {
      GridViewColumn columnInfo = this.ColumnInfo;
      if (!columnInfo.OwnerTemplate.EnableSorting || !columnInfo.AllowSort)
        return;
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SortAscendingMenuItem"));
      radMenuItem1.Click += new EventHandler(this.MenuItemSortAscending_Click);
      RadMenuItem radMenuItem2 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SortDescendingMenuItem"));
      radMenuItem2.Click += new EventHandler(this.MenuItemSortDescending_Click);
      if (this.Arrow.Visibility == ElementVisibility.Visible)
      {
        radMenuItem1.IsChecked = this.Arrow.Direction == Telerik.WinControls.ArrowDirection.Up;
        radMenuItem2.IsChecked = this.Arrow.Direction == Telerik.WinControls.ArrowDirection.Down;
      }
      contextMenu.Items.Add((RadItem) radMenuItem1);
      contextMenu.Items.Add((RadItem) radMenuItem2);
      if (this.ColumnInfo.IsGrouped)
        return;
      RadMenuItem radMenuItem3 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ClearSortingMenuItem"));
      radMenuItem3.Click += new EventHandler(this.MenuItemSortClear_Click);
      radMenuItem3.Enabled = columnInfo.IsSorted;
      contextMenu.Items.Add((RadItem) radMenuItem3);
    }

    private void CreateConditionalFormattingMenuItems(RadDropDownMenu contextMenu)
    {
      RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ConditionalFormattingMenuItem"));
      radMenuItem.Click += new EventHandler(this.MenuItemConditionFormatting_Click);
      contextMenu.Items.Add((RadItem) radMenuItem);
    }

    private void CreateGroupingMenuItems(RadDropDownMenu contextMenu)
    {
      GridViewColumn columnInfo = this.ColumnInfo;
      bool flag = columnInfo.OwnerTemplate != null ? columnInfo.AllowGroup && columnInfo.OwnerTemplate.EnableGrouping : columnInfo.AllowGroup;
      if (flag)
      {
        ColumnGroupsViewDefinition viewDefinition = this.TableElement.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
        if (viewDefinition == null ? this.HasSingleDataColumn() : this.HasSingleDataColumn(viewDefinition.ColumnGroups))
          flag = this.ViewTemplate.ShowGroupedColumns;
      }
      if (columnInfo == null || !flag)
        return;
      RadMenuItem radMenuItem = new RadMenuItem(columnInfo.IsGrouped ? LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("UngroupThisColumn") : LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("GroupByThisColumnMenuItem"));
      radMenuItem.Click += new EventHandler(this.MenuItemGroupByColumn_Click);
      contextMenu.Items.Add((RadItem) radMenuItem);
    }

    protected virtual void CreateColumnChooserMenuItems(RadDropDownMenu contextMenu)
    {
      if (!this.ViewTemplate.AllowColumnChooser)
        return;
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ColumnChooserMenuItem"));
      radMenuItem1.Click += new EventHandler(this.MenuItemColumnChooser_Click);
      contextMenu.Items.Add((RadItem) radMenuItem1);
      bool flag = false;
      if (this.ColumnInfo.AllowHide)
      {
        ColumnGroupsViewDefinition viewDefinition = this.TableElement.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
        if (viewDefinition != null)
        {
          GridViewColumnGroup group = viewDefinition.FindGroup(this.ColumnInfo);
          flag = this.CanHideGroup(group) && !this.HasSingleDataColumn(group) || !this.HasSingleDataColumn(group);
        }
        else
          flag = !this.HasSingleDataColumn();
      }
      if (!flag)
        return;
      RadMenuItem radMenuItem2 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("HideMenuItem"));
      radMenuItem2.Click += new EventHandler(this.MenuItemHide_Click);
      contextMenu.Items.Add((RadItem) radMenuItem2);
    }

    private bool HasSingleDataColumn(ColumnGroupCollection groups)
    {
      int num = 0;
      List<GridViewColumnGroup> gridViewColumnGroupList = new List<GridViewColumnGroup>((IEnumerable<GridViewColumnGroup>) groups);
      while (gridViewColumnGroupList.Count > 0)
      {
        GridViewColumnGroup gridViewColumnGroup = gridViewColumnGroupList[0];
        gridViewColumnGroupList.RemoveAt(0);
        if (gridViewColumnGroup.IsVisible)
        {
          if (gridViewColumnGroup.Groups.Count > 0)
          {
            gridViewColumnGroupList.AddRange((IEnumerable<GridViewColumnGroup>) gridViewColumnGroup.Groups);
          }
          else
          {
            foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) gridViewColumnGroup.Rows)
            {
              foreach (string columnName in (Collection<string>) row.ColumnNames)
              {
                if (this.ViewTemplate.Columns[columnName].IsVisible && (!this.ViewTemplate.Columns[columnName].IsGrouped || this.ViewTemplate.Columns[columnName].OwnerTemplate.ShowGroupedColumns))
                {
                  ++num;
                  if (num > 1)
                    return false;
                }
              }
            }
          }
        }
      }
      return true;
    }

    private bool HasSingleDataColumn(GridViewColumnGroup group)
    {
      int num = 0;
      foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
      {
        foreach (string columnName in (Collection<string>) row.ColumnNames)
        {
          if (this.ViewTemplate.Columns[columnName].IsVisible)
          {
            ++num;
            if (num > 1)
              return false;
          }
        }
      }
      return true;
    }

    private bool HasSingleDataColumn()
    {
      int num = 0;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.TableElement.ViewElement.RowLayout.RenderColumns)
      {
        if (renderColumn is GridViewDataColumn)
        {
          ++num;
          if (num > 1)
            return false;
        }
      }
      return true;
    }

    protected virtual void CreateColumnPinningMenuItems(RadDropDownMenu contextMenu)
    {
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinMenuItem"));
      RadMenuItem radMenuItem2 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("UnpinMenuItem"));
      radMenuItem2.Click += new EventHandler(this.unpin_Click);
      radMenuItem2.IsChecked = this.PinPosition == PinnedColumnPosition.None;
      radMenuItem1.Items.Add((RadItem) radMenuItem2);
      RadMenuItem radMenuItem3 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtLeftMenuItem"));
      radMenuItem3.Click += new EventHandler(this.pinAtLeft_Click);
      radMenuItem3.IsChecked = this.PinPosition == PinnedColumnPosition.Left;
      radMenuItem1.Items.Add((RadItem) radMenuItem3);
      RadMenuItem radMenuItem4 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtRightMenuItem"));
      radMenuItem4.Click += new EventHandler(this.pinAtRight_Click);
      radMenuItem4.IsChecked = this.PinPosition == PinnedColumnPosition.Right;
      radMenuItem1.Items.Add((RadItem) radMenuItem4);
      contextMenu.Items.Add((RadItem) radMenuItem1);
    }

    protected virtual void CreateBestFitMenuItems(RadDropDownMenu contextMenu)
    {
      RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("BestFitMenuItem"));
      radMenuItem.Click += new EventHandler(this.MenuItemBestFit_Click);
      radMenuItem.Enabled = this.ViewTemplate.AllowAutoSizeColumns && this.ViewTemplate.AllowColumnResize;
      contextMenu.Items.Add((RadItem) radMenuItem);
    }

    private void CreateExpressionMenuItems(RadDropDownMenu contextMenu)
    {
      if (!this.ColumnInfo.EnableExpressionEditor || this.ColumnInfo.IsDataBound || (object) this.ColumnInfo.GetType() == (object) typeof (GridViewImageColumn))
        return;
      RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ExpressionMenuItem"));
      radMenuItem.Click += new EventHandler(this.MenuItemExpression_Click);
      contextMenu.Items.Add((RadItem) radMenuItem);
    }

    private void MenuItemSortAscending_Click(object sender, EventArgs e)
    {
      this.Sort(RadSortOrder.Ascending);
    }

    private void MenuItemSortDescending_Click(object sender, EventArgs e)
    {
      this.Sort(RadSortOrder.Descending);
    }

    public virtual void Sort(RadSortOrder sortOrder)
    {
      if (this.groupSortDescriptors != null && this.groupSortDescriptors.Count > 0)
      {
        ListSortDirection listSortDirection = sortOrder == RadSortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
        foreach (SortDescriptor groupSortDescriptor in this.groupSortDescriptors)
          groupSortDescriptor.Direction = listSortDirection;
      }
      else
        this.ColumnInfo.Sort(sortOrder, (Control.ModifierKeys & Keys.Shift) != Keys.None && this.ViewTemplate.AllowMultiColumnSorting);
    }

    private void MenuItemSortClear_Click(object sender, EventArgs e)
    {
      this.ColumnInfo.Sort(RadSortOrder.None, true);
    }

    private void MenuItemHide_Click(object sender, EventArgs e)
    {
      this.GridViewElement.ColumnChooser.Template = this.ColumnInfo.OwnerTemplate;
      this.ColumnInfo.IsVisible = false;
    }

    private void MenuItemGroupByColumn_Click(object sender, EventArgs e)
    {
      if (!this.ColumnInfo.IsGrouped)
      {
        this.ViewTemplate.GroupDescriptors.Add(this.ColumnInfo.Name, ListSortDirection.Ascending);
      }
      else
      {
        GridViewCollectionChangingEventArgs args = new GridViewCollectionChangingEventArgs(this.ViewTemplate, NotifyCollectionChangedAction.Batch, (object) null, -1, -1);
        this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.GroupByChanging, (object) this.ViewTemplate, args);
        if (args.Cancel)
          return;
        this.ViewTemplate.GroupDescriptors.BeginUpdate();
        this.ViewTemplate.GroupDescriptors.Remove(this.ColumnInfo.Name);
        this.ViewTemplate.GroupDescriptors.EndUpdate(true);
      }
    }

    private void MenuItemConditionFormatting_Click(object sender, EventArgs e)
    {
      if (!this.IsInValidState(true))
        return;
      this.conditionalFormattingForm = this.CreateConditionalFormattingForm();
      Form form = this.ElementTree.Control.FindForm();
      this.conditionalFormattingForm.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      if (form != null)
      {
        this.conditionalFormattingForm.Owner = form;
        this.conditionalFormattingForm.TopMost = form.TopMost;
      }
      this.ViewTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.ConditionalFormattingFormShown, (object) this.conditionalFormattingForm, new EventArgs());
      this.conditionalFormattingForm.Show();
    }

    private void MenuItemColumnChooser_Click(object sender, EventArgs e)
    {
      this.GridViewElement.ShowColumnChooser(this.ViewTemplate);
    }

    private void MenuItemBestFit_Click(object sender, EventArgs e)
    {
      this.ColumnInfo.BestFit();
    }

    private void pinAtRight_Click(object sender, EventArgs e)
    {
      this.PinColumn(PinnedColumnPosition.Right);
    }

    private void pinAtLeft_Click(object sender, EventArgs e)
    {
      this.PinColumn(PinnedColumnPosition.Left);
    }

    private void unpin_Click(object sender, EventArgs e)
    {
      this.PinColumn(PinnedColumnPosition.None);
    }

    private void MenuItemExpression_Click(object sender, EventArgs e)
    {
      ExpressionEditorFormCreatedEventArgs args = new ExpressionEditorFormCreatedEventArgs(new RadExpressionEditorForm(this.ColumnInfo as GridViewDataColumn));
      this.MasterTemplate.EventDispatcher.RaiseEvent<ExpressionEditorFormCreatedEventArgs>(EventDispatcher.ExpressionEditorFormCreated, (object) this, args);
      RadExpressionEditorForm.Show(this.GridControl, this.ColumnInfo as GridViewDataColumn, args.ExpressionEditorForm);
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      this.ForeColor = SystemSkinManager.Instance.GetThemeColor(ColorProperty.TextColor);
    }

    protected override void PaintElementSkin(IGraphics graphics)
    {
      base.PaintElementSkin(graphics);
      if (!this.IsSorted)
        return;
      RadSortOrder? sortOrder = this.sortOrder;
      if (!SystemSkinManager.Instance.SetCurrentElement((sortOrder.GetValueOrDefault() != RadSortOrder.Ascending ? 0 : (sortOrder.HasValue ? 1 : 0)) != 0 ? VisualStyleElement.Header.SortArrow.SortedUp : VisualStyleElement.Header.SortArrow.SortedDown))
        return;
      Graphics underlayGraphics = graphics.UnderlayGraphics as Graphics;
      Rectangle bounds = new Rectangle(this.arrow.BoundingRectangle.Location, SystemSkinManager.Instance.GetPartPreferredSize(underlayGraphics, this.Bounds, ThemeSizeType.True));
      SystemSkinManager.Instance.PaintCurrentElement(underlayGraphics, bounds);
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0)
        return element != this.arrow;
      return base.ShouldPaintChild(element);
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      return this.Enabled ? (!this.IsMouseDown ? (this.IsMouseOver ? VistaAeroTheme.Header.Item.Hot : VistaAeroTheme.Header.Item.Normal) : (this.IsMouseOver ? VistaAeroTheme.Header.Item.Pressed : VistaAeroTheme.Header.Item.Hot)) : VistaAeroTheme.Header.Item.Normal;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.Enabled ? (!this.IsSorted ? (!this.IsMouseDown ? (this.IsMouseOver ? VistaAeroTheme.Header.Item.Hot : VistaAeroTheme.Header.Item.Normal) : (this.IsMouseOver ? VistaAeroTheme.Header.Item.Pressed : VistaAeroTheme.Header.Item.Hot)) : (!this.IsMouseDown ? (this.IsMouseOver ? VistaAeroTheme.Header.Item.SortedHot : VistaAeroTheme.Header.Item.SortedNormal) : (this.IsMouseOver ? VistaAeroTheme.Header.Item.SortedPressed : VistaAeroTheme.Header.Item.SortedHot))) : VistaAeroTheme.Header.Item.Normal;
    }

    protected virtual ConditionalFormattingForm CreateConditionalFormattingForm()
    {
      return new ConditionalFormattingForm(this.GridControl, this.ViewTemplate, this.ColumnInfo as GridViewDataColumn, this.ElementTree.ThemeName);
    }

    protected virtual void PinColumn(PinnedColumnPosition position)
    {
      this.ColumnInfo.PinPosition = position;
    }

    private void UpdateFilterButtonState()
    {
      GridViewDataColumn columnInfo = this.ColumnInfo as GridViewDataColumn;
      if (columnInfo == null)
        return;
      int num = (int) this.SetValue(GridFilterCellElement.IsFilterAppliedProperty, (object) (columnInfo.FilterDescriptor != null));
      this.filterFunctionButton.ToolTipText = this.CreateFilterStringDescription();
    }

    private string CreateFilterStringDescription()
    {
      GridViewDataColumn columnInfo = this.ColumnInfo as GridViewDataColumn;
      if (columnInfo == null)
        return string.Empty;
      return GridFilterStringProvider.GetFilterString(columnInfo.FilterDescriptor, this.ColumnInfo as GridViewComboBoxColumn, 150);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size1 = this.TableElement.ViewElement.RowLayout.ArrangeCell(new RectangleF((PointF) Point.Empty, availableSize), (GridCellElement) this).Size;
      SizeF empty = SizeF.Empty;
      SizeF size2 = this.GetClientRectangle(availableSize).Size;
      Padding borderThickness = this.GetBorderThickness(false);
      if (this.arrow != null && this.arrow.Visibility == ElementVisibility.Visible)
      {
        this.arrow.Measure(size2);
        empty.Width += (float) (2.0 * (double) this.arrow.DesiredSize.Width + 2.0);
        empty.Height = this.arrow.DesiredSize.Height;
        if (this.arrow.Alignment != System.Drawing.ContentAlignment.TopCenter)
          size2.Width -= empty.Width;
      }
      if (this.stackLayout != null && this.stackLayout.Visibility == ElementVisibility.Visible && this.filterFunctionButton.Visibility == ElementVisibility.Visible)
      {
        this.stackLayout.Measure(size2);
        empty.Width += this.stackLayout.DesiredSize.Width;
        empty.Height = Math.Max(empty.Height, this.stackLayout.DesiredSize.Height);
        size2.Width -= this.stackLayout.DesiredSize.Width;
      }
      this.Layout.Measure(size2);
      empty.Width += this.Layout.DesiredSize.Width;
      empty.Height = Math.Max(empty.Height, this.Layout.DesiredSize.Height);
      empty.Width += (float) (this.Padding.Horizontal + borderThickness.Horizontal + 2);
      empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      if (!float.IsInfinity(availableSize.Width))
        empty.Width = size1.Width;
      if (!float.IsInfinity(availableSize.Height))
        empty.Height = size1.Height;
      return empty;
    }

    private bool IsTextRightAligned()
    {
      return (this.TextAlignment == System.Drawing.ContentAlignment.MiddleRight || this.TextAlignment == System.Drawing.ContentAlignment.TopRight || this.TextAlignment == System.Drawing.ContentAlignment.BottomRight) && !this.RightToLeft || (this.TextAlignment == System.Drawing.ContentAlignment.MiddleLeft || this.TextAlignment == System.Drawing.ContentAlignment.TopLeft || this.TextAlignment == System.Drawing.ContentAlignment.BottomLeft) && this.RightToLeft;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.arrowPosition = this.SetArrowPosition();
      RectangleF clientRectangle1 = this.GetClientRectangle(finalSize);
      double width = (double) clientRectangle1.Width;
      if (this.stackLayout.Visibility == ElementVisibility.Visible && this.filterFunctionButton.Visibility == ElementVisibility.Visible)
      {
        float num1 = finalSize.Width - this.stackLayout.DesiredSize.Width - this.Layout.DesiredSize.Width;
        if ((double) num1 < 0.0)
          num1 = 0.0f;
        float num2 = (double) num1 <= (double) this.stackLayout.DesiredSize.Width || this.IsTextRightAligned() ? 0.0f : this.stackLayout.DesiredSize.Width;
        clientRectangle1.Width -= this.stackLayout.DesiredSize.Width - num2;
        if (this.RightToLeft)
          clientRectangle1.X += this.stackLayout.DesiredSize.Width - num2;
      }
      if (this.arrowPosition != GridHeaderCellElement.ArrowPositionEnum.Top && this.arrowPosition != GridHeaderCellElement.ArrowPositionEnum.Bottom)
      {
        clientRectangle1.Width -= (float) (2.0 * (double) this.arrow.DesiredSize.Width + 2.0);
        if (this.RightToLeft)
          clientRectangle1.X += (float) (2.0 * (double) this.arrow.DesiredSize.Width + 2.0);
      }
      this.Layout.Arrange(clientRectangle1);
      foreach (RadElement child in this.Children)
      {
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          if (child == this.arrow)
          {
            if (!this.ViewTemplate.EnableSorting)
              child.Visibility = ElementVisibility.Hidden;
            else if (2.0 * (double) this.arrow.DesiredSize.Width < (double) finalSize.Width)
            {
              if (this.sortOrder.HasValue)
              {
                RadSortOrder? sortOrder = this.sortOrder;
                if ((sortOrder.GetValueOrDefault() != RadSortOrder.None ? 1 : (!sortOrder.HasValue ? 1 : 0)) != 0 && this.ColumnInfo.SortOrder != RadSortOrder.None)
                  child.Visibility = ElementVisibility.Visible;
              }
              this.ArrangeArrow(finalSize, child);
            }
            else
              child.Visibility = ElementVisibility.Hidden;
          }
          else if (child == this.stackLayout)
          {
            RectangleF clientRectangle2 = this.GetClientRectangle(finalSize);
            if (this.RightToLeft)
              this.stackLayout.Arrange(new RectangleF(clientRectangle2.X, clientRectangle2.Y + (float) (((double) clientRectangle2.Height - (double) this.stackLayout.DesiredSize.Height) / 2.0), this.stackLayout.DesiredSize.Width, this.stackLayout.DesiredSize.Height));
            else
              this.stackLayout.Arrange(new RectangleF(clientRectangle2.Right - this.stackLayout.DesiredSize.Width, clientRectangle2.Y + (float) (((double) clientRectangle2.Height - (double) this.stackLayout.DesiredSize.Height) / 2.0), this.stackLayout.DesiredSize.Width, this.stackLayout.DesiredSize.Height));
          }
          else
            this.ArrangeElement(child, finalSize, clientRectangle1);
        }
      }
      return finalSize;
    }

    protected void ArrangeArrow(SizeF finalSize, RadElement element)
    {
      RectangleF finalRect = new RectangleF(PointF.Empty, this.arrow.DesiredSize);
      float num = 0.0f;
      if (this.FilterButton != null && this.FilterButton.Visibility != ElementVisibility.Collapsed)
        num = this.FilterButton.DesiredSize.Width + (float) this.FilterButton.Margin.Horizontal;
      switch (this.arrowPosition)
      {
        case GridHeaderCellElement.ArrowPositionEnum.Left:
          if (!this.RightToLeft)
          {
            finalRect.X = this.arrow.DesiredSize.Width + num;
            finalRect.Y = (float) (((double) finalSize.Height - (double) this.arrow.DesiredSize.Height) / 2.0);
            break;
          }
          finalRect.X = finalSize.Width - 2f * this.arrow.DesiredSize.Width - num;
          finalRect.Y = (float) (((double) finalSize.Height - (double) this.arrow.DesiredSize.Height) / 2.0);
          break;
        case GridHeaderCellElement.ArrowPositionEnum.Right:
          if (!this.RightToLeft)
          {
            finalRect.X = finalSize.Width - 2f * this.arrow.DesiredSize.Width - num;
            finalRect.Y = (float) (((double) finalSize.Height - (double) this.arrow.DesiredSize.Height) / 2.0);
            break;
          }
          finalRect.X = this.arrow.DesiredSize.Width + num;
          finalRect.Y = (float) (((double) finalSize.Height - (double) this.arrow.DesiredSize.Height) / 2.0);
          break;
        case GridHeaderCellElement.ArrowPositionEnum.Top:
          finalRect.X = (float) ((double) finalSize.Width / 2.0 - (double) this.arrow.DesiredSize.Width / 2.0);
          finalRect.Y = 0.0f;
          break;
        case GridHeaderCellElement.ArrowPositionEnum.Bottom:
          finalRect.X = (float) ((double) finalSize.Width / 2.0 - (double) this.arrow.DesiredSize.Width / 2.0);
          finalRect.Y = finalSize.Height - this.arrow.DesiredSize.Height;
          break;
      }
      element.Arrange(finalRect);
    }

    public void UpdateArrowState()
    {
      if (!this.IsInValidState(true) || !this.ViewTemplate.EnableSorting && !this.ColumnInfo.IsGrouped)
        return;
      RadSortOrder columnSortOrder = this.GetColumnSortOrder();
      if (columnSortOrder == RadSortOrder.None)
      {
        this.sortOrder = new RadSortOrder?(RadSortOrder.None);
        Padding padding = this.Padding;
        int num1 = (int) this.SetValue(GridHeaderCellElement.IsSortedAscendingProperty, (object) false);
        int num2 = (int) this.SetValue(GridHeaderCellElement.IsSortedDescendingProperty, (object) false);
        if (this.Arrow.Visibility == ElementVisibility.Hidden)
          return;
        this.Arrow.Visibility = ElementVisibility.Hidden;
        this.DecreasePadding(padding);
      }
      else
      {
        RadSortOrder radSortOrder1 = columnSortOrder;
        RadSortOrder? sortOrder1 = this.sortOrder;
        RadSortOrder radSortOrder2 = radSortOrder1;
        if ((sortOrder1.GetValueOrDefault() != radSortOrder2 ? 1 : (!sortOrder1.HasValue ? 1 : 0)) != 0)
        {
          this.sortOrder = new RadSortOrder?(radSortOrder1);
          Padding padding = this.Padding;
          RadSortOrder? sortOrder2 = this.sortOrder;
          ref RadSortOrder? local = ref sortOrder2;
          RadSortOrder valueOrDefault = local.GetValueOrDefault();
          if (local.HasValue)
          {
            switch (valueOrDefault)
            {
              case RadSortOrder.Ascending:
                this.Arrow.Direction = Telerik.WinControls.ArrowDirection.Up;
                if (this.Arrow.Visibility != ElementVisibility.Visible)
                {
                  this.Arrow.Visibility = ElementVisibility.Visible;
                  this.Arrow.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
                  break;
                }
                break;
              case RadSortOrder.Descending:
                this.Arrow.Direction = Telerik.WinControls.ArrowDirection.Down;
                if (this.Arrow.Visibility != ElementVisibility.Visible)
                {
                  this.Arrow.Visibility = ElementVisibility.Visible;
                  this.IncreasePadding(padding);
                  break;
                }
                break;
              case RadSortOrder.None:
                if (this.Arrow.Visibility != ElementVisibility.Hidden)
                {
                  this.Arrow.Visibility = ElementVisibility.Hidden;
                  this.DecreasePadding(padding);
                  break;
                }
                break;
            }
          }
        }
        int num1 = (int) this.SetValue(GridHeaderCellElement.IsSortedAscendingProperty, (object) (columnSortOrder == RadSortOrder.Ascending));
        int num2 = (int) this.SetValue(GridHeaderCellElement.IsSortedDescendingProperty, (object) (columnSortOrder == RadSortOrder.Descending));
      }
    }

    protected RadSortOrder GetColumnSortOrder()
    {
      this.groupSortDescriptors = this.ViewTemplate.GroupDescriptors.FindAllAssociatedSortDescriptors(this.ColumnInfo.Name, this.ViewTemplate.CaseSensitive);
      if (this.groupSortDescriptors == null || this.groupSortDescriptors.Count <= 0)
        return this.ColumnInfo.SortOrder;
      return this.groupSortDescriptors[0].Direction == ListSortDirection.Ascending ? RadSortOrder.Ascending : RadSortOrder.Descending;
    }

    protected virtual void UpdateFilterButtonVisibility()
    {
      GridViewDataColumn columnInfo = this.ColumnInfo as GridViewDataColumn;
      if (columnInfo == null)
        return;
      bool flag = columnInfo.AllowFiltering && this.ViewTemplate.EnableFiltering;
      if (this.ColumnInfo is GridViewImageColumn)
        flag = false;
      this.filterFunctionButton.Visibility = flag ? ElementVisibility.Visible : ElementVisibility.Hidden;
    }

    protected virtual void UpdateButtonsLayoutVisibility()
    {
      if (this.ViewTemplate.ShowHeaderCellButtons)
        this.stackLayout.Visibility = ElementVisibility.Visible;
      else
        this.stackLayout.Visibility = ElementVisibility.Collapsed;
    }

    private void IncreasePadding(Padding padding)
    {
      this.InvalidateMeasure();
    }

    private void DecreasePadding(Padding padding)
    {
      this.InvalidateMeasure();
    }

    protected GridHeaderCellElement.ArrowPositionEnum SetArrowPosition()
    {
      GridHeaderCellElement.ArrowPositionEnum arrowPositionEnum;
      switch (this.arrow.Alignment)
      {
        case System.Drawing.ContentAlignment.TopLeft:
        case System.Drawing.ContentAlignment.TopCenter:
        case System.Drawing.ContentAlignment.TopRight:
          arrowPositionEnum = GridHeaderCellElement.ArrowPositionEnum.Top;
          break;
        case System.Drawing.ContentAlignment.MiddleLeft:
          arrowPositionEnum = GridHeaderCellElement.ArrowPositionEnum.Left;
          break;
        case System.Drawing.ContentAlignment.MiddleCenter:
        case System.Drawing.ContentAlignment.MiddleRight:
          arrowPositionEnum = GridHeaderCellElement.ArrowPositionEnum.Right;
          break;
        case System.Drawing.ContentAlignment.BottomLeft:
        case System.Drawing.ContentAlignment.BottomCenter:
        case System.Drawing.ContentAlignment.BottomRight:
          arrowPositionEnum = GridHeaderCellElement.ArrowPositionEnum.Bottom;
          break;
        default:
          arrowPositionEnum = GridHeaderCellElement.ArrowPositionEnum.Right;
          break;
      }
      return arrowPositionEnum;
    }

    private void arrow_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.AlignmentProperty || !this.IsInValidState(true))
        return;
      this.UpdateInfo();
      this.InvalidateMeasure();
      this.Arrow.InvalidateMeasure();
      this.Parent.InvalidateArrange();
    }

    private void FilterFunctionButton_Click(object sender, EventArgs e)
    {
      if (this.lastFilterPopup != null)
      {
        this.lastFilterPopup.Dispose();
        this.lastFilterPopup = (IGridFilterPopup) null;
      }
      IGridFilterPopup filterPopup = this.CreateFilterPopup();
      FilterPopupRequiredEventArgs args1 = new FilterPopupRequiredEventArgs((GridViewDataColumn) this.ColumnInfo, filterPopup);
      this.GridViewElement.Template.EventDispatcher.RaiseEvent<FilterPopupRequiredEventArgs>(EventDispatcher.FilterPopupRequired, (object) this, args1);
      if (filterPopup != args1.FilterPopup)
        filterPopup = args1.FilterPopup;
      else
        this.lastFilterPopup = filterPopup;
      this.TableElement.GridFilterPopup = filterPopup as IGridFilterPopupInteraction;
      filterPopup.PopupOpening += new RadPopupOpeningEventHandler(this.filterPopup_PopupOpening);
      filterPopup.PopupClosed += new RadPopupClosedEventHandler(this.filterPopup_PopupClosed);
      filterPopup.FilterConfirmed += new EventHandler(this.filterPopup_FilterConfirmed);
      Point screen = this.filterFunctionButton.PointToScreen(this.RightToLeft ? new Point(this.filterFunctionButton.Size.Width, this.filterFunctionButton.Size.Height) : new Point(0, this.filterFunctionButton.Size.Height));
      BaseFilterPopup baseFilterPopup = filterPopup as BaseFilterPopup;
      if (baseFilterPopup != null)
      {
        baseFilterPopup.SetTheme(this.GridControl.ThemeName);
        baseFilterPopup.Scale(this.DpiScaleFactor);
      }
      FilterPopupInitializedEventArgs args2 = new FilterPopupInitializedEventArgs((GridViewDataColumn) this.ColumnInfo, filterPopup);
      this.GridViewElement.Template.EventDispatcher.RaiseEvent<FilterPopupInitializedEventArgs>(EventDispatcher.FilterPopupInitialized, (object) this, args2);
      filterPopup.Show(screen);
    }

    protected virtual void filterPopup_FilterConfirmed(object sender, EventArgs e)
    {
      IGridFilterPopup gridFilterPopup = (IGridFilterPopup) sender;
      gridFilterPopup.FilterConfirmed -= new EventHandler(this.filterPopup_FilterConfirmed);
      GridViewDataColumn columnInfo = (GridViewDataColumn) this.ColumnInfo;
      if (this.RowInfo == null && this.grid != null)
      {
        GridViewDataColumn dataColumn = ((BaseFilterPopup) gridFilterPopup).DataColumn;
        if (this.grid.Columns[dataColumn.Name] != null)
          this.grid.Columns[dataColumn.Name].SetFilterDescriptor(gridFilterPopup.FilterDescriptor);
      }
      else if (this.RowInfo != null)
      {
        this.RowInfo.Cache[(GridViewColumn) columnInfo] = (object) gridFilterPopup.FilterDescriptor;
        columnInfo.SetFilterDescriptor(gridFilterPopup.FilterDescriptor);
      }
      this.grid = (RadGridView) null;
      this.Focus();
    }

    private void filterPopup_PopupOpening(object sender, CancelEventArgs args)
    {
      this.grid = this.GridControl;
      ((IGridFilterPopup) sender).PopupOpening -= new RadPopupOpeningEventHandler(this.filterPopup_PopupOpening);
      int num = (int) this.filterFunctionButton.SetValue(GridFilterButtonElement.IsFilterMenuShownProperty, (object) !args.Cancel);
    }

    private void filterPopup_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      ((IGridFilterPopup) sender).PopupClosed -= new RadPopupClosedEventHandler(this.filterPopup_PopupClosed);
      int num = (int) this.filterFunctionButton.SetValue(GridFilterButtonElement.IsFilterMenuShownProperty, (object) false);
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      if (e.Property == GridViewDataColumn.AllowFilteringProperty)
      {
        this.UpdateFilterButtonVisibility();
      }
      else
      {
        if (e.Property != GridViewColumn.HeaderTextAlignmentProperty)
          return;
        this.TextAlignment = this.ColumnInfo.HeaderTextAlignment;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
    }

    protected override void OnZoomGesture(ZoomGestureEventArgs args)
    {
      base.OnZoomGesture(args);
      if (args.IsBegin)
        this.zoomedColumnWidth = (double) this.ColumnInfo.Width;
      this.zoomedColumnWidth *= args.ZoomFactor;
      this.ColumnInfo.Width = (int) Math.Round(this.zoomedColumnWidth / (double) this.DpiScaleFactor.Width);
      args.Handled = true;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      RadDragDropService service = this.GridViewElement.GetService<RadDragDropService>();
      if (service.State != RadServiceState.Working && (args.Location.X - this.ControlBoundingRectangle.X < 3 || this.ControlBoundingRectangle.Right - args.Location.X < 3))
        return;
      if (args.IsBegin)
      {
        int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) true);
        service.BeginDrag(this.ElementTree.Control.PointToScreen(args.Location), (ISupportDrag) this);
      }
      if (service.State == RadServiceState.Working)
        service.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
      if (args.IsEnd)
      {
        service.EndDrag();
        int num1 = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
        int num2 = (int) this.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      }
      args.Handled = true;
    }

    protected override void BindColumnProperties()
    {
      base.BindColumnProperties();
      int num1 = (int) this.BindProperty(LightVisualElement.ImageProperty, (RadObject) this.ColumnInfo, GridViewColumn.HeaderImageProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.BindProperty(LightVisualElement.ImageLayoutProperty, (RadObject) this.ColumnInfo, GridViewColumn.ImageLayoutProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.BindProperty(LightVisualElement.TextImageRelationProperty, (RadObject) this.ColumnInfo, GridViewColumn.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      int num4 = (int) this.BindProperty(RadItem.TextProperty, (RadObject) this.ColumnInfo, GridViewColumn.HeaderTextProperty, PropertyBindingOptions.OneWay);
    }

    protected override void UnbindColumnProperties()
    {
      int num1 = (int) this.UnbindProperty(LightVisualElement.ImageProperty);
      int num2 = (int) this.UnbindProperty(LightVisualElement.ImageLayoutProperty);
      int num3 = (int) this.UnbindProperty(LightVisualElement.TextImageRelationProperty);
      int num4 = (int) this.UnbindProperty(RadItem.TextProperty);
      base.UnbindColumnProperties();
    }

    protected override void UpdateInfoCore()
    {
      this.UpdateArrowState();
      this.UpdateButtonsLayoutVisibility();
      this.UpdateFilterButtonVisibility();
      base.UpdateInfoCore();
    }

    public override void SetContent()
    {
      this.UpdateFilterButtonState();
      base.SetContent();
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewDataColumn)
        return context is GridTableHeaderRowElement;
      return false;
    }

    protected enum ArrowPositionEnum
    {
      Left = 1,
      Right = 2,
      Top = 3,
      Bottom = 4,
    }
  }
}
