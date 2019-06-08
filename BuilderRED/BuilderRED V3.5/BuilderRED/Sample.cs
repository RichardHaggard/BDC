// Decompiled with JetBrains decompiler
// Type: BuilderRED.Sample
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Compatibility.VB6;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  internal class Sample
  {
    public static void GetDirectRating(string SampleID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM RO_CIRating ORDER BY CIRateLower DESC");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM Sample_Data WHERE [samp_data_id]={" + SampleID + "}");
      int num = -1;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["samp_data_comp_rate"])))
        num = Conversions.ToInteger(dataTable2.Rows[0]["samp_data_comp_rate"]);
      if (!(num >= 0 & num <= 100))
        return;
      DataRow[] dataRowArray = dataTable1.Select("CIRateLower<=" + Conversions.ToString(num) + " AND CIRateUpper>= " + Conversions.ToString(num));
      if (dataRowArray.Length > 0)
      {
        switch (Conversions.ToInteger(dataRowArray[0]["CIIndex"]))
        {
          case 1:
            mdUtility.fMainForm.optCompRating_1.Checked = true;
            break;
          case 2:
            mdUtility.fMainForm.optCompRating_2.Checked = true;
            break;
          case 3:
            mdUtility.fMainForm.optCompRating_3.Checked = true;
            break;
          case 4:
            mdUtility.fMainForm.optCompRating_4.Checked = true;
            break;
          case 5:
            mdUtility.fMainForm.optCompRating_5.Checked = true;
            break;
          case 6:
            mdUtility.fMainForm.optCompRating_6.Checked = true;
            break;
          case 7:
            mdUtility.fMainForm.optCompRating_7.Checked = true;
            break;
          case 8:
            mdUtility.fMainForm.optCompRating_8.Checked = true;
            break;
          case 9:
            mdUtility.fMainForm.optCompRating_9.Checked = true;
            break;
        }
      }
    }

    internal static void GetPaintRating(string SampleID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM RO_CIRating ORDER BY CIRateLower DESC");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM Sample_Data WHERE [samp_data_id]={" + SampleID + "}");
      int num = -1;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["samp_data_paint_rate"])))
        num = Conversions.ToInteger(dataTable2.Rows[0]["samp_data_paint_rate"]);
      if (!(num >= 0 & num <= 100))
        return;
      DataRow[] dataRowArray = dataTable1.Select("CIRateLower<=" + Conversions.ToString(num) + " AND CIRateUpper>= " + Conversions.ToString(num));
      if (dataRowArray.Length > 0)
      {
        switch (Conversions.ToInteger(dataRowArray[0]["CIIndex"]))
        {
          case 1:
            mdUtility.fMainForm.optPaintRating_1.Checked = true;
            break;
          case 2:
            mdUtility.fMainForm.optPaintRating_2.Checked = true;
            break;
          case 3:
            mdUtility.fMainForm.optPaintRating_3.Checked = true;
            break;
          case 4:
            mdUtility.fMainForm.optPaintRating_4.Checked = true;
            break;
          case 5:
            mdUtility.fMainForm.optPaintRating_5.Checked = true;
            break;
          case 6:
            mdUtility.fMainForm.optPaintRating_6.Checked = true;
            break;
          case 7:
            mdUtility.fMainForm.optPaintRating_7.Checked = true;
            break;
          case 8:
            mdUtility.fMainForm.optPaintRating_8.Checked = true;
            break;
          case 9:
            mdUtility.fMainForm.optPaintRating_9.Checked = true;
            break;
        }
      }
    }

    internal static string GetSampleLocationName(string SampleLocationID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT Name FROM Sample_Location WHERE Location_ID={" + SampleLocationID + "}");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToString(dataTable.Rows[0]["Name"]);
      return "";
    }

    public static void LoadSample(string SampleID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable(!mdUtility.UseUniformat ? "SELECT * FROM [Sample Info] WHERE [samp_data_id]={" + SampleID + "}" : "SELECT * FROM [SampleInfoUniformat] WHERE [samp_data_id]={" + SampleID + "}");
      string SectionID = mdUtility.DB.GetDataTable("SELECT insp_data_sec_id FROM Inspection_Data WHERE [insp_data_id]={" + dataTable.Rows[0]["samp_data_insp_data_id"].ToString() + "}").Rows[0]["insp_data_sec_id"].ToString();
      DataRow row = dataTable.Rows[0];
      string strInspectionID = row["samp_data_insp_data_id"].ToString();
      mdUtility.fMainForm.txtSQuantity.Text = Support.Format(RuntimeHelpers.GetObjectValue(Interaction.IIf(mdUtility.fMainForm.miUnits.Checked, RuntimeHelpers.GetObjectValue(row["eng_qty"]), RuntimeHelpers.GetObjectValue(row["met_qty"]))), "#,##0", FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["samp_data_loc"])))
      {
        mdUtility.fMainForm.cmdDeleteSample.Enabled = false;
      }
      else
      {
        mdUtility.fMainForm.cmdNewSample.Enabled = mdUtility.fMainForm.CanEditInspection;
        mdUtility.fMainForm.cmdDeleteSample.Enabled = mdUtility.fMainForm.CanEditInspection;
      }
      bool flag;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["BRED_Status"], (object) "C", false))
      {
        mdUtility.fMainForm.cboLocation.BackColor = Color.Yellow;
        flag = true;
      }
      else
      {
        mdUtility.fMainForm.cboLocation.BackColor = SystemColors.Window;
        flag = false;
      }
      if (!mdUtility.fMainForm.optMethod_1.Checked)
        mdUtility.fMainForm.txtSQuantity.Text = mdUtility.fMainForm.lblSecQtyValue.Text;
      mdUtility.fMainForm.lblPCInspValue.Text = Microsoft.VisualBasic.Strings.Format((object) mdUtility.GetPercent(strInspectionID, SampleID, Conversions.ToDouble(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtSQuantity.Text, "", false) == 0, (object) 0, (object) mdUtility.fMainForm.txtSQuantity.Text))), "0.00%");
      mdUtility.fMainForm.chkSampNonRep.CheckState = !Conversions.ToBoolean(row["samp_data_nonrep"]) ? CheckState.Unchecked : CheckState.Checked;
      mdUtility.fMainForm.SamplePainted = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["samp_data_paint"], (object) true, false);
      mdUtility.fMainForm.chkDefFree.CheckState = !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["samp_data_defect_free"], (object) true, false) ? CheckState.Unchecked : CheckState.Checked;
      mdUtility.fMainForm.chkPaintDefFree.CheckState = !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["samp_data_paint_defect_free"], (object) true, false) ? CheckState.Unchecked : CheckState.Checked;
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["samp_data_comments"])))
        mdUtility.fMainForm.cmdSampComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
      else
        mdUtility.fMainForm.cmdSampComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      if (mdUtility.fMainForm.optRatingType_2.Checked)
        Sample.LoadDirectRating(SampleID);
      else
        Sample.LoadSubCompGrid(SectionID, SampleID);
      if (!flag)
        return;
      mdUtility.fMainForm.tsbSave.Enabled = true;
      mdUtility.fMainForm.tsbCancel.Enabled = true;
      mdUtility.fMainForm.m_bInspNeedToSave = true;
    }

    private static void LoadDirectRating(string SampleID)
    {
      Sample.GetDirectRating(SampleID);
      Sample.GetPaintRating(SampleID);
    }

    public static void LoadSampleList(string InspectionID)
    {
      frmMain fMainForm = mdUtility.fMainForm;
      fMainForm.m_bDDLoad = true;
      mdUtility.LoadMstrTable("Samples", "SELECT * FROM qrySampleList WHERE [samp_data_insp_data_id]={" + InspectionID + "}");
      fMainForm.cboLocation.ValueMember = "SAMP_DATA_ID";
      fMainForm.cboLocation.DisplayMember = "Name";
      fMainForm.cboLocation.DataSource = (object) mdUtility.get_MstrTable("Samples");
      fMainForm.m_bDDLoad = false;
      if (fMainForm.cboLocation.Items.Count > 0)
      {
        if (mdUtility.get_MstrTable("Samples").Rows.Count == 1)
          fMainForm.frmMethod.Enabled = fMainForm.CanEditInspection;
        else
          fMainForm.frmMethod.Enabled = false;
      }
    }

    private static void LoadSubCompGrid(string SectionID, string SampleID)
    {
      frmMain fMainForm = mdUtility.fMainForm;
      mdUtility.LoadMstrTable("SubComps", "SELECT * FROM qrySampleSubComponents WHERE [samp_subcomp_samp_id]={" + SampleID + "}");
      mdUtility.LoadMstrTable("Tasks", "SELECT TaskID, TaskDescription FROM (Component_Section INNER JOIN RO_CMC ON\r\n              RO_CMC.CMC_ID = COMPONENT_SECTION.SEC_CMC_LINK)\r\n\t\t\t  INNER JOIN RO_TASK ON RO_TASK.TaskTaskListLink = RO_CMC.CMC_TASKLIST_LINK\r\n\t\t\t  where sec_id ={" + SectionID + "} ORDER BY TaskID ");
      DataTable dataTable1 = mdUtility.DB.GetDataTable(!mdUtility.UseUniformat ? "Select * FROM qrySectionSubComponents WHERE [SEC_ID]={" + SectionID + "}" : "Select * FROM qrySectionSubComponentsUniformat WHERE [SEC_ID]={" + SectionID + "}");
      DataTable dataTable2 = mdUtility.get_MstrTable("SubComps");
      try
      {
        foreach (DataRow row1 in dataTable1.Rows)
        {
          int num1 = 0;
          int num2 = checked (dataTable2.Rows.Count - 1);
          int index = 0;
          while (index <= num2)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable2.Rows[index]["SubCompLink"], row1["ID"], false))
            {
              num1 = -1;
              break;
            }
            checked { ++index; }
          }
          if ((uint) ~num1 > 0U)
          {
            DataRow row2 = dataTable2.NewRow();
            row2["BRED_Status"] = (object) "A";
            row2["SAMP_SUBCOMP_SAMP_ID"] = (object) SampleID;
            row2["SubCompLink"] = RuntimeHelpers.GetObjectValue(row1["ID"]);
            row2["Subcomponent"] = RuntimeHelpers.GetObjectValue(row1["Description"]);
            row2["SAMP_SUBCOMP_ALTUM"] = (object) false;
            row2["samp_subcomp_notappl"] = !mdUtility.fMainForm.chkDefFree.Checked ? (object) true : (object) false;
            row2["Distresses"] = (object) "Distresses";
            row2["Criteria"] = (object) "Criteria";
            row2["samp_subcomp_insp"] = (object) false;
            row2["SAMP_SUBCOMP_CONDRATE"] = (object) -1;
            if (mdUtility.fMainForm.chkPaintDefFree.Checked)
            {
              row2["samp_subcomp_paintna"] = (object) false;
              row2["samp_subcomp_paintdf"] = (object) true;
              row2["samp_subcomp_paintrate"] = (object) 100;
            }
            else
            {
              row2["samp_subcomp_paintna"] = (object) true;
              row2["samp_subcomp_paintdf"] = (object) false;
              row2["samp_subcomp_paintrate"] = (object) -1;
            }
            dataTable2.Rows.Add(row2);
          }
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      fMainForm.ugSubcomponents.DataSource = (object) mdUtility.get_MstrTable("SubComps");
      fMainForm.ugSubcomponents.DataBind();
      mdUtility.LoadMstrTable("Distresses", mdUtility.Units != mdUtility.SystemofMeasure.umMetric ? "Select [SAMP_SUBCOMP_SAMP_ID], BRED_Status, ID, Subcomplink, Distress, Severity, ENG_SUBCOMP_QTY As [Subcomponent Qty], ENG_DISTRESS_QTY As [Distress Qty], Density, Critical, ESC, [ESC Number], [ESC Date] FROM qryDistresses WHERE [samp_subcomp_samp_id]={" + SampleID + "}" : "Select [SAMP_SUBCOMP_SAMP_ID], BRED_Status, ID, Subcomplink, Distress, Severity, MET_SUBCOMP_QTY As [Subcomponent Qty], MET_DISTRESS_QTY As [Distress Qty], Density, Critical, ESC, [ESC Number], [ESC Date] FROM qryDistresses WHERE [samp_subcomp_samp_id]={" + SampleID + "}");
      if (mdUtility.get_MstrTable("Distresses").Rows.Count > 0)
      {
        try
        {
          foreach (DataRow row in mdUtility.get_MstrTable("Distresses").Rows)
          {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "D", false))
            {
              row.BeginEdit();
              row["BRED_Status"] = (object) "E";
              row.EndEdit();
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
      bool flag = fMainForm.chkSampPainted.Checked;
      UltraGrid ugSubcomponents = mdUtility.fMainForm.ugSubcomponents;
      DefaultableBoolean defaultableBoolean = !mdUtility.fMainForm.CanEditInspection ? DefaultableBoolean.False : DefaultableBoolean.True;
      ugSubcomponents.DisplayLayout.Bands[0].Override.AllowUpdate = defaultableBoolean;
      ugSubcomponents.DisplayLayout.Bands[0].Override.AllowColSizing = AllowColSizing.None;
      ugSubcomponents.DisplayLayout.Bands[0].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].ColHeaderLines = 2;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["BRED_Status"].Hidden = true;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["BRED_Status"].Width = 0;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["ID"].Width = 0;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_SAMP_ID"].Hidden = true;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SubcompLink"].Hidden = true;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SubcompLink"].Width = 0;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Subcomponent"].Hidden = false;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Subcomponent"].CellActivation = Activation.NoEdit;
      if ((uint) mdUtility.fMainForm.chkSampPainted.CheckState > 0U)
        ugSubcomponents.DisplayLayout.Bands[0].Columns["Subcomponent"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(2000.0)));
      else
        ugSubcomponents.DisplayLayout.Bands[0].Columns["Subcomponent"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(3750.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_NOTAPPL"].Header.Caption = "N/A";
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_NOTAPPL"].Hidden = false;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_NOTAPPL"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(450.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_NOTAPPL"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_NOTAPPL"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CONDRATE"].Header.Caption = "Condition\r\nRating";
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CONDRATE"].Hidden = false;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CONDRATE"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1200.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CONDRATE"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_CONDRATE"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Distresses"].Hidden = mdUtility.fMainForm.optRatingType_3.Checked;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Distresses"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1000.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Distresses"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Distresses"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Distresses"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Criteria"].Hidden = mdUtility.fMainForm.optRatingType_1.Checked;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Criteria"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1000.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Criteria"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Criteria"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Criteria"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_INSP"].Header.Caption = "Inspected";
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_INSP"].CellActivation = Activation.NoEdit;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_INSP"].Hidden = mdUtility.fMainForm.optRatingType_3.Checked;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_INSP"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(850.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_INSP"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_INSP"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_ALTUM"].Hidden = true;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_ALTUM"].Width = 0;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Header.Caption = "Paint\r\nN/A";
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Hidden = !flag;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(525.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Header.Caption = "Paint\r\nD/F";
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Hidden = !flag;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(525.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Header.Caption = "Paint\r\nRating";
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Hidden = !flag;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(675.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Comments"].Hidden = true;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Comments"].Width = checked ((int) Math.Round(Support.TwipsToPixelsX(1000.0)));
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Comments"].Header.Appearance.TextHAlign = HAlign.Center;
      ugSubcomponents.DisplayLayout.Bands[0].Columns["Comments"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedTextEditor;
      Sample.SetDropDownValueItems("SAMP_SUBCOMP_PAINTRATE");
      Sample.SetDropDownValueItems("SAMP_SUBCOMP_CONDRATE");
      ugSubcomponents.Refresh();
      if (mdUtility.fMainForm.chkSampPainted.CheckState == CheckState.Checked)
      {
        Activation activation = !mdUtility.fMainForm.CanEditInspection ? Activation.NoEdit : Activation.AllowEdit;
        ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].CellActivation = activation;
        ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].CellActivation = activation;
        ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].CellActivation = activation;
      }
      else
      {
        ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].CellActivation = Activation.NoEdit;
        ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].CellActivation = Activation.NoEdit;
        ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].CellActivation = Activation.NoEdit;
      }
    }

    public static bool SaveSampleData(string SampleID)
    {
      bool flag1;
      try
      {
        DataTable dataTable1 = mdUtility.DB.GetDataTable(!mdUtility.UseUniformat ? "SELECT * FROM [Sample Info] WHERE [samp_data_id]={" + SampleID + "}" : "SELECT * FROM [SampleInfoUniformat] WHERE [samp_data_id]={" + SampleID + "}");
        string sSelectCommand = "SELECT * FROM Sample_Data WHERE [samp_data_id]={" + SampleID + "}";
        DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM Sample_Data WHERE [samp_data_id]={" + SampleID + "}");
        if (dataTable2.Rows.Count == 0)
          throw new Exception("Unable to save sample data.  Sample data was not found.");
        DataRow row = dataTable2.Rows[0];
        int num = Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) ? 1 : (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false) ? 1 : 0);
        row["BRED_Status"] = num == 0 ? (object) "N" : (object) "U";
        mdUtility.fMainForm.cboLocation.BackColor = SystemColors.Window;
        row["samp_data_qty"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtSQuantity.Text, "", false) <= 0U ? (object) DBNull.Value : (!mdUtility.fMainForm.miUnits.Checked ? (object) mdUtility.fMainForm.txtSQuantity.Text : Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) Conversions.ToDouble(mdUtility.fMainForm.txtSQuantity.Text), dataTable1.Rows[0]["uom_conv"]));
        row["samp_data_paint"] = mdUtility.fMainForm.chkSampPainted.CheckState != CheckState.Checked ? (object) false : (object) true;
        row["samp_data_nonrep"] = mdUtility.fMainForm.chkSampNonRep.CheckState != CheckState.Checked ? (object) false : (object) true;
        row["samp_data_defect_free"] = mdUtility.fMainForm.chkDefFree.CheckState != CheckState.Checked ? (object) false : (object) true;
        row["samp_data_paint_defect_free"] = mdUtility.fMainForm.chkPaintDefFree.CheckState != CheckState.Checked ? (object) false : (object) true;
        mdUtility.DB.SaveDataTable(ref dataTable2, sSelectCommand);
        bool flag2 = !(mdUtility.fMainForm.optRatingType_1.Checked | mdUtility.fMainForm.optRatingType_3.Checked) ? Sample.SaveDirectRatingData(SampleID) : Sample.SaveSubComponentData(SampleID);
        if (flag2)
          mdUtility.fMainForm.m_bSampleNew = false;
        flag1 = flag2;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Sample), nameof (SaveSampleData));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
      return flag1;
    }

    private static bool SaveSubComponentData(string SampleID)
    {
      bool flag1;
      try
      {
        bool flag2 = false;
        int num1 = checked (mdUtility.get_MstrTable("SubComps").Rows.Count - 1);
        int index = 0;
        while (index <= num1)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(mdUtility.get_MstrTable("SubComps").Rows[index]["samp_subcomp_notappl"], (object) false, false))
          {
            flag2 = true;
            break;
          }
          checked { ++index; }
        }
        if (!flag2)
        {
          if (mdUtility.fMainForm.optRatingType_1.Checked | mdUtility.fMainForm.optRatingType_3.Checked)
          {
            int num2 = (int) Interaction.MsgBox((object) "You must inspect at least one subcomponent for this sample.", MsgBoxStyle.Critical, (object) null);
          }
          else
          {
            int num3 = (int) Interaction.MsgBox((object) "You must inspect at least one subcomponent for this inspection.", MsgBoxStyle.Critical, (object) null);
          }
          flag1 = false;
        }
        else
        {
          string str = "SELECT * FROM sample_subcomponent WHERE [samp_subcomp_samp_id]={" + SampleID + "}";
          DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
          DataTable dataTable2 = dataTable1;
          if (mdUtility.get_MstrTable("SubComps").Rows.Count == 0 & dataTable2.Rows.Count > 0)
          {
            try
            {
              foreach (DataRow row in dataTable1.Rows)
                row.Delete();
            }
            finally
            {
              IEnumerator enumerator;
              if (enumerator is IDisposable)
                (enumerator as IDisposable).Dispose();
            }
            mdUtility.DB.SaveDataTable(ref dataTable1, str);
          }
          else
          {
            try
            {
              foreach (DataRow row1 in mdUtility.get_MstrTable("SubComps").Rows)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row1["BRED_Status"]), (object) ""), (object) "", false))
                {
                  string Left = Conversions.ToString(row1["BRED_Status"]);
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "A", false) != 0)
                  {
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "C", false) == 0)
                    {
                      DataRow[] dataRowArray = dataTable2.Select("[samp_subcomp_id]='" + row1["ID"].ToString() + "'");
                      if (dataRowArray.Length == 0)
                      {
                        DataRow row2 = dataTable2.NewRow();
                        dataTable2.Rows.Add(row2);
                        row2["BRED_Status"] = (object) "N";
                        row2["samp_subcomp_id"] = (object) mdUtility.GetUniqueID();
                        row2["samp_subcomp_Samp_id"] = (object) SampleID;
                        dataRowArray = new DataRow[1]
                        {
                          row2
                        };
                      }
                      else if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable2.Rows[0]["BRED_Status"], (object) "N", false))
                        dataRowArray[0]["BRED_Status"] = (object) "U";
                      DataRow dataRow = dataRowArray[0];
                      dataRow["samp_subcomp_cmc_subcomp_link"] = RuntimeHelpers.GetObjectValue(row1["Subcomplink"]);
                      dataRow["samp_subcomp_notappl"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_notappl"]);
                      dataRow["samp_subcomp_condrate"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_condrate"]);
                      dataRow["samp_subcomp_insp"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_insp"]);
                      dataRow["samp_subcomp_paintna"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintna"]);
                      dataRow["samp_subcomp_paintdf"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintdf"]);
                      dataRow["samp_subcomp_altum"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["samp_subcomp_altum"])), (object) false, RuntimeHelpers.GetObjectValue(row1["samp_subcomp_altum"])));
                      dataRow["samp_subcomp_paintrate"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintrate"])), (object) 0, RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintrate"])));
                      row1["BRED_Status"] = (object) DBNull.Value;
                    }
                  }
                  else
                  {
                    DataRow row2 = dataTable2.NewRow();
                    DataRow dataRow = row2;
                    dataRow["BRED_Status"] = (object) "N";
                    dataRow["samp_subcomp_id"] = (object) mdUtility.GetUniqueID();
                    dataRow["samp_subcomp_Samp_id"] = (object) SampleID;
                    dataRow["samp_subcomp_cmc_subcomp_link"] = RuntimeHelpers.GetObjectValue(row1["Subcomplink"]);
                    dataRow["samp_subcomp_notappl"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_notappl"]);
                    dataRow["samp_subcomp_condrate"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_condrate"]);
                    dataRow["samp_subcomp_insp"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_insp"]);
                    dataRow["samp_subcomp_altum"] = RuntimeHelpers.GetObjectValue(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row1["samp_subcomp_altum"]), (object) false));
                    dataRow["samp_subcomp_paintna"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintna"]);
                    dataRow["samp_subcomp_paintdf"] = RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintdf"]);
                    dataRow["samp_subcomp_paintrate"] = RuntimeHelpers.GetObjectValue(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row1["samp_subcomp_paintrate"]), (object) 0));
                    dataTable2.Rows.Add(row2);
                    row1["BRED_Status"] = (object) DBNull.Value;
                    row1["ID"] = RuntimeHelpers.GetObjectValue(row2["samp_subcomp_id"]);
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
          }
          mdUtility.get_MstrTable("SubComps").AcceptChanges();
          mdUtility.DB.SaveDataTable(ref dataTable1, str);
          flag1 = !mdUtility.fMainForm.optRatingType_1.Checked ? Sample.SaveCriteriaData(SampleID) : Sample.SaveDistressData(SampleID);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Sample), nameof (SaveSubComponentData));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
      return flag1;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static bool SaveCriteriaData(string SampleID)
    {
      bool flag;
      try
      {
        string str = "SELECT * FROM sample_subcomp_Criteria";
        DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
        DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM qrySampleSubComponents WHERE [samp_subcomp_samp_id]={" + SampleID + "}");
        DataTable dataTable3 = dataTable1;
        if (mdUtility.get_MstrTable("SelectedCrit") != null)
        {
          try
          {
            foreach (DataRow row1 in mdUtility.get_MstrTable("SelectedCrit").Rows)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row1["CritStatus"], (object) "N", false))
              {
                Guid guid;
                if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["SubCompLink"])))
                {
                  DataRow[] dataRowArray = dataTable2.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SubCompLink=", row1["SubCompLink"])));
                  if (dataRowArray.Length == 0)
                    Information.Err().Raise(10100, (object) nameof (Sample), (object) "Could not find subcomponent for this distress", (object) null, (object) null);
                  object obj = dataRowArray[0]["ID"];
                  guid = obj != null ? (Guid) obj : new Guid();
                }
                else
                  Information.Err().Raise(10100, (object) "SaveCriterionData", (object) "No subcomponent link for this distress", (object) null, (object) null);
                DataRow[] dataRowArray1 = dataTable3.Select("[SSC_SUBCOMP_ID]='" + row1["SampSubCompID"].ToString() + "'");
                if (((IEnumerable<DataRow>) dataRowArray1).Count<DataRow>() > 0)
                  dataRowArray1[0].Delete();
                DataRow row2 = dataTable3.NewRow();
                dataTable3.Rows.Add(row2);
                DataRow dataRow = row2;
                dataRow["SSC_ID"] = (object) mdUtility.GetUniqueID();
                dataRow["SSC_CRITERION_LINK"] = RuntimeHelpers.GetObjectValue(row1["ID"]);
                dataRow["SSC_SUBCOMP_ID"] = (object) guid;
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
        mdUtility.DB.SaveDataTable(ref dataTable1, str);
        flag = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Sample), nameof (SaveCriteriaData));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static bool SaveDistressData(string SampleID)
    {
      bool flag;
      try
      {
        string str = "SELECT * FROM sample_subcomp_distress";
        DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
        DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM qrySampleSubComponents WHERE [samp_subcomp_samp_id]={" + SampleID + "}");
        DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT * FROM qrySubComponentUOM");
        DataTable dataTable4 = dataTable1;
        try
        {
          foreach (DataRow row1 in mdUtility.get_MstrTable("Distresses").Rows)
          {
            DataRow[] dataRowArray1;
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["SubCompLink"])))
            {
              dataRowArray1 = dataTable2.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SubCompLink=", row1["SubCompLink"])));
              if (dataRowArray1.Length == 0)
                Information.Err().Raise(10100, (object) nameof (Sample), (object) "Could not find subcomponent for this distress", (object) null, (object) null);
            }
            else
              Information.Err().Raise(10100, (object) nameof (SaveDistressData), (object) "No subcomponent link for this distress", (object) null, (object) null);
            string Left = Conversions.ToString(row1["BRED_Status"]);
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "A", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "C", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "R", false) == 0)
                {
                  dataRowArray1 = dataTable4.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "[ssd_id]={", row1["ID"]), (object) "}")));
                  if (dataRowArray1.Length > 0)
                  {
                    if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataRowArray1[0]["BRED_Status"], (object) "N", false))
                      dataRowArray1[0].Delete();
                    else
                      dataRowArray1[0]["BRED_Status"] = (object) "D";
                  }
                  row1.Delete();
                }
              }
              else
              {
                if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["SubCompLink"])))
                {
                  dataRowArray1 = dataTable4.Select("[SSD_ID]='" + row1["ID"].ToString() + "'");
                }
                else
                {
                  dataRowArray1 = new DataRow[1];
                  Information.Err().Raise(10100, (object) nameof (SaveDistressData), (object) "No subcomponent link for this distress", (object) null, (object) null);
                }
                if (dataRowArray1.Length > 0)
                {
                  DataRow dataRow = dataRowArray1[0];
                  if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataRow["BRED_Status"], (object) "N", false))
                    dataRow["BRED_Status"] = (object) "U";
                  if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.NotObject(dataTable2.Rows[0]["SAMP_SUBCOMP_ALTUM"]), (object) (mdUtility.Units == mdUtility.SystemofMeasure.umEnglish))))
                  {
                    DataRow[] dataRowArray2 = dataTable3.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "[CMC_SCOMP_ID]=", row1["Subcomplink"])));
                    dataRow["ssd_subcomp_qty"] = RuntimeHelpers.GetObjectValue(row1["Subcomponent Qty"]);
                    if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["ssd_subcomp_qty"])))
                      dataRow["ssd_subcomp_qty"] = Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) Conversions.ToDouble(row1["Subcomponent Qty"]), dataRowArray2[0]["UOM_CONV"]);
                    dataRow["ssd_subcomp_dist_qty"] = RuntimeHelpers.GetObjectValue(row1["Distress Qty"]);
                    if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["ssd_subcomp_dist_qty"])))
                      dataRow["ssd_subcomp_dist_qty"] = Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) Conversions.ToDouble(row1["Distress Qty"]), dataRowArray2[0]["UOM_CONV"]);
                  }
                  else
                  {
                    dataRow["ssd_subcomp_qty"] = RuntimeHelpers.GetObjectValue(row1["Subcomponent Qty"]);
                    dataRow["ssd_subcomp_dist_qty"] = RuntimeHelpers.GetObjectValue(row1["Distress Qty"]);
                  }
                  dataRow["ssd_dist_dens_link"] = (object) Sample.DistDensLink(Conversions.ToInteger(row1["Distress"]), Conversions.ToInteger(row1["Severity"]), Conversions.ToInteger(row1["Density"]));
                  dataRow["ssd_critical"] = (object) Conversions.ToBoolean(row1["Critical"]);
                  dataRow["ssd_esc"] = (object) Conversions.ToBoolean(row1["ESC"]);
                  dataRow["ssd_esc_num"] = RuntimeHelpers.GetObjectValue(row1["ESC Number"]);
                  dataRow["ssd_esc_date"] = RuntimeHelpers.GetObjectValue(Interaction.IIf(Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["ESC Date"])), (object) Conversions.ToDate("1/1/0001"), RuntimeHelpers.GetObjectValue(row1["ESC Date"])));
                  row1["BRED_Status"] = (object) "E";
                }
                else
                  Information.Err().Raise(10100, (object) "Sample:SaveSubcomponentData", (object) "Could not locate previous distress record", (object) null, (object) null);
              }
            }
            else
            {
              DataRow row2 = dataTable4.NewRow();
              dataTable4.Rows.Add(row2);
              DataRow dataRow = row2;
              dataRow["BRED_Status"] = (object) "N";
              dataRow["ssd_id"] = (object) mdUtility.GetUniqueID();
              dataRow["ssd_subcomp_id"] = RuntimeHelpers.GetObjectValue(dataRowArray1[0]["ID"]);
              if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.NotObject(dataTable2.Rows[0]["SAMP_SUBCOMP_ALTUM"]), (object) (mdUtility.Units == mdUtility.SystemofMeasure.umEnglish))))
              {
                DataRow[] dataRowArray2 = dataTable3.Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "[CMC_SCOMP_ID]=", row1["Subcomplink"])));
                dataRow["ssd_subcomp_qty"] = RuntimeHelpers.GetObjectValue(row1["Subcomponent Qty"]);
                if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["ssd_subcomp_qty"])))
                  dataRow["ssd_subcomp_qty"] = Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) Conversions.ToDouble(row1["Subcomponent Qty"]), dataRowArray2[0]["UOM_CONV"]);
                dataRow["ssd_subcomp_dist_qty"] = RuntimeHelpers.GetObjectValue(row1["Distress Qty"]);
                if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["ssd_subcomp_dist_qty"])))
                  dataRow["ssd_subcomp_dist_qty"] = Microsoft.VisualBasic.CompilerServices.Operators.DivideObject((object) Conversions.ToDouble(row1["Distress Qty"]), dataRowArray2[0]["UOM_CONV"]);
              }
              else
              {
                dataRow["ssd_subcomp_qty"] = RuntimeHelpers.GetObjectValue(row1["Subcomponent Qty"]);
                dataRow["ssd_subcomp_dist_qty"] = RuntimeHelpers.GetObjectValue(row1["Distress Qty"]);
              }
              dataRow["ssd_dist_dens_link"] = (object) Sample.DistDensLink(Conversions.ToInteger(row1["Distress"]), Conversions.ToInteger(row1["Severity"]), Conversions.ToInteger(row1["Density"]));
              dataRow["ssd_critical"] = (object) Conversions.ToBoolean(row1["Critical"]);
              dataRow["ssd_esc"] = (object) Conversions.ToBoolean(row1["ESC"]);
              dataRow["ssd_esc_num"] = RuntimeHelpers.GetObjectValue(row1["ESC Number"]);
              dataRow["ssd_esc_date"] = RuntimeHelpers.GetObjectValue(row1["ESC Date"]);
              row1["ID"] = RuntimeHelpers.GetObjectValue(row2["ssd_id"]);
              row1["BRED_Status"] = (object) "E";
            }
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        mdUtility.get_MstrTable("Distresses").AcceptChanges();
        mdUtility.DB.SaveDataTable(ref dataTable1, str);
        flag = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Sample), nameof (SaveDistressData));
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private static bool SaveDirectRatingData(string SampleID)
    {
      bool flag1;
      try
      {
        bool flag2 = false;
        frmMain fMainForm = mdUtility.fMainForm;
        if (fMainForm.optCompRating_1.Checked | fMainForm.optCompRating_2.Checked | fMainForm.optCompRating_3.Checked | fMainForm.optCompRating_4.Checked | fMainForm.optCompRating_5.Checked | fMainForm.optCompRating_6.Checked | fMainForm.optCompRating_7.Checked | fMainForm.optCompRating_8.Checked | fMainForm.optCompRating_9.Checked)
          flag2 = true;
        if (!flag2)
        {
          if (fMainForm.optRatingType_1.Checked | fMainForm.optRatingType_3.Checked)
          {
            int num1 = (int) Interaction.MsgBox((object) "You must rate the section before saving the sample.", MsgBoxStyle.Critical, (object) null);
          }
          else
          {
            int num2 = (int) Interaction.MsgBox((object) "You must rate the section before saving the inspection.", MsgBoxStyle.Critical, (object) null);
          }
          flag1 = false;
        }
        else
        {
          string str = "SELECT * FROM sample_data WHERE [samp_data_id]={" + SampleID + "}";
          DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
          DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM RO_CIRating");
          if (dataTable1.Rows.Count == 0)
            throw new Exception("Unable to save sample data.  Sample data record not found.");
          DataRow[] dataRowArray1 = !fMainForm.optCompRating_1.Checked ? (!fMainForm.optCompRating_2.Checked ? (!fMainForm.optCompRating_3.Checked ? (!fMainForm.optCompRating_4.Checked ? (!fMainForm.optCompRating_5.Checked ? (!fMainForm.optCompRating_6.Checked ? (!fMainForm.optCompRating_7.Checked ? (!fMainForm.optCompRating_8.Checked ? (!fMainForm.optCompRating_9.Checked ? (DataRow[]) null : dataTable2.Select("CIIndex=9")) : dataTable2.Select("CIIndex=8")) : dataTable2.Select("CIIndex=7")) : dataTable2.Select("CIIndex=6")) : dataTable2.Select("CIIndex=5")) : dataTable2.Select("CIIndex=4")) : dataTable2.Select("CIIndex=3")) : dataTable2.Select("CIIndex=2")) : dataTable2.Select("CIIndex=1");
          if (dataRowArray1 != null)
            dataTable1.Rows[0]["samp_data_comp_rate"] = RuntimeHelpers.GetObjectValue(dataRowArray1[0]["CIRateValue"]);
          DataRow[] dataRowArray2 = !fMainForm.optPaintRating_1.Checked ? (!fMainForm.optPaintRating_2.Checked ? (!fMainForm.optPaintRating_3.Checked ? (!fMainForm.optPaintRating_4.Checked ? (!fMainForm.optPaintRating_5.Checked ? (!fMainForm.optPaintRating_6.Checked ? (!fMainForm.optPaintRating_7.Checked ? (!fMainForm.optPaintRating_8.Checked ? (!fMainForm.optPaintRating_9.Checked ? (DataRow[]) null : dataTable2.Select("CIIndex=9")) : dataTable2.Select("CIIndex=8")) : dataTable2.Select("CIIndex=7")) : dataTable2.Select("CIIndex=6")) : dataTable2.Select("CIIndex=5")) : dataTable2.Select("CIIndex=4")) : dataTable2.Select("CIIndex=3")) : dataTable2.Select("CIIndex=2")) : dataTable2.Select("CIIndex=1");
          if (dataRowArray2 != null)
            dataTable1.Rows[0]["samp_data_paint_rate"] = RuntimeHelpers.GetObjectValue(dataRowArray2[0]["CIRateValue"]);
          mdUtility.DB.SaveDataTable(ref dataTable1, str);
          mdUtility.DB.ExecuteCommand("DELETE FROM Sample_Subcomponent WHERE SAMP_SUBCOMP_SAMP_ID={" + Microsoft.VisualBasic.Strings.Replace(SampleID, "'", "''", 1, -1, CompareMethod.Binary) + "}", false);
          flag1 = true;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Sample), nameof (SaveDirectRatingData));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
      return flag1;
    }

    public static int DistDensLink(int Distress, int Severity, int Density)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT Link FROM qryDistDensSevLink WHERE RO_Distress.DIST_ID=" + Conversions.ToString(Distress) + " And RO_Distress_Severity.DIST_SEV_SEV=" + Conversions.ToString(Severity) + " And RO_Distress_Density.DIST_DENS_DENS=" + Conversions.ToString(Density));
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(dataTable.Rows[0]["Link"]);
      return -1;
    }

    public static string NewSample(string InspectionID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT SUM(samp_data_qty) FROM sample_data WHERE [samp_data_insp_data_id]={" + InspectionID + "}");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM qrySamplesSection WHERE [INSP_DATA_ID]={" + InspectionID + "}");
      string one;
      if (dataTable2.Rows.Count > 0)
      {
        DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT * FROM Component_Section WHERE [SEC_ID]={" + dataTable2.Rows[0]["sec_id"].ToString() + "}");
        if (dataTable3.Rows.Count == 0)
          throw new Exception("Unable to find the samples parent section.");
        frmNewSample frmNewSample = new frmNewSample();
        frmNewSample.dQty = Conversions.ToDouble(Microsoft.VisualBasic.CompilerServices.Operators.SubtractObject(dataTable3.Rows[0]["SEC_QTY"], dataTable1.Rows[0][0]));
        if (frmNewSample.dQty < 0.0)
          frmNewSample.dQty = 0.0;
        one = frmNewSample.CreateOne(ref InspectionID);
      }
      return one;
    }

    public static void DeleteSample(string SampleID)
    {
      Sample.DeleteSubcomponent(SampleID);
      string str = "SELECT * FROM Sample_Data WHERE [Samp_Data_ID]={" + SampleID + "}";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      if (dataTable.Rows.Count == 0)
        throw new Exception("Unable to delete the sample. Sample was not found.");
      DataRow row = dataTable.Rows[0];
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
        row["BRED_Status"] = (object) "D";
      else
        row.Delete();
      mdUtility.DB.SaveDataTable(ref dataTable, str);
    }

    private static void DeleteSubcomponent(string strSampleID)
    {
      string str = "SELECT * FROM [Sample_Subcomponent] WHERE [SAMP_SUBCOMP_SAMP_ID]={" + strSampleID + "}";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          Sample.DeleteDistress(row["samp_subcomp_id"].ToString());
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
            row["BRED_Status"] = (object) "D";
          else
            row.Delete();
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
    }

    private static void DeleteDistress(string strSubcomponentID)
    {
      string str = "SELECT * FROM Sample_Subcomp_Distress WHERE [ssd_subcomp_id]={" + strSubcomponentID + "}";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
            row["BRED_Status"] = (object) "D";
          else
            row.Delete();
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
    }

    private static void SetDropDownValueItems(string strCol)
    {
      Infragistics.Win.ValueList valueList = new Infragistics.Win.ValueList();
      string sSQL = "";
      UltraGrid ugSubcomponents = mdUtility.fMainForm.ugSubcomponents;
      string Left = strCol;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "SAMP_SUBCOMP_PAINTRATE", false) != 0)
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "SAMP_SUBCOMP_CONDRATE", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "SAMP_SUBCOMP_SUBSTRATE", false) == 0)
          {
            if (ugSubcomponents.DisplayLayout.ValueLists.Exists("Substrate"))
              return;
            valueList = ugSubcomponents.DisplayLayout.ValueLists.Add("Substrate");
            sSQL = "SELECT Substrate_ID as ID, Substrate_Desc as DisplayName FROM RO_PaintSubstrate";
          }
        }
        else
        {
          if (ugSubcomponents.DisplayLayout.ValueLists.Exists("CIRating1"))
            return;
          valueList = ugSubcomponents.DisplayLayout.ValueLists.Add("CIRating1");
          sSQL = "SELECT CIRateValue as ID, CIRateName as DisplayName FROM RO_CIRating";
        }
      }
      else
      {
        if (ugSubcomponents.DisplayLayout.ValueLists.Exists("CIRating"))
          return;
        valueList = ugSubcomponents.DisplayLayout.ValueLists.Add("CIRating");
        sSQL = "SELECT CIRateValue as ID, CIRateName as DisplayName FROM RO_CIRating";
      }
      DataTable dataTable = mdUtility.DB.GetDataTable(sSQL);
      try
      {
        foreach (DataRow row in dataTable.Rows)
          valueList.ValueListItems.Add(RuntimeHelpers.GetObjectValue(row["ID"]), Conversions.ToString(row["DisplayName"]));
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      valueList.MaxDropDownItems = 12;
      valueList.DisplayStyle = ValueListDisplayStyle.Default;
      ugSubcomponents.DisplayLayout.Bands[0].Columns[strCol].ValueList = (IValueList) valueList;
      ugSubcomponents.DisplayLayout.Bands[0].Columns[strCol].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
      ugSubcomponents.DisplayLayout.Bands[0].Columns[strCol].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
    }

    public static string SampleLocationID(string strName, ref bool bNew)
    {
      string str1;
      try
      {
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strName, "", false) > 0U)
        {
          string str2 = "SELECT * FROM Sample_Location WHERE [Building_ID] = '" + mdUtility.fMainForm.CurrentBldg + "' and [Name]='" + Microsoft.VisualBasic.Strings.Replace(strName, "'", "''", 1, -1, CompareMethod.Binary) + "'";
          DataTable dataTable1 = mdUtility.DB.GetDataTable(str2);
          DataTable dataTable2 = dataTable1;
          if (dataTable1.Rows.Count > 0)
          {
            bNew = false;
            str1 = Conversions.ToString(dataTable1.Rows[0]["Location_ID"]);
          }
          else
          {
            DataRow row = dataTable2.NewRow();
            string uniqueId = mdUtility.GetUniqueID();
            row["Location_ID"] = (object) uniqueId;
            row["Building_ID"] = (object) mdUtility.fMainForm.CurrentBldg;
            row["Name"] = (object) strName;
            row["BRED_Status"] = (object) "N";
            dataTable2.Rows.Add(row);
            mdUtility.DB.SaveDataTable(ref dataTable1, str2);
            bNew = true;
            str1 = uniqueId;
          }
        }
        else
          str1 = "";
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Sample), nameof (SampleLocationID));
        str1 = "";
        ProjectData.ClearProjectError();
      }
      return str1;
    }

    public static void DeleteSampleLocationsForBuilding(string strBldgID)
    {
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strBldgID, "", false) <= 0U)
        return;
      string str = "SELECT * FROM Sample_Location WHERE Building_ID ={" + strBldgID + "}";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      try
      {
        foreach (DataRow row in dataTable.Rows)
          row.Delete();
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
    }
  }
}
