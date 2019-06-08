// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.DataCapacityIndexer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class DataCapacityIndexer
  {
    public int Version { get; set; }

    public ErrorCorrectionLevel ErrorCorrection { get; set; }

    public DataCapacityIndexer(int codeVersion, ErrorCorrectionLevel errorCorrectionLevel)
    {
      this.Version = codeVersion;
      this.ErrorCorrection = errorCorrectionLevel;
    }

    public override bool Equals(object obj)
    {
      return this.Equals(obj as DataCapacityIndexer);
    }

    public bool Equals(DataCapacityIndexer obj)
    {
      if (obj.Version == this.Version)
        return obj.ErrorCorrection == this.ErrorCorrection;
      return false;
    }

    public override int GetHashCode()
    {
      return this.Version * Convert.ToInt32((object) this.ErrorCorrection);
    }
  }
}
