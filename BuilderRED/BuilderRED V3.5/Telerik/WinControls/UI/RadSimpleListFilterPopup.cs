// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSimpleListFilterPopup
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
  public class RadSimpleListFilterPopup : BaseFilterPopup, IGridFilterPopupInteraction
  {
    private static Size Popup_Size = new Size(200, 340);
    private GridTableElement tableElement;
    private FilterMenuListItem listItem;

    public FilterMenuListItem ListMenuItem
    {
      get
      {
        return this.listItem;
      }
    }

    public RadSimpleListFilterPopup(GridViewDataColumn dataColumn)
    {
      this.DataColumn = dataColumn;
      this.tableElement = this.DataColumn.OwnerTemplate.MasterTemplate.Owner.TableElement;
      this.InitializeElements();
      this.Size = RadSimpleListFilterPopup.Popup_Size;
      this.PopupOpened += new RadPopupOpenedEventHandler(this.RadSimpleListFilterPopup_PopupOpened);
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
      this.listItem.ListControl.CallOnKeyDown(keys);
    }

    protected virtual void InitializeElements()
    {
      this.CreateGeneralMenuItems();
      this.CreateValueListElement();
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

    protected virtual void CreateValueListElement()
    {
      this.listItem = new FilterMenuListItem();
      this.listItem.Margin = new Padding(0, 5, 0, 0);
      this.listItem.DistinctListValues = this.GetDistinctValuesTable();
      this.SetListSelection();
      this.listItem.ListControl.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ListControl_SelectedIndexChanged);
      this.Items.Add((RadItem) this.listItem);
    }

    protected virtual void SetListSelection()
    {
      FilterDescriptor filterDescriptor1 = this.GetFilterDescriptor();
      CompositeFilterDescriptor filterDescriptor2 = filterDescriptor1 as CompositeFilterDescriptor;
      if (filterDescriptor2 != null)
      {
        foreach (FilterDescriptor filterDescriptor3 in (Collection<FilterDescriptor>) filterDescriptor2.FilterDescriptors)
        {
          if (filterDescriptor3.Operator == FilterOperator.IsEqualTo && filterDescriptor3.Value != null)
          {
            foreach (RadListDataItem radListDataItem in this.listItem.ListControl.Items)
            {
              if (radListDataItem.Text == filterDescriptor3.Value.ToString())
                radListDataItem.Selected = true;
            }
          }
        }
      }
      else
      {
        if (filterDescriptor1.Operator != FilterOperator.IsEqualTo)
          return;
        foreach (RadListDataItem radListDataItem in this.listItem.ListControl.Items)
        {
          if (radListDataItem.Value is ArrayList && ((ArrayList) radListDataItem.Value)[0] == filterDescriptor1.Value)
            radListDataItem.Selected = true;
        }
      }
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
      if ((target is RadSizableDropDownMenu || target is RadListControl || target is RadGridView) && this.listItem != null)
      {
        delta = delta >= 0 ? 3 : -3;
        this.ListItemScrollTo(delta);
      }
      return true;
    }

    public void ListItemScrollTo(int delta)
    {
      int num = this.listItem.ListControl.ListElement.VScrollBar.Value - delta * this.listItem.ListControl.ListElement.VScrollBar.SmallChange;
      if (num > this.listItem.ListControl.ListElement.VScrollBar.Maximum - this.listItem.ListControl.ListElement.VScrollBar.LargeChange + 1)
        num = this.listItem.ListControl.ListElement.VScrollBar.Maximum - this.listItem.ListControl.ListElement.VScrollBar.LargeChange + 1;
      if (num < this.listItem.ListControl.ListElement.VScrollBar.Minimum)
        num = 0;
      else if (num > this.listItem.ListControl.ListElement.VScrollBar.Maximum)
        num = this.listItem.ListControl.ListElement.VScrollBar.Maximum;
      this.listItem.ListControl.ListElement.VScrollBar.Value = num;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      RadSimpleListFilterPopup.Popup_Size = new Size((int) ((double) this.Size.Width / (double) this.RootElement.DpiScaleFactor.Width), (int) ((double) this.Size.Height / (double) this.RootElement.DpiScaleFactor.Height));
      base.OnSizeChanged(e);
    }

    public override void SetTheme(string themeName)
    {
      this.ThemeName = themeName;
      foreach (RadItem radItem in (RadItemCollection) this.Items)
      {
        if (radItem is FilterMenuListItem)
          ((FilterMenuListItem) radItem).ListControl.ThemeName = themeName;
      }
    }

    protected virtual void OnListSelectedIndexChanged(Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (!(this.listItem.ListControl.SelectedValue is ArrayList))
        return;
      this.FilterDescriptor = new FilterDescriptor(this.DataColumn.Name, FilterOperator.IsEqualTo, ((ArrayList) this.listItem.ListControl.SelectedValue)[0]);
    }

    protected virtual void OnSimpleListPopupOpened(EventArgs e)
    {
      this.Focus();
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

    private void ListControl_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.OnListSelectedIndexChanged(e);
      this.listItem.ListControl.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ListControl_SelectedIndexChanged);
      this.OnFilterConfirmed();
    }

    private void RadSimpleListFilterPopup_PopupOpened(object sender, EventArgs args)
    {
      this.OnSimpleListPopupOpened(args);
      this.PopupOpened -= new RadPopupOpenedEventHandler(this.RadSimpleListFilterPopup_PopupOpened);
    }

    private void FilterMenuItem_Click(object sender, EventArgs e)
    {
      this.OnFilterMenuItemClick(sender, e);
    }

    protected override void Dispose(bool disposing)
    {
      this.PopupOpening -= new RadPopupOpeningEventHandler(this.RadSimpleListFilterPopup_PopupOpened);
      this.listItem.ListControl.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ListControl_SelectedIndexChanged);
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
