// Decompiled with JetBrains decompiler
// Type: BuilderRED.Section
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinTree;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class Section
  {
    internal static string AddSection(
      string ComponentID,
      string SectionName,
      int Material,
      int ComponentType,
      double Quantity,
      string YearBuilt,
      bool YearEst,
      string Comments,
      string FunctionalAreaId,
      int StatusId,
      bool Painted,
      bool EnergyAudit,
      string YearPainted = "",
      string PaintType = "")
    {
      string str1 = "SELECT * FROM Component_Section WHERE ([BRED_Status]<>'D' OR [BRED_Status] IS NULL) AND [SEC_SYS_COMP_ID]={" + ComponentID + "} AND SEC_NAME='" + SectionName + "' AND [SEC_CMC_LINK]=" + Conversions.ToString(ComponentType);
      DataTable dataTable = mdUtility.DB.GetDataTable(str1);
      string str2;
      if (dataTable.Rows.Count > 0)
      {
        int num = (int) Interaction.MsgBox((object) "You cannot create a new section with the same name, material, and component type as an existing section", MsgBoxStyle.OkOnly, (object) null);
        str2 = "";
      }
      else
      {
        DataRow row = dataTable.NewRow();
        DataRow dataRow = row;
        dataRow["BRED_Status"] = (object) "N";
        string uniqueId = mdUtility.GetUniqueID();
        dataRow["SEC_ID"] = (object) uniqueId;
        dataRow["SEC_SYS_COMP_ID"] = (object) ComponentID;
        dataRow["SEC_COMMENTS"] = (object) Comments;
        dataRow["SEC_Status_ID"] = (object) StatusId;
        dataRow["Sec_CMC_Link"] = (object) ComponentType;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(FunctionalAreaId, "", false) > 0U)
          dataRow["SEC_FunctionalArea_ID"] = (object) FunctionalAreaId;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(SectionName ?? "", "", false) == 0)
          dataRow["SEC_NAME"] = (object) "N/A";
        else if (Microsoft.VisualBasic.Strings.Len(SectionName) > 50)
        {
          int num = (int) Interaction.MsgBox((object) "The section name can be no more than 50 characters.  \r\nThe section name will be truncated to 50 characters.", MsgBoxStyle.Information, (object) null);
          dataRow["SEC_NAME"] = (object) Microsoft.VisualBasic.Strings.Left(SectionName, 50);
        }
        else
          dataRow["SEC_NAME"] = (object) SectionName;
        dataRow["SEC_QTY"] = !mdUtility.fMainForm.miUnits.Checked ? (object) Quantity : (object) (Quantity / mdUtility.MetricConversionFactor((long) ComponentType));
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(YearBuilt), "", false) > 0U)
        {
          dataRow["SEC_YEAR_BUILT"] = (object) Conversions.ToInteger(Microsoft.VisualBasic.Strings.Trim(YearBuilt));
          dataRow["SEC_Date_Source"] = !YearEst ? (object) "Input" : (object) "Estimated";
        }
        if (Painted)
        {
          dataRow["SEC_PAINT"] = (object) true;
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(YearPainted), "", false) > 0U)
            dataRow["SEC_DATE_PAINTED"] = (object) Conversions.ToInteger(Microsoft.VisualBasic.Strings.Trim(YearPainted));
          dataRow["SEC_PAINT_LINK"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(PaintType, "", false) <= 0U ? (object) -1 : (object) PaintType;
        }
        else
        {
          dataRow["SEC_PAINT"] = (object) false;
          dataRow["SEC_DATE_PAINTED"] = (object) DBNull.Value;
          dataRow["SEC_PAINT_LINK"] = (object) -1;
        }
        if (mdUtility.UseEnergyForm && EnergyAudit)
          EfficiencyAssessment.CreateEntry(mdUtility.fMainForm.CurrentBldg, uniqueId);
        dataTable.Rows.Add(row);
        mdUtility.DB.SaveDataTable(ref dataTable, str1);
        str2 = uniqueId;
      }
      return str2;
    }

    internal static void LoadSection(string strID, bool resetSelectedTab = true)
    {
      if (resetSelectedTab)
        mdUtility.fMainForm.SectionPageView.SelectedPage = mdUtility.fMainForm.SectionPageViewPage;
      DataTable dataTable1 = mdUtility.DB.GetDataTable("select * from [Section Info] WHERE [SEC_ID]={" + strID + "}");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM qryRSLbySecID WHERE [SEC_ID]={" + strID + "}");
      if (dataTable1.Rows.Count == 0)
        throw new Exception("Unable to find the the supplied section id in the query 'Section Info'.");
      if (dataTable2.Rows.Count == 0)
        throw new Exception("Unable to find the supplied section id in query 'qryRSLbySecID'.");
      DataRow row1 = dataTable1.Rows[0];
      frmMain fMainForm = mdUtility.fMainForm;
      fMainForm.m_bSectionLoaded = false;
      fMainForm.cboSectionName.Text = "";
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["SEC_NAME"])))
        fMainForm.cboSectionName.SelectedText = Conversions.ToString(row1["Sec_Name"]);
      fMainForm.m_bDDLoad = true;
      string str = "SELECT MAT_CAT_ID, MAT_CAT_DESC FROM ";
      mdUtility.LoadMstrTable("SectionMaterial", Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(!mdUtility.UseUniformat ? (object) (str + "qryMaterialCategories WHERE [CMC_COMP_LINK]=") : (object) (str + "qryMaterialCategoriesUniformat WHERE [CMC_COMP_UII_LINK]="), row1["COMP_ID"])));
      fMainForm.cboSectionMaterial.SelectedIndex = -1;
      fMainForm.cboSectionMaterial.ValueMember = "MAT_CAT_ID";
      fMainForm.cboSectionMaterial.DisplayMember = "MAT_CAT_DESC";
      fMainForm.cboSectionMaterial.DataSource = (object) mdUtility.get_MstrTable("SectionMaterial");
      fMainForm.m_bDDLoad = false;
      if (fMainForm.cboSectionMaterial.SelectedIndex == -1 || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(fMainForm.cboSectionMaterial.SelectedValue, row1["MAT_CAT_ID"], false))
        fMainForm.cboSectionMaterial.SelectedValue = RuntimeHelpers.GetObjectValue(row1["MAT_CAT_ID"]);
      else
        fMainForm.cboSectionMaterial_SelectedIndexChanged((object) fMainForm.cboSectionMaterial, new EventArgs());
      fMainForm.lblMaterial.Text = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(row1["mat_cat_lbl"], (object) ":"));
      mdUtility.fMainForm.lblSectionYearBuilt.Text = Microsoft.VisualBasic.Strings.InStr(fMainForm.lblMaterial.Text, "Equip", CompareMethod.Binary) <= 0 ? "Year Built:" : "Year Installed:";
      fMainForm.cboSectionComponentType.SelectedValue = RuntimeHelpers.GetObjectValue(row1["SEC_CMC_LINK"]);
      if (fMainForm.miUnits.Checked)
      {
        fMainForm.txtSectionAmount.Text = Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["eng_qty"])) ? "" : Microsoft.VisualBasic.Strings.Format(RuntimeHelpers.GetObjectValue(row1["eng_qty"]), "##,##0");
        fMainForm.lblUOM.Text = Conversions.ToString(row1["uom_eng_unit_abbr"]);
      }
      else
      {
        fMainForm.txtSectionAmount.Text = Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["met_qty"])) ? "" : Microsoft.VisualBasic.Strings.Format(RuntimeHelpers.GetObjectValue(row1["met_qty"]), "##,##0");
        fMainForm.lblUOM.Text = Conversions.ToString(row1["uom_met_unit_abbr"]);
      }
      fMainForm.cboSectionStatus.ValueMember = "ID";
      fMainForm.cboSectionStatus.DisplayMember = "Description";
      fMainForm.cboSectionStatus.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM RO_ComponentSection_Status");
      fMainForm.cboSectionStatus.SelectedValue = RuntimeHelpers.GetObjectValue(row1["SEC_Status_ID"]);
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.lblUOM.Text, "EA", false) == 0)
      {
        fMainForm.cmdIncrease.Visible = true;
        fMainForm.cmdDecrease.Visible = true;
        fMainForm.cmdCalc.Visible = false;
      }
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.lblUOM.Text, "SF", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.lblUOM.Text, "SM", false) == 0)
      {
        fMainForm.cmdIncrease.Visible = false;
        fMainForm.cmdDecrease.Visible = false;
        fMainForm.cmdCalc.Visible = true;
      }
      else
      {
        fMainForm.cmdIncrease.Visible = false;
        fMainForm.cmdDecrease.Visible = false;
        fMainForm.cmdCalc.Visible = false;
      }
      fMainForm.txtSectionYearBuilt.Text = Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["sec_year_built"])) ? "" : Conversions.ToString(row1["sec_year_built"]);
      fMainForm.cboFunctionalArea.ValueMember = "Area_ID";
      fMainForm.cboFunctionalArea.DisplayMember = "Name";
      fMainForm.cboFunctionalArea.DataSource = (object) mdUtility.DB.GetDataTable("SELECT Functional_Area.Area_ID, Functional_Area.Name FROM Functional_Area INNER JOIN (((Facility INNER JOIN Building_System ON Facility.Facility_ID = Building_System.BLDG_SYS_BLDG_ID) INNER JOIN System_Component ON Building_System.BLDG_SYS_ID = System_Component.SYS_COMP_BLDG_SYS_ID) INNER JOIN Component_Section ON System_Component.SYS_COMP_ID = Component_Section.SEC_SYS_COMP_ID) ON Functional_Area.BLDG_ID = Facility.Facility_ID WHERE Component_Section.SEC_ID = {" + strID + "}");
      DataRow row2 = ((DataTable) fMainForm.cboFunctionalArea.DataSource).NewRow();
      row2["Area_ID"] = (object) DBNull.Value;
      row2["Name"] = (object) "";
      ((DataTable) fMainForm.cboFunctionalArea.DataSource).Rows.InsertAt(row2, 0);
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["SEC_FunctionalArea_ID"])))
        fMainForm.cboFunctionalArea.SelectedValue = (object) 0;
      else
        fMainForm.cboFunctionalArea.SelectedValue = RuntimeHelpers.GetObjectValue(row1["SEC_FunctionalArea_ID"]);
      if (Conversions.ToBoolean((object) (bool) (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["SEC_Date_Source"])) ? 0 : (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual(row1["SEC_Date_Source"], (object) "Input", false), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual(row1["SEC_DATE_SOURCE"], (object) "Roofer", false))) ? 1 : 0))))
      {
        fMainForm.chkYearEstimated.CheckState = CheckState.Checked;
        fMainForm.txtSectionYearBuilt.BackColor = Color.Yellow;
      }
      else
      {
        fMainForm.chkYearEstimated.CheckState = CheckState.Unchecked;
        fMainForm.txtSectionYearBuilt.BackColor = SystemColors.Window;
      }
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["PAINT_LIFE"])))
      {
        fMainForm.lblPainted.Visible = true;
        fMainForm.chkPainted.Visible = true;
      }
      else
      {
        fMainForm.lblPainted.Visible = false;
        fMainForm.chkPainted.Visible = false;
      }
      if (Conversions.ToBoolean(row1["sec_paint"]))
      {
        fMainForm.chkPainted.CheckState = CheckState.Checked;
        fMainForm.lblDatePainted.Visible = true;
        fMainForm.dtPainted.Visible = true;
        fMainForm.lblPaintType.Visible = true;
        fMainForm.cboSectionPaintType.Visible = true;
        if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row1["sec_paint_link"], (object) -1, false))
          fMainForm.cboSectionPaintType.SelectedValue = RuntimeHelpers.GetObjectValue(row1["sec_paint_link"]);
        else
          fMainForm.cboSectionPaintType.SelectedIndex = -1;
      }
      else
      {
        fMainForm.chkPainted.CheckState = CheckState.Unchecked;
        fMainForm.lblDatePainted.Visible = false;
        fMainForm.dtPainted.Visible = false;
        fMainForm.lblPaintType.Visible = false;
        fMainForm.cboSectionPaintType.Visible = false;
      }
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["sec_date_painted"])))
        fMainForm.dtPainted.Text = Conversions.ToString(row1["sec_date_painted"]);
      if (mdUtility.UseEnergyForm)
      {
        object obj = row1["SEC_ID"];
        Guid sectionId = obj != null ? (Guid) obj : new Guid();
        fMainForm.chkEnergyAuditRequired.CheckState = !EfficiencyAssessment.IsSectionEfficient(sectionId) ? CheckState.Checked : CheckState.Unchecked;
      }
      if (Conversions.ToBoolean((object) (bool) (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["sec_comments"])) ? 0 : (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.NotObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(row1["sec_comments"], (object) "", false))) ? 1 : 0))))
        fMainForm.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
      else
        fMainForm.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      fMainForm.m_bSectionNeedToSave = false;
      fMainForm.m_bSectionYearChanged = false;
      fMainForm.m_bSectionLoaded = true;
      Section.LockSection(false);
      Section.LoadSectionDetails(strID);
    }

    private static void LoadSectionDetails(string strID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM [SectionDetails] WHERE [SD_SEC_ID] = {" + strID + "} AND ([BRED_Status] IS NULL OR [BRED_Status] <> 'D')");
      DataTable tableSchema = mdUtility.DB.GetTableSchema("SectionDetails");
      try
      {
        foreach (DataRow row in dataTable.Rows)
          tableSchema.ImportRow(row);
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      dataTable.Dispose();
      mdUtility.fMainForm.CommandBar.Grip.Visibility = ElementVisibility.Collapsed;
      mdUtility.fMainForm.CommandBar.OverflowButton.Visibility = ElementVisibility.Collapsed;
      mdUtility.fMainForm.NewDetailButton.Click -= new EventHandler(Section.DetailsGridAddNew);
      mdUtility.fMainForm.NewDetailButton.Click += new EventHandler(Section.DetailsGridAddNew);
      RadGridView detailsGrid = mdUtility.fMainForm.DetailsGrid;
      detailsGrid.DataBindingComplete -= new GridViewBindingCompleteEventHandler(Section.DetailsGrid_DataBindingComplete);
      detailsGrid.DataBindingComplete += new GridViewBindingCompleteEventHandler(Section.DetailsGrid_DataBindingComplete);
      detailsGrid.CellFormatting -= new CellFormattingEventHandler(Section.DetailsGrid_CellFormatting);
      detailsGrid.CellFormatting += new CellFormattingEventHandler(Section.DetailsGrid_CellFormatting);
      detailsGrid.ViewCellFormatting -= new CellFormattingEventHandler(Section.radGridView1_ViewCellFormatting);
      detailsGrid.ViewCellFormatting += new CellFormattingEventHandler(Section.radGridView1_ViewCellFormatting);
      detailsGrid.Resize -= new EventHandler(Section.DetailsGrid_Resizing);
      detailsGrid.Resize += new EventHandler(Section.DetailsGrid_Resizing);
      detailsGrid.Templates.Clear();
      detailsGrid.Relations.Clear();
      detailsGrid.Columns.Clear();
      detailsGrid.AutoSizeRows = true;
      detailsGrid.DataSource = (object) tableSchema;
      detailsGrid.ReadOnly = true;
      detailsGrid.AllowAddNewRow = false;
      IEnumerator<GridViewDataColumn> enumerator1;
      try
      {
        enumerator1 = detailsGrid.Columns.GetEnumerator();
        while (enumerator1.MoveNext())
          enumerator1.Current.Width = 100;
      }
      finally
      {
        enumerator1?.Dispose();
      }
      Section.LoadDetailsTab(tableSchema);
      Section.LoadCommentsTab(tableSchema);
      detailsGrid.CommandCellClick -= new CommandCellClickEventHandler(Section.DetailsGrid_commandCellClick);
      detailsGrid.CommandCellClick += new CommandCellClickEventHandler(Section.DetailsGrid_commandCellClick);
      detailsGrid.CurrentRowChanging -= new CurrentRowChangingEventHandler(Section.DetailsGrid_CurrentRowChanging);
      detailsGrid.CurrentRow = (GridViewRowInfo) null;
      detailsGrid.CurrentRowChanging += new CurrentRowChangingEventHandler(Section.DetailsGrid_CurrentRowChanging);
      detailsGrid.CellClick -= new GridViewCellEventHandler(Section.DetailsGrid_CellClick);
      detailsGrid.CellClick += new GridViewCellEventHandler(Section.DetailsGrid_CellClick);
      detailsGrid.ChildViewExpanding -= new ChildViewExpandingEventHandler(Section.SectionDetails_ChildViewExpanding);
      detailsGrid.ChildViewExpanding += new ChildViewExpandingEventHandler(Section.SectionDetails_ChildViewExpanding);
      detailsGrid.ChildViewExpanded -= new ChildViewExpandedEventHandler(Section.SectionDetails_ChildViewExpanded);
      detailsGrid.ChildViewExpanded += new ChildViewExpandedEventHandler(Section.SectionDetails_ChildViewExpanded);
      GridViewCommandColumn viewCommandColumn1 = new GridViewCommandColumn();
      viewCommandColumn1.Name = "cmdCopyDetail";
      viewCommandColumn1.Width = 40;
      viewCommandColumn1.MaxWidth = 40;
      viewCommandColumn1.MinWidth = 40;
      detailsGrid.Columns.Add((GridViewDataColumn) viewCommandColumn1);
      detailsGrid.Columns.Move(detailsGrid.Columns.IndexOf("cmdCopyDetail"), 0);
      GridViewCommandColumn viewCommandColumn2 = new GridViewCommandColumn();
      viewCommandColumn2.Name = "cmdImageLink";
      viewCommandColumn2.Width = 40;
      viewCommandColumn2.MaxWidth = 40;
      viewCommandColumn2.MinWidth = 40;
      detailsGrid.Columns.Add((GridViewDataColumn) viewCommandColumn2);
      detailsGrid.Columns.Move(detailsGrid.Columns.IndexOf("cmdImageLink"), 0);
      GridViewCommandColumn viewCommandColumn3 = new GridViewCommandColumn();
      viewCommandColumn3.Name = "cmdDelete";
      viewCommandColumn3.Width = 40;
      viewCommandColumn3.MaxWidth = 40;
      viewCommandColumn3.MinWidth = 40;
      detailsGrid.Columns.Add((GridViewDataColumn) viewCommandColumn3);
      detailsGrid.Columns.Move(detailsGrid.Columns.IndexOf("cmdDelete"), 0);
      mdUtility.fMainForm.SetSectionChanged(false);
      mdUtility.fMainForm.DetailsPageViewPage.Text = "Details (" + Conversions.ToString(mdUtility.fMainForm.DetailsGrid.Rows.Count) + ")";
    }

    private static void LoadCommentsTab(DataTable table)
    {
      GridViewTemplate gridViewTemplate = new GridViewTemplate();
      gridViewTemplate.Caption = "Comments";
      gridViewTemplate.AllowAddNewRow = false;
      gridViewTemplate.DataSource = (object) table;
      gridViewTemplate.ShowColumnHeaders = false;
      gridViewTemplate.ShowRowHeaderColumn = false;
      gridViewTemplate.AllowRowResize = true;
      gridViewTemplate.Columns["Comment"].Width = checked (mdUtility.fMainForm.DetailsGrid.Width - 67);
      gridViewTemplate.Columns["Comment"].DisableHTMLRendering = false;
      mdUtility.fMainForm.DetailsGrid.Templates.Insert(1, gridViewTemplate);
      mdUtility.fMainForm.DetailsGrid.Relations.Add(new GridViewRelation((GridViewTemplate) mdUtility.fMainForm.DetailsGrid.MasterTemplate)
      {
        ChildTemplate = gridViewTemplate,
        ParentColumnNames = {
          "SD_ID"
        },
        ChildColumnNames = {
          "SD_ID"
        }
      });
      HtmlViewDefinition htmlViewDefinition = new HtmlViewDefinition();
      htmlViewDefinition.RowTemplate.Rows.Add(new RowDefinition()
      {
        Height = 300
      });
      htmlViewDefinition.RowTemplate.Rows[0].Cells.Add(new CellDefinition("Comment", 0, 1, 1));
      gridViewTemplate.ViewDefinition = (IGridViewDefinition) htmlViewDefinition;
      mdUtility.fMainForm.DetailsGrid.CreateCell -= new GridViewCreateCellEventHandler(Section.radGridView1_CreateCell);
      mdUtility.fMainForm.DetailsGrid.CreateCell += new GridViewCreateCellEventHandler(Section.radGridView1_CreateCell);
    }

    private static void LoadDetailsTab(DataTable table)
    {
      RadGridView detailsGrid = mdUtility.fMainForm.DetailsGrid;
      Section.fillDetailsDropDownLists();
      GridViewTemplate gridViewTemplate = new GridViewTemplate();
      gridViewTemplate.Caption = "Details";
      gridViewTemplate.AllowAddNewRow = false;
      gridViewTemplate.DataSource = (object) table;
      gridViewTemplate.AllowRowResize = false;
      gridViewTemplate.ShowColumnHeaders = false;
      gridViewTemplate.ShowRowHeaderColumn = false;
      IEnumerator<GridViewDataColumn> enumerator;
      try
      {
        enumerator = gridViewTemplate.Columns.GetEnumerator();
        while (enumerator.MoveNext())
        {
          GridViewColumn current = (GridViewColumn) enumerator.Current;
          current.Width = checked (mdUtility.fMainForm.DetailsGrid.Width - 67);
          current.DisableHTMLRendering = false;
        }
      }
      finally
      {
        enumerator?.Dispose();
      }
      detailsGrid.Templates.Insert(0, gridViewTemplate);
      detailsGrid.Relations.Add(new GridViewRelation((GridViewTemplate) detailsGrid.MasterTemplate)
      {
        ChildTemplate = gridViewTemplate,
        ParentColumnNames = {
          "SD_ID"
        },
        ChildColumnNames = {
          "SD_ID"
        }
      });
      HtmlViewDefinition htmlViewDefinition = new HtmlViewDefinition();
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.lblMaterial.Text, "Equipment Category:", false) == 0)
      {
        htmlViewDefinition.RowTemplate.Rows.Add(new RowDefinition()
        {
          Height = 32
        });
        htmlViewDefinition.RowTemplate.Rows[0].Cells.Add(new CellDefinition("ID_Number", 0, 1, 1));
      }
      int num = 1;
      do
      {
        htmlViewDefinition.RowTemplate.Rows.Add(new RowDefinition()
        {
          Height = 32
        });
        checked { ++num; }
      }
      while (num <= 18);
      htmlViewDefinition.RowTemplate.Rows[1].Cells.Add(new CellDefinition("Model", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[2].Cells.Add(new CellDefinition("Serial_Number", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[3].Cells.Add(new CellDefinition("Manufacturer", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[4].Cells.Add(new CellDefinition("Location", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[5].Cells.Add(new CellDefinition("Equipment_Type", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[6].Cells.Add(new CellDefinition("Equipment_Make", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[7].Cells.Add(new CellDefinition("Capacity", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[8].Cells.Add(new CellDefinition("Date_Manufactured", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[9].Cells.Add(new CellDefinition("Date_Installed", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[10].Cells.Add(new CellDefinition("Control_Type_Make", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[11].Cells.Add(new CellDefinition("Warranty_Date", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[12].Cells.Add(new CellDefinition("Warranty_Company", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[13].Cells.Add(new CellDefinition("Warranty_Date2", 0, 1, 1));
      htmlViewDefinition.RowTemplate.Rows[14].Cells.Add(new CellDefinition("Warranty_Company2", 0, 1, 1));
      gridViewTemplate.ViewDefinition = (IGridViewDefinition) htmlViewDefinition;
      detailsGrid.CreateCell -= new GridViewCreateCellEventHandler(Section.radGridView1_CreateCell);
      detailsGrid.CreateCell += new GridViewCreateCellEventHandler(Section.radGridView1_CreateCell);
    }

    private static void DetailsGrid_Resizing(object sender, EventArgs e)
    {
      IEnumerator<GridViewTemplate> enumerator1;
      try
      {
        enumerator1 = mdUtility.fMainForm.DetailsGrid.Templates.GetEnumerator();
        while (enumerator1.MoveNext())
        {
          GridViewTemplate current = enumerator1.Current;
          IEnumerator<GridViewDataColumn> enumerator2;
          try
          {
            enumerator2 = current.Columns.GetEnumerator();
            while (enumerator2.MoveNext())
              enumerator2.Current.Width = checked (mdUtility.fMainForm.DetailsGrid.Width - 67);
          }
          finally
          {
            enumerator2?.Dispose();
          }
        }
      }
      finally
      {
        enumerator1?.Dispose();
      }
    }

    internal static void DetailsGridAddNew(object sender, EventArgs e)
    {
      Section.CreateNewDetail(0);
    }

    private static DataRow CreateNewDetail(int index = 0)
    {
      RadGridView detailsGrid = mdUtility.fMainForm.DetailsGrid;
      if (detailsGrid.Rows.Count == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.lblMaterial.Text, "Equipment Category:", false) == 0)
      {
        DataRow row = ((DataTable) detailsGrid.DataSource).NewRow();
        row["SD_ID"] = (object) mdUtility.GetUniqueID().ToString();
        row["SD_Sec_ID"] = (object) mdUtility.fMainForm.CurrentSection.ToString();
        row["BRED_Status"] = (object) "N";
        if (index == 0)
          ((DataTable) detailsGrid.DataSource).Rows.Add(row);
        else
          ((DataTable) detailsGrid.DataSource).Rows.InsertAt(row, index);
        mdUtility.fMainForm.SetSectionChanged(true);
        return row;
      }
      int num = (int) Interaction.MsgBox((object) "Only equipment sections can have multiple section details.", MsgBoxStyle.OkOnly, (object) "Add not allowed");
      return (DataRow) null;
    }

    private static void DetailsGrid_CurrentRowChanging(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
    }

    private static void SectionDetails_ChildViewExpanding(
      object sender,
      ChildViewExpandingEventArgs e)
    {
      mdUtility.fMainForm.DetailsGrid.GridViewElement.SuspendLayout();
    }

    private static void SectionDetails_ChildViewExpanded(
      object sender,
      ChildViewExpandedEventArgs e)
    {
      mdUtility.fMainForm.DetailsGrid.GridViewElement.ResumeLayout(false);
    }

    private static void DetailsGrid_CellClick(object sender, GridViewCellEventArgs e)
    {
      if (e.Column is GridViewCommandColumn || e.Column is GridViewIndentColumn)
        return;
      e.Row.IsExpanded = !e.Row.IsExpanded;
    }

    private static void DetailsGrid_DataBindingComplete(
      object sender,
      GridViewBindingCompleteEventArgs e)
    {
      mdUtility.fMainForm.DetailsGrid.Columns["ID_Number"].HeaderText = "ID Number";
      mdUtility.fMainForm.DetailsGrid.Columns["Equipment_Type"].HeaderText = "Equipment Type";
      mdUtility.fMainForm.DetailsGrid.Columns["Equipment_Make"].HeaderText = "Equipment Make";
      mdUtility.fMainForm.DetailsGrid.Columns["Model"].HeaderText = "Model";
      mdUtility.fMainForm.DetailsGrid.Columns["Serial_Number"].HeaderText = "Serial Number";
      mdUtility.fMainForm.DetailsGrid.Columns["Capacity"].HeaderText = "Capacity";
      mdUtility.fMainForm.DetailsGrid.Columns["Manufacturer"].HeaderText = "Manufacturer";
      mdUtility.fMainForm.DetailsGrid.Columns["Date_Manufactured"].HeaderText = "Date Manufactured";
      mdUtility.fMainForm.DetailsGrid.Columns["Date_Installed"].HeaderText = "Date Installed";
      mdUtility.fMainForm.DetailsGrid.Columns["Control_Type_Make"].HeaderText = "Control Type/Make";
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Date"].HeaderText = "Warranty Date";
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Company"].HeaderText = "Warranty Company";
      mdUtility.fMainForm.DetailsGrid.Columns["Location"].HeaderText = "Location";
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Date2"].HeaderText = "Warranty Date 2";
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Company2"].HeaderText = "Warranty Company 2";
      mdUtility.fMainForm.DetailsGrid.Columns["ID_Number"].IsVisible = true;
      mdUtility.fMainForm.DetailsGrid.Columns["Equipment_Type"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Equipment_Make"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Model"].IsVisible = true;
      mdUtility.fMainForm.DetailsGrid.Columns["Serial_Number"].IsVisible = true;
      mdUtility.fMainForm.DetailsGrid.Columns["Capacity"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Manufacturer"].IsVisible = true;
      mdUtility.fMainForm.DetailsGrid.Columns["Date_Manufactured"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Date_Installed"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Control_Type_Make"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Date"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Company"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Location"].IsVisible = true;
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Date2"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["Warranty_Company2"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["BRED_Status"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["SD_ID"].IsVisible = false;
      mdUtility.fMainForm.DetailsGrid.Columns["SD_Sec_ID"].IsVisible = false;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.lblMaterial.Text, "Equipment Category:", false) <= 0U)
        return;
      mdUtility.fMainForm.DetailsGrid.Columns["ID_Number"].IsVisible = false;
    }

    private static void DetailsGrid_commandCellClick(object sender, EventArgs e)
    {
      GridCommandCellElement commandCellElement = sender as GridCommandCellElement;
      if (commandCellElement == null)
        return;
      string name = commandCellElement.ColumnInfo.Name;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "cmdDelete", false) != 0)
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "cmdImageLink", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name, "cmdCopyDetail", false) == 0)
          {
            try
            {
              DataRow newDetail = Section.CreateNewDetail(checked (commandCellElement.RowInfo.Index + 1));
              DataTable dataSource = (DataTable) mdUtility.fMainForm.DetailsGrid.DataSource;
              DataRow dataRow = dataSource.AsEnumerable().ElementAtOrDefault<DataRow>(commandCellElement.RowInfo.Index);
              if (newDetail != null)
              {
                int num = checked (dataSource.Columns.Count - 1);
                int index = 0;
                while (index <= num)
                {
                  string columnName = dataRow.Table.Columns[index].ColumnName;
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnName, "ID_Number", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnName, "SD_ID", false) != 0 && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnName, "BRED_Status", false) > 0U)
                    newDetail[index] = RuntimeHelpers.GetObjectValue(dataRow[index]);
                  checked { ++index; }
                }
              }
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
            }
          }
        }
        else
        {
          DataTable dataSource = (DataTable) mdUtility.fMainForm.DetailsGrid.DataSource;
          frmMain fMainForm = mdUtility.fMainForm;
          object obj = dataSource.AsEnumerable().ElementAtOrDefault<DataRow>(commandCellElement.RowInfo.Index)["SD_ID"];
          Guid selectedID = obj != null ? (Guid) obj : new Guid();
          fMainForm.OpenFrmImagesSectionDetail(selectedID);
        }
      }
      else if (Interaction.MsgBox((object) "Are you sure you want to delete this Section Detail", MsgBoxStyle.YesNo, (object) "Delete?") == MsgBoxResult.Yes)
      {
        mdUtility.fMainForm.DetailsGrid.Rows.Remove(commandCellElement.RowElement.RowInfo);
        mdUtility.fMainForm.SetSectionChanged(true);
      }
    }

    private static void radGridView1_ViewCellFormatting(object sender, CellFormattingEventArgs e)
    {
      if (e.CellElement is GridGroupExpanderCellElement)
      {
        ((GridGroupExpanderCellElement) e.CellElement).Expander.SignStyle = SignStyles.Image;
        ((GridGroupExpanderCellElement) e.CellElement).Expander.DrawSignFill = false;
        ((GridGroupExpanderCellElement) e.CellElement).Expander.DrawSignBorder = false;
        if (e.CellElement.RowInfo.IsExpanded)
          ((GridGroupExpanderCellElement) e.CellElement).Expander.SignImage = (Image) mdUtility.fMainForm.CollapseImage;
        else
          ((GridGroupExpanderCellElement) e.CellElement).Expander.SignImage = (Image) mdUtility.fMainForm.ExpansionImage;
      }
      GridDetailViewCellElement cellElement = e.CellElement as GridDetailViewCellElement;
      if (cellElement == null)
        return;
      cellElement.ChildTableElement.EnableHotTracking = false;
    }

    private static void DetailsGrid_CellFormatting(object sender, CellFormattingEventArgs e)
    {
      GridViewDataColumn columnInfo = e.CellElement.ColumnInfo as GridViewDataColumn;
      if (columnInfo != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.OwnerTemplate.Caption, "Details", false) == 0 && e.CellElement.Children.Count > 0)
      {
        System.Windows.Forms.Label control = (System.Windows.Forms.Label) ((TableLayoutPanel) ((RadHostItem) e.CellElement.Children[0]).HostedControl).Controls[0];
        string fieldName = columnInfo.FieldName;
        // ISSUE: reference to a compiler-generated method
        switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(fieldName))
        {
          case 651016848:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Date2", false) == 0)
            {
              control.Text = "Warranty Date 2:";
              break;
            }
            break;
          case 1029734518:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Control_Type_Make", false) == 0)
            {
              control.Text = "Control Type/Make: ";
              break;
            }
            break;
          case 1539345862:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Location", false) == 0)
            {
              control.Text = "Location: ";
              break;
            }
            break;
          case 1698094189:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Company", false) == 0)
            {
              control.Text = "Warranty Company: ";
              break;
            }
            break;
          case 1717866665:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Capacity", false) == 0)
            {
              control.Text = "Capacity: ";
              break;
            }
            break;
          case 2023210704:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Manufacturer", false) == 0)
            {
              control.Text = "Manufacturer: ";
              break;
            }
            break;
          case 2189814010:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Model", false) == 0)
            {
              control.Text = "Model: ";
              break;
            }
            break;
          case 2342475992:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Date_Installed", false) == 0)
            {
              control.Text = "Year Installed: ";
              break;
            }
            break;
          case 2749248770:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Date", false) == 0)
            {
              control.Text = "Warranty Date:";
              break;
            }
            break;
          case 2773601224:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "ID_Number", false) == 0)
            {
              control.Text = "ID Number:";
              break;
            }
            break;
          case 3025987981:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Company2", false) == 0)
            {
              control.Text = "Warranty Company 2: ";
              break;
            }
            break;
          case 3338749929:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Date_Manufactured", false) == 0)
            {
              control.Text = "Date Manufactured:";
              break;
            }
            break;
          case 3354038576:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Equipment_Type", false) == 0)
            {
              control.Text = "Equipment Type: ";
              break;
            }
            break;
          case 3652441439:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Serial_Number", false) == 0)
            {
              control.Text = "Serial Number: ";
              break;
            }
            break;
          case 4107503746:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Equipment_Make", false) == 0)
            {
              control.Text = "Equipment Make: ";
              break;
            }
            break;
        }
        if (e.CellElement is GridImageCellElement)
          e.CellElement.ImageLayout = ImageLayout.Stretch;
      }
      if (columnInfo != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.OwnerTemplate.Caption, "Comments", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.FieldName, "Comment", false) == 0)
        e.CellElement.Text = "<html><b>Comments:</b> ";
      if (columnInfo == null || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.OwnerTemplate.Caption, "", false) != 0 || !(e.CellElement is GridCommandCellElement))
        return;
      RadButtonElement child = (RadButtonElement) e.CellElement.Children[0];
      int num = (int) child.UnbindProperty(RadButtonItem.ImageProperty);
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.Name, "cmdDelete", false) == 0)
        child.ImagePrimitive.Image = (Image) BuilderRED.My.Resources.Resources.Delete;
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.Name, "cmdImageLink", false) == 0)
        child.ImagePrimitive.Image = (Image) BuilderRED.My.Resources.Resources.Images;
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(columnInfo.Name, "cmdCopyDetail", false) == 0)
        child.ImagePrimitive.Image = (Image) BuilderRED.My.Resources.Resources.Copy;
      child.ImagePrimitive.ScaleTransform = (SizeF) new Size(checked ((int) Math.Round((double) mdUtility.ScaleFactor)), checked ((int) Math.Round((double) mdUtility.ScaleFactor)));
      child.DisplayStyle = DisplayStyle.Image;
      child.ButtonFillElement.BackColor = Color.Transparent;
      child.ButtonFillElement.BackColor2 = Color.Transparent;
      child.ButtonFillElement.BackColor3 = Color.Transparent;
      child.ButtonFillElement.BackColor4 = Color.Transparent;
      child.ShowBorder = false;
      child.ImageAlignment = ContentAlignment.MiddleCenter;
      child.StretchHorizontally = false;
      child.Alignment = ContentAlignment.MiddleCenter;
    }

    private static void radGridView1_CreateCell(object sender, GridViewCreateCellEventArgs e)
    {
      if (!(e.Row is GridDataRowElement))
        return;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Column.OwnerTemplate.Caption, "Details", false) == 0)
      {
        string fieldName = e.Column.FieldName;
        // ISSUE: reference to a compiler-generated method
        switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(fieldName))
        {
          case 651016848:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Date2", false) == 0)
              goto label_19;
            else
              goto default;
          case 1029734518:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Control_Type_Make", false) == 0)
              goto label_20;
            else
              goto default;
          case 1539345862:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Location", false) == 0)
              goto label_20;
            else
              goto default;
          case 1698094189:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Company", false) == 0)
              goto label_20;
            else
              goto default;
          case 1717866665:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Capacity", false) == 0)
              break;
            goto default;
          case 2023210704:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Manufacturer", false) == 0)
              goto label_20;
            else
              goto default;
          case 2189814010:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Model", false) == 0)
              break;
            goto default;
          case 2342475992:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Date_Installed", false) == 0)
              break;
            goto default;
          case 2749248770:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Date", false) == 0)
              goto label_19;
            else
              goto default;
          case 2773601224:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "ID_Number", false) == 0)
              break;
            goto default;
          case 3025987981:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Warranty_Company2", false) == 0)
              break;
            goto default;
          case 3338749929:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Date_Manufactured", false) == 0)
              goto label_19;
            else
              goto default;
          case 3354038576:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Equipment_Type", false) == 0)
              break;
            goto default;
          case 3652441439:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Serial_Number", false) == 0)
              break;
            goto default;
          case 4107503746:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fieldName, "Equipment_Make", false) == 0)
              goto label_20;
            else
              goto default;
          default:
label_21:
            goto label_22;
        }
        e.CellType = typeof (Section.CustomDataCellElement);
        goto label_21;
label_19:
        e.CellType = typeof (Section.CustomDateTimeCellElement);
        goto label_21;
label_20:
        e.CellType = typeof (Section.CustomDropDownListCellElement);
        goto label_21;
      }
label_22:
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Column.OwnerTemplate.Caption, "Comments", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Column.FieldName, "Comment", false) == 0)
        e.CellType = typeof (Section.CustomCommentsCellElement);
    }

    public static List<KeyValuePair<string, List<string>>> DetailsDropDownLists { get; set; }

    private static void fillDetailsDropDownLists()
    {
      DataTable dataSource = (DataTable) mdUtility.fMainForm.DetailsGrid.DataSource;
      string str = "";
      if (dataSource.Rows.Count > 0)
        str = dataSource.Rows[0]["SD_Sec_ID"].ToString();
      Section.DetailsDropDownLists = new List<KeyValuePair<string, List<string>>>();
      try
      {
        foreach (DataColumn column in (InternalDataCollectionBase) dataSource.Columns)
        {
          if (((IEnumerable<string>) new string[4]{ "Manufacturer", "Equipment_Make", "Control_Type_Make", "Warranty_Company" }).Contains<string>(column.ColumnName))
          {
            string sSQL;
            if (mdUtility.BREDTableList.Contains((object) "RO_Section_Details"))
              sSQL = "SELECT SectionDetails." + column.ColumnName + " FROM SectionDetails UNION SELECT RO_Section_Details." + column.ColumnName + " FROM RO_Section_Details";
            else
              sSQL = "SELECT DISTINCT SectionDetails." + column.ColumnName + " FROM SectionDetails ";
            EnumerableRowCollection<DataRow> source = mdUtility.DB.GetDataTable(sSQL).AsEnumerable();
            Func<DataRow, string> selector;
            // ISSUE: reference to a compiler-generated field
            if (Section._Closure\u0024__.\u0024I23\u002D0 != null)
            {
              // ISSUE: reference to a compiler-generated field
              selector = Section._Closure\u0024__.\u0024I23\u002D0;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              Section._Closure\u0024__.\u0024I23\u002D0 = selector = (Func<DataRow, string>) (x => x[0].ToString());
            }
            List<string> list = source.Select<DataRow, string>(selector).ToList<string>();
            Section.DetailsDropDownLists.Add(new KeyValuePair<string, List<string>>(column.ColumnName, list));
          }
          if (((IEnumerable<string>) new string[1]{ "Location" }).Contains<string>(column.ColumnName))
          {
            EnumerableRowCollection<DataRow> source = mdUtility.DB.GetDataTable(dataSource.Rows.Count != 0 ? "SELECT DISTINCT Location FROM Samples_by_sections where [SEC_ID] = {" + str + "}" : "SELECT DISTINCT SectionDetails." + column.ColumnName + " FROM SectionDetails ").AsEnumerable();
            Func<DataRow, string> selector;
            // ISSUE: reference to a compiler-generated field
            if (Section._Closure\u0024__.\u0024I23\u002D1 != null)
            {
              // ISSUE: reference to a compiler-generated field
              selector = Section._Closure\u0024__.\u0024I23\u002D1;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              Section._Closure\u0024__.\u0024I23\u002D1 = selector = (Func<DataRow, string>) (x => x[0].ToString());
            }
            List<string> list = source.Select<DataRow, string>(selector).ToList<string>();
            Section.DetailsDropDownLists.Add(new KeyValuePair<string, List<string>>(column.ColumnName, list));
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

    internal static void LockSection(bool Lock)
    {
      mdUtility.fMainForm.txtSectionAmount.ReadOnly = Lock;
      mdUtility.fMainForm.txtSectionYearBuilt.ReadOnly = Lock;
      mdUtility.fMainForm.dtPainted.ReadOnly = Lock;
    }

    internal static void DeleteSection(string strSectionID)
    {
      try
      {
        DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT [SD_ID] FROM SectionDetails WHERE [sd_sec_id]={" + strSectionID + "}");
        try
        {
          foreach (DataRow row in dataTable1.Rows)
            SectionDetail.DeleteSectionDetail(row["sd_id"].ToString());
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        mdUtility.DeleteInspectionBySection(strSectionID);
        string str = "SELECT * FROM Component_Section WHERE [sec_id]={" + strSectionID + "}";
        DataTable dataTable2 = mdUtility.DB.GetDataTable(str);
        if (dataTable2.Rows.Count == 0)
          throw new Exception("Unable to delete the section.  Section was not found.");
        DataRow row1 = dataTable2.Rows[0];
        if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row1["BRED_Status"], (object) "N", false))
          row1["BRED_Status"] = (object) "D";
        else
          row1.Delete();
        mdUtility.DB.SaveDataTable(ref dataTable2, str);
        if (mdUtility.fMainForm.tvInventory.GetNodeByKey(strSectionID) != null)
          mdUtility.fMainForm.tvInventory.GetNodeByKey(strSectionID).Parent.Nodes.Remove(mdUtility.fMainForm.tvInventory.GetNodeByKey(strSectionID));
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strSectionID) != null)
        {
          frmMain fMainForm = mdUtility.fMainForm;
          UltraTreeNode nodeByKey = mdUtility.fMainForm.tvInspection.GetNodeByKey(strSectionID);
          ref UltraTreeNode local = ref nodeByKey;
          fMainForm.PurgeInspectionNode(ref local);
        }
        string[] strArray = new string[1];
        foreach (UltraTreeNode node in mdUtility.fMainForm.tvInspection.Nodes)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(node.Tag, (object) strSectionID, false))
          {
            strArray = (string[]) Utils.CopyArray((Array) strArray, (Array) new string[checked (Information.UBound((Array) strArray, 1) + 1 + 1)]);
            strArray[Information.UBound((Array) strArray, 1)] = node.Key;
          }
        }
        if (Information.UBound((Array) strArray, 1) > 0)
        {
          int num = Information.UBound((Array) strArray, 1);
          int index = 1;
          while (index <= num)
          {
            mdUtility.fMainForm.tvInspection.Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(strArray[index]));
            checked { ++index; }
          }
        }
        EfficiencyAssessment.DeleteEntry(strSectionID);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        Exception exception = ex;
        if (Information.Err().Number != 35601)
          throw exception;
        ProjectData.ClearProjectError();
      }
    }

    internal static bool SaveSection(string SectionID)
    {
      bool flag;
      try
      {
        switch (mdUtility.fMainForm.SectionPageView.Pages.IndexOf(mdUtility.fMainForm.SectionPageView.SelectedPage))
        {
          case 0:
            flag = Section.SaveSectionInfo(SectionID);
            break;
          case 1:
            flag = Section.SaveSectionDetails(SectionID);
            break;
        }
        frmMain fMainForm = mdUtility.fMainForm;
        fMainForm.m_bSectionNeedToSave = false;
        fMainForm.m_bSectionYearChanged = false;
        fMainForm.m_bSectionLoaded = false;
        Section.LoadSection(mdUtility.fMainForm.CurrentSection, false);
        fMainForm.m_bSectionLoaded = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Section), nameof (SaveSection));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private static bool SaveSectionDetails(string SectionID)
    {
      object obj = (object) ("SELECT * FROM [SectionDetails] WHERE [SD_SEC_ID] = {" + SectionID + "}");
      DataTable dataSource = (DataTable) mdUtility.fMainForm.DetailsGrid.DataSource;
      try
      {
        foreach (DataRow row in dataSource.Rows)
        {
          string Left = !row.HasVersion(DataRowVersion.Original) ? Conversions.ToString(row["BRED_Status"]) : Conversions.ToString(Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status", DataRowVersion.Original])) ? (object) null : row["BRED_Status", DataRowVersion.Original]);
          switch (row.RowState)
          {
            case DataRowState.Deleted:
              if (Left == null || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "U", false) == 0)
              {
                row.RejectChanges();
                row["BRED_Status"] = (object) "D";
                break;
              }
              break;
            case DataRowState.Modified:
              if (Left == null)
              {
                row["BRED_Status"] = (object) "U";
                break;
              }
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
      mdUtility.DB.SaveDataTable(ref dataSource, Conversions.ToString(obj));
      return true;
    }

    private static bool SaveSectionInfo(string SectionID)
    {
      bool flag1;
      if (!Section.OkToSave(SectionID))
      {
        flag1 = false;
      }
      else
      {
        frmMain fMainForm = mdUtility.fMainForm;
        DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM [Section Info] WHERE [sec_id]={" + SectionID + "}");
        string str1 = "SELECT * FROM Component_Section WHERE [sec_id]={" + SectionID + "}";
        DataTable dataTable2 = mdUtility.DB.GetDataTable(str1);
        if (dataTable2.Rows.Count == 0)
          throw new Exception("Unable to save the section.  The section was not found.");
        DataRow row = dataTable2.Rows[0];
        if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
          row["BRED_Status"] = (object) "U";
        if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["sec_name"])))
          row["sec_name"] = (object) string.Empty;
        bool flag2;
        if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.OrObject(Microsoft.VisualBasic.CompilerServices.Operators.OrObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual(row["sec_name"], (object) fMainForm.cboSectionName.SelectedText, false), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual((object) Section.MaterialCategory(Section.SectionCMC(SectionID)), fMainForm.cboSectionMaterial.SelectedValue, false)), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual((object) Section.ComponentType(Section.SectionCMC(SectionID)), fMainForm.cboSectionComponentType.SelectedValue, false))))
          flag2 = true;
        if (Microsoft.VisualBasic.Strings.Len(fMainForm.cboSectionName.Text) > 50)
        {
          int num = (int) Interaction.MsgBox((object) "The section name can be no more than 50 characters.  \r\nThe section name will be truncated to 50 characters.", MsgBoxStyle.Information, (object) null);
          row["sec_name"] = (object) Microsoft.VisualBasic.Strings.Left(fMainForm.cboSectionName.SelectedText, 50);
        }
        else
          row["sec_name"] = (object) fMainForm.cboSectionName.Text;
        row["Sec_CMC_Link"] = RuntimeHelpers.GetObjectValue(fMainForm.cboSectionComponentType.SelectedValue);
        row["sec_qty"] = !fMainForm.miUnits.Checked ? (object) Conversions.ToDouble(Microsoft.VisualBasic.Strings.Trim(fMainForm.txtSectionAmount.Text)) : Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) Conversions.ToDouble(Microsoft.VisualBasic.Strings.Trim(fMainForm.txtSectionAmount.Text)), dataTable1.Rows[0]["uom_conv"]);
        long year = Section.ConvertTextToYear(fMainForm.txtSectionYearBuilt.Text);
        row["sec_year_built"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(year > 0L, (object) year, (object) DBNull.Value));
        if (fMainForm.cboSectionStatus.SelectedValue != null)
          row["SEC_Status_ID"] = RuntimeHelpers.GetObjectValue(fMainForm.cboSectionStatus.SelectedValue);
        row["SEC_FunctionalArea_ID"] = RuntimeHelpers.GetObjectValue(Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(fMainForm.cboFunctionalArea.SelectedValue, (object) null, false) ? (object) DBNull.Value : fMainForm.cboFunctionalArea.SelectedValue);
        if (fMainForm.chkYearEstimated.CheckState == CheckState.Checked)
        {
          if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.OrObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(row["SEC_Date_Source"], (object) "Input", false), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(row["SEC_Date_Source"], (object) "Roofer", false))))
            row["SEC_Date_Source"] = (object) "Estimated";
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["SEC_Date_Source"], (object) "Roofer", false))
          row["SEC_Date_Source"] = (object) "Input";
        if (fMainForm.chkPainted.CheckState == CheckState.Checked)
        {
          row["sec_paint"] = (object) true;
          row["sec_date_painted"] = Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.dtPainted.Text, "", false) != 0 ? (object) fMainForm.dtPainted.Text : (object) DBNull.Value;
          row["sec_paint_link"] = !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(fMainForm.cboSectionPaintType.SelectedValue, (object) "", false) ? RuntimeHelpers.GetObjectValue(fMainForm.cboSectionPaintType.SelectedValue) : (object) -1;
        }
        else
        {
          row["sec_paint"] = (object) false;
          row["sec_date_painted"] = (object) DBNull.Value;
          row["sec_paint_link"] = (object) -1;
        }
        if (mdUtility.UseEnergyForm)
        {
          if (fMainForm.chkEnergyAuditRequired.CheckState == CheckState.Checked)
            EfficiencyAssessment.CreateEntry(mdUtility.fMainForm.CurrentBldg, SectionID);
          else
            EfficiencyAssessment.DeleteEntry(SectionID);
        }
        mdUtility.DB.SaveDataTable(ref dataTable2, str1);
        DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT Inspection_Data.INSP_DATA_SAMP, Sample_Data.SAMP_DATA_ID, Inspection_Data.INSP_DATA_SEC_ID FROM Inspection_Data INNER JOIN Sample_Data ON Inspection_Data.INSP_DATA_ID = Sample_Data.SAMP_DATA_INSP_DATA_ID WHERE Inspection_Data.INSP_DATA_SAMP=False AND Inspection_Data.INSP_DATA_SEC_ID = {" + row["sec_id"].ToString() + "}");
        if (dataTable3.Rows.Count > 0)
        {
          string str2 = "SELECT * FROM Sample_Data WHERE SAMP_DATA_ID = {" + dataTable3.Rows[0]["SAMP_DATA_ID"].ToString() + "}";
          DataTable dataTable4 = mdUtility.DB.GetDataTable(str2);
          dataTable4.Rows[0]["SAMP_DATA_QTY"] = RuntimeHelpers.GetObjectValue(row["SEC_QTY"]);
          mdUtility.DB.SaveDataTable(ref dataTable4, str2);
        }
        if (flag2)
          mdUtility.fMainForm.UpdateTreeLabels(mdUtility.fMainForm.CurrentSection);
        flag1 = true;
      }
      return flag1;
    }

    internal static string SectionLabel(string SectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [SectionName] FROM [SectionName] WHERE [SEC_ID]={" + SectionID + "}");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToString(dataTable.Rows[0]["SectionName"]);
      return "";
    }

    internal static int SectionMaterialLink(string SectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT MAT_CAT_ID FROM [Section Info] WHERE [SEC_ID]={" + SectionID + "}");
      if (dataTable.Rows.Count > 0 && (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["MAT_CAT_ID"])) && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(dataTable.Rows[0]["MAT_CAT_ID"]), "", false) > 0U))
        return Conversions.ToInteger(dataTable.Rows[0]["MAT_CAT_ID"]);
      return -1;
    }

    private static bool OkToSave(string SectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM Component_Section WHERE SEC_ID = {" + SectionID + "}");
      bool flag;
      if (mdUtility.DB.GetDataTable(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) ("SELECT * FROM Component_Section WHERE SEC_ID <> {" + SectionID + "}AND ([BRED_Status]<>'D' OR [BRED_Status] IS NULL) AND [SEC_SYS_COMP_ID]={" + dataTable.Rows[0]["SEC_SYS_COMP_ID"].ToString() + "} AND SEC_NAME='" + mdUtility.fMainForm.cboSectionName.Text + "' AND [SEC_CMC_LINK]="), dataTable.Rows[0]["SEC_CMC_LINK"]))).Rows.Count > 0)
      {
        int num = (int) Interaction.MsgBox((object) "You cannot have a section with the same name, material, and component type as an another section", MsgBoxStyle.OkOnly, (object) null);
        flag = false;
      }
      else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtSectionAmount.Text, "", false) > 0U && !Versioned.IsNumeric((object) mdUtility.fMainForm.txtSectionAmount.Text))
      {
        int num = (int) Interaction.MsgBox((object) "Please enter a valid number for the quantity", MsgBoxStyle.Critical, (object) null);
        flag = false;
      }
      else
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtSectionYearBuilt.Text, "", false) > 0U)
        {
          if (!Versioned.IsNumeric((object) mdUtility.fMainForm.txtSectionYearBuilt.Text))
          {
            int num = (int) Interaction.MsgBox((object) "Please enter a valid year for the Section's built/installed year.", MsgBoxStyle.Critical, (object) "Missing required field");
            flag = false;
            goto label_19;
          }
          else if (Conversions.ToInteger(mdUtility.fMainForm.txtSectionYearBuilt.Text) < 1776 | Conversions.ToInteger(mdUtility.fMainForm.txtSectionYearBuilt.Text) > 2100)
          {
            int num = (int) Interaction.MsgBox((object) "Please enter a year between 1776 and 2100 (all four digits) for the Section's built/installed year.", MsgBoxStyle.Critical, (object) "Invalid data format.");
            flag = false;
            goto label_19;
          }
          else if (Building.BuiltYear(mdUtility.fMainForm.CurrentBldg) > Conversions.ToInteger(mdUtility.fMainForm.txtSectionYearBuilt.Text))
          {
            int num = (int) Interaction.MsgBox((object) "The Section's built/installed year cannot be earlier than the building's constructiond date.", MsgBoxStyle.Critical, (object) null);
            flag = false;
            goto label_19;
          }
        }
        if (mdUtility.fMainForm.chkPainted.CheckState == CheckState.Checked && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.dtPainted.Text, "", false) > 0U)
        {
          if (!Versioned.IsNumeric((object) mdUtility.fMainForm.dtPainted.Text))
          {
            int num = (int) Interaction.MsgBox((object) "Please enter a valid year for this field.", MsgBoxStyle.OkOnly, (object) null);
            flag = false;
            goto label_19;
          }
          else if (Conversions.ToInteger(mdUtility.fMainForm.dtPainted.Text) > DateTime.Now.Year)
          {
            int num = (int) Interaction.MsgBox((object) "Year painted cannot be more than current date.", MsgBoxStyle.OkOnly, (object) null);
            flag = false;
            goto label_19;
          }
          else if (Conversions.ToInteger(mdUtility.fMainForm.dtPainted.Text) < Conversions.ToInteger(mdUtility.fMainForm.txtSectionYearBuilt.Text) | Conversions.ToInteger(mdUtility.fMainForm.dtPainted.Text) < 1776)
          {
            int num = (int) Interaction.MsgBox((object) "Year painted cannot be less than year built or 1776.", MsgBoxStyle.Critical, (object) "Invalid Paint Year");
            flag = false;
            mdUtility.fMainForm.dtPainted.Focus();
            goto label_19;
          }
        }
        flag = true;
      }
label_19:
      return flag;
    }

    internal static string SectionComponentLink(string SectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT Sec_Sys_Comp_ID FROM Component_Section WHERE [Sec_ID]={" + SectionID + "}");
      if (dataTable.Rows.Count > 0)
        return dataTable.Rows[0]["Sec_Sys_Comp_ID"].ToString();
      return "";
    }

    internal static long SectionCMC(string SectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT SEC_CMC_LINK FROM Component_Section WHERE [Sec_ID]={" + SectionID + "}");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToLong(dataTable.Rows[0]["SEC_CMC_LINK"]);
      return -1;
    }

    internal static int MaterialCategory(long lCMC)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT CMC_MCAT_LINK FROM [RO_CMC] WHERE [CMC_ID]= " + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) lCMC)));
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(dataTable.Rows[0]["CMC_MCAT_LINK"]);
      return -1;
    }

    internal static int ComponentType(long lCMC)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT CMC_CTYPE_LINK FROM [RO_CMC] WHERE [CMC_ID]=" + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) lCMC)));
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(dataTable.Rows[0]["CMC_CTYPE_LINK"]);
      return -1;
    }

    internal static long CMCByComponentMaterialAndType(int iComp, int iMat, int iType)
    {
      string sSQL;
      if (mdUtility.UseUniformat)
        sSQL = "SELECT CMC_ID FROM [RO_CMC] WHERE [CMC_MCAT_LINK]=" + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) iMat)) + " AND [CMC_CTYPE_LINK] = " + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) iType)) + " AND [CMC_COMP_UII_LINK] = " + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) iComp));
      else
        sSQL = "SELECT CMC_ID FROM [RO_CMC] WHERE [CMC_MCAT_LINK]=" + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) iMat)) + " AND [CMC_CTYPE_LINK] = " + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) iType)) + " AND [CMC_COMP_LINK] = " + Microsoft.VisualBasic.Strings.Trim(Conversion.Str((object) iComp));
      DataTable dataTable = mdUtility.DB.GetDataTable(sSQL);
      if (dataTable.Rows.Count > 0)
        return Conversions.ToLong(dataTable.Rows[0]["CMC_ID"]);
      return -1;
    }

    private static long ConvertTextToYear(string strYear)
    {
      long num1 = 0;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strYear, "", false) > 0U && Versioned.IsNumeric((object) strYear))
      {
        long num2 = Conversions.ToLong(strYear);
        if (num2 > 1800L & num2 < (long) checked (DateAndTime.Year(DateAndTime.Now) + 1))
          num1 = num2;
      }
      return num1;
    }

    public class CustomDateTimeCellElement : GridDateTimeCellElement
    {
      private System.Windows.Forms.Label m_lblHeader;
      private RadDateTimePicker m_dateValue;

      public CustomDateTimeCellElement(GridViewColumn column, GridRowElement row)
        : base(column, row)
      {
        this.m_dateValue.Format = DateTimePickerFormat.Short;
        if ((object) this.m_lblHeader.Text == (object) "Date Manufactured:")
          this.m_dateValue.MaxDate = DateTime.Now.Date;
        if (this.Value != null && this.Value != DBNull.Value)
          this.m_dateValue.Value = Conversions.ToDate(this.Value);
        this.m_dateValue.ValueChanged += new EventHandler(this.RadElement_Changed);
      }

      protected override void CreateChildElements()
      {
        base.CreateChildElements();
        this.m_lblHeader = new System.Windows.Forms.Label();
        this.m_lblHeader.Padding = new Padding(0, 0, 0, 3);
        this.m_dateValue = new RadDateTimePicker();
        this.m_dateValue.Margin = new Padding(0);
        this.m_dateValue.Dock = DockStyle.Fill;
        this.m_dateValue.Value = DateTime.MinValue;
        this.m_dateValue.Format = DateTimePickerFormat.Short;
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
        tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        this.m_lblHeader.Width = checked ((int) Math.Round(unchecked ((double) mdUtility.fMainForm.CreateGraphics().MeasureString("Warranty Company 2", mdUtility.fMainForm.Font).Width + 20.0)));
        tableLayoutPanel.ColumnStyles[0].SizeType = SizeType.AutoSize;
        this.m_lblHeader.TextAlign = ContentAlignment.MiddleRight;
        tableLayoutPanel.Controls.Add((Control) this.m_lblHeader, 0, 0);
        tableLayoutPanel.Controls.Add((Control) this.m_dateValue, 1, 0);
        this.Children.Add((RadElement) new RadHostItem((Control) tableLayoutPanel));
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (GridDataCellElement);
        }
      }

      private void RadElement_Changed(object sender, EventArgs e)
      {
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.Value)) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.Value, (object) this.m_dateValue.Value, false))
          return;
        this.m_dateValue.ValueChanged -= new EventHandler(this.RadElement_Changed);
        if (this.m_dateValue.Value.Equals(this.m_dateValue.NullDate))
          this.Value = (object) null;
        else
          this.Value = (object) this.m_dateValue.Value.Date;
        IEditableObject dataBoundItem = this.RowInfo.DataBoundItem as IEditableObject;
        if (dataBoundItem != null)
          dataBoundItem.EndEdit();
        mdUtility.fMainForm.SetSectionChanged(true);
        this.m_dateValue.ValueChanged += new EventHandler(this.RadElement_Changed);
      }
    }

    public class CustomDataCellElement : GridDataCellElement
    {
      private System.Windows.Forms.Label m_lblHeader;
      private System.Windows.Forms.TextBox m_txtValue;

      public CustomDataCellElement(GridViewColumn column, GridRowElement row)
        : base(column, row)
      {
        if (this.Value != null && this.Value != DBNull.Value)
          this.m_txtValue.Text = Conversions.ToString(this.Value);
        this.m_txtValue.TextChanged += new EventHandler(this.RadElement_Changed);
      }

      protected override void CreateChildElements()
      {
        base.CreateChildElements();
        this.m_lblHeader = new System.Windows.Forms.Label();
        this.m_lblHeader.Padding = new Padding(0, 0, 0, 3);
        this.m_txtValue = new System.Windows.Forms.TextBox();
        this.m_txtValue.Margin = new Padding(0);
        this.m_txtValue.Dock = DockStyle.Fill;
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
        tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        this.m_lblHeader.Width = checked ((int) Math.Round(unchecked ((double) mdUtility.fMainForm.CreateGraphics().MeasureString("Warranty Company 2", mdUtility.fMainForm.Font).Width + 20.0)));
        tableLayoutPanel.ColumnStyles[0].SizeType = SizeType.AutoSize;
        this.m_lblHeader.TextAlign = ContentAlignment.MiddleRight;
        tableLayoutPanel.Controls.Add((Control) this.m_lblHeader, 0, 0);
        tableLayoutPanel.Controls.Add((Control) this.m_txtValue, 1, 0);
        this.Children.Add((RadElement) new RadHostItem((Control) tableLayoutPanel));
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (GridDataCellElement);
        }
      }

      private void RadElement_Changed(object sender, EventArgs e)
      {
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.Value)) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.Value, (object) this.m_txtValue.Text, false))
          return;
        this.m_txtValue.TextChanged -= new EventHandler(this.RadElement_Changed);
        IEditableObject dataBoundItem = this.RowInfo.DataBoundItem as IEditableObject;
        if (dataBoundItem != null)
        {
          int maxLength = ((DataRowView) dataBoundItem).DataView.Table.Columns[this.ColumnIndex].MaxLength;
          if (this.m_txtValue.Text.Length > maxLength)
          {
            int num = (int) MessageBox.Show("Value cannot be longer than " + Conversions.ToString(maxLength) + " characters!");
            this.m_txtValue.Text = Conversions.ToString(this.Value);
          }
          else
          {
            this.Value = (object) this.m_txtValue.Text;
            dataBoundItem.EndEdit();
          }
          mdUtility.fMainForm.SetSectionChanged(true);
        }
        this.m_txtValue.TextChanged += new EventHandler(this.RadElement_Changed);
      }
    }

    public class CustomDropDownListCellElement : GridDataCellElement
    {
      private List<string> dropDownList;
      private System.Windows.Forms.Label m_lblHeader;
      private RadDropDownList m_ddlValue;

      public CustomDropDownListCellElement(GridViewColumn column, GridRowElement row)
        : base(column, row)
      {
        this.dropDownList = Section.DetailsDropDownLists.Find((Predicate<KeyValuePair<string, List<string>>>) (x => Microsoft.VisualBasic.CompilerServices.Operators.CompareString(x.Key, this.ColumnInfo.Name, false) == 0)).Value;
        this.m_ddlValue.BindingContext = new BindingContext();
        this.m_ddlValue.DataSource = (object) this.dropDownList;
        this.m_ddlValue.Text = this.Value == null || this.Value == DBNull.Value ? "" : Conversions.ToString(this.Value);
        this.m_ddlValue.TabStop = true;
      }

      protected override void OnLoaded()
      {
        base.OnLoaded();
        this.m_ddlValue.TextChanged += new EventHandler(this.RadElement_Changed);
      }

      protected override void CreateChildElements()
      {
        base.CreateChildElements();
        this.m_lblHeader = new System.Windows.Forms.Label();
        this.m_lblHeader.Padding = new Padding(0, 0, 0, 3);
        this.m_ddlValue = new RadDropDownList();
        this.m_ddlValue.Margin = new Padding(0);
        this.m_ddlValue.Dock = DockStyle.Fill;
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
        tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        this.m_lblHeader.Width = checked ((int) Math.Round(unchecked ((double) mdUtility.fMainForm.CreateGraphics().MeasureString("Warranty Company 2", mdUtility.fMainForm.Font).Width + 20.0)));
        tableLayoutPanel.ColumnStyles[0].SizeType = SizeType.AutoSize;
        this.m_lblHeader.TextAlign = ContentAlignment.MiddleRight;
        tableLayoutPanel.Controls.Add((Control) this.m_lblHeader, 0, 0);
        tableLayoutPanel.Controls.Add((Control) this.m_ddlValue, 1, 0);
        this.Children.Add((RadElement) new RadHostItem((Control) tableLayoutPanel));
        this.m_ddlValue.DropDownStyle = RadDropDownStyle.DropDown;
        this.m_ddlValue.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      }

      protected override void SetContentCore(object value)
      {
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (GridDataCellElement);
        }
      }

      private void RadElement_Changed(object sender, EventArgs e)
      {
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.Value)) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.Value, (object) this.m_ddlValue.Text, false))
          return;
        this.m_ddlValue.TextChanged -= new EventHandler(this.RadElement_Changed);
        if (this.RowInfo != null)
        {
          IEditableObject dataBoundItem = this.RowInfo.DataBoundItem as IEditableObject;
          if (dataBoundItem != null)
          {
            int maxLength = ((DataRowView) dataBoundItem).DataView.Table.Columns[this.ColumnIndex].MaxLength;
            if (this.m_ddlValue.Text.Length > maxLength)
            {
              int num = (int) MessageBox.Show("Value cannot be longer than " + Conversions.ToString(maxLength) + " characters!");
              this.m_ddlValue.Text = Conversions.ToString(this.Value);
            }
            else
            {
              this.Value = (object) this.m_ddlValue.Text;
              dataBoundItem.EndEdit();
            }
          }
        }
        mdUtility.fMainForm.SetSectionChanged(true);
        this.m_ddlValue.TextChanged += new EventHandler(this.RadElement_Changed);
      }
    }

    public class CustomCommentsCellElement : GridDataCellElement
    {
      private ctrlComments m_CommentsControl;

      public CustomCommentsCellElement(GridViewColumn column, GridRowElement row)
        : base(column, row)
      {
        if (this.Value != null && this.Value != DBNull.Value)
          this.m_CommentsControl.TextBox.Text = Conversions.ToString(this.Value);
        this.m_CommentsControl.TextBox.TextChanged += new EventHandler(this.RadElement_Changed);
      }

      protected override void CreateChildElements()
      {
        base.CreateChildElements();
        this.m_CommentsControl = new ctrlComments();
        this.Children.Add((RadElement) new RadHostItem((Control) this.m_CommentsControl));
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (GridDataCellElement);
        }
      }

      private void RadElement_Changed(object sender, EventArgs e)
      {
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.Value)) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.Value, (object) this.m_CommentsControl.TextBox.Text, false))
          return;
        this.m_CommentsControl.TextBox.TextChanged -= new EventHandler(this.RadElement_Changed);
        this.Value = (object) this.m_CommentsControl.TextBox.Text;
        IEditableObject dataBoundItem = this.RowInfo.DataBoundItem as IEditableObject;
        if (dataBoundItem != null)
          dataBoundItem.EndEdit();
        mdUtility.fMainForm.SetSectionChanged(true);
        this.m_CommentsControl.TextBox.TextChanged += new EventHandler(this.RadElement_Changed);
      }
    }
  }
}
