// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgCoverage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class dlgCoverage : Form
  {
    private IContainer components;
    private bool nonNumberEntered;
    private string m_bldg_ID;
    private double m_BuildingArea;

    public dlgCoverage()
    {
      this.nonNumberEntered = false;
      this.m_bldg_ID = mdUtility.fMainForm.CurrentBldg;
      this.m_BuildingArea = Conversions.ToDouble(mdUtility.DB.GetDataTable("SELECT Quantity FROM [Facility] where Facility_ID = '" + this.m_bldg_ID + "'").Rows[0]["Quantity"]);
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
      this.RadLabel1 = new RadLabel();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.lblPrevious = new RadLabel();
      this.txtPercent = new TextBox();
      this.RadLabel3 = new RadLabel();
      this.lblPreviousValue = new RadLabel();
      this.lblError = new RadLabel();
      this.RadLabel1.BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.lblPrevious.BeginInit();
      this.RadLabel3.BeginInit();
      this.lblPreviousValue.BeginInit();
      this.lblError.BeginInit();
      this.SuspendLayout();
      this.cmdOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdOK.Location = new Point(366, 3);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new Size(75, 23);
      this.cmdOK.TabIndex = 2;
      this.cmdOK.Text = "OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdCancel.DialogResult = DialogResult.Cancel;
      this.cmdCancel.Location = new Point(366, 33);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(75, 23);
      this.cmdCancel.TabIndex = 3;
      this.cmdCancel.Text = "Cancel";
      this.cmdCancel.UseVisualStyleBackColor = true;
      this.RadLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.RadLabel1, 2);
      this.RadLabel1.Location = new Point(49, 33);
      this.RadLabel1.Name = "RadLabel1";
      this.RadLabel1.Size = new Size(177, 16);
      this.RadLabel1.TabIndex = 10;
      this.RadLabel1.Text = "Enter Existing Coverage (1-100) : ";
      this.RadLabel1.TextAlignment = ContentAlignment.MiddleRight;
      this.TableLayoutPanel1.ColumnCount = 5;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.92308f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.07692f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPrevious, 0, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdOK, 4, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCancel, 4, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtPercent, 2, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.RadLabel1, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.RadLabel3, 3, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPreviousValue, 2, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblError, 1, 2);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 4;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
      this.TableLayoutPanel1.Size = new Size(444, 113);
      this.TableLayoutPanel1.TabIndex = 11;
      this.lblPrevious.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblPrevious, 2);
      this.lblPrevious.Location = new Point(66, 88);
      this.lblPrevious.Name = "lblPrevious";
      this.lblPrevious.Size = new Size(160, 16);
      this.lblPrevious.TabIndex = 15;
      this.lblPrevious.Text = "Previously Entered Coverage: ";
      this.txtPercent.Location = new Point(232, 33);
      this.txtPercent.MaxLength = 10;
      this.txtPercent.Name = "txtPercent";
      this.txtPercent.Size = new Size(84, 20);
      this.txtPercent.TabIndex = 1;
      this.RadLabel3.Location = new Point(322, 33);
      this.RadLabel3.Name = "RadLabel3";
      this.RadLabel3.Size = new Size(16, 16);
      this.RadLabel3.TabIndex = 14;
      this.RadLabel3.Text = "%";
      this.RadLabel3.TextAlignment = ContentAlignment.MiddleRight;
      this.lblPreviousValue.Location = new Point(232, 88);
      this.lblPreviousValue.Name = "lblPreviousValue";
      this.lblPreviousValue.Size = new Size(88, 18);
      this.lblPreviousValue.TabIndex = 16;
      this.lblPreviousValue.Text = "Display Previous";
      this.lblError.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblError, 3);
      this.lblError.ForeColor = Color.Red;
      this.lblError.Location = new Point(357, 63);
      this.lblError.Name = "lblError";
      this.lblError.Size = new Size(2, 2);
      this.lblError.TabIndex = 12;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(444, 113);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MinimumSize = new Size(450, 137);
      this.Name = nameof (dlgCoverage);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Building Coverage";
      this.RadLabel1.EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.lblPrevious.EndInit();
      this.RadLabel3.EndInit();
      this.lblPreviousValue.EndInit();
      this.lblError.EndInit();
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

    internal virtual RadLabel RadLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtPercent
    {
      get
      {
        return this._txtPercent;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtPercent_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtPercent_KeyPress);
        EventHandler eventHandler = new EventHandler(this.txtPercent_TextChanged);
        TextBox txtPercent1 = this._txtPercent;
        if (txtPercent1 != null)
        {
          txtPercent1.KeyDown -= keyEventHandler;
          txtPercent1.KeyPress -= pressEventHandler;
          txtPercent1.TextChanged -= eventHandler;
        }
        this._txtPercent = value;
        TextBox txtPercent2 = this._txtPercent;
        if (txtPercent2 == null)
          return;
        txtPercent2.KeyDown += keyEventHandler;
        txtPercent2.KeyPress += pressEventHandler;
        txtPercent2.TextChanged += eventHandler;
      }
    }

    internal virtual RadLabel lblError { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblPrevious { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblPreviousValue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public double GetCoverage(Form ParentForm, int iCMC_ID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT *  FROM Missing_Components WHERE Building_ID = '" + this.m_bldg_ID + "' AND MC_CMC_ID = " + Conversions.ToString(iCMC_ID));
      double dblTemp = dataTable.Rows.Count != 1 ? 0.0 : Conversions.ToDouble(dataTable.Rows[0]["MC_Coverage"]);
      if (dblTemp != 0.0)
      {
        this.lblPreviousValue.Text = mdUtility.FormatDoubleForDisplay(ref dblTemp);
      }
      else
      {
        this.lblPrevious.Visible = false;
        this.lblPreviousValue.Visible = false;
      }
      int num = (int) this.ShowDialog((IWin32Window) ParentForm);
      if (this.DialogResult == DialogResult.OK)
        return Conversions.ToDouble(this.txtPercent.Text);
      return -1.0;
    }

    private void txtPercent_KeyDown(object sender, KeyEventArgs e)
    {
      this.nonNumberEntered = false;
      if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Return)
        this.cmdOK_Click((object) this.cmdOK, new EventArgs());
      else if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete && (e.KeyCode != Keys.Decimal && e.KeyCode != Keys.OemPeriod || Strings.InStr(this.txtPercent.Text, ".", CompareMethod.Binary) > 1)))
        this.nonNumberEntered = true;
    }

    private void txtPercent_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.nonNumberEntered)
        return;
      e.Handled = true;
    }

    private void txtPercent_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.txtPercent.Text.StartsWith("."))
        {
          this.txtPercent.Text = "0.";
          this.txtPercent.SelectionStart = this.txtPercent.TextLength;
        }
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtPercent.Text, "", false) <= 0U)
          return;
        if (Conversions.ToDouble(this.txtPercent.Text) > 100.0)
        {
          this.txtPercent.BackColor = Color.Red;
          this.cmdOK.Enabled = false;
          this.lblError.Text = "Coverage must be between 0 and 100";
        }
        else
        {
          this.txtPercent.BackColor = Color.White;
          this.cmdOK.Enabled = true;
          this.lblError.Text = "";
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        this.txtPercent.BackColor = Color.Red;
        mdUtility.Errorhandler(ex2, this.Name, nameof (txtPercent_TextChanged));
        ProjectData.ClearProjectError();
      }
      finally
      {
      }
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtPercent.Text, "", false) == 0 || Conversions.ToDouble(this.txtPercent.Text) > 100.0)
      {
        int num = (int) MessageBox.Show("Coverage must be between 0 and 100");
      }
      else
        this.DialogResult = DialogResult.OK;
    }
  }
}
