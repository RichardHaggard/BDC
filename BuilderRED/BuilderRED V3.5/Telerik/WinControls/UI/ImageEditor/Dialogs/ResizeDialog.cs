// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.ResizeDialog
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
  public class ResizeDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private Size originalSize;
    private double originalAspectRatio;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelImageSize;
    private RadLabel radLabelWidth;
    private RadLabel radLabelHeight;
    private RadLabel radLabelRelativeSize;
    private RadLabel radLabelRelativeWidth;
    private RadLabel radLabelRelativeHeight;
    private RadSpinEditor radSpinEditorWidth;
    private RadSpinEditor radSpinEditorHeight;
    private RadSpinEditor radSpinEditorRelativeWidth;
    private RadSpinEditor radSpinEditorRelativeHeight;
    private RadButton radButtonReset;
    private RadCheckBox radCheckBoxAspectRation;

    public ResizeDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.originalSize = this.ImageEditorElement.CurrentBitmap.Size;
      this.originalAspectRatio = (double) this.ImageEditorElement.CurrentBitmap.Width / (double) this.ImageEditorElement.CurrentBitmap.Height;
      this.radSpinEditorWidth.Maximum = (Decimal) Math.Max(this.originalSize.Width, (int) short.MaxValue);
      this.radSpinEditorHeight.Maximum = (Decimal) Math.Max(this.originalSize.Height, (int) short.MaxValue);
      this.radSpinEditorWidth.Value = (Decimal) this.originalSize.Width;
      this.radSpinEditorHeight.Value = (Decimal) this.originalSize.Height;
      this.radSpinEditorRelativeWidth.Value = new Decimal(100);
      this.radSpinEditorRelativeHeight.Value = new Decimal(100);
      this.radSpinEditorRelativeWidth.Minimum = new Decimal(1) / this.radSpinEditorWidth.Value * new Decimal(100);
      this.radSpinEditorRelativeWidth.Maximum = this.radSpinEditorWidth.Maximum / this.radSpinEditorWidth.Value * new Decimal(100);
      this.radSpinEditorRelativeHeight.Minimum = new Decimal(1) / this.radSpinEditorHeight.Value * new Decimal(100);
      this.radSpinEditorRelativeHeight.Maximum = this.radSpinEditorHeight.Maximum / this.radSpinEditorHeight.Value * new Decimal(100);
      this.UnwireEvents();
      this.WireEvents();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 377), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 251), this.RootElement.DpiScaleFactor);
    }

    protected override void WireEvents()
    {
      this.radSpinEditorWidth.ValueChanged += new EventHandler(this.RadSpinEditorWidth_ValueChanged);
      this.radSpinEditorHeight.ValueChanged += new EventHandler(this.RadSpinEditorHeight_ValueChanged);
      this.radSpinEditorRelativeWidth.ValueChanged += new EventHandler(this.RadSpinEditorRelativeWidth_ValueChanged);
      this.radSpinEditorRelativeHeight.ValueChanged += new EventHandler(this.RadSpinEditorRelativeHeight_ValueChanged);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorWidth.ValueChanged -= new EventHandler(this.RadSpinEditorWidth_ValueChanged);
      this.radSpinEditorHeight.ValueChanged -= new EventHandler(this.RadSpinEditorHeight_ValueChanged);
      this.radSpinEditorRelativeWidth.ValueChanged -= new EventHandler(this.RadSpinEditorRelativeWidth_ValueChanged);
      this.radSpinEditorRelativeHeight.ValueChanged -= new EventHandler(this.RadSpinEditorRelativeHeight_ValueChanged);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.Resize((int) this.radSpinEditorWidth.Value, (int) this.radSpinEditorHeight.Value);
      this.result = DialogResult.OK;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogTitle");
      this.radLabelWidth.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogWidth");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogReset");
      this.radLabelHeight.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogHeight");
      this.radLabelImageSize.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogImageSize");
      this.radLabelRelativeSize.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogRelativeSize");
      this.radLabelRelativeWidth.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogRelativeWidth");
      this.radLabelRelativeHeight.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogRelativeHeight");
      this.radCheckBoxAspectRation.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ResizeDialogPreserveAspectRatio");
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadSpinEditorWidth_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      if (this.radCheckBoxAspectRation.Checked)
      {
        this.radSpinEditorHeight.Value = Math.Round(this.radSpinEditorWidth.Value / (Decimal) this.originalAspectRatio);
        this.radSpinEditorRelativeWidth.Value = this.radSpinEditorWidth.Value / (Decimal) this.originalSize.Width * new Decimal(100);
        this.radSpinEditorRelativeHeight.Value = this.radSpinEditorHeight.Value / (Decimal) this.originalSize.Height * new Decimal(100);
      }
      else
        this.radSpinEditorRelativeWidth.Value = this.radSpinEditorWidth.Value / (Decimal) this.originalSize.Width * new Decimal(100);
      this.ApplySettings();
      this.WireEvents();
    }

    private void RadSpinEditorHeight_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      if (this.radCheckBoxAspectRation.Checked)
      {
        this.radSpinEditorWidth.Value = Math.Round(this.radSpinEditorHeight.Value * (Decimal) this.originalAspectRatio);
        this.radSpinEditorRelativeWidth.Value = this.radSpinEditorWidth.Value / (Decimal) this.originalSize.Width * new Decimal(100);
        this.radSpinEditorRelativeHeight.Value = this.radSpinEditorHeight.Value / (Decimal) this.originalSize.Height * new Decimal(100);
      }
      else
        this.radSpinEditorRelativeHeight.Value = this.radSpinEditorHeight.Value / (Decimal) this.originalSize.Height * new Decimal(100);
      this.ApplySettings();
      this.WireEvents();
    }

    private void RadSpinEditorRelativeWidth_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      if (this.radCheckBoxAspectRation.Checked)
      {
        this.radSpinEditorWidth.Value = (Decimal) this.originalSize.Width * this.radSpinEditorRelativeWidth.Value / new Decimal(100);
        this.radSpinEditorHeight.Value = (Decimal) (int) (this.radSpinEditorWidth.Value / (Decimal) this.originalAspectRatio);
        this.radSpinEditorRelativeHeight.Value = this.radSpinEditorRelativeWidth.Value;
      }
      else
        this.radSpinEditorWidth.Value = Math.Round((Decimal) this.originalSize.Width * this.radSpinEditorRelativeWidth.Value / new Decimal(100));
      this.ApplySettings();
      this.WireEvents();
    }

    private void RadSpinEditorRelativeHeight_ValueChanged(object sender, EventArgs e)
    {
      this.UnwireEvents();
      if (this.radCheckBoxAspectRation.Checked)
      {
        this.radSpinEditorHeight.Value = (Decimal) this.originalSize.Height * this.radSpinEditorRelativeHeight.Value / new Decimal(100);
        this.radSpinEditorWidth.Value = (Decimal) (int) (this.radSpinEditorHeight.Value * (Decimal) this.originalAspectRatio);
        this.radSpinEditorRelativeWidth.Value = this.radSpinEditorRelativeHeight.Value;
      }
      else
        this.radSpinEditorHeight.Value = Math.Round((Decimal) this.originalSize.Height * this.radSpinEditorRelativeHeight.Value / new Decimal(100));
      this.ApplySettings();
      this.WireEvents();
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radSpinEditorWidth.Value = (Decimal) this.originalSize.Width;
      this.radSpinEditorHeight.Value = (Decimal) this.originalSize.Height;
      this.radSpinEditorRelativeWidth.Value = new Decimal(100);
      this.radSpinEditorRelativeHeight.Value = new Decimal(100);
      this.ApplySettings();
      this.result = DialogResult.Cancel;
      this.WireEvents();
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
      this.radLabelImageSize = new RadLabel();
      this.radLabelWidth = new RadLabel();
      this.radLabelHeight = new RadLabel();
      this.radLabelRelativeSize = new RadLabel();
      this.radLabelRelativeWidth = new RadLabel();
      this.radLabelRelativeHeight = new RadLabel();
      this.radSpinEditorWidth = new RadSpinEditor();
      this.radSpinEditorHeight = new RadSpinEditor();
      this.radSpinEditorRelativeWidth = new RadSpinEditor();
      this.radSpinEditorRelativeHeight = new RadSpinEditor();
      this.radButtonReset = new RadButton();
      this.radCheckBoxAspectRation = new RadCheckBox();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelImageSize.BeginInit();
      this.radLabelWidth.BeginInit();
      this.radLabelHeight.BeginInit();
      this.radLabelRelativeSize.BeginInit();
      this.radLabelRelativeWidth.BeginInit();
      this.radLabelRelativeHeight.BeginInit();
      this.radSpinEditorWidth.BeginInit();
      this.radSpinEditorHeight.BeginInit();
      this.radSpinEditorRelativeWidth.BeginInit();
      this.radSpinEditorRelativeHeight.BeginInit();
      this.radButtonReset.BeginInit();
      this.radCheckBoxAspectRation.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelImageSize, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelWidth, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelHeight, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelRelativeSize, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelRelativeWidth, 0, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelRelativeHeight, 1, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorWidth, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorHeight, 1, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorRelativeWidth, 0, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorRelativeHeight, 1, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 7);
      this.tableLayoutPanel1.Controls.Add((Control) this.radCheckBoxAspectRation, 0, 6);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 8;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
      this.tableLayoutPanel1.Size = new Size(292, 251);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelImageSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelImageSize.Location = new Point(3, 10);
      this.radLabelImageSize.Name = "radLabelImageSize";
      this.radLabelImageSize.Size = new Size(82, 18);
      this.radLabelImageSize.TabIndex = 0;
      this.radLabelImageSize.Text = "Image Size (px)";
      this.radLabelWidth.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelWidth.Location = new Point(3, 41);
      this.radLabelWidth.Name = "radLabelWidth";
      this.radLabelWidth.Size = new Size(39, 18);
      this.radLabelWidth.TabIndex = 0;
      this.radLabelWidth.Text = "Width:";
      this.radLabelHeight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelHeight.Location = new Point(149, 41);
      this.radLabelHeight.Name = "radLabelHeight";
      this.radLabelHeight.Size = new Size(42, 18);
      this.radLabelHeight.TabIndex = 0;
      this.radLabelHeight.Text = "Height:";
      this.radLabelRelativeSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelRelativeSize.Location = new Point(3, 103);
      this.radLabelRelativeSize.Name = "radLabelRelativeSize";
      this.radLabelRelativeSize.Size = new Size(87, 18);
      this.radLabelRelativeSize.TabIndex = 0;
      this.radLabelRelativeSize.Text = "Relative Size (%)";
      this.radLabelRelativeWidth.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelRelativeWidth.Location = new Point(3, 134);
      this.radLabelRelativeWidth.Name = "radLabelRelativeWidth";
      this.radLabelRelativeWidth.Size = new Size(39, 18);
      this.radLabelRelativeWidth.TabIndex = 0;
      this.radLabelRelativeWidth.Text = "Width:";
      this.radLabelRelativeHeight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelRelativeHeight.Location = new Point(149, 134);
      this.radLabelRelativeHeight.Name = "radLabelRelativeHeight";
      this.radLabelRelativeHeight.Size = new Size(42, 18);
      this.radLabelRelativeHeight.TabIndex = 0;
      this.radLabelRelativeHeight.Text = "Height:";
      this.radSpinEditorWidth.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorWidth.Location = new Point(3, 67);
      this.radSpinEditorWidth.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorWidth.Maximum = new Decimal(new int[4]
      {
        (int) short.MaxValue,
        0,
        0,
        0
      });
      this.radSpinEditorWidth.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorWidth.Name = "radSpinEditorWidth";
      this.radSpinEditorWidth.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      }));
      this.radSpinEditorWidth.Size = new Size(140, 20);
      this.radSpinEditorWidth.TabIndex = 2;
      this.radSpinEditorWidth.TabStop = false;
      this.radSpinEditorWidth.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorHeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorHeight.Location = new Point(149, 67);
      this.radSpinEditorHeight.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorHeight.Maximum = new Decimal(new int[4]
      {
        (int) short.MaxValue,
        0,
        0,
        0
      });
      this.radSpinEditorHeight.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorHeight.Name = "radSpinEditorHeight";
      this.radSpinEditorHeight.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      }));
      this.radSpinEditorHeight.Size = new Size(140, 20);
      this.radSpinEditorHeight.TabIndex = 2;
      this.radSpinEditorHeight.TabStop = false;
      this.radSpinEditorHeight.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorRelativeWidth.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorRelativeWidth.DecimalPlaces = 2;
      this.radSpinEditorRelativeWidth.Location = new Point(3, 160);
      this.radSpinEditorRelativeWidth.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorRelativeWidth.Maximum = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.radSpinEditorRelativeWidth.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        262144
      });
      this.radSpinEditorRelativeWidth.Name = "radSpinEditorRelativeWidth";
      this.radSpinEditorRelativeWidth.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        1,
        0,
        0,
        262144
      }));
      this.radSpinEditorRelativeWidth.Size = new Size(140, 20);
      this.radSpinEditorRelativeWidth.TabIndex = 2;
      this.radSpinEditorRelativeWidth.TabStop = false;
      this.radSpinEditorRelativeWidth.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        262144
      });
      this.radSpinEditorRelativeHeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorRelativeHeight.DecimalPlaces = 2;
      this.radSpinEditorRelativeHeight.Location = new Point(149, 160);
      this.radSpinEditorRelativeHeight.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorRelativeHeight.Maximum = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.radSpinEditorRelativeHeight.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        262144
      });
      this.radSpinEditorRelativeHeight.Name = "radSpinEditorRelativeHeight";
      this.radSpinEditorRelativeHeight.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        1,
        0,
        0,
        262144
      }));
      this.radSpinEditorRelativeHeight.Size = new Size(140, 20);
      this.radSpinEditorRelativeHeight.TabIndex = 2;
      this.radSpinEditorRelativeHeight.TabStop = false;
      this.radSpinEditorRelativeHeight.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        262144
      });
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(179, 224);
      this.radButtonReset.Name = "radButtonReset";
      this.radButtonReset.Size = new Size(110, 24);
      this.radButtonReset.TabIndex = 1;
      this.radButtonReset.Text = "Reset";
      this.radCheckBoxAspectRation.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.radCheckBoxAspectRation, 2);
      this.radCheckBoxAspectRation.Location = new Point(3, 199);
      this.radCheckBoxAspectRation.Margin = new Padding(3, 0, 3, 0);
      this.radCheckBoxAspectRation.Name = "radCheckBoxAspectRation";
      this.radCheckBoxAspectRation.Size = new Size(128, 18);
      this.radCheckBoxAspectRation.TabIndex = 3;
      this.radCheckBoxAspectRation.Text = "Preserve Aspect Ratio";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 251);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ResizeDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (ResizeDialog);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelImageSize.EndInit();
      this.radLabelWidth.EndInit();
      this.radLabelHeight.EndInit();
      this.radLabelRelativeSize.EndInit();
      this.radLabelRelativeWidth.EndInit();
      this.radLabelRelativeHeight.EndInit();
      this.radSpinEditorWidth.EndInit();
      this.radSpinEditorHeight.EndInit();
      this.radSpinEditorRelativeWidth.EndInit();
      this.radSpinEditorRelativeHeight.EndInit();
      this.radButtonReset.EndInit();
      this.radCheckBoxAspectRation.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
