// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.GeneralPurposeBitFlag
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  [Flags]
  internal enum GeneralPurposeBitFlag : ushort
  {
    FileIsEncrypted = 1,
    ZeroLocalHeader = 8,
    ReservedForEnhancedDeflating = 16, // 0x0010
    CompressedPatchedData = 32, // 0x0020
    StrongEncryption = 64, // 0x0040
    EncodingUtf8 = 2048, // 0x0800
    HideLocalHeader = 8192, // 0x2000
  }
}
