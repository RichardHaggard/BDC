// Decompiled with JetBrains decompiler
// Type: BuilderRED.Building
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace BuilderRED
{
  internal class Building
  {
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static string AddBuilding(
      string Number,
      string Name,
      string NoFloors,
      string YrBuilt,
      string YrRenovated,
      string BldgArea,
      string CatCode,
      string ConstType,
      string AlternateID,
      string AlternateIDSource,
      string Address,
      string City,
      string State,
      string ZipCode,
      string POC,
      string POC_Phone,
      string POC_Email,
      ref string BldgLabel)
    {
      try
      {
        mdUtility.DB.BeginTransaction();
        mdUtility.DB.GetDataTable("SELECT * FROM RO_Builder_UOM WHERE Bldr_UOM_Caption='Area'");
        string str1 = "SELECT * FROM Facility WHERE Number='" + Strings.Replace(Number, "'", "''", 1, -1, CompareMethod.Binary) + "' and Name='" + Strings.Replace(Name, "'", "''", 1, -1, CompareMethod.Binary) + "'";
        DataTable dataTable1 = mdUtility.DB.GetDataTable(str1);
        string uniqueId;
        if (dataTable1.Rows.Count > 0)
        {
          dataTable1.Rows[0]["BRED_Status"] = (object) "U";
          uniqueId = Conversions.ToString(dataTable1.Rows[0]["Facility_ID"]);
        }
        else
        {
          DataRow row1 = dataTable1.NewRow();
          dataTable1.Rows.Add(row1);
          DataRow dataRow1 = row1;
          uniqueId = mdUtility.GetUniqueID();
          dataRow1["Facility_ID"] = (object) uniqueId;
          dataRow1["bred_status"] = (object) "N";
          dataRow1[nameof (Number)] = (object) Number;
          dataRow1[nameof (Name)] = (object) Name;
          dataRow1["Type"] = (object) 1;
          dataRow1["Quantity"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(BldgArea, "", false) <= 0U ? (object) DBNull.Value : (!Versioned.IsNumeric((object) BldgArea) ? (object) DBNull.Value : (!mdUtility.fMainForm.miUnits.Checked ? (object) Conversions.ToDouble(BldgArea) : (object) (Conversions.ToDouble(BldgArea) / Building.GetUnitsConversionFactor((short?) Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(CatCode, "", false) > 0U, (object) Conversions.ToShort(CatCode), (object) null)))));
          dataRow1["YearConstructed"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(YrBuilt, "", false) > 0U, (object) YrBuilt, (object) DBNull.Value));
          dataRow1["YearRenovated"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(YrRenovated, "", false) > 0U, (object) YrRenovated, (object) DBNull.Value));
          dataRow1["CategoryCode_link"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(CatCode, "", false) > 0U, (object) CatCode, (object) DBNull.Value));
          dataRow1["ConstructionType_Link"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ConstType, "", false) > 0U, (object) ConstType, (object) DBNull.Value));
          dataRow1["Status_ID"] = (object) 1;
          dataRow1["bldg_no_floors"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(NoFloors, "", false) > 0U, (object) NoFloors, (object) DBNull.Value));
          dataRow1["alternate_id"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(AlternateID, "", false) > 0U, (object) AlternateID, (object) DBNull.Value));
          dataRow1["alternate_id_source"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(AlternateIDSource, "", false) > 0U, (object) AlternateIDSource, (object) DBNull.Value));
          dataRow1["bldg_strt"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Address, "", false) > 0U, (object) Address, (object) DBNull.Value));
          dataRow1["bldg_city"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(City, "", false) > 0U, (object) City, (object) DBNull.Value));
          dataRow1["bldg_st"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(State, "", false) > 0U, (object) State, (object) DBNull.Value));
          dataRow1["bldg_zip"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ZipCode, "", false) > 0U, (object) ZipCode, (object) DBNull.Value));
          dataRow1["bldg_poc_name"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(POC, "", false) > 0U, (object) POC, (object) DBNull.Value));
          dataRow1["bldg_poc_ph_no"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(POC_Phone, "", false) > 0U, (object) POC_Phone, (object) DBNull.Value));
          dataRow1["bldg_poc_email"] = RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(POC_Email, "", false) > 0U, (object) POC_Email, (object) DBNull.Value));
          mdUtility.DB.SaveDataTable(ref dataTable1, str1);
          DataTable dataTable2 = mdUtility.DB.GetDataTable("Select Org_ID from Organization where Org_Type = 1");
          if (dataTable2.Rows.Count > 0)
          {
            string str2 = "Select * from Organization_Facilities";
            DataTable dataTable3 = mdUtility.DB.GetDataTable(str2);
            DataRow row2 = dataTable3.NewRow();
            dataTable3.Rows.Add(row2);
            DataRow dataRow2 = row2;
            dataRow2["BRED_STATUS"] = (object) "N";
            dataRow2["Organization_ID"] = RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["ORG_ID"]);
            dataRow2["Facility_ID"] = (object) uniqueId;
            DataTable dataTable4 = mdUtility.DB.GetDataTable("SELECT Org_ID from Organization WHERE Org_Type = 2 AND ORG_NAME='Unassigned'");
            if (dataTable4.Rows.Count > 0)
            {
              DataRow row3 = dataTable3.NewRow();
              dataTable3.Rows.Add(row3);
              DataRow dataRow3 = row3;
              dataRow3["BRED_STATUS"] = (object) "N";
              dataRow3["Organization_ID"] = RuntimeHelpers.GetObjectValue(dataTable4.Rows[0]["ORG_ID"]);
              dataRow3["Facility_ID"] = (object) uniqueId;
            }
            else
              Information.Err().Raise(10100, (object) "Building: AddBuilding", (object) "Unable to find an Unassigned Complex in the Organization table.", (object) null, (object) null);
            mdUtility.DB.SaveDataTable(ref dataTable3, str2);
          }
          else
            Information.Err().Raise(10100, (object) "Building: AddBuilding", (object) "Unable to find a Site in the Organization table.", (object) null, (object) null);
        }
        mdUtility.DB.CommitTransaction();
        BldgLabel = Building.Label((object) Number, (object) Name);
        return uniqueId;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        Exception exception = ex;
        mdUtility.DB.RollbackTransaction();
        throw exception;
      }
    }

    public static void DeleteBuilding(string strBuildingID)
    {
      try
      {
        mdUtility.DB.BeginTransaction();
        DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM Building_System WHERE bldg_sys_bldg_id='" + strBuildingID + "'");
        try
        {
          foreach (DataRow row in dataTable1.Rows)
            BuildingSystem.DeleteSystem(row["bldg_sys_id"].ToString());
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        Sample.DeleteSampleLocationsForBuilding(strBuildingID);
        string str1 = "SELECT * FROM Facility WHERE [Facility_ID]='" + strBuildingID + "'";
        DataTable dataTable2 = mdUtility.DB.GetDataTable(str1);
        if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["bred_status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable2.Rows[0]["bred_status"], (object) "N", false))
          dataTable2.Rows[0]["bred_status"] = (object) "D";
        else
          dataTable2.Rows[0].Delete();
        mdUtility.DB.SaveDataTable(ref dataTable2, str1);
        string str2 = "Select * from Organization_Facilities where Facility_ID = '" + strBuildingID + "'";
        DataTable dataTable3 = mdUtility.DB.GetDataTable(str2);
        try
        {
          foreach (DataRow row in dataTable3.Rows)
          {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_STATUS"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_STATUS"], (object) "N", false))
              row["BRED_STATUS"] = (object) "D";
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
        mdUtility.DB.SaveDataTable(ref dataTable3, str2);
        string str3 = "SELECT * FROM Missing_Components WHERE Building_ID = '" + strBuildingID + "'";
        DataTable dataTable4 = mdUtility.DB.GetDataTable(str3);
        if (dataTable4.Rows.Count > 0)
        {
          try
          {
            foreach (DataRow row in dataTable4.Rows)
              row.Delete();
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          mdUtility.DB.SaveDataTable(ref dataTable4, str3);
        }
        string str4 = "SELECT * FROM ADA_Assessment WHERE Building_ID = '" + strBuildingID + "'";
        DataTable dataTable5 = mdUtility.DB.GetDataTable(str4);
        if (dataTable5.Rows.Count > 0)
        {
          string str5 = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM ADA_Attributes WHERE ADA_Assessment_ID = '", dataTable5.Rows[0]["ADA_Assessment_ID"]), (object) "'"));
          DataTable dataTable6 = mdUtility.DB.GetDataTable(str5);
          if (dataTable6.Rows.Count > 0)
          {
            try
            {
              foreach (DataRow row in dataTable6.Rows)
                row.Delete();
            }
            finally
            {
              IEnumerator enumerator;
              if (enumerator is IDisposable)
                (enumerator as IDisposable).Dispose();
            }
            mdUtility.DB.SaveDataTable(ref dataTable6, str5);
          }
          dataTable5.Rows[0].Delete();
          mdUtility.DB.SaveDataTable(ref dataTable5, str4);
        }
        mdUtility.fMainForm.tvInventory.Nodes.Remove(mdUtility.fMainForm.tvInventory.Nodes[strBuildingID]);
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuildingID) != null)
        {
          mdUtility.fMainForm.tvInspection.Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuildingID));
          mdUtility.fMainForm.tvInspection.Refresh();
        }
        mdUtility.DB.CommitTransaction();
        mdUtility.fMainForm.tvInventory.Refresh();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        Exception exception = ex;
        if (Information.Err().Number != 35601)
        {
          mdUtility.DB.RollbackTransaction();
          throw exception;
        }
        ProjectData.ClearProjectError();
      }
    }

    public static bool SaveBuilding(string BuildingID)
    {
      bool flag1 = false;
      bool flag2;
      try
      {
        if (Building.OkToSave())
        {
          mdUtility.DB.GetDataTable("SELECT * FROM RO_Builder_UOM WHERE bldr_uom_caption='Area'");
          string str = "SELECT * FROM Facility WHERE [Facility_id]='" + BuildingID + "'";
          DataTable dataTable = mdUtility.DB.GetDataTable(str);
          if (dataTable.Rows.Count == 0)
            throw new Exception("Unable to save building data.  Building was not found.");
          DataRow row = dataTable.Rows[0];
          frmMain fMainForm = mdUtility.fMainForm;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(row["Number"].ToString(), fMainForm.txtBuildingNumber.Text, false) != 0 || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["Name"], (object) fMainForm.txtBuildingName.Text, false))
            flag1 = true;
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
            row["bred_status"] = (object) "U";
          row["Number"] = (object) fMainForm.txtBuildingNumber.Text;
          row["Name"] = (object) fMainForm.txtBuildingName.Text;
          row["CategoryCode_Link"] = fMainForm.cboCatCode.SelectedIndex == -1 ? (object) DBNull.Value : RuntimeHelpers.GetObjectValue(fMainForm.cboCatCode.SelectedValue);
          row["ConstructionType_Link"] = fMainForm.cboConstructionType.SelectedIndex == -1 ? (object) DBNull.Value : RuntimeHelpers.GetObjectValue(fMainForm.cboConstructionType.SelectedValue);
          row["YearConstructed"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtYearBuilt.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtYearBuilt.Text;
          row["YearRenovated"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtYearRenovated.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtYearRenovated.Text;
          if ((uint) fMainForm.rcbInspIssue.SelectedIndex > 0U)
            row["BLDG_Inspection_Issue"] = (object) fMainForm.rcbInspIssue.SelectedIndex;
          row["BLDG_DNC_Systems"] = (object) mdUtility.fMainForm.CurrentDoesNotContain;
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtBldgArea.Text, "", false) > 0U)
            row["Quantity"] = !Versioned.IsNumeric((object) fMainForm.txtBldgArea.Text) ? (object) DBNull.Value : (!fMainForm.miUnits.Checked ? (object) Conversions.ToDouble(fMainForm.txtBldgArea.Text) : (object) (Conversions.ToDouble(fMainForm.txtBldgArea.Text) / Building.GetUnitsConversionFactor((short?) row["CategoryCode_Link"])));
          row["bldg_no_floors"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtNoFloors.Text, "", false) <= 0U ? (object) DBNull.Value : (!Versioned.IsNumeric((object) fMainForm.txtNoFloors.Text) ? (object) DBNull.Value : (object) Conversions.ToInteger(fMainForm.txtNoFloors.Text));
          row["alternate_id"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Strings.Trim(fMainForm.txtAlternateID.Text), "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtAlternateID.Text;
          row["alternate_id_source"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Strings.Trim(fMainForm.txtAlternateIDSource.Text), "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtAlternateIDSource.Text;
          row["bldg_strt"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtAddress.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtAddress.Text;
          row["bldg_city"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtCity.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtCity.Text;
          row["bldg_st"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtState.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtState.Text;
          row["bldg_zip"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtZipCode.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtZipCode.Text;
          row["bldg_poc_name"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtPOC.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtPOC.Text;
          row["bldg_poc_ph_no"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtPOCPhone.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtPOCPhone.Text;
          row["bldg_poc_email"] = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fMainForm.txtPOCEmail.Text, "", false) <= 0U ? (object) DBNull.Value : (object) fMainForm.txtPOCEmail.Text;
          mdUtility.DB.SaveDataTable(ref dataTable, str);
          if (flag1)
          {
            mdUtility.fMainForm.tvInventory.GetNodeByKey(BuildingID).Text = Building.Label((object) fMainForm.txtBuildingNumber.Text, (object) fMainForm.txtBuildingName.Text);
            mdUtility.fMainForm.tvInspection.GetNodeByKey(BuildingID).Text = Building.Label((object) fMainForm.txtBuildingNumber.Text, (object) fMainForm.txtBuildingName.Text);
          }
          fMainForm.BldgNeedToSave = false;
          fMainForm.m_bBldgLoaded = false;
          Building.LoadBuilding(mdUtility.fMainForm.CurrentBldg);
          fMainForm.m_bBldgLoaded = true;
          Building.LockBuilding(false);
          flag2 = true;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (Building), nameof (SaveBuilding));
        flag2 = false;
        ProjectData.ClearProjectError();
      }
      return flag2;
    }

    public static void LoadBuilding(string BldgID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM BuildingInfo WHERE [Facility_ID]='" + BldgID + "'");
      mdUtility.DB.GetDataTable("SELECT * FROM RO_Builder_UOM WHERE BLDR_UOM_Caption='Area'");
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM RO_BuildingInspection_Issue");
      if (dataTable1.Rows.Count == 0)
        throw new Exception("Unable to load building info.  Building was not found.");
      DataRow row = dataTable1.Rows[0];
      frmMain fMainForm = mdUtility.fMainForm;
      fMainForm.m_bBldgLoaded = false;
      fMainForm.txtBuildingNumber.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["Number"]), (object) ""));
      fMainForm.txtBuildingName.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["Name"]), (object) ""));
      fMainForm.cboCatCode.SelectedValue = RuntimeHelpers.GetObjectValue(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["CategoryCode_Link"]), (object) -1));
      fMainForm.cboConstructionType.SelectedValue = RuntimeHelpers.GetObjectValue(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["ConstructionType_Link"]), (object) -1));
      fMainForm.rcbInspIssue.ValueMember = "ID";
      fMainForm.rcbInspIssue.DisplayMember = "Description";
      fMainForm.rcbInspIssue.DataSource = (object) dataTable2;
      fMainForm.rcbInspIssue.SelectedIndex = Conversions.ToInteger(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["BLDG_Inspection_Issue"]), (object) -1));
      fMainForm.txtAlternateID.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["alternate_id"]), (object) ""));
      fMainForm.txtAlternateIDSource.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["alternate_id_source"]), (object) ""));
      fMainForm.CurrentDoesNotContain = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["BLDG_DNC_Systems"]), (object) "0000"));
      fMainForm.txtYearBuilt.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["YearConstructed"]), (object) ""));
      fMainForm.txtYearRenovated.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["YearRenovated"]), (object) ""));
      fMainForm.txtNoFloors.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_no_floors"]), (object) ""));
      fMainForm.txtAddress.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_strt"]), (object) ""));
      fMainForm.txtCity.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_city"]), (object) ""));
      fMainForm.txtState.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_st"]), (object) ""));
      fMainForm.txtZipCode.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_zip"]), (object) ""));
      fMainForm.txtPOC.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_poc_name"]), (object) ""));
      fMainForm.txtPOCPhone.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_poc_ph_no"]), (object) ""));
      fMainForm.txtPOCEmail.Text = Conversions.ToString(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(row["bldg_poc_email"]), (object) ""));
      fMainForm.txtBldgArea.Text = Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["Quantity"])) ? Conversions.ToString(0) : (!fMainForm.miUnits.Checked ? Strings.FormatNumber(RuntimeHelpers.GetObjectValue(row["Quantity"]), 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault) : Strings.FormatNumber(Microsoft.VisualBasic.CompilerServices.Operators.MultiplyObject(row["Quantity"], (object) Building.GetUnitsConversionFactor(new short?(checked ((short) int.Parse(Conversions.ToString(row["CategoryCode_Link"])))))), 2, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault));
      fMainForm.lblBldgSF.Text = Building.GetUnitsLabel((short?) row["CategoryCode_Link"]);
      if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["bldg_comments"])))
        fMainForm.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
      else
        fMainForm.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      fMainForm.BldgNeedToSave = false;
      fMainForm.m_bBldgLoaded = true;
      Building.LockBuilding(false);
    }

    public static void LockBuilding(bool Lock)
    {
      mdUtility.fMainForm.txtBuildingNumber.ReadOnly = Lock;
      mdUtility.fMainForm.txtBuildingName.ReadOnly = Lock;
      mdUtility.fMainForm.txtBldgArea.ReadOnly = Lock;
      mdUtility.fMainForm.txtYearBuilt.ReadOnly = Lock;
      mdUtility.fMainForm.txtYearRenovated.ReadOnly = Lock;
      mdUtility.fMainForm.txtNoFloors.ReadOnly = Lock;
      mdUtility.fMainForm.txtAlternateID.ReadOnly = Lock;
      mdUtility.fMainForm.txtAlternateIDSource.ReadOnly = Lock;
      mdUtility.fMainForm.txtAddress.ReadOnly = Lock;
      mdUtility.fMainForm.txtCity.ReadOnly = Lock;
      mdUtility.fMainForm.txtState.ReadOnly = Lock;
      mdUtility.fMainForm.txtZipCode.ReadOnly = Lock;
      mdUtility.fMainForm.txtPOC.ReadOnly = Lock;
      mdUtility.fMainForm.txtPOCEmail.ReadOnly = Lock;
      mdUtility.fMainForm.txtPOCPhone.ReadOnly = Lock;
    }

    public static string GetUnitsLabel(short? categoryLink)
    {
      short? nullable1 = new short?();
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT * FROM RO_Builder_UOM WHERE BLDR_UOM_Caption='Area'");
      if (categoryLink.HasValue)
      {
        using (IDataReader dataReader = mdUtility.DB.GetDataReader("SELECT * FROM RO_Usetype WHERE USETYPE_ID = " + (categoryLink.HasValue ? Conversions.ToString((int) categoryLink.GetValueOrDefault()) : (string) null)))
        {
          if (dataReader.Read())
          {
            try
            {
              nullable1 = (short?) dataReader["UOM_ID"];
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              nullable1 = new short?();
              ProjectData.ClearProjectError();
            }
          }
        }
      }
      frmMain fMainForm = mdUtility.fMainForm;
      bool? nullable2;
      if (!nullable1.HasValue)
      {
        nullable2 = new bool?(false);
      }
      else
      {
        int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        nullable2 = nullable3.HasValue ? new bool?((uint) nullable3.GetValueOrDefault() > 0U) : new bool?();
      }
      string str;
      if (nullable2.GetValueOrDefault())
      {
        DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM RO_Units_Conversion WHERE UOM_ID = " + (nullable1.HasValue ? Conversions.ToString((int) nullable1.GetValueOrDefault()) : (string) null));
        str = !fMainForm.miUnits.Checked ? Conversions.ToString(dataTable2.Rows[0]["uom_met_unit_abbr"]) : Conversions.ToString(dataTable2.Rows[0]["uom_eng_unit_abbr"]);
      }
      else
        str = !fMainForm.miUnits.Checked ? Conversions.ToString(dataTable1.Rows[0]["bldr_uom_met_unit_abbr"]) : Conversions.ToString(dataTable1.Rows[0]["bldr_uom_eng_unit_abbr"]);
      return str;
    }

    public static double GetUnitsConversionFactor(short? categoryLink)
    {
      short? nullable1 = new short?();
      double num = Conversions.ToDouble(mdUtility.DB.GetDataTable("SELECT * FROM RO_Builder_UOM WHERE BLDR_UOM_Caption='Area'").Rows[0]["bldr_uom_con_mult"]);
      if (categoryLink.HasValue)
      {
        using (IDataReader dataReader = mdUtility.DB.GetDataReader("SELECT * FROM RO_Usetype WHERE USETYPE_ID = " + (categoryLink.HasValue ? Conversions.ToString((int) categoryLink.GetValueOrDefault()) : (string) null)))
        {
          if (dataReader.Read())
          {
            try
            {
              nullable1 = (short?) dataReader["UOM_ID"];
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              nullable1 = new short?();
              ProjectData.ClearProjectError();
            }
          }
        }
      }
      frmMain frmMain = mdUtility.fMainForm;
      bool? nullable2;
      if (!nullable1.HasValue)
      {
        nullable2 = new bool?(false);
      }
      else
      {
        int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        nullable2 = nullable3.HasValue ? new bool?((uint) nullable3.GetValueOrDefault() > 0U) : new bool?();
      }
      if (nullable2.GetValueOrDefault())
        num = Conversions.ToDouble(mdUtility.DB.GetDataTable("SELECT * FROM RO_Units_Conversion WHERE UOM_ID = " + (nullable1.HasValue ? Conversions.ToString((int) nullable1.GetValueOrDefault()) : (string) null)).Rows[0]["UOM_CONV"]);
      frmMain = (frmMain) null;
      return num;
    }

    private static bool OkToSave()
    {
      bool flag;
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtBuildingNumber.Text, "", false) == 0 & Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtBuildingName.Text, "", false) == 0)
      {
        int num = (int) Interaction.MsgBox((object) "You must enter a Bldg No. or a Bldg. Name.", MsgBoxStyle.Critical, (object) "Missing Required Field.");
        flag = false;
      }
      else if (!Versioned.IsNumeric((object) mdUtility.fMainForm.txtYearBuilt.Text))
      {
        int num = (int) Interaction.MsgBox((object) "You must enter a valid year in for the built year.", MsgBoxStyle.Critical, (object) null);
        flag = false;
      }
      else if (Conversions.ToInteger(mdUtility.fMainForm.txtYearBuilt.Text) < 1750 | Conversions.ToInteger(mdUtility.fMainForm.txtYearBuilt.Text) > 2100)
      {
        int num = (int) Interaction.MsgBox((object) "You must enter a valid year for the built year.", MsgBoxStyle.Critical, (object) null);
        flag = false;
      }
      else if (!Versioned.IsNumeric((object) mdUtility.fMainForm.txtNoFloors.Text))
      {
        int num = (int) Interaction.MsgBox((object) "You must enter a valid number for the floor count.", MsgBoxStyle.Critical, (object) null);
        flag = false;
      }
      else if (!Versioned.IsNumeric((object) mdUtility.fMainForm.txtBldgArea.Text))
      {
        int num = (int) Interaction.MsgBox((object) "You must enter a valid quantity for the Bldg Area.", MsgBoxStyle.Critical, (object) null);
        flag = false;
      }
      else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.fMainForm.txtYearRenovated.Text, "", false) > 0U)
      {
        if (!Versioned.IsNumeric((object) mdUtility.fMainForm.txtYearRenovated.Text))
        {
          int num = (int) Interaction.MsgBox((object) "You must enter a valid year for the year renovated.", MsgBoxStyle.Critical, (object) null);
          flag = false;
        }
        else if (Conversions.ToInteger(mdUtility.fMainForm.txtYearRenovated.Text) < Conversions.ToInteger(mdUtility.fMainForm.txtYearBuilt.Text))
        {
          int num = (int) Interaction.MsgBox((object) "Year Renovated must be later than the Year Built.", MsgBoxStyle.Critical, (object) null);
          flag = false;
        }
        else
          flag = true;
      }
      else
        flag = true;
      return flag;
    }

    internal static string Label(object Number, object Name)
    {
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(Number)) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(Number, (object) "", false))
        return Conversions.ToString(Name);
      if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(Number)) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(Name, (object) "", false))
        return Conversions.ToString(Number);
      return Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Number, (object) " - "), Name));
    }

    internal static string LabelByID(string BldgID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT Number, Name FROM Facility WHERE [Facility_ID]='" + BldgID + "'");
      if (dataTable.Rows.Count > 0)
        return Building.Label(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["Number"]), RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["Name"]));
      return "";
    }

    public static int BuiltYear(string BldgID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT YearConstructed FROM Facility WHERE [Facility_ID]='" + BldgID + "'");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["YearConstructed"]), (object) -1));
      return -1;
    }

    public static string DoesNotContain(string BldgId)
    {
      string str = "0000";
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT BLDG_DNC_Systems FROM Facility WHERE [Facility_ID='" + BldgId + "'");
      if (dataTable.Rows.Count > 0 && !dataTable.Rows[0].IsNull(0))
        str = Conversions.ToString(dataTable.Rows[0]["BLDG_DNC_Systems"]);
      return str;
    }

    public static string Installation(string BldgID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT ORG_ID FROM InstallationBuilding WHERE [Facility_ID]='" + BldgID + "'");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToString(dataTable.Rows[0]["ORG_ID"]);
      return "";
    }

    public static bool BuildingIsNew(string strBldgID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT bred_status FROM Facility WHERE [Facility_ID]='" + strBldgID + "'");
      return dataTable.Rows.Count > 0 && (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["bred_status"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["bred_status"], (object) "N", false));
    }
  }
}
