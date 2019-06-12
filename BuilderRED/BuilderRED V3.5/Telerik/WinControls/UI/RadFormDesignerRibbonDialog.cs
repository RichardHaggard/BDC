// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormDesignerRibbonDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadFormDesignerRibbonDialog : RadForm
  {
    private IContainer components;
    private RadLabel radLblQuestion;
    private RadLabel radLblInfo;
    private RadButton radBtnYes;
    private RadButton radBtnNo;

    public RadFormDesignerRibbonDialog()
    {
      this.InitializeComponent();
    }

    private void radBtnYes_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Yes;
      this.Close();
    }

    private void radBtnNo_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.No;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radLblQuestion = new RadLabel();
      this.radLblInfo = new RadLabel();
      this.radBtnYes = new RadButton();
      this.radBtnNo = new RadButton();
      this.radLblQuestion.BeginInit();
      this.radLblInfo.BeginInit();
      this.radBtnYes.BeginInit();
      this.radBtnNo.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radLblQuestion.AutoSize = false;
      this.radLblQuestion.Location = new Point(12, 60);
      this.radLblQuestion.Name = "radLblQuestion";
      this.radLblQuestion.Size = new Size(402, 68);
      this.radLblQuestion.TabIndex = 0;
      this.radLblQuestion.Text = "If you choose to activate the RadRibbonFormBehavior it will be automatically deactivated when you remove the RadRibbonBar from the Form.\r\n\r\nWould you like to use the RadRibbonForm behavior?";
      this.radLblInfo.AutoSize = false;
      this.radLblInfo.Location = new Point(12, 12);
      this.radLblInfo.Name = "radLblInfo";
      this.radLblInfo.Size = new Size(402, 48);
      this.radLblInfo.TabIndex = 0;
      this.radLblInfo.Text = "When using the RadRibbonBar control it is recommended to either use the RadRibbonForm or activate the RadRibbonForm behavior on this form.";
      this.radBtnYes.Location = new Point(120, 134);
      this.radBtnYes.Name = "radBtnYes";
      this.radBtnYes.Size = new Size(75, 23);
      this.radBtnYes.TabIndex = 1;
      this.radBtnYes.Text = "Yes";
      this.radBtnYes.Click += new EventHandler(this.radBtnYes_Click);
      this.radBtnNo.Location = new Point(231, 134);
      this.radBtnNo.Name = "radBtnNo";
      this.radBtnNo.Size = new Size(75, 23);
      this.radBtnNo.TabIndex = 1;
      this.radBtnNo.Text = "No";
      this.radBtnNo.Click += new EventHandler(this.radBtnNo_Click);
      this.ClientSize = new Size(426, 184);
      this.Controls.Add((Control) this.radBtnNo);
      this.Controls.Add((Control) this.radBtnYes);
      this.Controls.Add((Control) this.radLblInfo);
      this.Controls.Add((Control) this.radLblQuestion);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (RadFormDesignerRibbonDialog);
      this.StartPosition = FormStartPosition.Manual;
      this.RootElement.ApplyShapeToControl = true;
      this.RootElement.MinSize = new Size(150, 36);
      this.Text = "RadForm Designer";
      this.radLblQuestion.EndInit();
      this.radLblInfo.EndInit();
      this.radBtnYes.EndInit();
      this.radBtnNo.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
