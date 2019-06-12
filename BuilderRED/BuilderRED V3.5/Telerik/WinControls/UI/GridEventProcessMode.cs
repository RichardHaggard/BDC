// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridEventProcessMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  [Flags]
  public enum GridEventProcessMode
  {
    Process = 1,
    PreProcess = 2,
    PostProcess = 4,
    AnalyzeQueue = 8,
    AllProcess = PostProcess | PreProcess | Process, // 0x00000007
    All = AllProcess | AnalyzeQueue, // 0x0000000F
  }
}
