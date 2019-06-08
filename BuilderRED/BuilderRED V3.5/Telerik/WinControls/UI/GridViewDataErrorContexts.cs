// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDataErrorContexts
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  [Flags]
  public enum GridViewDataErrorContexts
  {
    ClipboardContent = 16384, // 0x00004000
    Commit = 512, // 0x00000200
    CurrentCellChange = 4096, // 0x00001000
    Display = 2,
    Formatting = 1,
    InitialValueRestoration = 1024, // 0x00000400
    LeaveControl = 2048, // 0x00000800
    Parsing = 256, // 0x00000100
    PreferredSize = 4,
    RowDeletion = 8,
    Scroll = 8192, // 0x00002000
  }
}
