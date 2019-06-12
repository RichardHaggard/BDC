// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CompositeDataFilterForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class CompositeDataFilterForm : BaseCompositeFilterDialog
  {
    private GridViewDataColumn column;
    private CompositeFilterDescriptor compositeFilterDescriptor;
    private System.Type dataType;
    private IContainer components;
    private RadLabel radLabelFormTitle;
    private RadButton radButtonOK;
    private RadButton radButtonCancel;
    private RadDataFilter radDataFilter1;

    public CompositeDataFilterForm()
    {
      this.InitializeComponent();
      this.ShowInTaskbar = false;
      this.radDataFilter1.NodeAdded += new RadTreeView.RadTreeViewEventHandler(this.radDataFilter1_NodeAdded);
      this.radDataFilter1.Editing += new TreeNodeEditingEventHandler(this.radDataFilter1_Editing);
      this.radDataFilter1.NodeFormatting += new TreeNodeFormattingEventHandler(this.radDataFilter1_NodeFormatting);
    }

    private void radDataFilter1_NodeFormatting(object sender, TreeNodeFormattingEventArgs e)
    {
      DataFilterCriteriaElement nodeElement = e.NodeElement as DataFilterCriteriaElement;
      if (nodeElement == null)
        return;
      nodeElement.FieldElement.ChangeCursorOnMouseOver = false;
      nodeElement.FieldElement.Text = this.column.HeaderText;
    }

    private void radDataFilter1_Editing(object sender, TreeNodeEditingEventArgs e)
    {
      if (!(sender is DataFilterFieldEditorElement))
        return;
      e.Cancel = true;
    }

    private void radDataFilter1_NodeAdded(object sender, RadTreeViewEventArgs e)
    {
      DataFilterCriteriaNode node = e.Node as DataFilterCriteriaNode;
      if (node == null)
        return;
      node.Descriptor.PropertyName = this.column.Name;
    }

    public CompositeDataFilterForm(GridViewDataColumn dataColumn)
      : this(dataColumn, (FilterDescriptor) new CompositeFilterDescriptor())
    {
    }

    public CompositeDataFilterForm(GridViewDataColumn dataColumn, FilterDescriptor filterDescriptor)
      : this(dataColumn, filterDescriptor, false)
    {
    }

    public CompositeDataFilterForm(
      GridViewDataColumn dataColumn,
      FilterDescriptor filterDescriptor,
      bool useTypedEditors)
      : this()
    {
      this.Initialize(dataColumn, filterDescriptor, useTypedEditors);
    }

    public CompositeDataFilterForm(
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
      GridViewDateTimeColumn viewDateTimeColumn = dataColumn as GridViewDateTimeColumn;
      if (viewDateTimeColumn != null)
      {
        this.DataFilter.DataFilterElement.DefaultDateEditorFormat = viewDateTimeColumn.Format;
        this.DataFilter.DataFilterElement.DefaultCustomDateEditorFormat = viewDateTimeColumn.CustomFormat;
      }
      GridViewComboBoxColumn viewComboBoxColumn = dataColumn as GridViewComboBoxColumn;
      if (filterDescriptor == null)
      {
        System.Type dataType = dataColumn.DataType;
        if (viewComboBoxColumn != null)
          dataType = viewComboBoxColumn.FilteringMemberDataType;
        FilterOperator defaultFilterOperator = GridViewHelper.GetDefaultFilterOperator(dataType);
        filterDescriptor = viewDateTimeColumn == null ? new FilterDescriptor(dataColumn.Name, defaultFilterOperator, (object) null) : (FilterDescriptor) new DateFilterDescriptor(dataColumn.Name, defaultFilterOperator, new DateTime?(), false);
      }
      this.dataType = viewComboBoxColumn == null ? (!(dataColumn is GridViewCheckBoxColumn) ? dataColumn.DataType : typeof (bool)) : viewComboBoxColumn.FilteringMemberDataType;
      this.column = dataColumn;
      this.radDataFilter1.Descriptors.Clear();
      if (viewComboBoxColumn != null)
        this.radDataFilter1.Descriptors.Add((RadItem) new DataFilterComboDescriptorItem(this.column.Name, this.DataType, viewComboBoxColumn.DataSource, viewComboBoxColumn.DisplayMember, viewComboBoxColumn.ValueMember));
      else if (useTypedEditors)
        this.radDataFilter1.Descriptors.Add((RadItem) new DataFilterDescriptorItem(this.column.Name, this.DataType));
      else
        this.radDataFilter1.Descriptors.Add((RadItem) new DataFilterDescriptorItem(this.column.Name, typeof (string)));
      this.InitialSetFilterDescriptor(filterDescriptor);
      this.LocalizeForm();
    }

    private void LocalizeForm()
    {
      RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
      this.Text = string.Format(currentProvider.GetLocalizedString("CustomFilterDialogCaption"), (object) this.column.HeaderText);
      this.radLabelFormTitle.Text = currentProvider.GetLocalizedString("CustomFilterDialogLabel");
      this.radButtonOK.Text = currentProvider.GetLocalizedString("CustomFilterDialogBtnOk");
      this.radButtonCancel.Text = currentProvider.GetLocalizedString("CustomFilterDialogBtnCancel");
    }

    public RadDataFilter DataFilter
    {
      get
      {
        return this.radDataFilter1;
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
      CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
      DataFilterGroupNode node1 = this.radDataFilter1.DataFilterElement.Nodes[0] as DataFilterGroupNode;
      filterDescriptor.LogicalOperator = node1.LogicalOperator;
      foreach (RadTreeNode node2 in (Collection<RadTreeNode>) node1.Nodes)
      {
        DataFilterGroupNode dataFilterGroupNode = node2 as DataFilterGroupNode;
        if (dataFilterGroupNode != null && dataFilterGroupNode.CompositeDescriptor.FilterDescriptors.Count > 0)
          filterDescriptor.FilterDescriptors.Add((FilterDescriptor) dataFilterGroupNode.CompositeDescriptor);
        DataFilterCriteriaNode filterCriteriaNode = node2 as DataFilterCriteriaNode;
        if (filterCriteriaNode != null && filterCriteriaNode.FilterOperator != FilterOperator.None)
          filterDescriptor.FilterDescriptors.Add(filterCriteriaNode.Descriptor);
      }
      if (filterDescriptor.FilterDescriptors.Count == 0)
        filterDescriptor = (CompositeFilterDescriptor) null;
      else if (filterDescriptor.FilterDescriptors.Count == 1)
        return filterDescriptor.FilterDescriptors[0];
      return (FilterDescriptor) filterDescriptor;
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
      }
      else
      {
        if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
          return;
        this.SuspendLayout();
        this.Width += 40;
        this.Height += 35;
        this.radDataFilter1.Width += 40;
        this.radDataFilter1.Height += 20;
        this.radButtonOK.Top += 20;
        this.radButtonOK.Left += 20;
        this.radButtonCancel.Top += 20;
        this.radButtonCancel.Left += 20;
        this.radButtonCancel.Width += 20;
        this.ResumeLayout();
      }
    }

    private void radButtonOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      if (this.DialogResult != DialogResult.OK)
        return;
      this.UpdateValues();
      RadGridLocalizationProvider currentProvider = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider;
      if (GridFilterCellElement.ValidateUserFilter(this.FilterDescriptor))
        return;
      e.Cancel = true;
      string themeName = RadMessageBox.Instance.ThemeName;
      RadMessageBox.SetThemeName(this.ThemeName);
      RadMessageBox.Instance.RightToLeft = this.RightToLeft;
      int num = (int) RadMessageBox.Show(currentProvider.GetLocalizedString("CompositeFilterFormInvalidFilter"), currentProvider.GetLocalizedString("CompositeFilterFormErrorCaption"), MessageBoxButtons.OK, RadMessageIcon.Error);
      RadMessageBox.SetThemeName(themeName);
    }

    private void UpdateValues()
    {
      foreach (RadTreeNode node in this.radDataFilter1.DataFilterElement.GetNodes())
      {
        DataFilterCriteriaNode filterCriteriaNode = node as DataFilterCriteriaNode;
        if (filterCriteriaNode != null)
          this.UpdateEditorValue(filterCriteriaNode.Descriptor);
      }
    }

    protected bool IsEditableFilterOperator(FilterOperator filterOperator)
    {
      if (filterOperator != FilterOperator.None && filterOperator != FilterOperator.IsNotNull)
        return filterOperator != FilterOperator.IsNull;
      return false;
    }

    protected virtual void UpdateEditorValue(FilterDescriptor descriptor)
    {
      object obj = descriptor.Value;
      if (!this.IsEditableFilterOperator(descriptor.Operator) || obj != null && !string.IsNullOrEmpty(obj.ToString()))
        return;
      descriptor.Value = (object) null;
    }

    private void radButtonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void InitialSetFilterDescriptor(FilterDescriptor value)
    {
      CompositeFilterDescriptor filterDescriptor1 = value as CompositeFilterDescriptor;
      if (filterDescriptor1 == null)
      {
        filterDescriptor1 = new CompositeFilterDescriptor();
        filterDescriptor1.FilterDescriptors.Add(value);
      }
      if (filterDescriptor1.FilterDescriptors.Count == 0)
        filterDescriptor1.FilterDescriptors.Add(new FilterDescriptor(this.column.Name, FilterOperator.None, (object) null));
      this.compositeFilterDescriptor = filterDescriptor1;
      DataFilterRootNode node = this.radDataFilter1.Nodes[0] as DataFilterRootNode;
      this.radDataFilter1.DataFilterElement.ClearChildNodes((DataFilterGroupNode) node);
      node.LogicalOperator = filterDescriptor1.LogicalOperator;
      foreach (FilterDescriptor filterDescriptor2 in (Collection<FilterDescriptor>) filterDescriptor1.FilterDescriptors)
        node.Filters.Add(filterDescriptor2);
      foreach (FilterDescriptor filter in (Collection<FilterDescriptor>) node.Filters)
        this.radDataFilter1.DataFilterElement.AddChildNodes(filter, (RadTreeNode) node, false);
    }

    private void SetTheme(string themeName)
    {
      if (string.IsNullOrEmpty(themeName))
        themeName = this.ThemeName;
      else
        this.ThemeName = themeName;
      foreach (RadControl control in (ArrangedElementCollection) this.Controls)
        control.ThemeName = themeName;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radLabelFormTitle = new RadLabel();
      this.radButtonOK = new RadButton();
      this.radButtonCancel = new RadButton();
      this.radDataFilter1 = new RadDataFilter();
      this.radLabelFormTitle.BeginInit();
      this.radButtonOK.BeginInit();
      this.radButtonCancel.BeginInit();
      this.radDataFilter1.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radLabelFormTitle.Location = new Point(4, 12);
      this.radLabelFormTitle.Name = "radLabelFormTitle";
      this.radLabelFormTitle.Size = new Size(96, 18);
      this.radLabelFormTitle.TabIndex = 2;
      this.radLabelFormTitle.Text = "Show rows where:";
      this.radButtonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonOK.DialogResult = DialogResult.OK;
      this.radButtonOK.Location = new Point(288, 214);
      this.radButtonOK.Name = "radButtonOK";
      this.radButtonOK.Size = new Size(75, 23);
      this.radButtonOK.TabIndex = 6;
      this.radButtonOK.Text = "OK";
      this.radButtonOK.Click += new EventHandler(this.radButtonOK_Click);
      this.radButtonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonCancel.DialogResult = DialogResult.Cancel;
      this.radButtonCancel.Location = new Point(372, 214);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(75, 23);
      this.radButtonCancel.TabIndex = 7;
      this.radButtonCancel.Text = "Cancel";
      this.radButtonCancel.Click += new EventHandler(this.radButtonCancel_Click);
      this.radDataFilter1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radDataFilter1.Location = new Point(4, 36);
      this.radDataFilter1.Name = "radDataFilter1";
      this.radDataFilter1.Size = new Size(443, 172);
      this.radDataFilter1.TabIndex = 10;
      this.radDataFilter1.Text = "radDataFilter1";
      this.AcceptButton = (IButtonControl) this.radButtonOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.CancelButton = (IButtonControl) this.radButtonCancel;
      this.ClientSize = new Size(452, 240);
      this.Controls.Add((Control) this.radDataFilter1);
      this.Controls.Add((Control) this.radButtonCancel);
      this.Controls.Add((Control) this.radButtonOK);
      this.Controls.Add((Control) this.radLabelFormTitle);
      this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimumSize = new Size(410, 220);
      this.Name = nameof (CompositeDataFilterForm);
      this.RootElement.ApplyShapeToControl = true;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "ComplexFilterForm";
      this.radLabelFormTitle.EndInit();
      this.radButtonOK.EndInit();
      this.radButtonCancel.EndInit();
      this.radDataFilter1.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
