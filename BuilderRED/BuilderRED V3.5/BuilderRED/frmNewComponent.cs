// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmNewComponent
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
  internal class frmNewComponent : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmNewComponent";
    private string m_strSystemID;

    public frmNewComponent()
    {
      this.Load += new EventHandler(this.frmNewComponent_Load);
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

    internal virtual ComboBox cboComponents
    {
      get
      {
        return this._cboComponents;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboComponents_SelectedIndexChanged);
        ComboBox cboComponents1 = this._cboComponents;
        if (cboComponents1 != null)
          cboComponents1.SelectedIndexChanged -= eventHandler;
        this._cboComponents = value;
        ComboBox cboComponents2 = this._cboComponents;
        if (cboComponents2 == null)
          return;
        cboComponents2.SelectedIndexChanged += eventHandler;
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
      this.cboComponents = new ComboBox();
      this.HelpProvider = new HelpProvider();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.CancelButton_Renamed.BackColor = SystemColors.Control;
      this.CancelButton_Renamed.Cursor = Cursors.Default;
      this.CancelButton_Renamed.DialogResult = DialogResult.Cancel;
      this.CancelButton_Renamed.ForeColor = SystemColors.ControlText;
      this.CancelButton_Renamed.Location = new Point(294, 34);
      this.CancelButton_Renamed.Name = "CancelButton_Renamed";
      this.CancelButton_Renamed.RightToLeft = RightToLeft.No;
      this.CancelButton_Renamed.Size = new Size(81, 25);
      this.CancelButton_Renamed.TabIndex = 1;
      this.CancelButton_Renamed.Text = "Cancel";
      this.CancelButton_Renamed.UseVisualStyleBackColor = false;
      this.OKButton.BackColor = SystemColors.Control;
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.Enabled = false;
      this.OKButton.ForeColor = SystemColors.ControlText;
      this.OKButton.Location = new Point(294, 3);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 25);
      this.OKButton.TabIndex = 0;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = false;
      this.lblWarning.BackColor = SystemColors.Control;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblWarning, 2);
      this.lblWarning.Cursor = Cursors.Default;
      this.lblWarning.ForeColor = Color.Red;
      this.lblWarning.Location = new Point(3, 62);
      this.lblWarning.Name = "lblWarning";
      this.lblWarning.RightToLeft = RightToLeft.No;
      this.lblWarning.Size = new Size(320, 25);
      this.lblWarning.TabIndex = 4;
      this.lblWarning.Text = "There are no more components to be assigned to this system.";
      this.lblWarning.Visible = false;
      this.lblText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblText.BackColor = SystemColors.Control;
      this.lblText.Cursor = Cursors.Default;
      this.lblText.ForeColor = SystemColors.ControlText;
      this.lblText.Location = new Point(3, 0);
      this.lblText.Name = "lblText";
      this.lblText.RightToLeft = RightToLeft.No;
      this.lblText.Size = new Size(285, 31);
      this.lblText.TabIndex = 2;
      this.lblText.Text = "Please choose the type of the component you would like to add from the list below:";
      this.cboComponents.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cboComponents.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboComponents.Location = new Point(3, 34);
      this.cboComponents.Name = "cboComponents";
      this.cboComponents.Size = new Size(285, 21);
      this.cboComponents.TabIndex = 5;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblText, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cboComponents, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblWarning, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.OKButton, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.CancelButton_Renamed, 1, 1);
      this.TableLayoutPanel1.Location = new Point(12, 8);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(378, 91);
      this.TableLayoutPanel1.TabIndex = 6;
      this.AcceptButton = (IButtonControl) this.OKButton;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.AutoSize = true;
      this.BackColor = SystemColors.Control;
      this.CancelButton = (IButtonControl) this.CancelButton_Renamed;
      this.ClientSize = new Size(402, 111);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(178, 118);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmNewComponent);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add New Component";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
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
        string Left = Component.AddComponent(this.m_strSystemID, Conversions.ToInteger(this.cboComponents.SelectedValue));
        if ((uint) Operators.CompareString(Left, "", false) <= 0U)
          return;
        frmMain fMainForm = mdUtility.fMainForm;
        ref string local1 = ref this.m_strSystemID;
        ref string local2 = ref Left;
        ComboBox cboComponents;
        string text = (cboComponents = this.cboComponents).Text;
        ref string local3 = ref text;
        fMainForm.ComponentAdded(ref local1, ref local2, ref local3);
        cboComponents.Text = text;
        this.Close();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewComponent), nameof (OKButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    public void AddComponent(ref string SystemID)
    {
      try
      {
        this.m_strSystemID = SystemID;
        string sSQL;
        if (mdUtility.UseUniformat)
          sSQL = "SELECT RO_Component.[COMP_ID], RO_Component.COMP_DESC FROM Building_System INNER JOIN RO_Component ON Building_System.[bldg_sys_link] = RO_Component.[comp_sys_link] WHERE Building_System.[BLDG_SYS_ID]={" + mdUtility.fMainForm.CurrentSystem + "} AND [RO_Component].[IS_UII] = -1 and ([COMP_ID] NOT IN (SELECT [SYS_COMP_COMP_LINK] FROM System_Component WHERE [SYS_COMP_BLDG_SYS_ID]={" + mdUtility.fMainForm.CurrentSystem + "}) OR [comp_id] IN (SELECT [sys_comp_comp_link] FROM System_Component WHERE [bred_status]='D' AND [sys_comp_bldg_sys_id]={" + SystemID + "})) ORDER BY RO_Component.COMP_DESC";
        else
          sSQL = "SELECT RO_Component.[COMP_ID], RO_Component.COMP_DESC FROM Building_System INNER JOIN RO_Component ON Building_System.[bldg_sys_link] = RO_Component.[comp_sys_link] WHERE Building_System.[BLDG_SYS_ID]={" + mdUtility.fMainForm.CurrentSystem + "} AND [RO_Component].[IS_UII] = 0 and ([COMP_ID] NOT IN (SELECT [SYS_COMP_COMP_LINK] FROM System_Component WHERE [SYS_COMP_BLDG_SYS_ID]={" + mdUtility.fMainForm.CurrentSystem + "}) OR [comp_id] IN (SELECT [sys_comp_comp_link] FROM System_Component WHERE [bred_status]='D' AND [sys_comp_bldg_sys_id]={" + SystemID + "})) ORDER BY RO_Component.COMP_DESC";
        this.cboComponents.ValueMember = "COMP_ID";
        this.cboComponents.DisplayMember = "COMP_DESC";
        this.cboComponents.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        if (this.cboComponents.Items.Count == 0)
        {
          this.cboComponents.Enabled = false;
          this.lblWarning.Visible = true;
        }
        int num = (int) this.ShowDialog();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewComponent), nameof (AddComponent));
        ProjectData.ClearProjectError();
      }
    }

    private void CheckForm()
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "Add New Component");
        if (this.cboComponents.SelectedIndex != -1)
          this.OKButton.Enabled = true;
        else
          this.OKButton.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmNewComponent), nameof (CheckForm));
        ProjectData.ClearProjectError();
      }
    }

    private void cboComponents_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CheckForm();
    }

    private void frmNewComponent_Load(object sender, EventArgs e)
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
    }
  }
}
