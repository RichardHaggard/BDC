// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListFilterPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class RadListFilterPopup : BaseFilterPopup, IGridFilterPopupInteraction
  {
    private static Size Popup_Size = new Size(200, 300);
    private bool groupedDateValues;
    private GridTableElement tableElement;
    private FilterMenuTreeElement menuTreeElement;
    private FilterMenuButtonsItem buttonsMenuItem;
    private FilterMenuTextBoxItem textBoxMenuItem;
    private IRadListFilterElement listFilterElement;

    public FilterMenuTreeElement MenuTreeElement
    {
      get
      {
        return this.menuTreeElement;
      }
    }

    public FilterMenuButtonsItem ButtonsMenuItem
    {
      get
      {
        return this.buttonsMenuItem;
      }
    }

    public FilterMenuTextBoxItem TextBoxMenuItem
    {
      get
      {
        return this.textBoxMenuItem;
      }
    }

    protected System.Type ColumnFilteringDataType
    {
      get
      {
        GridViewComboBoxColumn dataColumn = this.DataColumn as GridViewComboBoxColumn;
        if (dataColumn != null)
          return dataColumn.FilteringMemberDataType;
        return this.DataColumn.DataType;
      }
    }

    public RadListFilterPopup(GridViewDataColumn dataColumn)
      : this(dataColumn, false)
    {
    }

    public RadListFilterPopup(GridViewDataColumn dataColumn, bool groupedDateValues)
    {
      this.groupedDateValues = groupedDateValues;
      this.DataColumn = dataColumn;
      this.tableElement = this.DataColumn.OwnerTemplate.MasterTemplate.Owner.TableElement;
      this.InitializeElements();
      this.Size = RadListFilterPopup.Popup_Size;
    }

    public bool IsPopupOpen
    {
      get
      {
        return PopupManager.Default.ContainsPopup((IPopupControl) this);
      }
    }

    public void ProcessKey(KeyEventArgs keys)
    {
      this.menuTreeElement.TreeView.CallOnKeyDown(keys);
    }

    protected override void OnPopupOpened()
    {
      base.OnPopupOpened();
      if (this.tableElement == null)
        return;
      this.tableElement.Focus();
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      if (target is RadSizableDropDownMenu || target is RadTreeView || target is RadGridView)
      {
        FilterMenuTreeItem filterMenuTreeItem = this.GetFilterMenuTreeItem();
        if (filterMenuTreeItem != null)
        {
          delta = delta >= 0 ? 3 : -3;
          filterMenuTreeItem.TreeElement.TreeView.TreeViewElement.ScrollTo(delta);
        }
      }
      return true;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      RadListFilterPopup.Popup_Size = new Size((int) ((double) this.Size.Width / (double) this.RootElement.DpiScaleFactor.Width), (int) ((double) this.Size.Height / (double) this.RootElement.DpiScaleFactor.Height));
      base.OnSizeChanged(e);
    }

    private FilterMenuTreeItem GetFilterMenuTreeItem()
    {
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is FilterMenuTreeItem)
          return (FilterMenuTreeItem) radItem;
      }
      return (FilterMenuTreeItem) null;
    }

    public override bool OnKeyDown(Keys keyData)
    {
      if (keyData != Keys.Return)
        return base.OnKeyDown(keyData);
      if (this.buttonsMenuItem.ButtonOK.Enabled)
        this.ButtonOK_Click((object) this, EventArgs.Empty);
      return true;
    }

    public override void SetTheme(string themeName)
    {
      this.ThemeName = themeName;
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is FilterMenuTreeItem)
          ((FilterMenuTreeItem) radItem).TreeElement.TreeView.ThemeName = themeName;
        else if (radItem is FilterMenuTextBoxItem)
          ((FilterMenuTextBoxItem) radItem).TextBox.ThemeName = themeName;
      }
      if (this.ThemeName == "TelerikMetroTouch" || !string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName) && ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.Width = 300;
        this.Height = 400;
        this.buttonsMenuItem.Margin = new Padding(0, 0, 20, 0);
      }
      else if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        Padding margin = this.buttonsMenuItem.Margin;
        margin.Right = 13;
        this.buttonsMenuItem.Margin = margin;
        this.Size = new Size(260, 510);
      }
      else
      {
        if (!this.ThemeName.StartsWith("Fluent") && (string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName) || !ThemeResolutionService.ApplicationThemeName.StartsWith("Fluent")))
          return;
        Padding margin = this.buttonsMenuItem.Margin;
        margin.Right = 10;
        this.buttonsMenuItem.Margin = margin;
      }
    }

    protected virtual void InitializeElements()
    {
      this.CreateFilterOperationsMenuItems();
      this.CreateListFilterMenuItems();
    }

    protected virtual void CreateFilterOperationsMenuItems()
    {
      FilterDescriptor filterDescriptor1 = this.GetFilterDescriptor();
      CompositeFilterDescriptor filterDescriptor2 = filterDescriptor1 as CompositeFilterDescriptor;
      CompositeFilterDescriptor.DescriptorType descriptorType = CompositeFilterDescriptor.GetDescriptorType(filterDescriptor2);
      RadMenuItem radMenuItem = new RadMenuItem();
      radMenuItem.IsChecked = false;
      radMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuAvailableFilters");
      radMenuItem.StretchVertically = false;
      this.Items.Add((RadItem) radMenuItem);
      RadMenuSeparatorItem menuSeparatorItem = new RadMenuSeparatorItem();
      menuSeparatorItem.StretchVertically = false;
      this.Items.Add((RadItem) menuSeparatorItem);
      System.Type filteringDataType = this.ColumnFilteringDataType;
      foreach (FilterOperationContext filterOperation in FilterOperationContext.GetFilterOperations(filteringDataType))
      {
        if (filterOperation.Operator == FilterOperator.None || filterOperation.Operator == FilterOperator.IsNull || filterOperation.Operator == FilterOperator.IsNotNull)
        {
          RadFilterOperationMenuItem operationMenuItem = new RadFilterOperationMenuItem(filterOperation);
          operationMenuItem.Click += new EventHandler(this.FilterMenuItem_Click);
          if (filterOperation.Operator == FilterOperator.None)
          {
            operationMenuItem.Enabled = filterDescriptor2 != null && filterDescriptor2.Operator != FilterOperator.None || operationMenuItem.Operator != filterDescriptor1.Operator;
            operationMenuItem.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuClearFilters");
            operationMenuItem.Image = (Image) Resources.ClearFilter;
            operationMenuItem.TextImageRelation = TextImageRelation.ImageBeforeText;
            operationMenuItem.ImageAlignment = ContentAlignment.MiddleLeft;
            operationMenuItem.DisplayStyle = DisplayStyle.ImageAndText;
            this.Items.Insert(0, (RadItem) operationMenuItem);
          }
          else
          {
            operationMenuItem.IsChecked = (filterDescriptor2 == null || filterDescriptor2.Operator == FilterOperator.None) && operationMenuItem.Operator == filterDescriptor1.Operator;
            if (operationMenuItem.IsChecked)
              radMenuItem.IsChecked = true;
            radMenuItem.Items.Add((RadItem) operationMenuItem);
          }
        }
        else
        {
          RadFilterComposeMenuItem filterComposeMenuItem = new RadFilterComposeMenuItem();
          filterComposeMenuItem.Text = filterOperation.Name;
          filterComposeMenuItem.FilterDescriptor = filterDescriptor1.Clone() as FilterDescriptor;
          filterComposeMenuItem.FilterDescriptor.Operator = filterOperation.Operator;
          filterComposeMenuItem.Click += new EventHandler(this.FilterMenuItem_Click);
          filterComposeMenuItem.IsChecked = (filterDescriptor2 == null || filterDescriptor2.Operator == FilterOperator.None) && filterOperation.Operator == filterDescriptor1.Operator;
          if (filterComposeMenuItem.IsChecked)
            radMenuItem.IsChecked = true;
          radMenuItem.Items.Add((RadItem) filterComposeMenuItem);
        }
      }
      if (GridViewHelper.IsNumeric(filteringDataType) || (object) filteringDataType == (object) typeof (DateTime))
      {
        RadFilterComposeMenuItem filterComposeMenuItem1 = new RadFilterComposeMenuItem("FilterFunctionsBetween");
        filterComposeMenuItem1.IsChecked = filterDescriptor2 != null && filterDescriptor2.Operator != FilterOperator.None && descriptorType == CompositeFilterDescriptor.DescriptorType.Between;
        if (filterComposeMenuItem1.IsChecked)
          radMenuItem.IsChecked = true;
        filterComposeMenuItem1.FilterDescriptor = (FilterDescriptor) this.GetCompositeFilterDescriptor(CompositeFilterDescriptor.DescriptorType.Between, filterDescriptor2);
        filterComposeMenuItem1.Click += new EventHandler(this.FilterMenuItem_Click);
        radMenuItem.Items.Add((RadItem) filterComposeMenuItem1);
        RadFilterComposeMenuItem filterComposeMenuItem2 = new RadFilterComposeMenuItem("FilterFunctionNotBetween");
        filterComposeMenuItem2.IsChecked = descriptorType == CompositeFilterDescriptor.DescriptorType.NotBetween;
        if (filterComposeMenuItem2.IsChecked)
          radMenuItem.IsChecked = true;
        filterComposeMenuItem2.FilterDescriptor = (FilterDescriptor) this.GetCompositeFilterDescriptor(CompositeFilterDescriptor.DescriptorType.NotBetween, filterDescriptor2);
        filterComposeMenuItem2.Click += new EventHandler(this.FilterMenuItem_Click);
        radMenuItem.Items.Add((RadItem) filterComposeMenuItem2);
      }
      if ((object) filteringDataType == (object) typeof (Image))
        return;
      RadFilterComposeMenuItem filterComposeMenuItem3 = new RadFilterComposeMenuItem("FilterFunctionsCustom");
      filterComposeMenuItem3.FilterDescriptor = filterDescriptor1.Clone() as FilterDescriptor;
      filterComposeMenuItem3.Click += new EventHandler(this.FilterMenuItem_Click);
      filterComposeMenuItem3.IsChecked = filterDescriptor2 != null && filterDescriptor2.Operator != FilterOperator.None && descriptorType == CompositeFilterDescriptor.DescriptorType.Unknown;
      if (filterComposeMenuItem3.IsChecked)
        radMenuItem.IsChecked = true;
      radMenuItem.Items.Add((RadItem) filterComposeMenuItem3);
    }

    protected virtual void CreateListFilterMenuItems()
    {
      this.textBoxMenuItem = new FilterMenuTextBoxItem();
      this.textBoxMenuItem.TextBox.TextChanged += new EventHandler(this.textBoxSearch_TextChanged);
      this.Items.Add((RadItem) this.textBoxMenuItem);
      System.Type filteringDataType = this.ColumnFilteringDataType;
      FilterMenuTreeItem filterMenuTreeItem = new FilterMenuTreeItem();
      filterMenuTreeItem.TreeElement.GroupedDateValues = this.groupedDateValues && ((object) filteringDataType == (object) typeof (DateTime) || (object) filteringDataType == (object) typeof (DateTime?));
      this.listFilterElement = (IRadListFilterElement) filterMenuTreeItem.TreeElement;
      this.menuTreeElement = filterMenuTreeItem.TreeElement;
      this.listFilterElement.SelectionChanged += new EventHandler(this.ListFilterElement_SelectionChanged);
      this.Items.Add((RadItem) filterMenuTreeItem);
      this.buttonsMenuItem = new FilterMenuButtonsItem();
      this.buttonsMenuItem.ButtonOK.Click += new EventHandler(this.ButtonOK_Click);
      this.buttonsMenuItem.ButtonCancel.Click += new EventHandler(this.ButtonCancel_Click);
      this.Items.Add((RadItem) this.buttonsMenuItem);
      RadListFilterDistinctValuesTable distinctValuesTable = this.GetDistinctValuesTable();
      this.listFilterElement.DistinctListValues = distinctValuesTable;
      RadListFilterDistinctValuesTable selectedValuesList = new RadListFilterDistinctValuesTable();
      selectedValuesList.FormatString = this.DataColumn.FormatString;
      RadListFilterDistinctValuesTable excludedValuesList = new RadListFilterDistinctValuesTable();
      excludedValuesList.FormatString = this.DataColumn.FormatString;
      this.listFilterElement.SelectedMode = this.GetGridFilteredValues(this.DataColumn.OwnerTemplate.FilterDescriptors, ref selectedValuesList, ref excludedValuesList);
      if (excludedValuesList.Count > 0)
      {
        selectedValuesList.Clear();
        foreach (DictionaryEntry dictionaryEntry in distinctValuesTable)
        {
          if (!excludedValuesList.Contains(dictionaryEntry.Key))
          {
            if (!(dictionaryEntry.Value is ArrayList))
              selectedValuesList.Add(dictionaryEntry.Key);
            else
              selectedValuesList.Add(dictionaryEntry.Key.ToString(), (ArrayList) dictionaryEntry.Value);
          }
        }
      }
      this.listFilterElement.SelectedValues = selectedValuesList;
      this.listFilterElement.EnableBlanks = this.DataColumn.ContainsBlanks;
    }

    protected virtual ListFilterSelectedMode GetGridFilteredValues(
      FilterDescriptorCollection descriptorCollection,
      ref RadListFilterDistinctValuesTable selectedValuesList,
      ref RadListFilterDistinctValuesTable excludedValuesList)
    {
      ListFilterSelectedMode filterSelectedMode = ListFilterSelectedMode.All;
      foreach (FilterDescriptor descriptor in (Collection<FilterDescriptor>) descriptorCollection)
      {
        if (descriptor is CompositeFilterDescriptor)
          filterSelectedMode = this.GetGridFilteredValues(((CompositeFilterDescriptor) descriptor).FilterDescriptors, ref selectedValuesList, ref excludedValuesList);
        else if (descriptor.PropertyName == this.DataColumn.Name)
        {
          switch (descriptor.Operator)
          {
            case FilterOperator.None:
            case FilterOperator.IsLike:
            case FilterOperator.IsNotLike:
            case FilterOperator.IsLessThan:
            case FilterOperator.IsLessThanOrEqualTo:
            case FilterOperator.IsGreaterThanOrEqualTo:
            case FilterOperator.IsGreaterThan:
            case FilterOperator.StartsWith:
            case FilterOperator.EndsWith:
            case FilterOperator.Contains:
            case FilterOperator.NotContains:
            case FilterOperator.IsContainedIn:
            case FilterOperator.IsNotContainedIn:
              filterSelectedMode = ListFilterSelectedMode.None;
              continue;
            case FilterOperator.IsEqualTo:
              selectedValuesList.Add(descriptor.Value);
              filterSelectedMode = ListFilterSelectedMode.Custom;
              continue;
            case FilterOperator.IsNotEqualTo:
              excludedValuesList.Add(descriptor.Value);
              filterSelectedMode = ListFilterSelectedMode.Custom;
              continue;
            case FilterOperator.IsNull:
              return ListFilterSelectedMode.Null;
            case FilterOperator.IsNotNull:
              return ListFilterSelectedMode.NotNull;
            default:
              continue;
          }
        }
      }
      return filterSelectedMode;
    }

    protected virtual void EnsureButtonOK()
    {
      if (this.listFilterElement.SelectedMode == ListFilterSelectedMode.None)
        this.buttonsMenuItem.ButtonOK.Enabled = false;
      else
        this.buttonsMenuItem.ButtonOK.Enabled = true;
    }

    protected virtual void OnButtonCancelClick(EventArgs e)
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected virtual void OnButtonOkClick(EventArgs e)
    {
      FilterOperator filterOperator = FilterOperator.IsEqualTo;
      switch (this.listFilterElement.SelectedMode)
      {
        case ListFilterSelectedMode.All:
          filterOperator = FilterOperator.None;
          break;
        case ListFilterSelectedMode.Null:
          filterOperator = FilterOperator.IsNull;
          break;
        case ListFilterSelectedMode.NotNull:
          filterOperator = FilterOperator.IsNotNull;
          break;
      }
      if (filterOperator != FilterOperator.IsEqualTo)
      {
        this.SetFilterOperator(filterOperator);
        this.ClosePopup(RadPopupCloseReason.CloseCalled);
      }
      else
      {
        CompositeFilterDescriptor filterDescriptor1 = new CompositeFilterDescriptor();
        filterDescriptor1.PropertyName = this.DataColumn.Name;
        RadListFilterDistinctValuesTable distinctValuesTable = this.GetDistinctValuesTable();
        string localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterMenuBlanks");
        bool flag = this.listFilterElement.SelectedValues.Contains((object) localizedString);
        if (this.listFilterElement.SelectedValues.Count > distinctValuesTable.Count / 2 && !flag)
        {
          filterDescriptor1.LogicalOperator = FilterLogicalOperator.And;
          foreach (DictionaryEntry dictionaryEntry in distinctValuesTable)
          {
            object key = dictionaryEntry.Key;
            if (string.IsNullOrEmpty(Convert.ToString(key)))
              key = (object) localizedString;
            DateTime result1;
            object result2;
            if ((this.DataColumn is GridViewDateTimeColumn || (object) this.DataColumn.DataType == (object) typeof (DateTime) || (object) this.DataColumn.DataType == (object) typeof (DateTime?)) && DateTime.TryParse(key.ToString(), out result1) && RadDataConverter.Instance.TryFormat((object) (this.groupedDateValues ? result1.Date : result1), typeof (string), (IDataConversionInfoProvider) this.DataColumn, out result2) == null)
              key = result2;
            if (!this.listFilterElement.SelectedValues.Contains(key))
            {
              foreach (object obj in (ArrayList) dictionaryEntry.Value)
              {
                FilterDescriptor filterDescriptor2 = obj != DBNull.Value ? (this.DataColumn is GridViewDateTimeColumn || (object) this.DataColumn.DataType == (object) typeof (DateTime) || (object) this.DataColumn.DataType == (object) typeof (DateTime?) ? (FilterDescriptor) new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsNotEqualTo, (DateTime?) obj, false) : new FilterDescriptor(this.DataColumn.Name, FilterOperator.IsNotEqualTo, obj)) : new FilterDescriptor(this.DataColumn.Name, FilterOperator.IsNotEqualTo, (object) null);
                filterDescriptor1.FilterDescriptors.Add(filterDescriptor2);
              }
            }
          }
        }
        else
        {
          filterDescriptor1.LogicalOperator = FilterLogicalOperator.Or;
          foreach (DictionaryEntry selectedValue in this.listFilterElement.SelectedValues)
          {
            foreach (object obj in (ArrayList) selectedValue.Value)
            {
              FilterDescriptor filterDescriptor2 = obj != DBNull.Value ? (this.DataColumn is GridViewDateTimeColumn || (object) this.DataColumn.DataType == (object) typeof (DateTime) || (object) this.DataColumn.DataType == (object) typeof (DateTime?) ? (FilterDescriptor) new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, (DateTime?) obj, false) : new FilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, obj)) : new FilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, (object) null);
              filterDescriptor1.FilterDescriptors.Add(filterDescriptor2);
            }
          }
        }
        this.FilterDescriptor = (FilterDescriptor) filterDescriptor1;
        this.OnFilterConfirmed();
      }
    }

    protected virtual void OnFilterListSelectionChanged(EventArgs e)
    {
      this.EnsureButtonOK();
    }

    protected virtual void OnTextBoxTextChanged(EventArgs e)
    {
      this.listFilterElement.Filter(this.textBoxMenuItem.TextBox.Text);
      this.EnsureButtonOK();
    }

    protected virtual void OnFilterMenuItemClick(object sender, EventArgs e)
    {
      RadFilterOperationMenuItem operationMenuItem = sender as RadFilterOperationMenuItem;
      if (operationMenuItem != null && !operationMenuItem.IsChecked)
        this.SetFilterOperator(operationMenuItem.Operator);
      RadFilterComposeMenuItem menuItem = sender as RadFilterComposeMenuItem;
      if (menuItem == null)
        return;
      this.EditFilterDescriptor(menuItem);
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      this.OnButtonCancelClick(e);
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
      this.OnButtonOkClick(e);
    }

    private void ListFilterElement_SelectionChanged(object sender, EventArgs e)
    {
      this.OnFilterListSelectionChanged(e);
    }

    private void textBoxSearch_TextChanged(object sender, EventArgs e)
    {
      this.OnTextBoxTextChanged(e);
    }

    private void FilterMenuItem_Click(object sender, EventArgs e)
    {
      this.OnFilterMenuItemClick(sender, e);
    }

    protected override void Dispose(bool disposing)
    {
      this.textBoxMenuItem.TextBox.TextChanged -= new EventHandler(this.textBoxSearch_TextChanged);
      this.listFilterElement.SelectionChanged -= new EventHandler(this.ListFilterElement_SelectionChanged);
      this.buttonsMenuItem.ButtonOK.Click -= new EventHandler(this.ButtonOK_Click);
      this.buttonsMenuItem.ButtonCancel.Click -= new EventHandler(this.ButtonCancel_Click);
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is RadMenuItem)
        {
          foreach (RadMenuItem radMenuItem in (RadItemCollection) ((RadMenuItemBase) radItem).Items)
            radItem.Click -= new EventHandler(this.FilterMenuItem_Click);
          radItem.Click -= new EventHandler(this.FilterMenuItem_Click);
        }
      }
      base.Dispose(disposing);
    }
  }
}
