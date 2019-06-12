// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.SaturationDialog
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
  public class SaturationDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelSaturation;
    private RadTrackBar radTrackBarValue;
    private RadSpinEditor radSpinEditorValue;
    private RadButton radButtonReset;

    public SaturationDialog(RadImageEditorElement imageEditorElement)
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

    protected override void WireEvents()
    {
      this.radSpinEditorValue.ValueChanged += new EventHandler(this.RadSpinEditorShift_ValueChanged);
      this.radTrackBarValue.Scroll += new ScrollEventHandler(this.RadTrackBarValue_Scroll);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorValue.ValueChanged -= new EventHandler(this.RadSpinEditorShift_ValueChanged);
      this.radTrackBarValue.Scroll -= new ScrollEventHandler(this.RadTrackBarValue_Scroll);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.SetSaturation((int) this.radSpinEditorValue.Value);
      this.result = DialogResult.OK;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("SaturationDialogTitle");
      this.radLabelSaturation.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("SaturationDialogSaturation");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("SaturationDialogReset");
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadTrackBarValue_Scroll(object sender, ScrollEventArgs e)
    {
      this.radSpinEditorValue.Value = (Decimal) this.radTrackBarValue.Value - new Decimal(100);
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarValue.Value = 0.0f;
      this.radSpinEditorValue.Value = new Decimal(0);
      this.ApplySettings();
      this.WireEvents();
      this.result = DialogResult.Cancel;
    }

    private void RadSpinEditorShift_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarValue.Value = (float) this.radSpinEditorValue.Value + 100f;
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
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.radLabelSaturation = new RadLabel();
      this.radTrackBarValue = new RadTrackBar();
      this.radSpinEditorValue = new RadSpinEditor();
      this.radButtonReset = new RadButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelSaturation.BeginInit();
      this.radTrackBarValue.BeginInit();
      this.radSpinEditorValue.BeginInit();
      this.radButtonReset.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelSaturation, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarValue, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorValue, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 2);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.Size = new Size(292, 110);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelSaturation.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelSaturation.Location = new Point(3, 15);
      this.radLabelSaturation.Name = "radLabelSaturation";
      this.radLabelSaturation.Size = new Size(58, 18);
      this.radLabelSaturation.TabIndex = 0;
      this.radLabelSaturation.Text = "Saturation";
      this.radTrackBarValue.BackColor = Color.Transparent;
      this.radTrackBarValue.LargeTickFrequency = 200;
      this.radTrackBarValue.Location = new Point(3, 37);
      this.radTrackBarValue.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarValue.Maximum = 200f;
      this.radTrackBarValue.Name = "radTrackBarValue";
      this.radTrackBarValue.Size = new Size(140, 34);
      this.radTrackBarValue.SmallTickFrequency = 0;
      this.radTrackBarValue.TabIndex = 1;
      this.radTrackBarValue.Value = 100f;
      this.radSpinEditorValue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorValue.DecimalPlaces = 1;
      this.radSpinEditorValue.Location = new Point(149, 44);
      this.radSpinEditorValue.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorValue.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 90);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (SaturationDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Saturation";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelSaturation.EndInit();
      this.radTrackBarValue.EndInit();
      this.radSpinEditorValue.EndInit();
      this.radButtonReset.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
