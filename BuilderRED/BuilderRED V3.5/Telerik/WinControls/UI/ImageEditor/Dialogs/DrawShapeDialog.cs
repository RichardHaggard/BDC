// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.DrawShapeDialog
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
  public class DrawShapeDialog : ImageEditorBaseDialog
  {
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelShape;
    private RadLabel radLabelBorderThickness;
    private RadLabel radLabelBorderColor;
    private RadColorBox radColorBoxFill;
    private RadColorBox radColorBoxBorder;
    private RadSpinEditor radSpinEditorBorderThickness;
    private RadDropDownList radDropDownListShape;
    private RadLabel radLabelShapeFill;

    public DrawShapeDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      foreach (object obj in Enum.GetValues(typeof (ShapeType)))
        this.radDropDownListShape.Items.Add(new RadListDataItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ShapeType" + obj.ToString()), obj));
      this.radDropDownListShape.SelectedIndex = 1;
      this.ApplySettings();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 196), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 132), this.RootElement.DpiScaleFactor);
    }

    protected override void WireEvents()
    {
      this.radDropDownListShape.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.RadDropDownListShape_SelectedIndexChanged);
      this.radColorBoxFill.ValueChanged += new EventHandler(this.RadColorBoxFill_ValueChanged);
      this.radSpinEditorBorderThickness.ValueChanged += new EventHandler(this.RadSpinEditorBorderThickness_ValueChanged);
      this.radColorBoxBorder.ValueChanged += new EventHandler(this.RadColorBoxBorder_ValueChanged);
    }

    protected override void UnwireEvents()
    {
      this.radDropDownListShape.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.RadDropDownListShape_SelectedIndexChanged);
      this.radColorBoxFill.ValueChanged -= new EventHandler(this.RadColorBoxFill_ValueChanged);
      this.radSpinEditorBorderThickness.ValueChanged -= new EventHandler(this.RadSpinEditorBorderThickness_ValueChanged);
      this.radColorBoxBorder.ValueChanged -= new EventHandler(this.RadColorBoxBorder_ValueChanged);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.StartDrawing(new ShapeInfo((ShapeType) this.radDropDownListShape.SelectedValue, this.radColorBoxFill.Value, this.radColorBoxBorder.Value, (int) this.radSpinEditorBorderThickness.Value));
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawShapeDialogTitle");
      this.radLabelShape.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawShapeDialogShape");
      this.radLabelShapeFill.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawShapeDialogShapeFill");
      this.radLabelBorderThickness.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawShapeDialogBorderThickness");
      this.radLabelBorderColor.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("DrawShapeDialogBorderColor");
    }

    private void RadColorBoxBorder_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadSpinEditorBorderThickness_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadColorBoxFill_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadDropDownListShape_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.ApplySettings();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
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
      this.radLabelShape = new RadLabel();
      this.radLabelBorderThickness = new RadLabel();
      this.radLabelBorderColor = new RadLabel();
      this.radColorBoxFill = new RadColorBox();
      this.radColorBoxBorder = new RadColorBox();
      this.radSpinEditorBorderThickness = new RadSpinEditor();
      this.radDropDownListShape = new RadDropDownList();
      this.radLabelShapeFill = new RadLabel();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelShape.BeginInit();
      this.radLabelBorderThickness.BeginInit();
      this.radLabelBorderColor.BeginInit();
      this.radColorBoxFill.BeginInit();
      this.radColorBoxBorder.BeginInit();
      this.radSpinEditorBorderThickness.BeginInit();
      this.radDropDownListShape.BeginInit();
      this.radLabelShapeFill.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelShape, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBorderThickness, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxFill, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxBorder, 1, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorBorderThickness, 1, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radDropDownListShape, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelShapeFill, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBorderColor, 0, 3);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tableLayoutPanel1.Size = new Size(292, 152);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelShape.Anchor = AnchorStyles.Left;
      this.radLabelShape.Location = new Point(3, 10);
      this.radLabelShape.Name = "radLabelShape";
      this.radLabelShape.Size = new Size(37, 18);
      this.radLabelShape.TabIndex = 0;
      this.radLabelShape.Text = "Shape";
      this.radLabelBorderThickness.Anchor = AnchorStyles.Left;
      this.radLabelBorderThickness.Location = new Point(3, 86);
      this.radLabelBorderThickness.Name = "radLabelBorderThickness";
      this.radLabelBorderThickness.Size = new Size(91, 18);
      this.radLabelBorderThickness.TabIndex = 0;
      this.radLabelBorderThickness.Text = "Border Thickness";
      this.radLabelBorderColor.Anchor = AnchorStyles.Left;
      this.radLabelBorderColor.Location = new Point(3, 124);
      this.radLabelBorderColor.Name = "radLabelBorderColor";
      this.radLabelBorderColor.Size = new Size(70, 18);
      this.radLabelBorderColor.TabIndex = 0;
      this.radLabelBorderColor.Text = "Border Color";
      this.radColorBoxFill.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxFill.Location = new Point(149, 47);
      this.radColorBoxFill.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxFill.Name = "radColorBoxFill";
      this.radColorBoxFill.Size = new Size(140, 20);
      this.radColorBoxFill.TabIndex = 3;
      this.radColorBoxFill.Value = Color.Transparent;
      this.radColorBoxBorder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxBorder.Location = new Point(149, 123);
      this.radColorBoxBorder.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxBorder.Name = "radColorBoxBorder";
      this.radColorBoxBorder.Size = new Size(140, 20);
      this.radColorBoxBorder.TabIndex = 3;
      this.radColorBoxBorder.Value = Color.Black;
      this.radSpinEditorBorderThickness.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorBorderThickness.Location = new Point(149, 85);
      this.radSpinEditorBorderThickness.Margin = new Padding(3, 0, 3, 0);
      this.radSpinEditorBorderThickness.Maximum = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.radSpinEditorBorderThickness.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorBorderThickness.Name = "radSpinEditorBorderThickness";
      this.radSpinEditorBorderThickness.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      }));
      this.radSpinEditorBorderThickness.Size = new Size(140, 20);
      this.radSpinEditorBorderThickness.TabIndex = 4;
      this.radSpinEditorBorderThickness.TabStop = false;
      this.radSpinEditorBorderThickness.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radDropDownListShape.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radDropDownListShape.DropDownStyle = RadDropDownStyle.DropDownList;
      this.radDropDownListShape.Location = new Point(149, 9);
      this.radDropDownListShape.Margin = new Padding(3, 0, 3, 0);
      this.radDropDownListShape.Name = "radDropDownListShape";
      this.radDropDownListShape.Size = new Size(140, 20);
      this.radDropDownListShape.TabIndex = 5;
      this.radDropDownListShape.Text = "radDropDownList1";
      this.radLabelShapeFill.Anchor = AnchorStyles.Left;
      this.radLabelShapeFill.Location = new Point(3, 48);
      this.radLabelShapeFill.Name = "radLabelShapeFill";
      this.radLabelShapeFill.Size = new Size(54, 18);
      this.radLabelShapeFill.TabIndex = 0;
      this.radLabelShapeFill.Text = "Shape Fill";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 132);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (DrawShapeDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (DrawShapeDialog);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelShape.EndInit();
      this.radLabelBorderThickness.EndInit();
      this.radLabelBorderColor.EndInit();
      this.radColorBoxFill.EndInit();
      this.radColorBoxBorder.EndInit();
      this.radSpinEditorBorderThickness.EndInit();
      this.radDropDownListShape.EndInit();
      this.radLabelShapeFill.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
