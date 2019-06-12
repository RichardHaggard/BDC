// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ConditionalFormattingForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Data.Expressions;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class ConditionalFormattingForm : RadForm, INotifyPropertyChanged
  {
    private ColumnDisplayStyle columnDisplayStyle = ColumnDisplayStyle.HeaderText | ColumnDisplayStyle.Name;
    private RadGridView ownerControl;
    private GridViewTemplate template;
    private GridViewDataColumn currentColumn;
    private ConditionalFormattingForm.ColumnFormattingInfo formattingInfo;
    private RadListDataItem oldListBoxItem;
    private bool updating;
    private bool sortColumnsAlphabetically;
    private bool selectFromVisibleColumnsOnly;
    private string themeToApply;
    private RadThemeComponentBase.ThemeContext context;
    private IContainer components;
    private RadDropDownList radComboBoxColumns;
    private ImageList imageList2;
    private RadDropDownList radComboBoxType;
    private RadLabel radLabelCellValue;
    private RadLabel radLabelValue1;
    private RadLabel radLabelValue2;
    private RadTextBox radTextBoxName;
    private RadTextBox radTextBoxValue1;
    private RadTextBox radTextBoxValue2;
    private RadButton radButtonOK;
    private RadButton radButtonCancel;
    private RadButton radButtonApply;
    private RadListControl radListBoxConditions;
    private RadCheckBox radCheckBoxApplyToRow;
    private RadButton radButtonRemove;
    private RadButton radButtonAdd;
    private RadLabel radLableFormatCellsWith;
    private RadCheckBox radCheckBoxApplyOnSelectedRows;
    private RadCheckBox radCheckBoxSortAlphabetically;
    private RadTextBox radTextBoxExpression;
    private RadRadioButton radRadioButtonExpression;
    private RadRadioButton radRadioButtonCondition;
    private RadLabel radLabelFormat;
    private RadButton radButtonExpressionEditor;
    private RadLabel radLabelRules;
    private RadLabel radLabelRuleName;
    private RadSeparator radSeparator1;
    private RadLabel radLabelRuleAppliesTo;
    private RadSeparator radSeparator2;
    private RadSeparator radSeparator3;
    private RadPropertyGrid radPropertyGridProperties;

    public ConditionalFormattingForm()
      : this((GridViewTemplate) null, (GridViewDataColumn) null, (string) null)
    {
    }

    public ConditionalFormattingForm(GridViewTemplate template)
      : this(template, (GridViewDataColumn) null, (string) null)
    {
    }

    public ConditionalFormattingForm(GridViewTemplate template, GridViewDataColumn column)
      : this(template, column, (string) null)
    {
    }

    public ConditionalFormattingForm(
      GridViewTemplate template,
      GridViewDataColumn column,
      string themeName)
    {
      this.InitializeComponent();
      this.LocalizeForm();
      this.template = template;
      this.ownerControl = this.template.MasterTemplate.Owner;
      this.currentColumn = column;
      this.themeToApply = themeName;
      this.radTextBoxExpression.Visible = false;
      this.radButtonExpressionEditor.Visible = false;
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
      this.context.ExcludeFromNearest.Add((Control) this.radButtonExpressionEditor);
    }

    public ConditionalFormattingForm(
      RadGridView radGridView,
      GridViewTemplate template,
      GridViewDataColumn column,
      string themeName)
    {
      this.InitializeComponent();
      this.LocalizeForm();
      this.ownerControl = radGridView;
      this.template = template;
      this.currentColumn = column;
      this.themeToApply = themeName;
      this.radTextBoxExpression.Visible = false;
      this.radButtonExpressionEditor.Visible = false;
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
      this.context.ExcludeFromNearest.Add((Control) this.radButtonExpressionEditor);
    }

    [DefaultValue(false)]
    public bool SelectFromVisibleColumnsOnly
    {
      get
      {
        return this.selectFromVisibleColumnsOnly;
      }
      set
      {
        if (this.selectFromVisibleColumnsOnly == value)
          return;
        this.selectFromVisibleColumnsOnly = value;
        if (!this.Visible)
          return;
        this.LoadData();
      }
    }

    [DefaultValue(ColumnDisplayStyle.HeaderText | ColumnDisplayStyle.Name)]
    public ColumnDisplayStyle ColumnDisplayStyle
    {
      get
      {
        return this.columnDisplayStyle;
      }
      set
      {
        if (this.columnDisplayStyle == value)
          return;
        this.columnDisplayStyle = value;
        if (!this.Visible)
          return;
        this.LoadData();
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.template;
      }
      set
      {
        if (this.template == value)
          return;
        this.template = value;
        this.OnNotifyPropertyChanged(nameof (Template));
      }
    }

    public GridViewDataColumn CurrentColumn
    {
      get
      {
        return this.currentColumn;
      }
      set
      {
        if (this.currentColumn == value)
          return;
        this.currentColumn = value;
        this.OnNotifyPropertyChanged(nameof (CurrentColumn));
      }
    }

    public void Edit(GridViewDataColumn column)
    {
      this.currentColumn = column;
      this.LoadData();
      if (this.Visible)
        return;
      this.Show();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.radComboBoxType.DataSource = (object) new string[13]
      {
        this.GetLocalString("[Choose one]"),
        this.GetLocalString("equals to [Value1]"),
        this.GetLocalString("is not equal to [Value1]"),
        this.GetLocalString("starts with [Value1]"),
        this.GetLocalString("ends with [Value1]"),
        this.GetLocalString("contains [Value1]"),
        this.GetLocalString("does not contain [Value1]"),
        this.GetLocalString("is greater than [Value1]"),
        this.GetLocalString("is greater than or equal [Value1]"),
        this.GetLocalString("is less than [Value1]"),
        this.GetLocalString("is less than or equal to [Value1]"),
        this.GetLocalString("is between [Value1] and [Value2]"),
        this.GetLocalString("is not between [Value1] and [Value1]")
      };
      this.AdjustFormForMaterialTheme();
      this.LoadData();
      this.radButtonApply.Enabled = false;
      this.radPropertyGridProperties.PropertyGridElement.SplitElement.HelpElementHeight = 60f;
      this.radPropertyGridProperties.SortOrder = SortOrder.Ascending;
      if (this.themeToApply == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.SetTheme(this.themeToApply);
        this.context.CorrectPositions();
        this.radButtonExpressionEditor.Width += 20;
      }
      else
        this.SetTheme(this.themeToApply);
    }

    private void radPropertyGridProperties_CreateItemElement(
      object sender,
      CreatePropertyGridItemElementEventArgs e)
    {
      switch (e.Item.Name)
      {
        case "CaseSensitive":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridCaseSensitive");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridCaseSensitiveDescription");
          break;
        case "CellBackColor":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridCellBackColor");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridCellBackColorDescription");
          break;
        case "CellForeColor":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridCellForeColor");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridCellForeColorDescription");
          break;
        case "Enabled":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridEnabled");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridEnabledDescription");
          break;
        case "RowBackColor":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridRowBackColor");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridRowBackColorDescription");
          break;
        case "RowForeColor":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridRowForeColor");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridRowForeColorDescription");
          break;
        case "RowTextAlignment":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridRowTextAlignment");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridRowTextAlignmentDescription");
          break;
        case "TextAlignment":
          e.Item.Label = this.GetLocalString("ConditionalFormattingPropertyGridTextAlignment");
          e.Item.Description = this.GetLocalString("ConditionalFormattingPropertyGridTextAlignmentDescription");
          break;
      }
    }

    private void radListBoxConditions_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.ConditionsListSelectionChangedProcessing();
    }

    private void radComboBoxColumns_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      RadListDataItem selectedItem = this.radListBoxConditions.SelectedItem;
      if (selectedItem != null && this.radComboBoxColumns.SelectedItem != null)
        ((ConditionalFormattingForm.ColumnFormattingInfo) selectedItem.Value).Column = (GridViewDataColumn) this.radComboBoxColumns.SelectedItem.Value;
      if (this.updating)
        return;
      this.radButtonApply.Enabled = true;
    }

    private void radPropertyGridProperties_PropertyValueChanged(
      object sender,
      PropertyGridItemValueChangedEventArgs e)
    {
      if (this.updating)
        return;
      this.radButtonApply.Enabled = true;
    }

    private void radCheckBoxApplyToRow_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (this.updating)
        return;
      this.radButtonApply.Enabled = true;
    }

    private void radCheckBoxApplyOnSelectedRows_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      if (this.updating)
        return;
      this.radButtonApply.Enabled = true;
    }

    private void radElement_Changed(object sender, EventArgs e)
    {
      if (this.updating)
        return;
      this.radButtonApply.Enabled = true;
    }

    private void radButtonAdd_Click(object sender, EventArgs e)
    {
      this.updating = true;
      if (this.radComboBoxColumns.SelectedItem == null)
        this.radComboBoxColumns.SelectedIndex = 0;
      BaseFormattingObject formattingObject = new BaseFormattingObject(this.GetNextName(), false);
      this.radListBoxConditions.Items.Add(new RadListDataItem(formattingObject.Name, (object) new ConditionalFormattingForm.ColumnFormattingInfo(this.currentColumn, formattingObject)));
      this.radListBoxConditions.SelectedIndex = this.radListBoxConditions.Items.Count - 1;
      this.updating = false;
      this.radButtonApply.Enabled = true;
    }

    private void radButtonRemove_Click(object sender, EventArgs e)
    {
      int selectedIndex = this.radListBoxConditions.SelectedIndex;
      if (selectedIndex < 0)
        return;
      this.radListBoxConditions.Items.RemoveAt(selectedIndex);
      if (selectedIndex > 0)
        this.radListBoxConditions.SelectedIndex = selectedIndex - 1;
      else if (this.radListBoxConditions.Items.Count > 0)
      {
        this.radListBoxConditions.SelectedIndex = 0;
        this.ConditionsListSelectionChangedProcessing();
      }
      else
      {
        this.radTextBoxName.Text = "";
        this.radTextBoxValue1.Text = "";
        this.radTextBoxValue2.Text = "";
        this.radTextBoxExpression.Text = "";
        this.radCheckBoxApplyToRow.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.radCheckBoxApplyOnSelectedRows.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.radComboBoxType.SelectedItem = (RadListDataItem) null;
        this.radComboBoxColumns.SelectedItem = (RadListDataItem) null;
        this.radPropertyGridProperties.SelectedObject = (object) null;
        this.UpdateUI();
      }
      if (this.radListBoxConditions.Items.Count == 0 && this.radComboBoxColumns.SelectedItem == null && this.radComboBoxColumns.Items.Count > 0)
        this.radComboBoxColumns.SelectedIndex = 0;
      this.radButtonApply.Enabled = true;
    }

    private void radButtonOK_Click(object sender, EventArgs e)
    {
      if (!this.ApplyConditionalFormatting())
        return;
      this.Close();
    }

    private void radButtonCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void radButtonApply_Click(object sender, EventArgs e)
    {
      this.ApplyConditionalFormatting();
    }

    private void radCheckBoxSortAlphabetically_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.sortColumnsAlphabetically = this.radCheckBoxSortAlphabetically.IsChecked;
      List<GridViewColumn> gridViewColumnList = this.SortColumns();
      string text1 = this.radComboBoxColumns.Text;
      this.radComboBoxColumns.Items.Clear();
      foreach (GridViewDataColumn gridViewDataColumn in gridViewColumnList)
      {
        string text2 = string.Empty;
        if ((this.columnDisplayStyle & ColumnDisplayStyle.Name) == ColumnDisplayStyle.Name)
          text2 = gridViewDataColumn.Name;
        if ((this.columnDisplayStyle & ColumnDisplayStyle.HeaderText) == ColumnDisplayStyle.HeaderText)
          text2 = string.IsNullOrEmpty(text2) ? gridViewDataColumn.HeaderText : string.Format("{1} ({0})", (object) text2, (object) gridViewDataColumn.HeaderText);
        RadListDataItem radListDataItem = new RadListDataItem(text2, (object) gridViewDataColumn);
        this.radComboBoxColumns.Items.Add(radListDataItem);
        if (radListDataItem.Text == text1)
          this.radComboBoxColumns.SelectedIndex = this.radComboBoxColumns.Items.IndexOf(radListDataItem);
      }
    }

    private void radRadioButtonCondition_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.UpdateConditionsVisibility();
    }

    private void radRadioButtonExpression_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.UpdateConditionsVisibility();
    }

    private void radButtonExpressionEditor_Click(object sender, EventArgs e)
    {
      if (this.formattingInfo == null)
        return;
      this.formattingInfo.FormattingObject = (BaseFormattingObject) new ExpressionFormattingObject()
      {
        Expression = this.radTextBoxExpression.Text
      };
      RadExpressionEditorForm expressionEditorForm = new RadExpressionEditorForm((ExpressionFormattingObject) this.formattingInfo.FormattingObject, this.template);
      ExpressionEditorFormCreatedEventArgs args = new ExpressionEditorFormCreatedEventArgs(expressionEditorForm);
      this.ownerControl.MasterTemplate.EventDispatcher.RaiseEvent<ExpressionEditorFormCreatedEventArgs>(EventDispatcher.ExpressionEditorFormCreated, (object) this, args);
      expressionEditorForm.FormClosed += new FormClosedEventHandler(this.form_FormClosed);
      RadExpressionEditorForm.Show(this.ownerControl, (ExpressionFormattingObject) this.formattingInfo.FormattingObject, args.ExpressionEditorForm);
    }

    private void radPropertyGridProperties_SelectedObjectChanged(
      object sender,
      PropertyGridSelectedObjectChangedEventArgs e)
    {
      if (this.radPropertyGridProperties.Items.Count <= 0)
        return;
      PropertyGridItem propertyGridItem = this.radPropertyGridProperties.Items["ApplyOnSelectedRows"];
      if (propertyGridItem == null)
        return;
      propertyGridItem.Visible = false;
      this.radPropertyGridProperties.PropertyGridElement.PropertyTableElement.Update(PropertyGridTableElement.UpdateActions.ExpandedChanged);
    }

    private void form_FormClosed(object sender, FormClosedEventArgs e)
    {
      (sender as RadExpressionEditorForm).FormClosed -= new FormClosedEventHandler(this.form_FormClosed);
      ExpressionFormattingObject formattingObject = this.formattingInfo.FormattingObject as ExpressionFormattingObject;
      if (formattingObject == null)
        return;
      this.radTextBoxExpression.Text = formattingObject.Expression;
    }

    private bool ApplyConditionalFormatting()
    {
      if (this.radButtonApply.Enabled)
      {
        RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
        string localizedString = currentProvider.GetLocalizedString("InvalidParameters");
        if (this.radListBoxConditions.Items.Count > 0)
        {
          if (this.radRadioButtonCondition.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
          {
            if (this.radComboBoxType.SelectedIndex == 0 && this.radListBoxConditions.Items.Count > 0)
            {
              int num = (int) RadMessageBox.Show(currentProvider.GetLocalizedString("PleaseSelectValidCellValue"), localizedString, MessageBoxButtons.OK, RadMessageIcon.Error);
              return false;
            }
            if (this.radComboBoxType.SelectedIndex < 11)
            {
              if (this.radListBoxConditions.SelectedItem != null && string.IsNullOrEmpty(this.radTextBoxValue1.Text))
              {
                int num = (int) RadMessageBox.Show(currentProvider.GetLocalizedString("PleaseSetValidCellValue"), localizedString, MessageBoxButtons.OK, RadMessageIcon.Error);
                return false;
              }
            }
            else if (string.IsNullOrEmpty(this.radTextBoxValue1.Text) || string.IsNullOrEmpty(this.radTextBoxValue2.Text))
            {
              int num = (int) RadMessageBox.Show(currentProvider.GetLocalizedString("PleaseSetValidCellValues"), localizedString, MessageBoxButtons.OK, RadMessageIcon.Error);
              return false;
            }
          }
          else if (!this.IsExpressionValid())
          {
            int num = (int) RadMessageBox.Show(currentProvider.GetLocalizedString("PleaseSetValidExpression"), localizedString, MessageBoxButtons.OK, RadMessageIcon.Error);
            return false;
          }
        }
        this.SaveCondition();
        try
        {
          this.ApplyFormatting();
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message, localizedString, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return false;
        }
      }
      return true;
    }

    private bool IsExpressionValid()
    {
      if (string.IsNullOrEmpty(this.radTextBoxExpression.Text))
        return false;
      if (this.template.RowCount == 0)
      {
        ExpressionNode expressionNode;
        return DataUtils.TryParse(this.radTextBoxExpression.Text, false, out expressionNode);
      }
      object result = (object) null;
      int currentPosition = this.template.DataView.CurrentPosition;
      if (this.template.ListSource.CollectionView.TryEvaluate(this.radTextBoxExpression.Text, (IEnumerable<GridViewRowInfo>) this.template.Rows, currentPosition >= 0 ? currentPosition : 0, out result))
        return result is bool;
      return false;
    }

    private void LocalizeForm()
    {
      this.Text = this.GetLocalString("ConditionalFormattingCaption");
      this.radLableFormatCellsWith.Text = this.GetLocalString("ConditionalFormattingLblColumn");
      this.radLabelRuleName.Text = this.GetLocalString("ConditionalFormattingLblName");
      this.radLabelCellValue.Text = this.GetLocalString("ConditionalFormattingLblType");
      this.radLabelValue1.Text = this.GetLocalString("ConditionalFormattingLblValue1");
      this.radLabelValue2.Text = this.GetLocalString("ConditionalFormattingLblValue2");
      this.radLabelFormat.Text = this.GetLocalString("ConditionalFormattingLblFormat");
      this.radLabelRules.Text = this.GetLocalString("ConditionalFormattingGrpConditions");
      this.radCheckBoxApplyToRow.Text = this.GetLocalString("ConditionalFormattingChkApplyToRow");
      this.radCheckBoxApplyOnSelectedRows.Text = this.GetLocalString("ConditionalFormattingChkApplyOnSelectedRows");
      this.radCheckBoxSortAlphabetically.Text = this.GetLocalString("ConditionalFormattingSortAlphabetically");
      this.radButtonAdd.Text = this.GetLocalString("ConditionalFormattingBtnAdd");
      this.radButtonRemove.Text = this.GetLocalString("ConditionalFormattingBtnRemove");
      this.radButtonOK.Text = this.GetLocalString("ConditionalFormattingBtnOK");
      this.radButtonCancel.Text = this.GetLocalString("ConditionalFormattingBtnCancel");
      this.radButtonApply.Text = this.GetLocalString("ConditionalFormattingBtnApply");
      this.radLabelRuleAppliesTo.Text = this.GetLocalString("Rule applies on:");
      this.radRadioButtonCondition.Text = this.GetLocalString("Condition");
      this.radRadioButtonExpression.Text = this.GetLocalString("Expression");
      this.radButtonExpressionEditor.Text = this.GetLocalString("ConditionalFormattingBtnExpression");
      this.radTextBoxExpression.NullText = this.GetLocalString("ConditionalFormattingTextBoxExpression");
    }

    private void AdjustFormForMaterialTheme()
    {
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      int num1 = (int) this.radLabelRules.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      int num2 = (int) this.radLabelRuleName.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      int num3 = (int) this.radLabelRuleAppliesTo.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      int num4 = (int) this.radLableFormatCellsWith.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      int num5 = (int) this.radLabelFormat.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
      this.radComboBoxColumns.Location = new Point(281, 104);
      this.radComboBoxColumns.Size = new Size(330, 37);
      this.radComboBoxType.Location = new Point(281, 266);
      this.radComboBoxType.Size = new Size(329, 37);
      this.radLabelCellValue.Location = new Point(281, 241);
      this.radLabelCellValue.Size = new Size(70, 21);
      this.radLabelValue1.Location = new Point(282, 318);
      this.radLabelValue1.Size = new Size(56, 21);
      this.radLabelValue2.Location = new Point(453, 318);
      this.radLabelValue2.Size = new Size(56, 21);
      this.radTextBoxName.Location = new Point(281, 25);
      this.radTextBoxName.Size = new Size(329, 36);
      this.radTextBoxValue1.Location = new Point(282, 340);
      this.radTextBoxValue1.Size = new Size(165, 36);
      this.radTextBoxValue2.Location = new Point(453, 340);
      this.radTextBoxValue2.Size = new Size(160, 36);
      this.radButtonOK.Location = new Point(341, 693);
      this.radButtonOK.Size = new Size(86, 36);
      this.radButtonCancel.Location = new Point(433, 693);
      this.radButtonCancel.Size = new Size(86, 36);
      this.radListBoxConditions.Location = new Point(2, 67);
      this.radListBoxConditions.Size = new Size(273, 518);
      this.radButtonApply.Location = new Point(525, 693);
      this.radButtonApply.Size = new Size(86, 36);
      this.radCheckBoxApplyToRow.Location = new Point(283, 629);
      this.radCheckBoxApplyToRow.Size = new Size(245, 19);
      this.radButtonRemove.Location = new Point(142, 25);
      this.radButtonRemove.Size = new Size(133, 36);
      this.radButtonAdd.Location = new Point(1, 25);
      this.radButtonAdd.Size = new Size(133, 36);
      this.radLableFormatCellsWith.Location = new Point(281, 163);
      this.radLableFormatCellsWith.Size = new Size(118, 21);
      this.radLabelFormat.Location = new Point(283, 398);
      this.radLabelFormat.Size = new Size(54, 21);
      this.radRadioButtonExpression.Location = new Point(281, 213);
      this.radRadioButtonExpression.Size = new Size(97, 22);
      this.radRadioButtonCondition.Location = new Point(281, 185);
      this.radRadioButtonCondition.Size = new Size(88, 22);
      this.radCheckBoxApplyOnSelectedRows.Location = new Point(283, 653);
      this.radCheckBoxApplyOnSelectedRows.Size = new Size(297, 19);
      this.radButtonExpressionEditor.Location = new Point(284, 340);
      this.radButtonExpressionEditor.Size = new Size(163, 36);
      this.radTextBoxExpression.Location = new Point(281, 241);
      this.radTextBoxExpression.Size = new Size(329, 76);
      this.radCheckBoxSortAlphabetically.Location = new Point(397, 79);
      this.radCheckBoxSortAlphabetically.Size = new Size(203, 19);
      this.radLabelRules.Location = new Point(1, 3);
      this.radLabelRules.Size = new Size(43, 21);
      this.radLabelRuleName.Location = new Point(281, 3);
      this.radLabelRuleName.Size = new Size(75, 21);
      this.radSeparator1.Location = new Point(281, 63);
      this.radSeparator1.Size = new Size(328, 10);
      this.radLabelRuleAppliesTo.Location = new Point(281, 79);
      this.radLabelRuleAppliesTo.Size = new Size(102, 21);
      this.radSeparator2.Location = new Point(281, 147);
      this.radSeparator2.Size = new Size(330, 10);
      this.radSeparator3.Location = new Point(283, 382);
      this.radSeparator3.Size = new Size(329, 10);
      this.radPropertyGridProperties.Location = new Point(283, 425);
      this.radPropertyGridProperties.Size = new Size(330, 198);
      this.Size = new Size(620, 771);
    }

    private void LoadData()
    {
      foreach (GridViewDataColumn sortColumn in this.SortColumns())
      {
        foreach (BaseFormattingObject formattingObject in (Collection<BaseFormattingObject>) sortColumn.ConditionalFormattingObjectList)
        {
          ConditionalFormattingForm.ColumnFormattingInfo columnFormattingInfo = new ConditionalFormattingForm.ColumnFormattingInfo(sortColumn, (BaseFormattingObject) formattingObject.Clone());
          this.radListBoxConditions.Items.Add(new RadListDataItem(formattingObject.Name, (object) columnFormattingInfo));
        }
        this.radComboBoxColumns.Items.Add(new RadListDataItem(this.GetColumnTitle(sortColumn), (object) sortColumn));
      }
      foreach (RadListDataItem radListDataItem in this.radListBoxConditions.Items)
      {
        if (((ConditionalFormattingForm.ColumnFormattingInfo) radListDataItem.Value).Column == this.currentColumn)
        {
          this.radListBoxConditions.SelectedItem = radListDataItem;
          return;
        }
      }
      this.radListBoxConditions.SelectedIndex = 0;
    }

    protected virtual string GetColumnTitle(GridViewDataColumn column)
    {
      string str = string.Empty;
      if ((this.columnDisplayStyle & ColumnDisplayStyle.Name) == ColumnDisplayStyle.Name)
        str = column.Name;
      if ((this.columnDisplayStyle & ColumnDisplayStyle.HeaderText) == ColumnDisplayStyle.HeaderText)
        str = string.IsNullOrEmpty(str) ? column.HeaderText : string.Format("{1} ({0})", (object) str, (object) column.HeaderText);
      return str;
    }

    private List<GridViewColumn> SortColumns()
    {
      List<GridViewColumn> gridViewColumnList = new List<GridViewColumn>(this.template.Columns.Count);
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.template.Columns)
      {
        if (!this.selectFromVisibleColumnsOnly || column.IsVisible)
          gridViewColumnList.Add(column);
      }
      if (this.sortColumnsAlphabetically)
        gridViewColumnList.Sort(new Comparison<GridViewColumn>(ConditionalFormattingForm.CompareColumnsByHeaderText));
      return gridViewColumnList;
    }

    private static int CompareColumnsByHeaderText(GridViewColumn column1, GridViewColumn column2)
    {
      if (column1.HeaderText == null)
        return column2.HeaderText == null ? 0 : -1;
      if (column2.HeaderText == null)
        return 1;
      return column1.HeaderText.CompareTo(column2.HeaderText);
    }

    private void SaveCondition()
    {
      if (!this.radButtonApply.Enabled || this.formattingInfo == null || (this.oldListBoxItem == null || this.radPropertyGridProperties.SelectedObject == null))
        return;
      this.formattingInfo.FormattingObject = this.radRadioButtonCondition.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On ? (BaseFormattingObject) new ExpressionFormattingObject() : (BaseFormattingObject) new ConditionalFormattingObject();
      this.formattingInfo.FormattingObject.Name = this.GetConditionName(this.radTextBoxName.Text);
      this.oldListBoxItem.Text = this.formattingInfo.FormattingObject.Name;
      if (this.radRadioButtonCondition.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
      {
        ConditionalFormattingObject formattingObject = this.formattingInfo.FormattingObject as ConditionalFormattingObject;
        formattingObject.TValue1 = this.radTextBoxValue1.Text;
        formattingObject.TValue2 = this.radTextBoxValue2.Text;
        formattingObject.ConditionType = (ConditionTypes) this.radComboBoxType.SelectedIndex;
      }
      else
        (this.formattingInfo.FormattingObject as ExpressionFormattingObject).Expression = this.radTextBoxExpression.Text;
      this.formattingInfo.FormattingObject.ApplyToRow = this.radCheckBoxApplyToRow.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
      this.formattingInfo.FormattingObject.ApplyOnSelectedRows = this.radCheckBoxApplyOnSelectedRows.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
      ((ConditionalFormatterProperty) this.radPropertyGridProperties.SelectedObject).CopyTo(this.formattingInfo.FormattingObject);
      if (this.radListBoxConditions.SelectedItem == null)
        return;
      this.formattingInfo.Column = (GridViewDataColumn) this.radComboBoxColumns.SelectedItem.Value;
    }

    private void ApplyFormatting()
    {
      if (!this.radButtonApply.Enabled)
        return;
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.template.Columns)
        column.ConditionalFormattingObjectList.Clear();
      foreach (RadListDataItem radListDataItem in this.radListBoxConditions.Items)
      {
        ConditionalFormattingForm.ColumnFormattingInfo columnFormattingInfo = (ConditionalFormattingForm.ColumnFormattingInfo) radListDataItem.Value;
        if (columnFormattingInfo.Column != null)
          columnFormattingInfo.Column.ConditionalFormattingObjectList.Add(columnFormattingInfo.FormattingObject);
      }
      this.radButtonApply.Enabled = false;
    }

    private void UpdateUI()
    {
      bool flag = this.radListBoxConditions.SelectedItem != null;
      this.radTextBoxName.Enabled = flag;
      this.radComboBoxType.Enabled = flag;
      this.radTextBoxValue1.Enabled = flag;
      this.radTextBoxValue2.Enabled = flag;
      this.radTextBoxExpression.Enabled = flag;
      this.radButtonExpressionEditor.Enabled = flag;
      this.radCheckBoxApplyToRow.Enabled = flag;
      this.radCheckBoxApplyOnSelectedRows.Enabled = flag;
      this.radPropertyGridProperties.Enabled = flag;
      this.radButtonRemove.Enabled = flag;
    }

    private string GetNextName()
    {
      int count = this.radListBoxConditions.Items.Count;
      string localizedString = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ConditionalFormattingItem");
      while (this.IsNameUsed(localizedString + count.ToString()))
        ++count;
      return localizedString + count.ToString();
    }

    private bool IsNameUsed(string name)
    {
      for (int index = 0; index < this.radListBoxConditions.Items.Count; ++index)
      {
        if (string.Compare(((ConditionalFormattingForm.ColumnFormattingInfo) this.radListBoxConditions.Items[index].Value).FormattingObject.Name, name) == 0)
          return true;
      }
      return false;
    }

    private string GetConditionName(string proposedName)
    {
      if (this.formattingInfo == null)
        return string.Empty;
      if (string.IsNullOrEmpty(proposedName))
        return this.formattingInfo.FormattingObject.GetHashCode().ToString();
      if (!this.GetConditionByName(proposedName))
        return proposedName;
      return string.Format("{0}.{1}", (object) proposedName, (object) this.formattingInfo.FormattingObject.GetHashCode());
    }

    private bool GetConditionByName(string name)
    {
      for (int index = 0; index < this.radListBoxConditions.Items.Count; ++index)
      {
        ConditionalFormattingForm.ColumnFormattingInfo columnFormattingInfo = this.radListBoxConditions.Items[index].Value as ConditionalFormattingForm.ColumnFormattingInfo;
        if (this.formattingInfo.FormattingObject.GetHashCode() != columnFormattingInfo.FormattingObject.GetHashCode() && string.Compare(columnFormattingInfo.FormattingObject.Name, name) == 0)
          return true;
      }
      return false;
    }

    private void ConditionsListSelectionChangedProcessing()
    {
      this.SaveCondition();
      RadListDataItem selectedItem = this.radListBoxConditions.SelectedItem;
      if (selectedItem != null)
      {
        ConditionalFormattingForm.ColumnFormattingInfo columnFormattingInfo = (ConditionalFormattingForm.ColumnFormattingInfo) selectedItem.Value;
        this.updating = true;
        this.formattingInfo = columnFormattingInfo;
        this.oldListBoxItem = selectedItem;
        this.radTextBoxName.Text = columnFormattingInfo.FormattingObject.Name;
        ConditionalFormattingObject formattingObject1 = columnFormattingInfo.FormattingObject as ConditionalFormattingObject;
        if (formattingObject1 != null)
        {
          this.radRadioButtonCondition.IsChecked = true;
          this.radRadioButtonExpression.IsChecked = false;
          this.radTextBoxValue1.Text = formattingObject1.TValue1;
          this.radTextBoxValue2.Text = formattingObject1.TValue2;
          this.radComboBoxType.SelectedIndex = (int) formattingObject1.ConditionType;
          this.radTextBoxExpression.Text = string.Empty;
        }
        else
        {
          ExpressionFormattingObject formattingObject2 = columnFormattingInfo.FormattingObject as ExpressionFormattingObject;
          if (formattingObject2 != null)
          {
            this.radRadioButtonCondition.IsChecked = false;
            this.radRadioButtonExpression.IsChecked = true;
            this.radTextBoxExpression.Text = formattingObject2.Expression;
            this.radTextBoxValue1.Text = string.Empty;
            this.radTextBoxValue2.Text = string.Empty;
            this.radComboBoxType.SelectedIndex = 0;
          }
          else
          {
            this.radTextBoxValue1.Text = string.Empty;
            this.radTextBoxValue2.Text = string.Empty;
            this.radComboBoxType.SelectedIndex = 0;
            this.radTextBoxExpression.Text = string.Empty;
          }
        }
        this.radCheckBoxApplyToRow.ToggleState = columnFormattingInfo.FormattingObject.ApplyToRow ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.radCheckBoxApplyOnSelectedRows.ToggleState = columnFormattingInfo.FormattingObject.ApplyOnSelectedRows ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        ConditionalFormatterProperty formatterProperty = new ConditionalFormatterProperty();
        formatterProperty.CopyFrom(columnFormattingInfo.FormattingObject);
        this.radPropertyGridProperties.SelectedObject = (object) formatterProperty;
        for (int index = 0; index < this.radComboBoxColumns.Items.Count; ++index)
        {
          if (this.radComboBoxColumns.Items[index].Value == columnFormattingInfo.Column)
          {
            this.radComboBoxColumns.SelectedIndex = index;
            break;
          }
        }
        this.updating = false;
      }
      this.UpdateUI();
    }

    private string GetLocalString(string id)
    {
      return LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString(id);
    }

    private void SetTheme(string themeName)
    {
      if (string.IsNullOrEmpty(themeName))
        return;
      this.ThemeName = themeName;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        RadControl radControl = control as RadControl;
        if (radControl != null)
          radControl.ThemeName = themeName;
      }
    }

    private void UpdateConditionsVisibility()
    {
      if (this.radRadioButtonCondition.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
      {
        this.radLabelCellValue.Visible = true;
        this.radComboBoxType.Visible = true;
        this.radLabelValue1.Visible = true;
        this.radTextBoxValue1.Visible = true;
        this.radLabelValue2.Visible = true;
        this.radTextBoxValue2.Visible = true;
        this.radTextBoxExpression.Visible = false;
        this.radButtonExpressionEditor.Visible = false;
      }
      else
      {
        this.radLabelCellValue.Visible = false;
        this.radComboBoxType.Visible = false;
        this.radLabelValue1.Visible = false;
        this.radTextBoxValue1.Visible = false;
        this.radLabelValue2.Visible = false;
        this.radTextBoxValue2.Visible = false;
        this.radTextBoxExpression.Visible = true;
        this.radButtonExpressionEditor.Visible = true;
      }
    }

    private void SetEnableApplicationThemeName(bool enabled, Control.ControlCollection controls)
    {
      foreach (Control control in (ArrangedElementCollection) controls)
      {
        this.SetEnableApplicationThemeName(enabled, control.Controls);
        RadControl radControl = control as RadControl;
        if (radControl != null)
          radControl.ElementTree.EnableApplicationThemeName = enabled;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConditionalFormattingForm));
      this.imageList2 = new ImageList(this.components);
      this.radComboBoxColumns = new RadDropDownList();
      this.radComboBoxType = new RadDropDownList();
      this.radLabelCellValue = new RadLabel();
      this.radLabelValue1 = new RadLabel();
      this.radLabelValue2 = new RadLabel();
      this.radTextBoxName = new RadTextBox();
      this.radTextBoxValue1 = new RadTextBox();
      this.radTextBoxValue2 = new RadTextBox();
      this.radButtonOK = new RadButton();
      this.radButtonCancel = new RadButton();
      this.radListBoxConditions = new RadListControl();
      this.radButtonApply = new RadButton();
      this.radCheckBoxApplyToRow = new RadCheckBox();
      this.radButtonRemove = new RadButton();
      this.radButtonAdd = new RadButton();
      this.radLableFormatCellsWith = new RadLabel();
      this.radLabelFormat = new RadLabel();
      this.radRadioButtonExpression = new RadRadioButton();
      this.radRadioButtonCondition = new RadRadioButton();
      this.radCheckBoxApplyOnSelectedRows = new RadCheckBox();
      this.radButtonExpressionEditor = new RadButton();
      this.radTextBoxExpression = new RadTextBox();
      this.radCheckBoxSortAlphabetically = new RadCheckBox();
      this.radLabelRules = new RadLabel();
      this.radLabelRuleName = new RadLabel();
      this.radSeparator1 = new RadSeparator();
      this.radLabelRuleAppliesTo = new RadLabel();
      this.radSeparator2 = new RadSeparator();
      this.radSeparator3 = new RadSeparator();
      this.radPropertyGridProperties = new RadPropertyGrid();
      this.radComboBoxColumns.BeginInit();
      this.radComboBoxType.BeginInit();
      this.radLabelCellValue.BeginInit();
      this.radLabelValue1.BeginInit();
      this.radLabelValue2.BeginInit();
      this.radTextBoxName.BeginInit();
      this.radTextBoxValue1.BeginInit();
      this.radTextBoxValue2.BeginInit();
      this.radButtonOK.BeginInit();
      this.radButtonCancel.BeginInit();
      this.radListBoxConditions.BeginInit();
      this.radButtonApply.BeginInit();
      this.radCheckBoxApplyToRow.BeginInit();
      this.radButtonRemove.BeginInit();
      this.radButtonAdd.BeginInit();
      this.radLableFormatCellsWith.BeginInit();
      this.radLabelFormat.BeginInit();
      this.radRadioButtonExpression.BeginInit();
      this.radRadioButtonCondition.BeginInit();
      this.radCheckBoxApplyOnSelectedRows.BeginInit();
      this.radButtonExpressionEditor.BeginInit();
      this.radTextBoxExpression.BeginInit();
      this.radCheckBoxSortAlphabetically.BeginInit();
      this.radLabelRules.BeginInit();
      this.radLabelRuleName.BeginInit();
      this.radSeparator1.BeginInit();
      this.radLabelRuleAppliesTo.BeginInit();
      this.radSeparator2.BeginInit();
      this.radSeparator3.BeginInit();
      this.radPropertyGridProperties.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.imageList2.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList2.ImageStream");
      this.imageList2.TransparentColor = Color.Magenta;
      this.imageList2.Images.SetKeyName(0, "minimize.png");
      this.imageList2.Images.SetKeyName(1, "maximize.png");
      this.imageList2.Images.SetKeyName(2, "close.png");
      this.radComboBoxColumns.AutoCompleteMode = AutoCompleteMode.Append;
      this.radComboBoxColumns.DropDownAnimationEnabled = true;
      this.radComboBoxColumns.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
      this.radComboBoxColumns.Location = new Point(268, 84);
      this.radComboBoxColumns.MaxDropDownItems = 0;
      this.radComboBoxColumns.Name = "radComboBoxColumns";
      this.radComboBoxColumns.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.radComboBoxColumns.ShowImageInEditorArea = true;
      this.radComboBoxColumns.Size = new Size(270, 20);
      this.radComboBoxColumns.TabIndex = 5;
      this.radComboBoxColumns.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radComboBoxColumns_SelectedIndexChanged);
      this.radComboBoxType.AutoCompleteMode = AutoCompleteMode.Append;
      this.radComboBoxType.DropDownAnimationEnabled = true;
      this.radComboBoxType.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
      this.radComboBoxType.Enabled = false;
      this.radComboBoxType.Location = new Point(268, 203);
      this.radComboBoxType.MaxDropDownItems = 0;
      this.radComboBoxType.Name = "radComboBoxType";
      this.radComboBoxType.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.radComboBoxType.RootElement.Enabled = false;
      this.radComboBoxType.ShowImageInEditorArea = true;
      this.radComboBoxType.Size = new Size(270, 20);
      this.radComboBoxType.TabIndex = 8;
      this.radComboBoxType.TextChanged += new EventHandler(this.radElement_Changed);
      this.radLabelCellValue.Location = new Point(268, 185);
      this.radLabelCellValue.Name = "radLabelCellValue";
      this.radLabelCellValue.Size = new Size(54, 18);
      this.radLabelCellValue.TabIndex = 23;
      this.radLabelCellValue.Text = "Cell value";
      this.radLabelValue1.Location = new Point(268, 229);
      this.radLabelValue1.Name = "radLabelValue1";
      this.radLabelValue1.Size = new Size(43, 18);
      this.radLabelValue1.TabIndex = 24;
      this.radLabelValue1.Text = "Value 1";
      this.radLabelValue2.Location = new Point(406, 229);
      this.radLabelValue2.Name = "radLabelValue2";
      this.radLabelValue2.Size = new Size(43, 18);
      this.radLabelValue2.TabIndex = 25;
      this.radLabelValue2.Text = "Value 2";
      this.radTextBoxName.Enabled = false;
      this.radTextBoxName.Location = new Point(268, 25);
      this.radTextBoxName.Name = "radTextBoxName";
      this.radTextBoxName.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.radTextBoxName.RootElement.Enabled = false;
      this.radTextBoxName.Size = new Size(270, 20);
      this.radTextBoxName.TabIndex = 3;
      this.radTextBoxName.TextChanged += new EventHandler(this.radElement_Changed);
      this.radTextBoxValue1.Enabled = false;
      this.radTextBoxValue1.Location = new Point(268, 247);
      this.radTextBoxValue1.Name = "radTextBoxValue1";
      this.radTextBoxValue1.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.radTextBoxValue1.RootElement.Enabled = false;
      this.radTextBoxValue1.Size = new Size(132, 20);
      this.radTextBoxValue1.TabIndex = 9;
      this.radTextBoxValue1.TextChanged += new EventHandler(this.radElement_Changed);
      this.radTextBoxValue2.Enabled = false;
      this.radTextBoxValue2.Location = new Point(406, 247);
      this.radTextBoxValue2.Name = "radTextBoxValue2";
      this.radTextBoxValue2.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.radTextBoxValue2.RootElement.Enabled = false;
      this.radTextBoxValue2.Size = new Size(132, 20);
      this.radTextBoxValue2.TabIndex = 10;
      this.radTextBoxValue2.TextChanged += new EventHandler(this.radElement_Changed);
      this.radButtonOK.DialogResult = DialogResult.OK;
      this.radButtonOK.Location = new Point(268, 562);
      this.radButtonOK.Name = "radButtonOK";
      this.radButtonOK.Size = new Size(86, 23);
      this.radButtonOK.TabIndex = 16;
      this.radButtonOK.Text = "OK";
      this.radButtonOK.Click += new EventHandler(this.radButtonOK_Click);
      this.radButtonCancel.DialogResult = DialogResult.Cancel;
      this.radButtonCancel.Location = new Point(360, 562);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(86, 23);
      this.radButtonCancel.TabIndex = 17;
      this.radButtonCancel.Text = "Cancel";
      this.radButtonCancel.Click += new EventHandler(this.radButtonCancel_Click);
      this.radListBoxConditions.CaseSensitiveSort = true;
      this.radListBoxConditions.Location = new Point(1, 54);
      this.radListBoxConditions.Name = "radListBoxConditions";
      this.radListBoxConditions.Size = new Size(260, 499);
      this.radListBoxConditions.TabIndex = 2;
      this.radListBoxConditions.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radListBoxConditions_SelectedIndexChanged);
      this.radButtonApply.Enabled = false;
      this.radButtonApply.Location = new Point(452, 562);
      this.radButtonApply.Name = "radButtonApply";
      this.radButtonApply.RootElement.Enabled = false;
      this.radButtonApply.Size = new Size(86, 23);
      this.radButtonApply.TabIndex = 18;
      this.radButtonApply.Text = "Apply";
      this.radButtonApply.Click += new EventHandler(this.radButtonApply_Click);
      this.radCheckBoxApplyToRow.Enabled = false;
      this.radCheckBoxApplyToRow.Location = new Point(268, 511);
      this.radCheckBoxApplyToRow.Name = "radCheckBoxApplyToRow";
      this.radCheckBoxApplyToRow.RootElement.Enabled = false;
      this.radCheckBoxApplyToRow.Size = new Size(194, 18);
      this.radCheckBoxApplyToRow.TabIndex = 14;
      this.radCheckBoxApplyToRow.Text = "Apply this formatting to entire row";
      this.radCheckBoxApplyToRow.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxApplyToRow_ToggleStateChanged);
      this.radButtonRemove.Enabled = false;
      this.radButtonRemove.Location = new Point(134, 25);
      this.radButtonRemove.Name = "radButtonRemove";
      this.radButtonRemove.RootElement.Enabled = false;
      this.radButtonRemove.Size = new Size((int) sbyte.MaxValue, 23);
      this.radButtonRemove.TabIndex = 1;
      this.radButtonRemove.Text = "Remove selected rule";
      this.radButtonRemove.Click += new EventHandler(this.radButtonRemove_Click);
      this.radButtonAdd.Location = new Point(1, 25);
      this.radButtonAdd.Name = "radButtonAdd";
      this.radButtonAdd.Size = new Size((int) sbyte.MaxValue, 23);
      this.radButtonAdd.TabIndex = 0;
      this.radButtonAdd.Text = "Add new rule";
      this.radButtonAdd.Click += new EventHandler(this.radButtonAdd_Click);
      this.radLableFormatCellsWith.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.radLableFormatCellsWith.Location = new Point(267, 120);
      this.radLableFormatCellsWith.Name = "radLableFormatCellsWith";
      this.radLableFormatCellsWith.Size = new Size(95, 16);
      this.radLableFormatCellsWith.TabIndex = 22;
      this.radLableFormatCellsWith.Text = "Format cells with";
      this.radLabelFormat.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.radLabelFormat.Location = new Point(266, 284);
      this.radLabelFormat.Name = "radLabelFormat";
      this.radLabelFormat.Size = new Size(43, 16);
      this.radLabelFormat.TabIndex = 26;
      this.radLabelFormat.Text = "Format";
      this.radRadioButtonExpression.Location = new Point(268, 159);
      this.radRadioButtonExpression.Name = "radRadioButtonExpression";
      this.radRadioButtonExpression.Size = new Size(86, 18);
      this.radRadioButtonExpression.TabIndex = 7;
      this.radRadioButtonExpression.Text = "Expression";
      this.radRadioButtonExpression.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonExpression_ToggleStateChanged);
      this.radRadioButtonCondition.Location = new Point(268, 139);
      this.radRadioButtonCondition.Name = "radRadioButtonCondition";
      this.radRadioButtonCondition.Size = new Size(77, 18);
      this.radRadioButtonCondition.TabIndex = 6;
      this.radRadioButtonCondition.TabStop = true;
      this.radRadioButtonCondition.Text = "Condition";
      this.radRadioButtonCondition.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radRadioButtonCondition.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonCondition_ToggleStateChanged);
      this.radCheckBoxApplyOnSelectedRows.Enabled = false;
      this.radCheckBoxApplyOnSelectedRows.Location = new Point(268, 535);
      this.radCheckBoxApplyOnSelectedRows.Name = "radCheckBoxApplyOnSelectedRows";
      this.radCheckBoxApplyOnSelectedRows.RootElement.Enabled = false;
      this.radCheckBoxApplyOnSelectedRows.Size = new Size(232, 18);
      this.radCheckBoxApplyOnSelectedRows.TabIndex = 15;
      this.radCheckBoxApplyOnSelectedRows.Text = "Apply this formatting if the row is selected";
      this.radCheckBoxApplyOnSelectedRows.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxApplyOnSelectedRows_ToggleStateChanged);
      this.radButtonExpressionEditor.Enabled = false;
      this.radButtonExpressionEditor.Image = (Image) componentResourceManager.GetObject("radButtonExpressionEditor.Image");
      this.radButtonExpressionEditor.Location = new Point(268, 244);
      this.radButtonExpressionEditor.Name = "radButtonExpressionEditor";
      this.radButtonExpressionEditor.Padding = new Padding(6, 0, 0, 0);
      this.radButtonExpressionEditor.RootElement.Enabled = false;
      this.radButtonExpressionEditor.RootElement.Padding = new Padding(6, 0, 0, 0);
      this.radButtonExpressionEditor.Size = new Size(135, 24);
      this.radButtonExpressionEditor.TabIndex = 12;
      this.radButtonExpressionEditor.Text = "Expression editor";
      this.radButtonExpressionEditor.Click += new EventHandler(this.radButtonExpressionEditor_Click);
      this.radTextBoxExpression.AcceptsReturn = true;
      this.radTextBoxExpression.Enabled = false;
      this.radTextBoxExpression.Location = new Point(268, 185);
      this.radTextBoxExpression.Multiline = true;
      this.radTextBoxExpression.Name = "radTextBoxExpression";
      this.radTextBoxExpression.NullText = "Expression";
      this.radTextBoxExpression.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.radTextBoxExpression.RootElement.Enabled = false;
      this.radTextBoxExpression.RootElement.StretchVertically = true;
      this.radTextBoxExpression.Size = new Size(270, 55);
      this.radTextBoxExpression.TabIndex = 11;
      this.radTextBoxExpression.TextChanged += new EventHandler(this.radElement_Changed);
      this.radCheckBoxSortAlphabetically.Location = new Point(381, 62);
      this.radCheckBoxSortAlphabetically.Name = "radCheckBoxSortAlphabetically";
      this.radCheckBoxSortAlphabetically.Size = new Size(157, 18);
      this.radCheckBoxSortAlphabetically.TabIndex = 4;
      this.radCheckBoxSortAlphabetically.Text = "Sort columns alphabetically";
      this.radCheckBoxSortAlphabetically.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxSortAlphabetically_ToggleStateChanged);
      this.radLabelRules.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.radLabelRules.Location = new Point(1, 3);
      this.radLabelRules.Name = "radLabelRules";
      this.radLabelRules.Size = new Size(36, 16);
      this.radLabelRules.TabIndex = 19;
      this.radLabelRules.Text = "Rules";
      this.radLabelRuleName.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.radLabelRuleName.Location = new Point(265, 3);
      this.radLabelRuleName.Name = "radLabelRuleName";
      this.radLabelRuleName.Size = new Size(63, 16);
      this.radLabelRuleName.TabIndex = 20;
      this.radLabelRuleName.Text = "Rule name";
      this.radSeparator1.Location = new Point(268, 52);
      this.radSeparator1.Name = "radSeparator1";
      this.radSeparator1.ShadowOffset = new Point(0, 0);
      this.radSeparator1.Size = new Size(270, 4);
      this.radSeparator1.TabIndex = 35;
      this.radSeparator1.Text = "radSeparator1";
      this.radLabelRuleAppliesTo.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.radLabelRuleAppliesTo.Location = new Point(266, 62);
      this.radLabelRuleAppliesTo.Name = "radLabelRuleAppliesTo";
      this.radLabelRuleAppliesTo.Size = new Size(84, 16);
      this.radLabelRuleAppliesTo.TabIndex = 21;
      this.radLabelRuleAppliesTo.Text = "Rule applies to";
      this.radSeparator2.Location = new Point(268, 110);
      this.radSeparator2.Name = "radSeparator2";
      this.radSeparator2.ShadowOffset = new Point(0, 0);
      this.radSeparator2.Size = new Size(270, 4);
      this.radSeparator2.TabIndex = 36;
      this.radSeparator2.Text = "radSeparator2";
      this.radSeparator3.Location = new Point(268, 274);
      this.radSeparator3.Name = "radSeparator3";
      this.radSeparator3.ShadowOffset = new Point(0, 0);
      this.radSeparator3.Size = new Size(270, 4);
      this.radSeparator3.TabIndex = 37;
      this.radSeparator3.Text = "radSeparator3";
      this.radPropertyGridProperties.Location = new Point(268, 304);
      this.radPropertyGridProperties.Name = "radPropertyGridProperties";
      this.radPropertyGridProperties.Size = new Size(270, 205);
      this.radPropertyGridProperties.TabIndex = 13;
      this.radPropertyGridProperties.Text = "radPropertyGridProperties";
      this.radPropertyGridProperties.SelectedObjectChanged += new PropertyGridSelectedObjectChangedEventHandler(this.radPropertyGridProperties_SelectedObjectChanged);
      this.radPropertyGridProperties.PropertyValueChanged += new PropertyGridItemValueChangedEventHandler(this.radPropertyGridProperties_PropertyValueChanged);
      this.radPropertyGridProperties.CreateItemElement += new CreatePropertyGridItemElementEventHandler(this.radPropertyGridProperties_CreateItemElement);
      this.AcceptButton = (IButtonControl) this.radButtonOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.CancelButton = (IButtonControl) this.radButtonCancel;
      this.ClientSize = new Size(544, 602);
      this.Controls.Add((Control) this.radPropertyGridProperties);
      this.Controls.Add((Control) this.radCheckBoxApplyOnSelectedRows);
      this.Controls.Add((Control) this.radLabelFormat);
      this.Controls.Add((Control) this.radCheckBoxApplyToRow);
      this.Controls.Add((Control) this.radSeparator3);
      this.Controls.Add((Control) this.radTextBoxValue2);
      this.Controls.Add((Control) this.radTextBoxValue1);
      this.Controls.Add((Control) this.radLabelValue2);
      this.Controls.Add((Control) this.radLabelCellValue);
      this.Controls.Add((Control) this.radSeparator2);
      this.Controls.Add((Control) this.radRadioButtonExpression);
      this.Controls.Add((Control) this.radLabelValue1);
      this.Controls.Add((Control) this.radComboBoxType);
      this.Controls.Add((Control) this.radCheckBoxSortAlphabetically);
      this.Controls.Add((Control) this.radRadioButtonCondition);
      this.Controls.Add((Control) this.radLabelRuleAppliesTo);
      this.Controls.Add((Control) this.radComboBoxColumns);
      this.Controls.Add((Control) this.radLableFormatCellsWith);
      this.Controls.Add((Control) this.radSeparator1);
      this.Controls.Add((Control) this.radLabelRuleName);
      this.Controls.Add((Control) this.radLabelRules);
      this.Controls.Add((Control) this.radButtonAdd);
      this.Controls.Add((Control) this.radButtonRemove);
      this.Controls.Add((Control) this.radListBoxConditions);
      this.Controls.Add((Control) this.radTextBoxName);
      this.Controls.Add((Control) this.radButtonApply);
      this.Controls.Add((Control) this.radButtonCancel);
      this.Controls.Add((Control) this.radButtonOK);
      this.Controls.Add((Control) this.radTextBoxExpression);
      this.Controls.Add((Control) this.radButtonExpressionEditor);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = nameof (ConditionalFormattingForm);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Conditional Formatting Rules Manager";
      this.radComboBoxColumns.EndInit();
      this.radComboBoxType.EndInit();
      this.radLabelCellValue.EndInit();
      this.radLabelValue1.EndInit();
      this.radLabelValue2.EndInit();
      this.radTextBoxName.EndInit();
      this.radTextBoxValue1.EndInit();
      this.radTextBoxValue2.EndInit();
      this.radButtonOK.EndInit();
      this.radButtonCancel.EndInit();
      this.radListBoxConditions.EndInit();
      this.radButtonApply.EndInit();
      this.radCheckBoxApplyToRow.EndInit();
      this.radButtonRemove.EndInit();
      this.radButtonAdd.EndInit();
      this.radLableFormatCellsWith.EndInit();
      this.radLabelFormat.EndInit();
      this.radRadioButtonExpression.EndInit();
      this.radRadioButtonCondition.EndInit();
      this.radCheckBoxApplyOnSelectedRows.EndInit();
      this.radButtonExpressionEditor.EndInit();
      this.radTextBoxExpression.EndInit();
      this.radCheckBoxSortAlphabetically.EndInit();
      this.radLabelRules.EndInit();
      this.radLabelRuleName.EndInit();
      this.radSeparator1.EndInit();
      this.radLabelRuleAppliesTo.EndInit();
      this.radSeparator2.EndInit();
      this.radSeparator3.EndInit();
      this.radPropertyGridProperties.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private class ColumnFormattingInfo
    {
      private GridViewDataColumn column;
      private BaseFormattingObject formattingObject;

      public ColumnFormattingInfo(GridViewDataColumn column, BaseFormattingObject formattingObject)
      {
        this.column = column;
        this.formattingObject = formattingObject;
      }

      public GridViewDataColumn Column
      {
        get
        {
          return this.column;
        }
        set
        {
          this.column = value;
        }
      }

      public BaseFormattingObject FormattingObject
      {
        get
        {
          return this.formattingObject;
        }
        set
        {
          this.formattingObject = value;
        }
      }
    }
  }
}
