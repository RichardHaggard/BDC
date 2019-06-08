// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMultiColumnComboBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadMultiColumnComboBoxElement : BaseComboBoxElement, IGridViewEventListener
  {
    private bool integralHeight = true;
    private BestFitColumnMode autoSizeDropDownColumnMode = BestFitColumnMode.AllCells;
    private int savedRowsHeight = -1;
    private int savedColumnsWidth = -1;
    private bool clearTextOnValidation = true;
    private const int DefaultDropDownWidth = -1;
    private BindingMemberInfo displayMember;
    private BindingMemberInfo valueMember;
    private bool autoSizeDropDownToBestFit;
    private bool initializing;
    private bool usedInRadGridView;
    private bool bestFitColumns;
    private bool adjustComboBoxSize;
    private bool autoSizeDropDownHeight;
    private bool bestFitAllRows;
    private GridViewRowInfo currentRow;
    private PopupEditorState currentState;
    private Size adjustedSize;
    private bool isValueChanging;
    private int update;
    private bool clearingFilters;
    private ElementVisibility vscrollbarVisibility;
    private GridViewRowInfo oldRow;

    static RadMultiColumnComboBoxElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadTextBoxElementStateManager(), typeof (RadMultiColumnComboBoxElement));
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      string text = this.TextBoxElement.Text;
      this.TextBoxElement.SuspendPropertyNotifications();
      this.TextBoxElement.TextBoxItem.SuspendPropertyNotifications();
      this.TextBoxElement.TextBoxItem.HostedControl.Text = string.Empty;
      this.TextBoxElement.TextBoxItem.HostedControl.Text = text;
      this.TextBoxElement.ResumePropertyNotifications();
      this.TextBoxElement.TextBoxItem.ResumePropertyNotifications();
      this.TextBoxElement.ResumePropertyNotifications();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ArrowButton.MouseUp += new MouseEventHandler(this.ArrowButton_Click);
    }

    protected override void DisposeManagedResources()
    {
      if (this.ArrowButton != null)
        this.ArrowButton.MouseUp -= new MouseEventHandler(this.ArrowButton_Click);
      base.DisposeManagedResources();
      if (!this.usedInRadGridView || this.PopupForm == null || this.PopupForm.IsDisposed)
        return;
      this.PopupForm.Dispose();
    }

    protected override RadPopupControlBase CreatePopupForm()
    {
      MultiColumnComboPopupForm columnComboPopupForm = new MultiColumnComboPopupForm((PopupEditorBaseElement) this);
      columnComboPopupForm.EditorControl.Focusable = false;
      columnComboPopupForm.MinimumSize = this.DropDownMaxSize;
      columnComboPopupForm.MaximumSize = this.DropDownMaxSize;
      columnComboPopupForm.Height = this.DropDownHeight;
      columnComboPopupForm.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      columnComboPopupForm.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.Smooth;
      columnComboPopupForm.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.Inherit;
      this.WirePopupFormEvents((RadPopupControlBase) columnComboPopupForm);
      return (RadPopupControlBase) columnComboPopupForm;
    }

    protected override void WirePopupFormEvents(RadPopupControlBase popup)
    {
      MultiColumnComboPopupForm columnComboPopupForm = popup as MultiColumnComboPopupForm;
      columnComboPopupForm.EditorControl.CurrentRowChanging += new CurrentRowChangingEventHandler(this.OnGrid_CurrentRowChanging);
      columnComboPopupForm.EditorControl.CurrentRowChanged += new CurrentRowChangedEventHandler(this.OnGrid_CurrentRowChanged);
      columnComboPopupForm.EditorControl.RowsChanged += new GridViewCollectionChangedEventHandler(this.OnGrid_RowsChanged);
      columnComboPopupForm.EditorControl.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    protected override void UnwirePopupFormEvents(RadPopupControlBase popup)
    {
      MultiColumnComboPopupForm columnComboPopupForm = popup as MultiColumnComboPopupForm;
      columnComboPopupForm.EditorControl.CurrentRowChanging -= new CurrentRowChangingEventHandler(this.OnGrid_CurrentRowChanging);
      columnComboPopupForm.EditorControl.CurrentRowChanged -= new CurrentRowChangedEventHandler(this.OnGrid_CurrentRowChanged);
      columnComboPopupForm.EditorControl.MasterTemplate.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      columnComboPopupForm.EditorControl.RowsChanged -= new GridViewCollectionChangedEventHandler(this.OnGrid_RowsChanged);
      base.UnwirePopupFormEvents(popup);
    }

    public override void Initialize(object value)
    {
      this.BeginUpdate();
      base.Initialize(value);
      this.EndUpdate();
    }

    internal GridViewDataColumn DisplayColumn
    {
      get
      {
        string fieldName = this.DisplayMember;
        if (string.IsNullOrEmpty(fieldName))
        {
          for (int index = 0; index < this.MultiColumnPopupForm.EditorControl.Columns.Count; ++index)
          {
            if ((object) this.MultiColumnPopupForm.EditorControl.Columns[index].DataType == (object) typeof (string))
            {
              fieldName = this.MultiColumnPopupForm.EditorControl.Columns[index].FieldName;
              break;
            }
          }
          if (this.MultiColumnPopupForm.EditorControl.Columns.Count > 0 && string.IsNullOrEmpty(fieldName))
            fieldName = this.MultiColumnPopupForm.EditorControl.Columns[0].FieldName;
        }
        this.DisplayMember = fieldName;
        GridViewDataColumn[] columnByFieldName = this.MultiColumnPopupForm.EditorControl.Columns.GetColumnByFieldName(fieldName);
        if (columnByFieldName.Length > 0)
          return columnByFieldName[0];
        return (GridViewDataColumn) null;
      }
    }

    internal GridViewDataColumn ValueColumn
    {
      get
      {
        string fieldName = this.ValueMember;
        if (string.IsNullOrEmpty(fieldName))
        {
          for (int index = 0; index < this.MultiColumnPopupForm.EditorControl.Columns.Count; ++index)
          {
            if ((object) this.MultiColumnPopupForm.EditorControl.Columns[index].DataType == (object) typeof (string))
            {
              fieldName = this.MultiColumnPopupForm.EditorControl.Columns[index].FieldName;
              break;
            }
          }
          if (string.IsNullOrEmpty(fieldName))
            fieldName = this.MultiColumnPopupForm.EditorControl.Columns[0].FieldName;
        }
        this.ValueMember = fieldName;
        GridViewDataColumn[] columnByFieldName = this.MultiColumnPopupForm.EditorControl.Columns.GetColumnByFieldName(fieldName);
        if (columnByFieldName.Length > 0)
          return columnByFieldName[0];
        return (GridViewDataColumn) null;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Specifies the mode for the Text clearing if the text does not match the text in the DisplayMembar column.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool ClearTextOnValidation
    {
      get
      {
        return this.clearTextOnValidation;
      }
      set
      {
        this.clearTextOnValidation = value;
      }
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    [DefaultValue(AutoCompleteMode.None)]
    [Description("Specifies the mode for the automatic completion feature used in the ComboBox and TextBox controls.")]
    public override AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return base.AutoCompleteMode;
      }
      set
      {
        base.AutoCompleteMode = value;
        if ((value & AutoCompleteMode.Suggest) == AutoCompleteMode.Suggest)
          this.AutoFilter = true;
        else
          this.AutoFilter = false;
      }
    }

    public PopupEditorState CurrentState
    {
      get
      {
        return this.currentState;
      }
    }

    public RadGridView EditorControl
    {
      get
      {
        if (this.MultiColumnPopupForm != null)
          return this.MultiColumnPopupForm.EditorControl;
        return (RadGridView) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridViewColumnCollection Columns
    {
      get
      {
        return this.EditorControl.Columns;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets a collection that contains all the rows in the RadGridView.")]
    public GridViewRowCollection Rows
    {
      get
      {
        return this.EditorControl.Rows;
      }
    }

    [RadDefaultValue("AutoFilter", typeof (MultiColumnComboPopupForm))]
    public virtual bool AutoFilter
    {
      get
      {
        return this.MultiColumnPopupForm.AutoFilter;
      }
      set
      {
        this.MultiColumnPopupForm.AutoFilter = value;
      }
    }

    [DefaultValue(false)]
    [Description("Determines whether the drop-down portion of the control will be auto-sized to best fit all columns. The DropDownWidth property must be set to its default value (-1) to allow this setting to work properly.")]
    public bool AutoSizeDropDownToBestFit
    {
      get
      {
        return this.autoSizeDropDownToBestFit;
      }
      set
      {
        this.autoSizeDropDownToBestFit = value;
        if (!value)
          return;
        this.savedColumnsWidth = -1;
      }
    }

    [DefaultValue(BestFitColumnMode.AllCells)]
    public virtual BestFitColumnMode AutoSizeDropDownColumnMode
    {
      get
      {
        return this.autoSizeDropDownColumnMode;
      }
      set
      {
        this.autoSizeDropDownColumnMode = value;
        if (value == BestFitColumnMode.AllCells)
          return;
        this.bestFitColumns = true;
      }
    }

    [Description("Determines whether the drop-down height will be auto-sized when filtering is applied.")]
    [DefaultValue(false)]
    public bool AutoSizeDropDownHeight
    {
      get
      {
        return this.autoSizeDropDownHeight;
      }
      set
      {
        this.autoSizeDropDownHeight = value;
      }
    }

    public MultiColumnComboPopupForm MultiColumnPopupForm
    {
      get
      {
        return this.PopupForm as MultiColumnComboPopupForm;
      }
    }

    public override object Value
    {
      get
      {
        if (this.ValueMember != string.Empty)
          return base.Value;
        if (this.SelectedItem != null)
        {
          GridViewRowInfo selectedItem = this.SelectedItem as GridViewRowInfo;
          if (selectedItem != null)
            return selectedItem.Cells[this.DisplayColumn.Name].Value;
        }
        return (object) null;
      }
      set
      {
        base.Value = value;
      }
    }

    [RadDescription("IntegralHeight", typeof (BaseComboBoxElement))]
    [RadDefaultValue("IntegralHeight", typeof (BaseComboBoxElement))]
    [Category("Behavior")]
    public override bool IntegralHeight
    {
      get
      {
        return this.integralHeight;
      }
      set
      {
        this.integralHeight = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override RadItemCollection Items
    {
      get
      {
        return (RadItemCollection) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    [RadDescription("SelectedItem", typeof (BaseComboBoxElement))]
    [Browsable(false)]
    public override object SelectedItem
    {
      get
      {
        return (object) this.EditorControl.CurrentRow;
      }
      set
      {
        if (!(value is GridViewRowInfo) && value != null)
          return;
        this.EditorControl.CurrentRow = value as GridViewRowInfo;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadDescription("SelectedIndex", typeof (BaseComboBoxElement))]
    [Category("Behavior")]
    public override int SelectedIndex
    {
      get
      {
        GridViewDataRowInfo currentRow = this.EditorControl.CurrentRow as GridViewDataRowInfo;
        int num = -1;
        if (currentRow != null)
          num = this.EditorControl.Rows.IndexOf((GridViewRowInfo) currentRow);
        return num;
      }
      set
      {
        GridViewRowInfo gridViewRowInfo = (GridViewRowInfo) null;
        if (value > -1)
          gridViewRowInfo = this.EditorControl.Rows[value];
        this.EditorControl.CurrentRow = gridViewRowInfo;
      }
    }

    [Browsable(true)]
    [DefaultValue(SizingMode.None)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, vertical or a combination of them.")]
    public override SizingMode DropDownSizingMode
    {
      get
      {
        return this.MultiColumnPopupForm.SizingMode;
      }
      set
      {
        this.MultiColumnPopupForm.SizingMode = value;
      }
    }

    [Description("Gets or sets a value indicating whether RadScrollViewer uses UI virtualization.")]
    [Category("Behavior")]
    [Browsable(true)]
    [RadDefaultValue("Virtualized", typeof (BaseComboBoxElement))]
    public override bool Virtualized
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [RadDefaultValue("DataSource", typeof (GridViewTemplate))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof (IListSource))]
    [RadDescription("DataSource", typeof (BaseComboBoxElement))]
    [Category("Data")]
    public override object DataSource
    {
      get
      {
        return this.EditorControl.DataSource;
      }
      set
      {
        this.EditorControl.DataSource = value;
      }
    }

    [RadDefaultValue("DisplayMember", typeof (BaseComboBoxElement))]
    [Category("Data")]
    [RadDescription("DisplayMember", typeof (BaseComboBoxElement))]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public override string DisplayMember
    {
      get
      {
        return this.displayMember.BindingMember;
      }
      set
      {
        if (!(this.displayMember.BindingMember != value))
          return;
        BindingMemberInfo displayMember = this.displayMember;
        try
        {
          this.displayMember = new BindingMemberInfo(value);
          this.RefreshItems(this.DataSource, this.displayMember);
        }
        catch
        {
          this.displayMember = displayMember;
        }
      }
    }

    [RadDescription("ValueMember", typeof (BaseComboBoxElement))]
    [RadDefaultValue("ValueMember", typeof (BaseComboBoxElement))]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    public override string ValueMember
    {
      get
      {
        return this.valueMember.BindingMember;
      }
      set
      {
        if (!(this.valueMember.BindingMember != value))
          return;
        BindingMemberInfo valueMember = this.valueMember;
        try
        {
          this.valueMember = new BindingMemberInfo(value);
          this.RefreshItems(this.DataSource, this.valueMember);
        }
        catch
        {
          this.valueMember = valueMember;
        }
      }
    }

    [Category("Behavior")]
    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [Localizable(true)]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    public new string NullText
    {
      get
      {
        return this.textBox.NullText;
      }
      set
      {
        this.textBox.NullText = value;
      }
    }

    [RadDescription("FormatInfo", typeof (BaseComboBoxElement))]
    [RadDefaultValue("FormatInfo", typeof (BaseComboBoxElement))]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public override IFormatProvider FormatInfo
    {
      get
      {
        return (IFormatProvider) null;
      }
      set
      {
      }
    }

    [RadDefaultValue("FormatString", typeof (BaseComboBoxElement))]
    [RadDescription("FormatString", typeof (BaseComboBoxElement))]
    [MergableProperty(false)]
    public override string FormatString
    {
      get
      {
        return string.Empty;
      }
      set
      {
      }
    }

    [RadDescription("FormattingEnabled", typeof (BaseComboBoxElement))]
    [RadDefaultValue("FormattingEnabled", typeof (BaseComboBoxElement))]
    public override bool FormattingEnabled
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [Bindable(true)]
    [RadDescription("SelectedValue", typeof (BaseComboBoxElement))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override object SelectedValue
    {
      get
      {
        if (this.EditorControl.CurrentRow == null || this.ValueColumn == null)
          return (object) null;
        return this.EditorControl.CurrentRow.Cells[this.ValueColumn.Name].Value;
      }
      set
      {
        if (this.ValueColumn == null)
          return;
        for (int index = 0; index < this.EditorControl.Rows.Count; ++index)
        {
          object obj = this.EditorControl.Rows[index].Cells[this.ValueColumn.Name].Value;
          if (obj != null && obj.Equals(value))
          {
            this.EditorControl.CurrentRow = this.EditorControl.Rows[index];
            break;
          }
        }
      }
    }

    protected override bool IndexChanging
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    public override BindingContext BindingContext
    {
      get
      {
        return base.BindingContext;
      }
      set
      {
        base.BindingContext = value;
        this.EditorControl.BindingContext = this.BindingContext;
      }
    }

    public override void BeginUpdate()
    {
      ++this.update;
    }

    public override void EndUpdate()
    {
      if (this.update == 0)
        return;
      --this.update;
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      if (this.ElementTree.Control is RadGridView)
      {
        this.TextBoxElement.Fill.BackColor = this.ComboBoxFill.BackColor;
        this.TextBoxElement.TextBoxItem.BackColor = this.ComboBoxFill.BackColor;
      }
      HostedTextBoxBase textBoxControl = this.TextBoxElement.TextBoxItem.TextBoxControl;
      textBoxControl.SelectionStart = 0;
      textBoxControl.SelectionLength = 0;
      textBoxControl.SelectionLength = textBoxControl.Text.Length;
      if (this.DropDownStyle == RadDropDownStyle.DropDownList)
        this.TextboxContentElement.Focus();
      else
        textBoxControl.Focus();
    }

    public override void EndInit()
    {
      if (this.ElementTree == null)
        this.initializing = true;
      this.EditorControl.EndInit();
      base.EndInit();
      if (this.ElementTree != null)
        return;
      this.initializing = false;
    }

    public override int GetItemHeight(int index)
    {
      return this.EditorControl.Rows[index].Height;
    }

    public override string GetItemText(object item)
    {
      return string.Empty;
    }

    public override ArrayList FindAllItems(string startsWith)
    {
      return new ArrayList((ICollection) this.MultiColumnPopupForm.FindAllItems(startsWith));
    }

    public void BestFitColumns()
    {
      if (!this.IsInValidState(true))
        return;
      if (this.EditorControl.CurrentRow != null)
        this.EditorControl.CurrentRow.EnsureVisible();
      this.EditorControl.MasterTemplate.BestFitColumns();
      if (this.IsPopupOpen)
        return;
      this.bestFitColumns = true;
    }

    public void BestFitColumns(bool adjustComboBoxSize, bool bestFitAllRows)
    {
      this.adjustComboBoxSize = adjustComboBoxSize;
      this.bestFitAllRows = bestFitAllRows;
      if (this.IsPopupOpen)
        this.EditorControl.TableElement.BestFitColumns();
      else
        this.bestFitColumns = true;
    }

    public virtual void ClearFilter()
    {
      this.clearingFilters = true;
      foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.EditorControl.FilterDescriptors)
        this.ClearFilterDescriptor(filterDescriptor);
      this.clearingFilters = false;
      if (this.KeyboardCommandIssued)
        return;
      this.CheckForCompleteMatchAndUpdateText();
    }

    public virtual void ApplyFilter()
    {
      try
      {
        this.SetCurrentState(PopupEditorState.Filtering);
        string textToSearch = this.Text;
        if (AutoCompleteMode.Append == (this.AutoCompleteMode & AutoCompleteMode.Append) && this.textBox.SelectionLength > 0 && this.textBox.SelectionStart > 0)
          textToSearch = this.textBox.Text.Substring(0, this.textBox.SelectionStart);
        foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.EditorControl.FilterDescriptors)
          this.ProcessFilterDescriptor(filterDescriptor, textToSearch);
        for (int index = 0; index < this.EditorControl.FilterDescriptors.Count; ++index)
        {
          FilterDescriptor filterDescriptor = this.EditorControl.FilterDescriptors[index];
          this.EditorControl.FilterDescriptors.RemoveAt(index);
          this.EditorControl.FilterDescriptors.Insert(index, filterDescriptor);
        }
        this.SelectFirstRow();
        if (this.KeyboardCommandIssued)
          return;
        this.CheckForCompleteMatchAndUpdateText();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        this.ResetCurrentState();
      }
    }

    protected virtual void ProcessFilterDescriptor(FilterDescriptor descriptor, string textToSearch)
    {
      CompositeFilterDescriptor filterDescriptor1 = descriptor as CompositeFilterDescriptor;
      if (filterDescriptor1 != null)
      {
        foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
          this.ProcessFilterDescriptor(filterDescriptor2, textToSearch);
      }
      else
      {
        object result = (object) null;
        GridViewDataColumn[] columnByFieldName = this.EditorControl.Columns.GetColumnByFieldName(descriptor.PropertyName);
        if (columnByFieldName.Length > 0 && (object) columnByFieldName[0].DataType == (object) typeof (string))
          result = (object) string.Empty;
        if (!string.IsNullOrEmpty(textToSearch) && columnByFieldName.Length > 0)
          RadDataConverter.Instance.TryParse((IDataConversionInfoProvider) columnByFieldName[0], (object) textToSearch, out result);
        descriptor.Value = result;
      }
    }

    protected virtual void ClearFilterDescriptor(FilterDescriptor descriptor)
    {
      CompositeFilterDescriptor filterDescriptor1 = descriptor as CompositeFilterDescriptor;
      if (filterDescriptor1 != null)
      {
        foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
          this.ClearFilterDescriptor(filterDescriptor2);
      }
      else
        descriptor.Value = (object) null;
    }

    protected override void OnPopupOpening(CancelEventArgs e)
    {
      if (this.bestFitColumns)
      {
        this.EditorControl.BestFitColumns(this.AutoSizeDropDownColumnMode);
        this.bestFitColumns = false;
      }
      this.EditorControl.GridNavigator.SelectRow(this.EditorControl.CurrentRow);
      base.OnPopupOpening(e);
    }

    protected override void OnPopupClosed(RadPopupClosedEventArgs e)
    {
      if (!this.isValueChanging)
        this.FireSelectionEvents(new CurrentRowChangedEventArgs(this.oldRow, this.EditorControl.CurrentRow));
      if (e.CloseReason == RadPopupCloseReason.Mouse)
        this.EditorControl.MasterTemplate.Refresh();
      base.OnPopupClosed(e);
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      if (this.IsPopupOpen)
        this.ClosePopup(RadPopupCloseReason.CloseCalled);
      if (this.Parent != null && this.Parent.ElementTree != null && this.Parent.ElementTree.Control is RadGridView)
        this.usedInRadGridView = true;
      if (this.Parent != null)
        this.EditorControl.BindingContext = this.Parent.BindingContext;
      base.OnParentChanged(previousParent);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadElement.VisibilityProperty)
      {
        if (this.IsPopupOpen)
          this.ClosePopup(RadPopupCloseReason.CloseCalled);
        this.SaveAndRestoreText();
      }
      if (e.Property == RadElement.RightToLeftProperty)
      {
        (this.PopupForm as MultiColumnComboPopupForm).EditorControl.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.Inherit;
        bool newValue = (bool) e.NewValue;
        if (this.Shape != null)
          this.Shape.IsRightToLeft = newValue;
        if (this.ArrowButton != null && this.ArrowButton.Shape != null)
          this.ArrowButton.Shape.IsRightToLeft = newValue;
      }
      base.OnPropertyChanged(e);
      if (e.Property != RadObject.BindingContextProperty)
        return;
      this.EditorControl.BindingContext = this.BindingContext;
    }

    private void SaveAndRestoreText()
    {
      this.BeginUpdate();
      int selectionLength = this.TextBoxElement.TextBoxItem.SelectionLength;
      int selectionStart = this.TextBoxElement.TextBoxItem.SelectionStart;
      string text = this.TextBoxElement.Text;
      this.TextBoxElement.Text = string.Empty;
      this.TextBoxElement.Text = text;
      this.TextBoxElement.TextBoxItem.SelectionLength = selectionLength;
      this.TextBoxElement.TextBoxItem.SelectionStart = selectionStart;
      this.EndUpdate();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == "CurrentState") || this.currentState != PopupEditorState.Selecting || !this.IsPopupOpen)
        return;
      this.ClosePopup();
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (!this.DblClickRotate || this.Rows == null || this.Rows.Count <= 0)
        return;
      this.SetCurrentState(PopupEditorState.Rotating);
      if (this.SelectedIndex < this.Rows.Count - 1)
        ++this.SelectedIndex;
      else
        this.SelectedIndex = 0;
      this.ClosePopup();
      this.ResetCurrentState();
    }

    protected override void ProcessKeyDown(object sender, KeyEventArgs e)
    {
      RadGridView control = this.ElementTree.Control as RadGridView;
      if (control != null)
      {
        switch (e.KeyCode)
        {
          case Keys.Tab:
          case Keys.Return:
          case Keys.Escape:
            if (control.CurrentRow is GridViewNewRowInfo || e.KeyCode != Keys.Return || !this.IsPopupOpen)
            {
              control.GridBehavior.ProcessKeyDown(e);
              return;
            }
            break;
          case Keys.Left:
          case Keys.Right:
            if (this.DropDownStyle == RadDropDownStyle.DropDown)
            {
              this.TextBoxElement.TextBoxItem.TextBoxControl.KeyUp -= new KeyEventHandler(this.TextBoxControl_KeyUp);
              this.TextBoxElement.TextBoxItem.TextBoxControl.KeyUp += new KeyEventHandler(this.TextBoxControl_KeyUp);
              return;
            }
            this.TextBoxControl_KeyUp((object) this, e);
            return;
        }
      }
      base.ProcessKeyDown(sender, e);
    }

    protected override void ProcessTextKeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Tab || e.KeyData == (Keys.Tab | Keys.Shift) || e.KeyCode == Keys.ShiftKey)
        return;
      base.ProcessTextKeyUp(sender, e);
    }

    protected override void ProcessPageUpDownKeys(KeyEventArgs e)
    {
      base.ProcessPageUpDownKeys(e);
      if (!this.IsPopupOpen)
        return;
      this.EditorControl.GridBehavior.ProcessKey(e);
    }

    protected override void ProcessDeleteKey(KeyEventArgs e)
    {
      if (this.DropDownStyle != RadDropDownStyle.DropDownList)
        return;
      if (this.Rows.Count > 0)
        this.SelectedIndex = 0;
      e.Handled = true;
    }

    public override bool ProcessEscKey(KeyEventArgs e)
    {
      if (!this.AutoFilter)
        return base.ProcessEscKey(e);
      this.EditorControl.MasterTemplate.EventDispatcher.SuspendNotifications();
      this.ClearFilter();
      if (this.currentRow != this.EditorControl.CurrentRow)
        this.EditorControl.CurrentRow = this.currentRow;
      this.SelectAllText(this.Text);
      this.EditorControl.MasterTemplate.EventDispatcher.ResumeNotifications();
      this.SetCurrentState(PopupEditorState.Ready);
      return true;
    }

    internal void SetCurrentRowOnReturnOrTabKey(KeyEventArgs e)
    {
      this.KeyboardCommandIssued = true;
      this.EditorControl.MasterTemplate.EventDispatcher.SuspendNotifications();
      if (this.AutoFilter)
      {
        int selectedIndex = this.SelectedIndex;
        this.ClearFilter();
        this.SelectedIndex = selectedIndex;
      }
      this.EditorControl.MasterTemplate.EventDispatcher.ResumeNotifications();
      GridViewRowInfo itemExact = this.FindItemExact(this.Text) as GridViewRowInfo;
      GridViewColumn gridViewColumn = (GridViewColumn) null;
      if (this.AutoFilter && this.Text != string.Empty && this.EditorControl.Rows.Count > 0)
      {
        gridViewColumn = (GridViewColumn) this.DisplayColumn;
        if (gridViewColumn == null && this.Columns.Count > 0)
          gridViewColumn = (GridViewColumn) this.Columns[0];
        this.EditorControl.MasterTemplate.EventDispatcher.SuspendNotifications();
        if (itemExact == null)
        {
          this.GetCurrentRow(false);
          if (this.SelectedItem != null)
          {
            GridViewRowInfo selectedItem = this.SelectedItem as GridViewRowInfo;
            if (selectedItem != null)
            {
              object obj = selectedItem.Cells[this.DisplayColumn.Name].Value;
              if (obj != null)
                this.SetText(obj.ToString());
            }
          }
          else if (this.ClearTextOnValidation)
            this.SetText("");
          this.EditorControl.MasterTemplate.EventDispatcher.ResumeNotifications();
          this.ClosePopup(RadPopupCloseReason.Keyboard);
          return;
        }
        this.EditorControl.MasterTemplate.EventDispatcher.ResumeNotifications();
      }
      if (itemExact == null)
      {
        this.EditorControl.MasterTemplate.EventDispatcher.SuspendNotifications();
        this.EditorControl.CurrentRow = (GridViewRowInfo) null;
        this.SelectedIndex = -1;
        this.EditorControl.MasterTemplate.EventDispatcher.ResumeNotifications();
      }
      if (itemExact != this.currentRow)
        this.FireSelectionEvents(new CurrentRowChangedEventArgs(this.currentRow, itemExact));
      string text = this.GetText((object) itemExact);
      if (e.KeyCode == Keys.Return && text != string.Empty)
        this.SetText(text);
      this.SelectAllText(this.Text);
      if (!string.IsNullOrEmpty(this.DisplayMember))
      {
        gridViewColumn = (GridViewColumn) this.DisplayColumn;
        if (gridViewColumn == null && this.Columns.Count > 0)
          gridViewColumn = (GridViewColumn) this.DisplayColumn;
      }
      if (gridViewColumn != null && itemExact != null && (this.EditorControl.CurrentRow != null && itemExact.Cells[gridViewColumn.Index].Value != this.EditorControl.CurrentRow.Cells[gridViewColumn.Index].Value))
        this.EditorControl.CurrentRow = this.currentRow;
      this.currentRow = itemExact;
      this.KeyboardCommandIssued = false;
      this.ClosePopup(RadPopupCloseReason.Keyboard);
    }

    public override void ProcessReturnKey(KeyEventArgs e)
    {
      base.ProcessReturnKey(e);
      this.SetCurrentRowOnReturnOrTabKey(e);
    }

    protected override void ProcessTextChanged(object sender, EventArgs e)
    {
      base.ProcessTextChanged(sender, e);
      if (this.AutoFilter && this.EditorControl.FilterDescriptors.Count > 0)
      {
        this.ApplyFilter();
        this.OnValueChanged(EventArgs.Empty);
      }
      this.SetCurrentState(PopupEditorState.Typing);
      if (this.AutoFilter && !this.IsPopupOpen)
        this.ShowPopup();
      this.ResetCurrentState();
    }

    private void TextBoxControl_KeyUp(object sender, KeyEventArgs e)
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.KeyUp -= new KeyEventHandler(this.TextBoxControl_KeyUp);
      this.TextboxContentElement.KeyUp -= new KeyEventHandler(this.TextBoxControl_KeyUp);
      (this.ElementTree.Control as RadGridView)?.GridBehavior.ProcessKeyDown(e);
    }

    private void OnGrid_CurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
    {
      if (this.CurrentState == PopupEditorState.Filtering)
        return;
      e.Cancel = this.RaiseValueChanging(e.NewRow);
    }

    private void OnGrid_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
    {
      this.SyncTextWithItem(false);
      if (this.CurrentState == PopupEditorState.Filtering)
        return;
      this.currentRow = e.CurrentRow;
      this.FireSelectionEvents(e);
    }

    private void OnGrid_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      this.savedRowsHeight = -1;
    }

    private void ArrowButton_Click(object sender, EventArgs e)
    {
      if (!this.AutoFilter)
        return;
      this.EditorControl.BeginUpdate();
      this.ClearFilter();
      this.EditorControl.EndUpdate(false);
    }

    private void SetCurrentState(PopupEditorState state)
    {
      this.currentState = state;
      this.OnNotifyPropertyChanged("CurrentState");
    }

    private void ResetCurrentState()
    {
      this.currentState = PopupEditorState.Ready;
      this.OnNotifyPropertyChanged("CurrentState");
    }

    protected override object FindItemExact(string text)
    {
      int itemIndexExact = this.FindItemIndexExact(text);
      if (itemIndexExact != -1)
        return (object) this.EditorControl.Rows[itemIndexExact];
      return (object) null;
    }

    protected override int FindItemIndexExact(string text)
    {
      GridViewDataRowInfo itemExact = this.MultiColumnPopupForm.FindItemExact(text) as GridViewDataRowInfo;
      if (itemExact == null)
        return -1;
      return itemExact.ViewTemplate.Rows.IndexOf((GridViewRowInfo) itemExact);
    }

    protected override object FindItem(string startsWith)
    {
      return (object) null;
    }

    protected override void DoScrollLineUp()
    {
      if (!this.MultiColumnPopupForm.Visible)
        return;
      RadScrollBarElement vscrollBar = this.EditorControl.TableElement.VScrollBar;
      if (vscrollBar.Value <= 0)
        return;
      int num = vscrollBar.Value - vscrollBar.SmallChange;
      if (num < vscrollBar.Minimum)
        vscrollBar.Value = vscrollBar.Minimum;
      else
        vscrollBar.Value = num;
    }

    protected override void DoScrollLineDown()
    {
      if (!this.MultiColumnPopupForm.Visible)
        return;
      RadScrollBarElement vscrollBar = this.EditorControl.TableElement.VScrollBar;
      if (vscrollBar.Value >= vscrollBar.Maximum)
        return;
      int num = vscrollBar.Value + vscrollBar.SmallChange;
      if (num > vscrollBar.Maximum - vscrollBar.LargeChange + 1)
        vscrollBar.Value = vscrollBar.Maximum - vscrollBar.LargeChange + 1;
      else
        vscrollBar.Value = num;
    }

    protected override void ScrollToHome()
    {
    }

    protected override void ScrollItemIntoView(object item)
    {
      (item as GridViewDataRowInfo)?.EnsureVisible();
    }

    protected override void SetAppendAutoComplete(KeyPressEventArgs e)
    {
      if (this.lastPressedChar == '\r')
      {
        this.textBox.SelectAll();
        this.textBox.Focus();
      }
      else
        this.SearchForStringInList(this.CreateFindString(e), e, this.LimitToList);
    }

    private string CreateFindString(KeyPressEventArgs e)
    {
      return this.textBox.SelectionLength != 0 ? string.Format("{0}{1}", (object) this.textBox.Text.Substring(0, this.textBox.SelectionStart), (object) e.KeyChar) : string.Format("{0}{1}", (object) this.textBox.Text, (object) e.KeyChar);
    }

    private void SearchForStringInList(string findString, KeyPressEventArgs e, bool limitToList)
    {
      GridViewRowInfo gridViewRowInfo = (GridViewRowInfo) null;
      if (this.MultiColumnPopupForm.EditorControl.RowCount > 0)
        gridViewRowInfo = this.MultiColumnPopupForm.FindItem(findString);
      if (gridViewRowInfo == null)
      {
        e.Handled = limitToList && e.KeyChar != '\b';
      }
      else
      {
        this.textBox.Text = gridViewRowInfo.Cells[this.DisplayColumn.Index].Value.ToString();
        this.textBox.SelectionStart = findString.Length;
        this.textBox.SelectionLength = this.textBox.Text.Length;
        e.Handled = true;
      }
    }

    protected override void SelectPreviousItem()
    {
      if (this.EditorControl.Rows.Count < 1)
        return;
      if (this.EditorControl.CurrentRow == null)
      {
        this.EditorControl.CurrentRow = this.GetCurrentRow(true);
      }
      else
      {
        this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectRow(this.EditorControl.CurrentRow);
        if (this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectPreviousRow(1))
          return;
        if (this.MultiColumnPopupForm.EditorControl.RowCount > 0)
        {
          this.MultiColumnPopupForm.EditorControl.BeginUpdate();
          this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectNextRow(1);
          this.MultiColumnPopupForm.EditorControl.EndUpdate(false);
        }
        this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectPreviousRow(1);
      }
    }

    protected override void SelectNextItem()
    {
      if (this.EditorControl.Rows.Count < 1)
        return;
      if (this.EditorControl.CurrentRow == null)
      {
        this.EditorControl.CurrentRow = this.GetCurrentRow(true);
      }
      else
      {
        this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectRow(this.EditorControl.CurrentRow);
        if (this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectNextRow(1))
          return;
        if (this.MultiColumnPopupForm.EditorControl.ChildRows.Count > this.MultiColumnPopupForm.EditorControl.CurrentRow.Index + 1)
          this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectFirstRow();
        this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectNextRow(1);
      }
    }

    protected override Size GetPopupSize(RadPopupControlBase popup, bool measure)
    {
      MultiColumnComboPopupForm popupForm = this.PopupForm as MultiColumnComboPopupForm;
      if (popupForm == null)
        return Size.Empty;
      Size size = new Size(this.ControlBoundingRectangle.Width, this.DropDownHeight);
      RadGridView editorControl = popupForm.EditorControl;
      GridTableElement tableElement = editorControl.TableElement;
      int maxDropDownItems = this.MaxDropDownItems;
      if (editorControl.RowCount > 0)
        Math.Min(editorControl.ChildRows.Count, this.MaxDropDownItems);
      if (tableElement.ElementState != ElementState.Loaded)
      {
        Size minimumSize = editorControl.MinimumSize;
        editorControl.MinimumSize = size;
        editorControl.Size = size;
        string textToSearch = this.CurrentState == PopupEditorState.Typing ? this.Text : string.Empty;
        foreach (FilterDescriptor filterDescriptor in (Collection<FilterDescriptor>) this.EditorControl.FilterDescriptors)
          this.ProcessFilterDescriptor(filterDescriptor, textToSearch);
        editorControl.LoadElementTree(size);
        editorControl.MinimumSize = minimumSize;
      }
      Padding borderThickness = LightVisualElement.GetBorderThickness((LightVisualElement) tableElement, false);
      bool flag = false;
      if (this.IntegralHeight || this.AutoSizeDropDownHeight)
      {
        if (this.savedRowsHeight == -1 || this.AutoSizeDropDownToBestFit && this.CurrentState == PopupEditorState.Typing)
        {
          this.savedColumnsWidth = -1;
          int num1 = 0;
          int num2 = 0;
          GridTraverser gridTraverser = new GridTraverser(editorControl.MasterView);
          RowElementProvider elementProvider = (RowElementProvider) tableElement.RowScroller.ElementProvider;
          while (gridTraverser.MoveNext())
          {
            if (num1 > this.MaxDropDownItems)
            {
              flag = true;
              break;
            }
            num2 = num2 + (int) elementProvider.GetElementSize(gridTraverser.Current).Height + editorControl.TableElement.RowSpacing;
            ++num1;
          }
          size.Height = num2 + tableElement.Padding.Vertical + borderThickness.Vertical - editorControl.TableElement.RowSpacing;
          size.Height += editorControl.GridViewElement.Padding.Vertical;
          if (this.DropDownSizingMode != SizingMode.None)
          {
            RadElement sizingGrip = (RadElement) popupForm.SizingGrip;
            Rectangle rectangle = new Rectangle(sizingGrip.BoundingRectangle.Location, Size.Add(sizingGrip.BoundingRectangle.Size, sizingGrip.Margin.Size));
            size.Height += rectangle.Height;
          }
          this.savedRowsHeight = size.Height;
        }
        else
          size.Height = this.savedRowsHeight;
      }
      if (this.AutoSizeDropDownToBestFit || this.adjustComboBoxSize)
      {
        if (this.savedColumnsWidth == -1 || this.adjustComboBoxSize)
        {
          this.adjustComboBoxSize = false;
          int num;
          if (this.AutoSizeDropDownToBestFit)
          {
            this.EditorControl.HorizontalScrollState = ScrollState.AlwaysHide;
            num = this.BestFitAllColumns(this.AutoSizeDropDownColumnMode);
          }
          else if (this.bestFitAllRows)
          {
            this.bestFitAllRows = false;
            num = this.BestFitAllColumns(this.AutoSizeDropDownColumnMode);
          }
          else
            num = this.BestFitAllColumns(BestFitColumnMode.DisplayedCells);
          if (num > 0)
          {
            size.Width = num + tableElement.Padding.Horizontal + borderThickness.Horizontal;
            size.Width += editorControl.GridViewElement.Padding.Horizontal;
            size.Width += tableElement.CellSpacing * (editorControl.ColumnCount - 1);
            if (flag)
            {
              tableElement.VScrollBar.ResetLayout(true);
              tableElement.VScrollBar.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
              SizeF desiredSize = tableElement.VScrollBar.GetDesiredSize(false);
              size.Width += (int) desiredSize.Width;
            }
          }
          this.adjustedSize = size;
          this.savedColumnsWidth = size.Width;
        }
        else
          size.Width = this.savedColumnsWidth;
      }
      else
      {
        if (this.bestFitColumns)
        {
          this.bestFitColumns = false;
          if (this.bestFitAllRows)
          {
            this.bestFitAllRows = false;
            this.BestFitAllColumns(this.AutoSizeDropDownColumnMode);
          }
          else
            this.BestFitAllColumns(BestFitColumnMode.DisplayedCells);
        }
        if (this.DropDownWidth != -1)
          size.Width = this.DropDownWidth;
        else if (this.adjustedSize.Width != 0)
          size.Width = this.adjustedSize.Width;
      }
      RadScrollBarElement hscrollBar = this.EditorControl.TableElement.HScrollBar;
      if (this.EditorControl.ChildRows.Count == 1)
      {
        float height = (float) hscrollBar.MinSize.Height;
        size.Height += (int) height;
      }
      return this.ApplyPopupSizeRestrictions(size);
    }

    protected override string GetText(object item)
    {
      string empty = string.Empty;
      GridViewRowInfo gridViewRowInfo = item as GridViewRowInfo;
      if (gridViewRowInfo != null)
      {
        object obj = gridViewRowInfo.Cells[this.DisplayColumn.Name].Value;
        if (obj != null)
          return obj.ToString();
      }
      return empty;
    }

    protected override object GetActiveItem()
    {
      return (object) this.EditorControl.CurrentRow;
    }

    protected virtual GridViewRowInfo GetCurrentRow(
      bool getFirstPosibleIfNoCurrentAvailable)
    {
      if (this.EditorControl.CurrentRow != null)
        return this.EditorControl.CurrentRow;
      if (!getFirstPosibleIfNoCurrentAvailable)
        return (GridViewRowInfo) null;
      if (this.EditorControl.Rows.Count > 0)
        return this.EditorControl.Rows[0];
      return (GridViewRowInfo) null;
    }

    protected virtual void SelectFirstRow()
    {
      if (this.EditorControl.ChildRows.Count <= 0 || this.EditorControl.CurrentRow == this.EditorControl.ChildRows[0])
        return;
      this.EditorControl.CurrentRow = this.EditorControl.ChildRows[0];
    }

    protected virtual void CheckForCompleteMatchAndUpdateText()
    {
      if (this.EditorControl.ChildRows.Count != 1 || !this.SetText(this.GetText((object) this.EditorControl.ChildRows[0])))
        return;
      this.Select(this.Text.Length, this.Text.Length);
    }

    protected virtual GridViewRowInfo FindItemExact(string text, string field)
    {
      return this.MultiColumnPopupForm.FindItemExact(text, field);
    }

    private void FireSelectionEvents(CurrentRowChangedEventArgs e)
    {
      if (this.update > 0 || e.OldRow != null && e.OldRow.ViewTemplate != null && e.OldRow.Index == -1 || e.OldRow == e.CurrentRow)
        return;
      this.isValueChanging = true;
      this.RaiseValueChanged();
      this.CallOnSelectedValueChanged((EventArgs) e);
      this.CallOnSelectedIndexChanged((EventArgs) e);
      this.oldRow = e.CurrentRow;
      this.isValueChanging = false;
    }

    private void SyncTextWithItem(bool selectFullText)
    {
      if (this.initializing)
        return;
      if (this.EditorControl.CurrentRow == null)
      {
        this.SetText(string.Empty);
      }
      else
      {
        if (!(this.EditorControl.CurrentRow is GridViewDataRowInfo))
          return;
        if (this.DisplayColumn == null)
        {
          this.SetText(this.EditorControl.CurrentRow.ToString());
        }
        else
        {
          object obj = this.EditorControl.CurrentRow.Cells[this.DisplayColumn.Name].Value;
          if (obj == null)
            this.SetText(string.Empty);
          else
            this.SetText(obj.ToString());
        }
        if (!selectFullText)
          return;
        if (this.currentState != PopupEditorState.Filtering)
          this.Select(0, this.textBox.Text.Length);
        else
          this.Select(this.textBox.Text.Length, this.textBox.Text.Length);
      }
    }

    private bool SetText(string text)
    {
      if (!(this.Text != text) || this.CurrentState == PopupEditorState.Typing || (this.CurrentState == PopupEditorState.Filtering || this.clearingFilters))
        return false;
      this.SuspendPropertyNotifications();
      this.Text = text;
      this.CallOnTextChanged(EventArgs.Empty);
      this.ResumePropertyNotifications();
      return true;
    }

    private Size ApplyPopupSizeRestrictions(Size dropDownSize)
    {
      if (this.DropDownMaxSize.Width > 0 && dropDownSize.Width > this.DropDownMaxSize.Width)
        dropDownSize.Width = this.DropDownMaxSize.Width;
      if (this.DropDownMaxSize.Height > 0 && dropDownSize.Height > this.DropDownMaxSize.Height)
        dropDownSize.Height = this.DropDownMaxSize.Height;
      if (dropDownSize.Width > SystemInformation.WorkingArea.Width)
        dropDownSize.Width = SystemInformation.WorkingArea.Width;
      if (dropDownSize.Height > SystemInformation.WorkingArea.Height)
        dropDownSize.Height = SystemInformation.WorkingArea.Height;
      return dropDownSize;
    }

    private void RefreshItems(object p, BindingMemberInfo bindingMemberInfo)
    {
      this.SyncTextWithItem(false);
    }

    private int BestFitAllColumns(BestFitColumnMode mode)
    {
      this.EditorControl.BestFitColumns(mode);
      this.EditorControl.TableElement.UpdateLayout();
      int num = 0;
      foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.EditorControl.TableElement.ViewElement.RowLayout.RenderColumns)
      {
        num += renderColumn.Width + this.EditorControl.TableElement.CellSpacing;
        num -= this.EditorControl.TableElement.CellSpacing;
      }
      return num;
    }

    private bool RaiseValueChanging(GridViewRowInfo newRow)
    {
      if (this.IsInValidState(true))
      {
        RadGridView control = this.ElementTree.Control as RadGridView;
        if (control != null)
        {
          ValueChangingEventArgs args = new ValueChangingEventArgs(newRow == null || this.ValueColumn == null ? (object) null : newRow.Cells[this.ValueColumn.Name].Value, this.SelectedValue);
          control.MasterTemplate.EventDispatcher.RaiseEvent<ValueChangingEventArgs>(EventDispatcher.ValueChanging, (object) this, args);
          return args.Cancel;
        }
      }
      return false;
    }

    private void RaiseValueChanged()
    {
      if (!this.IsInValidState(true))
        return;
      (this.ElementTree.Control as RadGridView)?.MasterTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.ValueChanged, (object) this, EventArgs.Empty);
    }

    protected internal override void SetActiveItem(object item)
    {
    }

    protected internal override void SetActiveItem(string text)
    {
      int itemIndexExact = this.FindItemIndexExact(text);
      if (itemIndexExact == -1)
        return;
      this.EditorControl.CurrentRow = this.EditorControl.Rows[itemIndexExact];
      this.MultiColumnPopupForm.EditorControl.GridNavigator.SelectRow(this.EditorControl.CurrentRow);
      this.textBox.SelectionStart = this.textBox.Text.Length;
    }

    internal void SetText(GridViewRowInfo rowInfo)
    {
      string text = this.GetText((object) rowInfo);
      if (!(text != string.Empty) || !(text != this.Text))
        return;
      this.SuspendPropertyNotifications();
      this.Text = text;
      this.textBox.SelectAll();
      this.CallOnTextChanged(EventArgs.Empty);
      this.ResumePropertyNotifications();
    }

    public GridEventType DesiredEvents
    {
      get
      {
        return GridEventType.UI;
      }
    }

    public EventListenerPriority Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    public GridEventProcessMode DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.PreProcess | GridEventProcessMode.PostProcess;
      }
    }

    public GridViewEventResult PreProcessEvent(GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.ViewChanged && ((DataViewChangedEventArgs) eventData.Arguments[0]).Action == ViewChangedAction.FilteringChanged && this.IsPopupOpen)
        this.vscrollbarVisibility = this.EditorControl.TableElement.VScrollBar.Visibility;
      return (GridViewEventResult) null;
    }

    public GridViewEventResult ProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public GridViewEventResult PostProcessEvent(GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.ViewChanged)
      {
        DataViewChangedEventArgs changedEventArgs = (DataViewChangedEventArgs) eventData.Arguments[0];
        if (changedEventArgs.Action == ViewChangedAction.FilteringChanged || changedEventArgs.Action == ViewChangedAction.Reset)
          this.savedRowsHeight = -1;
        if (changedEventArgs.Action == ViewChangedAction.ColumnPropertyChanged)
          this.savedColumnsWidth = -1;
        if (changedEventArgs.Action == ViewChangedAction.FilteringChanged && this.IsPopupOpen && this.AutoFilter)
        {
          if (this.AutoSizeDropDownToBestFit)
          {
            this.MultiColumnPopupForm.Size = this.GetPopupSize((RadPopupControlBase) this.MultiColumnPopupForm, false);
          }
          else
          {
            Size size = this.MultiColumnPopupForm.Size;
            if (this.AutoSizeDropDownHeight)
            {
              size.Height = 0;
              int num1 = 0;
              int num2 = 0;
              GridTraverser gridTraverser = new GridTraverser(this.EditorControl.MasterView);
              GridTableElement tableElement = this.EditorControl.TableElement;
              RowElementProvider elementProvider = (RowElementProvider) tableElement.RowScroller.ElementProvider;
              for (; gridTraverser.MoveNext() && num1 <= this.MaxDropDownItems; ++num1)
                num2 = num2 + (int) elementProvider.GetElementSize(gridTraverser.Current).Height + tableElement.RowSpacing;
              Padding borderThickness = LightVisualElement.GetBorderThickness((LightVisualElement) tableElement, false);
              size.Height = num2 + tableElement.Padding.Vertical + borderThickness.Vertical - tableElement.RowSpacing;
              size.Height += this.EditorControl.GridViewElement.Padding.Vertical;
              MultiColumnComboPopupForm popupForm = this.PopupForm as MultiColumnComboPopupForm;
              if (this.DropDownSizingMode != SizingMode.None)
              {
                RadElement sizingGrip = (RadElement) popupForm.SizingGrip;
                Rectangle rectangle = new Rectangle(sizingGrip.BoundingRectangle.Location, Size.Add(sizingGrip.BoundingRectangle.Size, sizingGrip.Margin.Size));
                size.Height += rectangle.Height;
              }
              this.MultiColumnPopupForm.Size = size;
            }
            if (this.adjustedSize.Width != 0)
            {
              if (this.vscrollbarVisibility != this.EditorControl.TableElement.VScrollBar.Visibility)
              {
                if (this.EditorControl.TableElement.VScrollBar.Visibility == ElementVisibility.Visible)
                  size.Width += (int) this.EditorControl.TableElement.VScrollBar.GetDesiredSize(false).Width;
                else
                  size.Width -= (int) this.EditorControl.TableElement.VScrollBar.GetDesiredSize(false).Width;
                this.vscrollbarVisibility = this.EditorControl.TableElement.VScrollBar.Visibility;
              }
              this.MultiColumnPopupForm.Size = size;
            }
          }
        }
      }
      return (GridViewEventResult) null;
    }

    public bool AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }
  }
}
