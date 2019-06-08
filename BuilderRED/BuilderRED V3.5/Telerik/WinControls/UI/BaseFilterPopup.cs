// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseFilterPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public abstract class BaseFilterPopup : RadSizableDropDownMenu, IGridFilterPopup, IDisposable
  {
    private GridViewDataColumn dataColumn;
    private FilterDescriptor filterDescriptor;
    private bool clickedBetweenItem;

    protected internal GridViewDataColumn DataColumn
    {
      get
      {
        return this.dataColumn;
      }
      set
      {
        this.dataColumn = value;
      }
    }

    protected virtual FilterDescriptor CreateFilterDescriptor(
      FilterOperator filterOperator)
    {
      return !(this.dataColumn is GridViewDateTimeColumn) ? new FilterDescriptor(this.dataColumn.Name, filterOperator, (object) null) : (FilterDescriptor) new DateFilterDescriptor(this.dataColumn.Name, filterOperator, new DateTime?(), false);
    }

    protected virtual void EditFilterDescriptor(RadFilterComposeMenuItem menuItem)
    {
      RadFilterComposeMenuItem filterComposeMenuItem = menuItem;
      this.clickedBetweenItem = false;
      if (menuItem.Text == LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionsBetween") || menuItem.Text == LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionNotBetween"))
        this.clickedBetweenItem = true;
      string themeName = this.ThemeName;
      using (BaseCompositeFilterDialog compositeFilterForm = this.CreateCompositeFilterForm())
      {
        compositeFilterForm.Initialize(this.dataColumn, filterComposeMenuItem.FilterDescriptor, true);
        compositeFilterForm.ThemeName = themeName;
        if (compositeFilterForm.ShowDialog() == DialogResult.Cancel)
          return;
        FilterDescriptor filterDescriptor = compositeFilterForm.FilterDescriptor;
        if (!GridFilterCellElement.ValidateUserFilter(filterDescriptor))
          return;
        this.filterDescriptor = filterDescriptor;
        this.OnFilterConfirmed();
      }
    }

    protected virtual BaseCompositeFilterDialog CreateCompositeFilterForm()
    {
      if (this.DataColumn != null && this.DataColumn.OwnerTemplate != null && this.DataColumn.OwnerTemplate.MasterTemplate != null)
      {
        GridViewCreateCompositeFilterDialogEventArgs args = new GridViewCreateCompositeFilterDialogEventArgs();
        args.Dialog = !this.clickedBetweenItem ? (BaseCompositeFilterDialog) new CompositeDataFilterForm() : (BaseCompositeFilterDialog) new CompositeFilterForm();
        this.DataColumn.OwnerTemplate.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCreateCompositeFilterDialogEventArgs>(EventDispatcher.CreateCompositeFilterDialog, (object) this, args);
        if (args.Dialog != null)
          return args.Dialog;
      }
      if (this.clickedBetweenItem)
        return (BaseCompositeFilterDialog) new CompositeFilterForm();
      return (BaseCompositeFilterDialog) new CompositeDataFilterForm();
    }

    protected virtual CompositeFilterDescriptor GetCompositeFilterDescriptor(
      CompositeFilterDescriptor.DescriptorType desiredType,
      CompositeFilterDescriptor currentDescriptor)
    {
      CompositeFilterDescriptor filterDescriptor;
      if (currentDescriptor != null)
      {
        filterDescriptor = currentDescriptor.Clone() as CompositeFilterDescriptor;
        filterDescriptor.NotOperator = desiredType == CompositeFilterDescriptor.DescriptorType.NotBetween;
      }
      else
        filterDescriptor = CompositeFilterDescriptor.CreateDescriptor(desiredType, this.dataColumn.Name, this.dataColumn.DataType, (object[]) null);
      return filterDescriptor;
    }

    protected virtual RadListFilterDistinctValuesTable GetDistinctValuesTable()
    {
      RadListFilterDistinctValuesTable distinctValuesTable = new RadListFilterDistinctValuesTable();
      distinctValuesTable.FormatString = this.dataColumn.FormatString;
      distinctValuesTable.DataConversionInfoProvider = (IDataConversionInfoProvider) this.dataColumn;
      GridViewColumnValuesCollection valuesWithFilter = this.dataColumn.DistinctValuesWithFilter;
      if (valuesWithFilter == null)
        return distinctValuesTable;
      GridViewComboBoxColumn dataColumn = this.dataColumn as GridViewComboBoxColumn;
      if (dataColumn != null && !string.IsNullOrEmpty(dataColumn.ValueMember))
      {
        foreach (object cellValue in valuesWithFilter)
        {
          if (cellValue != null && cellValue != DBNull.Value)
          {
            object filterValue = cellValue;
            object lookupValue = ((GridViewComboBoxColumn) this.dataColumn).GetLookupValue(cellValue);
            if (dataColumn.FilteringMode == GridViewFilteringMode.DisplayMember)
              filterValue = lookupValue;
            if (lookupValue != null)
              distinctValuesTable.Add(lookupValue.ToString(), filterValue);
          }
        }
      }
      else
      {
        foreach (object obj in valuesWithFilter)
          distinctValuesTable.Add(obj);
      }
      return distinctValuesTable;
    }

    protected virtual FilterDescriptor GetFilterDescriptor()
    {
      return this.dataColumn.FilterDescriptor ?? this.CreateFilterDescriptor(FilterOperator.None);
    }

    protected virtual void SetFilterOperator(FilterOperator filterOperator)
    {
      if (filterOperator != FilterOperator.None && filterOperator != FilterOperator.IsNull && filterOperator != FilterOperator.IsNotNull)
        throw new InvalidOperationException("Invalid filter operator in SetFilterOperator context!");
      this.filterDescriptor = (FilterDescriptor) null;
      if (filterOperator != FilterOperator.None)
        this.filterDescriptor = this.CreateFilterDescriptor(filterOperator);
      this.OnFilterConfirmed();
    }

    protected virtual bool ValidateUserFilter(FilterDescriptor descriptor)
    {
      return GridFilterCellElement.ValidateUserFilter(descriptor);
    }

    public abstract void SetTheme(string themeName);

    public virtual FilterDescriptor FilterDescriptor
    {
      get
      {
        return this.filterDescriptor;
      }
      set
      {
        this.filterDescriptor = value;
      }
    }

    public event EventHandler FilterConfirmed;

    protected virtual void OnFilterConfirmed()
    {
      if (this.dataColumn.OwnerTemplate.ExcelFilteredColumns.Contains(this.dataColumn))
        this.dataColumn.OwnerTemplate.ExcelFilteredColumns.Remove(this.dataColumn);
      if (this.filterDescriptor != null)
      {
        this.dataColumn.OwnerTemplate.ExcelFilteredColumns.Add(this.dataColumn);
        this.dataColumn.CreateSnapshot();
      }
      if (this.FilterConfirmed == null)
        return;
      this.FilterConfirmed((object) this, EventArgs.Empty);
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      base.ScaleControl(factor, specified);
    }

    [SpecialName]
    void IGridFilterPopup.add_PopupOpening(RadPopupOpeningEventHandler _param1)
    {
      this.PopupOpening += _param1;
    }

    [SpecialName]
    void IGridFilterPopup.remove_PopupOpening(RadPopupOpeningEventHandler _param1)
    {
      this.PopupOpening -= _param1;
    }

    [SpecialName]
    void IGridFilterPopup.add_PopupClosed(RadPopupClosedEventHandler _param1)
    {
      this.PopupClosed += _param1;
    }

    [SpecialName]
    void IGridFilterPopup.remove_PopupClosed(RadPopupClosedEventHandler _param1)
    {
      this.PopupClosed -= _param1;
    }

    void IGridFilterPopup.Show(Point _param1)
    {
      this.Show(_param1);
    }
  }
}
