// Decompiled with JetBrains decompiler
// Type: BuilderRED.ctrlADAQuestions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Tests;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class ctrlADAQuestions : UserControl
  {
    private IContainer components;
    private int m_Question_Number;
    private Color m_Color;

    public ctrlADAQuestions()
    {
      this.Resize += new EventHandler(this.ctrlADAQuestions_Resize);
      this.m_Color = Control.DefaultBackColor;
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
      this.components = (IContainer) new Container();
      this.tglbQuestion = new RadToggleButton();
      this.QaShape1 = new QAShape();
      this.lblChecklist = new RadLabel();
      this.RoundRectShape1 = new RoundRectShape(this.components);
      this.lblQuestion = new RadLabel();
      this.tblLayout = new TableLayoutPanel();
      this.tglbQuestion.BeginInit();
      this.lblChecklist.BeginInit();
      this.lblQuestion.BeginInit();
      this.tblLayout.SuspendLayout();
      this.SuspendLayout();
      this.tglbQuestion.AutoSize = true;
      this.tglbQuestion.Image = (Image) BuilderRED.My.Resources.Resources.Collapse_Down;
      this.tglbQuestion.Location = new Point(3, 3);
      this.tglbQuestion.Name = "tglbQuestion";
      this.tglbQuestion.Size = new Size(18, 18);
      this.tglbQuestion.TabIndex = 4;
      this.tglbQuestion.ThemeName = "Windows7";
      this.lblChecklist.BackColor = Color.Transparent;
      this.lblChecklist.BorderVisible = true;
      this.tblLayout.SetColumnSpan((Control) this.lblChecklist, 2);
      this.lblChecklist.Location = new Point(0, 24);
      this.lblChecklist.Margin = new Padding(0);
      this.lblChecklist.MaximumSize = new Size(0, 500);
      this.lblChecklist.Name = "lblChecklist";
      this.lblChecklist.Padding = new Padding(10, 0, 10, 0);
      this.lblChecklist.RootElement.MaxSize = new Size(0, 500);
      this.lblChecklist.RootElement.Padding = new Padding(10, 0, 10, 0);
      this.lblChecklist.Size = new Size(95, 26);
      this.lblChecklist.TabIndex = 0;
      this.lblChecklist.Text = "RadLabel";
      this.lblChecklist.Visible = false;
      ((RadLabelElement) this.lblChecklist.GetChildAt(0)).BorderVisible = true;
      ((RadItem) this.lblChecklist.GetChildAt(0)).Text = "RadLabel";
      this.lblChecklist.GetChildAt(0).Padding = new Padding(10, 0, 0, 0);
      this.lblChecklist.GetChildAt(0).FitToSizeMode = RadFitToSizeMode.FitToParentPadding;
      ((BorderPrimitive) this.lblChecklist.GetChildAt(0).GetChildAt(1)).FitToSizeMode = RadFitToSizeMode.FitToParentContent;
      this.lblChecklist.GetChildAt(0).GetChildAt(1).Padding = new Padding(9);
      this.lblChecklist.GetChildAt(0).GetChildAt(1).Margin = new Padding(0, 0, 20, 0);
      this.lblChecklist.GetChildAt(0).GetChildAt(1).Visibility = ElementVisibility.Visible;
      this.lblChecklist.GetChildAt(0).GetChildAt(1).Shape = (ElementShape) null;
      this.lblChecklist.GetChildAt(0).GetChildAt(2).Padding = new Padding(0);
      this.lblChecklist.GetChildAt(0).GetChildAt(2).Margin = new Padding(5);
      this.lblChecklist.GetChildAt(0).GetChildAt(2).PositionOffset = new SizeF(0.0f, 0.0f);
      ((TextPrimitive) this.lblChecklist.GetChildAt(0).GetChildAt(2).GetChildAt(1)).TextAlignment = ContentAlignment.TopLeft;
      this.lblChecklist.GetChildAt(0).GetChildAt(2).GetChildAt(1).Padding = new Padding(0, 0, 50, 0);
      this.lblChecklist.GetChildAt(0).GetChildAt(2).GetChildAt(1).Margin = new Padding(0, 0, 20, 0);
      this.lblQuestion.BackColor = Color.Transparent;
      this.lblQuestion.Dock = DockStyle.Fill;
      this.lblQuestion.Location = new Point(24, 0);
      this.lblQuestion.Margin = new Padding(0);
      this.lblQuestion.MaximumSize = new Size(0, 500);
      this.lblQuestion.Name = "lblQuestion";
      this.lblQuestion.Padding = new Padding(0, 0, 20, 0);
      this.lblQuestion.RootElement.MaxSize = new Size(0, 500);
      this.lblQuestion.RootElement.Padding = new Padding(0, 0, 20, 0);
      this.lblQuestion.Size = new Size(95, 21);
      this.lblQuestion.TabIndex = 8;
      this.lblQuestion.Text = "RadLabel";
      ((RadLabelElement) this.lblQuestion.GetChildAt(0)).BorderVisible = false;
      ((RadItem) this.lblQuestion.GetChildAt(0)).Text = "RadLabel";
      this.lblQuestion.GetChildAt(0).Padding = new Padding(0, 0, 20, 0);
      this.lblQuestion.GetChildAt(0).FitToSizeMode = RadFitToSizeMode.FitToParentPadding;
      this.lblQuestion.GetChildAt(0).Shape = (ElementShape) this.RoundRectShape1;
      ((BorderPrimitive) this.lblQuestion.GetChildAt(0).GetChildAt(1)).FitToSizeMode = RadFitToSizeMode.FitToParentContent;
      this.lblQuestion.GetChildAt(0).GetChildAt(1).Padding = new Padding(0, 0, 0, 9);
      this.lblQuestion.GetChildAt(0).GetChildAt(1).Margin = new Padding(0, 0, 20, 0);
      this.lblQuestion.GetChildAt(0).GetChildAt(1).Visibility = ElementVisibility.Hidden;
      this.lblQuestion.GetChildAt(0).GetChildAt(1).Shape = (ElementShape) null;
      this.lblQuestion.GetChildAt(0).GetChildAt(2).Padding = new Padding(0);
      this.lblQuestion.GetChildAt(0).GetChildAt(2).Margin = new Padding(0, 0, 0, 5);
      this.lblQuestion.GetChildAt(0).GetChildAt(2).PositionOffset = new SizeF(0.0f, 0.0f);
      ((TextPrimitive) this.lblQuestion.GetChildAt(0).GetChildAt(2).GetChildAt(1)).TextAlignment = ContentAlignment.TopLeft;
      this.lblQuestion.GetChildAt(0).GetChildAt(2).GetChildAt(1).Padding = new Padding(0);
      this.lblQuestion.GetChildAt(0).GetChildAt(2).GetChildAt(1).Margin = new Padding(0, 0, 20, 0);
      this.tblLayout.AutoSize = true;
      this.tblLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tblLayout.ColumnCount = 2;
      this.tblLayout.ColumnStyles.Add(new ColumnStyle());
      this.tblLayout.ColumnStyles.Add(new ColumnStyle());
      this.tblLayout.Controls.Add((Control) this.tglbQuestion, 0, 0);
      this.tblLayout.Controls.Add((Control) this.lblChecklist, 0, 1);
      this.tblLayout.Controls.Add((Control) this.lblQuestion, 1, 0);
      this.tblLayout.Dock = DockStyle.Fill;
      this.tblLayout.Location = new Point(0, 0);
      this.tblLayout.Name = "tblLayout";
      this.tblLayout.Padding = new Padding(0, 0, 10, 0);
      this.tblLayout.RowCount = 2;
      this.tblLayout.RowStyles.Add(new RowStyle());
      this.tblLayout.RowStyles.Add(new RowStyle());
      this.tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tblLayout.Size = new Size(129, 50);
      this.tblLayout.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.Controls.Add((Control) this.tblLayout);
      this.Margin = new Padding(0);
      this.Name = nameof (ctrlADAQuestions);
      this.Size = new Size(129, 50);
      this.tglbQuestion.EndInit();
      this.lblChecklist.EndInit();
      this.lblQuestion.EndInit();
      this.tblLayout.ResumeLayout(false);
      this.tblLayout.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual RadToggleButton tglbQuestion
    {
      get
      {
        return this._tglbQuestion;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        StateChangedEventHandler changedEventHandler = new StateChangedEventHandler(this.tglbQuestion_ToggleStateChanged);
        RadToggleButton tglbQuestion1 = this._tglbQuestion;
        if (tglbQuestion1 != null)
          tglbQuestion1.ToggleStateChanged -= changedEventHandler;
        this._tglbQuestion = value;
        RadToggleButton tglbQuestion2 = this._tglbQuestion;
        if (tglbQuestion2 == null)
          return;
        tglbQuestion2.ToggleStateChanged += changedEventHandler;
      }
    }

    internal virtual QAShape QaShape1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblChecklist { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RoundRectShape RoundRectShape1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblQuestion
    {
      get
      {
        return this._lblQuestion;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.lblQuestion_Click);
        RadLabel lblQuestion1 = this._lblQuestion;
        if (lblQuestion1 != null)
          lblQuestion1.Click -= eventHandler;
        this._lblQuestion = value;
        RadLabel lblQuestion2 = this._lblQuestion;
        if (lblQuestion2 == null)
          return;
        lblQuestion2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel tblLayout { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public int QuestionNumber
    {
      get
      {
        return this.m_Question_Number;
      }
      set
      {
        this.m_Question_Number = value;
      }
    }

    public Color ControlColor
    {
      get
      {
        return this.m_Color;
      }
      set
      {
        this.BackColor = value;
      }
    }

    private void tglbQuestion_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      this.lblChecklist.Visible = this.tglbQuestion.IsChecked;
      if (this.lblChecklist.Visible)
        this.tglbQuestion.Image = (Image) BuilderRED.My.Resources.Resources.Collapse_Up;
      else
        this.tglbQuestion.Image = (Image) BuilderRED.My.Resources.Resources.Collapse_Down;
    }

    private void ctrlADAQuestions_Resize(object sender, EventArgs e)
    {
      this.lblChecklist.MaximumSize = new Size(this.Width, 0);
      this.lblQuestion.MaximumSize = new Size(this.Width, 0);
    }

    private void lblQuestion_Click(object sender, EventArgs e)
    {
      this.tglbQuestion.PerformClick();
    }
  }
}
