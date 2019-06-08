// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.HueShiftDialog
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
  public class HueShiftDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelHueShift;
    private RadTrackBar radTrackBarShift;
    private RadSpinEditor radSpinEditorShift;
    private RadButton radButtonReset;

    public HueShiftDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.radTrackBarShift.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
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
      this.radSpinEditorShift.ValueChanged += new EventHandler(this.RadSpinEditorShift_ValueChanged);
      this.radTrackBarShift.Scroll += new ScrollEventHandler(this.RadTrackBarShift_Scroll);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorShift.ValueChanged -= new EventHandler(this.RadSpinEditorShift_ValueChanged);
      this.radTrackBarShift.Scroll -= new ScrollEventHandler(this.RadTrackBarShift_Scroll);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.SetHue((int) this.radSpinEditorShift.Value);
      this.result = DialogResult.OK;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("HueDialogTitle");
      this.radLabelHueShift.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("HueDialogHueShift");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("HueDialogReset");
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarShift.Value = 0.0f;
      this.radSpinEditorShift.Value = new Decimal(0);
      this.ApplySettings();
      this.WireEvents();
      this.result = DialogResult.Cancel;
    }

    private void RadSpinEditorShift_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarShift.Value = (float) this.radSpinEditorShift.Value;
      this.WireEvents();
      this.ApplySettings();
    }

    private void RadTrackBarShift_Scroll(object sender, ScrollEventArgs e)
    {
      this.radSpinEditorShift.Value = (Decimal) this.radTrackBarShift.Value;
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
      this.radLabelHueShift = new RadLabel();
      this.radTrackBarShift = new RadTrackBar();
      this.radSpinEditorShift = new RadSpinEditor();
      this.radButtonReset = new RadButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelHueShift.BeginInit();
      this.radTrackBarShift.BeginInit();
      this.radSpinEditorShift.BeginInit();
      this.radButtonReset.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelHueShift, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarShift, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorShift, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 2);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.Size = new Size(300, 110);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelHueShift.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelHueShift.Location = new Point(3, 15);
      this.radLabelHueShift.Name = "radLabelHueShift";
      this.radLabelHueShift.Size = new Size(52, 18);
      this.radLabelHueShift.TabIndex = 0;
      this.radLabelHueShift.Text = "Hue Shift";
      this.radTrackBarShift.BackColor = Color.Transparent;
      this.radTrackBarShift.LargeTickFrequency = 360;
      this.radTrackBarShift.Location = new Point(3, 37);
      this.radTrackBarShift.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarShift.Maximum = 360f;
      this.radTrackBarShift.Name = "radTrackBarShift";
      this.radTrackBarShift.Size = new Size(144, 34);
      this.radTrackBarShift.SmallTickFrequency = 0;
      this.radTrackBarShift.TabIndex = 1;
      this.radSpinEditorShift.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorShift.Location = new Point(153, 44);
      this.radSpinEditorShift.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorShift.Maximum = new Decimal(new int[4]
      {
        360,
        0,
        0,
        0
      });
      this.radSpinEditorShift.Name = "radSpinEditorShift";
      this.radSpinEditorShift.Size = new Size(144, 20);
      this.radSpinEditorShift.TabIndex = 2;
      this.radSpinEditorShift.TabStop = false;
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(187, 83);
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
      this.Name = nameof (HueShiftDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Hue Shift";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelHueShift.EndInit();
      this.radTrackBarShift.EndInit();
      this.radSpinEditorShift.EndInit();
      this.radButtonReset.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
