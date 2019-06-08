// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BestFitColumnMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public enum BestFitColumnMode
  {
    None = 1,
    DisplayedDataCells = 2,
    HeaderCells = 4,
    FilterCells = 8,
    SummaryRowCells = 16, // 0x00000010
    SystemCells = 28, // 0x0000001C
    DisplayedCells = 30, // 0x0000001E
    AllCells = 62, // 0x0000003E
  }
}
