// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmComment
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinSpellChecker;
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
  internal class frmComment : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmComment";
    private bool _bCanEdit;
    private bool _bChanged;
    private bool _bChangedWithoutSave;
    private bool _bLoaded;
    private string _strComment;

    public frmComment()
    {
      this.FormClosing += new FormClosingEventHandler(this.frmComment_FormClosing);
      this.Load += new EventHandler(this.frmComment_Load);
      this.InitializeComponent();
      float num = this.CreateGraphics().DpiX / 96f;
      this.tsMain.ImageScalingSize = new Size(checked ((int) Math.Round(unchecked (16.0 * (double) num))), checked ((int) Math.Round(unchecked (16.0 * (double) num))));
    }

    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual RichTextBox txtComment
    {
      get
      {
        return this._txtComment;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtComment_TextChanged);
        RichTextBox txtComment1 = this._txtComment;
        if (txtComment1 != null)
          txtComment1.TextChanged -= eventHandler;
        this._txtComment = value;
        RichTextBox txtComment2 = this._txtComment;
        if (txtComment2 == null)
          return;
        txtComment2.TextChanged += eventHandler;
      }
    }

    public virtual GroupBox Frame1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual UltraSpellChecker UltraSpellChecker1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStrip tsMain { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripButton tsbOK
    {
      get
      {
        return this._tsbOK;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsbOK_Click);
        ToolStripButton tsbOk1 = this._tsbOK;
        if (tsbOk1 != null)
          tsbOk1.Click -= eventHandler;
        this._tsbOK = value;
        ToolStripButton tsbOk2 = this._tsbOK;
        if (tsbOk2 == null)
          return;
        tsbOk2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbCancel
    {
      get
      {
        return this._tsbCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsbCancel_Click);
        ToolStripButton tsbCancel1 = this._tsbCancel;
        if (tsbCancel1 != null)
          tsbCancel1.Click -= eventHandler;
        this._tsbCancel = value;
        ToolStripButton tsbCancel2 = this._tsbCancel;
        if (tsbCancel2 == null)
          return;
        tsbCancel2.Click += eventHandler;
      }
    }

    internal virtual ToolStripSeparator ToolStripSeparator1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

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

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.Frame1 = new GroupBox();
      this.txtComment = new RichTextBox();
      this.HelpProvider = new HelpProvider();
      this.UltraSpellChecker1 = new UltraSpellChecker(this.components);
      this.tsMain = new ToolStrip();
      this.tsbOK = new ToolStripButton();
      this.tsbCancel = new ToolStripButton();
      this.ToolStripSeparator1 = new ToolStripSeparator();
      this.tsbSpellcheck = new ToolStripButton();
      this.tsbTimeStamp = new ToolStripButton();
      this.Frame1.SuspendLayout();
      this.UltraSpellChecker1.BeginInit();
      this.tsMain.SuspendLayout();
      this.SuspendLayout();
      this.Frame1.BackColor = SystemColors.Control;
      this.Frame1.Controls.Add((Control) this.txtComment);
      this.Frame1.ForeColor = SystemColors.ControlText;
      this.Frame1.Location = new Point(12, 28);
      this.Frame1.Name = "Frame1";
      this.Frame1.RightToLeft = RightToLeft.No;
      this.Frame1.Size = new Size(613, 182);
      this.Frame1.TabIndex = 2;
      this.Frame1.TabStop = false;
      this.txtComment.BackColor = SystemColors.Window;
      this.txtComment.Cursor = Cursors.IBeam;
      this.txtComment.Dock = DockStyle.Fill;
      this.txtComment.ForeColor = SystemColors.WindowText;
      this.txtComment.Location = new Point(3, 16);
      this.txtComment.MaxLength = 0;
      this.txtComment.Name = "txtComment";
      this.txtComment.RightToLeft = RightToLeft.No;
      this.txtComment.Size = new Size(607, 163);
      this.UltraSpellChecker1.SetSpellCheckerSettings((Control) this.txtComment, new SpellCheckerSettings(true));
      this.txtComment.TabIndex = 3;
      this.txtComment.Text = "";
      this.UltraSpellChecker1.ContainingControl = (ContainerControl) this;
      this.tsMain.GripStyle = ToolStripGripStyle.Hidden;
      this.tsMain.Items.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.tsbOK,
        (ToolStripItem) this.tsbCancel,
        (ToolStripItem) this.ToolStripSeparator1,
        (ToolStripItem) this.tsbSpellcheck,
        (ToolStripItem) this.tsbTimeStamp
      });
      this.tsMain.Location = new Point(0, 0);
      this.tsMain.Name = "tsMain";
      this.tsMain.Size = new Size(631, 25);
      this.tsMain.TabIndex = 5;
      this.tsMain.Text = "ToolStrip1";
      this.tsbOK.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbOK.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Check;
      this.tsbOK.ImageTransparentColor = Color.Magenta;
      this.tsbOK.Name = "tsbOK";
      this.tsbOK.Size = new Size(23, 22);
      this.tsbOK.Text = "ToolStripButton1";
      this.tsbOK.ToolTipText = "Submit Changes";
      this.tsbCancel.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbCancel.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Delete;
      this.tsbCancel.ImageTransparentColor = Color.Magenta;
      this.tsbCancel.Name = "tsbCancel";
      this.tsbCancel.Size = new Size(23, 22);
      this.tsbCancel.Text = "ToolStripButton2";
      this.tsbCancel.ToolTipText = "Cancel Changes";
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new Size(6, 25);
      this.tsbSpellcheck.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSpellcheck.Image = (Image) BuilderRED.My.Resources.Resources.Format_Spell_Check;
      this.tsbSpellcheck.ImageTransparentColor = Color.Magenta;
      this.tsbSpellcheck.Name = "tsbSpellcheck";
      this.tsbSpellcheck.Size = new Size(23, 22);
      this.tsbSpellcheck.Text = "ToolStripButton3";
      this.tsbSpellcheck.ToolTipText = "Check Spelling";
      this.tsbTimeStamp.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbTimeStamp.Image = (Image) BuilderRED.My.Resources.Resources.Approvals;
      this.tsbTimeStamp.ImageTransparentColor = Color.Magenta;
      this.tsbTimeStamp.Name = "tsbTimeStamp";
      this.tsbTimeStamp.Size = new Size(23, 22);
      this.tsbTimeStamp.Text = "Stamp";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(631, 222);
      this.ControlBox = false;
      this.Controls.Add((Control) this.tsMain);
      this.Controls.Add((Control) this.Frame1);
      this.Cursor = Cursors.Default;
      this.HelpProvider.SetHelpKeyword((Control) this, "1045");
      this.Location = new Point(184, 250);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmComment);
      this.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this, true);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Comment";
      this.Frame1.ResumeLayout(false);
      this.UltraSpellChecker1.EndInit();
      this.tsMain.ResumeLayout(false);
      this.tsMain.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual ToolStripButton tsbTimeStamp
    {
      get
      {
        return this._tsbTimeStamp;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsbTimeStamp_Click);
        ToolStripButton tsbTimeStamp1 = this._tsbTimeStamp;
        if (tsbTimeStamp1 != null)
          tsbTimeStamp1.Click -= eventHandler;
        this._tsbTimeStamp = value;
        ToolStripButton tsbTimeStamp2 = this._tsbTimeStamp;
        if (tsbTimeStamp2 == null)
          return;
        tsbTimeStamp2.Click += eventHandler;
      }
    }

    private bool CanEdit
    {
      get
      {
        return this._bCanEdit;
      }
      set
      {
        this._bCanEdit = value;
        this.txtComment.ReadOnly = !value;
      }
    }

    private DialogResult CheckOKToClose()
    {
      DialogResult dialogResult;
      if (this._bChangedWithoutSave)
      {
        switch (Interaction.MsgBox((object) "Data has changed.  Submit changes before closing?", MsgBoxStyle.YesNoCancel, (object) null))
        {
          case MsgBoxResult.Cancel:
            dialogResult = DialogResult.Cancel;
            break;
          case MsgBoxResult.Yes:
            this.SaveComments();
            dialogResult = DialogResult.Yes;
            break;
          case MsgBoxResult.No:
            dialogResult = !this._bChanged ? DialogResult.No : DialogResult.Yes;
            break;
        }
      }
      else
        dialogResult = !this._bChanged ? DialogResult.No : DialogResult.Yes;
      return dialogResult;
    }

    public string Comment
    {
      get
      {
        return this._strComment;
      }
    }

    public DialogResult EditComment(
      string strCaption,
      object strComment,
      bool bCanEdit,
      IWin32Window owner = null)
    {
      DialogResult dialogResult;
      try
      {
        if (!bCanEdit)
          strCaption += " (READ ONLY)";
        this.Text = strCaption;
        this._bLoaded = false;
        this._strComment = !Information.IsDBNull(RuntimeHelpers.GetObjectValue(strComment)) && Operators.CompareString(Strings.Trim(Conversions.ToString(strComment)), "", false) != 0 ? Conversions.ToString(strComment) : "";
        this.AddCommentWithBoldedTimeStampToRichTextBox(this._strComment, this.txtComment);
        this._bLoaded = true;
        this.CanEdit = bCanEdit;
        dialogResult = this.ShowDialog(owner);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), "NewComment");
        ProjectData.ClearProjectError();
      }
      return dialogResult;
    }

    public string NewComment(string strCaption, object strComment, bool bCanEdit)
    {
      string str = "";
      try
      {
        this.Text = strCaption;
        this._strComment = !Operators.ConditionalCompareObjectEqual(Operators.ConcatenateObject(strComment, (object) ""), (object) "", false) ? Conversions.ToString(strComment) : "";
        this.CanEdit = bCanEdit;
        this.RefreshData();
        int num = (int) this.ShowDialog();
        str = this._strComment;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), nameof (NewComment));
        ProjectData.ClearProjectError();
      }
      return str;
    }

    private void RefreshData()
    {
      try
      {
        this._bLoaded = false;
        this.txtComment.Text = this._strComment;
        this.txtComment.ReadOnly = !this._bCanEdit;
        this._bLoaded = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), nameof (RefreshData));
        ProjectData.ClearProjectError();
      }
    }

    private void SetChanged(bool aflag)
    {
      try
      {
        if (!this._bLoaded)
          return;
        this._bChangedWithoutSave = aflag;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), nameof (SetChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void frmComment_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        DialogResult close = this.CheckOKToClose();
        if (close == DialogResult.Cancel)
          e.Cancel = true;
        else
          this.DialogResult = close;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), "frmComment_Closing");
        ProjectData.ClearProjectError();
      }
    }

    private void frmComment_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
        this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
        this.HelpProvider.SetHelpKeyword((Control) this, "Comments");
        this._bChanged = false;
        this.tsbOK.Enabled = this.CanEdit;
        this.txtComment.SelectionStart = this.txtComment.Text.Length;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void txtComment_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.SetChanged(true);
    }

    private void tsbOK_Click(object sender, EventArgs e)
    {
      try
      {
        this.SaveComments();
        this._bChanged = true;
        this._bChangedWithoutSave = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), "SaveButton_Click");
        ProjectData.ClearProjectError();
      }
      this.Close();
    }

    private void SaveComments()
    {
      try
      {
        string str = this.txtComment.Text;
        if (Strings.InStr(str, "'", CompareMethod.Binary) > 0)
        {
          if (MessageBox.Show("You cannot have a \"'\" in the comment; BUILDER doesn't allow this character.\r\nOkay to remove this character from the comment?", "Invalid character found", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
          {
            int num = (int) MessageBox.Show("", "Not Saved, Invalid character found", MessageBoxButtons.OK);
            return;
          }
          str = Strings.Replace(str, "'", "", 1, -1, CompareMethod.Binary);
        }
        this._strComment = str;
        this._bChanged = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmComment), nameof (SaveComments));
        ProjectData.ClearProjectError();
      }
    }

    private void tsbCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void tsbSpellcheck_Click(object sender, EventArgs e)
    {
      int num = (int) this.UltraSpellChecker1.ShowSpellCheckDialog((object) this.txtComment);
    }

    private void tsbTimeStamp_Click(object sender, EventArgs e)
    {
      this.txtComment.SelectionStart = this.txtComment.Text.Length;
      int num = this.txtComment.Text.IndexOf('\n', this.txtComment.Text.Length == 0 ? 0 : checked (this.txtComment.Text.Length - 1));
      if (num != -1)
        this.txtComment.SelectedText = "\r\n";
      else if (num == -1 && (uint) this.txtComment.Text.Length > 0U)
        this.txtComment.SelectedText = "\r\n\r\n";
      this.txtComment.SelectionFont = new Font(this.txtComment.Font, FontStyle.Bold);
      this.txtComment.SelectedText = "[" + mdUtility.User.FirstName + " " + mdUtility.User.LastName + " on " + DateTime.Now.ToString() + "] \r\n";
      this.txtComment.SelectionFont = new Font(this.txtComment.Font, FontStyle.Regular);
      this.txtComment.SelectionStart = this.txtComment.Text.Length;
    }

    private void AddCommentWithBoldedTimeStampToRichTextBox(string comment, RichTextBox rtf)
    {
      rtf.SelectionStart = rtf.Text.Length;
      while ((uint) Operators.CompareString(comment, "", false) > 0U)
      {
        if (comment.IndexOf("[") != -1)
        {
          bool flag = false;
          while (!flag && comment.IndexOf("[") != -1)
          {
            rtf.SelectedText = comment.Substring(0, comment.IndexOf("["));
            comment = comment.Remove(0, comment.IndexOf("["));
            if (comment.IndexOf("]") != -1 && (comment.IndexOf("[", 1) != -1 && comment.IndexOf("]") < comment.IndexOf("[", 1) || comment.IndexOf("[", 1) == -1))
            {
              flag = true;
            }
            else
            {
              rtf.SelectedText = comment.Substring(0, checked (comment.IndexOf("[") + 1));
              comment = comment.Remove(0, checked (comment.IndexOf("[") + 1));
            }
          }
          if (comment.IndexOf("]") != -1)
          {
            string str = comment.Substring(0, checked (comment.IndexOf("]") + 1));
            comment = comment.Remove(0, checked (comment.IndexOf("]") + 1));
            if (str.IndexOf(" on ") != -1)
            {
              string s = str.Substring(checked (str.IndexOf(" on ") + 3), checked (str.Length - str.IndexOf(" on ") - 4));
              DateTime dateTime = new DateTime();
              ref DateTime local = ref dateTime;
              if (DateTime.TryParse(s, out local))
              {
                rtf.SelectionFont = new Font(this.txtComment.Font, FontStyle.Bold);
                rtf.SelectedText = str;
                rtf.SelectionFont = new Font(this.txtComment.Font, FontStyle.Regular);
              }
              else
                rtf.SelectedText = str;
            }
            else
              rtf.SelectedText = str;
          }
          else
          {
            rtf.SelectedText = comment;
            comment = "";
          }
        }
        else
        {
          this.txtComment.SelectionStart = this.txtComment.Text.Length;
          rtf.SelectedText = comment;
          comment = "";
        }
      }
    }
  }
}
