// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.EvaluationForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Properties;

namespace Telerik.WinControls
{
  internal class EvaluationForm : Form
  {
    private IContainer components;
    private PictureBox logoPictureBox;
    private System.Windows.Forms.Label labelProductName;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.TextBox textBoxDescription;
    private Button okButton;
    private System.Windows.Forms.Label labelCompanyName;
    private TableLayoutPanel tableLayoutPanel;
    private System.Windows.Forms.Label labelCopyright;

    internal EvaluationForm()
    {
      this.InitializeComponent();
      this.labelProductName.Text = "RadControls for WinForms";
      this.labelVersion.Text = string.Format("Version {0}", (object) "2018.3.1016.20");
      this.labelCopyright.Text = this.SplitCopyrightText("Copyright © 2006-2018 Telerik EAD. All rights reserved.");
      this.labelCompanyName.Text = "Telerik";
    }

    private string SplitCopyrightText(string copyrightText)
    {
      int num = copyrightText.IndexOf('.');
      if (num < 0 || num == copyrightText.Length - 1)
        return copyrightText;
      return copyrightText.Insert(num + 1, "\n");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.logoPictureBox = new PictureBox();
      this.labelProductName = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.textBoxDescription = new System.Windows.Forms.TextBox();
      this.okButton = new Button();
      this.labelCompanyName = new System.Windows.Forms.Label();
      this.tableLayoutPanel = new TableLayoutPanel();
      this.labelCopyright = new System.Windows.Forms.Label();
      ((ISupportInitialize) this.logoPictureBox).BeginInit();
      this.tableLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      this.logoPictureBox.Dock = DockStyle.Fill;
      this.logoPictureBox.Image = (Image) Resources.RadControls;
      this.logoPictureBox.Location = new Point(3, 3);
      this.logoPictureBox.Name = "logoPictureBox";
      this.tableLayoutPanel.SetRowSpan((Control) this.logoPictureBox, 4);
      this.logoPictureBox.Size = new Size(269, 114);
      this.logoPictureBox.TabIndex = 12;
      this.logoPictureBox.TabStop = false;
      this.labelProductName.Dock = DockStyle.Fill;
      this.labelProductName.Location = new Point(281, 0);
      this.labelProductName.Margin = new Padding(6, 0, 3, 0);
      this.labelProductName.MaximumSize = new Size(0, 17);
      this.labelProductName.Name = "labelProductName";
      this.labelProductName.Size = new Size(221, 17);
      this.labelProductName.TabIndex = 19;
      this.labelProductName.Text = "Product Name";
      this.labelProductName.TextAlign = ContentAlignment.MiddleLeft;
      this.labelVersion.Dock = DockStyle.Fill;
      this.labelVersion.Location = new Point(281, 30);
      this.labelVersion.Margin = new Padding(6, 0, 3, 0);
      this.labelVersion.MaximumSize = new Size(0, 17);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new Size(221, 17);
      this.labelVersion.TabIndex = 0;
      this.labelVersion.Text = "Version";
      this.labelVersion.TextAlign = ContentAlignment.MiddleLeft;
      this.textBoxDescription.Dock = DockStyle.Fill;
      this.textBoxDescription.Location = new Point(281, 123);
      this.textBoxDescription.Margin = new Padding(6, 3, 3, 3);
      this.textBoxDescription.Multiline = true;
      this.textBoxDescription.Name = "textBoxDescription";
      this.textBoxDescription.ReadOnly = true;
      this.textBoxDescription.ScrollBars = ScrollBars.Both;
      this.textBoxDescription.Size = new Size(221, 148);
      this.textBoxDescription.TabIndex = 23;
      this.textBoxDescription.TabStop = false;
      this.textBoxDescription.Text = "This is a trial copy for evaluation purposes only. For production use please obtain a commercial copy.";
      this.okButton.DialogResult = DialogResult.OK;
      this.okButton.Location = new Point(427, 282);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(75, 23);
      this.okButton.TabIndex = 24;
      this.okButton.Text = "&OK";
      this.labelCompanyName.Dock = DockStyle.Fill;
      this.labelCompanyName.Location = new Point(281, 90);
      this.labelCompanyName.Margin = new Padding(6, 0, 3, 0);
      this.labelCompanyName.MaximumSize = new Size(0, 17);
      this.labelCompanyName.Name = "labelCompanyName";
      this.labelCompanyName.Size = new Size(221, 17);
      this.labelCompanyName.TabIndex = 22;
      this.labelCompanyName.Text = "Company Name";
      this.labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
      this.tableLayoutPanel.ColumnCount = 2;
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.58787f));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.41213f));
      this.tableLayoutPanel.Controls.Add((Control) this.logoPictureBox, 0, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.labelProductName, 1, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.labelVersion, 1, 1);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCopyright, 1, 2);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCompanyName, 1, 3);
      this.tableLayoutPanel.Controls.Add((Control) this.textBoxDescription, 1, 4);
      this.tableLayoutPanel.Controls.Add((Control) this.okButton, 1, 5);
      this.tableLayoutPanel.Dock = DockStyle.Fill;
      this.tableLayoutPanel.Location = new Point(0, 0);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 6;
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
      this.tableLayoutPanel.Size = new Size(505, 308);
      this.tableLayoutPanel.TabIndex = 1;
      this.labelCopyright.Dock = DockStyle.Fill;
      this.labelCopyright.Location = new Point(281, 60);
      this.labelCopyright.Margin = new Padding(6, 0, 3, 0);
      this.labelCopyright.MaximumSize = new Size(0, 17);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new Size(221, 17);
      this.labelCopyright.TabIndex = 21;
      this.labelCopyright.Text = "Copyright";
      this.labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
      this.AcceptButton = (IButtonControl) this.okButton;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.White;
      this.CancelButton = (IButtonControl) this.okButton;
      this.ClientSize = new Size(505, 308);
      this.Controls.Add((Control) this.tableLayoutPanel);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (EvaluationForm);
      this.Text = "Evaluation Copy";
      ((ISupportInitialize) this.logoPictureBox).EndInit();
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
