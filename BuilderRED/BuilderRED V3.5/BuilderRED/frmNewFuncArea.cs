// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmNewFuncArea
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using BuilderRED.My;
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
  internal class frmNewFuncArea : Form
  {
    private IContainer components;
    private const string MOD_NAME = "frmNewFuncArea";

    public frmNewFuncArea()
    {
      this.Load += new EventHandler(this.frmNewFuncArea_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || this.components == null)
          return;
        this.components.Dispose();
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmNewFuncArea));
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.txtAreaName = new TextBox();
      this.lblAreaType = new Label();
      this.cboAreaType = new ComboBox();
      this.lblUseType = new Label();
      this.cboUseType = new ComboBox();
      this.txtAreaSize = new TextBox();
      this.lblAreaName = new Label();
      this.CancelButton_Renamed = new Button();
      this.OKButton = new Button();
      this.lblAreaSize = new Label();
      this.FlowLayoutPanel1 = new FlowLayoutPanel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.txtAreaName, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblAreaType, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboAreaType, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblUseType, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboUseType, 1, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtAreaSize, 1, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblAreaName, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.CancelButton_Renamed, 2, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.OKButton, 2, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblAreaSize, 0, 3);
      this.TableLayoutPanel1.Location = new Point(12, 13);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 4;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(444, 114);
      this.TableLayoutPanel1.TabIndex = 0;
      this.txtAreaName.Dock = DockStyle.Fill;
      this.txtAreaName.Location = new Point(72, 3);
      this.txtAreaName.Name = "txtAreaName";
      this.txtAreaName.Size = new Size(293, 20);
      this.txtAreaName.TabIndex = 1;
      this.lblAreaType.AutoSize = true;
      this.lblAreaType.Dock = DockStyle.Fill;
      this.lblAreaType.Location = new Point(3, 26);
      this.lblAreaType.Name = "lblAreaType";
      this.lblAreaType.Size = new Size(63, 27);
      this.lblAreaType.TabIndex = 2;
      this.lblAreaType.Text = "Area Type:";
      this.lblAreaType.TextAlign = ContentAlignment.MiddleRight;
      this.cboAreaType.Dock = DockStyle.Fill;
      this.cboAreaType.FormattingEnabled = true;
      this.cboAreaType.Location = new Point(72, 29);
      this.cboAreaType.Name = "cboAreaType";
      this.cboAreaType.Size = new Size(293, 21);
      this.cboAreaType.TabIndex = 3;
      this.lblUseType.AutoSize = true;
      this.lblUseType.Dock = DockStyle.Fill;
      this.lblUseType.Location = new Point(3, 53);
      this.lblUseType.Name = "lblUseType";
      this.lblUseType.Size = new Size(63, 27);
      this.lblUseType.TabIndex = 4;
      this.lblUseType.Text = "Use Type:";
      this.lblUseType.TextAlign = ContentAlignment.MiddleRight;
      this.cboUseType.Dock = DockStyle.Fill;
      this.cboUseType.FormattingEnabled = true;
      this.cboUseType.Location = new Point(72, 56);
      this.cboUseType.Name = "cboUseType";
      this.cboUseType.Size = new Size(293, 21);
      this.cboUseType.TabIndex = 5;
      this.txtAreaSize.Dock = DockStyle.Fill;
      this.txtAreaSize.Location = new Point(72, 83);
      this.txtAreaSize.Name = "txtAreaSize";
      this.txtAreaSize.Size = new Size(293, 20);
      this.txtAreaSize.TabIndex = 7;
      this.lblAreaName.AutoSize = true;
      this.lblAreaName.Dock = DockStyle.Fill;
      this.lblAreaName.Location = new Point(3, 0);
      this.lblAreaName.Name = "lblAreaName";
      this.lblAreaName.Size = new Size(63, 26);
      this.lblAreaName.TabIndex = 0;
      this.lblAreaName.Text = "Area Name:";
      this.lblAreaName.TextAlign = ContentAlignment.MiddleRight;
      this.CancelButton_Renamed.Dock = DockStyle.Fill;
      this.CancelButton_Renamed.Location = new Point(371, 29);
      this.CancelButton_Renamed.Name = "CancelButton_Renamed";
      this.CancelButton_Renamed.Size = new Size(70, 21);
      this.CancelButton_Renamed.TabIndex = 9;
      this.CancelButton_Renamed.Text = "Cancel";
      this.CancelButton_Renamed.UseVisualStyleBackColor = true;
      this.OKButton.Dock = DockStyle.Fill;
      this.OKButton.Enabled = false;
      this.OKButton.Location = new Point(371, 3);
      this.OKButton.Name = "OKButton";
      this.OKButton.Size = new Size(70, 20);
      this.OKButton.TabIndex = 8;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = true;
      this.lblAreaSize.AutoSize = true;
      this.lblAreaSize.Dock = DockStyle.Fill;
      this.lblAreaSize.Location = new Point(3, 80);
      this.lblAreaSize.Name = "lblAreaSize";
      this.lblAreaSize.Size = new Size(63, 34);
      this.lblAreaSize.TabIndex = 6;
      this.lblAreaSize.Text = "Size(*SF):";
      this.lblAreaSize.TextAlign = ContentAlignment.MiddleRight;
      this.FlowLayoutPanel1.Dock = DockStyle.Fill;
      this.FlowLayoutPanel1.Location = new Point(0, 0);
      this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
      this.FlowLayoutPanel1.Size = new Size(471, 145);
      this.FlowLayoutPanel1.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new Size(471, 145);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Controls.Add((Control) this.FlowLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmNewFuncArea);
      this.Text = "Add New Functional Area";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblAreaName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtAreaName
    {
      get
      {
        return this._txtAreaName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtAreaName_TextChanged);
        TextBox txtAreaName1 = this._txtAreaName;
        if (txtAreaName1 != null)
          txtAreaName1.TextChanged -= eventHandler;
        this._txtAreaName = value;
        TextBox txtAreaName2 = this._txtAreaName;
        if (txtAreaName2 == null)
          return;
        txtAreaName2.TextChanged += eventHandler;
      }
    }

    internal virtual Label lblAreaType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboAreaType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblUseType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboUseType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblAreaSize { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtAreaSize
    {
      get
      {
        return this._txtAreaSize;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtAreaSize_TextChanged);
        TextBox txtAreaSize1 = this._txtAreaSize;
        if (txtAreaSize1 != null)
          txtAreaSize1.TextChanged -= eventHandler;
        this._txtAreaSize = value;
        TextBox txtAreaSize2 = this._txtAreaSize;
        if (txtAreaSize2 == null)
          return;
        txtAreaSize2.TextChanged += eventHandler;
      }
    }

    internal virtual Button OKButton
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

    internal virtual Button CancelButton_Renamed
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

    internal virtual FlowLayoutPanel FlowLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void CancelButton_Renamed_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }

    private void frmNewFuncArea_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT ConfigValue FROM Configuration where [ConfigName] = 'Branch'");
        string sSQL;
        if (dataTable.Rows.Count > 0)
          sSQL = !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["ConfigValue"]), (object) ""), (object) "", false) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["ConfigValue"], (object) "Z", false) ? Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM RO_UseType WHERE Branch='", dataTable.Rows[0]["ConfigValue"]), (object) "' ORDER BY usetype_and_desc")) : "SELECT * FROM RO_UseType ORDER BY Usetype_And_Desc";
        this.cboUseType.ValueMember = "USETYPE_ID";
        this.cboUseType.DisplayMember = "USETYPE_AND_DESC";
        if (sSQL != null)
          this.cboUseType.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        this.cboAreaType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM RO_Functional_Usetype");
        this.cboAreaType.ValueMember = "ID";
        this.cboAreaType.DisplayMember = "Description";
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewFuncArea), nameof (frmNewFuncArea_Load));
        ProjectData.ClearProjectError();
      }
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (!Versioned.IsNumeric((object) this.txtAreaSize.Text) || Conversions.ToDouble(this.txtAreaSize.Text) <= 0.0)
        {
          int num1 = (int) Interaction.MsgBox((object) "Area size must be a positive number.", MsgBoxStyle.Critical, (object) null);
        }
        else
        {
          int integer1 = Conversions.ToInteger(this.cboAreaType.SelectedValue);
          int integer2 = Conversions.ToInteger(this.cboUseType.SelectedValue);
          double AreaSize = double.Parse(this.txtAreaSize.Text.ToString());
          string str = FunctionalArea.AddFuncArea(this.txtAreaName.Text, mdUtility.fMainForm.CurrentBldg, integer1, integer2, AreaSize, this.lblAreaSize.Text);
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Duplicate", false) == 0)
          {
            int num2 = (int) Interaction.MsgBox((object) "Functional Area Already Exists", MsgBoxStyle.Critical, (object) null);
            this.txtAreaName.Text = "";
            this.txtAreaName.Focus();
          }
          else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", false) > 0U)
          {
            MyProject.Forms.frmMain.tvFunctionality.ActiveNode = MyProject.Forms.frmMain.tvFunctionality.GetNodeByKey(str);
            MyProject.Forms.frmMain.tvFunctionality.Refresh();
            mdHierarchyFunction.LoadFunctionalityTree();
            this.Close();
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewFuncArea), nameof (OKButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void CheckForEnable()
    {
      try
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtAreaName.Text, "", false) > 0U & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtAreaSize.Text, "", false) > 0U)
          this.OKButton.Enabled = true;
        else
          this.OKButton.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewFuncArea), nameof (CheckForEnable));
        ProjectData.ClearProjectError();
      }
    }

    private void txtAreaName_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.CheckForEnable();
    }

    private void txtAreaSize_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.CheckForEnable();
    }

    public void AddFuncArea(bool unitOM)
    {
      try
      {
        this.cboAreaType.Refresh();
        this.cboUseType.Refresh();
        this.lblAreaSize.Text = unitOM ? "Size(*SF):" : "Size(*SM):";
        int num = (int) this.ShowDialog();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewFuncArea), nameof (AddFuncArea));
        ProjectData.ClearProjectError();
      }
    }
  }
}
