// Decompiled with JetBrains decompiler
// Type: BuilderRED.BuildingSystem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinTree;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;

namespace BuilderRED
{
  internal class BuildingSystem
  {
    internal static string AddSystem(string BldgID, int SysLink)
    {
      string str = "SELECT * from Building_System WHERE [BLDG_SYS_BLDG_ID]='" + BldgID + "' AND [bldg_sys_link]=" + Conversions.ToString(SysLink);
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      string uniqueId;
      if (dataTable.Rows.Count > 0)
      {
        dataTable.Rows[0]["BRED_Status"] = (object) "U";
        uniqueId = Conversions.ToString(dataTable.Rows[0]["BLDG_SYS_ID"]);
      }
      else
      {
        DataRow row = dataTable.NewRow();
        row["BLDG_SYS_BLDG_ID"] = (object) BldgID;
        uniqueId = mdUtility.GetUniqueID();
        row["BLDG_SYS_ID"] = (object) uniqueId;
        row["BLDG_SYS_LINK"] = (object) SysLink;
        row["bred_status"] = (object) "N";
        dataTable.Rows.Add(row);
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
      return uniqueId;
    }

    internal static void DeleteSystem(string strSystemID)
    {
      try
      {
        DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT [sys_comp_id] FROM system_component WHERE [sys_comp_bldg_sys_id]='" + strSystemID + "'");
        try
        {
          foreach (DataRow row in dataTable1.Rows)
            Component.DeleteComponent(row["sys_comp_id"].ToString());
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        string str = "SELECT * FROM building_system WHERE [bldg_sys_id]='" + strSystemID + "'";
        DataTable dataTable2 = mdUtility.DB.GetDataTable(str);
        if (dataTable2.Rows.Count > 0)
        {
          if (Information.IsDBNull((object) dataTable2.Rows[0]) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable2.Rows[0]["bred_status"], (object) "N", false))
            dataTable2.Rows[0]["BRED_Status"] = (object) "D";
          else
            dataTable2.Rows[0].Delete();
          mdUtility.DB.SaveDataTable(ref dataTable2, str);
        }
        if (mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystemID) != null)
        {
          mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystemID).Nodes.Clear();
          mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystemID).Parent.Nodes.Remove(mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystemID));
        }
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strSystemID) == null)
          return;
        frmMain fMainForm = mdUtility.fMainForm;
        UltraTreeNode nodeByKey = mdUtility.fMainForm.tvInspection.GetNodeByKey(strSystemID);
        ref UltraTreeNode local = ref nodeByKey;
        fMainForm.PurgeInspectionNode(ref local);
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

    internal static int SystemLink(string SystemID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT BLDG_SYS_Link FROM Building_System WHERE [BLDG_SYS_ID]='" + SystemID + "'");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(dataTable.Rows[0]["BLDG_SYS_Link"]);
      return -1;
    }

    internal static string Building(string SystemID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT BLDG_SYS_BLDG_ID FROM Building_System WHERE [BLDG_SYS_ID]='" + SystemID + "'");
      if (dataTable.Rows.Count > 0)
        return dataTable.Rows[0]["BLDG_SYS_BLDG_ID"].ToString();
      return Conversions.ToString(-1);
    }

    internal static bool SystemIsNew(string strSysID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT bred_status FROM Building_System WHERE [bldg_sys_id]='" + strSysID + "'");
      if (dataTable1.Rows.Count > 0)
      {
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable1.Rows[0]["bred_status"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable1.Rows[0]["bred_status"], (object) "N", false))
          return true;
        DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM System_Component WHERE [sys_comp_bldg_sys_id]='" + strSysID + "'");
        try
        {
          foreach (DataRow row1 in dataTable2.Rows)
          {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row1["bred_status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row1["bred_status"], (object) "N", false))
              return false;
            DataTable dataTable3 = mdUtility.DB.GetDataTable(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM Component_Section WHERE [sec_sys_comp_id]='", row1["sys_comp_id"]), (object) "'")));
            try
            {
              foreach (DataRow row2 in dataTable3.Rows)
              {
                if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row2["bred_status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row2["bred_status"], (object) "N", false))
                  return false;
              }
            }
            finally
            {
              IEnumerator enumerator;
              if (enumerator is IDisposable)
                (enumerator as IDisposable).Dispose();
            }
            dataTable3.Dispose();
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      return false;
    }
  }
}
