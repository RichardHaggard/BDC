// Decompiled with JetBrains decompiler
// Type: BuilderRED.mdHierarchyFunction
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
  [StandardModule]
  internal sealed class mdHierarchyFunction
  {
    internal static void LoadInspectionTree()
    {
      mdUtility.fMainForm.tvInspection.Nodes.Clear();
      mdUtility.fMainForm.tvInspection.Refresh();
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [Facility_ID], [Number], [Name] FROM Facility WHERE bred_status <> 'D' OR bred_status IS NULL ORDER BY Number");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvInspection.Nodes.Add(row["Facility_ID"].ToString(), Building.LabelByID(row["Facility_ID"].ToString()));
          ultraTreeNode.Tag = (object) "Building";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.fMainForm.Refresh();
    }

    internal static void LoadInspectionComponents(string strLocation, string strKey)
    {
      if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey + "1") != null)
        mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey + "1"));
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strLocation, "System", false) == 0)
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [sys_comp_id], [comp_desc] FROM Components_By_System WHERE [sys_comp_bldg_sys_id]={" + strKey + "} ORDER BY comp_desc");
        try
        {
          foreach (DataRow row in dataTable.Rows)
          {
            UltraTreeNode ultraTreeNode;
            try
            {
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add(row["sys_comp_id"].ToString(), Conversions.ToString(row["comp_desc"]));
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              Exception exception = ex;
              if (Information.Err().Number != 35602)
                throw exception;
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add(Conversions.ToString(row["sys_comp_id"]), Conversions.ToString(row["comp_desc"]));
              ProjectData.ClearProjectError();
            }
            ultraTreeNode.Tag = (object) "Component";
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      else
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [sys_comp_id], [comp_desc] FROM components_by_location WHERE [samp_data_loc]={" + Strings.Replace(strLocation, "'", "''", 1, -1, CompareMethod.Binary) + "} AND [Facility_ID]={" + mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Parent.Parent.Key + "} AND [insp_data_samp]=true AND (bred_status <> 'D' OR bred_status IS NULL) ORDER BY comp_desc");
        try
        {
          foreach (DataRow row in dataTable.Rows)
          {
            UltraTreeNode ultraTreeNode;
            try
            {
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add("C-" + mdUtility.NewRandomNumberString() + row["sys_comp_id"].ToString(), Conversions.ToString(row["comp_desc"]));
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              Exception exception = ex;
              if (Information.Err().Number != 35602)
                throw exception;
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add("C-" + mdUtility.NewRandomNumberString() + row["sys_comp_id"].ToString(), Conversions.ToString(row["comp_desc"]));
              ProjectData.ClearProjectError();
            }
            ultraTreeNode.Tag = RuntimeHelpers.GetObjectValue(row["sys_comp_id"]);
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
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

    internal static void LoadInspectionLocations(string strBuilding)
    {
      try
      {
        if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuilding + "1") != null)
          mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuilding).Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuilding + "1"));
        UltraTreeNode ultraTreeNode1 = mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuilding).Nodes.Add("L-" + strBuilding + "Non-sampling", "By System");
        ultraTreeNode1.Tag = (object) "Non-sampling";
        ultraTreeNode1.Nodes.Add(ultraTreeNode1.Key + "1", "Temp");
        UltraTreeNode ultraTreeNode2 = mdUtility.fMainForm.tvInspection.GetNodeByKey(strBuilding).Nodes.Add("L-" + strBuilding + "Sample Locations", "By Sample Location");
        ultraTreeNode2.Tag = (object) "Sample Locations";
        ultraTreeNode2.Nodes.Add(ultraTreeNode2.Key + "1", "Temp");
      }
      finally
      {
      }
    }

    internal static void LoadInspectionSample(string strLocation)
    {
      if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strLocation) != null)
        mdUtility.fMainForm.tvInspection.GetNodeByKey(strLocation).Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(strLocation + "1"));
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT DISTINCT [Location_ID], [Name] FROM Sample_Location WHERE [Building_ID]='" + mdUtility.fMainForm.tvInspection.GetNodeByKey(strLocation).Parent.Key + "' ORDER BY [Name]");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["Name"])))
          {
            UltraTreeNode ultraTreeNode;
            try
            {
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strLocation).Nodes.Add("L-" + mdUtility.NewRandomNumberString() + row["Location_ID"].ToString(), Conversions.ToString(row["Name"]));
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strLocation).Nodes.Add("L-" + mdUtility.NewRandomNumberString() + row["Location_ID"].ToString(), Conversions.ToString(row["Name"]));
              ProjectData.ClearProjectError();
            }
            ultraTreeNode.Tag = RuntimeHelpers.GetObjectValue(row["Location_ID"]);
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
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

    internal static void LoadInspectionSections(string strComponentID, string strKey)
    {
      if (mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey + "1") != null)
        mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey + "1"));
      string sSQL;
      bool flag;
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Parent.Parent.Tag, (object) "Non-sampling", false))
      {
        sSQL = "SELECT * FROM Sections_by_system WHERE [SEC_SYS_COMP_ID]={" + strComponentID + "} AND (bred_status <> 'D' OR bred_status IS NULL)";
        flag = true;
      }
      else
      {
        sSQL = "SELECT * FROM Sections_by_location WHERE [SEC_SYS_COMP_ID]={" + strComponentID + "} AND [samp_data_loc]={" + mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Parent.Tag.ToString() + "} AND (bred_status <> 'D' OR bred_status IS NULL)";
        flag = false;
      }
      DataTable dataTable = mdUtility.DB.GetDataTable(sSQL);
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode;
          if (flag)
          {
            ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add(row["sec_id"].ToString(), Section.SectionLabel(row["sec_id"].ToString()));
            ultraTreeNode.Tag = (object) "Section";
          }
          else
          {
            try
            {
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add("S-" + mdUtility.NewRandomNumberString() + row["sec_id"].ToString(), Section.SectionLabel(row["sec_id"].ToString()));
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              Exception exception = ex;
              if (!(Information.Err().Number == 35602 & Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Information.Err().Description, "Key is not unique in collection", false) == 0))
                throw exception;
              ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Nodes.Add(Conversions.ToString(row["sec_id"]), Section.SectionLabel(Conversions.ToString(row["sec_id"])));
              ProjectData.ClearProjectError();
            }
            ultraTreeNode.Tag = RuntimeHelpers.GetObjectValue(row["sec_id"]);
          }
          if (flag)
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    internal static void LoadInventoryComponents(string strSystem)
    {
      if (mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystem + "1") != null)
        mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystem).Nodes.Remove(mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystem + "1"));
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [sys_comp_id], comp_desc FROM components_by_system WHERE [sys_comp_bldg_sys_id]={" + strSystem + "} AND (bred_status <> 'D' OR bred_status IS NULL) ORDER BY comp_desc");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvInventory.GetNodeByKey(strSystem).Nodes.Add(row["sys_comp_id"].ToString(), Conversions.ToString(row["comp_desc"]));
          ultraTreeNode.Tag = (object) "Component";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    internal static void LoadInventorySections(string strComponent)
    {
      if (mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponent + "1") != null)
        mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponent).Nodes.Remove(mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponent + "1"));
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT sec_id, SectionName FROM Sections_by_system WHERE [sec_sys_comp_id]={" + strComponent + "} AND (bred_status <> 'D' OR bred_status IS NULL)");
      try
      {
        foreach (DataRow row in dataTable.Rows)
          mdUtility.fMainForm.tvInventory.GetNodeByKey(strComponent).Nodes.Add(row["sec_id"].ToString(), Conversions.ToString(row["SectionName"])).Tag = (object) "Section";
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    internal static void LoadInventorySystems(string strBuilding)
    {
      if (mdUtility.fMainForm.tvInventory.GetNodeByKey(strBuilding + "1") != null)
        mdUtility.fMainForm.tvInventory.Nodes[strBuilding].Nodes.Remove(mdUtility.fMainForm.tvInventory.GetNodeByKey(strBuilding + "1"));
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [bldg_sys_id], sys_desc FROM Systems WHERE [bldg_sys_bldg_id]={" + strBuilding + "} AND (bred_status <> 'D' OR bred_status IS NULL) ORDER BY sys_desc");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvInventory.Nodes[strBuilding].Nodes.Add(row["bldg_sys_id"].ToString(), Conversions.ToString(row["sys_desc"]));
          ultraTreeNode.Tag = (object) "System";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    internal static void LoadInventoryTree()
    {
      mdUtility.fMainForm.tvInventory.Nodes.Clear();
      mdUtility.fMainForm.tvInventory.Refresh();
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [Facility_ID], [Number], [Name] FROM Facility WHERE bred_status <> 'D' OR bred_status IS NULL ORDER BY Number, Name");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvInventory.Nodes.Add(row["Facility_ID"].ToString(), Building.Label(RuntimeHelpers.GetObjectValue(row["Number"]), RuntimeHelpers.GetObjectValue(row["Name"])));
          ultraTreeNode.Tag = (object) "Building";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.fMainForm.Refresh();
    }

    internal static void LoadFunctionalAreas(string strBuilding)
    {
      if (mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strBuilding + "1") != null)
        mdUtility.fMainForm.tvFunctionality.Nodes[strBuilding].Nodes.Remove(mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strBuilding + "1"));
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [Name], [Area_ID] FROM Functional_Area WHERE BLDG_ID={" + strBuilding + "} AND (bred_status <> 'D' OR bred_status IS NULL)");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvFunctionality.Nodes[strBuilding].Nodes.Add(row["Name"].ToString());
          ultraTreeNode.Tag = (object) "FuncArea";
          ultraTreeNode.Key = row["Area_ID"].ToString();
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    internal static void LoadFunctionalityTree()
    {
      mdUtility.fMainForm.tvFunctionality.Nodes.Clear();
      mdUtility.fMainForm.tvFunctionality.Refresh();
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [Facility_ID], [Number], [Name] FROM Facility WHERE bred_status <> 'D' OR bred_status IS NULL ORDER BY Number, Name");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvFunctionality.Nodes.Add(row["Facility_ID"].ToString(), Building.Label(RuntimeHelpers.GetObjectValue(row["Number"]), RuntimeHelpers.GetObjectValue(row["Name"])));
          ultraTreeNode.Tag = (object) "Building";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.fMainForm.Refresh();
    }

    internal static void LoadFunctionalityAssessments()
    {
    }

    internal static void LoadInspectionSystems(string Node, string Tag)
    {
      if (mdUtility.fMainForm.tvInspection.GetNodeByKey(Node + "1") != null)
        mdUtility.fMainForm.tvInspection.GetNodeByKey(Node).Nodes.Remove(mdUtility.fMainForm.tvInspection.GetNodeByKey(Node + "1"));
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT [bldg_sys_id], sys_desc FROM Systems WHERE [bldg_sys_bldg_id]={" + mdUtility.fMainForm.tvInspection.GetNodeByKey(Node).Parent.Key + "} AND (bred_status <> 'D' OR bred_status IS NULL) ORDER BY sys_desc");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(Node).Nodes.Add(row["bldg_sys_id"].ToString(), Conversions.ToString(row["sys_desc"]));
          ultraTreeNode.Tag = (object) "System";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    internal static void LoadInspectionSamples(string SectionID, string Node)
    {
      mdUtility.fMainForm.tvInspection.GetNodeByKey(Node).Nodes.Clear();
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT DISTINCT SAMP_DATA_LOC, Location FROM samples_by_sections WHERE [SEC_ID]={" + SectionID + "} ORDER BY Location");
      try
      {
        foreach (DataRow row in dataTable.Rows)
        {
          UltraTreeNode ultraTreeNode;
          try
          {
            ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(Node).Nodes.Add("L-" + mdUtility.NewRandomNumberString() + row["SAMP_DATA_LOC"].ToString(), Conversions.ToString(row["Location"]));
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            Exception exception = ex;
            if (!(Information.Err().Number == 35602 & Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Information.Err().Description, "Key is not unique in collection", false) == 0))
              throw exception;
            ultraTreeNode = mdUtility.fMainForm.tvInspection.GetNodeByKey(Node).Nodes.Add(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) ("S-" + mdUtility.NewRandomNumberString()), row["SAMP_DATA_LOC"])), Conversions.ToString(row["Location"]));
            ProjectData.ClearProjectError();
          }
          ultraTreeNode.Tag = RuntimeHelpers.GetObjectValue(row["SAMP_DATA_LOC"]);
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
