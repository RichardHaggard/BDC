// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmTaskList
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

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmTaskList : Form
  {
    private IContainer components;

    public frmTaskList()
    {
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
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      this.DataGridView1 = new DataGridView();
      this.taskCloseButton = new Button();
      ((ISupportInitialize) this.DataGridView1).BeginInit();
      this.SuspendLayout();
      this.DataGridView1.AllowUserToAddRows = false;
      this.DataGridView1.AllowUserToDeleteRows = false;
      this.DataGridView1.AllowUserToResizeColumns = false;
      this.DataGridView1.AllowUserToResizeRows = false;
      this.DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.DataGridView1.BackgroundColor = Color.White;
      this.DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle1.BackColor = Color.PowderBlue;
      gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle1.ForeColor = SystemColors.WindowText;
      gridViewCellStyle1.SelectionBackColor = Color.LightSteelBlue;
      gridViewCellStyle1.SelectionForeColor = SystemColors.ActiveCaptionText;
      gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      this.DataGridView1.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
      this.DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DataGridView1.Cursor = Cursors.IBeam;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = Color.PowderBlue;
      gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = SystemColors.ControlText;
      gridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
      gridViewCellStyle2.SelectionForeColor = SystemColors.ActiveCaptionText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      this.DataGridView1.DefaultCellStyle = gridViewCellStyle2;
      this.DataGridView1.Dock = DockStyle.Top;
      this.DataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
      this.DataGridView1.GridColor = Color.White;
      this.DataGridView1.Location = new Point(0, 0);
      this.DataGridView1.MultiSelect = false;
      this.DataGridView1.Name = "DataGridView1";
      this.DataGridView1.ReadOnly = true;
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = Color.White;
      gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = SystemColors.WindowText;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Desktop;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.DataGridView1.RowHeadersDefaultCellStyle = gridViewCellStyle3;
      this.DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      gridViewCellStyle4.BackColor = Color.White;
      gridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 7.8f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle4.ForeColor = SystemColors.ActiveCaptionText;
      gridViewCellStyle4.SelectionBackColor = SystemColors.Desktop;
      gridViewCellStyle4.SelectionForeColor = Color.White;
      this.DataGridView1.RowsDefaultCellStyle = gridViewCellStyle4;
      this.DataGridView1.RowTemplate.Height = 24;
      this.DataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;
      this.DataGridView1.ShowCellErrors = false;
      this.DataGridView1.ShowCellToolTips = false;
      this.DataGridView1.ShowEditingIcon = false;
      this.DataGridView1.ShowRowErrors = false;
      this.DataGridView1.Size = new Size(559, 252);
      this.DataGridView1.TabIndex = 122;
      this.taskCloseButton.Location = new Point(196, 258);
      this.taskCloseButton.Name = "taskCloseButton";
      this.taskCloseButton.Size = new Size(186, 37);
      this.taskCloseButton.TabIndex = 123;
      this.taskCloseButton.Text = "Close";
      this.taskCloseButton.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(559, 307);
      this.ControlBox = false;
      this.Controls.Add((Control) this.taskCloseButton);
      this.Controls.Add((Control) this.DataGridView1);
      this.Name = nameof (frmTaskList);
      this.Text = "Task List";
      ((ISupportInitialize) this.DataGridView1).EndInit();
      this.ResumeLayout(false);
    }

    public virtual DataGridView DataGridView1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button taskCloseButton
    {
      get
      {
        return this._taskCloseButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.CloseButton_Click);
        Button taskCloseButton1 = this._taskCloseButton;
        if (taskCloseButton1 != null)
          taskCloseButton1.Click -= eventHandler;
        this._taskCloseButton = value;
        Button taskCloseButton2 = this._taskCloseButton;
        if (taskCloseButton2 == null)
          return;
        taskCloseButton2.Click += eventHandler;
      }
    }

    private void CloseButton_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }
  }
}
