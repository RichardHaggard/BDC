// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapeEditor.CustomShapeEditorForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.OldShapeEditor
{
  public class CustomShapeEditorForm : Form
  {
    private IContainer components;
    private RadShapeEditorControl radShapeEditorControl1;
    private PropertyGrid propertyGrid1;
    private Button buttonOk;
    private Button buttonCancel;

    public RadShapeEditorControl EditorControl
    {
      get
      {
        return this.radShapeEditorControl1;
      }
    }

    public CustomShapeEditorForm()
    {
      this.InitializeComponent();
      this.radShapeEditorControl1.propertyGrid = this.propertyGrid1;
    }

    private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      int num = (int) MessageBox.Show("blabla");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.propertyGrid1 = new PropertyGrid();
      this.buttonOk = new Button();
      this.buttonCancel = new Button();
      this.radShapeEditorControl1 = new RadShapeEditorControl();
      this.SuspendLayout();
      this.propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.propertyGrid1.Location = new Point(360, 3);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new Size(156, 274);
      this.propertyGrid1.TabIndex = 1;
      this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
      this.buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonOk.DialogResult = DialogResult.OK;
      this.buttonOk.Location = new Point(360, 283);
      this.buttonOk.Name = "buttonOk";
      this.buttonOk.Size = new Size(75, 23);
      this.buttonOk.TabIndex = 2;
      this.buttonOk.Text = "OK";
      this.buttonOk.UseVisualStyleBackColor = true;
      this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(441, 283);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(75, 23);
      this.buttonCancel.TabIndex = 3;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.radShapeEditorControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radShapeEditorControl1.AutoScroll = true;
      this.radShapeEditorControl1.Dimension = new Rectangle(20, 20, 311, 263);
      this.radShapeEditorControl1.Location = new Point(3, 3);
      this.radShapeEditorControl1.Name = "radShapeEditorControl1";
      this.radShapeEditorControl1.Size = new Size(351, 303);
      this.radShapeEditorControl1.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(519, 309);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOk);
      this.Controls.Add((Control) this.propertyGrid1);
      this.Controls.Add((Control) this.radShapeEditorControl1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CustomShapeEditorForm);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Shape Designer";
      this.ResumeLayout(false);
    }
  }
}
