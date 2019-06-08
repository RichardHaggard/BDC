// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmCriteria
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmCriteria : Form
  {
    private IContainer components;
    private const string MOD_NAME = "frmCriteria";
    private string m_strSampSubCompID;
    private int m_lSubCompLink;
    public DataTable dtCriteria;
    private DataView dvCriteria;
    private bool m_bEdit;
    private bool m_bLoaded;
    private bool m_bSave;

    public frmCriteria()
    {
      this.Load += new EventHandler(this.frmCriteria_Load);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmCriteria));
      this.CritRadGridView = new RadGridView();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.CritListView = new ListView();
      this.critCloseButton = new Button();
      this.CritRadGridView.BeginInit();
      this.CritRadGridView.MasterTemplate.BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.CritRadGridView.BackColor = SystemColors.GradientActiveCaption;
      this.CritRadGridView.Location = new Point(0, 0);
      this.CritRadGridView.Name = "CritRadGridView";
      this.CritRadGridView.RootElement.AccessibleDescription = (string) null;
      this.CritRadGridView.RootElement.AccessibleName = (string) null;
      this.CritRadGridView.RootElement.ControlBounds = new Rectangle(0, 0, 240, 150);
      this.CritRadGridView.TabIndex = 0;
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.Controls.Add((Control) this.CritListView, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.critCloseButton, 0, 1);
      this.TableLayoutPanel1.Location = new Point(2, 14);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 77.48917f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.51082f));
      this.TableLayoutPanel1.Size = new Size(302, 260);
      this.TableLayoutPanel1.TabIndex = 0;
      this.CritListView.BackColor = Color.White;
      this.CritListView.BorderStyle = BorderStyle.FixedSingle;
      this.CritListView.CheckBoxes = true;
      this.CritListView.Dock = DockStyle.Fill;
      this.CritListView.Location = new Point(3, 3);
      this.CritListView.Name = "CritListView";
      this.CritListView.Size = new Size(296, 195);
      this.CritListView.TabIndex = 0;
      this.CritListView.UseCompatibleStateImageBehavior = false;
      this.critCloseButton.Anchor = AnchorStyles.None;
      this.critCloseButton.BackColor = SystemColors.ControlLight;
      this.critCloseButton.FlatStyle = FlatStyle.System;
      this.critCloseButton.Font = new Font("Microsoft Sans Serif", 10.2f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.critCloseButton.Location = new Point(68, 214);
      this.critCloseButton.Name = "critCloseButton";
      this.critCloseButton.Size = new Size(166, 33);
      this.critCloseButton.TabIndex = 1;
      this.critCloseButton.Text = "Submit";
      this.critCloseButton.UseVisualStyleBackColor = false;
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(317, 287);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.ForeColor = SystemColors.ActiveCaptionText;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmCriteria);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Criteria List";
      this.CritRadGridView.MasterTemplate.EndInit();
      this.CritRadGridView.EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private virtual RadGridView CritRadGridView { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel TableLayoutPanel1
    {
      get
      {
        return this._TableLayoutPanel1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        PaintEventHandler paintEventHandler = new PaintEventHandler(this.TableLayoutPanel1_Paint);
        TableLayoutPanel tableLayoutPanel1_1 = this._TableLayoutPanel1;
        if (tableLayoutPanel1_1 != null)
          tableLayoutPanel1_1.Paint -= paintEventHandler;
        this._TableLayoutPanel1 = value;
        TableLayoutPanel tableLayoutPanel1_2 = this._TableLayoutPanel1;
        if (tableLayoutPanel1_2 == null)
          return;
        tableLayoutPanel1_2.Paint += paintEventHandler;
      }
    }

    internal virtual ListView CritListView { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button critCloseButton
    {
      get
      {
        return this._critCloseButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.CloseButton_Click);
        Button critCloseButton1 = this._critCloseButton;
        if (critCloseButton1 != null)
          critCloseButton1.Click -= eventHandler;
        this._critCloseButton = value;
        Button critCloseButton2 = this._critCloseButton;
        if (critCloseButton2 == null)
          return;
        critCloseButton2.Click += eventHandler;
      }
    }

    public bool CanEdit
    {
      get
      {
        return this.m_bEdit;
      }
      set
      {
        this.m_bEdit = value;
      }
    }

    public string SubCompLink
    {
      set
      {
        this.m_lSubCompLink = Conversions.ToInteger(value);
      }
      get
      {
        return Conversions.ToString(this.m_lSubCompLink);
      }
    }

    public string SampSubCompID
    {
      set
      {
        this.m_strSampSubCompID = value;
      }
      get
      {
        return this.m_strSampSubCompID;
      }
    }

    private void RefreshData()
    {
      try
      {
        this.m_bLoaded = false;
        this.dvCriteria = new DataView(this.dtCriteria);
        this.CritListView.View = View.List;
        try
        {
          foreach (DataRow row1 in this.dtCriteria.Rows)
          {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = Conversions.ToString(row1["Criteria"]);
            DataRow dataRow;
            object[] objArray;
            bool[] flagArray;
            NewLateBinding.LateCall((object) listViewItem.SubItems, (System.Type) null, "Add", objArray = new object[1]
            {
              (dataRow = row1)["ID"]
            }, (string[]) null, (System.Type[]) null, flagArray = new bool[1]
            {
              true
            }, true);
            if (flagArray[0])
              dataRow["ID"] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(objArray[0]));
            listViewItem.SubItems.Add(this.SubCompLink);
            if (mdUtility.get_MstrTable("SelectedSSCCrit") != null && mdUtility.get_MstrTable("SelectedSSCCrit").Rows.Count > 0)
            {
              try
              {
                foreach (DataRow row2 in mdUtility.get_MstrTable("SelectedSSCCrit").Rows)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row2["SSC_CRITERION_LINK"], row1["ID"], false))
                  {
                    listViewItem.Checked = true;
                    break;
                  }
                }
              }
              finally
              {
                IEnumerator enumerator;
                if (enumerator is IDisposable)
                  (enumerator as IDisposable).Dispose();
              }
            }
            if (mdUtility.get_MstrTable("SelectedCrit") != null && mdUtility.get_MstrTable("SelectedCrit").Rows.Count > 0)
            {
              try
              {
                foreach (DataRow row2 in mdUtility.get_MstrTable("SelectedCrit").Rows)
                {
                  if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(row2["ID"], (object) listViewItem.SubItems[1].Text, false), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(row2["subcomplink"], (object) listViewItem.SubItems[2].Text, false)), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(row2["CritStatus"], (object) "N", false))))
                  {
                    listViewItem.Checked = true;
                    break;
                  }
                }
              }
              finally
              {
                IEnumerator enumerator;
                if (enumerator is IDisposable)
                  (enumerator as IDisposable).Dispose();
              }
            }
            this.CritListView.Items.Add(listViewItem);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.CritListView.Visible = true;
        this.FormatGrid();
        this.m_bLoaded = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmCriteria), nameof (RefreshData));
        ProjectData.ClearProjectError();
      }
    }

    private void CloseButton_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (mdUtility.get_MstrTable("SelectedCrit") != null && mdUtility.get_MstrTable("SelectedCrit").Rows.Count > 0)
        {
          try
          {
            foreach (DataRow row in mdUtility.get_MstrTable("SelectedCrit").Rows)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["subcomplink"], (object) this.SubCompLink, false))
                row["CritStatus"] = (object) "D";
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          mdUtility.get_MstrTable("SelectedCrit").AcceptChanges();
        }
        else
          mdUtility.LoadMstrTable("SelectedCrit", "SELECT 'ID' as ID, 'Criterion' as Criterion, 'subcomplink' as subcomplink, 'CritStatus' as CritStatus, 'SampSubCompID' as SampSubCompID");
        DataTable dataTable = mdUtility.get_MstrTable("SelectedCrit");
        ListView.CheckedListViewItemCollection checkedItems = this.CritListView.CheckedItems;
        try
        {
          foreach (ListViewItem listViewItem in checkedItems)
          {
            DataRow row = dataTable.NewRow();
            row["ID"] = (object) listViewItem.SubItems[1].Text;
            row["Criterion"] = (object) listViewItem.Text;
            row["subcomplink"] = (object) listViewItem.SubItems[2].Text;
            row["CritStatus"] = (object) "N";
            row["SampSubCompID"] = (object) this.SampSubCompID;
            dataTable.Rows.Add(row);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.Close();
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != -2147217842)
          mdUtility.Errorhandler(ex2, nameof (frmCriteria), nameof (CloseButton_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void frmCriteria_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.m_bLoaded = false;
        this.m_bLoaded = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmCriteria), "Form_Load");
        ProjectData.ClearProjectError();
      }
    }

    private void FormatGrid()
    {
    }

    private void SetDropDownValueItems(string strCol, string strLookUp, string strKey = "")
    {
    }

    private void MoveColumnToLeftMost(int iColumnIndex)
    {
    }

    private void SetEnabled()
    {
    }

    private void SetChanged()
    {
      if (!this.m_bLoaded)
        return;
      this.m_bSave = true;
    }

    public bool EditCriteria(ref int iCriteriaCount)
    {
      this.RefreshData();
      int num = (int) this.ShowDialog();
      bool bSave = this.m_bSave;
      iCriteriaCount = this.dvCriteria.Count;
      return bSave;
    }

    private void PositionESCLabel()
    {
    }

    private void rgDistresses_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
    {
    }

    private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
  }
}
