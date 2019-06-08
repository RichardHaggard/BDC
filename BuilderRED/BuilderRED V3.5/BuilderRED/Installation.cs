// Decompiled with JetBrains decompiler
// Type: BuilderRED.Installation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic.CompilerServices;
using System.Data;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class Installation
  {
    internal static int HVACZone(ref string InstallationID)
    {
      DataTable dataTable = mdUtility.DB.GetDataTable("SELECT * FROM InstallationBuilding WHERE ORG_ID='" + InstallationID + "'");
      if (dataTable.Rows.Count > 0)
        return Conversions.ToInteger(dataTable.Rows[0]["HVAC_ZONE"]);
      return 0;
    }
  }
}
