// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmEditLocation
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
  public class frmEditLocation : Form
  {
    private IContainer components;
    private const string MOD_NAME = "frmEditLocation";
    private string m_strOrigName;
    private string m_strName;

    public frmEditLocation()
    {
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    internal virtual Button cmdOK
    {
      get
      {
        return this._cmdOK;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdOK_Click);
        Button cmdOk1 = this._cmdOK;
        if (cmdOk1 != null)
          cmdOk1.Click -= eventHandler;
        this._cmdOK = value;
        Button cmdOk2 = this._cmdOK;
        if (cmdOk2 == null)
          return;
        cmdOk2.Click += eventHandler;
      }
    }

    internal virtual Button cmdCancel
    {
      get
      {
        return this._cmdCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCancel_Click);
        Button cmdCancel1 = this._cmdCancel;
        if (cmdCancel1 != null)
          cmdCancel1.Click -= eventHandler;
        this._cmdCancel = value;
        Button cmdCancel2 = this._cmdCancel;
        if (cmdCancel2 == null)
          return;
        cmdCancel2.Click += eventHandler;
      }
    }

    internal virtual TextBox txtName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Label lblInstruct { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmEditLocation));
      this.cmdOK = new Button();
      this.cmdCancel = new Button();
      this.txtName = new TextBox();
      this.lblName = new Label();
      this.lblInstruct = new Label();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.cmdOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.cmdOK.DialogResult = DialogResult.OK;
      this.cmdOK.Location = new Point(3, 90);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new Size(60, 24);
      this.cmdOK.TabIndex = 0;
      this.cmdOK.Text = "OK";
      this.cmdCancel.DialogResult = DialogResult.Cancel;
      this.cmdCancel.Location = new Point(69, 90);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new Size(60, 24);
      this.cmdCancel.TabIndex = 1;
      this.cmdCancel.Text = "Cancel";
      this.txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtName.Location = new Point(69, 64);
      this.txtName.Name = "txtName";
      this.txtName.Size = new Size(250, 20);
      this.txtName.TabIndex = 2;
      this.lblName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblName.AutoSize = true;
      this.lblName.Location = new Point(3, 61);
      this.lblName.Name = "lblName";
      this.lblName.Size = new Size(60, 13);
      this.lblName.TabIndex = 3;
      this.lblName.Text = "Edit Name:";
      this.lblName.TextAlign = ContentAlignment.MiddleRight;
      this.lblInstruct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.lblInstruct.AutoSize = true;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.lblInstruct, 2);
      this.lblInstruct.Location = new Point(3, 0);
      this.lblInstruct.Name = "lblInstruct";
      this.lblInstruct.Size = new Size(316, 52);
      this.lblInstruct.TabIndex = 4;
      this.lblInstruct.Text = componentResourceManager.GetString("lblInstruct.Text");
      this.TableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Controls.Add((Control) this.lblInstruct, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdOK, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.txtName, 1, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblName, 0, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.cmdCancel, 1, 2);
      this.TableLayoutPanel1.Location = new Point(12, 12);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(322, 117);
      this.TableLayoutPanel1.TabIndex = 5;
      this.AcceptButton = (IButtonControl) this.cmdOK;
      this.AutoScaleBaseSize = new Size(5, 13);
      this.AutoSize = true;
      this.CancelButton = (IButtonControl) this.cmdCancel;
      this.ClientSize = new Size(346, 141);
      this.ControlBox = false;
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmEditLocation);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Edit Location Name";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public string NewLocationName(string strCurrentName)
    {
      string strName;
      if ((uint) Operators.CompareString(strCurrentName, "", false) > 0U)
      {
        this.m_strOrigName = strCurrentName;
        this.txtName.Text = this.m_strOrigName;
        int num = (int) this.ShowDialog();
        strName = this.m_strName;
      }
      return strName;
    }

    private void cmdOK_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.NewNameOK())
        {
          this.m_strName = this.txtName.Text;
          this.Close();
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "The new name conflicts with an existing location name for this building.  Please select another name.", MsgBoxStyle.OkOnly, (object) "Problem Encountered");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmEditLocation), "OKButton_Click");
        ProjectData.ClearProjectError();
      }
    }

    private bool NewNameOK()
    {
      bool flag;
      try
      {
        if (Operators.CompareString(Strings.UCase(this.txtName.Text), Strings.UCase(this.m_strOrigName), false) == 0)
        {
          flag = true;
        }
        else
        {
          if ((uint) Operators.CompareString(this.txtName.Text, this.m_strOrigName, false) > 0U)
          {
            if (Operators.ConditionalCompareObjectEqual(mdUtility.DB.GetDataTable("SELECT Count(Name) FROM Sample_Location where Name = '" + Strings.Replace(this.txtName.Text, "'", "''", 1, -1, CompareMethod.Binary) + "' and Building_ID = '" + Strings.Replace(mdUtility.fMainForm.CurrentBldg, "'", "''", 1, -1, CompareMethod.Binary) + "'").Rows[0][0], (object) 0, false))
            {
              flag = true;
              goto label_8;
            }
          }
          flag = false;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmEditLocation), nameof (NewNameOK));
        flag = false;
        ProjectData.ClearProjectError();
      }
label_8:
      return flag;
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
      this.m_strName = "";
      this.Close();
    }
  }
}
