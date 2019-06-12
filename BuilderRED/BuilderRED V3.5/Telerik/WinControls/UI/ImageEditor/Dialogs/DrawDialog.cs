// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.DrawDialog
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
  public class DrawDialog : ImageEditorBaseDialog
  {
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelBrushSize;
    private RadSpinEditor radSpinEditorSize;
    private RadColorBox radColorBoxColor;
    private RadLabel radLabelBrushColor;

    public DrawDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.ApplySettings();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 84), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 56), this.RootElement.DpiScaleFactor);
    }

    protected override void WireEvents()
    {
      this.radSpinEditorSize.ValueChanged += new EventHandler(this.RadSpinEditorSize_ValueChanged);
      this.radColorBoxColor.ValueChanged += new EventHandler(this.RadColorBoxColor_ValueChanged);
    }

    protected override void UnwireEvents()
    {
      this.radSpinEditorSize.ValueChanged -= new EventHandler(this.RadSpinEditorSize_ValueChanged);
      this.radColorBoxColor.ValueChanged -= new EventHandler(this.RadColorBoxColor_ValueChanged);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.StartDrawing(new ShapeInfo(ShapeType.Freeflow, Color.Empty, this.radColorBoxColor.Value, (int) this.radSpinEditorSize.Value));
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawDialogTitle");
      this.radLabelBrushSize.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawDialogBrushSize");
      this.radLabelBrushColor.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawDialogBrushColor");
    }

    private void RadColorBoxColor_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadSpinEditorSize_ValueChanged(object sender, EventArgs e)
    {
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
      this.radLabelBrushSize = new RadLabel();
      this.radSpinEditorSize = new RadSpinEditor();
      this.radColorBoxColor = new RadColorBox();
      this.radLabelBrushColor = new RadLabel();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelBrushSize.BeginInit();
      this.radSpinEditorSize.BeginInit();
      this.radColorBoxColor.BeginInit();
      this.radLabelBrushColor.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBrushSize, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorSize, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxColor, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBrushColor, 0, 1);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Size = new Size(292, 76);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelBrushSize.Anchor = AnchorStyles.Left;
      this.radLabelBrushSize.Location = new Point(3, 10);
      this.radLabelBrushSize.Name = "radLabelBrushSize";
      this.radLabelBrushSize.Size = new Size(57, 18);
      this.radLabelBrushSize.TabIndex = 0;
      this.radLabelBrushSize.Text = "Brush Size";
      this.radSpinEditorSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorSize.Location = new Point(149, 9);
      this.radSpinEditorSize.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorSize.Maximum = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.radSpinEditorSize.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorSize.Name = "radSpinEditorSize";
      this.radSpinEditorSize.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      }));
      this.radSpinEditorSize.Size = new Size(140, 20);
      this.radSpinEditorSize.TabIndex = 1;
      this.radSpinEditorSize.TabStop = false;
      this.radSpinEditorSize.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radColorBoxColor.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxColor.Location = new Point(149, 47);
      this.radColorBoxColor.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxColor.Name = "radColorBoxColor";
      this.radColorBoxColor.Size = new Size(140, 20);
      this.radColorBoxColor.TabIndex = 2;
      this.radColorBoxColor.Value = Color.Black;
      this.radLabelBrushColor.Anchor = AnchorStyles.Left;
      this.radLabelBrushColor.Location = new Point(3, 48);
      this.radLabelBrushColor.Name = "radLabelBrushColor";
      this.radLabelBrushColor.Size = new Size(64, 18);
      this.radLabelBrushColor.TabIndex = 0;
      this.radLabelBrushColor.Text = "Brush Color";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 56);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (DrawDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Draw";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelBrushSize.EndInit();
      this.radSpinEditorSize.EndInit();
      this.radColorBoxColor.EndInit();
      this.radLabelBrushColor.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
