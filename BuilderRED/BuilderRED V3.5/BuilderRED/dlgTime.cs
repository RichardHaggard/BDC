// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgTime
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
  public class dlgTime : Form
  {
    private IContainer components;

    public dlgTime()
    {
      this.Load += new EventHandler(this.Time_Load);
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
      this.components = (IContainer) new Container();
      this.cmdOK = new Button();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.lblCurrentTime = new Label();
      this.Timer1 = new Timer(this.components);
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.cmdOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdOK.Location = new Point(366, 3);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new Size(75, 23);
      this.cmdOK.TabIndex = 2;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8f));
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdOK, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblCurrentTime, 0, 0);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 68f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.TableLayoutPanel1.Size = new Size(444, 113);
      this.TableLayoutPanel1.TabIndex = 11;
      this.lblCurrentTime.AutoSize = true;
      this.lblCurrentTime.Dock = DockStyle.Fill;
      this.lblCurrentTime.Font = new Font("Microsoft Sans Serif", 35f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblCurrentTime.Location = new Point(3, 0);
      this.lblCurrentTime.Name = "lblCurrentTime";
      this.TableLayoutPanel1.SetRowSpan((Control) this.lblCurrentTime, 2);
      this.lblCurrentTime.Size = new Size(354, 113);
      this.lblCurrentTime.TabIndex = 4;
      this.lblCurrentTime.Text = "Label1";
      this.lblCurrentTime.TextAlign = ContentAlignment.MiddleCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(444, 113);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MinimumSize = new Size(450, 137);
      this.Name = nameof (dlgTime);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Current Time";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual Button cmdOK
    {
      get
      {
        return this._cmdOK;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdOK_Click);
        Button cmdOk1 = this._cmdOK;
        if (cmdOk1 != null)
          cmdOk1.Click -= eventHandler;
        this._cmdOK = value;
        Button cmdOk2 = this._cmdOK;
        if (cmdOk2 == null)
          return;
        cmdOk2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblCurrentTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Timer Timer1
    {
      get
      {
        return this._Timer1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Timer1_Tick);
        Timer timer1_1 = this._Timer1;
        if (timer1_1 != null)
          timer1_1.Tick -= eventHandler;
        this._Timer1 = value;
        Timer timer1_2 = this._Timer1;
        if (timer1_2 == null)
          return;
        timer1_2.Tick += eventHandler;
      }
    }

    public double OpenTimeDialog(IWin32Window ParentForm)
    {
      int num1 = (int) this.ShowDialog(ParentForm);
      double num2;
      return num2;
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      this.lblCurrentTime.Text = DateTime.Now.ToLongTimeString();
    }

    private void Time_Load(object sender, EventArgs e)
    {
      this.Timer1.Start();
    }
  }
}
