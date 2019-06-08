// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmSeismic
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
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
  public class frmSeismic : Form
  {
    private IContainer components;
    private int rowNum;
    private ctrlGroupButtons ctrlSeismicity;
    private ctrlGroupButtons ctrlBuildingUse;
    private ctrlGroupButtons ctrlOccupancy;
    private ctrlGroupButtons ctrlSoilType;
    private ctrlGroupButtons ctrlFallingHazards;
    private ctrlGroupButtons ctrlEvaluation;
    private int m_iSeismicity;
    private int m_iSoilType;
    private ctrlSeismicBuildingType m_ADDColumn;
    private ctrlSeismicBuildingType m_LabelsColumn;
    private Button addButton;
    private string m_sComment;
    private AssessmentHelpers.WatermarkTextBox txtOther;
    private AssessmentHelpers.LabelWithMargins lblFinalScore;
    private string m_bldg_ID;
    private string m_bldgSeismic_id;
    private ctrlSeismicBuildingType m_LowestScorer;
    private string sSeismicAssessmentSQL;
    private string sBuildingUseSQL;
    private string sBuildingTypeSQL;
    private string sFallingHazardSQL;

    public frmSeismic()
    {
      this.Load += new EventHandler(this.frmSeismic_Load);
      this.Resize += new EventHandler(this.FormResize);
      this.rowNum = 0;
      this.ctrlSeismicity = new ctrlGroupButtons();
      this.ctrlBuildingUse = new ctrlGroupButtons();
      this.ctrlOccupancy = new ctrlGroupButtons();
      this.ctrlSoilType = new ctrlGroupButtons();
      this.ctrlFallingHazards = new ctrlGroupButtons();
      this.ctrlEvaluation = new ctrlGroupButtons();
      this.m_ADDColumn = new ctrlSeismicBuildingType("Add", 0, 0);
      this.m_LabelsColumn = new ctrlSeismicBuildingType("Label", 0, 0);
      this.addButton = new Button();
      this.txtOther = new AssessmentHelpers.WatermarkTextBox();
      this.lblFinalScore = new AssessmentHelpers.LabelWithMargins();
      this.m_bldg_ID = mdUtility.fMainForm.CurrentBldg;
      this.sSeismicAssessmentSQL = "SELECT * FROM SeismicAssessment WHERE Building_ID = '" + this.m_bldg_ID + "'";
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
      this.tblCategories = new TableLayoutPanel();
      this.RadLabel6 = new Label();
      this.RadLabel1 = new Label();
      this.RadLabel2 = new Label();
      this.RadLabel3 = new Label();
      this.RadLabel4 = new Label();
      this.btnSave = new Button();
      this.btnCancel = new Button();
      this.lblHeading = new Label();
      this.tblFormLayout = new TableLayoutPanel();
      this.tblBuildingTypes = new TableLayoutPanel();
      this.RadLabel5 = new Label();
      this.pnlBuildingTypes = new Panel();
      this.tblComments = new TableLayoutPanel();
      this.gbComments = new GroupBox();
      this.btnComment = new Button();
      this.gbDetailed = new GroupBox();
      this.btnNo = new RadioButton();
      this.btnYes = new RadioButton();
      this.gbFinalScore = new GroupBox();
      this.gbCustom = new GroupBox();
      this.lblUOM = new Label();
      this.txtQuantity = new TextBox();
      this.pnlFormLayout = new Panel();
      this.tblOuterMost = new TableLayoutPanel();
      this.lblBuildingName = new Label();
      this.label = new Label();
      this.tlpDetailedReqd = new TableLayoutPanel();
      this.tblCategories.SuspendLayout();
      this.tblFormLayout.SuspendLayout();
      this.tblBuildingTypes.SuspendLayout();
      this.tblComments.SuspendLayout();
      this.gbComments.SuspendLayout();
      this.gbDetailed.SuspendLayout();
      this.gbCustom.SuspendLayout();
      this.pnlFormLayout.SuspendLayout();
      this.tblOuterMost.SuspendLayout();
      this.tlpDetailedReqd.SuspendLayout();
      this.SuspendLayout();
      this.tblCategories.AutoSize = true;
      this.tblCategories.ColumnCount = 3;
      this.tblCategories.ColumnStyles.Add(new ColumnStyle());
      this.tblCategories.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblCategories.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86f));
      this.tblCategories.Controls.Add((Control) this.RadLabel6, 1, 1);
      this.tblCategories.Controls.Add((Control) this.RadLabel1, 1, 2);
      this.tblCategories.Controls.Add((Control) this.RadLabel2, 1, 3);
      this.tblCategories.Controls.Add((Control) this.RadLabel3, 1, 4);
      this.tblCategories.Controls.Add((Control) this.RadLabel4, 1, 5);
      this.tblCategories.Location = new Point(3, 2);
      this.tblCategories.Margin = new Padding(3, 2, 3, 2);
      this.tblCategories.Name = "tblCategories";
      this.tblCategories.RowCount = 7;
      this.tblCategories.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));
      this.tblCategories.RowStyles.Add(new RowStyle());
      this.tblCategories.RowStyles.Add(new RowStyle());
      this.tblCategories.RowStyles.Add(new RowStyle());
      this.tblCategories.RowStyles.Add(new RowStyle());
      this.tblCategories.RowStyles.Add(new RowStyle());
      this.tblCategories.RowStyles.Add(new RowStyle(SizeType.Absolute, 1f));
      this.tblCategories.Size = new Size(179, 78);
      this.tblCategories.TabIndex = 0;
      this.RadLabel6.Location = new Point(3, 1);
      this.RadLabel6.Name = "RadLabel6";
      this.RadLabel6.Size = new Size(60, 20);
      this.RadLabel6.TabIndex = 12;
      this.RadLabel6.Text = " ";
      this.RadLabel6.TextAlign = ContentAlignment.MiddleCenter;
      this.RadLabel1.Location = new Point(3, 21);
      this.RadLabel1.Name = "RadLabel1";
      this.RadLabel1.Size = new Size(74, 14);
      this.RadLabel1.TabIndex = 9;
      this.RadLabel1.Text = " ";
      this.RadLabel1.TextAlign = ContentAlignment.MiddleCenter;
      this.RadLabel2.Location = new Point(3, 35);
      this.RadLabel2.Name = "RadLabel2";
      this.RadLabel2.Size = new Size(66, 14);
      this.RadLabel2.TabIndex = 10;
      this.RadLabel2.Text = " ";
      this.RadLabel2.TextAlign = ContentAlignment.MiddleCenter;
      this.RadLabel3.Location = new Point(3, 49);
      this.RadLabel3.Name = "RadLabel3";
      this.RadLabel3.Size = new Size(56, 14);
      this.RadLabel3.TabIndex = 11;
      this.RadLabel3.Text = " ";
      this.RadLabel3.TextAlign = ContentAlignment.MiddleCenter;
      this.RadLabel4.Location = new Point(3, 63);
      this.RadLabel4.Name = "RadLabel4";
      this.RadLabel4.Size = new Size(87, 14);
      this.RadLabel4.TabIndex = 11;
      this.RadLabel4.Text = " ";
      this.RadLabel4.TextAlign = ContentAlignment.MiddleCenter;
      this.btnSave.AutoSize = true;
      this.btnSave.Dock = DockStyle.Fill;
      this.btnSave.Location = new Point(701, 2);
      this.btnSave.Margin = new Padding(3, 2, 3, 2);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(80, 23);
      this.btnSave.TabIndex = 6;
      this.btnSave.Text = "Save";
      this.btnCancel.AutoSize = true;
      this.btnCancel.Dock = DockStyle.Fill;
      this.btnCancel.Location = new Point(701, 29);
      this.btnCancel.Margin = new Padding(3, 2, 3, 2);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(80, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Cancel";
      this.lblHeading.AutoSize = true;
      this.tblOuterMost.SetColumnSpan((Control) this.lblHeading, 2);
      this.lblHeading.Dock = DockStyle.Fill;
      this.lblHeading.Location = new Point(3, 0);
      this.lblHeading.Name = "lblHeading";
      this.lblHeading.Size = new Size(692, 27);
      this.lblHeading.TabIndex = 8;
      this.lblHeading.Text = "Rapid Visual Screening of Buildings for Potential Seismic Hazards";
      this.tblFormLayout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tblFormLayout.AutoSize = true;
      this.tblFormLayout.ColumnCount = 1;
      this.tblFormLayout.ColumnStyles.Add(new ColumnStyle());
      this.tblFormLayout.Controls.Add((Control) this.tblBuildingTypes, 0, 1);
      this.tblFormLayout.Controls.Add((Control) this.tblCategories, 0, 0);
      this.tblFormLayout.Controls.Add((Control) this.tblComments, 0, 2);
      this.tblFormLayout.Location = new Point(3, 2);
      this.tblFormLayout.Margin = new Padding(3, 2, 3, 2);
      this.tblFormLayout.Name = "tblFormLayout";
      this.tblFormLayout.RowCount = 3;
      this.tblFormLayout.RowStyles.Add(new RowStyle());
      this.tblFormLayout.RowStyles.Add(new RowStyle());
      this.tblFormLayout.RowStyles.Add(new RowStyle());
      this.tblFormLayout.Size = new Size(778, 656);
      this.tblFormLayout.TabIndex = 1;
      this.tblBuildingTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tblBuildingTypes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tblBuildingTypes.ColumnCount = 2;
      this.tblBuildingTypes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblBuildingTypes.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 11f));
      this.tblBuildingTypes.Controls.Add((Control) this.RadLabel5, 0, 1);
      this.tblBuildingTypes.Controls.Add((Control) this.pnlBuildingTypes, 0, 2);
      this.tblBuildingTypes.Location = new Point(3, 84);
      this.tblBuildingTypes.Margin = new Padding(3, 2, 3, 2);
      this.tblBuildingTypes.Name = "tblBuildingTypes";
      this.tblBuildingTypes.RowCount = 3;
      this.tblBuildingTypes.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tblBuildingTypes.RowStyles.Add(new RowStyle());
      this.tblBuildingTypes.RowStyles.Add(new RowStyle());
      this.tblBuildingTypes.Size = new Size(772, 490);
      this.tblBuildingTypes.TabIndex = 1;
      this.RadLabel5.Dock = DockStyle.Fill;
      this.RadLabel5.Location = new Point(3, 20);
      this.RadLabel5.Name = "RadLabel5";
      this.RadLabel5.Size = new Size(755, 14);
      this.RadLabel5.TabIndex = 8;
      this.RadLabel5.Text = "Basic Score, Modifiers, and Final Score, S";
      this.pnlBuildingTypes.AutoScroll = true;
      this.pnlBuildingTypes.BorderStyle = BorderStyle.Fixed3D;
      this.pnlBuildingTypes.Location = new Point(3, 36);
      this.pnlBuildingTypes.Margin = new Padding(3, 2, 3, 2);
      this.pnlBuildingTypes.Name = "pnlBuildingTypes";
      this.pnlBuildingTypes.Size = new Size(731, 454);
      this.pnlBuildingTypes.TabIndex = 1;
      this.tblComments.AutoSize = true;
      this.tblComments.ColumnCount = 5;
      this.tblComments.ColumnStyles.Add(new ColumnStyle());
      this.tblComments.ColumnStyles.Add(new ColumnStyle());
      this.tblComments.ColumnStyles.Add(new ColumnStyle());
      this.tblComments.ColumnStyles.Add(new ColumnStyle());
      this.tblComments.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblComments.Controls.Add((Control) this.gbComments, 1, 0);
      this.tblComments.Controls.Add((Control) this.gbDetailed, 0, 0);
      this.tblComments.Controls.Add((Control) this.gbFinalScore, 2, 0);
      this.tblComments.Controls.Add((Control) this.gbCustom, 3, 0);
      this.tblComments.Dock = DockStyle.Fill;
      this.tblComments.Location = new Point(3, 578);
      this.tblComments.Margin = new Padding(3, 2, 3, 2);
      this.tblComments.Name = "tblComments";
      this.tblComments.RowCount = 2;
      this.tblComments.RowStyles.Add(new RowStyle());
      this.tblComments.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tblComments.Size = new Size(772, 76);
      this.tblComments.TabIndex = 2;
      this.gbComments.AutoSize = true;
      this.gbComments.Controls.Add((Control) this.btnComment);
      this.gbComments.Dock = DockStyle.Fill;
      this.gbComments.Location = new Point(198, 2);
      this.gbComments.Margin = new Padding(3, 2, 3, 2);
      this.gbComments.Name = "gbComments";
      this.gbComments.Padding = new Padding(3, 2, 10, 2);
      this.gbComments.Size = new Size(94, 58);
      this.gbComments.TabIndex = 2;
      this.gbComments.TabStop = false;
      this.gbComments.Text = "Comments";
      this.btnComment.Location = new Point(17, 21);
      this.btnComment.Margin = new Padding(3, 2, 3, 2);
      this.btnComment.Name = "btnComment";
      this.btnComment.Size = new Size(64, 20);
      this.btnComment.TabIndex = 0;
      this.btnComment.UseVisualStyleBackColor = true;
      this.gbDetailed.AutoSize = true;
      this.gbDetailed.Controls.Add((Control) this.tlpDetailedReqd);
      this.gbDetailed.Dock = DockStyle.Fill;
      this.gbDetailed.Location = new Point(3, 2);
      this.gbDetailed.Margin = new Padding(3, 2, 3, 2);
      this.gbDetailed.MinimumSize = new Size(189, 0);
      this.gbDetailed.Name = "gbDetailed";
      this.gbDetailed.Padding = new Padding(3, 2, 3, 2);
      this.gbDetailed.Size = new Size(189, 58);
      this.gbDetailed.TabIndex = 2;
      this.gbDetailed.TabStop = false;
      this.gbDetailed.Text = "Detailed Evaluation Required ?";
      this.btnNo.Appearance = Appearance.Button;
      this.btnNo.Dock = DockStyle.Fill;
      this.btnNo.Location = new Point(94, 2);
      this.btnNo.Margin = new Padding(3, 2, 3, 2);
      this.btnNo.Name = "btnNo";
      this.btnNo.Size = new Size(86, 37);
      this.btnNo.TabIndex = 1;
      this.btnNo.TabStop = true;
      this.btnNo.Text = "No";
      this.btnNo.TextAlign = ContentAlignment.MiddleCenter;
      this.btnNo.UseVisualStyleBackColor = true;
      this.btnYes.Appearance = Appearance.Button;
      this.btnYes.Dock = DockStyle.Fill;
      this.btnYes.Location = new Point(3, 2);
      this.btnYes.Margin = new Padding(3, 2, 3, 2);
      this.btnYes.Name = "btnYes";
      this.btnYes.Size = new Size(85, 37);
      this.btnYes.TabIndex = 0;
      this.btnYes.TabStop = true;
      this.btnYes.Text = "Yes";
      this.btnYes.TextAlign = ContentAlignment.MiddleCenter;
      this.btnYes.UseVisualStyleBackColor = true;
      this.gbFinalScore.AutoSize = true;
      this.gbFinalScore.Dock = DockStyle.Fill;
      this.gbFinalScore.Location = new Point(298, 2);
      this.gbFinalScore.Margin = new Padding(3, 2, 3, 2);
      this.gbFinalScore.MinimumSize = new Size(110, 48);
      this.gbFinalScore.Name = "gbFinalScore";
      this.gbFinalScore.Padding = new Padding(3, 2, 13, 2);
      this.gbFinalScore.Size = new Size(110, 58);
      this.gbFinalScore.TabIndex = 3;
      this.gbFinalScore.TabStop = false;
      this.gbFinalScore.Text = "Final Score";
      this.gbCustom.AutoSize = true;
      this.gbCustom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.gbCustom.Controls.Add((Control) this.lblUOM);
      this.gbCustom.Controls.Add((Control) this.txtQuantity);
      this.gbCustom.Dock = DockStyle.Fill;
      this.gbCustom.Location = new Point(414, 2);
      this.gbCustom.Margin = new Padding(3, 2, 3, 2);
      this.gbCustom.MinimumSize = new Size(110, 48);
      this.gbCustom.Name = "gbCustom";
      this.gbCustom.Padding = new Padding(3, 2, 13, 2);
      this.gbCustom.Size = new Size(143, 58);
      this.gbCustom.TabIndex = 4;
      this.gbCustom.TabStop = false;
      this.gbCustom.Text = "Custom Area";
      this.lblUOM.AutoSize = true;
      this.lblUOM.Location = new Point(107, 21);
      this.lblUOM.Name = "lblUOM";
      this.lblUOM.Size = new Size(20, 13);
      this.lblUOM.TabIndex = 1;
      this.lblUOM.Text = "SF";
      this.txtQuantity.Location = new Point(15, 21);
      this.txtQuantity.Margin = new Padding(3, 2, 3, 2);
      this.txtQuantity.Name = "txtQuantity";
      this.txtQuantity.Size = new Size(86, 20);
      this.txtQuantity.TabIndex = 0;
      this.pnlFormLayout.AutoScroll = true;
      this.tblOuterMost.SetColumnSpan((Control) this.pnlFormLayout, 3);
      this.pnlFormLayout.Controls.Add((Control) this.tblFormLayout);
      this.pnlFormLayout.Dock = DockStyle.Fill;
      this.pnlFormLayout.Location = new Point(3, 56);
      this.pnlFormLayout.Margin = new Padding(3, 2, 3, 2);
      this.pnlFormLayout.Name = "pnlFormLayout";
      this.pnlFormLayout.Size = new Size(778, 461);
      this.pnlFormLayout.TabIndex = 2;
      this.tblOuterMost.ColumnCount = 3;
      this.tblOuterMost.ColumnStyles.Add(new ColumnStyle());
      this.tblOuterMost.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblOuterMost.ColumnStyles.Add(new ColumnStyle());
      this.tblOuterMost.Controls.Add((Control) this.lblHeading, 0, 0);
      this.tblOuterMost.Controls.Add((Control) this.btnCancel, 2, 1);
      this.tblOuterMost.Controls.Add((Control) this.lblBuildingName, 0, 1);
      this.tblOuterMost.Controls.Add((Control) this.label, 1, 1);
      this.tblOuterMost.Controls.Add((Control) this.pnlFormLayout, 0, 2);
      this.tblOuterMost.Controls.Add((Control) this.btnSave, 1, 0);
      this.tblOuterMost.Dock = DockStyle.Fill;
      this.tblOuterMost.Location = new Point(0, 0);
      this.tblOuterMost.Margin = new Padding(3, 2, 3, 2);
      this.tblOuterMost.Name = "tblOuterMost";
      this.tblOuterMost.RowCount = 3;
      this.tblOuterMost.RowStyles.Add(new RowStyle());
      this.tblOuterMost.RowStyles.Add(new RowStyle());
      this.tblOuterMost.RowStyles.Add(new RowStyle());
      this.tblOuterMost.Size = new Size(784, 519);
      this.tblOuterMost.TabIndex = 3;
      this.lblBuildingName.AutoSize = true;
      this.lblBuildingName.Dock = DockStyle.Fill;
      this.lblBuildingName.Location = new Point(3, 27);
      this.lblBuildingName.Name = "lblBuildingName";
      this.lblBuildingName.Size = new Size(62, 27);
      this.lblBuildingName.TabIndex = 0;
      this.lblBuildingName.Text = "Building :    ";
      this.lblBuildingName.TextAlign = ContentAlignment.TopRight;
      this.label.AutoSize = true;
      this.label.Dock = DockStyle.Fill;
      this.label.Location = new Point(71, 27);
      this.label.Name = "label";
      this.label.Size = new Size(624, 27);
      this.label.TabIndex = 1;
      this.label.Text = "Label1";
      this.tlpDetailedReqd.ColumnCount = 2;
      this.tlpDetailedReqd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tlpDetailedReqd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tlpDetailedReqd.Controls.Add((Control) this.btnNo, 1, 0);
      this.tlpDetailedReqd.Controls.Add((Control) this.btnYes, 0, 0);
      this.tlpDetailedReqd.Dock = DockStyle.Fill;
      this.tlpDetailedReqd.Location = new Point(3, 15);
      this.tlpDetailedReqd.Name = "tlpDetailedReqd";
      this.tlpDetailedReqd.RowCount = 1;
      this.tlpDetailedReqd.RowStyles.Add(new RowStyle());
      this.tlpDetailedReqd.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tlpDetailedReqd.Size = new Size(183, 41);
      this.tlpDetailedReqd.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 519);
      this.Controls.Add((Control) this.tblOuterMost);
      this.Margin = new Padding(3, 2, 3, 2);
      this.MinimumSize = new Size(800, 550);
      this.Name = nameof (frmSeismic);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Seismic Assessment";
      this.tblCategories.ResumeLayout(false);
      this.tblFormLayout.ResumeLayout(false);
      this.tblFormLayout.PerformLayout();
      this.tblBuildingTypes.ResumeLayout(false);
      this.tblComments.ResumeLayout(false);
      this.tblComments.PerformLayout();
      this.gbComments.ResumeLayout(false);
      this.gbDetailed.ResumeLayout(false);
      this.gbCustom.ResumeLayout(false);
      this.gbCustom.PerformLayout();
      this.pnlFormLayout.ResumeLayout(false);
      this.pnlFormLayout.PerformLayout();
      this.tblOuterMost.ResumeLayout(false);
      this.tblOuterMost.PerformLayout();
      this.tlpDetailedReqd.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel tblCategories { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnSave
    {
      get
      {
        return this._btnSave;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnSave_Click);
        Button btnSave1 = this._btnSave;
        if (btnSave1 != null)
          btnSave1.Click -= eventHandler;
        this._btnSave = value;
        Button btnSave2 = this._btnSave;
        if (btnSave2 == null)
          return;
        btnSave2.Click += eventHandler;
      }
    }

    internal virtual Button btnCancel
    {
      get
      {
        return this._btnCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnCancel_Click);
        Button btnCancel1 = this._btnCancel;
        if (btnCancel1 != null)
          btnCancel1.Click -= eventHandler;
        this._btnCancel = value;
        Button btnCancel2 = this._btnCancel;
        if (btnCancel2 == null)
          return;
        btnCancel2.Click += eventHandler;
      }
    }

    internal virtual Label lblHeading { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label RadLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label RadLabel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label RadLabel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label RadLabel4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label RadLabel6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblFormLayout { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblBuildingTypes { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label RadLabel5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlBuildingTypes { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblOuterMost { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlFormLayout { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblComments { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox gbDetailed { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox gbComments { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnComment
    {
      get
      {
        return this._btnComment;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnComment_Click);
        Button btnComment1 = this._btnComment;
        if (btnComment1 != null)
          btnComment1.Click -= eventHandler;
        this._btnComment = value;
        Button btnComment2 = this._btnComment;
        if (btnComment2 == null)
          return;
        btnComment2.Click += eventHandler;
      }
    }

    internal virtual RadioButton btnNo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadioButton btnYes { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox gbFinalScore { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblBuildingName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label label { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox gbCustom { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblUOM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtQuantity { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpDetailedReqd { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public bool DetailedEval
    {
      get
      {
        return Conversions.ToBoolean(Interaction.IIf(this.btnYes.Checked, (object) true, (object) false));
      }
      set
      {
        this.btnYes.Checked = value;
        this.btnNo.Checked = !value;
      }
    }

    public string Comment
    {
      get
      {
        return this.m_sComment;
      }
      set
      {
        this.m_sComment = value;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_sComment, "", false) > 0U)
          this.btnComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
        else
          this.btnComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      }
    }

    public int Seismicity
    {
      get
      {
        return this.m_iSeismicity;
      }
      set
      {
      }
    }

    public int SoilType
    {
      get
      {
        return this.m_iSoilType;
      }
      set
      {
        this.m_iSoilType = value;
      }
    }

    private void SetSiteSeismicity()
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT TOP 1 * FROM [SeismicAssessment]");
      if (dataTable.Rows.Count != 1 || Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["SeismicityID"])))
        return;
      this.m_iSeismicity = Conversions.ToInteger(dataTable.Rows[0]["SeismicityID"]);
    }

    private void frmSeismic_Load(object sender, EventArgs e)
    {
      this.MakeForm();
      this.SetSiteSeismicity();
      this.CheckForSeismicData();
      try
      {
        foreach (Control control in (Control.ControlCollection) this.ctrlSeismicity.ButtonsTable.Controls)
          control.Click += new EventHandler(this.Seismicity_Clicked);
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      try
      {
        foreach (Control button in this.ctrlSoilType.ButtonArray)
          button.Click += new EventHandler(this.ctrlSoilType_Clicked);
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    private void MakeForm()
    {
      try
      {
        this.SuspendLayout();
        AssessmentHelpers.FontResizer((object) this.lblHeading.Font, (object) 3);
        this.ctrlSeismicity.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_Seismicity");
        this.ctrlSeismicity.DisplayMember = "SeismicityName";
        this.ctrlSeismicity.ValueMember = "SeismicityID";
        this.ctrlSeismicity.IsRadio = true;
        this.ctrlSeismicity.Header = "Seismicity";
        this.ctrlSeismicity.Draw();
        this.tblCategories.Controls.Add((Control) this.ctrlSeismicity, 0, 1);
        this.ctrlBuildingUse.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_BuildingUse");
        this.ctrlBuildingUse.DisplayMember = "BuildingUseName";
        this.ctrlBuildingUse.ValueMember = "BuildingUseID";
        this.ctrlBuildingUse.NumColumns = 3;
        this.ctrlBuildingUse.Header = "Building Use";
        this.ctrlBuildingUse.Draw();
        this.tblCategories.Controls.Add((Control) this.ctrlBuildingUse, 0, 2);
        this.ctrlOccupancy.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_Occupancy");
        this.ctrlOccupancy.DisplayMember = "OccupancyName";
        this.ctrlOccupancy.ValueMember = "OccupancyID";
        this.ctrlOccupancy.IsRadio = true;
        this.ctrlOccupancy.Header = "Occupancy";
        this.ctrlOccupancy.Draw();
        this.tblCategories.Controls.Add((Control) this.ctrlOccupancy, 0, 3);
        this.ctrlSoilType.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_SoilType");
        this.ctrlSoilType.DisplayMember = "SoilTypeDesc";
        this.ctrlSoilType.ValueMember = "SoilTypeID";
        this.ctrlSoilType.IsRadio = true;
        this.ctrlSoilType.Header = "Soil Type";
        this.ctrlSoilType.NumColumns = 6;
        string str = "ABCDEF";
        int index = 0;
        while (index < str.Length)
        {
          char ch = str[index];
          Label label = new Label();
          label.Width = AssessmentHelpers.GetButtonSize.Width;
          AssessmentHelpers.FontResizer((object) label.Font, (object) 3);
          label.Height = checked ((int) Math.Round(unchecked ((double) label.Font.GetHeight() + 2.0)));
          label.TextAlign = ContentAlignment.MiddleCenter;
          label.Text = Conversions.ToString(ch);
          this.ctrlSoilType.AddControlToControlsTable((Control) label);
          checked { ++index; }
        }
        this.ctrlSoilType.Draw();
        this.tblCategories.Controls.Add((Control) this.ctrlSoilType, 0, 4);
        this.tblCategories.SetColumnSpan((Control) this.ctrlSoilType, 2);
        this.ctrlFallingHazards.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_FallingHazards ORDER BY FallingHazardID");
        this.ctrlFallingHazards.DisplayMember = "FallingHazardName";
        this.ctrlFallingHazards.ValueMember = "FallingHazardID";
        this.ctrlFallingHazards.Header = "Falling Hazards";
        this.ctrlFallingHazards.Draw();
        this.ctrlFallingHazards.NumColumns = 5;
        this.ctrlFallingHazards.AddControlToControlsTable((Control) this.txtOther);
        this.txtOther.Enabled = false;
        this.txtOther.Margin = new Padding(0, checked (AssessmentHelpers.GetButtonSize.Height - this.Font.Height - 6), 0, 0);
        this.txtOther.Width = checked (AssessmentHelpers.GetButtonSize.Width + 20);
        this.txtOther.Dock = DockStyle.Left;
        this.ctrlFallingHazards.AddControlToControlsTable((Control) this.txtOther);
        this.tblCategories.Controls.Add((Control) this.ctrlFallingHazards, 0, 5);
        try
        {
          foreach (Control button in this.ctrlFallingHazards.ButtonArray)
            button.Click += new EventHandler(this.ctrlFallingHazards_Clicked);
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.pnlBuildingTypes.Controls.Add((Control) this.m_LabelsColumn);
        this.addButton.Size = AssessmentHelpers.GetButtonSize;
        this.addButton.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
        this.addButton.TextAlign = ContentAlignment.MiddleCenter;
        this.addButton.Click += new EventHandler(this.addButton_Clicked);
        this.m_ADDColumn.tblColumn.Controls.RemoveAt(0);
        this.m_ADDColumn.tblColumn.Controls.Add((Control) this.addButton, 0, 0);
        this.pnlBuildingTypes.Controls.Add((Control) this.m_ADDColumn);
        this.pnlBuildingTypes.Controls.SetChildIndex((Control) this.m_ADDColumn, 0);
        this.ResumeLayout();
        this.btnYes.Size = AssessmentHelpers.GetButtonSize;
        this.btnNo.Size = AssessmentHelpers.GetButtonSize;
        RadioButton btnNo = this.btnNo;
        Point location = this.btnYes.Location;
        int x = checked (location.X + this.btnYes.Size.Width + 2);
        location = this.btnYes.Location;
        int y = location.Y;
        Point point = new Point(x, y);
        btnNo.Location = point;
        this.btnComment.Size = AssessmentHelpers.GetButtonSize;
        this.lblFinalScore.Size = AssessmentHelpers.GetButtonSize;
        this.gbFinalScore.Controls.Add((Control) this.lblFinalScore);
        this.lblFinalScore.Location = new Point(20, 31);
        this.lblFinalScore.TextAlign = ContentAlignment.MiddleCenter;
        this.lblBuildingName.Text += Building.LabelByID(this.m_bldg_ID);
        this.label.Text = "";
        this.lblUOM.Text = !mdUtility.fMainForm.miUnits.Checked ? "SM" : "SF";
        AssessmentHelpers.FontResizer((object) this.lblFinalScore, (object) this.lblBuildingName, (object) this.txtQuantity, (object) 3);
        TextBox txtQuantity = this.txtQuantity;
        ref TextBox local1 = ref txtQuantity;
        Label lblUom = this.lblUOM;
        ref Label local2 = ref lblUom;
        AssessmentHelpers.TextBoxValidator textBoxValidator = new AssessmentHelpers.TextBoxValidator(ref local1, ref local2, AssessmentHelpers.TextBoxValidator.ValidationTypes.Quantity);
        this.lblUOM = lblUom;
        this.txtQuantity = txtQuantity;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Make Form Failed");
        ProjectData.ClearProjectError();
      }
    }

    private void CheckForSeismicData()
    {
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable(this.sSeismicAssessmentSQL);
        bool flag = false;
        if (dataTable.Rows.Count == 0)
        {
          DataRow row = dataTable.NewRow();
          if ((uint) this.m_iSeismicity > 0U)
            row["SeismicityID"] = (object) this.m_iSeismicity;
          row["Building_ID"] = (object) this.m_bldg_ID;
          this.m_bldgSeismic_id = mdUtility.GetUniqueID();
          row["BuildingSeismicID"] = (object) this.m_bldgSeismic_id;
          dataTable.Rows.Add(row);
          this.ctrlSeismicity.SetSelected = dataTable;
          mdUtility.DB.SaveDataTable(ref dataTable, this.sSeismicAssessmentSQL);
          this.Comment = "";
        }
        else
        {
          this.m_bldgSeismic_id = Conversions.ToString(dataTable.Rows[0]["BuildingSeismicID"]);
          if ((uint) this.m_iSeismicity > 0U)
            dataTable.Rows[0]["SeismicityID"] = (object) this.m_iSeismicity;
          flag = true;
        }
        this.sBuildingUseSQL = "SELECT * FROM Seismic_BuildingUse WHERE BuildingSeismicID = '" + this.m_bldgSeismic_id + "'";
        this.sBuildingTypeSQL = "SELECT * FROM Seismic_BuildingTypes WHERE BuildingSeismicID = '" + this.m_bldgSeismic_id + "'";
        this.sFallingHazardSQL = "SELECT * FROM Seismic_FallingHazards WHERE BuildingSeismicID = '" + this.m_bldgSeismic_id + "'";
        if (!flag)
          return;
        this.LoadFormFromDB(dataTable);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "GetSeismicData Failed");
        ProjectData.ClearProjectError();
      }
    }

    private void LoadFormFromDB(DataTable dtSeismic)
    {
      try
      {
        DataTable dataTable1 = mdUtility.DB.GetDataTable(this.sBuildingUseSQL);
        DataTable dataTable2 = mdUtility.DB.GetDataTable(this.sBuildingTypeSQL);
        DataTable dataTable3 = mdUtility.DB.GetDataTable(this.sFallingHazardSQL);
        this.ctrlBuildingUse.SetSelected = dataTable1;
        this.ctrlSoilType.SetSelected = dtSeismic;
        this.ctrlSeismicity.SetSelected = dtSeismic;
        this.ctrlOccupancy.SetSelected = dtSeismic;
        this.ctrlFallingHazards.SetSelected = dataTable3;
        try
        {
          foreach (DataRow row in dataTable3.Rows)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["FallingHazardID"], (object) 4, false) && !Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["FallingHazardOther"])))
              this.txtOther.Text = Conversions.ToString(row["FallingHazardOther"]);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        if (this.ctrlSeismicity.GetSelected.Rows.Count > 0)
          this.m_iSeismicity = Conversions.ToInteger(this.ctrlSeismicity.GetSelected.Rows[0]["SeismicityID"]);
        if (this.ctrlSoilType.GetSelected.Rows.Count > 0)
          this.m_iSoilType = Conversions.ToInteger(this.ctrlSoilType.GetSelected.Rows[0]["SoilTypeID"]);
        this.Comment = Information.IsDBNull(RuntimeHelpers.GetObjectValue(dtSeismic.Rows[0]["Comments"])) ? "" : Conversions.ToString(dtSeismic.Rows[0]["Comments"]);
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dtSeismic.Rows[0]["DetailedEval"])))
          this.DetailedEval = Conversions.ToBoolean(dtSeismic.Rows[0]["DetailedEval"]);
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dtSeismic.Rows[0]["Area"])))
        {
          TextBox txtQuantity = this.txtQuantity;
          double display = mdUtility.AreaConvertDBToDisplay(Conversions.ToDouble(dtSeismic.Rows[0]["Area"]));
          string str = mdUtility.FormatDoubleForDisplay(ref display);
          txtQuantity.Text = str;
        }
        try
        {
          foreach (DataRow row in dataTable2.Rows)
            this.AddNewBuildingType().LoadColumnFromDB(row);
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "FillForm Failed");
        ProjectData.ClearProjectError();
      }
    }

    private void SaveAssessment()
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable(this.sSeismicAssessmentSQL);
      DataRow row1 = dataTable1.Rows[0];
      DataTable dataTable2 = mdUtility.DB.GetDataTable(this.sBuildingUseSQL);
      DataTable dataTable3 = mdUtility.DB.GetDataTable(this.sBuildingTypeSQL);
      DataTable dataTable4 = mdUtility.DB.GetDataTable(this.sFallingHazardSQL);
      try
      {
        mdUtility.DB.BeginTransaction();
        this.Cursor = Cursors.WaitCursor;
        AssessmentHelpers.SaveInspector(dataTable1.Rows[0]);
        DataRow row2 = dataTable1.Rows[0];
        row2["SeismicityID"] = (uint) this.ctrlSeismicity.GetSelected.Rows.Count <= 0U ? (object) DBNull.Value : RuntimeHelpers.GetObjectValue(this.ctrlSeismicity.GetSelected.Rows[0]["SeismicityID"]);
        if ((uint) this.ctrlSoilType.GetSelected.Rows.Count > 0U)
          row2["SoilTypeID"] = RuntimeHelpers.GetObjectValue(this.ctrlSoilType.GetSelected.Rows[0]["SoilTypeID"]);
        else
          dataTable1.Rows[0]["SoilTypeID"] = (object) DBNull.Value;
        row2["OccupancyID"] = (uint) this.ctrlOccupancy.GetSelected.Rows.Count <= 0U ? (object) DBNull.Value : RuntimeHelpers.GetObjectValue(this.ctrlOccupancy.GetSelected.Rows[0]["OccupancyID"]);
        row2["Comments"] = (object) this.m_sComment;
        if (this.btnYes.Checked || this.btnNo.Checked)
          row2["DetailedEval"] = (object) this.DetailedEval;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtQuantity.Text, "", false) > 0U)
          row2["Area"] = (object) mdUtility.AreaConvertDisplayToDB(Conversions.ToDouble(this.txtQuantity.Text));
        try
        {
          foreach (DataRow row3 in dataTable4.Rows)
            row3.Delete();
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        mdUtility.DB.SaveDataTable(ref dataTable4, this.sFallingHazardSQL);
        try
        {
          foreach (DataRow row3 in this.ctrlFallingHazards.GetSelected.Rows)
          {
            DataRow row4 = dataTable4.NewRow();
            row4["BuildingSeismicID"] = (object) this.m_bldgSeismic_id;
            row4["SeismicFallingHazardID"] = (object) mdUtility.GetUniqueID();
            row4["FallingHazardID"] = RuntimeHelpers.GetObjectValue(row3["FallingHazardID"]);
            dataTable4.Rows.Add(row4);
            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row4["FallingHazardID"], (object) 4, false))
              row4["FallingHazardOther"] = (object) this.txtOther.Text;
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        try
        {
          foreach (DataRow row3 in dataTable2.Rows)
            row3.Delete();
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        mdUtility.DB.SaveDataTable(ref dataTable2, this.sBuildingUseSQL);
        try
        {
          foreach (DataRow row3 in this.ctrlBuildingUse.GetSelected.Rows)
          {
            DataRow row4 = dataTable2.NewRow();
            row4["BuildingSeismicID"] = (object) this.m_bldgSeismic_id;
            row4["SeismicBuildingUseID"] = (object) mdUtility.GetUniqueID();
            row4["BuildingUseID"] = RuntimeHelpers.GetObjectValue(row3["BuildingUseID"]);
            dataTable2.Rows.Add(row4);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        int index = 0;
        while (index < this.pnlBuildingTypes.Controls.Count)
        {
          ctrlSeismicBuildingType control = (ctrlSeismicBuildingType) this.pnlBuildingTypes.Controls[index];
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(control.SelectedBldgType, "", false) == 0)
            this.pnlBuildingTypes.Controls.Remove((Control) control);
          else
            checked { ++index; }
        }
        if (dataTable3.Rows.Count > 0)
        {
          try
          {
            foreach (DataRow row3 in dataTable3.Rows)
            {
              bool flag = false;
              try
              {
                foreach (ctrlSeismicBuildingType control in this.pnlBuildingTypes.Controls)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row3["SeismicBuildingTypeID"], (object) control.BuildingTypeID, false))
                  {
                    control.Save(ref row3);
                    this.pnlBuildingTypes.Controls.Remove((Control) control);
                    flag = true;
                    break;
                  }
                }
              }
              finally
              {
                IEnumerator enumerator;
                if (enumerator is IDisposable)
                  (enumerator as IDisposable).Dispose();
              }
              if (!flag)
              {
                ctrlSeismicBuildingType.DeleteModifiersFromDB(row3);
                row3.Delete();
              }
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
        }
        if (this.pnlBuildingTypes.Controls.Count > 0)
        {
          try
          {
            foreach (ctrlSeismicBuildingType control in this.pnlBuildingTypes.Controls)
            {
              DataRow dr = dataTable3.NewRow();
              dr["BuildingSeismicID"] = (object) this.m_bldgSeismic_id;
              dataTable3.Rows.Add(control.Save(ref dr));
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
        }
        mdUtility.DB.SaveDataTable(ref dataTable1, this.sSeismicAssessmentSQL);
        mdUtility.DB.SaveDataTable(ref dataTable2, this.sBuildingUseSQL);
        mdUtility.DB.SaveDataTable(ref dataTable3, this.sBuildingTypeSQL);
        mdUtility.DB.SaveDataTable(ref dataTable4, this.sFallingHazardSQL);
        mdUtility.DB.CommitTransaction();
        this.Close();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Save Assessment Failed");
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void addButton_Clicked(object sender, EventArgs e)
    {
      if (this.ctrlSeismicity.GetSelected.Rows.Count > 0 & this.ctrlSoilType.GetSelected.Rows.Count > 0)
      {
        this.AddNewBuildingType();
      }
      else
      {
        int num = (int) MessageBox.Show("A selection for \"Seismicity\" and \"Soil Type\" must be made before you can add a \"Building Type\"");
      }
    }

    private ctrlSeismicBuildingType AddNewBuildingType()
    {
      ctrlSeismicBuildingType seismicBuildingType = new ctrlSeismicBuildingType("BuildingType", this.m_iSoilType, this.m_iSeismicity);
      seismicBuildingType.FinalScoreChanged += new ctrlSeismicBuildingType.FinalScoreChangedEventHandler(this.UpdateFinalScore_Changed);
      this.pnlBuildingTypes.Controls.Add((Control) seismicBuildingType);
      this.pnlBuildingTypes.Controls.SetChildIndex((Control) seismicBuildingType, 0);
      this.pnlBuildingTypes.Controls.SetChildIndex((Control) this.m_ADDColumn, 0);
      return seismicBuildingType;
    }

    private void Seismicity_Clicked(object sender, EventArgs e)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("Select * From SeismicAssessment");
      if (Interaction.MsgBox((object) "Seismicity will be changed for the whole site.  Continue?", MsgBoxStyle.YesNo, (object) "Change Default Seismicity?") == MsgBoxResult.Yes)
      {
        try
        {
          this.Cursor = Cursors.WaitCursor;
          this.m_iSeismicity = Conversions.ToInteger(this.ctrlSeismicity.GetSelected.Rows[0]["SeismicityID"]);
          try
          {
            foreach (ctrlSeismicBuildingType control in this.pnlBuildingTypes.Controls)
            {
              if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(control.SelectedBldgType, "", false) > 0U)
                control.Seismicity = this.m_iSeismicity;
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          try
          {
            foreach (DataRow row in dataTable.Rows)
              row["SeismicityID"] = (object) this.m_iSeismicity;
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          mdUtility.DB.SaveDataTable(ref dataTable, "Select * From SeismicAssessment");
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          mdUtility.Errorhandler(ex, this.Name, "Seismicity_Clicked Failed");
          ProjectData.ClearProjectError();
        }
        finally
        {
          this.Cursor = Cursors.Default;
        }
      }
      else
      {
        try
        {
          foreach (Control control in (Control.ControlCollection) this.ctrlSeismicity.ButtonsTable.Controls)
            control.Click -= new EventHandler(this.Seismicity_Clicked);
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.ctrlSeismicity.SetSelected = dataTable.Rows[0].Table;
        try
        {
          foreach (Control control in (Control.ControlCollection) this.ctrlSeismicity.ButtonsTable.Controls)
            control.Click += new EventHandler(this.Seismicity_Clicked);
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
    }

    private void ctrlSoilType_Clicked(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        this.m_iSoilType = Conversions.ToInteger(this.ctrlSoilType.GetSelected.Rows[0]["SoilTypeID"]);
        try
        {
          foreach (ctrlSeismicBuildingType control in this.pnlBuildingTypes.Controls)
            control.SoilType = this.m_iSoilType;
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "SoilType_Clicked Failed");
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void ctrlFallingHazards_Clicked(object sender, EventArgs e)
    {
      try
      {
        CheckBox checkBox = (CheckBox) sender;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(checkBox.Text, "Other", false) != 0)
          return;
        if (checkBox.Checked)
        {
          this.txtOther.Enabled = true;
          this.txtOther.WatermarkText = "\"Other\"";
        }
        else
        {
          this.txtOther.Clear();
          this.txtOther.Enabled = false;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "FallingHazards_Clicked Failed");
        ProjectData.ClearProjectError();
      }
    }

    private void UpdateFinalScore_Changed(object sender, double score)
    {
      if (this.m_LowestScorer == null)
        this.m_LowestScorer = (ctrlSeismicBuildingType) sender;
      if (this.m_LowestScorer != sender && score < this.m_LowestScorer.FinalScore)
      {
        this.m_LowestScorer = (ctrlSeismicBuildingType) sender;
      }
      else
      {
        try
        {
          foreach (ctrlSeismicBuildingType control in this.pnlBuildingTypes.Controls)
          {
            if (Conversions.ToDouble(control.SelectedBldgType) > 0.0 && control.FinalScore < this.m_LowestScorer.FinalScore)
              this.m_LowestScorer = control;
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      this.lblFinalScore.Text = Conversions.ToString(this.m_LowestScorer.FinalScore);
    }

    private void btnComment_Click(object sender, EventArgs e)
    {
      this.Comment = AssessmentHelpers.CommentDialog("Comment for Seismic Assessment", this.m_sComment, (IWin32Window) this);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.SaveAssessment();
    }

    private void FormResize(object sender, EventArgs e)
    {
      this.pnlBuildingTypes.Width = checked (this.pnlFormLayout.Width - 50);
    }
  }
}
