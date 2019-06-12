// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgInspectSections
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
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
  internal class dlgInspectSections : Form
  {
    private IContainer components;
    public System.Windows.Forms.ToolTip ToolTip1;
    private const string MOD_NAME = "dlgInspectSections";
    private string m_strKey;
    private DataTable m_dtSections;

    public dlgInspectSections()
    {
      this.Load += new EventHandler(this.dlgInspectSections_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.HelpProvider = new HelpProvider();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.cmdCancel = new Button();
      this.cmdCopy = new Button();
      this.ugSectionList = new UltraGrid();
      this.TableLayoutPanel1.SuspendLayout();
      ((ISupportInitialize) this.ugSectionList).BeginInit();
      this.SuspendLayout();
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCancel, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCopy, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.ugSectionList, 0, 0);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(697, 213);
      this.TableLayoutPanel1.TabIndex = 6;
      this.cmdCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdCancel.BackColor = SystemColors.Control;
      this.cmdCancel.Cursor = Cursors.Default;
      this.cmdCancel.ForeColor = SystemColors.ControlText;
      this.cmdCancel.Location = new Point(264, 185);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.RightToLeft = RightToLeft.No;
      this.cmdCancel.Size = new Size(81, 25);
      this.cmdCancel.TabIndex = 8;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCopy.BackColor = SystemColors.Control;
      this.cmdCopy.Cursor = Cursors.Default;
      this.cmdCopy.ForeColor = SystemColors.ControlText;
      this.cmdCopy.Location = new Point(351, 185);
      this.cmdCopy.Name = "cmdCopy";
      this.cmdCopy.RightToLeft = RightToLeft.No;
      this.cmdCopy.Size = new Size(81, 25);
      this.cmdCopy.TabIndex = 7;
      this.cmdCopy.Text = "Copy";
      this.cmdCopy.UseVisualStyleBackColor = false;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.ugSectionList, 2);
      this.ugSectionList.Dock = DockStyle.Fill;
      this.ugSectionList.Location = new Point(3, 3);
      this.ugSectionList.Name = "ugSectionList";
      this.ugSectionList.Size = new Size(691, 176);
      this.ugSectionList.TabIndex = 6;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(697, 213);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.Location = new Point(70, (int) byte.MaxValue);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (dlgInspectSections);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Dialog Caption";
      this.TableLayoutPanel1.ResumeLayout(false);
      ((ISupportInitialize) this.ugSectionList).EndInit();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual UltraGrid ugSectionList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button cmdCopy
    {
      get
      {
        return this._cmdCopy;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCopy_Click);
        Button cmdCopy1 = this._cmdCopy;
        if (cmdCopy1 != null)
          cmdCopy1.Click -= eventHandler;
        this._cmdCopy = value;
        Button cmdCopy2 = this._cmdCopy;
        if (cmdCopy2 == null)
          return;
        cmdCopy2.Click += eventHandler;
      }
    }

    public virtual Button cmdCancel
    {
      get
      {
        return this._cmdCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCancel_Click);
        Button cmdCancel1 = this._cmdCancel;
        if (cmdCancel1 != null)
          cmdCancel1.Click -= eventHandler;
        this._cmdCancel = value;
        Button cmdCancel2 = this._cmdCancel;
        if (cmdCancel2 == null)
          return;
        cmdCancel2.Click += eventHandler;
      }
    }

    public void InspectSections(
      string BldgID,
      string sSampleID,
      string SampleKey,
      string SampleName)
    {
      try
      {
        this.m_strKey = SampleKey;
        this.Text = "Please select the sections to inspect at " + SampleName;
        this.m_dtSections = mdUtility.DB.GetDataTable("    SELECT DISTINCT S.[SYS_DESC]                                                          \t,S.[SYS_COMP_ID]                                                                    \t,S.[COMP_DESC]                                                                      \t,S.[SEC_ID]                                                                         \t,S.[SectionName] AS SectionName                                                     \t,0 AS [Copy]                                                                        FROM (                                                                                \tSELECT [SYS_DESC]                                                                   \t\t,[SYS_COMP_ID]                                                                  \t\t,[COMP_DESC]                                                                    \t\t,[SEC_ID]                                                                       \t\t,[SectionName]                                                                  \tFROM [qryInspectSections]                                                           \tWHERE [Facility_ID] = {" + BldgID + "}                                              \t) AS S                                                                              LEFT JOIN (                                                                           \tSELECT DISTINCT [SEC_ID]                                                            \tFROM [qryInspectSections]                                                           \tWHERE [Facility_ID] = {" + BldgID + "}                                              \t\tAND [Samp_Data_Loc] = {" + sSampleID + "}                                       \t) AS Exclude ON S.SEC_ID = Exclude.SEC_ID                                           WHERE EXCLUDE.SEC_ID IS NULL                                                      ");
        int index = checked (this.m_dtSections.Rows.Count - 1);
        while (index >= 0)
        {
          DataRow row = this.m_dtSections.Rows[index];
          if (this.SectionNodeAlreadyInTree(row["SYS_COMP_ID"].ToString(), row["SEC_ID"].ToString()))
            this.m_dtSections.Rows.RemoveAt(index);
          checked { index += -1; }
        }
        if (this.m_dtSections.Rows.Count > 0)
        {
          this.ugSectionList.DataSource = (object) this.m_dtSections;
          this.ugSectionList.DataBind();
          this.FormatGrid();
          int num = (int) this.ShowDialog();
        }
        else
        {
          int num1 = (int) Interaction.MsgBox((object) "There are no more sections available to inspect at this location.", MsgBoxStyle.OkOnly, (object) null);
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, nameof (dlgInspectSections), nameof (InspectSections));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.m_dtSections.Dispose();
      }
    }

    private void FormatGrid()
    {
      UltraGrid ugSectionList = this.ugSectionList;
      ugSectionList.DisplayLayout.Bands[0].Header.Appearance.TextHAlign = HAlign.Left;
      ugSectionList.DisplayLayout.Bands[0].Columns["SEC_ID"].Hidden = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["SEC_ID"].Width = 0;
      ugSectionList.DisplayLayout.Bands[0].Columns["SEC_ID"].LockedWidth = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_DESC"].Header.Caption = "System";
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_DESC"].CellAppearance.TextHAlign = HAlign.Left;
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_DESC"].CellActivation = Activation.NoEdit;
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_DESC"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(2000.0)));
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_DESC"].LockedWidth = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_COMP_ID"].Hidden = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_COMP_ID"].Width = 0;
      ugSectionList.DisplayLayout.Bands[0].Columns["SYS_COMP_ID"].LockedWidth = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["COMP_DESC"].Header.Caption = "Component";
      ugSectionList.DisplayLayout.Bands[0].Columns["COMP_DESC"].CellAppearance.TextHAlign = HAlign.Left;
      ugSectionList.DisplayLayout.Bands[0].Columns["COMP_DESC"].CellActivation = Activation.NoEdit;
      ugSectionList.DisplayLayout.Bands[0].Columns["COMP_DESC"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(2000.0)));
      ugSectionList.DisplayLayout.Bands[0].Columns["COMP_DESC"].LockedWidth = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["SectionName"].Header.Caption = "Section";
      ugSectionList.DisplayLayout.Bands[0].Columns["SectionName"].CellAppearance.TextHAlign = HAlign.Left;
      ugSectionList.DisplayLayout.Bands[0].Columns["SectionName"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(4900.0)));
      ugSectionList.DisplayLayout.Bands[0].Columns["SectionName"].LockedWidth = true;
      ugSectionList.DisplayLayout.Bands[0].Columns["Copy"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
      ugSectionList.DisplayLayout.Bands[0].Columns["Copy"].CellAppearance.TextHAlign = HAlign.Center;
      ugSectionList.DisplayLayout.Bands[0].Columns["Copy"].CellActivation = Activation.AllowEdit;
      ugSectionList.DisplayLayout.Bands[0].Columns["Copy"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(700.0)));
      ugSectionList.DisplayLayout.Bands[0].Columns["Copy"].LockedWidth = true;
    }

    private void cmdCancel_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }

    private void cmdCopy_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        foreach (UltraGridRow row in this.ugSectionList.Rows)
        {
          if (Conversions.ToBoolean(row.Cells["Copy"].Value))
          {
            string str = this.FindComponentKey(row.Cells["SYS_COMP_ID"].Value.ToString());
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", false) == 0)
              str = mdUtility.fMainForm.AttachComponent(this.m_strKey, row.Cells["SYS_COMP_ID"].Value.ToString(), row.Cells["COMP_DESC"].Value.ToString());
            mdUtility.fMainForm.AttachSection(str, row.Cells["SEC_ID"].Value.ToString(), row.Cells["SectionName"].Value.ToString());
          }
        }
        this.Close();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (dlgInspectSections), nameof (cmdCopy_Click));
        ProjectData.ClearProjectError();
      }
    }

    private string FindComponentKey(string ComponentID)
    {
      string str;
      try
      {
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(this.m_strKey) != null)
        {
          foreach (UltraTreeNode node in mdUtility.fMainForm.tvInspection.GetNodeByKey(this.m_strKey).Nodes)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Text, "Temp", false) == 0)
              node.Parent.ExpandAll(ExpandAllType.Always);
            else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Tag.ToString(), ComponentID, false) == 0)
            {
              str = node.Key;
              goto label_11;
            }
          }
        }
        str = "";
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (dlgInspectSections), nameof (FindComponentKey));
        str = "";
        ProjectData.ClearProjectError();
      }
label_11:
      return str;
    }

    private bool SectionNodeAlreadyInTree(string sCompID, string sSectID)
    {
      bool flag;
      if (mdUtility.fMainForm.tvInspection.GetNodeByKey(this.m_strKey) != null && mdUtility.fMainForm.tvInspection.GetNodeByKey(this.m_strKey).Nodes.Count > 0)
      {
        foreach (UltraTreeNode node1 in mdUtility.fMainForm.tvInspection.GetNodeByKey(this.m_strKey).Nodes)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node1.Text.ToLower(), "temp", false) == 0)
          {
            flag = false;
            goto label_14;
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node1.Tag.ToString(), sCompID, false) == 0)
          {
            foreach (UltraTreeNode node2 in node1.Nodes)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node2.Text.ToLower(), "temp", false) == 0)
              {
                flag = false;
                goto label_14;
              }
              else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node2.Tag.ToString(), sSectID, false) == 0)
              {
                flag = true;
                goto label_14;
              }
            }
          }
        }
      }
label_14:
      return flag;
    }

    private void dlgInspectSections_Load(object sender, EventArgs e)
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      try
      {
        foreach (Control control in this.Controls)
        {
          this.HelpProvider.SetHelpNavigator(control, HelpNavigator.KeywordIndex);
          this.HelpProvider.SetHelpKeyword(control, "Introduction to BuilderRED");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }
  }
}
