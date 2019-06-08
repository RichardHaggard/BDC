// Decompiled with JetBrains decompiler
// Type: BuilderRED.FunctionalArea
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace BuilderRED
{
  internal class FunctionalArea
  {
    public static string AddFuncArea(
      string AreaName,
      string BldgID,
      int AreaType,
      int UseType,
      double AreaSize,
      string unitOM)
    {
      try
      {
        mdUtility.DB.BeginTransaction();
        string str1 = "SELECT * FROM Functional_Area WHERE [Name]='" + AreaName + "' AND [BLDG_ID]='" + BldgID + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str1);
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(unitOM, "Size(*SF):", false) == 0)
          AreaSize *= 0.092903;
        string str2;
        if (dataTable.Rows.Count > 0)
        {
          str2 = "Duplicate";
        }
        else
        {
          DataRow row = dataTable.NewRow();
          dataTable.Rows.Add(row);
          DataRow dataRow = row;
          str2 = mdUtility.GetUniqueID();
          string str3 = BldgID;
          dataRow["Area_ID"] = (object) str2;
          dataRow["BLDG_ID"] = (object) str3;
          dataRow["bred_status"] = (object) "N";
          dataRow["Name"] = (object) AreaName;
          dataRow["FunctionalUsetype_Link"] = (object) AreaType;
          dataRow["CategoryCode_Link"] = (object) UseType;
          dataRow[nameof (AreaSize)] = (object) AreaSize;
          dataRow["FI"] = (object) DBNull.Value;
          dataRow["CI"] = (object) DBNull.Value;
          dataRow["FA_User"] = (object) DBNull.Value;
          mdUtility.DB.SaveDataTable(ref dataTable, str1);
        }
        mdUtility.DB.CommitTransaction();
        return str2;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        throw ex;
      }
    }

    internal static void DeleteFuncArea(string strAreaID)
    {
      try
      {
        string str = "SELECT * FROM Functional_Area WHERE [Area_ID]='" + strAreaID + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count > 0)
        {
          if (Information.IsDBNull((object) dataTable.Rows[0]) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable.Rows[0]["bred_status"], (object) "N", false))
            dataTable.Rows[0]["BRED_Status"] = (object) "D";
          else
            dataTable.Rows[0].Delete();
          mdUtility.DB.SaveDataTable(ref dataTable, str);
        }
        if (mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strAreaID) != null)
        {
          mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strAreaID).Nodes.Clear();
          mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strAreaID).Parent.Nodes.Remove(mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strAreaID));
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        Exception exception = ex;
        if (Information.Err().Number != 35601)
          throw exception;
        ProjectData.ClearProjectError();
      }
      mdUtility.DB.CloseLongConnection();
    }

    internal static bool FuncAreaIsNew(string strAreaID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT BRED_Status FROM Functional_Area WHERE [Area_ID]='" + strAreaID + "'");
      bool flag;
      if (dataTable.Rows.Count > 0)
      {
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["bred_status"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[0]["bred_status"], (object) "N", false))
          flag = true;
      }
      else
        flag = false;
      return flag;
    }
  }
}
