// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmMissingComponents
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmMissingComponents : Form
  {
    private IContainer components;
    private const string MOD_NAME = "frmMissingComponents";
    private string m_bldg_ID;
    private bool m_bInEditMode;
    private int m_iCurrentListBoxSelection;
    private bool quantityFocus;
    private bool commentFocus;

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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmMissingComponents));
      this.txtQuantity = new System.Windows.Forms.TextBox();
      this.txtComments = new System.Windows.Forms.TextBox();
      this.RadLabel1 = new RadLabel();
      this.RadLabel2 = new RadLabel();
      this.RadLabel3 = new RadLabel();
      this.btnDelete = new Button();
      this.lstboxMissingComponents = new RadListControl();
      this.btnClose = new Button();
      this.btnCancel = new Button();
      this.gbMissingList = new RadGroupBox();
      this.TableLayoutPanel4 = new TableLayoutPanel();
      this.btnAddButton = new RadSplitButton();
      this.AddMenuItem1 = new RadMenuItem();
      this.defaultItem = new RadMenuItem();
      this.TableLayoutPanel3 = new TableLayoutPanel();
      this.gbDetails = new RadGroupBox();
      this.TableLayoutPanel5 = new TableLayoutPanel();
      this.btnSave = new Button();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.lblSystem = new RadLabel();
      this.lblComponent = new RadLabel();
      this.flpTools = new FlowLayoutPanel();
      this.lblUnitOfMeasure = new System.Windows.Forms.Label();
      this.cmdIncrease = new Button();
      this.cmdDecrease = new Button();
      this.cmdCalc = new Button();
      this.RadLabel8 = new RadLabel();
      this.chkMissionCritical = new RadCheckBox();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.object_abcb0d77_4257_44f0_a1be_887225f514d0 = new RootRadElement();
      this.RadLabel1.BeginInit();
      this.RadLabel2.BeginInit();
      this.RadLabel3.BeginInit();
      this.lstboxMissingComponents.BeginInit();
      this.gbMissingList.BeginInit();
      this.gbMissingList.SuspendLayout();
      this.TableLayoutPanel4.SuspendLayout();
      this.btnAddButton.BeginInit();
      this.TableLayoutPanel3.SuspendLayout();
      this.gbDetails.BeginInit();
      this.gbDetails.SuspendLayout();
      this.TableLayoutPanel5.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.lblSystem.BeginInit();
      this.lblComponent.BeginInit();
      this.flpTools.SuspendLayout();
      this.RadLabel8.BeginInit();
      this.chkMissionCritical.BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.txtQuantity.Location = new Point(203, 27);
      this.txtQuantity.Margin = new Padding(3, 3, 3, 5);
      this.txtQuantity.MinimumSize = new Size(128, 27);
      this.txtQuantity.Name = "txtQuantity";
      this.txtQuantity.Size = new Size(128, 20);
      this.txtQuantity.TabIndex = 0;
      this.txtQuantity.TabStop = false;
      this.txtComments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel2.SetColumnSpan((Control) this.txtComments, 8);
      this.txtComments.Location = new Point(36, 55);
      this.txtComments.Multiline = true;
      this.txtComments.Name = "txtComments";
      this.txtComments.Size = new Size(609, 96);
      this.txtComments.TabIndex = 2;
      this.txtComments.TabStop = false;
      this.RadLabel1.Dock = DockStyle.Bottom;
      this.RadLabel1.Location = new Point(145, 31);
      this.RadLabel1.Name = "RadLabel1";
      this.RadLabel1.Size = new Size(52, 18);
      this.RadLabel1.TabIndex = 7;
      this.RadLabel1.Text = "Quantity:";
      this.RadLabel1.TextAlignment = ContentAlignment.MiddleRight;
      this.RadLabel2.Location = new Point(484, 76);
      this.RadLabel2.Name = "RadLabel2";
      this.RadLabel2.Size = new Size(2, 2);
      this.RadLabel2.TabIndex = 8;
      this.RadLabel3.Dock = DockStyle.Bottom;
      this.RadLabel3.Location = new Point(36, 31);
      this.RadLabel3.Name = "RadLabel3";
      this.RadLabel3.Size = new Size(63, 18);
      this.RadLabel3.TabIndex = 9;
      this.RadLabel3.Text = "Comments:";
      this.btnDelete.ForeColor = Color.Black;
      this.btnDelete.Location = new Point(367, 3);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(130, 24);
      this.btnDelete.TabIndex = 0;
      this.btnDelete.Text = "Delete";
      this.lstboxMissingComponents.Dock = DockStyle.Fill;
      this.lstboxMissingComponents.Location = new Point(36, 3);
      this.lstboxMissingComponents.Name = "lstboxMissingComponents";
      this.lstboxMissingComponents.Size = new Size(606, 233);
      this.lstboxMissingComponents.SortStyle = SortStyle.Ascending;
      this.lstboxMissingComponents.TabIndex = 0;
      this.lstboxMissingComponents.ThemeName = "CustomTelerikAll";
      this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnClose.Location = new Point(697, 3);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new Size(84, 24);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "Close";
      this.btnCancel.Anchor = AnchorStyles.Bottom;
      this.btnCancel.Location = new Point(368, 9);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(130, 24);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.gbMissingList.AccessibleRole = AccessibleRole.Grouping;
      this.gbMissingList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.gbMissingList.Controls.Add((Control) this.TableLayoutPanel4);
      this.gbMissingList.Controls.Add((Control) this.TableLayoutPanel3);
      this.gbMissingList.HeaderText = "Missing Components Selection List";
      this.gbMissingList.Location = new Point(3, 3);
      this.gbMissingList.Name = "gbMissingList";
      this.gbMissingList.Size = new Size(688, 318);
      this.gbMissingList.TabIndex = 1;
      this.gbMissingList.Text = "Missing Components Selection List";
      this.gbMissingList.ThemeName = "CustomTelerikAll";
      this.TableLayoutPanel4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel4.ColumnCount = 5;
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel4.Controls.Add((Control) this.btnAddButton, 1, 0);
      this.TableLayoutPanel4.Controls.Add((Control) this.btnDelete, 3, 0);
      this.TableLayoutPanel4.Location = new Point(5, 278);
      this.TableLayoutPanel4.Name = "TableLayoutPanel4";
      this.TableLayoutPanel4.RowCount = 1;
      this.TableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel4.Size = new Size(678, 35);
      this.TableLayoutPanel4.TabIndex = 1;
      this.btnAddButton.Items.AddRange((RadItem) this.AddMenuItem1, (RadItem) this.defaultItem);
      this.btnAddButton.Location = new Point(181, 3);
      this.btnAddButton.MinimumSize = new Size(130, 24);
      this.btnAddButton.Name = "btnAddButton";
      this.btnAddButton.RootElement.MinSize = new Size(130, 24);
      this.btnAddButton.Size = new Size(130, 24);
      this.btnAddButton.TabIndex = 0;
      this.btnAddButton.Text = "Add";
      this.btnAddButton.ThemeName = "CustomTelerikAll";
      this.btnAddButton.UseCompatibleTextRendering = false;
      ((RadItem) this.btnAddButton.GetChildAt(0)).Text = "Add";
      this.btnAddButton.GetChildAt(0).Margin = new Padding(0, 0, 0, 1);
      this.btnAddButton.GetChildAt(0).CanFocus = true;
      this.btnAddButton.GetChildAt(0).GetChildAt(0).Margin = new Padding(0);
      this.AddMenuItem1.AccessibleDescription = "Fire Sprinklers";
      this.AddMenuItem1.AccessibleName = "Fire Sprinklers";
      this.AddMenuItem1.Name = "AddMenuItem1";
      this.AddMenuItem1.Text = "Fire Sprinklers";
      this.defaultItem.AccessibleDescription = "RadMenuItem1";
      this.defaultItem.AccessibleName = "RadMenuItem1";
      this.defaultItem.Name = "defaultItem";
      this.defaultItem.Text = "RadMenuItem1";
      this.defaultItem.Visibility = ElementVisibility.Collapsed;
      this.TableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel3.ColumnCount = 3;
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33f));
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33f));
      this.TableLayoutPanel3.Controls.Add((Control) this.lstboxMissingComponents, 1, 0);
      this.TableLayoutPanel3.Location = new Point(5, 33);
      this.TableLayoutPanel3.Name = "TableLayoutPanel3";
      this.TableLayoutPanel3.RowCount = 1;
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel3.Size = new Size(678, 239);
      this.TableLayoutPanel3.TabIndex = 0;
      this.gbDetails.AccessibleRole = AccessibleRole.Grouping;
      this.gbDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.gbDetails.Controls.Add((Control) this.TableLayoutPanel5);
      this.gbDetails.Controls.Add((Control) this.RadLabel2);
      this.gbDetails.Controls.Add((Control) this.TableLayoutPanel2);
      this.gbDetails.ForeColor = Color.Black;
      this.gbDetails.HeaderText = "Missing Component Details";
      this.gbDetails.Location = new Point(3, 327);
      this.gbDetails.Name = "gbDetails";
      this.gbDetails.Size = new Size(688, 211);
      this.gbDetails.TabIndex = 25;
      this.gbDetails.Text = "Missing Component Details";
      this.gbDetails.ThemeName = "CustomTelerikAll";
      this.TableLayoutPanel5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel5.ColumnCount = 5;
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel5.Controls.Add((Control) this.btnSave, 1, 0);
      this.TableLayoutPanel5.Controls.Add((Control) this.btnCancel, 3, 0);
      this.TableLayoutPanel5.Location = new Point(2, 170);
      this.TableLayoutPanel5.Name = "TableLayoutPanel5";
      this.TableLayoutPanel5.RowCount = 1;
      this.TableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel5.Size = new Size(681, 36);
      this.TableLayoutPanel5.TabIndex = 29;
      this.btnSave.Anchor = AnchorStyles.Bottom;
      this.btnSave.Location = new Point(182, 9);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(130, 24);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "Save";
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
      this.TableLayoutPanel2.Controls.Add((Control) this.lblSystem, 1, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtComments, 1, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.RadLabel3, 1, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.lblComponent, 5, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.RadLabel1, 3, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtQuantity, 4, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.flpTools, 5, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.RadLabel8, 6, 1);
      this.TableLayoutPanel2.Controls.Add((Control) this.chkMissionCritical, 7, 1);
      this.TableLayoutPanel2.Location = new Point(2, 18);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 3;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.Size = new Size(681, 154);
      this.TableLayoutPanel2.TabIndex = 28;
      this.TableLayoutPanel2.SetColumnSpan((Control) this.lblSystem, 4);
      this.lblSystem.Dock = DockStyle.Bottom;
      this.lblSystem.Location = new Point(36, 3);
      this.lblSystem.Name = "lblSystem";
      this.lblSystem.Size = new Size(295, 18);
      this.lblSystem.TabIndex = 13;
      this.lblSystem.Text = "System:";
      this.TableLayoutPanel2.SetColumnSpan((Control) this.lblComponent, 5);
      this.lblComponent.Dock = DockStyle.Bottom;
      this.lblComponent.Location = new Point(337, 3);
      this.lblComponent.Name = "lblComponent";
      this.lblComponent.Size = new Size(68, 18);
      this.lblComponent.TabIndex = 14;
      this.lblComponent.Text = "Component:";
      this.flpTools.AutoSize = true;
      this.flpTools.Controls.Add((Control) this.lblUnitOfMeasure);
      this.flpTools.Controls.Add((Control) this.cmdIncrease);
      this.flpTools.Controls.Add((Control) this.cmdDecrease);
      this.flpTools.Controls.Add((Control) this.cmdCalc);
      this.flpTools.Dock = DockStyle.Bottom;
      this.flpTools.Location = new Point(334, 29);
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
      this.cmdIncrease.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
      this.cmdIncrease.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdIncrease.Location = new Point(13, 0);
      this.cmdIncrease.Margin = new Padding(0);
      this.cmdIncrease.Name = "cmdIncrease";
      this.cmdIncrease.Size = new Size(23, 23);
      this.cmdIncrease.TabIndex = 14;
      this.cmdIncrease.UseVisualStyleBackColor = true;
      this.cmdIncrease.Visible = false;
      this.cmdDecrease.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Symbol_Restricted_2;
      this.cmdDecrease.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdDecrease.Location = new Point(36, 0);
      this.cmdDecrease.Margin = new Padding(0);
      this.cmdDecrease.Name = "cmdDecrease";
      this.cmdDecrease.Size = new Size(23, 23);
      this.cmdDecrease.TabIndex = 15;
      this.cmdDecrease.UseVisualStyleBackColor = true;
      this.cmdDecrease.Visible = false;
      this.cmdCalc.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Calculator_Accounting;
      this.cmdCalc.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCalc.Location = new Point(59, 0);
      this.cmdCalc.Margin = new Padding(0);
      this.cmdCalc.Name = "cmdCalc";
      this.cmdCalc.Size = new Size(23, 23);
      this.cmdCalc.TabIndex = 16;
      this.cmdCalc.UseVisualStyleBackColor = true;
      this.cmdCalc.Visible = false;
      this.RadLabel8.Dock = DockStyle.Bottom;
      this.RadLabel8.Location = new Point(457, 31);
      this.RadLabel8.Name = "RadLabel8";
      this.RadLabel8.Size = new Size(84, 18);
      this.RadLabel8.TabIndex = 1;
      this.RadLabel8.Text = "Mission Critical:";
      this.RadLabel8.TextAlignment = ContentAlignment.MiddleRight;
      this.chkMissionCritical.Dock = DockStyle.Bottom;
      this.chkMissionCritical.Location = new Point(547, 27);
      this.chkMissionCritical.Name = "chkMissionCritical";
      this.chkMissionCritical.Size = new Size(22, 22);
      this.chkMissionCritical.TabIndex = 1;
      this.chkMissionCritical.GetChildAt(0).GetChildAt(1).GetChildAt(1).ScaleTransform = new SizeF(1f, 1f);
      this.chkMissionCritical.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(2).ScaleTransform = new SizeF(1.5f, 1.5f);
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90f));
      this.TableLayoutPanel1.Controls.Add((Control) this.gbDetails, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnClose, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.gbMissingList, 0, 0);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 40f));
      this.TableLayoutPanel1.Size = new Size(784, 541);
      this.TableLayoutPanel1.TabIndex = 4;
      this.object_abcb0d77_4257_44f0_a1be_887225f514d0.Name = "object_abcb0d77_4257_44f0_a1be_887225f514d0";
      this.object_abcb0d77_4257_44f0_a1be_887225f514d0.StretchHorizontally = true;
      this.object_abcb0d77_4257_44f0_a1be_887225f514d0.StretchVertically = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(784, 541);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(800, 550);
      this.Name = nameof (frmMissingComponents);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Missing Components List";
      this.RadLabel1.EndInit();
      this.RadLabel2.EndInit();
      this.RadLabel3.EndInit();
      this.lstboxMissingComponents.EndInit();
      this.gbMissingList.EndInit();
      this.gbMissingList.ResumeLayout(false);
      this.TableLayoutPanel4.ResumeLayout(false);
      this.btnAddButton.EndInit();
      this.TableLayoutPanel3.ResumeLayout(false);
      this.gbDetails.EndInit();
      this.gbDetails.ResumeLayout(false);
      this.gbDetails.PerformLayout();
      this.TableLayoutPanel5.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.lblSystem.EndInit();
      this.lblComponent.EndInit();
      this.flpTools.ResumeLayout(false);
      this.flpTools.PerformLayout();
      this.RadLabel8.EndInit();
      this.chkMissionCritical.EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual System.Windows.Forms.TextBox txtQuantity
    {
      get
      {
        return this._txtQuantity;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Details_Focus);
        System.Windows.Forms.TextBox txtQuantity1 = this._txtQuantity;
        if (txtQuantity1 != null)
          txtQuantity1.GotFocus -= eventHandler;
        this._txtQuantity = value;
        System.Windows.Forms.TextBox txtQuantity2 = this._txtQuantity;
        if (txtQuantity2 == null)
          return;
        txtQuantity2.GotFocus += eventHandler;
      }
    }

    internal virtual System.Windows.Forms.TextBox txtComments
    {
      get
      {
        return this._txtComments;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Details_Focus);
        System.Windows.Forms.TextBox txtComments1 = this._txtComments;
        if (txtComments1 != null)
          txtComments1.GotFocus -= eventHandler;
        this._txtComments = value;
        System.Windows.Forms.TextBox txtComments2 = this._txtComments;
        if (txtComments2 == null)
          return;
        txtComments2.GotFocus += eventHandler;
      }
    }

    internal virtual RadLabel RadLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnDelete
    {
      get
      {
        return this._btnDelete;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnDelete_Click);
        Button btnDelete1 = this._btnDelete;
        if (btnDelete1 != null)
          btnDelete1.Click -= eventHandler;
        this._btnDelete = value;
        Button btnDelete2 = this._btnDelete;
        if (btnDelete2 == null)
          return;
        btnDelete2.Click += eventHandler;
      }
    }

    internal virtual RadListControl lstboxMissingComponents
    {
      get
      {
        return this._lstboxMissingComponents;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.listMissingComponents_SelectedIndexChanged);
        VisualListItemFormattingEventHandler formattingEventHandler = new VisualListItemFormattingEventHandler(this.lstboxMissingComponents_VisualItemFormatting);
        Telerik.WinControls.UI.Data.PositionChangingEventHandler changingEventHandler = new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.lstBoxMissingComponents_SelectedItemChanging);
        RadListControl missingComponents1 = this._lstboxMissingComponents;
        if (missingComponents1 != null)
        {
          missingComponents1.SelectedValueChanged -= eventHandler;
          missingComponents1.VisualItemFormatting -= formattingEventHandler;
          missingComponents1.SelectedIndexChanging -= changingEventHandler;
        }
        this._lstboxMissingComponents = value;
        RadListControl missingComponents2 = this._lstboxMissingComponents;
        if (missingComponents2 == null)
          return;
        missingComponents2.SelectedValueChanged += eventHandler;
        missingComponents2.VisualItemFormatting += formattingEventHandler;
        missingComponents2.SelectedIndexChanging += changingEventHandler;
      }
    }

    internal virtual Button btnClose
    {
      get
      {
        return this._btnClose;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnClose_Click);
        Button btnClose1 = this._btnClose;
        if (btnClose1 != null)
          btnClose1.Click -= eventHandler;
        this._btnClose = value;
        Button btnClose2 = this._btnClose;
        if (btnClose2 == null)
          return;
        btnClose2.Click += eventHandler;
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
        EventHandler eventHandler = new EventHandler(this.ExitEditMode);
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

    internal virtual RadGroupBox gbMissingList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadGroupBox gbDetails { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel8 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblComponent { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblSystem { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual FlowLayoutPanel flpTools { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblUnitOfMeasure { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

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

    internal virtual RootRadElement object_abcb0d77_4257_44f0_a1be_887225f514d0 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadSplitButton btnAddButton { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadCheckBox chkMissionCritical
    {
      get
      {
        return this._chkMissionCritical;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Details_Focus);
        RadCheckBox chkMissionCritical1 = this._chkMissionCritical;
        if (chkMissionCritical1 != null)
          chkMissionCritical1.Click -= eventHandler;
        this._chkMissionCritical = value;
        RadCheckBox chkMissionCritical2 = this._chkMissionCritical;
        if (chkMissionCritical2 == null)
          return;
        chkMissionCritical2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

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

    internal virtual TableLayoutPanel TableLayoutPanel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadMenuItem AddMenuItem1
    {
      get
      {
        return this._AddMenuItem1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnSprinklers_Click);
        RadMenuItem addMenuItem1_1 = this._AddMenuItem1;
        if (addMenuItem1_1 != null)
          addMenuItem1_1.Click -= eventHandler;
        this._AddMenuItem1 = value;
        RadMenuItem addMenuItem1_2 = this._AddMenuItem1;
        if (addMenuItem1_2 == null)
          return;
        addMenuItem1_2.Click += eventHandler;
      }
    }

    internal virtual RadMenuItem defaultItem
    {
      get
      {
        return this._defaultItem;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnAddButton_Click);
        RadMenuItem defaultItem1 = this._defaultItem;
        if (defaultItem1 != null)
          defaultItem1.Click -= eventHandler;
        this._defaultItem = value;
        RadMenuItem defaultItem2 = this._defaultItem;
        if (defaultItem2 == null)
          return;
        defaultItem2.Click += eventHandler;
      }
    }

    private void frmMissingComponents_Load(object sender, EventArgs e)
    {
      this.btnSave.Visible = false;
      this.btnCancel.Visible = false;
      if (!mdUtility.UseSprinklerButton)
        this.AddMenuItem1.Visibility = ElementVisibility.Hidden;
      this.m_bldg_ID = mdUtility.fMainForm.CurrentBldg;
      this.RefreshMissingComponentList();
      this.lstboxMissingComponents.SelectedIndex = -1;
      this.ResetDetailsBox();
      this.btnAddButton.DefaultItem = (RadItem) this.defaultItem;
      this.txtQuantity.Click += new EventHandler(this.TextBoxItem_Click);
      this.txtComments.Click += new EventHandler(this.TextBoxItem_Click);
      System.Windows.Forms.TextBox txtQuantity = this.txtQuantity;
      ref System.Windows.Forms.TextBox local1 = ref txtQuantity;
      System.Windows.Forms.Label lblUnitOfMeasure = this.lblUnitOfMeasure;
      ref System.Windows.Forms.Label local2 = ref lblUnitOfMeasure;
      AssessmentHelpers.TextBoxValidator textBoxValidator = new AssessmentHelpers.TextBoxValidator(ref local1, ref local2, AssessmentHelpers.TextBoxValidator.ValidationTypes.Quantity);
      this.lblUnitOfMeasure = lblUnitOfMeasure;
      this.txtQuantity = txtQuantity;
    }

    private void listMissingComponents_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lstboxMissingComponents.SelectedValue == null)
        return;
      this.btnDelete.Enabled = true;
      DataRow row1 = mdUtility.DB.GetDataTable("SELECT *  FROM Missing_Components WHERE [MissingComponent_ID]='" + Conversions.ToString(this.lstboxMissingComponents.SelectedValue) + "'").Rows[0];
      this.txtComments.Text = Conversions.ToString(row1["MC_Comments"]);
      this.chkMissionCritical.Checked = Conversions.ToBoolean(row1["MC_MissionCritical"]);
      this.lblUnitOfMeasure.Text = mdUtility.UOMforCMC(Conversions.ToLong(row1["MC_CMC_ID"]));
      if (mdUtility.fMainForm.miUnits.Checked)
      {
        System.Windows.Forms.TextBox txtQuantity = this.txtQuantity;
        double dblTemp = Conversions.ToDouble(Microsoft.VisualBasic.CompilerServices.Operators.MultiplyObject(row1["MC_Quantity"], (object) mdUtility.MetricConversionFactor(Conversions.ToLong(row1["MC_CMC_ID"]))));
        string str = mdUtility.FormatDoubleForDisplay(ref dblTemp);
        txtQuantity.Text = str;
      }
      else
      {
        System.Windows.Forms.TextBox txtQuantity = this.txtQuantity;
        DataRow dataRow = row1;
        double dblTemp = Conversions.ToDouble(dataRow["MC_Quantity"]);
        string str1 = mdUtility.FormatDoubleForDisplay(ref dblTemp);
        dataRow["MC_Quantity"] = (object) dblTemp;
        string str2 = str1;
        txtQuantity.Text = str2;
      }
      DataRow row2 = mdUtility.DB.GetDataTable(!mdUtility.UseUniformat ? Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT  C.COMP_DESC, S.SYS_DESC, CMC.CMC_ID FROM RO_CMC CMC INNER JOIN (RO_Component C INNER JOIN RO_System S ON C.COMP_SYS_LINK = S.SYS_ID) ON CMC.CMC_COMP_LINK = C.COMP_ID WHERE(CMC.CMC_ID = ", row1["MC_CMC_ID"]), (object) ")")) : Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT  C.COMP_DESC, S.SYS_DESC, CMC.CMC_ID FROM RO_CMC CMC INNER JOIN (RO_Component C INNER JOIN RO_System S ON C.COMP_SYS_LINK = S.SYS_ID) ON CMC.CMC_COMP_UII_LINK = C.COMP_ID WHERE(CMC.CMC_ID = ", row1["MC_CMC_ID"]), (object) ")"))).Rows[0];
      this.lblSystem.Text = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "System: ", row2["SYS_DESC"]));
      this.lblComponent.Text = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "Component: ", row2["COMP_DESC"]));
    }

    private void btnAddButton_Click(object sender, EventArgs e)
    {
      frmAddMissingComponent missingComponent = new frmAddMissingComponent();
      try
      {
        this.Cursor = Cursors.WaitCursor;
        this.m_iCurrentListBoxSelection = this.lstboxMissingComponents.SelectedIndex;
        int num = (int) missingComponent.ShowDialog();
        this.RefreshMissingComponentList();
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(missingComponent.LastAddedComponent, "", false) > 0U)
          this.lstboxMissingComponents.SelectedValue = (object) missingComponent.LastAddedComponent;
        else
          this.lstboxMissingComponents.SelectedIndex = this.m_iCurrentListBoxSelection;
        missingComponent.Dispose();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Open Add Missing Components Form");
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      double result;
      if (!double.TryParse(this.txtQuantity.Text, out result))
      {
        this.txtQuantity.Focus();
        int num = (int) MessageBox.Show("Valid Quantity Required");
      }
      else
      {
        System.Windows.Forms.TextBox txtComments;
        string text = (txtComments = this.txtComments).Text;
        int num = MissingComponents.OkToSaveComments(ref text) ? 1 : 0;
        txtComments.Text = text;
        if (num == 0)
          return;
        string str = "SELECT *  FROM Missing_Components WHERE [MissingComponent_ID]='" + Conversions.ToString(this.lstboxMissingComponents.SelectedValue) + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        DataRow row = dataTable.Rows[0];
        DataRow dataRow = row;
        dataRow["MC_Quantity"] = !mdUtility.fMainForm.miUnits.Checked ? (object) this.txtQuantity.Text : (object) (Conversions.ToDouble(this.txtQuantity.Text) / mdUtility.MetricConversionFactor(Conversions.ToLong(row["MC_CMC_ID"])));
        dataRow["MC_Comments"] = (object) this.txtComments.Text;
        dataRow["MC_MissionCritical"] = (object) this.chkMissionCritical.Checked;
        mdUtility.DB.SaveDataTable(ref dataTable, str);
        this.ExitEditMode((object) this.btnCancel, EventArgs.Empty);
      }
    }

    private void ExitEditMode(object sender, EventArgs e)
    {
      this.btnDelete.Enabled = true;
      this.btnCancel.Visible = false;
      this.btnSave.Visible = false;
      this.btnAddButton.Enabled = true;
      this.cmdIncrease.Visible = false;
      this.cmdDecrease.Visible = false;
      this.cmdCalc.Visible = false;
      this.quantityFocus = false;
      this.commentFocus = false;
      this.gbDetails.Text = "Missing Component Details";
      this.gbDetails.HeaderAlignment = HeaderAlignment.Near;
      this.m_bInEditMode = false;
      int selectedIndex = this.lstboxMissingComponents.SelectedIndex;
      this.RefreshMissingComponentList();
      this.lstboxMissingComponents.SelectedIndex = selectedIndex;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      this.m_iCurrentListBoxSelection = this.lstboxMissingComponents.SelectedIndex;
      if (this.m_iCurrentListBoxSelection <= -1 || Interaction.MsgBox((object) "Are you sure?", MsgBoxStyle.YesNo, (object) "Delete") != MsgBoxResult.Yes)
        return;
      string str = "SELECT *  FROM Missing_Components WHERE [MissingComponent_ID]='" + Conversions.ToString(this.lstboxMissingComponents.SelectedValue) + "'";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      dataTable.Rows[0].Delete();
      mdUtility.DB.SaveDataTable(ref dataTable, str);
      this.RefreshMissingComponentList();
      this.ResetDetailsBox();
      this.lstboxMissingComponents.SelectedIndex = this.lstboxMissingComponents.Items.Count != this.m_iCurrentListBoxSelection ? this.m_iCurrentListBoxSelection : checked (this.m_iCurrentListBoxSelection - 1);
      this.listMissingComponents_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void RefreshMissingComponentList()
    {
      string sSQL;
      if (mdUtility.UseUniformat)
      {
        string str = "SELECT  C.COMP_DESC, S.SYS_DESC, CMC.CMC_ID FROM RO_CMC CMC INNER JOIN (RO_Component C INNER JOIN RO_System S ON C.COMP_SYS_LINK = S.SYS_ID) ON CMC.CMC_COMP_UII_LINK = C.COMP_ID WHERE(CMC.CMC_ID = " + Conversions.ToString(1) + ")";
        sSQL = "SELECT Missing_Components.MissingComponent_ID, RO_Component.COMP_DESC & \" - \" & RO_Material_Category.MAT_CAT_DESC & IIf([comp_type_desc]=\"N/A\",\"\",\" - \" & [comp_type_desc]) AS combine_Label FROM (RO_CMC INNER JOIN ((Missing_Components INNER JOIN qryComponentTypesUniformat ON Missing_Components.MC_CMC_ID = qryComponentTypesUniformat.CMC_ID) INNER JOIN RO_Material_Category ON qryComponentTypesUniformat.CMC_MCAT_LINK = RO_Material_Category.MAT_CAT_ID) ON RO_CMC.CMC_ID = Missing_Components.MC_CMC_ID) INNER JOIN RO_Component ON RO_CMC.CMC_COMP_UII_LINK = RO_Component.COMP_ID WHERE (((Missing_Components.Building_ID)='" + this.m_bldg_ID + "'))";
      }
      else
        sSQL = "SELECT Missing_Components.MissingComponent_ID, RO_Component.COMP_DESC & \" - \" & RO_Material_Category.MAT_CAT_DESC & IIf([comp_type_desc]=\"N/A\",\"\",\" - \" & [comp_type_desc]) AS combine_Label FROM (RO_CMC INNER JOIN ((Missing_Components INNER JOIN qryComponentTypes ON Missing_Components.MC_CMC_ID = qryComponentTypes.CMC_ID) INNER JOIN RO_Material_Category ON qryComponentTypes.CMC_MCAT_LINK = RO_Material_Category.MAT_CAT_ID) ON RO_CMC.CMC_ID = Missing_Components.MC_CMC_ID) INNER JOIN RO_Component ON RO_CMC.CMC_COMP_UII_LINK = RO_Component.COMP_ID WHERE (((Missing_Components.Building_ID)='" + this.m_bldg_ID + "'))";
      this.lstboxMissingComponents.DisplayMember = "combine_Label";
      this.lstboxMissingComponents.ValueMember = "MissingComponent_ID";
      this.lstboxMissingComponents.DataSource = (object) mdUtility.DB.GetDataTable(sSQL);
    }

    private void lstboxMissingComponents_VisualItemFormatting(
      object sender,
      VisualItemFormattingEventArgs args)
    {
      args.VisualItem.ToolTipText = args.VisualItem.Text;
    }

    private void ResetDetailsBox()
    {
      this.txtQuantity.ResetText();
      this.txtComments.ResetText();
      this.lblUnitOfMeasure.ResetText();
      this.chkMissionCritical.Checked = false;
      this.lblComponent.ResetText();
      this.lblSystem.ResetText();
    }

    private void Edit_Details(object sender)
    {
      this.m_bInEditMode = true;
      this.m_iCurrentListBoxSelection = this.lstboxMissingComponents.SelectedIndex;
      this.btnDelete.Enabled = false;
      this.btnSave.Visible = true;
      this.btnCancel.Visible = true;
      this.btnAddButton.Enabled = false;
      this.gbDetails.Text = "Edit Details";
      this.gbDetails.HeaderAlignment = HeaderAlignment.Center;
      this.btnDelete.Enabled = false;
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

    private void lstBoxMissingComponents_SelectedItemChanging(
      object sender,
      PositionChangingCancelEventArgs e)
    {
      if (!this.m_bInEditMode)
        return;
      e.Cancel = true;
    }

    private void Details_Focus(object sender, EventArgs e)
    {
      if (this.lstboxMissingComponents.SelectedIndex == -1)
        return;
      this.Edit_Details(RuntimeHelpers.GetObjectValue(sender));
    }

    private void cmdIncrease_Click(object sender, EventArgs e)
    {
      if (Versioned.IsNumeric((object) this.txtQuantity.Text))
      {
        System.Windows.Forms.TextBox txtQuantity = this.txtQuantity;
        double dblTemp = Conversions.ToDouble(this.txtQuantity.Text) + 1.0;
        string str = mdUtility.FormatDoubleForDisplay(ref dblTemp);
        txtQuantity.Text = str;
      }
      else
        this.txtQuantity.Text = Conversions.ToString(1);
    }

    private void cmdDecrease_Click(object sender, EventArgs e)
    {
      if (Versioned.IsNumeric((object) this.txtQuantity.Text) && Conversions.ToDouble(this.txtQuantity.Text) > 1.0)
      {
        System.Windows.Forms.TextBox txtQuantity = this.txtQuantity;
        double dblTemp = Conversions.ToDouble(this.txtQuantity.Text) - 1.0;
        string str = mdUtility.FormatDoubleForDisplay(ref dblTemp);
        txtQuantity.Text = str;
      }
      else
        this.txtQuantity.Text = Conversions.ToString(1);
    }

    private void cmdCalc_Click(object sender, EventArgs e)
    {
      double area = new dlgCalculateArea().CalculateArea((Form) this, Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtQuantity.Text, "", false) != 0 ? Conversions.ToDouble(this.txtQuantity.Text) : 0.0);
      if (area == -1.0)
        return;
      this.txtQuantity.Text = mdUtility.FormatDoubleForDisplay(ref area);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnSprinklers_Click(object sender, EventArgs e)
    {
      double coverage = new dlgCoverage().GetCoverage((Form) this, 5061);
      if (coverage == -1.0)
        return;
      string str = MissingComponents.AddMissingComponent(this.m_bldg_ID, 5061, 0.0, false, "", coverage);
      this.RefreshMissingComponentList();
      this.lstboxMissingComponents.SelectedValue = (object) str;
    }

    public frmMissingComponents()
    {
      this.Load += new EventHandler(this.frmMissingComponents_Load);
      this.quantityFocus = false;
      this.commentFocus = false;
      this.InitializeComponent();
      float num = this.CreateGraphics().DpiX / 96f;
      this.cmdIncrease.Size = new Size(checked ((int) Math.Round(unchecked ((double) this.cmdIncrease.Width * (double) num))), checked ((int) Math.Round(unchecked ((double) this.cmdIncrease.Height * (double) num))));
      this.cmdDecrease.Size = this.cmdIncrease.Size;
      this.cmdCalc.Size = this.cmdIncrease.Size;
    }

    private void TextBoxItem_Click(object sender, EventArgs e)
    {
      if (this.lstboxMissingComponents.SelectedIndex == -1)
        return;
      if ((System.Windows.Forms.TextBox) sender == this.txtQuantity && !this.quantityFocus)
      {
        this.txtQuantity.SelectAll();
        this.quantityFocus = true;
        this.commentFocus = false;
      }
      if ((System.Windows.Forms.TextBox) sender == this.txtComments && !this.commentFocus)
      {
        this.txtComments.SelectAll();
        this.quantityFocus = false;
        this.commentFocus = true;
      }
    }
  }
}
