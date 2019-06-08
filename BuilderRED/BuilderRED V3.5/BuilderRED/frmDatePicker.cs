// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmDatePicker
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
  public class frmDatePicker : Form
  {
    private IContainer components;

    public frmDatePicker()
    {
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
      this.OKButton = new Button();
      this.DateTimePicker1 = new DateTimePicker();
      this.SuspendLayout();
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.FlatStyle = FlatStyle.System;
      this.OKButton.Location = new Point(219, 12);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 23);
      this.OKButton.TabIndex = 1;
      this.OKButton.Text = "OK";
      this.DateTimePicker1.Location = new Point(13, 13);
      this.DateTimePicker1.Name = "DateTimePicker1";
      this.DateTimePicker1.Size = new Size(200, 20);
      this.DateTimePicker1.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.ClientSize = new Size(310, 43);
      this.ControlBox = false;
      this.Controls.Add((Control) this.DateTimePicker1);
      this.Controls.Add((Control) this.OKButton);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmDatePicker);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Select date of inspection ";
      this.TopMost = true;
      this.ResumeLayout(false);
    }

    public virtual Button OKButton
    {
      get
      {
        return this._OKButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.OKButton_Click);
        Button okButton1 = this._OKButton;
        if (okButton1 != null)
          okButton1.Click -= eventHandler;
        this._OKButton = value;
        Button okButton2 = this._OKButton;
        if (okButton2 == null)
          return;
        okButton2.Click += eventHandler;
      }
    }

    internal virtual DateTimePicker DateTimePicker1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public DateTime ShowDialog(DateTime? minDate = null, DateTime? maxDate = null)
    {
      if (minDate.HasValue)
        this.DateTimePicker1.MinDate = minDate.Value;
      if (maxDate.HasValue)
        this.DateTimePicker1.MaxDate = maxDate.Value;
      int num = (int) this.ShowDialog();
      return this.DateTimePicker1.Value;
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
