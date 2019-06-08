// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateFilterPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class RadDateFilterPopup : BaseFilterPopup
  {
    private static Size Popup_Size = new Size(220, 300);
    private int DefaultHeight = 300;
    private FilterMenuCalendarItem calendarItem;
    private FilterMenuButtonsItem buttonsMenuItem;

    public FilterMenuCalendarItem CalendarItem
    {
      get
      {
        return this.calendarItem;
      }
    }

    public FilterMenuButtonsItem ButtonsMenuItem
    {
      get
      {
        return this.buttonsMenuItem;
      }
    }

    public RadDateFilterPopup(GridViewDataColumn dataColumn)
    {
      this.DataColumn = dataColumn;
      this.Size = RadDateFilterPopup.Popup_Size;
      this.InitializeElements();
      this.PopupOpened += new RadPopupOpenedEventHandler(this.RadDateFilterPopup_PopupOpened);
    }

    protected virtual void InitializeElements()
    {
      this.CreateGeneralMenuItems();
      this.CreateCalendarElement();
      this.CreateButtonsElement();
      this.CreateDateCustomItems();
    }

    protected virtual void CreateGeneralMenuItems()
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
      foreach (FilterOperationContext filterOperation in FilterOperationContext.GetFilterOperations(this.DataColumn.DataType))
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
      if (!GridViewHelper.IsNumeric(this.DataColumn.DataType) && (object) this.DataColumn.DataType != (object) typeof (DateTime))
        return;
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

    protected virtual void CreateCalendarElement()
    {
      this.calendarItem = new FilterMenuCalendarItem();
      this.calendarItem.Margin = new Padding(0, 0, 0, 5);
      this.Items.Add((RadItem) this.calendarItem);
    }

    protected virtual void CreateDateCustomItems()
    {
      this.AddCustomMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionToday"), (FilterDescriptor) new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, new DateTime?(DateTime.Today), false));
      this.AddCustomMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionYesterday"), (FilterDescriptor) new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, new DateTime?(DateTime.Today.AddDays(-1.0)), false));
      string localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionDuringLast7days");
      CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
      filterDescriptor.FilterDescriptors.Add((FilterDescriptor) new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsGreaterThanOrEqualTo, new DateTime?(DateTime.Today.AddDays(-7.0)), false));
      filterDescriptor.FilterDescriptors.Add((FilterDescriptor) new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsLessThanOrEqualTo, new DateTime?(DateTime.Today), false));
      this.AddCustomMenuItem(localizedString, (FilterDescriptor) filterDescriptor);
    }

    protected virtual void CreateButtonsElement()
    {
      this.buttonsMenuItem = new FilterMenuButtonsItem();
      this.buttonsMenuItem.Margin = new Padding(0, 5, 0, 0);
      this.buttonsMenuItem.ButtonOK.Click += new EventHandler(this.ButtonOK_Click);
      this.buttonsMenuItem.ButtonCancel.Click += new EventHandler(this.ButtonCancel_Click);
      this.Items.Add((RadItem) this.buttonsMenuItem);
    }

    protected virtual void SetInitialSelection()
    {
      List<DateTime> selectedDates = new List<DateTime>();
      FilterDescriptor filterDescriptor = this.GetFilterDescriptor();
      this.GetSelectedDates(filterDescriptor, ref selectedDates);
      if (this.ShouldSelectDatesInCalendar(selectedDates))
      {
        this.calendarItem.CalendarElement.Calendar.SelectedDates.AddRange(selectedDates.ToArray());
        this.calendarItem.IsChecked = true;
      }
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        FilterMenuCustomDateItem menuCustomDateItem = radItem as FilterMenuCustomDateItem;
        if (menuCustomDateItem != null)
          menuCustomDateItem.IsChecked = this.SearchForAppliedFilterDescriptors(filterDescriptor, menuCustomDateItem.FilterDescriptor);
      }
    }

    protected virtual bool ShouldSelectDatesInCalendar(List<DateTime> selectedDates)
    {
      return selectedDates != null && selectedDates.Count != 0 && (selectedDates.Count != 1 || !this.IsDateToday(selectedDates[0]) && !this.IsDateYesterday(selectedDates[0])) && (selectedDates.Count != 2 || (!this.IsDateToday(selectedDates[0]) || !this.IsDateYesterday(selectedDates[1])) && (!this.IsDateToday(selectedDates[1]) || !this.IsDateYesterday(selectedDates[0])));
    }

    private bool SearchForAppliedFilterDescriptors(
      FilterDescriptor descriptor1,
      FilterDescriptor descriptor2)
    {
      if (descriptor1.Expression == descriptor2.Expression)
        return true;
      if (descriptor1 is CompositeFilterDescriptor)
      {
        foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) ((CompositeFilterDescriptor) descriptor1).FilterDescriptors)
        {
          if (this.SearchForAppliedFilterDescriptors(filterDescriptor, descriptor2))
            return true;
        }
      }
      return false;
    }

    private void GetSelectedDates(FilterDescriptor descriptor, ref List<DateTime> selectedDates)
    {
      if (descriptor is CompositeFilterDescriptor)
      {
        foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) ((CompositeFilterDescriptor) descriptor).FilterDescriptors)
          this.GetSelectedDates(filterDescriptor, ref selectedDates);
      }
      if (!(descriptor is DateFilterDescriptor) || descriptor.Operator != FilterOperator.IsEqualTo || !((DateFilterDescriptor) descriptor).Value.HasValue)
        return;
      selectedDates.Add(((DateFilterDescriptor) descriptor).Value.Value);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      RadDateFilterPopup.Popup_Size = new Size((int) ((double) this.Size.Width / (double) this.RootElement.DpiScaleFactor.Width), (int) ((double) this.Size.Height / (double) this.RootElement.DpiScaleFactor.Height));
      base.OnSizeChanged(e);
    }

    private bool IsDateToday(DateTime date)
    {
      return date.Date == DateTime.Today.Date;
    }

    private bool IsDateYesterday(DateTime date)
    {
      return date.Date == DateTime.Today.Date.AddDays(-1.0);
    }

    public virtual void ClearCustomMenuItems()
    {
      List<RadItem> radItemList = new List<RadItem>();
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is FilterMenuCustomDateItem)
          radItemList.Add(radItem);
      }
      foreach (RadItem radItem in radItemList)
      {
        this.Items.Remove(radItem);
        if (this.Size.Height == this.DefaultHeight)
        {
          this.Size = new Size(this.Size.Width, this.Size.Height - 25);
          this.DefaultHeight -= 25;
        }
      }
    }

    public virtual void AddCustomMenuItem(string text, FilterDescriptor descriptor)
    {
      FilterMenuCustomDateItem menuCustomDateItem = new FilterMenuCustomDateItem(text, descriptor);
      menuCustomDateItem.ToggleStateChanged += new StateChangedEventHandler(this.FilterMenuCustomItem_ToggleStateChanged);
      this.Items.Insert(this.Items.Count - 1, (RadItem) menuCustomDateItem);
      if (this.Size.Height != this.DefaultHeight)
        return;
      this.Size = new Size(this.Size.Width, this.Size.Height + 25);
      this.DefaultHeight += 25;
    }

    public virtual void RemoveCustomMenuItem(int position)
    {
      int num = 0;
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is FilterMenuCustomDateItem)
        {
          if (num == position)
          {
            this.Items.Remove(radItem);
            if (this.Size.Height != this.DefaultHeight)
              break;
            this.Size = new Size(this.Size.Width, this.Size.Height - 25);
            this.DefaultHeight -= 25;
            break;
          }
          ++num;
        }
      }
    }

    public override void SetTheme(string themeName)
    {
      this.ThemeName = themeName;
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is FilterMenuCalendarItem)
          ((FilterMenuCalendarItem) radItem).CalendarElement.Calendar.ThemeName = themeName;
      }
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.Width = 350;
        this.Height = 550;
        this.buttonsMenuItem.Margin = new Padding(0, 0, 20, 0);
        this.calendarItem.MinSize = new Size(300, 300);
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

    protected virtual void OnButtonOkClick(EventArgs e)
    {
      CompositeFilterDescriptor filterDescriptor1 = new CompositeFilterDescriptor();
      filterDescriptor1.PropertyName = this.DataColumn.Name;
      filterDescriptor1.LogicalOperator = FilterLogicalOperator.Or;
      if (this.calendarItem.IsChecked)
      {
        if (this.calendarItem.CalendarElement.Calendar.SelectedDates.Count == 0)
        {
          DateFilterDescriptor filterDescriptor2 = new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, new DateTime?(this.calendarItem.CalendarElement.Calendar.FocusedDate), false);
          filterDescriptor1.FilterDescriptors.Add((FilterDescriptor) filterDescriptor2);
        }
        else
        {
          foreach (DateTime selectedDate in this.calendarItem.CalendarElement.Calendar.SelectedDates)
          {
            DateFilterDescriptor filterDescriptor2 = new DateFilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, new DateTime?(selectedDate), false);
            filterDescriptor1.FilterDescriptors.Add((FilterDescriptor) filterDescriptor2);
          }
        }
      }
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        FilterMenuCustomDateItem menuCustomDateItem = radItem as FilterMenuCustomDateItem;
        if (menuCustomDateItem != null && menuCustomDateItem.IsChecked && !this.SearchForAppliedFilterDescriptors((FilterDescriptor) filterDescriptor1, menuCustomDateItem.FilterDescriptor))
          filterDescriptor1.FilterDescriptors.Add(menuCustomDateItem.FilterDescriptor);
      }
      this.FilterDescriptor = (FilterDescriptor) filterDescriptor1;
      this.OnFilterConfirmed();
    }

    protected virtual void OnButtonCancelClick(EventArgs e)
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected virtual void OnCustomFilterItemToggleStateChanged(
      object sender,
      StateChangedEventArgs e)
    {
      FilterMenuCustomDateItem menuCustomDateItem = sender as FilterMenuCustomDateItem;
      if (menuCustomDateItem.IsChecked || menuCustomDateItem.FilterDescriptor.Operator != FilterOperator.IsEqualTo || !this.calendarItem.IsChecked)
        return;
      DateTime? nullable = new DateTime?();
      if (menuCustomDateItem.FilterDescriptor.Value is DateTime)
        nullable = new DateTime?((DateTime) menuCustomDateItem.FilterDescriptor.Value);
      else if (menuCustomDateItem.FilterDescriptor.Value is DateTime?)
        nullable = (DateTime?) menuCustomDateItem.FilterDescriptor.Value;
      if (!nullable.HasValue)
        return;
      this.calendarItem.CalendarElement.Calendar.SelectedDates.Remove(nullable.Value);
      if (this.calendarItem.CalendarElement.Calendar.SelectedDates.Count != 0)
        return;
      this.calendarItem.IsChecked = false;
    }

    protected virtual void OnDateFilterPopupOpened(EventArgs e)
    {
      this.SetInitialSelection();
      this.Focus();
    }

    protected virtual void OnFilterItemClick(object sender, EventArgs e)
    {
      RadFilterOperationMenuItem operationMenuItem = sender as RadFilterOperationMenuItem;
      if (operationMenuItem != null && !operationMenuItem.IsChecked)
        this.SetFilterOperator(operationMenuItem.Operator);
      RadFilterComposeMenuItem menuItem = sender as RadFilterComposeMenuItem;
      if (menuItem == null)
        return;
      this.EditFilterDescriptor(menuItem);
    }

    private void FilterMenuCustomItem_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.OnCustomFilterItemToggleStateChanged(sender, args);
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      this.OnButtonCancelClick(e);
    }

    private void ButtonOK_Click(object sender, EventArgs e)
    {
      this.OnButtonOkClick(e);
    }

    private void RadDateFilterPopup_PopupOpened(object sender, EventArgs args)
    {
      this.OnDateFilterPopupOpened(args);
    }

    private void FilterMenuItem_Click(object sender, EventArgs e)
    {
      this.OnFilterItemClick(sender, e);
    }

    protected override void Dispose(bool disposing)
    {
      this.buttonsMenuItem.ButtonOK.Click -= new EventHandler(this.ButtonOK_Click);
      this.PopupOpened -= new RadPopupOpenedEventHandler(this.RadDateFilterPopup_PopupOpened);
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is RadMenuItem)
        {
          foreach (RadElement radElement in (RadItemCollection) ((RadMenuItemBase) radItem).Items)
            radElement.Click -= new EventHandler(this.FilterMenuItem_Click);
          radItem.Click -= new EventHandler(this.FilterMenuItem_Click);
        }
        else if (radItem is FilterMenuCustomDateItem)
          ((FilterMenuCustomDateItem) radItem).ToggleStateChanged -= new StateChangedEventHandler(this.FilterMenuCustomItem_ToggleStateChanged);
      }
      base.Dispose(disposing);
    }
  }
}
