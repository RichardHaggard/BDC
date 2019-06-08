// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class GridFilterCellElement : GridDataCellElement
  {
    public static RadProperty IsFilterAppliedProperty = RadProperty.Register(nameof (IsFilterApplied), typeof (bool), typeof (GridCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementSpacingProperty = RadProperty.Register(nameof (ElementSpacing), typeof (int), typeof (GridFilterCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private const float EditorMinWidth = 40f;
    private const float OperatorMinWidth = 10f;
    private RadButtonElement filterFunctionButton;
    private TextPrimitive filterOperator;
    private bool clickedBetweenItem;

    public GridFilterCellElement(GridViewDataColumn column, GridRowElement row)
      : base((GridViewColumn) column, row)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipDrawing = true;
    }

    protected override void CreateChildElements()
    {
      this.filterFunctionButton = (RadButtonElement) new GridFilterButtonElement();
      this.filterFunctionButton.NotifyParentOnMouseInput = false;
      this.filterFunctionButton.StretchHorizontally = false;
      this.filterFunctionButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.filterFunctionButton.Alignment = ContentAlignment.MiddleRight;
      this.filterFunctionButton.Class = "FilterFunctionButton";
      this.filterFunctionButton.Image = (Image) Resources.FilteringIcon;
      this.filterFunctionButton.Click += new EventHandler(this.FilterFunctionButton_Click);
      this.filterFunctionButton.ZIndex = 100;
      this.filterFunctionButton.GetValue(GridFilterCellElement.IsFilterAppliedProperty);
      this.Children.Add((RadElement) this.filterFunctionButton);
      this.filterOperator = new TextPrimitive();
      this.filterOperator.BypassLayoutPolicies = true;
      this.filterOperator.Alignment = ContentAlignment.MiddleLeft;
      this.filterOperator.TextAlignment = ContentAlignment.MiddleLeft;
      this.filterOperator.ClipDrawing = true;
      this.Children.Add((RadElement) this.filterOperator);
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewDataColumn && !(data is GridViewCheckBoxColumn) && !(data is GridViewComboBoxColumn))
        return context is GridFilterRowElement;
      return false;
    }

    public RadButtonElement FilterButton
    {
      get
      {
        return this.filterFunctionButton;
      }
    }

    public TextPrimitive FilterOperatorText
    {
      get
      {
        return this.filterOperator;
      }
    }

    public int ElementSpacing
    {
      get
      {
        return (int) this.GetValue(GridFilterCellElement.ElementSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridFilterCellElement.ElementSpacingProperty, (object) value);
      }
    }

    public bool IsFilterApplied
    {
      get
      {
        return (bool) this.GetValue(GridFilterCellElement.IsFilterAppliedProperty);
      }
    }

    public override bool IsEditable
    {
      get
      {
        FilterDescriptor descriptor = this.Descriptor;
        return this.DataColumnInfo.AllowFiltering && !(descriptor is CompositeFilterDescriptor) && (descriptor.Operator != FilterOperator.None && descriptor.Operator != FilterOperator.IsNull) && descriptor.Operator != FilterOperator.IsNotNull;
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
        return base.Value;
      }
      set
      {
        object result;
        RadDataConverter.Instance.TryParse((IDataConversionInfoProvider) this, value, out result);
        base.Value = result;
      }
    }

    protected FilterDescriptor Descriptor
    {
      get
      {
        if (this.FilteringRowInfo == null)
          return (FilterDescriptor) null;
        return this.FilteringRowInfo.GetFilterDescriptor(this.DataColumnInfo);
      }
    }

    protected GridViewFilteringRowInfo FilteringRowInfo
    {
      get
      {
        return this.RowInfo as GridViewFilteringRowInfo;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      SizeF availableSize1 = availableSize;
      this.FilterButton.Measure(availableSize1);
      float num = this.FilterButton.DesiredSize.Width + (float) this.FilterButton.Margin.Horizontal + (float) this.ElementSpacing;
      availableSize1.Width -= num;
      empty.Width += num;
      empty.Height = Math.Max(empty.Height, this.FilterButton.DesiredSize.Height);
      SizeF sizeF = SizeF.Empty;
      if (this.Editor != null)
      {
        this.Text = "";
      }
      else
      {
        this.Layout.Measure(availableSize1);
        sizeF = new SizeF(this.Layout.DesiredSize.Width, this.Layout.DesiredSize.Height);
      }
      foreach (RadElement child in this.Children)
      {
        if (child != this.FilterButton && child != this.FilterOperatorText)
        {
          child.Measure(availableSize1);
          sizeF.Width = Math.Max(sizeF.Width, child.DesiredSize.Width);
          sizeF.Height = Math.Max(sizeF.Height, child.DesiredSize.Height);
        }
      }
      sizeF.Width += (float) this.ElementSpacing;
      availableSize1.Width -= sizeF.Width;
      empty.Width += sizeF.Width;
      empty.Height = Math.Max(empty.Height, sizeF.Height);
      this.FilterOperatorText.Measure(availableSize1);
      empty.Width += this.FilterOperatorText.DesiredSize.Width;
      empty.Height = Math.Max(empty.Height, this.FilterOperatorText.DesiredSize.Height);
      Padding borderThickness = this.GetBorderThickness(true);
      empty.Height += (float) borderThickness.Vertical;
      SizeF size = this.TableElement.ViewElement.RowLayout.ArrangeCell(new RectangleF((PointF) Point.Empty, availableSize), (GridCellElement) this).Size;
      if (!float.IsInfinity(availableSize.Width))
        empty.Width = size.Width;
      if (!float.IsInfinity(availableSize.Height))
        empty.Height = size.Height;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      float width = this.FilterOperatorText.DesiredSize.Width;
      float num1 = (float) this.FilterButton.Margin.Horizontal + this.FilterButton.DesiredSize.Width;
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float num2 = clientRectangle.Width - num1 - (float) this.ElementSpacing;
      RadElement editorElement = (RadElement) this.GetEditorElement(this.Editor);
      RectangleF rectangleF = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
      RectangleF finalRect = new RectangleF(clientRectangle.X, clientRectangle.Y, num2, clientRectangle.Height);
      if (editorElement != null || this.Value != null && !string.IsNullOrEmpty(this.Value.ToString()))
      {
        if (this.filterOperator.Visibility != ElementVisibility.Collapsed)
        {
          float valueWidth = this.GetValueWidth(num2, clientRectangle);
          rectangleF = new RectangleF(num2 - valueWidth + (float) this.ElementSpacing, clientRectangle.Y, valueWidth, clientRectangle.Height);
          finalRect = new RectangleF(clientRectangle.X, clientRectangle.Y, rectangleF.X - clientRectangle.X, clientRectangle.Height);
          if ((double) finalRect.Width < 10.0)
            finalRect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
          else if ((double) finalRect.Width > (double) width)
          {
            float num3 = finalRect.Width - width;
            rectangleF.X -= num3;
            rectangleF.Width += num3;
            finalRect.Width = width;
          }
        }
        else
        {
          rectangleF = new RectangleF(clientRectangle.X, clientRectangle.Y, num2, clientRectangle.Height);
          finalRect = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
        }
      }
      if (this.RightToLeft)
      {
        rectangleF.X = clientRectangle.Width - num2;
        finalRect.Width = Math.Min(finalRect.Width, this.filterOperator.DesiredSize.Width);
        finalRect.X = clientRectangle.Width - finalRect.Width;
      }
      this.Layout.Arrange(rectangleF);
      foreach (RadElement child in this.Children)
      {
        if (this.filterFunctionButton == child)
          child.Arrange(clientRectangle);
        else if (this.filterOperator == child)
          child.Arrange(finalRect);
        else if (editorElement == child)
          this.ArrangeEditorElement(child, rectangleF, clientRectangle);
        else
          this.ArrangeElement(child, finalSize);
      }
      return finalSize;
    }

    protected virtual void ArrangeEditorElement(
      RadElement element,
      RectangleF editorRect,
      RectangleF clientRect)
    {
      float height = element.DesiredSize.Height;
      if (element.StretchVertically)
        height = clientRect.Height;
      if ((double) height == 0.0)
        height = editorRect.Height;
      editorRect.Height = Math.Min(editorRect.Height, height);
      editorRect.Y += (float) (((double) clientRect.Height - (double) editorRect.Height) / 2.0);
      element.Arrange(editorRect);
    }

    private float GetValueWidth(float cellWidth, RectangleF clientRect)
    {
      float num = this.Layout.DesiredSize.Width;
      if (this.Editor != null)
        num = Math.Max(num, 40f);
      return Math.Min(cellWidth, num);
    }

    protected override void OnCellFormatting(CellFormattingEventArgs e)
    {
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      if (e.Property != GridViewDataColumn.AllowFilteringProperty)
        return;
      this.UpdateItemsVisibility((bool) e.NewValue);
    }

    protected virtual void UpdateItemsVisibility(bool enabled)
    {
      this.UpdateFilterButtonVisibility(enabled);
      this.FilterOperatorText.Visibility = enabled ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.DrawText = enabled;
      this.filterOperator.Visibility = !enabled || !this.ViewTemplate.ShowFilterCellOperatorText ? ElementVisibility.Collapsed : ElementVisibility.Visible;
    }

    protected virtual void UpdateFilterButtonVisibility(bool enabled)
    {
      this.FilterButton.Visibility = enabled ? ElementVisibility.Visible : ElementVisibility.Hidden;
    }

    private void Editor_ValueChanged(object sender, EventArgs e)
    {
      this.FilteringRowInfo.SuspendUpdate();
      this.ViewTemplate.MasterTemplate.SynchronizationService.BeginDispatch();
      this.Value = this.Editor.Value;
      this.ViewTemplate.MasterTemplate.SynchronizationService.EndDispatch(true);
      this.FilteringRowInfo.ResumeUpdate();
    }

    private void FilterFunctionButton_Click(object sender, EventArgs e)
    {
      RadDropDownMenu filterMenu = this.CreateFilterMenu(this.DataColumnInfo.DataType);
      filterMenu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      ContextMenuOpeningEventArgs args = new ContextMenuOpeningEventArgs((IContextMenuProvider) this, filterMenu);
      this.GridViewElement.Template.EventDispatcher.RaiseEvent<ContextMenuOpeningEventArgs>(EventDispatcher.ContextMenuOpening, (object) this, args);
      if (args.Cancel || args.ContextMenu == null)
        return;
      Point point = args.ContextMenu.RightToLeft == RightToLeft.Yes ? new Point(this.filterFunctionButton.Size.Width, this.filterFunctionButton.Size.Height) : new Point(0, this.filterFunctionButton.Size.Height);
      args.ContextMenu.Show((RadItem) this.filterFunctionButton, point, RadDirection.Down);
      this.Focus();
    }

    private void FilterMenuItem_Click(object sender, EventArgs e)
    {
      RadFilterOperationMenuItem operationMenuItem = sender as RadFilterOperationMenuItem;
      if (operationMenuItem != null)
      {
        this.SetFilterOperator(operationMenuItem.Operator);
      }
      else
      {
        RadFilterComposeMenuItem menuItem = sender as RadFilterComposeMenuItem;
        if (menuItem == null)
          return;
        this.EditFilterDescriptor(menuItem);
      }
    }

    private void contextMenu_PopupOpening(object sender, CancelEventArgs args)
    {
      ((RadPopupControlBase) sender).PopupOpening -= new RadPopupOpeningEventHandler(this.contextMenu_PopupOpening);
      int num = (int) this.filterFunctionButton.SetValue(GridFilterButtonElement.IsFilterMenuShownProperty, (object) true);
    }

    private void contextMenu_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      ((RadPopupControlBase) sender).PopupClosed -= new RadPopupClosedEventHandler(this.contextMenu_PopupClosed);
      int num = (int) this.filterFunctionButton.SetValue(GridFilterButtonElement.IsFilterMenuShownProperty, (object) false);
    }

    public override void SetContent()
    {
      this.SetContentCore(this.Value);
      if (this.DataColumnInfo != null)
      {
        int num = (int) this.SetValue(GridFilterCellElement.IsFilterAppliedProperty, (object) (this.DataColumnInfo.FilterDescriptor != null));
      }
      this.InvalidateArrange();
    }

    protected override void SetContentCore(object value)
    {
      base.SetContentCore(value);
      this.OnViewCellFormatting(new CellFormattingEventArgs((GridCellElement) this));
    }

    protected override void UpdateInfoCore()
    {
      GridViewDataColumn columnInfo = this.ColumnInfo as GridViewDataColumn;
      this.UpdateItemsVisibility(columnInfo == null || columnInfo.AllowFiltering);
      base.UpdateInfoCore();
      this.SetSelectedFilterOperatorText();
    }

    protected virtual bool SetFilterOperator(FilterOperator filterOperator)
    {
      if (filterOperator == FilterOperator.None)
      {
        bool flag = this.RemoveFilterDescriptor();
        this.SetSelectedFilterOperatorText();
        return flag;
      }
      FilterDescriptor descriptor = this.Descriptor;
      if (descriptor == null || descriptor is CompositeFilterDescriptor)
      {
        if (descriptor is CompositeFilterDescriptor && !this.RemoveFilterDescriptor())
        {
          this.SetSelectedFilterOperatorText();
          return false;
        }
        if (this.FilteringRowInfo != null)
          this.FilteringRowInfo.CreateFilterDescriptor(this.DataColumnInfo, filterOperator);
        this.SetSelectedFilterOperatorText();
      }
      else
      {
        if (this.FilteringRowInfo != null)
          this.FilteringRowInfo.SuspendUpdate();
        if ((descriptor.Operator == FilterOperator.IsNull || descriptor.Operator == FilterOperator.IsNotNull) && (filterOperator != FilterOperator.IsNull && filterOperator != FilterOperator.IsNotNull))
          this.GridControl.FilterDescriptors.Remove(descriptor);
        descriptor.Operator = filterOperator;
        if (this.FilteringRowInfo != null)
          this.FilteringRowInfo.ResumeUpdate();
        bool flag1 = descriptor.Operator == FilterOperator.IsNull;
        bool flag2 = descriptor.Operator == FilterOperator.IsNotNull;
        if (descriptor.Value != null && !flag2 && !flag1 || (flag1 || flag2))
        {
          this.SetFilterDescriptor(descriptor);
          return true;
        }
        this.SetSelectedFilterOperatorText();
      }
      return true;
    }

    private void EditFilterDescriptor(RadFilterComposeMenuItem menuItem)
    {
      RadFilterComposeMenuItem filterComposeMenuItem = menuItem;
      this.clickedBetweenItem = false;
      if (menuItem.Text == LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionsBetween") || menuItem.Text == LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterFunctionNotBetween"))
        this.clickedBetweenItem = true;
      string themeName = this.GridControl.ThemeName;
      using (BaseCompositeFilterDialog compositeFilterForm = this.CreateCompositeFilterForm())
      {
        compositeFilterForm.ThemeName = themeName;
        compositeFilterForm.Initialize(this.DataColumnInfo, filterComposeMenuItem.FilterDescriptor, true);
        if (compositeFilterForm.ShowDialog() == DialogResult.Cancel)
          return;
        FilterDescriptor filterDescriptor = compositeFilterForm.FilterDescriptor;
        if (!GridFilterCellElement.ValidateUserFilter(filterDescriptor))
          return;
        this.SetFilterDescriptor(filterDescriptor);
      }
    }

    protected virtual BaseCompositeFilterDialog CreateCompositeFilterForm()
    {
      if (this.MasterTemplate != null)
      {
        GridViewCreateCompositeFilterDialogEventArgs args = new GridViewCreateCompositeFilterDialogEventArgs();
        args.Dialog = !this.clickedBetweenItem ? (BaseCompositeFilterDialog) new CompositeDataFilterForm() : (BaseCompositeFilterDialog) new CompositeFilterForm();
        this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCreateCompositeFilterDialogEventArgs>(EventDispatcher.CreateCompositeFilterDialog, (object) this, args);
        if (args.Dialog != null)
          return args.Dialog;
      }
      if (this.clickedBetweenItem)
        return (BaseCompositeFilterDialog) new CompositeFilterForm();
      return (BaseCompositeFilterDialog) new CompositeDataFilterForm();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static bool ValidateUserFilter(FilterDescriptor descriptor)
    {
      if (descriptor == null)
        return true;
      CompositeFilterDescriptor filterDescriptor1 = descriptor as CompositeFilterDescriptor;
      if (filterDescriptor1 == null)
        return descriptor.Value != null;
      if (filterDescriptor1.FilterDescriptors.Count == 1 && filterDescriptor1.NotOperator)
        return filterDescriptor1.FilterDescriptors[0].Value != null;
      bool flag = true;
      foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
      {
        CompositeFilterDescriptor filterDescriptor3 = filterDescriptor2 as CompositeFilterDescriptor;
        if (filterDescriptor3 != null)
        {
          if (!GridFilterCellElement.ValidateUserFilter((FilterDescriptor) filterDescriptor3))
          {
            flag = false;
            break;
          }
        }
        else if (!GridFilterCellElement.ValidateFilterDescriptor(filterDescriptor2))
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    private static bool ValidateFilterDescriptor(FilterDescriptor filterDescriptor)
    {
      bool flag = true;
      if (filterDescriptor.Operator != FilterOperator.IsNull && filterDescriptor.Operator != FilterOperator.IsNotNull)
        flag = filterDescriptor.Value != null;
      return flag;
    }

    protected virtual bool SetFilterDescriptor(FilterDescriptor descriptor)
    {
      if (this.FilteringRowInfo == null || !this.FilteringRowInfo.SetFilterDesriptor(this.DataColumnInfo, descriptor))
        return false;
      this.SetContent();
      this.SetSelectedFilterOperatorText();
      return true;
    }

    protected virtual bool RemoveFilterDescriptor()
    {
      return this.SetFilterDescriptor((FilterDescriptor) null);
    }

    public override void AddEditor(IInputEditor editor)
    {
      base.AddEditor(editor);
      editor.ValueChanged += new EventHandler(this.Editor_ValueChanged);
    }

    public override void RemoveEditor(IInputEditor editor)
    {
      editor.ValueChanged -= new EventHandler(this.Editor_ValueChanged);
      base.RemoveEditor(editor);
    }

    protected virtual RadDropDownMenu CreateFilterMenu(System.Type dataType)
    {
      FilterDescriptor descriptor = this.Descriptor;
      CompositeFilterDescriptor filterDescriptor = descriptor as CompositeFilterDescriptor;
      CompositeFilterDescriptor.DescriptorType descriptorType = CompositeFilterDescriptor.GetDescriptorType(filterDescriptor);
      RadDropDownMenu menu = new RadDropDownMenu();
      List<FilterOperationContext> filterOperations = FilterOperationContext.GetFilterOperations(dataType);
      bool flag = this.ColumnInfo is GridViewMultiComboBoxColumn;
      foreach (FilterOperationContext context in filterOperations)
      {
        if (!flag || context.Operator == FilterOperator.None || (context.Operator == FilterOperator.IsEqualTo || context.Operator == FilterOperator.IsNotEqualTo))
        {
          RadFilterOperationMenuItem operationMenuItem = new RadFilterOperationMenuItem(context);
          operationMenuItem.IsChecked = filterDescriptor == null && operationMenuItem.Operator == descriptor.Operator;
          operationMenuItem.Click += new EventHandler(this.FilterMenuItem_Click);
          menu.Items.Add((RadItem) operationMenuItem);
        }
      }
      if (GridViewHelper.IsNumeric(dataType) || (object) dataType == (object) typeof (DateTime))
      {
        RadFilterComposeMenuItem filterComposeMenuItem1 = new RadFilterComposeMenuItem("FilterFunctionsBetween");
        filterComposeMenuItem1.IsChecked = filterDescriptor != null && descriptorType == CompositeFilterDescriptor.DescriptorType.Between;
        filterComposeMenuItem1.FilterDescriptor = (FilterDescriptor) this.GetCompositeFilterDescriptor(CompositeFilterDescriptor.DescriptorType.Between, filterDescriptor, dataType);
        filterComposeMenuItem1.Click += new EventHandler(this.FilterMenuItem_Click);
        menu.Items.Add((RadItem) filterComposeMenuItem1);
        RadFilterComposeMenuItem filterComposeMenuItem2 = new RadFilterComposeMenuItem("FilterFunctionNotBetween");
        filterComposeMenuItem2.IsChecked = descriptorType == CompositeFilterDescriptor.DescriptorType.NotBetween;
        filterComposeMenuItem2.FilterDescriptor = (FilterDescriptor) this.GetCompositeFilterDescriptor(CompositeFilterDescriptor.DescriptorType.NotBetween, filterDescriptor, dataType);
        filterComposeMenuItem2.Click += new EventHandler(this.FilterMenuItem_Click);
        menu.Items.Add((RadItem) filterComposeMenuItem2);
      }
      if ((object) dataType != (object) typeof (Image))
      {
        RadFilterComposeMenuItem filterComposeMenuItem = new RadFilterComposeMenuItem("FilterFunctionsCustom");
        filterComposeMenuItem.FilterDescriptor = descriptor.Clone() as FilterDescriptor;
        filterComposeMenuItem.Click += new EventHandler(this.FilterMenuItem_Click);
        filterComposeMenuItem.IsChecked = filterDescriptor != null && descriptorType == CompositeFilterDescriptor.DescriptorType.Unknown;
        menu.Items.Add((RadItem) filterComposeMenuItem);
      }
      menu.PopupOpening += new RadPopupOpeningEventHandler(this.contextMenu_PopupOpening);
      menu.PopupClosed += new RadPopupClosedEventHandler(this.contextMenu_PopupClosed);
      GridViewContextMenuManager.UpdateMenuTheme((GridVisualElement) this.GridViewElement, menu);
      return menu;
    }

    private CompositeFilterDescriptor GetCompositeFilterDescriptor(
      CompositeFilterDescriptor.DescriptorType desiredType,
      CompositeFilterDescriptor currentDescriptor,
      System.Type dataType)
    {
      return currentDescriptor == null ? CompositeFilterDescriptor.CreateDescriptor(desiredType, this.ColumnInfo.Name, dataType, (object[]) null) : currentDescriptor.ConvertTo(desiredType, dataType);
    }

    protected virtual void SetSelectedFilterOperatorText()
    {
      if (this.ColumnInfo is GridViewCommandColumn)
        return;
      string localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterOperatorNoFilter");
      if (this.Descriptor != null)
      {
        RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
        if (this.Descriptor is CompositeFilterDescriptor)
        {
          localizedString = currentProvider.GetLocalizedString("FilterOperatorCustom");
        }
        else
        {
          switch (this.Descriptor.Operator)
          {
            case FilterOperator.IsLike:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorIsLike");
              break;
            case FilterOperator.IsNotLike:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorNotIsLike");
              break;
            case FilterOperator.IsLessThan:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorLessThan");
              break;
            case FilterOperator.IsLessThanOrEqualTo:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorLessThanOrEqualTo");
              break;
            case FilterOperator.IsEqualTo:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorEqualTo");
              break;
            case FilterOperator.IsNotEqualTo:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorNotEqualTo");
              break;
            case FilterOperator.IsGreaterThanOrEqualTo:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorGreaterThanOrEqualTo");
              break;
            case FilterOperator.IsGreaterThan:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorGreaterThan");
              break;
            case FilterOperator.StartsWith:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorStartsWith");
              break;
            case FilterOperator.EndsWith:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorEndsWith");
              break;
            case FilterOperator.Contains:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorContains");
              break;
            case FilterOperator.NotContains:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorDoesNotContain");
              break;
            case FilterOperator.IsNull:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorIsNull");
              break;
            case FilterOperator.IsNotNull:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorNotIsNull");
              break;
            case FilterOperator.IsContainedIn:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorIsContainedIn");
              break;
            case FilterOperator.IsNotContainedIn:
              localizedString = currentProvider.GetLocalizedString("FilterOperatorNotIsContainedIn");
              break;
            default:
              localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("FilterOperatorNoFilter");
              break;
          }
        }
      }
      this.filterOperator.Text = localizedString + ":";
    }

    internal override bool CanBestFit(BestFitColumnMode bestFitMode)
    {
      return (bestFitMode & BestFitColumnMode.FilterCells) > (BestFitColumnMode) 0;
    }
  }
}
