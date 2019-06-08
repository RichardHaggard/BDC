// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditor.Dialogs.ResizeCanvasDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI.ImageEditor.Dialogs
{
  public class ResizeCanvasDialog : ImageEditorBaseDialog
  {
    private DialogResult result = DialogResult.Cancel;
    private Size originalSize;
    private IContainer components;
    private TableLayoutPanel tableLayoutPanel1;
    private RadLabel radLabelCanvasSize;
    private RadLabel radLabelWidth;
    private RadLabel radLabelHeight;
    private RadLabel radLabelImageAlignment;
    private RadLabel radLabelBackground;
    private RadButton radButtonReset;
    private RadColorBox radColorBoxBackground;
    private RadSpinEditor radSpinEditorWidth;
    private RadSpinEditor radSpinEditorHeight;
    private TableLayoutPanel tableLayoutPanel2;
    private RadToggleButton radToggleButtonTopLeft;
    private RadToggleButton radToggleButtonTopCenter;
    private RadToggleButton radToggleButtonTopRight;
    private RadToggleButton radToggleButtonMiddleRight;
    private RadToggleButton radToggleButtonMiddleCenter;
    private RadToggleButton radToggleButtonMiddleLeft;
    private RadToggleButton radToggleButtonBottomLeft;
    private RadToggleButton radToggleButtonBottomCenter;
    private RadToggleButton radToggleButtonBottomRight;

    public ResizeCanvasDialog(RadImageEditorElement imageEditorElement)
      : base(imageEditorElement)
    {
      this.InitializeComponent();
      this.originalSize = imageEditorElement.CurrentBitmap.Size;
      this.radSpinEditorWidth.Value = (Decimal) this.originalSize.Width;
      this.radSpinEditorHeight.Value = (Decimal) this.originalSize.Height;
      this.WireEvents();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(438, 552), this.RootElement.DpiScaleFactor);
      else
        this.ClientSize = TelerikDpiHelper.ScaleSize(new Size(292, 368), this.RootElement.DpiScaleFactor);
    }

    protected override void WireEvents()
    {
      this.radToggleButtonTopLeft.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonTopCenter.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonTopRight.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonMiddleLeft.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonMiddleCenter.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonMiddleRight.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonBottomLeft.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonBottomCenter.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonBottomRight.ToggleStateChanging += new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radSpinEditorWidth.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorHeight.ValueChanged += new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radColorBoxBackground.ValueChanged += new EventHandler(this.RadColorBoxBackground_ValueChanged);
      this.radButtonReset.Click += new EventHandler(this.RadButtonReset_Click);
    }

    protected override void UnwireEvents()
    {
      this.radToggleButtonTopLeft.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonTopCenter.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonTopRight.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonMiddleLeft.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonMiddleCenter.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonMiddleRight.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonBottomLeft.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonBottomCenter.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radToggleButtonBottomRight.ToggleStateChanging -= new StateChangingEventHandler(this.RadToggleButton_ToggleStateChanging);
      this.radSpinEditorWidth.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radSpinEditorHeight.ValueChanged -= new EventHandler(this.RadSpinEditor_ValueChanged);
      this.radColorBoxBackground.ValueChanged -= new EventHandler(this.RadColorBoxBackground_ValueChanged);
      this.radButtonReset.Click -= new EventHandler(this.RadButtonReset_Click);
    }

    protected override void ApplySettings()
    {
      this.ImageEditorElement.ResizeCanvas((int) this.radSpinEditorWidth.Value, (int) this.radSpinEditorHeight.Value, this.GetImageAlignment(), this.radColorBoxBackground.Value);
      this.result = DialogResult.OK;
    }

    protected virtual ContentAlignment GetImageAlignment()
    {
      foreach (RadToggleButton control in (ArrangedElementCollection) this.tableLayoutPanel2.Controls)
      {
        if (control.IsChecked)
        {
          string str = control.Name.Split(new string[1]{ "Button" }, StringSplitOptions.RemoveEmptyEntries)[1];
          IEnumerator enumerator = Enum.GetValues(typeof (ContentAlignment)).GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              ContentAlignment current = (ContentAlignment) enumerator.Current;
              if (current.ToString() == str)
                return current;
            }
            break;
          }
          finally
          {
            (enumerator as IDisposable)?.Dispose();
          }
        }
      }
      return ContentAlignment.MiddleCenter;
    }

    protected override void LocalizeStrings()
    {
      this.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogTitle");
      this.radLabelCanvasSize.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogCanvasSize");
      this.radLabelWidth.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogWidth");
      this.radLabelHeight.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogHeight");
      this.radLabelImageAlignment.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogImageAlignment");
      this.radLabelBackground.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogBackground");
      this.radButtonReset.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CanvasResizeDialogReset");
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.DialogResult = this.result;
    }

    private void RadButtonReset_Click(object sender, EventArgs e)
    {
      this.UnwireEvents();
      this.radColorBoxBackground.Value = Color.Black;
      foreach (RadToggleButton control in (ArrangedElementCollection) this.tableLayoutPanel2.Controls)
        control.IsChecked = this.radToggleButtonMiddleCenter.Equals((object) control);
      this.radSpinEditorWidth.Value = (Decimal) this.originalSize.Width;
      this.radSpinEditorHeight.Value = (Decimal) this.originalSize.Height;
      this.WireEvents();
      this.ApplySettings();
      this.result = DialogResult.Cancel;
    }

    private void RadColorBoxBackground_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadSpinEditor_ValueChanged(object sender, EventArgs e)
    {
      this.ApplySettings();
    }

    private void RadToggleButton_ToggleStateChanging(object sender, StateChangingEventArgs e)
    {
      e.Cancel = true;
      this.UnwireEvents();
      foreach (RadToggleButton control in (ArrangedElementCollection) this.tableLayoutPanel2.Controls)
        control.IsChecked = sender.Equals((object) control);
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
      this.radLabelCanvasSize = new RadLabel();
      this.radLabelWidth = new RadLabel();
      this.radLabelHeight = new RadLabel();
      this.radLabelImageAlignment = new RadLabel();
      this.radLabelBackground = new RadLabel();
      this.radButtonReset = new RadButton();
      this.radColorBoxBackground = new RadColorBox();
      this.radSpinEditorWidth = new RadSpinEditor();
      this.radSpinEditorHeight = new RadSpinEditor();
      this.tableLayoutPanel2 = new TableLayoutPanel();
      this.radToggleButtonTopLeft = new RadToggleButton();
      this.radToggleButtonTopCenter = new RadToggleButton();
      this.radToggleButtonTopRight = new RadToggleButton();
      this.radToggleButtonMiddleRight = new RadToggleButton();
      this.radToggleButtonMiddleCenter = new RadToggleButton();
      this.radToggleButtonMiddleLeft = new RadToggleButton();
      this.radToggleButtonBottomLeft = new RadToggleButton();
      this.radToggleButtonBottomCenter = new RadToggleButton();
      this.radToggleButtonBottomRight = new RadToggleButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.radLabelCanvasSize.BeginInit();
      this.radLabelWidth.BeginInit();
      this.radLabelHeight.BeginInit();
      this.radLabelImageAlignment.BeginInit();
      this.radLabelBackground.BeginInit();
      this.radButtonReset.BeginInit();
      this.radColorBoxBackground.BeginInit();
      this.radSpinEditorWidth.BeginInit();
      this.radSpinEditorHeight.BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      this.radToggleButtonTopLeft.BeginInit();
      this.radToggleButtonTopCenter.BeginInit();
      this.radToggleButtonTopRight.BeginInit();
      this.radToggleButtonMiddleRight.BeginInit();
      this.radToggleButtonMiddleCenter.BeginInit();
      this.radToggleButtonMiddleLeft.BeginInit();
      this.radToggleButtonBottomLeft.BeginInit();
      this.radToggleButtonBottomCenter.BeginInit();
      this.radToggleButtonBottomRight.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelCanvasSize, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelWidth, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelHeight, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelImageAlignment, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radLabelBackground, 0, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radButtonReset, 1, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.radColorBoxBackground, 1, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorWidth, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.radSpinEditorHeight, 1, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel2, 0, 4);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 7;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.07148f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.07148f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.07148f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.07148f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 39.57114f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.07148f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.07148f));
      this.tableLayoutPanel1.Size = new Size(292, 368);
      this.tableLayoutPanel1.TabIndex = 0;
      this.radLabelCanvasSize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelCanvasSize.Location = new Point(3, 16);
      this.radLabelCanvasSize.Name = "radLabelCanvasSize";
      this.radLabelCanvasSize.Size = new Size(67, 18);
      this.radLabelCanvasSize.TabIndex = 0;
      this.radLabelCanvasSize.Text = "Canvas Size:";
      this.radLabelWidth.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelWidth.Location = new Point(3, 53);
      this.radLabelWidth.Name = "radLabelWidth";
      this.radLabelWidth.Size = new Size(39, 18);
      this.radLabelWidth.TabIndex = 0;
      this.radLabelWidth.Text = "Width:";
      this.radLabelHeight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelHeight.Location = new Point(149, 53);
      this.radLabelHeight.Name = "radLabelHeight";
      this.radLabelHeight.Size = new Size(42, 18);
      this.radLabelHeight.TabIndex = 0;
      this.radLabelHeight.Text = "Height:";
      this.radLabelImageAlignment.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.radLabelImageAlignment.Location = new Point(3, (int) sbyte.MaxValue);
      this.radLabelImageAlignment.Name = "radLabelImageAlignment";
      this.radLabelImageAlignment.Size = new Size(92, 18);
      this.radLabelImageAlignment.TabIndex = 0;
      this.radLabelImageAlignment.Text = "Image Alignment";
      this.radLabelBackground.Anchor = AnchorStyles.Left;
      this.radLabelBackground.Location = new Point(3, 302);
      this.radLabelBackground.Name = "radLabelBackground";
      this.radLabelBackground.Size = new Size(68, 18);
      this.radLabelBackground.TabIndex = 0;
      this.radLabelBackground.Text = "Background:";
      this.radButtonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonReset.Location = new Point(179, 341);
      this.radButtonReset.Name = "radButtonReset";
      this.radButtonReset.Size = new Size(110, 24);
      this.radButtonReset.TabIndex = 1;
      this.radButtonReset.Text = "Reset";
      this.radColorBoxBackground.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radColorBoxBackground.Location = new Point(149, 301);
      this.radColorBoxBackground.Margin = new Padding(3, 0, 3, 0);
      this.radColorBoxBackground.Name = "radColorBoxBackground";
      this.radColorBoxBackground.Size = new Size(140, 20);
      this.radColorBoxBackground.TabIndex = 2;
      this.radColorBoxBackground.Value = Color.Black;
      this.radSpinEditorWidth.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorWidth.Location = new Point(3, 82);
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
      this.radSpinEditorWidth.TabIndex = 3;
      this.radSpinEditorWidth.TabStop = false;
      this.radSpinEditorWidth.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.radSpinEditorHeight.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.radSpinEditorHeight.Location = new Point(149, 82);
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
      this.radSpinEditorHeight.TabIndex = 3;
      this.radSpinEditorHeight.TabStop = false;
      this.radSpinEditorHeight.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.tableLayoutPanel2.Anchor = AnchorStyles.None;
      this.tableLayoutPanel2.ColumnCount = 3;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.tableLayoutPanel2, 2);
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonTopLeft, 0, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonTopCenter, 1, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonTopRight, 2, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonMiddleRight, 2, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonMiddleCenter, 1, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonMiddleLeft, 0, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonBottomLeft, 0, 2);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonBottomCenter, 1, 2);
      this.tableLayoutPanel2.Controls.Add((Control) this.radToggleButtonBottomRight, 2, 2);
      this.tableLayoutPanel2.Location = new Point(81, 155);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 3;
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel2.Size = new Size(130, 130);
      this.tableLayoutPanel2.TabIndex = 4;
      this.radToggleButtonTopLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonTopLeft.Location = new Point(3, 3);
      this.radToggleButtonTopLeft.Name = "radToggleButtonTopLeft";
      this.radToggleButtonTopLeft.Size = new Size(37, 37);
      this.radToggleButtonTopLeft.TabIndex = 0;
      this.radToggleButtonTopCenter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonTopCenter.Location = new Point(46, 3);
      this.radToggleButtonTopCenter.Name = "radToggleButtonTopCenter";
      this.radToggleButtonTopCenter.Size = new Size(37, 37);
      this.radToggleButtonTopCenter.TabIndex = 0;
      this.radToggleButtonTopRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonTopRight.Location = new Point(89, 3);
      this.radToggleButtonTopRight.Name = "radToggleButtonTopRight";
      this.radToggleButtonTopRight.Size = new Size(38, 37);
      this.radToggleButtonTopRight.TabIndex = 0;
      this.radToggleButtonMiddleRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonMiddleRight.Location = new Point(89, 46);
      this.radToggleButtonMiddleRight.Name = "radToggleButtonMiddleRight";
      this.radToggleButtonMiddleRight.Size = new Size(38, 37);
      this.radToggleButtonMiddleRight.TabIndex = 0;
      this.radToggleButtonMiddleCenter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonMiddleCenter.CheckState = CheckState.Checked;
      this.radToggleButtonMiddleCenter.Location = new Point(46, 46);
      this.radToggleButtonMiddleCenter.Name = "radToggleButtonMiddleCenter";
      this.radToggleButtonMiddleCenter.Size = new Size(37, 37);
      this.radToggleButtonMiddleCenter.TabIndex = 0;
      this.radToggleButtonMiddleCenter.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radToggleButtonMiddleLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonMiddleLeft.Location = new Point(3, 46);
      this.radToggleButtonMiddleLeft.Name = "radToggleButtonMiddleLeft";
      this.radToggleButtonMiddleLeft.Size = new Size(37, 37);
      this.radToggleButtonMiddleLeft.TabIndex = 0;
      this.radToggleButtonBottomLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonBottomLeft.Location = new Point(3, 89);
      this.radToggleButtonBottomLeft.Name = "radToggleButtonBottomLeft";
      this.radToggleButtonBottomLeft.Size = new Size(37, 38);
      this.radToggleButtonBottomLeft.TabIndex = 0;
      this.radToggleButtonBottomCenter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonBottomCenter.Location = new Point(46, 89);
      this.radToggleButtonBottomCenter.Name = "radToggleButtonBottomCenter";
      this.radToggleButtonBottomCenter.Size = new Size(37, 38);
      this.radToggleButtonBottomCenter.TabIndex = 0;
      this.radToggleButtonBottomRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radToggleButtonBottomRight.Location = new Point(89, 89);
      this.radToggleButtonBottomRight.Name = "radToggleButtonBottomRight";
      this.radToggleButtonBottomRight.Size = new Size(38, 38);
      this.radToggleButtonBottomRight.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 368);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ResizeCanvasDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (ResizeCanvasDialog);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.radLabelCanvasSize.EndInit();
      this.radLabelWidth.EndInit();
      this.radLabelHeight.EndInit();
      this.radLabelImageAlignment.EndInit();
      this.radLabelBackground.EndInit();
      this.radButtonReset.EndInit();
      this.radColorBoxBackground.EndInit();
      this.radSpinEditorWidth.EndInit();
      this.radSpinEditorHeight.EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.radToggleButtonTopLeft.EndInit();
      this.radToggleButtonTopCenter.EndInit();
      this.radToggleButtonTopRight.EndInit();
      this.radToggleButtonMiddleRight.EndInit();
      this.radToggleButtonMiddleCenter.EndInit();
      this.radToggleButtonMiddleLeft.EndInit();
      this.radToggleButtonBottomLeft.EndInit();
      this.radToggleButtonBottomCenter.EndInit();
      this.radToggleButtonBottomRight.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
