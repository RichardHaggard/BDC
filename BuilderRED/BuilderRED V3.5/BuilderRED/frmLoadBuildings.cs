// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmLoadBuildings
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
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmLoadBuildings : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private Action m_ShowAction;
    private BackgroundWorker m_BackgroundWorker;

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual Label lblMessage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.lblMessage = new Label();
      this.RadWaitingBar1 = new RadWaitingBar();
      this.RadWaitingBar1.BeginInit();
      this.SuspendLayout();
      this.lblMessage.BackColor = SystemColors.Control;
      this.lblMessage.Cursor = Cursors.Default;
      this.lblMessage.ForeColor = SystemColors.ControlText;
      this.lblMessage.Location = new Point(21, 48);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.RightToLeft = RightToLeft.No;
      this.lblMessage.Size = new Size(370, 25);
      this.lblMessage.TabIndex = 0;
      this.lblMessage.Text = "While the buildings are being loaded.......";
      this.RadWaitingBar1.Location = new Point(21, 12);
      this.RadWaitingBar1.Name = "RadWaitingBar1";
      this.RadWaitingBar1.Size = new Size(370, 30);
      this.RadWaitingBar1.TabIndex = 1;
      this.RadWaitingBar1.Text = "RadWaitingBar1";
      this.RadWaitingBar1.UseWaitCursor = true;
      this.RadWaitingBar1.Visible = false;
      this.RadWaitingBar1.WaitingIndicatorSize = new Size(100, 30);
      this.RadWaitingBar1.WaitingStyle = WaitingBarStyles.Throbber;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(412, 85);
      this.ControlBox = false;
      this.Controls.Add((Control) this.RadWaitingBar1);
      this.Controls.Add((Control) this.lblMessage);
      this.Cursor = Cursors.WaitCursor;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(184, 250);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmLoadBuildings);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Please Wait";
      this.RadWaitingBar1.EndInit();
      this.ResumeLayout(false);
    }

    internal virtual RadWaitingBar RadWaitingBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public string ErrorMessage { get; set; }

    public frmLoadBuildings(string message = "While the buildings are being loaded.......")
    {
      this.Load += new EventHandler(this.frmLoadBuildings_Load);
      this.ErrorMessage = "";
      this.InitializeComponent();
      this.lblMessage.Text = message;
    }

    private void frmLoadBuildings_Load(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      this.RadWaitingBar1.StartWaiting();
    }

    public void ShowDialog(Action cmd)
    {
      this.m_BackgroundWorker = new BackgroundWorker();
      this.m_BackgroundWorker.DoWork += new DoWorkEventHandler(this.DoBackgroundWork);
      this.m_BackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWOrkCompleted);
      this.m_ShowAction = cmd;
      this.m_BackgroundWorker.RunWorkerAsync();
      this.RadWaitingBar1.Visible = true;
      int num = (int) this.ShowDialog();
    }

    public void DoBackgroundWork(object sender, DoWorkEventArgs e)
    {
      this.m_ShowAction();
    }

    public void BackgroundWOrkCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Error != null)
      {
        this.ErrorMessage = e.Error.Message;
        this.DialogResult = DialogResult.Abort;
      }
      this.Close();
    }
  }
}
