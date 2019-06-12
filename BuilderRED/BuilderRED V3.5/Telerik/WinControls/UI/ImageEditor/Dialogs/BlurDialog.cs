// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.BlurDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI.ImageEditor.Dialogs
{
  public class BlurDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadTrackBar radTrackBarValue;
    private RadSpinEditor radSpinEditorValue;
    private RadButton radButtonReset;
    private RadLabel radLabelAmount;

    public BlurDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.radTrackBarValue.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 135), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 90), this.RootElement.DpiScaleFactor);
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("BlurDialogTitle");
      this.radLabelAmount.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("BlurDialogAmount");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("BlurDialogReset");
    }

    protected override void WireEvents()
    {
      this.radSpinEditorValue.ValueChanged += new EventHandler(this.RadSpinEditorValue_ValueChanged);
      this.radTrackBarValue.Scroll += new ScrollEventHandler(this.RadTrackBarValue_Scroll);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorValue.ValueChanged -= new EventHandler(this.RadSpinEditorValue_ValueChanged);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.Blur((int) this.radSpinEditorValue.Value);
      this.result = DialogResult.OK;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.radSpinEditorValue.Value = new Decimal(0);
      this.result = DialogResult.Cancel;
    }

    private void RadTrackBarValue_Scroll(object sender, ScrollEventArgs e)
    {
      this.radSpinEditorValue.Value = (Decimal) this.radTrackBarValue.Value;
    }

    private void RadSpinEditorValue_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarValue.Value = (float) this.radSpinEditorValue.Value;
      this.WireEvents();
      this.ApplySettings();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radTrackBarValue = new RadTrackBar();
      this.radSpinEditorValue = new RadSpinEditor();
      this.radButtonReset = new RadButton();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.radLabelAmount = new RadLabel();
      this.radTrackBarValue.BeginInit();
      this.radSpinEditorValue.BeginInit();
      this.radButtonReset.BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelAmount.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radTrackBarValue.BackColor = Color.Transparent;
      this.radTrackBarValue.Location = new Point(3, 36);
      this.radTrackBarValue.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarValue.Maximum = 100f;
      this.radTrackBarValue.Name = "radTrackBarValue";
      this.radTrackBarValue.Size = new Size(140, 36);
      this.radTrackBarValue.SmallTickFrequency = 0;
      this.radTrackBarValue.TabIndex = 1;
      this.radSpinEditorValue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorValue.Location = new Point(149, 44);
      this.radSpinEditorValue.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorValue.Name = "radSpinEditorValue";
      this.radSpinEditorValue.Size = new Size(140, 20);
      this.radSpinEditorValue.TabIndex = 2;
      this.radSpinEditorValue.TabStop = false;
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(179, 83);
      this.radButtonReset.Name = "radButtonReset";
      this.radButtonReset.Size = new Size(110, 24);
      this.radButtonReset.TabIndex = 3;
      this.radButtonReset.Text = "Reset";
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarValue, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorValue, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelAmount, 0, 0);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.Size = new Size(292, 100);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelAmount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelAmount.Location = new Point(3, 15);
      this.radLabelAmount.Name = "radLabelAmount";
      this.radLabelAmount.Size = new Size(47, 18);
      this.radLabelAmount.TabIndex = 4;
      this.radLabelAmount.Text = "Amount";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 90);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (BlurDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Blur";
      this.radTrackBarValue.EndInit();
      this.radSpinEditorValue.EndInit();
      this.radButtonReset.EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelAmount.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
