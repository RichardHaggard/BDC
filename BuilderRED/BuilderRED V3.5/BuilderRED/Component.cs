// Decompiled with JetBrains decompiler
// Type: BuilderRED.Component
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
  internal class Component
  {
    internal static string AddComponent(string SystemID, int ComponentLink)
    {
      string str = "SELECT * FROM System_Component WHERE [SYS_COMP_BLDG_SYS_ID]='" + SystemID + "' AND [sys_comp_comp_link]=" + Conversions.ToString(ComponentLink);
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      string uniqueId;
      if (dataTable.Rows.Count > 0)
      {
        dataTable.Rows[0]["bred_status"] = (object) "U";
        uniqueId = Conversions.ToString(dataTable.Rows[0]["SYS_COMP_ID"]);
      }
      else
      {
        DataRow row = dataTable.NewRow();
        DataRow dataRow = row;
        dataRow["sys_comp_bldg_sys_id"] = (object) SystemID;
        uniqueId = mdUtility.GetUniqueID();
        dataRow["sys_comp_id"] = (object) uniqueId;
        dataRow["sys_comp_comp_link"] = (object) ComponentLink;
        dataRow["bred_status"] = (object) "N";
        dataTable.Rows.Add(row);
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
      return uniqueId;
    }

    internal static void DeleteComponent(string strComponentID)
    {
      try
      {
        DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT [SEC_ID] FROM Component_Section WHERE [sec_sys_comp_id]='" + strComponentID + "'");
        try
        {
          foreach (DataRow row in dataTable1.Rows)
            Section.DeleteSection(row["SEC_ID"].ToString());
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        string str = "SELECT * FROM system_component WHERE [sys_comp_id]='" + strComponentID + "'";
        DataTable dataTable2 = mdUtility.DB.GetDataTable(str);
        if (dataTable2.Rows.Count > 0)
        {
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["bred_status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable2.Rows[0]["bred_status"], (object) "N", false))
            dataTable2.Rows[0]["bred_status"] = (object) "D";
          else
            dataTable2.Rows[0].Delete();
          mdUtility.DB.SaveDataTable(ref dataTable2, str);
        }
        if (mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponentID) != null)
        {
          mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponentID).Nodes.Clear();
          mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponentID).Parent.Nodes.Remove(mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponentID));
        }
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strComponentID) != null)
        {
          frmMain fMainForm = mdUtility.fMainForm;
          UltraTreeNode nodeByKey = mdUtility.fMainForm.tvInspection.GetNodeByKey(strComponentID);
          ref UltraTreeNode local = ref nodeByKey;
          fMainForm.PurgeInspectionNode(ref local);
        }
        string[] strArray = new string[1];
        foreach (UltraTreeNode node in mdUtility.fMainForm.tvInspection.Nodes)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(node.Tag, (object) strComponentID, false))
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
            UltraTreeNode nodeByKey = mdUtility.fMainForm.tvInspection.GetNodeByKey(strArray[index]);
            while (nodeByKey.Nodes.Count > 0)
              mdUtility.fMainForm.tvInspection.Nodes.Remove(nodeByKey.Nodes[0]);
            mdUtility.fMainForm.tvInspection.Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(nodeByKey.Key));
            checked { ++index; }
          }
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
    }

    internal static int ComponentLink(string ComponentID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT SYS_COMP_COMP_LINK FROM System_Component WHERE [SYS_COMP_ID]='" + ComponentID + "'");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(dataTable.Rows[0]["SYS_COMP_COMP_LINK"].ToString());
      return -1;
    }

    internal static string BuildingSystem(string ComponentID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT SYS_COMP_BLDG_SYS_ID FROM System_Component WHERE [SYS_COMP_ID]='" + ComponentID + "'");
      if (dataTable.Rows.Count > 0)
        return dataTable.Rows[0]["SYS_COMP_BLDG_SYS_ID"].ToString();
      return "";
    }

    internal static string Description(string ComponentID)
    {
      DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT SYS_COMP_COMP_LINK FROM System_Component WHERE [Sys_Comp_ID]='" + ComponentID + "'");
      if (dataTable1.Rows.Count > 0)
      {
        DataTable dataTable2 = mdUtility.DB.GetDataTable(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT COMP_DESC FROM RO_Component WHERE [COMP_ID]=", dataTable1.Rows[0]["SYS_COMP_COMP_LINK"])));
        if (dataTable2.Rows.Count > 0)
          return dataTable2.Rows[0]["COMP_DESC"].ToString();
      }
      return "";
    }
  }
}
