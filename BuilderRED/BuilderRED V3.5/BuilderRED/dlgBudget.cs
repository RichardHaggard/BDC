// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgBudget
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
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
  public class dlgBudget : Form
  {
    private IContainer components;
    private bool nonNumberEntered;
    private string m_bldg_ID;

    public dlgBudget()
    {
      this.nonNumberEntered = false;
      this.m_bldg_ID = mdUtility.fMainForm.CurrentBldg;
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
      this.cmdOK = new Button();
      this.cmdCancel = new Button();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.txtBudget = new TextBox();
      this.Label1 = new Label();
      this.lblPreviousValue = new Label();
      this.lblPreviousLabel = new Label();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.cmdOK.Dock = DockStyle.Fill;
      this.cmdOK.Location = new Point(726, 6);
      this.cmdOK.Margin = new Padding(6, 6, 6, 6);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new Size(156, 46);
      this.cmdOK.TabIndex = 2;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdCancel.DialogResult = DialogResult.Cancel;
      this.cmdCancel.Dock = DockStyle.Fill;
      this.cmdCancel.Location = new Point(726, 64);
      this.cmdCancel.Margin = new Padding(6, 6, 6, 6);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(156, 60);
      this.cmdCancel.TabIndex = 3;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.TableLayoutPanel1.ColumnCount = 4;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPreviousLabel, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPreviousValue, 1, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdOK, 3, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCancel, 3, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtBudget, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.Label1, 0, 1);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Margin = new Padding(6, 6, 6, 6);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 4;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(888, 217);
      this.TableLayoutPanel1.TabIndex = 11;
      this.txtBudget.Dock = DockStyle.Fill;
      this.txtBudget.Location = new Point(285, 64);
      this.txtBudget.Margin = new Padding(6, 6, 6, 6);
      this.txtBudget.MaxLength = 15;
      this.txtBudget.Name = "txtBudget";
      this.txtBudget.Size = new Size(192, 31);
      this.txtBudget.TabIndex = 1;
      this.Label1.AutoSize = true;
      this.Label1.Dock = DockStyle.Fill;
      this.Label1.Location = new Point(3, 58);
      this.Label1.Name = "Label1";
      this.Label1.Size = new Size(273, 72);
      this.Label1.TabIndex = 14;
      this.Label1.Text = "Enter Budget:";
      this.Label1.TextAlign = ContentAlignment.TopRight;
      this.lblPreviousValue.AutoSize = true;
      this.lblPreviousValue.Dock = DockStyle.Fill;
      this.lblPreviousValue.Location = new Point(282, 130);
      this.lblPreviousValue.Name = "lblPreviousValue";
      this.lblPreviousValue.Size = new Size(198, 25);
      this.lblPreviousValue.TabIndex = 15;
      this.lblPreviousValue.Text = "Display Area Label";
      this.lblPreviousValue.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPreviousLabel.AutoSize = true;
      this.lblPreviousLabel.Dock = DockStyle.Fill;
      this.lblPreviousLabel.Location = new Point(3, 130);
      this.lblPreviousLabel.Name = "lblPreviousLabel";
      this.lblPreviousLabel.Size = new Size(273, 25);
      this.lblPreviousLabel.TabIndex = 16;
      this.lblPreviousLabel.Text = "Previously Entered Budget:";
      this.lblPreviousLabel.TextAlign = ContentAlignment.MiddleRight;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(888, 217);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Margin = new Padding(6, 6, 6, 6);
      this.MinimumSize = new Size(874, 198);
      this.Name = nameof (dlgBudget);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Budget Amount";
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

    internal virtual Button cmdCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtBudget
    {
      get
      {
        return this._txtBudget;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtBudget_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtBudget_KeyPress);
        EventHandler eventHandler = new EventHandler(this.txtBudget_TextChanged);
        TextBox txtBudget1 = this._txtBudget;
        if (txtBudget1 != null)
        {
          txtBudget1.KeyDown -= keyEventHandler;
          txtBudget1.KeyPress -= pressEventHandler;
          txtBudget1.TextChanged -= eventHandler;
        }
        this._txtBudget = value;
        TextBox txtBudget2 = this._txtBudget;
        if (txtBudget2 == null)
          return;
        txtBudget2.KeyDown += keyEventHandler;
        txtBudget2.KeyPress += pressEventHandler;
        txtBudget2.TextChanged += eventHandler;
      }
    }

    internal virtual Label lblPreviousValue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblPreviousLabel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public double OpenBudgetDialog(IWin32Window ParentForm, double dPrevious = 0.0, int sQuestionNum = 0)
    {
      if ((uint) sQuestionNum > 0U)
        this.Text = this.Text + " for Question " + Conversions.ToString(sQuestionNum);
      if (dPrevious != 0.0)
      {
        this.lblPreviousValue.Text = Strings.FormatCurrency((object) dPrevious, -1, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault);
      }
      else
      {
        this.lblPreviousLabel.Visible = false;
        this.lblPreviousValue.Visible = false;
      }
      int num = (int) this.ShowDialog(ParentForm);
      if (this.DialogResult == DialogResult.OK)
        return Conversions.ToDouble(Interaction.IIf((uint) Operators.CompareString(this.txtBudget.Text, "", false) > 0U, (object) this.txtBudget.Text, (object) dPrevious));
      return dPrevious;
    }

    private void txtBudget_KeyDown(object sender, KeyEventArgs e)
    {
      this.nonNumberEntered = false;
      if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
        this.cmdOK_Click((object) this.cmdOK, new EventArgs());
      else if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete && (e.KeyCode != Keys.Decimal && e.KeyCode != Keys.OemPeriod || Strings.InStr(this.txtBudget.Text, ".", CompareMethod.Binary) > 1)))
        this.nonNumberEntered = true;
    }

    private void txtBudget_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.nonNumberEntered)
        return;
      e.Handled = true;
    }

    private void txtBudget_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.txtBudget.Text.StartsWith("."))
          return;
        this.txtBudget.Text = "0.";
        this.txtBudget.SelectionStart = this.txtBudget.TextLength;
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        this.txtBudget.BackColor = Color.Red;
        mdUtility.Errorhandler(ex2, this.Name, nameof (txtBudget_TextChanged));
        ProjectData.ClearProjectError();
      }
      finally
      {
      }
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }
  }
}
