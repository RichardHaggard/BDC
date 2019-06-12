// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgCopySections
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class dlgCopySections : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "dlgCopySections";
    private string m_strBldgID;

    public dlgCopySections()
    {
      this.Load += new EventHandler(this.dlgCopySections_Load);
      this.InitializeComponent();
    }

    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual TextBox txtInstructions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtNewName
    {
      get
      {
        return this._txtNewName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtNewName_TextChanged);
        TextBox txtNewName1 = this._txtNewName;
        if (txtNewName1 != null)
          txtNewName1.TextChanged -= eventHandler;
        this._txtNewName = value;
        TextBox txtNewName2 = this._txtNewName;
        if (txtNewName2 == null)
          return;
        txtNewName2.TextChanged += eventHandler;
      }
    }

    public virtual Label lblReplacementName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblOriginalName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboNameSource { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadioButton rbInventoryCopyBuilding
    {
      get
      {
        return this._rbInventoryCopyBuilding;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.rbInventoryCopySection_CheckedChanged);
        RadioButton inventoryCopyBuilding1 = this._rbInventoryCopyBuilding;
        if (inventoryCopyBuilding1 != null)
          inventoryCopyBuilding1.CheckedChanged -= eventHandler;
        this._rbInventoryCopyBuilding = value;
        RadioButton inventoryCopyBuilding2 = this._rbInventoryCopyBuilding;
        if (inventoryCopyBuilding2 == null)
          return;
        inventoryCopyBuilding2.CheckedChanged += eventHandler;
      }
    }

    internal virtual ComboBox cboBuildingDest
    {
      get
      {
        return this._cboBuildingDest;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboBuildingDest_SelectedValueChanged);
        ComboBox cboBuildingDest1 = this._cboBuildingDest;
        if (cboBuildingDest1 != null)
          cboBuildingDest1.SelectedValueChanged -= eventHandler;
        this._cboBuildingDest = value;
        ComboBox cboBuildingDest2 = this._cboBuildingDest;
        if (cboBuildingDest2 == null)
          return;
        cboBuildingDest2.SelectedValueChanged += eventHandler;
      }
    }

    internal virtual RadioButton rbInventoryCopySection
    {
      get
      {
        return this._rbInventoryCopySection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.rbInventoryCopySection_CheckedChanged);
        RadioButton inventoryCopySection1 = this._rbInventoryCopySection;
        if (inventoryCopySection1 != null)
          inventoryCopySection1.CheckedChanged -= eventHandler;
        this._rbInventoryCopySection = value;
        RadioButton inventoryCopySection2 = this._rbInventoryCopySection;
        if (inventoryCopySection2 == null)
          return;
        inventoryCopySection2.CheckedChanged += eventHandler;
      }
    }

    public virtual TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboBuildingSource
    {
      get
      {
        return this._cboBuildingSource;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboBuildingSource_SelectedIndexChanged);
        ComboBox cboBuildingSource1 = this._cboBuildingSource;
        if (cboBuildingSource1 != null)
          cboBuildingSource1.SelectedIndexChanged -= eventHandler;
        this._cboBuildingSource = value;
        ComboBox cboBuildingSource2 = this._cboBuildingSource;
        if (cboBuildingSource2 == null)
          return;
        cboBuildingSource2.SelectedIndexChanged += eventHandler;
      }
    }

    public virtual Label lblBuildingSource { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblBuildingDest { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblCopySections { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (dlgCopySections));
      this.ToolTip1 = new ToolTip(this.components);
      this.txtInstructions = new TextBox();
      this.txtNewName = new TextBox();
      this.lblReplacementName = new Label();
      this.lblOriginalName = new Label();
      this.cboNameSource = new ComboBox();
      this.HelpProvider = new HelpProvider();
      this.tblCopySections = new TableLayoutPanel();
      this.tblCopyBuilding = new TableLayoutPanel();
      this.cboBuildingDest = new ComboBox();
      this.TextBox2 = new TextBox();
      this.cboBuildingSource = new ComboBox();
      this.lblBuildingDest = new Label();
      this.lblBuildingSource = new Label();
      this.rbInventoryCopySection = new RadioButton();
      this.cmdCancel = new Button();
      this.cmdOK = new Button();
      this.rbInventoryCopyBuilding = new RadioButton();
      this.chkCopyComments = new CheckBox();
      this.chkEstimateDates = new CheckBox();
      this.frmOptions = new GroupBox();
      this.tblCopySections.SuspendLayout();
      this.tblCopyBuilding.SuspendLayout();
      this.frmOptions.SuspendLayout();
      this.SuspendLayout();
      this.txtInstructions.AcceptsReturn = true;
      this.txtInstructions.BackColor = SystemColors.Window;
      this.tblCopySections.SetColumnSpan((Control) this.txtInstructions, 2);
      this.txtInstructions.Cursor = Cursors.IBeam;
      this.txtInstructions.ForeColor = SystemColors.WindowText;
      this.txtInstructions.Location = new Point(3, 3);
      this.txtInstructions.MaxLength = 0;
      this.txtInstructions.Multiline = true;
      this.txtInstructions.Name = "txtInstructions";
      this.txtInstructions.ReadOnly = true;
      this.txtInstructions.RightToLeft = RightToLeft.No;
      this.tblCopySections.SetRowSpan((Control) this.txtInstructions, 2);
      this.txtInstructions.Size = new Size(524, 65);
      this.txtInstructions.TabIndex = 3;
      this.txtInstructions.Text = componentResourceManager.GetString("txtInstructions.Text");
      this.txtNewName.AcceptsReturn = true;
      this.txtNewName.BackColor = SystemColors.Window;
      this.txtNewName.Cursor = Cursors.IBeam;
      this.txtNewName.ForeColor = SystemColors.WindowText;
      this.txtNewName.Location = new Point(113, 101);
      this.txtNewName.MaxLength = 0;
      this.txtNewName.Name = "txtNewName";
      this.txtNewName.RightToLeft = RightToLeft.No;
      this.txtNewName.Size = new Size(404, 20);
      this.txtNewName.TabIndex = 2;
      this.lblReplacementName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.lblReplacementName.AutoSize = true;
      this.lblReplacementName.BackColor = SystemColors.Control;
      this.lblReplacementName.Cursor = Cursors.Default;
      this.lblReplacementName.ForeColor = SystemColors.ControlText;
      this.lblReplacementName.Location = new Point(3, 98);
      this.lblReplacementName.Name = "lblReplacementName";
      this.lblReplacementName.RightToLeft = RightToLeft.No;
      this.lblReplacementName.Size = new Size(104, 26);
      this.lblReplacementName.TabIndex = 4;
      this.lblReplacementName.Text = "Replacement Name:";
      this.lblReplacementName.TextAlign = ContentAlignment.MiddleRight;
      this.lblOriginalName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.lblOriginalName.AutoSize = true;
      this.lblOriginalName.BackColor = SystemColors.Control;
      this.lblOriginalName.Cursor = Cursors.Default;
      this.lblOriginalName.ForeColor = SystemColors.ControlText;
      this.lblOriginalName.Location = new Point(30, 71);
      this.lblOriginalName.Name = "lblOriginalName";
      this.lblOriginalName.RightToLeft = RightToLeft.No;
      this.lblOriginalName.Size = new Size(77, 27);
      this.lblOriginalName.TabIndex = 1;
      this.lblOriginalName.Text = "Name to Copy:";
      this.lblOriginalName.TextAlign = ContentAlignment.MiddleRight;
      this.cboNameSource.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboNameSource.Location = new Point(113, 74);
      this.cboNameSource.Name = "cboNameSource";
      this.cboNameSource.Size = new Size(404, 21);
      this.cboNameSource.TabIndex = 10;
      this.tblCopySections.Anchor = AnchorStyles.None;
      this.tblCopySections.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tblCopySections.ColumnCount = 2;
      this.tblCopySections.ColumnStyles.Add(new ColumnStyle());
      this.tblCopySections.ColumnStyles.Add(new ColumnStyle());
      this.tblCopySections.Controls.Add((Control) this.txtInstructions, 0, 0);
      this.tblCopySections.Controls.Add((Control) this.cboNameSource, 1, 2);
      this.tblCopySections.Controls.Add((Control) this.txtNewName, 1, 3);
      this.tblCopySections.Controls.Add((Control) this.lblReplacementName, 0, 3);
      this.tblCopySections.Controls.Add((Control) this.lblOriginalName, 0, 2);
      this.tblCopySections.Controls.Add((Control) this.frmOptions, 1, 4);
      this.tblCopySections.Location = new Point(12, 50);
      this.tblCopySections.Name = "tblCopySections";
      this.tblCopySections.RowCount = 5;
      this.tblCopySections.RowStyles.Add(new RowStyle());
      this.tblCopySections.RowStyles.Add(new RowStyle());
      this.tblCopySections.RowStyles.Add(new RowStyle());
      this.tblCopySections.RowStyles.Add(new RowStyle());
      this.tblCopySections.RowStyles.Add(new RowStyle());
      this.tblCopySections.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tblCopySections.Size = new Size(530, 233);
      this.tblCopySections.TabIndex = 11;
      this.tblCopyBuilding.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tblCopyBuilding.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tblCopyBuilding.ColumnCount = 2;
      this.tblCopyBuilding.ColumnStyles.Add(new ColumnStyle());
      this.tblCopyBuilding.ColumnStyles.Add(new ColumnStyle());
      this.tblCopyBuilding.Controls.Add((Control) this.cboBuildingDest, 1, 3);
      this.tblCopyBuilding.Controls.Add((Control) this.TextBox2, 0, 0);
      this.tblCopyBuilding.Controls.Add((Control) this.cboBuildingSource, 1, 2);
      this.tblCopyBuilding.Controls.Add((Control) this.lblBuildingDest, 0, 3);
      this.tblCopyBuilding.Controls.Add((Control) this.lblBuildingSource, 0, 2);
      this.tblCopyBuilding.Location = new Point(12, 50);
      this.tblCopyBuilding.Name = "tblCopyBuilding";
      this.tblCopyBuilding.RowCount = 4;
      this.tblCopyBuilding.RowStyles.Add(new RowStyle());
      this.tblCopyBuilding.RowStyles.Add(new RowStyle());
      this.tblCopyBuilding.RowStyles.Add(new RowStyle());
      this.tblCopyBuilding.RowStyles.Add(new RowStyle());
      this.tblCopyBuilding.Size = new Size(530, 161);
      this.tblCopyBuilding.TabIndex = 12;
      this.cboBuildingDest.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboBuildingDest.DropDownWidth = 414;
      this.cboBuildingDest.Location = new Point(112, 101);
      this.cboBuildingDest.Name = "cboBuildingDest";
      this.cboBuildingDest.Size = new Size(405, 21);
      this.cboBuildingDest.TabIndex = 11;
      this.TextBox2.AcceptsReturn = true;
      this.TextBox2.BackColor = SystemColors.Window;
      this.tblCopyBuilding.SetColumnSpan((Control) this.TextBox2, 2);
      this.TextBox2.Cursor = Cursors.IBeam;
      this.TextBox2.ForeColor = SystemColors.WindowText;
      this.TextBox2.Location = new Point(3, 3);
      this.TextBox2.MaxLength = 0;
      this.TextBox2.Multiline = true;
      this.TextBox2.Name = "TextBox2";
      this.TextBox2.ReadOnly = true;
      this.TextBox2.RightToLeft = RightToLeft.No;
      this.tblCopyBuilding.SetRowSpan((Control) this.TextBox2, 2);
      this.TextBox2.Size = new Size(524, 65);
      this.TextBox2.TabIndex = 3;
      this.TextBox2.Text = "Use the Building Copy tool to copy inventory from one building to another.";
      this.cboBuildingSource.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboBuildingSource.DropDownWidth = 414;
      this.cboBuildingSource.Location = new Point(112, 74);
      this.cboBuildingSource.Name = "cboBuildingSource";
      this.cboBuildingSource.Size = new Size(405, 21);
      this.cboBuildingSource.TabIndex = 10;
      this.lblBuildingDest.AutoSize = true;
      this.lblBuildingDest.BackColor = SystemColors.Control;
      this.lblBuildingDest.Cursor = Cursors.Default;
      this.lblBuildingDest.ForeColor = SystemColors.ControlText;
      this.lblBuildingDest.Location = new Point(3, 98);
      this.lblBuildingDest.Name = "lblBuildingDest";
      this.lblBuildingDest.RightToLeft = RightToLeft.No;
      this.lblBuildingDest.Size = new Size(103, 13);
      this.lblBuildingDest.TabIndex = 4;
      this.lblBuildingDest.Text = "Destination Building:";
      this.lblBuildingDest.TextAlign = ContentAlignment.MiddleRight;
      this.lblBuildingSource.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.lblBuildingSource.AutoSize = true;
      this.lblBuildingSource.BackColor = SystemColors.Control;
      this.lblBuildingSource.Cursor = Cursors.Default;
      this.lblBuildingSource.ForeColor = SystemColors.ControlText;
      this.lblBuildingSource.Location = new Point(22, 71);
      this.lblBuildingSource.Name = "lblBuildingSource";
      this.lblBuildingSource.RightToLeft = RightToLeft.No;
      this.lblBuildingSource.Size = new Size(84, 27);
      this.lblBuildingSource.TabIndex = 1;
      this.lblBuildingSource.Text = "Source Building:";
      this.lblBuildingSource.TextAlign = ContentAlignment.MiddleRight;
      this.rbInventoryCopySection.AutoSize = true;
      this.rbInventoryCopySection.Location = new Point(171, 20);
      this.rbInventoryCopySection.Name = "rbInventoryCopySection";
      this.rbInventoryCopySection.Size = new Size(88, 17);
      this.rbInventoryCopySection.TabIndex = 13;
      this.rbInventoryCopySection.Text = "Copy Section";
      this.rbInventoryCopySection.UseVisualStyleBackColor = true;
      this.cmdCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdCancel.BackColor = SystemColors.Control;
      this.cmdCancel.Cursor = Cursors.Default;
      this.cmdCancel.FlatStyle = FlatStyle.System;
      this.cmdCancel.ForeColor = SystemColors.ControlText;
      this.cmdCancel.Location = new Point(450, 13);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.RightToLeft = RightToLeft.No;
      this.cmdCancel.Size = new Size(92, 31);
      this.cmdCancel.TabIndex = 10;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdOK.BackColor = SystemColors.Control;
      this.cmdOK.Cursor = Cursors.Default;
      this.cmdOK.FlatStyle = FlatStyle.System;
      this.cmdOK.ForeColor = SystemColors.ControlText;
      this.cmdOK.Location = new Point(339, 13);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.RightToLeft = RightToLeft.No;
      this.cmdOK.Size = new Size(92, 31);
      this.cmdOK.TabIndex = 11;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = false;
      this.rbInventoryCopyBuilding.AutoSize = true;
      this.rbInventoryCopyBuilding.Checked = true;
      this.rbInventoryCopyBuilding.Location = new Point(54, 20);
      this.rbInventoryCopyBuilding.Name = "rbInventoryCopyBuilding";
      this.rbInventoryCopyBuilding.Size = new Size(89, 17);
      this.rbInventoryCopyBuilding.TabIndex = 12;
      this.rbInventoryCopyBuilding.TabStop = true;
      this.rbInventoryCopyBuilding.Text = "Copy Building";
      this.rbInventoryCopyBuilding.UseVisualStyleBackColor = true;
      this.chkCopyComments.Cursor = Cursors.Default;
      this.chkCopyComments.FlatStyle = FlatStyle.System;
      this.chkCopyComments.Location = new Point(32, 16);
      this.chkCopyComments.Name = "chkCopyComments";
      this.chkCopyComments.RightToLeft = RightToLeft.No;
      this.chkCopyComments.Size = new Size(105, 25);
      this.chkCopyComments.TabIndex = 6;
      this.chkCopyComments.Text = "Copy Comments";
      this.chkEstimateDates.BackColor = SystemColors.Control;
      this.chkEstimateDates.Cursor = Cursors.Default;
      this.chkEstimateDates.FlatStyle = FlatStyle.System;
      this.chkEstimateDates.Location = new Point(32, 40);
      this.chkEstimateDates.Name = "chkEstimateDates";
      this.chkEstimateDates.RightToLeft = RightToLeft.No;
      this.chkEstimateDates.Size = new Size(344, 17);
      this.chkEstimateDates.TabIndex = 7;
      this.chkEstimateDates.Text = "Set Installed Date as \"Estimated\" for all sections being copied";
      this.chkEstimateDates.UseVisualStyleBackColor = false;
      this.frmOptions.BackColor = SystemColors.Control;
      this.tblCopySections.SetColumnSpan((Control) this.frmOptions, 2);
      this.frmOptions.Controls.Add((Control) this.chkEstimateDates);
      this.frmOptions.Controls.Add((Control) this.chkCopyComments);
      this.frmOptions.FlatStyle = FlatStyle.System;
      this.frmOptions.ForeColor = SystemColors.ControlText;
      this.frmOptions.Location = new Point(3, (int) sbyte.MaxValue);
      this.frmOptions.Name = "frmOptions";
      this.frmOptions.RightToLeft = RightToLeft.No;
      this.frmOptions.Size = new Size(524, 103);
      this.frmOptions.TabIndex = 5;
      this.frmOptions.TabStop = false;
      this.frmOptions.Text = "Options";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(564, 329);
      this.ControlBox = false;
      this.Controls.Add((Control) this.tblCopySections);
      this.Controls.Add((Control) this.tblCopyBuilding);
      this.Controls.Add((Control) this.rbInventoryCopyBuilding);
      this.Controls.Add((Control) this.rbInventoryCopySection);
      this.Controls.Add((Control) this.cmdCancel);
      this.Controls.Add((Control) this.cmdOK);
      this.Cursor = Cursors.Default;
      this.Location = new Point(184, 250);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(580, 345);
      this.Name = nameof (dlgCopySections);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Section Copy Tool";
      this.tblCopySections.ResumeLayout(false);
      this.tblCopySections.PerformLayout();
      this.tblCopyBuilding.ResumeLayout(false);
      this.tblCopyBuilding.PerformLayout();
      this.frmOptions.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel tblCopyBuilding { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox TextBox2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button cmdCancel
    {
      get
      {
        return this._cmdCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCancel_Click);
        Button cmdCancel1 = this._cmdCancel;
        if (cmdCancel1 != null)
          cmdCancel1.Click -= eventHandler;
        this._cmdCancel = value;
        Button cmdCancel2 = this._cmdCancel;
        if (cmdCancel2 == null)
          return;
        cmdCancel2.Click += eventHandler;
      }
    }

    public virtual Button cmdOK
    {
      get
      {
        return this._cmdOK;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdOK_Click);
        Button cmdOk1 = this._cmdOK;
        if (cmdOk1 != null)
          cmdOk1.Click -= eventHandler;
        this._cmdOK = value;
        Button cmdOk2 = this._cmdOK;
        if (cmdOk2 == null)
          return;
        cmdOk2.Click += eventHandler;
      }
    }

    public virtual GroupBox frmOptions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual CheckBox chkEstimateDates { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual CheckBox chkCopyComments { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public DialogResult CopySections(string BuildingID)
    {
      DialogResult dialogResult;
      try
      {
        this.m_strBldgID = BuildingID;
        this.cboNameSource.ValueMember = "SEC_Name";
        this.cboNameSource.DisplayMember = "SEC_Name";
        this.cboNameSource.DataSource = (object) mdUtility.DB.GetDataTable("SELECT [SEC_Name] FROM [qrySectionNamesByBldg] WHERE [Facility_ID]={" + BuildingID + "}");
        this.cboBuildingSource.ValueMember = "Facility_ID";
        this.cboBuildingSource.DisplayMember = "BldgLabel";
        this.cboBuildingSource.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM [Buildings] ORDER BY [BldgLabel]");
        dialogResult = this.ShowDialog();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (dlgCopySections), nameof (CopySections));
        ProjectData.ClearProjectError();
      }
      return dialogResult;
    }

    private void cmdCancel_Click(object eventSender, EventArgs eventArgs)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void cmdOK_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (!this.OKToCopy())
          return;
        mdUtility.DB.BeginTransaction();
        if (this.rbInventoryCopySection.Checked)
        {
          DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM qryBldgInventoryHierarchy WHERE [Facility_ID]={" + this.m_strBldgID + "} AND SEC_Name='" + Microsoft.VisualBasic.Strings.Replace(this.cboNameSource.Text, "'", "''", 1, -1, CompareMethod.Binary) + "' AND (BRED_Status <> 'D' OR BRED_Status IS NULL)");
          DataTable tableSchema = mdUtility.DB.GetTableSchema("Component_Section");
          try
          {
            foreach (DataRow row1 in dataTable.Rows)
            {
              DataRow row2 = tableSchema.NewRow();
              DataRow dataRow = row2;
              dataRow["BRED_Status"] = (object) "N";
              dataRow["SEC_ID"] = (object) mdUtility.GetUniqueID();
              dataRow["SEC_SYS_COMP_ID"] = RuntimeHelpers.GetObjectValue(row1["SYS_COMP_ID"]);
              dataRow["SEC_CMC_LINK"] = RuntimeHelpers.GetObjectValue(row1["SEC_CMC_LINK"]);
              dataRow["SEC_QTY"] = RuntimeHelpers.GetObjectValue(row1["SEC_QTY"]);
              dataRow["SEC_YEAR_BUILT"] = RuntimeHelpers.GetObjectValue(row1["SEC_YEAR_BUILT"]);
              dataRow["SEC_DATE_PAINTED"] = RuntimeHelpers.GetObjectValue(row1["SEC_DATE_PAINTED"]);
              dataRow["SEC_NAME"] = (object) this.txtNewName.Text;
              dataRow["SEC_PAINT"] = RuntimeHelpers.GetObjectValue(row1["SEC_PAINT"]);
              dataRow["SEC_PAINT_LINK"] = RuntimeHelpers.GetObjectValue(row1["SEC_PAINT_LINK"]);
              if (this.chkCopyComments.CheckState == CheckState.Checked)
                dataRow["SEC_COMMENTS"] = RuntimeHelpers.GetObjectValue(row1["SEC_COMMENTS"]);
              dataRow["SEC_DATE_SOURCE"] = this.chkEstimateDates.CheckState != CheckState.Checked ? RuntimeHelpers.GetObjectValue(row1["SEC_DATE_SOURCE"]) : (object) "Estimated";
              tableSchema.Rows.Add(row2);
              mdUtility.DB.SaveDataTable(ref tableSchema, "SELECT * FROM Component_Section");
              string str1 = Section.SectionLabel(row2["SEC_ID"].ToString());
              frmMain fMainForm = mdUtility.fMainForm;
              string str2 = row1["SYS_COMP_ID"].ToString();
              ref string local1 = ref str2;
              string str3 = row2["SEC_ID"].ToString();
              ref string local2 = ref str3;
              ref string local3 = ref str1;
              fMainForm.SectionAdded(ref local1, ref local2, ref local3);
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
        }
        else
          this.CopyBuildingInventory(this.cboBuildingSource.SelectedValue.ToString(), this.cboBuildingDest.SelectedValue.ToString());
        mdUtility.DB.CommitTransaction();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        mdUtility.DB.RollbackTransaction();
        mdUtility.Errorhandler(ex2, nameof (dlgCopySections), nameof (cmdOK_Click));
        ProjectData.ClearProjectError();
      }
    }

    private bool OKToCopy()
    {
      bool flag;
      try
      {
        if (this.rbInventoryCopySection.Checked)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(this.cboNameSource.Text), "", false) == 0)
          {
            int num = (int) Interaction.MsgBox((object) "You must choose an existing section name before proceeding.", MsgBoxStyle.Critical, (object) null);
            flag = false;
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(this.txtNewName.Text), "", false) == 0)
          {
            int num = (int) Interaction.MsgBox((object) "You must enter a replacement name before proceeding.", MsgBoxStyle.Critical, (object) null);
            flag = false;
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(this.txtNewName.Text), Microsoft.VisualBasic.Strings.Trim(this.cboNameSource.Text), false) == 0)
          {
            int num = (int) Interaction.MsgBox((object) "The Name to copy and the replacement name cannot match.", MsgBoxStyle.Critical, (object) null);
            flag = false;
          }
          else
            flag = true;
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(this.cboBuildingSource.Text), "", false) == 0)
        {
          int num = (int) Interaction.MsgBox((object) "You must select an existing [source] building before proceeding.", MsgBoxStyle.Critical, (object) null);
          flag = false;
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(this.cboBuildingDest.Text), "", false) == 0)
        {
          int num = (int) Interaction.MsgBox((object) "You must select an existing [destination] building before proceeding.", MsgBoxStyle.Critical, (object) null);
          flag = false;
        }
        else
          flag = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (dlgCopySections), nameof (OKToCopy));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private void dlgCopySections_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "Section Copy Tool");
        this.txtNewName.MaxLength = mdUtility.DB.GetTableSchema("Component_Section").Columns["SEC_NAME"].MaxLength;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (dlgCopySections), "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void rbInventoryCopySection_CheckedChanged(object sender, EventArgs e)
    {
      if (this.rbInventoryCopySection.Checked)
      {
        this.tblCopySections.Visible = true;
        this.tblCopyBuilding.Visible = false;
        this.checkSectionName();
      }
      else
      {
        this.tblCopySections.Visible = false;
        this.tblCopyBuilding.Visible = true;
        this.checkBuildingDest();
      }
    }

    private void cboBuildingSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.cboBuildingSource.SelectedValue, (object) null, false))
      {
        this.cboBuildingDest.Enabled = true;
        this.cboBuildingDest.ValueMember = "Facility_ID";
        this.cboBuildingDest.DisplayMember = "BldgLabel";
        this.cboBuildingDest.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM [Buildings] WHERE Facility_ID <> {" + this.cboBuildingSource.SelectedValue.ToString() + "} ORDER BY [BldgLabel]");
      }
      else
      {
        this.cboBuildingDest.DataSource = (object) null;
        this.cboBuildingDest.Enabled = false;
      }
    }

    private void checkBuildingDest()
    {
      if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual(this.cboBuildingDest.SelectedValue, (object) null, false), (object) this.rbInventoryCopyBuilding.Checked)))
        this.cmdOK.Enabled = true;
      else
        this.cmdOK.Enabled = false;
    }

    private void cboBuildingDest_SelectedValueChanged(object sender, EventArgs e)
    {
      this.checkBuildingDest();
    }

    private void checkSectionName()
    {
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtNewName.Text, "", false) > 0U & this.rbInventoryCopySection.Checked)
        this.cmdOK.Enabled = true;
      else
        this.cmdOK.Enabled = false;
    }

    private void txtNewName_TextChanged(object sender, EventArgs e)
    {
      this.checkSectionName();
    }

    private void CopyBuildingInventory(string SourceBuildingID, string DestinationBuildingID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM qryCopyBuildingSections WHERE [Facility_ID]={" + this.cboBuildingSource.SelectedValue.ToString() + "} AND (BRED_Status <> 'D' OR BRED_Status IS NULL)");
      DataTable tableSchema = mdUtility.DB.GetTableSchema("Component_Section");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM [Building_System] WHERE [BLDG_SYS_BLDG_ID]={" + this.cboBuildingDest.SelectedValue.ToString() + "}");
      DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT [System_Component].* FROM [Building_System] INNER JOIN [System_Component] ON Building_System.BLDG_SYS_ID = System_Component.SYS_COMP_BLDG_SYS_ID WHERE [Building_System].[BLDG_SYS_BLDG_ID]={" + this.cboBuildingDest.SelectedValue.ToString() + "}");
      try
      {
        foreach (DataRow row1 in dataTable1.Rows)
        {
          DataRow[] dataRowArray1 = dataTable2.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "[BLDG_SYS_LINK]=", row1["BLDG_SYS_LINK"])));
          DataRow[] dataRowArray2 = dataTable3.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "[SYS_COMP_COMP_LINK]=", row1["SYS_COMP_COMP_LINK"])));
          string str1;
          string str2;
          string uniqueId1;
          if (((IEnumerable<DataRow>) dataRowArray1).Count<DataRow>() > 0)
          {
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRowArray1[0]["BRED_Status"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataRowArray1[0]["BRED_Status"], (object) "D", false))
            {
              dataRowArray1[0]["BRED_Status"] = (object) "U";
              frmMain fMainForm = mdUtility.fMainForm;
              ComboBox cboBuildingDest;
              str1 = Conversions.ToString((cboBuildingDest = this.cboBuildingDest).SelectedValue);
              ref string local1 = ref str1;
              DataRow dataRow1;
              string str3 = Conversions.ToString((dataRow1 = dataRowArray1[0])["BLDG_SYS_ID"]);
              ref string local2 = ref str3;
              DataRow dataRow2;
              str2 = Conversions.ToString((dataRow2 = row1)["SYS_DESC"]);
              ref string local3 = ref str2;
              fMainForm.SystemAdded(ref local1, ref local2, ref local3);
              dataRow2["SYS_DESC"] = (object) str2;
              dataRow1["BLDG_SYS_ID"] = (object) str3;
              cboBuildingDest.SelectedValue = (object) str1;
            }
            uniqueId1 = dataRowArray1[0]["BLDG_SYS_ID"].ToString();
          }
          else
          {
            DataRow row2 = dataTable2.NewRow();
            row2["BLDG_SYS_BLDG_ID"] = RuntimeHelpers.GetObjectValue(this.cboBuildingDest.SelectedValue);
            uniqueId1 = mdUtility.GetUniqueID();
            row2["BLDG_SYS_ID"] = (object) uniqueId1;
            row2["BLDG_SYS_LINK"] = RuntimeHelpers.GetObjectValue(row1["BLDG_SYS_LINK"]);
            row2["bred_status"] = (object) "N";
            dataTable2.Rows.Add(row2);
            frmMain fMainForm = mdUtility.fMainForm;
            str2 = this.cboBuildingDest.SelectedValue.ToString();
            ref string local1 = ref str2;
            string str3 = uniqueId1.ToString();
            ref string local2 = ref str3;
            DataRow dataRow;
            str1 = Conversions.ToString((dataRow = row1)["SYS_DESC"]);
            ref string local3 = ref str1;
            fMainForm.SystemAdded(ref local1, ref local2, ref local3);
            dataRow["SYS_DESC"] = (object) str1;
          }
          string uniqueId2;
          if (((IEnumerable<DataRow>) dataRowArray2).Count<DataRow>() > 0)
          {
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRowArray2[0]["BRED_Status"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataRowArray2[0]["BRED_Status"], (object) "D", false))
            {
              dataRowArray2[0]["BRED_Status"] = (object) "U";
              frmMain fMainForm = mdUtility.fMainForm;
              ref string local1 = ref uniqueId1;
              DataRow dataRow1;
              str1 = Conversions.ToString((dataRow1 = dataRowArray2[0])["SYS_COMP_ID"]);
              ref string local2 = ref str1;
              DataRow dataRow2;
              string str3 = Conversions.ToString((dataRow2 = row1)["COMP_DESC"]);
              ref string local3 = ref str3;
              fMainForm.ComponentAdded(ref local1, ref local2, ref local3);
              dataRow2["COMP_DESC"] = (object) str3;
              dataRow1["SYS_COMP_ID"] = (object) str1;
            }
            uniqueId2 = dataRowArray2[0]["SYS_COMP_ID"].ToString();
          }
          else
          {
            DataRow row2 = dataTable3.NewRow();
            DataRow dataRow1 = row2;
            dataRow1["sys_comp_bldg_sys_id"] = (object) uniqueId1;
            uniqueId2 = mdUtility.GetUniqueID();
            dataRow1["sys_comp_id"] = (object) uniqueId2;
            dataRow1["sys_comp_comp_link"] = RuntimeHelpers.GetObjectValue(row1["SYS_COMP_COMP_LINK"]);
            dataRow1["bred_status"] = (object) "N";
            dataTable3.Rows.Add(row2);
            frmMain fMainForm = mdUtility.fMainForm;
            ref string local1 = ref uniqueId1;
            ref string local2 = ref uniqueId2;
            DataRow dataRow2;
            string str3 = Conversions.ToString((dataRow2 = row1)["COMP_DESC"]);
            ref string local3 = ref str3;
            fMainForm.ComponentAdded(ref local1, ref local2, ref local3);
            dataRow2["COMP_DESC"] = (object) str3;
          }
          DataRow row3 = tableSchema.NewRow();
          DataRow dataRow3 = row3;
          dataRow3["BRED_Status"] = (object) "N";
          dataRow3["SEC_ID"] = (object) mdUtility.GetUniqueID();
          dataRow3["SEC_SYS_COMP_ID"] = (object) uniqueId2;
          dataRow3["SEC_CMC_LINK"] = RuntimeHelpers.GetObjectValue(row1["SEC_CMC_LINK"]);
          dataRow3["SEC_QTY"] = RuntimeHelpers.GetObjectValue(row1["SEC_QTY"]);
          dataRow3["SEC_YEAR_BUILT"] = RuntimeHelpers.GetObjectValue(row1["SEC_YEAR_BUILT"]);
          dataRow3["SEC_DATE_PAINTED"] = RuntimeHelpers.GetObjectValue(row1["SEC_DATE_PAINTED"]);
          dataRow3["SEC_NAME"] = RuntimeHelpers.GetObjectValue(row1["SEC_NAME"]);
          dataRow3["SEC_PAINT"] = RuntimeHelpers.GetObjectValue(row1["SEC_PAINT"]);
          dataRow3["SEC_PAINT_LINK"] = RuntimeHelpers.GetObjectValue(row1["SEC_PAINT_LINK"]);
          if (this.chkCopyComments.CheckState == CheckState.Checked)
            dataRow3["SEC_COMMENTS"] = RuntimeHelpers.GetObjectValue(row1["SEC_COMMENTS"]);
          dataRow3["SEC_DATE_SOURCE"] = this.chkEstimateDates.CheckState != CheckState.Checked ? RuntimeHelpers.GetObjectValue(row1["SEC_DATE_SOURCE"]) : (object) "Estimated";
          tableSchema.Rows.Add(row3);
          string str4 = Section.SectionLabel(row3["SEC_ID"].ToString());
          frmMain fMainForm1 = mdUtility.fMainForm;
          string str5 = row3["SEC_SYS_COMP_ID"].ToString();
          ref string local4 = ref str5;
          str1 = row3["SEC_ID"].ToString();
          ref string local5 = ref str1;
          ref string local6 = ref str4;
          fMainForm1.SectionAdded(ref local4, ref local5, ref local6);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.SaveDataTable(ref dataTable2, "SELECT * FROM Building_System");
      mdUtility.DB.SaveDataTable(ref dataTable3, "SELECT * FROM System_Component");
      mdUtility.DB.SaveDataTable(ref tableSchema, "SELECT * FROM Component_Section");
    }
  }
}
