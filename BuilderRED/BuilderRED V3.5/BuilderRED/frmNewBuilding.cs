// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmNewBuilding
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmNewBuilding : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmNewBuilding";

    public frmNewBuilding()
    {
      this.Load += new EventHandler(this.frmNewBuilding_Load);
      this.InitializeComponent();
    }

    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual TextBox txtBldgArea
    {
      get
      {
        return this._txtBldgArea;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtBldgArea_Validating);
        TextBox txtBldgArea1 = this._txtBldgArea;
        if (txtBldgArea1 != null)
          txtBldgArea1.Validating -= cancelEventHandler;
        this._txtBldgArea = value;
        TextBox txtBldgArea2 = this._txtBldgArea;
        if (txtBldgArea2 == null)
          return;
        txtBldgArea2.Validating += cancelEventHandler;
      }
    }

    public virtual TextBox txtPOC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtPOCPhone { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtPOCEmail { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblPOCPhone { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblPOC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblPOCEmail { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox frmBuildingPOC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtCity { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtState { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtZipCode { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblCity { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblState { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblZipCode { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox frmBuildingAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtNoFloors
    {
      get
      {
        return this._txtNoFloors;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtNoFloors_Validating);
        TextBox txtNoFloors1 = this._txtNoFloors;
        if (txtNoFloors1 != null)
          txtNoFloors1.Validating -= cancelEventHandler;
        this._txtNoFloors = value;
        TextBox txtNoFloors2 = this._txtNoFloors;
        if (txtNoFloors2 == null)
          return;
        txtNoFloors2.Validating += cancelEventHandler;
      }
    }

    public virtual TextBox txtYearBuilt
    {
      get
      {
        return this._txtYearBuilt;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtYearBuilt_Validating);
        TextBox txtYearBuilt1 = this._txtYearBuilt;
        if (txtYearBuilt1 != null)
          txtYearBuilt1.Validating -= cancelEventHandler;
        this._txtYearBuilt = value;
        TextBox txtYearBuilt2 = this._txtYearBuilt;
        if (txtYearBuilt2 == null)
          return;
        txtYearBuilt2.Validating += cancelEventHandler;
      }
    }

    public virtual TextBox txtBuildingName
    {
      get
      {
        return this._txtBuildingName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtBuildingName_TextChanged);
        TextBox txtBuildingName1 = this._txtBuildingName;
        if (txtBuildingName1 != null)
          txtBuildingName1.TextChanged -= eventHandler;
        this._txtBuildingName = value;
        TextBox txtBuildingName2 = this._txtBuildingName;
        if (txtBuildingName2 == null)
          return;
        txtBuildingName2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtBuildingID
    {
      get
      {
        return this._txtBuildingID;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtBuildingID_TextChanged);
        TextBox txtBuildingId1 = this._txtBuildingID;
        if (txtBuildingId1 != null)
          txtBuildingId1.TextChanged -= eventHandler;
        this._txtBuildingID = value;
        TextBox txtBuildingId2 = this._txtBuildingID;
        if (txtBuildingId2 == null)
          return;
        txtBuildingId2.TextChanged += eventHandler;
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
        EventHandler eventHandler = new EventHandler(this.CancelButton_Renamed_Click);
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

    public virtual Label lblBldgUnits { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblBldgArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblNoFloors { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblYearBuilt { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblConstructionType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblCatcode { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblBuilding { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboCatCode
    {
      get
      {
        return this._cboCatCode;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboCatCode_SelectedIndexChanged);
        ComboBox cboCatCode1 = this._cboCatCode;
        if (cboCatCode1 != null)
          cboCatCode1.SelectedIndexChanged -= eventHandler;
        this._cboCatCode = value;
        ComboBox cboCatCode2 = this._cboCatCode;
        if (cboCatCode2 == null)
          return;
        cboCatCode2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual ComboBox cboConstructionType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpBuildingInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblAlternateID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtAlternateID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblAlternateIDSource { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtAlternateIDSource { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpAddressInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpPointOfContact { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.txtBldgArea = new TextBox();
      this.frmBuildingPOC = new GroupBox();
      this.tlpPointOfContact = new TableLayoutPanel();
      this.txtPOC = new TextBox();
      this.txtPOCEmail = new TextBox();
      this.lblPOC = new Label();
      this.lblPOCEmail = new Label();
      this.lblPOCPhone = new Label();
      this.txtPOCPhone = new TextBox();
      this.frmBuildingAddress = new GroupBox();
      this.tlpAddressInfo = new TableLayoutPanel();
      this.lblAddress = new Label();
      this.txtZipCode = new TextBox();
      this.txtState = new TextBox();
      this.lblZipCode = new Label();
      this.txtCity = new TextBox();
      this.txtAddress = new TextBox();
      this.lblState = new Label();
      this.lblCity = new Label();
      this.txtNoFloors = new TextBox();
      this.txtYearBuilt = new TextBox();
      this.txtBuildingName = new TextBox();
      this.txtBuildingID = new TextBox();
      this.CancelButton_Renamed = new Button();
      this.OKButton = new Button();
      this.lblBldgUnits = new Label();
      this.lblBldgArea = new Label();
      this.lblNoFloors = new Label();
      this.lblYearBuilt = new Label();
      this.lblConstructionType = new Label();
      this.lblCatcode = new Label();
      this.lblBuilding = new Label();
      this.cboCatCode = new ComboBox();
      this.cboConstructionType = new ComboBox();
      this.HelpProvider = new HelpProvider();
      this.tlpBuildingInfo = new TableLayoutPanel();
      this.lblAlternateID = new Label();
      this.txtAlternateID = new TextBox();
      this.lblAlternateIDSource = new Label();
      this.txtAlternateIDSource = new TextBox();
      this.lblYearRenovated = new Label();
      this.txtYearRenovated = new TextBox();
      this.frmBuildingPOC.SuspendLayout();
      this.tlpPointOfContact.SuspendLayout();
      this.frmBuildingAddress.SuspendLayout();
      this.tlpAddressInfo.SuspendLayout();
      this.tlpBuildingInfo.SuspendLayout();
      this.SuspendLayout();
      this.txtBldgArea.AcceptsReturn = true;
      this.txtBldgArea.BackColor = SystemColors.Window;
      this.txtBldgArea.Cursor = Cursors.IBeam;
      this.txtBldgArea.Dock = DockStyle.Fill;
      this.txtBldgArea.ForeColor = SystemColors.WindowText;
      this.txtBldgArea.Location = new Point(368, 118);
      this.txtBldgArea.MaxLength = 0;
      this.txtBldgArea.Name = "txtBldgArea";
      this.txtBldgArea.RightToLeft = RightToLeft.No;
      this.txtBldgArea.Size = new Size(100, 20);
      this.txtBldgArea.TabIndex = 6;
      this.frmBuildingPOC.BackColor = SystemColors.Control;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.frmBuildingPOC, 4);
      this.frmBuildingPOC.Controls.Add((Control) this.tlpPointOfContact);
      this.frmBuildingPOC.Dock = DockStyle.Fill;
      this.frmBuildingPOC.ForeColor = SystemColors.ControlText;
      this.frmBuildingPOC.Location = new Point(3, 273);
      this.frmBuildingPOC.Name = "frmBuildingPOC";
      this.frmBuildingPOC.RightToLeft = RightToLeft.No;
      this.frmBuildingPOC.Size = new Size(465, 100);
      this.frmBuildingPOC.TabIndex = 26;
      this.frmBuildingPOC.TabStop = false;
      this.frmBuildingPOC.Text = "Point of Contact";
      this.tlpPointOfContact.ColumnCount = 2;
      this.tlpPointOfContact.ColumnStyles.Add(new ColumnStyle());
      this.tlpPointOfContact.ColumnStyles.Add(new ColumnStyle());
      this.tlpPointOfContact.Controls.Add((Control) this.txtPOC, 1, 0);
      this.tlpPointOfContact.Controls.Add((Control) this.txtPOCEmail, 1, 2);
      this.tlpPointOfContact.Controls.Add((Control) this.lblPOC, 0, 0);
      this.tlpPointOfContact.Controls.Add((Control) this.lblPOCEmail, 0, 2);
      this.tlpPointOfContact.Controls.Add((Control) this.lblPOCPhone, 0, 1);
      this.tlpPointOfContact.Controls.Add((Control) this.txtPOCPhone, 1, 1);
      this.tlpPointOfContact.Dock = DockStyle.Fill;
      this.tlpPointOfContact.Location = new Point(3, 16);
      this.tlpPointOfContact.Name = "tlpPointOfContact";
      this.tlpPointOfContact.RowCount = 3;
      this.tlpPointOfContact.RowStyles.Add(new RowStyle());
      this.tlpPointOfContact.RowStyles.Add(new RowStyle());
      this.tlpPointOfContact.RowStyles.Add(new RowStyle());
      this.tlpPointOfContact.Size = new Size(459, 81);
      this.tlpPointOfContact.TabIndex = 30;
      this.txtPOC.AcceptsReturn = true;
      this.txtPOC.BackColor = SystemColors.Window;
      this.txtPOC.Cursor = Cursors.IBeam;
      this.txtPOC.Dock = DockStyle.Fill;
      this.txtPOC.ForeColor = SystemColors.WindowText;
      this.txtPOC.Location = new Point(50, 3);
      this.txtPOC.MaxLength = 0;
      this.txtPOC.Name = "txtPOC";
      this.txtPOC.RightToLeft = RightToLeft.No;
      this.txtPOC.Size = new Size(406, 20);
      this.txtPOC.TabIndex = 11;
      this.txtPOCEmail.AcceptsReturn = true;
      this.txtPOCEmail.BackColor = SystemColors.Window;
      this.txtPOCEmail.Cursor = Cursors.IBeam;
      this.txtPOCEmail.Dock = DockStyle.Fill;
      this.txtPOCEmail.ForeColor = SystemColors.WindowText;
      this.txtPOCEmail.Location = new Point(50, 55);
      this.txtPOCEmail.MaxLength = 0;
      this.txtPOCEmail.Name = "txtPOCEmail";
      this.txtPOCEmail.RightToLeft = RightToLeft.No;
      this.txtPOCEmail.Size = new Size(406, 20);
      this.txtPOCEmail.TabIndex = 13;
      this.lblPOC.AutoSize = true;
      this.lblPOC.BackColor = SystemColors.Control;
      this.lblPOC.Cursor = Cursors.Default;
      this.lblPOC.Dock = DockStyle.Fill;
      this.lblPOC.ForeColor = SystemColors.ControlText;
      this.lblPOC.Location = new Point(3, 0);
      this.lblPOC.Name = "lblPOC";
      this.lblPOC.RightToLeft = RightToLeft.No;
      this.lblPOC.Size = new Size(41, 26);
      this.lblPOC.TabIndex = 28;
      this.lblPOC.Text = "Name:";
      this.lblPOC.TextAlign = ContentAlignment.MiddleRight;
      this.lblPOCEmail.AutoSize = true;
      this.lblPOCEmail.BackColor = SystemColors.Control;
      this.lblPOCEmail.Cursor = Cursors.Default;
      this.lblPOCEmail.Dock = DockStyle.Fill;
      this.lblPOCEmail.ForeColor = SystemColors.ControlText;
      this.lblPOCEmail.Location = new Point(3, 52);
      this.lblPOCEmail.Name = "lblPOCEmail";
      this.lblPOCEmail.RightToLeft = RightToLeft.No;
      this.lblPOCEmail.Size = new Size(41, 29);
      this.lblPOCEmail.TabIndex = 27;
      this.lblPOCEmail.Text = "E-Mail:";
      this.lblPOCEmail.TextAlign = ContentAlignment.MiddleRight;
      this.lblPOCPhone.AutoSize = true;
      this.lblPOCPhone.BackColor = SystemColors.Control;
      this.lblPOCPhone.Cursor = Cursors.Default;
      this.lblPOCPhone.Dock = DockStyle.Fill;
      this.lblPOCPhone.ForeColor = SystemColors.ControlText;
      this.lblPOCPhone.Location = new Point(3, 26);
      this.lblPOCPhone.Name = "lblPOCPhone";
      this.lblPOCPhone.RightToLeft = RightToLeft.No;
      this.lblPOCPhone.Size = new Size(41, 26);
      this.lblPOCPhone.TabIndex = 29;
      this.lblPOCPhone.Text = "Phone:";
      this.lblPOCPhone.TextAlign = ContentAlignment.MiddleRight;
      this.txtPOCPhone.AcceptsReturn = true;
      this.txtPOCPhone.BackColor = SystemColors.Window;
      this.txtPOCPhone.Cursor = Cursors.IBeam;
      this.txtPOCPhone.Dock = DockStyle.Fill;
      this.txtPOCPhone.ForeColor = SystemColors.WindowText;
      this.txtPOCPhone.Location = new Point(50, 29);
      this.txtPOCPhone.MaxLength = 0;
      this.txtPOCPhone.Name = "txtPOCPhone";
      this.txtPOCPhone.RightToLeft = RightToLeft.No;
      this.txtPOCPhone.Size = new Size(406, 20);
      this.txtPOCPhone.TabIndex = 12;
      this.frmBuildingAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.frmBuildingAddress.AutoSize = true;
      this.frmBuildingAddress.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.frmBuildingAddress.BackColor = SystemColors.Control;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.frmBuildingAddress, 4);
      this.frmBuildingAddress.Controls.Add((Control) this.tlpAddressInfo);
      this.frmBuildingAddress.ForeColor = SystemColors.ControlText;
      this.frmBuildingAddress.Location = new Point(3, 170);
      this.frmBuildingAddress.Name = "frmBuildingAddress";
      this.frmBuildingAddress.RightToLeft = RightToLeft.No;
      this.frmBuildingAddress.Size = new Size(465, 97);
      this.frmBuildingAddress.TabIndex = 21;
      this.frmBuildingAddress.TabStop = false;
      this.frmBuildingAddress.Text = "Address";
      this.tlpAddressInfo.AutoSize = true;
      this.tlpAddressInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpAddressInfo.ColumnCount = 4;
      this.tlpAddressInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpAddressInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpAddressInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpAddressInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpAddressInfo.Controls.Add((Control) this.lblAddress, 0, 0);
      this.tlpAddressInfo.Controls.Add((Control) this.txtZipCode, 3, 2);
      this.tlpAddressInfo.Controls.Add((Control) this.txtState, 1, 2);
      this.tlpAddressInfo.Controls.Add((Control) this.lblZipCode, 2, 2);
      this.tlpAddressInfo.Controls.Add((Control) this.txtCity, 1, 1);
      this.tlpAddressInfo.Controls.Add((Control) this.txtAddress, 1, 0);
      this.tlpAddressInfo.Controls.Add((Control) this.lblState, 0, 2);
      this.tlpAddressInfo.Controls.Add((Control) this.lblCity, 0, 1);
      this.tlpAddressInfo.Dock = DockStyle.Fill;
      this.tlpAddressInfo.Location = new Point(3, 16);
      this.tlpAddressInfo.Name = "tlpAddressInfo";
      this.tlpAddressInfo.RowCount = 3;
      this.tlpAddressInfo.RowStyles.Add(new RowStyle());
      this.tlpAddressInfo.RowStyles.Add(new RowStyle());
      this.tlpAddressInfo.RowStyles.Add(new RowStyle());
      this.tlpAddressInfo.Size = new Size(459, 78);
      this.tlpAddressInfo.TabIndex = 26;
      this.lblAddress.AutoSize = true;
      this.lblAddress.BackColor = SystemColors.Control;
      this.lblAddress.Cursor = Cursors.Default;
      this.lblAddress.Dock = DockStyle.Fill;
      this.lblAddress.ForeColor = SystemColors.ControlText;
      this.lblAddress.Location = new Point(3, 0);
      this.lblAddress.Name = "lblAddress";
      this.lblAddress.RightToLeft = RightToLeft.No;
      this.lblAddress.Size = new Size(79, 26);
      this.lblAddress.TabIndex = 25;
      this.lblAddress.Text = "Street Address:";
      this.lblAddress.TextAlign = ContentAlignment.MiddleRight;
      this.txtZipCode.AcceptsReturn = true;
      this.txtZipCode.BackColor = SystemColors.Window;
      this.txtZipCode.Cursor = Cursors.IBeam;
      this.txtZipCode.Dock = DockStyle.Fill;
      this.txtZipCode.ForeColor = SystemColors.WindowText;
      this.txtZipCode.Location = new Point(202, 55);
      this.txtZipCode.MaxLength = 0;
      this.txtZipCode.Name = "txtZipCode";
      this.txtZipCode.RightToLeft = RightToLeft.No;
      this.txtZipCode.Size = new Size(254, 20);
      this.txtZipCode.TabIndex = 10;
      this.txtState.AcceptsReturn = true;
      this.txtState.BackColor = SystemColors.Window;
      this.txtState.Cursor = Cursors.IBeam;
      this.txtState.Dock = DockStyle.Fill;
      this.txtState.ForeColor = SystemColors.WindowText;
      this.txtState.Location = new Point(88, 55);
      this.txtState.MaxLength = 0;
      this.txtState.Name = "txtState";
      this.txtState.RightToLeft = RightToLeft.No;
      this.txtState.Size = new Size(49, 20);
      this.txtState.TabIndex = 9;
      this.lblZipCode.AutoSize = true;
      this.lblZipCode.BackColor = SystemColors.Control;
      this.lblZipCode.Cursor = Cursors.Default;
      this.lblZipCode.Dock = DockStyle.Fill;
      this.lblZipCode.ForeColor = SystemColors.ControlText;
      this.lblZipCode.Location = new Point(143, 52);
      this.lblZipCode.Name = "lblZipCode";
      this.lblZipCode.RightToLeft = RightToLeft.No;
      this.lblZipCode.Size = new Size(53, 26);
      this.lblZipCode.TabIndex = 22;
      this.lblZipCode.Text = "Zip Code:";
      this.lblZipCode.TextAlign = ContentAlignment.MiddleRight;
      this.txtCity.AcceptsReturn = true;
      this.txtCity.BackColor = SystemColors.Window;
      this.tlpAddressInfo.SetColumnSpan((Control) this.txtCity, 3);
      this.txtCity.Cursor = Cursors.IBeam;
      this.txtCity.Dock = DockStyle.Fill;
      this.txtCity.ForeColor = SystemColors.WindowText;
      this.txtCity.Location = new Point(88, 29);
      this.txtCity.MaxLength = 0;
      this.txtCity.Name = "txtCity";
      this.txtCity.RightToLeft = RightToLeft.No;
      this.txtCity.Size = new Size(368, 20);
      this.txtCity.TabIndex = 8;
      this.txtAddress.AcceptsReturn = true;
      this.txtAddress.BackColor = SystemColors.Window;
      this.tlpAddressInfo.SetColumnSpan((Control) this.txtAddress, 3);
      this.txtAddress.Cursor = Cursors.IBeam;
      this.txtAddress.Dock = DockStyle.Fill;
      this.txtAddress.ForeColor = SystemColors.WindowText;
      this.txtAddress.Location = new Point(88, 3);
      this.txtAddress.MaxLength = 0;
      this.txtAddress.Name = "txtAddress";
      this.txtAddress.RightToLeft = RightToLeft.No;
      this.txtAddress.Size = new Size(368, 20);
      this.txtAddress.TabIndex = 7;
      this.lblState.AutoSize = true;
      this.lblState.BackColor = SystemColors.Control;
      this.lblState.Cursor = Cursors.Default;
      this.lblState.Dock = DockStyle.Fill;
      this.lblState.ForeColor = SystemColors.ControlText;
      this.lblState.Location = new Point(3, 52);
      this.lblState.Name = "lblState";
      this.lblState.RightToLeft = RightToLeft.No;
      this.lblState.Size = new Size(79, 26);
      this.lblState.TabIndex = 23;
      this.lblState.Text = "State:";
      this.lblState.TextAlign = ContentAlignment.MiddleRight;
      this.lblCity.AutoSize = true;
      this.lblCity.BackColor = SystemColors.Control;
      this.lblCity.Cursor = Cursors.Default;
      this.lblCity.Dock = DockStyle.Fill;
      this.lblCity.ForeColor = SystemColors.ControlText;
      this.lblCity.Location = new Point(3, 26);
      this.lblCity.Name = "lblCity";
      this.lblCity.RightToLeft = RightToLeft.No;
      this.lblCity.Size = new Size(79, 26);
      this.lblCity.TabIndex = 24;
      this.lblCity.Text = "City:";
      this.lblCity.TextAlign = ContentAlignment.MiddleRight;
      this.txtNoFloors.AcceptsReturn = true;
      this.txtNoFloors.BackColor = SystemColors.Window;
      this.txtNoFloors.Cursor = Cursors.IBeam;
      this.txtNoFloors.Dock = DockStyle.Fill;
      this.txtNoFloors.ForeColor = SystemColors.WindowText;
      this.txtNoFloors.Location = new Point(368, 92);
      this.txtNoFloors.MaxLength = 0;
      this.txtNoFloors.Name = "txtNoFloors";
      this.txtNoFloors.RightToLeft = RightToLeft.No;
      this.txtNoFloors.Size = new Size(100, 20);
      this.txtNoFloors.TabIndex = 5;
      this.txtYearBuilt.AcceptsReturn = true;
      this.txtYearBuilt.BackColor = SystemColors.Window;
      this.txtYearBuilt.Cursor = Cursors.IBeam;
      this.txtYearBuilt.Dock = DockStyle.Fill;
      this.txtYearBuilt.ForeColor = SystemColors.WindowText;
      this.txtYearBuilt.Location = new Point(97, 92);
      this.txtYearBuilt.MaxLength = 0;
      this.txtYearBuilt.Name = "txtYearBuilt";
      this.txtYearBuilt.RightToLeft = RightToLeft.No;
      this.txtYearBuilt.Size = new Size(100, 20);
      this.txtYearBuilt.TabIndex = 4;
      this.txtBuildingName.AcceptsReturn = true;
      this.txtBuildingName.BackColor = SystemColors.Window;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.txtBuildingName, 2);
      this.txtBuildingName.Cursor = Cursors.IBeam;
      this.txtBuildingName.Dock = DockStyle.Fill;
      this.txtBuildingName.ForeColor = SystemColors.WindowText;
      this.txtBuildingName.Location = new Point(203, 3);
      this.txtBuildingName.MaxLength = 0;
      this.txtBuildingName.Name = "txtBuildingName";
      this.txtBuildingName.RightToLeft = RightToLeft.No;
      this.txtBuildingName.Size = new Size(265, 20);
      this.txtBuildingName.TabIndex = 1;
      this.txtBuildingID.AcceptsReturn = true;
      this.txtBuildingID.BackColor = SystemColors.Window;
      this.txtBuildingID.Cursor = Cursors.IBeam;
      this.txtBuildingID.Dock = DockStyle.Fill;
      this.txtBuildingID.ForeColor = SystemColors.WindowText;
      this.txtBuildingID.Location = new Point(97, 3);
      this.txtBuildingID.MaxLength = 0;
      this.txtBuildingID.Name = "txtBuildingID";
      this.txtBuildingID.RightToLeft = RightToLeft.No;
      this.txtBuildingID.Size = new Size(100, 20);
      this.txtBuildingID.TabIndex = 0;
      this.CancelButton_Renamed.BackColor = SystemColors.Control;
      this.CancelButton_Renamed.Cursor = Cursors.Default;
      this.CancelButton_Renamed.DialogResult = DialogResult.Cancel;
      this.CancelButton_Renamed.Dock = DockStyle.Fill;
      this.CancelButton_Renamed.ForeColor = SystemColors.ControlText;
      this.CancelButton_Renamed.Location = new Point(474, 34);
      this.CancelButton_Renamed.Name = "CancelButton_Renamed";
      this.CancelButton_Renamed.RightToLeft = RightToLeft.No;
      this.CancelButton_Renamed.Size = new Size(77, 25);
      this.CancelButton_Renamed.TabIndex = 15;
      this.CancelButton_Renamed.Text = "Cancel";
      this.CancelButton_Renamed.UseVisualStyleBackColor = false;
      this.OKButton.AutoSize = true;
      this.OKButton.BackColor = SystemColors.Control;
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.Dock = DockStyle.Fill;
      this.OKButton.Enabled = false;
      this.OKButton.ForeColor = SystemColors.ControlText;
      this.OKButton.Location = new Point(474, 3);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(77, 25);
      this.OKButton.TabIndex = 14;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = false;
      this.lblBldgUnits.BackColor = SystemColors.Control;
      this.lblBldgUnits.Cursor = Cursors.Default;
      this.lblBldgUnits.Dock = DockStyle.Fill;
      this.lblBldgUnits.ForeColor = SystemColors.ControlText;
      this.lblBldgUnits.Location = new Point(474, 115);
      this.lblBldgUnits.Name = "lblBldgUnits";
      this.lblBldgUnits.RightToLeft = RightToLeft.No;
      this.lblBldgUnits.Size = new Size(77, 26);
      this.lblBldgUnits.TabIndex = 31;
      this.lblBldgUnits.Text = "SF";
      this.lblBldgUnits.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBldgArea.AutoSize = true;
      this.lblBldgArea.BackColor = SystemColors.Control;
      this.lblBldgArea.Cursor = Cursors.Default;
      this.lblBldgArea.Dock = DockStyle.Fill;
      this.lblBldgArea.ForeColor = SystemColors.ControlText;
      this.lblBldgArea.Location = new Point(203, 115);
      this.lblBldgArea.Name = "lblBldgArea";
      this.lblBldgArea.RightToLeft = RightToLeft.No;
      this.lblBldgArea.Size = new Size(159, 26);
      this.lblBldgArea.TabIndex = 30;
      this.lblBldgArea.Text = "Quantity:";
      this.lblBldgArea.TextAlign = ContentAlignment.MiddleRight;
      this.lblNoFloors.AutoSize = true;
      this.lblNoFloors.BackColor = SystemColors.Control;
      this.lblNoFloors.Cursor = Cursors.Default;
      this.lblNoFloors.Dock = DockStyle.Fill;
      this.lblNoFloors.ForeColor = SystemColors.ControlText;
      this.lblNoFloors.Location = new Point(203, 89);
      this.lblNoFloors.Name = "lblNoFloors";
      this.lblNoFloors.RightToLeft = RightToLeft.No;
      this.lblNoFloors.Size = new Size(159, 26);
      this.lblNoFloors.TabIndex = 20;
      this.lblNoFloors.Text = "No. Floors:";
      this.lblNoFloors.TextAlign = ContentAlignment.MiddleRight;
      this.lblYearBuilt.AutoSize = true;
      this.lblYearBuilt.BackColor = SystemColors.Control;
      this.lblYearBuilt.Cursor = Cursors.Default;
      this.lblYearBuilt.Dock = DockStyle.Fill;
      this.lblYearBuilt.ForeColor = SystemColors.ControlText;
      this.lblYearBuilt.Location = new Point(3, 89);
      this.lblYearBuilt.Name = "lblYearBuilt";
      this.lblYearBuilt.RightToLeft = RightToLeft.No;
      this.lblYearBuilt.Size = new Size(88, 26);
      this.lblYearBuilt.TabIndex = 19;
      this.lblYearBuilt.Text = "Year Built:";
      this.lblYearBuilt.TextAlign = ContentAlignment.MiddleRight;
      this.lblConstructionType.AutoSize = true;
      this.lblConstructionType.BackColor = SystemColors.Control;
      this.lblConstructionType.Cursor = Cursors.Default;
      this.lblConstructionType.Dock = DockStyle.Fill;
      this.lblConstructionType.ForeColor = SystemColors.ControlText;
      this.lblConstructionType.Location = new Point(3, 62);
      this.lblConstructionType.Name = "lblConstructionType";
      this.lblConstructionType.RightToLeft = RightToLeft.No;
      this.lblConstructionType.Size = new Size(88, 27);
      this.lblConstructionType.TabIndex = 18;
      this.lblConstructionType.Text = "Const. Type:";
      this.lblConstructionType.TextAlign = ContentAlignment.MiddleRight;
      this.lblCatcode.AutoSize = true;
      this.lblCatcode.BackColor = SystemColors.Control;
      this.lblCatcode.Cursor = Cursors.Default;
      this.lblCatcode.Dock = DockStyle.Fill;
      this.lblCatcode.ForeColor = SystemColors.ControlText;
      this.lblCatcode.Location = new Point(3, 31);
      this.lblCatcode.Name = "lblCatcode";
      this.lblCatcode.RightToLeft = RightToLeft.No;
      this.lblCatcode.Size = new Size(88, 31);
      this.lblCatcode.TabIndex = 16;
      this.lblCatcode.Text = "Building Use:";
      this.lblCatcode.TextAlign = ContentAlignment.MiddleRight;
      this.lblBuilding.AutoSize = true;
      this.lblBuilding.BackColor = SystemColors.Control;
      this.lblBuilding.Cursor = Cursors.Default;
      this.lblBuilding.Dock = DockStyle.Fill;
      this.lblBuilding.ForeColor = SystemColors.ControlText;
      this.lblBuilding.Location = new Point(3, 0);
      this.lblBuilding.Name = "lblBuilding";
      this.lblBuilding.RightToLeft = RightToLeft.No;
      this.lblBuilding.Size = new Size(88, 31);
      this.lblBuilding.TabIndex = 14;
      this.lblBuilding.Text = "Building ID:";
      this.lblBuilding.TextAlign = ContentAlignment.MiddleRight;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.cboCatCode, 3);
      this.cboCatCode.Dock = DockStyle.Fill;
      this.cboCatCode.Location = new Point(97, 34);
      this.cboCatCode.Name = "cboCatCode";
      this.cboCatCode.Size = new Size(371, 21);
      this.cboCatCode.TabIndex = 2;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.cboConstructionType, 3);
      this.cboConstructionType.Dock = DockStyle.Fill;
      this.cboConstructionType.Location = new Point(97, 65);
      this.cboConstructionType.Name = "cboConstructionType";
      this.cboConstructionType.Size = new Size(371, 21);
      this.cboConstructionType.TabIndex = 3;
      this.tlpBuildingInfo.AutoSize = true;
      this.tlpBuildingInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpBuildingInfo.ColumnCount = 5;
      this.tlpBuildingInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66666f));
      this.tlpBuildingInfo.ColumnStyles.Add(new ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpBuildingInfo.Controls.Add((Control) this.OKButton, 4, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.frmBuildingPOC, 0, 7);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtBldgArea, 3, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.frmBuildingAddress, 0, 6);
      this.tlpBuildingInfo.Controls.Add((Control) this.cboConstructionType, 1, 2);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblBldgUnits, 4, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.CancelButton_Renamed, 4, 1);
      this.tlpBuildingInfo.Controls.Add((Control) this.cboCatCode, 1, 1);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtNoFloors, 3, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblBldgArea, 2, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblBuilding, 0, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtYearBuilt, 1, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblCatcode, 0, 1);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblNoFloors, 2, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblConstructionType, 0, 2);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblYearBuilt, 0, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtBuildingID, 1, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtBuildingName, 2, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblAlternateID, 0, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtAlternateID, 1, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblAlternateIDSource, 2, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtAlternateIDSource, 3, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblYearRenovated, 0, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtYearRenovated, 1, 4);
      this.tlpBuildingInfo.Dock = DockStyle.Fill;
      this.tlpBuildingInfo.Location = new Point(0, 0);
      this.tlpBuildingInfo.Name = "tlpBuildingInfo";
      this.tlpBuildingInfo.RowCount = 8;
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.Size = new Size(554, 376);
      this.tlpBuildingInfo.TabIndex = 32;
      this.lblAlternateID.AutoSize = true;
      this.lblAlternateID.Dock = DockStyle.Fill;
      this.lblAlternateID.Location = new Point(3, 141);
      this.lblAlternateID.Name = "lblAlternateID";
      this.lblAlternateID.Size = new Size(88, 26);
      this.lblAlternateID.TabIndex = 32;
      this.lblAlternateID.Text = "Alternate ID:";
      this.lblAlternateID.TextAlign = ContentAlignment.MiddleRight;
      this.txtAlternateID.Dock = DockStyle.Fill;
      this.txtAlternateID.Location = new Point(97, 144);
      this.txtAlternateID.Name = "txtAlternateID";
      this.txtAlternateID.Size = new Size(100, 20);
      this.txtAlternateID.TabIndex = 33;
      this.lblAlternateIDSource.AutoSize = true;
      this.lblAlternateIDSource.Dock = DockStyle.Fill;
      this.lblAlternateIDSource.Location = new Point(203, 141);
      this.lblAlternateIDSource.Name = "lblAlternateIDSource";
      this.lblAlternateIDSource.Size = new Size(159, 26);
      this.lblAlternateIDSource.TabIndex = 34;
      this.lblAlternateIDSource.Text = "Alternate ID Source:";
      this.lblAlternateIDSource.TextAlign = ContentAlignment.MiddleRight;
      this.txtAlternateIDSource.Dock = DockStyle.Fill;
      this.txtAlternateIDSource.Location = new Point(368, 144);
      this.txtAlternateIDSource.Name = "txtAlternateIDSource";
      this.txtAlternateIDSource.Size = new Size(100, 20);
      this.txtAlternateIDSource.TabIndex = 35;
      this.lblYearRenovated.AutoSize = true;
      this.lblYearRenovated.Dock = DockStyle.Fill;
      this.lblYearRenovated.Location = new Point(3, 115);
      this.lblYearRenovated.Name = "lblYearRenovated";
      this.lblYearRenovated.Size = new Size(88, 26);
      this.lblYearRenovated.TabIndex = 36;
      this.lblYearRenovated.Text = "Year Renovated:";
      this.lblYearRenovated.TextAlign = ContentAlignment.MiddleRight;
      this.txtYearRenovated.Dock = DockStyle.Fill;
      this.txtYearRenovated.Location = new Point(97, 118);
      this.txtYearRenovated.Name = "txtYearRenovated";
      this.txtYearRenovated.Size = new Size(100, 20);
      this.txtYearRenovated.TabIndex = 37;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(554, 376);
      this.ControlBox = false;
      this.Controls.Add((Control) this.tlpBuildingInfo);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(113, 94);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmNewBuilding);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add New Building";
      this.frmBuildingPOC.ResumeLayout(false);
      this.tlpPointOfContact.ResumeLayout(false);
      this.tlpPointOfContact.PerformLayout();
      this.frmBuildingAddress.ResumeLayout(false);
      this.frmBuildingAddress.PerformLayout();
      this.tlpAddressInfo.ResumeLayout(false);
      this.tlpAddressInfo.PerformLayout();
      this.tlpBuildingInfo.ResumeLayout(false);
      this.tlpBuildingInfo.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual Label lblYearRenovated { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtYearRenovated { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void CancelButton_Renamed_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }

    private void frmNewBuilding_Load(object eventSender, EventArgs eventArgs)
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
      this.HelpProvider.SetHelpKeyword((Control) this, "Add Bldg");
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT ConfigValue FROM Configuration where [ConfigName] = 'Branch'");
        string sSQL;
        if (dataTable.Rows.Count > 0)
          sSQL = !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["ConfigValue"]), (object) ""), (object) "", false) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["ConfigValue"], (object) "Z", false) ? Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM RO_UseType WHERE Branch='", dataTable.Rows[0]["ConfigValue"]), (object) "' ORDER BY usetype_and_desc")) : "SELECT * FROM RO_UseType ORDER BY Usetype_And_Desc";
        this.cboCatCode.ValueMember = "USETYPE_ID";
        this.cboCatCode.DisplayMember = "USETYPE_AND_DESC";
        this.cboCatCode.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        this.cboConstructionType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM RO_Construction_type");
        this.cboConstructionType.ValueMember = "CONST_TYPE_ID";
        this.cboConstructionType.DisplayMember = "CONST_TYPE_DESC";
        DataTable tableSchema = mdUtility.DB.GetTableSchema("BuildingInfo");
        this.txtBuildingID.MaxLength = tableSchema.Columns["Number"].MaxLength;
        this.txtBuildingName.MaxLength = tableSchema.Columns["Name"].MaxLength;
        this.txtYearBuilt.MaxLength = 4;
        this.txtNoFloors.MaxLength = 2;
        this.txtBldgArea.MaxLength = 10;
        this.txtAddress.MaxLength = tableSchema.Columns["BLDG_STRT"].MaxLength;
        this.txtCity.MaxLength = tableSchema.Columns["BLDG_CITY"].MaxLength;
        this.txtState.MaxLength = tableSchema.Columns["BLDG_ST"].MaxLength;
        this.txtZipCode.MaxLength = tableSchema.Columns["BLDG_ZIP"].MaxLength;
        this.txtPOC.MaxLength = tableSchema.Columns["BLDG_POC_NAME"].MaxLength;
        this.txtPOCPhone.MaxLength = tableSchema.Columns["BLDG_POC_PH_NO"].MaxLength;
        this.txtPOCEmail.MaxLength = tableSchema.Columns["BLDG_POC_EMAIL"].MaxLength;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), nameof (frmNewBuilding_Load));
        ProjectData.ClearProjectError();
      }
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      if (!this.ValidateChildren())
        return;
      try
      {
        string str;
        string BldgID = Building.AddBuilding(this.txtBuildingID.Text, this.txtBuildingName.Text, this.txtNoFloors.Text, this.txtYearBuilt.Text, this.txtYearRenovated.Text, this.txtBldgArea.Text, Conversions.ToString(this.cboCatCode.SelectedValue), Conversions.ToString(this.cboConstructionType.SelectedValue), this.txtAlternateID.Text, this.txtAlternateIDSource.Text, this.txtAddress.Text, this.txtCity.Text, this.txtState.Text, this.txtZipCode.Text, this.txtPOC.Text, this.txtPOCPhone.Text, this.txtPOCEmail.Text, ref str);
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(BldgID, "", false) > 0U)
          mdUtility.fMainForm.BuildingAdded(ref BldgID, ref str);
        this.Close();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), nameof (OKButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void CheckForEnable()
    {
      try
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtBuildingID.Text, "", false) > 0U | (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtBuildingName.Text, "", false) > 0U)
          this.OKButton.Enabled = true;
        else
          this.OKButton.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), nameof (CheckForEnable));
        ProjectData.ClearProjectError();
      }
    }

    private void txtBldgArea_Validating(object eventSender, CancelEventArgs eventArgs)
    {
      bool flag = eventArgs.Cancel;
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtBldgArea.Text, "", false) == 0 || !Versioned.IsNumeric((object) this.txtBldgArea.Text) || Conversions.ToDouble(this.txtBldgArea.Text) <= 0.0)
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a valid building area.", MsgBoxStyle.Critical, (object) null);
          flag = true;
        }
        eventArgs.Cancel = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), "txtYearBuilt_Validate");
        eventArgs.Cancel = true;
        ProjectData.ClearProjectError();
      }
    }

    private void txtBuildingID_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.CheckForEnable();
    }

    private void txtBuildingName_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.CheckForEnable();
    }

    public void AddBuilding()
    {
      try
      {
        this.cboCatCode.Refresh();
        this.cboConstructionType.Refresh();
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT bldr_uom_eng_unit_abbr, bldr_uom_met_unit_abbr FROM RO_Builder_UOM WHERE [bldr_uom_caption]='Area'");
        if (dataTable.Rows.Count > 0)
          this.lblBldgUnits.Text = !mdUtility.fMainForm.miUnits.Checked ? Conversions.ToString(dataTable.Rows[0]["bldr_uom_met_unit_abbr"]) : Conversions.ToString(dataTable.Rows[0]["bldr_uom_eng_unit_abbr"]);
        int num = (int) this.ShowDialog();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), nameof (AddBuilding));
        ProjectData.ClearProjectError();
      }
    }

    private void txtNoFloors_Validating(object eventSender, CancelEventArgs eventArgs)
    {
      bool flag = eventArgs.Cancel;
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtNoFloors.Text, "", false) == 0 || !Versioned.IsNumeric((object) this.txtNoFloors.Text) || Conversions.ToDouble(this.txtNoFloors.Text) <= 0.0)
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a valid number of floors.", MsgBoxStyle.Critical, (object) null);
          flag = true;
        }
        eventArgs.Cancel = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), nameof (txtNoFloors_Validating));
        eventArgs.Cancel = true;
        ProjectData.ClearProjectError();
      }
    }

    private void txtYearBuilt_Validating(object eventSender, CancelEventArgs eventArgs)
    {
      bool flag = eventArgs.Cancel;
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtYearBuilt.Text, "", false) == 0 || !Versioned.IsNumeric((object) this.txtYearBuilt.Text))
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a valid year for the year built.", MsgBoxStyle.Critical, (object) null);
          flag = true;
        }
        else if (Conversions.ToInteger(this.txtYearBuilt.Text) < 1750 | Conversions.ToInteger(this.txtYearBuilt.Text) > 2100)
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a valid year for the year built.", MsgBoxStyle.Critical, (object) null);
          flag = true;
        }
        else if (Versioned.IsNumeric((object) this.txtYearRenovated.Text) && Conversions.ToInteger(this.txtYearBuilt.Text) < Conversions.ToInteger(this.txtYearRenovated.Text))
        {
          int num1 = (int) Interaction.MsgBox((object) "Year Renovated must be greater than Year Built", MsgBoxStyle.Critical, (object) null);
        }
        eventArgs.Cancel = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewBuilding), nameof (txtYearBuilt_Validating));
        eventArgs.Cancel = true;
        ProjectData.ClearProjectError();
      }
    }

    private void cboCatCode_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.lblBldgUnits.Text = Building.GetUnitsLabel((short?) Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboCatCode.SelectedValue.ToString(), "", false) > 0U, RuntimeHelpers.GetObjectValue(this.cboCatCode.SelectedValue), (object) null));
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, "cboCatCodeSelectedIndexChanged");
        ProjectData.ClearProjectError();
      }
    }
  }
}
