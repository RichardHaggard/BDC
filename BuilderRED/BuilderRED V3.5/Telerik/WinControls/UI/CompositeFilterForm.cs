// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CompositeFilterForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Data;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class CompositeFilterForm : BaseCompositeFilterDialog
  {
    private GridViewDataColumn column;
    private CompositeFilterDescriptor compositeFilterDescriptor;
    private List<FilterOperationContext> filterOperationContext;
    private System.Type dataType;
    private RadControl rightEditor;
    private RadControl leftEditor;
    private IContainer components;
    private RadDropDownList radDropDownListLeftOperator;
    private RadDropDownList radDropDownListRightOperator;
    private RadLabel radLabelFormTitle;
    private RadRadioButton radioButtonAndOperator;
    private RadRadioButton radioButtonOrOperator;
    private RadButton radButtonOK;
    private RadButton radButtonCancel;
    private RadGroupBox groupBox;
    private RadCheckBox rchbNot;

    public CompositeFilterForm()
    {
      this.InitializeComponent();
      this.ShowInTaskbar = false;
    }

    public CompositeFilterForm(GridViewDataColumn dataColumn)
      : this(dataColumn, (FilterDescriptor) new CompositeFilterDescriptor())
    {
      this.compositeFilterDescriptor.LogicalOperator = FilterLogicalOperator.Or;
    }

    public CompositeFilterForm(GridViewDataColumn dataColumn, FilterDescriptor filterDescriptor)
      : this(dataColumn, filterDescriptor, false)
    {
    }

    public CompositeFilterForm(
      GridViewDataColumn dataColumn,
      FilterDescriptor filterDescriptor,
      bool useTypedEditors)
      : this()
    {
      this.Initialize(dataColumn, filterDescriptor, useTypedEditors);
    }

    public CompositeFilterForm(
      GridViewDataColumn dataColumn,
      FilterDescriptor filterDescriptor,
      bool useTypedEditors,
      string themeName)
      : this(dataColumn, filterDescriptor, useTypedEditors)
    {
      this.ThemeName = themeName;
    }

    public override void Initialize(
      GridViewDataColumn dataColumn,
      FilterDescriptor filterDescriptor,
      bool useTypedEditors)
    {
      GridViewComboBoxColumn viewComboBoxColumn1 = dataColumn as GridViewComboBoxColumn;
      if (filterDescriptor == null)
      {
        System.Type dataType = dataColumn.DataType;
        GridViewComboBoxColumn viewComboBoxColumn2 = dataColumn as GridViewComboBoxColumn;
        if (viewComboBoxColumn2 != null)
          dataType = viewComboBoxColumn2.FilteringMemberDataType;
        FilterOperator defaultFilterOperator = GridViewHelper.GetDefaultFilterOperator(dataType);
        filterDescriptor = !(dataColumn is GridViewDateTimeColumn) ? new FilterDescriptor(dataColumn.Name, defaultFilterOperator, (object) null) : (FilterDescriptor) new DateFilterDescriptor(dataColumn.Name, defaultFilterOperator, new DateTime?(), false);
      }
      this.dataType = viewComboBoxColumn1 == null ? dataColumn.DataType : viewComboBoxColumn1.FilteringMemberDataType;
      this.column = dataColumn;
      this.InitialSetFilterDescriptor(filterDescriptor);
      if (useTypedEditors)
        this.CreateEditors((GridViewColumn) this.column);
      else
        this.CreateEditors((GridViewColumn) null);
      this.LocalizeForm();
    }

    protected virtual void CreateEditors(GridViewColumn column)
    {
      if (column is GridViewDecimalColumn || column is GridViewRatingColumn)
        this.InitializeSpinEditors();
      else if (column is GridViewDateTimeColumn)
        this.InitializeDateTimeEditors();
      else if (column is GridViewComboBoxColumn || column is GridViewMultiComboBoxColumn)
        this.InitializeDropDownListEditors();
      else if (column is GridViewCheckBoxColumn)
        this.InitializeCheckBoxEditors();
      else if (column is GridViewColorColumn)
        this.InitializeColorBoxEditors();
      else
        this.InitializeTextBoxEditors();
    }

    private void LocalizeForm()
    {
      RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
      this.Text = string.Format(currentProvider.GetLocalizedString("CustomFilterDialogCaption"), (object) this.column.HeaderText);
      this.radLabelFormTitle.Text = currentProvider.GetLocalizedString("CustomFilterDialogLabel");
      this.radioButtonAndOperator.Text = currentProvider.GetLocalizedString("CustomFilterDialogRbAnd");
      this.radioButtonOrOperator.Text = currentProvider.GetLocalizedString("CustomFilterDialogRbOr");
      this.radButtonOK.Text = currentProvider.GetLocalizedString("CustomFilterDialogBtnOk");
      this.radButtonCancel.Text = currentProvider.GetLocalizedString("CustomFilterDialogBtnCancel");
      this.rchbNot.Text = currentProvider.GetLocalizedString("CustomFilterDialogCheckBoxNot");
      ImageAndTextLayoutPanel child = this.groupBox.RootElement.Children[0].Children[1].Children[2] as ImageAndTextLayoutPanel;
      (child.Children[1] as TextPrimitive).Text = "      " + this.rchbNot.Text;
      child.Visibility = ElementVisibility.Hidden;
    }

    private void InitializeColorBoxEditors()
    {
      this.rightEditor = (RadControl) new RadColorBox();
      this.leftEditor = (RadControl) new RadColorBox();
      this.InitialEditorsSetup();
    }

    private void InitializeTextBoxEditors()
    {
      this.rightEditor = (RadControl) new RadTextBox();
      this.leftEditor = (RadControl) new RadTextBox();
      this.InitialEditorsSetup();
    }

    private void InitializeSpinEditors()
    {
      RadSpinEditor spinEditor1 = new RadSpinEditor();
      RadSpinEditor spinEditor2 = new RadSpinEditor();
      this.rightEditor = (RadControl) spinEditor1;
      this.leftEditor = (RadControl) spinEditor2;
      GridViewDecimalColumn column1 = this.column as GridViewDecimalColumn;
      GridViewRatingColumn column2 = this.column as GridViewRatingColumn;
      if (column1 != null)
      {
        this.InitializeSpinEditor(spinEditor1, column1);
        this.InitializeSpinEditor(spinEditor2, column1);
      }
      else if (column2 != null)
      {
        this.InitializeRatingSpinEditor(spinEditor1, column2);
        this.InitializeRatingSpinEditor(spinEditor2, column2);
      }
      this.InitialEditorsSetup();
    }

    private void InitializeSpinEditor(RadSpinEditor spinEditor, GridViewDecimalColumn decimalColumn)
    {
      spinEditor.Minimum = decimalColumn.Minimum;
      spinEditor.Maximum = decimalColumn.Maximum;
      spinEditor.Step = decimalColumn.Step;
      spinEditor.DecimalPlaces = (object) decimalColumn.DataType == (object) typeof (double) || (object) decimalColumn.DataType == (object) typeof (float) || ((object) decimalColumn.DataType == (object) typeof (Decimal) || (object) decimalColumn.DataType == (object) typeof (float)) ? decimalColumn.DecimalPlaces : 0;
      spinEditor.ThousandsSeparator = decimalColumn.ThousandsSeparator;
      spinEditor.ShowUpDownButtons = decimalColumn.ShowUpDownButtons;
    }

    private void InitializeRatingSpinEditor(
      RadSpinEditor spinEditor,
      GridViewRatingColumn ratingColumn)
    {
      spinEditor.Minimum = (Decimal) ratingColumn.Minimum;
      spinEditor.Maximum = (Decimal) ratingColumn.Maximum;
      spinEditor.Step = new Decimal(1);
      spinEditor.DecimalPlaces = 1;
    }

    private void InitializeDateTimeEditors()
    {
      this.rightEditor = (RadControl) new RadDateTimePicker();
      this.leftEditor = (RadControl) new RadDateTimePicker();
      GridViewDateTimeColumn column = this.column as GridViewDateTimeColumn;
      if (column != null && !string.IsNullOrEmpty(column.CustomFormat))
      {
        ((RadDateTimePicker) this.rightEditor).Format = DateTimePickerFormat.Custom;
        ((RadDateTimePicker) this.rightEditor).CustomFormat = column.CustomFormat;
        ((RadDateTimePicker) this.leftEditor).Format = DateTimePickerFormat.Custom;
        ((RadDateTimePicker) this.leftEditor).CustomFormat = column.CustomFormat;
      }
      this.InitialEditorsSetup();
    }

    private void InitializeDropDownListEditors()
    {
      GridViewComboBoxColumn column = this.column as GridViewComboBoxColumn;
      if (column != null && column.DataSource != null && !string.IsNullOrEmpty(column.ValueMember))
      {
        this.rightEditor = (RadControl) this.CreateDropDownList();
        this.leftEditor = (RadControl) this.CreateDropDownList();
        this.InitialEditorsSetup();
      }
      else
        this.InitializeTextBoxEditors();
    }

    private RadDropDownList CreateDropDownList()
    {
      RadDropDownList radDropDownList = new RadDropDownList();
      radDropDownList.DropDownStyle = RadDropDownStyle.DropDownList;
      GridViewComboBoxColumn column = this.column as GridViewComboBoxColumn;
      radDropDownList.DataSource = (object) new BindingSource()
      {
        DataSource = column.DataSource
      };
      string str1 = column.DisplayMember != null ? column.DisplayMember : column.ValueMember;
      string str2 = column.ValueMember;
      radDropDownList.DisplayMember = str1;
      if (column.FilteringMode == GridViewFilteringMode.DisplayMember)
        str2 = str1;
      radDropDownList.ValueMember = str2;
      return radDropDownList;
    }

    private void InitializeCheckBoxEditors()
    {
      if (this.column != null && this.column is GridViewCheckBoxColumn)
      {
        if (this.ClientSize.Width < 260)
          this.ClientSize = new Size(260, 196);
        this.rightEditor = (RadControl) new RadCheckBox();
        this.rightEditor.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomFilterDialogFalse");
        ((RadToggleButton) this.rightEditor).ToggleStateChanged += new StateChangedEventHandler(this.CheckBoxEditor_ToggleStateChanged);
        this.rightEditor.Location = new Point(180, 81);
        this.leftEditor = (RadControl) new RadCheckBox();
        this.leftEditor.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomFilterDialogFalse");
        ((RadToggleButton) this.leftEditor).ToggleStateChanged += new StateChangedEventHandler(this.CheckBoxEditor_ToggleStateChanged);
        this.leftEditor.Location = new Point(180, 34);
        this.groupBox.Controls.Add((Control) this.rightEditor);
        this.groupBox.Controls.Add((Control) this.leftEditor);
      }
      else
        this.InitializeTextBoxEditors();
    }

    private void CheckBoxEditor_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      RadCheckBox radCheckBox = sender as RadCheckBox;
      if (radCheckBox == null)
        return;
      string localizedString1 = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomFilterDialogTrue");
      string localizedString2 = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("CustomFilterDialogFalse");
      radCheckBox.Text = radCheckBox.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? localizedString1 : localizedString2;
    }

    private void InitialEditorsSetup()
    {
      if (this.rightEditor == null || this.leftEditor == null)
        throw new NullReferenceException("Filter editors is not initialized!");
      int num = 20;
      Point location1 = this.radDropDownListLeftOperator.Location;
      Point location2 = this.radDropDownListRightOperator.Location;
      Size size1 = this.radDropDownListLeftOperator.Size;
      Size size2 = this.radDropDownListRightOperator.Size;
      this.rightEditor.Location = new Point(location2.X + size2.Width + num, location2.Y);
      this.rightEditor.Name = "radTextBoxRightOperatorValue";
      this.rightEditor.Size = size2;
      this.rightEditor.TabIndex = 5;
      this.rightEditor.TabStop = false;
      this.leftEditor.Location = new Point(location1.X + size1.Width + num, location1.Y);
      this.leftEditor.Name = "radTextBoxLeftOperatorValue";
      this.leftEditor.Size = size1;
      this.leftEditor.TabIndex = 1;
      this.leftEditor.TabStop = false;
      this.groupBox.Controls.Add((Control) this.rightEditor);
      this.groupBox.Controls.Add((Control) this.leftEditor);
    }

    public FilterLogicalOperator LogicalOperator
    {
      get
      {
        return this.compositeFilterDescriptor.LogicalOperator;
      }
      private set
      {
        if (this.compositeFilterDescriptor.LogicalOperator == value)
          return;
        this.compositeFilterDescriptor.LogicalOperator = value;
      }
    }

    public RadDropDownList ComboBoxLeftOperator
    {
      get
      {
        return this.radDropDownListLeftOperator;
      }
    }

    public RadDropDownList ComboBoxRightOperator
    {
      get
      {
        return this.radDropDownListRightOperator;
      }
    }

    public RadControl LeftEditor
    {
      get
      {
        return this.leftEditor;
      }
    }

    public RadControl RightEditor
    {
      get
      {
        return this.rightEditor;
      }
    }

    public override FilterDescriptor FilterDescriptor
    {
      get
      {
        return this.GetFilterDescriptor(this.compositeFilterDescriptor);
      }
    }

    private FilterDescriptor GetFilterDescriptor(CompositeFilterDescriptor composite)
    {
      FilterDescriptor filterDescriptor1 = new FilterDescriptor();
      filterDescriptor1.Operator = FilterOperator.None;
      FilterDescriptor filterDescriptor2 = new FilterDescriptor();
      filterDescriptor2.Operator = FilterOperator.None;
      if (composite.FilterDescriptors.Count > 0)
        filterDescriptor1 = composite.FilterDescriptors[0];
      if (composite.FilterDescriptors.Count > 1)
        filterDescriptor2 = composite.FilterDescriptors[1];
      if (composite.NotOperator && (filterDescriptor1.Operator != FilterOperator.None || filterDescriptor2.Operator != FilterOperator.None))
      {
        if (filterDescriptor1.Operator == FilterOperator.None)
          composite.FilterDescriptors.Remove(filterDescriptor1);
        if (filterDescriptor2.Operator == FilterOperator.None)
          composite.FilterDescriptors.Remove(filterDescriptor2);
        return (FilterDescriptor) composite;
      }
      if (filterDescriptor1.Operator != FilterOperator.None && filterDescriptor2.Operator != FilterOperator.None)
        return (FilterDescriptor) composite;
      if (filterDescriptor2.Operator == FilterOperator.None && filterDescriptor1.Operator != FilterOperator.None)
        return filterDescriptor1;
      if (filterDescriptor1.Operator == FilterOperator.None && filterDescriptor2.Operator != FilterOperator.None)
        return filterDescriptor2;
      return (FilterDescriptor) null;
    }

    public FilterDescriptor LeftDescriptor
    {
      get
      {
        if (this.compositeFilterDescriptor.FilterDescriptors.Count > 0)
          return this.compositeFilterDescriptor.FilterDescriptors[0];
        return (FilterDescriptor) null;
      }
    }

    public FilterDescriptor RightDescriptor
    {
      get
      {
        if (this.compositeFilterDescriptor.FilterDescriptors.Count > 1)
          return this.compositeFilterDescriptor.FilterDescriptors[1];
        return (FilterDescriptor) null;
      }
    }

    public System.Type DataType
    {
      get
      {
        return this.dataType;
      }
      set
      {
        this.dataType = value;
      }
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.PrepareDropDownLists(this.radDropDownListLeftOperator, this.LeftDescriptor.Operator);
      this.PrepareDropDownLists(this.radDropDownListRightOperator, this.RightDescriptor.Operator);
      this.PrepareEditors();
      this.radioButtonAndOperator.ToggleState = this.LogicalOperator == FilterLogicalOperator.And ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      this.radioButtonOrOperator.ToggleState = this.LogicalOperator == FilterLogicalOperator.Or ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      this.rchbNot.Checked = this.compositeFilterDescriptor.NotOperator;
      this.EnableNotCheckBox();
      this.radioButtonAndOperator.ToggleStateChanged += new StateChangedEventHandler(this.LogicalOperator_CheckedChanged);
      this.radioButtonOrOperator.ToggleStateChanged += new StateChangedEventHandler(this.LogicalOperator_CheckedChanged);
      this.SetTheme((string) null);
      this.radButtonOK.ButtonElement.ResetLayout(true);
      this.radButtonOK.ButtonElement.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
      SizeF desiredSize = this.radButtonOK.ButtonElement.GetDesiredSize(false);
      if (this.radButtonOK.Height < (int) desiredSize.Height)
        this.radButtonOK.Height = (int) desiredSize.Height;
      this.radButtonCancel.ButtonElement.ResetLayout(true);
      this.radButtonCancel.ButtonElement.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
      desiredSize = this.radButtonCancel.ButtonElement.GetDesiredSize(false);
      if (this.radButtonCancel.Height < (int) desiredSize.Height)
        this.radButtonCancel.Height = (int) desiredSize.Height;
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.ClientSize = new Size(401, 245);
        this.groupBox.Location = new Point(4, 36);
        this.groupBox.Size = new Size(384, 149);
        this.rchbNot.Location = new Point(5, 1);
        this.radButtonOK.Location = new Point(162, 201);
        this.radButtonCancel.Location = new Point(278, 201);
        this.radioButtonAndOperator.Size = new Size(69, 30);
        this.radioButtonAndOperator.Location = new Point(17, 70);
        this.radioButtonOrOperator.Location = new Point(92, 71);
        this.radioButtonOrOperator.Size = new Size(38, 17);
        this.radDropDownListLeftOperator.Location = new Point(13, 34);
        this.radDropDownListRightOperator.Location = new Point(14, 106);
        this.leftEditor.Top = this.radDropDownListLeftOperator.Top;
        this.rightEditor.Top = this.radDropDownListRightOperator.Top;
      }
      else
      {
        if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
          return;
        this.Width += 77;
        this.Height += 31;
        this.groupBox.BackColor = Color.Empty;
        this.groupBox.Width += 77;
        this.groupBox.Height += 26;
        this.radioButtonAndOperator.Top = this.radioButtonOrOperator.Top = this.radDropDownListLeftOperator.Bottom + 5;
        this.radioButtonOrOperator.Left = this.radioButtonAndOperator.Right + 5;
        this.radDropDownListRightOperator.Top = this.radioButtonAndOperator.Bottom + 5;
        this.radDropDownListLeftOperator.Width += 50;
        this.radDropDownListRightOperator.Width += 50;
        this.leftEditor.Left = this.radDropDownListLeftOperator.Right + 10;
        this.rightEditor.Left = this.radDropDownListRightOperator.Right + 10;
        this.leftEditor.Top = this.radDropDownListLeftOperator.Top;
        this.rightEditor.Top = this.radDropDownListRightOperator.Top;
        this.leftEditor.Width += 60;
        this.rightEditor.Width += 60;
        this.radButtonOK.Top = this.groupBox.Bottom + 6;
        this.radButtonCancel.Top = this.groupBox.Bottom + 6;
        this.radButtonCancel.Width += 20;
        this.radButtonCancel.Left = this.Width - this.radButtonCancel.Width - 6;
        this.radButtonOK.Left = this.radButtonCancel.Left - this.radButtonOK.Width - 5;
      }
    }

    private void RadDropDownList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (sender == this.radDropDownListLeftOperator)
      {
        this.SetFilterOperator(this.radDropDownListLeftOperator, this.leftEditor, this.LeftDescriptor);
      }
      else
      {
        if (sender != this.radDropDownListRightOperator)
          return;
        this.SetFilterOperator(this.radDropDownListRightOperator, this.rightEditor, this.RightDescriptor);
      }
    }

    private void radButtonOK_Click(object sender, EventArgs e)
    {
      this.ValidateSpinEditor(this.leftEditor as RadSpinEditor);
      this.ValidateSpinEditor(this.rightEditor as RadSpinEditor);
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      if (this.DialogResult == DialogResult.OK)
      {
        Exception exception = this.UpdateValues();
        RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
        if (exception != null)
        {
          e.Cancel = true;
          string themeName = RadMessageBox.Instance.ThemeName;
          RadMessageBox.SetThemeName(this.ThemeName);
          int num = (int) RadMessageBox.Show(exception.Message, currentProvider.GetLocalizedString("CompositeFilterFormErrorCaption"), MessageBoxButtons.OK, RadMessageIcon.Error);
          RadMessageBox.SetThemeName(themeName);
        }
        if (!GridFilterCellElement.ValidateUserFilter(this.GetFilterDescriptor(this.compositeFilterDescriptor.Clone() as CompositeFilterDescriptor)))
        {
          e.Cancel = true;
          string themeName = RadMessageBox.Instance.ThemeName;
          RadMessageBox.SetThemeName(this.ThemeName);
          int num = (int) RadMessageBox.Show(currentProvider.GetLocalizedString("CompositeFilterFormInvalidFilter"), currentProvider.GetLocalizedString("CompositeFilterFormErrorCaption"), MessageBoxButtons.OK, RadMessageIcon.Error);
          RadMessageBox.SetThemeName(themeName);
        }
      }
      RadCheckBox rightEditor = this.rightEditor as RadCheckBox;
      RadCheckBox leftEditor = this.leftEditor as RadCheckBox;
      if (rightEditor != null)
        rightEditor.ToggleStateChanged -= new StateChangedEventHandler(this.CheckBoxEditor_ToggleStateChanged);
      if (leftEditor == null)
        return;
      leftEditor.ToggleStateChanged -= new StateChangedEventHandler(this.CheckBoxEditor_ToggleStateChanged);
    }

    private Exception UpdateValues()
    {
      Exception parseException = (Exception) null;
      this.LeftDescriptor.Value = this.GetValueFromEditor(this.leftEditor, ref parseException);
      this.RightDescriptor.Value = this.GetValueFromEditor(this.rightEditor, ref parseException);
      return parseException;
    }

    protected virtual object GetValueFromEditor(
      RadControl editorControl,
      ref Exception parseException)
    {
      object result = (object) null;
      string text = editorControl.Text;
      if (editorControl is RadDateTimePicker)
        return (object) ((RadDateTimePicker) editorControl).Value;
      if (editorControl is RadDropDownList)
        return ((RadDropDownList) editorControl).SelectedValue;
      if (editorControl is RadMultiColumnComboBox)
        return ((RadMultiColumnComboBox) editorControl).SelectedValue;
      if (editorControl is RadSpinEditor)
        return (object) ((RadSpinEditor) editorControl).Value;
      if (editorControl is RadCheckBox)
        result = (object) (((RadToggleButton) editorControl).ToggleState == Telerik.WinControls.Enumerations.ToggleState.On);
      if (editorControl is RadColorBox)
        result = (object) ((RadColorBox) editorControl).Value;
      if (result == null && !string.IsNullOrEmpty(text))
        parseException = RadDataConverter.Instance.TryParse((IDataConversionInfoProvider) this.column, (object) text, out result);
      if (parseException != null)
        return (object) parseException;
      return result;
    }

    private void radButtonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void LogicalOperator_CheckedChanged(object sender, StateChangedEventArgs args)
    {
      RadRadioButton radRadioButton = sender as RadRadioButton;
      if (sender == this.radioButtonAndOperator && radRadioButton.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
      {
        this.LogicalOperator = FilterLogicalOperator.And;
      }
      else
      {
        if (sender != this.radioButtonOrOperator || radRadioButton.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On)
          return;
        this.LogicalOperator = FilterLogicalOperator.Or;
      }
    }

    private void rchbNot_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.compositeFilterDescriptor.NotOperator = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
    }

    private void InitialSetFilterDescriptor(FilterDescriptor value)
    {
      CompositeFilterDescriptor filterDescriptor = value as CompositeFilterDescriptor;
      if (filterDescriptor == null)
      {
        this.compositeFilterDescriptor = new CompositeFilterDescriptor();
        this.compositeFilterDescriptor.FilterDescriptors.Add(value);
      }
      else
        this.compositeFilterDescriptor = filterDescriptor;
      while (this.compositeFilterDescriptor.FilterDescriptors.Count < 2)
      {
        if ((object) this.DataType == (object) typeof (DateTime))
          this.compositeFilterDescriptor.FilterDescriptors.Add((FilterDescriptor) new DateFilterDescriptor(this.column.Name, FilterOperator.None, new DateTime?()));
        else
          this.compositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(this.column.Name, FilterOperator.None, (object) null));
      }
    }

    private void SetFilterOperator(
      RadDropDownList comboBox,
      RadControl textBox,
      FilterDescriptor descriptor)
    {
      descriptor.Operator = (FilterOperator) comboBox.SelectedValue;
      bool flag = this.IsEditableFilterOperator(descriptor.Operator);
      textBox.Enabled = flag;
      if (!flag)
        textBox.Text = string.Empty;
      this.EnableNotCheckBox();
    }

    private void EnableNotCheckBox()
    {
      this.rchbNot.Enabled = this.LeftDescriptor.Operator != FilterOperator.None || this.RightDescriptor.Operator != FilterOperator.None;
    }

    private void PrepareEditors()
    {
      this.PrepareEditor(this.leftEditor, this.LeftDescriptor);
      this.PrepareEditor(this.rightEditor, this.RightDescriptor);
    }

    private void PrepareEditor(RadControl editor, FilterDescriptor filterDescriptor)
    {
      editor.Enabled = this.IsEditableFilterOperator(filterDescriptor.Operator);
      if (editor is RadColorBox)
      {
        Color empty = Color.Empty;
        if (filterDescriptor.Value != null)
          empty = (Color) filterDescriptor.Value;
        ((RadColorBox) editor).Value = empty;
      }
      else if (editor is RadCheckBox)
        ((RadToggleButton) editor).ToggleState = Convert.ToBoolean(filterDescriptor.Value) ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      else if (editor is RadSpinEditor)
        ((RadSpinEditor) editor).Value = Convert.ToDecimal(filterDescriptor.Value);
      else if (editor is RadDropDownList)
        ((RadDropDownList) editor).SelectedValue = filterDescriptor.Value;
      else if (editor is RadDateTimePicker)
        ((RadDateTimePicker) editor).Value = filterDescriptor.Value == null ? DateTime.Now : Convert.ToDateTime(filterDescriptor.Value);
      else
        editor.Text = Convert.ToString(filterDescriptor.Value);
    }

    private void PrepareDropDownLists(
      RadDropDownList radDropDownList,
      FilterOperator filterOperator)
    {
      if (this.filterOperationContext == null)
        this.filterOperationContext = FilterOperationContext.GetFilterOperations(this.DataType);
      for (int index = 0; index < this.filterOperationContext.Count; ++index)
      {
        FilterOperationContext operationContext = this.filterOperationContext[index];
        RadListDataItem radListDataItem = new RadListDataItem();
        radListDataItem.Text = operationContext.Name;
        radListDataItem.Value = (object) operationContext.Operator;
        radDropDownList.Items.Add(radListDataItem);
        if (operationContext.Operator == filterOperator)
          radDropDownList.SelectedItem = radListDataItem;
      }
      radDropDownList.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.RadDropDownList_SelectedIndexChanged);
    }

    protected bool IsEditableFilterOperator(FilterOperator filterOperator)
    {
      if (filterOperator != FilterOperator.None && filterOperator != FilterOperator.IsNotNull)
        return filterOperator != FilterOperator.IsNull;
      return false;
    }

    private void SetTheme(string themeName)
    {
      if (string.IsNullOrEmpty(themeName))
        themeName = this.ThemeName;
      else
        this.ThemeName = themeName;
      foreach (RadControl control1 in (ArrangedElementCollection) this.Controls)
      {
        control1.ThemeName = themeName;
        if (control1 is RadGroupBox)
        {
          foreach (RadControl control2 in (ArrangedElementCollection) control1.Controls)
            control2.ThemeName = themeName;
        }
      }
    }

    private void ValidateSpinEditor(RadSpinEditor spinEditor)
    {
      spinEditor?.SpinElement.Validate();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radDropDownListLeftOperator = new RadDropDownList();
      this.radDropDownListRightOperator = new RadDropDownList();
      this.radLabelFormTitle = new RadLabel();
      this.radioButtonAndOperator = new RadRadioButton();
      this.radioButtonOrOperator = new RadRadioButton();
      this.radButtonOK = new RadButton();
      this.radButtonCancel = new RadButton();
      this.groupBox = new RadGroupBox();
      this.rchbNot = new RadCheckBox();
      this.radDropDownListLeftOperator.BeginInit();
      this.radLabelFormTitle.BeginInit();
      this.radDropDownListRightOperator.BeginInit();
      this.radioButtonAndOperator.BeginInit();
      this.radioButtonOrOperator.BeginInit();
      this.radButtonOK.BeginInit();
      this.radButtonCancel.BeginInit();
      this.groupBox.BeginInit();
      this.rchbNot.BeginInit();
      this.groupBox.SuspendLayout();
      this.BeginInit();
      this.SuspendLayout();
      this.radDropDownListLeftOperator.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
      this.radDropDownListLeftOperator.DropDownStyle = RadDropDownStyle.DropDownList;
      this.radDropDownListLeftOperator.Location = new Point(13, 32);
      this.radDropDownListLeftOperator.Name = "radComboBoxLeftOperator";
      this.radDropDownListLeftOperator.Size = new Size(149, 21);
      this.radDropDownListLeftOperator.TabIndex = 0;
      this.radDropDownListLeftOperator.TabStop = false;
      this.radLabelFormTitle.Location = new Point(4, 12);
      this.radLabelFormTitle.Name = "radLabelFormTitle";
      this.radLabelFormTitle.Size = new Size(96, 18);
      this.radLabelFormTitle.TabIndex = 2;
      this.radLabelFormTitle.Text = "Show rows where:";
      this.radDropDownListRightOperator.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
      this.radDropDownListRightOperator.DropDownStyle = RadDropDownStyle.DropDownList;
      this.radDropDownListRightOperator.Location = new Point(13, 79);
      this.radDropDownListRightOperator.Name = "radComboBoxRightOperator";
      this.radDropDownListRightOperator.Size = new Size(149, 21);
      this.radDropDownListRightOperator.TabIndex = 4;
      this.radDropDownListRightOperator.TabStop = false;
      this.radioButtonAndOperator.AutoSize = true;
      this.radioButtonAndOperator.Location = new Point(43, 57);
      this.radioButtonAndOperator.Name = "radioButtonAndOperator";
      this.radioButtonAndOperator.Size = new Size(46, 17);
      this.radioButtonAndOperator.TabIndex = 2;
      this.radioButtonAndOperator.TabStop = true;
      this.radioButtonAndOperator.Text = "And";
      this.radioButtonOrOperator.AutoSize = true;
      this.radioButtonOrOperator.Location = new Point(93, 57);
      this.radioButtonOrOperator.Name = "radioButtonOrOperator";
      this.radioButtonOrOperator.Size = new Size(38, 17);
      this.radioButtonOrOperator.TabIndex = 3;
      this.radioButtonOrOperator.TabStop = true;
      this.radioButtonOrOperator.Text = "Or";
      this.radButtonOK.DialogResult = DialogResult.OK;
      this.radButtonOK.Location = new Point(196, 161);
      this.radButtonOK.Name = "radButtonOK";
      this.radButtonOK.Size = new Size(75, 23);
      this.radButtonOK.TabIndex = 6;
      this.radButtonOK.Text = "OK";
      this.radButtonOK.Click += new EventHandler(this.radButtonOK_Click);
      this.radButtonCancel.DialogResult = DialogResult.Cancel;
      this.radButtonCancel.Location = new Point(280, 161);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(75, 23);
      this.radButtonCancel.TabIndex = 7;
      this.radButtonCancel.Text = "Cancel";
      this.radButtonCancel.Click += new EventHandler(this.radButtonCancel_Click);
      this.groupBox.BackColor = Color.Transparent;
      this.groupBox.Controls.Add((Control) this.radDropDownListLeftOperator);
      this.groupBox.Controls.Add((Control) this.rchbNot);
      this.groupBox.Controls.Add((Control) this.radDropDownListRightOperator);
      this.groupBox.Controls.Add((Control) this.radioButtonAndOperator);
      this.groupBox.Controls.Add((Control) this.radioButtonOrOperator);
      this.groupBox.FooterImageIndex = -1;
      this.groupBox.FooterImageKey = "";
      this.groupBox.HeaderImageIndex = -1;
      this.groupBox.HeaderImageKey = "";
      this.groupBox.HeaderMargin = new Padding(0);
      this.groupBox.Location = new Point(4, 36);
      this.groupBox.Name = "groupBox";
      this.groupBox.RootElement.EnableElementShadow = false;
      this.groupBox.Size = new Size(361, 119);
      this.groupBox.TabIndex = 9;
      this.rchbNot.Location = new Point(14, 1);
      this.rchbNot.Name = "rchbNot";
      this.rchbNot.Size = new Size(39, 18);
      this.rchbNot.TabIndex = 8;
      this.rchbNot.Text = "Not";
      this.rchbNot.ToggleStateChanged += new StateChangedEventHandler(this.rchbNot_ToggleStateChanged);
      this.AcceptButton = (IButtonControl) this.radButtonOK;
      this.CancelButton = (IButtonControl) this.radButtonCancel;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(370, 196);
      this.Controls.Add((Control) this.groupBox);
      this.Controls.Add((Control) this.radButtonCancel);
      this.Controls.Add((Control) this.radButtonOK);
      this.Controls.Add((Control) this.radLabelFormTitle);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (CompositeFilterForm);
      this.MaximizeBox = false;
      this.RootElement.ApplyShapeToControl = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "ComplexFilterForm";
      this.radDropDownListLeftOperator.EndInit();
      this.radDropDownListRightOperator.EndInit();
      this.radioButtonAndOperator.EndInit();
      this.radioButtonOrOperator.EndInit();
      this.radLabelFormTitle.EndInit();
      this.radButtonOK.EndInit();
      this.radButtonCancel.EndInit();
      this.groupBox.EndInit();
      this.rchbNot.EndInit();
      this.groupBox.ResumeLayout(false);
      this.groupBox.PerformLayout();
      this.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
