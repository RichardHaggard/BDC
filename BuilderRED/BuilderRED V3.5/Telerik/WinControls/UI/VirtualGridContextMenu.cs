// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridContextMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class VirtualGridContextMenu : RadDropDownMenu
  {
    private RadMenuItem copyItem;
    private RadMenuItem cutItem;
    private RadMenuItem pasteItem;
    private RadMenuItem editItem;
    private RadMenuItem clearValueItem;
    private RadMenuItem deleteRowItem;
    private RadMenuItem pinnedStatesItem;
    private RadMenuItem unpinRowItem;
    private RadMenuItem unpinColumnItem;
    private RadMenuItem pinAtTopItem;
    private RadMenuItem pinAtBottomItem;
    private RadMenuItem pinAtLeftItem;
    private RadMenuItem pinAtRightItem;
    private RadMenuItem sortAscendingItem;
    private RadMenuItem sortDescendingItem;
    private RadMenuItem clearSortItem;
    private RadMenuItem bestFitItem;
    private RadVirtualGridElement gridElement;
    private VirtualGridCellInfo currentContext;

    public RadVirtualGridElement GridElement
    {
      get
      {
        return this.gridElement;
      }
    }

    public VirtualGridContextMenu(RadVirtualGridElement gridElement)
    {
      this.gridElement = gridElement;
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadDropDownMenu).Name;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    protected override void InitializeChildren()
    {
      base.InitializeChildren();
      this.copyItem = new RadMenuItem();
      this.copyItem.Click += new EventHandler(this.OnCopyItemClick);
      this.cutItem = new RadMenuItem();
      this.cutItem.Click += new EventHandler(this.OnCutItemClick);
      this.pasteItem = new RadMenuItem();
      this.pasteItem.Click += new EventHandler(this.OnPasteItemClick);
      this.editItem = new RadMenuItem();
      this.editItem.Click += new EventHandler(this.OnEditItemClick);
      this.clearValueItem = new RadMenuItem();
      this.clearValueItem.Click += new EventHandler(this.OnClearValueItemClick);
      this.deleteRowItem = new RadMenuItem();
      this.deleteRowItem.Click += new EventHandler(this.OnDeleteRowItemClick);
      this.pinnedStatesItem = new RadMenuItem();
      this.unpinRowItem = new RadMenuItem();
      this.unpinRowItem.Click += new EventHandler(this.OnUnpinRowItemClick);
      this.unpinColumnItem = new RadMenuItem();
      this.unpinColumnItem.Click += new EventHandler(this.OnUnpinColumnItemClick);
      this.pinAtTopItem = new RadMenuItem();
      this.pinAtTopItem.Click += new EventHandler(this.OnPinAtTopItemClick);
      this.pinAtBottomItem = new RadMenuItem();
      this.pinAtBottomItem.Click += new EventHandler(this.OnPinAtBottomItemClick);
      this.pinAtLeftItem = new RadMenuItem();
      this.pinAtLeftItem.Click += new EventHandler(this.OnPinAtLeftItemClick);
      this.pinAtRightItem = new RadMenuItem();
      this.pinAtRightItem.Click += new EventHandler(this.OnPinAtRightItemClick);
      this.sortAscendingItem = new RadMenuItem();
      this.sortAscendingItem.Click += new EventHandler(this.OnSortAscendingItemClick);
      this.sortDescendingItem = new RadMenuItem();
      this.sortDescendingItem.Click += new EventHandler(this.OnSortDescendingItemClick);
      this.clearSortItem = new RadMenuItem();
      this.clearSortItem.Click += new EventHandler(this.OnClearSortItemClick);
      this.bestFitItem = new RadMenuItem();
      this.bestFitItem.Click += new EventHandler(this.OnBestFitItemClick);
      this.InitializeMenuItemsText();
    }

    public void InitializeMenuItemsText()
    {
      this.copyItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CopyMenuItem");
      this.cutItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CutMenuItem");
      this.pasteItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PasteMenuItem");
      this.editItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("EditMenuItem");
      this.clearValueItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ClearValueMenuItem");
      this.deleteRowItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("DeleteRowMenuItem");
      this.pinnedStatesItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinMenuItem");
      this.unpinRowItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("UnpinRowMenuItem");
      this.unpinColumnItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("UnpinColumnMenuItem");
      this.pinAtTopItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtTopMenuItem");
      this.pinAtBottomItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtBottomMenuItem");
      this.pinAtLeftItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtLeftMenuItem");
      this.pinAtRightItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtRightMenuItem");
      this.sortAscendingItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SortAscendingMenuItem");
      this.sortDescendingItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("SortDescendingMenuItem");
      this.clearSortItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ClearSortingMenuItem");
      this.bestFitItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("BestFitMenuItem");
    }

    public virtual void InitializeMenuItems(VirtualGridCellElement cell)
    {
      this.currentContext = new VirtualGridCellInfo(cell.RowIndex, cell.ColumnIndex, cell.ViewInfo);
      if (cell is VirtualGridHeaderCellElement)
        this.InitializeHeaderCellContextMenu((VirtualGridHeaderCellElement) cell);
      else if (cell is VirtualGridNewCellElement)
        this.InitializeNewRowContextMenu();
      else if (cell is VirtualGridFilterCellElement)
        this.InitializeFilterCellContextMenu();
      else if (cell is VirtualGridIndentCellElement)
      {
        if (cell.RowIndex < 0)
          return;
        this.InitializeRowContextMenu();
      }
      else
      {
        if (cell == null)
          return;
        this.InitializeDataCellContextMenu();
      }
    }

    protected virtual void InitializeRowContextMenu()
    {
      this.Items.Clear();
      this.pinnedStatesItem.Items.Clear();
      this.unpinRowItem.IsChecked = this.pinAtTopItem.IsChecked = this.pinAtBottomItem.IsChecked = false;
      switch (this.currentContext.ViewInfo.RowsViewState.GetPinPosition(this.currentContext.RowIndex))
      {
        case PinnedRowPosition.Top:
          this.pinAtTopItem.IsChecked = true;
          break;
        case PinnedRowPosition.Bottom:
          this.pinAtBottomItem.IsChecked = true;
          break;
        case PinnedRowPosition.None:
          this.unpinRowItem.IsChecked = true;
          break;
      }
      this.pinnedStatesItem.Items.Add((RadItem) this.unpinRowItem);
      this.pinnedStatesItem.Items.Add((RadItem) this.pinAtTopItem);
      this.pinnedStatesItem.Items.Add((RadItem) this.pinAtBottomItem);
      this.Items.Add((RadItem) this.pinnedStatesItem);
      this.Items.Add((RadItem) new RadMenuSeparatorItem());
      if (this.currentContext.ViewInfo.AllowCut)
        this.Items.Add((RadItem) this.cutItem);
      if (this.currentContext.ViewInfo.AllowCopy)
        this.Items.Add((RadItem) this.copyItem);
      if (this.currentContext.ViewInfo.AllowPaste)
        this.Items.Add((RadItem) this.pasteItem);
      if (!this.currentContext.ViewInfo.AllowDelete)
        return;
      this.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.Items.Add((RadItem) this.deleteRowItem);
    }

    protected virtual void InitializeFilterCellContextMenu()
    {
      this.Items.Clear();
      this.Items.Add((RadItem) this.copyItem);
      this.Items.Add((RadItem) this.pasteItem);
      this.Items.Add((RadItem) this.editItem);
    }

    protected virtual void InitializeNewRowContextMenu()
    {
      this.Items.Clear();
      this.Items.Add((RadItem) this.copyItem);
      this.Items.Add((RadItem) this.pasteItem);
      this.Items.Add((RadItem) this.editItem);
    }

    protected virtual void InitializeHeaderCellContextMenu(VirtualGridHeaderCellElement cell)
    {
      this.Items.Clear();
      this.sortAscendingItem.IsChecked = false;
      this.sortDescendingItem.IsChecked = false;
      this.clearSortItem.Enabled = true;
      switch (cell.SortOrder)
      {
        case RadSortOrder.Ascending:
          this.sortAscendingItem.IsChecked = true;
          break;
        case RadSortOrder.Descending:
          this.sortDescendingItem.IsChecked = true;
          break;
        case RadSortOrder.None:
          this.clearSortItem.Enabled = false;
          break;
      }
      this.unpinColumnItem.IsChecked = this.pinAtLeftItem.IsChecked = this.pinAtRightItem.IsChecked = false;
      switch (this.currentContext.ViewInfo.ColumnsViewState.GetPinPosition(this.currentContext.ColumnIndex))
      {
        case PinnedRowPosition.Top:
          this.pinAtLeftItem.IsChecked = true;
          break;
        case PinnedRowPosition.Bottom:
          this.pinAtRightItem.IsChecked = true;
          break;
        case PinnedRowPosition.None:
          this.unpinColumnItem.IsChecked = true;
          break;
      }
      if (this.GridElement.AllowSorting)
      {
        this.Items.Add((RadItem) this.sortAscendingItem);
        this.Items.Add((RadItem) this.sortDescendingItem);
      }
      if (this.GridElement.AllowSorting || !this.GridElement.AllowSorting && cell.SortOrder != RadSortOrder.None)
      {
        this.Items.Add((RadItem) this.clearSortItem);
        this.Items.Add((RadItem) new RadMenuSeparatorItem());
      }
      this.pinnedStatesItem.Items.Clear();
      this.pinnedStatesItem.Items.Add((RadItem) this.unpinColumnItem);
      this.pinnedStatesItem.Items.Add((RadItem) this.pinAtLeftItem);
      this.pinnedStatesItem.Items.Add((RadItem) this.pinAtRightItem);
      this.Items.Add((RadItem) this.pinnedStatesItem);
      this.bestFitItem.Enabled = cell.ViewInfo.AllowColumnResize;
      this.Items.Add((RadItem) this.bestFitItem);
    }

    protected virtual void InitializeDataCellContextMenu()
    {
      this.Items.Clear();
      if (this.GridElement.AllowCopy)
        this.Items.Add((RadItem) this.copyItem);
      if (this.GridElement.AllowPaste)
        this.Items.Add((RadItem) this.pasteItem);
      if (this.Items.Count > 0)
        this.Items.Add((RadItem) new RadMenuSeparatorItem());
      if (this.GridElement.AllowEdit)
      {
        this.Items.Add((RadItem) this.editItem);
        this.Items.Add((RadItem) this.clearValueItem);
        this.Items.Add((RadItem) new RadMenuSeparatorItem());
      }
      if (this.GridElement.AllowDelete)
        this.Items.Add((RadItem) this.deleteRowItem);
      if (!(this.Items[this.Items.Count - 1] is RadMenuSeparatorItem))
        return;
      this.Items.RemoveAt(this.Items.Count - 1);
    }

    protected virtual void OnClearSortItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SortDescriptors.Remove(this.GridElement.FindCellElement(this.currentContext.RowIndex, this.currentContext.ColumnIndex, this.currentContext.ViewInfo).FieldName);
    }

    protected virtual void OnSortDescendingItemClick(object sender, EventArgs e)
    {
      VirtualGridCellElement cellElement = this.GridElement.FindCellElement(this.currentContext.RowIndex, this.currentContext.ColumnIndex, this.currentContext.ViewInfo);
      this.currentContext.ViewInfo.SortDescriptors.BeginUpdate();
      this.currentContext.ViewInfo.SortDescriptors.Remove(cellElement.FieldName);
      this.currentContext.ViewInfo.SortDescriptors.Add(new SortDescriptor(cellElement.FieldName, ListSortDirection.Descending));
      this.currentContext.ViewInfo.SortDescriptors.EndUpdate();
    }

    protected virtual void OnSortAscendingItemClick(object sender, EventArgs e)
    {
      VirtualGridCellElement cellElement = this.GridElement.FindCellElement(this.currentContext.RowIndex, this.currentContext.ColumnIndex, this.currentContext.ViewInfo);
      this.currentContext.ViewInfo.SortDescriptors.BeginUpdate();
      this.currentContext.ViewInfo.SortDescriptors.Remove(cellElement.FieldName);
      this.currentContext.ViewInfo.SortDescriptors.Add(new SortDescriptor(cellElement.FieldName, ListSortDirection.Ascending));
      this.currentContext.ViewInfo.SortDescriptors.EndUpdate();
    }

    protected virtual void OnPinAtRightItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SetColumnPinPosition(this.currentContext.ColumnIndex, PinnedColumnPosition.Right);
    }

    protected virtual void OnPinAtLeftItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SetColumnPinPosition(this.currentContext.ColumnIndex, PinnedColumnPosition.Left);
    }

    protected virtual void OnPinAtBottomItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SetRowPinPosition(this.currentContext.RowIndex, PinnedRowPosition.Bottom);
    }

    protected virtual void OnPinAtTopItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SetRowPinPosition(this.currentContext.RowIndex, PinnedRowPosition.Top);
    }

    protected virtual void OnUnpinRowItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SetRowPinPosition(this.currentContext.RowIndex, PinnedRowPosition.None);
    }

    protected virtual void OnUnpinColumnItemClick(object sender, EventArgs e)
    {
      this.currentContext.ViewInfo.SetColumnPinPosition(this.currentContext.ColumnIndex, PinnedColumnPosition.None);
    }

    protected virtual void OnDeleteRowItemClick(object sender, EventArgs e)
    {
      this.GridElement.DeleteRow((IEnumerable<int>) new int[1]
      {
        this.currentContext.RowIndex
      }, this.currentContext.ViewInfo);
    }

    protected virtual void OnClearValueItemClick(object sender, EventArgs e)
    {
      this.GridElement.SetCellValue((object) null, this.currentContext.RowIndex, this.currentContext.ColumnIndex, this.currentContext.ViewInfo);
      this.GridElement.FindCellElement(this.currentContext.RowIndex, this.currentContext.ColumnIndex, this.currentContext.ViewInfo)?.Synchronize();
    }

    protected virtual void OnEditItemClick(object sender, EventArgs e)
    {
      this.GridElement.CurrentCell = this.currentContext;
      this.GridElement.BeginEdit();
    }

    protected virtual void OnPasteItemClick(object sender, EventArgs e)
    {
      this.GridElement.Paste();
    }

    protected virtual void OnCutItemClick(object sender, EventArgs e)
    {
      this.GridElement.CutSelection();
    }

    protected virtual void OnCopyItemClick(object sender, EventArgs e)
    {
      this.GridElement.CopySelection();
    }

    protected virtual void OnBestFitItemClick(object sender, EventArgs e)
    {
      this.GridElement.BestFitColumn(this.currentContext.ColumnIndex, this.currentContext.ViewInfo);
    }
  }
}
