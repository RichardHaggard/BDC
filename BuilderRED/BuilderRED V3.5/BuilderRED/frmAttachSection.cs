// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmAttachSection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinTree;
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
  internal class frmAttachSection : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmAttachSection";
    private bool m_bUserCanceled;
    private string m_strSectionID;
    private DataTable m_dtSections;

    public frmAttachSection()
    {
      this.Load += new EventHandler(this.frmAttachSection_Load);
      this.m_dtSections = new DataTable();
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

    internal virtual ComboBox cboSections
    {
      get
      {
        return this._cboSections;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboSections_SelectedIndexChanged);
        ComboBox cboSections1 = this._cboSections;
        if (cboSections1 != null)
          cboSections1.SelectedIndexChanged -= eventHandler;
        this._cboSections = value;
        ComboBox cboSections2 = this._cboSections;
        if (cboSections2 == null)
          return;
        cboSections2.SelectedIndexChanged += eventHandler;
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
      this.cboSections = new ComboBox();
      this.HelpProvider = new HelpProvider();
      this.SuspendLayout();
      this.CancelButton_Renamed.BackColor = SystemColors.Control;
      this.CancelButton_Renamed.Cursor = Cursors.Default;
      this.CancelButton_Renamed.DialogResult = DialogResult.Cancel;
      this.CancelButton_Renamed.ForeColor = SystemColors.ControlText;
      this.CancelButton_Renamed.Location = new Point(312, 40);
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
      this.OKButton.Location = new Point(312, 8);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 25);
      this.OKButton.TabIndex = 0;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = false;
      this.cboSections.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSections.Location = new Point(8, 16);
      this.cboSections.Name = "cboSections";
      this.cboSections.Size = new Size(296, 21);
      this.cboSections.TabIndex = 4;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(398, 69);
      this.Controls.Add((Control) this.cboSections);
      this.Controls.Add((Control) this.CancelButton_Renamed);
      this.Controls.Add((Control) this.OKButton);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(192, 384);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmAttachSection);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Please choose a section";
      this.ResumeLayout(false);
    }

    private void CancelButton_Renamed_Click(object eventSender, EventArgs eventArgs)
    {
      this.m_bUserCanceled = true;
      this.Close();
    }

    private void frmAttachSection_Load(object eventSender, EventArgs eventArgs)
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
      this.HelpProvider.SetHelpKeyword((Control) this, "New Section");
      this.m_bUserCanceled = false;
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      this.m_strSectionID = this.cboSections.SelectedValue.ToString();
      this.Close();
    }

    public string GetSection(string strComponentID, string SampleLocation)
    {
      string str = Conversions.ToString(-1);
      try
      {
        this.m_dtSections = mdUtility.DB.GetDataTable("SELECT [Section Info].[SEC_ID], [Section Info].SectionName, [Section Info].[SEC_SYS_COMP_ID] FROM [Section Info] WHERE ((([Section Info].[SEC_ID]) Not In (SELECT [SEC_ID] FROM Sections_by_location WHERE Sections_by_location.[SEC_SYS_COMP_ID]={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(strComponentID).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "} AND Samp_Data_Loc={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(SampleLocation).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "})) AND (([Section Info].[SEC_SYS_COMP_ID])={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(strComponentID).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "}) AND (([Section Info].BRED_Status)<>'D' Or ([Section Info].BRED_Status) Is Null))");
        if (this.m_dtSections.Rows.Count == 0)
        {
          int num = (int) Interaction.MsgBox((object) "There are no sections to attach.  Please switch \r\nto the Inventory screen and add them.", MsgBoxStyle.OkOnly, (object) "Attach Section to Inspection");
          str = "-1";
        }
        else
        {
          UltraTreeNode nodeByKey = mdUtility.fMainForm.tvInspection.GetNodeByKey(strComponentID);
          if (nodeByKey.Nodes.Count > 0)
          {
            UltraTreeNode ultraTreeNode = nodeByKey.Nodes[0];
            int num = checked (nodeByKey.Nodes.Count - 1);
            int index1 = 0;
            while (index1 <= num)
            {
              if (nodeByKey.Nodes[index1].Tag != null)
              {
                string Right = nodeByKey.Nodes[index1].Tag.ToString();
                int index2 = checked (this.m_dtSections.Rows.Count - 1);
                while (index2 >= 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_dtSections.Rows[index2]["SEC_ID"].ToString(), Right, false) == 0)
                    this.m_dtSections.Rows.Remove(this.m_dtSections.Rows[index2]);
                  checked { index2 += -1; }
                }
                ultraTreeNode = ultraTreeNode.GetSibling(NodePosition.Next);
              }
              checked { ++index1; }
            }
          }
          if (this.m_dtSections.Rows.Count == 0)
          {
            int num = (int) Interaction.MsgBox((object) "There are no sections to attach.  Please switch \r\nto the Inventory screen and add them.", MsgBoxStyle.OkOnly, (object) "Attach Section to Inspection");
            str = "-1";
          }
          else
          {
            this.cboSections.DisplayMember = "SectionName";
            this.cboSections.ValueMember = "SEC_ID";
            this.cboSections.DataSource = (object) this.m_dtSections;
            int num = (int) this.ShowDialog();
            str = !this.m_bUserCanceled ? this.m_strSectionID : Conversions.ToString(-1);
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmAttachSection), nameof (GetSection));
        ProjectData.ClearProjectError();
      }
      return str;
    }

    private void CheckForEnable()
    {
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.cboSections.SelectedValue, (object) null, false))
          this.OKButton.Enabled = true;
        else
          this.OKButton.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmAttachSection), nameof (CheckForEnable));
        ProjectData.ClearProjectError();
      }
    }

    private void cboSections_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CheckForEnable();
    }
  }
}
