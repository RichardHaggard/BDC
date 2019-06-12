// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmImages
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SMS.Libraries.Utility.BredPackage.Classes;
using SMS.Libraries.Utility.BredPackage.Classes.Containers;
using SMS.Libraries.Utility.BredPackage.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmImages : Form
  {
    private IContainer components;
    private Guid _invGuid;
    private Guid _siteGuid;
    private InventoryClass _inventoryClass;
    private string _bredPackageFileName;
    private ZipBredPackage _bredPackage;
    private bool _existingIsSelected;
    private List<IAttachmentInfo> _existingList;
    private List<IAttachmentInfo> _pendingList;

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
      this.cbExisting = new ComboBox();
      this.btnDeleteExisting = new Button();
      this.btnRemovePending = new Button();
      this.btnAddAllPending = new Button();
      this.pbImage = new PictureBox();
      this.lbPending = new ListBox();
      this.gbPending = new GroupBox();
      this.TableLayoutPanel4 = new TableLayoutPanel();
      this.btnAddNew = new Button();
      this.Label4 = new Label();
      this.Label3 = new Label();
      this.txtDescription = new TextBox();
      this.txtTitle = new TextBox();
      this.gpExisting = new GroupBox();
      this.TableLayoutPanel5 = new TableLayoutPanel();
      this.gbDetails = new GroupBox();
      this.TableLayoutPanel2 = new TableLayoutPanel();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.TableLayoutPanel3 = new TableLayoutPanel();
      ((ISupportInitialize) this.pbImage).BeginInit();
      this.gbPending.SuspendLayout();
      this.TableLayoutPanel4.SuspendLayout();
      this.gpExisting.SuspendLayout();
      this.TableLayoutPanel5.SuspendLayout();
      this.gbDetails.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.TableLayoutPanel3.SuspendLayout();
      this.SuspendLayout();
      this.cbExisting.Dock = DockStyle.Fill;
      this.cbExisting.FormattingEnabled = true;
      this.cbExisting.Location = new Point(3, 3);
      this.cbExisting.Name = "cbExisting";
      this.cbExisting.Size = new Size(220, 21);
      this.cbExisting.TabIndex = 0;
      this.btnDeleteExisting.AutoSize = true;
      this.btnDeleteExisting.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Delete;
      this.btnDeleteExisting.Location = new Point(3, 30);
      this.btnDeleteExisting.Name = "btnDeleteExisting";
      this.btnDeleteExisting.Size = new Size(161, 23);
      this.btnDeleteExisting.TabIndex = 4;
      this.btnDeleteExisting.Text = "Delete Image from Existing";
      this.btnDeleteExisting.TextAlign = ContentAlignment.MiddleRight;
      this.btnDeleteExisting.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btnDeleteExisting.UseVisualStyleBackColor = true;
      this.btnRemovePending.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Delete;
      this.btnRemovePending.Location = new Point(118, 329);
      this.btnRemovePending.Name = "btnRemovePending";
      this.btnRemovePending.Size = new Size(105, 23);
      this.btnRemovePending.TabIndex = 5;
      this.btnRemovePending.Text = "Selected Only";
      this.btnRemovePending.TextAlign = ContentAlignment.MiddleRight;
      this.btnRemovePending.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btnRemovePending.UseVisualStyleBackColor = true;
      this.btnAddAllPending.Dock = DockStyle.Right;
      this.btnAddAllPending.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Check;
      this.btnAddAllPending.Location = new Point(7, 329);
      this.btnAddAllPending.Name = "btnAddAllPending";
      this.btnAddAllPending.Size = new Size(105, 23);
      this.btnAddAllPending.TabIndex = 6;
      this.btnAddAllPending.Text = "All Pending";
      this.btnAddAllPending.TextAlign = ContentAlignment.MiddleRight;
      this.btnAddAllPending.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btnAddAllPending.UseVisualStyleBackColor = true;
      this.pbImage.Dock = DockStyle.Fill;
      this.pbImage.Location = new Point(3, 3);
      this.pbImage.Name = "pbImage";
      this.pbImage.Size = new Size(227, 218);
      this.pbImage.SizeMode = PictureBoxSizeMode.Zoom;
      this.pbImage.TabIndex = 10;
      this.pbImage.TabStop = false;
      this.TableLayoutPanel4.SetColumnSpan((Control) this.lbPending, 2);
      this.lbPending.Dock = DockStyle.Fill;
      this.lbPending.FormattingEnabled = true;
      this.lbPending.Location = new Point(3, 32);
      this.lbPending.Name = "lbPending";
      this.lbPending.SelectionMode = SelectionMode.MultiExtended;
      this.lbPending.Size = new Size(220, 291);
      this.lbPending.TabIndex = 1;
      this.gbPending.AutoSize = true;
      this.gbPending.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.gbPending.Controls.Add((Control) this.TableLayoutPanel4);
      this.gbPending.Dock = DockStyle.Fill;
      this.gbPending.Location = new Point(3, 3);
      this.gbPending.MinimumSize = new Size(104, 250);
      this.gbPending.Name = "gbPending";
      this.gbPending.Size = new Size(232, 374);
      this.gbPending.TabIndex = 15;
      this.gbPending.TabStop = false;
      this.gbPending.Text = "Pending";
      this.TableLayoutPanel4.ColumnCount = 2;
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel4.Controls.Add((Control) this.lbPending, 0, 1);
      this.TableLayoutPanel4.Controls.Add((Control) this.btnAddAllPending, 0, 2);
      this.TableLayoutPanel4.Controls.Add((Control) this.btnAddNew, 0, 0);
      this.TableLayoutPanel4.Controls.Add((Control) this.btnRemovePending, 1, 2);
      this.TableLayoutPanel4.Dock = DockStyle.Fill;
      this.TableLayoutPanel4.Location = new Point(3, 16);
      this.TableLayoutPanel4.Name = "TableLayoutPanel4";
      this.TableLayoutPanel4.RowCount = 2;
      this.TableLayoutPanel4.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel4.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel4.Size = new Size(226, 355);
      this.TableLayoutPanel4.TabIndex = 10;
      this.btnAddNew.AutoSize = true;
      this.TableLayoutPanel4.SetColumnSpan((Control) this.btnAddNew, 2);
      this.btnAddNew.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
      this.btnAddNew.Location = new Point(3, 3);
      this.btnAddNew.Name = "btnAddNew";
      this.btnAddNew.Size = new Size(163, 23);
      this.btnAddNew.TabIndex = 9;
      this.btnAddNew.Text = "Add New Image to Pending";
      this.btnAddNew.TextAlign = ContentAlignment.MiddleRight;
      this.btnAddNew.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.btnAddNew.UseVisualStyleBackColor = true;
      this.Label4.AutoSize = true;
      this.Label4.Location = new Point(3, 263);
      this.Label4.Name = "Label4";
      this.Label4.Size = new Size(60, 13);
      this.Label4.TabIndex = 14;
      this.Label4.Text = "Description";
      this.Label3.AutoSize = true;
      this.Label3.Location = new Point(3, 224);
      this.Label3.Name = "Label3";
      this.Label3.Size = new Size(27, 13);
      this.Label3.TabIndex = 13;
      this.Label3.Text = "Title";
      this.txtDescription.Dock = DockStyle.Fill;
      this.txtDescription.Location = new Point(3, 279);
      this.txtDescription.MinimumSize = new Size(250, 160);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new Size(250, 160);
      this.txtDescription.TabIndex = 12;
      this.txtTitle.Dock = DockStyle.Fill;
      this.txtTitle.Location = new Point(3, 240);
      this.txtTitle.MaxLength = 50;
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.Size = new Size(227, 20);
      this.txtTitle.TabIndex = 11;
      this.gpExisting.AutoSize = true;
      this.gpExisting.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.gpExisting.Controls.Add((Control) this.TableLayoutPanel5);
      this.gpExisting.Dock = DockStyle.Fill;
      this.gpExisting.Location = new Point(3, 383);
      this.gpExisting.Name = "gpExisting";
      this.gpExisting.Size = new Size(232, 75);
      this.gpExisting.TabIndex = 16;
      this.gpExisting.TabStop = false;
      this.gpExisting.Text = "Existing";
      this.TableLayoutPanel5.ColumnCount = 1;
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
      this.TableLayoutPanel5.Controls.Add((Control) this.cbExisting, 0, 0);
      this.TableLayoutPanel5.Controls.Add((Control) this.btnDeleteExisting, 0, 1);
      this.TableLayoutPanel5.Dock = DockStyle.Fill;
      this.TableLayoutPanel5.Location = new Point(3, 16);
      this.TableLayoutPanel5.Name = "TableLayoutPanel5";
      this.TableLayoutPanel5.RowCount = 2;
      this.TableLayoutPanel5.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel5.Size = new Size(226, 56);
      this.TableLayoutPanel5.TabIndex = 5;
      this.gbDetails.AutoSize = true;
      this.gbDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.gbDetails.Controls.Add((Control) this.TableLayoutPanel2);
      this.gbDetails.Dock = DockStyle.Fill;
      this.gbDetails.Location = new Point(247, 3);
      this.gbDetails.Name = "gbDetails";
      this.gbDetails.Size = new Size(239, 461);
      this.gbDetails.TabIndex = 17;
      this.gbDetails.TabStop = false;
      this.gbDetails.Text = "Details";
      this.gbDetails.Visible = false;
      this.TableLayoutPanel2.ColumnCount = 1;
      this.TableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel2.Controls.Add((Control) this.pbImage, 0, 0);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtDescription, 0, 4);
      this.TableLayoutPanel2.Controls.Add((Control) this.txtTitle, 0, 2);
      this.TableLayoutPanel2.Controls.Add((Control) this.Label4, 0, 3);
      this.TableLayoutPanel2.Controls.Add((Control) this.Label3, 0, 1);
      this.TableLayoutPanel2.Dock = DockStyle.Fill;
      this.TableLayoutPanel2.Location = new Point(3, 16);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 5;
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel2.Size = new Size(233, 442);
      this.TableLayoutPanel2.TabIndex = 15;
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.Controls.Add((Control) this.TableLayoutPanel3, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.gbDetails, 1, 0);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 1;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Size = new Size(489, 467);
      this.TableLayoutPanel1.TabIndex = 18;
      this.TableLayoutPanel3.AutoSize = true;
      this.TableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel3.ColumnCount = 1;
      this.TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel3.Controls.Add((Control) this.gpExisting, 0, 1);
      this.TableLayoutPanel3.Controls.Add((Control) this.gbPending, 0, 0);
      this.TableLayoutPanel3.Dock = DockStyle.Fill;
      this.TableLayoutPanel3.Location = new Point(3, 3);
      this.TableLayoutPanel3.Name = "TableLayoutPanel3";
      this.TableLayoutPanel3.RowCount = 2;
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 81f));
      this.TableLayoutPanel3.Size = new Size(238, 461);
      this.TableLayoutPanel3.TabIndex = 19;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(489, 467);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.MinimumSize = new Size(505, 505);
      this.Name = nameof (frmImages);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Images";
      ((ISupportInitialize) this.pbImage).EndInit();
      this.gbPending.ResumeLayout(false);
      this.TableLayoutPanel4.ResumeLayout(false);
      this.TableLayoutPanel4.PerformLayout();
      this.gpExisting.ResumeLayout(false);
      this.TableLayoutPanel5.ResumeLayout(false);
      this.TableLayoutPanel5.PerformLayout();
      this.gbDetails.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.TableLayoutPanel3.ResumeLayout(false);
      this.TableLayoutPanel3.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual ComboBox cbExisting
    {
      get
      {
        return this._cbExisting;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cbExisting_SelectedIndexChanged);
        ComboBox cbExisting1 = this._cbExisting;
        if (cbExisting1 != null)
          cbExisting1.SelectedIndexChanged -= eventHandler;
        this._cbExisting = value;
        ComboBox cbExisting2 = this._cbExisting;
        if (cbExisting2 == null)
          return;
        cbExisting2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual Button btnDeleteExisting
    {
      get
      {
        return this._btnDeleteExisting;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnDeleteExisting_Click);
        Button btnDeleteExisting1 = this._btnDeleteExisting;
        if (btnDeleteExisting1 != null)
          btnDeleteExisting1.Click -= eventHandler;
        this._btnDeleteExisting = value;
        Button btnDeleteExisting2 = this._btnDeleteExisting;
        if (btnDeleteExisting2 == null)
          return;
        btnDeleteExisting2.Click += eventHandler;
      }
    }

    internal virtual Button btnRemovePending
    {
      get
      {
        return this._btnRemovePending;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnRemovePending_Click);
        Button btnRemovePending1 = this._btnRemovePending;
        if (btnRemovePending1 != null)
          btnRemovePending1.Click -= eventHandler;
        this._btnRemovePending = value;
        Button btnRemovePending2 = this._btnRemovePending;
        if (btnRemovePending2 == null)
          return;
        btnRemovePending2.Click += eventHandler;
      }
    }

    internal virtual Button btnAddAllPending
    {
      get
      {
        return this._btnAddAllPending;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnAddAllPending_Click);
        Button btnAddAllPending1 = this._btnAddAllPending;
        if (btnAddAllPending1 != null)
          btnAddAllPending1.Click -= eventHandler;
        this._btnAddAllPending = value;
        Button btnAddAllPending2 = this._btnAddAllPending;
        if (btnAddAllPending2 == null)
          return;
        btnAddAllPending2.Click += eventHandler;
      }
    }

    internal virtual PictureBox pbImage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ListBox lbPending
    {
      get
      {
        return this._lbPending;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.lbPending_SelectedIndexChanged);
        ListBox lbPending1 = this._lbPending;
        if (lbPending1 != null)
          lbPending1.SelectedIndexChanged -= eventHandler;
        this._lbPending = value;
        ListBox lbPending2 = this._lbPending;
        if (lbPending2 == null)
          return;
        lbPending2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual GroupBox gbPending { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TextBox txtDescription
    {
      get
      {
        return this._txtDescription;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtDescription_TextChanged);
        TextBox txtDescription1 = this._txtDescription;
        if (txtDescription1 != null)
          txtDescription1.LostFocus -= eventHandler;
        this._txtDescription = value;
        TextBox txtDescription2 = this._txtDescription;
        if (txtDescription2 == null)
          return;
        txtDescription2.LostFocus += eventHandler;
      }
    }

    internal virtual TextBox txtTitle
    {
      get
      {
        return this._txtTitle;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.txtTitle_TextChanged);
        TextBox txtTitle1 = this._txtTitle;
        if (txtTitle1 != null)
          txtTitle1.LostFocus -= eventHandler;
        this._txtTitle = value;
        TextBox txtTitle2 = this._txtTitle;
        if (txtTitle2 == null)
          return;
        txtTitle2.LostFocus += eventHandler;
      }
    }

    internal virtual GroupBox gpExisting { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox gbDetails { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnAddNew
    {
      get
      {
        return this._btnAddNew;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnAddNew_Click);
        Button btnAddNew1 = this._btnAddNew;
        if (btnAddNew1 != null)
          btnAddNew1.Click -= eventHandler;
        this._btnAddNew = value;
        Button btnAddNew2 = this._btnAddNew;
        if (btnAddNew2 == null)
          return;
        btnAddNew2.Click += eventHandler;
      }
    }

    public frmImages(Guid siteID, Guid inventoryID, InventoryClass ic, string bredPackageFileName)
    {
      this.DragEnter += new DragEventHandler(this.frmImages_DragEnter);
      this.DragDrop += new DragEventHandler(this.frmImages_DragDrop);
      this._existingIsSelected = false;
      this.InitializeComponent();
      this._siteGuid = siteID;
      this._invGuid = inventoryID;
      this._inventoryClass = ic;
      this._bredPackageFileName = bredPackageFileName;
      if (this._bredPackage == null)
      {
        this._bredPackage = new ZipBredPackage(this._bredPackageFileName, false);
        this._bredPackage.LoadManifest();
      }
      this._pendingList = new List<IAttachmentInfo>();
      this.lbPending.DataSource = (object) this._pendingList;
      this.lbPending.DisplayMember = "FileName";
      this.RefreshControls();
    }

    public void RefreshControls()
    {
      this._existingList = this._bredPackage.ImageManifest.Where<IAttachmentInfo>((Func<IAttachmentInfo, bool>) (i => i.InventoryId == this._invGuid & i.InventoryClass == this._inventoryClass)).ToList<IAttachmentInfo>();
      this.cbExisting.DataSource = (object) new List<IAttachmentInfo>((IEnumerable<IAttachmentInfo>) this._existingList);
      if (this._existingList.Count == 0)
        this.cbExisting.Text = "";
      this.cbExisting.DisplayMember = "Title";
      this.cbExisting.Refresh();
      this.lbPending.DataSource = (object) new List<IAttachmentInfo>((IEnumerable<IAttachmentInfo>) this._pendingList);
      this.lbPending.DisplayMember = "Title";
      this.lbPending.Refresh();
    }

    public void LoadPendingFiles(string[] files)
    {
      string[] strArray = files;
      int index = 0;
      while (index < strArray.Length)
      {
        string str = strArray[index];
        if (this.isValidImageFileName(str))
          this.addToPending(str);
        checked { ++index; }
      }
      this.RefreshControls();
    }

    private bool FilenameContainsInvalidCharacters(string fileName)
    {
      return new Regex("[`~!@#\\$%\\^&\\*\\\\]").IsMatch(fileName);
    }

    private bool isValidImageFileName(string filePath)
    {
      if (this.FilenameContainsInvalidCharacters(Path.GetFileName(filePath)))
        return false;
      return new List<string>() { "jpg", "jpeg", "jpe", "jfif", "png", "gif" }.Contains(Path.GetExtension(filePath).TrimStart('.').ToLower());
    }

    private void btnAddNew_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Multiselect = true;
      openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
      OpenFileDialog openFileDialog2 = openFileDialog1;
      if (openFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      string[] fileNames = openFileDialog2.FileNames;
      int index = 0;
      while (index < fileNames.Length)
      {
        this.addToPending(fileNames[index]);
        checked { ++index; }
      }
      this.RefreshControls();
    }

    public void addToPending(string fileName)
    {
      if (!this.isValidImageFileName(fileName))
      {
        int num = (int) Interaction.MsgBox((object) string.Format("Invalid image format or file name: {0}", (object) fileName), MsgBoxStyle.Critical, (object) null);
      }
      else
      {
        string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
        withoutExtension.Substring(0, Math.Min(50, withoutExtension.Length));
        this._pendingList.Add((IAttachmentInfo) new CsvAttachmentInfo()
        {
          SiteId = this._siteGuid,
          InventoryId = this._invGuid,
          InventoryClass = this._inventoryClass,
          FileName = fileName,
          Title = withoutExtension,
          Description = ("Added On " + DateTime.UtcNow.ToShortDateString())
        });
      }
    }

    public string getStringFromEnum(InventoryClass ic)
    {
      return Enum.GetName(ic.GetType(), (object) ic);
    }

    private void btnRemovePending_Click(object sender, EventArgs e)
    {
      try
      {
        foreach (IAttachmentInfo selectedItem in this.lbPending.SelectedItems)
          this._pendingList.Remove(selectedItem);
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      this.RefreshControls();
      this.gbDetails.Visible = false;
    }

    private void lbPending_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.gbDetails.Visible = false;
      IAttachmentInfo selectedItem = this.lbPending.SelectedItem as IAttachmentInfo;
      if (selectedItem == null)
        return;
      this.txtTitle.Text = selectedItem.Title;
      this.txtDescription.Text = selectedItem.Description;
      Image image = Image.FromFile(selectedItem.FileName);
      int thumbWidth = checked ((int) Math.Round(Math.Min(240.0, Math.Max(180.0, unchecked ((double) checked (180 * image.Width) / (double) image.Height)))));
      int thumbHeight = checked ((int) Math.Round(Math.Min(180.0, unchecked ((double) checked (240 * image.Height) / (double) image.Width))));
      this.pbImage.Image = image.GetThumbnailImage(thumbWidth, thumbHeight, new Image.GetThumbnailImageAbort(this.ThumbnailCallback), IntPtr.Zero);
      image.Dispose();
      this.gbDetails.Visible = true;
      this._existingIsSelected = false;
    }

    private void frmImages_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }

    private void frmImages_DragDrop(object sender, DragEventArgs e)
    {
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
      int index = 0;
      while (index < data.Length)
      {
        this.addToPending(data[index]);
        checked { ++index; }
      }
      this.RefreshControls();
    }

    public void MoveFromPendingToExisting(IAttachmentInfo pendingAttachmentInfo)
    {
      IAttachmentInfo attachmentInfo = this._bredPackage.CreateAttachmentInfo(this._siteGuid, this._invGuid, this._inventoryClass, pendingAttachmentInfo.FileName, new Guid?());
      attachmentInfo.Title = pendingAttachmentInfo.Title;
      attachmentInfo.Description = pendingAttachmentInfo.Description;
      this._bredPackage.UpdateAttachmentInfo(attachmentInfo);
      this._pendingList.Remove(this._pendingList.Find((Predicate<IAttachmentInfo>) (p => Operators.CompareString(p.FileName, pendingAttachmentInfo.FileName, false) == 0)));
    }

    private void cbExisting_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cbExisting.SelectedItem == null)
      {
        this.gbDetails.Visible = false;
      }
      else
      {
        this.gbDetails.Visible = true;
        IAttachmentInfo selectedItem = this.cbExisting.SelectedItem as IAttachmentInfo;
        if (selectedItem == null)
          return;
        IAttachmentInfo attachmentInfo = this._bredPackage.ImageManifest.Where<IAttachmentInfo>((Func<IAttachmentInfo, bool>) (im => im.AttachmentId == selectedItem.AttachmentId)).FirstOrDefault<IAttachmentInfo>();
        if (attachmentInfo == null)
          return;
        this.txtTitle.Text = attachmentInfo.Title;
        this.txtDescription.Text = attachmentInfo.Description;
        Image image;
        using (Stream stream = (Stream) new MemoryStream(this._bredPackage.RetrieveAttachmentData(attachmentInfo.AttachmentId)))
          image = Image.FromStream(stream);
        if (!Information.IsNothing((object) image))
        {
          int thumbWidth = checked ((int) Math.Round(Math.Min(240.0, unchecked ((double) checked (180 * image.Width) / (double) image.Height))));
          this.pbImage.Image = image.GetThumbnailImage(thumbWidth, 180, new Image.GetThumbnailImageAbort(this.ThumbnailCallback), IntPtr.Zero);
          image.Dispose();
        }
        this._existingIsSelected = true;
      }
    }

    private bool ThumbnailCallback()
    {
      return true;
    }

    private void btnAddAllPending_Click(object sender, EventArgs e)
    {
      if (this.lbPending.DataSource != null)
      {
        try
        {
          foreach (IAttachmentInfo pendingAttachmentInfo in (IEnumerable) this.lbPending.DataSource)
          {
            if (Microsoft.VisualBasic.Strings.Trim(pendingAttachmentInfo.Title).Length == 0)
            {
              int num = (int) Interaction.MsgBox((object) "Image title is required.", MsgBoxStyle.Critical, (object) null);
              return;
            }
            this.MoveFromPendingToExisting(pendingAttachmentInfo);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.RefreshControls();
        this.gbDetails.Visible = false;
      }
    }

    private void btnDeleteExisting_Click(object sender, EventArgs e)
    {
      IAttachmentInfo selectedItem = this.cbExisting.SelectedItem as IAttachmentInfo;
      if (selectedItem == null)
        return;
      IAttachmentInfo attachmentInfo = this._bredPackage.ImageManifest.Where<IAttachmentInfo>((Func<IAttachmentInfo, bool>) (im => im.AttachmentId == selectedItem.AttachmentId)).FirstOrDefault<IAttachmentInfo>();
      if (attachmentInfo == null)
        return;
      this._bredPackage.DeleteAttachmentInfo(attachmentInfo);
      this.RefreshControls();
      this.gbDetails.Visible = false;
    }

    private void txtTitle_TextChanged(object sender, EventArgs e)
    {
      if (this._existingIsSelected)
      {
        IAttachmentInfo selectedItem = this.cbExisting.SelectedItem as IAttachmentInfo;
        if (selectedItem == null)
          return;
        IAttachmentInfo attachmentInfo = this._bredPackage.ImageManifest.Where<IAttachmentInfo>((Func<IAttachmentInfo, bool>) (im => im.AttachmentId == selectedItem.AttachmentId)).FirstOrDefault<IAttachmentInfo>();
        if (attachmentInfo == null || (uint) Operators.CompareString(attachmentInfo.Title, this.txtTitle.Text, false) <= 0U)
          return;
        attachmentInfo.Title = this.txtTitle.Text;
        this._bredPackage.UpdateAttachmentInfo(attachmentInfo);
      }
      else
      {
        IAttachmentInfo selectedItem = this.lbPending.SelectedItem as IAttachmentInfo;
        if (selectedItem == null)
          return;
        selectedItem.Title = this.txtTitle.Text;
      }
    }

    private void txtDescription_TextChanged(object sender, EventArgs e)
    {
      if (this._existingIsSelected)
      {
        IAttachmentInfo selectedItem = this.cbExisting.SelectedItem as IAttachmentInfo;
        if (selectedItem == null)
          return;
        IAttachmentInfo attachmentInfo = this._bredPackage.ImageManifest.Where<IAttachmentInfo>((Func<IAttachmentInfo, bool>) (im => im.AttachmentId == selectedItem.AttachmentId)).FirstOrDefault<IAttachmentInfo>();
        if (attachmentInfo == null || (uint) Operators.CompareString(attachmentInfo.Description, this.txtDescription.Text, false) <= 0U)
          return;
        attachmentInfo.Description = this.txtDescription.Text;
        this._bredPackage.UpdateAttachmentInfo(attachmentInfo);
      }
      else
        (this.lbPending.SelectedItem as IAttachmentInfo).Description = this.txtDescription.Text;
    }
  }
}
