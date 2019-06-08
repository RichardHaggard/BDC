// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridFilterCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
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
  public class VirtualGridFilterCellElement : VirtualGridCellElement
  {
    public static RadProperty IsFilterAppliedProperty = RadProperty.Register(nameof (IsFilterApplied), typeof (bool), typeof (VirtualGridFilterCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementSpacingProperty = RadProperty.Register(nameof (ElementSpacing), typeof (int), typeof (VirtualGridFilterCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private const float EditorMinWidth = 40f;
    private const float OperatorMinWidth = 10f;
    private RadButtonElement filterFunctionButton;
    private TextPrimitive filterOperatorText;

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
        return this.filterOperatorText;
      }
    }

    public int ElementSpacing
    {
      get
      {
        return (int) this.GetValue(VirtualGridFilterCellElement.ElementSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(VirtualGridFilterCellElement.ElementSpacingProperty, (object) value);
      }
    }

    public bool IsFilterApplied
    {
      get
      {
        return (bool) this.GetValue(VirtualGridFilterCellElement.IsFilterAppliedProperty);
      }
    }

    public override bool CanEdit
    {
      get
      {
        if (this.Descriptor == null)
          return true;
        if (this.Descriptor.Operator != FilterOperator.IsNotNull)
          return this.Descriptor.Operator != FilterOperator.IsNull;
        return false;
      }
    }

    protected FilterDescriptor Descriptor
    {
      get
      {
        if (this.ViewInfo == null)
          return (FilterDescriptor) null;
        int index = this.ViewInfo.FilterDescriptors.IndexOf(this.FieldName);
        if (index >= 0)
          return this.ViewInfo.FilterDescriptors[index];
        return (FilterDescriptor) null;
      }
    }

    private FilterOperator SelectedFilterOperator
    {
      get
      {
        if (this.Descriptor != null)
          return this.Descriptor.Operator;
        if (this.ViewInfo.FilterRowValues.ContainsKey(this.ColumnIndex))
          return this.ViewInfo.FilterRowValues[this.ColumnIndex];
        System.Type columnDataType = this.ViewInfo.GetColumnDataType(this.ColumnIndex);
        if ((object) columnDataType != null)
          return GridViewHelper.GetDefaultFilterOperator(columnDataType);
        return FilterOperator.Contains;
      }
    }

    protected override void CreateChildElements()
    {
      this.filterFunctionButton = (RadButtonElement) new VirtualGridFilterButtonElement();
      this.filterFunctionButton.NotifyParentOnMouseInput = false;
      this.filterFunctionButton.StretchHorizontally = false;
      this.filterFunctionButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.filterFunctionButton.Alignment = ContentAlignment.MiddleRight;
      this.filterFunctionButton.Class = "FilterFunctionButton";
      this.filterFunctionButton.Image = (Image) Resources.FilteringIcon;
      this.filterFunctionButton.Click += new EventHandler(this.FilterFunctionButton_Click);
      this.filterFunctionButton.ZIndex = 100;
      this.filterFunctionButton.GetValue(VirtualGridFilterCellElement.IsFilterAppliedProperty);
      this.Children.Add((RadElement) this.filterFunctionButton);
      this.filterOperatorText = new TextPrimitive();
      this.filterOperatorText.BypassLayoutPolicies = true;
      this.filterOperatorText.Alignment = ContentAlignment.MiddleLeft;
      this.filterOperatorText.TextAlignment = ContentAlignment.MiddleLeft;
      this.filterOperatorText.ClipDrawing = true;
      this.Children.Add((RadElement) this.filterOperatorText);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
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

    public override bool IsCompatible(int data, object context)
    {
      if (data >= 0)
        return context is VirtualGridFilterRowElement;
      return false;
    }

    public override bool IsInResizeLocation(Point point)
    {
      return false;
    }

    protected virtual RadDropDownMenu CreateFilterMenu()
    {
      System.Type dataType = this.ViewInfo.GetColumnDataType(this.ColumnIndex);
      if ((object) dataType == null)
        dataType = typeof (string);
      CompositeFilterDescriptor descriptor = this.Descriptor as CompositeFilterDescriptor;
      int descriptorType = (int) CompositeFilterDescriptor.GetDescriptorType(descriptor);
      RadDropDownMenu radDropDownMenu = new RadDropDownMenu();
      foreach (FilterOperationContext filterOperation in FilterOperationContext.GetFilterOperations(dataType))
      {
        RadFilterOperationMenuItem operationMenuItem = new RadFilterOperationMenuItem(filterOperation);
        operationMenuItem.IsChecked = descriptor == null && operationMenuItem.Operator == this.SelectedFilterOperator;
        operationMenuItem.Click += new EventHandler(this.FilterMenuItem_Click);
        operationMenuItem.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString(filterOperation.id);
        radDropDownMenu.Items.Add((RadItem) operationMenuItem);
      }
      radDropDownMenu.PopupOpening += new RadPopupOpeningEventHandler(this.contextMenu_PopupOpening);
      radDropDownMenu.PopupClosed += new RadPopupClosedEventHandler(this.contextMenu_PopupClosed);
      this.InitializeMenuItemsText();
      return radDropDownMenu;
    }

    public void InitializeMenuItemsText()
    {
    }

    protected virtual bool RemoveFilterDescriptor()
    {
      return this.SetFilterDescriptor((FilterDescriptor) null);
    }

    protected virtual bool SetFilterDescriptor(FilterDescriptor descriptor)
    {
      if (this.ViewInfo == null || this.Descriptor == descriptor)
        return false;
      if (this.ViewInfo.FilterDescriptors.Contains(descriptor))
        this.ViewInfo.FilterDescriptors.Remove(descriptor);
      if (descriptor != null)
        this.ViewInfo.FilterDescriptors.BeginUpdate();
      this.ViewInfo.FilterDescriptors.Remove(this.Descriptor);
      if (descriptor != null)
        this.ViewInfo.FilterDescriptors.EndUpdate(false);
      if (descriptor == null)
      {
        if (this.ViewInfo.FilterRowValues.ContainsKey(this.ColumnIndex))
          this.ViewInfo.FilterRowValues.Remove(this.ColumnIndex);
        this.Value = (object) null;
      }
      else
        this.ViewInfo.FilterDescriptors.Add(descriptor);
      this.SetSelectedFilterOperatorText();
      return true;
    }

    protected virtual bool SetFilterOperator(FilterOperator filterOperator)
    {
      if (filterOperator == FilterOperator.None)
      {
        bool flag = this.RemoveFilterDescriptor();
        this.SetSelectedFilterOperatorText();
        return flag;
      }
      FilterDescriptor descriptor1 = this.Descriptor;
      FilterDescriptor descriptor2 = descriptor1 != null ? descriptor1.Clone() as FilterDescriptor : (FilterDescriptor) null;
      if (descriptor2 == null || descriptor2 is CompositeFilterDescriptor)
      {
        if (descriptor2 is CompositeFilterDescriptor && !this.RemoveFilterDescriptor())
        {
          this.SetSelectedFilterOperatorText();
          return false;
        }
        if (filterOperator == FilterOperator.Contains)
        {
          if (this.ViewInfo.FilterRowValues.ContainsKey(this.ColumnIndex))
            this.ViewInfo.FilterRowValues.Remove(this.ColumnIndex);
        }
        else
          this.ViewInfo.FilterRowValues[this.ColumnIndex] = filterOperator;
        this.SetSelectedFilterOperatorText();
        this.SetFilterDescriptor(new FilterDescriptor(this.FieldName, filterOperator, (object) null));
        return true;
      }
      descriptor2.Operator = filterOperator;
      if (filterOperator == FilterOperator.IsNull || filterOperator == FilterOperator.IsNotNull)
        this.TableElement.GridElement.CancelEdit();
      this.SetFilterDescriptor(descriptor2);
      return true;
    }

    protected virtual void SetSelectedFilterOperatorText()
    {
      RadVirtualGridLocalizationProvider currentProvider = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider;
      string localizedString1 = currentProvider.GetLocalizedString("FilterOperatorNoFilter");
      if (this.Descriptor is CompositeFilterDescriptor)
        localizedString1 = currentProvider.GetLocalizedString("FilterOperatorCustom");
      string localizedString2;
      switch (this.SelectedFilterOperator)
      {
        case FilterOperator.IsLike:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorIsLike");
          break;
        case FilterOperator.IsNotLike:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorNotIsLike");
          break;
        case FilterOperator.IsLessThan:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorLessThan");
          break;
        case FilterOperator.IsLessThanOrEqualTo:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorLessThanOrEqualTo");
          break;
        case FilterOperator.IsEqualTo:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorEqualTo");
          break;
        case FilterOperator.IsNotEqualTo:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorNotEqualTo");
          break;
        case FilterOperator.IsGreaterThanOrEqualTo:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorGreaterThanOrEqualTo");
          break;
        case FilterOperator.IsGreaterThan:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorGreaterThan");
          break;
        case FilterOperator.StartsWith:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorStartsWith");
          break;
        case FilterOperator.EndsWith:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorEndsWith");
          break;
        case FilterOperator.Contains:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorContains");
          break;
        case FilterOperator.NotContains:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorDoesNotContain");
          break;
        case FilterOperator.IsNull:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorIsNull");
          break;
        case FilterOperator.IsNotNull:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorNotIsNull");
          break;
        case FilterOperator.IsContainedIn:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorIsContainedIn");
          break;
        case FilterOperator.IsNotContainedIn:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorNotIsContainedIn");
          break;
        default:
          localizedString2 = currentProvider.GetLocalizedString("FilterOperatorNoFilter");
          break;
      }
      this.filterOperatorText.Text = localizedString2 + ":";
    }

    protected override void UpdateInfo(VirtualGridCellValueNeededEventArgs args)
    {
      this.FieldName = args.FieldName;
      this.SetSelectedFilterOperatorText();
      int num = (int) this.SetValue(VirtualGridFilterCellElement.IsFilterAppliedProperty, (object) (this.Descriptor != null));
      if (this.Descriptor != null)
        this.Text = Convert.ToString(this.Descriptor.Value);
      else
        this.Text = string.Empty;
    }

    private CompositeFilterDescriptor GetCompositeFilterDescriptor(
      CompositeFilterDescriptor.DescriptorType desiredType,
      CompositeFilterDescriptor currentDescriptor,
      System.Type dataType)
    {
      return currentDescriptor == null ? CompositeFilterDescriptor.CreateDescriptor(desiredType, this.FieldName, dataType, (object[]) null) : currentDescriptor.ConvertTo(desiredType, dataType);
    }

    private float GetValueWidth(float cellWidth, RectangleF clientRect)
    {
      float num = this.Layout.DesiredSize.Width;
      if (this.Editor != null)
        num = Math.Max(num, 40f);
      return Math.Min(cellWidth, num);
    }

    private void Editor_ValueChanged(object sender, EventArgs e)
    {
      this.Value = this.Editor.Value;
      FilterDescriptor descriptor = this.Descriptor;
      if (descriptor == null)
      {
        this.SetFilterDescriptor(new FilterDescriptor(this.FieldName, this.SelectedFilterOperator, this.Value));
      }
      else
      {
        descriptor.Value = this.Editor.Value;
        this.TableElement.GridElement.OnFilterDescriptorsChanged(this.ViewInfo);
      }
    }

    private void FilterFunctionButton_Click(object sender, EventArgs e)
    {
      RadDropDownMenu filterMenu = this.CreateFilterMenu();
      filterMenu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      bool flag = filterMenu.RightToLeft == RightToLeft.Yes;
      Point point = flag ? new Point(this.filterFunctionButton.Size.Width, this.filterFunctionButton.Size.Height) : new Point(0, this.filterFunctionButton.Size.Height);
      VirtualGridContextMenuOpeningEventArgs args = new VirtualGridContextMenuOpeningEventArgs(this.RowIndex, this.ColumnIndex, this.ViewInfo, filterMenu);
      if (!this.TableElement.GridElement.OnContextMenuOpening(args) || args.ContextMenu == null || args.ContextMenu.Items.Count <= 0)
        return;
      filterMenu.HorizontalPopupAlignment = flag ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
      filterMenu.ThemeName = this.ElementTree.ThemeName;
      filterMenu.Show((RadItem) this.filterFunctionButton, point, RadDirection.Down);
      this.Focus();
    }

    private void contextMenu_PopupOpening(object sender, CancelEventArgs args)
    {
      ((RadPopupControlBase) sender).PopupOpening -= new RadPopupOpeningEventHandler(this.contextMenu_PopupOpening);
      int num = (int) this.filterFunctionButton.SetValue(VirtualGridFilterButtonElement.IsFilterMenuShownProperty, (object) true);
    }

    private void contextMenu_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      ((RadPopupControlBase) sender).PopupClosed -= new RadPopupClosedEventHandler(this.contextMenu_PopupClosed);
      int num = (int) this.filterFunctionButton.SetValue(VirtualGridFilterButtonElement.IsFilterMenuShownProperty, (object) false);
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
        RadFilterComposeMenuItem filterComposeMenuItem = sender as RadFilterComposeMenuItem;
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
      SizeF sizeF1 = SizeF.Empty;
      if (this.Editor != null)
      {
        this.Text = "";
      }
      else
      {
        this.Layout.Measure(availableSize1);
        sizeF1 = new SizeF(this.Layout.DesiredSize.Width, this.Layout.DesiredSize.Height);
      }
      foreach (RadElement child in this.Children)
      {
        if (child != this.FilterButton && child != this.FilterOperatorText)
        {
          child.Measure(availableSize1);
          sizeF1.Width = Math.Max(sizeF1.Width, child.DesiredSize.Width);
          sizeF1.Height = Math.Max(sizeF1.Height, child.DesiredSize.Height);
        }
      }
      sizeF1.Width += (float) this.ElementSpacing;
      availableSize1.Width -= sizeF1.Width;
      empty.Width += sizeF1.Width;
      empty.Height = Math.Max(empty.Height, sizeF1.Height);
      this.FilterOperatorText.Measure(availableSize1);
      empty.Width += this.FilterOperatorText.DesiredSize.Width;
      empty.Height = Math.Max(empty.Height, this.FilterOperatorText.DesiredSize.Height);
      Padding borderThickness = this.GetBorderThickness(true);
      empty.Height += (float) borderThickness.Vertical;
      SizeF sizeF2 = base.MeasureOverride(availableSize);
      if (!float.IsInfinity(availableSize.Width))
        empty.Width = sizeF2.Width;
      if (!float.IsInfinity(availableSize.Height))
        empty.Height = sizeF2.Height;
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
      if (editorElement != null || this.Descriptor != null && this.Descriptor.Value != null)
      {
        if (this.filterOperatorText.Visibility != ElementVisibility.Collapsed)
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
        finalRect.Width = Math.Min(finalRect.Width, this.filterOperatorText.DesiredSize.Width);
        finalRect.X = clientRectangle.Width - finalRect.Width;
      }
      this.Layout.Arrange(rectangleF);
      foreach (RadElement child in this.Children)
      {
        if (this.filterFunctionButton == child)
          child.Arrange(clientRectangle);
        else if (this.filterOperatorText == child)
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
  }
}
