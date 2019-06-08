// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CustomShapeEditorForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls.Properties;

namespace Telerik.WinControls
{
  public class CustomShapeEditorForm : Form
  {
    private DialogResult result;
    private IContainer components;
    private RadShapeEditorControl shapeEditorControl1;
    private PropertyGrid propertyGrid1;
    private CheckBox checkBox_GridSnap;
    private CheckBox checkBox_CtrlSnap;
    private CheckBox checkBox_CurveSnap;
    private CheckBox checkBox_ExtSnap;
    private ToolTip generalTooltip;
    private System.Windows.Forms.ComboBox comboBox1;
    private Button button1;
    private Button button2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private NumericUpDown numericUpDown1;
    private Button Button_ExtToFit;
    private Button Button_FitBoundsToEditor;
    private Button Button_FitShapeToEditor;

    public DialogResult Result
    {
      get
      {
        return this.result;
      }
    }

    public CustomShapeEditorForm()
    {
      this.InitializeComponent();
      this.shapeEditorControl1.propertyGrid = this.propertyGrid1;
    }

    public CustomShape EditShape(CustomShape shape)
    {
      if (shape == null)
        return (CustomShape) null;
      CustomShape customShape = shape.Clone();
      this.shapeEditorControl1.Reset();
      if (!customShape.DoFixDimension())
        customShape.CreateRectangleShape(0.0f, 0.0f, 200f, 120f);
      this.shapeEditorControl1.Shape = customShape.Shape;
      this.shapeEditorControl1.Dimension = customShape.Dimension;
      if ((this.result = this.ShowDialog()) != DialogResult.OK)
        return shape;
      customShape.Dimension = this.shapeEditorControl1.Dimension;
      return customShape;
    }

    public CustomShape CreateShape()
    {
      this.shapeEditorControl1.Reset();
      CustomShape customShape = new CustomShape();
      if (!customShape.DoFixDimension())
        customShape.CreateRectangleShape(0.0f, 0.0f, 200f, 120f);
      this.shapeEditorControl1.Shape = customShape.Shape;
      this.shapeEditorControl1.Dimension = customShape.Dimension;
      if ((this.result = this.ShowDialog()) != DialogResult.OK)
        return (CustomShape) null;
      customShape.Dimension = this.shapeEditorControl1.Dimension;
      return customShape;
    }

    public RadShapeEditorControl EditorControl
    {
      get
      {
        return this.shapeEditorControl1;
      }
    }

    private void OnSnapChanged(object sender, SnapChangedEventArgs args)
    {
      if ((args.param & RadShapeEditorControl.SnapTypes.SnapToGrid) != (RadShapeEditorControl.SnapTypes) 0 && this.checkBox_GridSnap.Checked != this.shapeEditorControl1.GridSnap)
        this.checkBox_GridSnap.Checked = this.shapeEditorControl1.GridSnap;
      if ((args.param & RadShapeEditorControl.SnapTypes.SnapToCtrl) != (RadShapeEditorControl.SnapTypes) 0 && this.checkBox_CtrlSnap.Checked != this.shapeEditorControl1.CtrlPointsSnap)
        this.checkBox_CtrlSnap.Checked = this.shapeEditorControl1.CtrlPointsSnap;
      if ((args.param & RadShapeEditorControl.SnapTypes.SnapToCurves) != (RadShapeEditorControl.SnapTypes) 0 && this.checkBox_CurveSnap.Checked != this.shapeEditorControl1.CurvesSnap)
        this.checkBox_CurveSnap.Checked = this.shapeEditorControl1.CurvesSnap;
      if ((args.param & RadShapeEditorControl.SnapTypes.SnapToExtensions) == (RadShapeEditorControl.SnapTypes) 0 || this.checkBox_ExtSnap.Checked == this.shapeEditorControl1.ExtensionsSnap)
        return;
      this.checkBox_ExtSnap.Checked = this.shapeEditorControl1.ExtensionsSnap;
    }

    private void checkBoxSnap_CheckedChanged(object sender, EventArgs e)
    {
      if (sender == null)
        return;
      switch ((sender as CheckBox).Name)
      {
        case "checkBox_GridSnap":
          this.shapeEditorControl1.GridSnap = this.checkBox_GridSnap.Checked;
          break;
        case "checkBox_CtrlSnap":
          this.shapeEditorControl1.CtrlPointsSnap = this.checkBox_CtrlSnap.Checked;
          break;
        case "checkBox_CurveSnap":
          this.shapeEditorControl1.CurvesSnap = this.checkBox_CurveSnap.Checked;
          break;
        case "checkBox_ExtSnap":
          this.shapeEditorControl1.ExtensionsSnap = this.checkBox_ExtSnap.Checked;
          break;
      }
    }

    private void ZoomCombo_TextChanged(object sender, EventArgs e)
    {
      string s = new Regex("(?<value>([0-9]+(\\.[0-9]*)?)|([0-9]*\\.[0-9]+))\\%?").Match(this.comboBox1.Text).Groups["value"].Value;
      if (s.Length < 1)
        return;
      double num = (double) this.shapeEditorControl1.ZoomCenter(float.Parse(s) / 100f);
    }

    private void OnZoomChanged(object sender, ZoomChangedEventArgs args)
    {
      if (this.ActiveControl == this.comboBox1)
        return;
      this.comboBox1.Text = Convert.ToString(Math.Round((double) args.zoomCoef * 100.0, 2));
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
      this.shapeEditorControl1.GridSize = (float) (int) this.numericUpDown1.Value;
    }

    private void Button_ExtToFit_Click(object sender, EventArgs e)
    {
      this.shapeEditorControl1.ExtendBoundsToFitShape();
    }

    private void Button_FitBoundsToEditor_Click(object sender, EventArgs e)
    {
      this.shapeEditorControl1.FitBoundsToScreen();
    }

    private void Button_FitShapeToEditor_Click(object sender, EventArgs e)
    {
      this.shapeEditorControl1.FitShapeToScreen();
    }

    private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      this.shapeEditorControl1.UpdateShape();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ShapeLinesCollection shapeLinesCollection = new ShapeLinesCollection();
      this.propertyGrid1 = new PropertyGrid();
      this.generalTooltip = new ToolTip(this.components);
      this.Button_FitShapeToEditor = new Button();
      this.Button_FitBoundsToEditor = new Button();
      this.Button_ExtToFit = new Button();
      this.checkBox_ExtSnap = new CheckBox();
      this.checkBox_CurveSnap = new CheckBox();
      this.checkBox_CtrlSnap = new CheckBox();
      this.checkBox_GridSnap = new CheckBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.button1 = new Button();
      this.button2 = new Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.numericUpDown1 = new NumericUpDown();
      this.shapeEditorControl1 = new RadShapeEditorControl();
      this.numericUpDown1.BeginInit();
      this.SuspendLayout();
      this.propertyGrid1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.propertyGrid1.Location = new Point(613, 1);
      this.propertyGrid1.Name = "propertyGrid1";
      this.propertyGrid1.Size = new Size(201, 444);
      this.propertyGrid1.TabIndex = 1;
      this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
      this.generalTooltip.Tag = (object) "";
      this.generalTooltip.ToolTipIcon = ToolTipIcon.Info;
      this.generalTooltip.ToolTipTitle = "Information";
      this.Button_FitShapeToEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.Button_FitShapeToEditor.Image = (Image) Resources.FitShape;
      this.Button_FitShapeToEditor.Location = new Point(398, 447);
      this.Button_FitShapeToEditor.Name = "Button_FitShapeToEditor";
      this.Button_FitShapeToEditor.Size = new Size(25, 24);
      this.Button_FitShapeToEditor.TabIndex = 8;
      this.generalTooltip.SetToolTip((Control) this.Button_FitShapeToEditor, "Fit the shape to the visible editor area");
      this.Button_FitShapeToEditor.UseVisualStyleBackColor = true;
      this.Button_FitShapeToEditor.Click += new EventHandler(this.Button_FitShapeToEditor_Click);
      this.Button_FitBoundsToEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.Button_FitBoundsToEditor.Image = (Image) Resources.FitBounds;
      this.Button_FitBoundsToEditor.Location = new Point(367, 447);
      this.Button_FitBoundsToEditor.Name = "Button_FitBoundsToEditor";
      this.Button_FitBoundsToEditor.Size = new Size(25, 24);
      this.Button_FitBoundsToEditor.TabIndex = 8;
      this.generalTooltip.SetToolTip((Control) this.Button_FitBoundsToEditor, "Fit the bounding rectangle to the visible editor area.");
      this.Button_FitBoundsToEditor.UseVisualStyleBackColor = true;
      this.Button_FitBoundsToEditor.Click += new EventHandler(this.Button_FitBoundsToEditor_Click);
      this.Button_ExtToFit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.Button_ExtToFit.Image = (Image) Resources.extToFit;
      this.Button_ExtToFit.Location = new Point(336, 447);
      this.Button_ExtToFit.Name = "Button_ExtToFit";
      this.Button_ExtToFit.Size = new Size(25, 24);
      this.Button_ExtToFit.TabIndex = 8;
      this.generalTooltip.SetToolTip((Control) this.Button_ExtToFit, "Extends bounds to fit the whole shape");
      this.Button_ExtToFit.UseVisualStyleBackColor = true;
      this.Button_ExtToFit.Click += new EventHandler(this.Button_ExtToFit_Click);
      this.checkBox_ExtSnap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkBox_ExtSnap.Appearance = Appearance.Button;
      this.checkBox_ExtSnap.BackgroundImage = (Image) Resources.snapToExtBtn;
      this.checkBox_ExtSnap.BackgroundImageLayout = ImageLayout.Center;
      this.checkBox_ExtSnap.Checked = true;
      this.checkBox_ExtSnap.CheckState = CheckState.Checked;
      this.checkBox_ExtSnap.ImageAlign = ContentAlignment.TopLeft;
      this.checkBox_ExtSnap.Location = new Point(52, 446);
      this.checkBox_ExtSnap.Margin = new Padding(0);
      this.checkBox_ExtSnap.Name = "checkBox_ExtSnap";
      this.checkBox_ExtSnap.Size = new Size(25, 25);
      this.checkBox_ExtSnap.TabIndex = 2;
      this.generalTooltip.SetToolTip((Control) this.checkBox_ExtSnap, "Toggles Snap To Tangents/Extensions (shortcut: E)");
      this.checkBox_ExtSnap.UseVisualStyleBackColor = true;
      this.checkBox_ExtSnap.CheckedChanged += new EventHandler(this.checkBoxSnap_CheckedChanged);
      this.checkBox_CurveSnap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkBox_CurveSnap.Appearance = Appearance.Button;
      this.checkBox_CurveSnap.BackgroundImage = (Image) Resources.snapToCurveBtn;
      this.checkBox_CurveSnap.BackgroundImageLayout = ImageLayout.Center;
      this.checkBox_CurveSnap.Checked = true;
      this.checkBox_CurveSnap.CheckState = CheckState.Checked;
      this.checkBox_CurveSnap.ImageAlign = ContentAlignment.TopLeft;
      this.checkBox_CurveSnap.Location = new Point(26, 446);
      this.checkBox_CurveSnap.Margin = new Padding(0);
      this.checkBox_CurveSnap.Name = "checkBox_CurveSnap";
      this.checkBox_CurveSnap.Size = new Size(25, 25);
      this.checkBox_CurveSnap.TabIndex = 2;
      this.generalTooltip.SetToolTip((Control) this.checkBox_CurveSnap, "Toggles Snap To Lines/Curves (shortcut: L)");
      this.checkBox_CurveSnap.UseVisualStyleBackColor = true;
      this.checkBox_CurveSnap.CheckedChanged += new EventHandler(this.checkBoxSnap_CheckedChanged);
      this.checkBox_CtrlSnap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkBox_CtrlSnap.Appearance = Appearance.Button;
      this.checkBox_CtrlSnap.BackgroundImage = (Image) Resources.snapToCtrlBtn;
      this.checkBox_CtrlSnap.BackgroundImageLayout = ImageLayout.Center;
      this.checkBox_CtrlSnap.Checked = true;
      this.checkBox_CtrlSnap.CheckState = CheckState.Checked;
      this.checkBox_CtrlSnap.ImageAlign = ContentAlignment.TopLeft;
      this.checkBox_CtrlSnap.Location = new Point(0, 446);
      this.checkBox_CtrlSnap.Margin = new Padding(0);
      this.checkBox_CtrlSnap.Name = "checkBox_CtrlSnap";
      this.checkBox_CtrlSnap.Size = new Size(25, 25);
      this.checkBox_CtrlSnap.TabIndex = 2;
      this.generalTooltip.SetToolTip((Control) this.checkBox_CtrlSnap, "Toggles Snap To Control Points (shortcut: C)");
      this.checkBox_CtrlSnap.UseVisualStyleBackColor = true;
      this.checkBox_CtrlSnap.CheckedChanged += new EventHandler(this.checkBoxSnap_CheckedChanged);
      this.checkBox_GridSnap.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.checkBox_GridSnap.Appearance = Appearance.Button;
      this.checkBox_GridSnap.BackgroundImage = (Image) Resources.snapToGridBtn;
      this.checkBox_GridSnap.BackgroundImageLayout = ImageLayout.Center;
      this.checkBox_GridSnap.Checked = true;
      this.checkBox_GridSnap.CheckState = CheckState.Checked;
      this.checkBox_GridSnap.ImageAlign = ContentAlignment.TopLeft;
      this.checkBox_GridSnap.Location = new Point(78, 446);
      this.checkBox_GridSnap.Margin = new Padding(0);
      this.checkBox_GridSnap.Name = "checkBox_GridSnap";
      this.checkBox_GridSnap.Size = new Size(25, 25);
      this.checkBox_GridSnap.TabIndex = 2;
      this.generalTooltip.SetToolTip((Control) this.checkBox_GridSnap, "Toggles Snap To Grid (shortcut: G)");
      this.checkBox_GridSnap.UseVisualStyleBackColor = true;
      this.checkBox_GridSnap.CheckedChanged += new EventHandler(this.checkBoxSnap_CheckedChanged);
      this.comboBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[9]
      {
        (object) "1600%",
        (object) "800%",
        (object) "400%",
        (object) "200%",
        (object) "100%",
        (object) "75%",
        (object) "50%",
        (object) "25%",
        (object) "10%"
      });
      this.comboBox1.Location = new Point(146, 449);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(74, 21);
      this.comboBox1.TabIndex = 3;
      this.comboBox1.TextChanged += new EventHandler(this.ZoomCombo_TextChanged);
      this.button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button1.DialogResult = DialogResult.OK;
      this.button1.Location = new Point(613, 448);
      this.button1.Name = "button1";
      this.button1.Size = new Size(97, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "OK";
      this.button1.UseVisualStyleBackColor = true;
      this.button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button2.DialogResult = DialogResult.Cancel;
      this.button2.Location = new Point(717, 448);
      this.button2.Name = "button2";
      this.button2.Size = new Size(97, 23);
      this.button2.TabIndex = 5;
      this.button2.Text = "Cancel";
      this.button2.UseVisualStyleBackColor = true;
      this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(109, 452);
      this.label1.Name = "label1";
      this.label1.Size = new Size(37, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Zoom:";
      this.label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(229, 452);
      this.label2.Name = "label2";
      this.label2.Size = new Size(50, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Grid size:";
      this.numericUpDown1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.numericUpDown1.Increment = new Decimal(new int[4]
      {
        8,
        0,
        0,
        0
      });
      this.numericUpDown1.Location = new Point(278, 449);
      this.numericUpDown1.Maximum = new Decimal(new int[4]
      {
        500,
        0,
        0,
        0
      });
      this.numericUpDown1.Minimum = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new Size(52, 20);
      this.numericUpDown1.TabIndex = 7;
      this.numericUpDown1.Value = new Decimal(new int[4]
      {
        32,
        0,
        0,
        0
      });
      this.numericUpDown1.ValueChanged += new EventHandler(this.numericUpDown1_ValueChanged);
      this.shapeEditorControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.shapeEditorControl1.BackColor = Color.White;
      this.shapeEditorControl1.BorderStyle = BorderStyle.FixedSingle;
      this.shapeEditorControl1.CtrlPointsSnap = true;
      this.shapeEditorControl1.Cursor = Cursors.Cross;
      this.shapeEditorControl1.CurvesSnap = true;
      this.shapeEditorControl1.Dimension = new Rectangle(64, 64, 512, 256);
      this.shapeEditorControl1.ExtensionsSnap = true;
      this.shapeEditorControl1.GridSize = 32f;
      this.shapeEditorControl1.GridSnap = true;
      this.shapeEditorControl1.Location = new Point(0, 0);
      this.shapeEditorControl1.MinimumSize = new Size(60, 60);
      this.shapeEditorControl1.Name = "shapeEditorControl1";
      this.shapeEditorControl1.Shape = shapeLinesCollection;
      this.shapeEditorControl1.Size = new Size(607, 445);
      this.shapeEditorControl1.TabIndex = 0;
      this.shapeEditorControl1.SnapChanged += new SnapChangedEventHandler(this.OnSnapChanged);
      this.shapeEditorControl1.ZoomChanged += new ZoomChangedEventHandler(this.OnZoomChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(814, 471);
      this.Controls.Add((Control) this.Button_FitShapeToEditor);
      this.Controls.Add((Control) this.Button_FitBoundsToEditor);
      this.Controls.Add((Control) this.Button_ExtToFit);
      this.Controls.Add((Control) this.numericUpDown1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.checkBox_ExtSnap);
      this.Controls.Add((Control) this.checkBox_CurveSnap);
      this.Controls.Add((Control) this.checkBox_CtrlSnap);
      this.Controls.Add((Control) this.checkBox_GridSnap);
      this.Controls.Add((Control) this.propertyGrid1);
      this.Controls.Add((Control) this.shapeEditorControl1);
      this.MinimumSize = new Size(638, 200);
      this.Name = nameof (CustomShapeEditorForm);
      this.Text = "Custom Shape Editor";
      this.numericUpDown1.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
