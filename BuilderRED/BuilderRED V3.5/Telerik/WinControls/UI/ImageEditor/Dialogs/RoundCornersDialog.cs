// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.RoundCornersDialog
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
  public class RoundCornersDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelRadius;
    private RadLabel radLabelBackground;
    private RadLabel radLabelBorderThickness;
    private RadLabel radLabelBorderColor;
    private RadTrackBar radTrackBarRadius;
    private RadTrackBar radTrackBarBorderThickness;
    private RadSpinEditor radSpinEditorRadius;
    private RadSpinEditor radSpinEditorBorderThickness;
    private RadColorBox radColorBoxBackground;
    private RadColorBox radColorBoxBorderColor;
    private RadButton radButtonReset;

    public RoundCornersDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.radTrackBarRadius.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
      this.radTrackBarBorderThickness.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 399), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 266), this.RootElement.DpiScaleFactor);
    }

    protected override void WireEvents()
    {
      this.radSpinEditorRadius.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorBorderThickness.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radColorBoxBackground.ValueChanged += new EventHandler(this.RadColorBox_ValueChanged);
      this.radColorBoxBorderColor.ValueChanged += new EventHandler(this.RadColorBox_ValueChanged);
      this.radTrackBarBorderThickness.Scroll += new ScrollEventHandler(this.RadTrackBarBorderThickness_Scroll);
      this.radTrackBarRadius.Scroll += new ScrollEventHandler(this.RadTrackBarBorderThickness_Scroll);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorRadius.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorBorderThickness.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radColorBoxBackground.ValueChanged -= new EventHandler(this.RadColorBox_ValueChanged);
      this.radColorBoxBorderColor.ValueChanged -= new EventHandler(this.RadColorBox_ValueChanged);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.RoundCorners((int) this.radSpinEditorRadius.Value, this.radColorBoxBackground.Value, (int) this.radSpinEditorBorderThickness.Value, this.radColorBoxBorderColor.Value);
      this.result = DialogResult.OK;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RoundCornersDialogTitle");
      this.radLabelRadius.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RoundCornersDialogRadius");
      this.radLabelBackground.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RoundCornersDialogBackground");
      this.radLabelBorderThickness.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RoundCornersDialogBorderThickness");
      this.radLabelBorderColor.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RoundCornersDialogBorderColor");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RoundCornersDialogReset");
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radSpinEditorRadius.Value = new Decimal(0);
      this.radSpinEditorBorderThickness.Value = new Decimal(0);
      this.radColorBoxBackground.Value = Color.Empty;
      this.radColorBoxBorderColor.Value = Color.Empty;
      this.ApplySettings();
      this.WireEvents();
      this.result = DialogResult.Cancel;
    }

    private void RadTrackBarBorderThickness_Scroll(object sender, ScrollEventArgs e)
    {
      this.radSpinEditorRadius.Value = (Decimal) this.radTrackBarRadius.Value;
      this.radSpinEditorBorderThickness.Value = (Decimal) this.radTrackBarBorderThickness.Value;
    }

    private void RadColorBox_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadSpinEditor_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarBorderThickness.Value = (float) this.radSpinEditorBorderThickness.Value;
      this.radTrackBarRadius.Value = (float) this.radSpinEditorRadius.Value;
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
      this.radLabelRadius = new RadLabel();
      this.radLabelBackground = new RadLabel();
      this.radLabelBorderThickness = new RadLabel();
      this.radLabelBorderColor = new RadLabel();
      this.radTrackBarRadius = new RadTrackBar();
      this.radTrackBarBorderThickness = new RadTrackBar();
      this.radSpinEditorRadius = new RadSpinEditor();
      this.radSpinEditorBorderThickness = new RadSpinEditor();
      this.radColorBoxBackground = new RadColorBox();
      this.radColorBoxBorderColor = new RadColorBox();
      this.radButtonReset = new RadButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelRadius.BeginInit();
      this.radLabelBackground.BeginInit();
      this.radLabelBorderThickness.BeginInit();
      this.radLabelBorderColor.BeginInit();
      this.radTrackBarRadius.BeginInit();
      this.radTrackBarBorderThickness.BeginInit();
      this.radSpinEditorRadius.BeginInit();
      this.radSpinEditorBorderThickness.BeginInit();
      this.radColorBoxBackground.BeginInit();
      this.radColorBoxBorderColor.BeginInit();
      this.radButtonReset.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelRadius, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBackground, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBorderThickness, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBorderColor, 0, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarRadius, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarBorderThickness, 0, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorRadius, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorBorderThickness, 1, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxBackground, 1, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxBorderColor, 1, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 6);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 7;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28571f));
      this.tableLayoutPanel1.Size = new Size(292, 266);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelRadius.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radLabelRadius.AutoSize = false;
      this.radLabelRadius.Location = new Point(3, 9);
      this.radLabelRadius.Name = "radLabelRadius";
      this.radLabelRadius.Size = new Size(39, 18);
      this.radLabelRadius.TabIndex = 0;
      this.radLabelRadius.Text = "Radius";
      this.radLabelBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radLabelBackground.AutoSize = false;
      this.radLabelBackground.Location = new Point(3, 77);
      this.radLabelBackground.Name = "radLabelBackground";
      this.radLabelBackground.Size = new Size(68, 18);
      this.radLabelBackground.TabIndex = 0;
      this.radLabelBackground.Text = "Background:";
      this.radLabelBorderThickness.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radLabelBorderThickness.AutoSize = false;
      this.radLabelBorderThickness.Location = new Point(3, 120);
      this.radLabelBorderThickness.Name = "radLabelBorderThickness";
      this.radLabelBorderThickness.Size = new Size(93, 18);
      this.radLabelBorderThickness.TabIndex = 0;
      this.radLabelBorderThickness.Text = "Border Thickness:";
      this.radLabelBorderColor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radLabelBorderColor.AutoSize = false;
      this.radLabelBorderColor.Location = new Point(3, 188);
      this.radLabelBorderColor.Name = "radLabelBorderColor";
      this.radLabelBorderColor.Size = new Size(72, 18);
      this.radLabelBorderColor.TabIndex = 0;
      this.radLabelBorderColor.Text = "Border Color:";
      this.radTrackBarRadius.BackColor = Color.Transparent;
      this.radTrackBarRadius.LargeTickFrequency = 100;
      this.radTrackBarRadius.Location = new Point(3, 37);
      this.radTrackBarRadius.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarRadius.Maximum = 100f;
      this.radTrackBarRadius.Name = "radTrackBarRadius";
      this.radTrackBarRadius.Size = new Size(140, 16);
      this.radTrackBarRadius.SmallTickFrequency = 0;
      this.radTrackBarRadius.TabIndex = 1;
      this.radTrackBarBorderThickness.BackColor = Color.Transparent;
      this.radTrackBarBorderThickness.LargeTickFrequency = 100;
      this.radTrackBarBorderThickness.Location = new Point(3, 148);
      this.radTrackBarBorderThickness.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarBorderThickness.Maximum = 100f;
      this.radTrackBarBorderThickness.Name = "radTrackBarBorderThickness";
      this.radTrackBarBorderThickness.Size = new Size(140, 16);
      this.radTrackBarBorderThickness.SmallTickFrequency = 0;
      this.radTrackBarBorderThickness.TabIndex = 1;
      this.radSpinEditorRadius.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorRadius.Location = new Point(149, 37);
      this.radSpinEditorRadius.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorRadius.Name = "radSpinEditorRadius";
      this.radSpinEditorRadius.Size = new Size(140, 20);
      this.radSpinEditorRadius.TabIndex = 2;
      this.radSpinEditorRadius.TabStop = false;
      this.radSpinEditorBorderThickness.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorBorderThickness.Location = new Point(149, 148);
      this.radSpinEditorBorderThickness.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorBorderThickness.Name = "radSpinEditorBorderThickness";
      this.radSpinEditorBorderThickness.Size = new Size(140, 20);
      this.radSpinEditorBorderThickness.TabIndex = 2;
      this.radSpinEditorBorderThickness.TabStop = false;
      this.radColorBoxBackground.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxBackground.Location = new Point(149, 74);
      this.radColorBoxBackground.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxBackground.Name = "radColorBoxBackground";
      this.radColorBoxBackground.Size = new Size(140, 20);
      this.radColorBoxBackground.TabIndex = 3;
      this.radColorBoxBackground.Value = Color.White;
      this.radColorBoxBorderColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxBorderColor.Location = new Point(149, 185);
      this.radColorBoxBorderColor.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxBorderColor.Name = "radColorBoxBorderColor";
      this.radColorBoxBorderColor.Size = new Size(140, 20);
      this.radColorBoxBorderColor.TabIndex = 3;
      this.radColorBoxBorderColor.Value = Color.White;
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(179, 239);
      this.radButtonReset.Name = "radButtonReset";
      this.radButtonReset.Size = new Size(110, 24);
      this.radButtonReset.TabIndex = 4;
      this.radButtonReset.Text = "Reset";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 266);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (RoundCornersDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Round Corners";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelRadius.EndInit();
      this.radLabelBackground.EndInit();
      this.radLabelBorderThickness.EndInit();
      this.radLabelBorderColor.EndInit();
      this.radTrackBarRadius.EndInit();
      this.radTrackBarBorderThickness.EndInit();
      this.radSpinEditorRadius.EndInit();
      this.radSpinEditorBorderThickness.EndInit();
      this.radColorBoxBackground.EndInit();
      this.radColorBoxBorderColor.EndInit();
      this.radButtonReset.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
