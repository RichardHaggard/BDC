// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.DrawTextDialog
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
  public class DrawTextDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelText;
    private RadLabel radLabelFontSize;
    private RadLabel radLabelTextColor;
    private RadLabel radLabelHorizontalPosition;
    private RadLabel radLabelVerticalPosition;
    private RadLabel radLabelRotation;
    private RadTrackBar radTrackBarFontSize;
    private RadTrackBar radTrackBarHorizontalPosition;
    private RadTrackBar radTrackBarVerticalPosition;
    private RadTrackBar radTrackBarRotation;
    private RadSpinEditor radSpinEditorFontSize;
    private RadSpinEditor radSpinEditorHorizontalPosition;
    private RadSpinEditor radSpinEditorVerticalPosition;
    private RadSpinEditor radSpinEditorRotation;
    private RadColorBox radColorBoxTextColor;
    private RadButton radButtonReset;
    private RadTextBox radTextBoxText;

    public DrawTextDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.radTrackBarFontSize.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
      this.radTrackBarRotation.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
      this.radTrackBarHorizontalPosition.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
      this.radTrackBarVerticalPosition.TickFormatting += new TickFormattingEventHandler(((ImageEditorBaseDialog) this).RadTrackBar_TickFormatting);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 684), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 456), this.RootElement.DpiScaleFactor);
      this.radSpinEditorHorizontalPosition.Maximum = (Decimal) this.ImageEditorElement.CurrentBitmap.Width;
      this.radSpinEditorVerticalPosition.Maximum = (Decimal) this.ImageEditorElement.CurrentBitmap.Height;
      this.radTrackBarHorizontalPosition.LargeTickFrequency = this.ImageEditorElement.CurrentBitmap.Width;
      this.radTrackBarVerticalPosition.LargeTickFrequency = this.ImageEditorElement.CurrentBitmap.Height;
      this.radTrackBarHorizontalPosition.Maximum = (float) this.ImageEditorElement.CurrentBitmap.Width;
      this.radTrackBarVerticalPosition.Maximum = (float) this.ImageEditorElement.CurrentBitmap.Height;
    }

    protected override void WireEvents()
    {
      this.radTextBoxText.TextChanged += new EventHandler(this.RadTextBoxText_TextChanged);
      this.radSpinEditorFontSize.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorHorizontalPosition.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorVerticalPosition.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorRotation.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radColorBoxTextColor.ValueChanged += new EventHandler(this.RadColorBoxTextColor_ValueChanged);
      this.radTrackBarFontSize.Scroll += new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarHorizontalPosition.Scroll += new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarVerticalPosition.Scroll += new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarRotation.Scroll += new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radTextBoxText.TextChanged -= new EventHandler(this.RadTextBoxText_TextChanged);
      this.radSpinEditorFontSize.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorHorizontalPosition.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorVerticalPosition.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorRotation.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radColorBoxTextColor.ValueChanged -= new EventHandler(this.RadColorBoxTextColor_ValueChanged);
      this.radTrackBarFontSize.Scroll -= new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarHorizontalPosition.Scroll -= new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarVerticalPosition.Scroll -= new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radTrackBarRotation.Scroll -= new ScrollEventHandler(this.RadTrackBar_Scroll);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.DrawString(this.radTextBoxText.Text, (int) this.radSpinEditorFontSize.Value, this.radColorBoxTextColor.Value, (int) this.radSpinEditorHorizontalPosition.Value, (int) this.radSpinEditorVerticalPosition.Value, (int) this.radSpinEditorRotation.Value);
      this.result = DialogResult.OK;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogTitle");
      this.radLabelText.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogText");
      this.radTextBoxText.NullText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogYourTextHere");
      this.radLabelFontSize.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogFontSize");
      this.radLabelTextColor.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogTextColort");
      this.radLabelHorizontalPosition.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogHorizontalPosition");
      this.radLabelVerticalPosition.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogVerticalPosition");
      this.radLabelRotation.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogRotation");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawTextDialogReset");
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadColorBoxTextColor_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadTrackBar_Scroll(object sender, ScrollEventArgs e)
    {
      this.radSpinEditorRotation.Value = (Decimal) this.radTrackBarRotation.Value;
      this.radSpinEditorVerticalPosition.Value = (Decimal) this.radTrackBarVerticalPosition.Value;
      this.radSpinEditorHorizontalPosition.Value = (Decimal) this.radTrackBarHorizontalPosition.Value;
      this.radSpinEditorFontSize.Value = (Decimal) this.radTrackBarFontSize.Value;
    }

    private void RadSpinEditor_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radTrackBarRotation.Value = (float) this.radSpinEditorRotation.Value;
      this.radTrackBarVerticalPosition.Value = (float) this.radSpinEditorVerticalPosition.Value;
      this.radTrackBarHorizontalPosition.Value = (float) this.radSpinEditorHorizontalPosition.Value;
      this.radTrackBarFontSize.Value = (float) this.radSpinEditorFontSize.Value;
      this.WireEvents();
      this.ApplySettings();
    }

    private void RadTextBoxText_TextChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.radTextBoxText.Text = (string) null;
      this.radSpinEditorFontSize.Value = new Decimal(36);
      this.radColorBoxTextColor.Value = Color.Black;
      this.radSpinEditorHorizontalPosition.Value = new Decimal(0);
      this.radSpinEditorVerticalPosition.Value = new Decimal(0);
      this.radSpinEditorRotation.Value = new Decimal(0);
      this.ApplySettings();
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
      this.radLabelText = new RadLabel();
      this.radLabelFontSize = new RadLabel();
      this.radLabelTextColor = new RadLabel();
      this.radLabelHorizontalPosition = new RadLabel();
      this.radLabelVerticalPosition = new RadLabel();
      this.radLabelRotation = new RadLabel();
      this.radSpinEditorFontSize = new RadSpinEditor();
      this.radSpinEditorHorizontalPosition = new RadSpinEditor();
      this.radSpinEditorVerticalPosition = new RadSpinEditor();
      this.radSpinEditorRotation = new RadSpinEditor();
      this.radColorBoxTextColor = new RadColorBox();
      this.radButtonReset = new RadButton();
      this.radTextBoxText = new RadTextBox();
      this.radTrackBarFontSize = new RadTrackBar();
      this.radTrackBarHorizontalPosition = new RadTrackBar();
      this.radTrackBarVerticalPosition = new RadTrackBar();
      this.radTrackBarRotation = new RadTrackBar();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelText.BeginInit();
      this.radLabelFontSize.BeginInit();
      this.radLabelTextColor.BeginInit();
      this.radLabelHorizontalPosition.BeginInit();
      this.radLabelVerticalPosition.BeginInit();
      this.radLabelRotation.BeginInit();
      this.radSpinEditorFontSize.BeginInit();
      this.radSpinEditorHorizontalPosition.BeginInit();
      this.radSpinEditorVerticalPosition.BeginInit();
      this.radSpinEditorRotation.BeginInit();
      this.radColorBoxTextColor.BeginInit();
      this.radButtonReset.BeginInit();
      this.radTextBoxText.BeginInit();
      this.radTrackBarFontSize.BeginInit();
      this.radTrackBarHorizontalPosition.BeginInit();
      this.radTrackBarVerticalPosition.BeginInit();
      this.radTrackBarRotation.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelText, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelFontSize, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelTextColor, 0, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelHorizontalPosition, 0, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelVerticalPosition, 0, 7);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelRotation, 0, 9);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorFontSize, 1, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorHorizontalPosition, 1, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorVerticalPosition, 1, 8);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorRotation, 1, 10);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxTextColor, 1, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 11);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTextBoxText, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarFontSize, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarHorizontalPosition, 0, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarVerticalPosition, 0, 8);
      this.tableLayoutPanel1.Controls.Add((Control) this.radTrackBarRotation, 0, 10);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 12;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333f));
      this.tableLayoutPanel1.Size = new Size(292, 456);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelText.Location = new Point(3, 16);
      this.radLabelText.Name = "radLabelText";
      this.radLabelText.Size = new Size(27, 18);
      this.radLabelText.TabIndex = 0;
      this.radLabelText.Text = "Text";
      this.radLabelFontSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelFontSize.Location = new Point(3, 90);
      this.radLabelFontSize.Name = "radLabelFontSize";
      this.radLabelFontSize.Size = new Size(50, 18);
      this.radLabelFontSize.TabIndex = 0;
      this.radLabelFontSize.Text = "Font size";
      this.radLabelTextColor.Anchor = AnchorStyles.Left;
      this.radLabelTextColor.Location = new Point(3, 157);
      this.radLabelTextColor.Name = "radLabelTextColor";
      this.radLabelTextColor.Size = new Size(140, 18);
      this.radLabelTextColor.TabIndex = 0;
      this.radLabelTextColor.Text = "Text Color";
      this.radLabelHorizontalPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelHorizontalPosition.Location = new Point(3, 201);
      this.radLabelHorizontalPosition.Name = "radLabelHorizontalPosition";
      this.radLabelHorizontalPosition.Size = new Size(101, 18);
      this.radLabelHorizontalPosition.TabIndex = 0;
      this.radLabelHorizontalPosition.Text = "Horizontal Position";
      this.radLabelVerticalPosition.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelVerticalPosition.Location = new Point(3, 275);
      this.radLabelVerticalPosition.Name = "radLabelVerticalPosition";
      this.radLabelVerticalPosition.Size = new Size(87, 18);
      this.radLabelVerticalPosition.TabIndex = 0;
      this.radLabelVerticalPosition.Text = "Vertical Position";
      this.radLabelRotation.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelRotation.Location = new Point(3, 349);
      this.radLabelRotation.Name = "radLabelRotation";
      this.radLabelRotation.Size = new Size(49, 18);
      this.radLabelRotation.TabIndex = 0;
      this.radLabelRotation.Text = "Rotation";
      this.radSpinEditorFontSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorFontSize.Location = new Point(149, 119);
      this.radSpinEditorFontSize.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorFontSize.Minimum = new Decimal(new int[4]
      {
        5,
        0,
        0,
        0
      });
      this.radSpinEditorFontSize.Name = "radSpinEditorFontSize";
      this.radSpinEditorFontSize.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        36,
        0,
        0,
        0
      }));
      this.radSpinEditorFontSize.Size = new Size(140, 20);
      this.radSpinEditorFontSize.TabIndex = 2;
      this.radSpinEditorFontSize.TabStop = false;
      this.radSpinEditorFontSize.Value = new Decimal(new int[4]
      {
        36,
        0,
        0,
        0
      });
      this.radSpinEditorHorizontalPosition.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorHorizontalPosition.Location = new Point(149, 230);
      this.radSpinEditorHorizontalPosition.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorHorizontalPosition.Name = "radSpinEditorHorizontalPosition";
      this.radSpinEditorHorizontalPosition.Size = new Size(140, 20);
      this.radSpinEditorHorizontalPosition.TabIndex = 2;
      this.radSpinEditorHorizontalPosition.TabStop = false;
      this.radSpinEditorVerticalPosition.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorVerticalPosition.Location = new Point(149, 304);
      this.radSpinEditorVerticalPosition.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorVerticalPosition.Name = "radSpinEditorVerticalPosition";
      this.radSpinEditorVerticalPosition.Size = new Size(140, 20);
      this.radSpinEditorVerticalPosition.TabIndex = 2;
      this.radSpinEditorVerticalPosition.TabStop = false;
      this.radSpinEditorRotation.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorRotation.Location = new Point(149, 378);
      this.radSpinEditorRotation.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorRotation.Maximum = new Decimal(new int[4]
      {
        360,
        0,
        0,
        0
      });
      this.radSpinEditorRotation.Name = "radSpinEditorRotation";
      this.radSpinEditorRotation.Size = new Size(140, 20);
      this.radSpinEditorRotation.TabIndex = 2;
      this.radSpinEditorRotation.TabStop = false;
      this.radColorBoxTextColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxTextColor.Location = new Point(149, 156);
      this.radColorBoxTextColor.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxTextColor.Name = "radColorBoxTextColor";
      this.radColorBoxTextColor.Size = new Size(140, 20);
      this.radColorBoxTextColor.TabIndex = 3;
      this.radColorBoxTextColor.Value = Color.Black;
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(179, 429);
      this.radButtonReset.Name = "radButtonReset";
      this.radButtonReset.Size = new Size(110, 24);
      this.radButtonReset.TabIndex = 4;
      this.radButtonReset.Text = "Reset";
      this.radTextBoxText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radTextBoxText.Location = new Point(3, 45);
      this.radTextBoxText.Margin = new Padding(3, 0, 3, 0);
      this.radTextBoxText.Name = "radTextBoxText";
      this.radTextBoxText.NullText = "Your text here...";
      this.radTextBoxText.Size = new Size(140, 20);
      this.radTextBoxText.TabIndex = 5;
      this.radTrackBarFontSize.BackColor = Color.Transparent;
      this.radTrackBarFontSize.Location = new Point(3, 111);
      this.radTrackBarFontSize.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarFontSize.Maximum = 100f;
      this.radTrackBarFontSize.Minimum = 5f;
      this.radTrackBarFontSize.Name = "radTrackBarFontSize";
      this.radTrackBarFontSize.Size = new Size(140, 37);
      this.radTrackBarFontSize.SmallTickFrequency = 0;
      this.radTrackBarFontSize.TabIndex = 1;
      this.radTrackBarFontSize.Value = 36f;
      this.radTrackBarHorizontalPosition.BackColor = Color.Transparent;
      this.radTrackBarHorizontalPosition.Location = new Point(3, 222);
      this.radTrackBarHorizontalPosition.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarHorizontalPosition.Maximum = 100f;
      this.radTrackBarHorizontalPosition.Name = "radTrackBarHorizontalPosition";
      this.radTrackBarHorizontalPosition.Size = new Size(140, 37);
      this.radTrackBarHorizontalPosition.SmallTickFrequency = 0;
      this.radTrackBarHorizontalPosition.TabIndex = 1;
      this.radTrackBarVerticalPosition.BackColor = Color.Transparent;
      this.radTrackBarVerticalPosition.Location = new Point(3, 296);
      this.radTrackBarVerticalPosition.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarVerticalPosition.Maximum = 100f;
      this.radTrackBarVerticalPosition.Name = "radTrackBarVerticalPosition";
      this.radTrackBarVerticalPosition.Size = new Size(140, 37);
      this.radTrackBarVerticalPosition.SmallTickFrequency = 0;
      this.radTrackBarVerticalPosition.TabIndex = 1;
      this.radTrackBarRotation.BackColor = Color.Transparent;
      this.radTrackBarRotation.LargeTickFrequency = 20;
      this.radTrackBarRotation.Location = new Point(3, 370);
      this.radTrackBarRotation.Margin = new Padding(3, 0, 3, 0);
      this.radTrackBarRotation.Maximum = 360f;
      this.radTrackBarRotation.Name = "radTrackBarRotation";
      this.radTrackBarRotation.Size = new Size(140, 37);
      this.radTrackBarRotation.SmallTickFrequency = 0;
      this.radTrackBarRotation.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 456);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (DrawTextDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (DrawTextDialog);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelText.EndInit();
      this.radLabelFontSize.EndInit();
      this.radLabelTextColor.EndInit();
      this.radLabelHorizontalPosition.EndInit();
      this.radLabelVerticalPosition.EndInit();
      this.radLabelRotation.EndInit();
      this.radSpinEditorFontSize.EndInit();
      this.radSpinEditorHorizontalPosition.EndInit();
      this.radSpinEditorVerticalPosition.EndInit();
      this.radSpinEditorRotation.EndInit();
      this.radColorBoxTextColor.EndInit();
      this.radButtonReset.EndInit();
      this.radTextBoxText.EndInit();
      this.radTrackBarFontSize.EndInit();
      this.radTrackBarHorizontalPosition.EndInit();
      this.radTrackBarVerticalPosition.EndInit();
      this.radTrackBarRotation.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
