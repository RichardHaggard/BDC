// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmWorkItems
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinTree;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmWorkItems : Form
  {
    private IContainer components;

    public frmWorkItems()
    {
      this.FormClosing += new FormClosingEventHandler(this.frmWorkItems_Closing);
      this.Load += new EventHandler(this.frmWorkItems_Load);
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
      GridViewComboBoxColumn viewComboBoxColumn1 = new GridViewComboBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn1 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn2 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn3 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn4 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn5 = new GridViewTextBoxColumn();
      GridViewComboBoxColumn viewComboBoxColumn2 = new GridViewComboBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn6 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn7 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn8 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn9 = new GridViewTextBoxColumn();
      GridViewTextBoxColumn viewTextBoxColumn10 = new GridViewTextBoxColumn();
      GridViewDateTimeColumn viewDateTimeColumn = new GridViewDateTimeColumn();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmWorkItems));
      this.dgvWorkItems = new RadGridView();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.btnStartInspection = new Button();
      this.dgvWorkItems.BeginInit();
      this.dgvWorkItems.MasterTemplate.BeginInit();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.dgvWorkItems.BackColor = SystemColors.Control;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.dgvWorkItems, 2);
      this.dgvWorkItems.Cursor = Cursors.Default;
      this.dgvWorkItems.Dock = DockStyle.Fill;
      this.dgvWorkItems.Font = new Font("Segoe UI", 8.25f);
      this.dgvWorkItems.ForeColor = SystemColors.ControlText;
      this.dgvWorkItems.ImeMode = ImeMode.NoControl;
      this.dgvWorkItems.Location = new Point(3, 3);
      this.dgvWorkItems.MasterTemplate.AllowAddNewRow = false;
      this.dgvWorkItems.MasterTemplate.AllowDragToGroup = false;
      this.dgvWorkItems.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
      viewComboBoxColumn1.FieldName = "Status";
      viewComboBoxColumn1.HeaderText = "Status";
      viewComboBoxColumn1.Name = "Status";
      viewComboBoxColumn1.Width = 75;
      viewTextBoxColumn1.FieldName = "FacilityDesc";
      viewTextBoxColumn1.HeaderText = "Building";
      viewTextBoxColumn1.Name = "Building";
      viewTextBoxColumn1.ReadOnly = true;
      viewTextBoxColumn1.Width = 76;
      viewTextBoxColumn2.FieldName = "SYS_DESC";
      viewTextBoxColumn2.HeaderText = "System";
      viewTextBoxColumn2.Name = "System";
      viewTextBoxColumn2.ReadOnly = true;
      viewTextBoxColumn2.Width = 76;
      viewTextBoxColumn3.FieldName = "COMP_DESC";
      viewTextBoxColumn3.HeaderText = "Component";
      viewTextBoxColumn3.Name = "Component";
      viewTextBoxColumn3.ReadOnly = true;
      viewTextBoxColumn3.Width = 82;
      viewTextBoxColumn4.FieldName = "SectionDesc";
      viewTextBoxColumn4.HeaderText = "Section";
      viewTextBoxColumn4.Name = "Section";
      viewTextBoxColumn4.ReadOnly = true;
      viewTextBoxColumn4.Width = 76;
      viewTextBoxColumn5.FieldName = "Activity_Name";
      viewTextBoxColumn5.HeaderText = "Activity";
      viewTextBoxColumn5.Name = "ActivityName";
      viewTextBoxColumn5.ReadOnly = true;
      viewTextBoxColumn5.Width = 76;
      viewComboBoxColumn2.FieldName = "InspectionTypeLink";
      viewComboBoxColumn2.HeaderText = "Inspection Type";
      viewComboBoxColumn2.Name = "InspectionType";
      viewComboBoxColumn2.ReadOnly = true;
      viewComboBoxColumn2.Width = 101;
      viewTextBoxColumn6.AllowGroup = false;
      viewTextBoxColumn6.EnableExpressionEditor = false;
      viewTextBoxColumn6.FieldName = "WorkItemId";
      viewTextBoxColumn6.HeaderText = "WorkItemId";
      viewTextBoxColumn6.IsVisible = false;
      viewTextBoxColumn6.Name = "WorkItemId";
      viewTextBoxColumn6.ReadOnly = true;
      viewTextBoxColumn6.VisibleInColumnChooser = false;
      viewTextBoxColumn6.Width = 67;
      viewTextBoxColumn7.AllowGroup = false;
      viewTextBoxColumn7.EnableExpressionEditor = false;
      viewTextBoxColumn7.FieldName = "FiscalYear";
      viewTextBoxColumn7.HeaderText = "Fiscal Year";
      viewTextBoxColumn7.Name = "FiscalYear";
      viewTextBoxColumn7.ReadOnly = true;
      viewTextBoxColumn7.Width = 76;
      viewTextBoxColumn8.AllowGroup = false;
      viewTextBoxColumn8.EnableExpressionEditor = false;
      viewTextBoxColumn8.FieldName = "CompletionYear";
      viewTextBoxColumn8.HeaderText = "Completion Year";
      viewTextBoxColumn8.Name = "CompletionYear";
      viewTextBoxColumn8.ReadOnly = true;
      viewTextBoxColumn8.Width = 106;
      viewTextBoxColumn9.AllowGroup = false;
      viewTextBoxColumn9.EnableExpressionEditor = false;
      viewTextBoxColumn9.FieldName = "Description";
      viewTextBoxColumn9.HeaderText = "Description";
      viewTextBoxColumn9.Name = "Description";
      viewTextBoxColumn9.ReadOnly = true;
      viewTextBoxColumn9.Width = 79;
      viewTextBoxColumn10.AllowGroup = false;
      viewTextBoxColumn10.EnableExpressionEditor = false;
      viewTextBoxColumn10.FieldName = "SectionLink";
      viewTextBoxColumn10.HeaderText = "SectionLink";
      viewTextBoxColumn10.IsVisible = false;
      viewTextBoxColumn10.Name = "SectionLink";
      viewTextBoxColumn10.ReadOnly = true;
      viewTextBoxColumn10.VisibleInColumnChooser = false;
      viewDateTimeColumn.DateTimeKind = DateTimeKind.Local;
      viewDateTimeColumn.FieldName = "DateCompleted";
      viewDateTimeColumn.HeaderText = "DateCompleted";
      viewDateTimeColumn.Name = "DateCompleted";
      viewDateTimeColumn.ReadOnly = true;
      viewDateTimeColumn.Width = 169;
      this.dgvWorkItems.MasterTemplate.Columns.AddRange((GridViewDataColumn) viewComboBoxColumn1, (GridViewDataColumn) viewTextBoxColumn1, (GridViewDataColumn) viewTextBoxColumn2, (GridViewDataColumn) viewTextBoxColumn3, (GridViewDataColumn) viewTextBoxColumn4, (GridViewDataColumn) viewTextBoxColumn5, (GridViewDataColumn) viewComboBoxColumn2, (GridViewDataColumn) viewTextBoxColumn6, (GridViewDataColumn) viewTextBoxColumn7, (GridViewDataColumn) viewTextBoxColumn8, (GridViewDataColumn) viewTextBoxColumn9, (GridViewDataColumn) viewTextBoxColumn10, (GridViewDataColumn) viewDateTimeColumn);
      this.dgvWorkItems.MasterTemplate.EnableAlternatingRowColor = true;
      this.dgvWorkItems.MasterTemplate.EnableFiltering = true;
      this.dgvWorkItems.MasterTemplate.EnableGrouping = false;
      this.dgvWorkItems.MasterTemplate.ShowHeaderCellButtons = true;
      this.dgvWorkItems.Name = "dgvWorkItems";
      this.dgvWorkItems.RightToLeft = RightToLeft.No;
      this.dgvWorkItems.ShowGroupPanel = false;
      this.dgvWorkItems.ShowHeaderCellButtons = true;
      this.dgvWorkItems.Size = new Size(1002, 431);
      this.dgvWorkItems.TabIndex = 0;
      this.dgvWorkItems.Text = "RadGridView1";
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.dgvWorkItems, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnStartInspection, 1, 1);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.Size = new Size(1008, 466);
      this.TableLayoutPanel1.TabIndex = 1;
      this.btnStartInspection.Location = new Point(903, 440);
      this.btnStartInspection.Name = "btnStartInspection";
      this.btnStartInspection.Size = new Size(102, 23);
      this.btnStartInspection.TabIndex = 1;
      this.btnStartInspection.Text = "Start Inspection";
      this.btnStartInspection.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1008, 466);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmWorkItems);
      this.Text = "Work Items";
      this.dgvWorkItems.MasterTemplate.EndInit();
      this.dgvWorkItems.EndInit();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    internal virtual RadGridView dgvWorkItems
    {
      get
      {
        return this._dgvWorkItems;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        GridViewCellEventHandler cellEventHandler1 = new GridViewCellEventHandler(this.dgvWorkItems_CellDoubleClick);
        CurrentRowChangedEventHandler changedEventHandler = new CurrentRowChangedEventHandler(this.dgvWorkItems_CurrentRowChanged);
        GridViewCellEventHandler cellEventHandler2 = new GridViewCellEventHandler(this.dgvWorkItems_CellEndEdit);
        RadGridView dgvWorkItems1 = this._dgvWorkItems;
        if (dgvWorkItems1 != null)
        {
          dgvWorkItems1.CellDoubleClick -= cellEventHandler1;
          dgvWorkItems1.CurrentRowChanged -= changedEventHandler;
          dgvWorkItems1.CellEndEdit -= cellEventHandler2;
        }
        this._dgvWorkItems = value;
        RadGridView dgvWorkItems2 = this._dgvWorkItems;
        if (dgvWorkItems2 == null)
          return;
        dgvWorkItems2.CellDoubleClick += cellEventHandler1;
        dgvWorkItems2.CurrentRowChanged += changedEventHandler;
        dgvWorkItems2.CellEndEdit += cellEventHandler2;
      }
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnStartInspection
    {
      get
      {
        return this._btnStartInspection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnStartInspection_Click);
        Button btnStartInspection1 = this._btnStartInspection;
        if (btnStartInspection1 != null)
          btnStartInspection1.Click -= eventHandler;
        this._btnStartInspection = value;
        Button btnStartInspection2 = this._btnStartInspection;
        if (btnStartInspection2 == null)
          return;
        btnStartInspection2.Click += eventHandler;
      }
    }

    private DataRow Section_ParentComponent(DataRow row)
    {
      return mdUtility.DB.GetDataTable("SELECT * FROM System_Component WHERE SYS_COMP_ID='" + row["SEC_SYS_COMP_ID"].ToString() + "'").Rows[0];
    }

    private DataRow Component_ParentSystem(DataRow row)
    {
      return mdUtility.DB.GetDataTable("SELECT * FROM Building_System WHERE BLDG_SYS_ID='" + row["SYS_COMP_BLDG_SYS_ID"].ToString() + "'").Rows[0];
    }

    private DataRow System_ParentFacility(DataRow row)
    {
      return mdUtility.DB.GetDataTable("SELECT * FROM Facility WHERE Facility_ID='" + row["BLDG_SYS_BLDG_ID"].ToString() + "'").Rows[0];
    }

    private void frmWorkItems_Closing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      this.Hide();
    }

    private void StartInspection(string key)
    {
      this.Visible = false;
      DataRow row1 = this.Section_ParentComponent(mdUtility.DB.GetDataTable("SELECT * FROM Component_Section WHERE SEC_ID='" + key + "'").Rows[0]);
      DataRow row2 = this.Component_ParentSystem(row1);
      DataRow dataRow = this.System_ParentFacility(row2);
      UltraTreeNode nodeByKey1 = mdUtility.fMainForm.tvInspection.GetNodeByKey(dataRow["Facility_ID"].ToString());
      nodeByKey1.Expanded = true;
      nodeByKey1.Nodes[0].Expanded = true;
      mdUtility.fMainForm.tvInspection.GetNodeByKey(row2["BLDG_SYS_ID"].ToString()).Expanded = true;
      mdUtility.fMainForm.tvInspection.GetNodeByKey(row1["SYS_COMP_ID"].ToString()).Expanded = true;
      if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInventory)
        mdUtility.fMainForm.ToggleMode();
      frmMain fMainForm = mdUtility.fMainForm;
      UltraTreeNode nodeByKey2 = mdUtility.fMainForm.tvInspection.GetNodeByKey(key);
      ref UltraTreeNode local = ref nodeByKey2;
      fMainForm.SelectNewActiveInspectionNode(ref local);
    }

    private void dgvWorkItems_CellDoubleClick(object sender, GridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 | e.RowIndex > this.dgvWorkItems.Rows.Count || Information.IsDBNull(RuntimeHelpers.GetObjectValue(e.Row.Cells["InspectionType"].Value)))
        return;
      this.StartInspection(this.dgvWorkItems.Rows[e.RowIndex].Cells["SectionLink"].Value.ToString());
    }

    private void btnStartInspection_Click(object sender, EventArgs e)
    {
      if ((uint) this.dgvWorkItems.SelectedRows.Count <= 0U)
        return;
      this.StartInspection(this.dgvWorkItems.SelectedRows[0].Cells["SectionLink"].Value.ToString());
    }

    private void frmWorkItems_Load(object sender, EventArgs e)
    {
      this.dgvWorkItems.DataSource = (object) mdUtility.DB.GetDataTable("    SELECT WorkItem.BRED_Status                                                                                                          \t,WorkItem.WorkItemID                                                                                                                \t,WorkItem.WorkItemType                                                                                                              \t,WorkItem.MILink                                                                                                                    \t,WorkItem.STATUS                                                                                                                    \t,WorkItem.Description                                                                                                               \t,WorkItem.FiscalYear                                                                                                                \t,WorkItem.CompletionYear                                                                                                            \t,WorkItem.DateCompleted                                                                                                             \t,WorkItem.SectionLink                                                                                                               \t,[Facility].[Number] + ' ' + [Facility].[Name] AS FacilityDesc                                                                      \t,[UII Naming Analysis].SYS_DESC                                                                                                     \t,[UII Naming Analysis].RO_Component.COMP_DESC                                                                                     \t,[UII Naming Analysis].[" + (mdUtility.UseUniformat ? "UII Proposed Name" : "Standard Name") + "] AS SectionDesc                                                                    \t,Cost_Activity.Activity_Name                                                                                                        \t,Cost_Activity.InspectionTypeLink                                                                                                   FROM (                                                                                                                                \tFacility INNER JOIN (                                                                                                               \t\t(                                                                                                                               \t\t\tWorkItem INNER JOIN Component_Section ON WorkItem.SectionLink = Component_Section.SEC_ID                                    \t\t\t) INNER JOIN (                                                                                                              \t\t\tSystem_Component INNER JOIN (                                                                                               \t\t\t\tBuilding_System INNER JOIN [UII Naming Analysis] ON Building_System.BLDG_SYS_LINK = [UII Naming Analysis].SYS_ID        \t\t\t\t) ON (Building_System.BLDG_SYS_ID = System_Component.SYS_COMP_BLDG_SYS_ID)                                              \t\t\t\tAND (System_Component.SYS_COMP_COMP_LINK = [UII Naming Analysis].COMP_ID)                                               \t\t\t) ON (System_Component.SYS_COMP_ID = Component_Section.SEC_SYS_COMP_ID)                                                     \t\t\tAND (Component_Section.SEC_CMC_LINK = [UII Naming Analysis].CMC_ID)                                                         \t\t) ON Facility.Facility_ID = Building_System.BLDG_SYS_BLDG_ID                                                                    \t)                                                                                                                                   INNER JOIN Cost_Activity ON WorkItem.ActivityLink = Cost_Activity.Activity_ID;                                                    ");
      GridViewComboBoxColumn column1 = (GridViewComboBoxColumn) this.dgvWorkItems.Columns["Status"];
      GridViewComboBoxColumn viewComboBoxColumn1 = column1;
      IEnumerable<WorkItem.StatusType> source1 = Enum.GetValues(typeof (WorkItem.StatusType)).Cast<WorkItem.StatusType>();
      Func<WorkItem.StatusType, VB\u0024AnonymousType_0<int, string>> selector1;
      // ISSUE: reference to a compiler-generated field
      if (frmWorkItems._Closure\u0024__.\u0024I23\u002D0 != null)
      {
        // ISSUE: reference to a compiler-generated field
        selector1 = frmWorkItems._Closure\u0024__.\u0024I23\u002D0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmWorkItems._Closure\u0024__.\u0024I23\u002D0 = selector1 = enm =>
        {
          var data = new{ Index = Convert.ToInt32((int) enm), Description = enm.ToString() };
          return data;
        };
      }
      List<VB\u0024AnonymousType_0<int, string>> list1 = source1.Select(selector1).ToList();
      viewComboBoxColumn1.DataSource = (object) list1;
      column1.DisplayMember = "Description";
      column1.ValueMember = "Index";
      GridViewComboBoxColumn column2 = (GridViewComboBoxColumn) this.dgvWorkItems.Columns["InspectionType"];
      GridViewComboBoxColumn viewComboBoxColumn2 = column2;
      IEnumerable<InspectionSchedule.InspType> source2 = Enum.GetValues(typeof (InspectionSchedule.InspType)).Cast<InspectionSchedule.InspType>();
      Func<InspectionSchedule.InspType, VB\u0024AnonymousType_0<int, string>> selector2;
      // ISSUE: reference to a compiler-generated field
      if (frmWorkItems._Closure\u0024__.\u0024I23\u002D1 != null)
      {
        // ISSUE: reference to a compiler-generated field
        selector2 = frmWorkItems._Closure\u0024__.\u0024I23\u002D1;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmWorkItems._Closure\u0024__.\u0024I23\u002D1 = selector2 = enm =>
        {
          var data = new{ Index = Convert.ToInt32((int) enm), Description = enm.ToString() };
          return data;
        };
      }
      List<VB\u0024AnonymousType_0<int, string>> list2 = source2.Select(selector2).ToList();
      viewComboBoxColumn2.DataSource = (object) list2;
      column2.DisplayMember = "Description";
      column2.ValueMember = "Index";
    }

    private object CellValueToStringValue(object value)
    {
      if (value is DateTime)
        return Information.IsDBNull(RuntimeHelpers.GetObjectValue(value)) ? (object) "" : (object) ((DateTime) value).ToShortDateString();
      return Information.IsDBNull(RuntimeHelpers.GetObjectValue(value)) ? (object) "" : (object) value.ToString();
    }

    private object ValueToCheckBoxName(object value)
    {
      if (value is DateTime)
        return Information.IsDBNull(RuntimeHelpers.GetObjectValue(value)) ? (object) "(Blanks)" : (object) ((DateTime) value).ToShortDateString();
      return Information.IsDBNull(RuntimeHelpers.GetObjectValue(value)) ? (object) "(Blanks)" : (object) value.ToString();
    }

    private object CheckBoxNameToCellValue(object checkBoxItem)
    {
      return Microsoft.VisualBasic.CompilerServices.Operators.CompareString(checkBoxItem.ToString(), "(Blanks)", false) != 0 ? (object) checkBoxItem.ToString() : (object) "";
    }

    public void UpdateWorkItemInDatabase(GridViewRowInfo gridRow)
    {
      string str1 = gridRow.Cells["WorkItemID"].Value.ToString();
      string str2 = Conversions.ToString(gridRow.Cells["Status"].Value);
      bool flag = !Information.IsDBNull(RuntimeHelpers.GetObjectValue(gridRow.Cells["DateCompleted"].Value));
      mdUtility.DB.ExecuteCommand("UPDATE WorkItem SET Status='" + str2 + "' WHERE WorkItemID='" + str1 + "'", false);
      if (flag)
        mdUtility.DB.ExecuteCommand("UPDATE WorkItem SET DateCompleted='" + Conversions.ToString(gridRow.Cells["DateCompleted"].Value) + "' WHERE WorkItemID='" + str1 + "'", false);
      mdUtility.DB.ExecuteCommand("UPDATE WorkItem SET BRED_Status= 'U' WHERE WorkItemID='" + str1 + "'", false);
    }

    private void dgvWorkItems_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
    {
      this.btnStartInspection.Enabled = e.CurrentRow != null && !Information.IsDBNull(RuntimeHelpers.GetObjectValue(e.CurrentRow.Cells["InspectionType"].Value));
    }

    private void dgvWorkItems_CellEndEdit(object sender, GridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 | e.RowIndex >= this.dgvWorkItems.Rows.Count)
        return;
      GridViewRowInfo row = this.dgvWorkItems.Rows[e.RowIndex];
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row.Cells["Status"].Value, (object) WorkItem.StatusType.Completed, false))
        row.Cells["DateCompleted"].Value = (object) DateTime.Now;
      this.UpdateWorkItemInDatabase(row);
    }
  }
}
