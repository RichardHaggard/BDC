// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgCalculateArea
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  public class dlgCalculateArea : Form
  {
    private IContainer components;
    private bool nonNumberEntered;

    public dlgCalculateArea()
    {
      this.nonNumberEntered = false;
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || this.components == null)
          return;
        this.components.Dispose();
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.lblLength = new Label();
      this.txtLength = new TextBox();
      this.lblTimes = new Label();
      this.lblWidth = new Label();
      this.txtWidth = new TextBox();
      this.lblEqual = new Label();
      this.lblArea = new Label();
      this.lblAreaResult = new Label();
      this.cmdOK = new Button();
      this.cmdCancel = new Button();
      this.lblOriginalValue = new Label();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 6;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblLength, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtLength, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblTimes, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblWidth, 2, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtWidth, 2, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblEqual, 3, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblArea, 4, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblAreaResult, 4, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdOK, 5, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCancel, 5, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblOriginalValue, 0, 2);
      this.TableLayoutPanel1.Location = new Point(24, 23);
      this.TableLayoutPanel1.Margin = new Padding(6);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38f));
      this.TableLayoutPanel1.Size = new Size(810, 156);
      this.TableLayoutPanel1.TabIndex = 0;
      this.lblLength.AutoSize = true;
      this.lblLength.Dock = DockStyle.Fill;
      this.lblLength.Location = new Point(6, 0);
      this.lblLength.Margin = new Padding(6, 0, 6, 0);
      this.lblLength.Name = "lblLength";
      this.lblLength.Size = new Size(196, 56);
      this.lblLength.TabIndex = 0;
      this.lblLength.Text = "Length";
      this.lblLength.TextAlign = ContentAlignment.BottomCenter;
      this.txtLength.Dock = DockStyle.Fill;
      this.txtLength.Location = new Point(6, 62);
      this.txtLength.Margin = new Padding(6);
      this.txtLength.Name = "txtLength";
      this.txtLength.Size = new Size(196, 20);
      this.txtLength.TabIndex = 1;
      this.lblTimes.AutoSize = true;
      this.lblTimes.Dock = DockStyle.Fill;
      this.lblTimes.Location = new Point(214, 0);
      this.lblTimes.Margin = new Padding(6, 0, 6, 0);
      this.lblTimes.Name = "lblTimes";
      this.lblTimes.Size = new Size(12, 56);
      this.lblTimes.TabIndex = 2;
      this.lblTimes.Text = "x";
      this.lblTimes.TextAlign = ContentAlignment.BottomCenter;
      this.lblWidth.AutoSize = true;
      this.lblWidth.Dock = DockStyle.Fill;
      this.lblWidth.Location = new Point(238, 0);
      this.lblWidth.Margin = new Padding(6, 0, 6, 0);
      this.lblWidth.Name = "lblWidth";
      this.lblWidth.Size = new Size(196, 56);
      this.lblWidth.TabIndex = 3;
      this.lblWidth.Text = "Width";
      this.lblWidth.TextAlign = ContentAlignment.BottomCenter;
      this.txtWidth.Dock = DockStyle.Fill;
      this.txtWidth.Location = new Point(238, 62);
      this.txtWidth.Margin = new Padding(6);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new Size(196, 20);
      this.txtWidth.TabIndex = 4;
      this.lblEqual.AutoSize = true;
      this.lblEqual.Dock = DockStyle.Fill;
      this.lblEqual.Location = new Point(446, 0);
      this.lblEqual.Margin = new Padding(6, 0, 6, 0);
      this.lblEqual.Name = "lblEqual";
      this.lblEqual.Size = new Size(13, 56);
      this.lblEqual.TabIndex = 5;
      this.lblEqual.Text = "=";
      this.lblEqual.TextAlign = ContentAlignment.BottomCenter;
      this.lblArea.AutoSize = true;
      this.lblArea.Dock = DockStyle.Fill;
      this.lblArea.Location = new Point(471, 0);
      this.lblArea.Margin = new Padding(6, 0, 6, 0);
      this.lblArea.Name = "lblArea";
      this.lblArea.Size = new Size(171, 56);
      this.lblArea.TabIndex = 6;
      this.lblArea.Text = "Area";
      this.lblArea.TextAlign = ContentAlignment.BottomCenter;
      this.lblAreaResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblAreaResult.AutoSize = true;
      this.lblAreaResult.Location = new Point(471, 56);
      this.lblAreaResult.Margin = new Padding(6, 0, 6, 0);
      this.lblAreaResult.Name = "lblAreaResult";
      this.lblAreaResult.Size = new Size(171, 56);
      this.lblAreaResult.TabIndex = 7;
      this.lblAreaResult.TextAlign = ContentAlignment.MiddleCenter;
      this.cmdOK.DialogResult = DialogResult.OK;
      this.cmdOK.Dock = DockStyle.Fill;
      this.cmdOK.Location = new Point(654, 6);
      this.cmdOK.Margin = new Padding(6);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new Size(150, 44);
      this.cmdOK.TabIndex = 8;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdCancel.DialogResult = DialogResult.Cancel;
      this.cmdCancel.Dock = DockStyle.Fill;
      this.cmdCancel.Location = new Point(654, 62);
      this.cmdCancel.Margin = new Padding(6);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(150, 44);
      this.cmdCancel.TabIndex = 9;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.lblOriginalValue.AutoSize = true;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblOriginalValue, 5);
      this.lblOriginalValue.Dock = DockStyle.Fill;
      this.lblOriginalValue.Location = new Point(6, 112);
      this.lblOriginalValue.Margin = new Padding(6, 0, 6, 0);
      this.lblOriginalValue.Name = "lblOriginalValue";
      this.lblOriginalValue.Size = new Size(636, 44);
      this.lblOriginalValue.TabIndex = 10;
      this.lblOriginalValue.Text = "Previous Value:";
      this.lblOriginalValue.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOriginalValue.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(858, 202);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Margin = new Padding(6);
      this.Name = nameof (dlgCalculateArea);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Calculate Quantity";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblLength { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtLength
    {
      get
      {
        return this._txtLength;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtLength_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtLength_KeyPress);
        EventHandler eventHandler = new EventHandler(this.txtLength_TextChanged);
        TextBox txtLength1 = this._txtLength;
        if (txtLength1 != null)
        {
          txtLength1.KeyDown -= keyEventHandler;
          txtLength1.KeyPress -= pressEventHandler;
          txtLength1.TextChanged -= eventHandler;
        }
        this._txtLength = value;
        TextBox txtLength2 = this._txtLength;
        if (txtLength2 == null)
          return;
        txtLength2.KeyDown += keyEventHandler;
        txtLength2.KeyPress += pressEventHandler;
        txtLength2.TextChanged += eventHandler;
      }
    }

    internal virtual Label lblTimes { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblWidth { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtWidth
    {
      get
      {
        return this._txtWidth;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtWidth_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtWidth_KeyPress);
        EventHandler eventHandler = new EventHandler(this.txtWidth_TextChanged);
        TextBox txtWidth1 = this._txtWidth;
        if (txtWidth1 != null)
        {
          txtWidth1.KeyDown -= keyEventHandler;
          txtWidth1.KeyPress -= pressEventHandler;
          txtWidth1.TextChanged -= eventHandler;
        }
        this._txtWidth = value;
        TextBox txtWidth2 = this._txtWidth;
        if (txtWidth2 == null)
          return;
        txtWidth2.KeyDown += keyEventHandler;
        txtWidth2.KeyPress += pressEventHandler;
        txtWidth2.TextChanged += eventHandler;
      }
    }

    internal virtual Label lblEqual { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblAreaResult { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button cmdOK { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button cmdCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblOriginalValue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public double CalculateArea(Form ParentForm, double PreviousValue = 0.0)
    {
      if (PreviousValue != 0.0)
      {
        this.lblOriginalValue.Text = "Previous Value: " + Conversions.ToString(PreviousValue);
        this.lblOriginalValue.Visible = true;
      }
      int num = (int) this.ShowDialog((IWin32Window) ParentForm);
      if (this.DialogResult == DialogResult.OK && (uint) Operators.CompareString(this.lblAreaResult.Text, "", false) > 0U)
        return Conversions.ToDouble(this.lblAreaResult.Text);
      return -1.0;
    }

    private void txtLength_KeyDown(object sender, KeyEventArgs e)
    {
      this.nonNumberEntered = false;
      if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)))
        return;
      this.nonNumberEntered = true;
    }

    private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.nonNumberEntered)
        return;
      e.Handled = true;
    }

    private void txtLength_TextChanged(object sender, EventArgs e)
    {
      this.CalculateAreaValue();
    }

    private void txtWidth_KeyDown(object sender, KeyEventArgs e)
    {
      this.nonNumberEntered = false;
      if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete || (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod)))
        return;
      this.nonNumberEntered = true;
    }

    private void txtWidth_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.nonNumberEntered)
        return;
      e.Handled = true;
    }

    private void txtWidth_TextChanged(object sender, EventArgs e)
    {
      this.CalculateAreaValue();
    }

    private void CalculateAreaValue()
    {
      if (!((uint) Operators.CompareString(this.txtLength.Text, "", false) > 0U & (uint) Operators.CompareString(this.txtWidth.Text, "", false) > 0U))
        return;
      this.lblAreaResult.Text = Conversions.ToString(Conversions.ToDouble(this.txtLength.Text) * Conversions.ToDouble(this.txtWidth.Text));
    }
  }
}
