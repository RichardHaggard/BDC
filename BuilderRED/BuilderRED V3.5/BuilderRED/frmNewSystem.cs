// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmNewSystem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmNewSystem : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmNewSystem";
    private string m_strBldgID;

    public frmNewSystem()
    {
      this.Load += new EventHandler(this.frmNewSystem_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
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

    public virtual Label lblWarning { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboSystems
    {
      get
      {
        return this._cboSystems;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboSystems_SelectedIndexChanged);
        ComboBox cboSystems1 = this._cboSystems;
        if (cboSystems1 != null)
          cboSystems1.SelectedIndexChanged -= eventHandler;
        this._cboSystems = value;
        ComboBox cboSystems2 = this._cboSystems;
        if (cboSystems2 == null)
          return;
        cboSystems2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.CancelButton_Renamed = new Button();
      this.OKButton = new Button();
      this.lblWarning = new Label();
      this.lblText = new Label();
      this.cboSystems = new ComboBox();
      this.HelpProvider = new HelpProvider();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.CancelButton_Renamed.AutoSize = true;
      this.CancelButton_Renamed.BackColor = SystemColors.Control;
      this.CancelButton_Renamed.Cursor = Cursors.Default;
      this.CancelButton_Renamed.DialogResult = DialogResult.Cancel;
      this.CancelButton_Renamed.Dock = DockStyle.Fill;
      this.CancelButton_Renamed.ForeColor = SystemColors.ControlText;
      this.CancelButton_Renamed.Location = new Point(318, 32);
      this.CancelButton_Renamed.Name = "CancelButton_Renamed";
      this.CancelButton_Renamed.RightToLeft = RightToLeft.No;
      this.CancelButton_Renamed.Size = new Size(81, 25);
      this.CancelButton_Renamed.TabIndex = 1;
      this.CancelButton_Renamed.Text = "Cancel";
      this.CancelButton_Renamed.UseVisualStyleBackColor = false;
      this.OKButton.AutoSize = true;
      this.OKButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.OKButton.BackColor = SystemColors.Control;
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.Dock = DockStyle.Fill;
      this.OKButton.Enabled = false;
      this.OKButton.ForeColor = SystemColors.ControlText;
      this.OKButton.Location = new Point(318, 3);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 23);
      this.OKButton.TabIndex = 0;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = false;
      this.lblWarning.AutoSize = true;
      this.lblWarning.BackColor = SystemColors.Control;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblWarning, 2);
      this.lblWarning.Cursor = Cursors.Default;
      this.lblWarning.Dock = DockStyle.Fill;
      this.lblWarning.ForeColor = Color.Red;
      this.lblWarning.Location = new Point(3, 60);
      this.lblWarning.Name = "lblWarning";
      this.lblWarning.RightToLeft = RightToLeft.No;
      this.lblWarning.Size = new Size(396, 47);
      this.lblWarning.TabIndex = 3;
      this.lblWarning.Text = "There are no more systems to be assigned to this building.";
      this.lblWarning.Visible = false;
      this.lblText.AutoSize = true;
      this.lblText.BackColor = SystemColors.Control;
      this.lblText.Cursor = Cursors.Default;
      this.lblText.Dock = DockStyle.Fill;
      this.lblText.ForeColor = SystemColors.ControlText;
      this.lblText.Location = new Point(3, 0);
      this.lblText.Name = "lblText";
      this.lblText.RightToLeft = RightToLeft.No;
      this.lblText.Size = new Size(309, 29);
      this.lblText.TabIndex = 2;
      this.lblText.Text = "Please choose the type of the new system you would like to add from the list box below.";
      this.cboSystems.Dock = DockStyle.Fill;
      this.cboSystems.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSystems.Location = new Point(3, 32);
      this.cboSystems.Name = "cboSystems";
      this.cboSystems.Size = new Size(309, 21);
      this.cboSystems.TabIndex = 4;
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblWarning, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboSystems, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.CancelButton_Renamed, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.OKButton, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblText, 0, 0);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(402, 107);
      this.TableLayoutPanel1.TabIndex = 5;
      this.AcceptButton = (IButtonControl) this.OKButton;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackColor = SystemColors.Control;
      this.CancelButton = (IButtonControl) this.CancelButton_Renamed;
      this.ClientSize = new Size(402, 107);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(184, 250);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmNewSystem);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add New System";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void CancelButton_Renamed_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        string Left = BuildingSystem.AddSystem(this.m_strBldgID, Conversions.ToInteger(this.cboSystems.SelectedValue));
        if ((uint) Operators.CompareString(Left, "", false) <= 0U)
          return;
        frmMain fMainForm = mdUtility.fMainForm;
        ref string local1 = ref this.m_strBldgID;
        ref string local2 = ref Left;
        string text = this.cboSystems.Text;
        ref string local3 = ref text;
        fMainForm.SystemAdded(ref local1, ref local2, ref local3);
        this.Close();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSystem), nameof (OKButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    public void AddSystem(ref string BldgID)
    {
      try
      {
        this.m_strBldgID = BldgID;
        string sSQL;
        if (mdUtility.UseUniformat)
          sSQL = "Select SYS_ID, SYS_DESC FROM RO_System WHERE IS_UII = -1 and SYS_ID NOT IN (SELECT [BLDG_SYS_LINK] FROM Building_System WHERE [BLDG_SYS_BLDG_ID]='" + this.m_strBldgID + "') OR SYS_ID IN (SELECT [BLDG_SYS_LINK] FROM Building_System WHERE [BLDG_SYS_BLDG_ID]='" + this.m_strBldgID + "' AND BRED_STATUS='D')";
        else
          sSQL = "Select SYS_ID, SYS_DESC FROM RO_System WHERE IS_UII = 0 and SYS_ID NOT IN (SELECT [BLDG_SYS_LINK] FROM Building_System WHERE [BLDG_SYS_BLDG_ID]='" + this.m_strBldgID + "') OR SYS_ID IN (SELECT [BLDG_SYS_LINK] FROM Building_System WHERE [BLDG_SYS_BLDG_ID]='" + this.m_strBldgID + "' AND BRED_STATUS='D')";
        this.cboSystems.DisplayMember = "SYS_DESC";
        this.cboSystems.ValueMember = "SYS_ID";
        this.cboSystems.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        this.cboSystems.Refresh();
        if (this.cboSystems.Items.Count == 0)
        {
          this.cboSystems.Enabled = false;
          this.lblWarning.Visible = true;
          this.OKButton.Enabled = false;
        }
        int num = (int) this.ShowDialog();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSystem), nameof (AddSystem));
        ProjectData.ClearProjectError();
      }
    }

    private void CheckForEnable()
    {
      try
      {
        if (this.cboSystems.SelectedIndex != -1)
          this.OKButton.Enabled = true;
        else
          this.OKButton.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewSystem), nameof (CheckForEnable));
        ProjectData.ClearProjectError();
      }
    }

    private void cboSystems_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CheckForEnable();
    }

    private void frmNewSystem_Load(object sender, EventArgs e)
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
      this.HelpProvider.SetHelpKeyword((Control) this, "Add New System");
    }
  }
}
