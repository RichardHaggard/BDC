// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmChooseInspector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  internal class frmChooseInspector : Form
  {
    private IContainer components;
    public ToolTip ToolTip1;
    private const string MOD_NAME = "frmChooseInspector";

    public frmChooseInspector()
    {
      this.Load += new EventHandler(this.frmChooseInspector_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual ComboBox cboInspectors
    {
      get
      {
        return this._cboInspectors;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboInspectors_TextChanged);
        EventHandler eventHandler2 = new EventHandler(this.cboInspectors_SelectedIndexChanged);
        ComboBox cboInspectors1 = this._cboInspectors;
        if (cboInspectors1 != null)
        {
          cboInspectors1.TextChanged -= eventHandler1;
          cboInspectors1.SelectedIndexChanged -= eventHandler2;
        }
        this._cboInspectors = value;
        ComboBox cboInspectors2 = this._cboInspectors;
        if (cboInspectors2 == null)
          return;
        cboInspectors2.TextChanged += eventHandler1;
        cboInspectors2.SelectedIndexChanged += eventHandler2;
      }
    }

    public virtual Button OKButton
    {
      get
      {
        return this._OKButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.OKButton_Click);
        Button okButton1 = this._OKButton;
        if (okButton1 != null)
          okButton1.Click -= eventHandler;
        this._OKButton = value;
        Button okButton2 = this._OKButton;
        if (okButton2 == null)
          return;
        okButton2.Click += eventHandler;
      }
    }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.ToolTip1 = new ToolTip(this.components);
      this.cboInspectors = new ComboBox();
      this.OKButton = new Button();
      this.HelpProvider = new HelpProvider();
      this.SuspendLayout();
      this.cboInspectors.Cursor = Cursors.Default;
      this.cboInspectors.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboInspectors.Location = new Point(8, 8);
      this.cboInspectors.Name = "cboInspectors";
      this.cboInspectors.RightToLeft = RightToLeft.No;
      this.cboInspectors.Size = new Size(289, 21);
      this.cboInspectors.TabIndex = 1;
      this.OKButton.Cursor = Cursors.Default;
      this.OKButton.Enabled = false;
      this.OKButton.FlatStyle = FlatStyle.System;
      this.OKButton.Location = new Point(304, 8);
      this.OKButton.Name = "OKButton";
      this.OKButton.RightToLeft = RightToLeft.No;
      this.OKButton.Size = new Size(81, 23);
      this.OKButton.TabIndex = 0;
      this.OKButton.Text = "OK";
      this.AutoScaleBaseSize = new Size(5, 13);
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(394, 36);
      this.ControlBox = false;
      this.Controls.Add((Control) this.cboInspectors);
      this.Controls.Add((Control) this.OKButton);
      this.Cursor = Cursors.Default;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Location = new Point(184, 250);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmChooseInspector);
      this.RightToLeft = RightToLeft.No;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Choose the Inspector for this session";
      this.TopMost = true;
      this.ResumeLayout(false);
    }

    private void cboInspectors_TextChanged(object eventSender, EventArgs eventArgs)
    {
      this.OKButton.Enabled = true;
    }

    private void cboInspectors_SelectedIndexChanged(object eventSender, EventArgs eventArgs)
    {
      this.OKButton.Enabled = true;
    }

    private void frmChooseInspector_Load(object eventSender, EventArgs eventArgs)
    {
      ArrayList arrayList = new ArrayList();
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.KeywordIndex);
      this.HelpProvider.SetHelpKeyword((Control) this, "Inspector");
      DataTable dataTable = (DataTable) null;
      int num1;
      while (true)
      {
        try
        {
          dataTable = mdUtility.DB.GetDataTable("SELECT * FROM [UserAccount] ORDER BY LastName, FirstName");
          break;
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          Thread.Sleep(50);
          checked { ++num1; }
          if (num1 > 100)
          {
            ProjectData.ClearProjectError();
            break;
          }
          ProjectData.ClearProjectError();
        }
      }
      if (num1 > 100)
      {
        int num2 = (int) Interaction.MsgBox((object) "Unable to locate inspector list in database.\rLoading of data may be incomplete. Please\rtry selecting the Inspections button again.", MsgBoxStyle.Critical, (object) "Problem encountered");
        this.Close();
      }
      else
      {
        try
        {
          this.cboInspectors.Items.Clear();
          int num2 = checked (dataTable.Rows.Count - 1);
          int index = 0;
          while (index <= num2)
          {
            arrayList.Add((object) new frmChooseInspector.ListBoxItem(dataTable.Rows[index]["User_ID"].ToString(), Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(dataTable.Rows[index]["LastName"], (object) ", "), dataTable.Rows[index]["FirstName"]))));
            checked { ++index; }
          }
          this.cboInspectors.ValueMember = "ID";
          this.cboInspectors.DisplayMember = "Name";
          this.cboInspectors.DataSource = (object) arrayList;
          this.cboInspectors.SelectedIndex = this.cboInspectors.Items.Count <= 1 ? 0 : 1;
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          mdUtility.Errorhandler(ex, nameof (frmChooseInspector), nameof (frmChooseInspector_Load));
          this.Close();
          ProjectData.ClearProjectError();
        }
      }
    }

    private void OKButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        mdUtility.strCurrentInspector = Conversions.ToString(this.cboInspectors.SelectedValue);
        mdUtility.fMainForm.tsslInspector.Text = "Current Inspector: " + this.cboInspectors.Text;
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM UserAccount where User_ID = {" + mdUtility.strCurrentInspector + "}");
        if (dataTable.Rows.Count > 0)
          mdUtility.Units = !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["UnitPreference"], (object) 1, false) ? mdUtility.SystemofMeasure.umMetric : mdUtility.SystemofMeasure.umEnglish;
        mdUtility.User = new mdUtility.UserAccount()
        {
          FirstName = Conversions.ToString(dataTable.Rows[0]["FirstName"]),
          LastName = Conversions.ToString(dataTable.Rows[0]["LastName"]),
          ID = Guid.Parse(dataTable.Rows[0]["User_ID"].ToString())
        };
        this.Close();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmChooseInspector), nameof (OKButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private class ListBoxItem
    {
      private string _Name;
      private string _ID;

      public ListBoxItem(string ID, string Name)
      {
        this._ID = ID;
        this._Name = Name;
      }

      public string ID
      {
        get
        {
          return this._ID;
        }
        set
        {
          this._ID = value;
        }
      }

      public string Name
      {
        get
        {
          return this._Name;
        }
        set
        {
          this._Name = value;
        }
      }
    }
  }
}
