// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmAboutBRED
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmAboutBRED : Form
  {
    private IContainer components;
    private const string MOD_NAME = "frmAboutBRED";

    public frmAboutBRED()
    {
      this.Load += new EventHandler(this.frmAboutBRED_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    internal virtual PictureBox PictureBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox grpAcknowledgements { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RichTextBox RichTextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button cmdHelp
    {
      get
      {
        return this._cmdHelp;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdHelp_Click);
        Button cmdHelp1 = this._cmdHelp;
        if (cmdHelp1 != null)
          cmdHelp1.Click -= eventHandler;
        this._cmdHelp = value;
        Button cmdHelp2 = this._cmdHelp;
        if (cmdHelp2 == null)
          return;
        cmdHelp2.Click += eventHandler;
      }
    }

    internal virtual Button OK
    {
      get
      {
        return this._OK;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.OK_Click);
        Button ok1 = this._OK;
        if (ok1 != null)
          ok1.Click -= eventHandler;
        this._OK = value;
        Button ok2 = this._OK;
        if (ok2 == null)
          return;
        ok2.Click += eventHandler;
      }
    }

    internal virtual Label lblBREDVersion { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblInventoryVersion { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblLookupVersion { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblBREDDB { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblInventoryDB { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblLookupDB { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblBREDProgram { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmAboutBRED));
      this.PictureBox1 = new PictureBox();
      this.grpAcknowledgements = new GroupBox();
      this.RichTextBox1 = new RichTextBox();
      this.cmdHelp = new Button();
      this.OK = new Button();
      this.lblBREDVersion = new Label();
      this.lblInventoryVersion = new Label();
      this.lblLookupVersion = new Label();
      this.lblBREDDB = new Label();
      this.lblInventoryDB = new Label();
      this.lblLookupDB = new Label();
      this.lblBREDProgram = new Label();
      this.Label2 = new Label();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      ((ISupportInitialize) this.PictureBox1).BeginInit();
      this.grpAcknowledgements.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.PictureBox1.Dock = DockStyle.Fill;
      this.PictureBox1.Image = (Image) componentResourceManager.GetObject("PictureBox1.Image");
      this.PictureBox1.Location = new Point(3, 3);
      this.PictureBox1.Name = "PictureBox1";
      this.TableLayoutPanel1.SetRowSpan((Control) this.PictureBox1, 5);
      this.PictureBox1.Size = new Size(167, 336);
      this.PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
      this.PictureBox1.TabIndex = 0;
      this.PictureBox1.TabStop = false;
      this.grpAcknowledgements.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.grpAcknowledgements, 3);
      this.grpAcknowledgements.Controls.Add((Control) this.RichTextBox1);
      this.grpAcknowledgements.Location = new Point(176, 3);
      this.grpAcknowledgements.Name = "grpAcknowledgements";
      this.grpAcknowledgements.Size = new Size(567, 232);
      this.grpAcknowledgements.TabIndex = 1;
      this.grpAcknowledgements.TabStop = false;
      this.grpAcknowledgements.Text = "Acknowledgements";
      this.RichTextBox1.Dock = DockStyle.Fill;
      this.RichTextBox1.Location = new Point(3, 16);
      this.RichTextBox1.Name = "RichTextBox1";
      this.RichTextBox1.ReadOnly = true;
      this.RichTextBox1.Size = new Size(561, 213);
      this.RichTextBox1.TabIndex = 0;
      this.RichTextBox1.TabStop = false;
      this.RichTextBox1.Text = componentResourceManager.GetString("RichTextBox1.Text");
      this.cmdHelp.Dock = DockStyle.Fill;
      this.cmdHelp.Location = new Point(655, 305);
      this.cmdHelp.Name = "cmdHelp";
      this.TableLayoutPanel1.SetRowSpan((Control) this.cmdHelp, 2);
      this.cmdHelp.Size = new Size(88, 34);
      this.cmdHelp.TabIndex = 2;
      this.cmdHelp.Text = "More...";
      this.OK.Dock = DockStyle.Fill;
      this.OK.Location = new Point(655, 241);
      this.OK.Name = "OK";
      this.TableLayoutPanel1.SetRowSpan((Control) this.OK, 2);
      this.OK.Size = new Size(88, 58);
      this.OK.TabIndex = 3;
      this.OK.Text = "OK";
      this.lblBREDVersion.AutoSize = true;
      this.lblBREDVersion.Dock = DockStyle.Fill;
      this.lblBREDVersion.Location = new Point(176, 289);
      this.lblBREDVersion.Name = "lblBREDVersion";
      this.lblBREDVersion.Size = new Size(141, 13);
      this.lblBREDVersion.TabIndex = 4;
      this.lblBREDVersion.Text = "BRED Database Version:";
      this.lblBREDVersion.TextAlign = ContentAlignment.TopRight;
      this.lblInventoryVersion.AutoSize = true;
      this.lblInventoryVersion.Dock = DockStyle.Fill;
      this.lblInventoryVersion.Location = new Point(176, 302);
      this.lblInventoryVersion.Name = "lblInventoryVersion";
      this.lblInventoryVersion.Size = new Size(141, 13);
      this.lblInventoryVersion.TabIndex = 5;
      this.lblInventoryVersion.Text = "Inventory Database Version:";
      this.lblInventoryVersion.TextAlign = ContentAlignment.TopRight;
      this.lblLookupVersion.AutoSize = true;
      this.lblLookupVersion.Dock = DockStyle.Fill;
      this.lblLookupVersion.Location = new Point(176, 315);
      this.lblLookupVersion.Name = "lblLookupVersion";
      this.lblLookupVersion.Size = new Size(141, 27);
      this.lblLookupVersion.TabIndex = 6;
      this.lblLookupVersion.Text = "Lookup Database Version:";
      this.lblLookupVersion.TextAlign = ContentAlignment.TopRight;
      this.lblBREDDB.AutoSize = true;
      this.lblBREDDB.Dock = DockStyle.Fill;
      this.lblBREDDB.Location = new Point(323, 289);
      this.lblBREDDB.Name = "lblBREDDB";
      this.lblBREDDB.Size = new Size(326, 13);
      this.lblBREDDB.TabIndex = 7;
      this.lblInventoryDB.AutoSize = true;
      this.lblInventoryDB.Dock = DockStyle.Fill;
      this.lblInventoryDB.Location = new Point(323, 302);
      this.lblInventoryDB.Name = "lblInventoryDB";
      this.lblInventoryDB.Size = new Size(326, 13);
      this.lblInventoryDB.TabIndex = 8;
      this.lblLookupDB.AutoSize = true;
      this.lblLookupDB.Dock = DockStyle.Fill;
      this.lblLookupDB.Location = new Point(323, 315);
      this.lblLookupDB.Name = "lblLookupDB";
      this.lblLookupDB.Size = new Size(326, 27);
      this.lblLookupDB.TabIndex = 9;
      this.lblBREDProgram.AutoSize = true;
      this.lblBREDProgram.Dock = DockStyle.Fill;
      this.lblBREDProgram.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblBREDProgram.Location = new Point(323, 238);
      this.lblBREDProgram.Name = "lblBREDProgram";
      this.lblBREDProgram.Size = new Size(326, 51);
      this.lblBREDProgram.TabIndex = 11;
      this.lblBREDProgram.TextAlign = ContentAlignment.MiddleLeft;
      this.Label2.AutoSize = true;
      this.Label2.Dock = DockStyle.Fill;
      this.Label2.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.Label2.Location = new Point(176, 238);
      this.Label2.Name = "Label2";
      this.Label2.Size = new Size(141, 51);
      this.Label2.TabIndex = 10;
      this.Label2.Text = "BRED Version:";
      this.Label2.TextAlign = ContentAlignment.MiddleRight;
      this.TableLayoutPanel1.ColumnCount = 4;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdHelp, 3, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.OK, 3, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblInventoryDB, 2, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblLookupDB, 2, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblBREDVersion, 1, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.Label2, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblBREDProgram, 2, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblInventoryVersion, 1, 3);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblLookupVersion, 1, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.PictureBox1, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.grpAcknowledgements, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblBREDDB, 2, 2);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 5;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(746, 342);
      this.TableLayoutPanel1.TabIndex = 12;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(746, 342);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Name = nameof (frmAboutBRED);
      this.Text = "About Builder RED...";
      ((ISupportInitialize) this.PictureBox1).EndInit();
      this.grpAcknowledgements.ResumeLayout(false);
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void cmdHelp_Click(object sender, EventArgs e)
    {
      try
      {
        Help.ShowHelp((Control) this, mdUtility.HelpFilePath);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmAboutBRED), nameof (cmdHelp_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void OK_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void frmAboutBRED_Load(object sender, EventArgs e)
    {
      this.lblBREDProgram.Text = this.GetType().Assembly.GetName().Version.ToString();
      try
      {
        this.lblBREDDB.Text = this.GetVersion("BRED");
        this.lblInventoryDB.Text = this.GetVersion("Inventory");
        this.lblLookupDB.Text = this.GetVersion("Lookup");
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    private string GetVersion(string DBName)
    {
      string str;
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM AppVer_" + DBName);
        if (dataTable.Rows.Count > 0)
        {
          str = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(dataTable.Rows[0]["Version_Major"], (object) "."), dataTable.Rows[0]["Version_Minor"]), (object) "."), dataTable.Rows[0]["Version_Build"]));
          goto label_6;
        }
      }
      catch (OleDbException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        str = "";
        ProjectData.ClearProjectError();
        goto label_6;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmAboutBRED), nameof (GetVersion));
        str = "";
        ProjectData.ClearProjectError();
        goto label_6;
      }
      str = "";
label_6:
      return str;
    }
  }
}
