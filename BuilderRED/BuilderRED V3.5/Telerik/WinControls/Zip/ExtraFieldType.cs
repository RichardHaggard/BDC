// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ExtraFieldType
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal enum ExtraFieldType
  {
    Unknown = -1,
    Zip64 = 1,
    Ntfs = 10, // 0x0000000A
    StrongEncryption = 23, // 0x00000017
    UnixTime = 21589, // 0x00005455
    AesEncryption = 39169, // 0x00009901
  }
}
