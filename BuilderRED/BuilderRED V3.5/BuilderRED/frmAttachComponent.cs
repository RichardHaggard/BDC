// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmAttachComponent
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
  internal class frmAttachComponent : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmAttachComponent";
    private bool m_bUserCanceled;
    private string m_strComponentID;
    private DataTable dtComponents;

    public frmAttachComponent()
    {
      this.Load += new EventHandler(this.frmAttachComponent_Load);
      this.dtComponents = new DataTable();
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

    internal virtual ComboBox cboComponents
    {
      get
      {
        return this._cboComponents;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboComponents_Click);
        ComboBox cboComponents1 = this._cboComponents;
        if (cboComponents1 != null)
          cboComponents1.Click -= eventHandler;
        this._cboComponents = value;
        ComboBox cboComponents2 = this._cboComponents;
        if (cboComponents2 == null)
          return;
        cboComponents2.Click += eventHandler;
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
      this.cboComponents = new ComboBox();
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
      this.OKButton.ForeColor = SystemColors.ControlText;
      this.OKButton.Location = new Point(312, 8);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 25);
      this.OKButton.TabIndex = 0;
      this.OKButton.Text = "OK";
      this.OKButton.UseVisualStyleBackColor = false;
      this.cboComponents.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboComponents.Location = new Point(8, 16);
      this.cboComponents.Name = "cboComponents";
      this.cboComponents.Size = new Size(296, 21);
      this.cboComponents.TabIndex = 3;
      this.AcceptButton = (IButtonControl) this.OKButton;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.CancelButton = (IButtonControl) this.CancelButton_Renamed;
      this.ClientSize = new Size(400, 71);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cboComponents);
      this.Controls.Add((Control) this.CancelButton_Renamed);
      this.Controls.Add((Control) this.OKButton);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(528, 490);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmAttachComponent);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Please choose a component";
      this.ResumeLayout(false);
    }

    private void CancelButton_Renamed_Click(object eventSender, EventArgs eventArgs)
    {
      this.m_bUserCanceled = true;
      this.Close();
    }

    private void frmAttachComponent_Load(object eventSender, EventArgs eventArgs)
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
      this.HelpProvider.SetHelpKeyword((Control) this, "New Component");
      this.m_bUserCanceled = false;
    }

    public string GetComponent(string BuildingID, string Location_Renamed)
    {
      string str = Conversions.ToString(-1);
      try
      {
        this.dtComponents = mdUtility.DB.GetDataTable("SELECT System_Component.[SYS_COMP_ID], RO_Component.COMP_DESC FROM RO_Component INNER JOIN (Building_System INNER JOIN System_Component ON Building_System.[BLDG_SYS_ID] = System_Component.[SYS_COMP_BLDG_SYS_ID]) ON RO_Component.[COMP_ID] = System_Component.[SYS_COMP_COMP_LINK] WHERE (((System_Component.[SYS_COMP_ID]) Not In (SELECT [SYS_COMP_ID] FROM Components_by_location WHERE Components_by_location.[Facility_ID]={" + BuildingID + "} AND SAMP_DATA_LOC={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(Location_Renamed).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "})) AND ((Building_System.[BLDG_SYS_BLDG_ID])={" + BuildingID + "}) AND ((System_Component.BRED_Status)<>'D' Or (System_Component.BRED_Status) Is Null)) ORDER BY RO_Component.COMP_DESC;");
        if (this.dtComponents.Rows.Count == 0)
        {
          int num = (int) Interaction.MsgBox((object) "There are no components to attach.  Please switch\rto the inventory screen and add them.", MsgBoxStyle.OkOnly, (object) "Attach Components to Inspection");
          str = Conversions.ToString(-1);
        }
        else
        {
          UltraTreeNode nodeByKey = mdUtility.fMainForm.tvInspection.GetNodeByKey(Location_Renamed);
          if (nodeByKey.Nodes.Count > 0)
          {
            UltraTreeNode ultraTreeNode = nodeByKey.Nodes[0];
            int count = nodeByKey.Nodes.Count;
            int num = 1;
            while (num <= count)
            {
              if (ultraTreeNode.Tag != null)
              {
                DataView dataView = new DataView(this.dtComponents);
                dataView.RowFilter = "[sys_comp_id]= '" + Strings.Replace(ultraTreeNode.Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "'";
                while (dataView.Count > 0)
                  dataView.Delete(0);
                ultraTreeNode = ultraTreeNode.GetSibling(NodePosition.Next);
              }
              checked { ++num; }
            }
          }
          this.cboComponents.DisplayMember = "COMP_DESC";
          this.cboComponents.ValueMember = "SYS_COMP_ID";
          this.cboComponents.DataSource = (object) this.dtComponents;
          if (this.cboComponents.Items.Count == 0)
          {
            int num = (int) Interaction.MsgBox((object) "There are no components to attach.  Please switch\rto the inventory screen and add them.", MsgBoxStyle.OkOnly, (object) "Attach Components to Inspection");
            str = Conversions.ToString(-1);
          }
          else
          {
            int num = (int) this.ShowDialog();
            str = !this.m_bUserCanceled ? this.m_strComponentID : Conversions.ToString(-1);
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmAttachComponent), nameof (GetComponent));
        ProjectData.ClearProjectError();
      }
      return str;
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      this.m_strComponentID = this.cboComponents.SelectedValue.ToString();
      this.Close();
    }

    private void CheckForEnable()
    {
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.cboComponents.SelectedValue, (object) null, false))
          this.OKButton.Enabled = true;
        else
          this.OKButton.Enabled = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmAttachComponent), nameof (CheckForEnable));
        ProjectData.ClearProjectError();
      }
    }

    private void cboComponents_Click(object sender, EventArgs e)
    {
      this.CheckForEnable();
    }
  }
}
