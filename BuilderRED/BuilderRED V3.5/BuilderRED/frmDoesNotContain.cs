// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmDoesNotContain
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
  public class frmDoesNotContain : Form
  {
    private IContainer components;
    private int _currentCount;

    public frmDoesNotContain()
    {
      this.Load += new EventHandler(this.frmDoesNotContain_Load);
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
      ListViewItem listViewItem1 = new ListViewItem("Electrical");
      ListViewItem listViewItem2 = new ListViewItem("HVAC");
      ListViewItem listViewItem3 = new ListViewItem("Fire Supression");
      ListViewItem listViewItem4 = new ListViewItem("Plumbing");
      this.grbSystemsList = new GroupBox();
      this.lsvDoesNotContain = new ListView();
      this.btnUpdate = new Button();
      this.grbSystemsList.SuspendLayout();
      this.SuspendLayout();
      this.grbSystemsList.Controls.Add((Control) this.lsvDoesNotContain);
      this.grbSystemsList.Location = new Point(13, 13);
      this.grbSystemsList.Name = "grbSystemsList";
      this.grbSystemsList.Size = new Size(117, 138);
      this.grbSystemsList.TabIndex = 0;
      this.grbSystemsList.TabStop = false;
      this.grbSystemsList.Text = "Systems List";
      this.lsvDoesNotContain.CheckBoxes = true;
      this.lsvDoesNotContain.Dock = DockStyle.Fill;
      listViewItem1.StateImageIndex = 0;
      listViewItem2.StateImageIndex = 0;
      listViewItem3.StateImageIndex = 0;
      listViewItem4.StateImageIndex = 0;
      this.lsvDoesNotContain.Items.AddRange(new ListViewItem[4]
      {
        listViewItem1,
        listViewItem2,
        listViewItem3,
        listViewItem4
      });
      this.lsvDoesNotContain.LabelWrap = false;
      this.lsvDoesNotContain.Location = new Point(3, 16);
      this.lsvDoesNotContain.Name = "lsvDoesNotContain";
      this.lsvDoesNotContain.Size = new Size(111, 119);
      this.lsvDoesNotContain.TabIndex = 0;
      this.lsvDoesNotContain.UseCompatibleStateImageBehavior = false;
      this.lsvDoesNotContain.View = View.SmallIcon;
      this.btnUpdate.Location = new Point(38, 157);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new Size(69, 30);
      this.btnUpdate.TabIndex = 1;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(146, 199);
      this.Controls.Add((Control) this.btnUpdate);
      this.Controls.Add((Control) this.grbSystemsList);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmDoesNotContain);
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Does Not Contain";
      this.grbSystemsList.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal virtual GroupBox grbSystemsList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnUpdate
    {
      get
      {
        return this._btnUpdate;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnUpdate_Click);
        Button btnUpdate1 = this._btnUpdate;
        if (btnUpdate1 != null)
          btnUpdate1.Click -= eventHandler;
        this._btnUpdate = value;
        Button btnUpdate2 = this._btnUpdate;
        if (btnUpdate2 == null)
          return;
        btnUpdate2.Click += eventHandler;
      }
    }

    internal virtual ListView lsvDoesNotContain { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public int CurrentCount
    {
      get
      {
        return this._currentCount;
      }
      set
      {
        this._currentCount = value;
      }
    }

    private void frmDoesNotContain_Load(object eventSender, EventArgs eventArgs)
    {
      string currentDoesNotContain = mdUtility.fMainForm.CurrentDoesNotContain;
      if ((uint) Operators.CompareString(currentDoesNotContain, "0000", false) > 0U)
      {
        int num = checked (mdUtility.fMainForm.CurrentDoesNotContain.Length - 1);
        int startIndex = 0;
        while (startIndex <= num)
        {
          this.lsvDoesNotContain.Items[startIndex].Checked = Operators.CompareString(currentDoesNotContain.Substring(startIndex, 1), "1", false) == 0;
          checked { ++startIndex; }
        }
      }
      this.CurrentCount = this.lsvDoesNotContain.CheckedItems.Count;
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
      string Left = "";
      int num1 = checked (this.lsvDoesNotContain.Items.Count - 1);
      int index = 0;
      while (index <= num1)
      {
        Left = !this.lsvDoesNotContain.Items[index].Checked ? Left + "0" : Left + "1";
        checked { ++index; }
      }
      if ((uint) Operators.CompareString(Left, mdUtility.fMainForm.CurrentDoesNotContain, false) > 0U)
      {
        mdUtility.fMainForm.CurrentDoesNotContain = Left;
        try
        {
          Building.SaveBuilding(mdUtility.fMainForm.CurrentBldg);
          int num2 = (int) Interaction.MsgBox((object) "Update Sucessful!", MsgBoxStyle.Information, (object) null);
          this.Close();
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          mdUtility.Errorhandler(ex, this.Name, "Building Does Not Contain");
          ProjectData.ClearProjectError();
        }
      }
      else
      {
        int num3 = (int) Interaction.MsgBox((object) "No Changes to List", MsgBoxStyle.Information, (object) null);
      }
    }
  }
}
