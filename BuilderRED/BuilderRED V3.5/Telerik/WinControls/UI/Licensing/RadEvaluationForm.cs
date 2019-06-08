// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Licensing.RadEvaluationForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.Licensing
{
  public class RadEvaluationForm : RadForm
  {
    private IContainer components;
    private RadLabel radLabelCopyright;
    private RadLabel radLabelVersion;
    private TableLayoutPanel tableLayoutPanel;
    private PictureBox pictureBoxRadControls;
    private RadButton btnRemindLater;
    private RadButton btnPurchaseNow;
    private RadLabel radLabelThankYou;
    private RadLabel radLabelPurchase;

    public RadEvaluationForm()
    {
      this.InitializeComponent();
      ThemeResolutionService.LoadPackageResource("Telerik.WinControls.UI.EvalFormTheme.tssp");
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, "EvalFormTheme");
      this.radLabelVersion.Text = string.Format("Version {0}", (object) "2018.3.1016.20");
      this.radLabelCopyright.Text = this.SplitCopyrightText("Copyright © 2006-2018 Telerik EAD. All rights reserved.");
      this.FormElement.TitleBar.Children[1].Visibility = ElementVisibility.Collapsed;
      this.Icon = Telerik.WinControls.ResFinder.ProgressIcon;
      this.pictureBoxRadControls.Image = Telerik.WinControls.ResFinder.WinFormsLogoWithText;
    }

    private string SplitCopyrightText(string copyrightText)
    {
      int num = copyrightText.IndexOf('.');
      if (num < 0 || num == copyrightText.Length - 1)
        return copyrightText;
      return copyrightText.Insert(num + 1, "\n");
    }

    private void RadEvaluationForm_Load(object sender, EventArgs e)
    {
      this.radLabelThankYou.MaximumSize = new Size(this.tableLayoutPanel.Width, 0);
      this.radLabelPurchase.MaximumSize = new Size(this.tableLayoutPanel.Width, 0);
    }

    private void btnRemindLater_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnPurchaseNow_Click(object sender, EventArgs e)
    {
      Process.Start("http://www.telerik.com/purchase.aspx?utm_source=vs&utm_medium=dsk&utm_campaign=WFtrial");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radLabelCopyright = new RadLabel();
      this.radLabelVersion = new RadLabel();
      this.tableLayoutPanel = new TableLayoutPanel();
      this.pictureBoxRadControls = new PictureBox();
      this.radLabelThankYou = new RadLabel();
      this.btnRemindLater = new RadButton();
      this.btnPurchaseNow = new RadButton();
      this.radLabelPurchase = new RadLabel();
      this.radLabelCopyright.BeginInit();
      this.radLabelVersion.BeginInit();
      this.tableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.pictureBoxRadControls).BeginInit();
      this.radLabelThankYou.BeginInit();
      this.btnRemindLater.BeginInit();
      this.btnPurchaseNow.BeginInit();
      this.radLabelPurchase.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radLabelCopyright.Dock = DockStyle.Top;
      this.radLabelCopyright.Font = new Font("Segoe UI", 8f);
      this.radLabelCopyright.Location = new Point(3, 244);
      this.radLabelCopyright.Name = "radLabelCopyright";
      this.radLabelCopyright.RootElement.ControlBounds = new Rectangle(3, 264, 100, 18);
      this.radLabelCopyright.Size = new Size(54, 18);
      this.radLabelCopyright.TabIndex = 27;
      this.radLabelCopyright.Text = "Copyright";
      this.radLabelVersion.Dock = DockStyle.Top;
      this.radLabelVersion.Font = new Font("Segoe UI", 8f);
      this.radLabelVersion.Location = new Point(3, 220);
      this.radLabelVersion.Name = "radLabelVersion";
      this.radLabelVersion.RootElement.ControlBounds = new Rectangle(3, 247, 100, 18);
      this.radLabelVersion.Size = new Size(42, 18);
      this.radLabelVersion.TabIndex = 26;
      this.radLabelVersion.Text = "Version";
      this.tableLayoutPanel.BackColor = Color.Transparent;
      this.tableLayoutPanel.ColumnCount = 3;
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62f));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38f));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 122f));
      this.tableLayoutPanel.Controls.Add((Control) this.pictureBoxRadControls, 0, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.radLabelThankYou, 0, 1);
      this.tableLayoutPanel.Controls.Add((Control) this.radLabelCopyright, 0, 4);
      this.tableLayoutPanel.Controls.Add((Control) this.radLabelVersion, 0, 3);
      this.tableLayoutPanel.Controls.Add((Control) this.btnRemindLater, 2, 4);
      this.tableLayoutPanel.Controls.Add((Control) this.btnPurchaseNow, 1, 4);
      this.tableLayoutPanel.Controls.Add((Control) this.radLabelPurchase, 0, 2);
      this.tableLayoutPanel.Dock = DockStyle.Fill;
      this.tableLayoutPanel.Location = new Point(0, 0);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.tableLayoutPanel.RowCount = 5;
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 29.97764f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 24.82269f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.34043f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 8.510638f));
      this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 14.1844f));
      this.tableLayoutPanel.Size = new Size(443, 282);
      this.tableLayoutPanel.TabIndex = 2;
      this.pictureBoxRadControls.BackColor = Color.Transparent;
      this.tableLayoutPanel.SetColumnSpan((Control) this.pictureBoxRadControls, 3);
      this.pictureBoxRadControls.Dock = DockStyle.Fill;
      this.pictureBoxRadControls.InitialImage = (Image) null;
      this.pictureBoxRadControls.Location = new Point(3, 3);
      this.pictureBoxRadControls.Name = "pictureBoxRadControls";
      this.pictureBoxRadControls.Padding = new Padding(5);
      this.pictureBoxRadControls.Size = new Size(437, 78);
      this.pictureBoxRadControls.TabIndex = 29;
      this.pictureBoxRadControls.TabStop = false;
      this.tableLayoutPanel.SetColumnSpan((Control) this.radLabelThankYou, 3);
      this.radLabelThankYou.Dock = DockStyle.Top;
      this.radLabelThankYou.Font = new Font("Segoe UI Light", 10.5f);
      this.radLabelThankYou.Location = new Point(3, 87);
      this.radLabelThankYou.Name = "radLabelThankYou";
      this.radLabelThankYou.RootElement.ControlBounds = new Rectangle(3, 87, 100, 18);
      this.radLabelThankYou.Size = new Size(737, 22);
      this.radLabelThankYou.TabIndex = 32;
      this.radLabelThankYou.Text = "Thank you for using Telerik® UI for WinForms to create visually stunning and reliable desktop apps in a fraction of the time.";
      this.btnRemindLater.Dock = DockStyle.Fill;
      this.btnRemindLater.Font = new Font("Segoe UI", 9f);
      this.btnRemindLater.Location = new Point(313, 244);
      this.btnRemindLater.Name = "btnRemindLater";
      this.btnRemindLater.RootElement.ControlBounds = new Rectangle(314, 247, 110, 24);
      this.btnRemindLater.Size = new Size((int) sbyte.MaxValue, 35);
      this.btnRemindLater.TabIndex = 30;
      this.btnRemindLater.Text = "Remind me later";
      this.btnRemindLater.Click += new EventHandler(this.btnRemindLater_Click);
      this.btnPurchaseNow.Dock = DockStyle.Fill;
      this.btnPurchaseNow.Font = new Font("Segoe UI", 9f);
      this.btnPurchaseNow.Location = new Point(181, 244);
      this.btnPurchaseNow.Name = "btnPurchaseNow";
      this.btnPurchaseNow.RootElement.ControlBounds = new Rectangle(181, 247, 110, 24);
      this.btnPurchaseNow.Size = new Size(126, 35);
      this.btnPurchaseNow.TabIndex = 29;
      this.btnPurchaseNow.Text = "PURCHASE NOW";
      this.btnPurchaseNow.Click += new EventHandler(this.btnPurchaseNow_Click);
      this.radLabelPurchase.Font = new Font("Segoe UI", 9f);
      this.radLabelPurchase.Location = new Point(3, 157);
      this.radLabelPurchase.Name = "radLabelPurchase";
      this.radLabelPurchase.RootElement.ControlBounds = new Rectangle(3, 157, 100, 18);
      this.radLabelPurchase.Size = new Size(905, 19);
      this.radLabelPurchase.TabIndex = 33;
      this.radLabelPurchase.Text = "Purchase the commercial version and get access to the latest updates across the suite with the industry-leading Telerik support to ensure your projects run smoothly.";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(443, 282);
      this.Controls.Add((Control) this.tableLayoutPanel);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (RadEvaluationForm);
      this.RootElement.ApplyShapeToControl = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Evaluation Copy";
      this.TopMost = true;
      this.Load += new EventHandler(this.RadEvaluationForm_Load);
      this.radLabelCopyright.EndInit();
      this.radLabelVersion.EndInit();
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      ((ISupportInitialize) this.pictureBoxRadControls).EndInit();
      this.radLabelThankYou.EndInit();
      this.btnRemindLater.EndInit();
      this.btnPurchaseNow.EndInit();
      this.radLabelPurchase.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
