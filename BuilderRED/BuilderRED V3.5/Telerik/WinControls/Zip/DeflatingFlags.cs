// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.DeflatingFlags
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  [Flags]
  internal enum DeflatingFlags : ushort
  {
    Normal = 0,
    Maximum = 2,
    Fast = 4,
    SuperFast = Fast | Maximum, // 0x0006
  }
}
