// Decompiled with JetBrains decompiler
// Type: BuilderRED.Inspection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class Inspection
  {
    public static int IInspectionWindow
    {
      get
      {
        return Conversions.ToInteger(mdUtility.get_ConfigValue("InspWindowInDays"));
      }
    }

    internal static void ClearInspections()
    {
      frmMain fMainForm = mdUtility.fMainForm;
      mdUtility.fMainForm.m_bInspLoaded = false;
      Inspection.ClearInspectionConditionButtons();
      fMainForm.m_bDDLoad = true;
      fMainForm.cboLocation.DataSource = (object) null;
      fMainForm.cboLocation.SelectedIndex = -1;
      fMainForm.m_bDDLoad = false;
      fMainForm.txtSQuantity.Text = "";
      fMainForm.lblPCInspValue.Text = "";
      fMainForm.optRatingType_1.Checked = false;
      fMainForm.optRatingType_2.Checked = false;
      fMainForm.optRatingType_3.Checked = false;
      fMainForm.optMethod_0.Checked = false;
      fMainForm.optMethod_1.Checked = false;
      fMainForm.chkSampPainted.CheckState = CheckState.Unchecked;
      fMainForm.chkSampNonRep.CheckState = CheckState.Unchecked;
      fMainForm.chkDefFree.CheckState = CheckState.Unchecked;
      fMainForm.chkPaintDefFree.CheckState = CheckState.Unchecked;
      fMainForm.frmLocation.Visible = false;
      fMainForm.frmDirectRating.Visible = false;
      fMainForm.frmDistressSurvey.Visible = false;
      mdUtility.fMainForm.m_bInspLoaded = true;
    }

    internal static void ClearInspectionConditionButtons()
    {
      mdUtility.fMainForm.optCompRating_1.Checked = false;
      mdUtility.fMainForm.optCompRating_2.Checked = false;
      mdUtility.fMainForm.optCompRating_3.Checked = false;
      mdUtility.fMainForm.optCompRating_4.Checked = false;
      mdUtility.fMainForm.optCompRating_5.Checked = false;
      mdUtility.fMainForm.optCompRating_6.Checked = false;
      mdUtility.fMainForm.optCompRating_7.Checked = false;
      mdUtility.fMainForm.optCompRating_8.Checked = false;
      mdUtility.fMainForm.optCompRating_9.Checked = false;
      mdUtility.fMainForm.optPaintRating_1.Checked = false;
      mdUtility.fMainForm.optPaintRating_2.Checked = false;
      mdUtility.fMainForm.optPaintRating_3.Checked = false;
      mdUtility.fMainForm.optPaintRating_4.Checked = false;
      mdUtility.fMainForm.optPaintRating_5.Checked = false;
      mdUtility.fMainForm.optPaintRating_6.Checked = false;
      mdUtility.fMainForm.optPaintRating_7.Checked = false;
      mdUtility.fMainForm.optPaintRating_8.Checked = false;
      mdUtility.fMainForm.optPaintRating_9.Checked = false;
    }

    internal static void LoadInspectionDates(string strSectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM [Section Info] WHERE [sec_id]={" + strSectionID + "}");
      if (dataTable.Rows.Count == 0)
        throw new Exception("Unable to load Inspection.  Unable to find section information.");
      DataRow row = dataTable.Rows[0];
      frmMain fMainForm = mdUtility.fMainForm;
      fMainForm.txtComponent.Text = Conversions.ToString(row["comp_desc"]);
      fMainForm.lblMaterialCategory.Text = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(row["mat_cat_lbl"], (object) ":"));
      fMainForm.txtMaterialCategory.Text = Conversions.ToString(row["mat_cat_desc"]);
      fMainForm.txtComponentType.Text = Conversions.ToString(row["comp_type_desc"]);
      if (fMainForm.miUnits.Checked)
      {
        fMainForm.lblSecQtyValue.Text = Support.Format(RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(row["eng_qty"]), "", false) > 0U, RuntimeHelpers.GetObjectValue(row["eng_qty"]), (object) "")), "##,##0", Microsoft.VisualBasic.FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
        fMainForm.lblSecQtyUM.Text = Conversions.ToString(row["uom_eng_unit_abbr"]);
      }
      else
      {
        fMainForm.lblSecQtyValue.Text = Support.Format(RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(row["met_qty"]), "", false) > 0U, RuntimeHelpers.GetObjectValue(row["met_qty"]), (object) "")), "##,##0", Microsoft.VisualBasic.FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
        fMainForm.lblSecQtyUM.Text = Conversions.ToString(row["uom_met_unit_abbr"]);
      }
      fMainForm.chkSampPainted.Enabled = Conversions.ToBoolean(row["SEC_PAINT"]);
      fMainForm.frmDirectPaint.Visible = Conversions.ToBoolean(row["SEC_PAINT"]);
      string sSQL;
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(fMainForm.tvInspection.GetNodeByKey(fMainForm.CurrentSection).Parent.Parent.Parent.Tag, (object) "Non-sampling", false))
      {
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation) != null)
          sSQL = "SELECT DISTINCT [insp_data_id], insp_data_insp_date, insp_date FROM qryInspectionList WHERE [insp_data_sec_id]={" + strSectionID + "} AND [INSP_DATA_SAMP] = TRUE AND [SAMP_DATA_LOC]={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "} AND (BRED_Status <> 'D' OR BRED_Status IS NULL) ORDER BY insp_data_insp_date DESC";
        else
          sSQL = "SELECT DISTINCT [insp_data_id], insp_data_insp_date, insp_date FROM qryInspectionList WHERE [insp_data_sec_id]={" + strSectionID + "} AND (BRED_Status <> 'D' OR BRED_Status IS NULL) ORDER BY insp_data_insp_date DESC";
      }
      else
        sSQL = "SELECT DISTINCT [insp_data_id], insp_data_insp_date, insp_date FROM qryInspectionList WHERE [insp_data_sec_id]={" + strSectionID + "} AND [INSP_DATA_SAMP] = true AND SAMP_DATA_LOC={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "} AND (BRED_Status <> 'D' OR BRED_Status IS NULL) ORDER BY insp_data_insp_date DESC";
      fMainForm.m_bDDLoad = true;
      mdUtility.LoadMstrTable("InspectionDates", sSQL);
      fMainForm.cboInspectionDates.DisplayMember = "insp_date";
      fMainForm.cboInspectionDates.ValueMember = "insp_data_id";
      fMainForm.cboInspectionDates.DataSource = (object) mdUtility.get_MstrTable("InspectionDates");
      fMainForm.cboInspectionDates.SelectedIndex = -1;
      fMainForm.m_bDDLoad = false;
      if (Inspection.CanAddInspection(strSectionID))
      {
        fMainForm.cmdNewInspection.Enabled = true;
        if (mdUtility.get_MstrTable("InspectionDates").Rows.Count == 0)
          fMainForm.cmdCopyInspection.Enabled = false;
        else
          fMainForm.cmdCopyInspection.Enabled = true;
      }
      else
      {
        fMainForm.cmdNewInspection.Enabled = false;
        fMainForm.cmdCopyInspection.Enabled = false;
      }
      if ((uint) mdUtility.get_MstrTable("InspectionDates").Rows.Count > 0U)
      {
        fMainForm.cboInspectionDates.Enabled = true;
        fMainForm.lblNoInspection.Visible = false;
        fMainForm.cboInspectionDates.SelectedIndex = 0;
      }
      else
      {
        fMainForm.cboInspectionDates.Enabled = false;
        Inspection.ClearInspections();
        if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectGreater(mdUtility.DB.GetDataTable("SELECT Count(INSP_DATA_ID) FROM Inspection_Data WHERE [insp_data_sec_id]={" + strSectionID + "}").Rows[0][0], (object) 0, false))
          fMainForm.lblNoInspection.Visible = false;
        else
          fMainForm.lblNoInspection.Visible = true;
        fMainForm.CanEditInspection = false;
      }
    }

    private static bool CanAddInspection(string SectionID)
    {
      DateTime dateTime = DateAndTime.DateAdd(DateInterval.Day, (double) checked (-1 * Inspection.IInspectionWindow), DateAndTime.Today);
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [INSP_DATA_ID] FROM Inspection_Data WHERE [insp_data_sec_id]={" + SectionID + "} AND ([INSP_DATA_INSP_DATE]>=#" + Conversions.ToString(dateTime) + "# AND [INSP_Source] <> 'InstallDate') AND (BRED_Status <> 'D' OR BRED_Status IS NULL)");
      if (dataTable.Rows.Count == 0)
        return true;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.CurrentLocation.ToString(), "", false) <= 0U)
        return false;
      return Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(mdUtility.DB.GetDataTable("SELECT Count(SAMP_DATA_ID) FROM Sample_Data WHERE [SAMP_DATA_INSP_DATA_ID]={" + dataTable.Rows[0]["INSP_DATA_ID"].ToString() + "} AND SAMP_DATA_LOC={" + Strings.Replace(mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation).Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "}").Rows[0][0], (object) 0, false);
    }

    internal static void LoadInspection(string InspectionID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM Inspection_Data WHERE [insp_data_id]={" + InspectionID + "}");
      Inspection.ClearInspections();
      if (dataTable1.Rows.Count <= 0)
        return;
      mdUtility.fMainForm.m_bInspLoaded = false;
      DataRow row = dataTable1.Rows[0];
      mdUtility.fMainForm.CanEditInspection = Inspection.CanEditInspection(InspectionID);
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["insp_data_type"], (object) 1, false))
        mdUtility.fMainForm.optRatingType_1.Checked = true;
      else if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["insp_data_type"], (object) 2, false))
        mdUtility.fMainForm.optRatingType_2.Checked = true;
      else
        mdUtility.fMainForm.optRatingType_3.Checked = true;
      if (Conversions.ToBoolean(row["insp_data_samp"]))
        mdUtility.fMainForm.optMethod_1.Checked = true;
      else
        mdUtility.fMainForm.optMethod_0.Checked = true;
      mdUtility.fMainForm.lblSampQty.Text = mdUtility.fMainForm.lblSecQtyUM.Text;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["insp_data_comments"])))
        mdUtility.fMainForm.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
      else
        mdUtility.fMainForm.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable1.Rows[0]["InspectorLink"])))
      {
        DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM UserAccount WHERE User_ID={" + dataTable1.Rows[0]["InspectorLink"].ToString() + "}");
        if (dataTable2.Rows.Count > 0)
        {
          string Left1 = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(dataTable2.Rows[0]["LastName"], (object) ""));
          string Left2 = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(dataTable2.Rows[0]["FirstName"], (object) ""));
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "", false) > 0U)
          {
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "", false) > 0U)
              Left1 = Left1 + ", " + Left2;
          }
          else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "", false) > 0U)
            Left1 = Left2;
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "", false) > 0U)
            mdUtility.fMainForm.tsslStatus.Text = "Inspected By: " + Left1;
          else
            mdUtility.fMainForm.tsslStatus.Text = "LookupDataBaseName";
        }
        else
          mdUtility.fMainForm.tsslStatus.Text = "LookupDataBaseName";
      }
      Sample.LoadSampleList(InspectionID);
      frmMain fMainForm = mdUtility.fMainForm;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.CurrentLocation, "", false) == 0)
      {
        fMainForm.cboLocation.SelectedIndex = 0;
      }
      else
      {
        bool flag = false;
        int num = checked (fMainForm.cboLocation.Items.Count - 1);
        int index = 0;
        while (index <= num)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Strings.Right(Conversions.ToString(NewLateBinding.LateIndexGet(fMainForm.cboLocation.Items[index], new object[1]{ (object) "Name" }, (string[]) null)), Strings.Len(fMainForm.tvInspection.GetNodeByKey(fMainForm.CurrentLocation).Text)), fMainForm.tvInspection.GetNodeByKey(fMainForm.CurrentLocation).Text, false) == 0)
          {
            flag = true;
            break;
          }
          checked { ++index; }
        }
        fMainForm.cboLocation.SelectedIndex = !flag ? 0 : index;
      }
      mdUtility.fMainForm.cboLocation_SelectedIndexChanged((object) null, (EventArgs) null);
      fMainForm.m_bInspLoaded = true;
    }

    private static bool CanEditInspection(string InspectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT INSP_Source, INSP_DATA_INSP_DATE FROM Inspection_Data WHERE [INSP_DATA_ID]={" + InspectionID + "}");
      bool flag;
      if (dataTable.Rows.Count > 0)
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["INSP_Source"], (object) "InstallDate", false))
          flag = false;
        else if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.OrObject(Microsoft.VisualBasic.CompilerServices.Operators.OrObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(dataTable.Rows[0]["INSP_Source"], (object) "Copy", false), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(dataTable.Rows[0]["INSP_Source"], (object) nameof (Inspection), false)), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(dataTable.Rows[0]["INSP_Source"], (object) "Rapid Inspection", false))))
          flag = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectLessEqual((object) DateAndTime.DateAdd(DateInterval.Day, (double) checked (-1 * Inspection.IInspectionWindow), DateAndTime.Today), dataTable.Rows[0]["INSP_DATA_INSP_DATE"], false);
        else if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["INSP_Source"], (object) "WorkItem", false))
          flag = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectLessEqual((object) DateAndTime.DateAdd(DateInterval.Month, -1.0, DateAndTime.Today), dataTable.Rows[0]["INSP_DATA_INSP_DATE"], false);
      }
      else
        flag = false;
      return flag;
    }

    internal static void DeleteInspection(string InspectionID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM [Sample_Data] WHERE [samp_data_insp_data_id]={" + InspectionID + "}");
      try
      {
        foreach (DataRow row1 in dataTable1.Rows)
        {
          DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM sample_subcomponent WHERE [samp_subcomp_samp_id]={" + row1["samp_data_id"].ToString() + "}");
          try
          {
            foreach (DataRow row2 in dataTable2.Rows)
              mdUtility.DB.ExecuteCommand("DELETE FROM Sample_Subcomp_Distress WHERE [ssd_subcomp_id]={" + row2["samp_subcomp_samp_id"].ToString() + "}", false);
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          mdUtility.DB.ExecuteCommand("DELETE FROM Sample_SubComponent WHERE [samp_subcomp_samp_id]={" + row1["samp_data_id"].ToString() + "}", false);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.ExecuteCommand("DELETE FROM Sample_Data WHERE [samp_data_insp_data_id]={" + InspectionID + "}", false);
      mdUtility.DB.ExecuteCommand("DELETE FROM Inspection_Data WHERE [insp_data_id]={" + InspectionID + "}", false);
      mdUtility.fMainForm.SetInspChanged(false);
    }

    internal static bool SaveInspectionData(string InspectionID)
    {
      string str = "SELECT * FROM Inspection_Data WHERE [insp_data_id]={" + InspectionID + "}";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      if (dataTable.Rows.Count <= 0)
        return false;
      DataRow row = dataTable.Rows[0];
      Conversions.ToDate(mdUtility.fMainForm.cboInspectionDates.Text);
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
        row["BRED_Status"] = (object) "U";
      row["insp_data_type"] = !mdUtility.fMainForm.optRatingType_1.Checked ? (!mdUtility.fMainForm.optRatingType_2.Checked ? (object) 3 : (object) 2) : (object) 1;
      row["insp_data_samp"] = !mdUtility.fMainForm.optMethod_0.Checked ? (object) true : (object) false;
      mdUtility.DB.SaveDataTable(ref dataTable, str);
      bool flag = Sample.SaveSampleData(mdUtility.fMainForm.CurrentSample);
      if (flag)
      {
        if (mdUtility.fMainForm.m_bInspNew && mdUtility.BREDTableList.Contains((object) "WorkItem") && (mdUtility.fWorkItems != null && mdUtility.fWorkItems.dgvWorkItems != null) && mdUtility.fWorkItems.dgvWorkItems.CurrentRow != null)
        {
          GridViewRowInfo currentRow = mdUtility.fWorkItems.dgvWorkItems.CurrentRow;
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(currentRow.Cells["SectionLink"].Value, dataTable.Rows[0]["INSP_DATA_SEC_ID"], false))
          {
            currentRow.Cells["Status"].Value = (object) WorkItem.StatusType.Completed;
            currentRow.Cells["DateCompleted"].Value = (object) DateTime.Now;
            mdUtility.fWorkItems.UpdateWorkItemInDatabase(currentRow);
          }
        }
        mdUtility.fMainForm.m_bInspNew = false;
        mdUtility.fMainForm.SetInspChanged(false);
        Conversions.ToDate(mdUtility.fMainForm.cboInspectionDates.Text).Year.ToString();
      }
      return flag;
    }

    private static void CopyDataRow(DataRow drSource, ref DataRow drDest)
    {
      int index1 = 0;
      object[] itemArray = drSource.ItemArray;
      int index2 = 0;
      while (index2 < itemArray.Length)
      {
        object objectValue = RuntimeHelpers.GetObjectValue(itemArray[index2]);
        drDest[index1] = RuntimeHelpers.GetObjectValue(objectValue);
        checked { ++index1; }
        checked { ++index2; }
      }
    }

    internal static void CopyInspection(string InspectionID)
    {
      DateTime dateTime = DateAndTime.DateAdd(DateInterval.Day, (double) checked (-1 * Inspection.IInspectionWindow), DateAndTime.Today);
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM Inspection_Data WHERE [insp_data_id]={" + InspectionID + "}");
      if (dataTable1.Rows.Count <= 0)
        throw new Exception("Unable to find the source inspection record.");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM Inspection_Data WHERE [INSP_DATA_INSP_DATE]>=#" + Conversions.ToString(dateTime) + "# AND [INSP_DATA_SEC_ID]={" + dataTable1.Rows[0]["insp_data_sec_id"].ToString() + "}");
      bool flag;
      string uniqueId1;
      if (dataTable2.Rows.Count == 0)
      {
        flag = false;
        DataRow drDest = dataTable2.NewRow();
        Inspection.CopyDataRow(dataTable1.Rows[0], ref drDest);
        DataRow dataRow = drDest;
        dataRow["bred_status"] = (object) "N";
        uniqueId1 = mdUtility.GetUniqueID();
        dataRow["insp_data_id"] = (object) uniqueId1;
        dataRow["insp_data_insp_date"] = (object) DateAndTime.Today;
        dataRow["InspectorLink"] = (object) mdUtility.strCurrentInspector;
        dataRow["INSP_Source"] = (object) nameof (Inspection);
        dataTable2.Rows.Add(drDest);
        mdUtility.DB.SaveDataTable(ref dataTable2, "SELECT * FROM Inspection_Data");
        mdUtility.fMainForm.m_bInspNew = true;
      }
      else
      {
        flag = true;
        uniqueId1 = Conversions.ToString(dataTable2.Rows[0]["insp_data_id"]);
      }
      string sSQL;
      if (flag)
        sSQL = "SELECT * FROM Sample_Data WHERE [samp_data_insp_data_id]={" + InspectionID + "} AND [samp_data_id]={" + Strings.Replace(Conversions.ToString(mdUtility.fMainForm.cboLocation.SelectedValue), "'", "''", 1, -1, CompareMethod.Binary) + "}";
      else
        sSQL = "SELECT * FROM Sample_Data WHERE [samp_data_insp_data_id]={" + InspectionID + "}";
      DataTable dataTable3 = mdUtility.DB.GetDataTable(sSQL);
      DataTable tableSchema1 = mdUtility.DB.GetTableSchema("Sample_Data");
      try
      {
        foreach (DataRow row1 in dataTable3.Rows)
        {
          DataRow drDest = tableSchema1.NewRow();
          Inspection.CopyDataRow(row1, ref drDest);
          DataRow dataRow1 = drDest;
          dataRow1["BRED_Status"] = (object) "C";
          string uniqueId2 = mdUtility.GetUniqueID();
          dataRow1["samp_data_id"] = (object) uniqueId2;
          dataRow1["samp_data_insp_data_id"] = (object) uniqueId1;
          dataRow1["samp_data_comments"] = (object) DBNull.Value;
          tableSchema1.Rows.Add(drDest);
          mdUtility.DB.SaveDataTable(ref tableSchema1, "SELECT * FROM Sample_Data");
          mdUtility.fMainForm.m_bSampleNew = true;
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable2.Rows[0]["insp_data_type"], (object) 1, false))
          {
            DataTable tableSchema2 = mdUtility.DB.GetTableSchema("Sample_Subcomponent");
            DataTable dataTable4 = mdUtility.DB.GetDataTable(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM Sample_Subcomponent WHERE [samp_subcomp_samp_id]={", row1["samp_data_id"]), (object) "}")));
            try
            {
              foreach (DataRow row2 in dataTable4.Rows)
              {
                drDest = tableSchema2.NewRow();
                Inspection.CopyDataRow(row2, ref drDest);
                DataRow dataRow2 = drDest;
                dataRow2["BRED_Status"] = (object) "N";
                string uniqueId3 = mdUtility.GetUniqueID();
                dataRow2["samp_subcomp_id"] = (object) uniqueId3;
                dataRow2["samp_subcomp_samp_id"] = (object) uniqueId2;
                dataRow2["samp_subcomp_comments"] = (object) DBNull.Value;
                dataRow2["samp_subcomp_ci"] = (object) DBNull.Value;
                tableSchema2.Rows.Add(drDest);
                mdUtility.DB.SaveDataTable(ref tableSchema2, "SELECT * FROM Sample_Subcomponent");
                DataTable tableSchema3 = mdUtility.DB.GetTableSchema("Sample_SubComp_Distress");
                DataTable dataTable5 = mdUtility.DB.GetDataTable(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM Sample_SubComp_Distress WHERE [ssd_subcomp_id]={", row2["samp_subcomp_id"]), (object) "}")));
                try
                {
                  foreach (DataRow row3 in dataTable5.Rows)
                  {
                    drDest = tableSchema3.NewRow();
                    Inspection.CopyDataRow(row3, ref drDest);
                    DataRow dataRow3 = drDest;
                    dataRow3["BRED_Status"] = (object) "N";
                    dataRow3["ssd_id"] = (object) mdUtility.GetUniqueID();
                    dataRow3["ssd_subcomp_id"] = (object) uniqueId3;
                    tableSchema3.Rows.Add(drDest);
                    mdUtility.DB.SaveDataTable(ref tableSchema3, "SELECT * FROM Sample_SubComp_Distress");
                  }
                }
                finally
                {
                  IEnumerator enumerator;
                  if (enumerator is IDisposable)
                    (enumerator as IDisposable).Dispose();
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
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.fMainForm.m_bInspLoaded = false;
      Inspection.LoadInspectionDates(dataTable2.Rows[0]["insp_data_sec_id"].ToString());
      mdUtility.fMainForm.m_bInspLoaded = true;
      mdUtility.fMainForm.SetInspChanged(true);
    }

    internal static string AddInspection(
      string SectionID,
      DateTime InspectionDate,
      bool Sampling,
      Inspection.InspectionType InspectionType,
      string InspectionComments = "",
      string SampleLocation = "",
      int DirectRating = -1,
      string DirectComments = "",
      int PaintRating = -1,
      string PaintComments = "")
    {
      string str = "SELECT * FROM Inspection_Data WHERE [INSP_DATA_INSP_DATE]>=#" + Conversions.ToString(DateAndTime.DateAdd(DateInterval.Day, (double) checked (-1 * Inspection.IInspectionWindow), DateAndTime.Today)) + "# AND [INSP_DATA_SEC_ID]={" + SectionID + "}";
      DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM [Section Info] WHERE [sec_id]={" + SectionID + "}");
      string uniqueId;
      if (dataTable1.Rows.Count == 0)
      {
        DataRow row = dataTable1.NewRow();
        DataRow dataRow = row;
        dataRow["INSP_DATA_SEC_ID"] = (object) SectionID;
        uniqueId = mdUtility.GetUniqueID();
        dataRow["INSP_DATA_ID"] = (object) uniqueId;
        dataRow["inspectorlink"] = (object) mdUtility.strCurrentInspector;
        dataRow["INSP_Source"] = (object) nameof (Inspection);
        dataRow["INSP_DATA_INSP_DATE"] = (object) InspectionDate;
        dataRow["INSP_DATA_SAMP"] = (object) Sampling;
        dataRow["bred_status"] = (object) "N";
        dataRow["insp_data_type"] = (object) InspectionType;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(InspectionComments, "", false) > 0U)
          dataRow["INSP_DATA_COMMENTS"] = (object) InspectionComments;
        dataTable1.Rows.Add(row);
      }
      else
      {
        DataRow row = dataTable1.Rows[0];
        uniqueId = Conversions.ToString(row["INSP_DATA_ID"]);
        row["bred_status"] = (object) "U";
      }
      mdUtility.DB.SaveDataTable(ref dataTable1, str);
      DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT SUM(SAMP_DATA_QTY) AS SAMP_QTY_SUM FROM Sample_Data WHERE [SAMP_DATA_INSP_DATA_ID]={" + uniqueId + "}");
      DataTable tableSchema = mdUtility.DB.GetTableSchema("Sample_Data");
      DataRow row1 = tableSchema.NewRow();
      DataRow dataRow1 = row1;
      dataRow1["BRED_Status"] = (object) "N";
      dataRow1["samp_data_id"] = (object) mdUtility.GetUniqueID();
      dataRow1["samp_data_insp_data_id"] = (object) uniqueId;
      double num = Conversions.ToDouble(Microsoft.VisualBasic.CompilerServices.Operators.SubtractObject(dataTable2.Rows[0]["sec_qty"], UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable3.Rows[0]["SAMP_QTY_SUM"]), (object) 0)));
      dataRow1["samp_data_qty"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(num < 0.0, (object) 0, (object) num));
      dataRow1["samp_data_paint"] = RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["sec_paint"]);
      if (Sampling && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(SampleLocation, "", false) != 0)
        dataRow1["samp_data_loc"] = (object) mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation).Tag.ToString();
      if (DirectRating != -1 || PaintRating != -1)
      {
        DataTable dataTable4 = mdUtility.DB.GetDataTable("SELECT * FROM RO_CIRating");
        if (DirectRating > 0)
        {
          DataRow[] dataRowArray = dataTable4.Select("CIIndex=" + Conversions.ToString(DirectRating));
          if (dataRowArray != null)
          {
            dataRow1["samp_data_comp_rate"] = RuntimeHelpers.GetObjectValue(dataRowArray[0]["CIRateValue"]);
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(DirectComments, "", false) > 0U)
              dataRow1["samp_data_comp_rate_comments"] = (object) DirectComments;
          }
        }
        if (PaintRating > 0)
        {
          DataRow[] dataRowArray = dataTable4.Select("CIIndex=" + Conversions.ToString(PaintRating));
          if (dataRowArray != null)
          {
            dataRow1["samp_data_paint_rate"] = RuntimeHelpers.GetObjectValue(dataRowArray[0]["CIRateValue"]);
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(PaintComments, "", false) > 0U)
              dataRow1["samp_data_paint_rate_comments"] = (object) PaintComments;
          }
        }
      }
      tableSchema.Rows.Add(row1);
      mdUtility.DB.SaveDataTable(ref tableSchema, "SELECT * FROM Sample_Data");
      return uniqueId;
    }

    internal static void NewInspection(Guid sectionID)
    {
      string str = "SELECT * FROM Inspection_Data WHERE [INSP_DATA_INSP_DATE]>=#" + Conversions.ToString(DateAndTime.DateAdd(DateInterval.Day, (double) checked (-1 * Inspection.IInspectionWindow), DateAndTime.Today)) + "# AND [INSP_DATA_SEC_ID]={" + sectionID.ToString() + "}";
      DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM [Section Info] WHERE [sec_id]={" + sectionID.ToString() + "}");
      DateTime dateTime = new frmDatePicker().ShowDialog(new DateTime?(DateAndTime.DateAdd(DateInterval.Day, (double) checked (-1 * Inspection.IInspectionWindow), DateAndTime.Today)), new DateTime?(DateAndTime.Today));
      Guid guid;
      if (dataTable1.Rows.Count == 0)
      {
        DataRow row = dataTable1.NewRow();
        DataRow dataRow = row;
        dataRow["INSP_DATA_SEC_ID"] = (object) sectionID;
        guid = Guid.Parse(mdUtility.GetUniqueID());
        dataRow["INSP_DATA_ID"] = (object) guid;
        dataRow["inspectorlink"] = (object) mdUtility.strCurrentInspector;
        dataRow["INSP_Source"] = (object) nameof (Inspection);
        dataRow["INSP_DATA_INSP_DATE"] = (object) dateTime;
        dataRow["INSP_DATA_SAMP"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.CurrentLocation, "", false) == 0, (object) false, (object) true));
        dataRow["bred_status"] = (object) "N";
        dataRow["insp_data_type"] = (object) 2;
        dataTable1.Rows.Add(row);
        mdUtility.DB.SaveDataTable(ref dataTable1, str);
        Section.SectionMaterialLink(sectionID.ToString());
        mdUtility.fMainForm.m_bInspNew = true;
      }
      else
      {
        object obj = dataTable1.Rows[0]["INSP_DATA_ID"];
        guid = obj != null ? (Guid) obj : new Guid();
      }
      DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT SUM(SAMP_DATA_QTY) AS SAMP_QTY_SUM FROM Sample_Data WHERE [SAMP_DATA_INSP_DATA_ID]={" + guid.ToString() + "}");
      DataTable tableSchema = mdUtility.DB.GetTableSchema("Sample_Data");
      DataRow row1 = tableSchema.NewRow();
      DataRow dataRow1 = row1;
      dataRow1["BRED_Status"] = (object) "N";
      dataRow1["samp_data_id"] = (object) mdUtility.GetUniqueID();
      dataRow1["samp_data_insp_data_id"] = (object) guid;
      double num = Conversions.ToDouble(Microsoft.VisualBasic.CompilerServices.Operators.SubtractObject(dataTable2.Rows[0]["sec_qty"], UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable3.Rows[0]["SAMP_QTY_SUM"]), (object) 0)));
      dataRow1["samp_data_qty"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(num < 0.0, (object) 0, (object) num));
      dataRow1["samp_data_paint"] = RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["sec_paint"]);
      if (mdUtility.fMainForm.tvInspection.Visible && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.CurrentLocation, "", false) != 0)
        dataRow1["samp_data_loc"] = (object) mdUtility.fMainForm.tvInspection.GetNodeByKey(mdUtility.fMainForm.CurrentLocation).Tag.ToString();
      tableSchema.Rows.Add(row1);
      mdUtility.DB.SaveDataTable(ref tableSchema, "SELECT * FROM Sample_Data");
      mdUtility.fMainForm.m_bSampleNew = true;
      mdUtility.fMainForm.m_bInspLoaded = false;
      Inspection.LoadInspectionDates(sectionID.ToString());
      mdUtility.fMainForm.m_bInspLoaded = true;
      mdUtility.fMainForm.SetInspChanged(true);
      if (!mdUtility.fMainForm.txtSQuantity.Visible)
        return;
      mdUtility.fMainForm.txtSQuantity.Focus();
    }

    internal static string Section(ref string InspectionID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT insp_data_sec_id FROM Inspection_Data WHERE [Insp_Data_ID]={" + InspectionID + "}");
      if (dataTable.Rows.Count > 0)
        return dataTable.Rows[0]["insp_data_sec_id"].ToString();
      return "";
    }

    public enum InspectionType
    {
      DistressSurvey = 1,
      DirectRating = 2,
      PMinspection = 3,
    }
  }
}
