// Decompiled with JetBrains decompiler
// Type: BuilderRED.ctrlADARadioButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  public class ctrlADARadioButton : UserControl
  {
    private IContainer components;
    private string mComment;
    private double? mBudget;
    private ctrlADARadioButton.StatusRB? mStatusRB;
    private int mQuestionNumber;
    private Color m_Color;

    public ctrlADARadioButton()
    {
      this.mComment = (string) null;
      this.mBudget = new double?();
      this.mStatusRB = new ctrlADARadioButton.StatusRB?();
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
      this.ToolTip1 = new ToolTip(this.components);
      this.btnCompliant = new Button();
      this.btnNonCompliant = new Button();
      this.btnNA = new Button();
      this.btnComment = new Button();
      this.btnBudget = new Button();
      this.SuspendLayout();
      this.btnCompliant.AutoSize = true;
      this.btnCompliant.Location = new Point(3, 3);
      this.btnCompliant.Name = "btnCompliant";
      this.btnCompliant.Size = new Size(75, 25);
      this.btnCompliant.TabIndex = 3;
      this.btnCompliant.UseVisualStyleBackColor = true;
      this.btnNonCompliant.AutoSize = true;
      this.btnNonCompliant.Location = new Point(78, 3);
      this.btnNonCompliant.Name = "btnNonCompliant";
      this.btnNonCompliant.Size = new Size(75, 25);
      this.btnNonCompliant.TabIndex = 4;
      this.btnNonCompliant.UseVisualStyleBackColor = true;
      this.btnNA.BackColor = SystemColors.ControlDark;
      this.btnNA.BackgroundImageLayout = ImageLayout.Center;
      this.btnNA.Location = new Point(153, 3);
      this.btnNA.Name = "btnNA";
      this.btnNA.Size = new Size(75, 25);
      this.btnNA.TabIndex = 5;
      this.btnNA.UseVisualStyleBackColor = true;
      this.btnComment.AutoSize = true;
      this.btnComment.BackColor = SystemColors.ControlDark;
      this.btnComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.btnComment.Location = new Point(234, 3);
      this.btnComment.Name = "btnComment";
      this.btnComment.Size = new Size(45, 25);
      this.btnComment.TabIndex = 7;
      this.btnComment.UseVisualStyleBackColor = true;
      this.btnBudget.AutoSize = true;
      this.btnBudget.BackColor = SystemColors.ControlDark;
      this.btnBudget.Image = (Image) BuilderRED.My.Resources.Resources.Sign_Dollar___Blue;
      this.btnBudget.Location = new Point(279, 3);
      this.btnBudget.Margin = new Padding(3, 3, 0, 3);
      this.btnBudget.Name = "btnBudget";
      this.btnBudget.Size = new Size(45, 25);
      this.btnBudget.TabIndex = 6;
      this.btnBudget.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.Controls.Add((Control) this.btnComment);
      this.Controls.Add((Control) this.btnBudget);
      this.Controls.Add((Control) this.btnNA);
      this.Controls.Add((Control) this.btnNonCompliant);
      this.Controls.Add((Control) this.btnCompliant);
      this.Margin = new Padding(0);
      this.MinimumSize = new Size(243, 29);
      this.Name = nameof (ctrlADARadioButton);
      this.Size = new Size(324, 31);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnCompliant
    {
      get
      {
        return this._btnCompliant;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnCompliant_Clicked);
        Button btnCompliant1 = this._btnCompliant;
        if (btnCompliant1 != null)
          btnCompliant1.Click -= eventHandler;
        this._btnCompliant = value;
        Button btnCompliant2 = this._btnCompliant;
        if (btnCompliant2 == null)
          return;
        btnCompliant2.Click += eventHandler;
      }
    }

    internal virtual Button btnNonCompliant
    {
      get
      {
        return this._btnNonCompliant;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnNonCompliant_Clicked);
        Button btnNonCompliant1 = this._btnNonCompliant;
        if (btnNonCompliant1 != null)
          btnNonCompliant1.Click -= eventHandler;
        this._btnNonCompliant = value;
        Button btnNonCompliant2 = this._btnNonCompliant;
        if (btnNonCompliant2 == null)
          return;
        btnNonCompliant2.Click += eventHandler;
      }
    }

    internal virtual Button btnNA
    {
      get
      {
        return this._btnNA;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnNA_Clicked);
        Button btnNa1 = this._btnNA;
        if (btnNa1 != null)
          btnNa1.Click -= eventHandler;
        this._btnNA = value;
        Button btnNa2 = this._btnNA;
        if (btnNa2 == null)
          return;
        btnNa2.Click += eventHandler;
      }
    }

    internal virtual Button btnBudget
    {
      get
      {
        return this._btnBudget;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnBudget_Click);
        Button btnBudget1 = this._btnBudget;
        if (btnBudget1 != null)
          btnBudget1.Click -= eventHandler;
        this._btnBudget = value;
        Button btnBudget2 = this._btnBudget;
        if (btnBudget2 == null)
          return;
        btnBudget2.Click += eventHandler;
      }
    }

    internal virtual Button btnComment
    {
      get
      {
        return this._btnComment;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnComment_Click);
        Button btnComment1 = this._btnComment;
        if (btnComment1 != null)
          btnComment1.Click -= eventHandler;
        this._btnComment = value;
        Button btnComment2 = this._btnComment;
        if (btnComment2 == null)
          return;
        btnComment2.Click += eventHandler;
      }
    }

    public string Comment
    {
      get
      {
        return this.mComment;
      }
      set
      {
        this.mComment = value;
        if ((uint) Operators.CompareString(this.mComment, "", false) > 0U)
          this.btnComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
        else
          this.btnComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      }
    }

    public double? Budget
    {
      get
      {
        return this.mBudget;
      }
      set
      {
        this.mBudget = value;
        double? mBudget = this.mBudget;
        if ((mBudget.HasValue ? new bool?(mBudget.GetValueOrDefault() > 0.0) : new bool?()).GetValueOrDefault())
          this.btnBudget.Image = (Image) BuilderRED.My.Resources.Resources.Sign_Dollar___Blue_Check;
        else
          this.btnBudget.Image = (Image) BuilderRED.My.Resources.Resources.Sign_Dollar___Blue;
      }
    }

    public int QuestionNumber
    {
      get
      {
        return this.mQuestionNumber;
      }
      set
      {
        this.mQuestionNumber = value;
      }
    }

    public ctrlADARadioButton.StatusRB? Status
    {
      get
      {
        return this.mStatusRB;
      }
      set
      {
        this.mStatusRB = value;
        this.SelectRadioButton();
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
        this.m_Color = value;
      }
    }

    public void HideExtraButtons()
    {
      this.btnBudget.Visible = false;
      this.btnComment.Visible = false;
    }

    private void SelectRadioButton()
    {
      if (!this.mStatusRB.HasValue)
        return;
      Button btnCompliant = this.btnCompliant;
      ctrlADARadioButton.StatusRB? mStatusRb = this.mStatusRB;
      int? nullable1 = mStatusRb.HasValue ? new int?((int) mStatusRb.GetValueOrDefault()) : new int?();
      bool? nullable2 = nullable1.HasValue ? new bool?(nullable1.GetValueOrDefault() == 1) : new bool?();
      Bitmap bitmap1 = nullable2.GetValueOrDefault() ? BuilderRED.My.Resources.Resources.Plain_Check : (Bitmap) null;
      btnCompliant.Image = (Image) bitmap1;
      Button btnNa = this.btnNA;
      mStatusRb = this.mStatusRB;
      nullable1 = mStatusRb.HasValue ? new int?((int) mStatusRb.GetValueOrDefault()) : new int?();
      bool? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new bool?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new bool?(nullable1.GetValueOrDefault() == 0);
      nullable2 = nullable3;
      Bitmap bitmap2 = nullable2.GetValueOrDefault() ? BuilderRED.My.Resources.Resources.Symbol_NA : (Bitmap) null;
      btnNa.Image = (Image) bitmap2;
      Button btnNonCompliant = this.btnNonCompliant;
      mStatusRb = this.mStatusRB;
      nullable1 = mStatusRb.HasValue ? new int?((int) mStatusRb.GetValueOrDefault()) : new int?();
      bool? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new bool?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new bool?(nullable1.GetValueOrDefault() == -1);
      nullable2 = nullable4;
      Bitmap bitmap3 = nullable2.GetValueOrDefault() ? BuilderRED.My.Resources.Resources.Delete : (Bitmap) null;
      btnNonCompliant.Image = (Image) bitmap3;
    }

    private void btnNA_Clicked(object sender, EventArgs e)
    {
      this.Status = new ctrlADARadioButton.StatusRB?(ctrlADARadioButton.StatusRB.N_A);
      this.SelectRadioButton();
    }

    private void btnNonCompliant_Clicked(object sender, EventArgs e)
    {
      this.Status = new ctrlADARadioButton.StatusRB?(ctrlADARadioButton.StatusRB.NON_COMPLIANT);
      this.SelectRadioButton();
    }

    private void btnCompliant_Clicked(object sender, EventArgs e)
    {
      this.Status = new ctrlADARadioButton.StatusRB?(ctrlADARadioButton.StatusRB.COMPLIANT);
      this.SelectRadioButton();
    }

    private void btnBudget_Click(object sender, EventArgs e)
    {
      dlgBudget dlgBudget = new dlgBudget();
      try
      {
        this.Budget = new double?(dlgBudget.OpenBudgetDialog((IWin32Window) this.btnBudget, Conversions.ToDouble(Interaction.IIf(this.Budget.HasValue, (object) this.Budget, (object) null)), this.mQuestionNumber));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (btnBudget_Click));
        ProjectData.ClearProjectError();
      }
      finally
      {
        dlgBudget.Dispose();
      }
    }

    private void btnComment_Click(object sender, EventArgs e)
    {
      this.Comment = AssessmentHelpers.CommentDialog("Comment for Question " + Conversions.ToString(this.QuestionNumber), this.mComment, (IWin32Window) this);
    }

    public enum StatusRB
    {
      NON_COMPLIANT = -1,
      N_A = 0,
      COMPLIANT = 1,
    }
  }
}
