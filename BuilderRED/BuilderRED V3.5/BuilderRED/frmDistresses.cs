// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmDistresses
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmDistresses : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmDistresses";
    private string m_strSampleID;
    private int m_lSubCompLink;
    public string m_strSectionName;
    public DataTable dtDistresses;
    private DataView dvDistresses;
    private double m_dSubQty;
    private bool m_bEdit;
    private bool m_bLoaded;
    private bool m_bSave;
    private bool m_bAltUM;

    public frmDistresses()
    {
      this.Load += new EventHandler(this.frmDistresses_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual CheckBox chkAltUM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Panel frmAltUM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button cmdLikeSubComponents
    {
      get
      {
        return this._cmdLikeSubComponents;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdLikeSubComponents_Click);
        Button likeSubComponents1 = this._cmdLikeSubComponents;
        if (likeSubComponents1 != null)
          likeSubComponents1.Click -= eventHandler;
        this._cmdLikeSubComponents = value;
        Button likeSubComponents2 = this._cmdLikeSubComponents;
        if (likeSubComponents2 == null)
          return;
        likeSubComponents2.Click += eventHandler;
      }
    }

    public virtual Button DeleteButton
    {
      get
      {
        return this._DeleteButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.DeleteButton_Click);
        Button deleteButton1 = this._DeleteButton;
        if (deleteButton1 != null)
          deleteButton1.Click -= eventHandler;
        this._DeleteButton = value;
        Button deleteButton2 = this._DeleteButton;
        if (deleteButton2 == null)
          return;
        deleteButton2.Click += eventHandler;
      }
    }

    public virtual Button CloseButton
    {
      get
      {
        return this._CloseButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.CloseButton_Click);
        Button closeButton1 = this._CloseButton;
        if (closeButton1 != null)
          closeButton1.Click -= eventHandler;
        this._CloseButton = value;
        Button closeButton2 = this._CloseButton;
        if (closeButton2 == null)
          return;
        closeButton2.Click += eventHandler;
      }
    }

    public virtual Button NewButton
    {
      get
      {
        return this._NewButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.NewButton_Click);
        Button newButton1 = this._NewButton;
        if (newButton1 != null)
          newButton1.Click -= eventHandler;
        this._NewButton = value;
        Button newButton2 = this._NewButton;
        if (newButton2 == null)
          return;
        newButton2.Click += eventHandler;
      }
    }

    public virtual Label lblDistSubCompIDVal { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistSubCompID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistCompIDVal { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistSampUnit { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistSecIDVal { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistSampUnitVal { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistSecID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDistCompID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblESC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.frmAltUM = new Panel();
      this.chkAltUM = new CheckBox();
      this.cmdLikeSubComponents = new Button();
      this.DeleteButton = new Button();
      this.CloseButton = new Button();
      this.NewButton = new Button();
      this.lblDistSubCompIDVal = new Label();
      this.lblDistSubCompID = new Label();
      this.lblDistCompIDVal = new Label();
      this.lblDistSampUnit = new Label();
      this.lblDistSecIDVal = new Label();
      this.lblDistSampUnitVal = new Label();
      this.lblDistSecID = new Label();
      this.lblDistCompID = new Label();
      this.lblESC = new Label();
      this.HelpProvider = new HelpProvider();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.rgDistresses = new RadGridView();
      this.frmAltUM.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.rgDistresses.BeginInit();
      this.rgDistresses.MasterTemplate.BeginInit();
      this.SuspendLayout();
      this.frmAltUM.BackColor = SystemColors.Control;
      this.frmAltUM.Controls.Add((Control) this.chkAltUM);
      this.frmAltUM.Cursor = Cursors.Default;
      this.frmAltUM.ForeColor = SystemColors.ControlText;
      this.frmAltUM.Location = new Point(735, 85);
      this.frmAltUM.Margin = new Padding(0);
      this.frmAltUM.Name = "frmAltUM";
      this.frmAltUM.RightToLeft = RightToLeft.No;
      this.frmAltUM.Size = new Size(277, 27);
      this.frmAltUM.TabIndex = 13;
      this.chkAltUM.BackColor = SystemColors.Control;
      this.chkAltUM.Cursor = Cursors.Default;
      this.chkAltUM.Dock = DockStyle.Fill;
      this.chkAltUM.ForeColor = SystemColors.ControlText;
      this.chkAltUM.Location = new Point(0, 0);
      this.chkAltUM.Margin = new Padding(4);
      this.chkAltUM.Name = "chkAltUM";
      this.chkAltUM.RightToLeft = RightToLeft.No;
      this.chkAltUM.Size = new Size(277, 27);
      this.chkAltUM.TabIndex = 14;
      this.chkAltUM.Text = "Alternative UM \"Each\" (unit count)";
      this.chkAltUM.UseVisualStyleBackColor = false;
      this.cmdLikeSubComponents.AutoSize = true;
      this.cmdLikeSubComponents.BackColor = SystemColors.Control;
      this.cmdLikeSubComponents.Cursor = Cursors.Default;
      this.cmdLikeSubComponents.Dock = DockStyle.Fill;
      this.cmdLikeSubComponents.ForeColor = SystemColors.ControlText;
      this.cmdLikeSubComponents.Location = new Point(735, 0);
      this.cmdLikeSubComponents.Margin = new Padding(0);
      this.cmdLikeSubComponents.Name = "cmdLikeSubComponents";
      this.cmdLikeSubComponents.RightToLeft = RightToLeft.No;
      this.cmdLikeSubComponents.Size = new Size(277, 33);
      this.cmdLikeSubComponents.TabIndex = 12;
      this.cmdLikeSubComponents.Text = "Like Subcomponents...";
      this.cmdLikeSubComponents.UseVisualStyleBackColor = false;
      this.DeleteButton.AutoSize = true;
      this.DeleteButton.BackColor = SystemColors.Control;
      this.DeleteButton.Cursor = Cursors.Default;
      this.DeleteButton.Dock = DockStyle.Left;
      this.DeleteButton.ForeColor = SystemColors.ControlText;
      this.DeleteButton.Location = new Point(564, 4);
      this.DeleteButton.Margin = new Padding(4);
      this.DeleteButton.Name = "DeleteButton";
      this.DeleteButton.RightToLeft = RightToLeft.No;
      this.DeleteButton.Size = new Size(108, 33);
      this.DeleteButton.TabIndex = 2;
      this.DeleteButton.Text = "Delete";
      this.DeleteButton.UseVisualStyleBackColor = false;
      this.CloseButton.AutoSize = true;
      this.CloseButton.BackColor = SystemColors.Control;
      this.CloseButton.Cursor = Cursors.Default;
      this.CloseButton.Dock = DockStyle.Right;
      this.CloseButton.ForeColor = SystemColors.ControlText;
      this.CloseButton.Location = new Point(332, 4);
      this.CloseButton.Margin = new Padding(4);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.RightToLeft = RightToLeft.No;
      this.CloseButton.Size = new Size(108, 33);
      this.CloseButton.TabIndex = 1;
      this.CloseButton.Text = "Close";
      this.CloseButton.UseVisualStyleBackColor = false;
      this.NewButton.AutoSize = true;
      this.NewButton.BackColor = SystemColors.Control;
      this.NewButton.Cursor = Cursors.Default;
      this.NewButton.Dock = DockStyle.Fill;
      this.NewButton.ForeColor = SystemColors.ControlText;
      this.NewButton.Location = new Point(448, 4);
      this.NewButton.Margin = new Padding(4);
      this.NewButton.Name = "NewButton";
      this.NewButton.RightToLeft = RightToLeft.No;
      this.NewButton.Size = new Size(108, 33);
      this.NewButton.TabIndex = 0;
      this.NewButton.Text = "Add";
      this.NewButton.UseVisualStyleBackColor = false;
      this.lblDistSubCompIDVal.BackColor = SystemColors.Control;
      this.lblDistSubCompIDVal.Cursor = Cursors.Default;
      this.lblDistSubCompIDVal.Dock = DockStyle.Fill;
      this.lblDistSubCompIDVal.ForeColor = SystemColors.ControlText;
      this.lblDistSubCompIDVal.Location = new Point(146, 59);
      this.lblDistSubCompIDVal.Margin = new Padding(4, 0, 4, 0);
      this.lblDistSubCompIDVal.Name = "lblDistSubCompIDVal";
      this.lblDistSubCompIDVal.RightToLeft = RightToLeft.No;
      this.lblDistSubCompIDVal.Size = new Size(585, 26);
      this.lblDistSubCompIDVal.TabIndex = 11;
      this.lblDistSubCompID.AutoSize = true;
      this.lblDistSubCompID.BackColor = Color.Transparent;
      this.lblDistSubCompID.Cursor = Cursors.Default;
      this.lblDistSubCompID.Dock = DockStyle.Fill;
      this.lblDistSubCompID.ForeColor = SystemColors.ControlText;
      this.lblDistSubCompID.Location = new Point(4, 59);
      this.lblDistSubCompID.Margin = new Padding(4, 0, 4, 0);
      this.lblDistSubCompID.Name = "lblDistSubCompID";
      this.lblDistSubCompID.RightToLeft = RightToLeft.No;
      this.lblDistSubCompID.Size = new Size(134, 26);
      this.lblDistSubCompID.TabIndex = 10;
      this.lblDistSubCompID.Text = "Subcomponent:";
      this.lblDistSubCompID.TextAlign = ContentAlignment.TopRight;
      this.lblDistCompIDVal.BackColor = SystemColors.Control;
      this.lblDistCompIDVal.Cursor = Cursors.Default;
      this.lblDistCompIDVal.Dock = DockStyle.Fill;
      this.lblDistCompIDVal.ForeColor = SystemColors.ControlText;
      this.lblDistCompIDVal.Location = new Point(146, 0);
      this.lblDistCompIDVal.Margin = new Padding(4, 0, 4, 0);
      this.lblDistCompIDVal.Name = "lblDistCompIDVal";
      this.lblDistCompIDVal.RightToLeft = RightToLeft.No;
      this.lblDistCompIDVal.Size = new Size(585, 33);
      this.lblDistCompIDVal.TabIndex = 9;
      this.lblDistSampUnit.AutoSize = true;
      this.lblDistSampUnit.BackColor = Color.Transparent;
      this.lblDistSampUnit.Cursor = Cursors.Default;
      this.lblDistSampUnit.Dock = DockStyle.Fill;
      this.lblDistSampUnit.ForeColor = SystemColors.ControlText;
      this.lblDistSampUnit.Location = new Point(4, 85);
      this.lblDistSampUnit.Margin = new Padding(4, 0, 4, 0);
      this.lblDistSampUnit.Name = "lblDistSampUnit";
      this.lblDistSampUnit.RightToLeft = RightToLeft.No;
      this.lblDistSampUnit.Size = new Size(134, 27);
      this.lblDistSampUnit.TabIndex = 8;
      this.lblDistSampUnit.Text = "Subcomponent UM:";
      this.lblDistSampUnit.TextAlign = ContentAlignment.TopRight;
      this.lblDistSecIDVal.BackColor = SystemColors.Control;
      this.lblDistSecIDVal.Cursor = Cursors.Default;
      this.lblDistSecIDVal.Dock = DockStyle.Fill;
      this.lblDistSecIDVal.ForeColor = SystemColors.ControlText;
      this.lblDistSecIDVal.Location = new Point(146, 33);
      this.lblDistSecIDVal.Margin = new Padding(4, 0, 4, 0);
      this.lblDistSecIDVal.Name = "lblDistSecIDVal";
      this.lblDistSecIDVal.RightToLeft = RightToLeft.No;
      this.lblDistSecIDVal.Size = new Size(585, 26);
      this.lblDistSecIDVal.TabIndex = 7;
      this.lblDistSampUnitVal.BackColor = SystemColors.Control;
      this.lblDistSampUnitVal.Cursor = Cursors.Default;
      this.lblDistSampUnitVal.Dock = DockStyle.Fill;
      this.lblDistSampUnitVal.ForeColor = SystemColors.ControlText;
      this.lblDistSampUnitVal.Location = new Point(146, 85);
      this.lblDistSampUnitVal.Margin = new Padding(4, 0, 4, 0);
      this.lblDistSampUnitVal.Name = "lblDistSampUnitVal";
      this.lblDistSampUnitVal.RightToLeft = RightToLeft.No;
      this.lblDistSampUnitVal.Size = new Size(585, 27);
      this.lblDistSampUnitVal.TabIndex = 6;
      this.lblDistSecID.AutoSize = true;
      this.lblDistSecID.BackColor = Color.Transparent;
      this.lblDistSecID.Cursor = Cursors.Default;
      this.lblDistSecID.Dock = DockStyle.Fill;
      this.lblDistSecID.ForeColor = SystemColors.ControlText;
      this.lblDistSecID.Location = new Point(4, 33);
      this.lblDistSecID.Margin = new Padding(4, 0, 4, 0);
      this.lblDistSecID.Name = "lblDistSecID";
      this.lblDistSecID.RightToLeft = RightToLeft.No;
      this.lblDistSecID.Size = new Size(134, 26);
      this.lblDistSecID.TabIndex = 5;
      this.lblDistSecID.Text = "Section Description:";
      this.lblDistSecID.TextAlign = ContentAlignment.TopRight;
      this.lblDistCompID.AutoSize = true;
      this.lblDistCompID.BackColor = Color.Transparent;
      this.lblDistCompID.Cursor = Cursors.Default;
      this.lblDistCompID.Dock = DockStyle.Fill;
      this.lblDistCompID.ForeColor = SystemColors.ControlText;
      this.lblDistCompID.Location = new Point(4, 0);
      this.lblDistCompID.Margin = new Padding(4, 0, 4, 0);
      this.lblDistCompID.Name = "lblDistCompID";
      this.lblDistCompID.RightToLeft = RightToLeft.No;
      this.lblDistCompID.Size = new Size(134, 33);
      this.lblDistCompID.TabIndex = 4;
      this.lblDistCompID.Text = "Component:";
      this.lblDistCompID.TextAlign = ContentAlignment.TopRight;
      this.lblESC.AutoSize = true;
      this.lblESC.BackColor = Color.FromArgb(0, 0, 192);
      this.lblESC.Cursor = Cursors.Default;
      this.lblESC.Dock = DockStyle.Fill;
      this.lblESC.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblESC.ForeColor = Color.White;
      this.lblESC.Location = new Point(739, 112);
      this.lblESC.Margin = new Padding(4, 0, 0, 0);
      this.lblESC.Name = "lblESC";
      this.lblESC.RightToLeft = RightToLeft.No;
      this.lblESC.Size = new Size(273, 17);
      this.lblESC.TabIndex = 3;
      this.lblESC.Text = "Emergency/Service Info";
      this.lblESC.TextAlign = ContentAlignment.TopCenter;
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistCompID, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.frmAltUM, 2, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistSecID, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdLikeSubComponents, 2, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblESC, 2, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistSubCompID, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistSampUnit, 0, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistCompIDVal, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistSecIDVal, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistSampUnitVal, 1, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDistSubCompIDVal, 1, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.TableLayoutPanel2, 0, 6);
      this.TableLayoutPanel1.Controls.Add((Control) this.rgDistresses, 0, 5);
      this.TableLayoutPanel1.Location = new Point(1, 2);
      this.TableLayoutPanel1.Margin = new Padding(4);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 7;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(1012, 452);
      this.TableLayoutPanel1.TabIndex = 19;
      this.TableLayoutPanel2.ColumnCount = 3;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.TableLayoutPanel2, 3);
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel2.Controls.Add((Control) this.DeleteButton, 2, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.CloseButton, 0, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.NewButton, 1, 0);
      this.TableLayoutPanel2.Dock = DockStyle.Bottom;
      this.TableLayoutPanel2.Location = new Point(4, 407);
      this.TableLayoutPanel2.Margin = new Padding(4);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 1;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.Size = new Size(1004, 41);
      this.TableLayoutPanel2.TabIndex = 19;
      this.rgDistresses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.rgDistresses.BackColor = SystemColors.ControlLightLight;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.rgDistresses, 3);
      this.rgDistresses.ImeMode = ImeMode.Katakana;
      this.rgDistresses.Location = new Point(0, 129);
      this.rgDistresses.Margin = new Padding(0);
      this.rgDistresses.Name = "rgDistresses";
      this.rgDistresses.RootElement.AccessibleDescription = (string) null;
      this.rgDistresses.RootElement.AccessibleName = (string) null;
      this.rgDistresses.RootElement.ControlBounds = new Rectangle(0, 129, 300, 187);
      this.rgDistresses.Size = new Size(1012, 274);
      this.rgDistresses.TabIndex = 20;
      this.rgDistresses.Text = "RadGridView1";
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(1012, 452);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.Location = new Point(-8, 89);
      this.Margin = new Padding(4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmDistresses);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Inspection Checklist";
      this.frmAltUM.ResumeLayout(false);
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.rgDistresses.MasterTemplate.EndInit();
      this.rgDistresses.EndInit();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1
    {
      get
      {
        return this._TableLayoutPanel1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        PaintEventHandler paintEventHandler = new PaintEventHandler(this.TableLayoutPanel1_Paint);
        TableLayoutPanel tableLayoutPanel1_1 = this._TableLayoutPanel1;
        if (tableLayoutPanel1_1 != null)
          tableLayoutPanel1_1.Paint -= paintEventHandler;
        this._TableLayoutPanel1 = value;
        TableLayoutPanel tableLayoutPanel1_2 = this._TableLayoutPanel1;
        if (tableLayoutPanel1_2 == null)
          return;
        tableLayoutPanel1_2.Paint += paintEventHandler;
      }
    }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual RadGridView rgDistresses
    {
      get
      {
        return this._rgDistresses;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        GridViewCellEventHandler cellEventHandler1 = new GridViewCellEventHandler(this.rgDistresses_CellClick);
        GridViewCellEventHandler cellEventHandler2 = new GridViewCellEventHandler(this.rgdistresses_CellEditorInitialized);
        GridViewCellEventHandler cellEventHandler3 = new GridViewCellEventHandler(this.rgDistresses_CellValueChanged);
        CellValidatingEventHandler validatingEventHandler = new CellValidatingEventHandler(this.rgDistresses_CellValidating);
        RadGridView rgDistresses1 = this._rgDistresses;
        if (rgDistresses1 != null)
        {
          rgDistresses1.CellClick -= cellEventHandler1;
          rgDistresses1.CellEditorInitialized -= cellEventHandler2;
          rgDistresses1.CellValueChanged -= cellEventHandler3;
          rgDistresses1.CellValidating -= validatingEventHandler;
        }
        this._rgDistresses = value;
        RadGridView rgDistresses2 = this._rgDistresses;
        if (rgDistresses2 == null)
          return;
        rgDistresses2.CellClick += cellEventHandler1;
        rgDistresses2.CellEditorInitialized += cellEventHandler2;
        rgDistresses2.CellValueChanged += cellEventHandler3;
        rgDistresses2.CellValidating += validatingEventHandler;
      }
    }

    public string aSample
    {
      set
      {
        this.m_dSubQty = -1.0;
        this.m_strSampleID = value;
        this.RefreshData();
      }
    }

    public bool CanEdit
    {
      get
      {
        return this.m_bEdit;
      }
      set
      {
        this.m_bEdit = value;
        this.frmAltUM.Enabled = value;
      }
    }

    public string SubCompLink
    {
      set
      {
        this.m_lSubCompLink = Conversions.ToInteger(value);
      }
    }

    private void RefreshData()
    {
      try
      {
        this.m_bLoaded = false;
        this.dvDistresses = new DataView(this.dtDistresses);
        this.dvDistresses.RowFilter = "Subcomplink=" + Conversions.ToString(this.m_lSubCompLink) + " AND BRED_Status<>'R' and BRED_Status <>'D'";
        if (this.dvDistresses.Count > 0 & this.CanEdit)
          this.DeleteButton.Enabled = true;
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT UOM_ENG_UNIT_ABBR, UOM_MET_UNIT_ABBR FROM qrySubcomponentUOM WHERE [CMC_SCOMP_ID]=" + Conversions.ToString(this.m_lSubCompLink));
        this.lblDistSampUnitVal.Text = mdUtility.Units != mdUtility.SystemofMeasure.umEnglish ? Conversions.ToString(dataTable.Rows[0]["UOM_MET_UNIT_ABBR"]) : Conversions.ToString(dataTable.Rows[0]["UOM_ENG_UNIT_ABBR"]);
        if (this.dvDistresses.Count > 0)
        {
          int num = checked (this.dvDistresses.Count - 1);
          int index = 0;
          while (index <= num)
          {
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(Interaction.IIf(Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Subcomponent Qty"])), (object) "", RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Subcomponent Qty"]))), "", false) > 0U)
            {
              this.m_dSubQty = Conversions.ToDouble(this.dvDistresses[index]["Subcomponent Qty"]);
              break;
            }
            checked { ++index; }
          }
        }
        this.rgDistresses.DataSource = (object) this.dvDistresses;
        this.FormatGrid();
        this.m_bLoaded = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), nameof (RefreshData));
        ProjectData.ClearProjectError();
      }
    }

    private void CloseButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (this.dvDistresses.Count > 0)
        {
          int num1 = checked (this.dvDistresses.Count - 1);
          int index = 0;
          while (index <= num1)
          {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Distress"])) | Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Severity"])) | Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Density"])))
            {
              int num2 = (int) Interaction.MsgBox((object) "You must supply distress, severity and density values for all distress records.", MsgBoxStyle.Critical, (object) null);
              return;
            }
            checked { ++index; }
          }
        }
        this.m_bAltUM = this.chkAltUM.CheckState == CheckState.Checked;
        this.Close();
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != -2147217842)
          mdUtility.Errorhandler(ex2, nameof (frmDistresses), nameof (CloseButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void cmdLikeSubComponents_Click(object eventSender, EventArgs eventArgs)
    {
      dlgOtherDistresses dlgOtherDistresses1 = new dlgOtherDistresses();
      try
      {
        string str = "";
        if (this.dvDistresses.Count > 0)
        {
          int num = checked (this.dvDistresses.Count - 1);
          int index = 0;
          while (index <= num)
          {
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Distress"])) & !Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Severity"])) & !Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.dvDistresses[index]["Density"])))
              str = str + Conversions.ToString(Sample.DistDensLink(Conversions.ToInteger(this.dvDistresses[index]["Distress"]), Conversions.ToInteger(this.dvDistresses[index]["Severity"]), Conversions.ToInteger(this.dvDistresses[index]["Density"]))) + ",";
            checked { ++index; }
          }
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", false) > 0U)
            str = Microsoft.VisualBasic.Strings.Left(str, checked (Microsoft.VisualBasic.Strings.Len(str) - 1));
        }
        dlgOtherDistresses dlgOtherDistresses2 = dlgOtherDistresses1;
        frmDistresses frmDistresses = this;
        ref frmDistresses local1 = ref frmDistresses;
        ref int local2 = ref this.m_lSubCompLink;
        ref string local3 = ref str;
        if (!dlgOtherDistresses2.ShowLike(ref local1, ref local2, ref local3))
          return;
        this.rgDistresses.Refresh();
        this.DeleteButton.Enabled = true;
        this.SetChanged();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), nameof (cmdLikeSubComponents_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteButton_Click(object eventSender, EventArgs eventArgs)
    {
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.dvDistresses[this.rgDistresses.CurrentRow.Index]["BRED_Status"], (object) "A", false))
      {
        this.dtDistresses.Rows.Remove(this.dvDistresses[this.rgDistresses.CurrentRow.Index].Row);
        this.rgDistresses.Update();
      }
      else
      {
        this.dvDistresses[this.rgDistresses.CurrentRow.Index]["BRED_Status"] = (object) "R";
        this.dvDistresses = new DataView(this.dtDistresses);
        this.dvDistresses.RowFilter = "Subcomplink=" + Conversions.ToString(this.m_lSubCompLink) + " AND BRED_Status<>'R' and BRED_Status <>'D'";
      }
      if (this.dvDistresses.Count == 0)
        this.DeleteButton.Enabled = false;
      else
        this.DeleteButton.Enabled = true;
      this.SetChanged();
    }

    private void frmDistresses_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        try
        {
          foreach (Control control in this.Controls)
          {
            this.HelpProvider.SetHelpNavigator(control, HelpNavigator.KeywordIndex);
            this.HelpProvider.SetHelpKeyword(control, "Distresses");
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.m_bLoaded = false;
        if (this.CanEdit)
        {
          this.NewButton.Enabled = true;
          this.DeleteButton.Enabled = true;
          this.cmdLikeSubComponents.Enabled = true;
        }
        else
        {
          this.NewButton.Enabled = false;
          this.DeleteButton.Enabled = false;
          this.cmdLikeSubComponents.Enabled = false;
        }
        this.m_bLoaded = true;
        this.rgDistresses.AllowAddNewRow = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void FormatGrid()
    {
      try
      {
        RadGridView rgDistresses = this.rgDistresses;
        bool canEditInspection = mdUtility.fMainForm.CanEditInspection;
        IEnumerator<GridViewDataColumn> enumerator;
        try
        {
          enumerator = rgDistresses.Columns.GetEnumerator();
          while (enumerator.MoveNext())
            enumerator.Current.ReadOnly = !canEditInspection;
        }
        finally
        {
          enumerator?.Dispose();
        }
        rgDistresses.Columns["SAMP_SUBCOMP_SAMP_ID"].IsVisible = false;
        rgDistresses.Columns["SAMP_SUBCOMP_SAMP_ID"].Width = 0;
        rgDistresses.Columns["BRED_Status"].IsVisible = false;
        rgDistresses.Columns["BRED_Status"].Width = 0;
        rgDistresses.Columns["ID"].IsVisible = false;
        rgDistresses.Columns["ID"].Width = 0;
        rgDistresses.Columns["SubCompLink"].IsVisible = false;
        rgDistresses.Columns["SubCompLink"].Width = 0;
        rgDistresses.Columns["Distress"].IsVisible = false;
        rgDistresses.Columns["Distress"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["Distress"].TextAlignment = ContentAlignment.MiddleLeft;
        rgDistresses.Columns["Distress"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(2550.0)));
        rgDistresses.Columns["Severity"].IsVisible = false;
        rgDistresses.Columns["Severity"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["Severity"].TextAlignment = ContentAlignment.MiddleLeft;
        rgDistresses.Columns["Severity"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1100.0)));
        rgDistresses.Columns["Subcomponent Qty"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1200.0)));
        rgDistresses.Columns["Subcomponent Qty"].HeaderText = "Subcomponent\r\nQty (Optional)";
        rgDistresses.Columns["Subcomponent Qty"].IsVisible = true;
        rgDistresses.Columns["Distress Qty"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1000.0)));
        rgDistresses.Columns["Distress Qty"].HeaderText = "Distress Qty\r\n(Optional)";
        rgDistresses.Columns["Distress Qty"].IsVisible = true;
        rgDistresses.Columns["Density"].IsVisible = false;
        rgDistresses.Columns["Density"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["Density"].TextAlignment = ContentAlignment.MiddleLeft;
        rgDistresses.Columns["Density"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1500.0)));
        rgDistresses.Columns["Critical"].IsVisible = true;
        rgDistresses.Columns["Critical"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["Critical"].TextAlignment = ContentAlignment.MiddleLeft;
        rgDistresses.Columns["Critical"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(600.0)));
        rgDistresses.Columns["ESC"].IsVisible = true;
        rgDistresses.Columns["ESC"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["ESC"].TextAlignment = ContentAlignment.MiddleLeft;
        rgDistresses.Columns["ESC"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(600.0)));
        rgDistresses.Columns["ESC Number"].IsVisible = true;
        rgDistresses.Columns["ESC Number"].HeaderText = "ESC\r\nNumber";
        rgDistresses.Columns["ESC Number"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["ESC Number"].TextAlignment = ContentAlignment.MiddleLeft;
        rgDistresses.Columns["ESC Number"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(900.0)));
        rgDistresses.Columns["ESC Date"].HeaderText = "Date\r\nCompleted";
        rgDistresses.Columns["ESC Date"].IsVisible = true;
        rgDistresses.Columns["ESC Date"].HeaderTextAlignment = ContentAlignment.MiddleCenter;
        rgDistresses.Columns["ESC Date"].TextAlignment = ContentAlignment.MiddleLeft;
        this.SetDropDownValueItems("Distress", "SubCompDistresses", Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) this.m_lSubCompLink)));
        this.SetDropDownValueItems("Density", "DensityValues", "");
        this.SetDropDownValueItems("Severity", "SeverityValues", "");
        this.rgDistresses.Columns.Move(this.rgDistresses.Columns["DensityDisplay"].Index, 0);
        this.rgDistresses.Columns.Move(this.rgDistresses.Columns["SeverityDisplay"].Index, 0);
        this.rgDistresses.Columns.Move(this.rgDistresses.Columns["DistressDisplay"].Index, 0);
        this.PositionESCLabel();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), nameof (FormatGrid));
        ProjectData.ClearProjectError();
      }
    }

    private void SetDropDownValueItems(string strCol, string strLookUp, string strKey = "")
    {
      try
      {
        string Left = strCol;
        string sSQL;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Distress", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Density", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Severity", false) == 0)
              sSQL = "SELECT [severity_id] as ID, severity_desc as Description FROM RO_Distress_Severity_Values";
          }
          else
            sSQL = "SELECT [density_id] as ID, density_desc as Description FROM RO_Distress_Density_Values";
        }
        else
          sSQL = "SELECT ID, Description FROM qrySubcomponentDistress WHERE [SCOMP_DIST_CMC_SCOMP_LINK]=" + strKey;
        DataTable dataTable = mdUtility.DB.GetDataTable(sSQL);
        RadGridView rgDistresses = this.rgDistresses;
        GridViewComboBoxColumn viewComboBoxColumn = new GridViewComboBoxColumn();
        viewComboBoxColumn.Name = strCol + "Display";
        viewComboBoxColumn.HeaderText = strCol;
        viewComboBoxColumn.DataSource = (object) dataTable;
        viewComboBoxColumn.ValueMember = "ID";
        viewComboBoxColumn.DisplayMember = "Description";
        viewComboBoxColumn.FieldName = strCol;
        viewComboBoxColumn.Width = 200;
        rgDistresses.Columns.Add((GridViewDataColumn) viewComboBoxColumn);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), nameof (SetDropDownValueItems));
        ProjectData.ClearProjectError();
      }
    }

    private void MoveColumnToLeftMost(int iColumnIndex)
    {
      this.rgDistresses.Columns.Move(iColumnIndex, 0);
    }

    private void NewButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (this.rgDistresses.Rows.Count > 0 & this.rgDistresses.CurrentCell != null && this.rgDistresses.CurrentCell.IsInEditMode)
          this.rgDistresses.Update();
        DataRow row = this.dvDistresses.Table.NewRow();
        row["BRED_Status"] = (object) "A";
        row["ID"] = (object) mdUtility.GetUniqueID();
        row["SubCompLink"] = (object) this.m_lSubCompLink;
        if (this.m_dSubQty != -1.0)
          row["Subcomponent Qty"] = (object) this.m_dSubQty;
        row["Critical"] = (object) false;
        row["ESC"] = (object) false;
        this.dtDistresses.Rows.Add(row);
        this.rgDistresses.Refresh();
        this.DeleteButton.Enabled = true;
        this.SetChanged();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), nameof (NewButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void SetEnabled()
    {
      if (this.m_bEdit)
        return;
      this.DeleteButton.Enabled = false;
      this.NewButton.Enabled = false;
    }

    private void SetChanged()
    {
      if (!this.m_bLoaded)
        return;
      this.m_bSave = true;
    }

    public bool EditDistresses(ref bool AltUM, ref int iDistressCount)
    {
      this.m_bAltUM = AltUM;
      this.chkAltUM.Checked = AltUM;
      int num = (int) this.ShowDialog();
      bool bSave = this.m_bSave;
      iDistressCount = this.dvDistresses.Count;
      AltUM = this.m_bAltUM;
      return bSave;
    }

    private void PositionESCLabel()
    {
      try
      {
        RadGridView rgDistresses = this.rgDistresses;
        int num1 = checked (rgDistresses.Left + 20);
        int num2 = checked (rgDistresses.Columns.Count - 1);
        int index = 0;
        int num3;
        while (index <= num2)
        {
          if (rgDistresses.Columns[index].IsVisible)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(rgDistresses.Columns[index].HeaderText, "ESC", false) == 0 | Microsoft.VisualBasic.CompilerServices.Operators.CompareString(rgDistresses.Columns[index].HeaderText, "ESC\r\nNumber", false) == 0 | Microsoft.VisualBasic.CompilerServices.Operators.CompareString(rgDistresses.Columns[index].HeaderText, "Date\r\nCompleted", false) == 0)
              checked { num3 += rgDistresses.Columns[index].Width; }
            else
              checked { num1 += rgDistresses.Columns[index].Width; }
          }
          checked { ++index; }
        }
        this.lblESC.Left = num1;
        this.lblESC.Width = num3;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), nameof (PositionESCLabel));
        ProjectData.ClearProjectError();
      }
    }

    private void rgDistresses_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
    {
      string key = e.Cell.Column.Key;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "Subcomponent Qty", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "Distress Qty", false) != 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Cell.Text ?? "", "", false) <= 0U)
        return;
      if (!Versioned.IsNumeric((object) e.Cell.Text))
      {
        int num = (int) Interaction.MsgBox((object) "You must enter a numeric value for this field.", MsgBoxStyle.OkOnly, (object) null);
        e.Cancel = true;
      }
      else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value.ToString() ?? "", "", false) > 0U & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value.ToString() ?? "", "", false) > 0U && Conversions.ToDouble(this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value.ToString()) < Conversions.ToDouble(this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value.ToString()))
      {
        int num = (int) Interaction.MsgBox((object) "You cannot have more of the distress quantity than the subcomponent quantity.", MsgBoxStyle.Critical, (object) null);
        e.Cancel = true;
      }
    }

    private void rgDistresses_CellClick(object sender, GridViewCellEventArgs e)
    {
      if (e.Row is GridViewTableHeaderRowInfo || (Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.rgDistresses.CurrentRow.Cells["DistressDisplay"].Value)) || this.rgDistresses.CurrentRow.Cells["DistressDisplay"].Value == null))
        return;
      this.HelpProvider.SetHelpKeyword((Control) this.rgDistresses, this.rgDistresses.CurrentRow.Cells["DistressDisplay"].Value.ToString());
    }

    private void rgdistresses_CellEditorInitialized(object sender, GridViewCellEventArgs e)
    {
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Column.HeaderText, "Severity", false) != 0)
        return;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.rgDistresses.CurrentRow.Cells["DistressDisplay"].Value)) && this.rgDistresses.CurrentRow.Cells["DistressDisplay"].Value != null)
      {
        RadDropDownListEditorElement editorElement = (RadDropDownListEditorElement) ((BaseInputEditor) this.rgDistresses.ActiveEditor).EditorElement;
        DataTable dataTable = mdUtility.DB.GetDataTable(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT [Severity_ID] AS ID, [Severity_Desc] AS Description FROM RO_Distress_Severity_Values INNER JOIN RO_Distress_Severity ON RO_Distress_Severity_Values.Severity_ID = RO_Distress_Severity.DIST_SEV_SEV WHERE [DIST_SEV_DIST_LINK]=", this.rgDistresses.CurrentRow.Cells["DistressDisplay"].Value)));
        editorElement.DataSource = (object) dataTable;
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.rgDistresses.CurrentRow.Cells["SeverityDisplay"].Value)) && this.rgDistresses.CurrentRow.Cells["SeverityDisplay"].Value != null && ((IEnumerable<DataRow>) dataTable.Select("ID = " + this.rgDistresses.CurrentRow.Cells["SeverityDisplay"].Value.ToString())).Count<DataRow>() > 0)
        {
          editorElement.SelectedValue = RuntimeHelpers.GetObjectValue(this.rgDistresses.CurrentRow.Cells["SeverityDisplay"].Value);
        }
        else
        {
          editorElement.SelectedValue = (object) null;
          this.rgDistresses.CurrentRow.Cells["SeverityDisplay"].Value = (object) null;
        }
      }
      else
        e.ActiveEditor.EndEdit();
    }

    private void rgDistresses_CellValueChanged(object sender, GridViewCellEventArgs e)
    {
      try
      {
        string name = e.Column.Name;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "Subcomponent Qty", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "Distress Qty", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "Density", false) == 0 && this.m_bLoaded)
          {
            this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value = (object) DBNull.Value;
            this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value = (object) DBNull.Value;
          }
        }
        else
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value.ToString() ?? "", "", false) != 0 && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value.ToString() ?? "", "", false) > 0U)
          {
            double num = Conversions.ToDouble(this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value.ToString()) / Conversions.ToDouble(this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value.ToString());
            this.m_bLoaded = false;
            if (num <= 0.001)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 1;
            else if (num > 0.001 & num <= 0.01)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 2;
            else if (num > 0.01 & num <= 0.05)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 4;
            else if (num > 0.05 & num <= 0.1)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 8;
            else if (num > 0.1 & num <= 0.25)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 16;
            else if (num > 0.25 & num <= 0.5)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 32;
            else if (num > 0.5 & num < 1.0)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 64;
            else if (num >= 1.0)
              this.rgDistresses.CurrentRow.Cells["Density"].Value = (object) 96;
            this.m_bLoaded = true;
          }
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Column.HeaderText, "Subcomponent Qty", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(e.Value, (object) ""), (object) "", false) && this.m_dSubQty == -1.0)
            this.m_dSubQty = Conversions.ToDouble(e.Value);
        }
        if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.rgDistresses.CurrentRow.Cells["BRED_Status"].Value)))
          this.rgDistresses.CurrentRow.Cells["BRED_Status"].Value = (object) "C";
        else if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.rgDistresses.CurrentRow.Cells["BRED_Status"].Value, (object) "A", false))
          this.rgDistresses.CurrentRow.Cells["BRED_Status"].Value = (object) "C";
        this.SetChanged();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmDistresses), "rgDistresses_AfterCellUpdate");
        ProjectData.ClearProjectError();
      }
    }

    private void rgDistresses_CellValidating(object sender, CellValidatingEventArgs e)
    {
      if (e.Column == null)
        return;
      string name = e.Column.Name;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "Subcomponent Qty", false) != 0)
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "Distress Qty", false) == 0 && (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value.ToString() ?? "", "", false) != 0 && e.Value != null && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Value.ToString() ?? "", "", false) > 0U && Conversions.ToDouble(this.rgDistresses.CurrentRow.Cells["Subcomponent Qty"].Value.ToString()) < Conversions.ToDouble(e.Value.ToString())))
        {
          int num = (int) Interaction.MsgBox((object) "You cannot have more of the distress quantity than the subcomponent quantity.", MsgBoxStyle.Critical, (object) null);
          e.Cancel = true;
        }
      }
      else if (e.Value != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Value.ToString() ?? "", "", false) != 0 && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value.ToString() ?? "", "", false) > 0U && Conversions.ToDouble(e.Value.ToString()) < Conversions.ToDouble(this.rgDistresses.CurrentRow.Cells["Distress Qty"].Value.ToString()))
      {
        int num = (int) Interaction.MsgBox((object) "You cannot have more of the distress quantity than the subcomponent quantity.", MsgBoxStyle.Critical, (object) null);
        e.Cancel = true;
      }
    }

    private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {
    }
  }
}
