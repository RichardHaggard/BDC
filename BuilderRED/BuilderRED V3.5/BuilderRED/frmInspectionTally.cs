// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmInspectionTally
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
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
  internal class frmInspectionTally : Form
  {
    private IContainer components;
    public System.Windows.Forms.ToolTip ToolTip1;
    private const string MOD_NAME = "frmInspectionTally";
    private DataTable m_dtSections;

    public frmInspectionTally()
    {
      this.Load += new EventHandler(this.frmInspectionTally_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
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

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.OKButton = new Button();
      this.ugTally = new UltraGrid();
      this.HelpProvider = new HelpProvider();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      ((ISupportInitialize) this.ugTally).BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.OKButton.BackColor = SystemColors.Control;
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.ForeColor = SystemColors.ControlText;
      this.OKButton.Location = new Point(316, 387);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 25);
      this.OKButton.TabIndex = 0;
      this.OKButton.Text = "Exit";
      this.OKButton.UseVisualStyleBackColor = false;
      this.ugTally.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.ugTally, 3);
      this.ugTally.Cursor = Cursors.Default;
      this.ugTally.DisplayLayout.AddNewBox.Prompt = "Add ...";
      this.ugTally.DisplayLayout.GroupByBox.Prompt = "Drag a column header here to group by that column.";
      this.ugTally.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
      this.ugTally.DisplayLayout.Override.AllowColMoving = AllowColMoving.NotAllowed;
      this.ugTally.DisplayLayout.Override.AllowColSwapping = AllowColSwapping.NotAllowed;
      this.ugTally.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
      this.ugTally.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
      this.ugTally.DisplayLayout.Override.GroupByRowDescriptionMask = "[caption] : [value] ([count] [count,items,item,items])";
      this.ugTally.DisplayLayout.Override.NullText = "";
      this.ugTally.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
      this.ugTally.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
      this.ugTally.Location = new Point(3, 3);
      this.ugTally.Name = "ugTally";
      this.ugTally.Size = new Size(708, 378);
      this.ugTally.TabIndex = 4;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.Controls.Add((Control) this.OKButton, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.ugTally, 0, 0);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(714, 415);
      this.TableLayoutPanel1.TabIndex = 8;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(714, 415);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.Location = new Point(80, 240);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmInspectionTally);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Inspection Tally";
      ((ISupportInitialize) this.ugTally).EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual UltraGrid ugTally { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void frmInspectionTally_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "Inspection Summary");
        Cursor.Current = Cursors.WaitCursor;
        this.m_dtSections = !mdUtility.fMainForm.miUnits.Checked ? (!mdUtility.UseUniformat ? mdUtility.DB.GetDataTable("SELECT * FROM qryInspectionTallyMet") : mdUtility.DB.GetDataTable("SELECT * FROM qryInspectionTallyMetUniformat")) : (!mdUtility.UseUniformat ? mdUtility.DB.GetDataTable("SELECT * FROM qryInspectionTallyEng") : mdUtility.DB.GetDataTable("SELECT * FROM qryInspectionTallyEngUniformat"));
        if (this.m_dtSections.Rows.Count <= 0)
          return;
        this.ugTally.DataSource = (object) this.m_dtSections;
        UltraGrid ugTally = this.ugTally;
        ugTally.DisplayLayout.Bands[0].Columns["Building"].Width = 100;
        ugTally.DisplayLayout.Bands[0].Columns["Component"].Width = 115;
        ugTally.DisplayLayout.Bands[0].Columns["Section Description"].Width = 150;
        ugTally.DisplayLayout.Bands[0].Columns["UM"].Width = 25;
        ugTally.DisplayLayout.Bands[0].Columns["Section Qty"].Width = 65;
        ugTally.DisplayLayout.Bands[0].Columns["Sample Qty"].Width = 65;
        ugTally.DisplayLayout.Bands[0].Columns["Percent Insp"].Width = 70;
        ugTally.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
        this.ugTally.DataBind();
        Cursor.Current = Cursors.Default;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmInspectionTally), "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }
  }
}
