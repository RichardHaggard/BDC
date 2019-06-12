// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDataCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Localization;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class GridDataCellElement : GridVirtualizedCellElement, IEditableCell, IDataConversionInfoProvider, ITypeDescriptorContext, System.IServiceProvider
  {
    public static RadProperty IsFirstDataCellProperty = RadProperty.Register(nameof (IsFirstDataCell), typeof (bool), typeof (GridDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsLastDataCellProperty = RadProperty.Register(nameof (IsLastDataCell), typeof (bool), typeof (GridDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsErrorsProperty = RadProperty.Register(nameof (ContainsErrors), typeof (bool), typeof (GridDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsInEditModeProperty = RadProperty.Register(nameof (IsInEditMode), typeof (bool), typeof (GridDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private IInputEditor editor;
    private BaseFormattingObject parentFormattingObject;
    private bool cellStyleApplied;
    internal bool IsLeftMost;

    static GridDataCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridDataCellElementStateManager(), typeof (GridDataCellElement));
    }

    public GridDataCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "DataCell";
      this.AllowRowReorder = true;
      this.ClipDrawing = true;
    }

    public override void Detach()
    {
      int num1 = (int) this.ResetValue(GridDataCellElement.IsFirstDataCellProperty);
      int num2 = (int) this.ResetValue(GridDataCellElement.IsLastDataCellProperty);
      int num3 = (int) this.ResetValue(GridDataCellElement.ContainsErrorsProperty);
      int num4 = (int) this.ResetValue(GridDataCellElement.IsInEditModeProperty);
      this.ToolTipText = (string) null;
      base.Detach();
    }

    [Category("Appearance")]
    public virtual bool IsFirstDataCell
    {
      get
      {
        return (bool) this.GetValue(GridDataCellElement.IsFirstDataCellProperty);
      }
      private set
      {
        int num = (int) this.SetValue(GridDataCellElement.IsFirstDataCellProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool IsLastDataCell
    {
      get
      {
        return (bool) this.GetValue(GridDataCellElement.IsLastDataCellProperty);
      }
      private set
      {
        int num = (int) this.SetValue(GridDataCellElement.IsLastDataCellProperty, (object) value);
      }
    }

    [Category("Appearance")]
    public virtual bool IsInEditMode
    {
      get
      {
        return (bool) this.GetValue(GridDataCellElement.IsInEditModeProperty);
      }
    }

    public override object Value
    {
      get
      {
        if (this.RowElement == null)
          return (object) null;
        return this.RowInfo[this.ColumnInfo];
      }
      set
      {
        if (this.RowElement == null)
          return;
        object obj = RadDataConverter.Instance.Parse((IDataConversionInfoProvider) this, value);
        GridViewGroupRowInfo parent1 = this.RowInfo.Parent as GridViewGroupRowInfo;
        this.RowInfo[this.ColumnInfo] = obj;
        if (this.RowInfo == null)
          return;
        GridViewGroupRowInfo parent2 = this.RowInfo.Parent as GridViewGroupRowInfo;
        if (parent1 != null && parent2 != null && parent1 != parent2)
        {
          this.ViewTemplate.InvalidateGroupSummaryRows(parent1.Group, true);
          this.ViewTemplate.InvalidateGroupSummaryRows(parent2.Group, true);
        }
        int num = 0;
        if (this.ViewTemplate != null && this.ViewTemplate.DataSource != null)
          num = this.ViewTemplate.DataSource.GetHashCode();
        GridViewCellEventArgs args = new GridViewCellEventArgs(this.RowInfo, this.ColumnInfo, this.GridViewElement.ActiveEditor);
        this.ViewTemplate.EventDispatcher.RaiseEvent<GridViewCellEventArgs>(EventDispatcher.CellValueChanged, (object) this, args);
        if (this.ViewTemplate == null || (this.ViewTemplate.DataSource == null || num != this.ViewTemplate.DataSource.GetHashCode()) && !this.ViewTemplate.IsVirtualRows)
          return;
        this.SetContent();
        this.UpdateInfo();
      }
    }

    internal override bool CanBestFit(BestFitColumnMode bestFitMode)
    {
      return (bestFitMode & BestFitColumnMode.DisplayedDataCells) > (BestFitColumnMode) 0;
    }

    public override bool SupportsConditionalFormatting
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
        return (bool) this.GetValue(GridDataCellElement.ContainsErrorsProperty);
      }
    }

    public GridExpanderItem ExpanderItem
    {
      get
      {
        if (this.SelfReferenceLayout != null)
          return this.SelfReferenceLayout.ExpanderItem;
        return (GridExpanderItem) null;
      }
    }

    public SelfReferenceCellLayout SelfReferenceLayout
    {
      get
      {
        GridDataRowElement rowElement = this.RowElement as GridDataRowElement;
        if (rowElement != null && this.ColumnInfo.OwnerTemplate != null && (this.ColumnInfo.OwnerTemplate.MasterTemplate.SelfReferenceExpanderColumn != null && this.ColumnInfo.OwnerTemplate.MasterTemplate.SelfReferenceExpanderColumn == this.ColumnInfo || this.ColumnInfo.OwnerTemplate.MasterTemplate.SelfReferenceExpanderColumn == null && this.IsFirstDataCell))
          return rowElement.SelfReferenceLayout;
        return (SelfReferenceCellLayout) null;
      }
    }

    protected internal GridViewDataColumn DataColumnInfo
    {
      get
      {
        return (GridViewDataColumn) this.ColumnInfo;
      }
    }

    public override RadDropDownMenu MergeMenus(
      IContextMenuManager contextMenuManager,
      params object[] parameters)
    {
      if (this.ViewTemplate.AllowCellContextMenu)
        return base.MergeMenus(contextMenuManager, parameters);
      return (RadDropDownMenu) null;
    }

    protected internal override void ShowContextMenu()
    {
      if (!this.ViewTemplate.AllowCellContextMenu)
        return;
      base.ShowContextMenu();
    }

    protected override void CreateContextMenuItems(RadDropDownMenu menu)
    {
      this.CreateClipboardMenuItems(menu);
      this.CreateEditMenuItems(menu);
    }

    private void CreateClipboardMenuItems(RadDropDownMenu contextMenu)
    {
      if (this.GridViewElement.Template.ClipboardCopyMode != GridViewClipboardCopyMode.Disable)
      {
        RadMenuItem radMenuItem = (RadMenuItem) new CopyPasteMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CopyMenuItem"));
        radMenuItem.Click += new EventHandler(this.ItemCopy_Click);
        contextMenu.Items.Add((RadItem) radMenuItem);
      }
      if (!this.IsEditable || this.GridViewElement.Template.GridReadOnly || !this.ViewTemplate.AllowEditRow || this.ColumnInfo.ReadOnly)
        return;
      RadMenuItem radMenuItem1 = (RadMenuItem) new CopyPasteMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PasteMenuItem"));
      radMenuItem1.Click += new EventHandler(this.ItemPaste_Click);
      contextMenu.Items.Add((RadItem) radMenuItem1);
    }

    private void CreateEditMenuItems(RadDropDownMenu contextMenu)
    {
      if (!this.IsEditable || this.GridViewElement.Template.GridReadOnly || !this.ViewTemplate.AllowEditRow || this.ColumnInfo.ReadOnly || this.GridViewElement.BeginEditMode == RadGridViewBeginEditMode.BeginEditProgrammatically)
        return;
      contextMenu.Items.Add((RadItem) new RadMenuSeparatorItem());
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("EditMenuItem"));
      radMenuItem1.Click += new EventHandler(this.Item_Click);
      contextMenu.Items.Add((RadItem) radMenuItem1);
      RadMenuItem radMenuItem2 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ClearValueMenuItem"));
      radMenuItem2.Click += new EventHandler(this.ClearValue_Click);
      contextMenu.Items.Add((RadItem) radMenuItem2);
    }

    private void ClearValue_Click(object sender, EventArgs e)
    {
      GridViewRowInfo rowInfo = this.RowInfo;
      this.Value = (object) null;
      this.SetContent();
      this.Invalidate();
    }

    private void ItemPaste_Click(object sender, EventArgs e)
    {
      this.MasterTemplate.Paste();
    }

    private void ItemCopy_Click(object sender, EventArgs e)
    {
      this.MasterTemplate.BeginCellCopy();
      this.MasterTemplate.Copy();
      this.MasterTemplate.EndCellCopy();
    }

    private void Item_Click(object sender, EventArgs e)
    {
      this.IsCurrent = true;
      this.GridViewElement.EditorManager.BeginEdit();
    }

    public virtual bool IsEditable
    {
      get
      {
        return !this.MasterTemplate.GridReadOnly && !this.ViewTemplate.ReadOnly && !this.ColumnInfo.ReadOnly && ((!(this.RowInfo is GridViewDataRowInfo) || this.ViewTemplate.AllowEditRow) && (this.Enabled && !this.RowInfo.Cells[this.ColumnInfo.Name].ReadOnly));
      }
    }

    public virtual IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    public virtual void AddEditor(IInputEditor editor)
    {
      GridViewEditManager editorManager = this.GridViewElement.EditorManager;
      if (editor != null && this.editor != editor && !editorManager.IsPermanentEditor(editor.GetType()))
      {
        this.editor = editor;
        RadItem editorElement = this.GetEditorElement(this.editor);
        if (editorElement != null && !this.Children.Contains((RadElement) editorElement))
        {
          int num = (int) editorElement.SetDefaultValueOverride(VisualElement.ForeColorProperty, (object) Color.FromKnownColor(KnownColor.ControlText));
          this.Children.Add((RadElement) editorElement);
          BaseGridEditor baseGridEditor = editor as BaseGridEditor;
          if (baseGridEditor != null && baseGridEditor.ClearCellText)
            this.Text = string.Empty;
        }
      }
      int num1 = (int) this.SetValue(GridDataCellElement.IsInEditModeProperty, (object) true);
    }

    public virtual void RemoveEditor(IInputEditor editor)
    {
      GridViewEditManager gridViewEditManager = (GridViewEditManager) null;
      if (this.GridViewElement != null)
        gridViewEditManager = this.GridViewElement.EditorManager;
      if (gridViewEditManager == null || editor != null && !gridViewEditManager.IsPermanentEditor(editor.GetType()))
      {
        RadItem editorElement = this.GetEditorElement(editor);
        if (editorElement != null && this.Children.Contains((RadElement) editorElement))
          this.Children.Remove((RadElement) editorElement);
        this.editor = (IInputEditor) null;
        this.SetContent();
      }
      int num = (int) this.SetValue(GridDataCellElement.IsInEditModeProperty, (object) false);
    }

    public RadItem GetEditorElement(IInputEditor editor)
    {
      RadItem radItem = editor as RadItem;
      if (radItem == null)
      {
        BaseGridEditor baseGridEditor = editor as BaseGridEditor;
        if (baseGridEditor != null)
        {
          radItem = baseGridEditor.EditorElement as RadItem;
        }
        else
        {
          BaseInputEditor baseInputEditor = editor as BaseInputEditor;
          if (baseInputEditor != null)
            radItem = baseInputEditor.EditorElement as RadItem;
        }
      }
      return radItem;
    }

    System.Type IDataConversionInfoProvider.DataType
    {
      get
      {
        return this.DataColumnInfo?.DataType;
      }
      set
      {
        GridViewDataColumn dataColumnInfo = this.DataColumnInfo;
        if (dataColumnInfo == null)
          return;
        dataColumnInfo.DataType = value;
      }
    }

    TypeConverter IDataConversionInfoProvider.DataTypeConverter
    {
      get
      {
        return this.DataColumnInfo?.DataTypeConverter;
      }
      set
      {
        GridViewDataColumn dataColumnInfo = this.DataColumnInfo;
        if (dataColumnInfo == null)
          return;
        dataColumnInfo.DataTypeConverter = value;
      }
    }

    object IDataConversionInfoProvider.DataSourceNullValue
    {
      get
      {
        return this.DataColumnInfo?.DataSourceNullValue;
      }
      set
      {
        GridViewDataColumn dataColumnInfo = this.DataColumnInfo;
        if (dataColumnInfo == null)
          return;
        dataColumnInfo.DataSourceNullValue = value;
      }
    }

    object IDataConversionInfoProvider.NullValue
    {
      get
      {
        return this.DataColumnInfo?.NullValue;
      }
      set
      {
        GridViewDataColumn dataColumnInfo = this.DataColumnInfo;
        if (dataColumnInfo == null)
          return;
        dataColumnInfo.NullValue = value;
      }
    }

    CultureInfo IDataConversionInfoProvider.FormatInfo
    {
      get
      {
        return this.DataColumnInfo?.FormatInfo;
      }
      set
      {
        GridViewDataColumn dataColumnInfo = this.DataColumnInfo;
        if (dataColumnInfo == null)
          return;
        dataColumnInfo.FormatInfo = value;
      }
    }

    string IDataConversionInfoProvider.FormatString
    {
      get
      {
        return this.FormatString;
      }
      set
      {
        this.FormatString = value;
      }
    }

    protected override void UpdateInfoCore()
    {
      this.UpdateFormattingObject();
      this.UpdateParentFormattingObject();
      this.SetTextAlignment();
      this.IsFirstDataCell = this.ColumnInfo == this.TableElement.ViewElement.RowLayout.FirstDataColumn;
      this.IsLastDataCell = this.ColumnInfo == this.TableElement.ViewElement.RowLayout.LastDataColumn;
      GridViewCellInfo cell = this.RowInfo.Cells[this.ColumnInfo.Name];
      if (cell != null)
      {
        this.UpdateErrorInfo(cell);
        this.UpdateStyle(cell);
      }
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.CellSelect)
      {
        this.IsSelected = this.MasterTemplate.SelectedCells.IsSelected(this.RowInfo, this.ColumnInfo);
        this.RowElement.UpdateContainsCellsState();
      }
      else
      {
        this.IsSelected = false;
        this.RowElement.UpdateContainsCellsState();
      }
      this.UpdateSelfReferenceLayout();
      base.UpdateInfoCore();
      this.IsPinned = this.ColumnInfo.PinPosition != PinnedColumnPosition.None || this.RowInfo.PinPosition != PinnedRowPosition.None;
    }

    private void UpdateSelfReferenceLayout()
    {
      if (this.SelfReferenceLayout != null)
      {
        this.SelfReferenceLayout.CreateCellElements(this);
      }
      else
      {
        if (this.IsFirstDataCell)
          return;
        GridDataRowElement rowElement = this.RowElement as GridDataRowElement;
        if (rowElement == null)
          return;
        SelfReferenceCellLayout selfReferenceLayout = rowElement.SelfReferenceLayout;
        if (selfReferenceLayout == null)
          return;
        StackLayoutElement stackLayoutElement = selfReferenceLayout.StackLayoutElement;
        if (stackLayoutElement == null || stackLayoutElement.Parent != this)
          return;
        this.Children.Remove((RadElement) stackLayoutElement);
      }
    }

    protected override void OnViewCellFormatting(CellFormattingEventArgs e)
    {
      this.OnCellFormatting(e);
      base.OnViewCellFormatting(e);
    }

    protected virtual void OnCellFormatting(CellFormattingEventArgs e)
    {
      this.GridViewElement.OnCellFormatting((object) this, e);
    }

    protected virtual void UpdateErrorInfo(GridViewCellInfo cellInfo)
    {
      if (this.GridViewElement.ShowCellErrors)
      {
        string errorText = cellInfo.ErrorText;
        if (!string.IsNullOrEmpty(errorText))
        {
          this.ToolTipText = errorText;
          int num = (int) this.SetValue(GridDataCellElement.ContainsErrorsProperty, (object) true);
          return;
        }
      }
      if (!this.ContainsErrors)
        return;
      this.ToolTipText = string.Empty;
      int num1 = (int) this.SetValue(GridDataCellElement.ContainsErrorsProperty, (object) false);
    }

    protected virtual void UpdateStyle(GridViewCellInfo cellInfo)
    {
      if (cellInfo.HasStyle)
      {
        this.ForeColor = cellInfo.Style.ForeColor;
        if (cellInfo.Style.GetValueSource(GridViewCellStyle.FontProperty) == ValueSource.Local)
        {
          this.Font = cellInfo.Style.Font;
        }
        else
        {
          int num = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
        if (cellInfo.Style.CustomizeFill)
        {
          this.DrawFill = cellInfo.Style.DrawFill;
          this.BackColor = cellInfo.Style.BackColor;
          this.BackColor2 = cellInfo.Style.BackColor2;
          this.BackColor3 = cellInfo.Style.BackColor3;
          this.BackColor4 = cellInfo.Style.BackColor4;
          this.GradientStyle = cellInfo.Style.GradientStyle;
          this.GradientAngle = cellInfo.Style.GradientAngle;
          this.GradientPercentage = cellInfo.Style.GradientPercentage;
          this.GradientPercentage2 = cellInfo.Style.GradientPercentage2;
          this.NumberOfColors = cellInfo.Style.NumberOfColors;
        }
        else
          this.ResetFillProperties();
        if (cellInfo.Style.CustomizeBorder)
        {
          this.DrawBorder = cellInfo.Style.DrawBorder;
          this.BorderBoxStyle = cellInfo.Style.BorderBoxStyle;
          this.BorderWidth = cellInfo.Style.BorderWidth;
          this.BorderLeftWidth = cellInfo.Style.BorderLeftWidth;
          this.BorderRightWidth = cellInfo.Style.BorderRightWidth;
          this.BorderTopWidth = cellInfo.Style.BorderTopWidth;
          this.BorderBottomWidth = cellInfo.Style.BorderBottomWidth;
          this.BorderGradientAngle = cellInfo.Style.BorderGradientAngle;
          this.BorderGradientStyle = cellInfo.Style.BorderGradientStyle;
          this.BorderColor = cellInfo.Style.BorderColor;
          this.BorderColor2 = cellInfo.Style.BorderColor2;
          this.BorderColor3 = cellInfo.Style.BorderColor3;
          this.BorderColor4 = cellInfo.Style.BorderColor4;
          this.BorderInnerColor = cellInfo.Style.BorderInnerColor;
          this.BorderInnerColor2 = cellInfo.Style.BorderInnerColor2;
          this.BorderInnerColor3 = cellInfo.Style.BorderInnerColor3;
          this.BorderInnerColor4 = cellInfo.Style.BorderInnerColor4;
          this.BorderTopColor = cellInfo.Style.BorderTopColor;
          this.BorderBottomColor = cellInfo.Style.BorderBottomColor;
          this.BorderLeftColor = cellInfo.Style.BorderLeftColor;
          this.BorderRightColor = cellInfo.Style.BorderRightColor;
        }
        else
          this.ResetBorderProperties();
        this.cellStyleApplied = true;
      }
      else
      {
        if (!this.cellStyleApplied)
          return;
        int num1 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        this.ResetFillProperties();
        this.ResetBorderProperties();
      }
    }

    private void ResetFillProperties()
    {
      int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
      int num3 = (int) this.ResetValue(LightVisualElement.BackColor2Property, ValueResetFlags.Local);
      int num4 = (int) this.ResetValue(LightVisualElement.BackColor3Property, ValueResetFlags.Local);
      int num5 = (int) this.ResetValue(LightVisualElement.BackColor4Property, ValueResetFlags.Local);
      int num6 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
      int num7 = (int) this.ResetValue(LightVisualElement.GradientAngleProperty, ValueResetFlags.Local);
      int num8 = (int) this.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
      int num9 = (int) this.ResetValue(LightVisualElement.GradientPercentageProperty, ValueResetFlags.Local);
      int num10 = (int) this.ResetValue(LightVisualElement.GradientPercentage2Property, ValueResetFlags.Local);
    }

    private void ResetBorderProperties()
    {
      int num1 = (int) this.ResetValue(LightVisualElement.DrawBorderProperty, ValueResetFlags.Local);
      int num2 = (int) this.ResetValue(LightVisualElement.BorderBoxStyleProperty, ValueResetFlags.Local);
      int num3 = (int) this.ResetValue(LightVisualElement.BorderWidthProperty, ValueResetFlags.Local);
      int num4 = (int) this.ResetValue(LightVisualElement.BorderLeftWidthProperty, ValueResetFlags.Local);
      int num5 = (int) this.ResetValue(LightVisualElement.BorderRightWidthProperty, ValueResetFlags.Local);
      int num6 = (int) this.ResetValue(LightVisualElement.BorderTopWidthProperty, ValueResetFlags.Local);
      int num7 = (int) this.ResetValue(LightVisualElement.BorderBottomWidthProperty, ValueResetFlags.Local);
      int num8 = (int) this.ResetValue(LightVisualElement.BorderGradientAngleProperty, ValueResetFlags.Local);
      int num9 = (int) this.ResetValue(LightVisualElement.BorderGradientStyleProperty, ValueResetFlags.Local);
      int num10 = (int) this.ResetValue(LightVisualElement.BorderColorProperty, ValueResetFlags.Local);
      int num11 = (int) this.ResetValue(LightVisualElement.BorderColor2Property, ValueResetFlags.Local);
      int num12 = (int) this.ResetValue(LightVisualElement.BorderColor3Property, ValueResetFlags.Local);
      int num13 = (int) this.ResetValue(LightVisualElement.BorderColor4Property, ValueResetFlags.Local);
      int num14 = (int) this.ResetValue(LightVisualElement.BorderInnerColorProperty, ValueResetFlags.Local);
      int num15 = (int) this.ResetValue(LightVisualElement.BorderInnerColor2Property, ValueResetFlags.Local);
      int num16 = (int) this.ResetValue(LightVisualElement.BorderInnerColor3Property, ValueResetFlags.Local);
      int num17 = (int) this.ResetValue(LightVisualElement.BorderInnerColor4Property, ValueResetFlags.Local);
      int num18 = (int) this.ResetValue(LightVisualElement.BorderTopColorProperty, ValueResetFlags.Local);
      int num19 = (int) this.ResetValue(LightVisualElement.BorderBottomColorProperty, ValueResetFlags.Local);
      int num20 = (int) this.ResetValue(LightVisualElement.BorderLeftColorProperty, ValueResetFlags.Local);
      int num21 = (int) this.ResetValue(LightVisualElement.BorderRightColorProperty, ValueResetFlags.Local);
    }

    protected override RectangleF GetClipRect()
    {
      if (this.SelfReferenceLayout == null)
        return base.GetClipRect();
      RectangleF bounds = (RectangleF) this.Bounds;
      bounds.X -= this.BorderTopWidth + this.BorderWidth;
      bounds.Height += (float) ((double) this.BorderTopWidth + (double) this.BorderBottomWidth + 2.0 * (double) this.BorderWidth);
      return bounds;
    }

    protected virtual void SetTextAlignment()
    {
      if (this.FormattingObject != null && this.FormattingObject.IsValueSet("TextAlignment") || this.parentFormattingObject != null && this.parentFormattingObject.IsValueSet("RowTextAlignment") || this.ColumnInfo == null)
        return;
      this.TextAlignment = this.ColumnInfo.TextAlignment;
    }

    protected override void NotifyFormatChanged(BaseFormattingObject oldFormat)
    {
      base.NotifyFormatChanged(oldFormat);
      BaseFormattingObject formattingObject = this.FormattingObject;
      if (formattingObject != null)
      {
        if (formattingObject.IsValueSet("CellBackColor"))
        {
          this.DrawFill = true;
          this.GradientStyle = GradientStyles.Solid;
          this.BackColor = formattingObject.CellBackColor;
        }
        else
        {
          int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
          int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
          int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        }
        if (formattingObject.IsValueSet("CellForeColor"))
          this.ForeColor = formattingObject.CellForeColor;
        else if (this.parentFormattingObject == null || !this.parentFormattingObject.IsValueSet("RowForeColor"))
        {
          int num4 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        }
        if (formattingObject.IsValueSet("TextAlignment"))
          this.TextAlignment = formattingObject.TextAlignment;
        else if (this.parentFormattingObject == null || !this.parentFormattingObject.IsValueSet("RowTextAlignment"))
        {
          int num5 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
        }
        if (formattingObject.IsValueSet("CellFont"))
        {
          this.Font = formattingObject.CellFont;
        }
        else
        {
          if (this.parentFormattingObject != null && this.parentFormattingObject.IsValueSet("RowFont"))
            return;
          int num1 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
      }
      else
      {
        if (oldFormat == null)
          return;
        int num1 = (int) this.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
        int num2 = (int) this.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
        int num3 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        int num4 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        int num5 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
        int num6 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      }
    }

    private void UpdateFormattingObject()
    {
      BaseFormattingObject formattingObject1 = (BaseFormattingObject) null;
      foreach (BaseFormattingObject formattingObject2 in (Collection<BaseFormattingObject>) this.ColumnInfo.ConditionalFormattingObjectList)
      {
        if (!formattingObject2.ApplyOnSelectedRows && this.RowInfo.IsSelected)
          this.UnsetFormattingObjectProperties(formattingObject2, formattingObject1);
        else if (formattingObject2.Evaluate(this.RowInfo, this.ColumnInfo))
        {
          if (formattingObject1 == null)
            formattingObject1 = new BaseFormattingObject();
          this.SetFormattingObjectProperties(formattingObject2, formattingObject1);
        }
      }
      this.SetFormattingObject(formattingObject1);
    }

    private void UpdateParentFormattingObject()
    {
      GridDataRowElement rowElement = this.RowElement as GridDataRowElement;
      BaseFormattingObject formattingObject1 = (BaseFormattingObject) null;
      if (rowElement != null)
      {
        formattingObject1 = rowElement.FormattingObject;
        if (formattingObject1 != null && !formattingObject1.ApplyToRow)
          formattingObject1 = (BaseFormattingObject) null;
      }
      BaseFormattingObject formattingObject2 = this.parentFormattingObject;
      this.parentFormattingObject = formattingObject1;
      this.SuspendPropertyNotifications();
      this.ApplyParentFormatting(formattingObject2);
      this.ResumePropertyNotifications();
    }

    private void ApplyParentFormatting(BaseFormattingObject oldParentFormat)
    {
      if (this.parentFormattingObject == null)
      {
        if (oldParentFormat != null)
        {
          if (this.FormattingObject == null || !this.FormattingObject.IsValueSet("CellForeColor"))
          {
            int num1 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
          }
          if (this.FormattingObject == null || !this.FormattingObject.IsValueSet("TextAlignment"))
          {
            int num2 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
          }
        }
        if (this.FormattingObject != null && this.FormattingObject.IsValueSet("CellFont"))
          return;
        int num = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      }
      else
      {
        if (this.parentFormattingObject.IsValueSet("RowForeColor"))
        {
          if (this.FormattingObject == null || !this.FormattingObject.IsValueSet("CellForeColor"))
            this.ForeColor = this.parentFormattingObject.RowForeColor;
        }
        else if (this.FormattingObject == null || !this.FormattingObject.IsValueSet("CellForeColor"))
        {
          int num1 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        }
        if (this.parentFormattingObject.IsValueSet("RowTextAlignment"))
        {
          if (this.FormattingObject == null || !this.FormattingObject.IsValueSet("TextAlignment"))
            this.TextAlignment = this.parentFormattingObject.RowTextAlignment;
        }
        else if (this.FormattingObject == null || !this.FormattingObject.IsValueSet("TextAlignment"))
        {
          int num2 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
        }
        if (this.parentFormattingObject.IsValueSet("RowFont"))
        {
          if (this.FormattingObject != null && this.FormattingObject.IsValueSet("CellFont"))
            return;
          this.Font = this.parentFormattingObject.RowFont;
        }
        else
        {
          if (this.FormattingObject != null && this.FormattingObject.IsValueSet("CellFont"))
            return;
          int num3 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
      }
    }

    protected override string ApplyFormatString(object value)
    {
      return RadDataConverter.Instance.Format(value, typeof (string), (IDataConversionInfoProvider) this) as string;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF desiredSize = this.Layout.DesiredSize;
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.GridViewElement.AutoSizeRows && this.Editor != null && !this.GridViewElement.EditorManager.IsPermanentEditor(this.Editor.GetType()))
      {
        Padding borderThickness = this.GetBorderThickness(false);
        sizeF.Height = Math.Max(desiredSize.Height + (float) borderThickness.Vertical + (float) this.Padding.Vertical, sizeF.Height);
      }
      if (this.GridViewElement.AutoSizeRows && this.RowInfo != null)
      {
        if (this.RowInfo.MaxHeight >= 0)
          sizeF.Height = Math.Min(sizeF.Height, (float) this.RowInfo.MaxHeight);
        if (this.RowInfo.MinHeight >= 0)
          sizeF.Height = Math.Max(sizeF.Height, (float) this.RowInfo.MinHeight);
      }
      return sizeF;
    }

    protected override SizeF CalculateDesiredSize(
      SizeF availableSize,
      SizeF desiredSize,
      SizeF elementsDesiredSize)
    {
      if (this.SelfReferenceLayout == null)
        return base.CalculateDesiredSize(availableSize, desiredSize, elementsDesiredSize);
      desiredSize.Width += elementsDesiredSize.Width;
      if ((double) elementsDesiredSize.Height > (double) desiredSize.Height)
        desiredSize.Height = elementsDesiredSize.Height;
      desiredSize.Width = Math.Min(desiredSize.Width, availableSize.Width);
      desiredSize.Height = Math.Min(desiredSize.Height, availableSize.Height);
      return desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      SelfReferenceCellLayout selfReferenceLayout = this.SelfReferenceLayout;
      GridDataRowElement rowElement = this.RowElement as GridDataRowElement;
      if (this.ViewTemplate != null && this.ViewTemplate.IsSelfReference && (rowElement != null && this.ColumnInfo.OwnerTemplate != null))
      {
        if (selfReferenceLayout != null)
          rowElement.SelfReferenceLayout.StackLayoutElement.Visibility = ElementVisibility.Visible;
        else
          rowElement.SelfReferenceLayout.StackLayoutElement.Visibility = ElementVisibility.Collapsed;
      }
      if (selfReferenceLayout != null)
        this.ArrangeSelfReferencePanel(finalSize, ref clientRectangle);
      if (this.IsLeftMost && selfReferenceLayout == null && (rowElement != null && rowElement.SelfReferenceLayout != null))
      {
        int width = (int) rowElement.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
        if (this.RightToLeft)
        {
          clientRectangle.Width -= (float) width;
        }
        else
        {
          clientRectangle.X += (float) width;
          clientRectangle.Width -= (float) width;
        }
      }
      this.Layout.Arrange(clientRectangle);
      foreach (RadElement child in this.Children)
      {
        if (selfReferenceLayout == null || selfReferenceLayout.StackLayoutElement != child)
          this.ArrangeElement(child, finalSize, clientRectangle);
      }
      if (this.editor != null)
        this.ArrangeEditorElement(finalSize, clientRectangle);
      return finalSize;
    }

    protected virtual void ArrangeSelfReferencePanel(SizeF finalSize, ref RectangleF clientRect)
    {
      RadElement stackLayoutElement = (RadElement) this.SelfReferenceLayout.StackLayoutElement;
      int width = (int) this.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
      RectangleF rectangleF = clientRect;
      rectangleF.Width = (float) width;
      if (stackLayoutElement.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
      {
        rectangleF.Location = (PointF) (this.RightToLeft ? new Point((int) ((double) clientRect.Right - (double) width), 0) : Point.Empty);
        rectangleF.Height = finalSize.Height;
        stackLayoutElement.Arrange(rectangleF);
      }
      else
      {
        if (this.RightToLeft)
          rectangleF.X = clientRect.Right - (float) width;
        this.ArrangeElement(stackLayoutElement, finalSize, rectangleF);
      }
      if (this.RightToLeft)
      {
        clientRect.Width -= (float) width;
      }
      else
      {
        clientRect.X += (float) width;
        clientRect.Width -= (float) width;
      }
    }

    protected virtual void ArrangeEditorElement(SizeF finalSize, RectangleF clientRect)
    {
      RadElement editorElement = (RadElement) this.GetEditorElement(this.editor);
      float height1 = editorElement.DesiredSize.Height;
      if (editorElement.StretchVertically)
        height1 = clientRect.Height;
      float height2 = Math.Min(height1, finalSize.Height);
      if (this.ViewTemplate.ViewDefinition is HtmlViewDefinition && this.ControlBoundingRectangle.X < 0)
        clientRect.X += (float) -this.ControlBoundingRectangle.X;
      RectangleF finalRect = new RectangleF(clientRect.X, clientRect.Y + (float) (((double) clientRect.Height - (double) height2) / 2.0), clientRect.Width, height2);
      editorElement.Arrange(finalRect);
    }

    protected override void BindColumnProperties()
    {
      base.BindColumnProperties();
      PropertyBindingOptions options = PropertyBindingOptions.OneWay;
      int num = (int) this.BindProperty(GridCellElement.FormatStringProperty, (RadObject) this.ColumnInfo, GridViewDataColumn.FormatStringProperty, options);
    }

    protected override void UnbindColumnProperties()
    {
      int num = (int) this.UnbindProperty(GridCellElement.FormatStringProperty);
      base.UnbindColumnProperties();
    }

    IContainer ITypeDescriptorContext.Container
    {
      get
      {
        if (this.ElementTree == null)
          return (IContainer) null;
        IComponent control = (IComponent) this.ElementTree.Control;
        if (control != null)
        {
          ISite site = control.Site;
          if (site != null)
            return site.Container;
        }
        return (IContainer) null;
      }
    }

    object ITypeDescriptorContext.Instance
    {
      get
      {
        return (object) this;
      }
    }

    void ITypeDescriptorContext.OnComponentChanged()
    {
    }

    bool ITypeDescriptorContext.OnComponentChanging()
    {
      return true;
    }

    PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
    {
      get
      {
        return (PropertyDescriptor) null;
      }
    }

    object System.IServiceProvider.GetService(System.Type serviceType)
    {
      if ((object) serviceType == (object) typeof (GridDataCellElement))
        return (object) this;
      return (object) null;
    }
  }
}
