// Decompiled with JetBrains decompiler
// Type: BuilderRED.dlgImageBarcode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Services.ReportsTelerik;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.Reporting;

namespace BuilderRED
{
  [DesignerGenerated]
  public class dlgImageBarcode : Form
  {
    private IContainer components;
    private IReportDocument m_ReportDocument;

    static dlgImageBarcode()
    {
      dlgImageBarcode.DefaultMultiplier = 1.5;
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
      this.rvReportsViewer = new Telerik.ReportViewer.WinForms.ReportViewer();
      this.pnlMultiplier = new System.Windows.Forms.Panel();
      this.btn3 = new RadioButton();
      this.btn2 = new RadioButton();
      this.btnCancel = new Button();
      this.Label1 = new Label();
      this.TableLayoutPanel1.SuspendLayout();
      this.pnlMultiplier.SuspendLayout();
      this.SuspendLayout();
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.rvReportsViewer, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.pnlMultiplier, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnCancel, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.Label1, 0, 2);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(444, 176);
      this.TableLayoutPanel1.TabIndex = 11;
      this.rvReportsViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.rvReportsViewer, 2);
      this.rvReportsViewer.Location = new Point(3, 35);
      this.rvReportsViewer.Name = "rvReportsViewer";
      this.rvReportsViewer.Size = new Size(438, 118);
      this.rvReportsViewer.TabIndex = 3;
      this.pnlMultiplier.Controls.Add((Control) this.btn3);
      this.pnlMultiplier.Controls.Add((Control) this.btn2);
      this.pnlMultiplier.Location = new Point(0, 0);
      this.pnlMultiplier.Margin = new Padding(0);
      this.pnlMultiplier.Name = "pnlMultiplier";
      this.pnlMultiplier.Size = new Size(108, 32);
      this.pnlMultiplier.TabIndex = 12;
      this.btn3.Appearance = Appearance.Button;
      this.btn3.AutoSize = true;
      this.btn3.Location = new Point(46, 3);
      this.btn3.Name = "btn3";
      this.btn3.Size = new Size(28, 23);
      this.btn3.TabIndex = 2;
      this.btn3.TabStop = true;
      this.btn3.Tag = (object) "2";
      this.btn3.Text = "2x";
      this.btn3.UseVisualStyleBackColor = true;
      this.btn2.Appearance = Appearance.Button;
      this.btn2.AutoSize = true;
      this.btn2.Location = new Point(11, 3);
      this.btn2.Name = "btn2";
      this.btn2.Size = new Size(28, 23);
      this.btn2.TabIndex = 1;
      this.btn2.TabStop = true;
      this.btn2.Tag = (object) "1.5";
      this.btn2.Text = "1x";
      this.btn2.UseVisualStyleBackColor = true;
      this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnCancel.Location = new Point(363, 2);
      this.btnCancel.Margin = new Padding(3, 2, 3, 2);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(78, 23);
      this.btnCancel.TabIndex = 9;
      this.btnCancel.Text = "Close";
      this.Label1.AutoSize = true;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.Label1, 2);
      this.Label1.Location = new Point(3, 156);
      this.Label1.Name = "Label1";
      this.Label1.Size = new Size(366, 13);
      this.Label1.TabIndex = 13;
      this.Label1.Text = "This barcode is for use in linking images with this inventory or inspection item";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.ClientSize = new Size(444, 176);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MinimumSize = new Size(450, 137);
      this.Name = nameof (dlgImageBarcode);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.pnlMultiplier.ResumeLayout(false);
      this.pnlMultiplier.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnCancel
    {
      get
      {
        return this._btnCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnCancel_Click);
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

    internal virtual System.Windows.Forms.Panel pnlMultiplier { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Telerik.ReportViewer.WinForms.ReportViewer rvReportsViewer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadioButton btn3
    {
      get
      {
        return this._btn3;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.radioMultiplierSelected);
        RadioButton btn3_1 = this._btn3;
        if (btn3_1 != null)
          btn3_1.Click -= eventHandler;
        this._btn3 = value;
        RadioButton btn3_2 = this._btn3;
        if (btn3_2 == null)
          return;
        btn3_2.Click += eventHandler;
      }
    }

    internal virtual RadioButton btn2
    {
      get
      {
        return this._btn2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.radioMultiplierSelected);
        RadioButton btn2_1 = this._btn2;
        if (btn2_1 != null)
          btn2_1.Click -= eventHandler;
        this._btn2 = value;
        RadioButton btn2_2 = this._btn2;
        if (btn2_2 == null)
          return;
        btn2_2.Click += eventHandler;
      }
    }

    internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public static double DefaultMultiplier { get; set; }

    public dlgImageBarcode(string guid, string title = "")
    {
      this.Load += (EventHandler) ((a0, a1) => this.me_load());
      this.InitializeComponent();
      this.Text = title;
      InstanceReportSource instanceReportSource = new InstanceReportSource();
      this.m_ReportDocument = (IReportDocument) new ImageBarcode(guid);
      instanceReportSource.ReportDocument = this.m_ReportDocument;
      this.rvReportsViewer.ReportSource = (ReportSource) instanceReportSource;
      try
      {
        foreach (Control control in this.pnlMultiplier.Controls)
        {
          RadioButton radioButton = control as RadioButton;
          if (radioButton != null && Operators.ConditionalCompareObjectEqual(radioButton.Tag, (object) dlgImageBarcode.DefaultMultiplier, false))
            radioButton.Select();
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      this.rvReportsViewer.ToolbarVisible = false;
    }

    private void me_load()
    {
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void ChangeSize(double multiplier)
    {
      this.rvReportsViewer.Size = new Size(checked ((int) Math.Round(unchecked (multiplier * 350.0 + 20.0))), checked ((int) Math.Round(unchecked (multiplier * 140.0 + 20.0))));
      ((ImageBarcode) this.m_ReportDocument).ApplySizeMultiplier(multiplier);
      dlgImageBarcode.DefaultMultiplier = multiplier;
      this.rvReportsViewer.RefreshReport();
    }

    private void radioMultiplierSelected(object sender, EventArgs e)
    {
      this.ChangeSize(Conversions.ToDouble(((Control) sender).Tag.ToString()));
    }
  }
}
