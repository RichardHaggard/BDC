// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgOtherDistresses
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
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
  internal class dlgOtherDistresses : Form
  {
    private IContainer components;
    public System.Windows.Forms.ToolTip ToolTip1;
    private const string MOD_NAME = "dlgOtherDistresses";
    private frmDistresses frmDistressList;
    private int m_lSubCompLink;
    private bool m_bButtonFlag;
    private DataTable m_dtDistresses;

    public dlgOtherDistresses()
    {
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    internal virtual PrintDialog PrintDialog1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual UltraGrid ugDistresses { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

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

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.cmdCancel = new Button();
      this.cmdCopy = new Button();
      this.PrintDialog1 = new PrintDialog();
      this.ugDistresses = new UltraGrid();
      this.HelpProvider = new HelpProvider();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      ((ISupportInitialize) this.ugDistresses).BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.cmdCancel.BackColor = SystemColors.Control;
      this.cmdCancel.Cursor = Cursors.Default;
      this.cmdCancel.ForeColor = SystemColors.ControlText;
      this.cmdCancel.Location = new Point(220, 179);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.RightToLeft = RightToLeft.No;
      this.cmdCancel.Size = new Size(81, 25);
      this.cmdCancel.TabIndex = 1;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = false;
      this.cmdCopy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdCopy.BackColor = SystemColors.Control;
      this.cmdCopy.Cursor = Cursors.Default;
      this.cmdCopy.ForeColor = SystemColors.ControlText;
      this.cmdCopy.Location = new Point(133, 179);
      this.cmdCopy.Name = "cmdCopy";
      this.cmdCopy.RightToLeft = RightToLeft.No;
      this.cmdCopy.Size = new Size(81, 25);
      this.cmdCopy.TabIndex = 0;
      this.cmdCopy.Text = "Copy";
      this.cmdCopy.UseVisualStyleBackColor = false;
      this.ugDistresses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.ugDistresses, 2);
      this.ugDistresses.Location = new Point(3, 3);
      this.ugDistresses.Name = "ugDistresses";
      this.ugDistresses.Size = new Size(428, 170);
      this.ugDistresses.TabIndex = 4;
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCancel, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.ugDistresses, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCopy, 0, 1);
      this.TableLayoutPanel1.Location = new Point(12, 12);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(434, 207);
      this.TableLayoutPanel1.TabIndex = 5;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(458, 231);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.Location = new Point(184, 250);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (dlgOtherDistresses);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Dialog Caption";
      ((ISupportInitialize) this.ugDistresses).EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public bool ShowLike(ref frmDistresses Distresses, ref int Subcomp, ref string ExistingList)
    {
      bool flag;
      try
      {
        this.Text = "Previous Inspection Distresses";
        this.frmDistressList = Distresses;
        this.m_lSubCompLink = Subcomp;
        flag = false;
        string sSQL;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ExistingList, "", false) > 0U)
        {
          if (mdUtility.Units == mdUtility.SystemofMeasure.umMetric)
            sSQL = "SELECT DISTINCT [SAMP_SUBCOMP_CMC_SUBCOMP_LINK], [DIST_DENS_ID],[DIST_ID], DIST_DESC, [DIST_SEV_SEV], Severity_DESC, MET_SUBCOMP_QTY AS [Subcomponent Qty], MET_DISTRESS_QTY AS [Distress Qty], [DIST_DENS_DENS], Density_DESC, Copy FROM qrySubcomponentDistresses WHERE [SAMP_SUBCOMP_CMC_SUBCOMP_LINK]=" + Conversions.ToString(Subcomp) + " AND [DIST_DENS_ID] NOT IN(" + ExistingList + ")";
          else
            sSQL = "SELECT DISTINCT [SAMP_SUBCOMP_CMC_SUBCOMP_LINK], [DIST_DENS_ID],[DIST_ID], DIST_DESC, [DIST_SEV_SEV], Severity_DESC, ENG_SUBCOMP_QTY AS [Subcomponent Qty], ENG_DISTRESS_QTY AS [Distress Qty], [DIST_DENS_DENS], Density_DESC, Copy FROM qrySubcomponentDistresses WHERE [SAMP_SUBCOMP_CMC_SUBCOMP_LINK]=" + Conversions.ToString(Subcomp) + " AND [DIST_DENS_ID] NOT IN(" + ExistingList + ")";
        }
        else
          sSQL = mdUtility.Units != mdUtility.SystemofMeasure.umMetric ? "SELECT DISTINCT [SAMP_SUBCOMP_CMC_SUBCOMP_LINK], [DIST_DENS_ID],[DIST_ID], DIST_DESC, [DIST_SEV_SEV], Severity_DESC, ENG_SUBCOMP_QTY AS [Subcomponent Qty], ENG_DISTRESS_QTY AS [Distress Qty], [DIST_DENS_DENS], Density_DESC, Copy FROM qrySubcomponentDistresses WHERE [SAMP_SUBCOMP_CMC_SUBCOMP_LINK]=" + Conversions.ToString(Subcomp) : "SELECT DISTINCT [SAMP_SUBCOMP_CMC_SUBCOMP_LINK], [DIST_DENS_ID],[DIST_ID], DIST_DESC, [DIST_SEV_SEV], Severity_DESC, MET_SUBCOMP_QTY AS [Subcomponent Qty], MET_DISTRESS_QTY AS [Distress Qty], [DIST_DENS_DENS], Density_DESC, Copy FROM qrySubcomponentDistresses WHERE [SAMP_SUBCOMP_CMC_SUBCOMP_LINK]=" + Conversions.ToString(Subcomp);
        this.m_dtDistresses = mdUtility.DB.GetDataTable(sSQL);
        if (this.m_dtDistresses.Rows.Count > 0)
        {
          this.ugDistresses.DataSource = (object) this.m_dtDistresses;
          this.ugDistresses.DataBind();
          this.FormatGrid();
          int num = (int) this.ShowDialog();
        }
        else
        {
          int num1 = (int) Interaction.MsgBox((object) "No other Distresses found for this Subcomponent.", MsgBoxStyle.OkOnly, (object) null);
        }
        flag = this.m_bButtonFlag;
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, nameof (dlgOtherDistresses), nameof (ShowLike));
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private void cmdCancel_Click(object eventSender, EventArgs eventArgs)
    {
      this.m_bButtonFlag = false;
      this.Close();
    }

    private void cmdCopy_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        int num = checked (this.m_dtDistresses.Rows.Count - 1);
        int index = 0;
        while (index <= num)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.m_dtDistresses.Rows[index]["Copy"], (object) true, false) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.m_dtDistresses.Rows[index]["Copy"], (object) 1, false))
          {
            DataTable dataTable = this.frmDistressList.dtDistresses;
            DataRow row = this.frmDistressList.dtDistresses.NewRow();
            row["BRED_Status"] = (object) "A";
            row["SubcompLink"] = (object) this.m_lSubCompLink;
            row["Distress"] = RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["DIST_ID"]);
            row["Severity"] = RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["DIST_SEV_SEV"]);
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["Subcomponent Qty"])))
              row["Subcomponent Qty"] = RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["Subcomponent Qty"]);
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["Distress Qty"])))
              row["Distress Qty"] = RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["Distress Qty"]);
            row["Density"] = RuntimeHelpers.GetObjectValue(this.m_dtDistresses.Rows[index]["DIST_DENS_DENS"]);
            row["Critical"] = (object) false;
            row["ESC"] = (object) false;
            this.frmDistressList.dtDistresses.Rows.Add(row);
            dataTable = (DataTable) null;
          }
          checked { ++index; }
        }
        this.m_bButtonFlag = true;
        this.Close();
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, nameof (dlgOtherDistresses), nameof (cmdCopy_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void FormatGrid()
    {
      UltraGrid ugDistresses = this.ugDistresses;
      ugDistresses.DisplayLayout.Bands[0].Header.Appearance.TextHAlign = HAlign.Center;
      ugDistresses.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CMC_SUBCOMP_LINK"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CMC_SUBCOMP_LINK"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CMC_SUBCOMP_LINK"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DENS_ID"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DENS_ID"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DENS_ID"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_ID"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_ID"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_ID"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DESC"].Header.Caption = "Distress";
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DESC"].CellActivation = Activation.NoEdit;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DESC"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(2500.0)));
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DESC"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_SEV_SEV"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_SEV_SEV"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_SEV_SEV"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Severity_Desc"].Header.Caption = "Severity";
      ugDistresses.DisplayLayout.Bands[0].Columns["Severity_Desc"].CellActivation = Activation.NoEdit;
      ugDistresses.DisplayLayout.Bands[0].Columns["Severity_Desc"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Subcomponent Qty"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Subcomponent Qty"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["Subcomponent Qty"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Distress Qty"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Distress Qty"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["Distress Qty"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DENS_DENS"].Hidden = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DENS_DENS"].Width = 0;
      ugDistresses.DisplayLayout.Bands[0].Columns["DIST_DENS_DENS"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Density_Desc"].Header.Caption = "Density";
      ugDistresses.DisplayLayout.Bands[0].Columns["Density_Desc"].CellActivation = Activation.NoEdit;
      ugDistresses.DisplayLayout.Bands[0].Columns["Density_Desc"].LockedWidth = true;
      ugDistresses.DisplayLayout.Bands[0].Columns["Copy"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
      ugDistresses.DisplayLayout.Bands[0].Columns["Copy"].CellAppearance.TextHAlign = HAlign.Center;
      ugDistresses.DisplayLayout.Bands[0].Columns["Copy"].CellActivation = Activation.AllowEdit;
      ugDistresses.DisplayLayout.Bands[0].Columns["Copy"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(600.0)));
      ugDistresses.DisplayLayout.Bands[0].Columns["Copy"].LockedWidth = true;
    }
  }
}
