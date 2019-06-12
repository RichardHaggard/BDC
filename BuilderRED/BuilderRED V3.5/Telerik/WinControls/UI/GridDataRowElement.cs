// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDataRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GridDataRowElement : GridVirtualizedRowElement
  {
    public static RadProperty IsExpandedProperty = RadProperty.Register("IsExpanded", typeof (bool), typeof (GridDataRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsErrorsProperty = RadProperty.Register(nameof (ContainsErrors), typeof (bool), typeof (GridDataRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private bool isAlternatingRowColorSet;
    private SelfReferenceCellLayout selfReferenceLayout;
    private double zoomedRowHeight;

    public virtual SelfReferenceCellLayout SelfReferenceLayout
    {
      get
      {
        if (!(this.RowInfo is GridViewHierarchyRowInfo))
          return (SelfReferenceCellLayout) null;
        if (this.ViewTemplate != null && this.ViewTemplate.IsSelfReference && this.selfReferenceLayout == null)
          this.selfReferenceLayout = new SelfReferenceCellLayout((GridRowElement) this);
        return this.selfReferenceLayout;
      }
    }

    protected override bool CanApplyAlternatingColor
    {
      get
      {
        return true;
      }
    }

    public bool ContainsErrors
    {
      get
      {
        return (bool) this.GetValue(GridDataRowElement.ContainsErrorsProperty);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrag = true;
      this.AllowDrop = true;
    }

    public override void UpdateInfo()
    {
      if (this.RowInfo != null)
      {
        this.UpdateFormattingObject();
        if (this.GridViewElement.ShowRowErrors)
        {
          int num1 = (int) this.SetValue(GridDataRowElement.ContainsErrorsProperty, (object) !string.IsNullOrEmpty(this.RowInfo.ErrorText));
        }
        else
        {
          int num2 = (int) this.SetValue(GridDataRowElement.ContainsErrorsProperty, (object) false);
        }
      }
      base.UpdateInfo();
      if (!this.IsInValidState(true))
        return;
      this.GridViewElement.CallRowFormatting((object) this, new RowFormattingEventArgs((GridRowElement) this));
    }

    public override bool IsCompatible(GridViewRowInfo data, object context)
    {
      return data is GridViewDataRowInfo;
    }

    public override Type GetCellType(GridViewColumn column)
    {
      GridViewIndentColumn viewIndentColumn = column as GridViewIndentColumn;
      if (viewIndentColumn != null && viewIndentColumn.IndentLevel == -1)
        return typeof (GridGroupExpanderCellElement);
      return base.GetCellType(column);
    }

    public override GridCellElement CreateCell(GridViewColumn column)
    {
      GridCellElement cell = base.CreateCell(column);
      if (cell is GridGroupExpanderCellElement)
        cell.ThemeRole = "HierarchyExpanderCell";
      return cell;
    }

    public override void Detach()
    {
      this.DetachSelfReferenceLayout();
      int num = (int) this.ResetValue(RadElement.ContainsMouseProperty, ValueResetFlags.Local);
      base.Detach();
    }

    private void DetachSelfReferenceLayout()
    {
      if (this.selfReferenceLayout == null)
        return;
      this.selfReferenceLayout.DetachCellElements();
    }

    public override RadDropDownMenu MergeMenus(
      RadDropDownMenu contextMenu,
      params object[] parameters)
    {
      if (contextMenu == null)
        return (RadDropDownMenu) null;
      bool flag1 = this.GridViewElement.Template.GridReadOnly || this.ViewTemplate.ReadOnly;
      bool flag2 = true;
      for (int index = 0; index < contextMenu.Items.Count; ++index)
      {
        if (contextMenu.Items[index] is CopyPasteMenuItem)
        {
          flag2 = false;
          break;
        }
      }
      if (flag2)
      {
        if (!flag1)
        {
          if (contextMenu.Items.Count > 0)
            contextMenu.Items.Add((RadItem) new RadMenuSeparatorItem());
          if (this.ViewTemplate.MasterTemplate.ClipboardCutMode != GridViewClipboardCutMode.Disable && this.ViewTemplate.AllowEditRow)
          {
            RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CutMenuItem"));
            radMenuItem.Click += new EventHandler(this.cutItem_Click);
            contextMenu.Items.Add((RadItem) radMenuItem);
          }
          if (this.ViewTemplate.MasterTemplate.ClipboardCopyMode != GridViewClipboardCopyMode.Disable)
          {
            RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CopyMenuItem"));
            radMenuItem.Click += new EventHandler(this.copyItem_Click);
            contextMenu.Items.Add((RadItem) radMenuItem);
          }
          if (this.ViewTemplate.MasterTemplate.ClipboardPasteMode != GridViewClipboardPasteMode.Disable && this.ViewTemplate.AllowEditRow)
          {
            RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PasteMenuItem"));
            radMenuItem.Click += new EventHandler(this.pasteItem_Click);
            contextMenu.Items.Add((RadItem) radMenuItem);
          }
        }
        else if (this.ViewTemplate.MasterTemplate.ClipboardCopyMode != GridViewClipboardCopyMode.Disable)
        {
          RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CopyMenuItem"));
          radMenuItem.Click += new EventHandler(this.copyItem_Click);
          contextMenu.Items.Add((RadItem) radMenuItem);
        }
      }
      if (this.ViewTemplate.AllowDeleteRow && !flag1)
      {
        contextMenu.Items.Add((RadItem) new RadMenuSeparatorItem());
        RadMenuItem radMenuItem = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("DeleteRowMenuItem"));
        radMenuItem.Click += new EventHandler(this.ItemDelete_Click);
        contextMenu.Items.Add((RadItem) radMenuItem);
      }
      return contextMenu;
    }

    private void pasteItem_Click(object sender, EventArgs e)
    {
      this.ViewTemplate.MasterTemplate.Paste();
    }

    private void copyItem_Click(object sender, EventArgs e)
    {
      this.ViewTemplate.MasterTemplate.BeginRowCopy();
      this.ViewTemplate.MasterTemplate.Copy();
      this.ViewTemplate.MasterTemplate.EndRowCopy();
    }

    private void cutItem_Click(object sender, EventArgs e)
    {
      MasterGridViewTemplate masterTemplate = this.ViewTemplate.MasterTemplate;
      masterTemplate.BeginRowCopy();
      this.ViewTemplate.MasterTemplate.Cut();
      masterTemplate.EndRowCopy();
    }

    private void ItemDelete_Click(object sender, EventArgs e)
    {
      this.GridViewElement.Navigator.DeleteSelectedRows();
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      if (!this.GridViewElement.EditorManager.IsInEditMode && this.RowInfo.ViewTemplate.AllowRowReorder && !this.RowInfo.ViewTemplate.ListSource.IsDataBound)
        return !(this.RowInfo.ViewTemplate.HierarchyDataProvider is GridViewEventDataProvider);
      return false;
    }

    protected override object GetDragContextCore()
    {
      return (object) this.Data;
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      if (dragObject.GetDataContext() is GroupFieldDragDropContext)
        return true;
      GridViewRowInfo dataContext = dragObject.GetDataContext() as GridViewRowInfo;
      if (dataContext == null)
        return base.ProcessDragOver(currentMouseLocation, dragObject);
      return this.RowInfo.ViewTemplate.SortDescriptors.Count <= 0 && this.RowInfo.ViewTemplate.GroupDescriptors.Count <= 0 && dataContext.ViewTemplate == this.Data.ViewTemplate;
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      GroupFieldDragDropContext dataContext1 = dragObject.GetDataContext() as GroupFieldDragDropContext;
      if (dataContext1 != null)
      {
        dataContext1.ViewTemplate.GroupDescriptors.Remove(dataContext1.GroupDescription);
      }
      else
      {
        GridViewRowInfo dataContext2 = dragObject.GetDataContext() as GridViewRowInfo;
        if (dataContext2 != null)
        {
          if (this.RowInfo.ViewTemplate.SortDescriptors.Count > 0 || this.RowInfo.ViewTemplate.GroupDescriptors.Count > 0)
            return;
          bool isDroppedAtLeft = RadGridViewDragDropService.IsDroppedAtTop(dropLocation, this.Size.Height);
          GridViewTemplate viewTemplate = this.Data.ViewTemplate;
          int targetIndex = viewTemplate.Rows.IndexOf(this.Data);
          int draggedItemIndex = viewTemplate.Rows.IndexOf(dataContext2);
          RadGridViewDragDropService.CalculateTargetIndex(isDroppedAtLeft, viewTemplate.RowCount, ref targetIndex, ref draggedItemIndex);
          viewTemplate.Rows.Move(draggedItemIndex, targetIndex);
        }
        else
          base.ProcessDragDrop(dropLocation, dragObject);
      }
    }

    protected override Image GetDragHintCore()
    {
      Bitmap bitmap = base.GetDragHintCore() as Bitmap;
      int width = bitmap.Width;
      if (width > 150)
      {
        width = 150;
        bitmap = ImageHelper.Crop(bitmap, new Rectangle(Point.Empty, new Size(width, bitmap.Height)));
      }
      ImageHelper.ApplyMask(bitmap, (Brush) new LinearGradientBrush(new Rectangle(0, 0, width, bitmap.Height), Color.White, Color.Black, LinearGradientMode.Horizontal));
      return (Image) bitmap;
    }

    public override bool SupportsConditionalFormatting
    {
      get
      {
        return true;
      }
    }

    protected override void NotifyFormatChanged(BaseFormattingObject oldFormat)
    {
      base.NotifyFormatChanged(oldFormat);
      BaseFormattingObject formattingObject = this.FormattingObject;
      if (formattingObject != null && formattingObject.ApplyToRow)
      {
        if (formattingObject.IsValueSet("RowBackColor"))
        {
          this.DrawFill = true;
          this.GradientStyle = GradientStyles.Solid;
          this.BackColor = formattingObject.RowBackColor;
        }
        else
        {
          int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
          int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
          int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        }
        if (formattingObject.IsValueSet("RowFont"))
        {
          this.Font = formattingObject.RowFont;
        }
        else
        {
          int num = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
      }
      else
      {
        if (oldFormat == null)
          return;
        int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        int num4 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        this.ResetAlternatingRowColor();
      }
    }

    private void UpdateFormattingObject()
    {
      BaseFormattingObject formattingObject1 = (BaseFormattingObject) null;
      if (this.SupportsConditionalFormatting)
      {
        foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.ViewTemplate.Columns)
        {
          GridViewDataColumn gridViewDataColumn = column as GridViewDataColumn;
          if (gridViewDataColumn != null)
          {
            foreach (BaseFormattingObject formattingObject2 in (Collection<BaseFormattingObject>) gridViewDataColumn.ConditionalFormattingObjectList)
            {
              if (!formattingObject2.ApplyOnSelectedRows && this.RowInfo.IsSelected)
                this.UnsetFormattingObjectProperties(formattingObject2, formattingObject1);
              else if (formattingObject2.ApplyToRow && formattingObject2.Evaluate(this.RowInfo, (GridViewColumn) gridViewDataColumn))
              {
                if (formattingObject1 == null)
                  formattingObject1 = new BaseFormattingObject();
                this.SetFormattingObjectProperties(formattingObject2, formattingObject1);
              }
            }
          }
        }
      }
      this.SetFormattingObject(formattingObject1);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == GridDataRowElement.IsExpandedProperty)
      {
        this.RowInfo.IsExpanded = (bool) this.GetValue(GridDataRowElement.IsExpandedProperty);
      }
      else
      {
        if (e.Property != RadItem.VisualStateProperty || this.ViewTemplate == null)
          return;
        this.UpdateAlternatingRowColor();
      }
    }

    protected override void OnRowPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnRowPropertyChanged(e);
      if (e.PropertyName == "IsExpanded")
      {
        int num = (int) this.SetValue(GridDataRowElement.IsExpandedProperty, (object) this.RowInfo.IsExpanded);
      }
      this.UpdateInfo();
    }

    protected override void DisposeManagedResources()
    {
      if (this.selfReferenceLayout != null)
        this.selfReferenceLayout.Dispose();
      base.DisposeManagedResources();
    }

    protected override void OnZoomGesture(ZoomGestureEventArgs args)
    {
      base.OnZoomGesture(args);
      if (this.RowInfo == null)
        return;
      if (args.IsBegin)
        this.zoomedRowHeight = (double) this.RowInfo.GetActualHeight((IGridView) this.TableElement);
      this.zoomedRowHeight *= args.ZoomFactor;
      this.RowInfo.Height = (int) this.zoomedRowHeight;
      args.Handled = true;
    }

    protected override void ApplyCustomFormatting()
    {
      this.UpdateAlternatingRowColor();
      base.ApplyCustomFormatting();
    }

    protected virtual void UpdateAlternatingRowColor()
    {
      bool flag1 = false;
      if (this.ViewTemplate.EnableAlternatingRowColor)
      {
        if (this.ViewTemplate.IsSelfReference)
        {
          GridTraverser gridTraverser = new GridTraverser(this.ViewInfo, GridTraverser.TraversalModes.ScrollableRows);
          bool flag2 = false;
          while (gridTraverser.MoveNext())
          {
            if (gridTraverser.Current == this.RowInfo)
            {
              this.IsOdd = flag2;
              break;
            }
            flag2 = !flag2;
          }
        }
        else
          this.IsOdd = this.RowInfo.Index % 2 == 1;
        this.isAlternatingRowColorSet = this.BackColor == this.TableElement.AlternatingRowColor;
        if (this.CanApplyAlternatingColor && this.IsOdd && this.MasterTemplate != null && (this.MasterTemplate.SelectionMode == GridViewSelectionMode.CellSelect || !this.IsCurrent && !this.IsSelected))
          flag1 = true;
      }
      else
        this.IsOdd = false;
      if (this.isAlternatingRowColorSet == flag1)
        return;
      this.isAlternatingRowColorSet = flag1;
      this.NotifyAlternatingRowChange();
    }

    protected internal override ValueUpdateResult ResetValueCore(
      RadPropertyValue propVal,
      ValueResetFlags flags)
    {
      ValueUpdateResult valueUpdateResult = base.ResetValueCore(propVal, flags);
      if (propVal.Property == VisualElement.BackColorProperty && flags >= ValueResetFlags.Local)
        this.UpdateAlternatingRowColor();
      return valueUpdateResult;
    }

    protected void ResetAlternatingRowColor()
    {
      this.isAlternatingRowColorSet = false;
      this.ResetAlternatingRowProperties(this.MasterTemplate.SelectionMode == GridViewSelectionMode.CellSelect);
    }

    private void NotifyAlternatingRowChange()
    {
      if (this.FormattingObject != null && this.FormattingObject.ApplyToRow && this.FormattingObject.IsValueSet("RowBackColor"))
        return;
      bool isCellSelectionMode = this.MasterTemplate.SelectionMode == GridViewSelectionMode.CellSelect;
      if (this.isAlternatingRowColorSet && (isCellSelectionMode || !this.IsCurrent && !this.IsSelected))
        this.SetAlternatingRowProperties(isCellSelectionMode);
      else
        this.ResetAlternatingRowProperties(isCellSelectionMode);
    }

    private IEnumerable<string> GetThemeOverrideAlternatingRowStates(
      bool isCellSelectionMode)
    {
      List<string> stringList = new List<string>();
      stringList.Add("");
      if (isCellSelectionMode)
      {
        stringList.Add("ContainsCurrentCell");
        stringList.Add("ContainsSelectedCells");
        stringList.Add("ContainsCurrentCell.ContainsSelectedCells");
      }
      stringList.Add("Disabled");
      return (IEnumerable<string>) stringList;
    }

    private void SetAlternatingRowProperties(bool isCellSelectionMode)
    {
      IEnumerable<string> alternatingRowStates = this.GetThemeOverrideAlternatingRowStates(isCellSelectionMode);
      this.SuspendApplyOfThemeSettings();
      foreach (string state in alternatingRowStates)
      {
        this.SetThemeValueOverride(LightVisualElement.DrawFillProperty, (object) true, state);
        this.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, state);
        this.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.TableElement.AlternatingRowColor, state);
      }
      this.ResumeApplyOfThemeSettings();
    }

    private void ResetAlternatingRowProperties(bool isCellSelectionMode)
    {
      IEnumerable<string> alternatingRowStates = this.GetThemeOverrideAlternatingRowStates(isCellSelectionMode);
      this.SuspendApplyOfThemeSettings();
      foreach (string state in alternatingRowStates)
      {
        this.ResetThemeValueOverride(LightVisualElement.DrawFillProperty, state);
        this.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty, state);
        this.ResetThemeValueOverride(VisualElement.BackColorProperty, state);
      }
      this.ResumeApplyOfThemeSettings();
    }
  }
}
