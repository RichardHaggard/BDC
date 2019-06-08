// Decompiled with JetBrains decompiler
// Type: BuilderRED.ctrlComments
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinSpellChecker;
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
  public class ctrlComments : UserControl
  {
    private IContainer components;
    private const string MOD_NAME = "ctrlComment";

    public ctrlComments()
    {
      this.Load += new EventHandler(this.frmComment_Load);
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
      this.tsMain = new ToolStrip();
      this.tsbSpellcheck = new ToolStripButton();
      this.Frame1 = new GroupBox();
      this.txtComment = new TextBox();
      this.UltraSpellChecker1 = new UltraSpellChecker(this.components);
      this.HelpProvider = new HelpProvider();
      this.ToolTip1 = new ToolTip(this.components);
      this.tsMain.SuspendLayout();
      this.Frame1.SuspendLayout();
      this.UltraSpellChecker1.BeginInit();
      this.SuspendLayout();
      this.tsMain.GripStyle = ToolStripGripStyle.Hidden;
      this.tsMain.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.tsbSpellcheck
      });
      this.tsMain.Location = new Point(0, 0);
      this.tsMain.Name = "tsMain";
      this.tsMain.Size = new Size(395, 25);
      this.tsMain.TabIndex = 6;
      this.tsMain.Text = "ToolStrip1";
      this.tsbSpellcheck.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSpellcheck.Image = (Image) BuilderRED.My.Resources.Resources.Format_Spell_Check;
      this.tsbSpellcheck.ImageTransparentColor = Color.Magenta;
      this.tsbSpellcheck.Name = "tsbSpellcheck";
      this.tsbSpellcheck.Size = new Size(23, 22);
      this.tsbSpellcheck.Text = "ToolStripButton3";
      this.tsbSpellcheck.ToolTipText = "Check Spelling";
      this.Frame1.BackColor = SystemColors.Control;
      this.Frame1.Controls.Add((Control) this.txtComment);
      this.Frame1.Dock = DockStyle.Fill;
      this.Frame1.ForeColor = SystemColors.ControlText;
      this.Frame1.Location = new Point(0, 25);
      this.Frame1.Name = "Frame1";
      this.Frame1.Padding = new Padding(5);
      this.Frame1.RightToLeft = RightToLeft.No;
      this.Frame1.Size = new Size(395, 132);
      this.Frame1.TabIndex = 7;
      this.Frame1.TabStop = false;
      this.txtComment.AcceptsReturn = true;
      this.txtComment.BackColor = SystemColors.Window;
      this.txtComment.Cursor = Cursors.IBeam;
      this.txtComment.Dock = DockStyle.Fill;
      this.txtComment.ForeColor = SystemColors.WindowText;
      this.txtComment.Location = new Point(5, 18);
      this.txtComment.MaxLength = 0;
      this.txtComment.Multiline = true;
      this.txtComment.Name = "txtComment";
      this.txtComment.RightToLeft = RightToLeft.No;
      this.txtComment.Size = new Size(385, 109);
      this.txtComment.TabIndex = 3;
      this.UltraSpellChecker1.ContainingControl = (ContainerControl) this;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.Frame1);
      this.Controls.Add((Control) this.tsMain);
      this.Name = nameof (ctrlComments);
      this.Size = new Size(395, 157);
      this.tsMain.ResumeLayout(false);
      this.tsMain.PerformLayout();
      this.Frame1.ResumeLayout(false);
      this.Frame1.PerformLayout();
      this.UltraSpellChecker1.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual ToolStrip tsMain { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripButton tsbSpellcheck
    {
      get
      {
        return this._tsbSpellcheck;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsbSpellcheck_Click);
        ToolStripButton tsbSpellcheck1 = this._tsbSpellcheck;
        if (tsbSpellcheck1 != null)
          tsbSpellcheck1.Click -= eventHandler;
        this._tsbSpellcheck = value;
        ToolStripButton tsbSpellcheck2 = this._tsbSpellcheck;
        if (tsbSpellcheck2 == null)
          return;
        tsbSpellcheck2.Click += eventHandler;
      }
    }

    public virtual GroupBox Frame1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual TextBox txtComment
    {
      get
      {
        return this._txtComment;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtSectionAmount_KeyPress);
        TextBox txtComment1 = this._txtComment;
        if (txtComment1 != null)
          txtComment1.KeyPress -= pressEventHandler;
        this._txtComment = value;
        TextBox txtComment2 = this._txtComment;
        if (txtComment2 == null)
          return;
        txtComment2.KeyPress += pressEventHandler;
      }
    }

    internal virtual UltraSpellChecker UltraSpellChecker1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual ToolTip ToolTip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public TextBox TextBox
    {
      get
      {
        return this.txtComment;
      }
      set
      {
        this.txtComment = value;
      }
    }

    private void frmComment_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "Comments");
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, "ctrlComment", "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void txtSectionAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = Operators.CompareString(Conversions.ToString(e.KeyChar), "'", false) == 0;
    }

    private void tsbSpellcheck_Click(object sender, EventArgs e)
    {
      int num = (int) this.UltraSpellChecker1.ShowSpellCheckDialog((object) this.txtComment);
    }
  }
}
