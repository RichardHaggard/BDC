// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDataFilterDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadDataFilterDialog : RadForm
  {
    internal string initialExpression = string.Empty;
    private IContainer components;
    private RadDataFilter radDataFilter1;
    private RadButton radButtonOK;
    private RadButton radButtonCancel;
    private RadButton radButtonApply;

    public RadDataFilterDialog()
    {
      this.InitializeComponent();
      this.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("DialogTitle");
      this.radButtonOK.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("DialogOKButton");
      this.radButtonCancel.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("DialogCancelButton");
      this.radButtonApply.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("DialogApplyButton");
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.initialExpression = this.radDataFilter1.Expression;
    }

    public RadDataFilter DataFilter
    {
      get
      {
        return this.radDataFilter1;
      }
    }

    public object DataSource
    {
      get
      {
        return this.DataFilter.DataSource;
      }
      set
      {
        this.DataFilter.DataSource = value;
      }
    }

    protected virtual void ApplyFilter()
    {
      this.initialExpression = this.radDataFilter1.Expression;
      this.radDataFilter1.ApplyFilter();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);
      if (e.Cancel)
        return;
      this.radDataFilter1.Expression = this.initialExpression;
    }

    protected virtual void OnOKButtonClick(object sender, EventArgs e)
    {
      this.ApplyFilter();
      this.Close();
    }

    protected virtual void OnCancelButtonClick(object sender, EventArgs e)
    {
      this.radDataFilter1.Expression = this.initialExpression;
      this.Close();
    }

    protected virtual void OnApplyButtonClick(object sender, EventArgs e)
    {
      this.ApplyFilter();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radDataFilter1 = new RadDataFilter();
      this.radButtonOK = new RadButton();
      this.radButtonCancel = new RadButton();
      this.radButtonApply = new RadButton();
      this.radDataFilter1.BeginInit();
      this.radButtonOK.BeginInit();
      this.radButtonCancel.BeginInit();
      this.radButtonApply.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radDataFilter1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radDataFilter1.Location = new Point(4, 4);
      this.radDataFilter1.Name = "radDataFilter1";
      this.radDataFilter1.Size = new Size(434, 184);
      this.radDataFilter1.TabIndex = 0;
      this.radDataFilter1.Text = "radDataFilter1";
      this.radButtonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonOK.Location = new Point(160, 192);
      this.radButtonOK.Name = "radButtonOK";
      this.radButtonOK.Size = new Size(90, 24);
      this.radButtonOK.TabIndex = 1;
      this.radButtonOK.Text = "OK";
      this.radButtonOK.Click += new EventHandler(this.OnOKButtonClick);
      this.radButtonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonCancel.Location = new Point(254, 192);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(90, 24);
      this.radButtonCancel.TabIndex = 2;
      this.radButtonCancel.Text = "Cancel";
      this.radButtonCancel.Click += new EventHandler(this.OnCancelButtonClick);
      this.radButtonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButtonApply.Location = new Point(348, 192);
      this.radButtonApply.Name = "radButtonApply";
      this.radButtonApply.Size = new Size(90, 24);
      this.radButtonApply.TabIndex = 2;
      this.radButtonApply.Text = "Apply";
      this.radButtonApply.Click += new EventHandler(this.OnApplyButtonClick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(442, 220);
      this.Controls.Add((Control) this.radButtonApply);
      this.Controls.Add((Control) this.radButtonCancel);
      this.Controls.Add((Control) this.radButtonOK);
      this.Controls.Add((Control) this.radDataFilter1);
      this.Name = nameof (RadDataFilterDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowIcon = false;
      this.Text = "Form1";
      this.radDataFilter1.EndInit();
      this.radButtonOK.EndInit();
      this.radButtonCancel.EndInit();
      this.radButtonApply.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
