// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgTimeoutMessage
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
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class dlgTimeoutMessage : Form
  {
    private IContainer components;
    private int iCountdown;

    public dlgTimeoutMessage()
    {
      this.iCountdown = 10;
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
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.lblMessage = new Label();
      this.cmdOK = new Button();
      this.lblTimeout = new RadLabel();
      this.Timer1 = new Timer(this.components);
      this.TableLayoutPanel1.SuspendLayout();
      this.lblTimeout.BeginInit();
      this.SuspendLayout();
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblMessage, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdOK, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblTimeout, 0, 1);
      this.TableLayoutPanel1.Location = new Point(12, 12);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.TableLayoutPanel1.Size = new Size(405, 81);
      this.TableLayoutPanel1.TabIndex = 0;
      this.lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lblMessage.AutoSize = true;
      this.lblMessage.Location = new Point(3, 0);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new Size(318, 29);
      this.lblMessage.TabIndex = 6;
      this.lblMessage.TextAlign = ContentAlignment.BottomCenter;
      this.cmdOK.DialogResult = DialogResult.OK;
      this.cmdOK.Location = new Point(327, 3);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new Size(75, 23);
      this.cmdOK.TabIndex = 8;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.lblTimeout.Location = new Point(3, 32);
      this.lblTimeout.Name = "lblTimeout";
      this.lblTimeout.Size = new Size(61, 16);
      this.lblTimeout.TabIndex = 9;
      this.lblTimeout.Text = "RadLabel1";
      this.Timer1.Enabled = true;
      this.Timer1.Interval = 1000;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(429, 105);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (dlgTimeoutMessage);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Notification";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.lblTimeout.EndInit();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblMessage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

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

    internal virtual RadLabel lblTimeout { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

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

    public void ShowMessage(Form ParentForm, string sMessage)
    {
      this.lblMessage.Text = sMessage;
      this.lblTimeout.Text = "Closing in " + Conversions.ToString(this.iCountdown) + " sec...";
      this.Show((IWin32Window) ParentForm);
      this.Timer1.Start();
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      if (this.iCountdown == -1)
        this.Close();
      this.lblTimeout.Text = "Closing in " + Conversions.ToString(this.iCountdown) + " sec...";
      // ISSUE: variable of a reference type
      int& local;
      // ISSUE: explicit reference operation
      int num = checked (^(local = ref this.iCountdown) - 1);
      local = num;
    }
  }
}
