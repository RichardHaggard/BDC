// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmAddMissingComponent
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
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmAddMissingComponent : Form
  {
    private IContainer components;
    private const string MOD_NAME = "frmMissingComponents";
    private string m_bldg_ID;
    private bool m_bInEditMode;
    private string m_LastAddedComponent;
    private bool nonNumberEntered;

    public frmAddMissingComponent()
    {
      this.Load += new EventHandler(this.frmAddMissingComponents_Load);
      this.m_LastAddedComponent = "";
      this.nonNumberEntered = false;
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmAddMissingComponent));
      this.gbSelection = new RadGroupBox();
      this.TableLayoutPanel3 = new TableLayoutPanel();
      this.RadLabel4 = new RadLabel();
      this.cboComponents = new ComboBox();
      this.RadLabel6 = new RadLabel();
      this.cboSystems = new ComboBox();
      this.cboMaterialCategory = new ComboBox();
      this.RadLabel5 = new RadLabel();
      this.cboComponentType = new ComboBox();
      this.RadLabel7 = new RadLabel();
      this.btnAddButton = new Button();
      this.btnCancel = new Button();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.gbDetails = new RadGroupBox();
      this.RadLabel2 = new RadLabel();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.txtComments = new RadTextBox();
      this.RadLabel3 = new RadLabel();
      this.RadLabel1 = new RadLabel();
      this.txtQuantity = new TextBox();
      this.flpTools = new FlowLayoutPanel();
      this.lblUnitOfMeasure = new Label();
      this.cmdIncrease = new Button();
      this.cmdDecrease = new Button();
      this.cmdCalc = new Button();
      this.lblMissionCritical = new RadLabel();
      this.chkMissionCritical = new RadCheckBox();
      this.Panel1 = new Panel();
      this.gbSelection.BeginInit();
      this.gbSelection.SuspendLayout();
      this.TableLayoutPanel3.SuspendLayout();
      this.RadLabel4.BeginInit();
      this.RadLabel6.BeginInit();
      this.RadLabel5.BeginInit();
      this.RadLabel7.BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.gbDetails.BeginInit();
      this.gbDetails.SuspendLayout();
      this.RadLabel2.BeginInit();
      this.TableLayoutPanel2.SuspendLayout();
      this.txtComments.BeginInit();
      this.RadLabel3.BeginInit();
      this.RadLabel1.BeginInit();
      this.flpTools.SuspendLayout();
      this.lblMissionCritical.BeginInit();
      this.chkMissionCritical.BeginInit();
      this.Panel1.SuspendLayout();
      this.SuspendLayout();
      this.gbSelection.AccessibleRole = AccessibleRole.Grouping;
      this.gbSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.gbSelection.Controls.Add((Control) this.TableLayoutPanel3);
      this.gbSelection.FooterImageIndex = -1;
      this.gbSelection.FooterImageKey = "";
      this.gbSelection.HeaderImageIndex = -1;
      this.gbSelection.HeaderImageKey = "";
      this.gbSelection.HeaderMargin = new Padding(0);
      this.gbSelection.HeaderText = "Select Component";
      this.gbSelection.Location = new Point(3, 3);
      this.gbSelection.Name = "gbSelection";
      this.gbSelection.Padding = new Padding(2, 18, 2, 2);
      this.gbSelection.RootElement.Padding = new Padding(2, 18, 2, 2);
      this.gbSelection.Size = new Size(664, 244);
      this.gbSelection.TabIndex = 23;
      this.gbSelection.Text = "Select Component";
      this.gbSelection.ThemeName = "CustomTelerik";
      this.TableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel3.AutoSize = true;
      this.TableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel3.ColumnCount = 3;
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33f));
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33f));
      this.TableLayoutPanel3.Controls.Add((Control) this.RadLabel4, 1, 0);
      this.TableLayoutPanel3.Controls.Add((Control) this.cboComponents, 1, 3);
      this.TableLayoutPanel3.Controls.Add((Control) this.RadLabel6, 1, 4);
      this.TableLayoutPanel3.Controls.Add((Control) this.cboSystems, 1, 1);
      this.TableLayoutPanel3.Controls.Add((Control) this.cboMaterialCategory, 1, 5);
      this.TableLayoutPanel3.Controls.Add((Control) this.RadLabel5, 1, 2);
      this.TableLayoutPanel3.Controls.Add((Control) this.cboComponentType, 1, 7);
      this.TableLayoutPanel3.Controls.Add((Control) this.RadLabel7, 1, 6);
      this.TableLayoutPanel3.Location = new Point(5, 21);
      this.TableLayoutPanel3.Name = "TableLayoutPanel3";
      this.TableLayoutPanel3.RowCount = 8;
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel3.Size = new Size(654, 204);
      this.TableLayoutPanel3.TabIndex = 9;
      this.RadLabel4.Dock = DockStyle.Bottom;
      this.RadLabel4.Location = new Point(36, 3);
      this.RadLabel4.Name = "RadLabel4";
      this.RadLabel4.Size = new Size(44, 18);
      this.RadLabel4.TabIndex = 4;
      this.RadLabel4.Text = "System:";
      this.cboComponents.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cboComponents.DropDownHeight = 200;
      this.cboComponents.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboComponents.FormattingEnabled = true;
      this.cboComponents.IntegralHeight = false;
      this.cboComponents.Location = new Point(36, 78);
      this.cboComponents.Name = "cboComponents";
      this.cboComponents.Size = new Size(582, 21);
      this.cboComponents.TabIndex = 1;
      this.RadLabel6.Dock = DockStyle.Bottom;
      this.RadLabel6.Location = new Point(36, 105);
      this.RadLabel6.Name = "RadLabel6";
      this.RadLabel6.Size = new Size(98, 18);
      this.RadLabel6.TabIndex = 6;
      this.RadLabel6.Text = "Material Category:";
      this.cboSystems.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cboSystems.DropDownHeight = 200;
      this.cboSystems.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSystems.FormattingEnabled = true;
      this.cboSystems.IntegralHeight = false;
      this.cboSystems.Location = new Point(36, 27);
      this.cboSystems.Name = "cboSystems";
      this.cboSystems.Size = new Size(582, 21);
      this.cboSystems.TabIndex = 0;
      this.cboMaterialCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cboMaterialCategory.DropDownHeight = 200;
      this.cboMaterialCategory.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboMaterialCategory.FormattingEnabled = true;
      this.cboMaterialCategory.IntegralHeight = false;
      this.cboMaterialCategory.Location = new Point(36, 129);
      this.cboMaterialCategory.Name = "cboMaterialCategory";
      this.cboMaterialCategory.Size = new Size(582, 21);
      this.cboMaterialCategory.TabIndex = 2;
      this.RadLabel5.Dock = DockStyle.Bottom;
      this.RadLabel5.Location = new Point(36, 54);
      this.RadLabel5.Name = "RadLabel5";
      this.RadLabel5.Size = new Size(68, 18);
      this.RadLabel5.TabIndex = 5;
      this.RadLabel5.Text = "Component:";
      this.cboComponentType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.cboComponentType.DropDownHeight = 200;
      this.cboComponentType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboComponentType.FormattingEnabled = true;
      this.cboComponentType.IntegralHeight = false;
      this.cboComponentType.Location = new Point(36, 180);
      this.cboComponentType.Name = "cboComponentType";
      this.cboComponentType.Size = new Size(582, 21);
      this.cboComponentType.TabIndex = 3;
      this.RadLabel7.Dock = DockStyle.Bottom;
      this.RadLabel7.Location = new Point(36, 156);
      this.RadLabel7.Name = "RadLabel7";
      this.RadLabel7.Size = new Size(95, 18);
      this.RadLabel7.TabIndex = 7;
      this.RadLabel7.Text = "Component Type:";
      this.btnAddButton.Location = new Point(2, 3);
      this.btnAddButton.Name = "btnAddButton";
      this.btnAddButton.Size = new Size(88, 24);
      this.btnAddButton.TabIndex = 10;
      this.btnAddButton.Text = "Add";
      this.btnCancel.Location = new Point(2, 33);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(88, 24);
      this.btnCancel.TabIndex = 5;
      this.btnCancel.Text = "Cancel";
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.gbDetails, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.gbSelection, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.Panel1, 1, 0);
      this.TableLayoutPanel1.Location = new Point(12, 12);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(769, 503);
      this.TableLayoutPanel1.TabIndex = 28;
      this.gbDetails.AccessibleRole = AccessibleRole.Grouping;
      this.gbDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.gbDetails.Controls.Add((Control) this.RadLabel2);
      this.gbDetails.Controls.Add((Control) this.TableLayoutPanel2);
      this.gbDetails.FooterImageIndex = -1;
      this.gbDetails.FooterImageKey = "";
      this.gbDetails.ForeColor = Color.Black;
      this.gbDetails.HeaderImageIndex = -1;
      this.gbDetails.HeaderImageKey = "";
      this.gbDetails.HeaderMargin = new Padding(0);
      this.gbDetails.HeaderText = "Missing Component Details";
      this.gbDetails.Location = new Point(3, 253);
      this.gbDetails.Name = "gbDetails";
      this.gbDetails.Padding = new Padding(2, 18, 2, 2);
      this.gbDetails.RootElement.ForeColor = Color.Black;
      this.gbDetails.RootElement.Padding = new Padding(2, 18, 2, 2);
      this.gbDetails.Size = new Size(664, 247);
      this.gbDetails.TabIndex = 29;
      this.gbDetails.Text = "Missing Component Details";
      this.gbDetails.ThemeName = "CustomTelerik";
      this.RadLabel2.Location = new Point(484, 76);
      this.RadLabel2.Name = "RadLabel2";
      this.RadLabel2.Size = new Size(2, 2);
      this.RadLabel2.TabIndex = 8;
      this.TableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel2.ColumnCount = 10;
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33f));
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40f));
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120f));
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33f));
      this.TableLayoutPanel2.Controls.Add((Control) this.txtComments, 1, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.RadLabel3, 1, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.RadLabel1, 3, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtQuantity, 4, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.flpTools, 5, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblMissionCritical, 6, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.chkMissionCritical, 7, 1);
      this.TableLayoutPanel2.Location = new Point(2, 18);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 3;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 83f));
      this.TableLayoutPanel2.Size = new Size(657, 224);
      this.TableLayoutPanel2.TabIndex = 28;
      this.txtComments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel2.SetColumnSpan((Control) this.txtComments, 8);
      this.txtComments.Location = new Point(36, 39);
      this.txtComments.Multiline = true;
      this.txtComments.Name = "txtComments";
      this.txtComments.RootElement.StretchVertically = true;
      this.txtComments.Size = new Size(585, 182);
      this.txtComments.TabIndex = 2;
      this.txtComments.TabStop = false;
      this.RadLabel3.Dock = DockStyle.Bottom;
      this.RadLabel3.Location = new Point(36, 15);
      this.RadLabel3.Name = "RadLabel3";
      this.RadLabel3.Size = new Size(63, 18);
      this.RadLabel3.TabIndex = 9;
      this.RadLabel3.Text = "Comments:";
      this.RadLabel1.Dock = DockStyle.Bottom;
      this.RadLabel1.Location = new Point(145, 15);
      this.RadLabel1.Name = "RadLabel1";
      this.RadLabel1.Size = new Size(52, 18);
      this.RadLabel1.TabIndex = 7;
      this.RadLabel1.Text = "Quantity:";
      this.RadLabel1.TextAlignment = ContentAlignment.MiddleRight;
      this.txtQuantity.Location = new Point(203, 3);
      this.txtQuantity.Margin = new Padding(3, 3, 3, 6);
      this.txtQuantity.MinimumSize = new Size(128, 27);
      this.txtQuantity.Name = "txtQuantity";
      this.txtQuantity.Size = new Size(128, 27);
      this.txtQuantity.TabIndex = 0;
      this.txtQuantity.TabStop = false;
      this.flpTools.AutoSize = true;
      this.flpTools.Controls.Add((Control) this.lblUnitOfMeasure);
      this.flpTools.Controls.Add((Control) this.cmdIncrease);
      this.flpTools.Controls.Add((Control) this.cmdDecrease);
      this.flpTools.Controls.Add((Control) this.cmdCalc);
      this.flpTools.Dock = DockStyle.Bottom;
      this.flpTools.Location = new Point(334, 13);
      this.flpTools.Margin = new Padding(0);
      this.flpTools.Name = "flpTools";
      this.flpTools.Size = new Size(120, 23);
      this.flpTools.TabIndex = 24;
      this.lblUnitOfMeasure.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblUnitOfMeasure.AutoSize = true;
      this.lblUnitOfMeasure.BackColor = SystemColors.Control;
      this.lblUnitOfMeasure.Cursor = Cursors.Default;
      this.lblUnitOfMeasure.ForeColor = SystemColors.ControlText;
      this.lblUnitOfMeasure.Location = new Point(3, 3);
      this.lblUnitOfMeasure.Margin = new Padding(3, 3, 10, 3);
      this.lblUnitOfMeasure.Name = "lblUnitOfMeasure";
      this.lblUnitOfMeasure.RightToLeft = RightToLeft.No;
      this.lblUnitOfMeasure.Size = new Size(0, 17);
      this.lblUnitOfMeasure.TabIndex = 13;
      this.lblUnitOfMeasure.TextAlign = ContentAlignment.MiddleLeft;
      this.cmdIncrease.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
      this.cmdIncrease.Location = new Point(13, 0);
      this.cmdIncrease.Margin = new Padding(0);
      this.cmdIncrease.Name = "cmdIncrease";
      this.cmdIncrease.Size = new Size(23, 23);
      this.cmdIncrease.TabIndex = 14;
      this.cmdIncrease.UseVisualStyleBackColor = true;
      this.cmdIncrease.Visible = false;
      this.cmdDecrease.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Restricted_2;
      this.cmdDecrease.Location = new Point(36, 0);
      this.cmdDecrease.Margin = new Padding(0);
      this.cmdDecrease.Name = "cmdDecrease";
      this.cmdDecrease.Size = new Size(23, 23);
      this.cmdDecrease.TabIndex = 15;
      this.cmdDecrease.UseVisualStyleBackColor = true;
      this.cmdDecrease.Visible = false;
      this.cmdCalc.Image = (Image) BuilderRED.My.Resources.Resources.Calculator_Accounting;
      this.cmdCalc.Location = new Point(59, 0);
      this.cmdCalc.Margin = new Padding(0);
      this.cmdCalc.Name = "cmdCalc";
      this.cmdCalc.Size = new Size(23, 23);
      this.cmdCalc.TabIndex = 16;
      this.cmdCalc.UseVisualStyleBackColor = true;
      this.cmdCalc.Visible = false;
      this.lblMissionCritical.Dock = DockStyle.Bottom;
      this.lblMissionCritical.Location = new Point(457, 15);
      this.lblMissionCritical.Name = "lblMissionCritical";
      this.lblMissionCritical.Size = new Size(84, 18);
      this.lblMissionCritical.TabIndex = 12;
      this.lblMissionCritical.Text = "Mission Critical:";
      this.lblMissionCritical.TextAlignment = ContentAlignment.MiddleRight;
      this.chkMissionCritical.Dock = DockStyle.Bottom;
      this.chkMissionCritical.Location = new Point(547, 11);
      this.chkMissionCritical.Name = "chkMissionCritical";
      this.chkMissionCritical.Size = new Size(22, 22);
      this.chkMissionCritical.TabIndex = 27;
      this.chkMissionCritical.GetChildAt(0).GetChildAt(1).GetChildAt(1).ScaleTransform = new SizeF(1f, 1f);
      this.chkMissionCritical.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2).ScaleTransform = new SizeF(1.5f, 1.5f);
      this.Panel1.AutoSize = true;
      this.Panel1.Controls.Add((Control) this.btnAddButton);
      this.Panel1.Controls.Add((Control) this.btnCancel);
      this.Panel1.Location = new Point(673, 3);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new Size(93, 60);
      this.Panel1.TabIndex = 28;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 519);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(800, 550);
      this.Name = nameof (frmAddMissingComponent);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Add Missing Component";
      this.gbSelection.EndInit();
      this.gbSelection.ResumeLayout(false);
      this.gbSelection.PerformLayout();
      this.TableLayoutPanel3.ResumeLayout(false);
      this.TableLayoutPanel3.PerformLayout();
      this.RadLabel4.EndInit();
      this.RadLabel6.EndInit();
      this.RadLabel5.EndInit();
      this.RadLabel7.EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.gbDetails.EndInit();
      this.gbDetails.ResumeLayout(false);
      this.gbDetails.PerformLayout();
      this.RadLabel2.EndInit();
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.txtComments.EndInit();
      this.RadLabel3.EndInit();
      this.RadLabel1.EndInit();
      this.flpTools.ResumeLayout(false);
      this.flpTools.PerformLayout();
      this.lblMissionCritical.EndInit();
      this.chkMissionCritical.EndInit();
      this.Panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal virtual RadGroupBox gbSelection { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboSystems
    {
      get
      {
        return this._cboSystems;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboSystems_SelectedChangeCommitted);
        EventHandler eventHandler2 = new EventHandler(this.gbSelection_Click);
        ComboBox cboSystems1 = this._cboSystems;
        if (cboSystems1 != null)
        {
          cboSystems1.SelectionChangeCommitted -= eventHandler1;
          cboSystems1.Click -= eventHandler2;
        }
        this._cboSystems = value;
        ComboBox cboSystems2 = this._cboSystems;
        if (cboSystems2 == null)
          return;
        cboSystems2.SelectionChangeCommitted += eventHandler1;
        cboSystems2.Click += eventHandler2;
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
        EventHandler eventHandler1 = new EventHandler(this.cboComponents_SelectedChangeCommitted);
        EventHandler eventHandler2 = new EventHandler(this.gbSelection_Click);
        ComboBox cboComponents1 = this._cboComponents;
        if (cboComponents1 != null)
        {
          cboComponents1.SelectionChangeCommitted -= eventHandler1;
          cboComponents1.Click -= eventHandler2;
        }
        this._cboComponents = value;
        ComboBox cboComponents2 = this._cboComponents;
        if (cboComponents2 == null)
          return;
        cboComponents2.SelectionChangeCommitted += eventHandler1;
        cboComponents2.Click += eventHandler2;
      }
    }

    internal virtual ComboBox cboMaterialCategory
    {
      get
      {
        return this._cboMaterialCategory;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboMaterialCategory_SelectedChangeCommitted);
        EventHandler eventHandler2 = new EventHandler(this.gbSelection_Click);
        ComboBox materialCategory1 = this._cboMaterialCategory;
        if (materialCategory1 != null)
        {
          materialCategory1.SelectionChangeCommitted -= eventHandler1;
          materialCategory1.Click -= eventHandler2;
        }
        this._cboMaterialCategory = value;
        ComboBox materialCategory2 = this._cboMaterialCategory;
        if (materialCategory2 == null)
          return;
        materialCategory2.SelectionChangeCommitted += eventHandler1;
        materialCategory2.Click += eventHandler2;
      }
    }

    internal virtual ComboBox cboComponentType
    {
      get
      {
        return this._cboComponentType;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboComponentType_SelectedIndexChanged);
        EventHandler eventHandler2 = new EventHandler(this.gbSelection_Click);
        ComboBox cboComponentType1 = this._cboComponentType;
        if (cboComponentType1 != null)
        {
          cboComponentType1.SelectedIndexChanged -= eventHandler1;
          cboComponentType1.Click -= eventHandler2;
        }
        this._cboComponentType = value;
        ComboBox cboComponentType2 = this._cboComponentType;
        if (cboComponentType2 == null)
          return;
        cboComponentType2.SelectedIndexChanged += eventHandler1;
        cboComponentType2.Click += eventHandler2;
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

    internal virtual Button btnAddButton
    {
      get
      {
        return this._btnAddButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnAddButton_Click);
        Button btnAddButton1 = this._btnAddButton;
        if (btnAddButton1 != null)
          btnAddButton1.Click -= eventHandler;
        this._btnAddButton = value;
        Button btnAddButton2 = this._btnAddButton;
        if (btnAddButton2 == null)
          return;
        btnAddButton2.Click += eventHandler;
      }
    }

    internal virtual RadLabel RadLabel7 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadGroupBox gbDetails { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadTextBox txtComments { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtQuantity
    {
      get
      {
        return this._txtQuantity;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtQuantity_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtQuantity_KeyPress);
        EventHandler eventHandler = new EventHandler(this.txtQuantity_TextChanged);
        TextBox txtQuantity1 = this._txtQuantity;
        if (txtQuantity1 != null)
        {
          txtQuantity1.KeyDown -= keyEventHandler;
          txtQuantity1.KeyPress -= pressEventHandler;
          txtQuantity1.TextChanged -= eventHandler;
        }
        this._txtQuantity = value;
        TextBox txtQuantity2 = this._txtQuantity;
        if (txtQuantity2 == null)
          return;
        txtQuantity2.KeyDown += keyEventHandler;
        txtQuantity2.KeyPress += pressEventHandler;
        txtQuantity2.TextChanged += eventHandler;
      }
    }

    internal virtual FlowLayoutPanel flpTools { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Label lblUnitOfMeasure { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button cmdIncrease
    {
      get
      {
        return this._cmdIncrease;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdIncrease_Click);
        Button cmdIncrease1 = this._cmdIncrease;
        if (cmdIncrease1 != null)
          cmdIncrease1.Click -= eventHandler;
        this._cmdIncrease = value;
        Button cmdIncrease2 = this._cmdIncrease;
        if (cmdIncrease2 == null)
          return;
        cmdIncrease2.Click += eventHandler;
      }
    }

    internal virtual Button cmdDecrease
    {
      get
      {
        return this._cmdDecrease;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdDecrease_Click);
        Button cmdDecrease1 = this._cmdDecrease;
        if (cmdDecrease1 != null)
          cmdDecrease1.Click -= eventHandler;
        this._cmdDecrease = value;
        Button cmdDecrease2 = this._cmdDecrease;
        if (cmdDecrease2 == null)
          return;
        cmdDecrease2.Click += eventHandler;
      }
    }

    internal virtual Button cmdCalc
    {
      get
      {
        return this._cmdCalc;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCalc_Click);
        Button cmdCalc1 = this._cmdCalc;
        if (cmdCalc1 != null)
          cmdCalc1.Click -= eventHandler;
        this._cmdCalc = value;
        Button cmdCalc2 = this._cmdCalc;
        if (cmdCalc2 == null)
          return;
        cmdCalc2.Click += eventHandler;
      }
    }

    internal virtual RadLabel lblMissionCritical
    {
      get
      {
        return this._lblMissionCritical;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.lblMissionCritical_Click);
        RadLabel lblMissionCritical1 = this._lblMissionCritical;
        if (lblMissionCritical1 != null)
          lblMissionCritical1.Click -= eventHandler;
        this._lblMissionCritical = value;
        RadLabel lblMissionCritical2 = this._lblMissionCritical;
        if (lblMissionCritical2 == null)
          return;
        lblMissionCritical2.Click += eventHandler;
      }
    }

    internal virtual RadCheckBox chkMissionCritical { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public string LastAddedComponent
    {
      get
      {
        return this.m_LastAddedComponent;
      }
      set
      {
        this.m_LastAddedComponent = value;
      }
    }

    private void frmAddMissingComponents_Load(object sender, EventArgs e)
    {
      this.gbDetails.Enabled = false;
      this.m_bldg_ID = mdUtility.fMainForm.CurrentBldg;
      this.btnAddButton.Enabled = false;
      this.LoadSystems();
      this.cboSystems.SelectedIndex = -1;
      TextBox txtQuantity = this.txtQuantity;
      ref TextBox local1 = ref txtQuantity;
      Label lblUnitOfMeasure = this.lblUnitOfMeasure;
      ref Label local2 = ref lblUnitOfMeasure;
      AssessmentHelpers.TextBoxValidator textBoxValidator = new AssessmentHelpers.TextBoxValidator(ref local1, ref local2, AssessmentHelpers.TextBoxValidator.ValidationTypes.Quantity);
      this.lblUnitOfMeasure = lblUnitOfMeasure;
      this.txtQuantity = txtQuantity;
    }

    private void LoadSystems()
    {
      string sSQL = !mdUtility.UseUniformat ? "Select SYS_ID, SYS_DESC FROM RO_System WHERE IS_UII = 0" : "Select SYS_ID, SYS_DESC FROM RO_System WHERE IS_UII = -1";
      this.cboSystems.DisplayMember = "SYS_DESC";
      this.cboSystems.ValueMember = "SYS_ID";
      this.cboSystems.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
      this.cboSystems.Refresh();
      this.cboSystems.ResetText();
      this.cboComponents.Enabled = false;
      this.cboMaterialCategory.Enabled = false;
      this.cboComponentType.Enabled = false;
    }

    private void cboSystems_SelectedChangeCommitted(object sender, EventArgs e)
    {
      this.cboMaterialCategory.Enabled = false;
      this.cboComponentType.Enabled = false;
      this.cboMaterialCategory.SelectedIndex = -1;
      this.cboComponentType.SelectedIndex = -1;
      string str = Conversions.ToString(this.cboSystems.SelectedValue);
      string sSQL = !mdUtility.UseUniformat ? "SELECT RO_Component.[COMP_ID], RO_Component.COMP_DESC FROM RO_Component WHERE RO_Component.[comp_sys_link] =" + str + " AND [RO_Component].[IS_UII] = 0  ORDER BY RO_Component.COMP_DESC" : "SELECT RO_Component.[COMP_ID], RO_Component.COMP_DESC FROM RO_Component WHERE RO_Component.[comp_sys_link] =" + str + " AND [RO_Component].[IS_UII] = -1  ORDER BY RO_Component.COMP_DESC";
      this.cboComponents.ValueMember = "COMP_ID";
      this.cboComponents.DisplayMember = "COMP_DESC";
      this.cboComponents.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
      this.cboComponents.Refresh();
      this.cboComponents.Enabled = true;
      this.cboComponents.SelectedIndex = -1;
    }

    private void cboComponents_SelectedChangeCommitted(object sender, EventArgs e)
    {
      this.cboComponentType.Enabled = false;
      this.cboComponentType.SelectedIndex = -1;
      if (this.cboSystems.SelectedIndex == -1)
        return;
      string str = Conversions.ToString(this.cboComponents.SelectedValue);
      string sSQL = !mdUtility.UseUniformat ? "SELECT [MAT_CAT_ID], mat_cat_desc FROM qryMaterialCategories WHERE [cmc_comp_link]=" + str : "SELECT [MAT_CAT_ID], mat_cat_desc FROM qryMaterialCategoriesUniformat WHERE [cmc_comp_uii_link]=" + str;
      this.cboMaterialCategory.DisplayMember = "mat_cat_desc";
      this.cboMaterialCategory.ValueMember = "MAT_CAT_ID";
      this.cboMaterialCategory.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
      this.cboMaterialCategory.Refresh();
      if (((DataTable) this.cboMaterialCategory.DataSource).Rows.Count == 0)
      {
        this.cboMaterialCategory.ResetText();
        this.cboComponentType.ResetText();
        this.cboMaterialCategory.Enabled = false;
        this.cboComponentType.DataSource = (object) null;
      }
      else
        this.cboMaterialCategory.Enabled = true;
      this.cboMaterialCategory.SelectedIndex = -1;
    }

    private void cboMaterialCategory_SelectedChangeCommitted(object sender, EventArgs e)
    {
      if (this.cboSystems.SelectedIndex == -1)
        return;
      try
      {
        this.Cursor = Cursors.WaitCursor;
        object Right = (object) null;
        if (this.cboComponentType.SelectedItem != null)
          Right = RuntimeHelpers.GetObjectValue(((DataRowView) this.cboComponentType.SelectedItem).Row[1]);
        string str1 = Conversions.ToString(this.cboComponents.SelectedValue);
        string str2 = Conversions.ToString(this.cboMaterialCategory.SelectedValue);
        string sSQL;
        if (mdUtility.UseUniformat)
          sSQL = "SELECT [CMC_ID], comp_type_desc FROM qryComponentTypesUniformat WHERE [comp_id]= " + str1 + " AND [CMC_MCAT_LINK]= " + str2 + " ORDER BY COMP_TYPE_DESC";
        else
          sSQL = "SELECT [CMC_ID], comp_type_desc FROM qryComponentTypes WHERE [comp_id]= " + str1 + " AND [CMC_MCAT_LINK]= " + str2 + " ORDER BY COMP_TYPE_DESC";
        this.cboComponentType.DisplayMember = "comp_type_desc";
        this.cboComponentType.ValueMember = "CMC_ID";
        this.cboComponentType.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
        this.cboComponentType.Refresh();
        bool flag = false;
        if (Right == null)
        {
          this.cboComponentType.SelectedIndex = -1;
        }
        else
        {
          try
          {
            foreach (object obj in this.cboComponentType.Items)
            {
              object objectValue = RuntimeHelpers.GetObjectValue(obj);
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, (System.Type) null, "Row", new object[1]{ (object) 1 }, (string[]) null, (System.Type[]) null, (bool[]) null), Right, false))
              {
                this.cboComponentType.SelectedItem = RuntimeHelpers.GetObjectValue(objectValue);
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
            this.cboComponentType.SelectedIndex = -1;
        }
        if (this.cboComponentType.SelectedIndex != -1 & this.cboMaterialCategory.SelectedIndex != -1)
          this.lblUnitOfMeasure.Text = mdUtility.UOMforCMC(Conversions.ToLong(this.cboComponentType.SelectedValue));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, "frmMissingComponents", "cboMaterialCategory_SelectedIndexChanged");
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
        this.cboComponentType.Enabled = true;
      }
    }

    private void cboComponentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cboSystems.SelectedIndex == -1)
        return;
      try
      {
        if (this.cboComponentType.SelectedIndex != -1 & this.cboMaterialCategory.SelectedIndex != -1)
        {
          this.lblUnitOfMeasure.Text = mdUtility.UOMforCMC(Conversions.ToLong(this.cboComponentType.SelectedValue));
          this.btnAddButton.Enabled = true;
          this.txtQuantity.Focus();
          this.gbDetails.Enabled = true;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUnitOfMeasure.Text, "EA", false) == 0)
          {
            this.cmdIncrease.Visible = true;
            this.cmdDecrease.Visible = true;
            this.cmdCalc.Visible = false;
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUnitOfMeasure.Text, "SF", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUnitOfMeasure.Text, "SM", false) == 0)
          {
            this.cmdIncrease.Visible = false;
            this.cmdDecrease.Visible = false;
            this.cmdCalc.Visible = true;
          }
          else
          {
            this.cmdIncrease.Visible = false;
            this.cmdDecrease.Visible = false;
            this.cmdCalc.Visible = false;
          }
        }
        else
        {
          this.lblUnitOfMeasure.Text = (string) null;
          this.btnAddButton.Enabled = false;
          this.cmdIncrease.Visible = false;
          this.cmdDecrease.Visible = false;
          this.cmdCalc.Visible = false;
          this.gbDetails.Enabled = false;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, "frmMissingComponents", nameof (cboComponentType_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void gbSelection_Click(object sender, EventArgs e)
    {
      (sender as ComboBox).DroppedDown = true;
    }

    private void btnAddButton_Click(object sender, EventArgs e)
    {
      RadTextBox txtComments;
      string text = (txtComments = this.txtComments).Text;
      int num1 = MissingComponents.OkToSaveComments(ref text) ? 1 : 0;
      txtComments.Text = text;
      if (num1 == 0)
        return;
      try
      {
        double result;
        if (double.TryParse(this.txtQuantity.Text, out result))
        {
          if (mdUtility.fMainForm.miUnits.Checked)
            result /= mdUtility.MetricConversionFactor((long) Conversions.ToInteger(this.cboComponentType.SelectedValue));
          this.LastAddedComponent = MissingComponents.AddMissingComponent(this.m_bldg_ID, Conversions.ToInteger(this.cboComponentType.SelectedValue), result, this.chkMissionCritical.Checked, this.txtComments.Text, 0.0);
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.LastAddedComponent, "", false) > 0U)
            this.Close();
        }
        else
        {
          this.txtQuantity.Focus();
          int num2 = (int) MessageBox.Show("Valid Quantity Required");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "AddButtonClick");
        ProjectData.ClearProjectError();
      }
    }

    private void cmdIncrease_Click(object sender, EventArgs e)
    {
      if (Versioned.IsNumeric((object) this.txtQuantity.Text))
        this.txtQuantity.Text = Conversions.ToString(Conversions.ToDouble(this.txtQuantity.Text) + 1.0);
      else
        this.txtQuantity.Text = Conversions.ToString(1);
    }

    private void cmdDecrease_Click(object sender, EventArgs e)
    {
      if (Versioned.IsNumeric((object) this.txtQuantity.Text) && Conversions.ToDouble(this.txtQuantity.Text) > 1.0)
        this.txtQuantity.Text = Conversions.ToString(Conversions.ToDouble(this.txtQuantity.Text) - 1.0);
      else
        this.txtQuantity.Text = Conversions.ToString(1);
    }

    private void cmdCalc_Click(object sender, EventArgs e)
    {
      double area = new dlgCalculateArea().CalculateArea((Form) this, Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtQuantity.Text, "", false) != 0 ? Conversions.ToDouble(this.txtQuantity.Text) : 0.0);
      if (area == -1.0)
        return;
      this.txtQuantity.Text = Conversions.ToString(area);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void lblMissionCritical_Click(object sender, EventArgs e)
    {
      this.chkMissionCritical.Checked = !this.chkMissionCritical.Checked;
    }

    private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
    {
      this.nonNumberEntered = false;
      if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete))
        return;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUnitOfMeasure.Text, "EA", false) > 0U)
      {
        if (e.KeyCode != Keys.Decimal && e.KeyCode != Keys.OemPeriod || Strings.InStr(this.txtQuantity.Text, ".", CompareMethod.Binary) > 1)
          this.nonNumberEntered = true;
      }
      else
        this.nonNumberEntered = true;
    }

    private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.nonNumberEntered)
        return;
      e.Handled = true;
    }

    private void txtQuantity_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.txtQuantity.Text.StartsWith("."))
          return;
        this.txtQuantity.Text = "0.";
        this.txtQuantity.SelectionStart = this.txtQuantity.TextLength;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (txtQuantity_TextChanged));
        ProjectData.ClearProjectError();
      }
      finally
      {
      }
    }
  }
}
