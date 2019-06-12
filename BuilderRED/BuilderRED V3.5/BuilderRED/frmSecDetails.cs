// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmSecDetails
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
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
  internal class frmSecDetails : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private bool m_bIsEquipment;
    private bool m_bReadOnly;
    private string m_strSection;
    private bool m_bIsNew;
    private string m_strCurrentSec;
    private string m_strComment;
    private bool m_bLoaded;
    private bool m_bWarranty1;
    private bool m_bWarranty2;
    private bool m_bManufactured;
    private bool m_bInstalled;
    private bool m_bDDLoad;

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    internal virtual ImageList ImageList1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Panel frmNoRecord { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblNoEquip { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Panel frmEquip { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboEquip
    {
      get
      {
        return this._cboEquip;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboEquip_SelectedIndexChanged);
        ComboBox cboEquip1 = this._cboEquip;
        if (cboEquip1 != null)
          cboEquip1.SelectedIndexChanged -= eventHandler;
        this._cboEquip = value;
        ComboBox cboEquip2 = this._cboEquip;
        if (cboEquip2 == null)
          return;
        cboEquip2.SelectedIndexChanged += eventHandler;
      }
    }

    public virtual Button cmdEditName
    {
      get
      {
        return this._cmdEditName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdEditName_Click);
        Button cmdEditName1 = this._cmdEditName;
        if (cmdEditName1 != null)
          cmdEditName1.Click -= eventHandler;
        this._cmdEditName = value;
        Button cmdEditName2 = this._cmdEditName;
        if (cmdEditName2 == null)
          return;
        cmdEditName2.Click += eventHandler;
      }
    }

    public virtual Label lblSelect { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtIDNumber
    {
      get
      {
        return this._txtIDNumber;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtIdNumber1 = this._txtIDNumber;
        if (txtIdNumber1 != null)
          txtIdNumber1.TextChanged -= eventHandler;
        this._txtIDNumber = value;
        TextBox txtIdNumber2 = this._txtIDNumber;
        if (txtIdNumber2 == null)
          return;
        txtIdNumber2.TextChanged += eventHandler;
      }
    }

    public virtual GroupBox frmEqDet { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtModel
    {
      get
      {
        return this._txtModel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtModel1 = this._txtModel;
        if (txtModel1 != null)
          txtModel1.TextChanged -= eventHandler;
        this._txtModel = value;
        TextBox txtModel2 = this._txtModel;
        if (txtModel2 == null)
          return;
        txtModel2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtEquipmentMake
    {
      get
      {
        return this._txtEquipmentMake;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtEquipmentMake1 = this._txtEquipmentMake;
        if (txtEquipmentMake1 != null)
          txtEquipmentMake1.TextChanged -= eventHandler;
        this._txtEquipmentMake = value;
        TextBox txtEquipmentMake2 = this._txtEquipmentMake;
        if (txtEquipmentMake2 == null)
          return;
        txtEquipmentMake2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtControlTypeMake
    {
      get
      {
        return this._txtControlTypeMake;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtControlTypeMake1 = this._txtControlTypeMake;
        if (txtControlTypeMake1 != null)
          txtControlTypeMake1.TextChanged -= eventHandler;
        this._txtControlTypeMake = value;
        TextBox txtControlTypeMake2 = this._txtControlTypeMake;
        if (txtControlTypeMake2 == null)
          return;
        txtControlTypeMake2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtLocation
    {
      get
      {
        return this._txtLocation;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtLocation1 = this._txtLocation;
        if (txtLocation1 != null)
          txtLocation1.TextChanged -= eventHandler;
        this._txtLocation = value;
        TextBox txtLocation2 = this._txtLocation;
        if (txtLocation2 == null)
          return;
        txtLocation2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtManufacturer
    {
      get
      {
        return this._txtManufacturer;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtManufacturer1 = this._txtManufacturer;
        if (txtManufacturer1 != null)
          txtManufacturer1.TextChanged -= eventHandler;
        this._txtManufacturer = value;
        TextBox txtManufacturer2 = this._txtManufacturer;
        if (txtManufacturer2 == null)
          return;
        txtManufacturer2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtWarrantyCo1
    {
      get
      {
        return this._txtWarrantyCo1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtWarrantyCo1_1 = this._txtWarrantyCo1;
        if (txtWarrantyCo1_1 != null)
          txtWarrantyCo1_1.TextChanged -= eventHandler;
        this._txtWarrantyCo1 = value;
        TextBox txtWarrantyCo1_2 = this._txtWarrantyCo1;
        if (txtWarrantyCo1_2 == null)
          return;
        txtWarrantyCo1_2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtCapacity
    {
      get
      {
        return this._txtCapacity;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtCapacity1 = this._txtCapacity;
        if (txtCapacity1 != null)
          txtCapacity1.TextChanged -= eventHandler;
        this._txtCapacity = value;
        TextBox txtCapacity2 = this._txtCapacity;
        if (txtCapacity2 == null)
          return;
        txtCapacity2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtSerialNumber
    {
      get
      {
        return this._txtSerialNumber;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtSerialNumber1 = this._txtSerialNumber;
        if (txtSerialNumber1 != null)
          txtSerialNumber1.TextChanged -= eventHandler;
        this._txtSerialNumber = value;
        TextBox txtSerialNumber2 = this._txtSerialNumber;
        if (txtSerialNumber2 == null)
          return;
        txtSerialNumber2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtDateInstalled
    {
      get
      {
        return this._txtDateInstalled;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtDateInstalled_Validating);
        TextBox txtDateInstalled1 = this._txtDateInstalled;
        if (txtDateInstalled1 != null)
        {
          txtDateInstalled1.TextChanged -= eventHandler;
          txtDateInstalled1.Validating -= cancelEventHandler;
        }
        this._txtDateInstalled = value;
        TextBox txtDateInstalled2 = this._txtDateInstalled;
        if (txtDateInstalled2 == null)
          return;
        txtDateInstalled2.TextChanged += eventHandler;
        txtDateInstalled2.Validating += cancelEventHandler;
      }
    }

    public virtual TextBox txtEquipmentType
    {
      get
      {
        return this._txtEquipmentType;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtEquipmentType1 = this._txtEquipmentType;
        if (txtEquipmentType1 != null)
          txtEquipmentType1.TextChanged -= eventHandler;
        this._txtEquipmentType = value;
        TextBox txtEquipmentType2 = this._txtEquipmentType;
        if (txtEquipmentType2 == null)
          return;
        txtEquipmentType2.TextChanged += eventHandler;
      }
    }

    public virtual TextBox txtWarrantyCo2
    {
      get
      {
        return this._txtWarrantyCo2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtGPropVal_0_TextChanged);
        TextBox txtWarrantyCo2_1 = this._txtWarrantyCo2;
        if (txtWarrantyCo2_1 != null)
          txtWarrantyCo2_1.TextChanged -= eventHandler;
        this._txtWarrantyCo2 = value;
        TextBox txtWarrantyCo2_2 = this._txtWarrantyCo2;
        if (txtWarrantyCo2_2 == null)
          return;
        txtWarrantyCo2_2.TextChanged += eventHandler;
      }
    }

    public virtual Label lblEquipmentMake { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblModel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblManufacturer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblWarrantyCo1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblCapacity { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblControlTypeMake { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblSerialNumber { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblYearInstalled { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblDateManufactured { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblWarrantyDate1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblEquipmentType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblWarrantyDate2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblWarrantyCo2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual DateTimePicker dtpWarrantyDate2
    {
      get
      {
        return this._dtpWarrantyDate2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.dtpDateManufactured_ValueChanged);
        DateTimePicker dtpWarrantyDate2_1 = this._dtpWarrantyDate2;
        if (dtpWarrantyDate2_1 != null)
          dtpWarrantyDate2_1.ValueChanged -= eventHandler;
        this._dtpWarrantyDate2 = value;
        DateTimePicker dtpWarrantyDate2_2 = this._dtpWarrantyDate2;
        if (dtpWarrantyDate2_2 == null)
          return;
        dtpWarrantyDate2_2.ValueChanged += eventHandler;
      }
    }

    internal virtual DateTimePicker dtpDateManufactured
    {
      get
      {
        return this._dtpDateManufactured;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.dtpDateManufactured_ValueChanged);
        DateTimePicker dateManufactured1 = this._dtpDateManufactured;
        if (dateManufactured1 != null)
          dateManufactured1.ValueChanged -= eventHandler;
        this._dtpDateManufactured = value;
        DateTimePicker dateManufactured2 = this._dtpDateManufactured;
        if (dateManufactured2 == null)
          return;
        dateManufactured2.ValueChanged += eventHandler;
      }
    }

    internal virtual DateTimePicker dtpWarrantyDate1
    {
      get
      {
        return this._dtpWarrantyDate1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.dtpDateManufactured_ValueChanged);
        DateTimePicker dtpWarrantyDate1_1 = this._dtpWarrantyDate1;
        if (dtpWarrantyDate1_1 != null)
          dtpWarrantyDate1_1.ValueChanged -= eventHandler;
        this._dtpWarrantyDate1 = value;
        DateTimePicker dtpWarrantyDate1_2 = this._dtpWarrantyDate1;
        if (dtpWarrantyDate1_2 == null)
          return;
        dtpWarrantyDate1_2.ValueChanged += eventHandler;
      }
    }

    internal virtual CheckBox chkWarrantyDate1
    {
      get
      {
        return this._chkWarrantyDate1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkWarrantyDate1_CheckedChanged);
        CheckBox chkWarrantyDate1_1 = this._chkWarrantyDate1;
        if (chkWarrantyDate1_1 != null)
          chkWarrantyDate1_1.CheckedChanged -= eventHandler;
        this._chkWarrantyDate1 = value;
        CheckBox chkWarrantyDate1_2 = this._chkWarrantyDate1;
        if (chkWarrantyDate1_2 == null)
          return;
        chkWarrantyDate1_2.CheckedChanged += eventHandler;
      }
    }

    internal virtual CheckBox chkWarrantyDate2
    {
      get
      {
        return this._chkWarrantyDate2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkWarrantyDate2_CheckedChanged);
        CheckBox chkWarrantyDate2_1 = this._chkWarrantyDate2;
        if (chkWarrantyDate2_1 != null)
          chkWarrantyDate2_1.CheckedChanged -= eventHandler;
        this._chkWarrantyDate2 = value;
        CheckBox chkWarrantyDate2_2 = this._chkWarrantyDate2;
        if (chkWarrantyDate2_2 == null)
          return;
        chkWarrantyDate2_2.CheckedChanged += eventHandler;
      }
    }

    internal virtual CheckBox chkManufacturedDate
    {
      get
      {
        return this._chkManufacturedDate;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkManufacturedDate_CheckedChanged);
        CheckBox manufacturedDate1 = this._chkManufacturedDate;
        if (manufacturedDate1 != null)
          manufacturedDate1.CheckedChanged -= eventHandler;
        this._chkManufacturedDate = value;
        CheckBox manufacturedDate2 = this._chkManufacturedDate;
        if (manufacturedDate2 == null)
          return;
        manufacturedDate2.CheckedChanged += eventHandler;
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmSecDetails));
      this.ToolTip1 = new ToolTip(this.components);
      this.cmdEditName = new Button();
      this.ImageList1 = new ImageList(this.components);
      this.frmNoRecord = new Panel();
      this.lblNoEquip = new Label();
      this.frmEquip = new Panel();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.lblSelect = new Label();
      this.Panel1 = new Panel();
      this.cboEquip = new ComboBox();
      this.txtIDNumber = new TextBox();
      this.frmEqDet = new GroupBox();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.txtDateInstalled = new TextBox();
      this.lblControlTypeMake = new Label();
      this.dtpDateManufactured = new DateTimePicker();
      this.txtControlTypeMake = new TextBox();
      this.chkManufacturedDate = new CheckBox();
      this.lblYearInstalled = new Label();
      this.lblDateManufactured = new Label();
      this.txtLocation = new TextBox();
      this.lblLocation = new Label();
      this.dtpWarrantyDate2 = new DateTimePicker();
      this.chkWarrantyDate2 = new CheckBox();
      this.lblWarrantyDate2 = new Label();
      this.dtpWarrantyDate1 = new DateTimePicker();
      this.chkWarrantyDate1 = new CheckBox();
      this.lblWarrantyDate1 = new Label();
      this.txtManufacturer = new TextBox();
      this.lblWarrantyCo2 = new Label();
      this.lblManufacturer = new Label();
      this.txtCapacity = new TextBox();
      this.lblCapacity = new Label();
      this.txtSerialNumber = new TextBox();
      this.lblSerialNumber = new Label();
      this.txtModel = new TextBox();
      this.lblModel = new Label();
      this.txtEquipmentMake = new TextBox();
      this.lblEquipmentMake = new Label();
      this.lblEquipmentType = new Label();
      this.txtEquipmentType = new TextBox();
      this.lblWarrantyCo1 = new Label();
      this.txtWarrantyCo1 = new TextBox();
      this.txtWarrantyCo2 = new TextBox();
      this.HelpProvider = new HelpProvider();
      this.tsCommands = new ToolStrip();
      this.tsbClose = new ToolStripButton();
      this.tsbSave = new ToolStripButton();
      this.tsbNew = new ToolStripButton();
      this.tsbDelete = new ToolStripButton();
      this.tsbComments = new ToolStripButton();
      this.frmNoRecord.SuspendLayout();
      this.frmEquip.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.frmEqDet.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.tsCommands.SuspendLayout();
      this.SuspendLayout();
      this.cmdEditName.BackColor = SystemColors.Control;
      this.cmdEditName.Cursor = Cursors.Default;
      this.cmdEditName.ForeColor = SystemColors.ControlText;
      this.cmdEditName.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Edit;
      this.cmdEditName.Location = new Point(713, 3);
      this.cmdEditName.Name = "cmdEditName";
      this.cmdEditName.RightToLeft = RightToLeft.No;
      this.cmdEditName.Size = new Size(1, 25);
      this.cmdEditName.TabIndex = 34;
      this.cmdEditName.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdEditName, "Edit the equipment ID");
      this.cmdEditName.UseVisualStyleBackColor = false;
      this.ImageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ImageList1.ImageStream");
      this.ImageList1.TransparentColor = Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "Symbol Delete.png");
      this.ImageList1.Images.SetKeyName(1, "Logout.png");
      this.ImageList1.Images.SetKeyName(2, "save.png");
      this.ImageList1.Images.SetKeyName(3, "Document Add.png");
      this.ImageList1.Images.SetKeyName(4, "Delete.png");
      this.ImageList1.Images.SetKeyName(5, "Clipboard.png");
      this.frmNoRecord.AutoSize = true;
      this.frmNoRecord.BackColor = SystemColors.Control;
      this.frmNoRecord.Controls.Add((Control) this.lblNoEquip);
      this.frmNoRecord.Cursor = Cursors.Default;
      this.frmNoRecord.Dock = DockStyle.Fill;
      this.frmNoRecord.ForeColor = SystemColors.ControlText;
      this.frmNoRecord.Location = new Point(0, 27);
      this.frmNoRecord.Name = "frmNoRecord";
      this.frmNoRecord.RightToLeft = RightToLeft.No;
      this.frmNoRecord.Size = new Size(650, 336);
      this.frmNoRecord.TabIndex = 20;
      this.lblNoEquip.BackColor = SystemColors.Control;
      this.lblNoEquip.Cursor = Cursors.Default;
      this.lblNoEquip.Dock = DockStyle.Fill;
      this.lblNoEquip.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblNoEquip.ForeColor = SystemColors.ControlText;
      this.lblNoEquip.Location = new Point(0, 0);
      this.lblNoEquip.Name = "lblNoEquip";
      this.lblNoEquip.RightToLeft = RightToLeft.No;
      this.lblNoEquip.Size = new Size(650, 336);
      this.lblNoEquip.TabIndex = 17;
      this.lblNoEquip.Text = "No equipment listed in current inventory";
      this.lblNoEquip.TextAlign = ContentAlignment.MiddleCenter;
      this.frmEquip.BackColor = SystemColors.Control;
      this.frmEquip.Controls.Add((Control) this.TableLayoutPanel1);
      this.frmEquip.Cursor = Cursors.Default;
      this.frmEquip.Dock = DockStyle.Fill;
      this.frmEquip.ForeColor = SystemColors.ControlText;
      this.frmEquip.Location = new Point(0, 27);
      this.frmEquip.Name = "frmEquip";
      this.frmEquip.RightToLeft = RightToLeft.No;
      this.frmEquip.Size = new Size(650, 336);
      this.frmEquip.TabIndex = 21;
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Controls.Add((Control) this.lblSelect, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.Panel1, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdEditName, 2, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.frmEqDet, 0, 1);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Size = new Size(650, 336);
      this.TableLayoutPanel1.TabIndex = 38;
      this.lblSelect.AutoSize = true;
      this.lblSelect.BackColor = SystemColors.Control;
      this.lblSelect.Cursor = Cursors.Default;
      this.lblSelect.Dock = DockStyle.Fill;
      this.lblSelect.ForeColor = SystemColors.ControlText;
      this.lblSelect.Location = new Point(3, 0);
      this.lblSelect.Name = "lblSelect";
      this.lblSelect.RightToLeft = RightToLeft.No;
      this.lblSelect.Size = new Size(122, 31);
      this.lblSelect.TabIndex = 36;
      this.lblSelect.Text = "Select Equipment:";
      this.lblSelect.TextAlign = ContentAlignment.MiddleRight;
      this.Panel1.AutoSize = true;
      this.Panel1.Controls.Add((Control) this.cboEquip);
      this.Panel1.Controls.Add((Control) this.txtIDNumber);
      this.Panel1.Dock = DockStyle.Fill;
      this.Panel1.Location = new Point(128, 0);
      this.Panel1.Margin = new Padding(0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new Size(582, 31);
      this.Panel1.TabIndex = 37;
      this.cboEquip.Dock = DockStyle.Fill;
      this.cboEquip.Location = new Point(0, 0);
      this.cboEquip.Name = "cboEquip";
      this.cboEquip.Size = new Size(582, 24);
      this.cboEquip.TabIndex = 37;
      this.cboEquip.Text = "ComboBox1";
      this.txtIDNumber.AcceptsReturn = true;
      this.txtIDNumber.BackColor = SystemColors.Window;
      this.txtIDNumber.Cursor = Cursors.IBeam;
      this.txtIDNumber.Dock = DockStyle.Fill;
      this.txtIDNumber.ForeColor = SystemColors.WindowText;
      this.txtIDNumber.Location = new Point(0, 0);
      this.txtIDNumber.MaxLength = 0;
      this.txtIDNumber.Name = "txtIDNumber";
      this.txtIDNumber.RightToLeft = RightToLeft.No;
      this.txtIDNumber.Size = new Size(582, 22);
      this.txtIDNumber.TabIndex = 1;
      this.txtIDNumber.Tag = (object) "ID_Number";
      this.frmEqDet.AutoSize = true;
      this.frmEqDet.BackColor = SystemColors.Control;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.frmEqDet, 3);
      this.frmEqDet.Controls.Add((Control) this.TableLayoutPanel2);
      this.frmEqDet.Dock = DockStyle.Fill;
      this.frmEqDet.ForeColor = SystemColors.ControlText;
      this.frmEqDet.Location = new Point(3, 34);
      this.frmEqDet.Name = "frmEqDet";
      this.frmEqDet.RightToLeft = RightToLeft.No;
      this.frmEqDet.Size = new Size(644, 299);
      this.frmEqDet.TabIndex = 19;
      this.frmEqDet.TabStop = false;
      this.TableLayoutPanel2.AutoSize = true;
      this.TableLayoutPanel2.ColumnCount = 5;
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.Controls.Add((Control) this.txtDateInstalled, 3, 9);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblControlTypeMake, 0, 9);
      this.TableLayoutPanel2.Controls.Add((Control) this.dtpDateManufactured, 4, 8);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtControlTypeMake, 1, 9);
      this.TableLayoutPanel2.Controls.Add((Control) this.chkManufacturedDate, 3, 8);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblYearInstalled, 2, 9);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblDateManufactured, 2, 8);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtLocation, 1, 8);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblLocation, 0, 8);
      this.TableLayoutPanel2.Controls.Add((Control) this.dtpWarrantyDate2, 4, 7);
      this.TableLayoutPanel2.Controls.Add((Control) this.chkWarrantyDate2, 3, 7);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblWarrantyDate2, 2, 7);
      this.TableLayoutPanel2.Controls.Add((Control) this.dtpWarrantyDate1, 4, 6);
      this.TableLayoutPanel2.Controls.Add((Control) this.chkWarrantyDate1, 3, 6);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblWarrantyDate1, 2, 6);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtManufacturer, 1, 5);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblWarrantyCo2, 0, 7);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblManufacturer, 0, 5);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtCapacity, 1, 4);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblCapacity, 0, 4);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtSerialNumber, 1, 3);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblSerialNumber, 0, 3);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtModel, 1, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblModel, 0, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtEquipmentMake, 1, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblEquipmentMake, 0, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblEquipmentType, 0, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtEquipmentType, 1, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblWarrantyCo1, 0, 6);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtWarrantyCo1, 1, 6);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtWarrantyCo2, 1, 7);
      this.TableLayoutPanel2.Dock = DockStyle.Fill;
      this.TableLayoutPanel2.Location = new Point(3, 18);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 11;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel2.Size = new Size(638, 278);
      this.TableLayoutPanel2.TabIndex = 40;
      this.txtDateInstalled.AcceptsReturn = true;
      this.txtDateInstalled.BackColor = SystemColors.Window;
      this.TableLayoutPanel2.SetColumnSpan((Control) this.txtDateInstalled, 2);
      this.txtDateInstalled.Cursor = Cursors.IBeam;
      this.txtDateInstalled.Dock = DockStyle.Fill;
      this.txtDateInstalled.ForeColor = SystemColors.WindowText;
      this.txtDateInstalled.Location = new Point(491, (int) byte.MaxValue);
      this.txtDateInstalled.MaxLength = 4;
      this.txtDateInstalled.Name = "txtDateInstalled";
      this.txtDateInstalled.RightToLeft = RightToLeft.No;
      this.txtDateInstalled.Size = new Size(144, 22);
      this.txtDateInstalled.TabIndex = 15;
      this.txtDateInstalled.Tag = (object) "Date Installed";
      this.lblControlTypeMake.AutoSize = true;
      this.lblControlTypeMake.BackColor = SystemColors.Control;
      this.lblControlTypeMake.Cursor = Cursors.Default;
      this.lblControlTypeMake.Dock = DockStyle.Fill;
      this.lblControlTypeMake.ForeColor = SystemColors.ControlText;
      this.lblControlTypeMake.Location = new Point(3, 252);
      this.lblControlTypeMake.Name = "lblControlTypeMake";
      this.lblControlTypeMake.RightToLeft = RightToLeft.No;
      this.lblControlTypeMake.Size = new Size(145, 28);
      this.lblControlTypeMake.TabIndex = 27;
      this.lblControlTypeMake.Text = "Control Type/Make:";
      this.lblControlTypeMake.TextAlign = ContentAlignment.MiddleRight;
      this.dtpDateManufactured.Checked = false;
      this.dtpDateManufactured.Dock = DockStyle.Fill;
      this.dtpDateManufactured.Format = DateTimePickerFormat.Short;
      this.dtpDateManufactured.Location = new Point(515, 227);
      this.dtpDateManufactured.Name = "dtpDateManufactured";
      this.dtpDateManufactured.Size = new Size(120, 22);
      this.dtpDateManufactured.TabIndex = 35;
      this.txtControlTypeMake.AcceptsReturn = true;
      this.txtControlTypeMake.BackColor = SystemColors.Window;
      this.txtControlTypeMake.Cursor = Cursors.IBeam;
      this.txtControlTypeMake.Dock = DockStyle.Fill;
      this.txtControlTypeMake.ForeColor = SystemColors.WindowText;
      this.txtControlTypeMake.Location = new Point(154, (int) byte.MaxValue);
      this.txtControlTypeMake.MaxLength = 0;
      this.txtControlTypeMake.Name = "txtControlTypeMake";
      this.txtControlTypeMake.RightToLeft = RightToLeft.No;
      this.txtControlTypeMake.Size = new Size(192, 22);
      this.txtControlTypeMake.TabIndex = 14;
      this.txtControlTypeMake.Tag = (object) "Control_Type_Make";
      this.chkManufacturedDate.AutoSize = true;
      this.chkManufacturedDate.Dock = DockStyle.Fill;
      this.chkManufacturedDate.Location = new Point(491, 227);
      this.chkManufacturedDate.Name = "chkManufacturedDate";
      this.chkManufacturedDate.Size = new Size(18, 22);
      this.chkManufacturedDate.TabIndex = 39;
      this.lblYearInstalled.AutoSize = true;
      this.lblYearInstalled.BackColor = SystemColors.Control;
      this.lblYearInstalled.Cursor = Cursors.Default;
      this.lblYearInstalled.Dock = DockStyle.Fill;
      this.lblYearInstalled.ForeColor = SystemColors.ControlText;
      this.lblYearInstalled.Location = new Point(352, 252);
      this.lblYearInstalled.Name = "lblYearInstalled";
      this.lblYearInstalled.RightToLeft = RightToLeft.No;
      this.lblYearInstalled.Size = new Size(133, 28);
      this.lblYearInstalled.TabIndex = 25;
      this.lblYearInstalled.Text = "Year Installed:";
      this.lblYearInstalled.TextAlign = ContentAlignment.MiddleRight;
      this.lblDateManufactured.AutoSize = true;
      this.lblDateManufactured.BackColor = SystemColors.Control;
      this.lblDateManufactured.Cursor = Cursors.Default;
      this.lblDateManufactured.Dock = DockStyle.Fill;
      this.lblDateManufactured.ForeColor = SystemColors.ControlText;
      this.lblDateManufactured.Location = new Point(352, 224);
      this.lblDateManufactured.Name = "lblDateManufactured";
      this.lblDateManufactured.RightToLeft = RightToLeft.No;
      this.lblDateManufactured.Size = new Size(133, 28);
      this.lblDateManufactured.TabIndex = 24;
      this.lblDateManufactured.Text = "Date Manufactured:";
      this.lblDateManufactured.TextAlign = ContentAlignment.MiddleRight;
      this.txtLocation.AcceptsReturn = true;
      this.txtLocation.BackColor = SystemColors.Window;
      this.txtLocation.Cursor = Cursors.IBeam;
      this.txtLocation.Dock = DockStyle.Fill;
      this.txtLocation.ForeColor = SystemColors.WindowText;
      this.txtLocation.Location = new Point(154, 227);
      this.txtLocation.MaxLength = 0;
      this.txtLocation.Name = "txtLocation";
      this.txtLocation.RightToLeft = RightToLeft.No;
      this.txtLocation.Size = new Size(192, 22);
      this.txtLocation.TabIndex = 12;
      this.txtLocation.Tag = (object) "Location";
      this.lblLocation.AutoSize = true;
      this.lblLocation.BackColor = SystemColors.Control;
      this.lblLocation.Cursor = Cursors.Default;
      this.lblLocation.Dock = DockStyle.Fill;
      this.lblLocation.ForeColor = SystemColors.ControlText;
      this.lblLocation.Location = new Point(3, 224);
      this.lblLocation.Name = "lblLocation";
      this.lblLocation.RightToLeft = RightToLeft.No;
      this.lblLocation.Size = new Size(145, 28);
      this.lblLocation.TabIndex = 30;
      this.lblLocation.Text = "Location:";
      this.lblLocation.TextAlign = ContentAlignment.MiddleRight;
      this.dtpWarrantyDate2.Checked = false;
      this.dtpWarrantyDate2.Dock = DockStyle.Fill;
      this.dtpWarrantyDate2.Format = DateTimePickerFormat.Short;
      this.dtpWarrantyDate2.Location = new Point(515, 199);
      this.dtpWarrantyDate2.Name = "dtpWarrantyDate2";
      this.dtpWarrantyDate2.Size = new Size(120, 22);
      this.dtpWarrantyDate2.TabIndex = 36;
      this.chkWarrantyDate2.AutoSize = true;
      this.chkWarrantyDate2.Dock = DockStyle.Fill;
      this.chkWarrantyDate2.Location = new Point(491, 199);
      this.chkWarrantyDate2.Name = "chkWarrantyDate2";
      this.chkWarrantyDate2.Size = new Size(18, 22);
      this.chkWarrantyDate2.TabIndex = 38;
      this.lblWarrantyDate2.AutoSize = true;
      this.lblWarrantyDate2.BackColor = SystemColors.Control;
      this.lblWarrantyDate2.Cursor = Cursors.Default;
      this.lblWarrantyDate2.Dock = DockStyle.Fill;
      this.lblWarrantyDate2.ForeColor = SystemColors.ControlText;
      this.lblWarrantyDate2.Location = new Point(352, 196);
      this.lblWarrantyDate2.Name = "lblWarrantyDate2";
      this.lblWarrantyDate2.RightToLeft = RightToLeft.No;
      this.lblWarrantyDate2.Size = new Size(133, 28);
      this.lblWarrantyDate2.TabIndex = 21;
      this.lblWarrantyDate2.Text = "Warranty Date 2:";
      this.lblWarrantyDate2.TextAlign = ContentAlignment.MiddleRight;
      this.dtpWarrantyDate1.Checked = false;
      this.dtpWarrantyDate1.Dock = DockStyle.Fill;
      this.dtpWarrantyDate1.Format = DateTimePickerFormat.Short;
      this.dtpWarrantyDate1.Location = new Point(515, 171);
      this.dtpWarrantyDate1.Name = "dtpWarrantyDate1";
      this.dtpWarrantyDate1.Size = new Size(120, 22);
      this.dtpWarrantyDate1.TabIndex = 34;
      this.chkWarrantyDate1.AutoSize = true;
      this.chkWarrantyDate1.Dock = DockStyle.Fill;
      this.chkWarrantyDate1.Location = new Point(491, 171);
      this.chkWarrantyDate1.Name = "chkWarrantyDate1";
      this.chkWarrantyDate1.Size = new Size(18, 22);
      this.chkWarrantyDate1.TabIndex = 37;
      this.lblWarrantyDate1.AutoSize = true;
      this.lblWarrantyDate1.BackColor = SystemColors.Control;
      this.lblWarrantyDate1.Cursor = Cursors.Default;
      this.lblWarrantyDate1.Dock = DockStyle.Fill;
      this.lblWarrantyDate1.ForeColor = SystemColors.ControlText;
      this.lblWarrantyDate1.Location = new Point(352, 168);
      this.lblWarrantyDate1.Name = "lblWarrantyDate1";
      this.lblWarrantyDate1.RightToLeft = RightToLeft.No;
      this.lblWarrantyDate1.Size = new Size(133, 28);
      this.lblWarrantyDate1.TabIndex = 23;
      this.lblWarrantyDate1.Text = "Warranty Date 1:";
      this.lblWarrantyDate1.TextAlign = ContentAlignment.MiddleRight;
      this.txtManufacturer.AcceptsReturn = true;
      this.txtManufacturer.BackColor = SystemColors.Window;
      this.txtManufacturer.Cursor = Cursors.IBeam;
      this.txtManufacturer.Dock = DockStyle.Fill;
      this.txtManufacturer.ForeColor = SystemColors.WindowText;
      this.txtManufacturer.Location = new Point(154, 143);
      this.txtManufacturer.MaxLength = 40;
      this.txtManufacturer.Name = "txtManufacturer";
      this.txtManufacturer.RightToLeft = RightToLeft.No;
      this.txtManufacturer.Size = new Size(192, 22);
      this.txtManufacturer.TabIndex = 7;
      this.txtManufacturer.Tag = (object) "Manufacturer";
      this.lblWarrantyCo2.AutoSize = true;
      this.lblWarrantyCo2.BackColor = SystemColors.Control;
      this.lblWarrantyCo2.Cursor = Cursors.Default;
      this.lblWarrantyCo2.Dock = DockStyle.Fill;
      this.lblWarrantyCo2.ForeColor = SystemColors.ControlText;
      this.lblWarrantyCo2.Location = new Point(3, 196);
      this.lblWarrantyCo2.Name = "lblWarrantyCo2";
      this.lblWarrantyCo2.RightToLeft = RightToLeft.No;
      this.lblWarrantyCo2.Size = new Size(145, 28);
      this.lblWarrantyCo2.TabIndex = 20;
      this.lblWarrantyCo2.Text = "Warranty Company 2:";
      this.lblWarrantyCo2.TextAlign = ContentAlignment.MiddleRight;
      this.lblManufacturer.AutoSize = true;
      this.lblManufacturer.BackColor = SystemColors.Control;
      this.lblManufacturer.Cursor = Cursors.Default;
      this.lblManufacturer.Dock = DockStyle.Fill;
      this.lblManufacturer.ForeColor = SystemColors.ControlText;
      this.lblManufacturer.Location = new Point(3, 140);
      this.lblManufacturer.Name = "lblManufacturer";
      this.lblManufacturer.RightToLeft = RightToLeft.No;
      this.lblManufacturer.Size = new Size(145, 28);
      this.lblManufacturer.TabIndex = 31;
      this.lblManufacturer.Text = "Manufacturer:";
      this.lblManufacturer.TextAlign = ContentAlignment.MiddleRight;
      this.txtCapacity.AcceptsReturn = true;
      this.txtCapacity.BackColor = SystemColors.Window;
      this.txtCapacity.Cursor = Cursors.IBeam;
      this.txtCapacity.Dock = DockStyle.Fill;
      this.txtCapacity.ForeColor = SystemColors.WindowText;
      this.txtCapacity.Location = new Point(154, 115);
      this.txtCapacity.MaxLength = 0;
      this.txtCapacity.Name = "txtCapacity";
      this.txtCapacity.RightToLeft = RightToLeft.No;
      this.txtCapacity.Size = new Size(192, 22);
      this.txtCapacity.TabIndex = 6;
      this.txtCapacity.Tag = (object) "Capacity";
      this.lblCapacity.AutoSize = true;
      this.lblCapacity.BackColor = SystemColors.Control;
      this.lblCapacity.Cursor = Cursors.Default;
      this.lblCapacity.Dock = DockStyle.Fill;
      this.lblCapacity.ForeColor = SystemColors.ControlText;
      this.lblCapacity.Location = new Point(3, 112);
      this.lblCapacity.Name = "lblCapacity";
      this.lblCapacity.RightToLeft = RightToLeft.No;
      this.lblCapacity.Size = new Size(145, 28);
      this.lblCapacity.TabIndex = 28;
      this.lblCapacity.Text = "Capacity:";
      this.lblCapacity.TextAlign = ContentAlignment.MiddleRight;
      this.txtSerialNumber.AcceptsReturn = true;
      this.txtSerialNumber.BackColor = SystemColors.Window;
      this.txtSerialNumber.Cursor = Cursors.IBeam;
      this.txtSerialNumber.Dock = DockStyle.Fill;
      this.txtSerialNumber.ForeColor = SystemColors.WindowText;
      this.txtSerialNumber.Location = new Point(154, 87);
      this.txtSerialNumber.MaxLength = 0;
      this.txtSerialNumber.Name = "txtSerialNumber";
      this.txtSerialNumber.RightToLeft = RightToLeft.No;
      this.txtSerialNumber.Size = new Size(192, 22);
      this.txtSerialNumber.TabIndex = 4;
      this.txtSerialNumber.Tag = (object) "Serial_Number";
      this.lblSerialNumber.AutoSize = true;
      this.lblSerialNumber.BackColor = SystemColors.Control;
      this.lblSerialNumber.Cursor = Cursors.Default;
      this.lblSerialNumber.Dock = DockStyle.Fill;
      this.lblSerialNumber.ForeColor = SystemColors.ControlText;
      this.lblSerialNumber.Location = new Point(3, 84);
      this.lblSerialNumber.Name = "lblSerialNumber";
      this.lblSerialNumber.RightToLeft = RightToLeft.No;
      this.lblSerialNumber.Size = new Size(145, 28);
      this.lblSerialNumber.TabIndex = 26;
      this.lblSerialNumber.Text = "Serial Number:";
      this.lblSerialNumber.TextAlign = ContentAlignment.MiddleRight;
      this.txtModel.AcceptsReturn = true;
      this.txtModel.BackColor = SystemColors.Window;
      this.txtModel.Cursor = Cursors.IBeam;
      this.txtModel.Dock = DockStyle.Fill;
      this.txtModel.ForeColor = SystemColors.WindowText;
      this.txtModel.Location = new Point(154, 59);
      this.txtModel.MaxLength = 0;
      this.txtModel.Name = "txtModel";
      this.txtModel.RightToLeft = RightToLeft.No;
      this.txtModel.Size = new Size(192, 22);
      this.txtModel.TabIndex = 5;
      this.txtModel.Tag = (object) "Model";
      this.lblModel.AutoSize = true;
      this.lblModel.BackColor = SystemColors.Control;
      this.lblModel.Cursor = Cursors.Default;
      this.lblModel.Dock = DockStyle.Fill;
      this.lblModel.ForeColor = SystemColors.ControlText;
      this.lblModel.Location = new Point(3, 56);
      this.lblModel.Name = "lblModel";
      this.lblModel.RightToLeft = RightToLeft.No;
      this.lblModel.Size = new Size(145, 28);
      this.lblModel.TabIndex = 32;
      this.lblModel.Text = "Model:";
      this.lblModel.TextAlign = ContentAlignment.MiddleRight;
      this.txtEquipmentMake.AcceptsReturn = true;
      this.txtEquipmentMake.BackColor = SystemColors.Window;
      this.txtEquipmentMake.Cursor = Cursors.IBeam;
      this.txtEquipmentMake.Dock = DockStyle.Fill;
      this.txtEquipmentMake.ForeColor = SystemColors.WindowText;
      this.txtEquipmentMake.Location = new Point(154, 31);
      this.txtEquipmentMake.MaxLength = 0;
      this.txtEquipmentMake.Name = "txtEquipmentMake";
      this.txtEquipmentMake.RightToLeft = RightToLeft.No;
      this.txtEquipmentMake.Size = new Size(192, 22);
      this.txtEquipmentMake.TabIndex = 3;
      this.txtEquipmentMake.Tag = (object) "Equipment_Make";
      this.lblEquipmentMake.AutoSize = true;
      this.lblEquipmentMake.BackColor = SystemColors.Control;
      this.lblEquipmentMake.Cursor = Cursors.Default;
      this.lblEquipmentMake.Dock = DockStyle.Fill;
      this.lblEquipmentMake.ForeColor = SystemColors.ControlText;
      this.lblEquipmentMake.Location = new Point(3, 28);
      this.lblEquipmentMake.Name = "lblEquipmentMake";
      this.lblEquipmentMake.RightToLeft = RightToLeft.No;
      this.lblEquipmentMake.Size = new Size(145, 28);
      this.lblEquipmentMake.TabIndex = 33;
      this.lblEquipmentMake.Text = "Equipment Make:";
      this.lblEquipmentMake.TextAlign = ContentAlignment.MiddleRight;
      this.lblEquipmentType.AutoSize = true;
      this.lblEquipmentType.BackColor = SystemColors.Control;
      this.lblEquipmentType.Cursor = Cursors.Default;
      this.lblEquipmentType.Dock = DockStyle.Fill;
      this.lblEquipmentType.ForeColor = SystemColors.ControlText;
      this.lblEquipmentType.Location = new Point(3, 0);
      this.lblEquipmentType.Name = "lblEquipmentType";
      this.lblEquipmentType.RightToLeft = RightToLeft.No;
      this.lblEquipmentType.Size = new Size(145, 28);
      this.lblEquipmentType.TabIndex = 22;
      this.lblEquipmentType.Text = "Equipment Type:";
      this.lblEquipmentType.TextAlign = ContentAlignment.MiddleRight;
      this.txtEquipmentType.AcceptsReturn = true;
      this.txtEquipmentType.BackColor = SystemColors.Window;
      this.txtEquipmentType.Cursor = Cursors.IBeam;
      this.txtEquipmentType.Dock = DockStyle.Fill;
      this.txtEquipmentType.ForeColor = SystemColors.WindowText;
      this.txtEquipmentType.Location = new Point(154, 3);
      this.txtEquipmentType.MaxLength = 0;
      this.txtEquipmentType.Name = "txtEquipmentType";
      this.txtEquipmentType.RightToLeft = RightToLeft.No;
      this.txtEquipmentType.Size = new Size(192, 22);
      this.txtEquipmentType.TabIndex = 2;
      this.txtEquipmentType.Tag = (object) "Equipment_Type";
      this.lblWarrantyCo1.AutoSize = true;
      this.lblWarrantyCo1.BackColor = SystemColors.Control;
      this.lblWarrantyCo1.Cursor = Cursors.Default;
      this.lblWarrantyCo1.Dock = DockStyle.Fill;
      this.lblWarrantyCo1.ForeColor = SystemColors.ControlText;
      this.lblWarrantyCo1.Location = new Point(3, 168);
      this.lblWarrantyCo1.Name = "lblWarrantyCo1";
      this.lblWarrantyCo1.RightToLeft = RightToLeft.No;
      this.lblWarrantyCo1.Size = new Size(145, 28);
      this.lblWarrantyCo1.TabIndex = 29;
      this.lblWarrantyCo1.Text = "Warranty Company 1:";
      this.lblWarrantyCo1.TextAlign = ContentAlignment.MiddleRight;
      this.txtWarrantyCo1.AcceptsReturn = true;
      this.txtWarrantyCo1.BackColor = SystemColors.Window;
      this.txtWarrantyCo1.Cursor = Cursors.IBeam;
      this.txtWarrantyCo1.Dock = DockStyle.Fill;
      this.txtWarrantyCo1.ForeColor = SystemColors.WindowText;
      this.txtWarrantyCo1.Location = new Point(154, 171);
      this.txtWarrantyCo1.MaxLength = 0;
      this.txtWarrantyCo1.Name = "txtWarrantyCo1";
      this.txtWarrantyCo1.RightToLeft = RightToLeft.No;
      this.txtWarrantyCo1.Size = new Size(192, 22);
      this.txtWarrantyCo1.TabIndex = 8;
      this.txtWarrantyCo1.Tag = (object) "Warranty_Company";
      this.txtWarrantyCo2.AcceptsReturn = true;
      this.txtWarrantyCo2.BackColor = SystemColors.Window;
      this.txtWarrantyCo2.Cursor = Cursors.IBeam;
      this.txtWarrantyCo2.Dock = DockStyle.Fill;
      this.txtWarrantyCo2.ForeColor = SystemColors.WindowText;
      this.txtWarrantyCo2.Location = new Point(154, 199);
      this.txtWarrantyCo2.MaxLength = 0;
      this.txtWarrantyCo2.Name = "txtWarrantyCo2";
      this.txtWarrantyCo2.RightToLeft = RightToLeft.No;
      this.txtWarrantyCo2.Size = new Size(192, 22);
      this.txtWarrantyCo2.TabIndex = 10;
      this.txtWarrantyCo2.Tag = (object) "Warranty_Company2";
      this.tsCommands.GripStyle = ToolStripGripStyle.Hidden;
      this.tsCommands.ImageScalingSize = new Size(20, 20);
      this.tsCommands.Items.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.tsbClose,
        (ToolStripItem) this.tsbSave,
        (ToolStripItem) this.tsbNew,
        (ToolStripItem) this.tsbDelete,
        (ToolStripItem) this.tsbComments
      });
      this.tsCommands.Location = new Point(0, 0);
      this.tsCommands.Name = "tsCommands";
      this.tsCommands.Size = new Size(650, 27);
      this.tsCommands.TabIndex = 41;
      this.tsCommands.Text = "tsCommands";
      this.tsbClose.Image = (Image) BuilderRED.My.Resources.Resources.Close;
      this.tsbClose.ImageTransparentColor = Color.Magenta;
      this.tsbClose.Name = "tsbClose";
      this.tsbClose.Size = new Size(69, 24);
      this.tsbClose.Text = "Close";
      this.tsbSave.Enabled = false;
      this.tsbSave.Image = (Image) BuilderRED.My.Resources.Resources.save;
      this.tsbSave.ImageTransparentColor = Color.Magenta;
      this.tsbSave.Name = "tsbSave";
      this.tsbSave.Size = new Size(64, 24);
      this.tsbSave.Text = "Save";
      this.tsbNew.Image = (Image) BuilderRED.My.Resources.Resources._new;
      this.tsbNew.ImageTransparentColor = Color.Magenta;
      this.tsbNew.Name = "tsbNew";
      this.tsbNew.Size = new Size(63, 24);
      this.tsbNew.Text = "New";
      this.tsbDelete.Image = (Image) BuilderRED.My.Resources.Resources.Delete;
      this.tsbDelete.ImageTransparentColor = Color.Magenta;
      this.tsbDelete.Name = "tsbDelete";
      this.tsbDelete.Size = new Size(77, 24);
      this.tsbDelete.Text = "Delete";
      this.tsbComments.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.tsbComments.ImageTransparentColor = Color.Magenta;
      this.tsbComments.Name = "tsbComments";
      this.tsbComments.Size = new Size(104, 24);
      this.tsbComments.Text = "Comments";
      this.AutoScaleBaseSize = new Size(6, 15);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(650, 363);
      this.Controls.Add((Control) this.frmEquip);
      this.Controls.Add((Control) this.frmNoRecord);
      this.Controls.Add((Control) this.tsCommands);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(188, (int) byte.MaxValue);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSecDetails);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Equipment Details";
      this.frmNoRecord.ResumeLayout(false);
      this.frmEquip.ResumeLayout(false);
      this.frmEquip.PerformLayout();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.frmEqDet.ResumeLayout(false);
      this.frmEqDet.PerformLayout();
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.tsCommands.ResumeLayout(false);
      this.tsCommands.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStrip tsCommands { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripButton tsbClose
    {
      get
      {
        return this._tsbClose;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsCommands_Click);
        ToolStripButton tsbClose1 = this._tsbClose;
        if (tsbClose1 != null)
          tsbClose1.Click -= eventHandler;
        this._tsbClose = value;
        ToolStripButton tsbClose2 = this._tsbClose;
        if (tsbClose2 == null)
          return;
        tsbClose2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbSave
    {
      get
      {
        return this._tsbSave;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsCommands_Click);
        ToolStripButton tsbSave1 = this._tsbSave;
        if (tsbSave1 != null)
          tsbSave1.Click -= eventHandler;
        this._tsbSave = value;
        ToolStripButton tsbSave2 = this._tsbSave;
        if (tsbSave2 == null)
          return;
        tsbSave2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbNew
    {
      get
      {
        return this._tsbNew;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsCommands_Click);
        ToolStripButton tsbNew1 = this._tsbNew;
        if (tsbNew1 != null)
          tsbNew1.Click -= eventHandler;
        this._tsbNew = value;
        ToolStripButton tsbNew2 = this._tsbNew;
        if (tsbNew2 == null)
          return;
        tsbNew2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbDelete
    {
      get
      {
        return this._tsbDelete;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsCommands_Click);
        ToolStripButton tsbDelete1 = this._tsbDelete;
        if (tsbDelete1 != null)
          tsbDelete1.Click -= eventHandler;
        this._tsbDelete = value;
        ToolStripButton tsbDelete2 = this._tsbDelete;
        if (tsbDelete2 == null)
          return;
        tsbDelete2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbComments
    {
      get
      {
        return this._tsbComments;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsCommands_Click);
        ToolStripButton tsbComments1 = this._tsbComments;
        if (tsbComments1 != null)
          tsbComments1.Click -= eventHandler;
        this._tsbComments = value;
        ToolStripButton tsbComments2 = this._tsbComments;
        if (tsbComments2 == null)
          return;
        tsbComments2.Click += eventHandler;
      }
    }

    public frmSecDetails()
    {
      this.Load += new EventHandler(this.frmSecDetails_Load);
      this.InitializeComponent();
      float num = this.CreateGraphics().DpiX / 96f;
      this.tsCommands.ImageScalingSize = new Size(checked ((int) Math.Round(unchecked (16.0 * (double) num))), checked ((int) Math.Round(unchecked (16.0 * (double) num))));
    }

    public bool ReadOnly
    {
      get
      {
        return this.m_bReadOnly;
      }
      set
      {
        this.m_bReadOnly = value;
        this.txtIDNumber.ReadOnly = value;
        this.txtModel.ReadOnly = value;
        this.txtEquipmentType.ReadOnly = value;
        this.txtWarrantyCo1.ReadOnly = value;
        this.txtEquipmentMake.ReadOnly = value;
        this.txtControlTypeMake.ReadOnly = value;
        this.txtManufacturer.ReadOnly = value;
        this.txtCapacity.ReadOnly = value;
        this.txtSerialNumber.ReadOnly = value;
        this.txtLocation.ReadOnly = value;
        this.txtWarrantyCo2.ReadOnly = value;
        this.txtDateInstalled.ReadOnly = value;
        this.chkManufacturedDate.Enabled = !value;
        this.chkWarrantyDate1.Enabled = !value;
        this.chkWarrantyDate2.Enabled = !value;
        this.dtpDateManufactured.Enabled = !value;
        this.dtpWarrantyDate1.Enabled = !value;
        this.dtpWarrantyDate2.Enabled = !value;
        this.tsbNew.Enabled = !value;
        this.tsbDelete.Enabled = !value;
        this.cmdEditName.Enabled = !value;
      }
    }

    private void FormatForRecords()
    {
      if (this.cboEquip.Items.Count > 0)
      {
        this.frmNoRecord.Visible = false;
        this.frmEquip.Visible = true;
        if (this.m_bIsEquipment)
        {
          this.lblSelect.Text = "Select Equipment:";
          this.lblSelect.Visible = true;
          this.cboEquip.Visible = true;
          this.txtIDNumber.Visible = false;
          this.cmdEditName.Visible = true;
          this.cmdEditName.Enabled = !this.ReadOnly;
          this.tsbNew.Enabled = !this.ReadOnly;
        }
        else
        {
          this.cmdEditName.Visible = false;
          this.lblSelect.Visible = false;
          this.cboEquip.Visible = false;
          this.txtIDNumber.Visible = false;
          this.tsbNew.Enabled = false;
        }
        this.tsbDelete.Enabled = !this.ReadOnly;
        this.tsbComments.Enabled = true;
      }
      else
      {
        this.frmNoRecord.Visible = true;
        this.frmEquip.Visible = false;
        this.tsbNew.Enabled = !this.ReadOnly;
        this.tsbDelete.Enabled = false;
        this.tsbComments.Enabled = false;
      }
    }

    private void ClearControls()
    {
      this.txtIDNumber.Text = "";
      this.txtModel.Text = "";
      this.txtWarrantyCo1.Text = "";
      this.txtEquipmentMake.Text = "";
      this.txtControlTypeMake.Text = "";
      this.txtManufacturer.Text = "";
      this.txtCapacity.Text = "";
      this.txtSerialNumber.Text = "";
      this.txtLocation.Text = "";
      this.txtWarrantyCo2.Text = "";
      this.txtEquipmentType.Text = "";
      this.chkWarrantyDate1.Checked = false;
      this.chkWarrantyDate2.Checked = false;
      this.chkManufacturedDate.Checked = false;
      this.dtpWarrantyDate1.Value = DateAndTime.Today;
      this.dtpWarrantyDate2.Value = DateAndTime.Today;
      this.dtpDateManufactured.Value = DateAndTime.Today;
      this.txtDateInstalled.Text = "";
    }

    private bool CheckForChanges()
    {
      bool flag;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tsbClose.Text, "Cancel", false) == 0)
      {
        switch (Interaction.MsgBox((object) "Data has changed.  Save changes?", MsgBoxStyle.YesNoCancel, (object) "Section Details"))
        {
          case MsgBoxResult.Yes:
            this.SaveData();
            this.SetChanged(false);
            flag = true;
            break;
          case MsgBoxResult.No:
            flag = true;
            this.SetChanged(false);
            break;
          default:
            flag = false;
            break;
        }
      }
      else
        flag = true;
      return flag;
    }

    public string aSection
    {
      set
      {
        try
        {
          this.m_strSection = value;
          if (Strings.InStr(1, this.Text, "Equipment", CompareMethod.Binary) > 0)
          {
            this.m_bIsEquipment = true;
            this.frmEqDet.Top = checked ((int) Math.Round(Support.TwipsToPixelsY(unchecked (Support.PixelsToTwipsY((double) this.cboEquip.Top) + Support.PixelsToTwipsY((double) this.cboEquip.Height) + 60.0))));
          }
          else
          {
            this.m_bIsEquipment = false;
            this.frmEqDet.Top = this.cboEquip.Top;
          }
          this.frmEquip.Visible = false;
          this.cboEquip.Visible = false;
          this.lblSelect.Visible = false;
          this.txtIDNumber.Visible = false;
          this.cmdEditName.Visible = false;
          this.frmNoRecord.Visible = true;
          this.GetEquipmentList();
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          mdUtility.Errorhandler(ex, this.Name, nameof (aSection));
          ProjectData.ClearProjectError();
        }
      }
    }

    private void GetEquipmentList()
    {
      try
      {
        this.m_bDDLoad = true;
        this.cboEquip.DataSource = (object) null;
        this.cboEquip.SelectedIndex = -1;
        this.m_bDDLoad = false;
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [SD_ID], [ID_Number] FROM [SectionDetails] WHERE [SD_Sec_ID]='" + Strings.Replace(this.m_strSection, "'", "''", 1, -1, CompareMethod.Binary) + "' AND (BRED_Status <> 'D' OR BRED_Status IS NULL) ORDER BY [ID_Number]");
        this.cboEquip.DisplayMember = "ID_Number";
        this.cboEquip.ValueMember = "SD_ID";
        if (dataTable.Rows.Count > 0)
        {
          if (this.m_bIsEquipment)
            this.cboEquip.Visible = true;
          if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject((object) ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_strCurrentSec, "", false) > 0U), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual((object) this.m_strCurrentSec, dataTable.Rows[0]["SD_ID"], false))))
          {
            this.m_bDDLoad = true;
            this.cboEquip.DataSource = (object) dataTable;
            this.cboEquip.SelectedIndex = -1;
            this.m_bDDLoad = false;
            this.cboEquip.SelectedValue = (object) this.m_strCurrentSec;
          }
          else
          {
            this.cboEquip.DataSource = (object) dataTable;
            this.cboEquip.SelectedIndex = 0;
          }
        }
        else
          this.FormatForRecords();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (GetEquipmentList));
        ProjectData.ClearProjectError();
      }
    }

    private void CloseForm()
    {
      try
      {
        bool flag;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tsbClose.Text, "Close", false) == 0)
          flag = true;
        if (!this.CheckForChanges())
          return;
        if (flag)
          this.Close();
        else
          this.FormatForRecords();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (CloseForm));
        ProjectData.ClearProjectError();
      }
    }

    private void CreateNew()
    {
      try
      {
        this.m_bLoaded = false;
        this.ClearControls();
        this.m_bLoaded = true;
        this.m_bIsNew = true;
        this.cboEquip.Visible = false;
        this.frmNoRecord.Visible = false;
        this.frmEquip.Visible = true;
        if (this.m_bIsEquipment)
        {
          this.lblSelect.Text = "Enter Equipment ID:";
          this.lblSelect.Visible = true;
          this.txtIDNumber.Visible = true;
          this.txtIDNumber.Focus();
          this.cmdEditName.Visible = true;
        }
        else
        {
          this.lblSelect.Visible = false;
          this.txtIDNumber.Text = "1";
          this.txtIDNumber.Visible = false;
          this.cmdEditName.Visible = false;
        }
        this.m_strComment = "";
        this.cmdEditName.Enabled = false;
        this.tsbNew.Enabled = false;
        this.tsbDelete.Enabled = false;
        this.tsbComments.Enabled = true;
        this.SetChanged(true);
        this.Refresh();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (CreateNew));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteItem()
    {
      try
      {
        if (Interaction.MsgBox(!this.m_bIsEquipment ? (object) "Delete the section details?" : (object) "Delete the selected equipment?", MsgBoxStyle.YesNo, (object) this.Text) != MsgBoxResult.Yes)
          return;
        string str = "SELECT * FROM SectionDetails WHERE [SD_ID]='" + Strings.Replace(Conversions.ToString(this.cboEquip.SelectedValue), "'", "''", 1, -1, CompareMethod.Binary) + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count == 0)
          throw new Exception("Unable to delete the section detail. Section detail record was not found.");
        if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["BRED_Status"], (object) "N", false))
          dataTable.Rows[0].Delete();
        else
          dataTable.Rows[0]["BRED_Status"] = (object) "D";
        mdUtility.DB.SaveDataTable(ref dataTable, str);
        this.m_strCurrentSec = "";
        this.SetChanged(false);
        this.GetEquipmentList();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteItem));
        ProjectData.ClearProjectError();
      }
    }

    private void SaveChanges()
    {
      try
      {
        if (!this.cboEquip.Visible & Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtIDNumber.Text, "", false) == 0 & this.m_bIsEquipment)
        {
          int num = (int) Interaction.MsgBox((object) "Please enter an equipment ID before saving.", MsgBoxStyle.Critical, (object) "Errors Encountered");
        }
        else
          this.SaveData();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (SaveChanges));
        ProjectData.ClearProjectError();
      }
    }

    private void SetChanged(bool aflag)
    {
      try
      {
        if (!this.m_bLoaded)
          return;
        this.tsbSave.Enabled = aflag;
        ToolStripButton tsbClose = this.tsbClose;
        if (aflag)
        {
          tsbClose.Text = "Cancel";
          tsbClose.ImageIndex = 0;
          tsbClose.ToolTipText = "Cancel changes";
        }
        else
        {
          tsbClose.Text = "Close";
          tsbClose.ImageIndex = 1;
          tsbClose.ToolTipText = "Close this window";
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (SetChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void SaveData()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        DataTable oDT;
        DataRow row;
        string uniqueId;
        if (this.m_bIsNew)
        {
          oDT = mdUtility.DB.GetTableSchema("SectionDetails");
          row = oDT.NewRow();
          uniqueId = mdUtility.GetUniqueID();
          row["SD_ID"] = (object) uniqueId;
          row["SD_Sec_ID"] = (object) this.m_strSection;
          this.m_strCurrentSec = uniqueId;
          oDT.Rows.Add(row);
        }
        else
        {
          oDT = mdUtility.DB.GetDataTable("SELECT * FROM [SectionDetails] WHERE [SD_ID]='" + Strings.Replace(this.m_strCurrentSec, "'", "''", 1, -1, CompareMethod.Binary) + "'");
          if (oDT.Rows.Count == 0)
            throw new Exception("Unable to save section details.  Section detail record was not found.");
          row = oDT.Rows[0];
          uniqueId = Conversions.ToString(row["SD_ID"]);
        }
        if (row != null)
        {
          if (this.m_bIsNew)
            row["BRED_Status"] = (object) "N";
          else if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["BRED_Status"], (object) "D", false))
            row["BRED_Status"] = (object) "U";
        }
        if (this.txtIDNumber.Visible)
          row[this.txtIDNumber.Tag.ToString()] = (object) this.txtIDNumber.Text;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtModel.Text, "", false) > 0U)
          row[this.txtModel.Tag.ToString()] = (object) this.txtModel.Text;
        else
          row[this.txtModel.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtWarrantyCo1.Text, "", false) > 0U)
          row[this.txtWarrantyCo1.Tag.ToString()] = (object) this.txtWarrantyCo1.Text;
        else
          row[this.txtWarrantyCo1.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtEquipmentMake.Text, "", false) > 0U)
          row[this.txtEquipmentMake.Tag.ToString()] = (object) this.txtEquipmentMake.Text;
        else
          row[this.txtEquipmentMake.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtControlTypeMake.Text, "", false) > 0U)
          row[this.txtControlTypeMake.Tag.ToString()] = (object) this.txtControlTypeMake.Text;
        else
          row[this.txtControlTypeMake.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtManufacturer.Text, "", false) > 0U)
          row[this.txtManufacturer.Tag.ToString()] = (object) this.txtManufacturer.Text;
        else
          row[this.txtManufacturer.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtCapacity.Text, "", false) > 0U)
          row[this.txtCapacity.Tag.ToString()] = (object) this.txtCapacity.Text;
        else
          row[this.txtCapacity.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSerialNumber.Text, "", false) > 0U)
          row[this.txtSerialNumber.Tag.ToString()] = (object) this.txtSerialNumber.Text;
        else
          row[this.txtSerialNumber.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtLocation.Text, "", false) > 0U)
          row[this.txtLocation.Tag.ToString()] = (object) this.txtLocation.Text;
        else
          row[this.txtLocation.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtWarrantyCo2.Text, "", false) > 0U)
          row[this.txtWarrantyCo2.Tag.ToString()] = (object) this.txtWarrantyCo2.Text;
        else
          row[this.txtWarrantyCo2.Tag.ToString()] = (object) DBNull.Value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtEquipmentType.Text, "", false) > 0U)
          row[this.txtEquipmentType.Tag.ToString()] = (object) this.txtEquipmentType.Text;
        else
          row[this.txtEquipmentType.Tag.ToString()] = (object) DBNull.Value;
        row["Date_Installed"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtDateInstalled.Text, "", false) <= 0U ? (object) DBNull.Value : (object) this.txtDateInstalled.Text;
        row["Comment"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_strComment, "", false) <= 0U ? (object) DBNull.Value : (object) this.m_strComment;
        row["Warranty_Date"] = !this.chkWarrantyDate1.Checked ? (object) DBNull.Value : (object) this.dtpWarrantyDate1.Value.ToShortDateString();
        row["Warranty_Date2"] = !this.chkWarrantyDate2.Checked ? (object) DBNull.Value : (object) this.dtpWarrantyDate2.Value.ToShortDateString();
        row["Date_Manufactured"] = !this.chkManufacturedDate.Checked ? (object) DBNull.Value : (object) this.dtpDateManufactured.Value.ToShortDateString();
        mdUtility.DB.SaveDataTable(ref oDT, "SELECT * FROM [SectionDetails] WHERE [SD_ID]='" + uniqueId + "'");
        this.SetChanged(false);
        this.m_bIsNew = false;
        this.GetEquipmentList();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (SaveData));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void FormatTexts()
    {
      try
      {
        DataTable tableSchema = mdUtility.DB.GetTableSchema("SectionDetails");
        this.txtIDNumber.MaxLength = tableSchema.Columns["ID_Number"].MaxLength;
        this.txtEquipmentType.MaxLength = tableSchema.Columns["Equipment_Type"].MaxLength;
        this.txtEquipmentMake.MaxLength = tableSchema.Columns["Equipment_Make"].MaxLength;
        this.txtSerialNumber.MaxLength = tableSchema.Columns["Serial_Number"].MaxLength;
        this.txtModel.MaxLength = tableSchema.Columns["Model"].MaxLength;
        this.txtCapacity.MaxLength = tableSchema.Columns["Capacity"].MaxLength;
        this.txtManufacturer.MaxLength = tableSchema.Columns["Manufacturer"].MaxLength;
        this.txtWarrantyCo1.MaxLength = tableSchema.Columns["Warranty_Company"].MaxLength;
        this.txtWarrantyCo2.MaxLength = tableSchema.Columns["Warranty_Company2"].MaxLength;
        this.txtLocation.MaxLength = tableSchema.Columns["Location"].MaxLength;
        this.txtControlTypeMake.MaxLength = tableSchema.Columns["Control_Type_Make"].MaxLength;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (FormatTexts));
        ProjectData.ClearProjectError();
      }
    }

    private void EditComment()
    {
      try
      {
        frmComment frmComment = new frmComment();
        string strCaption = Strings.InStr(1, this.Text, "Equipment", CompareMethod.Binary) <= 0 ? "Comment for " + Strings.Right(this.Text, checked (Strings.Len(this.Text) - Strings.InStr(1, this.Text, "for", CompareMethod.Binary) + 3)) : "Comment for " + this.cboEquip.SelectedText;
        if (frmComment.EditComment(strCaption, (object) this.m_strComment, !this.ReadOnly, (IWin32Window) null) != DialogResult.Yes || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_strComment, frmComment.Comment, false) <= 0U)
          return;
        this.m_strComment = frmComment.Comment;
        this.SetChanged(true);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (EditComment));
        ProjectData.ClearProjectError();
      }
    }

    private void frmSecDetails_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.tsCommands.Font = this.Font;
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "Section Details");
        this.FormatTexts();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void txtGPropVal_0_TextChanged(object sender, EventArgs e)
    {
      this.SetChanged(true);
    }

    private void txtDateInstalled_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtDateInstalled.Text, "", false) > 0U && !Versioned.IsNumeric((object) this.txtDateInstalled.Text))
        {
          int num = (int) Interaction.MsgBox((object) "Please enter a valid date in this field.", MsgBoxStyle.OkOnly, (object) null);
          this.txtDateInstalled.Focus();
          e.Cancel = true;
        }
        this.m_bInstalled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (txtDateInstalled_Validating));
        ProjectData.ClearProjectError();
      }
    }

    private void cboEquip_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bDDLoad)
        {
          string str = this.cboEquip.SelectedIndex == -1 ? this.m_strCurrentSec : Conversions.ToString(this.cboEquip.SelectedValue);
          if (!this.CheckForChanges())
            return;
          this.m_bLoaded = false;
          this.ClearControls();
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual((object) str, this.cboEquip.SelectedValue, false))
            this.cboEquip.SelectedValue = (object) str;
          DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM SectionDetails WHERE [SD_ID]='" + str + "'");
          if (dataTable.Rows.Count == 0)
            throw new Exception("Unable to load the section detials. Section details record not found.");
          DataRow row = dataTable.Rows[0];
          this.txtIDNumber.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtIDNumber.Tag.ToString()]), (object) ""));
          this.txtModel.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtModel.Tag.ToString()]), (object) ""));
          this.txtWarrantyCo1.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtWarrantyCo1.Tag.ToString()]), (object) ""));
          this.txtEquipmentMake.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtEquipmentMake.Tag.ToString()]), (object) ""));
          this.txtControlTypeMake.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtControlTypeMake.Tag.ToString()]), (object) ""));
          this.txtManufacturer.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtManufacturer.Tag.ToString()]), (object) ""));
          this.txtCapacity.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtCapacity.Tag.ToString()]), (object) ""));
          this.txtSerialNumber.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtSerialNumber.Tag.ToString()]), (object) ""));
          this.txtLocation.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtLocation.Tag.ToString()]), (object) ""));
          this.txtWarrantyCo2.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtWarrantyCo2.Tag.ToString()]), (object) ""));
          this.txtEquipmentType.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row[this.txtEquipmentType.Tag.ToString()]), (object) ""));
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["Warranty_Date"])))
          {
            this.dtpWarrantyDate1.Value = DateAndTime.Today;
            this.chkWarrantyDate1.Checked = false;
            this.dtpWarrantyDate1.Enabled = false;
          }
          else
          {
            this.dtpWarrantyDate1.Value = Conversions.ToDate(row["Warranty_Date"]);
            this.chkWarrantyDate1.Checked = true;
            this.dtpWarrantyDate1.Enabled = (1 & (!this.ReadOnly ? 1 : 0)) != 0;
          }
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["Warranty_Date2"])))
          {
            this.dtpWarrantyDate2.Value = DateAndTime.Today;
            this.chkWarrantyDate2.Checked = false;
            this.dtpWarrantyDate2.Enabled = false;
          }
          else
          {
            this.dtpWarrantyDate2.Value = Conversions.ToDate(row["Warranty_Date2"]);
            this.chkWarrantyDate2.Checked = true;
            this.dtpWarrantyDate2.Enabled = (1 & (!this.ReadOnly ? 1 : 0)) != 0;
          }
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["Date_Manufactured"])))
          {
            this.dtpDateManufactured.Value = DateAndTime.Today;
            this.chkManufacturedDate.Checked = false;
            this.dtpDateManufactured.Enabled = false;
          }
          else
          {
            this.dtpDateManufactured.Value = Conversions.ToDate(row["Date_Manufactured"]);
            this.chkManufacturedDate.Checked = true;
            this.dtpDateManufactured.Enabled = (1 & (!this.ReadOnly ? 1 : 0)) != 0;
          }
          this.txtDateInstalled.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["Date_Installed"]), (object) ""));
          this.m_strComment = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["Comment"]), (object) ""));
          this.m_strCurrentSec = Conversions.ToString(this.cboEquip.SelectedValue);
          this.m_bLoaded = true;
          this.FormatForRecords();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cboEquip_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cmdEditName_Click(object sender, EventArgs e)
    {
      this.cboEquip.Visible = false;
      this.lblSelect.Text = "Enter Equipment ID:";
      this.lblSelect.Visible = true;
      this.txtIDNumber.Visible = true;
      this.txtIDNumber.Focus();
      this.cmdEditName.Enabled = false;
      this.tsbNew.Enabled = false;
      this.SetChanged(true);
    }

    private bool NonTextDataIsValid(ref Control Cntl)
    {
      bool flag;
      try
      {
        flag = true;
        string name = Cntl.Name;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "txtWarrantyDate", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "txtWarrantyDate2", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "txtDateManufactured", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "txtDateInstalled", false) == 0)
          {
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Cntl.Text, "", false) > 0U)
            {
              if (!Versioned.IsNumeric((object) Cntl.Text))
              {
                int num = (int) Interaction.MsgBox((object) "Please enter a valid year for the year installed.", MsgBoxStyle.OkOnly, (object) null);
                flag = false;
              }
              else if (Conversions.ToDouble(Cntl.Text) <= 1900.0 & Conversions.ToDouble(Cntl.Text) < (double) checked (DateAndTime.Year(DateAndTime.Now) + 1))
              {
                int num = (int) Interaction.MsgBox((object) "Please enter a valid year for the year installed.", MsgBoxStyle.OkOnly, (object) null);
                flag = false;
              }
            }
          }
        }
        else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Cntl.Text, "", false) > 0U)
        {
          if (!Information.IsDate((object) Cntl.Text))
          {
            int num = (int) Interaction.MsgBox((object) "Please enter a valid date (mm/dd/yyyy) for warranty and manufactured dates.", MsgBoxStyle.OkOnly, (object) null);
            flag = false;
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (NonTextDataIsValid));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private void dtpDateManufactured_ValueChanged(object sender, EventArgs e)
    {
      this.SetChanged(true);
    }

    private void chkWarrantyDate1_CheckedChanged(object sender, EventArgs e)
    {
      this.dtpWarrantyDate1.Enabled = this.chkWarrantyDate1.Checked;
      this.SetChanged(true);
    }

    private void chkWarrantyDate2_CheckedChanged(object sender, EventArgs e)
    {
      this.dtpWarrantyDate2.Enabled = this.chkWarrantyDate2.Checked;
      this.SetChanged(true);
    }

    private void chkManufacturedDate_CheckedChanged(object sender, EventArgs e)
    {
      this.dtpDateManufactured.Enabled = this.chkManufacturedDate.Checked;
      this.SetChanged(true);
    }

    private void tsCommands_Click(object sender, EventArgs e)
    {
      ToolStripButton toolStripButton = (ToolStripButton) sender;
      bool flag;
      if (flag)
        return;
      string name = toolStripButton.Name;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, this.tsbClose.Name, false) == 0)
        this.CloseForm();
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, this.tsbSave.Name, false) == 0)
        this.SaveChanges();
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, this.tsbNew.Name, false) == 0)
        this.CreateNew();
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, this.tsbDelete.Name, false) == 0)
        this.DeleteItem();
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, this.tsbComments.Name, false) == 0)
        this.EditComment();
    }
  }
}
