// Decompiled with JetBrains decompiler
// Type: BuilderRED.SectionDetail
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
  internal class SectionDetail
  {
    internal static void DeleteSectionDetail(string strSectionDetailID)
    {
      try
      {
        string str = "SELECT * FROM SectionDetails WHERE [sd_id]='" + strSectionDetailID + "'";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count == 0)
          throw new Exception("Unable to delete the section detail.  Section Detail was not found.");
        DataRow row = dataTable.Rows[0];
        if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(row["BRED_Status"], (object) "N", false))
          row["BRED_Status"] = (object) "D";
        else
          row.Delete();
        mdUtility.DB.SaveDataTable(ref dataTable, str);
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
  }
}
