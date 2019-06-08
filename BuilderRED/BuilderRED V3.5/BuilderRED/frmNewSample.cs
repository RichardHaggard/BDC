// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmNewSample
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
  internal class frmNewSample : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmNewSample";
    private string m_strLocID;
    private string m_strLocName;
    private bool m_bNewLocID;
    public string m_strCurrentInsp;
    public double dQty;

    public frmNewSample()
    {
      this.Load += new EventHandler(this.frmNewSample_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual GroupBox frmOption { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button CreateButton
    {
      get
      {
        return this._CreateButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.CreateButton_Click);
        Button createButton1 = this._CreateButton;
        if (createButton1 != null)
          createButton1.Click -= eventHandler;
        this._CreateButton = value;
        Button createButton2 = this._CreateButton;
        if (createButton2 == null)
          return;
        createButton2.Click += eventHandler;
      }
    }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboLocations
    {
      get
      {
        return this._cboLocations;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboLocations_SelectedIndexChanged);
        ComboBox cboLocations1 = this._cboLocations;
        if (cboLocations1 != null)
          cboLocations1.SelectedIndexChanged -= eventHandler;
        this._cboLocations = value;
        ComboBox cboLocations2 = this._cboLocations;
        if (cboLocations2 == null)
          return;
        cboLocations2.SelectedIndexChanged += eventHandler;
      }
    }

    public virtual Button CancelBN
    {
      get
      {
        return this._CancelBN;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.CancelBN_Click);
        Button cancelBn1 = this._CancelBN;
        if (cancelBn1 != null)
          cancelBn1.Click -= eventHandler;
        this._CancelBN = value;
        Button cancelBn2 = this._CancelBN;
        if (cancelBn2 == null)
          return;
        cancelBn2.Click += eventHandler;
      }
    }

    internal virtual RadioButton optNewLoc
    {
      get
      {
        return this._optNewLoc;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optExistingLoc_CheckedChanged);
        RadioButton optNewLoc1 = this._optNewLoc;
        if (optNewLoc1 != null)
          optNewLoc1.CheckedChanged -= eventHandler;
        this._optNewLoc = value;
        RadioButton optNewLoc2 = this._optNewLoc;
        if (optNewLoc2 == null)
          return;
        optNewLoc2.CheckedChanged += eventHandler;
      }
    }

    internal virtual RadioButton optExistingLoc
    {
      get
      {
        return this._optExistingLoc;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optExistingLoc_CheckedChanged);
        RadioButton optExistingLoc1 = this._optExistingLoc;
        if (optExistingLoc1 != null)
          optExistingLoc1.CheckedChanged -= eventHandler;
        this._optExistingLoc = value;
        RadioButton optExistingLoc2 = this._optExistingLoc;
        if (optExistingLoc2 == null)
          return;
        optExistingLoc2.CheckedChanged += eventHandler;
      }
    }

    internal virtual Label lblInstruct { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txt_1
    {
      get
      {
        return this._txt_1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txt_1_TextChanged);
        TextBox txt1_1 = this._txt_1;
        if (txt1_1 != null)
          txt1_1.TextChanged -= eventHandler;
        this._txt_1 = value;
        TextBox txt1_2 = this._txt_1;
        if (txt1_2 == null)
          return;
        txt1_2.TextChanged += eventHandler;
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.CancelBN = new Button();
      this.frmOption = new GroupBox();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.cboLocations = new ComboBox();
      this.optExistingLoc = new RadioButton();
      this.optNewLoc = new RadioButton();
      this.txt_1 = new TextBox();
      this.CreateButton = new Button();
      this.HelpProvider = new HelpProvider();
      this.lblInstruct = new Label();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.frmOption.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.CancelBN.BackColor = SystemColors.Control;
      this.CancelBN.Cursor = Cursors.Default;
      this.CancelBN.ForeColor = SystemColors.ControlText;
      this.CancelBN.Location = new Point(462, 34);
      this.CancelBN.Name = "CancelBN";
      this.CancelBN.RightToLeft = RightToLeft.No;
      this.CancelBN.Size = new Size(81, 25);
      this.CancelBN.TabIndex = 1;
      this.CancelBN.Text = "Cancel";
      this.CancelBN.UseVisualStyleBackColor = false;
      this.frmOption.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.frmOption.BackColor = SystemColors.Control;
      this.frmOption.Controls.Add((Control) this.TableLayoutPanel2);
      this.frmOption.ForeColor = SystemColors.ControlText;
      this.frmOption.Location = new Point(3, 34);
      this.frmOption.Name = "frmOption";
      this.frmOption.RightToLeft = RightToLeft.No;
      this.frmOption.Size = new Size(453, 78);
      this.frmOption.TabIndex = 2;
      this.frmOption.TabStop = false;
      this.TableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel2.ColumnCount = 2;
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.Controls.Add((Control) this.cboLocations, 1, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.optExistingLoc, 0, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.optNewLoc, 0, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.txt_1, 1, 0);
      this.TableLayoutPanel2.Location = new Point(6, 16);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 2;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.Size = new Size(441, 56);
      this.TableLayoutPanel2.TabIndex = 5;
      this.cboLocations.Dock = DockStyle.Fill;
      this.cboLocations.Location = new Point(202, 29);
      this.cboLocations.MaxDropDownItems = 15;
      this.cboLocations.Name = "cboLocations";
      this.cboLocations.Size = new Size(236, 21);
      this.cboLocations.TabIndex = 6;
      this.optExistingLoc.AutoSize = true;
      this.optExistingLoc.Dock = DockStyle.Fill;
      this.optExistingLoc.Location = new Point(3, 29);
      this.optExistingLoc.Name = "optExistingLoc";
      this.optExistingLoc.Size = new Size(193, 24);
      this.optExistingLoc.TabIndex = 9;
      this.optExistingLoc.Text = "Use the Selected Existing Location:";
      this.optNewLoc.AutoSize = true;
      this.optNewLoc.Checked = true;
      this.optNewLoc.Dock = DockStyle.Fill;
      this.optNewLoc.Location = new Point(3, 3);
      this.optNewLoc.Name = "optNewLoc";
      this.optNewLoc.Size = new Size(193, 20);
      this.optNewLoc.TabIndex = 8;
      this.optNewLoc.TabStop = true;
      this.optNewLoc.Text = "Create a New Location with Name:";
      this.txt_1.AcceptsReturn = true;
      this.txt_1.BackColor = SystemColors.Window;
      this.txt_1.Cursor = Cursors.IBeam;
      this.txt_1.Dock = DockStyle.Fill;
      this.txt_1.ForeColor = SystemColors.WindowText;
      this.txt_1.Location = new Point(202, 3);
      this.txt_1.MaxLength = 0;
      this.txt_1.Name = "txt_1";
      this.txt_1.Size = new Size(236, 20);
      this.txt_1.TabIndex = 10;
      this.CreateButton.BackColor = SystemColors.Control;
      this.CreateButton.Cursor = Cursors.Default;
      this.CreateButton.ForeColor = SystemColors.ControlText;
      this.CreateButton.Location = new Point(462, 3);
      this.CreateButton.Name = "CreateButton";
      this.CreateButton.RightToLeft = RightToLeft.No;
      this.CreateButton.Size = new Size(81, 25);
      this.CreateButton.TabIndex = 0;
      this.CreateButton.Text = "OK";
      this.CreateButton.UseVisualStyleBackColor = false;
      this.lblInstruct.AutoSize = true;
      this.lblInstruct.Location = new Point(3, 0);
      this.lblInstruct.Name = "lblInstruct";
      this.lblInstruct.Size = new Size(217, 13);
      this.lblInstruct.TabIndex = 3;
      this.lblInstruct.Text = "Select one of the following available options:";
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.CreateButton, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.frmOption, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.CancelBN, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblInstruct, 0, 0);
      this.TableLayoutPanel1.Location = new Point(12, 12);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(546, 115);
      this.TableLayoutPanel1.TabIndex = 4;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(570, 139);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(111, 129);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmNewSample);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Dialog Caption";
      this.frmOption.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void frmNewSample_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.CreateButton.Enabled = false;
        mdUtility.DB.GetTableSchema("Sample_Data");
        this.txt_1.MaxLength = 50;
        this.cboLocations.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void CancelBN_Click(object eventSender, EventArgs eventArgs)
    {
      this.m_strLocID = "";
      this.Close();
    }

    private void CreateButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (this.NewNameOK())
        {
          if (this.optExistingLoc.Checked)
          {
            this.m_strLocID = this.cboLocations.SelectedValue.ToString();
            this.m_strLocName = this.cboLocations.Text;
            this.m_bNewLocID = false;
          }
          else
          {
            this.m_strLocID = Sample.SampleLocationID(this.txt_1.Text, ref this.m_bNewLocID);
            this.m_strLocName = this.txt_1.Text;
          }
          this.Close();
        }
        else if (this.optNewLoc.Checked)
        {
          int num1 = (int) Interaction.MsgBox((object) "A location with this name already exists for the current building.\r\nPlease select another name or the option to use an existing location.", MsgBoxStyle.OkOnly, (object) "Problem encountered");
        }
        else
        {
          int num2 = (int) Interaction.MsgBox((object) "This inspection already has a sample at the selected location. Please select another location.", MsgBoxStyle.OkOnly, (object) "Problem encountered");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), nameof (CreateButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private bool NewNameOK()
    {
      bool flag;
      try
      {
        string sSQL;
        if (this.optNewLoc.Checked)
          sSQL = "Select Count(Name) FROM Sample_Location WHERE [Building_ID] = {" + mdUtility.fMainForm.CurrentBldg + "} AND [Name] = '" + Strings.Replace(this.txt_1.Text, "'", "''", 1, -1, CompareMethod.Binary) + "'";
        else
          sSQL = "SELECT Count([SAMP_DATA_LOC]) FROM Sample_Data WHERE [SAMP_DATA_INSP_DATA_ID] = {" + mdUtility.fMainForm.CurrentInspection + "} AND [SAMP_DATA_LOC] = {" + Strings.Replace(this.cboLocations.SelectedValue.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "}";
        flag = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(mdUtility.DB.GetDataTable(sSQL).Rows[0][0], (object) 0, false);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), nameof (NewNameOK));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private void CheckforCreateEnable()
    {
      try
      {
        bool flag = false;
        if (this.optNewLoc.Checked && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txt_1.Text, "", false) != 0 || this.optExistingLoc.Checked && this.cboLocations.SelectedIndex != -1)
          flag = true;
        this.CreateButton.Enabled = flag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), nameof (CheckforCreateEnable));
        ProjectData.ClearProjectError();
      }
    }

    public string CreateOne(ref string InspectionID)
    {
      string strLocId;
      try
      {
        this.m_strCurrentInsp = InspectionID;
        string str1 = Inspection.Section(ref InspectionID);
        this.SetUpForm("Create");
        this.dQty = Conversions.ToDouble(mdUtility.DB.GetDataTable("SELECT Sum(samp_data_qty) AS samp_data_qty FROM Sample_Data WHERE [samp_data_insp_data_id]={" + InspectionID + "}").Rows[0][0]);
        if (this.cboLocations.Items.Count > 0)
          this.cboLocations.SelectedIndex = 0;
        int num = (int) this.ShowDialog();
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_strLocID, "", false) > 0U)
        {
          Cursor.Current = Cursors.WaitCursor;
          DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT sec_paint, sec_qty FROM Component_Section WHERE [SEC_ID]={" + str1 + "}");
          string str2 = "SELECT * FROM Sample_Data WHERE [samp_data_insp_data_id]={" + InspectionID + "}";
          DataTable dataTable2 = mdUtility.DB.GetDataTable(str2);
          DataRow row = dataTable2.NewRow();
          DataRow dataRow = row;
          dataRow["BRED_Status"] = (object) "N";
          dataRow["samp_data_id"] = (object) mdUtility.GetUniqueID();
          dataRow["samp_data_insp_data_id"] = (object) InspectionID;
          dataRow["samp_data_loc"] = (object) this.m_strLocID;
          dataRow["samp_data_paint"] = RuntimeHelpers.GetObjectValue(dataTable1.Rows[0]["sec_paint"]);
          dataRow["samp_data_qty"] = Microsoft.VisualBasic.CompilerServices.Operators.SubtractObject(dataTable1.Rows[0]["sec_qty"], (object) this.dQty);
          dataTable2.Rows.Add(row);
          mdUtility.DB.SaveDataTable(ref dataTable2, str2);
          mdUtility.fMainForm.AddNewLocationToInspectionTree(Section.SectionComponentLink(str1), str1, this.m_strLocID, this.m_strLocName);
        }
        strLocId = this.m_strLocID;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), nameof (CreateOne));
        ProjectData.ClearProjectError();
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
      return strLocId;
    }

    public void EditSample(ref string SampleID)
    {
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT Name, SAMP_DATA_LOC, samp_data_insp_data_id FROM qrySampleList WHERE [samp_data_id]={" + SampleID + "}");
        if (dataTable.Rows.Count > 0)
        {
          this.m_strCurrentInsp = dataTable.Rows[0]["samp_data_insp_data_id"].ToString();
          this.txt_1.Text = Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["Name"]), (object) "")).ToLower(), "initial sample", false) != 0 ? Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["Name"]), (object) "")) : "";
          this.m_strLocID = Conversions.ToString(UtilityFunctions.FixDBNull((object) dataTable.Rows[0]["SAMP_DATA_LOC"].ToString(), (object) ""));
        }
        this.SetUpForm("Edit");
        int num = (int) this.ShowDialog();
        this.Cursor = Cursors.WaitCursor;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_strLocID, "", false) <= 0U)
          return;
        this.SaveSampleDataLocation(ref SampleID);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), nameof (EditSample));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void SaveSampleDataLocation(ref string SampleID)
    {
      try
      {
        string currentSectionId = mdUtility.fMainForm.CurrentSectionID;
        string strCompID = Section.SectionComponentLink(currentSectionId);
        string str1 = "SELECT * FROM Sample_Data WHERE [samp_data_id]={" + SampleID + "}";
        DataTable dataTable = mdUtility.DB.GetDataTable(str1);
        if (dataTable.Rows.Count <= 0)
          return;
        string str2 = Conversions.ToString(UtilityFunctions.FixDBNull((object) dataTable.Rows[0]["samp_data_loc"].ToString(), (object) ""));
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str2, this.m_strLocID, false) > 0U)
        {
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable.Rows[0]["BRED_Status"], (object) "N", false))
            dataTable.Rows[0]["BRED_Status"] = (object) "U";
          dataTable.Rows[0]["samp_data_loc"] = (object) this.m_strLocID;
          mdUtility.DB.SaveDataTable(ref dataTable, str1);
        }
        mdUtility.fMainForm.MoveSampleLocation(strCompID, currentSectionId, str2, this.m_strLocID, this.m_strLocName);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), "SaveLocation");
        ProjectData.ClearProjectError();
      }
    }

    private void cboLocations_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CheckforCreateEnable();
    }

    private void SetUpForm(string strMode)
    {
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strMode, "Create", false) == 0)
        {
          this.HelpProvider.SetHelpKeyword((Control) this, "Add a Sample Location");
          this.CreateButton.Enabled = true;
          this.ToolTip1.SetToolTip((Control) this.CreateButton, "Add a new sample");
          this.Text = "Adding a new sample...";
        }
        else
        {
          this.HelpProvider.SetHelpKeyword((Control) this, "Edit Sample Location Name");
          this.Text = "Editing selected sample location...";
          this.CreateButton.Text = "OK";
          this.ToolTip1.SetToolTip((Control) this.CreateButton, "Change sample location to another existing location");
        }
        string sSQL = "SELECT * FROM Sample_Location WHERE Building_ID = {" + mdUtility.fMainForm.CurrentBldg + "} and Location_ID Not In (Select [Location_ID] from qrySampleLocationsbyInspection where [InspID] = {" + Strings.Replace(this.m_strCurrentInsp, "'", "''", 1, -1, CompareMethod.Binary) + "}) ORDER BY Name";
        this.cboLocations.DisplayMember = "Name";
        this.cboLocations.ValueMember = "Location_ID";
        this.cboLocations.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        if (this.cboLocations.Items.Count != 0)
          return;
        this.optExistingLoc.Enabled = false;
        this.cboLocations.SelectedIndex = -1;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSample), nameof (SetUpForm));
        ProjectData.ClearProjectError();
      }
    }

    private void optExistingLoc_CheckedChanged(object sender, EventArgs e)
    {
      if (this.optExistingLoc.Checked)
      {
        this.txt_1.Enabled = false;
        this.txt_1.Text = "";
        this.cboLocations.Enabled = true;
      }
      else
      {
        this.txt_1.Enabled = true;
        this.cboLocations.Enabled = false;
      }
      this.CheckforCreateEnable();
    }

    private void txt_1_TextChanged(object sender, EventArgs e)
    {
      this.CheckforCreateEnable();
    }
  }
}
