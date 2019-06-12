// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmReportsViewer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ClosedXML.Excel;
using ERDC.CERL.SMS.Libraries.Services.ReportsTelerik;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SMS.Libraries.Utility.BredPackage.Classes.Containers;
using SMS.Libraries.Utility.BredPackage.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.Reporting;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmReportsViewer : Form
  {
    private IContainer components;
    private ZipBredPackage _ZP;
    public DataTable BuildingDT;

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
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmReportsViewer));
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.cboReportsList = new ComboBox();
      this.lblSelectReport = new Label();
      this.btnCancel = new Button();
      this.rvReportsViewer = new Telerik.ReportViewer.WinForms.ReportViewer();
      this.clbBuildingsList = new CheckedListBox();
      this.btnExcel = new Button();
      this.btnClear = new Button();
      this.btnLoad = new Button();
      this.btnRefresh = new Button();
      this.ttReports = new ToolTip(this.components);
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel1.ColumnCount = 5;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.cboReportsList, 1, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.lblSelectReport, 0, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnCancel, 4, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.rvReportsViewer, 0, 5);
      this.TableLayoutPanel1.Controls.Add((Control) this.clbBuildingsList, 2, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnExcel, 4, 4);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnClear, 3, 1);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnLoad, 3, 0);
      this.TableLayoutPanel1.Controls.Add((Control) this.btnRefresh, 3, 2);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 6;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Size = new Size(784, 512);
      this.TableLayoutPanel1.TabIndex = 0;
      this.cboReportsList.Dock = DockStyle.Fill;
      this.cboReportsList.FormattingEnabled = true;
      this.cboReportsList.Location = new Point(84, 3);
      this.cboReportsList.Name = "cboReportsList";
      this.cboReportsList.Size = new Size((int) byte.MaxValue, 21);
      this.cboReportsList.TabIndex = 0;
      this.lblSelectReport.AutoSize = true;
      this.lblSelectReport.Dock = DockStyle.Fill;
      this.lblSelectReport.Location = new Point(3, 0);
      this.lblSelectReport.Name = "lblSelectReport";
      this.lblSelectReport.Padding = new Padding(0, 5, 0, 0);
      this.lblSelectReport.Size = new Size(75, 27);
      this.lblSelectReport.TabIndex = 3;
      this.lblSelectReport.Text = "Select Report:";
      this.lblSelectReport.TextAlign = ContentAlignment.TopRight;
      this.btnCancel.AutoSize = true;
      this.btnCancel.Dock = DockStyle.Fill;
      this.btnCancel.Location = new Point(696, 2);
      this.btnCancel.Margin = new Padding(3, 2, 3, 2);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(85, 23);
      this.btnCancel.TabIndex = 8;
      this.btnCancel.Text = "Close";
      this.ttReports.SetToolTip((Control) this.btnCancel, "Close Reports Viewer");
      this.rvReportsViewer.AutoSize = true;
      this.TableLayoutPanel1.SetColumnSpan((Control) this.rvReportsViewer, 5);
      this.rvReportsViewer.Dock = DockStyle.Fill;
      this.rvReportsViewer.Location = new Point(3, 208);
      this.rvReportsViewer.Name = "rvReportsViewer";
      this.rvReportsViewer.Size = new Size(778, 301);
      this.rvReportsViewer.TabIndex = 1;
      this.ttReports.SetToolTip((Control) this.rvReportsViewer, "Select Report");
      this.clbBuildingsList.Dock = DockStyle.Fill;
      this.clbBuildingsList.FormattingEnabled = true;
      this.clbBuildingsList.Location = new Point(345, 3);
      this.clbBuildingsList.Name = "clbBuildingsList";
      this.TableLayoutPanel1.SetRowSpan((Control) this.clbBuildingsList, 5);
      this.clbBuildingsList.Size = new Size((int) byte.MaxValue, 199);
      this.clbBuildingsList.TabIndex = 9;
      this.ttReports.SetToolTip((Control) this.clbBuildingsList, "Select building/s to filter");
      this.btnExcel.AutoSize = true;
      this.btnExcel.Dock = DockStyle.Fill;
      this.btnExcel.Enabled = false;
      this.btnExcel.Location = new Point(696, 179);
      this.btnExcel.Name = "btnExcel";
      this.btnExcel.Size = new Size(85, 23);
      this.btnExcel.TabIndex = 11;
      this.btnExcel.Text = "Export Excel";
      this.ttReports.SetToolTip((Control) this.btnExcel, "Create raw data .xlsx file of currently loaded report.");
      this.btnExcel.UseVisualStyleBackColor = true;
      this.btnClear.AutoSize = true;
      this.btnClear.Dock = DockStyle.Fill;
      this.btnClear.Location = new Point(606, 30);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new Size(84, 23);
      this.btnClear.TabIndex = 10;
      this.btnClear.Text = "Clear";
      this.ttReports.SetToolTip((Control) this.btnClear, "Clear Building Filters");
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnLoad.AutoSize = true;
      this.btnLoad.Dock = DockStyle.Fill;
      this.btnLoad.Location = new Point(606, 2);
      this.btnLoad.Margin = new Padding(3, 2, 3, 2);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new Size(84, 23);
      this.btnLoad.TabIndex = 2;
      this.btnLoad.Text = "Load";
      this.ttReports.SetToolTip((Control) this.btnLoad, "Load currently selected report");
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnRefresh.AutoSize = true;
      this.btnRefresh.Dock = DockStyle.Fill;
      this.btnRefresh.Location = new Point(606, 59);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new Size(84, 23);
      this.btnRefresh.TabIndex = 12;
      this.btnRefresh.Text = "Refresh";
      this.ttReports.SetToolTip((Control) this.btnRefresh, "Refresh Building List");
      this.btnRefresh.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 512);
      this.Controls.Add((Control) this.TableLayoutPanel1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(800, 550);
      this.Name = nameof (frmReportsViewer);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Reports Viewer";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ComboBox cboReportsList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnLoad
    {
      get
      {
        return this._btnLoad;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnLoad_Click);
        Button btnLoad1 = this._btnLoad;
        if (btnLoad1 != null)
          btnLoad1.Click -= eventHandler;
        this._btnLoad = value;
        Button btnLoad2 = this._btnLoad;
        if (btnLoad2 == null)
          return;
        btnLoad2.Click += eventHandler;
      }
    }

    internal virtual Label lblSelectReport { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnCancel
    {
      get
      {
        return this._btnCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnCancel_Click);
        Button btnCancel1 = this._btnCancel;
        if (btnCancel1 != null)
          btnCancel1.Click -= eventHandler;
        this._btnCancel = value;
        Button btnCancel2 = this._btnCancel;
        if (btnCancel2 == null)
          return;
        btnCancel2.Click += eventHandler;
      }
    }

    internal virtual CheckedListBox clbBuildingsList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnClear
    {
      get
      {
        return this._btnClear;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnClear_Click);
        Button btnClear1 = this._btnClear;
        if (btnClear1 != null)
          btnClear1.Click -= eventHandler;
        this._btnClear = value;
        Button btnClear2 = this._btnClear;
        if (btnClear2 == null)
          return;
        btnClear2.Click += eventHandler;
      }
    }

    internal virtual Button btnExcel
    {
      get
      {
        return this._btnExcel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnExcel_Click);
        Button btnExcel1 = this._btnExcel;
        if (btnExcel1 != null)
          btnExcel1.Click -= eventHandler;
        this._btnExcel = value;
        Button btnExcel2 = this._btnExcel;
        if (btnExcel2 == null)
          return;
        btnExcel2.Click += eventHandler;
      }
    }

    internal virtual ToolTip ttReports { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual Telerik.ReportViewer.WinForms.ReportViewer rvReportsViewer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnRefresh
    {
      get
      {
        return this._btnRefresh;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnRefresh_Click);
        Button btnRefresh1 = this._btnRefresh;
        if (btnRefresh1 != null)
          btnRefresh1.Click -= eventHandler;
        this._btnRefresh = value;
        Button btnRefresh2 = this._btnRefresh;
        if (btnRefresh2 == null)
          return;
        btnRefresh2.Click += eventHandler;
      }
    }

    public frmReportsViewer()
    {
      this.InitializeComponent();
      this.FillReportsList();
      this.FillBuildingsList();
    }

    public frmReportsViewer(ZipBredPackage ZPB)
    {
      this.InitializeComponent();
      this.FillReportsList();
      this.FillBuildingsList();
      this._ZP = ZPB;
    }

    public List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> GetImages(
      List<KeyValuePair<string, string>> filterList)
    {
      // ISSUE: variable of a compiler-generated type
      frmReportsViewer._Closure\u0024__50\u002D0 closure500_1;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      frmReportsViewer._Closure\u0024__50\u002D0 closure500_2 = new frmReportsViewer._Closure\u0024__50\u002D0(closure500_1);
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> source1 = new List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
      this._ZP.ResetManifest();
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT Facility_ID AS ID, '' AS ParentID,  Number + ' - ' + Name AS Description FROM Facility");
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList1 = source1;
      DataTable dtOwner1 = dataTable1;
      List<IAttachmentInfo> imageManifest1 = this._ZP.ImageManifest;
      Func<IAttachmentInfo, bool> predicate1;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D0 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate1 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I50\u002D0 = predicate1 = (Func<IAttachmentInfo, bool>) (im => im.InventoryClass == InventoryClass.Facility);
      }
      List<IAttachmentInfo> list1 = imageManifest1.Where<IAttachmentInfo>(predicate1).ToList<IAttachmentInfo>();
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> owners1 = this.MatchImagesToOwners(dtOwner1, list1, "Building");
      imageList1.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) owners1);
      string str1 = "";
      int num = checked (filterList.Count - 1);
      int index = 0;
      while (index <= num)
      {
        if ((uint) index > 0U)
          str1 += " OR ";
        string[] strArray = new string[6]{ str1, " ([Description] = '", null, null, null, null };
        KeyValuePair<string, string> filter = filterList[index];
        strArray[2] = filter.Key;
        strArray[3] = " - ";
        filter = filterList[index];
        strArray[4] = filter.Value;
        strArray[5] = "')";
        str1 = string.Concat(strArray);
        checked { ++index; }
      }
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str1, "", false) > 0U)
      {
        // ISSUE: reference to a compiler-generated field
        closure500_2.\u0024VB\u0024Local_lstBuildingIds = new List<string>();
        // ISSUE: variable of a compiler-generated type
        frmReportsViewer._Closure\u0024__50\u002D0 closure500_3 = closure500_2;
        IEnumerable<DataRow> source2 = ((IEnumerable<DataRow>) dataTable1.Select(str1)).AsEnumerable<DataRow>();
        Func<DataRow, string> selector;
        // ISSUE: reference to a compiler-generated field
        if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D1 != null)
        {
          // ISSUE: reference to a compiler-generated field
          selector = frmReportsViewer._Closure\u0024__.\u0024I50\u002D1;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          frmReportsViewer._Closure\u0024__.\u0024I50\u002D1 = selector = (Func<DataRow, string>) (r => r["ID"].ToString().ToLower());
        }
        List<string> list2 = source2.Select<DataRow, string>(selector).ToList<string>();
        // ISSUE: reference to a compiler-generated field
        closure500_3.\u0024VB\u0024Local_lstBuildingIds = list2;
      }
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT DISTINCT  Building_System.BLDG_SYS_ID AS ID, Building_System.BLDG_SYS_BLDG_ID AS ParentID, [UII Naming Analysis].SYS_DESC AS Description FROM Building_System INNER JOIN [UII Naming Analysis] ON Building_System.BLDG_SYS_LINK = [UII Naming Analysis].SYS_ID ORDER BY Building_System.BLDG_SYS_BLDG_ID, [UII Naming Analysis].SYS_DESC;");
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList2 = source1;
      DataTable dtOwner2 = dataTable2;
      List<IAttachmentInfo> imageManifest2 = this._ZP.ImageManifest;
      Func<IAttachmentInfo, bool> predicate2;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D2 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate2 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D2;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I50\u002D2 = predicate2 = (Func<IAttachmentInfo, bool>) (im => im.InventoryClass == InventoryClass.BuildingSystem);
      }
      List<IAttachmentInfo> list3 = imageManifest2.Where<IAttachmentInfo>(predicate2).ToList<IAttachmentInfo>();
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> owners2 = this.MatchImagesToOwners(dtOwner2, list3, "System");
      imageList2.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) owners2);
      DataTable dataTable3 = mdUtility.DB.GetDataTable("  SELECT DISTINCT System_Component.SYS_COMP_ID AS ID, System_Component.SYS_COMP_BLDG_SYS_ID AS ParentID, [UII Naming Analysis].RO_Component.COMP_DESC AS Description    FROM System_Component INNER JOIN [UII Naming Analysis] ON System_Component.SYS_COMP_COMP_LINK = [UII Naming Analysis].COMP_ID                                         ORDER BY System_Component.SYS_COMP_BLDG_SYS_ID, [UII Naming Analysis].RO_Component.COMP_DESC;                                                                       ");
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList3 = source1;
      DataTable dtOwner3 = dataTable3;
      List<IAttachmentInfo> imageManifest3 = this._ZP.ImageManifest;
      Func<IAttachmentInfo, bool> predicate3;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D3 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate3 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D3;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I50\u002D3 = predicate3 = (Func<IAttachmentInfo, bool>) (im => im.InventoryClass == InventoryClass.SystemComponent);
      }
      List<IAttachmentInfo> list4 = imageManifest3.Where<IAttachmentInfo>(predicate3).ToList<IAttachmentInfo>();
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> owners3 = this.MatchImagesToOwners(dtOwner3, list4, "Component");
      imageList3.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) owners3);
      string str2 = Conversions.ToString(Interaction.IIf(mdUtility.UseUniformat, (object) "UII Proposed Name", (object) "Standard Name"));
      DataTable dataTable4 = mdUtility.DB.GetDataTable(" SELECT DISTINCT Component_Section.SEC_ID AS ID, Component_Section.SEC_SYS_COMP_ID AS ParentID, SEC_NAME & ': ' & [UII Naming Analysis].[" + str2 + "] AS Description  FROM Component_Section INNER JOIN [UII Naming Analysis] ON Component_Section.SEC_CMC_LINK = [UII Naming Analysis].CMC_ID                                 ORDER BY Component_Section.SEC_SYS_COMP_ID, SEC_NAME & ': ' & [UII Naming Analysis].[" + str2 + "];                                                                  ");
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList4 = source1;
      DataTable dtOwner4 = dataTable4;
      List<IAttachmentInfo> imageManifest4 = this._ZP.ImageManifest;
      Func<IAttachmentInfo, bool> predicate4;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D4 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate4 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D4;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I50\u002D4 = predicate4 = (Func<IAttachmentInfo, bool>) (im => im.InventoryClass == InventoryClass.ComponentSection);
      }
      List<IAttachmentInfo> list5 = imageManifest4.Where<IAttachmentInfo>(predicate4).ToList<IAttachmentInfo>();
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> owners4 = this.MatchImagesToOwners(dtOwner4, list5, "Section");
      imageList4.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) owners4);
      DataTable dataTable5 = mdUtility.DB.GetDataTable(" SELECT SectionDetails.SD_ID AS ID                                                                    \t,SectionDetails.SD_Sec_ID AS ParentID                                                             \t,IIf(IsNull(ID_Number)                                                                            \t\tOR ID_Number = '', '', 'ID:' & ID_Number & ';') & IIf(IsNull(Equipment_Type)                  \t\tOR Equipment_Type = '', '', ' Type:' & Equipment_Type & ';') & IIf(IsNull(Equipment_Make)     \t\tOR Equipment_Make = '', '', ' Type:' & Equipment_Make & ';') & IIf(IsNull(Model)              \t\tOR Model = '', '', ' Model:' & Model & ';') AS Description                                    FROM SectionDetails                                                                                  ORDER BY SectionDetails.SD_Sec_ID                                                                    \t,IIf(IsNull(ID_Number)                                                                            \t\tOR ID_Number = '', '', 'ID:' & ID_Number & ';') & IIf(IsNull(Equipment_Type)                  \t\tOR Equipment_Type = '', '', ' Type:' & Equipment_Type & ';') & IIf(IsNull(Equipment_Make)      \t\tOR Equipment_Make = '', '', ' Type:' & Equipment_Make & ';') & IIf(IsNull(Model)               \t\tOR Model = '', '', ' Model:' & Model & ';');                                                 ");
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList5 = source1;
      DataTable dtOwner5 = dataTable5;
      List<IAttachmentInfo> imageManifest5 = this._ZP.ImageManifest;
      Func<IAttachmentInfo, bool> predicate5;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D5 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate5 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D5;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I50\u002D5 = predicate5 = (Func<IAttachmentInfo, bool>) (im => im.InventoryClass == InventoryClass.SectionDetail);
      }
      List<IAttachmentInfo> list6 = imageManifest5.Where<IAttachmentInfo>(predicate5).ToList<IAttachmentInfo>();
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> owners5 = this.MatchImagesToOwners(dtOwner5, list6, "Section Detail");
      imageList5.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) owners5);
      DataTable dataTable6 = mdUtility.DB.GetDataTable(" SELECT Inspection_Data.INSP_DATA_ID AS ID                 \t,Inspection_Data.INSP_DATA_SEC_ID AS ParentID            \t,Inspection_Data.INSP_DATA_INSP_DATE AS Description      FROM Inspection_Data;                                    ");
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList6 = source1;
      DataTable dtOwner6 = dataTable6;
      List<IAttachmentInfo> imageManifest6 = this._ZP.ImageManifest;
      Func<IAttachmentInfo, bool> predicate6;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D6 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate6 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D6;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I50\u002D6 = predicate6 = (Func<IAttachmentInfo, bool>) (im => im.InventoryClass == InventoryClass.InspectionData);
      }
      List<IAttachmentInfo> list7 = imageManifest6.Where<IAttachmentInfo>(predicate6).ToList<IAttachmentInfo>();
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> owners6 = this.MatchImagesToOwners(dtOwner6, list7, "Inspection");
      imageList6.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) owners6);
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> imageList7;
      // ISSUE: reference to a compiler-generated field
      if (closure500_2.\u0024VB\u0024Local_lstBuildingIds != null)
      {
        // ISSUE: variable of a compiler-generated type
        frmReportsViewer._Closure\u0024__50\u002D1 closure501_1;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        frmReportsViewer._Closure\u0024__50\u002D1 closure501_2 = new frmReportsViewer._Closure\u0024__50\u002D1(closure501_1);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        closure501_2.\u0024VB\u0024Local_buildings = source1.Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(new Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>(closure500_2._Lambda\u0024__7)).ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
        // ISSUE: variable of a compiler-generated type
        frmReportsViewer._Closure\u0024__50\u002D1 closure501_3 = closure501_2;
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> source2 = source1;
        Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool> predicate7;
        // ISSUE: reference to a compiler-generated field
        if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D8 != null)
        {
          // ISSUE: reference to a compiler-generated field
          predicate7 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D8;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          frmReportsViewer._Closure\u0024__.\u0024I50\u002D8 = predicate7 = (Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>) (i => Microsoft.VisualBasic.CompilerServices.Operators.CompareString(i.ClassType, "System", false) == 0);
        }
        // ISSUE: reference to a compiler-generated method
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> list2 = source2.Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(predicate7).Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(new Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>(closure501_2._Lambda\u0024__9)).ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
        // ISSUE: reference to a compiler-generated field
        closure501_3.\u0024VB\u0024Local_systems = list2;
        // ISSUE: variable of a compiler-generated type
        frmReportsViewer._Closure\u0024__50\u002D1 closure501_4 = closure501_2;
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> source3 = source1;
        Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool> predicate8;
        // ISSUE: reference to a compiler-generated field
        if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D11 != null)
        {
          // ISSUE: reference to a compiler-generated field
          predicate8 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D11;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          frmReportsViewer._Closure\u0024__.\u0024I50\u002D11 = predicate8 = (Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>) (i => Microsoft.VisualBasic.CompilerServices.Operators.CompareString(i.ClassType, "Component", false) == 0);
        }
        // ISSUE: reference to a compiler-generated method
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> list8 = source3.Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(predicate8).Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(new Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>(closure501_2._Lambda\u0024__12)).ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
        // ISSUE: reference to a compiler-generated field
        closure501_4.\u0024VB\u0024Local_components = list8;
        // ISSUE: variable of a compiler-generated type
        frmReportsViewer._Closure\u0024__50\u002D1 closure501_5 = closure501_2;
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> source4 = source1;
        Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool> predicate9;
        // ISSUE: reference to a compiler-generated field
        if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D14 != null)
        {
          // ISSUE: reference to a compiler-generated field
          predicate9 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D14;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          frmReportsViewer._Closure\u0024__.\u0024I50\u002D14 = predicate9 = (Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>) (i => Microsoft.VisualBasic.CompilerServices.Operators.CompareString(i.ClassType, "Section", false) == 0);
        }
        // ISSUE: reference to a compiler-generated method
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> list9 = source4.Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(predicate9).Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(new Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>(closure501_2._Lambda\u0024__15)).ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
        // ISSUE: reference to a compiler-generated field
        closure501_5.\u0024VB\u0024Local_sections = list9;
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> source5 = source1;
        Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool> predicate10;
        // ISSUE: reference to a compiler-generated field
        if (frmReportsViewer._Closure\u0024__.\u0024I50\u002D17 != null)
        {
          // ISSUE: reference to a compiler-generated field
          predicate10 = frmReportsViewer._Closure\u0024__.\u0024I50\u002D17;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          frmReportsViewer._Closure\u0024__.\u0024I50\u002D17 = predicate10 = (Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>) (i => Microsoft.VisualBasic.CompilerServices.Operators.CompareString(i.ClassType, "Section Detail", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(i.ClassType, "Inspection", false) == 0);
        }
        // ISSUE: reference to a compiler-generated method
        List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> list10 = source5.Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(predicate10).Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(new Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>(closure501_2._Lambda\u0024__18)).ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
        imageList7 = new List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
        // ISSUE: reference to a compiler-generated field
        imageList7.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) closure501_2.\u0024VB\u0024Local_buildings);
        // ISSUE: reference to a compiler-generated field
        imageList7.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) closure501_2.\u0024VB\u0024Local_systems.ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>());
        // ISSUE: reference to a compiler-generated field
        imageList7.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) closure501_2.\u0024VB\u0024Local_components.ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>());
        // ISSUE: reference to a compiler-generated field
        imageList7.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) closure501_2.\u0024VB\u0024Local_sections.ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>());
        imageList7.AddRange((IEnumerable<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>) list10.ToList<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>());
      }
      else
        imageList7 = source1;
      return imageList7;
    }

    private List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> MatchImagesToOwners(
      DataTable dtOwner,
      List<IAttachmentInfo> images,
      string inventoryClass)
    {
      List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image> source1 = new List<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>();
      try
      {
        foreach (DataRow row in dtOwner.Rows)
        {
          row["ID"] = (object) row["ID"].ToString().ToLower();
          row["ParentID"] = (object) row["ParentID"].ToString().ToLower();
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      List<IAttachmentInfo>.Enumerator enumerator1;
      try
      {
        enumerator1 = images.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          IAttachmentInfo current = enumerator1.Current;
          current.InventoryId = new Guid(current.InventoryId.ToString().ToLower());
        }
      }
      finally
      {
        enumerator1.Dispose();
      }
      if (dtOwner.Rows.Count > 0)
      {
        EnumerableRowCollection<DataRow> source2 = dtOwner.AsEnumerable();
        Func<DataRow, object> keySelector1;
        // ISSUE: reference to a compiler-generated field
        if (frmReportsViewer._Closure\u0024__.\u0024I51\u002D0 != null)
        {
          // ISSUE: reference to a compiler-generated field
          keySelector1 = frmReportsViewer._Closure\u0024__.\u0024I51\u002D0;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          frmReportsViewer._Closure\u0024__.\u0024I51\u002D0 = keySelector1 = (Func<DataRow, object>) (row => row["ID"]);
        }
        Queue<DataRow> dataRowQueue = new Queue<DataRow>((IEnumerable<DataRow>) source2.OrderBy<DataRow, object>(keySelector1));
        DataRow dataRow = dataRowQueue.Dequeue();
        IEnumerator<IAttachmentInfo> enumerator2;
        try
        {
          List<IAttachmentInfo> source3 = images;
          Func<IAttachmentInfo, Guid> keySelector2;
          // ISSUE: reference to a compiler-generated field
          if (frmReportsViewer._Closure\u0024__.\u0024I51\u002D1 != null)
          {
            // ISSUE: reference to a compiler-generated field
            keySelector2 = frmReportsViewer._Closure\u0024__.\u0024I51\u002D1;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            frmReportsViewer._Closure\u0024__.\u0024I51\u002D1 = keySelector2 = (Func<IAttachmentInfo, Guid>) (i => i.InventoryId);
          }
          enumerator2 = source3.OrderBy<IAttachmentInfo, Guid>(keySelector2).GetEnumerator();
          while (enumerator2.MoveNext())
          {
            IAttachmentInfo current = enumerator2.Current;
            while ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(current.InventoryId.ToString(), dataRow["ID"].ToString(), false) > 0U)
              dataRow = dataRowQueue.Dequeue();
            ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image image = new ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image() { OwnerId = dataRow["ID"].ToString(), OwnerLabel = Conversions.ToString(dataRow["Description"]), ParentID = dataRow["ParentID"].ToString(), AddDate = new DateTime?(current.Date), FileName = current.FileName, Title = current.Title, Description = current.Description, ClassType = inventoryClass };
            source1.Add(image);
          }
        }
        finally
        {
          enumerator2?.Dispose();
        }
        try
        {
          foreach (DataRow row in dtOwner.Rows)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            frmReportsViewer._Closure\u0024__51\u002D0 closure510 = new frmReportsViewer._Closure\u0024__51\u002D0(closure510);
            // ISSUE: reference to a compiler-generated field
            closure510.\u0024VB\u0024Local_ownerRow = row;
            // ISSUE: reference to a compiler-generated method
            if (source1.Where<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>(new Func<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image, bool>(closure510._Lambda\u0024__2)).FirstOrDefault<ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image>() == null)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              source1.Add(new ERDC.CERL.SMS.Libraries.Services.ReportsTelerik.Image()
              {
                OwnerId = closure510.\u0024VB\u0024Local_ownerRow["ID"].ToString(),
                OwnerLabel = Conversions.ToString(closure510.\u0024VB\u0024Local_ownerRow["Description"]),
                ParentID = closure510.\u0024VB\u0024Local_ownerRow["ParentID"].ToString(),
                ClassType = inventoryClass
              });
            }
          }
        }
        finally
        {
          IEnumerator enumerator3;
          if (enumerator3 is IDisposable)
            (enumerator3 as IDisposable).Dispose();
        }
      }
      return source1;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      InstanceReportSource instanceReportSource = new InstanceReportSource();
      List<DataRowView> list1 = this.clbBuildingsList.CheckedItems.OfType<DataRowView>().ToList<DataRowView>();
      Func<DataRowView, KeyValuePair<string, string>> selector;
      // ISSUE: reference to a compiler-generated field
      if (frmReportsViewer._Closure\u0024__.\u0024I52\u002D0 != null)
      {
        // ISSUE: reference to a compiler-generated field
        selector = frmReportsViewer._Closure\u0024__.\u0024I52\u002D0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        frmReportsViewer._Closure\u0024__.\u0024I52\u002D0 = selector = (Func<DataRowView, KeyValuePair<string, string>>) (x => new KeyValuePair<string, string>(x["Number"].ToString(), x["Name"].ToString()));
      }
      List<KeyValuePair<string, string>> list2 = list1.Select<DataRowView, KeyValuePair<string, string>>(selector).ToList<KeyValuePair<string, string>>();
      string str = this.cboReportsList.SelectedValue.ToString();
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(str))
      {
        case 401363915:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Images", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new Images(this.GetImages(list2));
            break;
          }
          break;
        case 620836960:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Facility Summary", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new FacilitySummary(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
        case 1426661725:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Inventory Details", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new InventoryDetails(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
        case 2060094845:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Sections Not Inspected", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new SectionsNotInspected(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
        case 2429122380:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Inspection Summary NonRep Samples", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new InspectionSummaryNonRepSamplesvb(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
        case 2597560123:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Inventory Summary", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new InventorySummary(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
        case 2726760412:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "System Summary", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new SystemSummary(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
        case 2929580583:
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "Inspection Summary", false) == 0)
          {
            instanceReportSource.ReportDocument = (IReportDocument) new InspectionSummary(mdUtility.DB.ConnectionString, list2);
            break;
          }
          break;
      }
      instanceReportSource.ReportDocument.PageSettings.Landscape = true;
      this.rvReportsViewer.ReportSource = (ReportSource) instanceReportSource;
      this.rvReportsViewer.RefreshReport();
      this.btnExcel.Enabled = true;
    }

    private void FillReportsList()
    {
      this.cboReportsList.DataSource = (object) new List<string>()
      {
        "Facility Summary",
        "System Summary",
        "Inventory Summary",
        "Inventory Details",
        "Inspection Summary",
        "Inspection Summary NonRep Samples",
        "Sections Not Inspected",
        "Images"
      };
    }

    private void FillBuildingsList()
    {
      this.clbBuildingsList.DataSource = (object) mdUtility.DB.GetDataTable("SELECT Facility_ID AS ID, Number + ' ' + Name as Description, Number, Name FROM Facility ORDER BY Number");
      this.clbBuildingsList.ValueMember = "ID";
      this.clbBuildingsList.DisplayMember = "Description";
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      try
      {
        foreach (object checkedIndex in this.clbBuildingsList.CheckedIndices)
          this.clbBuildingsList.SetItemCheckState(Conversions.ToInteger(checkedIndex), CheckState.Unchecked);
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    private void btnExcel_Click(object sender, EventArgs e)
    {
      object objectValue = RuntimeHelpers.GetObjectValue(((IExcelReport) ((InstanceReportSource) this.rvReportsViewer.ReportSource).ReportDocument).SqlSelectCommand);
      DataTable dataTable = !(objectValue is string) ? (DataTable) objectValue : mdUtility.DB.GetDataTable(Conversions.ToString(((IExcelReport) ((InstanceReportSource) this.rvReportsViewer.ReportSource).ReportDocument).SqlSelectCommand));
      XLWorkbook xlWorkbook = new XLWorkbook();
      xlWorkbook.Worksheets.Add(dataTable);
      this.ApplyColumnFormatting((IExcelReport) ((InstanceReportSource) this.rvReportsViewer.ReportSource).ReportDocument, xlWorkbook.Worksheets.ElementAtOrDefault<IXLWorksheet>(0), dataTable);
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.FileName = ((ReportItemBase) ((InstanceReportSource) this.rvReportsViewer.ReportSource).ReportDocument).Name;
        saveFileDialog.Filter = "Excel(2007-2010)|*.xlsx|All Files|*.*";
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
          return;
        xlWorkbook.SaveAs(saveFileDialog.FileName);
      }
    }

    private void ApplyColumnFormatting(
      IExcelReport rpt,
      IXLWorksheet wrkSheet,
      DataTable exportTable)
    {
      Collection<KeyValuePair<string, ReportUtility.ColumnStyle>> formatsValuePairs = rpt.GetColumnFormatsValuePairs();
      if (formatsValuePairs == null)
        return;
      IEnumerator<KeyValuePair<string, ReportUtility.ColumnStyle>> enumerator;
      try
      {
        enumerator = formatsValuePairs.GetEnumerator();
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, ReportUtility.ColumnStyle> current = enumerator.Current;
          wrkSheet.Column(XLHelper.GetColumnLetterFromNumber(checked (exportTable.Columns[current.Key].Ordinal + 1))).Style.NumberFormat.Format = ReportUtility.GetExcelColumnFormat(current.Value);
        }
      }
      finally
      {
        enumerator?.Dispose();
      }
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
      this.FillBuildingsList();
    }
  }
}
