// Decompiled with JetBrains decompiler
// Type: BuilderRED.EfficiencyAssessment
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Data;
using System.Windows.Forms;

namespace BuilderRED
{
  public class EfficiencyAssessment
  {
    public static void CreateEntry(string Building_ID, string Section_ID)
    {
      try
      {
        string str = "SELECT * FROM [Efficiency_Assessment] WHERE [EA_Section_ID] = '" + Section_ID + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count != 0)
          return;
        DataRow row = dataTable.NewRow();
        DataRow dataRow = row;
        dataRow[nameof (Building_ID)] = (object) Building_ID;
        dataRow["EA_Section_ID"] = (object) Section_ID;
        dataRow["EfficiencyAssessment_ID"] = (object) mdUtility.GetUniqueID();
        dataTable.Rows.Add(row);
        mdUtility.DB.SaveDataTable(ref dataTable, str);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        int num = (int) MessageBox.Show("Create Efficiency Assessment Failed");
        ProjectData.ClearProjectError();
      }
    }

    public static void DeleteEntry(string Section_ID)
    {
      try
      {
        string str = "SELECT * FROM [Efficiency_Assessment] WHERE [EA_Section_ID] = '" + Section_ID + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count <= 0)
          return;
        dataTable.Rows[0].Delete();
        mdUtility.DB.SaveDataTable(ref dataTable, str);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        int num = (int) MessageBox.Show("Delete Efficiency Assessment Failed");
        ProjectData.ClearProjectError();
      }
    }

    public static bool IsSectionEfficient(Guid sectionId)
    {
      bool flag;
      try
      {
        flag = mdUtility.DB.GetDataTable("SELECT * FROM [Efficiency_Assessment] WHERE [EA_Section_ID] = '" + sectionId.ToString() + "'").Rows.Count <= 0;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        int num = (int) MessageBox.Show("Check for Efficiency Assessment Failed");
        ProjectData.ClearProjectError();
      }
      return flag;
    }
  }
}
