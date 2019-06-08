// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmNewSection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using BuilderRED.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmNewSection : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmNewSection";
    private string m_strComponentID;
    private int m_iCompLink;
    private int m_intBldgYear;
    private string m_strInspComments;
    private string m_strDirectComments;
    private string m_strPaintComments;
    private string m_strSectionComments;
    private bool nonNumberEntered;

    public frmNewSection()
    {
      this.Load += new EventHandler(this.frmNewSection_Load);
      this.nonNumberEntered = false;
      this.InitializeComponent();
      float num = this.CreateGraphics().DpiX / 96f;
      this.cmdIncrease.Size = new Size(checked ((int) Math.Round(unchecked ((double) this.cmdIncrease.Width * (double) num))), checked ((int) Math.Round(unchecked ((double) this.cmdIncrease.Height * (double) num))));
      this.cmdDecrease.Size = this.cmdIncrease.Size;
      this.cmdCalc.Size = this.cmdIncrease.Size;
      this.cmdInspComments.Size = this.cmdIncrease.Size;
    }

    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual CheckBox chkYearEst
    {
      get
      {
        return this._chkYearEst;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkYearEst_CheckStateChanged);
        CheckBox chkYearEst1 = this._chkYearEst;
        if (chkYearEst1 != null)
          chkYearEst1.CheckStateChanged -= eventHandler;
        this._chkYearEst = value;
        CheckBox chkYearEst2 = this._chkYearEst;
        if (chkYearEst2 == null)
          return;
        chkYearEst2.CheckStateChanged += eventHandler;
      }
    }

    public virtual TextBox txtDatePainted
    {
      get
      {
        return this._txtDatePainted;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtDatePainted_Validating);
        TextBox txtDatePainted1 = this._txtDatePainted;
        if (txtDatePainted1 != null)
          txtDatePainted1.Validating -= cancelEventHandler;
        this._txtDatePainted = value;
        TextBox txtDatePainted2 = this._txtDatePainted;
        if (txtDatePainted2 == null)
          return;
        txtDatePainted2.Validating += cancelEventHandler;
      }
    }

    public virtual CheckBox chkPainted
    {
      get
      {
        return this._chkPainted;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkPainted_CheckedChanged);
        CheckBox chkPainted1 = this._chkPainted;
        if (chkPainted1 != null)
          chkPainted1.CheckedChanged -= eventHandler;
        this._chkPainted = value;
        CheckBox chkPainted2 = this._chkPainted;
        if (chkPainted2 == null)
          return;
        chkPainted2.CheckedChanged += eventHandler;
      }
    }

    public virtual TextBox txtSectionYearBuilt
    {
      get
      {
        return this._txtSectionYearBuilt;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtSectionYearBuilt_Leave);
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtSectionYearBuilt_Validating);
        TextBox sectionYearBuilt1 = this._txtSectionYearBuilt;
        if (sectionYearBuilt1 != null)
        {
          sectionYearBuilt1.Leave -= eventHandler;
          sectionYearBuilt1.Validating -= cancelEventHandler;
        }
        this._txtSectionYearBuilt = value;
        TextBox sectionYearBuilt2 = this._txtSectionYearBuilt;
        if (sectionYearBuilt2 == null)
          return;
        sectionYearBuilt2.Leave += eventHandler;
        sectionYearBuilt2.Validating += cancelEventHandler;
      }
    }

    public virtual TextBox txtSectionAmount
    {
      get
      {
        return this._txtSectionAmount;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtSectionAmount_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtSectionAmount_KeyPress);
        EventHandler eventHandler = new EventHandler(this.txtSectionAmount_TextChanged);
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtSectionAmount_Validating);
        TextBox txtSectionAmount1 = this._txtSectionAmount;
        if (txtSectionAmount1 != null)
        {
          txtSectionAmount1.KeyDown -= keyEventHandler;
          txtSectionAmount1.KeyPress -= pressEventHandler;
          txtSectionAmount1.TextChanged -= eventHandler;
          txtSectionAmount1.Validating -= cancelEventHandler;
        }
        this._txtSectionAmount = value;
        TextBox txtSectionAmount2 = this._txtSectionAmount;
        if (txtSectionAmount2 == null)
          return;
        txtSectionAmount2.KeyDown += keyEventHandler;
        txtSectionAmount2.KeyPress += pressEventHandler;
        txtSectionAmount2.TextChanged += eventHandler;
        txtSectionAmount2.Validating += cancelEventHandler;
      }
    }

    public virtual Button CancelButton_Renamed
    {
      get
      {
        return this._CancelButton_Renamed;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.CancelButton_Click);
        Button cancelButtonRenamed1 = this._CancelButton_Renamed;
        if (cancelButtonRenamed1 != null)
          cancelButtonRenamed1.Click -= eventHandler;
        this._CancelButton_Renamed = value;
        Button cancelButtonRenamed2 = this._CancelButton_Renamed;
        if (cancelButtonRenamed2 == null)
          return;
        cancelButtonRenamed2.Click += eventHandler;
      }
    }

    public virtual Label lblPaintType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDatePainted { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblPainted { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblSectionYearBuilt { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblSectionAmount { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblUOM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblSectionName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblComponentType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboSectionName
    {
      get
      {
        return this._cboSectionName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboSectionName_TextChanged);
        EventHandler eventHandler2 = new EventHandler(this.cboSectionName_SelectedIndexChanged);
        ComboBox cboSectionName1 = this._cboSectionName;
        if (cboSectionName1 != null)
        {
          cboSectionName1.TextChanged -= eventHandler1;
          cboSectionName1.SelectedIndexChanged -= eventHandler2;
        }
        this._cboSectionName = value;
        ComboBox cboSectionName2 = this._cboSectionName;
        if (cboSectionName2 == null)
          return;
        cboSectionName2.TextChanged += eventHandler1;
        cboSectionName2.SelectedIndexChanged += eventHandler2;
      }
    }

    internal virtual ComboBox cboSectionMaterial
    {
      get
      {
        return this._cboSectionMaterial;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboSectionMaterial_SelectedIndexChanged);
        ComboBox cboSectionMaterial1 = this._cboSectionMaterial;
        if (cboSectionMaterial1 != null)
          cboSectionMaterial1.SelectedIndexChanged -= eventHandler;
        this._cboSectionMaterial = value;
        ComboBox cboSectionMaterial2 = this._cboSectionMaterial;
        if (cboSectionMaterial2 == null)
          return;
        cboSectionMaterial2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual ComboBox cboComponentType
    {
      get
      {
        return this._cboComponentType;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboComponentType_SelectedIndexChanged);
        ComboBox cboComponentType1 = this._cboComponentType;
        if (cboComponentType1 != null)
          cboComponentType1.SelectedIndexChanged -= eventHandler;
        this._cboComponentType = value;
        ComboBox cboComponentType2 = this._cboComponentType;
        if (cboComponentType2 == null)
          return;
        cboComponentType2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual ComboBox cboPaint { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual CheckBox chkAddInspection
    {
      get
      {
        return this._chkAddInspection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkAddInspection_CheckedChanged);
        CheckBox chkAddInspection1 = this._chkAddInspection;
        if (chkAddInspection1 != null)
          chkAddInspection1.CheckedChanged -= eventHandler;
        this._chkAddInspection = value;
        CheckBox chkAddInspection2 = this._chkAddInspection;
        if (chkAddInspection2 == null)
          return;
        chkAddInspection2.CheckedChanged += eventHandler;
      }
    }

    internal virtual GroupBox gbCurrentInspection { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual DateTimePicker dtpInspDate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblInspDate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboDirectRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblPaintRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboPaintRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button cmdInspComments
    {
      get
      {
        return this._cmdInspComments;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdInspComments_Click);
        Button cmdInspComments1 = this._cmdInspComments;
        if (cmdInspComments1 != null)
          cmdInspComments1.Click -= eventHandler;
        this._cmdInspComments = value;
        Button cmdInspComments2 = this._cmdInspComments;
        if (cmdInspComments2 == null)
          return;
        cmdInspComments2.Click += eventHandler;
      }
    }

    internal virtual CheckBox chkEnergyAuditRequired { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblEnergyAuditRequired { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblMaterial { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button OKButton
    {
      get
      {
        return this._OKButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.OKButton_Click);
        Button okButton1 = this._OKButton;
        if (okButton1 != null)
          okButton1.Click -= eventHandler;
        this._OKButton = value;
        Button okButton2 = this._OKButton;
        if (okButton2 == null)
          return;
        okButton2.Click += eventHandler;
      }
    }

    internal virtual FlowLayoutPanel FlowLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button cmdIncrease
    {
      get
      {
        return this._cmdIncrease;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdIncrease_Click);
        Button cmdIncrease1 = this._cmdIncrease;
        if (cmdIncrease1 != null)
          cmdIncrease1.Click -= eventHandler;
        this._cmdIncrease = value;
        Button cmdIncrease2 = this._cmdIncrease;
        if (cmdIncrease2 == null)
          return;
        cmdIncrease2.Click += eventHandler;
      }
    }

    internal virtual Button cmdDecrease
    {
      get
      {
        return this._cmdDecrease;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdDecrease_Click);
        Button cmdDecrease1 = this._cmdDecrease;
        if (cmdDecrease1 != null)
          cmdDecrease1.Click -= eventHandler;
        this._cmdDecrease = value;
        Button cmdDecrease2 = this._cmdDecrease;
        if (cmdDecrease2 == null)
          return;
        cmdDecrease2.Click += eventHandler;
      }
    }

    internal virtual Button cmdCalc
    {
      get
      {
        return this._cmdCalc;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCalc_Click);
        Button cmdCalc1 = this._cmdCalc;
        if (cmdCalc1 != null)
          cmdCalc1.Click -= eventHandler;
        this._cmdCalc = value;
        Button cmdCalc2 = this._cmdCalc;
        if (cmdCalc2 == null)
          return;
        cmdCalc2.Click += eventHandler;
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.txtDatePainted = new TextBox();
      this.cmdIncrease = new Button();
      this.cmdDecrease = new Button();
      this.cmdCalc = new Button();
      this.chkYearEst = new CheckBox();
      this.chkPainted = new CheckBox();
      this.txtSectionYearBuilt = new TextBox();
      this.txtSectionAmount = new TextBox();
      this.CancelButton_Renamed = new Button();
      this.lblPaintType = new Label();
      this.lblDatePainted = new Label();
      this.lblPainted = new Label();
      this.lblSectionYearBuilt = new Label();
      this.lblSectionAmount = new Label();
      this.lblUOM = new Label();
      this.lblSectionName = new Label();
      this.lblComponentType = new Label();
      this.cboSectionName = new ComboBox();
      this.cboSectionMaterial = new ComboBox();
      this.cboComponentType = new ComboBox();
      this.cboPaint = new ComboBox();
      this.HelpProvider = new HelpProvider();
      this.chkAddInspection = new CheckBox();
      this.gbCurrentInspection = new GroupBox();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.lblInspDate = new Label();
      this.cmdInspComments = new Button();
      this.lblRating = new Label();
      this.lblPaintRating = new Label();
      this.dtpInspDate = new DateTimePicker();
      this.cboPaintRating = new ComboBox();
      this.cboDirectRating = new ComboBox();
      this.chkEnergyAuditRequired = new CheckBox();
      this.lblEnergyAuditRequired = new Label();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.lblMaterial = new Label();
      this.OKButton = new Button();
      this.FlowLayoutPanel1 = new FlowLayoutPanel();
      this.btnComments = new Button();
      this.Label1 = new Label();
      this.cboFunctionalArea = new ComboBox();
      this.lblStatus = new Label();
      this.cboStatus = new ComboBox();
      this.gbCurrentInspection.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.FlowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.txtDatePainted.AcceptsReturn = true;
      this.txtDatePainted.BackColor = SystemColors.Window;
      this.txtDatePainted.Cursor = Cursors.IBeam;
      this.txtDatePainted.Dock = DockStyle.Fill;
      this.txtDatePainted.ForeColor = SystemColors.WindowText;
      this.txtDatePainted.Location = new Point(119, 248);
      this.txtDatePainted.MaxLength = 0;
      this.txtDatePainted.Name = "txtDatePainted";
      this.txtDatePainted.RightToLeft = RightToLeft.No;
      this.txtDatePainted.Size = new Size(73, 20);
      this.txtDatePainted.TabIndex = 7;
      this.ToolTip1.SetToolTip((Control) this.txtDatePainted, "Enter Year Painted in 'YYYY' format.");
      this.txtDatePainted.Visible = false;
      this.cmdIncrease.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
      this.cmdIncrease.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdIncrease.Location = new Point(13, 0);
      this.cmdIncrease.Margin = new Padding(0);
      this.cmdIncrease.Name = "cmdIncrease";
      this.cmdIncrease.Size = new Size(23, 23);
      this.cmdIncrease.TabIndex = 14;
      this.ToolTip1.SetToolTip((Control) this.cmdIncrease, "Increment Quantity");
      this.cmdIncrease.UseVisualStyleBackColor = true;
      this.cmdDecrease.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Symbol_Restricted_2;
      this.cmdDecrease.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdDecrease.Location = new Point(36, 0);
      this.cmdDecrease.Margin = new Padding(0);
      this.cmdDecrease.Name = "cmdDecrease";
      this.cmdDecrease.Size = new Size(23, 23);
      this.cmdDecrease.TabIndex = 15;
      this.ToolTip1.SetToolTip((Control) this.cmdDecrease, "Decrease Quantity");
      this.cmdDecrease.UseVisualStyleBackColor = true;
      this.cmdCalc.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Calculator_Accounting;
      this.cmdCalc.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCalc.Location = new Point(59, 0);
      this.cmdCalc.Margin = new Padding(0);
      this.cmdCalc.Name = "cmdCalc";
      this.cmdCalc.Size = new Size(23, 23);
      this.cmdCalc.TabIndex = 16;
      this.ToolTip1.SetToolTip((Control) this.cmdCalc, "Calculate Quantity");
      this.cmdCalc.UseVisualStyleBackColor = true;
      this.chkYearEst.BackColor = SystemColors.Control;
      this.chkYearEst.Cursor = Cursors.Default;
      this.chkYearEst.ForeColor = SystemColors.ControlText;
      this.chkYearEst.Location = new Point(198, 122);
      this.chkYearEst.Name = "chkYearEst";
      this.chkYearEst.RightToLeft = RightToLeft.No;
      this.chkYearEst.Size = new Size(105, 17);
      this.chkYearEst.TabIndex = 5;
      this.chkYearEst.Text = "Estimated";
      this.chkYearEst.UseVisualStyleBackColor = false;
      this.chkPainted.BackColor = SystemColors.Control;
      this.chkPainted.Cursor = Cursors.Default;
      this.chkPainted.Dock = DockStyle.Fill;
      this.chkPainted.ForeColor = SystemColors.ControlText;
      this.chkPainted.Location = new Point(119, 225);
      this.chkPainted.Name = "chkPainted";
      this.chkPainted.RightToLeft = RightToLeft.No;
      this.chkPainted.Size = new Size(73, 17);
      this.chkPainted.TabIndex = 6;
      this.chkPainted.UseVisualStyleBackColor = false;
      this.txtSectionYearBuilt.AcceptsReturn = true;
      this.txtSectionYearBuilt.BackColor = SystemColors.Window;
      this.txtSectionYearBuilt.Cursor = Cursors.IBeam;
      this.txtSectionYearBuilt.Dock = DockStyle.Fill;
      this.txtSectionYearBuilt.ForeColor = SystemColors.WindowText;
      this.txtSectionYearBuilt.Location = new Point(119, 122);
      this.txtSectionYearBuilt.MaxLength = 0;
      this.txtSectionYearBuilt.Name = "txtSectionYearBuilt";
      this.txtSectionYearBuilt.RightToLeft = RightToLeft.No;
      this.txtSectionYearBuilt.Size = new Size(73, 20);
      this.txtSectionYearBuilt.TabIndex = 4;
      this.txtSectionAmount.AcceptsReturn = true;
      this.txtSectionAmount.BackColor = SystemColors.Window;
      this.txtSectionAmount.Cursor = Cursors.IBeam;
      this.txtSectionAmount.Dock = DockStyle.Fill;
      this.txtSectionAmount.ForeColor = SystemColors.WindowText;
      this.txtSectionAmount.Location = new Point(119, 96);
      this.txtSectionAmount.MaxLength = 0;
      this.txtSectionAmount.Name = "txtSectionAmount";
      this.txtSectionAmount.RightToLeft = RightToLeft.No;
      this.txtSectionAmount.Size = new Size(73, 20);
      this.txtSectionAmount.TabIndex = 3;
      this.CancelButton_Renamed.BackColor = SystemColors.Control;
      this.CancelButton_Renamed.Cursor = Cursors.Default;
      this.CancelButton_Renamed.DialogResult = DialogResult.Cancel;
      this.CancelButton_Renamed.Dock = DockStyle.Fill;
      this.CancelButton_Renamed.ForeColor = SystemColors.ControlText;
      this.CancelButton_Renamed.Location = new Point(458, 34);
      this.CancelButton_Renamed.Name = "CancelButton_Renamed";
      this.CancelButton_Renamed.RightToLeft = RightToLeft.No;
      this.CancelButton_Renamed.Size = new Size(81, 25);
      this.CancelButton_Renamed.TabIndex = 12;
      this.CancelButton_Renamed.Text = "Cancel";
      this.CancelButton_Renamed.UseVisualStyleBackColor = false;
      this.lblPaintType.AutoSize = true;
      this.lblPaintType.BackColor = SystemColors.Control;
      this.lblPaintType.Cursor = Cursors.Default;
      this.lblPaintType.Dock = DockStyle.Fill;
      this.lblPaintType.ForeColor = SystemColors.ControlText;
      this.lblPaintType.Location = new Point(3, 271);
      this.lblPaintType.Name = "lblPaintType";
      this.lblPaintType.RightToLeft = RightToLeft.No;
      this.lblPaintType.Size = new Size(110, 20);
      this.lblPaintType.TabIndex = 19;
      this.lblPaintType.Text = "Paint/Coating Type:";
      this.lblPaintType.TextAlign = ContentAlignment.MiddleRight;
      this.lblPaintType.Visible = false;
      this.lblDatePainted.AutoSize = true;
      this.lblDatePainted.BackColor = SystemColors.Control;
      this.lblDatePainted.Cursor = Cursors.Default;
      this.lblDatePainted.Dock = DockStyle.Fill;
      this.lblDatePainted.ForeColor = SystemColors.ControlText;
      this.lblDatePainted.Location = new Point(3, 245);
      this.lblDatePainted.Name = "lblDatePainted";
      this.lblDatePainted.RightToLeft = RightToLeft.No;
      this.lblDatePainted.Size = new Size(110, 26);
      this.lblDatePainted.TabIndex = 17;
      this.lblDatePainted.Text = "Year Painted/Coated:";
      this.lblDatePainted.TextAlign = ContentAlignment.MiddleRight;
      this.lblDatePainted.Visible = false;
      this.lblPainted.AutoSize = true;
      this.lblPainted.BackColor = SystemColors.Control;
      this.lblPainted.Cursor = Cursors.Default;
      this.lblPainted.Dock = DockStyle.Fill;
      this.lblPainted.ForeColor = SystemColors.ControlText;
      this.lblPainted.Location = new Point(3, 222);
      this.lblPainted.Name = "lblPainted";
      this.lblPainted.RightToLeft = RightToLeft.No;
      this.lblPainted.Size = new Size(110, 23);
      this.lblPainted.TabIndex = 16;
      this.lblPainted.Text = "Painted/Coated:";
      this.lblPainted.TextAlign = ContentAlignment.MiddleRight;
      this.lblSectionYearBuilt.AutoSize = true;
      this.lblSectionYearBuilt.BackColor = SystemColors.Control;
      this.lblSectionYearBuilt.Cursor = Cursors.Default;
      this.lblSectionYearBuilt.Dock = DockStyle.Fill;
      this.lblSectionYearBuilt.ForeColor = SystemColors.ControlText;
      this.lblSectionYearBuilt.Location = new Point(3, 119);
      this.lblSectionYearBuilt.Name = "lblSectionYearBuilt";
      this.lblSectionYearBuilt.RightToLeft = RightToLeft.No;
      this.lblSectionYearBuilt.Size = new Size(110, 26);
      this.lblSectionYearBuilt.TabIndex = 15;
      this.lblSectionYearBuilt.Text = "Year Built:";
      this.lblSectionYearBuilt.TextAlign = ContentAlignment.MiddleRight;
      this.lblSectionAmount.AutoSize = true;
      this.lblSectionAmount.BackColor = SystemColors.Control;
      this.lblSectionAmount.Cursor = Cursors.Default;
      this.lblSectionAmount.Dock = DockStyle.Fill;
      this.lblSectionAmount.ForeColor = SystemColors.ControlText;
      this.lblSectionAmount.Location = new Point(3, 93);
      this.lblSectionAmount.Name = "lblSectionAmount";
      this.lblSectionAmount.RightToLeft = RightToLeft.No;
      this.lblSectionAmount.Size = new Size(110, 26);
      this.lblSectionAmount.TabIndex = 14;
      this.lblSectionAmount.Text = "Quantity:";
      this.lblSectionAmount.TextAlign = ContentAlignment.MiddleRight;
      this.lblUOM.AutoSize = true;
      this.lblUOM.BackColor = SystemColors.Control;
      this.lblUOM.Cursor = Cursors.Default;
      this.lblUOM.ForeColor = SystemColors.ControlText;
      this.lblUOM.Location = new Point(3, 3);
      this.lblUOM.Margin = new Padding(3, 3, 10, 3);
      this.lblUOM.Name = "lblUOM";
      this.lblUOM.RightToLeft = RightToLeft.No;
      this.lblUOM.Size = new Size(0, 13);
      this.lblUOM.TabIndex = 13;
      this.lblUOM.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSectionName.AutoSize = true;
      this.lblSectionName.BackColor = SystemColors.Control;
      this.lblSectionName.Cursor = Cursors.Default;
      this.lblSectionName.Dock = DockStyle.Fill;
      this.lblSectionName.ForeColor = SystemColors.ControlText;
      this.lblSectionName.Location = new Point(3, 0);
      this.lblSectionName.Name = "lblSectionName";
      this.lblSectionName.RightToLeft = RightToLeft.No;
      this.lblSectionName.Size = new Size(110, 31);
      this.lblSectionName.TabIndex = 12;
      this.lblSectionName.Text = "Section Name:";
      this.lblSectionName.TextAlign = ContentAlignment.MiddleRight;
      this.lblComponentType.AutoSize = true;
      this.lblComponentType.BackColor = SystemColors.Control;
      this.lblComponentType.Cursor = Cursors.Default;
      this.lblComponentType.Dock = DockStyle.Fill;
      this.lblComponentType.ForeColor = SystemColors.ControlText;
      this.lblComponentType.Location = new Point(3, 62);
      this.lblComponentType.Name = "lblComponentType";
      this.lblComponentType.RightToLeft = RightToLeft.No;
      this.lblComponentType.Size = new Size(110, 31);
      this.lblComponentType.TabIndex = 10;
      this.lblComponentType.Text = "Component Type:";
      this.lblComponentType.TextAlign = ContentAlignment.MiddleRight;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.cboSectionName, 2);
      this.cboSectionName.Dock = DockStyle.Fill;
      this.cboSectionName.Location = new Point(119, 3);
      this.cboSectionName.MaxLength = 50;
      this.cboSectionName.Name = "cboSectionName";
      this.cboSectionName.Size = new Size(333, 21);
      this.cboSectionName.TabIndex = 0;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.cboSectionMaterial, 2);
      this.cboSectionMaterial.Dock = DockStyle.Fill;
      this.cboSectionMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSectionMaterial.Location = new Point(119, 34);
      this.cboSectionMaterial.Name = "cboSectionMaterial";
      this.cboSectionMaterial.Size = new Size(333, 21);
      this.cboSectionMaterial.TabIndex = 1;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.cboComponentType, 2);
      this.cboComponentType.Dock = DockStyle.Fill;
      this.cboComponentType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboComponentType.DropDownWidth = 500;
      this.cboComponentType.Location = new Point(119, 65);
      this.cboComponentType.Name = "cboComponentType";
      this.cboComponentType.Size = new Size(333, 21);
      this.cboComponentType.TabIndex = 2;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.cboPaint, 2);
      this.cboPaint.Dock = DockStyle.Fill;
      this.cboPaint.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPaint.Location = new Point(119, 274);
      this.cboPaint.Name = "cboPaint";
      this.cboPaint.Size = new Size(333, 21);
      this.cboPaint.TabIndex = 8;
      this.cboPaint.Visible = false;
      this.chkAddInspection.AutoSize = true;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.chkAddInspection, 2);
      this.chkAddInspection.Dock = DockStyle.Fill;
      this.chkAddInspection.Location = new Point(3, 294);
      this.chkAddInspection.Name = "chkAddInspection";
      this.chkAddInspection.Size = new Size(189, 17);
      this.chkAddInspection.TabIndex = 9;
      this.chkAddInspection.Text = "Add current inspection";
      this.gbCurrentInspection.AutoSize = true;
      this.gbCurrentInspection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.gbCurrentInspection, 3);
      this.gbCurrentInspection.Controls.Add((Control) this.TableLayoutPanel2);
      this.gbCurrentInspection.Dock = DockStyle.Fill;
      this.gbCurrentInspection.Enabled = false;
      this.gbCurrentInspection.Location = new Point(3, 317);
      this.gbCurrentInspection.Name = "gbCurrentInspection";
      this.gbCurrentInspection.Size = new Size(449, 153);
      this.gbCurrentInspection.TabIndex = 10;
      this.gbCurrentInspection.TabStop = false;
      this.gbCurrentInspection.Text = "Current Inspection Info";
      this.TableLayoutPanel2.AutoSize = true;
      this.TableLayoutPanel2.ColumnCount = 3;
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.Controls.Add((Control) this.lblInspDate, 0, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.cmdInspComments, 2, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblRating, 0, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblPaintRating, 0, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.dtpInspDate, 1, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.cboPaintRating, 1, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.cboDirectRating, 1, 1);
      this.TableLayoutPanel2.Dock = DockStyle.Fill;
      this.TableLayoutPanel2.Location = new Point(3, 16);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 4;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel2.Size = new Size(443, 134);
      this.TableLayoutPanel2.TabIndex = 34;
      this.lblInspDate.AutoSize = true;
      this.lblInspDate.Cursor = Cursors.Default;
      this.lblInspDate.Dock = DockStyle.Fill;
      this.lblInspDate.Location = new Point(3, 0);
      this.lblInspDate.Name = "lblInspDate";
      this.lblInspDate.RightToLeft = RightToLeft.No;
      this.lblInspDate.Size = new Size(85, 26);
      this.lblInspDate.TabIndex = 26;
      this.lblInspDate.Text = "Inspection Date:";
      this.lblInspDate.TextAlign = ContentAlignment.MiddleRight;
      this.cmdInspComments.BackColor = SystemColors.Control;
      this.cmdInspComments.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.cmdInspComments.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdInspComments.Cursor = Cursors.Default;
      this.cmdInspComments.ForeColor = SystemColors.ControlText;
      this.cmdInspComments.Location = new Point(225, 0);
      this.cmdInspComments.Margin = new Padding(0);
      this.cmdInspComments.Name = "cmdInspComments";
      this.cmdInspComments.Size = new Size(24, 25);
      this.cmdInspComments.TabIndex = 33;
      this.cmdInspComments.UseVisualStyleBackColor = false;
      this.lblRating.AutoSize = true;
      this.lblRating.Cursor = Cursors.Default;
      this.lblRating.Dock = DockStyle.Fill;
      this.lblRating.Location = new Point(3, 26);
      this.lblRating.Name = "lblRating";
      this.lblRating.RightToLeft = RightToLeft.No;
      this.lblRating.Size = new Size(85, 27);
      this.lblRating.TabIndex = 28;
      this.lblRating.Text = "Direct Rating:";
      this.lblRating.TextAlign = ContentAlignment.MiddleRight;
      this.lblPaintRating.AutoSize = true;
      this.lblPaintRating.Cursor = Cursors.Default;
      this.lblPaintRating.Dock = DockStyle.Fill;
      this.lblPaintRating.Location = new Point(3, 53);
      this.lblPaintRating.Name = "lblPaintRating";
      this.lblPaintRating.RightToLeft = RightToLeft.No;
      this.lblPaintRating.Size = new Size(85, 27);
      this.lblPaintRating.TabIndex = 30;
      this.lblPaintRating.Text = "Paint Rating:";
      this.lblPaintRating.TextAlign = ContentAlignment.MiddleRight;
      this.lblPaintRating.Visible = false;
      this.dtpInspDate.CustomFormat = "";
      this.dtpInspDate.Dock = DockStyle.Fill;
      this.dtpInspDate.Format = DateTimePickerFormat.Short;
      this.dtpInspDate.Location = new Point(94, 3);
      this.dtpInspDate.Name = "dtpInspDate";
      this.dtpInspDate.Size = new Size(128, 20);
      this.dtpInspDate.TabIndex = 0;
      this.cboPaintRating.Dock = DockStyle.Fill;
      this.cboPaintRating.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboPaintRating.Items.AddRange(new object[9]
      {
        (object) "G+",
        (object) "G",
        (object) "G-",
        (object) "A+",
        (object) "A",
        (object) "A-",
        (object) "R+",
        (object) "R",
        (object) "R-"
      });
      this.cboPaintRating.Location = new Point(94, 56);
      this.cboPaintRating.Name = "cboPaintRating";
      this.cboPaintRating.Size = new Size(128, 21);
      this.cboPaintRating.TabIndex = 2;
      this.cboPaintRating.Visible = false;
      this.cboDirectRating.Dock = DockStyle.Fill;
      this.cboDirectRating.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboDirectRating.Items.AddRange(new object[9]
      {
        (object) "G+",
        (object) "G",
        (object) "G-",
        (object) "A+",
        (object) "A",
        (object) "A-",
        (object) "R+",
        (object) "R",
        (object) "R-"
      });
      this.cboDirectRating.Location = new Point(94, 29);
      this.cboDirectRating.Name = "cboDirectRating";
      this.cboDirectRating.Size = new Size(128, 21);
      this.cboDirectRating.TabIndex = 1;
      this.chkEnergyAuditRequired.BackColor = SystemColors.Control;
      this.chkEnergyAuditRequired.Cursor = Cursors.Default;
      this.chkEnergyAuditRequired.Dock = DockStyle.Fill;
      this.chkEnergyAuditRequired.ForeColor = SystemColors.ControlText;
      this.chkEnergyAuditRequired.Location = new Point(119, 175);
      this.chkEnergyAuditRequired.Name = "chkEnergyAuditRequired";
      this.chkEnergyAuditRequired.RightToLeft = RightToLeft.No;
      this.chkEnergyAuditRequired.Size = new Size(73, 17);
      this.chkEnergyAuditRequired.TabIndex = 9;
      this.chkEnergyAuditRequired.UseVisualStyleBackColor = false;
      this.lblEnergyAuditRequired.AutoSize = true;
      this.lblEnergyAuditRequired.Dock = DockStyle.Fill;
      this.lblEnergyAuditRequired.Location = new Point(3, 172);
      this.lblEnergyAuditRequired.Name = "lblEnergyAuditRequired";
      this.lblEnergyAuditRequired.Size = new Size(110, 23);
      this.lblEnergyAuditRequired.TabIndex = 21;
      this.lblEnergyAuditRequired.Text = "NOT Energy Efficient:";
      this.lblEnergyAuditRequired.TextAlign = ContentAlignment.MiddleRight;
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel1.ColumnCount = 4;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.chkEnergyAuditRequired, 1, 8);
      this.TableLayoutPanel1.Controls.Add((Control) this.chkYearEst, 2, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboPaint, 1, 12);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPaintType, 0, 12);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblEnergyAuditRequired, 0, 8);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtDatePainted, 1, 11);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboSectionName, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.chkPainted, 1, 10);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblDatePainted, 0, 11);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblSectionName, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboSectionMaterial, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboComponentType, 1, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblMaterial, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtSectionYearBuilt, 1, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPainted, 0, 10);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblComponentType, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtSectionAmount, 1, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblSectionYearBuilt, 0, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblSectionAmount, 0, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.gbCurrentInspection, 0, 14);
      this.TableLayoutPanel1.Controls.Add((Control) this.chkAddInspection, 0, 13);
      this.TableLayoutPanel1.Controls.Add((Control) this.OKButton, 3, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.CancelButton_Renamed, 3, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.FlowLayoutPanel1, 2, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnComments, 3, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.Label1, 0, 5);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboFunctionalArea, 1, 5);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblStatus, 0, 9);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboStatus, 1, 9);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 15;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.TableLayoutPanel1.Size = new Size(542, 473);
      this.TableLayoutPanel1.TabIndex = 22;
      this.lblMaterial.AutoSize = true;
      this.lblMaterial.BackColor = SystemColors.Control;
      this.lblMaterial.Cursor = Cursors.Default;
      this.lblMaterial.Dock = DockStyle.Fill;
      this.lblMaterial.ForeColor = SystemColors.ControlText;
      this.lblMaterial.Location = new Point(3, 31);
      this.lblMaterial.Name = "lblMaterial";
      this.lblMaterial.RightToLeft = RightToLeft.No;
      this.lblMaterial.Size = new Size(110, 31);
      this.lblMaterial.TabIndex = 11;
      this.lblMaterial.Text = "Material Category:";
      this.lblMaterial.TextAlign = ContentAlignment.MiddleRight;
      this.OKButton.BackColor = SystemColors.Control;
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.Dock = DockStyle.Fill;
      this.OKButton.ForeColor = SystemColors.ControlText;
      this.OKButton.Location = new Point(458, 3);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 25);
      this.OKButton.TabIndex = 12;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = false;
      this.FlowLayoutPanel1.AutoSize = true;
      this.FlowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.FlowLayoutPanel1.Controls.Add((Control) this.lblUOM);
      this.FlowLayoutPanel1.Controls.Add((Control) this.cmdIncrease);
      this.FlowLayoutPanel1.Controls.Add((Control) this.cmdDecrease);
      this.FlowLayoutPanel1.Controls.Add((Control) this.cmdCalc);
      this.FlowLayoutPanel1.Dock = DockStyle.Fill;
      this.FlowLayoutPanel1.Location = new Point(195, 93);
      this.FlowLayoutPanel1.Margin = new Padding(0);
      this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
      this.FlowLayoutPanel1.Size = new Size(260, 26);
      this.FlowLayoutPanel1.TabIndex = 23;
      this.btnComments.BackColor = SystemColors.Control;
      this.btnComments.Cursor = Cursors.Default;
      this.btnComments.Dock = DockStyle.Fill;
      this.btnComments.ForeColor = SystemColors.ControlText;
      this.btnComments.Location = new Point(458, 65);
      this.btnComments.Name = "btnComments";
      this.btnComments.RightToLeft = RightToLeft.No;
      this.btnComments.Size = new Size(81, 25);
      this.btnComments.TabIndex = 24;
      this.btnComments.Text = "Comments";
      this.btnComments.UseVisualStyleBackColor = false;
      this.Label1.AutoSize = true;
      this.Label1.Dock = DockStyle.Fill;
      this.Label1.Location = new Point(3, 145);
      this.Label1.Name = "Label1";
      this.Label1.Size = new Size(110, 27);
      this.Label1.TabIndex = 25;
      this.Label1.Text = "Functional Area:";
      this.Label1.TextAlign = ContentAlignment.MiddleRight;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.cboFunctionalArea, 2);
      this.cboFunctionalArea.Dock = DockStyle.Fill;
      this.cboFunctionalArea.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboFunctionalArea.FormattingEnabled = true;
      this.cboFunctionalArea.Location = new Point(119, 148);
      this.cboFunctionalArea.Name = "cboFunctionalArea";
      this.cboFunctionalArea.Size = new Size(333, 21);
      this.cboFunctionalArea.TabIndex = 26;
      this.lblStatus.AutoSize = true;
      this.lblStatus.Dock = DockStyle.Right;
      this.lblStatus.Location = new Point(34, 195);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(79, 27);
      this.lblStatus.TabIndex = 27;
      this.lblStatus.Text = "Section Status:";
      this.lblStatus.TextAlign = ContentAlignment.MiddleRight;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.cboStatus, 2);
      this.cboStatus.Dock = DockStyle.Fill;
      this.cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboStatus.FormattingEnabled = true;
      this.cboStatus.Location = new Point(119, 198);
      this.cboStatus.Name = "cboStatus";
      this.cboStatus.Size = new Size(333, 21);
      this.cboStatus.TabIndex = 28;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(542, 473);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.Location = new Point(184, 250);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(558, 489);
      this.Name = nameof (frmNewSection);
      this.RightToLeft = RightToLeft.No;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add New Section";
      this.gbCurrentInspection.ResumeLayout(false);
      this.gbCurrentInspection.PerformLayout();
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.FlowLayoutPanel1.ResumeLayout(false);
      this.FlowLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button btnComments
    {
      get
      {
        return this._btnComments;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnComments_Click);
        Button btnComments1 = this._btnComments;
        if (btnComments1 != null)
          btnComments1.Click -= eventHandler;
        this._btnComments = value;
        Button btnComments2 = this._btnComments;
        if (btnComments2 == null)
          return;
        btnComments2.Click += eventHandler;
      }
    }

    internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboFunctionalArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void CancelButton_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }

    private void chkYearEst_CheckStateChanged(object eventSender, EventArgs eventArgs)
    {
      if (this.chkYearEst.CheckState == CheckState.Checked)
        this.txtSectionYearBuilt.BackColor = Color.Yellow;
      else
        this.txtSectionYearBuilt.BackColor = SystemColors.Window;
    }

    private void chkPainted_CheckedChanged(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (this.chkPainted.CheckState == CheckState.Checked)
        {
          this.lblDatePainted.Visible = true;
          this.txtDatePainted.Visible = true;
          this.lblPaintType.Visible = true;
          this.cboPaint.Visible = true;
          this.lblPaintRating.Visible = this.chkPainted.Checked;
          this.cboPaintRating.Visible = this.chkPainted.Checked;
          string InstallationID = Building.Installation(BuildingSystem.Building(Component.BuildingSystem(this.m_strComponentID)));
          DataTable dataTable = mdUtility.DB.GetDataTable("SELECT PAINT_LIFE FROM qryRSLbyCMC WHERE [CMC_ID]=" + Conversions.ToString(Conversions.ToInteger(this.cboComponentType.SelectedValue)) + " AND ([HVAC_Zone]=" + Conversions.ToString(Installation.HVACZone(ref InstallationID)) + " OR [HVAC_Zone]=0)");
          if (dataTable.Rows.Count > 0)
          {
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["PAINT_LIFE"])))
            {
              int integer = Conversions.ToInteger(dataTable.Rows[0]["PAINT_LIFE"]);
              int num = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? checked (DateAndTime.Year(DateAndTime.Now) - this.m_intBldgYear) : checked (DateAndTime.Year(DateAndTime.Now) - Conversions.ToInteger(this.txtSectionYearBuilt.Text));
              if ((double) num / (double) integer > 1.5)
                this.txtDatePainted.Text = Conversions.ToString(checked (DateAndTime.Year(DateAndTime.Now) - unchecked (num % integer)));
              else
                this.txtDatePainted.Text = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? Conversions.ToString(this.m_intBldgYear) : this.txtSectionYearBuilt.Text;
            }
            else
              this.txtDatePainted.Text = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? Conversions.ToString(this.m_intBldgYear) : this.txtSectionYearBuilt.Text;
          }
          else
            this.txtDatePainted.Text = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? Conversions.ToString(this.m_intBldgYear) : this.txtSectionYearBuilt.Text;
        }
        else
        {
          this.lblDatePainted.Visible = false;
          this.txtDatePainted.Visible = false;
          this.lblPaintType.Visible = false;
          this.cboPaint.Visible = false;
          this.cboPaint.SelectedIndex = -1;
          this.lblPaintRating.Visible = false;
          this.cboPaintRating.Visible = false;
          this.cboPaintRating.SelectedIndex = -1;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), "chkPainted_CheckStateChanged");
        ProjectData.ClearProjectError();
      }
    }

    private void frmNewSection_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (MySettingsProperty.Settings.AddSectionFormSize != new Size(0, 0))
          this.Size = MySettingsProperty.Settings.AddSectionFormSize;
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "New Section");
        this.txtSectionAmount.MaxLength = 10;
        this.txtSectionYearBuilt.MaxLength = 4;
        this.lblEnergyAuditRequired.Visible = mdUtility.UseEnergyForm;
        this.chkEnergyAuditRequired.Visible = mdUtility.UseEnergyForm;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (frmNewSection_Load));
        ProjectData.ClearProjectError();
      }
    }

    private void btnComments_Click(object sender, EventArgs e)
    {
      this.m_strSectionComments = new frmComment().NewComment("Comments for new section", (object) this.m_strSectionComments, true);
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (!this.OkToSave())
          return;
        string str1 = Section.AddSection(this.m_strComponentID, this.cboSectionName.Text, Conversions.ToInteger(this.cboSectionMaterial.SelectedValue), Conversions.ToInteger(this.cboComponentType.SelectedValue), Conversions.ToDouble(this.txtSectionAmount.Text), Strings.Trim(this.txtSectionYearBuilt.Text), (uint) this.chkYearEst.CheckState > 0U, this.m_strSectionComments, Conversions.ToString(this.cboFunctionalArea.SelectedValue != null ? this.cboFunctionalArea.SelectedValue : (object) null), Conversions.ToInteger(this.cboStatus.SelectedValue), (uint) this.chkPainted.CheckState > 0U, (uint) this.chkEnergyAuditRequired.CheckState > 0U, Strings.Trim(this.txtDatePainted.Text), this.cboPaint.SelectedValue.ToString());
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str1, "", false) > 0U)
        {
          if (this.chkAddInspection.Checked)
            Inspection.AddInspection(str1, this.dtpInspDate.Value, false, Inspection.InspectionType.DirectRating, this.m_strInspComments, "", checked (this.cboDirectRating.SelectedIndex + 1), this.m_strDirectComments, checked (this.cboPaintRating.SelectedIndex + 1), this.m_strPaintComments);
          frmMain fMainForm = mdUtility.fMainForm;
          ref string local1 = ref this.m_strComponentID;
          ref string local2 = ref str1;
          string str2 = Section.SectionLabel(str1);
          ref string local3 = ref str2;
          fMainForm.SectionAdded(ref local1, ref local2, ref local3);
          this.Close();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (OKButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void txtDatePainted_Validating(object eventSender, CancelEventArgs eventArgs)
    {
      bool flag = eventArgs.Cancel;
      try
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtDatePainted.Text, "", false) > 0U)
        {
          if (!Versioned.IsNumeric((object) this.txtDatePainted.Text))
          {
            int num = (int) Interaction.MsgBox((object) "Please enter a valid year for this field.", MsgBoxStyle.OkOnly, (object) null);
            flag = true;
          }
          else if (Conversions.ToInteger(this.txtDatePainted.Text) < 1776 | Conversions.ToInteger(this.txtDatePainted.Text) > 2100)
          {
            int num = (int) Interaction.MsgBox((object) "Please enter a year between 1776 and 2100 (all four digits).", MsgBoxStyle.OkOnly, (object) null);
            this.txtDatePainted.Focus();
            flag = true;
          }
        }
        eventArgs.Cancel = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (txtDatePainted_Validating));
        eventArgs.Cancel = true;
        ProjectData.ClearProjectError();
      }
    }

    private void txtSectionAmount_KeyDown(object sender, KeyEventArgs e)
    {
      this.nonNumberEntered = false;
      if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete))
        return;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "EA", false) > 0U)
      {
        if (e.KeyCode != Keys.Decimal && e.KeyCode != Keys.OemPeriod || Strings.InStr(this.txtSectionAmount.Text, ".", CompareMethod.Binary) > 1)
          this.nonNumberEntered = true;
      }
      else
        this.nonNumberEntered = true;
    }

    private void txtSectionAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.nonNumberEntered)
        return;
      e.Handled = true;
    }

    private void txtSectionAmount_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.CheckForm();
    }

    private void txtSectionAmount_Validating(object eventSender, CancelEventArgs eventArgs)
    {
      bool flag = eventArgs.Cancel;
      try
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionAmount.Text, "", false) > 0U && !Versioned.IsNumeric((object) this.txtSectionAmount.Text))
        {
          int num = (int) Interaction.MsgBox((object) "Please enter a valid number for the quantity", MsgBoxStyle.OkOnly, (object) null);
          flag = true;
        }
        eventArgs.Cancel = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (txtSectionAmount_Validating));
        eventArgs.Cancel = true;
        ProjectData.ClearProjectError();
      }
    }

    public void AddSection(ref string ComponentID)
    {
      try
      {
        this.m_strComponentID = ComponentID;
        this.m_iCompLink = Component.ComponentLink(ComponentID);
        string key = mdUtility.fMainForm.tvInventory.GetNodeByKey(this.m_strComponentID).Parent.Parent.Key;
        this.LoadExistingSectionNames(key);
        DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT Comp_Is_Equip FROM qryComponentProps WHERE [sys_comp_id]={" + this.m_strComponentID + "}");
        if (dataTable1.Rows.Count == 0)
          throw new Exception("Unable to add section. Component was not found.");
        if (Conversions.ToBoolean(dataTable1.Rows[0]["Comp_Is_Equip"]))
        {
          this.lblMaterial.Text = "Equipment Category:";
          this.lblSectionYearBuilt.Text = "Year Installed/Renewed";
        }
        else
        {
          this.lblMaterial.Text = "Material Category:";
          this.lblSectionYearBuilt.Text = "Year Built/Renewed";
        }
        this.cboSectionName.Text = "N/A";
        this.m_intBldgYear = Building.BuiltYear(key);
        if (this.m_intBldgYear == -1)
          this.m_intBldgYear = 0;
        this.cboPaint.ValueMember = "Paint_TYPE_ID";
        this.cboPaint.DisplayMember = "PAINT_TYPE_DESC";
        this.cboPaint.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM qryPaintTypes");
        this.cboFunctionalArea.ValueMember = "Area_ID";
        this.cboFunctionalArea.DisplayMember = "Name";
        this.cboFunctionalArea.DataSource = (object) mdUtility.DB.GetDataTable("SELECT Functional_Area.Area_ID, Functional_Area.Name FROM Functional_Area INNER JOIN (Facility INNER JOIN (Building_System INNER JOIN System_Component ON Building_System.BLDG_SYS_ID = System_Component.SYS_COMP_BLDG_SYS_ID) ON Facility.Facility_ID = Building_System.BLDG_SYS_BLDG_ID) ON Functional_Area.BLDG_ID = Facility.Facility_ID WHERE System_Component.Sys_Comp_ID = {" + this.m_strComponentID + "}");
        this.cboStatus.ValueMember = "ID";
        this.cboStatus.DisplayMember = "Description";
        this.cboStatus.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM RO_ComponentSection_Status");
        DataRow row = ((DataTable) this.cboFunctionalArea.DataSource).NewRow();
        row["Area_ID"] = (object) DBNull.Value;
        row["Name"] = (object) "";
        ((DataTable) this.cboFunctionalArea.DataSource).Rows.InsertAt(row, 0);
        this.cboFunctionalArea.SelectedValue = (object) 0;
        string sSQL = !mdUtility.UseUniformat ? "SELECT [MAT_CAT_ID], mat_cat_desc FROM qryMaterialCategories WHERE [cmc_comp_link]=" + Conversions.ToString(this.m_iCompLink) : "SELECT [MAT_CAT_ID], mat_cat_desc FROM qryMaterialCategoriesUniformat WHERE [cmc_comp_uii_link]=" + Conversions.ToString(this.m_iCompLink);
        this.cboSectionMaterial.DisplayMember = "mat_cat_desc";
        this.cboSectionMaterial.ValueMember = "MAT_CAT_ID";
        DataTable dataTable2 = mdUtility.DB.GetDataTable(sSQL);
        this.cboSectionMaterial.DataSource = (object) dataTable2;
        this.dtpInspDate.Value = DateAndTime.Today;
        if (dataTable2.Rows.Count > 0)
        {
          int num1 = (int) this.ShowDialog();
        }
        else
        {
          int num2 = (int) Interaction.MsgBox((object) "Unable to add section.  There are no section level categories to choose from.", MsgBoxStyle.Information, (object) null);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (AddSection));
        ProjectData.ClearProjectError();
      }
    }

    private void CheckForm()
    {
      try
      {
        bool flag = true;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboSectionName.Text, "", false) == 0)
          flag = false;
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboSectionMaterial.Text, "", false) == 0)
          flag = false;
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboComponentType.Text, "", false) == 0)
          flag = false;
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionAmount.Text, "", false) == 0)
          flag = false;
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboSectionName.Text, "N/A", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboSectionMaterial.Text, "N/A", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboComponentType.Text, "N/A", false) == 0)
          flag = false;
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboStatus.Text, "N/A", false) == 0)
          flag = false;
        this.OKButton.Enabled = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (CheckForm));
        ProjectData.ClearProjectError();
      }
    }

    private void txtSectionYearBuilt_Leave(object eventSender, EventArgs eventArgs)
    {
      if (Interaction.MsgBox((object) "Are you sure of this date?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, (object) "Estimated Date?") == MsgBoxResult.Yes)
        this.chkYearEst.CheckState = CheckState.Unchecked;
      else
        this.chkYearEst.CheckState = CheckState.Checked;
    }

    private void txtSectionYearBuilt_Validating(object eventSender, CancelEventArgs eventArgs)
    {
      bool flag = eventArgs.Cancel;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) > 0U)
      {
        if (!Versioned.IsNumeric((object) this.txtSectionYearBuilt.Text))
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a numeric value for the year built.", MsgBoxStyle.Critical, (object) null);
          flag = true;
        }
        else if (Conversions.ToInteger(this.txtSectionYearBuilt.Text) < 1750 | Conversions.ToInteger(this.txtSectionYearBuilt.Text) > 2100)
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a valid year for the year this section was built/installed.", MsgBoxStyle.Critical, (object) null);
          flag = true;
        }
      }
      eventArgs.Cancel = flag;
    }

    private bool OkToSave()
    {
      bool flag;
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionAmount.Text, "", false) == 0)
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a section quantity.", MsgBoxStyle.Critical, (object) "Missing Required Field");
          flag = false;
        }
        else if (!Versioned.IsNumeric((object) this.txtSectionAmount.Text))
        {
          int num = (int) Interaction.MsgBox((object) "Please enter a valid number for the section quantity.", MsgBoxStyle.Critical, (object) "Invalid value");
          flag = false;
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) == 0)
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a value for the year built/installed.", MsgBoxStyle.Critical, (object) "Missing Required Field");
          flag = false;
        }
        else if (!Versioned.IsNumeric((object) this.txtSectionYearBuilt.Text))
        {
          int num = (int) Interaction.MsgBox((object) "Please enter a valid year for the Section's year built/installed.", MsgBoxStyle.Critical, (object) "Invalid value");
          flag = false;
        }
        else if (Conversions.ToInteger(this.txtSectionYearBuilt.Text) < 1776 | Conversions.ToInteger(this.txtSectionYearBuilt.Text) > 2100)
        {
          int num = (int) Interaction.MsgBox((object) "Please enter a year between 1776 and 2100 (all four digits) for the Section's year built/installed.", MsgBoxStyle.Critical, (object) "Invalid data format.");
          flag = false;
        }
        else if (this.m_intBldgYear > Conversions.ToInteger(this.txtSectionYearBuilt.Text))
        {
          int num = (int) Interaction.MsgBox((object) "The Section's year built/installed cannot be earlier than the building's construction date.", MsgBoxStyle.Critical, (object) null);
          flag = false;
        }
        else
        {
          if (this.chkPainted.CheckState == CheckState.Checked)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtDatePainted.Text, "", false) == 0)
            {
              int num = (int) Interaction.MsgBox((object) "You must enter a value for the year painted if you've checked that the section is painted.", MsgBoxStyle.Critical, (object) "Missing Required Field");
              flag = false;
              goto label_29;
            }
            else if (!Versioned.IsNumeric((object) this.txtDatePainted.Text))
            {
              int num = (int) Interaction.MsgBox((object) "Please enter a valid year for the Section's year painted.", MsgBoxStyle.Critical, (object) "Invalid value");
              flag = false;
              goto label_29;
            }
            else if (Conversions.ToInteger(this.txtDatePainted.Text) > DateTime.Now.Year)
            {
              int num = (int) Interaction.MsgBox((object) "Year painted cannot be more than current year", MsgBoxStyle.Critical, (object) "Invalid Value");
              flag = false;
              goto label_29;
            }
            else if (Conversions.ToInteger(this.txtDatePainted.Text) < Conversions.ToInteger(this.txtSectionYearBuilt.Text) | Conversions.ToInteger(this.txtDatePainted.Text) < 1776)
            {
              int num = (int) Interaction.MsgBox((object) "Year painted cannot be less than year built or 1776.", MsgBoxStyle.Critical, (object) "Invalid Paint Year");
              flag = false;
              this.txtDatePainted.Focus();
              this.txtDatePainted.SelectAll();
              goto label_29;
            }
          }
          else if (this.chkAddInspection.Checked)
          {
            if (DateTime.Compare(this.dtpInspDate.Value, Conversions.ToDate("1/1/" + this.txtSectionYearBuilt.Text)) < 0)
            {
              int num = (int) Interaction.MsgBox((object) "Please specify an inspection date after the section was installed.", MsgBoxStyle.Critical, (object) "Invalid Value");
              flag = false;
              goto label_29;
            }
            else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboDirectRating.Text, "", false) == 0)
            {
              int num = (int) Interaction.MsgBox((object) "Please enter a direct rating for the [current] inspection you would like created.", MsgBoxStyle.Critical, (object) "Invalid Value");
              flag = false;
              goto label_29;
            }
          }
          flag = true;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (OkToSave));
        flag = false;
        ProjectData.ClearProjectError();
      }
label_29:
      return flag;
    }

    private void cboSectionMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.cboSectionMaterial.SelectedIndex == -1)
          return;
        this.Cursor = Cursors.WaitCursor;
        object objectValue;
        if (this.cboComponentType.SelectedIndex != -1)
          objectValue = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboComponentType.SelectedText, "N/A", false) > 0U, RuntimeHelpers.GetObjectValue(this.cboComponentType.SelectedItem), (object) null));
        string sSQL;
        if (mdUtility.UseUniformat)
          sSQL = "SELECT [CMC_ID], comp_type_desc FROM qryComponentTypesUniformat WHERE [comp_id]= " + Conversions.ToString(this.m_iCompLink) + " AND [CMC_MCAT_LINK]= " + Conversions.ToString(this.cboSectionMaterial.SelectedValue) + " ORDER BY COMP_TYPE_DESC";
        else
          sSQL = "SELECT [CMC_ID], comp_type_desc FROM qryComponentTypes WHERE [comp_id]= " + Conversions.ToString(this.m_iCompLink) + " AND [CMC_MCAT_LINK]= " + Conversions.ToString(this.cboSectionMaterial.SelectedValue) + " ORDER BY COMP_TYPE_DESC";
        this.cboComponentType.DisplayMember = "comp_type_desc";
        this.cboComponentType.ValueMember = "CMC_id";
        this.cboComponentType.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        this.cboComponentType.DropDownWidth = mdUtility.GetDropDownWidth((DataTable) this.cboComponentType.DataSource, this.cboComponentType.DisplayMember, this.cboComponentType, (Form) this);
        this.cboComponentType.Refresh();
        this.cboComponentType.SelectedIndex = objectValue != null ? (!this.cboComponentType.Items.Contains(RuntimeHelpers.GetObjectValue(objectValue)) ? 0 : this.cboComponentType.Items.IndexOf(RuntimeHelpers.GetObjectValue(objectValue))) : (this.cboComponentType.Items.Count != 2 ? 0 : 1);
        this.CheckForm();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (cboSectionMaterial_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void cboComponentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.CheckForm();
        if (!(this.cboComponentType.SelectedIndex != -1 & this.cboSectionMaterial.SelectedIndex != -1))
          return;
        this.lblUOM.Text = mdUtility.UOMforCMC(Conversions.ToLong(this.cboComponentType.SelectedValue));
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "EA", false) == 0)
        {
          this.cmdIncrease.Visible = true;
          this.cmdDecrease.Visible = true;
          this.cmdCalc.Visible = false;
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "SF", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "SM", false) == 0)
        {
          this.cmdIncrease.Visible = false;
          this.cmdDecrease.Visible = false;
          this.cmdCalc.Visible = true;
        }
        else
        {
          this.cmdIncrease.Visible = false;
          this.cmdDecrease.Visible = false;
          this.cmdCalc.Visible = false;
        }
        this.ToolTip1.SetToolTip((Control) this.cboComponentType, Conversions.ToString(NewLateBinding.LateGet(this.cboComponentType.SelectedItem, (System.Type) null, "item", new object[1]
        {
          (object) "comp_type_desc"
        }, (string[]) null, (System.Type[]) null, (bool[]) null)));
        if (this.chkYearEst.CheckState != CheckState.Checked)
        {
          long integer1 = (long) Conversions.ToInteger(this.cboComponentType.SelectedValue);
          string InstallationID = Building.Installation(BuildingSystem.Building(Component.BuildingSystem(this.m_strComponentID)));
          int num1 = Installation.HVACZone(ref InstallationID);
          DataTable dataTable = mdUtility.DB.GetDataTable("SELECT SERVICE_LIFE, PAINT_LIFE FROM qryRSLbyCMC WHERE [CMC_ID]=" + Conversions.ToString(integer1) + " AND ([HVAC_Zone]=" + Conversions.ToString(num1) + " OR [HVAC_Zone]=0)");
          if (dataTable.Rows.Count > 0)
          {
            int integer2 = Conversions.ToInteger(dataTable.Rows[0]["SERVICE_LIFE"]);
            int num2 = checked (DateAndTime.Year(DateAndTime.Now) - this.m_intBldgYear);
            if ((double) num2 / (double) integer2 > 1.5)
            {
              this.txtSectionYearBuilt.Text = Conversions.ToString(checked (DateAndTime.Year(DateAndTime.Now) - unchecked (num2 % integer2)));
              this.chkYearEst.Checked = true;
            }
            else
            {
              this.txtSectionYearBuilt.Text = Conversions.ToString(this.m_intBldgYear);
              this.chkYearEst.Checked = false;
            }
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["PAINT_LIFE"])))
            {
              this.lblPainted.Visible = true;
              this.chkPainted.Visible = true;
            }
            else
            {
              this.lblPainted.Visible = false;
              this.chkPainted.Visible = false;
              this.lblDatePainted.Visible = false;
              this.txtDatePainted.Visible = false;
              this.lblPaintType.Visible = false;
              this.cboPaint.Visible = false;
            }
          }
          else
          {
            this.txtSectionYearBuilt.Text = Conversions.ToString(this.m_intBldgYear);
            this.chkYearEst.Checked = false;
            this.lblPainted.Visible = false;
            this.chkPainted.Visible = false;
            this.lblDatePainted.Visible = false;
            this.txtDatePainted.Visible = false;
            this.lblPaintType.Visible = false;
            this.cboPaint.Visible = false;
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (cboComponentType_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cboSectionName_TextChanged(object sender, EventArgs e)
    {
      this.CheckForm();
    }

    private void cboSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CheckForm();
    }

    private void LoadExistingSectionNames(string BLDGID)
    {
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT DISTINCT SEC_NAME FROM [qrySectionNamesByBldg] where [Facility_ID] = {" + BLDGID + "}");
        ComboBox cboSectionName = this.cboSectionName;
        cboSectionName.Items.Clear();
        cboSectionName.Text = "";
        cboSectionName.BeginUpdate();
        try
        {
          foreach (DataRow row in dataTable.Rows)
            cboSectionName.Items.Add(RuntimeHelpers.GetObjectValue(row["Sec_Name"]));
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        cboSectionName.EndUpdate();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSection), nameof (LoadExistingSectionNames));
        ProjectData.ClearProjectError();
      }
    }

    private void chkAddInspection_CheckedChanged(object sender, EventArgs e)
    {
      this.gbCurrentInspection.Enabled = this.chkAddInspection.Checked;
    }

    private void cmdInspComments_Click(object sender, EventArgs e)
    {
      this.m_strInspComments = new frmComment().NewComment("Inspection Rating comments for new section", (object) this.m_strInspComments, true);
    }

    private void cmdIncrease_Click(object sender, EventArgs e)
    {
      if (Versioned.IsNumeric((object) this.txtSectionAmount.Text))
        this.txtSectionAmount.Text = Conversions.ToString(Conversions.ToDouble(this.txtSectionAmount.Text) + 1.0);
      else
        this.txtSectionAmount.Text = Conversions.ToString(1);
    }

    private void cmdDecrease_Click(object sender, EventArgs e)
    {
      if (Versioned.IsNumeric((object) this.txtSectionAmount.Text) && Conversions.ToDouble(this.txtSectionAmount.Text) > 1.0)
        this.txtSectionAmount.Text = Conversions.ToString(Conversions.ToDouble(this.txtSectionAmount.Text) - 1.0);
      else
        this.txtSectionAmount.Text = Conversions.ToString(1);
    }

    private void cmdCalc_Click(object sender, EventArgs e)
    {
      double area = new dlgCalculateArea().CalculateArea((Form) this, Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionAmount.Text, "", false) != 0 ? Conversions.ToDouble(this.txtSectionAmount.Text) : 0.0);
      if (area == -1.0)
        return;
      this.txtSectionAmount.Text = Conversions.ToString(area);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      MySettingsProperty.Settings.AddSectionFormSize = this.Size;
      MySettingsProperty.Settings.Save();
    }
  }
}
