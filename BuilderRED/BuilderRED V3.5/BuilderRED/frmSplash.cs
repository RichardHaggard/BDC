// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmSplash
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmSplash : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;

    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual System.Windows.Forms.Label lblTM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual PictureBox Image1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblTitle2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblProductName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblCompanyProduct { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblPlatform { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblVersion { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual PictureBox imgAboutBuilder { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox fraMainFrame { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmSplash));
      this.ToolTip1 = new ToolTip(this.components);
      this.fraMainFrame = new GroupBox();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.Image1 = new PictureBox();
      this.lblTM = new System.Windows.Forms.Label();
      this.lblVersion = new System.Windows.Forms.Label();
      this.lblCompanyProduct = new System.Windows.Forms.Label();
      this.lblProductName = new System.Windows.Forms.Label();
      this.lblPlatform = new System.Windows.Forms.Label();
      this.lblTitle2 = new System.Windows.Forms.Label();
      this.imgAboutBuilder = new PictureBox();
      this.FlowLayoutPanel1 = new FlowLayoutPanel();
      this.fraMainFrame.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      ((ISupportInitialize) this.Image1).BeginInit();
      ((ISupportInitialize) this.imgAboutBuilder).BeginInit();
      this.FlowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.fraMainFrame.AutoSize = true;
      this.fraMainFrame.BackColor = SystemColors.Control;
      this.fraMainFrame.Controls.Add((Control) this.TableLayoutPanel1);
      this.fraMainFrame.Dock = DockStyle.Fill;
      this.fraMainFrame.ForeColor = SystemColors.ControlText;
      this.fraMainFrame.Location = new Point(6, 130);
      this.fraMainFrame.Margin = new Padding(6, 6, 6, 6);
      this.fraMainFrame.Name = "fraMainFrame";
      this.fraMainFrame.Padding = new Padding(6, 6, 6, 6);
      this.fraMainFrame.RightToLeft = RightToLeft.No;
      this.fraMainFrame.Size = new Size(990, 327);
      this.fraMainFrame.TabIndex = 0;
      this.fraMainFrame.TabStop = false;
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.ColumnCount = 3;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.Image1, 0, 5);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblTM, 2, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblVersion, 1, 5);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblCompanyProduct, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblProductName, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblPlatform, 1, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblTitle2, 0, 2);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(6, 30);
      this.TableLayoutPanel1.Margin = new Padding(6, 6, 6, 6);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 7;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Size = new Size(978, 291);
      this.TableLayoutPanel1.TabIndex = 8;
      this.Image1.Cursor = Cursors.Default;
      this.Image1.Dock = DockStyle.Fill;
      this.Image1.Image = (Image) componentResourceManager.GetObject("Image1.Image");
      this.Image1.Location = new Point(6, 195);
      this.Image1.Margin = new Padding(6, 6, 6, 6);
      this.Image1.Name = "Image1";
      this.Image1.Size = new Size(108, 90);
      this.Image1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.Image1.TabIndex = 7;
      this.Image1.TabStop = false;
      this.lblTM.AutoSize = true;
      this.lblTM.BackColor = SystemColors.Control;
      this.lblTM.Cursor = Cursors.Default;
      this.lblTM.Dock = DockStyle.Fill;
      this.lblTM.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTM.ForeColor = SystemColors.ControlText;
      this.lblTM.Location = new Point(928, 149);
      this.lblTM.Margin = new Padding(6, 0, 6, 0);
      this.lblTM.Name = "lblTM";
      this.lblTM.RightToLeft = RightToLeft.No;
      this.lblTM.Size = new Size(44, 40);
      this.lblTM.TabIndex = 6;
      this.lblTM.Text = "TM";
      this.lblVersion.AutoSize = true;
      this.lblVersion.BackColor = Color.Transparent;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblVersion, 2);
      this.lblVersion.Cursor = Cursors.Default;
      this.lblVersion.Dock = DockStyle.Fill;
      this.lblVersion.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblVersion.ForeColor = SystemColors.ControlText;
      this.lblVersion.Location = new Point(126, 189);
      this.lblVersion.Margin = new Padding(6, 0, 6, 0);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.RightToLeft = RightToLeft.No;
      this.lblVersion.Size = new Size(846, 102);
      this.lblVersion.TabIndex = 1;
      this.lblVersion.Tag = (object) "Version";
      this.lblVersion.Text = "Version";
      this.lblVersion.TextAlign = ContentAlignment.MiddleRight;
      this.lblCompanyProduct.AutoSize = true;
      this.lblCompanyProduct.BackColor = Color.Transparent;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblCompanyProduct, 3);
      this.lblCompanyProduct.Cursor = Cursors.Default;
      this.lblCompanyProduct.Dock = DockStyle.Fill;
      this.lblCompanyProduct.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblCompanyProduct.ForeColor = SystemColors.ControlText;
      this.lblCompanyProduct.Location = new Point(6, 0);
      this.lblCompanyProduct.Margin = new Padding(6, 0, 6, 0);
      this.lblCompanyProduct.Name = "lblCompanyProduct";
      this.lblCompanyProduct.RightToLeft = RightToLeft.No;
      this.lblCompanyProduct.Size = new Size(966, 30);
      this.lblCompanyProduct.TabIndex = 3;
      this.lblCompanyProduct.Tag = (object) "CompanyProduct";
      this.lblCompanyProduct.Text = "U.S. Army Construction Engineering Research Laboratory";
      this.lblProductName.AutoSize = true;
      this.lblProductName.BackColor = Color.Transparent;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblProductName, 3);
      this.lblProductName.Cursor = Cursors.Default;
      this.lblProductName.Dock = DockStyle.Fill;
      this.lblProductName.Font = new Font("Microsoft Sans Serif", 29.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblProductName.ForeColor = SystemColors.ControlText;
      this.lblProductName.Location = new Point(6, 30);
      this.lblProductName.Margin = new Padding(6, 0, 6, 0);
      this.lblProductName.Name = "lblProductName";
      this.lblProductName.Size = new Size(966, 89);
      this.lblProductName.TabIndex = 4;
      this.lblProductName.Tag = (object) "Product";
      this.lblProductName.Text = "Product";
      this.lblProductName.TextAlign = ContentAlignment.TopCenter;
      this.lblPlatform.AutoSize = true;
      this.lblPlatform.BackColor = Color.Transparent;
      this.lblPlatform.Cursor = Cursors.Default;
      this.lblPlatform.Dock = DockStyle.Fill;
      this.lblPlatform.Font = new Font("Microsoft Sans Serif", 13.5f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblPlatform.ForeColor = SystemColors.ControlText;
      this.lblPlatform.Location = new Point(126, 149);
      this.lblPlatform.Margin = new Padding(6, 0, 6, 0);
      this.lblPlatform.Name = "lblPlatform";
      this.lblPlatform.RightToLeft = RightToLeft.No;
      this.lblPlatform.Size = new Size(790, 40);
      this.lblPlatform.TabIndex = 2;
      this.lblPlatform.Tag = (object) "Platform";
      this.lblPlatform.Text = "for Windows";
      this.lblPlatform.TextAlign = ContentAlignment.TopRight;
      this.lblTitle2.AutoSize = true;
      this.lblTitle2.BackColor = Color.Transparent;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblTitle2, 3);
      this.lblTitle2.Cursor = Cursors.Default;
      this.lblTitle2.Dock = DockStyle.Fill;
      this.lblTitle2.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle2.ForeColor = SystemColors.ControlText;
      this.lblTitle2.Location = new Point(6, 119);
      this.lblTitle2.Margin = new Padding(6, 0, 6, 0);
      this.lblTitle2.Name = "lblTitle2";
      this.lblTitle2.RightToLeft = RightToLeft.No;
      this.lblTitle2.Size = new Size(966, 30);
      this.lblTitle2.TabIndex = 5;
      this.lblTitle2.Text = "(Remote Entry Database)";
      this.lblTitle2.TextAlign = ContentAlignment.TopCenter;
      this.imgAboutBuilder.Cursor = Cursors.Default;
      this.imgAboutBuilder.Image = (Image) componentResourceManager.GetObject("imgAboutBuilder.Image");
      this.imgAboutBuilder.Location = new Point(6, 6);
      this.imgAboutBuilder.Margin = new Padding(6, 6, 6, 6);
      this.imgAboutBuilder.Name = "imgAboutBuilder";
      this.imgAboutBuilder.Size = new Size(990, 112);
      this.imgAboutBuilder.SizeMode = PictureBoxSizeMode.Zoom;
      this.imgAboutBuilder.TabIndex = 9;
      this.imgAboutBuilder.TabStop = false;
      this.FlowLayoutPanel1.AutoSize = true;
      this.FlowLayoutPanel1.Controls.Add((Control) this.imgAboutBuilder);
      this.FlowLayoutPanel1.Controls.Add((Control) this.fraMainFrame);
      this.FlowLayoutPanel1.Dock = DockStyle.Fill;
      this.FlowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
      this.FlowLayoutPanel1.Location = new Point(0, 0);
      this.FlowLayoutPanel1.Margin = new Padding(6, 6, 6, 6);
      this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
      this.FlowLayoutPanel1.Size = new Size(994, 488);
      this.FlowLayoutPanel1.TabIndex = 9;
      this.AutoScaleDimensions = new SizeF(192f, 192f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.AutoSize = true;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(994, 488);
      this.ControlBox = false;
      this.Controls.Add((Control) this.FlowLayoutPanel1);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(3, 3);
      this.Margin = new Padding(6, 6, 6, 6);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSplash);
      this.RightToLeft = RightToLeft.No;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.fraMainFrame.ResumeLayout(false);
      this.fraMainFrame.PerformLayout();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      ((ISupportInitialize) this.Image1).EndInit();
      ((ISupportInitialize) this.imgAboutBuilder).EndInit();
      this.FlowLayoutPanel1.ResumeLayout(false);
      this.FlowLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual FlowLayoutPanel FlowLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    static frmSplash()
    {
      RadTypeResolver.Instance.ResolveTypesInCurrentAssembly = true;
    }

    public frmSplash()
    {
      this.Load += new EventHandler(this.frmSplash_Load);
      this.InitializeComponent();
      ThemeResolutionService.LoadPackageResource("BuilderRED.CustomTelerikAll.tssp");
      ThemeResolutionService.ApplicationThemeName = "CustomTelerikAll";
    }

    private void frmSplash_Load(object eventSender, EventArgs eventArgs)
    {
      this.lblVersion.Text = "Version " + Conversions.ToString(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMajorPart) + "." + Conversions.ToString(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMinorPart) + "." + Conversions.ToString(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileBuildPart);
      this.lblProductName.Text = Assembly.GetExecutingAssembly().GetName().Name;
    }
  }
}
