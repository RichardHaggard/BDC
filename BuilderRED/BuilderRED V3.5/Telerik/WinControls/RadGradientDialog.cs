// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadGradientDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls
{
  public class RadGradientDialog : Form
  {
    private IContainer components;
    private GradientEditorControl editorControl;
    private Button btnOk;
    private Button btnCancel;

    public RadGradientDialog()
    {
      this.InitializeComponent();
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.editorControl.Fill;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.editorControl = new GradientEditorControl();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.SuspendLayout();
      this.editorControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.editorControl.AutoSize = true;
      this.editorControl.Location = new Point(0, 0);
      this.editorControl.Name = "editorControl";
      this.editorControl.Size = new Size(582, 290);
      this.editorControl.TabIndex = 0;
      this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Location = new Point(412, 301);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(493, 301);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.ClientSize = new Size(580, 336);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.editorControl);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (RadGradientDialog);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
