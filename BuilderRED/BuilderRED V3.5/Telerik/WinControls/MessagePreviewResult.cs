// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.MessagePreviewResult
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  [Flags]
  public enum MessagePreviewResult
  {
    NotProcessed = 0,
    Processed = 1,
    NoDispatch = 2,
    NoContinue = 4,
    ProcessedNoDispatch = NoDispatch | Processed, // 0x00000003
    All = ProcessedNoDispatch | NoContinue, // 0x00000007
  }
}
