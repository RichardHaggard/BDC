// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgLogin
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
  internal class dlgLogin : Form
  {
    private IContainer components;

    public dlgLogin()
    {
      this.Load += new EventHandler(this.Page_Load);
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
      this.txtUserName = new TextBox();
      this.txtPassword = new TextBox();
      this.lblInvalidLogin = new Label();
      this.btnLogin = new Button();
      this.Label1 = new Label();
      this.Label2 = new Label();
      this.btnCancel = new Button();
      this.SuspendLayout();
      this.txtUserName.Location = new Point(110, 31);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new Size(139, 20);
      this.txtUserName.TabIndex = 0;
      this.txtPassword.Location = new Point(110, 76);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new Size(139, 20);
      this.txtPassword.TabIndex = 1;
      this.lblInvalidLogin.AutoSize = true;
      this.lblInvalidLogin.ForeColor = Color.Red;
      this.lblInvalidLogin.Location = new Point(45, 161);
      this.lblInvalidLogin.Name = "lblInvalidLogin";
      this.lblInvalidLogin.Size = new Size(0, 13);
      this.lblInvalidLogin.TabIndex = 3;
      this.btnLogin.Location = new Point(57, 119);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new Size(75, 23);
      this.btnLogin.TabIndex = 4;
      this.btnLogin.Text = "Login";
      this.btnLogin.UseVisualStyleBackColor = true;
      this.Label1.AutoSize = true;
      this.Label1.Location = new Point(45, 35);
      this.Label1.Name = "Label1";
      this.Label1.Size = new Size(63, 13);
      this.Label1.TabIndex = 5;
      this.Label1.Text = "User Name:";
      this.Label2.AutoSize = true;
      this.Label2.Location = new Point(52, 80);
      this.Label2.Name = "Label2";
      this.Label2.Size = new Size(56, 13);
      this.Label2.TabIndex = 6;
      this.Label2.Text = "Password:";
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(162, 119);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.AcceptButton = (IButtonControl) this.btnLogin;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(294, 192);
      this.ControlBox = false;
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.Label2);
      this.Controls.Add((Control) this.Label1);
      this.Controls.Add((Control) this.btnLogin);
      this.Controls.Add((Control) this.lblInvalidLogin);
      this.Controls.Add((Control) this.txtPassword);
      this.Controls.Add((Control) this.txtUserName);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (dlgLogin);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Login";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TextBox txtUserName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtPassword { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblInvalidLogin { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnLogin { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnCancel
    {
      get
      {
        return this._btnCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Button1_Click);
        Button btnCancel1 = this._btnCancel;
        if (btnCancel1 != null)
          btnCancel1.Click -= eventHandler;
        this._btnCancel = value;
        Button btnCancel2 = this._btnCancel;
        if (btnCancel2 == null)
          return;
        btnCancel2.Click += eventHandler;
      }
    }

    private void Page_Load(object sender, EventArgs e)
    {
      mdUtility.LoggedIn = false;
      this.txtPassword.PasswordChar = '*';
      this.txtUserName.Focus();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
