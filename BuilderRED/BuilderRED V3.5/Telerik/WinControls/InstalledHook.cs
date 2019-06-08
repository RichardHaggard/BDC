// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.InstalledHook
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  [Flags]
  public enum InstalledHook
  {
    None = 0,
    GetMessage = 1,
    CallWndProc = 2,
    SystemMessage = 4,
    All = SystemMessage | CallWndProc | GetMessage, // 0x00000007
  }
}
