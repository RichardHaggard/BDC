// Decompiled with JetBrains decompiler
// Type: BuilderRED.MissingComponents
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using BuilderRED.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Data;
using System.Windows.Forms;

namespace BuilderRED
{
  public class MissingComponents
  {
    internal static string AddMissingComponent(
      string sBldg,
      int iCMC_ID,
      double dQuantity,
      bool bMissionCritical,
      string sComments,
      double dCoverage = 0.0)
    {
      string str1 = "";
      try
      {
        string str2 = "SELECT * FROM Missing_Components WHERE [Building_ID]='" + sBldg + "' AND [MC_CMC_ID] =" + Conversions.ToString(iCMC_ID) ?? "";
        DataTable dataTable = mdUtility.DB.GetDataTable(str2);
        if (dataTable.Rows.Count > 0)
        {
          MyProject.Forms.frmMissingComponents.Enabled = false;
          if (Interaction.MsgBox((object) "An entry for this component already exists.  Overwrite?", MsgBoxStyle.YesNoCancel, (object) null) == MsgBoxResult.Yes)
          {
            DataRow row = dataTable.Rows[0];
            row["MC_Quantity"] = (object) dQuantity;
            row["MC_Comments"] = (object) sComments;
            row["MC_MissionCritical"] = (object) bMissionCritical;
            if (dCoverage != 0.0)
              row["MC_Coverage"] = (object) dCoverage;
            str1 = Conversions.ToString(row["MissingComponent_ID"]);
            mdUtility.DB.SaveDataTable(ref dataTable, str2);
          }
        }
        else
        {
          DataRow row = dataTable.NewRow();
          str1 = mdUtility.GetUniqueID();
          DataRow dataRow = row;
          dataRow["Building_ID"] = (object) sBldg;
          dataRow["MissingComponent_ID"] = (object) str1;
          dataRow["MC_CMC_ID"] = (object) iCMC_ID;
          dataRow["MC_Quantity"] = (object) dQuantity;
          dataRow["MC_MissionCritical"] = (object) bMissionCritical;
          dataRow["MC_Comments"] = (object) sComments;
          if (dCoverage != 0.0)
            dataRow["MC_Coverage"] = (object) dCoverage;
          dataTable.Rows.Add(row);
          mdUtility.DB.SaveDataTable(ref dataTable, str2);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        int num = (int) MessageBox.Show("Missing Component add failed");
        ProjectData.ClearProjectError();
      }
      return str1;
    }

    public static bool OkToSaveComments(ref string strComment)
    {
      bool flag = true;
      try
      {
        if (Strings.InStr(strComment, "'", CompareMethod.Binary) > 0)
        {
          if (MessageBox.Show("You cannot have a \"'\" in the comment; BUILDER doesn't allow this character.\r\nOkay to remove this character from the comment?", "Invalid character found", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
          {
            int num = (int) MessageBox.Show("", "Not Saved, Invalid character found", MessageBoxButtons.OK);
            flag = false;
          }
          else
          {
            strComment = Strings.Replace(strComment, "'", "", 1, -1, CompareMethod.Binary);
            flag = true;
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, "Missing Component", "SaveComments");
        ProjectData.ClearProjectError();
      }
      return flag;
    }
  }
}
