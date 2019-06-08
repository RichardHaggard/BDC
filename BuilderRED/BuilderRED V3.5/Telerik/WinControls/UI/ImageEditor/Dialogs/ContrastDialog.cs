// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.ContrastDialog
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
  public class ContrastDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelBrightness;
    private RadLabel radLabelContrast;
    private RadTrackBar radTrackBarBrightness;
    private RadTrackBar radTrackBarContrast;
    private RadSpinEditor radSpinEditorBrightness;
    private RadSpinEditor radSpinEditorContrast;
    private RadButton radButtonReset;

    public ContrastDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.radTrackBarBrightness.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
      this.radTrackBarContrast.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 240), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 160), this.RootElement.DpiScaleFactor);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.SetContrastAndBrightness((int) this.radSpinEditorContrast.Value, (int) this.radSpinEditorBrightness.Value);
      this.result = DialogResult.OK;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ContrastAndBrightnessDialogTitle");
      this.radLabelBrightness.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ContrastAndBrightnessDialogBrightness");
      this.radLabelContrast.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ContrastAndBrightnessDialogContrast");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ContrastAndBrightnessDialogReset");
    }

    protected override void WireEvents()
    {
      this.radSpinEditorBrightness.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorContrast.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radTrackBarBrightness.Scroll += new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarContrast.Scroll += new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorBrightness.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorContrast.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radTrackBarBrightness.Scroll -= new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarContrast.Scroll -= new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadTrackBar_Scroll(object sender, ScrollEventArgs e)
    {
      this.radSpinEditorBrightness.Value = (Decimal) (this.radTrackBarBrightness.Value - 100f);
      this.radSpinEditorContrast.Value = (Decimal) (this.radTrackBarContrast.Value - 100f);
    }

    private void RadSpinEditor_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarBrightness.Value = (float) this.radSpinEditorBrightness.Value + 100f;
      this.radTrackBarContrast.Value = (float) this.radSpinEditorContrast.Value + 100f;
      this.WireEvents();
      this.ApplySettings();
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarBrightness.Value = 100f;
      this.radSpinEditorBrightness.Value = new Decimal(0);
      this.radTrackBarContrast.Value = 100f;
      this.radSpinEditorContrast.Value = new Decimal(0);
      this.ApplySettings();
      this.WireEvents();
      this.result = DialogResult.Cancel;
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
      this.radLabelContrast = new RadLabel();
      this.radTrackBarBrightness = new RadTrackBar();
      this.radTrackBarContrast = new RadTrackBar();
      this.radSpinEditorBrightness = new RadSpinEditor();
      this.radSpinEditorContrast = new RadSpinEditor();
      this.radButtonReset = new RadButton();
      this.radLabelBrightness = new RadLabel();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelContrast.BeginInit();
      this.radTrackBarBrightness.BeginInit();
      this.radTrackBarContrast.BeginInit();
      this.radSpinEditorBrightness.BeginInit();
      this.radSpinEditorContrast.BeginInit();
      this.radButtonReset.BeginInit();
      this.radLabelBrightness.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelContrast, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarBrightness, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarContrast, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorBrightness, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorContrast, 1, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBrightness, 0, 0);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 5;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel1.Size = new Size(292, 181);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelContrast.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelContrast.Location = new Point(3, 87);
      this.radLabelContrast.Name = "radLabelContrast";
      this.radLabelContrast.Size = new Size(48, 18);
      this.radLabelContrast.TabIndex = 1;
      this.radLabelContrast.Text = "Contrast";
      this.radTrackBarBrightness.BackColor = Color.Transparent;
      this.radTrackBarBrightness.LargeTickFrequency = 200;
      this.radTrackBarBrightness.Location = new Point(3, 36);
      this.radTrackBarBrightness.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarBrightness.Maximum = 200f;
      this.radTrackBarBrightness.Name = "radTrackBarBrightness";
      this.radTrackBarBrightness.Size = new Size(140, 36);
      this.radTrackBarBrightness.SmallTickFrequency = 0;
      this.radTrackBarBrightness.TabIndex = 2;
      this.radTrackBarBrightness.Value = 100f;
      this.radTrackBarContrast.BackColor = Color.Transparent;
      this.radTrackBarContrast.LargeTickFrequency = 50;
      this.radTrackBarContrast.Location = new Point(3, 108);
      this.radTrackBarContrast.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarContrast.Maximum = 200f;
      this.radTrackBarContrast.Name = "radTrackBarContrast";
      this.radTrackBarContrast.Size = new Size(140, 36);
      this.radTrackBarContrast.SmallTickFrequency = 10;
      this.radTrackBarContrast.TabIndex = 3;
      this.radTrackBarContrast.Value = 100f;
      this.radSpinEditorBrightness.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorBrightness.Location = new Point(149, 44);
      this.radSpinEditorBrightness.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorBrightness.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
      this.radSpinEditorBrightness.Name = "radSpinEditorBrightness";
      this.radSpinEditorBrightness.Size = new Size(140, 20);
      this.radSpinEditorBrightness.TabIndex = 4;
      this.radSpinEditorBrightness.TabStop = false;
      this.radSpinEditorContrast.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorContrast.Location = new Point(149, 116);
      this.radSpinEditorContrast.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorContrast.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
      this.radSpinEditorContrast.Name = "radSpinEditorContrast";
      this.radSpinEditorContrast.Size = new Size(140, 20);
      this.radSpinEditorContrast.TabIndex = 5;
      this.radSpinEditorContrast.TabStop = false;
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(179, 154);
      this.radButtonReset.Name = "radButtonReset";
      this.radButtonReset.Size = new Size(110, 24);
      this.radButtonReset.TabIndex = 6;
      this.radButtonReset.Text = "Reset";
      this.radLabelBrightness.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelBrightness.Location = new Point(3, 15);
      this.radLabelBrightness.Name = "radLabelBrightness";
      this.radLabelBrightness.Size = new Size(58, 18);
      this.radLabelBrightness.TabIndex = 0;
      this.radLabelBrightness.Text = "Brightness";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 160);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ContrastDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Contrast";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelContrast.EndInit();
      this.radTrackBarBrightness.EndInit();
      this.radTrackBarContrast.EndInit();
      this.radSpinEditorBrightness.EndInit();
      this.radSpinEditorContrast.EndInit();
      this.radButtonReset.EndInit();
      this.radLabelBrightness.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
